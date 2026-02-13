using System;
using System.IO;

namespace Celeste.Core.Platform.Interop;

public static class CelestePathBridge
{
    private static Func<string> _contentPathProvider;
    private static Func<string> _savePathProvider;
    private static Func<string> _logsPathProvider;
    private static Action<string, string, string>? _logSink;

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
}
