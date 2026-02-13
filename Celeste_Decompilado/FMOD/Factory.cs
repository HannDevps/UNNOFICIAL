using System;
using System.Runtime.InteropServices;

namespace FMOD;

public class Factory
{
	public static RESULT System_Create(out System system)
	{
		system = null;
		RESULT rESULT = RESULT.OK;
		IntPtr system2 = default(IntPtr);
		rESULT = FMOD_System_Create(out system2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		system = new System(system2);
		return rESULT;
	}

	[DllImport("fmod")]
	private static extern RESULT FMOD_System_Create(out IntPtr system);
}
