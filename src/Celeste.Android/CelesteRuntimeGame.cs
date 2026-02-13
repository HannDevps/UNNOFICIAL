using System;
using System.Diagnostics;
using System.Globalization;
using Celeste.Android.Platform.Configuration;
using Celeste.Android.Platform.Diagnostics;
using Celeste.Android.Platform.Fullscreen;
using Celeste.Android.Platform.Lifecycle;
using Celeste.Android.Platform.Rendering;
using Celeste.Core.Platform.Interop;
using Celeste.Core.Platform.Logging;
using Celeste.Core.Platform.Runtime;
using Celeste.Core.Platform.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste.Android;

public sealed class CelesteRuntimeGame : global::Celeste.Celeste, IAndroidGameLifecycle
{
    private readonly PlatformServices _services;
    private readonly ImmersiveFullscreenController _fullscreen;
    private readonly global::Android.App.Activity _activity;
    private readonly string _activeAbi;
    private readonly bool _lowMemoryMode;
    private readonly bool _aggressiveGc;
    private readonly RuntimeGameConfigSource _gameConfigSource;
    private readonly RuntimeOverlayConfigSource _overlayConfigSource;
    private readonly BitmapFallbackFont _overlayFallbackFont = new();

    private DateTime _lastHeartbeatUtc;
    private DateTime _lastGcUtc;
    private DateTime _lastEmergencyGcUtc;
    private DateTime _lastConfigPollUtc;
    private DateTime _lastOverlaySnapshotUtc;
    private string _lastSceneName = "null";
    private RuntimeGameConfig _gameConfig;
    private RuntimeOverlayConfig _overlayConfig;
    private string[] _overlayLines = Array.Empty<string>();
    private SpriteBatch? _overlaySpriteBatch;
    private SpriteFont? _overlayFont;
    private Texture2D? _overlayPixel;
    private bool _graphicsConfigApplied;
    private int _lastAppliedBackBufferWidth;
    private int _lastAppliedBackBufferHeight;
    private string _lastGraphicsConfigFingerprint = string.Empty;

    public CelesteRuntimeGame(PlatformServices services, ImmersiveFullscreenController fullscreen, global::Android.App.Activity activity, string activeAbi)
    {
        _services = services;
        _fullscreen = fullscreen;
        _activity = activity;
        _activeAbi = activeAbi;
        _lowMemoryMode = AndroidRuntimePolicy.IsLowMemoryModeEnabled();
        _aggressiveGc = AndroidRuntimePolicy.IsAggressiveGarbageCollectionEnabled();
        _gameConfigSource = new RuntimeGameConfigSource(_services.Paths, _services.Logger);
        _overlayConfigSource = new RuntimeOverlayConfigSource(_services.Paths, _services.Logger);
        _gameConfig = _gameConfigSource.Current;
        _overlayConfig = _overlayConfigSource.Current;

        AppContext.SetSwitch(AndroidRuntimePolicy.ForceLegacyBlendStateSwitch, _gameConfig.ForceLegacyBlendStates);
        ApplyRuntimeTuning("CTOR");

        _services.Logger.Log(LogLevel.Info, "RUNTIME", "CelesteRuntimeGame created", context: BuildRuntimeContext("CTOR"));
        _services.Logger.Log(LogLevel.Info, "CONFIG", "Runtime game config source", context: "path=" + _gameConfigSource.ActivePath);
        _services.Logger.Log(LogLevel.Info, "OVERLAY", "Runtime overlay config source", context: "path=" + _overlayConfigSource.ActivePath);
        ConfigureRuntimeMenuBridge();
        AndroidCrashReporter.LogMemoryPressure(_services.Logger, "Runtime game created", BuildRuntimeContext("CTOR_MEMORY"), LogLevel.Info);
    }

    protected override void Initialize()
    {
        try
        {
            _services.Logger.Log(LogLevel.Info, "RUNTIME", $"Initialize runtime ABI={_activeAbi}", context: BuildRuntimeContext("INITIALIZE_BEGIN"));
            ReloadExternalConfigs(force: true, context: "Initialize");
            base.Initialize();
            ApplyGraphicsConfiguration("Initialize");
            ApplyRuntimeTuning("INITIALIZE");
            _services.Logger.Log(LogLevel.Info, "RUNTIME", "Initialize runtime completed", context: BuildRuntimeContext("INITIALIZE_DONE"));
        }
        catch (Exception exception)
        {
            _services.Logger.Log(LogLevel.Error, "RUNTIME", "Initialize runtime failed", exception, BuildRuntimeContext("INITIALIZE_FAIL"));
            _services.Logger.Flush();
            throw;
        }
    }

    protected override void LoadContent()
    {
        try
        {
            _services.Logger.Log(LogLevel.Info, "RUNTIME", "LoadContent begin", context: BuildRuntimeContext("LOADCONTENT_BEGIN"));
            base.LoadContent();

            _overlaySpriteBatch = new SpriteBatch(GraphicsDevice);
            _overlayPixel = new Texture2D(GraphicsDevice, 1, 1);
            _overlayPixel.SetData(new[] { Color.White });
            try
            {
                _overlayFont = Content.Load<SpriteFont>("ErrorFont");
            }
            catch (Exception exception)
            {
                _overlayFont = null;
                _services.Logger.Log(LogLevel.Warn, "OVERLAY", "Failed to load ErrorFont for runtime overlay. Fallback bitmap font will be used.", exception);
            }

            RefreshOverlaySnapshot(force: true);
            _services.Logger.Log(LogLevel.Info, "RUNTIME", "LoadContent done", context: BuildRuntimeContext("LOADCONTENT_DONE"));
            AndroidCrashReporter.LogMemoryPressure(_services.Logger, "Runtime LoadContent done", BuildRuntimeContext("LOADCONTENT_MEMORY"), LogLevel.Info);
        }
        catch (Exception exception)
        {
            _services.Logger.Log(LogLevel.Error, "RUNTIME", "LoadContent failed", exception, BuildRuntimeContext("LOADCONTENT_FAIL"));
            _services.Logger.Flush();
            throw;
        }
    }

