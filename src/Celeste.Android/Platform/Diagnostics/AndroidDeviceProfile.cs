using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace Celeste.Android.Platform.Diagnostics;

public enum AndroidDeviceTier
{
    Low,
    Medium,
    High
}

public sealed class AndroidDeviceProfile
{
    public string ActiveAbi { get; init; } = "unknown";

    public bool Is64BitAbi { get; init; }

    public int CpuCores { get; init; }

    public int ApiLevel { get; init; }

    public long TotalRamMb { get; init; }

    public long AvailableRamMb { get; init; }

    public int MemoryClassMb { get; init; }

    public int LargeMemoryClassMb { get; init; }

    public bool IsLowRamDevice { get; init; }

    public AndroidDeviceTier Tier { get; init; }

    public bool EnableLowMemoryMode => Tier == AndroidDeviceTier.Low;

    public bool EnableAggressiveGarbageCollection => Tier != AndroidDeviceTier.High;

    public bool PreferReachGraphicsProfile => Tier == AndroidDeviceTier.Low;

    public int RuntimeHeartbeatSeconds => Tier == AndroidDeviceTier.Low ? 5 : 10;

    public string ToContextString()
    {
        return $"tier={Tier}; abi={ActiveAbi}; abi64={Is64BitAbi}; api={ApiLevel}; cpuCores={CpuCores}; totalRamMb={TotalRamMb}; availableRamMb={AvailableRamMb}; memoryClassMb={MemoryClassMb}; largeMemoryClassMb={LargeMemoryClassMb}; isLowRamDevice={IsLowRamDevice}";
    }

    public static AndroidDeviceProfile Capture(Context context, string activeAbi)
    {
        var activityManager = context.GetSystemService(Context.ActivityService) as ActivityManager;
        var memoryInfo = new ActivityManager.MemoryInfo();
        activityManager?.GetMemoryInfo(memoryInfo);

        var totalRamMb = memoryInfo.TotalMem > 0 ? memoryInfo.TotalMem / (1024 * 1024) : 0;
        var availableRamMb = memoryInfo.AvailMem > 0 ? memoryInfo.AvailMem / (1024 * 1024) : 0;
        var memoryClassMb = activityManager?.MemoryClass ?? 0;
        var largeMemoryClassMb = activityManager?.LargeMemoryClass ?? 0;
        var lowRamDevice = activityManager?.IsLowRamDevice ?? false;
        var cpuCores = Math.Max(1, System.Environment.ProcessorCount);
        var is64BitAbi = !string.IsNullOrWhiteSpace(activeAbi) && activeAbi.Contains("64", StringComparison.Ordinal);

        var tier = ClassifyDeviceTier(totalRamMb, memoryClassMb, cpuCores, lowRamDevice, is64BitAbi);

        return new AndroidDeviceProfile
        {
            ActiveAbi = activeAbi,
            Is64BitAbi = is64BitAbi,
            CpuCores = cpuCores,
            ApiLevel = (int)Build.VERSION.SdkInt,
            TotalRamMb = totalRamMb,
            AvailableRamMb = availableRamMb,
            MemoryClassMb = memoryClassMb,
            LargeMemoryClassMb = largeMemoryClassMb,
            IsLowRamDevice = lowRamDevice,
            Tier = tier
        };
    }

    private static AndroidDeviceTier ClassifyDeviceTier(long totalRamMb, int memoryClassMb, int cpuCores, bool lowRamDevice, bool is64BitAbi)
    {
        var lowSignals = 0;
        if (lowRamDevice)
        {
            lowSignals++;
        }

        if (totalRamMb > 0 && totalRamMb <= 4096)
        {
            lowSignals++;
        }

        if (memoryClassMb > 0 && memoryClassMb <= 256)
        {
            lowSignals++;
        }

        if (cpuCores <= 4)
        {
            lowSignals++;
        }

        if (!is64BitAbi)
        {
            lowSignals++;
        }

        if (lowSignals >= 2)
        {
            return AndroidDeviceTier.Low;
        }

        if (totalRamMb >= 8192 && cpuCores >= 8 && is64BitAbi)
        {
            return AndroidDeviceTier.High;
        }

        return AndroidDeviceTier.Medium;
    }
}
