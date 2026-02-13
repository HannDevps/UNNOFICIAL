using System;
using System.Collections.Generic;
using Celeste.Core.Platform.Interop;
using Monocle;

namespace Celeste;

public static class GameplayBuffers
{
	public static VirtualRenderTarget Gameplay;

	public static VirtualRenderTarget Level;

	public static VirtualRenderTarget ResortDust;

	public static VirtualRenderTarget LightBuffer;

	public static VirtualRenderTarget Light;

	public static VirtualRenderTarget Displacement;

	public static VirtualRenderTarget MirrorSources;

	public static VirtualRenderTarget MirrorMasks;

	public static VirtualRenderTarget SpeedRings;

	public static VirtualRenderTarget Lightning;

	public static VirtualRenderTarget TempA;

	public static VirtualRenderTarget TempB;

	private static List<VirtualRenderTarget> all = new List<VirtualRenderTarget>();

	public static bool EnsureValid(string source, bool forceRecreate = false)
	{
		if (!forceRecreate && AreValid())
		{
			return false;
		}

		string text = forceRecreate ? "forced" : "invalid";
		CelestePathBridge.LogWarn("GPU", "Gameplay buffers recreation requested (" + text + ") from " + source + ".");
		Create();
		return true;
	}

	public static bool AreValid()
	{
		return IsValid(Gameplay, 320, 180) && IsValid(Level, 320, 180) && IsValid(ResortDust, 320, 180) && IsValid(LightBuffer, 1024, 1024) && IsValid(Light, 320, 180) && IsValid(Displacement, 320, 180) && IsValid(MirrorSources, 384, 244) && IsValid(MirrorMasks, 384, 244) && IsValid(SpeedRings, 512, 512) && IsValid(Lightning, 160, 160) && IsValid(TempA, 320, 180) && IsValid(TempB, 320, 180);
	}

	private static bool IsValid(VirtualRenderTarget target, int width, int height)
	{
		if (target == null || target.Target == null || target.IsDisposed)
		{
			return false;
		}

		try
		{
			if (target.Target.Width != width || target.Target.Height != height)
			{
				return false;
			}
		}
		catch (ObjectDisposedException)
		{
			return false;
		}

		return true;
	}

	public static void Create()
	{
		Unload();
		Gameplay = Create(320, 180);
		Level = Create(320, 180);
		ResortDust = Create(320, 180);
		Light = Create(320, 180);
		Displacement = Create(320, 180);
		LightBuffer = Create(1024, 1024);
		MirrorSources = Create(384, 244);
		MirrorMasks = Create(384, 244);
		SpeedRings = Create(512, 512);
		Lightning = Create(160, 160);
		TempA = Create(320, 180);
		TempB = Create(320, 180);
	}

	private static VirtualRenderTarget Create(int width, int height)
	{
		VirtualRenderTarget virtualRenderTarget = VirtualContent.CreateRenderTarget("gameplay-buffer-" + all.Count, width, height);
		all.Add(virtualRenderTarget);
		return virtualRenderTarget;
	}

	public static void Unload()
	{
		foreach (VirtualRenderTarget item in all)
		{
			item.Dispose();
		}
		all.Clear();
	}
}
