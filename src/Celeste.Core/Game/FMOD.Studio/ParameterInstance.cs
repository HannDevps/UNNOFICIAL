using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio;

public class ParameterInstance : HandleBase
{
	public RESULT getDescription(out PARAMETER_DESCRIPTION description)
	{
		description = default(PARAMETER_DESCRIPTION);
		PARAMETER_DESCRIPTION_INTERNAL description2;
		RESULT rESULT = FMOD_Studio_ParameterInstance_GetDescription(rawPtr, out description2);
		if (rESULT != RESULT.OK)
		{
			return rESULT;
		}
		description2.assign(out description);
		return rESULT;
	}

	public RESULT getValue(out float value)
	{
		return FMOD_Studio_ParameterInstance_GetValue(rawPtr, out value);
	}

	public RESULT setValue(float value)
	{
		return FMOD_Studio_ParameterInstance_SetValue(rawPtr, value);
	}

	[DllImport("fmodstudio")]
	private static extern bool FMOD_Studio_ParameterInstance_IsValid(IntPtr parameter);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_ParameterInstance_GetDescription(IntPtr parameter, out PARAMETER_DESCRIPTION_INTERNAL description);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_ParameterInstance_GetValue(IntPtr parameter, out float value);

	[DllImport("fmodstudio")]
	private static extern RESULT FMOD_Studio_ParameterInstance_SetValue(IntPtr parameter, float value);

	public ParameterInstance(IntPtr raw)
		: base(raw)
	{
	}

	protected override bool isValidInternal()
	{
		return FMOD_Studio_ParameterInstance_IsValid(rawPtr);
	}
}
