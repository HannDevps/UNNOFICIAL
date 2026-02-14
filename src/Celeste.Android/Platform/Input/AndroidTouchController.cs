using System;
using System.Collections.Generic;
using System.IO;
using Android.Views;
using Celeste.Android.Platform.Configuration;
using Celeste.Android.Platform.Rendering;
using Celeste.Core.Platform.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Monocle;

namespace Celeste.Android.Platform.Input;

public sealed class AndroidTouchController : IDisposable
{
    private const int SourceKeyboardMask = 0x00000101;
    private const int SourceMouseMask = 0x00002002;
    private const int SourceGamepadMask = 0x00000401;
    private const int SourceJoystickMask = 0x01000010;

    private readonly IAppLogger _logger;
    private readonly Dictionary<int, MenuTouchTrack> _menuTouchTracks = new();
    private readonly List<int> _menuTouchCleanup = new();

    private RuntimeGameConfig _config = RuntimeGameConfig.CreateDefault();
    private DateTime _lastExternalInputCheckUtc;
    private bool _externalInputConnected;
    private Texture2D? _circleTexture;
    private readonly Dictionary<string, Texture2D> _promptTextures = new(StringComparer.Ordinal);
    private bool _promptTexturesInitialized;

    private bool _touchSuppressedByHardware;
    private bool _showGameplayOverlay;
    private float _overlayOpacity;

    private bool _moveLeft;
    private bool _moveRight;
    private bool _moveUp;
    private bool _moveDown;

    private bool _buttonA;
    private bool _buttonB;
    private bool _buttonX;
    private bool _buttonY;
    private bool _leftTrigger;
    private bool _buttonStart;
    private bool _buttonBack;

    private bool _menuPulseLeft;
    private bool _menuPulseRight;
    private bool _menuPulseUp;
    private bool _menuPulseDown;
    private bool _menuPulseConfirm;
    private bool _menuPulseCancel;
    private bool _menuPulsePause;
    private bool _menuPulseJournal;

    private bool _menuPointerDown;
    private bool _menuPointerPressedThisFrame;
    private Vector2 _menuPointerPosition;
    private int _pendingMenuCancelFrames;
    private int _menuPointerPulseFrames;

    private Vector2 _leftStickCenter;
    private Vector2 _leftStickKnob;
    private float _leftStickRadius;
    private int _activeStickTouchId = -1;

    private Vector2 _actionCenter;
    private float _buttonRadius;
    private float _actionSpacing;
    private Vector2 _buttonCenterA;
    private Vector2 _buttonCenterB;
    private Vector2 _buttonCenterX;
    private Vector2 _buttonCenterY;

    private Vector2 _leftTriggerCenter;
    private Vector2 _startCenter;
    private Vector2 _backCenter;

    private readonly struct MenuTouchTrack
    {
        public readonly Vector2 Start;
        public readonly Vector2 Last;
        public readonly bool SwipeConsumed;
        public readonly long LastUpdateTicks;

        public MenuTouchTrack(Vector2 start, Vector2 last, bool swipeConsumed, long lastUpdateTicks)
        {
            Start = start;
            Last = last;
            SwipeConsumed = swipeConsumed;
            LastUpdateTicks = lastUpdateTicks;
        }

        public MenuTouchTrack WithLast(Vector2 last, long ticks)
        {
            return new MenuTouchTrack(Start, last, SwipeConsumed, ticks);
        }

        public MenuTouchTrack WithSwipeConsumed(bool consumed, Vector2 last, long ticks)
        {
            return new MenuTouchTrack(Start, last, consumed, ticks);
        }

        public MenuTouchTrack WithStart(Vector2 start, Vector2 last, long ticks)
        {
            return new MenuTouchTrack(start, last, swipeConsumed: false, ticks);
        }
    }

    private static readonly long MenuTouchOrphanTicks = TimeSpan.FromMilliseconds(120).Ticks;

    public AndroidTouchController(IAppLogger logger)
    {
        _logger = logger;
    }

    public void Dispose()
    {
        DisposePromptTextures();
        _circleTexture?.Dispose();
        _circleTexture = null;
    }

    public void ApplyConfig(RuntimeGameConfig config)
    {
        _config = config ?? RuntimeGameConfig.CreateDefault();
    }

    public void QueueMenuCancelPulse()
    {
        _pendingMenuCancelFrames = Math.Max(_pendingMenuCancelFrames, 2);
    }

