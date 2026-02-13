using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Input;

namespace Monocle;

[Serializable]
public class Binding
{
	public List<Keys> Keyboard = new List<Keys>();

	public List<Buttons> Controller = new List<Buttons>();

	[XmlIgnore]
	public List<Binding> ExclusiveFrom = new List<Binding>();

	public bool HasInput
	{
		get
		{
			if (Keyboard.Count <= 0)
			{
				return Controller.Count > 0;
			}
			return true;
		}
	}

	public bool Add(params Keys[] keys)
	{
		bool result = false;
		foreach (Keys keys2 in keys)
		{
			if (Keyboard.Contains(keys2))
			{
				continue;
			}
			foreach (Binding item in ExclusiveFrom)
			{
				if (!item.Needs(keys2))
				{
					continue;
				}
				goto IL_0061;
			}
			Keyboard.Add(keys2);
			result = true;
			IL_0061:;
		}
		return result;
	}

	public bool Add(params Buttons[] buttons)
	{
		bool result = false;
		foreach (Buttons buttons2 in buttons)
		{
			if (Controller.Contains(buttons2))
			{
				continue;
			}
			foreach (Binding item in ExclusiveFrom)
			{
				if (!item.Needs(buttons2))
				{
					continue;
				}
				goto IL_0061;
			}
			Controller.Add(buttons2);
			result = true;
			IL_0061:;
		}
		return result;
	}

	public bool Needs(Buttons button)
	{
		if (Controller.Contains(button))
		{
			if (Controller.Count <= 1)
			{
				return true;
			}
			if (!IsExclusive(button))
			{
				return false;
			}
			foreach (Buttons item in Controller)
			{
				if (item != button && IsExclusive(item))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool Needs(Keys key)
	{
		if (Keyboard.Contains(key))
		{
			if (Keyboard.Count <= 1)
			{
				return true;
			}
			if (!IsExclusive(key))
			{
				return false;
			}
			foreach (Keys item in Keyboard)
			{
				if (item != key && IsExclusive(item))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool IsExclusive(Buttons button)
	{
		foreach (Binding item in ExclusiveFrom)
		{
			if (item.Controller.Contains(button))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsExclusive(Keys key)
	{
		foreach (Binding item in ExclusiveFrom)
		{
			if (item.Keyboard.Contains(key))
			{
				return false;
			}
		}
		return true;
	}

	public bool ClearKeyboard()
	{
		if (ExclusiveFrom.Count > 0)
		{
			if (Keyboard.Count <= 1)
			{
				return false;
			}
			int index = 0;
			for (int i = 1; i < Keyboard.Count; i++)
			{
				if (IsExclusive(Keyboard[i]))
				{
					index = i;
				}
			}
			Keys item = Keyboard[index];
			Keyboard.Clear();
			Keyboard.Add(item);
		}
		else
		{
			Keyboard.Clear();
		}
		return true;
	}

	public bool ClearGamepad()
	{
		if (ExclusiveFrom.Count > 0)
		{
			if (Controller.Count <= 1)
			{
				return false;
			}
			int index = 0;
			for (int i = 1; i < Controller.Count; i++)
			{
				if (IsExclusive(Controller[i]))
				{
					index = i;
				}
			}
			Buttons item = Controller[index];
			Controller.Clear();
			Controller.Add(item);
		}
		else
		{
			Controller.Clear();
		}
		return true;
	}

	public float Axis(int gamepadIndex, float threshold)
	{
		foreach (Keys item in Keyboard)
		{
			if (MInput.Keyboard.Check(item))
			{
				return 1f;
			}
		}
		foreach (Buttons item2 in Controller)
		{
			float num = MInput.GamePads[gamepadIndex].Axis(item2, threshold);
			if (num != 0f)
			{
				return num;
			}
		}
		return 0f;
	}

	public bool Check(int gamepadIndex, float threshold)
	{
		for (int i = 0; i < Keyboard.Count; i++)
		{
			if (MInput.Keyboard.Check(Keyboard[i]))
			{
				return true;
			}
		}
		for (int j = 0; j < Controller.Count; j++)
		{
			if (MInput.GamePads[gamepadIndex].Check(Controller[j], threshold))
			{
				return true;
			}
		}
		return false;
	}

	public bool Pressed(int gamepadIndex, float threshold)
	{
		for (int i = 0; i < Keyboard.Count; i++)
		{
			if (MInput.Keyboard.Pressed(Keyboard[i]))
			{
				return true;
			}
		}
		for (int j = 0; j < Controller.Count; j++)
		{
			if (MInput.GamePads[gamepadIndex].Pressed(Controller[j], threshold))
			{
				return true;
			}
		}
		return false;
	}

	public bool Released(int gamepadIndex, float threshold)
	{
		for (int i = 0; i < Keyboard.Count; i++)
		{
			if (MInput.Keyboard.Released(Keyboard[i]))
			{
				return true;
			}
		}
		for (int j = 0; j < Controller.Count; j++)
		{
			if (MInput.GamePads[gamepadIndex].Released(Controller[j], threshold))
			{
				return true;
			}
		}
		return false;
	}

	public static void SetExclusive(params Binding[] list)
	{
		Binding[] array = list;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].ExclusiveFrom.Clear();
		}
		array = list;
		foreach (Binding binding in array)
		{
			foreach (Binding binding2 in list)
			{
				if (binding != binding2)
				{
					binding.ExclusiveFrom.Add(binding2);
					binding2.ExclusiveFrom.Add(binding);
				}
			}
		}
	}
}
