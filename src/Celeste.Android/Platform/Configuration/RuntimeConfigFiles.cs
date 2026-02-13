using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Celeste.Core.Platform.Logging;
using Celeste.Core.Platform.Paths;
using Microsoft.Xna.Framework;

namespace Celeste.Android.Platform.Configuration;

public enum RuntimeResolutionModes
{
	Native,
	Fixed,
	Scale
}

public enum RuntimeAspectModes
{
	Fit,
	Fill,
	Stretch
}

public enum RuntimeScaleFilters
{
	Point,
	Linear
}

public enum RuntimeGraphicsProfiles
{
	Reach,
	HiDef
}

public enum RuntimeDepthStencilModes
{
	None,
	Depth16,
	Depth24Stencil8
}

public enum RuntimeQualityLevels
{
	Low,
	Medium,
	High
}

public enum RuntimeOverlayPositions
{
	TopLeft,
	TopRight,
	BottomLeft,
	BottomRight
}

public sealed class RuntimeGameConfig
{
	public RuntimeResolutionModes ResolutionMode { get; set; } = RuntimeResolutionModes.Native;

	public int InternalWidth { get; set; } = 1200;

	public int InternalHeight { get; set; } = 720;

	public bool BackBufferNative { get; set; } = true;

	public int BackBufferWidth { get; set; }

	public int BackBufferHeight { get; set; }

	public float RenderScale { get; set; } = 1f;

	public RuntimeAspectModes AspectMode { get; set; } = RuntimeAspectModes.Fill;

	public RuntimeScaleFilters ScaleFilter { get; set; } = RuntimeScaleFilters.Point;

	public bool VSync { get; set; } = true;

	public string TargetFps { get; set; } = "60";

	public RuntimeGraphicsProfiles GraphicsProfile { get; set; } = RuntimeGraphicsProfiles.HiDef;

	public RuntimeDepthStencilModes DepthStencil { get; set; } = RuntimeDepthStencilModes.Depth24Stencil8;

	public bool Bloom { get; set; } = true;

	public RuntimeQualityLevels PostProcessingQuality { get; set; } = RuntimeQualityLevels.High;

	public RuntimeQualityLevels Particles { get; set; } = RuntimeQualityLevels.High;

	public bool ForceLegacyBlendStates { get; set; } = true;

	public bool ForceCompatibilityCompositor { get; set; } = true;

	public bool EnableDiagnosticLogs { get; set; } = true;

	public bool UseEdgeToEdgeOnAndroid { get; set; } = true;

	public static RuntimeGameConfig CreateDefault()
	{
		return new RuntimeGameConfig();
	}

