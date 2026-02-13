using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class IceTileOverlay : Entity
{
	private List<MTexture> surfaces;

	private float alpha;

	public IceTileOverlay()
	{
		base.Depth = -10010;
		base.Tag = Tags.Global;
		Visible = false;
		surfaces = GFX.Game.GetAtlasSubtextures("scenery/iceSurface");
	}

	public override void Update()
	{
		base.Update();
		alpha = Calc.Approach(alpha, ((base.Scene as Level).CoreMode == Session.CoreModes.Cold) ? 1 : 0, Engine.DeltaTime * 4f);
		Visible = alpha > 0f;
	}

	public override void Render()
	{
		Level level = base.Scene as Level;
		Camera camera = level.Camera;
		Color color = Color.White * alpha;
		int num = (int)(Math.Floor((camera.Left - level.SolidTiles.X) / 8f) - 1.0);
		int num2 = (int)(Math.Floor((camera.Top - level.SolidTiles.Y) / 8f) - 1.0);
		int num3 = (int)(Math.Ceiling((camera.Right - level.SolidTiles.X) / 8f) + 1.0);
		int num4 = (int)(Math.Ceiling((camera.Bottom - level.SolidTiles.Y) / 8f) + 1.0);
		for (int i = num; i < num3; i++)
		{
			for (int j = num2; j < num4; j++)
			{
				if (level.SolidsData.SafeCheck(i, j) != '0' && level.SolidsData.SafeCheck(i, j - 1) == '0')
				{
					Vector2 position = level.SolidTiles.Position + new Vector2(i, j) * 8f;
					int index = (i * 5 + j * 17) % surfaces.Count;
					surfaces[index].Draw(position, Vector2.Zero, color);
				}
			}
		}
	}
}
