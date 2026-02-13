using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Studio;

public class SOUND_INFO
{
	public byte[] name_or_data;

	public MODE mode;

	public CREATESOUNDEXINFO exinfo;

	public int subsoundindex;

	public string name
	{
		get
		{
			if ((mode & (MODE.OPENMEMORY | MODE.OPENMEMORY_POINT)) == 0 && name_or_data != null)
			{
				int num = Array.IndexOf(name_or_data, (byte)0);
				if (num > 0)
				{
					return Encoding.UTF8.GetString(name_or_data, 0, num);
				}
				return null;
			}
			return null;
		}
	}

	~SOUND_INFO()
	{
		if (exinfo.inclusionlist != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(exinfo.inclusionlist);
		}
	}
}
