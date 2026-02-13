using System;

namespace FMOD.Studio;

internal struct USER_PROPERTY_INTERNAL
{
	private IntPtr name;

	private USER_PROPERTY_TYPE type;

	private Union_IntBoolFloatString value;

	public USER_PROPERTY createPublic()
	{
		USER_PROPERTY result = new USER_PROPERTY
		{
			name = MarshallingHelper.stringFromNativeUtf8(name),
			type = type
		};
		switch (type)
		{
		case USER_PROPERTY_TYPE.INTEGER:
			result.intvalue = value.intvalue;
			break;
		case USER_PROPERTY_TYPE.BOOLEAN:
			result.boolvalue = value.boolvalue;
			break;
		case USER_PROPERTY_TYPE.FLOAT:
			result.floatvalue = value.floatvalue;
			break;
		case USER_PROPERTY_TYPE.STRING:
			result.stringvalue = MarshallingHelper.stringFromNativeUtf8(value.stringvalue);
			break;
		}
		return result;
	}
}
