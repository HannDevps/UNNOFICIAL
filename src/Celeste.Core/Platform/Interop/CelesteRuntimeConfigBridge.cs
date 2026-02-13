using System;

namespace Celeste.Core.Platform.Interop;

public enum RuntimeUiAspectModes
{
    Fit,
    Fill,
    Stretch
}

public enum RuntimeUiScaleFilters
{
    Point,
    Linear
}

public enum RuntimeUiQualityLevels
{
    Low,
    Medium,
    High
}

public enum RuntimeUiOverlayPositions
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public sealed class RuntimeUiConfigSnapshot
{
    public bool VSync { get; set; }

    public string TargetFps { get; set; } = "60";

    public RuntimeUiAspectModes AspectMode { get; set; } = RuntimeUiAspectModes.Fill;

    public RuntimeUiScaleFilters ScaleFilter { get; set; } = RuntimeUiScaleFilters.Point;

    public bool Bloom { get; set; } = true;

    public RuntimeUiQualityLevels PostProcessingQuality { get; set; } = RuntimeUiQualityLevels.High;

    public RuntimeUiQualityLevels Particles { get; set; } = RuntimeUiQualityLevels.High;

    public bool ForceCompatibilityCompositor { get; set; } = true;

    public bool ForceLegacyBlendStates { get; set; } = true;

    public bool EnableDiagnosticLogs { get; set; } = true;

    public bool UseEdgeToEdgeOnAndroid { get; set; } = true;

    public bool ShowFps { get; set; }

    public bool ShowMemory { get; set; }

    public bool ShowResolution { get; set; }

    public bool ShowViewport { get; set; }

    public bool ShowScale { get; set; }

    public RuntimeUiOverlayPositions OverlayPosition { get; set; } = RuntimeUiOverlayPositions.TopLeft;

    public float OverlayFontScale { get; set; } = 1f;

    public int OverlayUpdateIntervalMs { get; set; } = 500;

    public bool OverlayBackground { get; set; }

    public int OverlayPadding { get; set; } = 8;

    public RuntimeUiConfigSnapshot Clone()
    {
        return new RuntimeUiConfigSnapshot
        {
            VSync = VSync,
            TargetFps = TargetFps,
            AspectMode = AspectMode,
            ScaleFilter = ScaleFilter,
            Bloom = Bloom,
            PostProcessingQuality = PostProcessingQuality,
            Particles = Particles,
            ForceCompatibilityCompositor = ForceCompatibilityCompositor,
            ForceLegacyBlendStates = ForceLegacyBlendStates,
            EnableDiagnosticLogs = EnableDiagnosticLogs,
            UseEdgeToEdgeOnAndroid = UseEdgeToEdgeOnAndroid,
            ShowFps = ShowFps,
            ShowMemory = ShowMemory,
            ShowResolution = ShowResolution,
            ShowViewport = ShowViewport,
            ShowScale = ShowScale,
            OverlayPosition = OverlayPosition,
            OverlayFontScale = OverlayFontScale,
            OverlayUpdateIntervalMs = OverlayUpdateIntervalMs,
            OverlayBackground = OverlayBackground,
            OverlayPadding = OverlayPadding
        };
    }
}

public sealed class RuntimeUiConfigUpdate
{
    public bool? VSync { get; set; }

    public string? TargetFps { get; set; }

    public RuntimeUiAspectModes? AspectMode { get; set; }

    public RuntimeUiScaleFilters? ScaleFilter { get; set; }

    public bool? Bloom { get; set; }

    public RuntimeUiQualityLevels? PostProcessingQuality { get; set; }

    public RuntimeUiQualityLevels? Particles { get; set; }

    public bool? ForceCompatibilityCompositor { get; set; }

    public bool? ForceLegacyBlendStates { get; set; }

    public bool? EnableDiagnosticLogs { get; set; }

    public bool? UseEdgeToEdgeOnAndroid { get; set; }

    public bool? ShowFps { get; set; }

    public bool? ShowMemory { get; set; }

    public bool? ShowResolution { get; set; }

    public bool? ShowViewport { get; set; }

    public bool? ShowScale { get; set; }

    public RuntimeUiOverlayPositions? OverlayPosition { get; set; }

    public float? OverlayFontScale { get; set; }

    public int? OverlayUpdateIntervalMs { get; set; }

    public bool? OverlayBackground { get; set; }

    public int? OverlayPadding { get; set; }

    public bool HasAnyValue()
    {
        return VSync.HasValue
            || TargetFps != null
            || AspectMode.HasValue
            || ScaleFilter.HasValue
            || Bloom.HasValue
            || PostProcessingQuality.HasValue
            || Particles.HasValue
            || ForceCompatibilityCompositor.HasValue
            || ForceLegacyBlendStates.HasValue
            || EnableDiagnosticLogs.HasValue
            || UseEdgeToEdgeOnAndroid.HasValue
            || ShowFps.HasValue
            || ShowMemory.HasValue
            || ShowResolution.HasValue
            || ShowViewport.HasValue
            || ShowScale.HasValue
            || OverlayPosition.HasValue
            || OverlayFontScale.HasValue
            || OverlayUpdateIntervalMs.HasValue
            || OverlayBackground.HasValue
            || OverlayPadding.HasValue;
    }
}

public static class CelesteRuntimeConfigBridge
{
    private static readonly object Sync = new object();

    private static Func<RuntimeUiConfigSnapshot>? _snapshotProvider;

    private static Action<RuntimeUiConfigUpdate>? _applyUpdate;

    public static void Configure(Func<RuntimeUiConfigSnapshot> snapshotProvider, Action<RuntimeUiConfigUpdate> applyUpdate)
    {
        lock (Sync)
        {
            _snapshotProvider = snapshotProvider;
            _applyUpdate = applyUpdate;
        }
    }

    public static void Clear()
    {
        lock (Sync)
        {
            _snapshotProvider = null;
            _applyUpdate = null;
        }
    }

    public static bool IsAvailable
    {
        get
        {
            lock (Sync)
            {
                return _snapshotProvider != null && _applyUpdate != null;
            }
        }
    }

    public static bool TryGetSnapshot(out RuntimeUiConfigSnapshot snapshot)
    {
        Func<RuntimeUiConfigSnapshot>? snapshotProvider;
        lock (Sync)
        {
            snapshotProvider = _snapshotProvider;
        }

        if (snapshotProvider == null)
        {
            snapshot = new RuntimeUiConfigSnapshot();
            return false;
        }

        snapshot = snapshotProvider().Clone();
        return true;
    }

    public static bool TryApplyUpdate(RuntimeUiConfigUpdate update)
    {
        if (update == null || !update.HasAnyValue())
        {
            return false;
        }

        Action<RuntimeUiConfigUpdate>? applyUpdate;
        lock (Sync)
        {
            applyUpdate = _applyUpdate;
        }

        if (applyUpdate == null)
        {
            return false;
        }

        applyUpdate(update);
        return true;
    }
}
