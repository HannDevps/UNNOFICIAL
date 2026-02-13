using System.Collections.Generic;
using System.Linq;

namespace Celeste.Core.Platform.Content;

public sealed class ContentValidationReport
{
    public ContentValidationStatus Status { get; set; }

    public int ScannedDirectoryCount { get; set; }

    public int ScannedFileCount { get; set; }

    public List<ContentValidationIssue> Issues { get; } = new();

    public bool IsValid => Status == ContentValidationStatus.Ok && Issues.All(issue => issue.Severity != IssueSeverity.Error);

    public IEnumerable<ContentValidationIssue> ErrorIssues => Issues.Where(issue => issue.Severity == IssueSeverity.Error);

    public IEnumerable<ContentValidationIssue> WarningIssues => Issues.Where(issue => issue.Severity == IssueSeverity.Warning);
}
