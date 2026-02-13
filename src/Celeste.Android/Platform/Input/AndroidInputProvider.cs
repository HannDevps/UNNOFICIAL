using Celeste.Core.Platform.Input;
using Celeste.Core.Platform.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Celeste.Android.Platform.Input;

public sealed class AndroidInputProvider : IInputProvider
{
    private readonly IAppLogger _logger;
    private KeyboardState _currentKeyboard;
    private KeyboardState _previousKeyboard;
    private GamePadState _currentGamePad;
    private GamePadState _previousGamePad;
    private bool _lastGamePadConnected;

    public AndroidInputProvider(IAppLogger logger)
    {
        _logger = logger;
        CurrentInputSummary = "GamePad=Unknown Keyboard=Unknown";
        _logger.Log(LogLevel.Info, "INPUT", "Input provider created (gamepad + keyboard only, no touch mappings)");
    }

    public string CurrentInputSummary { get; private set; }

    public void Update()
    {
        _previousKeyboard = _currentKeyboard;
        _previousGamePad = _currentGamePad;

        _currentKeyboard = Keyboard.GetState();
        _currentGamePad = GamePad.GetState(PlayerIndex.One);

        var connected = _currentGamePad.IsConnected;
        if (connected != _lastGamePadConnected)
        {
            _lastGamePadConnected = connected;
            _logger.Log(LogLevel.Info, "INPUT", connected ? "Gamepad connected" : "Gamepad disconnected");
        }

        CurrentInputSummary = $"GamePad={(connected ? "Connected" : "Disconnected")}; Keyboard=Available";
    }

    public bool IsRetryPressed()
    {
        return IsNewButtonPress(Buttons.Start) || IsNewKeyPress(Keys.Enter);
    }

    public bool IsBackPressed()
    {
        return IsNewButtonPress(Buttons.Back) || IsNewButtonPress(Buttons.B) || IsNewKeyPress(Keys.Escape);
    }

    public bool IsDiagnosticComboActive()
    {
        return IsButtonDown(Buttons.Start) || IsKeyDown(Keys.Enter);
    }

    private bool IsNewKeyPress(Keys key)
    {
        return _currentKeyboard.IsKeyDown(key) && !_previousKeyboard.IsKeyDown(key);
    }

    private bool IsNewButtonPress(Buttons button)
    {
        return _currentGamePad.IsButtonDown(button) && !_previousGamePad.IsButtonDown(button);
    }

    private bool IsKeyDown(Keys key)
    {
        return _currentKeyboard.IsKeyDown(key);
    }

    private bool IsButtonDown(Buttons button)
    {
        return _currentGamePad.IsButtonDown(button);
    }
}
