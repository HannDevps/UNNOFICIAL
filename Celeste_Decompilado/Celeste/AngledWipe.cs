using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class AngledWipe : ScreenWipe
{
	private const int rows = 6;

	private const float angleSize = 64f;

	private VertexPositionColor[] vertexBuffer = new VertexPositionColor[36];

	public AngledWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		for (int i = 0; i < vertexBuffer.Length; i++)
		{
			vertexBuffer[i].Color = ScreenWipe.WipeColor;
		}
	}

	public override void Render(Scene scene)
	{
		float num = 183.33333f;
		float num2 = -64f;
		float num3 = 1984f;
		for (int i = 0; i < 6; i++)
		{
			int num4 = i * 6;
			float num5 = num2;
			float num6 = -10f + (float)i * num;
			float num7 = 0f;
			float num8 = (float)i / 6f;
			float num9 = (WipeIn ? (1f - num8) : num8) * 0.3f;
			if (Percent > num9)
			{
				num7 = Math.Min(1f, (Percent - num9) / 0.7f);
			}
			if (WipeIn)
			{
				num7 = 1f - num7;
			}
			float num10 = num3 * num7;
			vertexBuffer[num4].Position = new Vector3(num5, num6, 0f);
			vertexBuffer[num4 + 1].Position = new Vector3(num5 + num10, num6, 0f);
			vertexBuffer[num4 + 2].Position = new Vector3(num5, num6 + num, 0f);
			vertexBuffer[num4 + 3].Position = new Vector3(num5 + num10, num6, 0f);
			vertexBuffer[num4 + 4].Position = new Vector3(num5 + num10 + 64f, num6 + num, 0f);
			vertexBuffer[num4 + 5].Position = new Vector3(num5, num6 + num, 0f);
		}
		if (WipeIn)
		{
			for (int j = 0; j < vertexBuffer.Length; j++)
			{
				vertexBuffer[j].Position.X = 1920f - vertexBuffer[j].Position.X;
				vertexBuffer[j].Position.Y = 1080f - vertexBuffer[j].Position.Y;
			}
		}
		ScreenWipe.DrawPrimitives(vertexBuffer);
	}
}
