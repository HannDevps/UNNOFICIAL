using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class WindWipe : ScreenWipe
{
	private int t;

	private int columns;

	private int rows;

	private VertexPositionColor[] vertexBuffer;

	public WindWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		t = 40;
		columns = 1920 / t + 1;
		rows = 1080 / t + 1;
		vertexBuffer = new VertexPositionColor[columns * rows * 6];
		for (int i = 0; i < vertexBuffer.Length; i++)
		{
			vertexBuffer[i].Color = ScreenWipe.WipeColor;
		}
	}

	public override void Render(Scene scene)
	{
		float num = columns * rows;
		int num2 = 0;
		for (int i = 0; i < columns; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				int num3 = (WipeIn ? (columns - i - 1) : i);
				float num4 = (float)((j + num3 % 2) % 2 * (rows + j / 2) + (j + num3 % 2 + 1) % 2 * (j / 2) + num3 * rows) / num * 0.5f;
				float num5 = num4 + 300f / num;
				float num6 = (Math.Max(num4, Math.Min(num5, WipeIn ? (1f - Percent) : Percent)) - num4) / (num5 - num4);
				float num7 = ((float)i - 0.5f) * (float)t;
				float num8 = ((float)j - 0.5f) * (float)t - (float)t * 0.5f * num6;
				float x = num7 + (float)t;
				float y = num8 + (float)t * num6;
				vertexBuffer[num2].Position = new Vector3(num7, num8, 0f);
				vertexBuffer[num2 + 1].Position = new Vector3(x, num8, 0f);
				vertexBuffer[num2 + 2].Position = new Vector3(num7, y, 0f);
				vertexBuffer[num2 + 3].Position = new Vector3(x, num8, 0f);
				vertexBuffer[num2 + 4].Position = new Vector3(x, y, 0f);
				vertexBuffer[num2 + 5].Position = new Vector3(num7, y, 0f);
				num2 += 6;
			}
		}
		ScreenWipe.DrawPrimitives(vertexBuffer);
	}
}
