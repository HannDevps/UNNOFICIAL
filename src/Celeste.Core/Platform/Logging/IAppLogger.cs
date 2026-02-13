using System;

namespace Celeste.Core.Platform.Logging;

public interface IAppLogger
{
    string CurrentSessionLogFile { get; }

    void Log(LogLevel level, string tag, string message, Exception? exception = null, string? context = null);

    void Flush();
}
