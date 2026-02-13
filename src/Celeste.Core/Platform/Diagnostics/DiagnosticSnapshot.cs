using Celeste.Core.Platform.Content;

namespace Celeste.Core.Platform.Diagnostics;

public sealed class DiagnosticSnapshot
{
    public string BaseDataPath { get; init; } = string.Empty;

    public string ContentPath { get; init; } = string.Empty;

    public string LogsPath { get; init; } = string.Empty;

    public string SavePath { get; init; } = string.Empty;

    public string ActiveAbi { get; init; } = string.Empty;

    public string AudioBackend { get; init; } = string.Empty;

    public bool AudioInitialized { get; init; }

    public bool LowMemoryModeEnabled { get; init; }

    public bool AggressiveGarbageCollectionEnabled { get; init; }

    public bool PreferReachGraphicsProfile { get; init; }

    public bool ForceLegacyBlendStates { get; init; }

    public string InputSummary { get; init; } = string.Empty;

    public ContentValidationStatus ContentStatus { get; init; }

    public int ContentErrorCount { get; init; }

    public int ContentWarningCount { get; init; }

    public string SessionLogFile { get; init; } = string.Empty;
}