    protected override void Update(GameTime gameTime)
    {
        try
        {
            PollConfigHotReload();
            base.Update(gameTime);
            TrackSceneChanges();
            ApplySceneQualityTuning();
            EmitRuntimeHeartbeat();
            RefreshOverlaySnapshot(force: false);
        }
        catch (Exception exception)
        {
            _services.Logger.Log(LogLevel.Error, "RUNTIME", "Unhandled exception in runtime Update", exception, BuildRuntimeContext("UPDATE_FAIL"));
            _services.Logger.Flush();
            throw;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        try
        {
            base.Draw(gameTime);
            DrawRuntimeOverlay();
        }
        catch (Exception exception)
        {
            _services.Logger.Log(LogLevel.Error, "RUNTIME", "Unhandled exception in runtime Draw", exception, BuildRuntimeContext("DRAW_FAIL"));
            _services.Logger.Flush();
            throw;
        }
    }

    protected override void OnSceneTransition(Scene last, Scene next)
    {
        var fromScene = last?.GetType().FullName ?? "null";
        var toScene = next?.GetType().FullName ?? "null";

        _services.Logger.Log(LogLevel.Info, "SCENE", "Scene transition begin", context: $"from={fromScene}; to={toScene}; {BuildRuntimeContext("SCENE_TRANSITION")}");

        try
        {
            if (_aggressiveGc)
            {
                TryConservativeGcHint("scene_transition");
            }

            AndroidCrashReporter.LogMemoryPressure(_services.Logger, "Runtime scene transition", $"from={fromScene}; to={toScene}; {BuildRuntimeContext("SCENE_TRANSITION_MEMORY")}", LogLevel.Info);
            base.OnSceneTransition(last, next);
            _services.Logger.Log(LogLevel.Info, "SCENE", "Scene transition done", context: $"from={fromScene}; to={toScene}");
        }
        catch (Exception exception)
        {
            _services.Logger.Log(LogLevel.Error, "SCENE", "Scene transition failed", exception, $"from={fromScene}; to={toScene}; {BuildRuntimeContext("SCENE_TRANSITION_FAIL")}");
            _services.Logger.Flush();
            throw;
        }
    }

    public void HandlePause()
    {
        _services.Logger.Log(LogLevel.Info, "LIFECYCLE", "Runtime HandlePause", context: BuildRuntimeContext("HANDLE_PAUSE"));
        ReloadExternalConfigs(force: true, context: "HandlePause");
        global::Celeste.Audio.PauseMusic = true;
        global::Celeste.Audio.PauseGameplaySfx = true;
        global::Celeste.Audio.PauseUISfx = true;
    }

    public void HandleResume()
    {
        _services.Logger.Log(LogLevel.Info, "LIFECYCLE", "Runtime HandleResume", context: BuildRuntimeContext("HANDLE_RESUME"));
        ReloadExternalConfigs(force: true, context: "HandleResume");
        ApplyGraphicsConfiguration("HandleResume");
        ApplyRuntimeTuning("HANDLE_RESUME");
        _fullscreen.Apply(_activity, "Runtime-HandleResume");
        global::Celeste.Audio.PauseMusic = false;
        global::Celeste.Audio.PauseGameplaySfx = false;
        global::Celeste.Audio.PauseUISfx = false;
        RefreshOverlaySnapshot(force: true);
        AndroidCrashReporter.LogMemoryPressure(_services.Logger, "Runtime resumed", BuildRuntimeContext("HANDLE_RESUME_MEMORY"), LogLevel.Info);
    }

    public void HandleFocusChanged(bool hasFocus)
    {
        _services.Logger.Log(LogLevel.Info, "LIFECYCLE", $"Runtime HandleFocusChanged={hasFocus}", context: BuildRuntimeContext("HANDLE_FOCUS"));
        if (hasFocus)
        {
            _fullscreen.Apply(_activity, "Runtime-HandleFocusChanged");
        }
    }

    public void HandleLowMemory()
    {
        _services.Logger.Log(LogLevel.Warn, "MEMORY", "Runtime HandleLowMemory", context: BuildRuntimeContext("HANDLE_LOW_MEMORY"));
        TryEmergencyMemoryMitigation("low_memory", force: true);
    }

    public void HandleTrimMemory(int level, string levelName)
    {
        _services.Logger.Log(LogLevel.Warn, "MEMORY", "Runtime HandleTrimMemory", context: $"level={level}; levelName={levelName}; {BuildRuntimeContext("HANDLE_TRIM_MEMORY")}");
        if (ShouldTriggerEmergencyGc(level))
        {
            TryEmergencyMemoryMitigation($"trim_memory_{levelName}", force: level >= 15);
        }
    }

    public void HandleDestroy()
    {
        _services.Logger.Log(LogLevel.Info, "LIFECYCLE", "Runtime HandleDestroy", context: BuildRuntimeContext("HANDLE_DESTROY"));
        CelesteRuntimeConfigBridge.Clear();
        AndroidCrashReporter.LogMemoryPressure(_services.Logger, "Runtime destroy", BuildRuntimeContext("HANDLE_DESTROY_MEMORY"), LogLevel.Info);
        _services.Logger.Flush();
    }

    private void TrackSceneChanges()
    {
        var sceneName = Engine.Scene?.GetType().FullName ?? "null";
        if (string.Equals(sceneName, _lastSceneName, StringComparison.Ordinal))
        {
            return;
        }

        _lastSceneName = sceneName;
        _services.Logger.Log(LogLevel.Info, "SCENE", "Scene active", context: BuildRuntimeContext("SCENE_ACTIVE"));
    }

    private void EmitRuntimeHeartbeat()
    {
        var now = DateTime.UtcNow;
        var intervalSeconds = _lowMemoryMode ? 5 : 10;
        if (_lastHeartbeatUtc != DateTime.MinValue && (now - _lastHeartbeatUtc).TotalSeconds < intervalSeconds)
        {
            return;
        }

        _lastHeartbeatUtc = now;
        _services.Logger.Log(LogLevel.Info, "HEARTBEAT", "Runtime alive", context: BuildRuntimeContext("HEARTBEAT"));
        if (_lowMemoryMode)
        {
            AndroidCrashReporter.LogMemoryPressure(_services.Logger, "Runtime heartbeat memory", BuildRuntimeContext("HEARTBEAT_MEMORY"), LogLevel.Info);
        }
    }

    private void TryConservativeGcHint(string reason)
    {
        var now = DateTime.UtcNow;
        if (_lastGcUtc != DateTime.MinValue && (now - _lastGcUtc).TotalSeconds < 5)
        {
            return;
        }

        _lastGcUtc = now;
        var managedBefore = GC.GetTotalMemory(false);
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        var managedAfter = GC.GetTotalMemory(false);
        _services.Logger.Log(LogLevel.Info, "MEMORY", "Runtime GC hint applied", context: $"reason={reason}; managedBefore={managedBefore}; managedAfter={managedAfter}; gc0={GC.CollectionCount(0)}; gc1={GC.CollectionCount(1)}; gc2={GC.CollectionCount(2)}");
    }

    private bool ShouldTriggerEmergencyGc(int level)
    {
        return level >= 10 || _aggressiveGc || _lowMemoryMode;
    }

    private void TryEmergencyMemoryMitigation(string reason, bool force)
    {
        var now = DateTime.UtcNow;
        if (!force && _lastEmergencyGcUtc != DateTime.MinValue && (now - _lastEmergencyGcUtc).TotalSeconds < 2)
        {
            return;
        }

        _lastEmergencyGcUtc = now;
        var managedBefore = GC.GetTotalMemory(false);

        try
        {
            global::Celeste.Audio.ReleaseUnusedDescriptions();
        }
        catch (Exception exception)
        {
            _services.Logger.Log(LogLevel.Warn, "MEMORY", "ReleaseUnusedDescriptions failed during memory mitigation", exception);
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var managedAfter = GC.GetTotalMemory(false);
        _services.Logger.Log(LogLevel.Warn, "MEMORY", "Runtime emergency GC executed", context: $"reason={reason}; managedBefore={managedBefore}; managedAfter={managedAfter}; gc0={GC.CollectionCount(0)}; gc1={GC.CollectionCount(1)}; gc2={GC.CollectionCount(2)}");
    }

    private void PollConfigHotReload()
    {
        var now = DateTime.UtcNow;
        var intervalMs = _lowMemoryMode ? 1500 : 800;
        if (_lastConfigPollUtc != DateTime.MinValue && (now - _lastConfigPollUtc).TotalMilliseconds < intervalMs)
        {
            return;
        }

        _lastConfigPollUtc = now;
        bool gameChanged = _gameConfigSource.Reload(force: false, reason: "hot_reload");
        bool overlayChanged = _overlayConfigSource.Reload(force: false, reason: "hot_reload");
        if (!gameChanged && !overlayChanged)
        {
            return;
        }

        _gameConfig = _gameConfigSource.Current;
        _overlayConfig = _overlayConfigSource.Current;
        if (gameChanged)
        {
            if (_gameConfig.EnableDiagnosticLogs)
            {
                _services.Logger.Log(LogLevel.Info, "CONFIG", "Runtime game config hot-reloaded", context: "path=" + _gameConfigSource.ActivePath);
            }

            AppContext.SetSwitch(AndroidRuntimePolicy.ForceLegacyBlendStateSwitch, _gameConfig.ForceLegacyBlendStates);
            ApplyGraphicsConfiguration("HotReload");
            ApplyRuntimeTuning("HotReload");
        }

        if (overlayChanged && _gameConfig.EnableDiagnosticLogs)
        {
            _services.Logger.Log(LogLevel.Info, "OVERLAY", "Runtime overlay config hot-reloaded", context: "path=" + _overlayConfigSource.ActivePath);
        }

        RefreshOverlaySnapshot(force: true);
    }

    private void ReloadExternalConfigs(bool force, string context)
    {
        bool gameChanged = _gameConfigSource.Reload(force, context);
        if (gameChanged || force)
        {
            _gameConfig = _gameConfigSource.Current;
            if (_gameConfig.EnableDiagnosticLogs)
            {
                _services.Logger.Log(LogLevel.Info, "CONFIG", $"Runtime game config reloaded ({context})", context: $"path={_gameConfigSource.ActivePath}");
            }

            AppContext.SetSwitch(AndroidRuntimePolicy.ForceLegacyBlendStateSwitch, _gameConfig.ForceLegacyBlendStates);
        }

        bool overlayChanged = _overlayConfigSource.Reload(force, context);
        if (overlayChanged || force)
        {
            _overlayConfig = _overlayConfigSource.Current;
            if (_gameConfig.EnableDiagnosticLogs)
            {
                _services.Logger.Log(LogLevel.Info, "OVERLAY", $"Runtime overlay config reloaded ({context})", context: $"path={_overlayConfigSource.ActivePath}");
            }
        }
    }

    private void ApplyGraphicsConfiguration(string context)
    {
        var manager = Engine.Graphics;
        if (manager == null || manager.GraphicsDevice == null)
        {
            return;
        }

        var graphicsDevice = manager.GraphicsDevice;
        int width = ResolveBackBufferWidth(graphicsDevice);
        int height = ResolveBackBufferHeight(graphicsDevice);
        var profile = ResolveGraphicsProfile(_gameConfig.GraphicsProfile);
        var depthFormat = ResolveDepthFormat(_gameConfig.DepthStencil);

        _gameConfig.TryGetTargetFps(out int targetFps, out bool uncapped);
        var fp = $"{width}x{height}@vsync={_gameConfig.VSync};fps={_gameConfig.TargetFps};profile={profile};depth={depthFormat};aspect={_gameConfig.AspectMode};filter={_gameConfig.ScaleFilter};edge={_gameConfig.UseEdgeToEdgeOnAndroid}";
        if (_graphicsConfigApplied && string.Equals(fp, _lastGraphicsConfigFingerprint, StringComparison.Ordinal))
        {
            return;
        }

        manager.SynchronizeWithVerticalRetrace = _gameConfig.VSync;
        manager.GraphicsProfile = profile;
        manager.PreferredDepthStencilFormat = depthFormat;
        manager.PreferredBackBufferWidth = width;
        manager.PreferredBackBufferHeight = height;
        manager.ApplyChanges();

        if (uncapped)
        {
            IsFixedTimeStep = false;
        }
        else
        {
            IsFixedTimeStep = true;
            targetFps = Math.Clamp(targetFps, 15, 240);
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / targetFps);
        }

        Engine.ConfigurePresentation(
            ResolveAspectMode(_gameConfig.AspectMode),
            _gameConfig.ScaleFilter == RuntimeScaleFilters.Linear,
            _gameConfig.UseEdgeToEdgeOnAndroid);

        _lastAppliedBackBufferWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
        _lastAppliedBackBufferHeight = graphicsDevice.PresentationParameters.BackBufferHeight;
        _graphicsConfigApplied = true;
        _lastGraphicsConfigFingerprint = fp;

        if (_gameConfig.EnableDiagnosticLogs)
        {
            _services.Logger.Log(
                LogLevel.Info,
                "GRAPHICS",
                $"Runtime graphics applied ({context})",
                context: $"fp={fp}; fixedStep={IsFixedTimeStep}; elapsed={TargetElapsedTime.TotalMilliseconds:0.###}ms; appliedBackBuffer={_lastAppliedBackBufferWidth}x{_lastAppliedBackBufferHeight}");
        }
    }

    private int ResolveBackBufferWidth(GraphicsDevice graphicsDevice)
    {
        if (!_gameConfig.BackBufferNative && _gameConfig.BackBufferWidth > 0)
        {
            return Math.Clamp(_gameConfig.BackBufferWidth, 320, 8192);
        }

        int width = graphicsDevice.PresentationParameters.BackBufferWidth;
        if (width > 0)
        {
            return width;
        }

        if (Engine.Graphics != null && Engine.Graphics.PreferredBackBufferWidth > 0)
        {
            return Engine.Graphics.PreferredBackBufferWidth;
        }

        return Math.Clamp(_gameConfig.InternalWidth, 320, 8192);
    }

    private int ResolveBackBufferHeight(GraphicsDevice graphicsDevice)
    {
        if (!_gameConfig.BackBufferNative && _gameConfig.BackBufferHeight > 0)
        {
            return Math.Clamp(_gameConfig.BackBufferHeight, 180, 8192);
        }

        int height = graphicsDevice.PresentationParameters.BackBufferHeight;
        if (height > 0)
        {
            return height;
        }

        if (Engine.Graphics != null && Engine.Graphics.PreferredBackBufferHeight > 0)
        {
            return Engine.Graphics.PreferredBackBufferHeight;
        }

        return Math.Clamp(_gameConfig.InternalHeight, 180, 8192);
    }

    private static Engine.PresentationAspectModes ResolveAspectMode(RuntimeAspectModes aspectMode)
    {
        return aspectMode switch
        {
            RuntimeAspectModes.Fill => Engine.PresentationAspectModes.Fill,
            RuntimeAspectModes.Stretch => Engine.PresentationAspectModes.Stretch,
            _ => Engine.PresentationAspectModes.Fit,
        };
    }

    private static GraphicsProfile ResolveGraphicsProfile(RuntimeGraphicsProfiles profile)
    {
        return profile switch
        {
            RuntimeGraphicsProfiles.Reach => GraphicsProfile.Reach,
            _ => GraphicsProfile.HiDef,
        };
    }

    private static DepthFormat ResolveDepthFormat(RuntimeDepthStencilModes depthMode)
    {
        return depthMode switch
        {
            RuntimeDepthStencilModes.None => DepthFormat.None,
            RuntimeDepthStencilModes.Depth16 => DepthFormat.Depth16,
            _ => DepthFormat.Depth24Stencil8,
        };
    }

    private void ApplyRuntimeTuning(string context)
    {
        AndroidRuntimeTuning.ForceCompatibilityCompositor = _gameConfig.ForceCompatibilityCompositor;
        AndroidRuntimeTuning.DisableBloom = !_gameConfig.Bloom;
        AndroidRuntimeTuning.DisablePostProcessing = _gameConfig.PostProcessingQuality == RuntimeQualityLevels.Low;
        AndroidRuntimeTuning.ParticleQualityTier = ResolveQualityTier(_gameConfig.Particles);
        AndroidRuntimeTuning.EnableDiagnosticLogs = _gameConfig.EnableDiagnosticLogs;

        if (_gameConfig.EnableDiagnosticLogs)
        {
            _services.Logger.Log(
                LogLevel.Info,
                "TUNING",
                $"Runtime tuning applied ({context})",
                context: $"comp={AndroidRuntimeTuning.ForceCompatibilityCompositor}; bloom={AndroidRuntimeTuning.DisableBloom}; post={AndroidRuntimeTuning.DisablePostProcessing}; particles={AndroidRuntimeTuning.ParticleQualityTier}; logs={AndroidRuntimeTuning.EnableDiagnosticLogs}");
        }
    }

    private static int ResolveQualityTier(RuntimeQualityLevels quality)
    {
        return quality switch
        {
            RuntimeQualityLevels.Low => 0,
            RuntimeQualityLevels.Medium => 1,
            _ => 2,
        };
    }

    private void ApplySceneQualityTuning()
    {
        // Current quality toggles are consumed directly from AndroidRuntimeTuning
        // by Level/Hires renderer paths during render.
    }

    private void ConfigureRuntimeMenuBridge()
    {
        CelesteRuntimeConfigBridge.Configure(BuildRuntimeUiSnapshot, ApplyRuntimeUiUpdate);
    }

    private RuntimeUiConfigSnapshot BuildRuntimeUiSnapshot()
    {
        return new RuntimeUiConfigSnapshot
        {
            VSync = _gameConfig.VSync,
            TargetFps = _gameConfig.TargetFps,
            AspectMode = ToUiAspectMode(_gameConfig.AspectMode),
            ScaleFilter = ToUiScaleFilter(_gameConfig.ScaleFilter),
            Bloom = _gameConfig.Bloom,
            PostProcessingQuality = ToUiQuality(_gameConfig.PostProcessingQuality),
            Particles = ToUiQuality(_gameConfig.Particles),
            ForceCompatibilityCompositor = _gameConfig.ForceCompatibilityCompositor,
            ForceLegacyBlendStates = _gameConfig.ForceLegacyBlendStates,
            EnableDiagnosticLogs = _gameConfig.EnableDiagnosticLogs,
            UseEdgeToEdgeOnAndroid = _gameConfig.UseEdgeToEdgeOnAndroid,
            ShowFps = _overlayConfig.ShowFps,
            ShowMemory = _overlayConfig.ShowMemory,
            ShowResolution = _overlayConfig.ShowResolution,
            ShowViewport = _overlayConfig.ShowViewport,
            ShowScale = _overlayConfig.ShowScale,
            OverlayPosition = ToUiOverlayPosition(_overlayConfig.Position),
            OverlayFontScale = _overlayConfig.FontScale,
            OverlayUpdateIntervalMs = _overlayConfig.UpdateIntervalMs,
            OverlayBackground = _overlayConfig.Background,
            OverlayPadding = _overlayConfig.Padding
        };
    }

    private void ApplyRuntimeUiUpdate(RuntimeUiConfigUpdate update)
    {
        if (update == null || !update.HasAnyValue())
        {
            return;
        }

        bool gameChanged = false;
        bool overlayChanged = false;

        if (update.VSync.HasValue && _gameConfig.VSync != update.VSync.Value)
        {
            _gameConfig.VSync = update.VSync.Value;
            gameChanged = true;
        }

        if (update.TargetFps != null)
        {
            string normalizedTarget = NormalizeTargetFps(update.TargetFps);
            if (!string.Equals(_gameConfig.TargetFps, normalizedTarget, StringComparison.Ordinal))
            {
                _gameConfig.TargetFps = normalizedTarget;
                gameChanged = true;
            }
        }

        if (update.AspectMode.HasValue)
        {
            RuntimeAspectModes aspectMode = FromUiAspectMode(update.AspectMode.Value);
            if (_gameConfig.AspectMode != aspectMode)
            {
                _gameConfig.AspectMode = aspectMode;
                gameChanged = true;
            }
        }

        if (update.ScaleFilter.HasValue)
        {
            RuntimeScaleFilters scaleFilter = FromUiScaleFilter(update.ScaleFilter.Value);
            if (_gameConfig.ScaleFilter != scaleFilter)
            {
                _gameConfig.ScaleFilter = scaleFilter;
                gameChanged = true;
            }
        }

        if (update.Bloom.HasValue && _gameConfig.Bloom != update.Bloom.Value)
        {
            _gameConfig.Bloom = update.Bloom.Value;
            gameChanged = true;
        }

        if (update.PostProcessingQuality.HasValue)
        {
            RuntimeQualityLevels quality = FromUiQuality(update.PostProcessingQuality.Value);
            if (_gameConfig.PostProcessingQuality != quality)
            {
                _gameConfig.PostProcessingQuality = quality;
                gameChanged = true;
            }
        }

        if (update.Particles.HasValue)
        {
            RuntimeQualityLevels particles = FromUiQuality(update.Particles.Value);
            if (_gameConfig.Particles != particles)
            {
                _gameConfig.Particles = particles;
                gameChanged = true;
            }
        }

        if (update.ForceCompatibilityCompositor.HasValue && _gameConfig.ForceCompatibilityCompositor != update.ForceCompatibilityCompositor.Value)
        {
            _gameConfig.ForceCompatibilityCompositor = update.ForceCompatibilityCompositor.Value;
            gameChanged = true;
        }

        if (update.ForceLegacyBlendStates.HasValue && _gameConfig.ForceLegacyBlendStates != update.ForceLegacyBlendStates.Value)
        {
            _gameConfig.ForceLegacyBlendStates = update.ForceLegacyBlendStates.Value;
            gameChanged = true;
        }

        if (update.EnableDiagnosticLogs.HasValue && _gameConfig.EnableDiagnosticLogs != update.EnableDiagnosticLogs.Value)
        {
            _gameConfig.EnableDiagnosticLogs = update.EnableDiagnosticLogs.Value;
            gameChanged = true;
        }

        if (update.UseEdgeToEdgeOnAndroid.HasValue && _gameConfig.UseEdgeToEdgeOnAndroid != update.UseEdgeToEdgeOnAndroid.Value)
        {
            _gameConfig.UseEdgeToEdgeOnAndroid = update.UseEdgeToEdgeOnAndroid.Value;
            gameChanged = true;
        }

        if (update.ShowFps.HasValue && _overlayConfig.ShowFps != update.ShowFps.Value)
        {
            _overlayConfig.ShowFps = update.ShowFps.Value;
            overlayChanged = true;
        }

        if (update.ShowMemory.HasValue && _overlayConfig.ShowMemory != update.ShowMemory.Value)
        {
            _overlayConfig.ShowMemory = update.ShowMemory.Value;
            overlayChanged = true;
        }

        if (update.ShowResolution.HasValue && _overlayConfig.ShowResolution != update.ShowResolution.Value)
        {
            _overlayConfig.ShowResolution = update.ShowResolution.Value;
            overlayChanged = true;
        }

        if (update.ShowViewport.HasValue && _overlayConfig.ShowViewport != update.ShowViewport.Value)
        {
            _overlayConfig.ShowViewport = update.ShowViewport.Value;
            overlayChanged = true;
        }

        if (update.ShowScale.HasValue && _overlayConfig.ShowScale != update.ShowScale.Value)
        {
            _overlayConfig.ShowScale = update.ShowScale.Value;
            overlayChanged = true;
        }

        if (update.OverlayPosition.HasValue)
        {
            RuntimeOverlayPositions position = FromUiOverlayPosition(update.OverlayPosition.Value);
            if (_overlayConfig.Position != position)
            {
                _overlayConfig.Position = position;
                overlayChanged = true;
            }
        }

        if (update.OverlayFontScale.HasValue)
        {
            float fontScale = Math.Clamp(update.OverlayFontScale.Value, 0.5f, 4f);
            if (Math.Abs(_overlayConfig.FontScale - fontScale) > 0.0001f)
            {
                _overlayConfig.FontScale = fontScale;
                overlayChanged = true;
            }
        }

        if (update.OverlayUpdateIntervalMs.HasValue)
        {
            int interval = Math.Clamp(update.OverlayUpdateIntervalMs.Value, 100, 10000);
            if (_overlayConfig.UpdateIntervalMs != interval)
            {
                _overlayConfig.UpdateIntervalMs = interval;
                overlayChanged = true;
            }
        }

        if (update.OverlayBackground.HasValue && _overlayConfig.Background != update.OverlayBackground.Value)
        {
            _overlayConfig.Background = update.OverlayBackground.Value;
            overlayChanged = true;
        }

        if (update.OverlayPadding.HasValue)
        {
            int padding = Math.Clamp(update.OverlayPadding.Value, 0, 64);
            if (_overlayConfig.Padding != padding)
            {
                _overlayConfig.Padding = padding;
                overlayChanged = true;
            }
        }

        if (!gameChanged && !overlayChanged)
        {
            return;
        }

        if (gameChanged)
        {
            _gameConfigSource.Save(_gameConfig, "runtime_menu");
            AppContext.SetSwitch(AndroidRuntimePolicy.ForceLegacyBlendStateSwitch, _gameConfig.ForceLegacyBlendStates);
            ApplyGraphicsConfiguration("RuntimeMenu");
            ApplyRuntimeTuning("RuntimeMenu");
        }

        if (overlayChanged)
        {
            _overlayConfigSource.Save(_overlayConfig, "runtime_menu");
        }

        RefreshOverlaySnapshot(force: true);
    }

    private static string NormalizeTargetFps(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "60";
        }

        string text = value.Trim();
        if (string.Equals(text, "uncapped", StringComparison.OrdinalIgnoreCase))
        {
            return "Uncapped";
        }

        if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
        {
            return Math.Clamp(result, 15, 240).ToString(CultureInfo.InvariantCulture);
        }

        return "60";
    }

