namespace Celeste.Core.Platform.Paths;

public interface IPathsProvider
{
    string BaseDataPath { get; }

    string ContentPath { get; }

    string LogsPath { get; }

    string SavePath { get; }

    DirectoryLayoutResult EnsureDirectoryLayout();
}
