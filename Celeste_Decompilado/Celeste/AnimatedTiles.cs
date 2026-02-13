using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class AnimatedTiles : Component
{
	private class Tile
	{
		public int AnimationID;

		public float Frame;

		public Vector2 Scale;
	}

	public Camera ClipCamera;

	public Vector2 Position;

	public Color Color = Color.White;

	public float Alpha = 1f;

	public AnimatedTilesBank Bank;

	private VirtualMap<List<Tile>> tiles;

	public AnimatedTiles(int columns, int rows, AnimatedTilesBank bank)
		: base(active: true, visible: true)
	{
		tiles = new VirtualMap<List<Tile>>(columns, rows);
		Bank = bank;
	}

	public void Set(int x, int y, string name, float scaleX = 1f, float scaleY = 1f)
	{
		if (!string.IsNullOrEmpty(name))
		{
			AnimatedTilesBank.Animation animation = Bank.AnimationsByName[name];
			List<Tile> list = tiles[x, y];
			if (list == null)
			{
				List<Tile> list2 = (tiles[x, y] = new List<Tile>());
				list = list2;
			}
			list.Add(new Tile
			{
				AnimationID = animation.ID,
				Frame = Calc.Random.Next(animation.Frames.Length),
				Scale = new Vector2(scaleX, scaleY)
			});
		}
	}

	public Rectangle GetClippedRenderTiles(int extend)
	{
		Vector2 vector = base.Entity.Position + Position;
		int val;
		int val2;
		int val3;
		int val4;
		if (ClipCamera == null)
		{
			val = -extend;
			val2 = -extend;
			val3 = tiles.Columns + extend;
			val4 = tiles.Rows + extend;
		}
		else
		{
			Camera clipCamera = ClipCamera;
			val = (int)Math.Max(0.0, Math.Floor((clipCamera.Left - vector.X) / 8f) - (double)extend);
			val2 = (int)Math.Max(0.0, Math.Floor((clipCamera.Top - vector.Y) / 8f) - (double)extend);
			val3 = (int)Math.Min(tiles.Columns, Math.Ceiling((clipCamera.Right - vector.X) / 8f) + (double)extend);
			val4 = (int)Math.Min(tiles.Rows, Math.Ceiling((clipCamera.Bottom - vector.Y) / 8f) + (double)extend);
		}
		val = Math.Max(val, 0);
		val2 = Math.Max(val2, 0);
		val3 = Math.Min(val3, tiles.Columns);
		val4 = Math.Min(val4, tiles.Rows);
		return new Rectangle(val, val2, val3 - val, val4 - val2);
	}

	public override void Update()
	{
		Rectangle clippedRenderTiles = GetClippedRenderTiles(1);
		for (int i = clippedRenderTiles.Left; i < clippedRenderTiles.Right; i++)
		{
			for (int j = clippedRenderTiles.Top; j < clippedRenderTiles.Bottom; j++)
			{
				List<Tile> list = tiles[i, j];
				if (list != null)
				{
					for (int k = 0; k < list.Count; k++)
					{
						AnimatedTilesBank.Animation animation = Bank.Animations[list[k].AnimationID];
						list[k].Frame += Engine.DeltaTime / animation.Delay;
					}
				}
			}
		}
	}

	public override void Render()
	{
		RenderAt(base.Entity.Position + Position);
	}

	public void RenderAt(Vector2 position)
	{
		Rectangle clippedRenderTiles = GetClippedRenderTiles(1);
		Color color = Color * Alpha;
		for (int i = clippedRenderTiles.Left; i < clippedRenderTiles.Right; i++)
		{
			for (int j = clippedRenderTiles.Top; j < clippedRenderTiles.Bottom; j++)
			{
				List<Tile> list = tiles[i, j];
				if (list != null)
				{
					for (int k = 0; k < list.Count; k++)
					{
						Tile tile = list[k];
						AnimatedTilesBank.Animation animation = Bank.Animations[tile.AnimationID];
						animation.Frames[(int)tile.Frame % animation.Frames.Length].Draw(position + animation.Offset + new Vector2((float)i + 0.5f, (float)j + 0.5f) * 8f, animation.Origin, color, tile.Scale);
					}
				}
			}
		}
	}
}
