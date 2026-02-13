using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class DreamWipe : ScreenWipe
{
	private struct Circle
	{
		public Vector2 Position;

		public float Radius;

		public float Delay;
	}

	private readonly int circleColumns = 15;

	private readonly int circleRows = 8;

	private const int circleSegments = 32;

	private const float circleFillSpeed = 400f;

	private static Circle[] circles;

	private static VertexPositionColor[] vertexBuffer;

	public DreamWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		if (vertexBuffer == null)
		{
			vertexBuffer = new VertexPositionColor[(circleColumns + 2) * (circleRows + 2) * 32 * 3];
		}
		if (circles == null)
		{
			circles = new Circle[(circleColumns + 2) * (circleRows + 2)];
		}
		for (int i = 0; i < vertexBuffer.Length; i++)
		{
			vertexBuffer[i].Color = ScreenWipe.WipeColor;
		}
		int num = 1920 / circleColumns;
		int num2 = 1080 / circleRows;
		int j = 0;
		int num3 = 0;
		for (; j < circleColumns + 2; j++)
		{
			for (int k = 0; k < circleRows + 2; k++)
			{
				circles[num3].Position = new Vector2(((float)(j - 1) + 0.2f + Calc.Random.NextFloat(0.6f)) * (float)num, ((float)(k - 1) + 0.2f + Calc.Random.NextFloat(0.6f)) * (float)num2);
				circles[num3].Delay = Calc.Random.NextFloat(0.05f) + (float)(WipeIn ? (circleColumns - j) : j) * 0.018f;
				circles[num3].Radius = (WipeIn ? (400f * (Duration - circles[num3].Delay)) : 0f);
				num3++;
			}
		}
	}

	public override void Update(Scene scene)
	{
		base.Update(scene);
		for (int i = 0; i < circles.Length; i++)
		{
			if (!WipeIn)
			{
				circles[i].Delay -= Engine.DeltaTime;
				if (circles[i].Delay <= 0f)
				{
					circles[i].Radius += Engine.DeltaTime * 400f;
				}
			}
			else if (circles[i].Radius > 0f)
			{
				circles[i].Radius -= Engine.DeltaTime * 400f;
			}
			else
			{
				circles[i].Radius = 0f;
			}
		}
	}

	public override void Render(Scene scene)
	{
		int num = 0;
		for (int i = 0; i < circles.Length; i++)
		{
			Circle circle = circles[i];
			Vector2 vector = new Vector2(1f, 0f);
			for (float num2 = 0f; num2 < 32f; num2 += 1f)
			{
				Vector2 vector2 = Calc.AngleToVector((num2 + 1f) / 32f * ((float)Math.PI * 2f), 1f);
				vertexBuffer[num++].Position = new Vector3(circle.Position, 0f);
				vertexBuffer[num++].Position = new Vector3(circle.Position + vector * circle.Radius, 0f);
				vertexBuffer[num++].Position = new Vector3(circle.Position + vector2 * circle.Radius, 0f);
				vector = vector2;
			}
		}
		ScreenWipe.DrawPrimitives(vertexBuffer);
	}
}
