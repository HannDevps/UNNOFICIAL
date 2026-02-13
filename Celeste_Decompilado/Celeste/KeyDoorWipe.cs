using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class KeyDoorWipe : ScreenWipe
{
	private VertexPositionColor[] vertex = new VertexPositionColor[57];

	public KeyDoorWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		for (int i = 0; i < vertex.Length; i++)
		{
			vertex[i].Color = ScreenWipe.WipeColor;
		}
	}

	public override void Render(Scene scene)
	{
		int num = 1090;
		int num2 = 540;
		float num3 = (WipeIn ? (1f - Percent) : Percent);
		float num4 = Ease.SineInOut(Math.Min(1f, num3 / 0.5f));
		float num5 = Ease.SineInOut(1f - Calc.Clamp((num3 - 0.5f) / 0.3f, 0f, 1f));
		float num6 = num4;
		float num7 = 1f + (1f - num4) * 0.5f;
		float num8 = 960f * num4;
		float num9 = 128f * num5 * num6;
		float num10 = 128f * num5 * num7;
		float num11 = (float)num2 - (float)num2 * 0.3f * num5 * num7;
		float y = (float)num2 + (float)num2 * 0.5f * num5 * num7;
		float num12 = 0f;
		float angleRadians = 0f;
		int num13 = 0;
		vertex[num13++].Position = new Vector3(-10f, -10f, 0f);
		vertex[num13++].Position = new Vector3(num8, -10f, 0f);
		vertex[num13++].Position = new Vector3(num8, num11 - num10, 0f);
		for (int i = 1; i <= 8; i++)
		{
			num12 = -(float)Math.PI / 2f - (float)(i - 1) / 8f * ((float)Math.PI / 2f);
			angleRadians = -(float)Math.PI / 2f - (float)i / 8f * ((float)Math.PI / 2f);
			vertex[num13++].Position = new Vector3(-10f, -10f, 0f);
			vertex[num13++].Position = new Vector3(new Vector2(num8, num11) + Calc.AngleToVector(num12, 1f) * new Vector2(num9, num10), 0f);
			vertex[num13++].Position = new Vector3(new Vector2(num8, num11) + Calc.AngleToVector(angleRadians, 1f) * new Vector2(num9, num10), 0f);
		}
		vertex[num13++].Position = new Vector3(-10f, -10f, 0f);
		vertex[num13++].Position = new Vector3(num8 - num9, num11, 0f);
		vertex[num13++].Position = new Vector3(-10f, num, 0f);
		for (int j = 1; j <= 6; j++)
		{
			num12 = (float)Math.PI - (float)(j - 1) / 8f * ((float)Math.PI / 2f);
			angleRadians = (float)Math.PI - (float)j / 8f * ((float)Math.PI / 2f);
			vertex[num13++].Position = new Vector3(-10f, num, 0f);
			vertex[num13++].Position = new Vector3(new Vector2(num8, num11) + Calc.AngleToVector(num12, 1f) * new Vector2(num9, num10), 0f);
			vertex[num13++].Position = new Vector3(new Vector2(num8, num11) + Calc.AngleToVector(angleRadians, 1f) * new Vector2(num9, num10), 0f);
		}
		vertex[num13++].Position = new Vector3(-10f, num, 0f);
		vertex[num13++].Position = new Vector3(new Vector2(num8, num11) + Calc.AngleToVector(angleRadians, 1f) * new Vector2(num9, num10), 0f);
		vertex[num13++].Position = new Vector3(num8 - num9 * 0.8f, y, 0f);
		vertex[num13++].Position = new Vector3(-10f, num, 0f);
		vertex[num13++].Position = new Vector3(num8 - num9 * 0.8f, y, 0f);
		vertex[num13++].Position = new Vector3(num8, y, 0f);
		vertex[num13++].Position = new Vector3(-10f, num, 0f);
		vertex[num13++].Position = new Vector3(num8, y, 0f);
		vertex[num13++].Position = new Vector3(num8, num, 0f);
		ScreenWipe.DrawPrimitives(vertex);
		for (num13 = 0; num13 < vertex.Length; num13++)
		{
			vertex[num13].Position.X = 1920f - vertex[num13].Position.X;
		}
		ScreenWipe.DrawPrimitives(vertex);
	}
}
