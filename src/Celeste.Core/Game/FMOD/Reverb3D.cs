using System;
using System.Runtime.InteropServices;

namespace FMOD;

public class Reverb3D : HandleBase
{
	public RESULT release()
	{
		RESULT num = FMOD_Reverb3D_Release(getRaw());
		if (num == RESULT.OK)
		{
			rawPtr = IntPtr.Zero;
		}
		return num;
	}

	public RESULT set3DAttributes(ref VECTOR position, float mindistance, float maxdistance)
	{
		return FMOD_Reverb3D_Set3DAttributes(rawPtr, ref position, mindistance, maxdistance);
	}

	public RESULT get3DAttributes(ref VECTOR position, ref float mindistance, ref float maxdistance)
	{
		return FMOD_Reverb3D_Get3DAttributes(rawPtr, ref position, ref mindistance, ref maxdistance);
	}

	public RESULT setProperties(ref REVERB_PROPERTIES properties)
	{
		return FMOD_Reverb3D_SetProperties(rawPtr, ref properties);
	}

	public RESULT getProperties(ref REVERB_PROPERTIES properties)
	{
		return FMOD_Reverb3D_GetProperties(rawPtr, ref properties);
	}

	public RESULT setActive(bool active)
	{
		return FMOD_Reverb3D_SetActive(rawPtr, active);
	}

	public RESULT getActive(out bool active)
	{
		return FMOD_Reverb3D_GetActive(rawPtr, out active);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Reverb3D_SetUserData(rawPtr, userdata);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Reverb3D_GetUserData(rawPtr, out userdata);
	}

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_Release(IntPtr reverb);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_Set3DAttributes(IntPtr reverb, ref VECTOR position, float mindistance, float maxdistance);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_Get3DAttributes(IntPtr reverb, ref VECTOR position, ref float mindistance, ref float maxdistance);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_SetProperties(IntPtr reverb, ref REVERB_PROPERTIES properties);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_GetProperties(IntPtr reverb, ref REVERB_PROPERTIES properties);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_SetActive(IntPtr reverb, bool active);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_GetActive(IntPtr reverb, out bool active);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_SetUserData(IntPtr reverb, IntPtr userdata);

	[DllImport("fmod")]
	private static extern RESULT FMOD_Reverb3D_GetUserData(IntPtr reverb, out IntPtr userdata);

	public Reverb3D(IntPtr raw)
		: base(raw)
	{
	}
}
