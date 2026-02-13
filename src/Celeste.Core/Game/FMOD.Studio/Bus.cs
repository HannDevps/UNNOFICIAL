using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Studio;

public class Bus : HandleBase
{
	public RESULT getID(out Guid id)
	{
		return FMOD_Studio_Bus_GetID(rawPtr, out id);
	}

	public RESULT getPath(out string path)
	{
		path = null;
		byte[] array = new byte[256];
		int retrieved = 0;
		RESULT rESULT = FMOD_Studio_Bus_GetPath(rawPtr, array, array.Length, out retrieved);
		if (rESULT == RESULT.ERR_TRUNCATED)
		{
			array = new byte[retrieved];
			rESULT = FMOD_Studio_Bus_GetPath(rawPtr, array, array.Length, out retrieved);
		}
		if (rESULT == RESULT.OK)
		{
			path = Encoding.UTF8.GetString(array, 0, retrieved - 1);
		}
		return rESULT;
	}

	public RESULT getVolume(out float volume, out float finalvolume)
	{
		return FMOD_Studio_Bus_GetVolume(rawPtr, out volume, out finalvolume);
	}

	public RESULT setVolume(float volume)
	{
		return FMOD_Studio_Bus_SetVolume(rawPtr, volume);
	}

	public RESULT getPaused(out bool paused)
	{
		return FMOD_Studio_Bus_GetPaused(rawPtr, out paused);
	}

	public RESULT setPaused(bool paused)
	{
		return FMOD_Studio_Bus_SetPaused(rawPtr, paused);
	}

	public RESULT getMute(out bool mute)
	{
		return FMOD_Studio_Bus_GetMute(rawPtr, out mute);
	}

	public RESULT setMute(bool mute)
	{
		return FMOD_Studio_Bus_SetMute(rawPtr, mute);
	}

	public RESULT stopAllEvents(STOP_MODE mode)
	{
		return FMOD_Studio_Bus_StopAllEvents(rawPtr, mode);
	}

	public RESULT lockChannelGroup()
	{
		return FMOD_Studio_Bus_LockChannelGroup(rawPtr);
	}

	public RESULT unlockChannelGroup()
	{
		return FMOD_Studio_Bus_UnlockChannelGroup(rawPtr);
	}

	public RESULT getChannelGroup(out ChannelGroup group)
	{
		group = null;
		IntPtr group2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_Bus_GetChannelGroup(rawPtr, out group2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		group = new ChannelGroup(group2);
		return rESULT;
	}

	[DllImport("fmodstudio")]
	private static extern bool FMOD_Studio_Bus_IsValid(IntPtr bus);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_GetID(IntPtr bus, out Guid id);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_GetPath(IntPtr bus, [Out] byte[] path, int size, out int retrieved);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_GetVolume(IntPtr bus, out float volume, out float finalvolume);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_SetVolume(IntPtr bus, float volume);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_GetPaused(IntPtr bus, out bool paused);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_SetPaused(IntPtr bus, bool paused);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_GetMute(IntPtr bus, out bool mute);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_SetMute(IntPtr bus, bool mute);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_StopAllEvents(IntPtr bus, STOP_MODE mode);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_LockChannelGroup(IntPtr bus);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_UnlockChannelGroup(IntPtr bus);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bus_GetChannelGroup(IntPtr bus, out IntPtr group);

	public Bus(IntPtr raw)
		: base(raw)
	{
	}

	protected override bool isValidInternal()
	{
		return FMOD_Studio_Bus_IsValid(rawPtr);
	}
}
