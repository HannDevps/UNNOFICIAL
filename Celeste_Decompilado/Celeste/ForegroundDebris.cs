using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ForegroundDebris : Entity
{
	private Vector2 start;

	private float parallax;

	public ForegroundDebris(Vector2 position)
		: base(position)
	{
		start = Position;
		base.Depth = -999900;
		string key = "scenery/fgdebris/" + Calc.Random.Choose("rock_a", "rock_b");
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures(key);
		atlasSubtextures.Reverse();
		foreach (MTexture item in atlasSubtextures)
		{
			Image img = new Image(item);
			img.CenterOrigin();
			Add(img);
			SineWave sine = new SineWave(0.4f);
			sine.Randomize();
			sine.OnUpdate = delegate
			{
				img.Y = sine.Value * 2f;
			};
			Add(sine);
		}
		parallax = 0.05f + Calc.Random.NextFloat(0.08f);
	}

	public ForegroundDebris(EntityData data, Vector2 offset)
		: this(data.Position + offset)
	{
	}

	public override void Render()
	{
		Vector2 vector = SceneAs<Level>().Camera.Position + new Vector2(320f, 180f) / 2f - start;
		Vector2 position = Position;
		Position -= vector * parallax;
		base.Render();
		Position = position;
	}
}
