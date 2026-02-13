using System;
using System.Collections.Generic;
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
    private readonly Dictionary<int, Vector2> _menuTouchStart = new();
    private readonly HashSet<int> _menuSwipeConsumed = new();

    private RuntimeGameConfig _config = RuntimeGameConfig.CreateDefault();
    private DateTime _lastExternalInputCheckUtc;
    private bool _externalInputConnected;
    private Texture2D? _circleTexture;

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
    private bool _leftBumper;
    private bool _rightBumper;
    private bool _leftTrigger;
    private bool _rightTrigger;
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

    private Vector2 _leftStickCenter;
    private Vector2 _leftStickKnob;
    private float _leftStickRadius;

    private Vector2 _actionCenter;
    private float _buttonRadius;
    private float _actionSpacing;
    private Vector2 _buttonCenterA;
    private Vector2 _buttonCenterB;
    private Vector2 _buttonCenterX;
    private Vector2 _buttonCenterY;

    private Vector2 _leftBumperCenter;
    private Vector2 _rightBumperCenter;
    private Vector2 _leftTriggerCenter;
    private Vector2 _rightTriggerCenter;
    private Vector2 _startCenter;
    private Vector2 _backCenter;

    private Vector2 _dpadCenter;
    private Vector2 _dpadLeftCenter;
    private Vector2 _dpadRightCenter;
    private Vector2 _dpadUpCenter;
    private Vector2 _dpadDownCenter;
    private float _dpadRadius;

    public AndroidTouchController(IAppLogger logger)
    {
        _logger = logger;
    }

    public void Dispose()
    {
        _circleTexture?.Dispose();
        _circleTexture = null;
    }

    public void ApplyConfig(RuntimeGameConfig config)
    {
        _config = config ?? RuntimeGameConfig.CreateDefault();
    }

    public KeyboardState ApplyKeyboardState(KeyboardState hardwareState)
    {
        ResetFrameState();

        bool gameplayScene = IsGameplayScene();
        bool touchEnabled = _config.TouchEnabled;
        bool showControlsInScene = gameplayScene || !_config.TouchGameplayOnly;
        _showGameplayOverlay = touchEnabled && showControlsInScene;
        _overlayOpacity = Math.Clamp(_config.TouchOpacity, 0.15f, 1f);

        if (!touchEnabled)
        {
            _touchSuppressedByHardware = false;
            _menuPointerDown = false;
            _menuPointerPressedThisFrame = false;
            _menuTouchStart.Clear();
            _menuSwipeConsumed.Clear();
            return hardwareState;
        }

        _touchSuppressedByHardware = _config.TouchAutoDisableOnExternalInput && IsExternalInputConnected();
        if (_touchSuppressedByHardware)
        {
            _showGameplayOverlay = false;
            _menuPointerDown = false;
            _menuPointerPressedThisFrame = false;
            _menuTouchStart.Clear();
            _menuSwipeConsumed.Clear();
            return hardwareState;
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
            _menuTouchStart.Clear();
            _menuSwipeConsumed.Clear();
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

        float alpha = _overlayOpacity;
        Color plate = ApplyAlpha(new Color(10, 16, 25), alpha * 0.65f);
        Color accent = ApplyAlpha(new Color(220, 226, 236), alpha * 0.78f);

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);

        DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 1.04f, plate);
        DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 0.7f, ApplyAlpha(new Color(40, 56, 76), alpha * 0.8f));

        if (_leftStickKnob != Vector2.Zero)
        {
            DrawCircle(spriteBatch, _leftStickKnob, _leftStickRadius * 0.42f, accent);
        }
        else
        {
            DrawCircle(spriteBatch, _leftStickCenter, _leftStickRadius * 0.42f, ApplyAlpha(new Color(140, 154, 170), alpha * 0.55f));
        }

        if (_config.TouchEnableDpad)
        {
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _dpadLeftCenter, _dpadRadius, "L", _moveLeft, new Color(70, 88, 110), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _dpadRightCenter, _dpadRadius, "R", _moveRight, new Color(70, 88, 110), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _dpadUpCenter, _dpadRadius, "U", _moveUp, new Color(70, 88, 110), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _dpadDownCenter, _dpadRadius, "D", _moveDown, new Color(70, 88, 110), alpha);
        }

        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterA, _buttonRadius, "A", _buttonA, new Color(58, 182, 102), alpha);
        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterB, _buttonRadius, "B", _buttonB, new Color(217, 86, 89), alpha);
        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterX, _buttonRadius, "X", _buttonX, new Color(79, 153, 243), alpha);
        DrawFaceButton(spriteBatch, font, fallbackFont, pixel, _buttonCenterY, _buttonRadius, "Y", _buttonY, new Color(223, 192, 63), alpha);

        if (_config.TouchEnableShoulders)
        {
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _leftBumperCenter, _buttonRadius * 0.82f, "LB", _leftBumper, new Color(120, 135, 156), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _rightBumperCenter, _buttonRadius * 0.82f, "RB", _rightBumper, new Color(120, 135, 156), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _leftTriggerCenter, _buttonRadius * 0.75f, "LT", _leftTrigger, new Color(102, 118, 140), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _rightTriggerCenter, _buttonRadius * 0.75f, "RT", _rightTrigger, new Color(102, 118, 140), alpha);
        }

        if (_config.TouchEnableStartSelect)
        {
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _backCenter, _buttonRadius * 0.65f, "BACK", _buttonBack, new Color(86, 104, 128), alpha);
            DrawDigitalButton(spriteBatch, font, fallbackFont, pixel, _startCenter, _buttonRadius * 0.65f, "START", _buttonStart, new Color(86, 104, 128), alpha);
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
        _leftBumper = false;
        _rightBumper = false;
        _leftTrigger = false;
        _rightTrigger = false;
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
            if (distSq <= stickCaptureRadius * stickCaptureRadius && distSq < bestDistSq)
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
                _leftBumper |= IsInsideCircle(pos, _leftBumperCenter, _buttonRadius * 0.9f);
                _rightBumper |= IsInsideCircle(pos, _rightBumperCenter, _buttonRadius * 0.9f);
                _leftTrigger |= IsInsideCircle(pos, _leftTriggerCenter, _buttonRadius * 0.85f);
                _rightTrigger |= IsInsideCircle(pos, _rightTriggerCenter, _buttonRadius * 0.85f);
            }

            if (_config.TouchEnableStartSelect)
            {
                _buttonStart |= IsInsideCircle(pos, _startCenter, _buttonRadius * 0.75f);
                _buttonBack |= IsInsideCircle(pos, _backCenter, _buttonRadius * 0.75f);
            }

            if (_config.TouchEnableDpad)
            {
                _moveLeft |= IsInsideCircle(pos, _dpadLeftCenter, _dpadRadius);
                _moveRight |= IsInsideCircle(pos, _dpadRightCenter, _dpadRadius);
                _moveUp |= IsInsideCircle(pos, _dpadUpCenter, _dpadRadius);
                _moveDown |= IsInsideCircle(pos, _dpadDownCenter, _dpadRadius);
            }
        }

        if (stickTouchId >= 0)
        {
            float length = stickDelta.Length();
            if (length > _leftStickRadius && length > 0f)
            {
                stickDelta *= _leftStickRadius / length;
                length = _leftStickRadius;
            }

            _leftStickKnob = _leftStickCenter + stickDelta * 0.45f;

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
            _leftStickKnob = _leftStickCenter;
        }
    }

    private void UpdateMenuTouchState(TouchCollection touches, float width, float height)
    {
        bool anyDown = false;
        bool pointerCaptured = false;
        float swipeThreshold = MathF.Max(width, height) * 0.06f;

        for (int i = 0; i < touches.Count; i++)
        {
            TouchLocation touch = touches[i];
            Vector2 pos = touch.Position;
            if (!IsInsideSurface(pos, width, height))
            {
                continue;
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
                _menuTouchStart[touch.Id] = pos;
                _menuSwipeConsumed.Remove(touch.Id);
                _menuPointerPressedThisFrame = true;
                continue;
            }

            if (touch.State == TouchLocationState.Moved)
            {
                if (_menuTouchStart.TryGetValue(touch.Id, out Vector2 start) && !_menuSwipeConsumed.Contains(touch.Id))
                {
                    Vector2 delta = pos - start;
                    if (MathF.Abs(delta.X) >= swipeThreshold || MathF.Abs(delta.Y) >= swipeThreshold)
                    {
                        EmitSwipe(delta);
                        _menuSwipeConsumed.Add(touch.Id);
                    }
                }

                continue;
            }

            if (touch.State == TouchLocationState.Released)
            {
                if (_menuTouchStart.TryGetValue(touch.Id, out Vector2 start) && !_menuSwipeConsumed.Contains(touch.Id))
                {
                    Vector2 delta = pos - start;
                    if (MathF.Abs(delta.X) >= swipeThreshold || MathF.Abs(delta.Y) >= swipeThreshold)
                    {
                        EmitSwipe(delta);
                    }
                    else
                    {
                        EmitTap(pos, width, height);
                    }
                }

                _menuTouchStart.Remove(touch.Id);
                _menuSwipeConsumed.Remove(touch.Id);
                continue;
            }

            if (touch.State == TouchLocationState.Invalid)
            {
                _menuTouchStart.Remove(touch.Id);
                _menuSwipeConsumed.Remove(touch.Id);
            }
        }

        _menuPointerDown = anyDown;
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
        else if (delta.Y < 0f)
        {
            _menuPulseUp = true;
        }
        else
        {
            _menuPulseDown = true;
        }
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

        if (_buttonA || _buttonY) AddUnique(merged, ref count, map.Jump);
        if (_buttonB) AddUnique(merged, ref count, map.Dash);
        if (_buttonX)
        {
            AddUnique(merged, ref count, map.Dash);
            AddUnique(merged, ref count, map.Talk);
        }
        if (_leftBumper || _rightBumper || _leftTrigger || _rightTrigger) AddUnique(merged, ref count, map.Grab);
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

        _leftStickRadius = minDimension * Math.Clamp(_config.TouchLeftStickRadius, 0.08f, 0.2f) * scale;
        _buttonRadius = minDimension * Math.Clamp(_config.TouchButtonRadius, 0.05f, 0.14f) * scale;
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

        float shoulderY = height * 0.13f;
        _leftBumperCenter = new Vector2(width * 0.23f, shoulderY);
        _rightBumperCenter = new Vector2(width * 0.77f, shoulderY);
        _leftTriggerCenter = new Vector2(width * 0.1f, shoulderY * 0.9f);
        _rightTriggerCenter = new Vector2(width * 0.9f, shoulderY * 0.9f);
        _backCenter = new Vector2(width * 0.43f, height * 0.12f);
        _startCenter = new Vector2(width * 0.57f, height * 0.12f);

        _dpadCenter = _leftStickCenter + new Vector2(0f, -_leftStickRadius * 2f);
        _dpadRadius = _buttonRadius * 0.78f;
        float dpadGap = _dpadRadius * 1.05f;
        _dpadLeftCenter = _dpadCenter + new Vector2(-dpadGap, 0f);
        _dpadRightCenter = _dpadCenter + new Vector2(dpadGap, 0f);
        _dpadUpCenter = _dpadCenter + new Vector2(0f, -dpadGap);
        _dpadDownCenter = _dpadCenter + new Vector2(0f, dpadGap);
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

    private void DrawFaceButton(SpriteBatch spriteBatch, SpriteFont? font, BitmapFallbackFont fallbackFont, Texture2D pixel, Vector2 center, float radius, string label, bool pressed, Color baseColor, float alpha)
    {
        Color color = ApplyAlpha(baseColor, pressed ? alpha : alpha * 0.8f);
        if (pressed)
        {
            color = Color.Lerp(color, ApplyAlpha(Color.White, alpha), 0.2f);
        }

        DrawCircle(spriteBatch, center, radius, color);
        DrawButtonLabel(spriteBatch, font, fallbackFont, pixel, label, center, radius, pressed, alpha);
    }

    private void DrawDigitalButton(SpriteBatch spriteBatch, SpriteFont? font, BitmapFallbackFont fallbackFont, Texture2D pixel, Vector2 center, float radius, string label, bool pressed, Color baseColor, float alpha)
    {
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
                data[y * size + x] = new Color(255, 255, 255, (byte)Math.Clamp((int)MathF.Round(alpha * 255f), 0, 255));
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