    public KeyboardState ApplyKeyboardState(KeyboardState hardwareState)
    {
        ResetFrameState();
        ConsumePendingMenuSignals();

        bool gameplayScene = IsGameplayScene();
        bool touchEnabled = _config.TouchEnabled;
        bool showControlsInScene = gameplayScene || !_config.TouchGameplayOnly;
        _showGameplayOverlay = touchEnabled && showControlsInScene;
        _overlayOpacity = Math.Clamp(_config.TouchOpacity, 0.15f, 1f);

        if (!touchEnabled)
        {
            _touchSuppressedByHardware = false;
            _activeStickTouchId = -1;
            _menuPointerDown = false;
            _menuPointerPressedThisFrame = false;
            _menuPointerPulseFrames = 0;
            _menuTouchTracks.Clear();
            _menuTouchCleanup.Clear();
            return BuildKeyboardState(hardwareState);
        }

        _touchSuppressedByHardware = _config.TouchAutoDisableOnExternalInput && IsExternalInputConnected();
        if (_touchSuppressedByHardware)
        {
            _showGameplayOverlay = false;
            _activeStickTouchId = -1;
            _menuPointerDown = false;
            _menuPointerPressedThisFrame = false;
            _menuPointerPulseFrames = 0;
            _menuTouchTracks.Clear();
            _menuTouchCleanup.Clear();
            return BuildKeyboardState(hardwareState);
        }

        TouchCollection touches = TouchPanel.GetState();
        ResolveSurfaceSize(out float width, out float height);

        if (_showGameplayOverlay)
        {
            UpdateGameplayState(touches, width, height);
        }
        else
        {
            _leftStickKnob = Vector2.Zero;
            _activeStickTouchId = -1;
        }

        bool allowMenuTouch = !gameplayScene && _config.TouchTapMenuNavigation;
        if (allowMenuTouch)
        {
            UpdateMenuTouchState(touches, width, height);
        }
        else
        {
            _menuPointerDown = false;
            _menuPointerPressedThisFrame = false;
            _menuPointerPulseFrames = 0;
            _menuTouchTracks.Clear();
            _menuTouchCleanup.Clear();
        }

        return BuildKeyboardState(hardwareState);
    }

    public MouseState ApplyMouseState(MouseState hardwareState)
    {
        if (!CanEmitMenuPointer())
        {
            return hardwareState;
        }

        var leftButton = (_menuPointerDown || _menuPointerPressedThisFrame) ? ButtonState.Pressed : ButtonState.Released;
        return new MouseState(
            (int)MathF.Round(_menuPointerPosition.X),
            (int)MathF.Round(_menuPointerPosition.Y),
            hardwareState.ScrollWheelValue,
            leftButton,
            hardwareState.MiddleButton,
            hardwareState.RightButton,
            hardwareState.XButton1,
            hardwareState.XButton2);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Texture2D pixel, SpriteFont? font, BitmapFallbackFont fallbackFont)
    {
        if (!_showGameplayOverlay || _touchSuppressedByHardware)
        {
            return;
        }

        EnsureCircleTexture(graphicsDevice);
        if (_circleTexture == null)
        {
            return;
        }

        EnsurePromptTextures(graphicsDevice);

        float alpha = _overlayOpacity;
        Color plate = ApplyAlpha(new Color(10, 16, 25), alpha * 0.65f);
        Color accent = ApplyAlpha(new Color(220, 226, 236), alpha * 0.78f);

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);

