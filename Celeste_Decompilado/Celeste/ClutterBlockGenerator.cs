using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public static class ClutterBlockGenerator
{
	private struct Tile
	{
		public int Color;

		public bool Wall;

		public ClutterBlock Block;

		public bool Empty
		{
			get
			{
				if (!Wall)
				{
					return Color == -1;
				}
				return false;
			}
		}
	}

	private class TextureSet
	{
		public int Columns;

		public int Rows;

		public List<MTexture> textures = new List<MTexture>();
	}

	private static Level level;

	private static Tile[,] tiles;

	private static List<Point> active = new List<Point>();

	private static List<List<TextureSet>> textures;

	private static int columns;

	private static int rows;

	private static bool[] enabled = new bool[3];

	private static bool initialized;

	public static void Init(Level lvl)
	{
		if (initialized)
		{
			return;
		}
		initialized = true;
		level = lvl;
		columns = level.Bounds.Width / 8;
		rows = level.Bounds.Height / 8 + 1;
		if (tiles == null)
		{
			tiles = new Tile[200, 200];
		}
		for (int i = 0; i < columns; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				tiles[i, j].Color = -1;
				tiles[i, j].Block = null;
			}
		}
		for (int k = 0; k < enabled.Length; k++)
		{
			enabled[k] = !level.Session.GetFlag("oshiro_clutter_cleared_" + k);
		}
		if (textures == null)
		{
			textures = new List<List<TextureSet>>();
			for (int l = 0; l < 3; l++)
			{
				List<TextureSet> list = new List<TextureSet>();
				Atlas game = GFX.Game;
				ClutterBlock.Colors colors = (ClutterBlock.Colors)l;
				foreach (MTexture atlasSubtexture in game.GetAtlasSubtextures("objects/resortclutter/" + colors.ToString() + "_"))
				{
					int num = atlasSubtexture.Width / 8;
					int num2 = atlasSubtexture.Height / 8;
					TextureSet textureSet = null;
					foreach (TextureSet item in list)
					{
						if (item.Columns == num && item.Rows == num2)
						{
							textureSet = item;
							break;
						}
					}
					if (textureSet == null)
					{
						TextureSet obj = new TextureSet
						{
							Columns = num,
							Rows = num2
						};
						textureSet = obj;
						list.Add(obj);
					}
					textureSet.textures.Add(atlasSubtexture);
				}
				list.Sort((TextureSet a, TextureSet b) => -Math.Sign(a.Columns * a.Rows - b.Columns * b.Rows));
				textures.Add(list);
			}
		}
		Point levelSolidOffset = level.LevelSolidOffset;
		for (int num3 = 0; num3 < columns; num3++)
		{
			for (int num4 = 0; num4 < rows; num4++)
			{
				tiles[num3, num4].Wall = level.SolidsData[levelSolidOffset.X + num3, levelSolidOffset.Y + num4] != '0';
			}
		}
	}

	public static void Dispose()
	{
		textures = null;
		tiles = null;
		initialized = false;
	}

	public static void Add(int x, int y, int w, int h, ClutterBlock.Colors color)
	{
		level.Add(new ClutterBlockBase(new Vector2(level.Bounds.X, level.Bounds.Y) + new Vector2(x, y) * 8f, w * 8, h * 8, enabled[(int)color], color));
		if (!enabled[(int)color])
		{
			return;
		}
		int i = Math.Max(0, x);
		for (int num = Math.Min(columns, x + w); i < num; i++)
		{
			int j = Math.Max(0, y);
			for (int num2 = Math.Min(rows, y + h); j < num2; j++)
			{
				Point item = new Point(i, j);
				tiles[item.X, item.Y].Color = (int)color;
				active.Add(item);
			}
		}
	}

	public static void Generate()
	{
		if (!initialized)
		{
			return;
		}
		active.Shuffle();
		List<ClutterBlock> list = new List<ClutterBlock>();
		Rectangle bounds = level.Bounds;
		foreach (Point item in active)
		{
			if (tiles[item.X, item.Y].Block != null)
			{
				continue;
			}
			int num = 0;
			int color;
			TextureSet textureSet;
			while (true)
			{
				color = tiles[item.X, item.Y].Color;
				textureSet = textures[color][num];
				bool flag = true;
				if (item.X + textureSet.Columns <= columns && item.Y + textureSet.Rows <= rows)
				{
					int num2 = item.X;
					int num3 = item.X + textureSet.Columns;
					while (flag && num2 < num3)
					{
						int num4 = item.Y;
						int num5 = item.Y + textureSet.Rows;
						while (flag && num4 < num5)
						{
							Tile tile = tiles[num2, num4];
							if (tile.Block != null || tile.Color != color)
							{
								flag = false;
							}
							num4++;
						}
						num2++;
					}
					if (flag)
					{
						break;
					}
				}
				num++;
			}
			ClutterBlock clutterBlock = new ClutterBlock(new Vector2(bounds.X, bounds.Y) + new Vector2(item.X, item.Y) * 8f, Calc.Random.Choose(textureSet.textures), (ClutterBlock.Colors)color);
			for (int i = item.X; i < item.X + textureSet.Columns; i++)
			{
				for (int j = item.Y; j < item.Y + textureSet.Rows; j++)
				{
					tiles[i, j].Block = clutterBlock;
				}
			}
			list.Add(clutterBlock);
			level.Add(clutterBlock);
		}
		for (int k = 0; k < columns; k++)
		{
			for (int l = 0; l < rows; l++)
			{
				Tile tile2 = tiles[k, l];
				if (tile2.Block == null)
				{
					continue;
				}
				ClutterBlock block = tile2.Block;
				if (!block.TopSideOpen && (l == 0 || tiles[k, l - 1].Empty))
				{
					block.TopSideOpen = true;
				}
				if (!block.LeftSideOpen && (k == 0 || tiles[k - 1, l].Empty))
				{
					block.LeftSideOpen = true;
				}
				if (!block.RightSideOpen && (k == columns - 1 || tiles[k + 1, l].Empty))
				{
					block.RightSideOpen = true;
				}
				if (!block.OnTheGround && l < rows - 1)
				{
					Tile tile3 = tiles[k, l + 1];
					if (tile3.Wall)
					{
						block.OnTheGround = true;
					}
					else if (tile3.Block != null && tile3.Block != block && !block.HasBelow.Contains(tile3.Block))
					{
						block.HasBelow.Add(tile3.Block);
						block.Below.Add(tile3.Block);
						tile3.Block.Above.Add(block);
					}
				}
			}
		}
		foreach (ClutterBlock item2 in list)
		{
			if (item2.OnTheGround)
			{
				SetAboveToOnGround(item2);
			}
		}
		initialized = false;
		level = null;
		active.Clear();
	}

	private static void SetAboveToOnGround(ClutterBlock block)
	{
		foreach (ClutterBlock item in block.Above)
		{
			if (!item.OnTheGround)
			{
				item.OnTheGround = true;
				SetAboveToOnGround(item);
			}
		}
	}
}