	public bool TryGetTargetFps(out int targetFps, out bool uncapped)
	{
		uncapped = false;
		targetFps = 60;
		if (string.Equals(TargetFps, "Uncapped", StringComparison.OrdinalIgnoreCase))
		{
			uncapped = true;
			return true;
		}

		if (int.TryParse(TargetFps, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
		{
			targetFps = Math.Clamp(result, 15, 240);
			return true;
		}

		return false;
	}

	public string ToFileContent()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("# Runtime game config");
		stringBuilder.AppendLine("# ResolutionMode = Native|Fixed|Scale");
		stringBuilder.AppendLine("# InternalResolution = 1200x720");
		stringBuilder.AppendLine("# BackBufferResolution = Native|2400x1080");
		stringBuilder.AppendLine("# RenderScale = 1.0");
		stringBuilder.AppendLine("# AspectMode = Fit|Fill|Stretch");
		stringBuilder.AppendLine("# ScaleFilter = Point|Linear");
		stringBuilder.AppendLine("# VSync = true|false");
		stringBuilder.AppendLine("# TargetFps = 30|60|90|120|Uncapped");
		stringBuilder.AppendLine("# GraphicsProfile = Reach|HiDef");
		stringBuilder.AppendLine("# DepthStencil = None|Depth16|Depth24Stencil8");
		stringBuilder.AppendLine("# Bloom = true|false");
		stringBuilder.AppendLine("# PostProcessingQuality = Low|Medium|High");
		stringBuilder.AppendLine("# Particles = Low|Medium|High");
		stringBuilder.AppendLine("# ForceCompatibilityCompositor = true|false");
		stringBuilder.AppendLine("# ForceLegacyBlendStates = true|false");
		stringBuilder.AppendLine("# EnableDiagnosticLogs = true|false");
		stringBuilder.AppendLine("# UseEdgeToEdgeOnAndroid = true|false");
		stringBuilder.AppendLine("ResolutionMode = " + ResolutionMode);
		stringBuilder.AppendLine("InternalResolution = " + InternalWidth + "x" + InternalHeight);
		if (BackBufferNative)
		{
			stringBuilder.AppendLine("BackBufferResolution = Native");
		}
		else
		{
			stringBuilder.AppendLine("BackBufferResolution = " + BackBufferWidth + "x" + BackBufferHeight);
		}
		stringBuilder.AppendLine("RenderScale = " + RenderScale.ToString("0.###", CultureInfo.InvariantCulture));
		stringBuilder.AppendLine("AspectMode = " + AspectMode);
		stringBuilder.AppendLine("ScaleFilter = " + ScaleFilter);
		stringBuilder.AppendLine("VSync = " + VSync.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("TargetFps = " + TargetFps);
		stringBuilder.AppendLine("GraphicsProfile = " + GraphicsProfile);
		stringBuilder.AppendLine("DepthStencil = " + DepthStencil);
		stringBuilder.AppendLine("Bloom = " + Bloom.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("PostProcessingQuality = " + PostProcessingQuality);
		stringBuilder.AppendLine("Particles = " + Particles);
		stringBuilder.AppendLine("ForceCompatibilityCompositor = " + ForceCompatibilityCompositor.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("ForceLegacyBlendStates = " + ForceLegacyBlendStates.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("EnableDiagnosticLogs = " + EnableDiagnosticLogs.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("UseEdgeToEdgeOnAndroid = " + UseEdgeToEdgeOnAndroid.ToString().ToLowerInvariant());
		return stringBuilder.ToString();
	}
}

public sealed class RuntimeOverlayConfig
{
	public bool ShowFps { get; set; }

	public bool ShowMemory { get; set; }

	public bool ShowResolution { get; set; }

	public bool ShowViewport { get; set; }

	public bool ShowScale { get; set; }

	public RuntimeOverlayPositions Position { get; set; } = RuntimeOverlayPositions.TopLeft;

	public float FontScale { get; set; } = 1f;

	public int UpdateIntervalMs { get; set; } = 500;

	public bool Background { get; set; }

	public int Padding { get; set; } = 8;

	public Color TextColor { get; set; } = Color.White;

	public bool HasVisibleData => ShowFps || ShowMemory || ShowResolution || ShowViewport || ShowScale;

	public static RuntimeOverlayConfig CreateDefault()
	{
		return new RuntimeOverlayConfig();
	}

	public string ToFileContent()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("# Runtime diagnostic overlay config");
		stringBuilder.AppendLine("ShowFps = " + ShowFps.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("ShowMemory = " + ShowMemory.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("ShowResolution = " + ShowResolution.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("ShowViewport = " + ShowViewport.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("ShowScale = " + ShowScale.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("Position = " + Position);
		stringBuilder.AppendLine("FontScale = " + FontScale.ToString("0.###", CultureInfo.InvariantCulture));
		stringBuilder.AppendLine("UpdateIntervalMs = " + UpdateIntervalMs.ToString(CultureInfo.InvariantCulture));
		stringBuilder.AppendLine("Background = " + Background.ToString().ToLowerInvariant());
		stringBuilder.AppendLine("Padding = " + Padding.ToString(CultureInfo.InvariantCulture));
		stringBuilder.AppendLine("TextColor = #" + TextColor.A.ToString("X2", CultureInfo.InvariantCulture) + TextColor.R.ToString("X2", CultureInfo.InvariantCulture) + TextColor.G.ToString("X2", CultureInfo.InvariantCulture) + TextColor.B.ToString("X2", CultureInfo.InvariantCulture));
		return stringBuilder.ToString();
	}
}

public sealed class RuntimeGameConfigSource
{
	private readonly IPathsProvider paths;

	private readonly IAppLogger logger;

	private DateTime lastWriteUtc;

	private string activePath = string.Empty;

	public RuntimeGameConfigSource(IPathsProvider paths, IAppLogger logger)
	{
		this.paths = paths;
		this.logger = logger;
		Current = RuntimeGameConfig.CreateDefault();
		Reload(force: true, reason: "startup");
	}

	public RuntimeGameConfig Current { get; private set; }

	public string ActivePath => activePath;

	public bool TryReload(out RuntimeGameConfig config)
	{
		bool changed = Reload(force: false, reason: "hot_reload");
		config = Current;
		return changed;
	}

	public bool Reload(bool force, string reason)
	{
		string text = ResolveGameConfigPath(paths, logger);
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}

		DateTime dateTime = GetLastWriteUtc(text);
		if (!force && string.Equals(activePath, text, StringComparison.Ordinal) && dateTime == lastWriteUtc)
		{
			return false;
		}

		activePath = text;
		EnsureDefaultExists(text, Current.ToFileContent(), logger, "CONFIG");
		Dictionary<string, string> values = ReadValuesFromFile(text, logger, "CONFIG");
		RuntimeGameConfig runtimeGameConfig = RuntimeGameConfig.CreateDefault();
		ApplyGameValues(runtimeGameConfig, values, logger);
		Current = runtimeGameConfig;
		lastWriteUtc = GetLastWriteUtc(text);
		logger.Log(LogLevel.Info, "CONFIG", "Game config loaded", context: "path=" + text + "; reason=" + reason);
		return true;
	}

	public bool Save(RuntimeGameConfig config, string reason)
	{
		if (config == null)
		{
			return false;
		}

		string text = string.IsNullOrWhiteSpace(activePath) ? ResolveGameConfigPath(paths, logger) : activePath;
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}

		try
		{
			string? directoryName = Path.GetDirectoryName(text);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			File.WriteAllText(text, config.ToFileContent());
			activePath = text;
			Current = config;
			lastWriteUtc = GetLastWriteUtc(text);
			logger.Log(LogLevel.Info, "CONFIG", "Game config saved", context: "path=" + text + "; reason=" + reason);
			return true;
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, "CONFIG", "Failed to save game config", ex, "path=" + text + "; reason=" + reason);
			return false;
		}
	}

	private static void ApplyGameValues(RuntimeGameConfig config, Dictionary<string, string> values, IAppLogger logger)
	{
		if (TryGetEnum(values, "ResolutionMode", out RuntimeResolutionModes resolutionMode))
		{
			config.ResolutionMode = resolutionMode;
		}

		if (TryGetResolution(values, "InternalResolution", out int width, out int height, out bool _))
		{
			config.InternalWidth = Math.Clamp(width, 320, 8192);
			config.InternalHeight = Math.Clamp(height, 180, 8192);
		}

		if (values.TryGetValue("BackBufferResolution", out string? value) && !string.IsNullOrWhiteSpace(value))
		{
			if (string.Equals(value.Trim(), "native", StringComparison.OrdinalIgnoreCase))
			{
				config.BackBufferNative = true;
			}
			else if (TryGetResolution(values, "BackBufferResolution", out int width2, out int height2, out bool _))
			{
				config.BackBufferNative = false;
				config.BackBufferWidth = Math.Clamp(width2, 320, 8192);
				config.BackBufferHeight = Math.Clamp(height2, 180, 8192);
			}
		}

		if (TryGetFloat(values, "RenderScale", out float result))
		{
			config.RenderScale = Math.Clamp(result, 0.25f, 2f);
		}

		if (TryGetEnum(values, "AspectMode", out RuntimeAspectModes aspectMode))
		{
			config.AspectMode = aspectMode;
		}

		if (TryGetEnum(values, "ScaleFilter", out RuntimeScaleFilters scaleFilter))
		{
			config.ScaleFilter = scaleFilter;
		}

		if (TryGetBool(values, "VSync", out bool result2))
		{
			config.VSync = result2;
		}

		if (values.TryGetValue("TargetFps", out string? value2) && !string.IsNullOrWhiteSpace(value2))
		{
			config.TargetFps = value2.Trim();
		}

		if (TryGetEnum(values, "GraphicsProfile", out RuntimeGraphicsProfiles graphicsProfile))
		{
			config.GraphicsProfile = graphicsProfile;
		}

		if (TryGetEnum(values, "DepthStencil", out RuntimeDepthStencilModes depthStencil))
		{
			config.DepthStencil = depthStencil;
		}

		if (TryGetBool(values, "Bloom", out bool result3))
		{
			config.Bloom = result3;
		}

		if (TryGetEnum(values, "PostProcessingQuality", out RuntimeQualityLevels postProcessingQuality))
		{
			config.PostProcessingQuality = postProcessingQuality;
		}

		if (TryGetEnum(values, "Particles", out RuntimeQualityLevels particles))
		{
			config.Particles = particles;
		}

		if (TryGetBool(values, "ForceCompatibilityCompositor", out bool forceCompatibilityCompositor))
		{
			config.ForceCompatibilityCompositor = forceCompatibilityCompositor;
		}

		if (TryGetBool(values, "ForceLegacyBlendStates", out bool result4))
		{
			config.ForceLegacyBlendStates = result4;
		}

		if (TryGetBool(values, "EnableDiagnosticLogs", out bool result5))
		{
			config.EnableDiagnosticLogs = result5;
		}

		if (TryGetBool(values, "UseEdgeToEdgeOnAndroid", out bool result6))
		{
			config.UseEdgeToEdgeOnAndroid = result6;
		}

		if (!config.TryGetTargetFps(out _, out _))
		{
			logger.Log(LogLevel.Warn, "CONFIG", "Invalid TargetFps in game.config. Using default 60.", context: "value=" + config.TargetFps);
			config.TargetFps = "60";
		}
	}

	private static string ResolveGameConfigPath(IPathsProvider paths, IAppLogger logger)
	{
		string text = "/data/celeste.app/Files/game.config";
		string text2 = "/data/celeste.app/Files/Config/game.config";
		string text3 = Path.Combine(paths.BaseDataPath, "game.config");
		string text4 = FirstExistingPath(text, text2, text3);
		if (!string.IsNullOrWhiteSpace(text4))
		{
			return text4;
		}

		if (CanWritePath(text))
		{
			TryMirrorAlias(text, text2, RuntimeGameConfig.CreateDefault().ToFileContent(), logger, "CONFIG");
			return text;
		}

		logger.Log(LogLevel.Warn, "CONFIG", "Preferred game.config path unavailable. Falling back to app data path.", context: "preferred=" + text + "; fallback=" + text3);
		return text3;
	}

	private static void TryMirrorAlias(string primaryPath, string aliasPath, string content, IAppLogger logger, string tag)
	{
		try
		{
			Directory.CreateDirectory(Path.GetDirectoryName(primaryPath) ?? string.Empty);
			Directory.CreateDirectory(Path.GetDirectoryName(aliasPath) ?? string.Empty);
			if (!File.Exists(primaryPath))
			{
				File.WriteAllText(primaryPath, content);
			}

			if (!File.Exists(aliasPath))
			{
				File.WriteAllText(aliasPath, content);
			}
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, tag, "Failed to mirror config alias", ex, "primary=" + primaryPath + "; alias=" + aliasPath);
		}
	}

	private static bool CanWritePath(string path)
	{
		try
		{
			string? directoryName = Path.GetDirectoryName(path);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			string text = path + ".probe";
			File.WriteAllText(text, "ok");
			File.Delete(text);
			return true;
		}
		catch
		{
			return false;
		}
	}

	private static DateTime GetLastWriteUtc(string path)
	{
		try
		{
			if (File.Exists(path))
			{
				return File.GetLastWriteTimeUtc(path);
			}
		}
		catch
		{
		}

		return DateTime.MinValue;
	}

	private static string FirstExistingPath(params string[] candidates)
	{
		foreach (string text in candidates)
		{
			if (!string.IsNullOrWhiteSpace(text) && File.Exists(text))
			{
				return text;
			}
		}

		return string.Empty;
	}

	private static void EnsureDefaultExists(string path, string defaultContent, IAppLogger logger, string tag)
	{
		try
		{
			string? directoryName = Path.GetDirectoryName(path);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			if (!File.Exists(path))
			{
				File.WriteAllText(path, defaultContent);
				logger.Log(LogLevel.Info, tag, "Default config created", context: "path=" + path);
			}
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, tag, "Failed to create default config", ex, "path=" + path);
		}
	}

	private static Dictionary<string, string> ReadValuesFromFile(string path, IAppLogger logger, string tag)
	{
		try
		{
			if (!File.Exists(path))
			{
				return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			}

			string text = File.ReadAllText(path);
			return ParseKeyValues(text);
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, tag, "Failed reading config. Using defaults.", ex, "path=" + path);
			return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}
	}

	private static Dictionary<string, string> ParseKeyValues(string content)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		string[] array = content.Replace("\r\n", "\n").Split('\n');
		foreach (string text in array)
		{
			string text2 = text.Trim();
			if (text2.Length == 0 || text2.StartsWith("#") || text2.StartsWith(";") || text2.StartsWith("//"))
			{
				continue;
			}

			int num = text2.IndexOf('=');
			if (num <= 0)
			{
				continue;
			}

			string key = text2.Substring(0, num).Trim();
			string value = text2.Substring(num + 1).Trim();
			dictionary[key] = value;
		}

		return dictionary;
	}

	private static bool TryGetBool(Dictionary<string, string> values, string key, out bool value)
	{
		value = false;
		if (!values.TryGetValue(key, out string? value2))
		{
			return false;
		}

		if (bool.TryParse(value2, out bool result))
		{
			value = result;
			return true;
		}

		if (string.Equals(value2, "1", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "on", StringComparison.OrdinalIgnoreCase))
		{
			value = true;
			return true;
		}

		if (string.Equals(value2, "0", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "no", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "off", StringComparison.OrdinalIgnoreCase))
		{
			value = false;
			return true;
		}

		return false;
	}

	private static bool TryGetFloat(Dictionary<string, string> values, string key, out float value)
	{
		value = 0f;
		return values.TryGetValue(key, out string? value2) && float.TryParse(value2, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
	}

	private static bool TryGetEnum<TEnum>(Dictionary<string, string> values, string key, out TEnum value) where TEnum : struct
	{
		if (values.TryGetValue(key, out string? value2) && Enum.TryParse(value2, ignoreCase: true, out value))
		{
			return true;
		}

		value = default(TEnum);
		return false;
	}

	private static bool TryGetResolution(Dictionary<string, string> values, string key, out int width, out int height, out bool native)
	{
		width = 0;
		height = 0;
		native = false;
		if (!values.TryGetValue(key, out string? value) || string.IsNullOrWhiteSpace(value))
		{
			return false;
		}

		if (string.Equals(value.Trim(), "native", StringComparison.OrdinalIgnoreCase))
		{
			native = true;
			return true;
		}

		string[] array = value.ToLowerInvariant().Split('x', 'X');
		if (array.Length != 2)
		{
			return false;
		}

		if (!int.TryParse(array[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out width))
		{
			return false;
		}

		if (!int.TryParse(array[1].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out height))
		{
			return false;
		}

		return width > 0 && height > 0;
	}
}

public sealed class RuntimeOverlayConfigSource
{
	private readonly IPathsProvider paths;

	private readonly IAppLogger logger;

	private DateTime lastWriteUtc;

	private string activePath = string.Empty;

	public RuntimeOverlayConfigSource(IPathsProvider paths, IAppLogger logger)
	{
		this.paths = paths;
		this.logger = logger;
		Current = RuntimeOverlayConfig.CreateDefault();
		Reload(force: true, reason: "startup");
	}

	public RuntimeOverlayConfig Current { get; private set; }

	public string ActivePath => activePath;

	public bool TryReload(out RuntimeOverlayConfig config)
	{
		bool changed = Reload(force: false, reason: "hot_reload");
		config = Current;
		return changed;
	}

	public bool Reload(bool force, string reason)
	{
		string text = ResolveOverlayConfigPath(paths, logger);
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}

		DateTime dateTime = GetLastWriteUtc(text);
		if (!force && string.Equals(activePath, text, StringComparison.Ordinal) && dateTime == lastWriteUtc)
		{
			return false;
		}

		activePath = text;
		EnsureDefaultExists(text, Current.ToFileContent(), logger, "OVERLAY");
		Dictionary<string, string> values = ReadValuesFromFile(text, logger, "OVERLAY");
		RuntimeOverlayConfig runtimeOverlayConfig = RuntimeOverlayConfig.CreateDefault();
		ApplyOverlayValues(runtimeOverlayConfig, values, logger);
		Current = runtimeOverlayConfig;
		lastWriteUtc = GetLastWriteUtc(text);
		logger.Log(LogLevel.Info, "OVERLAY", "Overlay config loaded", context: "path=" + text + "; reason=" + reason);
		return true;
	}

	public bool Save(RuntimeOverlayConfig config, string reason)
	{
		if (config == null)
		{
			return false;
		}

		string text = string.IsNullOrWhiteSpace(activePath) ? ResolveOverlayConfigPath(paths, logger) : activePath;
		if (string.IsNullOrWhiteSpace(text))
		{
			return false;
		}

		try
		{
			string? directoryName = Path.GetDirectoryName(text);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			File.WriteAllText(text, config.ToFileContent());
			activePath = text;
			Current = config;
			lastWriteUtc = GetLastWriteUtc(text);
			logger.Log(LogLevel.Info, "OVERLAY", "Overlay config saved", context: "path=" + text + "; reason=" + reason);
			return true;
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, "OVERLAY", "Failed to save overlay config", ex, "path=" + text + "; reason=" + reason);
			return false;
		}
	}

	private static void ApplyOverlayValues(RuntimeOverlayConfig config, Dictionary<string, string> values, IAppLogger logger)
	{
		if (TryGetBool(values, "ShowFps", out bool result))
		{
			config.ShowFps = result;
		}

		if (TryGetBool(values, "ShowMemory", out bool result2))
		{
			config.ShowMemory = result2;
		}

		if (TryGetBool(values, "ShowResolution", out bool result3))
		{
			config.ShowResolution = result3;
		}

		if (TryGetBool(values, "ShowViewport", out bool result4))
		{
			config.ShowViewport = result4;
		}

		if (TryGetBool(values, "ShowScale", out bool result5))
		{
			config.ShowScale = result5;
		}

		if (TryGetEnum(values, "Position", out RuntimeOverlayPositions position))
		{
			config.Position = position;
		}

		if (TryGetFloat(values, "FontScale", out float result6))
		{
			config.FontScale = Math.Clamp(result6, 0.5f, 4f);
		}

		if (values.TryGetValue("UpdateIntervalMs", out string? value) && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result7))
		{
			config.UpdateIntervalMs = Math.Clamp(result7, 100, 10000);
		}

		if (TryGetBool(values, "Background", out bool result8))
		{
			config.Background = result8;
		}

		if (values.TryGetValue("Padding", out string? value2) && int.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result9))
		{
			config.Padding = Math.Clamp(result9, 0, 64);
		}

		if (values.TryGetValue("TextColor", out string? value3) && TryParseColor(value3, out Color color))
		{
			config.TextColor = color;
		}
		else if (values.ContainsKey("TextColor"))
		{
			logger.Log(LogLevel.Warn, "OVERLAY", "Invalid TextColor. Keeping default white.", context: "value=" + value3);
		}
	}

	private static bool TryParseColor(string value, out Color color)
	{
		color = Color.White;
		if (string.IsNullOrWhiteSpace(value))
		{
			return false;
		}

		string text = value.Trim();
		if (text.StartsWith("#", StringComparison.Ordinal))
		{
			text = text.Substring(1);
			if (text.Length == 6 && uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint result))
			{
				byte r = (byte)((result >> 16) & 0xFFu);
				byte g = (byte)((result >> 8) & 0xFFu);
				byte b = (byte)(result & 0xFFu);
				color = new Color(r, g, b, byte.MaxValue);
				return true;
			}

			if (text.Length == 8 && uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint result2))
			{
				byte a = (byte)((result2 >> 24) & 0xFFu);
				byte r2 = (byte)((result2 >> 16) & 0xFFu);
				byte g2 = (byte)((result2 >> 8) & 0xFFu);
				byte b2 = (byte)(result2 & 0xFFu);
				color = new Color(r2, g2, b2, a);
				return true;
			}
		}