    private static RuntimeUiAspectModes ToUiAspectMode(RuntimeAspectModes mode)
    {
        return mode switch
        {
            RuntimeAspectModes.Fit => RuntimeUiAspectModes.Fit,
            RuntimeAspectModes.Stretch => RuntimeUiAspectModes.Stretch,
            _ => RuntimeUiAspectModes.Fill,
        };
    }

    private static RuntimeAspectModes FromUiAspectMode(RuntimeUiAspectModes mode)
    {
        return mode switch
        {
            RuntimeUiAspectModes.Fit => RuntimeAspectModes.Fit,
            RuntimeUiAspectModes.Stretch => RuntimeAspectModes.Stretch,
            _ => RuntimeAspectModes.Fill,
        };
    }

    private static RuntimeUiScaleFilters ToUiScaleFilter(RuntimeScaleFilters filter)
    {
        return filter switch
        {
            RuntimeScaleFilters.Linear => RuntimeUiScaleFilters.Linear,
            _ => RuntimeUiScaleFilters.Point,
        };
    }

    private static RuntimeScaleFilters FromUiScaleFilter(RuntimeUiScaleFilters filter)
    {
        return filter switch
        {
            RuntimeUiScaleFilters.Linear => RuntimeScaleFilters.Linear,
            _ => RuntimeScaleFilters.Point,
        };
    }

