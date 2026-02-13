using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Android.Runtime;
using Celeste.Android.Platform.Logging;
using Celeste.Core.Platform.Interop;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Diagnostics;

public static class AndroidCrashReporter
{
    private static readonly object Sync = new();
    private static bool _registered;
    private static IAppLogger? _logger;
    private static string _owner = "unknown";

    public static void Attach(IAppLogger logger, string owner)
    {
        lock (Sync)
        {
            _logger = logger;
            _owner = owner;

            if (_registered)
            {
                return;
            }

            RegisterHandlers();
            _registered = true;
        }

        SafeLog(LogLevel.Info, "EXCEPTION", $"Crash reporter attached owner={owner}");

        if (logger is AndroidDualLogger androidLogger && androidLogger.RecoveredUncleanShutdown)
        {
            SafeLog(LogLevel.Warn, "EXCEPTION", "Detected previous unclean shutdown marker", context: androidLogger.RecoveredUncleanShutdownDetail);
        }
    }

    public static void LogMemoryPressure(IAppLogger logger, string source, string? detail = null, LogLevel level = LogLevel.Warn)
    {
        try
        {
            var runtime = Java.Lang.Runtime.GetRuntime();
            var javaUsed = runtime.TotalMemory() - runtime.FreeMemory();
            var javaMax = runtime.MaxMemory();
            var nativeHeap = TryGetNativeHeapAllocatedSize();
            var context = BuildContext(detail)
                + $"; javaUsedBytes={javaUsed}; javaMaxBytes={javaMax}; nativeHeapBytes={nativeHeap}";

            logger.Log(level, "MEMORY", source, context: context);
        }
        catch (Exception exception)
        {
            logger.Log(LogLevel.Warn, "MEMORY", $"Failed to capture memory snapshot for {source}", exception);
        }
    }

    private static void RegisterHandlers()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            var exception = args.ExceptionObject as Exception
                            ?? new Exception($"Unhandled non-Exception object: {args.ExceptionObject}");
            Handle("AppDomain.CurrentDomain.UnhandledException", exception, $"isTerminating={args.IsTerminating}");
        };

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            Handle("TaskScheduler.UnobservedTaskException", args.Exception, null);
            args.SetObserved();
        };

        AndroidEnvironment.UnhandledExceptionRaiser += (_, args) =>
        {
            Handle("AndroidEnvironment.UnhandledExceptionRaiser", args.Exception, null);
        };
    }

    private static void Handle(string source, Exception exception, string? extraContext)
    {
        var thread = Thread.CurrentThread;
        var context = BuildContext(extraContext)
            + $"; threadName={thread.Name ?? "unnamed"}; threadId={thread.ManagedThreadId}";

        SafeLog(LogLevel.Error, "EXCEPTION", source, exception, context);
        CelestePathBridge.LogError("EXCEPTION", $"{source} | {context}");
        CelestePathBridge.LogError("EXCEPTION", exception.ToString());

        try
        {
            global::Monocle.ErrorLog.Write(exception);
        }
        catch (Exception writeException)
        {
            SafeLog(LogLevel.Warn, "EXCEPTION", "Failed to write Celeste error log", writeException);
        }

        PersistCrashArtifact(source, exception, context);
        SafeFlush();
    }

    private static string BuildContext(string? extraContext)
    {
        var context = $"owner={_owner}; managedBytes={GC.GetTotalMemory(false)}; gc0={GC.CollectionCount(0)}; gc1={GC.CollectionCount(1)}; gc2={GC.CollectionCount(2)}";
        if (!string.IsNullOrWhiteSpace(extraContext))
        {
            context += $"; {extraContext}";
        }

        return context;
    }

    private static long TryGetNativeHeapAllocatedSize()
    {
        try
        {
            var debugType = typeof(global::Android.OS.Debug);
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Static;

            var property = debugType.GetProperty("NativeHeapAllocatedSize", flags);
            if (property?.PropertyType == typeof(long) && property.GetValue(null) is long propertyValue)
            {
                return propertyValue;
            }

            var method = debugType.GetMethod("GetNativeHeapAllocatedSize", flags, binder: null, types: Type.EmptyTypes, modifiers: null);
            if (method?.Invoke(null, null) is long methodValue)
            {
                return methodValue;
            }
        }
        catch
        {
            // Best effort only.
        }

        return 0;
    }

    private static void SafeLog(LogLevel level, string tag, string message, Exception? exception = null, string? context = null)
    {
        try
        {
            _logger?.Log(level, tag, message, exception, context);
        }
        catch
        {
            // Avoid recursive failures while handling fatal exceptions.
        }
    }

    private static void SafeFlush()
    {
        try
        {
            _logger?.Flush();
        }
        catch
        {
            // Best effort only.
        }
    }

    private static void PersistCrashArtifact(string source, Exception exception, string context)
    {
        try
        {
            if (_logger is AndroidDualLogger androidLogger)
            {
                androidLogger.PersistCrashReport(source, exception, context);
                return;
            }

            if (_logger is null)
            {
                return;
            }

            var logsPath = Path.GetDirectoryName(_logger.CurrentSessionLogFile);
            if (string.IsNullOrWhiteSpace(logsPath))
            {
                return;
            }

            Directory.CreateDirectory(logsPath);
            var path = Path.Combine(logsPath, $"crash_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
            File.WriteAllText(path, $"Source: {source}{Environment.NewLine}Context: {context}{Environment.NewLine}{exception}");
        }
        catch
        {
            // Best effort only.
        }
    }
}
