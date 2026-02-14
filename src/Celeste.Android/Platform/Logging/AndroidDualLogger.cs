using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Logging;

public sealed class AndroidDualLogger : IAppLogger, IDisposable
{
    private const string UnifiedLogFileName = "tudo_unificado.txt";
    private const int TailScanBytes = 65536;

    private static readonly object SharedWriterSync = new();
    private static StreamWriter? SharedWriter;
    private static string? SharedWriterPath;
    private static int SharedWriterRefCount;
    private static long GlobalLineSequence;

    private readonly string _logsPath;
    private bool _disposed;

    public bool RecoveredUncleanShutdown { get; }

    public string RecoveredUncleanShutdownDetail { get; } = string.Empty;

    public AndroidDualLogger(string logsPath)
    {
        _logsPath = logsPath;

        Directory.CreateDirectory(logsPath);
        CleanupLegacyArtifacts();

        CurrentSessionLogFile = Path.Combine(logsPath, UnifiedLogFileName);

        if (TryDetectPreviousUncleanShutdown(CurrentSessionLogFile, out var detail))
        {
            RecoveredUncleanShutdown = true;
            RecoveredUncleanShutdownDetail = detail;
        }

        AcquireSharedWriter();
        WriteSessionHeader();
    }

    public string CurrentSessionLogFile { get; }

    public void Log(LogLevel level, string tag, string message, Exception? exception = null, string? context = null)
    {
        var safeTag = string.IsNullOrWhiteSpace(tag) ? "CELESTE" : tag;
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        var sequence = Interlocked.Increment(ref GlobalLineSequence);
        var thread = Thread.CurrentThread;
        var line = $"{timestamp} | #{sequence:D6} | {level} | {safeTag} | {message} | tid={thread.ManagedThreadId}; thread={thread.Name ?? "unnamed"}";

        if (!string.IsNullOrWhiteSpace(context))
        {
            line += $" | {context}";
        }

        if (exception is not null)
        {
            line += Environment.NewLine + exception;
        }

        lock (SharedWriterSync)
        {
            if (_disposed || SharedWriter is null)
            {
                return;
            }

            SharedWriter.WriteLine(line);
        }

        switch (level)
        {
            case LogLevel.Info:
                global::Android.Util.Log.Info(safeTag, line);
                break;
            case LogLevel.Warn:
                global::Android.Util.Log.Warn(safeTag, line);
                break;
            default:
                global::Android.Util.Log.Error(safeTag, line);
                break;
        }
    }

    public void PersistCrashReport(string source, Exception exception, string? context)
    {
        try
        {
            Log(LogLevel.Error, "CRASH", $"UNHANDLED_EXCEPTION source={source}", exception, context);
        }
        catch
        {
            // Best-effort only.
        }
    }

    public void Flush()
    {
        lock (SharedWriterSync)
        {
            if (_disposed || SharedWriter is null)
            {
                return;
            }

            SharedWriter.Flush();
        }
    }

    public void Dispose()
    {
        lock (SharedWriterSync)
        {
            if (_disposed)
            {
                return;
            }

            if (SharedWriter is not null)
            {
                SharedWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | #END | INFO | APP | SESSION_CLOSE | pid={Environment.ProcessId}");
                SharedWriter.Flush();
            }

            _disposed = true;

            SharedWriterRefCount = Math.Max(0, SharedWriterRefCount - 1);
            if (SharedWriterRefCount == 0)
            {
                SharedWriter?.Dispose();
                SharedWriter = null;
                SharedWriterPath = null;
            }
        }
    }

    private void WriteSessionHeader()
    {
        var sdkInt = global::Android.OS.Build.VERSION.SdkInt;
        var fingerprint = global::Android.OS.Build.Fingerprint ?? "unknown";
        lock (SharedWriterSync)
        {
            if (SharedWriter is null)
            {
                return;
            }

            SharedWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | #000000 | INFO | APP | SESSION_OPEN | pid={Environment.ProcessId}; utc={DateTime.UtcNow:O}; sdk={sdkInt}; fingerprint={fingerprint}");

            if (RecoveredUncleanShutdown)
            {
                SharedWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | #000000 | WARN | APP | PREVIOUS_SESSION_UNCLEAN_SHUTDOWN | detail={RecoveredUncleanShutdownDetail}");
            }
        }
    }

    private void AcquireSharedWriter()
    {
        lock (SharedWriterSync)
        {
            if (SharedWriter is null || !string.Equals(SharedWriterPath, CurrentSessionLogFile, StringComparison.Ordinal))
            {
                SharedWriter?.Dispose();
                SharedWriterPath = CurrentSessionLogFile;
                SharedWriter = new StreamWriter(new FileStream(CurrentSessionLogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite), new UTF8Encoding(false))
                {
                    AutoFlush = true
                };
            }

            SharedWriterRefCount++;
        }
    }

    private void CleanupLegacyArtifacts()
    {
        TryDeleteFile(Path.Combine(_logsPath, "session_active.marker"));
        TryDeleteFile(Path.Combine(_logsPath, "session_latest.txt"));
        TryDeleteFile(Path.Combine(_logsPath, "crash_last.txt"));
        TryDeleteFile(Path.Combine(_logsPath, "error_log.txt"));

        foreach (var path in SafeEnumerateFiles(_logsPath, "crash_*.txt"))
        {
            TryDeleteFile(path);
        }
    }

    private static bool TryDetectPreviousUncleanShutdown(string unifiedLogPath, out string detail)
    {
        detail = string.Empty;
        var tail = SafeReadTail(unifiedLogPath, TailScanBytes);
        if (string.IsNullOrWhiteSpace(tail))
        {
            return false;
        }

        var lastOpen = tail.LastIndexOf("SESSION_OPEN", StringComparison.Ordinal);
        if (lastOpen < 0)
        {
            return false;
        }

        var lastClose = tail.LastIndexOf("SESSION_CLOSE", StringComparison.Ordinal);
        if (lastClose > lastOpen)
        {
            return false;
        }

        var markerStart = lastOpen;
        var markerLength = tail.IndexOf('\n', markerStart);
        detail = markerLength > markerStart
            ? tail.Substring(markerStart, markerLength - markerStart).Trim()
            : "SESSION_OPEN detected without SESSION_CLOSE.";
        return true;
    }

    private static string SafeReadTail(string path, int maxBytes)
    {
        try
        {
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            if (stream.Length <= 0)
            {
                return string.Empty;
            }

            var bytesToRead = (int)Math.Min(maxBytes, stream.Length);
            stream.Seek(-bytesToRead, SeekOrigin.End);
            var buffer = new byte[bytesToRead];
            var read = stream.Read(buffer, 0, bytesToRead);
            return Encoding.UTF8.GetString(buffer, 0, read);
        }
        catch
        {
            return string.Empty;
        }
    }

    private static IEnumerable<string> SafeEnumerateFiles(string directory, string pattern)
    {
        try
        {
            return Directory.EnumerateFiles(directory, pattern);
        }
        catch
        {
            return Array.Empty<string>();
        }
    }

    private static void TryDeleteFile(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            // Best effort cleanup.
        }
    }
}
