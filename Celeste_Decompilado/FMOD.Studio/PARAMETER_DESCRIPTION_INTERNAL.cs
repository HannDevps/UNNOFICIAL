using System;

namespace FMOD.Studio;

internal struct PARAMETER_DESCRIPTION_INTERNAL
{
	public IntPtr name;

	public int index;

	public float minimum;

	public float maximum;

	public float defaultvalue;

	public PARAMETER_TYPE type;

	public void assign(out PARAMETER_DESCRIPTION publicDesc)
	{
		publicDesc.name = MarshallingHelper.stringFromNativeUtf8(name);
		publicDesc.index = index;
		publicDesc.minimum = minimum;
		publicDesc.maximum = maximum;
		publicDesc.defaultvalue = defaultvalue;
		publicDesc.type = type;
	}
}
