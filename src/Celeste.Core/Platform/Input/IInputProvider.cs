namespace Celeste.Core.Platform.Input;

public interface IInputProvider
{
    string CurrentInputSummary { get; }

    void Update();

    bool IsRetryPressed();

    bool IsBackPressed();

    bool IsDiagnosticComboActive();
}
