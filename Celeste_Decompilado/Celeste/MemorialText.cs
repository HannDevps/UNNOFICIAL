using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class MemorialText : Entity
{
	public bool Show;

	public bool Dreamy;

	public Memorial Memorial;

	private float index;

	private string message;

	private float alpha;

	private float timer;

	private float widestCharacter;

	private int firstLineLength;

	private SoundSource textSfx;

	private bool textSfxPlaying;

	public MemorialText(Memorial memorial, bool dreamy)
	{
		AddTag(Tags.HUD);
		AddTag(Tags.PauseUpdate);
		Add(textSfx = new SoundSource());
		Dreamy = dreamy;
		Memorial = memorial;
		message = Dialog.Clean("memorial");
		firstLineLength = CountToNewline(0);
		for (int i = 0; i < message.Length; i++)
		{
			float x = ActiveFont.Measure(message[i]).X;
			if (x > widestCharacter)
			{
				widestCharacter = x;
			}
		}
		widestCharacter *= 0.9f;
	}

	public override void Update()
	{
		base.Update();
		if ((base.Scene as Level).Paused)
		{
			textSfx.Pause();
			return;
		}
		timer += Engine.DeltaTime;
		if (!Show)
		{
			alpha = Calc.Approach(alpha, 0f, Engine.DeltaTime);
			if (alpha <= 0f)
			{
				index = firstLineLength;
			}
		}
		else
		{
			alpha = Calc.Approach(alpha, 1f, Engine.DeltaTime * 2f);
			if (alpha >= 1f)
			{
				index = Calc.Approach(index, message.Length, 32f * Engine.DeltaTime);
			}
		}
		if (Show && alpha >= 1f && index < (float)message.Length)
		{
			if (!textSfxPlaying)
			{
				textSfxPlaying = true;
				textSfx.Play(Dreamy ? "event:/ui/game/memorial_dream_text_loop" : "event:/ui/game/memorial_text_loop");
				textSfx.Param("end", 0f);
			}
		}
		else if (textSfxPlaying)
		{
			textSfxPlaying = false;
			textSfx.Stop();
			textSfx.Param("end", 1f);
		}
		textSfx.Resume();
	}

	private int CountToNewline(int start)
	{
		int i;
		for (i = start; i < message.Length && message[i] != '\n'; i++)
		{
		}
		return i - start;
	}

	public override void Render()
	{
		if ((base.Scene as Level).FrozenOrPaused || (base.Scene as Level).Completed || !(index > 0f) || !(alpha > 0f))
		{
			return;
		}
		Camera camera = SceneAs<Level>().Camera;
		Vector2 vector = new Vector2((Memorial.X - camera.X) * 6f, (Memorial.Y - camera.Y) * 6f - 350f - ActiveFont.LineHeight * 3.3f);
		if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
		{
			vector.X = 1920f - vector.X;
		}
		float num = Ease.CubeInOut(alpha);
		int num2 = (int)Math.Min(message.Length, index);
		int num3 = 0;
		float num4 = 64f * (1f - num);
		int num5 = CountToNewline(0);
		for (int i = 0; i < num2; i++)
		{
			char c = message[i];
			if (c == '\n')
			{
				num3 = 0;
				num5 = CountToNewline(i + 1);
				num4 += ActiveFont.LineHeight * 1.1f;
				continue;
			}
			float x = 1f;
			float x2 = (float)(-num5) * widestCharacter / 2f + ((float)num3 + 0.5f) * widestCharacter;
			float num6 = 0f;
			if (Dreamy && c != ' ' && c != '-' && c != '\n')
			{
				c = message[(i + (int)(Math.Sin(timer * 2f + (float)i / 8f) * 4.0) + message.Length) % message.Length];
				num6 = (float)Math.Sin(timer * 2f + (float)i / 8f) * 8f;
				x = ((!(Math.Sin(timer * 4f + (float)i / 16f) < 0.0)) ? 1 : (-1));
			}
			ActiveFont.Draw(c, vector + new Vector2(x2, num4 + num6), new Vector2(0.5f, 1f), new Vector2(x, 1f), Color.White * num);
			num3++;
		}
	}
}