        bool hasCustomStickBase = DrawPromptTexture(spriteBatch, "STICK_BASE", _leftStickCenter, _leftStickRadius * 1.04f, ApplyAlpha(Color.White, alpha * 0.95f));
        if (!hasCustomStickBase)
        {
            DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 1.06f, ApplyAlpha(new Color(210, 223, 240), alpha * 0.22f));
            DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 0.97f, plate);
            DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 0.8f, ApplyAlpha(new Color(36, 52, 74), alpha * 0.82f));
            DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 0.62f, ApplyAlpha(new Color(14, 20, 30), alpha * 0.8f));
        }

        Vector2 knobCenter = _leftStickKnob == Vector2.Zero ? _leftStickCenter : _leftStickKnob;
        bool hasCustomStickThumb = DrawPromptTexture(spriteBatch, "STICK_THUMB", knobCenter, _leftStickRadius * 0.48f, ApplyAlpha(Color.White, alpha));
        if (!hasCustomStickThumb)
        {
            DrawCircle(spriteBatch, knobCenter, _leftStickRadius * 0.44f, ApplyAlpha(new Color(196, 212, 230), alpha * 0.45f));
            DrawCircle(spriteBatch, knobCenter, _leftStickRadius * 0.4f, ApplyAlpha(new Color(28, 40, 58), alpha * 0.95f));
            DrawCircle(spriteBatch, knobCenter, _leftStickRadius * 0.26f, accent);
        }

        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterA, _buttonRadius, "A", _buttonA, new Color(58, 182, 102), alpha, "FACE_A");
        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterB, _buttonRadius, "B", _buttonB, new Color(217, 86, 89), alpha, "FACE_B");
        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterX, _buttonRadius, "X", _buttonX, new Color(79, 153, 243), alpha, "FACE_X");
        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterY, _buttonRadius, "RT", _buttonY, new Color(223, 192, 63), alpha, "RT");

        if (_config.TouchEnableShoulders)
        {
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _leftTriggerCenter, _buttonRadius * 0.75f, "LT", _leftTrigger, new Color(102, 118, 140), alpha, "LT");
        }

        if (_config.TouchEnableStartSelect)
        {
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _backCenter, _buttonRadius * 0.65f, "BACK", _buttonBack, new Color(86, 104, 128), alpha, "BACK");
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _startCenter, _buttonRadius * 0.65f, "START", _buttonStart, new Color(86, 104, 128), alpha, "START");
        }

        spriteBatch.End();
    }

    private void ResetFrameState()
    {
        _moveLeft = false;
        _moveRight = false;
        _moveUp = false;
        _moveDown = false;
        _buttonA = false;
        _buttonB = false;
        _buttonX = false;
        _buttonY = false;
        _leftTrigger = false;
        _buttonStart = false;
        _buttonBack = false;
        _menuPulseLeft = false;
        _menuPulseRight = false;
        _menuPulseUp = false;
        _menuPulseDown = false;
        _menuPulseConfirm = false;
        _menuPulseCancel = false;
        _menuPulsePause = false;
        _menuPulseJournal = false;
        _menuPointerPressedThisFrame = false;
    }

    private void UpdateGameplayState(TouchCollection touches, float width, float height)
    {
        PrepareLayout(width, height);

        int stickTouchId = -1;
        Vector2 stickDelta = Vector2.Zero;
        float stickCaptureRadius = _leftStickRadius * 1.6f;
        float bestDistSq = float.MaxValue;

        if (_activeStickTouchId >= 0 && TryGetTouchById(touches, _activeStickTouchId, out TouchLocation activeTouch) && IsTouchDown(activeTouch.State))
        {
            stickTouchId = _activeStickTouchId;
            stickDelta = activeTouch.Position - _leftStickCenter;
            bestDistSq = stickDelta.LengthSquared();
        }

        for (int i = 0; i < touches.Count; i++)
        {
            TouchLocation touch = touches[i];
            if (!IsTouchDown(touch.State))
            {
                continue;
            }

            Vector2 pos = touch.Position;
            if (!IsInsideSurface(pos, width, height))
            {
                continue;
            }

            Vector2 delta = pos - _leftStickCenter;
            float distSq = delta.LengthSquared();
            if (stickTouchId < 0 && distSq <= stickCaptureRadius * stickCaptureRadius && distSq < bestDistSq)
            {
                bestDistSq = distSq;
                stickTouchId = touch.Id;
                stickDelta = delta;
            }

            _buttonA |= IsInsideCircle(pos, _buttonCenterA, _buttonRadius);
            _buttonB |= IsInsideCircle(pos, _buttonCenterB, _buttonRadius);
            _buttonX |= IsInsideCircle(pos, _buttonCenterX, _buttonRadius);
            _buttonY |= IsInsideCircle(pos, _buttonCenterY, _buttonRadius);

            if (_config.TouchEnableShoulders)
            {
                _leftTrigger |= IsInsideCircle(pos, _leftTriggerCenter, _buttonRadius * 0.85f);
            }

            if (_config.TouchEnableStartSelect)
            {
                _buttonStart |= IsInsideCircle(pos, _startCenter, _buttonRadius * 0.75f);
                _buttonBack |= IsInsideCircle(pos, _backCenter, _buttonRadius * 0.75f);
            }

        }

        if (stickTouchId >= 0)
        {
            _activeStickTouchId = stickTouchId;
            float length = stickDelta.Length();
            if (length > _leftStickRadius && length > 0f)
            {
                stickDelta *= _leftStickRadius / length;
                length = _leftStickRadius;
            }

            Vector2 knobTarget = _leftStickCenter + stickDelta * 0.45f;
            if (_leftStickKnob == Vector2.Zero)
            {
                _leftStickKnob = knobTarget;
            }
            else
            {
                _leftStickKnob = Vector2.Lerp(_leftStickKnob, knobTarget, 0.45f);
            }

            float deadzone = Math.Clamp(_config.TouchLeftStickDeadzone, 0.05f, 0.7f) * _leftStickRadius;
            if (length > deadzone)
            {
                float nx = stickDelta.X / _leftStickRadius;
                float ny = stickDelta.Y / _leftStickRadius;
                _moveLeft |= nx <= -0.35f;
                _moveRight |= nx >= 0.35f;
                _moveUp |= ny <= -0.35f;
                _moveDown |= ny >= 0.35f;
            }
        }
        else
        {
            _activeStickTouchId = -1;
            if (_leftStickKnob == Vector2.Zero)
            {
                _leftStickKnob = _leftStickCenter;
            }
            else
            {
                _leftStickKnob = Vector2.Lerp(_leftStickKnob, _leftStickCenter, 0.2f);
            }
        }
    }

    private void UpdateMenuTouchState(TouchCollection touches, float width, float height)
    {
        bool anyDown = false;
        bool pointerCaptured = false;
        float swipeThreshold = MathF.Max(width, height) * 0.055f;
        long nowTicks = DateTime.UtcNow.Ticks;
        _menuTouchCleanup.Clear();

        for (int i = 0; i < touches.Count; i++)
        {
            TouchLocation touch = touches[i];
            Vector2 pos = touch.Position;
            if (!IsInsideSurface(pos, width, height))
            {
                continue;
            }

            int touchId = touch.Id;
            if (!_menuTouchTracks.TryGetValue(touchId, out MenuTouchTrack track))
            {
                Vector2 start = pos;
                if (touch.TryGetPreviousLocation(out TouchLocation previousStart) && IsTouchDown(previousStart.State))
                {
                    start = previousStart.Position;
                }

                track = new MenuTouchTrack(start, pos, swipeConsumed: false, nowTicks);
            }
            else
            {
                track = track.WithLast(pos, nowTicks);
            }

            if (IsTouchDown(touch.State))
            {
                anyDown = true;
                if (!pointerCaptured)
                {
                    pointerCaptured = true;
                    _menuPointerPosition = pos;
                }
            }

            if (touch.State == TouchLocationState.Pressed)
            {
                track = track.WithStart(pos, pos, nowTicks);
                _menuTouchTracks[touchId] = track;
                QueueMenuPointerPulse();
                continue;
            }

            if (touch.State == TouchLocationState.Moved)
            {
                if (!track.SwipeConsumed)
                {
                    Vector2 delta = pos - track.Start;
                    if (MathF.Abs(delta.X) >= swipeThreshold || MathF.Abs(delta.Y) >= swipeThreshold)
                    {
                        EmitSwipe(delta);
                        track = track.WithSwipeConsumed(consumed: true, pos, nowTicks);
                    }
                }

                _menuTouchTracks[touchId] = track;

                continue;
            }

            if (touch.State == TouchLocationState.Released)
            {
                if (!pointerCaptured)
                {
                    pointerCaptured = true;
                    _menuPointerPosition = pos;
                }

                if (!_menuTouchTracks.ContainsKey(touchId)
                    && touch.TryGetPreviousLocation(out TouchLocation previousRelease)
                    && IsTouchDown(previousRelease.State))
                {
                    track = new MenuTouchTrack(previousRelease.Position, pos, swipeConsumed: false, nowTicks);
                    QueueMenuPointerPulse();
                }

                FinalizeMenuTouch(track, pos, swipeThreshold, width, height);
                _menuTouchTracks.Remove(touchId);
                continue;
            }

            if (touch.State == TouchLocationState.Invalid)
            {
                _menuTouchTracks.Remove(touchId);
                continue;
            }

            _menuTouchTracks[touchId] = track;
        }

        if (_menuTouchTracks.Count > 0)
        {
            foreach (KeyValuePair<int, MenuTouchTrack> pair in _menuTouchTracks)
            {
                if (nowTicks - pair.Value.LastUpdateTicks <= MenuTouchOrphanTicks)
                {
                    continue;
                }

                FinalizeMenuTouch(pair.Value, pair.Value.Last, swipeThreshold, width, height);
                _menuTouchCleanup.Add(pair.Key);
            }

            for (int i = 0; i < _menuTouchCleanup.Count; i++)
            {
                _menuTouchTracks.Remove(_menuTouchCleanup[i]);
            }
        }

        _menuPointerDown = anyDown;
    }

    private void FinalizeMenuTouch(MenuTouchTrack track, Vector2 end, float swipeThreshold, float width, float height)
    {
        _menuPointerPosition = end;

        if (!track.SwipeConsumed)
        {
            Vector2 delta = end - track.Start;
            if (MathF.Abs(delta.X) >= swipeThreshold || MathF.Abs(delta.Y) >= swipeThreshold)
            {
                EmitSwipe(delta);
            }
            else
            {
                EmitTap(end, width, height);
            }
        }

        QueueMenuPointerPulse();
    }

    private void EmitSwipe(Vector2 delta)
    {
        if (MathF.Abs(delta.X) >= MathF.Abs(delta.Y))
        {
            if (delta.X < 0f)
            {
                _menuPulseLeft = true;
            }
            else
            {
                _menuPulseRight = true;
            }
        }
    }

    private void ConsumePendingMenuSignals()
    {
        if (_pendingMenuCancelFrames <= 0)
        {
            if (_menuPointerPulseFrames > 0)
            {
                _menuPointerPressedThisFrame = true;
                _menuPointerPulseFrames--;
            }

            return;
        }

        _menuPulseCancel = true;
        _pendingMenuCancelFrames--;

        if (_menuPointerPulseFrames > 0)
        {
            _menuPointerPressedThisFrame = true;
            _menuPointerPulseFrames--;
        }
    }

    private void QueueMenuPointerPulse(int frames = 2)
    {
        _menuPointerPulseFrames = Math.Max(_menuPointerPulseFrames, Math.Max(1, frames));
        _menuPointerPressedThisFrame = true;
    }

    private void EmitTap(Vector2 pos, float width, float height)
    {
        float nx = pos.X / Math.Max(1f, width);
        float ny = pos.Y / Math.Max(1f, height);

        if (ny <= 0.18f)
        {
            if (nx <= 0.22f)
            {
                _menuPulseCancel = true;
            }
            else if (nx >= 0.78f)
            {
                _menuPulsePause = true;
            }
            else
            {
                _menuPulseJournal = true;
            }

            return;
        }

        if (nx <= 0.2f)
        {
            _menuPulseLeft = true;
            return;
        }

        if (nx >= 0.8f)
        {
            _menuPulseRight = true;
            return;
        }

        if (ny <= 0.38f)
        {
            _menuPulseUp = true;
            return;
        }

        if (ny >= 0.78f)
        {
            _menuPulseDown = true;
            return;
        }

        _menuPulseConfirm = true;
    }

    private KeyboardState BuildKeyboardState(KeyboardState hardwareState)
    {
        KeyMap map = ResolveKeyMap();
        Keys[] hardwareKeys = hardwareState.GetPressedKeys();
        Keys[] merged = new Keys[hardwareKeys.Length + 24];
        Array.Copy(hardwareKeys, merged, hardwareKeys.Length);
        int count = hardwareKeys.Length;

        if (_moveLeft) AddUnique(merged, ref count, map.MoveLeft);
        if (_moveRight) AddUnique(merged, ref count, map.MoveRight);
        if (_moveUp) AddUnique(merged, ref count, map.MoveUp);
        if (_moveDown) AddUnique(merged, ref count, map.MoveDown);

        if (_buttonA) AddUnique(merged, ref count, map.Jump);
        if (_buttonB) AddUnique(merged, ref count, map.Dash);
        if (_buttonX)
        {
            AddUnique(merged, ref count, map.Dash);
            AddUnique(merged, ref count, map.Talk);
        }
        if (_leftTrigger || _buttonY) AddUnique(merged, ref count, map.Grab);
        if (_buttonStart) AddUnique(merged, ref count, map.Pause);
        if (_buttonBack) AddUnique(merged, ref count, map.Pause);

        if (_menuPulseLeft) AddUnique(merged, ref count, map.MenuLeft);
        if (_menuPulseRight) AddUnique(merged, ref count, map.MenuRight);
        if (_menuPulseUp) AddUnique(merged, ref count, map.MenuUp);
        if (_menuPulseDown) AddUnique(merged, ref count, map.MenuDown);
        if (_menuPulseConfirm) AddUnique(merged, ref count, map.MenuConfirm);
        if (_menuPulseCancel) AddUnique(merged, ref count, map.MenuCancel);
        if (_menuPulsePause) AddUnique(merged, ref count, map.Pause);
        if (_menuPulseJournal) AddUnique(merged, ref count, map.MenuJournal);

        if (count == hardwareKeys.Length)
        {
            return hardwareState;
        }

        if (count < merged.Length)
        {
            Array.Resize(ref merged, count);
        }

        return new KeyboardState(merged);
    }

    private static void AddUnique(Keys[] target, ref int count, Keys value)
    {
        if (value == Keys.None)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            if (target[i] == value)
            {
                return;
            }
        }

        target[count] = value;
        count++;
    }

    private static bool IsTouchDown(TouchLocationState state)
    {
        return state != TouchLocationState.Released && state != TouchLocationState.Invalid;
    }

    private static bool IsInsideSurface(Vector2 pos, float width, float height)
    {
        return pos.X >= 0f && pos.Y >= 0f && pos.X <= width && pos.Y <= height;
    }

    private static bool IsInsideCircle(Vector2 pos, Vector2 center, float radius)
    {
        Vector2 delta = pos - center;
        return delta.LengthSquared() <= radius * radius;
    }

    private static bool TryGetTouchById(TouchCollection touches, int id, out TouchLocation touch)
    {
        for (int i = 0; i < touches.Count; i++)
        {
            if (touches[i].Id == id)
            {
                touch = touches[i];
                return true;
            }
        }

        touch = default(TouchLocation);
        return false;
    }

    private static bool IsGameplayScene()
    {
        return Engine.Scene is global::Celeste.Level;
    }

    private bool CanEmitMenuPointer()
    {
        if (!_config.TouchEnabled || _touchSuppressedByHardware)
        {
            return false;
        }

        if (IsGameplayScene())
        {
            return false;
        }

        if (!_config.TouchTapMenuNavigation)
        {
            return false;
        }

        return _menuPointerDown || _menuPointerPressedThisFrame;
    }

    private void ResolveSurfaceSize(out float width, out float height)
    {
        width = TouchPanel.DisplayWidth;
        height = TouchPanel.DisplayHeight;

        if (width > 1f && height > 1f)
        {
            return;
        }

        width = Engine.Graphics?.GraphicsDevice?.PresentationParameters.BackBufferWidth ?? 1f;
        height = Engine.Graphics?.GraphicsDevice?.PresentationParameters.BackBufferHeight ?? 1f;
        width = Math.Max(width, 1f);
        height = Math.Max(height, 1f);
    }

    private void PrepareLayout(float width, float height)
    {
        float minDimension = MathF.Min(width, height);
        float scale = Math.Clamp(_config.TouchScale, 0.65f, 1.8f);

        _leftStickRadius = minDimension * Math.Clamp(_config.TouchLeftStickRadius, 0.1f, 0.2f) * scale;
        _buttonRadius = minDimension * Math.Clamp(_config.TouchButtonRadius, 0.08f, 0.14f) * scale;
        _actionSpacing = _buttonRadius * Math.Clamp(_config.TouchActionSpacing, 1.05f, 2f);

        _leftStickCenter = new Vector2(
            width * Math.Clamp(_config.TouchLeftStickX, 0.06f, 0.45f),
            height * Math.Clamp(_config.TouchLeftStickY, 0.4f, 0.95f));

        _actionCenter = new Vector2(
            width * Math.Clamp(_config.TouchActionX, 0.52f, 0.95f),
            height * Math.Clamp(_config.TouchActionY, 0.4f, 0.95f));

        _buttonCenterA = _actionCenter + new Vector2(0f, _actionSpacing);
        _buttonCenterB = _actionCenter + new Vector2(_actionSpacing, 0f);
        _buttonCenterX = _actionCenter + new Vector2(-_actionSpacing, 0f);
        _buttonCenterY = _actionCenter + new Vector2(0f, -_actionSpacing);

        float shoulderY = height * Math.Clamp(_config.TouchShoulderY, 0.06f, 0.3f);
        _leftTriggerCenter = new Vector2(width * 0.1f, shoulderY * 0.9f);
        float startSelectY = height * Math.Clamp(_config.TouchStartSelectY, 0.06f, 0.3f);
        _backCenter = new Vector2(width * 0.79f, startSelectY);
        _startCenter = new Vector2(width * 0.93f, startSelectY);
    }

    private bool IsExternalInputConnected()
    {
        DateTime now = DateTime.UtcNow;
        if (_lastExternalInputCheckUtc != DateTime.MinValue && (now - _lastExternalInputCheckUtc).TotalMilliseconds < 500)
        {
            return _externalInputConnected;
        }

        _lastExternalInputCheckUtc = now;
        bool connected = DetectExternalInputConnected();
        if (connected != _externalInputConnected)
        {
            _externalInputConnected = connected;
            _logger.Log(
                LogLevel.Info,
                "INPUT",
                connected
                    ? "External input detected (gamepad/keyboard/mouse). Touch controls disabled."
                    : "No external input detected. Touch controls enabled.");
        }

        return _externalInputConnected;
    }

    private static bool DetectExternalInputConnected()
    {
        if (GamePad.GetState(PlayerIndex.One).IsConnected)
        {
            return true;
        }

        try
        {
            int[] ids = InputDevice.GetDeviceIds();
            for (int i = 0; i < ids.Length; i++)
            {
                InputDevice? device = InputDevice.GetDevice(ids[i]);
                if (device == null || device.IsVirtual || !device.IsExternal)
                {
                    continue;
                }

                int sources = (int)device.Sources;
                bool keyboard = (sources & SourceKeyboardMask) == SourceKeyboardMask;
                bool mouse = (sources & SourceMouseMask) == SourceMouseMask;
                bool gamepad = (sources & SourceGamepadMask) == SourceGamepadMask || (sources & SourceJoystickMask) == SourceJoystickMask;

                if (keyboard || mouse || gamepad)
                {
                    return true;
                }
            }
        }
        catch
        {
        }

        return false;
    }

    private static Color ApplyAlpha(Color color, float alpha)
    {
        return new Color(color.R, color.G, color.B, (byte)Math.Clamp((int)MathF.Round(alpha * 255f), 0, 255));
    }

    private void DrawFaceButton(SpriteBatch spriteBatch, SpriteFont? font, BitmapFallbackFont fallbackFont, Texture2D pixel, Vector2 center, float radius, string label, bool pressed, Color baseColor, float alpha, string promptKey)
    {
        float drawRadius = pressed ? radius * 1.04f : radius;
        if (DrawPromptTexture(spriteBatch, promptKey, center, drawRadius, pressed ? ApplyAlpha(Color.White, alpha) : ApplyAlpha(new Color(225, 234, 245), alpha * 0.93f)))
        {
            return;
        }

        Color color = ApplyAlpha(baseColor, pressed ? alpha : alpha * 0.8f);
        if (pressed)
        {
            color = Color.Lerp(color, ApplyAlpha(Color.White, alpha), 0.2f);
        }

        DrawCircle(spriteBatch, center, radius, color);
        DrawButtonLabel(spriteBatch, font, fallbackFont, pixel, label, center, radius, pressed, alpha);
    }

    private void DrawDigitalButton(SpriteBatch spriteBatch, SpriteFont? font, BitmapFallbackFont fallbackFont, Texture2D pixel, Vector2 center, float radius, string label, bool pressed, Color baseColor, float alpha, string promptKey)
    {
        float drawRadius = pressed ? radius * 1.04f : radius;
        if (DrawPromptTexture(spriteBatch, promptKey, center, drawRadius, pressed ? ApplyAlpha(Color.White, alpha) : ApplyAlpha(new Color(222, 232, 244), alpha * 0.9f)))
        {
            return;
        }

        Color color = ApplyAlpha(baseColor, pressed ? alpha : alpha * 0.72f);
        if (pressed)
        {
            color = Color.Lerp(color, ApplyAlpha(Color.White, alpha), 0.15f);
        }

        DrawCircle(spriteBatch, center, radius, color);
        DrawButtonLabel(spriteBatch, font, fallbackFont, pixel, label, center, radius, pressed, alpha);
    }

    private void DrawButtonLabel(SpriteBatch spriteBatch, SpriteFont? font, BitmapFallbackFont fallbackFont, Texture2D pixel, string label, Vector2 center, float radius, bool pressed, float alpha)
    {
        Color textColor = pressed ? ApplyAlpha(Color.White, alpha) : ApplyAlpha(new Color(232, 238, 245), alpha * 0.9f);

        if (font != null)
        {
            float scale = Math.Clamp(radius / 28f, 0.38f, 0.85f);
            Vector2 size = font.MeasureString(label) * scale;
            Vector2 pos = center - size * 0.5f;
            spriteBatch.DrawString(font, label, pos, textColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return;
        }

        float fallbackScale = Math.Clamp(radius / 14f, 0.45f, 1.2f);
        float width = Math.Max(1f, label.Length) * 6f * fallbackScale;
        float height = 7f * fallbackScale;
        Vector2 fallbackPos = new Vector2(center.X - width * 0.5f, center.Y - height * 0.5f);
        fallbackFont.DrawString(spriteBatch, pixel, label, fallbackPos, textColor, fallbackScale);
    }

    private bool DrawPromptTexture(SpriteBatch spriteBatch, string key, Vector2 center, float radius, Color color)
    {
        if (!_promptTextures.TryGetValue(key, out Texture2D? texture))
        {
            return false;
        }

        float size = Math.Max(texture.Width, texture.Height);
        if (size <= 0f)
        {
            return false;
        }

        float scale = (radius * 2f) / size;
        Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
        spriteBatch.Draw(texture, center, null, color, 0f, origin, scale, SpriteEffects.None, 0f);
        return true;
    }

    private void EnsurePromptTextures(GraphicsDevice graphicsDevice)
    {
        if (_promptTexturesInitialized)
        {
            return;
        }

        _promptTexturesInitialized = true;
        foreach (string sourceRoot in EnumeratePromptSourceRoots())
        {
            if (!Directory.Exists(sourceRoot))
            {
                continue;
            }

            TryLoadPromptTextures(graphicsDevice, sourceRoot);
            if (_promptTextures.Count <= 0)
            {
                continue;
            }

            _logger.Log(LogLevel.Info, "INPUT", "Touch prompt textures loaded", context: $"count={_promptTextures.Count}; root={sourceRoot}");
            return;
        }

        TryLoadPromptTexturesFromAssets(graphicsDevice);
        if (_promptTextures.Count > 0)
        {
            _logger.Log(LogLevel.Info, "INPUT", "Touch prompt textures loaded from Android assets", context: $"count={_promptTextures.Count}");
            return;
        }

        _logger.Log(LogLevel.Warn, "INPUT", "Touch custom icons not found. Falling back to generated touch buttons.");
    }

    private void TryLoadPromptTextures(GraphicsDevice graphicsDevice, string sourceRoot)
    {
        Dictionary<string, string[]> map = BuildCustomPromptPathMap();

        foreach ((string key, string[] candidates) in map)
        {
            LoadPromptTexture(graphicsDevice, sourceRoot, key, candidates);
        }
    }

    private void LoadPromptTexture(GraphicsDevice graphicsDevice, string sourceRoot, string key, string[] candidates)
    {
        if (_promptTextures.ContainsKey(key))
        {
            return;
        }

        for (int i = 0; i < candidates.Length; i++)
        {
            string relative = candidates[i].Replace('/', Path.DirectorySeparatorChar);
            string path = Path.Combine(sourceRoot, relative);
            if (!File.Exists(path))
            {
                continue;
            }

            try
            {
                byte[] raw = File.ReadAllBytes(path);
                if (TryCreateTextureFromBytes(graphicsDevice, raw, out Texture2D texture))
                {
                    _promptTextures[key] = texture;
                    return;
                }
            }
            catch
            {
            }
        }
    }

    private void TryLoadPromptTexturesFromAssets(GraphicsDevice graphicsDevice)
    {
        Dictionary<string, string[]> map = BuildCustomPromptPathMap();

        foreach ((string key, string[] candidates) in map)
        {
            if (_promptTextures.ContainsKey(key))
            {
                continue;
            }

            for (int i = 0; i < candidates.Length; i++)
            {
                string relative = candidates[i];
                if (TryLoadTextureFromAndroidAsset(graphicsDevice, relative, out Texture2D texture))
                {
                    _promptTextures[key] = texture;
                    break;
                }
            }
        }
    }

    private static bool TryLoadTextureFromAndroidAsset(GraphicsDevice graphicsDevice, string assetPath, out Texture2D texture)
    {
        texture = null!;

        try
        {
            var assets = global::Android.App.Application.Context?.Assets;
            if (assets == null)
            {
                return false;
            }

            using Stream stream = assets.Open(assetPath);
            using var memory = new MemoryStream();
            stream.CopyTo(memory);
            return TryCreateTextureFromBytes(graphicsDevice, memory.ToArray(), out texture);
        }
        catch
        {
            return false;
        }
    }

    private static bool TryCreateTextureFromBytes(GraphicsDevice graphicsDevice, byte[] rawBytes, out Texture2D texture)
    {
        texture = null!;
        try
        {
            byte[] decoded = global::Celeste.CustomIconData.DecodeIfNeeded(rawBytes);
            using var imageStream = new MemoryStream(decoded, writable: false);
            texture = Texture2D.FromStream(graphicsDevice, imageStream);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static Dictionary<string, string[]> BuildCustomPromptPathMap()
    {
        string[] Custom(params string[] names)
        {
            string[] result = new string[names.Length * 4];
            for (int i = 0; i < names.Length; i++)
            {
                string stem = Path.GetFileNameWithoutExtension(names[i]);
                result[i * 4] = "CONTROLES CELESTE/" + stem + ".dat";
                result[i * 4 + 1] = "controls_custom/" + stem + ".dat";
                result[i * 4 + 2] = "CONTROLES CELESTE/" + names[i];
                result[i * 4 + 3] = "controls_custom/" + names[i];
            }

            return result;
        }

        string[] CustomAnalog(params string[] names)
        {
            string[] result = new string[names.Length * 4];
            for (int i = 0; i < names.Length; i++)
            {
                string stem = Path.GetFileNameWithoutExtension(names[i]);
                result[i * 4] = "ARQUIVOSPARAOANALOGICO/" + stem + ".dat";
                result[i * 4 + 1] = "analog_custom/" + stem + ".dat";
                result[i * 4 + 2] = "ARQUIVOSPARAOANALOGICO/" + names[i];
                result[i * 4 + 3] = "analog_custom/" + names[i];
            }

            return result;
        }

        return new Dictionary<string, string[]>(StringComparer.Ordinal)
        {
            ["FACE_A"] = Custom("pular.png"),
            ["FACE_B"] = Custom("dash-back.png"),
            ["FACE_X"] = Custom("dash.png"),
            ["LT"] = Custom("agarrar.png"),
            ["RT"] = Custom("agarrar2.png"),
            ["START"] = Custom("pause-start.png"),
            ["BACK"] = Custom("select.png"),
            ["STICK_BASE"] = CustomAnalog("analogico.png"),
            ["STICK_THUMB"] = CustomAnalog("analogic1.png")
        };
    }

    private static IEnumerable<string> EnumeratePromptSourceRoots()
    {
        var roots = new List<string>();
        if (!string.IsNullOrWhiteSpace(Environment.CurrentDirectory))
        {
            DirectoryInfo? directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
            for (int i = 0; i < 6 && directoryInfo != null; i++)
            {
                roots.Add(directoryInfo.FullName);
                roots.Add(Path.Combine(directoryInfo.FullName, "CELESTEPORT"));
                directoryInfo = directoryInfo.Parent;
            }
        }

        if (!string.IsNullOrWhiteSpace(AppContext.BaseDirectory))
        {
            roots.Add(AppContext.BaseDirectory);
        }

        if (!string.IsNullOrWhiteSpace(Engine.ContentDirectory))
        {
            roots.Add(Engine.ContentDirectory);
        }

        roots.Add("/data/celeste.app/Files");
        roots.Add("/data/celeste.app/Files/Config");

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < roots.Count; i++)
        {
            string root = roots[i];
            if (string.IsNullOrWhiteSpace(root) || !seen.Add(root))
            {
                continue;
            }

            yield return root;
        }
    }

    private void DisposePromptTextures()
    {
        foreach (Texture2D texture in _promptTextures.Values)
        {
            texture.Dispose();
        }

        _promptTextures.Clear();
        _promptTexturesInitialized = false;
    }

    private void DrawCircle(SpriteBatch spriteBatch, Vector2 center, float radius, Color color)
    {
        if (_circleTexture == null)
        {
            return;
        }

        Rectangle destination = new Rectangle(
            (int)MathF.Round(center.X - radius),
            (int)MathF.Round(center.Y - radius),
            Math.Max(1, (int)MathF.Ceiling(radius * 2f)),
            Math.Max(1, (int)MathF.Ceiling(radius * 2f)));

        spriteBatch.Draw(_circleTexture, destination, color);
    }

    private void EnsureCircleTexture(GraphicsDevice graphicsDevice)
    {
        if (_circleTexture != null)
        {
            return;
        }

        const int size = 128;
        var data = new Color[size * size];
        Vector2 center = new Vector2((size - 1) * 0.5f, (size - 1) * 0.5f);
        float radius = center.X;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 delta = new Vector2(x, y) - center;
                float dist = delta.Length() / radius;
                float alpha = Math.Clamp(1f - dist, 0f, 1f);
                alpha = alpha * alpha;
                data[y * size + x] = new Color((byte)255, (byte)255, (byte)255, (byte)Math.Clamp((int)MathF.Round(alpha * 255f), 0, 255));
            }
        }

        _circleTexture = new Texture2D(graphicsDevice, size, size, false, SurfaceFormat.Color);
        _circleTexture.SetData(data);
    }

    private static KeyMap ResolveKeyMap()
    {
        global::Celeste.Settings? settings = global::Celeste.Settings.Instance;
        return new KeyMap
        {
            MoveLeft = FirstKey(settings?.Left, Keys.Left),
            MoveRight = FirstKey(settings?.Right, Keys.Right),
            MoveUp = FirstKey(settings?.Up, Keys.Up),
            MoveDown = FirstKey(settings?.Down, Keys.Down),
            Jump = FirstKey(settings?.Jump, Keys.C),
            Dash = FirstKey(settings?.Dash, Keys.X),
            Talk = FirstKey(settings?.Talk, Keys.X),
            Grab = FirstKey(settings?.Grab, Keys.Z),
            Pause = FirstKey(settings?.Pause, Keys.Enter),
            MenuLeft = FirstKey(settings?.MenuLeft, Keys.Left),
            MenuRight = FirstKey(settings?.MenuRight, Keys.Right),
            MenuUp = FirstKey(settings?.MenuUp, Keys.Up),
            MenuDown = FirstKey(settings?.MenuDown, Keys.Down),
            MenuConfirm = FirstKey(settings?.Confirm, Keys.C),
            MenuCancel = FirstKey(settings?.Cancel, Keys.Escape),
            MenuJournal = FirstKey(settings?.Journal, Keys.Tab)
        };
    }

    private static Keys FirstKey(Binding? binding, Keys fallback)
    {
        if (binding != null)
        {
            List<Keys> keys = binding.Keyboard;
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] != Keys.None)
                {
                    return keys[i];
                }
            }
        }

        return fallback;
    }

    private sealed class KeyMap
    {
        public Keys MoveLeft;
        public Keys MoveRight;
        public Keys MoveUp;
        public Keys MoveDown;
        public Keys Jump;
        public Keys Dash;
        public Keys Talk;
        public Keys Grab;
        public Keys Pause;
        public Keys MenuLeft;
        public Keys MenuRight;
        public Keys MenuUp;
        public Keys MenuDown;
        public Keys MenuConfirm;
        public Keys MenuCancel;
        public Keys MenuJournal;
    }
}
