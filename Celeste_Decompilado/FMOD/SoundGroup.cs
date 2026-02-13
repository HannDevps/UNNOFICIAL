using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD;

public class SoundGroup : HandleBase
{
	public RESULT release()
	{
		RESULT num = FMOD_SoundGroup_Release(getRaw());
		if (num == RESULT.OK)
		{
			rawPtr = IntPtr.Zero;
		}
		return num;
	}

	public RESULT getSystemObject(out System system)
	{
		system = null;
		IntPtr system2;
		RESULT result = FMOD_SoundGroup_GetSystemObject(rawPtr, out system2);
		system = new System(system2);
		return result;
	}

	public RESULT setMaxAudible(int maxaudible)
	{
		return FMOD_SoundGroup_SetMaxAudible(rawPtr, maxaudible);
	}

	public RESULT getMaxAudible(out int maxaudible)
	{
		return FMOD_SoundGroup_GetMaxAudible(rawPtr, out maxaudible);
	}

	public RESULT setMaxAudibleBehavior(SOUNDGROUP_BEHAVIOR behavior)
	{
		return FMOD_SoundGroup_SetMaxAudibleBehavior(rawPtr, behavior);
	}

	public RESULT getMaxAudibleBehavior(out SOUNDGROUP_BEHAVIOR behavior)
	{
		return FMOD_SoundGroup_GetMaxAudibleBehavior(rawPtr, out behavior);
	}

	public RESULT setMuteFadeSpeed(float speed)
	{
		return FMOD_SoundGroup_SetMuteFadeSpeed(rawPtr, speed);
	}

	public RESULT getMuteFadeSpeed(out float speed)
	{
		return FMOD_SoundGroup_GetMuteFadeSpeed(rawPtr, out speed);
	}

	public RESULT setVolume(float volume)
	{
		return FMOD_SoundGroup_SetVolume(rawPtr, volume);
	}

	public RESULT getVolume(out float volume)
	{
		return FMOD_SoundGroup_GetVolume(rawPtr, out volume);
	}

	public RESULT stop()
	{
		return FMOD_SoundGroup_Stop(rawPtr);
	}

	public RESULT getName(StringBuilder name, int namelen)
	{
		IntPtr intPtr = Marshal.AllocHGlobal(name.Capacity);
		RESULT result = FMOD_SoundGroup_GetName(rawPtr, intPtr, namelen);
		StringMarshalHelper.NativeToBuilder(name, intPtr);
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	public RESULT getNumSounds(out int numsounds)
	{
		return FMOD_SoundGroup_GetNumSounds(rawPtr, out numsounds);
	}

	public RESULT getSound(int index, out Sound sound)
	{
		sound = null;
		IntPtr sound2;
		RESULT result = FMOD_SoundGroup_GetSound(rawPtr, index, out sound2);
		sound = new Sound(sound2);
		return result;
	}

	public RESULT getNumPlaying(out int numplaying)
	{
		return FMOD_SoundGroup_GetNumPlaying(rawPtr, out numplaying);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_SoundGroup_SetUserData(rawPtr, userdata);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_SoundGroup_GetUserData(rawPtr, out userdata);
	}

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_Release(IntPtr soundgroup);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetSystemObject(IntPtr soundgroup, out IntPtr system);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_SetMaxAudible(IntPtr soundgroup, int maxaudible);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetMaxAudible(IntPtr soundgroup, out int maxaudible);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_SetMaxAudibleBehavior(IntPtr soundgroup, SOUNDGROUP_BEHAVIOR behavior);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetMaxAudibleBehavior(IntPtr soundgroup, out SOUNDGROUP_BEHAVIOR behavior);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_SetMuteFadeSpeed(IntPtr soundgroup, float speed);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetMuteFadeSpeed(IntPtr soundgroup, out float speed);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_SetVolume(IntPtr soundgroup, float volume);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetVolume(IntPtr soundgroup, out float volume);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_Stop(IntPtr soundgroup);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetName(IntPtr soundgroup, IntPtr name, int namelen);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetNumSounds(IntPtr soundgroup, out int numsounds);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetSound(IntPtr soundgroup, int index, out IntPtr sound);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetNumPlaying(IntPtr soundgroup, out int numplaying);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_SetUserData(IntPtr soundgroup, IntPtr userdata);

	[DllImport("fmod")]
	private static extern RESULT FMOD_SoundGroup_GetUserData(IntPtr soundgroup, out IntPtr userdata);

	public SoundGroup(IntPtr raw)
		: base(raw)
	{
	}
}
