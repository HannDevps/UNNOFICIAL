using System;
using Android.OS;
using Celeste.Core.Platform.Audio;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Audio;

public sealed class FmodAudioBackend : IAudioBackend
{
    private readonly IAppLogger _logger;

    public FmodAudioBackend(IAppLogger logger)
    {
        _logger = logger;
    }

    public string BackendName => "FmodAudioBackend";

    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        try
        {
            _logger.Log(LogLevel.Info, "FMOD", $"Preparing FMOD backend for ABI={Build.SupportedAbis?[0] ?? "unknown"}");
            _logger.Log(LogLevel.Info, "FMOD", "Native FMOD load is deferred to Celeste Audio.Init (P/Invoke), avoiding Java JNI load");
            IsInitialized = true;
            _logger.Log(LogLevel.Info, "FMOD", "FMOD backend ready");
        }
        catch (Exception exception)
        {
            IsInitialized = false;
            _logger.Log(LogLevel.Error, "FMOD", "Failed to prepare FMOD backend", exception);
        }
    }

    public void OnPause()
    {
        _logger.Log(LogLevel.Info, "FMOD", "OnPause requested for FMOD backend");
    }

    public void OnResume()
    {
        _logger.Log(LogLevel.Info, "FMOD", "OnResume requested for FMOD backend");
    }
}