    private static RuntimeUiQualityLevels ToUiQuality(RuntimeQualityLevels quality)
    {
        return quality switch
        {
            RuntimeQualityLevels.Low => RuntimeUiQualityLevels.Low,
            RuntimeQualityLevels.Medium => RuntimeUiQualityLevels.Medium,
            _ => RuntimeUiQualityLevels.High,
        };
    }

    private static RuntimeQualityLevels FromUiQuality(RuntimeUiQualityLevels quality)
    {
        return quality switch
        {
            RuntimeUiQualityLevels.Low => RuntimeQualityLevels.Low,
            RuntimeUiQualityLevels.Medium => RuntimeQualityLevels.Medium,
            _ => RuntimeQualityLevels.High,
        };
    }

    private static RuntimeUiOverlayPositions ToUiOverlayPosition(RuntimeOverlayPositions position)
    {
        return position switch
        {
            RuntimeOverlayPositions.TopRight => RuntimeUiOverlayPositions.TopRight,
            RuntimeOverlayPositions.BottomLeft => RuntimeUiOverlayPositions.BottomLeft,
            RuntimeOverlayPositions.BottomRight => RuntimeUiOverlayPositions.BottomRight,
            _ => RuntimeUiOverlayPositions.TopLeft,
        };
    }

    private static RuntimeOverlayPositions FromUiOverlayPosition(RuntimeUiOverlayPositions position)
    {
        return position switch
        {
            RuntimeUiOverlayPositions.TopRight => RuntimeOverlayPositions.TopRight,
            RuntimeUiOverlayPositions.BottomLeft => RuntimeOverlayPositions.BottomLeft,
            RuntimeUiOverlayPositions.BottomRight => RuntimeOverlayPositions.BottomRight,
            _ => RuntimeOverlayPositions.TopLeft,
        };
    }

