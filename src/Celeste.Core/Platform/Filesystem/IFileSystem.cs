using System.Collections.Generic;
using System.IO;

namespace Celeste.Core.Platform.Filesystem;

public interface IFileSystem
{
    string ResolvePath(string path);

    bool FileExists(string path);

    bool DirectoryExists(string path);

    IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);

    IEnumerable<string> EnumerateDirectories(string path);

    IEnumerable<string> EnumerateEntries(string path);

    Stream OpenRead(string path);

    Stream OpenWrite(string path, bool overwrite = true);

    void CreateDirectory(string path);

    void DeleteFile(string path);
}
