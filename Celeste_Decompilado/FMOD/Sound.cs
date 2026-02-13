using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD;

public class Sound : HandleBase
{
	public RESULT release()
	{
		RESULT num = FMOD_Sound_Release(rawPtr);
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
		RESULT result = FMOD_Sound_GetSystemObject(rawPtr, out system2);
		system = new System(system2);
		return result;
	}

	public RESULT @lock(uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2)
	{
		return FMOD_Sound_Lock(rawPtr, offset, length, out ptr1, out ptr2, out len1, out len2);
	}

	public RESULT unlock(IntPtr ptr1, IntPtr ptr2, uint len1, uint len2)
	{
		return FMOD_Sound_Unlock(rawPtr, ptr1, ptr2, len1, len2);
	}

	public RESULT setDefaults(float frequency, int priority)
	{
		return FMOD_Sound_SetDefaults(rawPtr, frequency, priority);
	}

	public RESULT getDefaults(out float frequency, out int priority)
	{
		return FMOD_Sound_GetDefaults(rawPtr, out frequency, out priority);
	}

	public RESULT set3DMinMaxDistance(float min, float max)
	{
		return FMOD_Sound_Set3DMinMaxDistance(rawPtr, min, max);
	}

	public RESULT get3DMinMaxDistance(out float min, out float max)
	{
		return FMOD_Sound_Get3DMinMaxDistance(rawPtr, out min, out max);
	}

	public RESULT set3DConeSettings(float insideconeangle, float outsideconeangle, float outsidevolume)
	{
		return FMOD_Sound_Set3DConeSettings(rawPtr, insideconeangle, outsideconeangle, outsidevolume);
	}

	public RESULT get3DConeSettings(out float insideconeangle, out float outsideconeangle, out float outsidevolume)
	{
		return FMOD_Sound_Get3DConeSettings(rawPtr, out insideconeangle, out outsideconeangle, out outsidevolume);
	}

	public RESULT set3DCustomRolloff(ref VECTOR points, int numpoints)
	{
		return FMOD_Sound_Set3DCustomRolloff(rawPtr, ref points, numpoints);
	}

	public RESULT get3DCustomRolloff(out IntPtr points, out int numpoints)
	{
		return FMOD_Sound_Get3DCustomRolloff(rawPtr, out points, out numpoints);
	}

	public RESULT getSubSound(int index, out Sound subsound)
	{
		subsound = null;
		IntPtr subsound2;
		RESULT result = FMOD_Sound_GetSubSound(rawPtr, index, out subsound2);
		subsound = new Sound(subsound2);
		return result;
	}

	public RESULT getSubSoundParent(out Sound parentsound)
	{
		parentsound = null;
		IntPtr parentsound2;
		RESULT result = FMOD_Sound_GetSubSoundParent(rawPtr, out parentsound2);
		parentsound = new Sound(parentsound2);
		return result;
	}

