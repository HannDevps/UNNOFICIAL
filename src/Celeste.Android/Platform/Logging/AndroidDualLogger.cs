using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Logging;

public sealed class AndroidDualLogger : IAppLogger, IDisposable
{
    private const string UnifiedLogFileName = "tudo_unificado.txt";

    private static readonly object SharedWriterSync = new();
    private static StreamWriter? SharedWriter;
    private static string? SharedWriterPath;
    private static int SharedWriterRefCount;
    private static long GlobalLineSequence;

    private readonly string _logsPath;
    private readonly string _sessionStateFile;
    private readonly string _lastSessionPointerFile;
    private bool _disposed;

    public bool RecoveredUncleanShutdown { get; }

    public string RecoveredUncleanShutdownDetail { get; } = string.Empty;

    public AndroidDualLogger(string logsPath)
    {
        _logsPath = logsPath;
        _sessionStateFile = Path.Combine(logsPath, "session_active.marker");
        _lastSessionPointerFile = Path.Combine(logsPath, "session_latest.txt");

        Directory.CreateDirectory(logsPath);

        if (File.Exists(_sessionStateFile))
        {
            RecoveredUncleanShutdown = true;
            RecoveredUncleanShutdownDetail = SafeReadText(_sessionStateFile);
        }

        CurrentSessionLogFile = Path.Combine(logsPath, UnifiedLogFileName);
        AcquireSharedWriter();

        WriteSessionLifecycleMarker("ACTIVE");
        SafeWriteText(_lastSessionPointerFile, CurrentSessionLogFile + Environment.NewLine);
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
        var timestamp = DateTime.Now;
        var crashFileName = $"crash_{timestamp:yyyy-MM-dd_HH-mm-ss}.txt";
        var crashFilePath = Path.Combine(_logsPath, crashFileName);
        var crashBody = BuildCrashBody(source, exception, context);

        try
        {
            SafeWriteText(crashFilePath, crashBody);
            SafeWriteText(Path.Combine(_logsPath, "crash_last.txt"), crashBody);
            WriteSessionLifecycleMarker($"CRASH:{source}");
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
        var shouldDeleteMarker = false;

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
                shouldDeleteMarker = true;
            }
        }

        if (shouldDeleteMarker)
        {
            TryDeleteSessionMarker();
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

    private static string BuildCrashBody(string source, Exception exception, string? context)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
        builder.AppendLine($"ProcessId: {Environment.ProcessId}");
        builder.AppendLine($"Source: {source}");
        if (!string.IsNullOrWhiteSpace(context))
        {
            builder.AppendLine($"Context: {context}");
        }

        builder.AppendLine("Exception:");
        builder.AppendLine(exception.ToString());
        return builder.ToString();
    }

    private void WriteSessionLifecycleMarker(string state)
    {
        var marker = $"state={state}; pid={Environment.ProcessId}; utc={DateTime.UtcNow:O}; log={CurrentSessionLogFile}";
        SafeWriteText(_sessionStateFile, marker);
    }

    private void TryDeleteSessionMarker()
    {
        try
        {
            if (File.Exists(_sessionStateFile))
            {
                File.Delete(_sessionStateFile);
            }
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }

    private static void SafeWriteText(string path, string content)
    {
        try
        {
            File.WriteAllText(path, content);
        }
        catch
        {
            // Ignore write failures for best-effort diagnostics.
        }
    }

    private static string SafeReadText(string path)
    {
        try
        {
            return File.ReadAllText(path).Trim();
        }
        catch
        {
            return string.Empty;
        }
    }
}
