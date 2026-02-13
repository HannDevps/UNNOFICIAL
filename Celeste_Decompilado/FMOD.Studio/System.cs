using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Studio;

public class System : HandleBase
{
	public static RESULT create(out System studiosystem)
	{
		RESULT rESULT = RESULT.OK;
		studiosystem = null;
		rESULT = FMOD_Studio_System_Create(out var studiosystem2, 69652u);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		studiosystem = new System(studiosystem2);
		return rESULT;
	}

	public RESULT setAdvancedSettings(ADVANCEDSETTINGS settings)
	{
		settings.cbsize = Marshal.SizeOf(typeof(ADVANCEDSETTINGS));
		return FMOD_Studio_System_SetAdvancedSettings(rawPtr, ref settings);
	}

	public RESULT getAdvancedSettings(out ADVANCEDSETTINGS settings)
	{
		settings.cbsize = Marshal.SizeOf(typeof(ADVANCEDSETTINGS));
		return FMOD_Studio_System_GetAdvancedSettings(rawPtr, out settings);
	}

	public RESULT initialize(int maxchannels, INITFLAGS studioFlags, FMOD.INITFLAGS flags, IntPtr extradriverdata)
	{
		return FMOD_Studio_System_Initialize(rawPtr, maxchannels, studioFlags, flags, extradriverdata);
	}

	public RESULT release()
	{
		return FMOD_Studio_System_Release(rawPtr);
	}

	public RESULT update()
	{
		return FMOD_Studio_System_Update(rawPtr);
	}

	public RESULT getLowLevelSystem(out FMOD.System system)
	{
		system = null;
		IntPtr system2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetLowLevelSystem(rawPtr, out system2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		system = new FMOD.System(system2);
		return rESULT;
	}

	public RESULT getEvent(string path, out EventDescription _event)
	{
		_event = null;
		IntPtr description = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetEvent(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), out description);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		_event = new EventDescription(description);
		return rESULT;
	}

	public RESULT getBus(string path, out Bus bus)
	{
		bus = null;
		IntPtr bus2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetBus(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), out bus2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bus = new Bus(bus2);
		return rESULT;
	}

	public RESULT getVCA(string path, out VCA vca)
	{
		vca = null;
		IntPtr vca2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetVCA(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), out vca2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		vca = new VCA(vca2);
		return rESULT;
	}

	public RESULT getBank(string path, out Bank bank)
	{
		bank = null;
		IntPtr bank2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetBank(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), out bank2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bank = new Bank(bank2);
		return rESULT;
	}

	public RESULT getEventByID(Guid guid, out EventDescription _event)
	{
		_event = null;
		IntPtr description = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetEventByID(rawPtr, ref guid, out description);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		_event = new EventDescription(description);
		return rESULT;
	}

