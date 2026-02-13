using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class HeartWipe : ScreenWipe
{
	private VertexPositionColor[] vertex = new VertexPositionColor[111];

	public HeartWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		for (int i = 0; i < vertex.Length; i++)
		{
			vertex[i].Color = ScreenWipe.WipeColor;
		}
	}

	public override void Render(Scene scene)
	{
		float num = ((WipeIn ? Percent : (1f - Percent)) - 0.2f) / 0.8f;
		if (num <= 0f)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null, Engine.ScreenMatrix);
			Draw.Rect(-1f, -1f, Engine.Width + 2, Engine.Height + 2, ScreenWipe.WipeColor);
			Draw.SpriteBatch.End();
			return;
		}
		Vector2 vector = new Vector2(Engine.Width, Engine.Height) / 2f;
		float num2 = (float)Engine.Width * 0.75f * num;
		float num3 = (float)Engine.Width * num;
		float num4 = -0.25f;
		float num5 = -(float)Math.PI / 2f;
		Vector2 vector2 = vector + new Vector2((0f - (float)Math.Cos(num4)) * num2, (0f - num2) / 2f);
		int num6 = 0;
		for (int i = 1; i <= 16; i++)
		{
			float angleRadians = num4 + (num5 - num4) * ((float)(i - 1) / 16f);
			float angleRadians2 = num4 + (num5 - num4) * ((float)i / 16f);
			vertex[num6++].Position = new Vector3(vector.X, 0f - num3, 0f);
			vertex[num6++].Position = new Vector3(vector2 + Calc.AngleToVector(angleRadians, num2), 0f);
			vertex[num6++].Position = new Vector3(vector2 + Calc.AngleToVector(angleRadians2, num2), 0f);
		}
		vertex[num6++].Position = new Vector3(vector.X, 0f - num3, 0f);
		vertex[num6++].Position = new Vector3(vector2 + new Vector2(0f, 0f - num2), 0f);
		vertex[num6++].Position = new Vector3(0f - num3, 0f - num3, 0f);
		vertex[num6++].Position = new Vector3(0f - num3, 0f - num3, 0f);
		vertex[num6++].Position = new Vector3(vector2 + new Vector2(0f, 0f - num2), 0f);
		vertex[num6++].Position = new Vector3(0f - num3, vector2.Y, 0f);
		float num7 = (float)Math.PI * 3f / 4f;
		for (int j = 1; j <= 16; j++)
		{
			float angleRadians3 = -(float)Math.PI / 2f - (float)(j - 1) / 16f * num7;
			float angleRadians4 = -(float)Math.PI / 2f - (float)j / 16f * num7;
			vertex[num6++].Position = new Vector3(0f - num3, vector2.Y, 0f);
			vertex[num6++].Position = new Vector3(vector2 + Calc.AngleToVector(angleRadians3, num2), 0f);
			vertex[num6++].Position = new Vector3(vector2 + Calc.AngleToVector(angleRadians4, num2), 0f);
		}
		Vector2 value = vector2 + Calc.AngleToVector(-(float)Math.PI / 2f - num7, num2);
		Vector2 value2 = vector + new Vector2(0f, num2 * 1.8f);
		vertex[num6++].Position = new Vector3(0f - num3, vector2.Y, 0f);
		vertex[num6++].Position = new Vector3(value, 0f);
		vertex[num6++].Position = new Vector3(0f - num3, (float)Engine.Height + num3, 0f);
		vertex[num6++].Position = new Vector3(0f - num3, (float)Engine.Height + num3, 0f);
		vertex[num6++].Position = new Vector3(value, 0f);
		vertex[num6++].Position = new Vector3(value2, 0f);
		vertex[num6++].Position = new Vector3(0f - num3, (float)Engine.Height + num3, 0f);
		vertex[num6++].Position = new Vector3(value2, 0f);
		vertex[num6++].Position = new Vector3(vector.X, (float)Engine.Height + num3, 0f);
		ScreenWipe.DrawPrimitives(vertex);
		for (num6 = 0; num6 < vertex.Length; num6++)
		{
			vertex[num6].Position.X = 1920f - vertex[num6].Position.X;
		}
		ScreenWipe.DrawPrimitives(vertex);
	}
}
