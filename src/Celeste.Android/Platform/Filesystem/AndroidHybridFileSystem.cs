using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Android.Content.Res;
using Celeste.Core.Platform.Filesystem;
using Celeste.Core.Platform.Logging;
using Celeste.Core.Platform.Paths;

namespace Celeste.Android.Platform.Filesystem;

public sealed class AndroidHybridFileSystem : IFileSystem
{
    private const string ContentAssetRoot = "Content";

    private readonly AssetManager _assets;
    private readonly IPathsProvider _paths;
    private readonly IAppLogger _logger;
    private readonly object _assetCacheSync = new();
    private readonly Dictionary<string, string[]> _assetListCache = new(StringComparer.Ordinal);
    private readonly Dictionary<string, Regex> _patternCache = new(StringComparer.OrdinalIgnoreCase);

    private bool _assetIndexBuilt;
    private HashSet<string> _assetFiles = new(StringComparer.Ordinal);
    private HashSet<string> _assetDirectories = new(StringComparer.Ordinal);

    public AndroidHybridFileSystem(AssetManager assets, IPathsProvider paths, IAppLogger logger)
    {
        _assets = assets;
        _paths = paths;
        _logger = logger;
    }

    public string ResolvePath(string path)
    {
        return PathResolver.ResolveRootedPath(_paths, path);
    }

    public bool FileExists(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            EnsureAssetIndex();
            return _assetFiles.Contains(assetPath);
        }

