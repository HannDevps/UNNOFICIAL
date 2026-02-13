using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Celeste.Core.Platform.Filesystem;
using Celeste.Core.Platform.Logging;
using Celeste.Core.Platform.Paths;

namespace Celeste.Android.Platform.Filesystem;

public sealed class AndroidFileSystem : IFileSystem
{
    private readonly IPathsProvider _paths;
    private readonly IAppLogger _logger;

    public AndroidFileSystem(IPathsProvider paths, IAppLogger logger)
    {
        _paths = paths;
        _logger = logger;
    }

    public string ResolvePath(string path)
    {
        return PathResolver.ResolveRootedPath(_paths, path);
    }

    public bool FileExists(string path)
    {
        return File.Exists(ResolvePath(path));
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(ResolvePath(path));
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
        var resolved = ResolvePath(path);
        if (!Directory.Exists(resolved))
        {
            return Enumerable.Empty<string>();
        }

        try
        {
            return Directory.EnumerateFiles(resolved, searchPattern, searchOption).ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FS", $"EnumerateFiles failed for '{resolved}'", exception);
            return Enumerable.Empty<string>();
        }
    }

    public IEnumerable<string> EnumerateDirectories(string path)
    {
        var resolved = ResolvePath(path);
        if (!Directory.Exists(resolved))
        {
            return Enumerable.Empty<string>();
        }

        try
        {
            return Directory.EnumerateDirectories(resolved).ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FS", $"EnumerateDirectories failed for '{resolved}'", exception);
            return Enumerable.Empty<string>();
        }
    }

    public IEnumerable<string> EnumerateEntries(string path)
    {
        var resolved = ResolvePath(path);
        if (!Directory.Exists(resolved))
        {
            return Enumerable.Empty<string>();
        }

        try
        {
            return Directory.EnumerateFileSystemEntries(resolved).ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FS", $"EnumerateEntries failed for '{resolved}'", exception);
            return Enumerable.Empty<string>();
        }
    }

    public Stream OpenRead(string path)
    {
        var resolved = ResolvePath(path);
        try
        {
            return File.OpenRead(resolved);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, "FS", $"OpenRead failed: source='{path}' resolved='{resolved}'", exception);
            throw;
        }
    }

    public Stream OpenWrite(string path, bool overwrite = true)
    {
        var resolved = ResolvePath(path);
        var directory = Path.GetDirectoryName(resolved);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            return new FileStream(
                resolved,
                overwrite ? FileMode.Create : FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, "FS", $"OpenWrite failed: source='{path}' resolved='{resolved}'", exception);
            throw;
        }
    }

    public void CreateDirectory(string path)
    {
        Directory.CreateDirectory(ResolvePath(path));
    }

    public void DeleteFile(string path)
    {
        var resolved = ResolvePath(path);
        if (File.Exists(resolved))
        {
            File.Delete(resolved);
        }
    }
}
