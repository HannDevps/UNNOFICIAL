using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio;

public struct SOUND_INFO_INTERNAL
{
	private IntPtr name_or_data;

	private MODE mode;

	private CREATESOUNDEXINFO exinfo;

	private int subsoundindex;

	public void assign(out SOUND_INFO publicInfo)
	{
		publicInfo = new SOUND_INFO();
		publicInfo.mode = mode;
		publicInfo.exinfo = exinfo;
		publicInfo.exinfo.inclusionlist = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
		Marshal.WriteInt32(publicInfo.exinfo.inclusionlist, subsoundindex);
		publicInfo.exinfo.inclusionlistnum = 1;
		publicInfo.subsoundindex = subsoundindex;
		if (name_or_data != IntPtr.Zero)
		{
			int num;
			int num2;
			if ((mode & (MODE.OPENMEMORY | MODE.OPENMEMORY_POINT)) != MODE.DEFAULT)
			{
				publicInfo.mode = (MODE)(((uint)publicInfo.mode & 0xEFFFFFFFu) | 0x800);
				num = (int)exinfo.fileoffset;
				publicInfo.exinfo.fileoffset = 0u;
				num2 = (int)exinfo.length;
			}
			else
			{
				num = 0;
				num2 = MarshallingHelper.stringLengthUtf8(name_or_data) + 1;
			}
			publicInfo.name_or_data = new byte[num2];
			Marshal.Copy(new IntPtr(name_or_data.ToInt64() + num), publicInfo.name_or_data, 0, num2);
		}
		else
		{
			publicInfo.name_or_data = null;
		}
	}
}
