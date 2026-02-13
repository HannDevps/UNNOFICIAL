using System;

namespace Celeste.Core.Platform.Boot;

public sealed class BootPhaseResult
{
    private BootPhaseResult(bool isSuccess, string message, Exception? exception, object? payload)
    {
        IsSuccess = isSuccess;
        Message = message;
        Exception = exception;
        Payload = payload;
    }

    public bool IsSuccess { get; }

    public string Message { get; }

    public Exception? Exception { get; }

    public object? Payload { get; }

    public static BootPhaseResult Success(string message = "OK", object? payload = null)
    {
        return new BootPhaseResult(true, message, null, payload);
    }

    public static BootPhaseResult Failure(string message, Exception? exception = null, object? payload = null)
    {
        return new BootPhaseResult(false, message, exception, payload);
    }
}
