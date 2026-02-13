using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Starfield : Backdrop
{
	public struct Star
	{
		public MTexture Texture;

		public Vector2 Position;

		public Color Color;

		public int NodeIndex;

		public float NodePercent;

		public float Distance;

		public float Sine;
	}

	public const int StepSize = 32;

	public const int Steps = 15;

	public const float MinDist = 4f;

	public const float MaxDist = 24f;

	public float FlowSpeed;

	public List<float> YNodes = new List<float>();

	public Star[] Stars = new Star[128];

	public Starfield(Color color, float speed = 1f)
	{
		Color = color;
		FlowSpeed = speed;
		float num = Calc.Random.NextFloat(180f);
		int num2 = 0;
		while (num2 < 15)
		{
			YNodes.Add(num);
			num2++;
			num += (float)Calc.Random.Choose(-1, 1) * (16f + Calc.Random.NextFloat(24f));
		}
		for (int i = 0; i < 4; i++)
		{
			YNodes[YNodes.Count - 1 - i] = Calc.LerpClamp(YNodes[YNodes.Count - 1 - i], YNodes[0], 1f - (float)i / 4f);
		}
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("particles/starfield/");
		for (int j = 0; j < Stars.Length; j++)
		{
			float num3 = Calc.Random.NextFloat(1f);
			Stars[j].NodeIndex = Calc.Random.Next(YNodes.Count - 1);
			Stars[j].NodePercent = Calc.Random.NextFloat(1f);
			Stars[j].Distance = 4f + num3 * 20f;
			Stars[j].Sine = Calc.Random.NextFloat((float)Math.PI * 2f);
			Stars[j].Position = GetTargetOfStar(ref Stars[j]);
			Stars[j].Color = Color.Lerp(Color, Color.Transparent, num3 * 0.5f);
			int index = (int)Calc.Clamp(Ease.CubeIn(1f - num3) * (float)atlasSubtextures.Count, 0f, atlasSubtextures.Count - 1);
			Stars[j].Texture = atlasSubtextures[index];
		}
	}

	public override void Update(Scene scene)
	{
		base.Update(scene);
		for (int i = 0; i < Stars.Length; i++)
		{
			UpdateStar(ref Stars[i]);
		}
	}

	private void UpdateStar(ref Star star)
	{
		star.Sine += Engine.DeltaTime * FlowSpeed;
		star.NodePercent += Engine.DeltaTime * 0.25f * FlowSpeed;
		if (star.NodePercent >= 1f)
		{
			star.NodePercent -= 1f;
			star.NodeIndex++;
			if (star.NodeIndex >= YNodes.Count - 1)
			{
				star.NodeIndex = 0;
				star.Position.X -= 448f;
			}
		}
		star.Position += (GetTargetOfStar(ref star) - star.Position) / 50f;
	}

	private Vector2 GetTargetOfStar(ref Star star)
	{
		Vector2 vector = new Vector2(star.NodeIndex * 32, YNodes[star.NodeIndex]);
		Vector2 vector2 = new Vector2((star.NodeIndex + 1) * 32, YNodes[star.NodeIndex + 1]);
		Vector2 vector3 = vector + (vector2 - vector) * star.NodePercent;
		Vector2 vector4 = (vector2 - vector).SafeNormalize();
		Vector2 vector5 = new Vector2(0f - vector4.Y, vector4.X);
		return vector3 + vector5 * star.Distance * (float)Math.Sin(star.Sine);
	}

	public override void Render(Scene scene)
	{
		Vector2 position = (scene as Level).Camera.Position;
		for (int i = 0; i < Stars.Length; i++)
		{
			Vector2 position2 = new Vector2
			{
				X = -64f + Mod(Stars[i].Position.X - position.X * Scroll.X, 448f),
				Y = -16f + Mod(Stars[i].Position.Y - position.Y * Scroll.Y, 212f)
			};
			Stars[i].Texture.DrawCentered(position2, Stars[i].Color * FadeAlphaMultiplier);
		}
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
