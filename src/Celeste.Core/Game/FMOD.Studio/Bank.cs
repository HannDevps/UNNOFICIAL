using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Studio;

public class Bank : HandleBase
{
	public RESULT getID(out Guid id)
	{
		return FMOD_Studio_Bank_GetID(rawPtr, out id);
	}

	public RESULT getPath(out string path)
	{
		path = null;
		byte[] array = new byte[256];
		int retrieved = 0;
		RESULT rESULT = FMOD_Studio_Bank_GetPath(rawPtr, array, array.Length, out retrieved);
		if (rESULT == RESULT.ERR_TRUNCATED)
		{
			array = new byte[retrieved];
			rESULT = FMOD_Studio_Bank_GetPath(rawPtr, array, array.Length, out retrieved);
		}
		if (rESULT == RESULT.OK)
		{
			path = Encoding.UTF8.GetString(array, 0, retrieved - 1);
		}
		return rESULT;
	}

	public RESULT unload()
	{
		RESULT rESULT = FMOD_Studio_Bank_Unload(rawPtr);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		rawPtr = IntPtr.Zero;
		return RESULT.OK;
	}

	public RESULT loadSampleData()
	{
		return FMOD_Studio_Bank_LoadSampleData(rawPtr);
	}

	public RESULT unloadSampleData()
	{
		return FMOD_Studio_Bank_UnloadSampleData(rawPtr);
	}

	public RESULT getLoadingState(out LOADING_STATE state)
	{
		return FMOD_Studio_Bank_GetLoadingState(rawPtr, out state);
	}

	public RESULT getSampleLoadingState(out LOADING_STATE state)
	{
		return FMOD_Studio_Bank_GetSampleLoadingState(rawPtr, out state);
	}

	public RESULT getStringCount(out int count)
	{
		return FMOD_Studio_Bank_GetStringCount(rawPtr, out count);
	}

	public RESULT getStringInfo(int index, out Guid id, out string path)
	{
		path = null;
		byte[] array = new byte[256];
		int retrieved = 0;
		RESULT rESULT = FMOD_Studio_Bank_GetStringInfo(rawPtr, index, out id, array, array.Length, out retrieved);
		if (rESULT == RESULT.ERR_TRUNCATED)
		{
			array = new byte[retrieved];
			rESULT = FMOD_Studio_Bank_GetStringInfo(rawPtr, index, out id, array, array.Length, out retrieved);
		}
		if (rESULT == RESULT.OK)
		{
			path = Encoding.UTF8.GetString(array, 0, retrieved - 1);
		}
		return RESULT.OK;
	}

	public RESULT getEventCount(out int count)
	{
		return FMOD_Studio_Bank_GetEventCount(rawPtr, out count);
	}

	public RESULT getEventList(out EventDescription[] array)
	{
		array = null;
		RESULT rESULT = FMOD_Studio_Bank_GetEventCount(rawPtr, out var count);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count == 0)
		{
			array = new EventDescription[0];
			return rESULT;
		}
		IntPtr[] array2 = new IntPtr[count];
		rESULT = FMOD_Studio_Bank_GetEventList(rawPtr, array2, count, out var count2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count2 > count)
		{
			count2 = count;
		}
		array = new EventDescription[count2];
		for (int i = 0; i < count2; i++)
		{
			array[i] = new EventDescription(array2[i]);
		}
		return RESULT.OK;
	}

	public RESULT getBusCount(out int count)
	{
		return FMOD_Studio_Bank_GetBusCount(rawPtr, out count);
	}

	public RESULT getBusList(out Bus[] array)
	{
		array = null;
		RESULT rESULT = FMOD_Studio_Bank_GetBusCount(rawPtr, out var count);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count == 0)
		{
			array = new Bus[0];
			return rESULT;
		}
		IntPtr[] array2 = new IntPtr[count];
		rESULT = FMOD_Studio_Bank_GetBusList(rawPtr, array2, count, out var count2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count2 > count)
		{
			count2 = count;
		}
		array = new Bus[count2];
		for (int i = 0; i < count2; i++)
		{
			array[i] = new Bus(array2[i]);
		}
		return RESULT.OK;
	}

	public RESULT getVCACount(out int count)
	{
		return FMOD_Studio_Bank_GetVCACount(rawPtr, out count);
	}

	public RESULT getVCAList(out VCA[] array)
	{
		array = null;
		RESULT rESULT = FMOD_Studio_Bank_GetVCACount(rawPtr, out var count);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count == 0)
		{
			array = new VCA[0];
			return rESULT;
		}
		IntPtr[] array2 = new IntPtr[count];
		rESULT = FMOD_Studio_Bank_GetVCAList(rawPtr, array2, count, out var count2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count2 > count)
		{
			count2 = count;
		}
		array = new VCA[count2];
		for (int i = 0; i < count2; i++)
		{
			array[i] = new VCA(array2[i]);
		}
		return RESULT.OK;
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Studio_Bank_GetUserData(rawPtr, out userdata);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Studio_Bank_SetUserData(rawPtr, userdata);
	}

	[DllImport("fmodstudio")]
	private static extern bool FMOD_Studio_Bank_IsValid(IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetID(IntPtr bank, out Guid id);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetPath(IntPtr bank, [Out] byte[] path, int size, out int retrieved);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_Unload(IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_LoadSampleData(IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_UnloadSampleData(IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetLoadingState(IntPtr bank, out LOADING_STATE state);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetSampleLoadingState(IntPtr bank, out LOADING_STATE state);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetStringCount(IntPtr bank, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetStringInfo(IntPtr bank, int index, out Guid id, [Out] byte[] path, int size, out int retrieved);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetEventCount(IntPtr bank, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetEventList(IntPtr bank, IntPtr[] array, int capacity, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetBusCount(IntPtr bank, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetBusList(IntPtr bank, IntPtr[] array, int capacity, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetVCACount(IntPtr bank, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetVCAList(IntPtr bank, IntPtr[] array, int capacity, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_GetUserData(IntPtr studiosystem, out IntPtr userdata);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_Bank_SetUserData(IntPtr studiosystem, IntPtr userdata);

	public Bank(IntPtr raw)
		: base(raw)
	{
	}

	protected override bool isValidInternal()
	{
		return FMOD_Studio_Bank_IsValid(rawPtr);
	}
}
