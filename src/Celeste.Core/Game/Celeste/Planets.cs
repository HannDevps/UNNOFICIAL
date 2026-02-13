using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Planets : Backdrop
{
	private struct Planet
	{
		public MTexture Texture;

		public Vector2 Position;
	}

	private Planet[] planets;

	public const int MapWidth = 640;

	public const int MapHeight = 360;

	public Planets(int count, string size)
	{
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("bgs/10/" + size);
		planets = new Planet[count];
		for (int i = 0; i < planets.Length; i++)
		{
			planets[i].Texture = Calc.Random.Choose(atlasSubtextures);
			planets[i].Position = new Vector2
			{
				X = Calc.Random.NextFloat(640f),
				Y = Calc.Random.NextFloat(360f)
			};
		}
	}

	public override void Render(Scene scene)
	{
		Vector2 position = (scene as Level).Camera.Position;
		Color color = Color * FadeAlphaMultiplier;
		for (int i = 0; i < planets.Length; i++)
		{
			Vector2 position2 = new Vector2
			{
				X = -32f + Mod(planets[i].Position.X - position.X * Scroll.X, 640f),
				Y = -32f + Mod(planets[i].Position.Y - position.Y * Scroll.Y, 360f)
			};
			planets[i].Texture.DrawCentered(position2, color);
		}
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
