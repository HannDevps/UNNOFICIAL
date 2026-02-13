using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FMOD.Studio;

public class CommandReplay : HandleBase
{
	public RESULT getSystem(out System system)
	{
		system = null;
		IntPtr system2 = default(IntPtr);
		RESULT num = FMOD_Studio_CommandReplay_GetSystem(rawPtr, out system2);
		if (num == RESULT.OK)
		{
			system = new System(system2);
		}
		return num;
	}

	public RESULT getLength(out float totalTime)
	{
		return FMOD_Studio_CommandReplay_GetLength(rawPtr, out totalTime);
	}

	public RESULT getCommandCount(out int count)
	{
		return FMOD_Studio_CommandReplay_GetCommandCount(rawPtr, out count);
	}

	public RESULT getCommandInfo(int commandIndex, out COMMAND_INFO info)
	{
		COMMAND_INFO_INTERNAL info2 = default(COMMAND_INFO_INTERNAL);
		RESULT rESULT = FMOD_Studio_CommandReplay_GetCommandInfo(rawPtr, commandIndex, out info2);
		if (rESULT != RESULT.OK)
		{
			info = default(COMMAND_INFO);
			return rESULT;
		}
		info = info2.createPublic();
		return rESULT;
	}

	public RESULT getCommandString(int commandIndex, out string description)
	{
		description = null;
		byte[] array = new byte[8];
		RESULT rESULT;
		while (true)
		{
			rESULT = FMOD_Studio_CommandReplay_GetCommandString(rawPtr, commandIndex, array, array.Length);
			switch (rESULT)
			{
			case RESULT.ERR_TRUNCATED:
				goto IL_0020;
			case RESULT.OK:
			{
				int i;
				for (i = 0; array[i] != 0; i++)
				{
				}
				description = Encoding.UTF8.GetString(array, 0, i);
				break;
			}
			}
			break;
			IL_0020:
			array = new byte[2 * array.Length];
		}
		return rESULT;
	}

	public RESULT getCommandAtTime(float time, out int commandIndex)
	{
		return FMOD_Studio_CommandReplay_GetCommandAtTime(rawPtr, time, out commandIndex);
	}

	public RESULT setBankPath(string bankPath)
	{
		return FMOD_Studio_CommandReplay_SetBankPath(rawPtr, Encoding.UTF8.GetBytes(bankPath + "\0"));
	}

	public RESULT start()
	{
		return FMOD_Studio_CommandReplay_Start(rawPtr);
	}

	public RESULT stop()
	{
		return FMOD_Studio_CommandReplay_Stop(rawPtr);
	}

	public RESULT seekToTime(float time)
	{
		return FMOD_Studio_CommandReplay_SeekToTime(rawPtr, time);
	}

	public RESULT seekToCommand(int commandIndex)
	{
		return FMOD_Studio_CommandReplay_SeekToCommand(rawPtr, commandIndex);
	}

	public RESULT getPaused(out bool paused)
	{
		return FMOD_Studio_CommandReplay_GetPaused(rawPtr, out paused);
	}

	public RESULT setPaused(bool paused)
	{
		return FMOD_Studio_CommandReplay_SetPaused(rawPtr, paused);
	}

	public RESULT getPlaybackState(out PLAYBACK_STATE state)
	{
		return FMOD_Studio_CommandReplay_GetPlaybackState(rawPtr, out state);
	}

	public RESULT getCurrentCommand(out int commandIndex, out float currentTime)
	{
		return FMOD_Studio_CommandReplay_GetCurrentCommand(rawPtr, out commandIndex, out currentTime);
	}

	public RESULT release()
	{
		return FMOD_Studio_CommandReplay_Release(rawPtr);
	}

	public RESULT setFrameCallback(COMMANDREPLAY_FRAME_CALLBACK callback)
	{
		return FMOD_Studio_CommandReplay_SetFrameCallback(rawPtr, callback);
	}

	public RESULT setLoadBankCallback(COMMANDREPLAY_LOAD_BANK_CALLBACK callback)
	{
		return FMOD_Studio_CommandReplay_SetLoadBankCallback(rawPtr, callback);
	}

	public RESULT setCreateInstanceCallback(COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback)
	{
		return FMOD_Studio_CommandReplay_SetCreateInstanceCallback(rawPtr, callback);
	}

	public RESULT getUserData(out IntPtr userdata)
	{
		return FMOD_Studio_CommandReplay_GetUserData(rawPtr, out userdata);
	}

	public RESULT setUserData(IntPtr userdata)
	{
		return FMOD_Studio_CommandReplay_SetUserData(rawPtr, userdata);
	}

	[DllImport("fmodstudio")]
	private static extern bool FMOD_Studio_CommandReplay_IsValid(IntPtr replay);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetSystem(IntPtr replay, out IntPtr system);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetLength(IntPtr replay, out float totalTime);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetCommandCount(IntPtr replay, out int count);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetCommandInfo(IntPtr replay, int commandIndex, out COMMAND_INFO_INTERNAL info);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetCommandString(IntPtr replay, int commandIndex, [Out] byte[] description, int capacity);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetCommandAtTime(IntPtr replay, float time, out int commandIndex);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SetBankPath(IntPtr replay, byte[] bankPath);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_Start(IntPtr replay);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_Stop(IntPtr replay);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SeekToTime(IntPtr replay, float time);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SeekToCommand(IntPtr replay, int commandIndex);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetPaused(IntPtr replay, out bool paused);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SetPaused(IntPtr replay, bool paused);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetPlaybackState(IntPtr replay, out PLAYBACK_STATE state);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetCurrentCommand(IntPtr replay, out int commandIndex, out float currentTime);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_Release(IntPtr replay);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SetFrameCallback(IntPtr replay, COMMANDREPLAY_FRAME_CALLBACK callback);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SetLoadBankCallback(IntPtr replay, COMMANDREPLAY_LOAD_BANK_CALLBACK callback);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SetCreateInstanceCallback(IntPtr replay, COMMANDREPLAY_CREATE_INSTANCE_CALLBACK callback);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_GetUserData(IntPtr replay, out IntPtr userdata);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_CommandReplay_SetUserData(IntPtr replay, IntPtr userdata);

	public CommandReplay(IntPtr raw)
		: base(raw)
	{
	}

	protected override bool isValidInternal()
	{
		return FMOD_Studio_CommandReplay_IsValid(rawPtr);
	}
}