    private void RefreshOverlaySnapshot(bool force)
    {
        var now = DateTime.UtcNow;
        if (!force && _overlayConfig.UpdateIntervalMs > 0 && (now - _lastOverlaySnapshotUtc).TotalMilliseconds < _overlayConfig.UpdateIntervalMs)
        {
            return;
        }

        _lastOverlaySnapshotUtc = now;
        if (!_overlayConfig.HasVisibleData)
        {
            _overlayLines = Array.Empty<string>();
            return;
        }

        var lines = new System.Collections.Generic.List<string>(capacity: 6);
        var graphicsDevice = Engine.Graphics?.GraphicsDevice;
        var backBufferWidth = graphicsDevice?.PresentationParameters.BackBufferWidth ?? 0;
        var backBufferHeight = graphicsDevice?.PresentationParameters.BackBufferHeight ?? 0;
        var viewport = Engine.Viewport;

        if (_overlayConfig.ShowFps)
        {
            lines.Add($"FPS: {Engine.FPS}");
        }

        if (_overlayConfig.ShowMemory)
        {
            long managedMb = GC.GetTotalMemory(false) / (1024 * 1024);
            long processMb = 0;
            try
            {
                using Process process = Process.GetCurrentProcess();
                processMb = process.WorkingSet64 / (1024 * 1024);
            }
            catch
            {
                processMb = 0;
            }

            if (processMb > 0)
            {
                lines.Add($"Memory: managed {managedMb} MB | process {processMb} MB");
            }
            else
            {
                lines.Add($"Memory: managed {managedMb} MB");
            }
        }

        if (_overlayConfig.ShowResolution)
        {
            lines.Add($"Resolution: internal {Engine.Width}x{Engine.Height} | backbuffer {backBufferWidth}x{backBufferHeight}");
        }

        if (_overlayConfig.ShowViewport)
        {
            lines.Add($"Viewport: {viewport.X},{viewport.Y} {viewport.Width}x{viewport.Height}");
        }

        if (_overlayConfig.ShowScale)
        {
            float scaleX = Engine.ScreenMatrix.M11;
            float scaleY = Engine.ScreenMatrix.M22;
            string filter = _gameConfig.ScaleFilter == RuntimeScaleFilters.Linear ? "Linear" : "Point";
            lines.Add($"Scale: {scaleX:0.###}x{scaleY:0.###} | Aspect={Engine.PresentationAspectMode} | Filter={filter}");
        }

        _overlayLines = lines.ToArray();
    }

