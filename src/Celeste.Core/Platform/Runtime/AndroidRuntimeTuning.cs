namespace Celeste.Core.Platform.Runtime;

public static class AndroidRuntimeTuning
{
	public static bool ForceCompatibilityCompositor { get; set; }

	public static bool DisableBloom { get; set; }

	public static bool DisablePostProcessing { get; set; }

	public static int ParticleQualityTier { get; set; } = 2;

	public static bool EnableDiagnosticLogs { get; set; }
}