        return File.Exists(resolved);
    }

    public bool DirectoryExists(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            EnsureAssetIndex();
            return _assetDirectories.Contains(assetPath);
        }

        return Directory.Exists(resolved);
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            return EnumerateContentFiles(assetPath, searchPattern, searchOption).ToList();
        }

        if (!Directory.Exists(resolved))
        {
            return Enumerable.Empty<string>();
        }

        try
        {
            return Directory.EnumerateFiles(resolved, searchPattern, searchOption).ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FS", $"EnumerateFiles failed for '{resolved}'", exception);
            return Enumerable.Empty<string>();
        }
    }

    public IEnumerable<string> EnumerateDirectories(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            return EnumerateContentDirectories(assetPath).ToList();
        }

        if (!Directory.Exists(resolved))
        {
            return Enumerable.Empty<string>();
        }

        try
        {
            return Directory.EnumerateDirectories(resolved).ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FS", $"EnumerateDirectories failed for '{resolved}'", exception);
            return Enumerable.Empty<string>();
        }
    }

    public IEnumerable<string> EnumerateEntries(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            var directories = EnumerateContentDirectories(assetPath);
            var files = EnumerateContentFiles(assetPath, "*", SearchOption.TopDirectoryOnly);
            return directories.Concat(files).ToList();
        }

        if (!Directory.Exists(resolved))
        {
            return Enumerable.Empty<string>();
        }

        try
        {
            return Directory.EnumerateFileSystemEntries(resolved).ToList();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Warn, "FS", $"EnumerateEntries failed for '{resolved}'", exception);
            return Enumerable.Empty<string>();
        }
    }

    public Stream OpenRead(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            EnsureAssetIndex();
            if (!_assetFiles.Contains(assetPath))
            {
                throw new FileNotFoundException("Asset not found", assetPath);
            }

            try
            {
                return _assets.Open(assetPath);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, "FS", $"OpenRead asset failed: source='{path}' asset='{assetPath}'", exception);
                throw;
            }
        }

        try
        {
            return File.OpenRead(resolved);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, "FS", $"OpenRead failed: source='{path}' resolved='{resolved}'", exception);
            throw;
        }
    }

    public Stream OpenWrite(string path, bool overwrite = true)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            throw new InvalidOperationException($"Content asset is read-only: {assetPath}");
        }

        var directory = Path.GetDirectoryName(resolved);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            return new FileStream(
                resolved,
                overwrite ? FileMode.Create : FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, "FS", $"OpenWrite failed: source='{path}' resolved='{resolved}'", exception);
            throw;
        }
    }

    public void CreateDirectory(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            throw new InvalidOperationException($"Content asset is read-only: {assetPath}");
        }

        Directory.CreateDirectory(resolved);
    }

    public void DeleteFile(string path)
    {
        var resolved = ResolvePath(path);
        if (TryMapToContentAsset(resolved, out var assetPath, out _))
        {
            throw new InvalidOperationException($"Content asset is read-only: {assetPath}");
        }

        if (File.Exists(resolved))
        {
            File.Delete(resolved);
        }
    }

    private IEnumerable<string> EnumerateContentFiles(string assetDirectoryPath, string searchPattern, SearchOption searchOption)
    {
        EnsureAssetIndex();
        var normalizedDirectory = NormalizeAssetPath(assetDirectoryPath);
        var prefix = normalizedDirectory == ContentAssetRoot
            ? ContentAssetRoot + "/"
            : normalizedDirectory + "/";
        var regex = GetSearchPatternRegex(searchPattern);

        foreach (var assetFile in _assetFiles)
        {
            if (!assetFile.StartsWith(prefix, StringComparison.Ordinal))
            {
                continue;
            }

            var parent = GetParentAssetPath(assetFile);
            if (searchOption == SearchOption.TopDirectoryOnly && !string.Equals(parent, normalizedDirectory, StringComparison.Ordinal))
            {
                continue;
            }

            var fileName = Path.GetFileName(assetFile);
            if (!regex.IsMatch(fileName))
            {
                continue;
            }

            yield return ToContentPseudoPath(assetFile);
        }
    }

    private IEnumerable<string> EnumerateContentDirectories(string assetDirectoryPath)
    {
        EnsureAssetIndex();
        var normalizedDirectory = NormalizeAssetPath(assetDirectoryPath);
        foreach (var assetDirectory in _assetDirectories)
        {
            if (string.Equals(assetDirectory, normalizedDirectory, StringComparison.Ordinal))
            {
                continue;
            }

            if (!string.Equals(GetParentAssetPath(assetDirectory), normalizedDirectory, StringComparison.Ordinal))
            {
                continue;
            }

            yield return ToContentPseudoPath(assetDirectory);
        }
    }

    private void EnsureAssetIndex()
    {
        if (_assetIndexBuilt)
        {
            return;
        }

        lock (_assetCacheSync)
        {
            if (_assetIndexBuilt)
            {
                return;
            }

            _assetFiles = new HashSet<string>(StringComparer.Ordinal);
            _assetDirectories = new HashSet<string>(StringComparer.Ordinal);
            var rootEntries = ListAssetEntries(ContentAssetRoot);
            if (rootEntries.Length > 0)
            {
                _assetDirectories.Add(ContentAssetRoot);
                BuildAssetIndexRecursive(ContentAssetRoot);
            }

            _assetIndexBuilt = true;
            _logger.Log(LogLevel.Info, "FS", "Indexed APK assets for Content", context: $"directories={_assetDirectories.Count}; files={_assetFiles.Count}");
        }
    }

    private void BuildAssetIndexRecursive(string directory)
    {
        var children = ListAssetEntries(directory);
        for (var i = 0; i < children.Length; i++)
        {
            var childName = children[i];
            var childPath = directory + "/" + childName;
            var descendants = ListAssetEntries(childPath);
            if (descendants.Length > 0)
            {
                _assetDirectories.Add(childPath);
                BuildAssetIndexRecursive(childPath);
                continue;
            }

            _assetFiles.Add(childPath);
        }
    }

    private string[] ListAssetEntries(string assetPath)
    {
        var normalized = NormalizeAssetPath(assetPath);
        lock (_assetCacheSync)
        {
            if (_assetListCache.TryGetValue(normalized, out var cached))
            {
                return cached;
            }
        }

        string[] listed;
        try
        {
            listed = _assets.List(normalized) ?? Array.Empty<string>();
        }
        catch
        {
            listed = Array.Empty<string>();
        }

        lock (_assetCacheSync)
        {
            _assetListCache[normalized] = listed;
        }

        return listed;
    }

    private static string NormalizeAssetPath(string assetPath)
    {
        var normalized = assetPath.Replace('\\', '/').Trim('/');
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return ContentAssetRoot;
        }

        if (string.Equals(normalized, ContentAssetRoot, StringComparison.OrdinalIgnoreCase))
        {
            return ContentAssetRoot;
        }

        if (normalized.StartsWith(ContentAssetRoot + "/", StringComparison.OrdinalIgnoreCase))
        {
            return ContentAssetRoot + normalized.Substring(ContentAssetRoot.Length);
        }

        return ContentAssetRoot + "/" + normalized;
    }

    private static string GetParentAssetPath(string assetPath)
    {
        var lastSlash = assetPath.LastIndexOf('/');
        if (lastSlash <= 0)
        {
            return ContentAssetRoot;
        }

        return assetPath.Substring(0, lastSlash);
    }

    private string ToContentPseudoPath(string assetPath)
    {
        var normalized = NormalizeAssetPath(assetPath);
        if (string.Equals(normalized, ContentAssetRoot, StringComparison.Ordinal))
        {
            return _paths.ContentPath;
        }

        var relative = normalized.Substring(ContentAssetRoot.Length + 1).Replace('/', Path.DirectorySeparatorChar);
        return Path.Combine(_paths.ContentPath, relative);
    }

    private bool TryMapToContentAsset(string resolvedPath, out string assetPath, out string pseudoPath)
    {
        var normalized = NormalizeResolvedPath(resolvedPath);
        var contentRoot = NormalizeResolvedPath(_paths.ContentPath);

        if (string.Equals(normalized, contentRoot, StringComparison.OrdinalIgnoreCase))
        {
            assetPath = ContentAssetRoot;
            pseudoPath = _paths.ContentPath;
            return true;
        }

        if (normalized.StartsWith(contentRoot + "/", StringComparison.OrdinalIgnoreCase))
        {
            var suffix = normalized.Substring(contentRoot.Length + 1);
            assetPath = ContentAssetRoot + "/" + suffix;
            pseudoPath = Path.Combine(_paths.ContentPath, suffix.Replace('/', Path.DirectorySeparatorChar));
            return true;
        }

        if (string.Equals(normalized, ContentAssetRoot, StringComparison.OrdinalIgnoreCase))
        {
            assetPath = ContentAssetRoot;
            pseudoPath = _paths.ContentPath;
            return true;
        }

        if (normalized.StartsWith(ContentAssetRoot + "/", StringComparison.OrdinalIgnoreCase))
        {
            var suffix = normalized.Substring(ContentAssetRoot.Length + 1);
            assetPath = ContentAssetRoot + "/" + suffix;
            pseudoPath = Path.Combine(_paths.ContentPath, suffix.Replace('/', Path.DirectorySeparatorChar));
            return true;
        }

        assetPath = string.Empty;
        pseudoPath = string.Empty;
        return false;
    }

    private static string NormalizeResolvedPath(string path)
    {
        return (path ?? string.Empty)
            .Replace('\\', '/')
            .TrimEnd('/');
    }

    private Regex GetSearchPatternRegex(string searchPattern)
    {
        var pattern = string.IsNullOrWhiteSpace(searchPattern) ? "*" : searchPattern;
        lock (_assetCacheSync)
        {
            if (_patternCache.TryGetValue(pattern, out var cached))
            {
                return cached;
            }

            var expression = "^" + Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".") + "$";
            var compiled = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            _patternCache[pattern] = compiled;
            return compiled;
        }
    }
}
