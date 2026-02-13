using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD;

public class ChannelGroup : ChannelControl
{
	public RESULT release()
	{
		RESULT num = FMOD_ChannelGroup_Release(getRaw());
		if (num == RESULT.OK)
		{
			rawPtr = IntPtr.Zero;
		}
		return num;
	}

	public RESULT addGroup(ChannelGroup group, bool propagatedspclock, out DSPConnection connection)
	{
		connection = null;
		IntPtr connection2;
		RESULT result = FMOD_ChannelGroup_AddGroup(getRaw(), group.getRaw(), propagatedspclock, out connection2);
		connection = new DSPConnection(connection2);
		return result;
	}

	public RESULT getNumGroups(out int numgroups)
	{
		return FMOD_ChannelGroup_GetNumGroups(getRaw(), out numgroups);
	}

	public RESULT getGroup(int index, out ChannelGroup group)
	{
		group = null;
		IntPtr group2;
		RESULT result = FMOD_ChannelGroup_GetGroup(getRaw(), index, out group2);
		group = new ChannelGroup(group2);
		return result;
	}

	public RESULT getParentGroup(out ChannelGroup group)
	{
		group = null;
		IntPtr group2;
		RESULT result = FMOD_ChannelGroup_GetParentGroup(getRaw(), out group2);
		group = new ChannelGroup(group2);
		return result;
	}

	public RESULT getName(StringBuilder name, int namelen)
	{
		IntPtr intPtr = Marshal.AllocHGlobal(name.Capacity);
		RESULT result = FMOD_ChannelGroup_GetName(getRaw(), intPtr, namelen);
		StringMarshalHelper.NativeToBuilder(name, intPtr);
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	public RESULT getNumChannels(out int numchannels)
	{
		return FMOD_ChannelGroup_GetNumChannels(getRaw(), out numchannels);
	}

	public RESULT getChannel(int index, out Channel channel)
	{
		channel = null;
		IntPtr channel2;
		RESULT result = FMOD_ChannelGroup_GetChannel(getRaw(), index, out channel2);
		channel = new Channel(channel2);
		return result;
	}

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_Release(IntPtr channelgroup);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_AddGroup(IntPtr channelgroup, IntPtr group, bool propagatedspclock, out IntPtr connection);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_GetNumGroups(IntPtr channelgroup, out int numgroups);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_GetGroup(IntPtr channelgroup, int index, out IntPtr group);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_GetParentGroup(IntPtr channelgroup, out IntPtr group);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_GetName(IntPtr channelgroup, IntPtr name, int namelen);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_GetNumChannels(IntPtr channelgroup, out int numchannels);

	[DllImport("fmod")]
	private static extern RESULT FMOD_ChannelGroup_GetChannel(IntPtr channelgroup, int index, out IntPtr channel);

	public ChannelGroup(IntPtr raw)
		: base(raw)
	{
	}
}