		string[] array = text.Split(',');
		if (array.Length is < 3 or > 4)
		{
			return false;
		}

		if (!byte.TryParse(array[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out byte result3))
		{
			return false;
		}

		if (!byte.TryParse(array[1].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out byte result4))
		{
			return false;
		}

		if (!byte.TryParse(array[2].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out byte result5))
		{
			return false;
		}

		byte a2 = byte.MaxValue;
		if (array.Length == 4 && !byte.TryParse(array[3].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out a2))
		{
			return false;
		}

		color = new Color(result3, result4, result5, a2);
		return true;
	}

	private static string ResolveOverlayConfigPath(IPathsProvider paths, IAppLogger logger)
	{
		string text = "/data/celeste.app/Files/Overlay/overlayconfig";
		string text2 = Path.Combine(paths.BaseDataPath, "Overlay", "overlayconfig");
		if (File.Exists(text))
		{
			return text;
		}

		if (File.Exists(text2))
		{
			return text2;
		}

		if (CanWritePath(text))
		{
			return text;
		}

		logger.Log(LogLevel.Warn, "OVERLAY", "Preferred overlayconfig path unavailable. Falling back to app data path.", context: "preferred=" + text + "; fallback=" + text2);
		return text2;
	}

	private static bool CanWritePath(string path)
	{
		try
		{
			string? directoryName = Path.GetDirectoryName(path);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			string text = path + ".probe";
			File.WriteAllText(text, "ok");
			File.Delete(text);
			return true;
		}
		catch
		{
			return false;
		}
	}

