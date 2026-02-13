using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class InputMappingInfo : TextMenu.Item
{
	private List<object> info = new List<object>();

	private bool controllerMode;

	private float borderEase;

	private bool fixedPosition;

	public InputMappingInfo(bool controllerMode)
	{
		string[] array = Dialog.Clean("BTN_CONFIG_INFO").Split('|');
		if (array.Length == 3)
		{
			info.Add(array[0]);
			info.Add(Input.MenuConfirm);
			info.Add(array[1]);
			info.Add(Input.MenuJournal);
			info.Add(array[2]);
		}
		this.controllerMode = controllerMode;
		AboveAll = true;
	}

	public override float LeftWidth()
	{
		return 100f;
	}

	public override float Height()
	{
		return ActiveFont.LineHeight * 2f;
	}

	public override void Update()
	{
		borderEase = Calc.Approach(borderEase, fixedPosition ? 1f : 0f, Engine.DeltaTime * 4f);
		base.Update();
	}

	public override void Render(Vector2 position, bool highlighted)
	{
		fixedPosition = false;
		if (position.Y < 100f)
		{
			fixedPosition = true;
			position.Y = 100f;
		}
		Color color = Color.Gray * Ease.CubeOut(Container.Alpha);
		Color strokeColor = Color.Black * Ease.CubeOut(Container.Alpha);
		Color color2 = Color.White * Ease.CubeOut(Container.Alpha);
		float num = 0f;
		for (int i = 0; i < info.Count; i++)
		{
			if (info[i] is string)
			{
				string text = info[i] as string;
				num += ActiveFont.Measure(text).X * 0.6f;
			}
			else if (info[i] is VirtualButton)
			{
				VirtualButton virtualButton = info[i] as VirtualButton;
				if (controllerMode)
				{
					MTexture mTexture = Input.GuiButton(virtualButton, Input.PrefixMode.Attached);
					num += (float)mTexture.Width * 0.6f;
				}
				else if (virtualButton.Binding.Keyboard.Count > 0)
				{
					MTexture mTexture2 = Input.GuiKey(virtualButton.Binding.Keyboard[0]);
					num += (float)mTexture2.Width * 0.6f;
				}
				else
				{
					MTexture mTexture3 = Input.GuiKey(Keys.None);
					num += (float)mTexture3.Width * 0.6f;
				}
			}
		}
		Vector2 position2 = position + new Vector2(Container.Width - num, 0f) / 2f;
		if (borderEase > 0f)
		{
			Draw.HollowRect(position2.X - 22f, position2.Y - 42f, num + 44f, 84f, Color.White * Ease.CubeOut(Container.Alpha) * borderEase);
			Draw.HollowRect(position2.X - 21f, position2.Y - 41f, num + 42f, 82f, Color.White * Ease.CubeOut(Container.Alpha) * borderEase);
			Draw.Rect(position2.X - 20f, position2.Y - 40f, num + 40f, 80f, Color.Black * Ease.CubeOut(Container.Alpha));
		}
		for (int j = 0; j < info.Count; j++)
		{
			if (info[j] is string)
			{
				string text2 = info[j] as string;
				ActiveFont.DrawOutline(text2, position2, new Vector2(0f, 0.5f), Vector2.One * 0.6f, color, 2f, strokeColor);
				position2.X += ActiveFont.Measure(text2).X * 0.6f;
			}
			else if (info[j] is VirtualButton)
			{
				VirtualButton virtualButton2 = info[j] as VirtualButton;
				if (controllerMode)
				{
					MTexture mTexture4 = Input.GuiButton(virtualButton2, Input.PrefixMode.Attached);
					mTexture4.DrawJustified(position2, new Vector2(0f, 0.5f), color2, 0.6f);
					position2.X += (float)mTexture4.Width * 0.6f;
				}
				else if (virtualButton2.Binding.Keyboard.Count > 0)
				{
					MTexture mTexture5 = Input.GuiKey(virtualButton2.Binding.Keyboard[0]);
					mTexture5.DrawJustified(position2, new Vector2(0f, 0.5f), color2, 0.6f);
					position2.X += (float)mTexture5.Width * 0.6f;
				}
				else
				{
					MTexture mTexture6 = Input.GuiKey(Keys.None);
					mTexture6.DrawJustified(position2, new Vector2(0f, 0.5f), color2, 0.6f);
					position2.X += (float)mTexture6.Width * 0.6f;
				}
			}
		}
	}
}