	public RESULT getName(StringBuilder name, int namelen)
	{
		IntPtr intPtr = Marshal.AllocHGlobal(name.Capacity);
		RESULT result = FMOD_Sound_GetName(rawPtr, intPtr, namelen);
		StringMarshalHelper.NativeToBuilder(name, intPtr);
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	public RESULT getLength(out uint length, TIMEUNIT lengthtype)
	{
		return FMOD_Sound_GetLength(rawPtr, out length, lengthtype);
	}

	public RESULT getFormat(out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits)
	{
		return FMOD_Sound_GetFormat(rawPtr, out type, out format, out channels, out bits);
	}

	public RESULT getNumSubSounds(out int numsubsounds)
	{
		return FMOD_Sound_GetNumSubSounds(rawPtr, out numsubsounds);
	}

	public RESULT getNumTags(out int numtags, out int numtagsupdated)
	{
		return FMOD_Sound_GetNumTags(rawPtr, out numtags, out numtagsupdated);
	}

	public RESULT getTag(string name, int index, out TAG tag)
	{
		return FMOD_Sound_GetTag(rawPtr, name, index, out tag);
	}

	public RESULT getOpenState(out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy)
	{
		return FMOD_Sound_GetOpenState(rawPtr, out openstate, out percentbuffered, out starving, out diskbusy);
	}

	public RESULT readData(IntPtr buffer, uint length, out uint read)
	{
		return FMOD_Sound_ReadData(rawPtr, buffer, length, out read);
	}

	public RESULT seekData(uint pcm)
	{
		return FMOD_Sound_SeekData(rawPtr, pcm);
	}

	public RESULT setSoundGroup(SoundGroup soundgroup)
	{
		return FMOD_Sound_SetSoundGroup(rawPtr, soundgroup.getRaw());
	}

	public RESULT getSoundGroup(out SoundGroup soundgroup)
	{
		soundgroup = null;
		IntPtr soundgroup2;
		RESULT result = FMOD_Sound_GetSoundGroup(rawPtr, out soundgroup2);
		soundgroup = new SoundGroup(soundgroup2);
		return result;
	}

	public RESULT getNumSyncPoints(out int numsyncpoints)
	{
		return FMOD_Sound_GetNumSyncPoints(rawPtr, out numsyncpoints);
	}

	public RESULT getSyncPoint(int index, out IntPtr point)
	{
		return FMOD_Sound_GetSyncPoint(rawPtr, index, out point);
	}

	public RESULT getSyncPointInfo(IntPtr point, StringBuilder name, int namelen, out uint offset, TIMEUNIT offsettype)
	{
		IntPtr intPtr = Marshal.AllocHGlobal(name.Capacity);
		RESULT result = FMOD_Sound_GetSyncPointInfo(rawPtr, point, intPtr, namelen, out offset, offsettype);
		StringMarshalHelper.NativeToBuilder(name, intPtr);
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	public RESULT addSyncPoint(uint offset, TIMEUNIT offsettype, string name, out IntPtr point)
	{
		return FMOD_Sound_AddSyncPoint(rawPtr, offset, offsettype, name, out point);
	}

	public RESULT deleteSyncPoint(IntPtr point)
	{
		return FMOD_Sound_DeleteSyncPoint(rawPtr, point);
	}

	public RESULT setMode(MODE mode)
	{
		return FMOD_Sound_SetMode(rawPtr, mode);
	}

	public RESULT getMode(out MODE mode)
	{
		return FMOD_Sound_GetMode(rawPtr, out mode);
	}

	public RESULT setLoopCount(int loopcount)
	{
		return FMOD_Sound_SetLoopCount(rawPtr, loopcount);
	}

	public RESULT getLoopCount(out int loopcount)
	{
		return FMOD_Sound_GetLoopCount(rawPtr, out loopcount);
	}

	public RESULT setLoopPoints(uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype)
	{
		return FMOD_Sound_SetLoopPoints(rawPtr, loopstart, loopstarttype, loopend, loopendtype);
	}

	public RESULT getLoopPoints(out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype)
	{
		return FMOD_Sound_GetLoopPoints(rawPtr, out loopstart, loopstarttype, out loopend, loopendtype);
	}

	public RESULT getMusicNumChannels(out int numchannels)
	{
		return FMOD_Sound_GetMusicNumChannels(rawPtr, out numchannels);
	}

	public RESULT setMusicChannelVolume(int channel, float volume)
	{
		return FMOD_Sound_SetMusicChannelVolume(rawPtr, channel, volume);
	}

	public RESULT getMusicChannelVolume(int channel, out float volume)
	{
		return FMOD_Sound_GetMusicChannelVolume(rawPtr, channel, out volume);
	}

	public RESULT setMusicSpeed(float speed)
	{
		return FMOD_Sound_SetMusicSpeed(rawPtr, speed);
	}

	public RESULT getMusicSpeed(out float speed)
	{
		return FMOD_Sound_GetMusicSpeed(rawPtr, out speed);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Sound_SetUserData(rawPtr, userdata);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Sound_GetUserData(rawPtr, out userdata);
	}

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Release(IntPtr sound);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetSystemObject(IntPtr sound, out IntPtr system);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Lock(IntPtr sound, uint offset, uint length, out IntPtr ptr1, out IntPtr ptr2, out uint len1, out uint len2);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Unlock(IntPtr sound, IntPtr ptr1, IntPtr ptr2, uint len1, uint len2);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetDefaults(IntPtr sound, float frequency, int priority);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetDefaults(IntPtr sound, out float frequency, out int priority);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Set3DMinMaxDistance(IntPtr sound, float min, float max);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Get3DMinMaxDistance(IntPtr sound, out float min, out float max);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Set3DConeSettings(IntPtr sound, float insideconeangle, float outsideconeangle, float outsidevolume);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Get3DConeSettings(IntPtr sound, out float insideconeangle, out float outsideconeangle, out float outsidevolume);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Set3DCustomRolloff(IntPtr sound, ref VECTOR points, int numpoints);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_Get3DCustomRolloff(IntPtr sound, out IntPtr points, out int numpoints);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetSubSound(IntPtr sound, int index, out IntPtr subsound);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetSubSoundParent(IntPtr sound, out IntPtr parentsound);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetName(IntPtr sound, IntPtr name, int namelen);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetLength(IntPtr sound, out uint length, TIMEUNIT lengthtype);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetFormat(IntPtr sound, out SOUND_TYPE type, out SOUND_FORMAT format, out int channels, out int bits);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetNumSubSounds(IntPtr sound, out int numsubsounds);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetNumTags(IntPtr sound, out int numtags, out int numtagsupdated);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetTag(IntPtr sound, string name, int index, out TAG tag);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetOpenState(IntPtr sound, out OPENSTATE openstate, out uint percentbuffered, out bool starving, out bool diskbusy);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_ReadData(IntPtr sound, IntPtr buffer, uint length, out uint read);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SeekData(IntPtr sound, uint pcm);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetSoundGroup(IntPtr sound, IntPtr soundgroup);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetSoundGroup(IntPtr sound, out IntPtr soundgroup);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetNumSyncPoints(IntPtr sound, out int numsyncpoints);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetSyncPoint(IntPtr sound, int index, out IntPtr point);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetSyncPointInfo(IntPtr sound, IntPtr point, IntPtr name, int namelen, out uint offset, TIMEUNIT offsettype);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_AddSyncPoint(IntPtr sound, uint offset, TIMEUNIT offsettype, string name, out IntPtr point);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_DeleteSyncPoint(IntPtr sound, IntPtr point);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetMode(IntPtr sound, MODE mode);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetMode(IntPtr sound, out MODE mode);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetLoopCount(IntPtr sound, int loopcount);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetLoopCount(IntPtr sound, out int loopcount);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetLoopPoints(IntPtr sound, uint loopstart, TIMEUNIT loopstarttype, uint loopend, TIMEUNIT loopendtype);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetLoopPoints(IntPtr sound, out uint loopstart, TIMEUNIT loopstarttype, out uint loopend, TIMEUNIT loopendtype);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetMusicNumChannels(IntPtr sound, out int numchannels);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetMusicChannelVolume(IntPtr sound, int channel, float volume);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetMusicChannelVolume(IntPtr sound, int channel, out float volume);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetMusicSpeed(IntPtr sound, float speed);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetMusicSpeed(IntPtr sound, out float speed);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_SetUserData(IntPtr sound, IntPtr userdata);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Sound_GetUserData(IntPtr sound, out IntPtr userdata);

	public Sound(IntPtr raw)
		: base(raw)
	{
	}
}
