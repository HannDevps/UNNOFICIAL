namespace Celeste.Core.Platform.Boot;

public enum BootPhase
{
    BootInitLogger,
    BootInitPaths,
    BootApplyFullscreen,
    BootValidateContent,
    BootInitInput,
    BootInitAudio,
    BootStartGame
}