	private static DateTime GetLastWriteUtc(string path)
	{
		try
		{
			if (File.Exists(path))
			{
				return File.GetLastWriteTimeUtc(path);
			}
		}
		catch
		{
		}

		return DateTime.MinValue;
	}

	private static void EnsureDefaultExists(string path, string defaultContent, IAppLogger logger, string tag)
	{
		try
		{
			string? directoryName = Path.GetDirectoryName(path);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}

			if (!File.Exists(path))
			{
				File.WriteAllText(path, defaultContent);
				logger.Log(LogLevel.Info, tag, "Default config created", context: "path=" + path);
			}
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, tag, "Failed to create default config", ex, "path=" + path);
		}
	}

	private static Dictionary<string, string> ReadValuesFromFile(string path, IAppLogger logger, string tag)
	{
		try
		{
			if (!File.Exists(path))
			{
				return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			}

			string text = File.ReadAllText(path);
			return ParseKeyValues(text);
		}
		catch (Exception ex)
		{
			logger.Log(LogLevel.Warn, tag, "Failed reading config. Using defaults.", ex, "path=" + path);
			return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}
	}

	private static Dictionary<string, string> ParseKeyValues(string content)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		string[] array = content.Replace("\r\n", "\n").Split('\n');
		foreach (string text in array)
		{
			string text2 = text.Trim();
			if (text2.Length == 0 || text2.StartsWith("#") || text2.StartsWith(";") || text2.StartsWith("//"))
			{
				continue;
			}

			int num = text2.IndexOf('=');
			if (num <= 0)
			{
				continue;
			}

			string key = text2.Substring(0, num).Trim();
			string value = text2.Substring(num + 1).Trim();
			dictionary[key] = value;
		}

		return dictionary;
	}

	private static bool TryGetBool(Dictionary<string, string> values, string key, out bool value)
	{
		value = false;
		if (!values.TryGetValue(key, out string? value2))
		{
			return false;
		}

		if (bool.TryParse(value2, out bool result))
		{
			value = result;
			return true;
		}

		if (string.Equals(value2, "1", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "on", StringComparison.OrdinalIgnoreCase))
		{
			value = true;
			return true;
		}

		if (string.Equals(value2, "0", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "no", StringComparison.OrdinalIgnoreCase) || string.Equals(value2, "off", StringComparison.OrdinalIgnoreCase))
		{
			value = false;
			return true;
		}

		return false;
	}

	private static bool TryGetFloat(Dictionary<string, string> values, string key, out float value)
	{
		value = 0f;
		return values.TryGetValue(key, out string? value2) && float.TryParse(value2, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
	}

	private static bool TryGetEnum<TEnum>(Dictionary<string, string> values, string key, out TEnum value) where TEnum : struct
	{
		if (values.TryGetValue(key, out string? value2) && Enum.TryParse(value2, ignoreCase: true, out value))
		{
			return true;
		}

		value = default(TEnum);
		return false;
	}
}
