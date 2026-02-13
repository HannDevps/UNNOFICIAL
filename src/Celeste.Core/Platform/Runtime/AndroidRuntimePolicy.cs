using System;

namespace Celeste.Core.Platform.Runtime;

public static class AndroidRuntimePolicy
{
    public const string LowMemoryModeSwitch = "Celeste.Android.LowMemoryMode";
    public const string AggressiveGarbageCollectionSwitch = "Celeste.Android.AggressiveGarbageCollection";
    public const string PreferReachGraphicsProfileSwitch = "Celeste.Android.PreferReachGraphicsProfile";
    public const string ForceLegacyBlendStateSwitch = "Celeste.Android.ForceLegacyBlendStates";

    public static bool IsLowMemoryModeEnabled()
    {
        return IsAndroidWithSwitchEnabled(LowMemoryModeSwitch);
    }

    public static bool IsAggressiveGarbageCollectionEnabled()
    {
        return IsAndroidWithSwitchEnabled(AggressiveGarbageCollectionSwitch);
    }

    public static bool ShouldPreferReachGraphicsProfile()
    {
        return IsAndroidWithSwitchEnabled(PreferReachGraphicsProfileSwitch);
    }

    public static bool ShouldForceLegacyBlendStates()
    {
        return IsAndroidWithSwitchEnabled(ForceLegacyBlendStateSwitch);
    }

    private static bool IsAndroidWithSwitchEnabled(string switchName)
    {
        return OperatingSystem.IsAndroid() && AppContext.TryGetSwitch(switchName, out var enabled) && enabled;
    }
}
