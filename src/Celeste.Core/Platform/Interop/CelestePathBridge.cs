using System;
using System.Collections.Generic;
using System.IO;
using Celeste.Core.Platform.Filesystem;

namespace Celeste.Core.Platform.Interop;

public static class CelestePathBridge
{
    private static Func<string> _contentPathProvider;
    private static Func<string> _savePathProvider;
    private static Func<string> _logsPathProvider;
    private static Action<string, string, string>? _logSink;
    private static IFileSystem? _fileSystem;

    public static void Configure(
        Func<string> contentPathProvider,
        Func<string> savePathProvider,
        Func<string> logsPathProvider,
        Action<string, string, string>? logSink = null)
    {
        _contentPathProvider = contentPathProvider;
        _savePathProvider = savePathProvider;
        _logsPathProvider = logsPathProvider;
        _logSink = logSink;
    }

    public static void ConfigureFileSystem(IFileSystem? fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public static string ResolveContentDirectory(string fallbackDirectory)
    {
        return _contentPathProvider != null ? _contentPathProvider() : fallbackDirectory;
    }

    public static string ResolveSaveDirectory(string fallbackDirectory)
    {
        return _savePathProvider != null ? _savePathProvider() : fallbackDirectory;
    }

    public static string ResolveErrorLogPath(string fallbackFileName)
    {
        if (_logsPathProvider == null)
        {
            return fallbackFileName;
        }

        return Path.Combine(_logsPathProvider(), fallbackFileName);
    }

    public static void LogInfo(string tag, string message)
    {
        _logSink?.Invoke("INFO", tag, message);
    }

    public static void LogWarn(string tag, string message)
    {
        _logSink?.Invoke("WARN", tag, message);
    }

    public static void LogError(string tag, string message)
    {
        _logSink?.Invoke("ERROR", tag, message);
    }

    public static bool ContentFileExists(string path)
    {
        var resolved = ResolveContentPath(path);
        if (_fileSystem != null)
        {
            return _fileSystem.FileExists(resolved);
        }

        return File.Exists(resolved);
    }

    public static bool ContentDirectoryExists(string path)
    {
        var resolved = ResolveContentPath(path);
        if (_fileSystem != null)
        {
            return _fileSystem.DirectoryExists(resolved);
        }

        return Directory.Exists(resolved);
    }

    public static IEnumerable<string> EnumerateContentFiles(string path, string searchPattern, SearchOption searchOption)
    {
        var resolved = ResolveContentPath(path);
        if (_fileSystem != null)
        {
            return _fileSystem.EnumerateFiles(resolved, searchPattern, searchOption);
        }

        if (!Directory.Exists(resolved))
        {
            return Array.Empty<string>();
        }

        return Directory.EnumerateFiles(resolved, searchPattern, searchOption);
    }

    public static IEnumerable<string> EnumerateContentDirectories(string path)
    {
        var resolved = ResolveContentPath(path);
        if (_fileSystem != null)
        {
            return _fileSystem.EnumerateDirectories(resolved);
        }

        if (!Directory.Exists(resolved))
        {
            return Array.Empty<string>();
        }

        return Directory.EnumerateDirectories(resolved);
    }

    public static Stream OpenContentRead(string path)
    {
        var resolved = ResolveContentPath(path);
        if (_fileSystem != null)
        {
            return _fileSystem.OpenRead(resolved);
        }

        return File.OpenRead(resolved);
    }

    private static string ResolveContentPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        if (Path.IsPathRooted(path))
        {
            return path;
        }

        var normalized = path.Replace('\\', '/');
        if (string.Equals(normalized, "Content", StringComparison.OrdinalIgnoreCase) ||
            normalized.StartsWith("Content/", StringComparison.OrdinalIgnoreCase))
        {
            var root = ResolveContentDirectory("Content");
            if (string.Equals(normalized, "Content", StringComparison.OrdinalIgnoreCase))
            {
                return root;
            }

            var suffix = normalized.Substring("Content".Length).TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            return Path.Combine(root, suffix);
        }

        return Path.Combine(ResolveContentDirectory("Content"), normalized.Replace('/', Path.DirectorySeparatorChar));
    }
}
