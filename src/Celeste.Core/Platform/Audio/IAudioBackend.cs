namespace Celeste.Core.Platform.Audio;

public interface IAudioBackend
{
    string BackendName { get; }

    bool IsInitialized { get; }

    void Initialize();

    void OnPause();

    void OnResume();
}
