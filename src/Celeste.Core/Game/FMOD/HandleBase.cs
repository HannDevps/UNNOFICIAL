using System;

namespace FMOD;

public class HandleBase
{
	protected IntPtr rawPtr;

	public HandleBase(IntPtr newPtr)
	{
		rawPtr = newPtr;
	}

	public bool isValid()
	{
		return rawPtr != IntPtr.Zero;
	}

	public IntPtr getRaw()
	{
		return rawPtr;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as HandleBase);
	}

	public bool Equals(HandleBase p)
	{
		if ((object)p != null)
		{
			return rawPtr == p.rawPtr;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return rawPtr.ToInt32();
	}

	public static bool operator ==(HandleBase a, HandleBase b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.rawPtr == b.rawPtr;
	}

	public static bool operator !=(HandleBase a, HandleBase b)
	{
		return !(a == b);
	}
}
