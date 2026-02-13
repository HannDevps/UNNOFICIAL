using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class PreviewPortrait : Scene
{
	private Sprite animation;

	private List<string> options = new List<string>();

	private List<string> animations = new List<string>();

	private Vector2 topleft = new Vector2(64f, 64f);

	private string currentPortrait;

	public Vector2 Mouse => Vector2.Transform(new Vector2(MInput.Mouse.CurrentState.X, MInput.Mouse.CurrentState.Y), Matrix.Invert(Engine.ScreenMatrix));

	public PreviewPortrait(float scroll = 64f)
	{
		foreach (KeyValuePair<string, SpriteData> spriteDatum in GFX.PortraitsSpriteBank.SpriteData)
		{
			if (spriteDatum.Key.StartsWith("portrait"))
			{
				options.Add(spriteDatum.Key);
			}
		}
		topleft.Y = scroll;
	}

	public override void Update()
	{
		if (animation != null)
		{
			animation.Update();
			if (MInput.Mouse.PressedLeftButton)
			{
				for (int i = 0; i < animations.Count; i++)
				{
					if (MouseOverOption(i))
					{
						if (i == 0)
						{
							animation = null;
						}
						else
						{
							animation.Play(animations[i]);
						}
						break;
					}
				}
			}
		}
		else if (MInput.Mouse.PressedLeftButton)
		{
			for (int j = 0; j < options.Count; j++)
			{
				if (!MouseOverOption(j))
				{
					continue;
				}
				currentPortrait = options[j].Split('_')[1];
				animation = GFX.PortraitsSpriteBank.Create(options[j]);
				animations.Clear();
				animations.Add("<-BACK");
				XmlElement xML = GFX.PortraitsSpriteBank.SpriteData[options[j]].Sources[0].XML;
				foreach (XmlElement item in xML.GetElementsByTagName("Anim"))
				{
					animations.Add(item.Attr("id"));
				}
				foreach (XmlElement item2 in xML.GetElementsByTagName("Loop"))
				{
					animations.Add(item2.Attr("id"));
				}
				break;
			}
		}
		topleft.Y += (float)MInput.Mouse.WheelDelta * Engine.DeltaTime * ActiveFont.LineHeight;
		if (MInput.Keyboard.Pressed(Keys.F1))
		{
			Celeste.ReloadPortraits();
			Engine.Scene = new PreviewPortrait(topleft.Y);
		}
	}

	public override void Render()
	{
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, RasterizerState.CullNone, null, Engine.ScreenMatrix);
		Draw.Rect(0f, 0f, 960f, 1080f, Color.DarkSlateGray * 0.25f);
		if (animation != null)
		{
			animation.Scale = Vector2.One;
			animation.Position = new Vector2(1440f, 540f);
			animation.Render();
			int num = 0;
			foreach (string animation in animations)
			{
				Color color = Color.Gray;
				if (MouseOverOption(num))
				{
					color = Color.White;
				}
				else if (this.animation.CurrentAnimationID == animation)
				{
					color = Color.Yellow;
				}
				ActiveFont.Draw(animation, topleft + new Vector2(0f, (float)num * ActiveFont.LineHeight), color);
				num++;
			}
			if (!string.IsNullOrEmpty(this.animation.CurrentAnimationID))
			{
				string[] array = this.animation.CurrentAnimationID.Split('_');
				if (array.Length > 1)
				{
					ActiveFont.Draw(currentPortrait + " " + array[1], new Vector2(1440f, 1016f), new Vector2(0.5f, 1f), Vector2.One, Color.White);
				}
			}
		}
		else
		{
			int num2 = 0;
			foreach (string option in options)
			{
				ActiveFont.Draw(option, topleft + new Vector2(0f, (float)num2 * ActiveFont.LineHeight), MouseOverOption(num2) ? Color.White : Color.Gray);
				num2++;
			}
		}
		Draw.Rect(Mouse.X - 12f, Mouse.Y - 4f, 24f, 8f, Color.Red);
		Draw.Rect(Mouse.X - 4f, Mouse.Y - 12f, 8f, 24f, Color.Red);
		Draw.SpriteBatch.End();
	}

	private bool MouseOverOption(int i)
	{
		if (Mouse.X > topleft.X && Mouse.Y > topleft.Y + (float)i * ActiveFont.LineHeight && MInput.Mouse.X < 960f)
		{
			return Mouse.Y < topleft.Y + (float)(i + 1) * ActiveFont.LineHeight;
		}
		return false;
	}
}
