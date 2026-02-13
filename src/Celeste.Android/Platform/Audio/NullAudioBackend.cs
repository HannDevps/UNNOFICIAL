using Celeste.Core.Platform.Audio;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Audio;

public sealed class NullAudioBackend : IAudioBackend
{
    private readonly IAppLogger _logger;

    public NullAudioBackend(IAppLogger logger)
    {
        _logger = logger;
    }

    public string BackendName => "NullAudioBackend";

    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
        _logger.Log(LogLevel.Warn, "AUDIO", "NullAudioBackend active (silent fallback)");
    }

    public void OnPause()
    {
        _logger.Log(LogLevel.Info, "AUDIO", "NullAudioBackend OnPause");
    }

    public void OnResume()
    {
        _logger.Log(LogLevel.Info, "AUDIO", "NullAudioBackend OnResume");
    }
}
