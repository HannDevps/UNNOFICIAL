using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class CurtainWipe : ScreenWipe
{
	private VertexPositionColor[] vertexBufferLeft = new VertexPositionColor[192];

	private VertexPositionColor[] vertexBufferRight = new VertexPositionColor[192];

	public CurtainWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		for (int i = 0; i < vertexBufferLeft.Length; i++)
		{
			vertexBufferLeft[i].Color = ScreenWipe.WipeColor;
		}
	}

	public override void Render(Scene scene)
	{
		float num = (WipeIn ? Ease.CubeInOut : Ease.CubeInOut)(WipeIn ? (1f - Percent) : Percent);
		float num2 = Math.Min(1f, num / 0.3f);
		float num3 = Math.Max(0f, Math.Min(1f, (num - 0.1f) / 0.9f / 0.9f));
		Vector2 vector = new Vector2(0f, 540f * num2);
		Vector2 vector2 = new Vector2(1920f, 1592f) / 2f;
		Vector2 control = (vector + vector2) / 2f + Vector2.UnitY * 1080f * 0.25f;
		Vector2 vector3 = new Vector2(896f + 200f * num, -350f + 256f * num2);
		Vector2 point = new SimpleCurve(vector, vector2, control).GetPoint(num3);
		Vector2 vector4 = new Vector2(point.X + 64f * num, 1080f);
		int i = 0;
		vertexBufferLeft[i++].Position = new Vector3(-10f, -10f, 0f);
		vertexBufferLeft[i++].Position = new Vector3(vector3.X, -10f, 0f);
		vertexBufferLeft[i++].Position = new Vector3(vector3.X, vector3.Y, 0f);
		vertexBufferLeft[i++].Position = new Vector3(-10f, -10f, 0f);
		vertexBufferLeft[i++].Position = new Vector3(-10f, point.Y, 0f);
		vertexBufferLeft[i++].Position = new Vector3(point.X, point.Y, 0f);
		vertexBufferLeft[i++].Position = new Vector3(point.X, point.Y, 0f);
		vertexBufferLeft[i++].Position = new Vector3(-10f, point.Y, 0f);
		vertexBufferLeft[i++].Position = new Vector3(-10f, 1090f, 0f);
		vertexBufferLeft[i++].Position = new Vector3(point.X, point.Y, 0f);
		vertexBufferLeft[i++].Position = new Vector3(-10f, 1090f, 0f);
		vertexBufferLeft[i++].Position = new Vector3(vector4.X, vector4.Y + 10f, 0f);
		int num4 = i;
		Vector2 value = vector3;
		for (; i < vertexBufferLeft.Length; i += 3)
		{
			Vector2 point2 = new SimpleCurve(vector3, point, (vector3 + point) / 2f + new Vector2(0f, 384f * num3)).GetPoint((float)(i - num4) / (float)(vertexBufferLeft.Length - num4 - 3));
			vertexBufferLeft[i].Position = new Vector3(-10f, -10f, 0f);
			vertexBufferLeft[i + 1].Position = new Vector3(value, 0f);
			vertexBufferLeft[i + 2].Position = new Vector3(point2, 0f);
			value = point2;
		}
		for (i = 0; i < vertexBufferLeft.Length; i++)
		{
			vertexBufferRight[i] = vertexBufferLeft[i];
			vertexBufferRight[i].Position.X = 1920f - vertexBufferRight[i].Position.X;
		}
		ScreenWipe.DrawPrimitives(vertexBufferLeft);
		ScreenWipe.DrawPrimitives(vertexBufferRight);
	}
}
