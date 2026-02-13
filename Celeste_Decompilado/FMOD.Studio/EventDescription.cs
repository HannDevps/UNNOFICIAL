using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Studio;

public class EventDescription : HandleBase
{
	public RESULT getID(out Guid id)
	{
		return FMOD_Studio_EventDescription_GetID(rawPtr, out id);
	}

	public RESULT getPath(out string path)
	{
		path = null;
		byte[] array = new byte[256];
		int retrieved = 0;
		RESULT rESULT = FMOD_Studio_EventDescription_GetPath(rawPtr, array, array.Length, out retrieved);
		if (rESULT == RESULT.ERR_TRUNCATED)
		{
			array = new byte[retrieved];
			rESULT = FMOD_Studio_EventDescription_GetPath(rawPtr, array, array.Length, out retrieved);
		}
		if (rESULT == RESULT.OK)
		{
			path = Encoding.UTF8.GetString(array, 0, retrieved - 1);
		}
		return rESULT;
	}

	public RESULT getParameterCount(out int count)
	{
		return FMOD_Studio_EventDescription_GetParameterCount(rawPtr, out count);
	}

	public RESULT getParameterByIndex(int index, out PARAMETER_DESCRIPTION parameter)
	{
		parameter = default(PARAMETER_DESCRIPTION);
		PARAMETER_DESCRIPTION_INTERNAL parameter2;
		RESULT rESULT = FMOD_Studio_EventDescription_GetParameterByIndex(rawPtr, index, out parameter2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		parameter2.assign(out parameter);
		return rESULT;
	}

	public RESULT getParameter(string name, out PARAMETER_DESCRIPTION parameter)
	{
		parameter = default(PARAMETER_DESCRIPTION);
		PARAMETER_DESCRIPTION_INTERNAL parameter2;
		RESULT rESULT = FMOD_Studio_EventDescription_GetParameter(rawPtr, Encoding.UTF8.GetBytes(name + "\0"), out parameter2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		parameter2.assign(out parameter);
		return rESULT;
	}

	public RESULT getUserPropertyCount(out int count)
	{
		return FMOD_Studio_EventDescription_GetUserPropertyCount(rawPtr, out count);
	}

	public RESULT getUserPropertyByIndex(int index, out USER_PROPERTY property)
	{
		USER_PROPERTY_INTERNAL property2;
		RESULT rESULT = FMOD_Studio_EventDescription_GetUserPropertyByIndex(rawPtr, index, out property2);
		if (rESULT != RESULT.OK)
		{
			property = default(USER_PROPERTY);
			return rESULT;
		}
		property = property2.createPublic();
		return RESULT.OK;
	}

	public RESULT getUserProperty(string name, out USER_PROPERTY property)
	{
		USER_PROPERTY_INTERNAL property2;
		RESULT rESULT = FMOD_Studio_EventDescription_GetUserProperty(rawPtr, Encoding.UTF8.GetBytes(name + "\0"), out property2);
		if (rESULT != RESULT.OK)
		{
			property = default(USER_PROPERTY);
			return rESULT;
		}
		property = property2.createPublic();
		return RESULT.OK;
	}

	public RESULT getLength(out int length)
	{
		return FMOD_Studio_EventDescription_GetLength(rawPtr, out length);
	}

	public RESULT getMinimumDistance(out float distance)
	{
		return FMOD_Studio_EventDescription_GetMinimumDistance(rawPtr, out distance);
	}

	public RESULT getMaximumDistance(out float distance)
	{
		return FMOD_Studio_EventDescription_GetMaximumDistance(rawPtr, out distance);
	}

	public RESULT getSoundSize(out float size)
	{
		return FMOD_Studio_EventDescription_GetSoundSize(rawPtr, out size);
	}

	public RESULT isSnapshot(out bool snapshot)
	{
		return FMOD_Studio_EventDescription_IsSnapshot(rawPtr, out snapshot);
	}

	public RESULT isOneshot(out bool oneshot)
	{
		return FMOD_Studio_EventDescription_IsOneshot(rawPtr, out oneshot);
	}

	public RESULT isStream(out bool isStream)
	{
		return FMOD_Studio_EventDescription_IsStream(rawPtr, out isStream);
	}

	public RESULT is3D(out bool is3D)
	{
		return FMOD_Studio_EventDescription_Is3D(rawPtr, out is3D);
	}

	public RESULT hasCue(out bool cue)
	{
		return FMOD_Studio_EventDescription_HasCue(rawPtr, out cue);
	}

	public RESULT createInstance(out EventInstance instance)
	{
		instance = null;
		IntPtr instance2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_EventDescription_CreateInstance(rawPtr, out instance2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		instance = new EventInstance(instance2);
		return rESULT;
	}

	public RESULT getInstanceCount(out int count)
	{
		return FMOD_Studio_EventDescription_GetInstanceCount(rawPtr, out count);
	}

	public RESULT getInstanceList(out EventInstance[] array)
	{
		array = null;
		RESULT rESULT = FMOD_Studio_EventDescription_GetInstanceCount(rawPtr, out var count);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count == 0)
		{
			array = new EventInstance[0];
			return rESULT;
		}
		IntPtr[] array2 = new IntPtr[count];
		rESULT = FMOD_Studio_EventDescription_GetInstanceList(rawPtr, array2, count, out var count2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count2 > count)
		{
			count2 = count;
		}
		array = new EventInstance[count2];
		for (int i = 0; i < count2; i++)
		{
			array[i] = new EventInstance(array2[i]);
		}
		return RESULT.OK;
	}

	public RESULT loadSampleData()
	{
		return FMOD_Studio_EventDescription_LoadSampleData(rawPtr);
	}

	public RESULT unloadSampleData()
	{
		return FMOD_Studio_EventDescription_UnloadSampleData(rawPtr);
	}

	public RESULT getSampleLoadingState(out LOADING_STATE state)
	{
		return FMOD_Studio_EventDescription_GetSampleLoadingState(rawPtr, out state);
	}

	public RESULT releaseAllInstances()
	{
		return FMOD_Studio_EventDescription_ReleaseAllInstances(rawPtr);
	}

	public RESULT setCallback(EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask = EVENT_CALLBACK_TYPE.ALL)
	{
		return FMOD_Studio_EventDescription_SetCallback(rawPtr, callback, callbackmask);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Studio_EventDescription_GetUserData(rawPtr, out userdata);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Studio_EventDescription_SetUserData(rawPtr, userdata);
	}

	[DllImport("fmodstudio")]
	private static extern bool FMOD_Studio_EventDescription_IsValid(IntPtr eventdescription);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetID(IntPtr eventdescription, out Guid id);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetPath(IntPtr eventdescription, [Out] byte[] path, int size, out int retrieved);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetParameterCount(IntPtr eventdescription, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetParameterByIndex(IntPtr eventdescription, int index, out PARAMETER_DESCRIPTION_INTERNAL parameter);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetParameter(IntPtr eventdescription, byte[] name, out PARAMETER_DESCRIPTION_INTERNAL parameter);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyCount(IntPtr eventdescription, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetUserPropertyByIndex(IntPtr eventdescription, int index, out USER_PROPERTY_INTERNAL property);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetUserProperty(IntPtr eventdescription, byte[] name, out USER_PROPERTY_INTERNAL property);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetLength(IntPtr eventdescription, out int length);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetMinimumDistance(IntPtr eventdescription, out float distance);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetMaximumDistance(IntPtr eventdescription, out float distance);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetSoundSize(IntPtr eventdescription, out float size);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_IsSnapshot(IntPtr eventdescription, out bool snapshot);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_IsOneshot(IntPtr eventdescription, out bool oneshot);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_IsStream(IntPtr eventdescription, out bool isStream);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_Is3D(IntPtr eventdescription, out bool is3D);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_HasCue(IntPtr eventdescription, out bool cue);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_CreateInstance(IntPtr eventdescription, out IntPtr instance);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetInstanceCount(IntPtr eventdescription, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetInstanceList(IntPtr eventdescription, IntPtr[] array, int capacity, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_LoadSampleData(IntPtr eventdescription);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_UnloadSampleData(IntPtr eventdescription);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetSampleLoadingState(IntPtr eventdescription, out LOADING_STATE state);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_ReleaseAllInstances(IntPtr eventdescription);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_SetCallback(IntPtr eventdescription, EVENT_CALLBACK callback, EVENT_CALLBACK_TYPE callbackmask);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_GetUserData(IntPtr eventdescription, out IntPtr userdata);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_EventDescription_SetUserData(IntPtr eventdescription, IntPtr userdata);

	public EventDescription(IntPtr raw)
		: base(raw)
	{
	}

	protected override bool isValidInternal()
	{
		return FMOD_Studio_EventDescription_IsValid(rawPtr);
	}
}
