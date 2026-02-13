using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Celeste.Core.Platform.Logging;

namespace Celeste.Core.Platform.Boot;

public sealed class BootStateMachine
{
    private readonly IReadOnlyList<BootPhase> _orderedPhases;

    public BootStateMachine(IEnumerable<BootPhase>? orderedPhases = null)
    {
        _orderedPhases = (orderedPhases ?? new[]
        {
            BootPhase.BootInitLogger,
            BootPhase.BootInitPaths,
            BootPhase.BootApplyFullscreen,
            BootPhase.BootValidateContent,
            BootPhase.BootInitInput,
            BootPhase.BootInitAudio,
            BootPhase.BootStartGame
        }).ToList();
    }

    public BootExecutionResult Execute(Func<BootPhase, BootPhaseResult> phaseExecutor, BootPhase startPhase, IAppLogger? logger = null)
    {
        if (!_orderedPhases.Contains(startPhase))
        {
            return new BootExecutionResult(false, null, $"Unknown start phase: {startPhase}", null, new Dictionary<BootPhase, BootPhaseResult>());
        }

        var phaseResults = new Dictionary<BootPhase, BootPhaseResult>();
        var startIndex = -1;
        for (var i = 0; i < _orderedPhases.Count; i++)
        {
            if (_orderedPhases[i] == startPhase)
            {
                startIndex = i;
                break;
            }
        }

        for (var i = startIndex; i < _orderedPhases.Count; i++)
        {
            var phase = _orderedPhases[i];
            var watch = Stopwatch.StartNew();
            logger?.Log(LogLevel.Info, "BOOT", $"ENTER {phase}");

            BootPhaseResult result;
            try
            {
                result = phaseExecutor(phase);
            }
            catch (Exception exception)
            {
                result = BootPhaseResult.Failure($"Unhandled exception in {phase}", exception);
            }

            watch.Stop();
            phaseResults[phase] = result;

            if (result.IsSuccess)
            {
                logger?.Log(LogLevel.Info, "BOOT", $"EXIT {phase} | OK | {watch.ElapsedMilliseconds}ms | {result.Message}");
                continue;
            }

            logger?.Log(LogLevel.Error, "BOOT", $"EXIT {phase} | FAIL | {watch.ElapsedMilliseconds}ms | {result.Message}", result.Exception);
            return new BootExecutionResult(false, phase, result.Message, result.Exception, phaseResults);
        }

        return new BootExecutionResult(true, null, "BOOT_COMPLETED", null, phaseResults);
    }
}