	public RESULT getBusByID(Guid guid, out Bus bus)
	{
		bus = null;
		IntPtr bus2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetBusByID(rawPtr, ref guid, out bus2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bus = new Bus(bus2);
		return rESULT;
	}

	public RESULT getVCAByID(Guid guid, out VCA vca)
	{
		vca = null;
		IntPtr vca2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetVCAByID(rawPtr, ref guid, out vca2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		vca = new VCA(vca2);
		return rESULT;
	}

	public RESULT getBankByID(Guid guid, out Bank bank)
	{
		bank = null;
		IntPtr bank2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_GetBankByID(rawPtr, ref guid, out bank2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bank = new Bank(bank2);
		return rESULT;
	}

	public RESULT getSoundInfo(string key, out SOUND_INFO info)
	{
		SOUND_INFO_INTERNAL info2;
		RESULT rESULT = FMOD_Studio_System_GetSoundInfo(rawPtr, Encoding.UTF8.GetBytes(key + "\0"), out info2);
		if (rESULT != RESULT.OK)
		{
			info = new SOUND_INFO();
			return rESULT;
		}
		info2.assign(out info);
		return rESULT;
	}

	public RESULT lookupID(string path, out Guid guid)
	{
		return FMOD_Studio_System_LookupID(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), out guid);
	}

	public RESULT lookupPath(Guid guid, out string path)
	{
		path = null;
		byte[] array = new byte[256];
		int retrieved = 0;
		RESULT rESULT = FMOD_Studio_System_LookupPath(rawPtr, ref guid, array, array.Length, out retrieved);
		if (rESULT == RESULT.ERR_TRUNCATED)
		{
			array = new byte[retrieved];
			rESULT = FMOD_Studio_System_LookupPath(rawPtr, ref guid, array, array.Length, out retrieved);
		}
		if (rESULT == RESULT.OK)
		{
			path = Encoding.UTF8.GetString(array, 0, retrieved - 1);
		}
		return rESULT;
	}

	public RESULT getNumListeners(out int numlisteners)
	{
		return FMOD_Studio_System_GetNumListeners(rawPtr, out numlisteners);
	}

	public RESULT setNumListeners(int numlisteners)
	{
		return FMOD_Studio_System_SetNumListeners(rawPtr, numlisteners);
	}

	public RESULT getListenerAttributes(int listener, out _3D_ATTRIBUTES attributes)
	{
		return FMOD_Studio_System_GetListenerAttributes(rawPtr, listener, out attributes);
	}

	public RESULT setListenerAttributes(int listener, _3D_ATTRIBUTES attributes)
	{
		return FMOD_Studio_System_SetListenerAttributes(rawPtr, listener, ref attributes);
	}

	public RESULT getListenerWeight(int listener, out float weight)
	{
		return FMOD_Studio_System_GetListenerWeight(rawPtr, listener, out weight);
	}

	public RESULT setListenerWeight(int listener, float weight)
	{
		return FMOD_Studio_System_SetListenerWeight(rawPtr, listener, weight);
	}

	public RESULT loadBankFile(string name, LOAD_BANK_FLAGS flags, out Bank bank)
	{
		bank = null;
		IntPtr bank2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_LoadBankFile(rawPtr, Encoding.UTF8.GetBytes(name + "\0"), flags, out bank2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bank = new Bank(bank2);
		return rESULT;
	}

	public RESULT loadBankMemory(byte[] buffer, LOAD_BANK_FLAGS flags, out Bank bank)
	{
		bank = null;
		IntPtr bank2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_LoadBankMemory(rawPtr, buffer, buffer.Length, LOAD_MEMORY_MODE.LOAD_MEMORY, flags, out bank2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bank = new Bank(bank2);
		return rESULT;
	}

	public RESULT loadBankCustom(BANK_INFO info, LOAD_BANK_FLAGS flags, out Bank bank)
	{
		bank = null;
		info.size = Marshal.SizeOf(info);
		IntPtr bank2 = default(IntPtr);
		RESULT rESULT = FMOD_Studio_System_LoadBankCustom(rawPtr, ref info, flags, out bank2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		bank = new Bank(bank2);
		return rESULT;
	}

	public RESULT unloadAll()
	{
		return FMOD_Studio_System_UnloadAll(rawPtr);
	}

	public RESULT flushCommands()
	{
		return FMOD_Studio_System_FlushCommands(rawPtr);
	}

	public RESULT flushSampleLoading()
	{
		return FMOD_Studio_System_FlushSampleLoading(rawPtr);
	}

	public RESULT startCommandCapture(string path, COMMANDCAPTURE_FLAGS flags)
	{
		return FMOD_Studio_System_StartCommandCapture(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), flags);
	}

	public RESULT stopCommandCapture()
	{
		return FMOD_Studio_System_StopCommandCapture(rawPtr);
	}

	public RESULT loadCommandReplay(string path, COMMANDREPLAY_FLAGS flags, out CommandReplay replay)
	{
		replay = null;
		IntPtr commandReplay = default(IntPtr);
		RESULT num = FMOD_Studio_System_LoadCommandReplay(rawPtr, Encoding.UTF8.GetBytes(path + "\0"), flags, out commandReplay);
		if (num == RESULT.OK)
		{
			replay = new CommandReplay(commandReplay);
		}
		return num;
	}

	public RESULT getBankCount(out int count)
	{
		return FMOD_Studio_System_GetBankCount(rawPtr, out count);
	}

	public RESULT getBankList(out Bank[] array)
	{
		array = null;
		RESULT rESULT = FMOD_Studio_System_GetBankCount(rawPtr, out var count);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count == 0)
		{
			array = new Bank[0];
			return rESULT;
		}
		IntPtr[] array2 = new IntPtr[count];
		rESULT = FMOD_Studio_System_GetBankList(rawPtr, array2, count, out var count2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		if (count2 > count)
		{
			count2 = count;
		}
		array = new Bank[count2];
		for (int i = 0; i < count2; i++)
		{
			array[i] = new Bank(array2[i]);
		}
		return RESULT.OK;
	}

	public RESULT getCPUUsage(out CPU_USAGE usage)
	{
		return FMOD_Studio_System_GetCPUUsage(rawPtr, out usage);
	}

	public RESULT getBufferUsage(out BUFFER_USAGE usage)
	{
		return FMOD_Studio_System_GetBufferUsage(rawPtr, out usage);
	}

	public RESULT resetBufferUsage()
	{
		return FMOD_Studio_System_ResetBufferUsage(rawPtr);
	}

	public RESULT setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask = SYSTEM_CALLBACK_TYPE.ALL)
	{
		return FMOD_Studio_System_SetCallback(rawPtr, callback, callbackmask);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Studio_System_GetUserData(rawPtr, out userdata);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Studio_System_SetUserData(rawPtr, userdata);
	}

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_Create(out IntPtr studiosystem, uint headerversion);

	[DllImport("fmodstudio")]
	private static extern bool FMOD_Studio_System_IsValid(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_SetAdvancedSettings(IntPtr studiosystem, ref ADVANCEDSETTINGS settings);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetAdvancedSettings(IntPtr studiosystem, out ADVANCEDSETTINGS settings);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_Initialize(IntPtr studiosystem, int maxchannels, INITFLAGS studioFlags, FMOD.INITFLAGS flags, IntPtr extradriverdata);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_Release(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_Update(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetLowLevelSystem(IntPtr studiosystem, out IntPtr system);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetEvent(IntPtr studiosystem, byte[] path, out IntPtr description);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBus(IntPtr studiosystem, byte[] path, out IntPtr bus);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetVCA(IntPtr studiosystem, byte[] path, out IntPtr vca);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBank(IntPtr studiosystem, byte[] path, out IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetEventByID(IntPtr studiosystem, ref Guid guid, out IntPtr description);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBusByID(IntPtr studiosystem, ref Guid guid, out IntPtr bus);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetVCAByID(IntPtr studiosystem, ref Guid guid, out IntPtr vca);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBankByID(IntPtr studiosystem, ref Guid guid, out IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetSoundInfo(IntPtr studiosystem, byte[] key, out SOUND_INFO_INTERNAL info);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_LookupID(IntPtr studiosystem, byte[] path, out Guid guid);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_LookupPath(IntPtr studiosystem, ref Guid guid, [Out] byte[] path, int size, out int retrieved);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetNumListeners(IntPtr studiosystem, out int numlisteners);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_SetNumListeners(IntPtr studiosystem, int numlisteners);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetListenerAttributes(IntPtr studiosystem, int listener, out _3D_ATTRIBUTES attributes);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_SetListenerAttributes(IntPtr studiosystem, int listener, ref _3D_ATTRIBUTES attributes);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetListenerWeight(IntPtr studiosystem, int listener, out float weight);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_SetListenerWeight(IntPtr studiosystem, int listener, float weight);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_LoadBankFile(IntPtr studiosystem, byte[] filename, LOAD_BANK_FLAGS flags, out IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_LoadBankMemory(IntPtr studiosystem, byte[] buffer, int length, LOAD_MEMORY_MODE mode, LOAD_BANK_FLAGS flags, out IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_LoadBankCustom(IntPtr studiosystem, ref BANK_INFO info, LOAD_BANK_FLAGS flags, out IntPtr bank);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_UnloadAll(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_FlushCommands(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_FlushSampleLoading(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_StartCommandCapture(IntPtr studiosystem, byte[] path, COMMANDCAPTURE_FLAGS flags);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_StopCommandCapture(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_LoadCommandReplay(IntPtr studiosystem, byte[] path, COMMANDREPLAY_FLAGS flags, out IntPtr commandReplay);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBankCount(IntPtr studiosystem, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBankList(IntPtr studiosystem, IntPtr[] array, int capacity, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetCPUUsage(IntPtr studiosystem, out CPU_USAGE usage);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetBufferUsage(IntPtr studiosystem, out BUFFER_USAGE usage);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_ResetBufferUsage(IntPtr studiosystem);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_SetCallback(IntPtr studiosystem, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_GetUserData(IntPtr studiosystem, out IntPtr userdata);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_System_SetUserData(IntPtr studiosystem, IntPtr userdata);

	public System(IntPtr raw)
		: base(raw)
	{
	}

	protected override bool isValidInternal()
	{
		return FMOD_Studio_System_IsValid(rawPtr);
	}
}
