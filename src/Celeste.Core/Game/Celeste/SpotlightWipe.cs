using System;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class SpotlightWipe : ScreenWipe
{
	public static Vector2 FocusPoint;

	public static float Modifier = 0f;

	public bool Linear;

	private const float SmallCircleRadius = 288f;

	private const float EaseDuration = 1.8f;

	private const float EaseOpenPercent = 0.2f;

	private const float EaseClosePercent = 0.2f;

	private static VertexPositionColor[] vertexBuffer = new VertexPositionColor[768];

	private EventInstance sfx;

	public SpotlightWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		Duration = 1.8f;
		Modifier = 0f;
		if (wipeIn)
		{
			sfx = Audio.Play("event:/game/general/spotlight_intro");
		}
		else
		{
			sfx = Audio.Play("event:/game/general/spotlight_outro");
		}
	}

	public override void Cancel()
	{
		if (sfx != null)
		{
			sfx.stop(STOP_MODE.IMMEDIATE);
			sfx.release();
			sfx = null;
		}
		base.Cancel();
	}

	public override void Render(Scene scene)
	{
		float num = (WipeIn ? Percent : (1f - Percent));
		Vector2 focusPoint = FocusPoint;
		if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
		{
			focusPoint.X = 320f - focusPoint.X;
		}
		focusPoint.X *= 6f;
		focusPoint.Y *= 6f;
		float num2 = 0f;
		float num3 = 288f + Modifier;
		DrawSpotlight(radius: Linear ? (Ease.CubeInOut(num) * 1920f) : ((num < 0.2f) ? (Ease.CubeInOut(num / 0.2f) * num3) : ((!(num < 0.8f)) ? (num3 + (num - 0.8f) / 0.2f * (1920f - num3)) : num3)), position: focusPoint, color: ScreenWipe.WipeColor);
	}

	public static void DrawSpotlight(Vector2 position, float radius, Color color)
	{
		Vector2 vector = new Vector2(1f, 0f);
		for (int i = 0; i < vertexBuffer.Length; i += 12)
		{
			Vector2 vector2 = Calc.AngleToVector(((float)i + 12f) / (float)vertexBuffer.Length * ((float)Math.PI * 2f), 1f);
			vertexBuffer[i].Position = new Vector3(position + vector * 5000f, 0f);
			vertexBuffer[i].Color = color;
			vertexBuffer[i + 1].Position = new Vector3(position + vector * radius, 0f);
			vertexBuffer[i + 1].Color = color;
			vertexBuffer[i + 2].Position = new Vector3(position + vector2 * radius, 0f);
			vertexBuffer[i + 2].Color = color;
			vertexBuffer[i + 3].Position = new Vector3(position + vector * 5000f, 0f);
			vertexBuffer[i + 3].Color = color;
			vertexBuffer[i + 4].Position = new Vector3(position + vector2 * 5000f, 0f);
			vertexBuffer[i + 4].Color = color;
			vertexBuffer[i + 5].Position = new Vector3(position + vector2 * radius, 0f);
			vertexBuffer[i + 5].Color = color;
			vertexBuffer[i + 6].Position = new Vector3(position + vector * radius, 0f);
			vertexBuffer[i + 6].Color = color;
			vertexBuffer[i + 7].Position = new Vector3(position + vector * (radius - 2f), 0f);
			vertexBuffer[i + 7].Color = Color.Transparent;
			vertexBuffer[i + 8].Position = new Vector3(position + vector2 * (radius - 2f), 0f);
			vertexBuffer[i + 8].Color = Color.Transparent;
			vertexBuffer[i + 9].Position = new Vector3(position + vector * radius, 0f);
			vertexBuffer[i + 9].Color = color;
			vertexBuffer[i + 10].Position = new Vector3(position + vector2 * radius, 0f);
			vertexBuffer[i + 10].Color = color;
			vertexBuffer[i + 11].Position = new Vector3(position + vector2 * (radius - 2f), 0f);
			vertexBuffer[i + 11].Color = Color.Transparent;
			vector = vector2;
		}
		ScreenWipe.DrawPrimitives(vertexBuffer);
	}
}
