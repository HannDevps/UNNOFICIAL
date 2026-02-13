using Celeste.Core.Platform.Audio;
using Celeste.Core.Platform.Filesystem;
using Celeste.Core.Platform.Input;
using Celeste.Core.Platform.Logging;
using Celeste.Core.Platform.Paths;

namespace Celeste.Core.Platform.Services;

public sealed class PlatformServices
{
    public PlatformServices(
        IAppLogger logger,
        IPathsProvider paths,
        IFileSystem fileSystem,
        IInputProvider input,
        IAudioBackend audio)
    {
        Logger = logger;
        Paths = paths;
        FileSystem = fileSystem;
        Input = input;
        Audio = audio;
    }

    public IAppLogger Logger { get; }

    public IPathsProvider Paths { get; }

    public IFileSystem FileSystem { get; }

    public IInputProvider Input { get; }

    public IAudioBackend Audio { get; }
}
