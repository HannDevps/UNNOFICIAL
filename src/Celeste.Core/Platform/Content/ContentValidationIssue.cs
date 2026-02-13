namespace Celeste.Core.Platform.Content;

public sealed record ContentValidationIssue(
    IssueSeverity Severity,
    string Code,
    string RelativePath,
    string AbsolutePath,
    string Message,
    string Suggestion
);
