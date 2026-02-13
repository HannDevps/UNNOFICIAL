using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class SpeedrunTimerDisplay : Entity
{
	public float CompleteTimer;

	public const int GuiChapterHeight = 58;

	public const int GuiFileHeight = 78;

	private static float numberWidth;

	private static float spacerWidth;

	private MTexture bg = GFX.Gui["strawberryCountBG"];

	public float DrawLerp;

	private Wiggler wiggler;

	public SpeedrunTimerDisplay()
	{
		base.Tag = (int)Tags.HUD | (int)Tags.Global | (int)Tags.PauseUpdate | (int)Tags.TransitionUpdate;
		base.Depth = -100;
		base.Y = 60f;
		CalculateBaseSizes();
		Add(wiggler = Wiggler.Create(0.5f, 4f));
	}

	public static void CalculateBaseSizes()
	{
		PixelFont font = Dialog.Languages["english"].Font;
		float fontFaceSize = Dialog.Languages["english"].FontFaceSize;
		PixelFontSize pixelFontSize = font.Get(fontFaceSize);
		for (int i = 0; i < 10; i++)
		{
			float x = pixelFontSize.Measure(i.ToString()).X;
			if (x > numberWidth)
			{
				numberWidth = x;
			}
		}
		spacerWidth = pixelFontSize.Measure('.').X;
	}

	public override void Update()
	{
		Level level = base.Scene as Level;
		if (level.Completed)
		{
			if (CompleteTimer == 0f)
			{
				wiggler.Start();
			}
			CompleteTimer += Engine.DeltaTime;
		}
		bool flag = false;
		if (level.Session.Area.ID != 8 && !level.TimerHidden)
		{
			if (Settings.Instance.SpeedrunClock == SpeedrunType.Chapter)
			{
				if (CompleteTimer < 3f)
				{
					flag = true;
				}
			}
			else if (Settings.Instance.SpeedrunClock == SpeedrunType.File)
			{
				flag = true;
			}
		}
		DrawLerp = Calc.Approach(DrawLerp, flag ? 1 : 0, Engine.DeltaTime * 4f);
		base.Update();
	}

	public override void Render()
	{
		if (!(DrawLerp <= 0f))
		{
			float num = -300f * Ease.CubeIn(1f - DrawLerp);
			Level level = base.Scene as Level;
			Session session = level.Session;
			if (Settings.Instance.SpeedrunClock == SpeedrunType.Chapter)
			{
				string timeString = TimeSpan.FromTicks(session.Time).ShortGameplayFormat();
				bg.Draw(new Vector2(num, base.Y));
				DrawTime(new Vector2(num + 32f, base.Y + 44f), timeString, 1f + wiggler.Value * 0.15f, session.StartedFromBeginning, level.Completed, session.BeatBestTime);
			}
			else if (Settings.Instance.SpeedrunClock == SpeedrunType.File)
			{
				TimeSpan timeSpan = TimeSpan.FromTicks(session.Time);
				string text = "";
				text = ((!(timeSpan.TotalHours >= 1.0)) ? timeSpan.ToString("mm\\:ss") : ((int)timeSpan.TotalHours + ":" + timeSpan.ToString("mm\\:ss")));
				TimeSpan timeSpan2 = TimeSpan.FromTicks(SaveData.Instance.Time);
				int num2 = (int)timeSpan2.TotalHours;
				string timeString2 = num2 + timeSpan2.ToString("\\:mm\\:ss\\.fff");
				int num3 = ((num2 < 10) ? 64 : ((num2 < 100) ? 96 : 128));
				Draw.Rect(num, base.Y, num3 + 2, 38f, Color.Black);
				bg.Draw(new Vector2(num + (float)num3, base.Y));
				DrawTime(new Vector2(num + 32f, base.Y + 44f), timeString2);
				bg.Draw(new Vector2(num, base.Y + 38f), Vector2.Zero, Color.White, 0.6f);
				DrawTime(new Vector2(num + 32f, base.Y + 40f + 26.400002f), text, (1f + wiggler.Value * 0.15f) * 0.6f, session.StartedFromBeginning, level.Completed, session.BeatBestTime, 0.6f);
			}
		}
	}

	public static void DrawTime(Vector2 position, string timeString, float scale = 1f, bool valid = true, bool finished = false, bool bestTime = false, float alpha = 1f)
	{
		PixelFont font = Dialog.Languages["english"].Font;
		float fontFaceSize = Dialog.Languages["english"].FontFaceSize;
		float num = scale;
		float num2 = position.X;
		float num3 = position.Y;
		Color color = Color.White * alpha;
		Color color2 = Color.LightGray * alpha;
		if (!valid)
		{
			color = Calc.HexToColor("918988") * alpha;
			color2 = Calc.HexToColor("7a6f6d") * alpha;
		}
		else if (bestTime)
		{
			color = Calc.HexToColor("fad768") * alpha;
			color2 = Calc.HexToColor("cfa727") * alpha;
		}
		else if (finished)
		{
			color = Calc.HexToColor("6ded87") * alpha;
			color2 = Calc.HexToColor("43d14c") * alpha;
		}
		for (int i = 0; i < timeString.Length; i++)
		{
			char c = timeString[i];
			if (c == '.')
			{
				num = scale * 0.7f;
				num3 -= 5f * scale;
			}
			Color color3 = ((c == ':' || c == '.' || num < scale) ? color2 : color);
			float num4 = (((c == ':' || c == '.') ? spacerWidth : numberWidth) + 4f) * num;
			font.DrawOutline(fontFaceSize, c.ToString(), new Vector2(num2 + num4 / 2f, num3), new Vector2(0.5f, 1f), Vector2.One * num, color3, 2f, Color.Black);
			num2 += num4;
		}
	}

	public static float GetTimeWidth(string timeString, float scale = 1f)
	{
		float num = scale;
		float num2 = 0f;
		foreach (char c in timeString)
		{
			if (c == '.')
			{
				num = scale * 0.7f;
			}
			float num3 = (((c == ':' || c == '.') ? spacerWidth : numberWidth) + 4f) * num;
			num2 += num3;
		}
		return num2;
	}
}
