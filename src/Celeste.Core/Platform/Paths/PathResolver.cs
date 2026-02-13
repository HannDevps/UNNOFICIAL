using System;
using System.IO;

namespace Celeste.Core.Platform.Paths;

public static class PathResolver
{
    public static string ResolveRootedPath(IPathsProvider paths, string sourcePath)
    {
        if (string.IsNullOrWhiteSpace(sourcePath))
        {
            return sourcePath;
        }

        if (Path.IsPathRooted(sourcePath))
        {
            return sourcePath;
        }

        var normalized = sourcePath.Replace('\\', '/');

        if (normalized.StartsWith("Content/", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(normalized, "Content", StringComparison.OrdinalIgnoreCase))
        {
            return CombineFromRoot(paths.ContentPath, normalized, "Content");
        }

        if (normalized.StartsWith("Save/", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(normalized, "Save", StringComparison.OrdinalIgnoreCase))
        {
            return CombineFromRoot(paths.SavePath, normalized, "Save");
        }

        if (normalized.StartsWith("Logs/", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(normalized, "Logs", StringComparison.OrdinalIgnoreCase))
        {
            return CombineFromRoot(paths.LogsPath, normalized, "Logs");
        }

        return Path.Combine(paths.BaseDataPath, normalized.Replace('/', Path.DirectorySeparatorChar));
    }

    private static string CombineFromRoot(string rootPath, string sourcePath, string rootToken)
    {
        if (string.Equals(sourcePath, rootToken, StringComparison.OrdinalIgnoreCase))
        {
            return rootPath;
        }

        var suffix = sourcePath[rootToken.Length..].TrimStart('/');
        var osSuffix = suffix.Replace('/', Path.DirectorySeparatorChar);
        return Path.Combine(rootPath, osSuffix);
    }
}
