using System;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Celeste.Core.Platform.Audio;
using Celeste.Core.Platform.Logging;

namespace Celeste.Android.Platform.Audio;

public sealed class FmodAudioBackend : IAudioBackend
{
    private const string FmodClassPath = "org/fmod/FMOD";

    private readonly IAppLogger _logger;
    private readonly Context _context;
    private bool _javaBridgeReady;

    public FmodAudioBackend(Context context, IAppLogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public string BackendName => "FmodAudioBackend";

    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        try
        {
            _logger.Log(LogLevel.Info, "FMOD", $"Preparing FMOD backend for ABI={Build.SupportedAbis?[0] ?? "unknown"}");

            _javaBridgeReady = TryInitJavaBridge();
            if (_javaBridgeReady)
            {
                _logger.Log(LogLevel.Info, "FMOD", "FMOD Java bridge initialized (org.fmod.FMOD.init)");
            }
            else
            {
                _logger.Log(LogLevel.Warn, "FMOD", "FMOD Java bridge could not be initialized; native FMOD init may fail on some devices");
            }

            IsInitialized = true;
            _logger.Log(LogLevel.Info, "FMOD", "FMOD backend ready");
        }
        catch (Exception exception)
        {
            IsInitialized = false;
            _logger.Log(LogLevel.Error, "FMOD", "Failed to prepare FMOD backend", exception);
        }
    }

    public void OnPause()
    {
        if (!_javaBridgeReady)
        {
            return;
        }

        TryInvokeOptionalNoArg("onPause", "FMOD Java bridge onPause");
    }

    public void OnResume()
    {
        if (!_javaBridgeReady)
        {
            return;
        }

        TryInvokeOptionalNoArg("onResume", "FMOD Java bridge onResume");
    }

    public void Shutdown()
    {
        if (!_javaBridgeReady)
        {
            return;
        }

        TryInvokeOptionalNoArg("close", "FMOD Java bridge close");
        _javaBridgeReady = false;
        IsInitialized = false;
    }

    private bool TryInitJavaBridge()
    {
        IntPtr classRef = IntPtr.Zero;
        try
        {
            classRef = JNIEnv.FindClass(FmodClassPath);
            if (classRef == IntPtr.Zero)
            {
                if (TryClearPendingJavaException(out var detail))
                {
                    _logger.Log(LogLevel.Warn, "FMOD", "Failed to locate org.fmod.FMOD class", context: detail);
                }

                return false;
            }

            JValue[] initArgs = { new(_context) };
            if (TryCallStaticVoid(classRef, "init", "(Landroid/content/Context;)V", initArgs))
            {
                return true;
            }

            if (TryCallStaticBoolean(classRef, "init", "(Landroid/content/Context;)Z", initArgs, out var initResult))
            {
                return initResult;
            }

            if (TryCallStaticInt(classRef, "init", "(Landroid/content/Context;)I", initArgs, out var initCode))
            {
                return initCode == 0;
            }

            return false;
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FMOD", "Exception while initializing FMOD Java bridge", exception);
            return false;
        }
        finally
        {
            if (classRef != IntPtr.Zero)
            {
                JNIEnv.DeleteLocalRef(classRef);
            }
        }
    }

    private void TryInvokeOptionalNoArg(string methodName, string operation)
    {
        IntPtr classRef = IntPtr.Zero;
        try
        {
            classRef = JNIEnv.FindClass(FmodClassPath);
            if (classRef == IntPtr.Zero)
            {
                TryClearPendingJavaException(out _);
                return;
            }

            if (TryCallStaticVoid(classRef, methodName, "()V"))
            {
                _logger.Log(LogLevel.Info, "FMOD", operation);
            }
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FMOD", operation + " failed", exception);
        }
        finally
        {
            if (classRef != IntPtr.Zero)
            {
                JNIEnv.DeleteLocalRef(classRef);
            }
        }
    }

    private bool TryCallStaticVoid(IntPtr classRef, string methodName, string signature, JValue[]? args = null)
    {
        IntPtr method = JNIEnv.GetStaticMethodID(classRef, methodName, signature);
        if (method == IntPtr.Zero)
        {
            TryClearPendingJavaException(out _);
            return false;
        }

        if (args is null)
        {
            JNIEnv.CallStaticVoidMethod(classRef, method);
        }
        else
        {
            JNIEnv.CallStaticVoidMethod(classRef, method, args);
        }

        if (TryClearPendingJavaException(out var detail))
        {
            _logger.Log(LogLevel.Warn, "FMOD", $"Java call {methodName}{signature} failed", context: detail);
            return false;
        }

        return true;
    }

    private bool TryCallStaticBoolean(IntPtr classRef, string methodName, string signature, JValue[] args, out bool value)
    {
        value = false;
        IntPtr method = JNIEnv.GetStaticMethodID(classRef, methodName, signature);
        if (method == IntPtr.Zero)
        {
            TryClearPendingJavaException(out _);
            return false;
        }

        value = JNIEnv.CallStaticBooleanMethod(classRef, method, args);
        if (TryClearPendingJavaException(out var detail))
        {
            _logger.Log(LogLevel.Warn, "FMOD", $"Java call {methodName}{signature} failed", context: detail);
            value = false;
            return false;
        }

        return true;
    }

    private bool TryCallStaticInt(IntPtr classRef, string methodName, string signature, JValue[] args, out int value)
    {
        value = -1;
        IntPtr method = JNIEnv.GetStaticMethodID(classRef, methodName, signature);
        if (method == IntPtr.Zero)
        {
            TryClearPendingJavaException(out _);
            return false;
        }

        value = JNIEnv.CallStaticIntMethod(classRef, method, args);
        if (TryClearPendingJavaException(out var detail))
        {
            _logger.Log(LogLevel.Warn, "FMOD", $"Java call {methodName}{signature} failed", context: detail);
            value = -1;
            return false;
        }

        return true;
    }

    private static bool TryClearPendingJavaException(out string detail)
    {
        detail = string.Empty;
        IntPtr exceptionRef = IntPtr.Zero;
        try
        {
            exceptionRef = JNIEnv.ExceptionOccurred();
            if (exceptionRef == IntPtr.Zero)
            {
                return false;
            }

            JNIEnv.ExceptionClear();
            detail = "Java exception";

            using var throwable = Java.Lang.Object.GetObject<Java.Lang.Throwable>(exceptionRef, JniHandleOwnership.TransferLocalRef);
            detail = throwable?.ToString() ?? detail;
            exceptionRef = IntPtr.Zero;
            return true;
        }
        catch (Exception exception)
        {
            try
            {
                JNIEnv.ExceptionClear();
            }
            catch
            {
            }

            if (exceptionRef != IntPtr.Zero)
            {
                try
                {
                    JNIEnv.DeleteLocalRef(exceptionRef);
                }
                catch
                {
                }
            }

            detail = "Java exception (failed to decode): " + exception.Message;
            return true;
        }
    }
}
