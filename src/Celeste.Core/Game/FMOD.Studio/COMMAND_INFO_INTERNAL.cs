using System;

namespace FMOD.Studio;

internal struct COMMAND_INFO_INTERNAL
{
	public IntPtr commandname;

	public int parentcommandindex;

	public int framenumber;

	public float frametime;

	public INSTANCETYPE instancetype;

	public INSTANCETYPE outputtype;

	public uint instancehandle;

	public uint outputhandle;

	public COMMAND_INFO createPublic()
	{
		return new COMMAND_INFO
		{
			commandname = MarshallingHelper.stringFromNativeUtf8(commandname),
			parentcommandindex = parentcommandindex,
			framenumber = framenumber,
			frametime = frametime,
			instancetype = instancetype,
			outputtype = outputtype,
			instancehandle = instancehandle,
			outputhandle = outputhandle
		};
	}
}
