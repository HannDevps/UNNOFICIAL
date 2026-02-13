using System;
using System.Collections.Generic;

namespace Celeste.Core.Platform.Boot;

public sealed class BootExecutionResult
{
    public BootExecutionResult(bool success, BootPhase? failedPhase, string message, Exception? exception, IReadOnlyDictionary<BootPhase, BootPhaseResult> phaseResults)
    {
        Success = success;
        FailedPhase = failedPhase;
        Message = message;
        Exception = exception;
        PhaseResults = phaseResults;
    }

    public bool Success { get; }

    public BootPhase? FailedPhase { get; }

    public string Message { get; }

    public Exception? Exception { get; }

    public IReadOnlyDictionary<BootPhase, BootPhaseResult> PhaseResults { get; }
}
