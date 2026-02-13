using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class QuickResetHint : Entity
{
	private string textStart;

	private string textHold;

	private string textPress;

	private List<object> controllerList;

	private List<object> keyboardList;

	public QuickResetHint()
	{
		base.Tag = Tags.HUD;
		Buttons buttons = Buttons.LeftShoulder;
		Buttons buttons2 = Buttons.RightShoulder;
		textStart = Dialog.Clean("UI_QUICK_RESTART_TITLE") + " ";
		textHold = Dialog.Clean("UI_QUICK_RESTART_HOLD");
		textPress = Dialog.Clean("UI_QUICK_RESTART_PRESS");
		if (Settings.Instance.Language == "japanese")
		{
			controllerList = new List<object>
			{
				textStart,
				buttons,
				buttons2,
				textHold,
				"、",
				Input.FirstButton(Input.Pause),
				textPress
			};
			keyboardList = new List<object>
			{
				textStart,
				Input.FirstKey(Input.QuickRestart),
				textPress
			};
		}
		else
		{
			controllerList = new List<object>
			{
				textStart,
				textHold,
				buttons,
				buttons2,
				",  ",
				textPress,
				Input.FirstButton(Input.Pause)
			};
			keyboardList = new List<object>
			{
				textStart,
				textPress,
				Input.FirstKey(Input.QuickRestart)
			};
		}
	}

	public override void Render()
	{
		List<object> list = (Input.GuiInputController() ? controllerList : keyboardList);
		float num = 0f;
		foreach (object item in list)
		{
			if (item is string)
			{
				num += ActiveFont.Measure(item as string).X;
			}
			else if (item is Buttons)
			{
				num += (float)Input.GuiSingleButton((Buttons)item).Width + 16f;
			}
			else if (item is Keys)
			{
				num += (float)Input.GuiKey((Keys)item).Width + 16f;
			}
		}
		num *= 0.75f;
		Vector2 vector = new Vector2((1920f - num) / 2f, 980f);
		foreach (object item2 in list)
		{
			if (item2 is string)
			{
				ActiveFont.DrawOutline(item2 as string, vector, new Vector2(0f, 0.5f), Vector2.One * 0.75f, Color.LightGray, 2f, Color.Black);
				vector.X += ActiveFont.Measure(item2 as string).X * 0.75f;
			}
			else if (item2 is Buttons)
			{
				MTexture mTexture = Input.GuiSingleButton((Buttons)item2);
				mTexture.DrawJustified(vector + new Vector2(((float)mTexture.Width + 16f) * 0.75f * 0.5f, 0f), new Vector2(0.5f, 0.5f), Color.White, 0.75f);
				vector.X += ((float)mTexture.Width + 16f) * 0.75f;
			}
			else if (item2 is Keys)
			{
				MTexture mTexture2 = Input.GuiKey((Keys)item2);
				mTexture2.DrawJustified(vector + new Vector2(((float)mTexture2.Width + 16f) * 0.75f * 0.5f, 0f), new Vector2(0.5f, 0.5f), Color.White, 0.75f);
				vector.X += ((float)mTexture2.Width + 16f) * 0.75f;
			}
		}
	}
}
