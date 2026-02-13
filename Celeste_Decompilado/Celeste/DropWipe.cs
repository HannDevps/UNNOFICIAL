using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class DropWipe : ScreenWipe
{
	private const int columns = 10;

	private float[] meetings;

	private Color color;

	public DropWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		color = ScreenWipe.WipeColor;
		meetings = new float[10];
		for (int i = 0; i < 10; i++)
		{
			meetings[i] = 0.05f + Calc.Random.NextFloat() * 0.9f;
		}
	}

	public override void Render(Scene scene)
	{
		float num = (WipeIn ? (1f - Percent) : Percent);
		float num2 = 192f;
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Engine.ScreenMatrix);
		if (num >= 0.995f)
		{
			Draw.Rect(-10f, -10f, Engine.Width + 20, Engine.Height + 20, color);
		}
		else
		{
			for (int i = 0; i < 10; i++)
			{
				float num3 = (float)i / 10f;
				float num4 = (WipeIn ? (1f - num3) : num3) * 0.3f;
				if (num > num4)
				{
					float num5 = Ease.CubeIn(Math.Min(1f, (num - num4) / 0.7f));
					float num6 = 1080f * meetings[i] * num5;
					float num7 = 1080f * (1f - meetings[i]) * num5;
					Draw.Rect((float)i * num2 - 1f, -10f, num2 + 2f, num6 + 10f, color);
					Draw.Rect((float)i * num2 - 1f, 1080f - num7, num2 + 2f, num7 + 10f, color);
				}
			}
		}
		Draw.SpriteBatch.End();
	}
}
