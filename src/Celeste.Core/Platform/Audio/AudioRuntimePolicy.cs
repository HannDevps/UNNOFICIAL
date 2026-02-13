using System;

namespace Celeste.Core.Platform.Audio;

public static class AudioRuntimePolicy
{
    public const string EnableFmodOnAndroidSwitch = "Celeste.Android.EnableFmodAudio";

    public static bool IsFmodEnabledOnAndroid()
    {
        return AppContext.TryGetSwitch(EnableFmodOnAndroidSwitch, out var enabled) && enabled;
    }

    public static bool ShouldForceSilentAudio()
    {
        return OperatingSystem.IsAndroid() && !IsFmodEnabledOnAndroid();
    }
}
