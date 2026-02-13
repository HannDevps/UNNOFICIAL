using System;
using Celeste.Core.Platform.Interop;
using Celeste.Core.Platform.Runtime;

namespace Celeste;

internal static class GraphicsBlendCompatibility
{
	private static bool runtimeLegacyBlendFallback;

	private static bool fallbackLogged;

	public static bool UseLegacyBlendStates => OperatingSystem.IsAndroid() &&
		(runtimeLegacyBlendFallback || AndroidRuntimePolicy.ShouldForceLegacyBlendStates());

	public static bool TryEnableLegacyBlendFallback(string source, Exception exception)
	{
		if (!OperatingSystem.IsAndroid() || exception is not ArgumentException)
		{
			return false;
		}

		runtimeLegacyBlendFallback = true;
		if (!fallbackLogged)
		{
			fallbackLogged = true;
			CelestePathBridge.LogWarn("GPU", "BlendFunction Min/Max unsupported by current Android GPU. Legacy blend fallback enabled.");
			CelestePathBridge.LogWarn("GPU", $"Blend fallback source={source}; reason={exception.Message}");
		}

		return true;
	}
}
