using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class SolidTiles : Solid
{
	public TileGrid Tiles;

	public AnimatedTiles AnimatedTiles;

	public Grid Grid;

	private VirtualMap<char> tileTypes;

	public SolidTiles(Vector2 position, VirtualMap<char> data)
		: base(position, 0f, 0f, safe: true)
	{
		base.Tag = Tags.Global;
		base.Depth = -10000;
		tileTypes = data;
		EnableAssistModeChecks = false;
		AllowStaticMovers = false;
		base.Collider = (Grid = new Grid(data.Columns, data.Rows, 8f, 8f));
		for (int i = 0; i < data.Columns; i += 50)
		{
			for (int j = 0; j < data.Rows; j += 50)
			{
				if (!data.AnyInSegmentAtTile(i, j))
				{
					continue;
				}
				int k = i;
				for (int num = Math.Min(k + 50, data.Columns); k < num; k++)
				{
					int l = j;
					for (int num2 = Math.Min(l + 50, data.Rows); l < num2; l++)
					{
						if (data[k, l] != '0')
						{
							Grid[k, l] = true;
						}
					}
				}
			}
		}
		Autotiler.Generated generated = GFX.FGAutotiler.GenerateMap(data, paddingIgnoreOutOfLevel: true);
		Tiles = generated.TileGrid;
		Tiles.VisualExtend = 1;
		Add(Tiles);
		Add(AnimatedTiles = generated.SpriteOverlay);
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Tiles.ClipCamera = SceneAs<Level>().Camera;
		AnimatedTiles.ClipCamera = Tiles.ClipCamera;
	}

	private int CoreTileSurfaceIndex()
	{
		Level level = base.Scene as Level;
		if (level.CoreMode == Session.CoreModes.Hot)
		{
			return 37;
		}
		if (level.CoreMode == Session.CoreModes.Cold)
		{
			return 36;
		}
		return 3;
	}

	private int SurfaceSoundIndexAt(Vector2 readPosition)
	{
		int num = (int)((readPosition.X - base.X) / 8f);
		int num2 = (int)((readPosition.Y - base.Y) / 8f);
		if (num >= 0 && num2 >= 0 && num < Grid.CellsX && num2 < Grid.CellsY)
		{
			char c = tileTypes[num, num2];
			switch (c)
			{
			case 'k':
				return CoreTileSurfaceIndex();
			default:
				if (SurfaceIndex.TileToIndex.ContainsKey(c))
				{
					return SurfaceIndex.TileToIndex[c];
				}
				break;
			case '0':
				break;
			}
		}
		return -1;
	}

	public override int GetWallSoundIndex(Player player, int side)
	{
		int num = SurfaceSoundIndexAt(player.Center + Vector2.UnitX * side * 8f);
		if (num < 0)
		{
			num = SurfaceSoundIndexAt(player.Center + new Vector2(side * 8, -6f));
		}
		if (num < 0)
		{
			num = SurfaceSoundIndexAt(player.Center + new Vector2(side * 8, 6f));
		}
		return num;
	}

	public override int GetStepSoundIndex(Entity entity)
	{
		int num = SurfaceSoundIndexAt(entity.BottomCenter + Vector2.UnitY * 4f);
		if (num == -1)
		{
			num = SurfaceSoundIndexAt(entity.BottomLeft + Vector2.UnitY * 4f);
		}
		if (num == -1)
		{
			num = SurfaceSoundIndexAt(entity.BottomRight + Vector2.UnitY * 4f);
		}
		return num;
	}

	public override int GetLandSoundIndex(Entity entity)
	{
		int num = SurfaceSoundIndexAt(entity.BottomCenter + Vector2.UnitY * 4f);
		if (num == -1)
		{
			num = SurfaceSoundIndexAt(entity.BottomLeft + Vector2.UnitY * 4f);
		}
		if (num == -1)
		{
			num = SurfaceSoundIndexAt(entity.BottomRight + Vector2.UnitY * 4f);
		}
		return num;
	}
}
