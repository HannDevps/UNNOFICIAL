using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class FallWipe : ScreenWipe
{
	private VertexPositionColor[] vertexBuffer = new VertexPositionColor[9];

	public FallWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		for (int i = 0; i < vertexBuffer.Length; i++)
		{
			vertexBuffer[i].Color = ScreenWipe.WipeColor;
		}
	}

	public override void Render(Scene scene)
	{
		float percent = Percent;
		Vector2 value = new Vector2(960f, 1080f - 2160f * percent);
		Vector2 value2 = new Vector2(-10f, 2160f * (1f - percent));
		Vector2 value3 = new Vector2(base.Right, 2160f * (1f - percent));
		if (!WipeIn)
		{
			vertexBuffer[0].Position = new Vector3(value, 0f);
			vertexBuffer[1].Position = new Vector3(value2, 0f);
			vertexBuffer[2].Position = new Vector3(value3, 0f);
			vertexBuffer[3].Position = new Vector3(value2, 0f);
			vertexBuffer[4].Position = new Vector3(value3, 0f);
			vertexBuffer[5].Position = new Vector3(value2.X, value2.Y + 1080f + 10f, 0f);
			vertexBuffer[6].Position = new Vector3(value3, 0f);
			vertexBuffer[8].Position = new Vector3(value3.X, value3.Y + 1080f + 10f, 0f);
			vertexBuffer[7].Position = new Vector3(value2.X, value2.Y + 1080f + 10f, 0f);
		}
		else
		{
			vertexBuffer[0].Position = new Vector3(value2.X, value.Y - 1080f - 10f, 0f);
			vertexBuffer[1].Position = new Vector3(value3.X, value.Y - 1080f - 10f, 0f);
			vertexBuffer[2].Position = new Vector3(value, 0f);
			vertexBuffer[3].Position = new Vector3(value2.X, value.Y - 1080f - 10f, 0f);
			vertexBuffer[4].Position = new Vector3(value, 0f);
			vertexBuffer[5].Position = new Vector3(value2, 0f);
			vertexBuffer[6].Position = new Vector3(value3.X, value.Y - 1080f - 10f, 0f);
			vertexBuffer[7].Position = new Vector3(value3, 0f);
			vertexBuffer[8].Position = new Vector3(value, 0f);
		}
		for (int i = 0; i < vertexBuffer.Length; i++)
		{
			vertexBuffer[i].Position.Y = 1080f - vertexBuffer[i].Position.Y;
		}
		ScreenWipe.DrawPrimitives(vertexBuffer);
	}
}