    private void DrawRuntimeOverlay()
    {
        if (_overlayLines.Length == 0)
            return;

        if (_overlaySpriteBatch == null || _overlayPixel == null)
            return;

        var font = _overlayFont;
        float scale = _overlayConfig.FontScale;
        var color = _overlayConfig.TextColor;

        var textSize = MeasureOverlayText(font, scale);
        float panelWidth = textSize.X + _overlayConfig.Padding * 2;
        float panelHeight = textSize.Y + _overlayConfig.Padding * 2;
        var origin = ResolveOverlayOrigin(panelWidth, panelHeight);
        var textOrigin = origin + new Vector2(_overlayConfig.Padding, _overlayConfig.Padding);

        _overlaySpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);

        if (_overlayConfig.Background)
        {
            var bgRect = new Rectangle(
                (int)MathF.Round(origin.X),
                (int)MathF.Round(origin.Y),
                Math.Max(1, (int)MathF.Ceiling(panelWidth)),
                Math.Max(1, (int)MathF.Ceiling(panelHeight)));
            _overlaySpriteBatch.Draw(_overlayPixel, bgRect, new Color(0, 0, 0, 160));
        }

        if (font != null)
        {
            float lineH = font.LineSpacing * scale;
            for (int i = 0; i < _overlayLines.Length; i++)
            {
                var pos = new Vector2(textOrigin.X, textOrigin.Y + i * lineH);
                _overlaySpriteBatch.DrawString(font, _overlayLines[i], pos, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
        else
        {
            float lineH = _overlayFallbackFont.LineHeight(scale);
            for (int i = 0; i < _overlayLines.Length; i++)
            {
                var pos = new Vector2(textOrigin.X, textOrigin.Y + i * lineH);
                _overlayFallbackFont.DrawString(_overlaySpriteBatch, _overlayPixel, _overlayLines[i], pos, color, scale);
            }
        }

        _overlaySpriteBatch.End();
    }

    private Vector2 MeasureOverlayText(SpriteFont? font, float scale)
    {
        if (_overlayLines.Length == 0)
        {
            return Vector2.Zero;
        }

        float maxWidth = 0f;
        float totalHeight = 0f;

        if (font != null)
        {
            float lineHeight = font.LineSpacing * scale;
            for (int i = 0; i < _overlayLines.Length; i++)
            {
                float width = font.MeasureString(_overlayLines[i]).X * scale;
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }

            totalHeight = lineHeight * _overlayLines.Length;
        }
        else
        {
            float lineHeight = _overlayFallbackFont.LineHeight(scale);
            for (int i = 0; i < _overlayLines.Length; i++)
            {
                float width = Math.Max(1, _overlayLines[i].Length) * 6f * scale;
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }

            totalHeight = lineHeight * _overlayLines.Length;
        }

        return new Vector2(maxWidth, totalHeight);
    }

    private Vector2 ResolveOverlayOrigin(float panelWidth, float panelHeight)
    {
        var viewport = Engine.Viewport;
        float left = viewport.X;
        float top = viewport.Y;
        float right = viewport.X + viewport.Width;
        float bottom = viewport.Y + viewport.Height;

        if (viewport.Width <= 0 || viewport.Height <= 0)
        {
            int backBufferWidth = Engine.Graphics?.GraphicsDevice?.PresentationParameters.BackBufferWidth ?? Engine.Width;
            int backBufferHeight = Engine.Graphics?.GraphicsDevice?.PresentationParameters.BackBufferHeight ?? Engine.Height;
            left = 0f;
            top = 0f;
            right = backBufferWidth;
            bottom = backBufferHeight;
        }

        float pad = Math.Max(0f, _overlayConfig.Padding);
        return _overlayConfig.Position switch
        {
            RuntimeOverlayPositions.TopRight => new Vector2(right - panelWidth - pad, top + pad),
            RuntimeOverlayPositions.BottomLeft => new Vector2(left + pad, bottom - panelHeight - pad),
            RuntimeOverlayPositions.BottomRight => new Vector2(right - panelWidth - pad, bottom - panelHeight - pad),
            _ => new Vector2(left + pad, top + pad),
        };
    }

    private string BuildRuntimeContext(string stage)
    {
        var scene = Engine.Scene?.GetType().FullName ?? "null";
        return $"stage={stage}; abi={_activeAbi}; scene={scene}; fps={Engine.FPS}; lowMemoryMode={_lowMemoryMode}; aggressiveGc={_aggressiveGc}; legacyBlend={AndroidRuntimePolicy.ShouldForceLegacyBlendStates()}; managedBytes={GC.GetTotalMemory(false)}; gc0={GC.CollectionCount(0)}; gc1={GC.CollectionCount(1)}; gc2={GC.CollectionCount(2)}";
    }
}
