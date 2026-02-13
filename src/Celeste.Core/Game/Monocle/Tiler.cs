using System;

namespace Monocle;

public static class Tiler
{
	public enum EdgeBehavior
	{
		True,
		False,
		Wrap
	}

	public static int TileX { get; private set; }

	public static int TileY { get; private set; }

	public static bool Left { get; private set; }

	public static bool Right { get; private set; }

	public static bool Up { get; private set; }

	public static bool Down { get; private set; }

	public static bool UpLeft { get; private set; }

	public static bool UpRight { get; private set; }

	public static bool DownLeft { get; private set; }

	public static bool DownRight { get; private set; }

	public static int[,] Tile(bool[,] bits, Func<int> tileDecider, Action<int> tileOutput, int tileWidth, int tileHeight, EdgeBehavior edges)
	{
		int length = bits.GetLength(0);
		int length2 = bits.GetLength(1);
		int[,] array = new int[length, length2];
		for (TileX = 0; TileX < length; TileX++)
		{
			for (TileY = 0; TileY < length2; TileY++)
			{
				if (bits[TileX, TileY])
				{
					switch (edges)
					{
					case EdgeBehavior.True:
						Left = TileX == 0 || bits[TileX - 1, TileY];
						Right = TileX == length - 1 || bits[TileX + 1, TileY];
						Up = TileY == 0 || bits[TileX, TileY - 1];
						Down = TileY == length2 - 1 || bits[TileX, TileY + 1];
						UpLeft = TileX == 0 || TileY == 0 || bits[TileX - 1, TileY - 1];
						UpRight = TileX == length - 1 || TileY == 0 || bits[TileX + 1, TileY - 1];
						DownLeft = TileX == 0 || TileY == length2 - 1 || bits[TileX - 1, TileY + 1];
						DownRight = TileX == length - 1 || TileY == length2 - 1 || bits[TileX + 1, TileY + 1];
						break;
					case EdgeBehavior.False:
						Left = TileX != 0 && bits[TileX - 1, TileY];
						Right = TileX != length - 1 && bits[TileX + 1, TileY];
						Up = TileY != 0 && bits[TileX, TileY - 1];
						Down = TileY != length2 - 1 && bits[TileX, TileY + 1];
						UpLeft = TileX != 0 && TileY != 0 && bits[TileX - 1, TileY - 1];
						UpRight = TileX != length - 1 && TileY != 0 && bits[TileX + 1, TileY - 1];
						DownLeft = TileX != 0 && TileY != length2 - 1 && bits[TileX - 1, TileY + 1];
						DownRight = TileX != length - 1 && TileY != length2 - 1 && bits[TileX + 1, TileY + 1];
						break;
					case EdgeBehavior.Wrap:
						Left = bits[(TileX + length - 1) % length, TileY];
						Right = bits[(TileX + 1) % length, TileY];
						Up = bits[TileX, (TileY + length2 - 1) % length2];
						Down = bits[TileX, (TileY + 1) % length2];
						UpLeft = bits[(TileX + length - 1) % length, (TileY + length2 - 1) % length2];
						UpRight = bits[(TileX + 1) % length, (TileY + length2 - 1) % length2];
						DownLeft = bits[(TileX + length - 1) % length, (TileY + 1) % length2];
						DownRight = bits[(TileX + 1) % length, (TileY + 1) % length2];
						break;
					}
					int num = tileDecider();
					tileOutput(num);
					array[TileX, TileY] = num;
				}
			}
		}
		return array;
	}

	public static int[,] Tile(bool[,] bits, bool[,] mask, Func<int> tileDecider, Action<int> tileOutput, int tileWidth, int tileHeight, EdgeBehavior edges)
	{
		int length = bits.GetLength(0);
		int length2 = bits.GetLength(1);
		int[,] array = new int[length, length2];
		for (TileX = 0; TileX < length; TileX++)
		{
			for (TileY = 0; TileY < length2; TileY++)
			{
				if (bits[TileX, TileY])
				{
					switch (edges)
					{
					case EdgeBehavior.True:
						Left = TileX == 0 || bits[TileX - 1, TileY] || mask[TileX - 1, TileY];
						Right = TileX == length - 1 || bits[TileX + 1, TileY] || mask[TileX + 1, TileY];
						Up = TileY == 0 || bits[TileX, TileY - 1] || mask[TileX, TileY - 1];
						Down = TileY == length2 - 1 || bits[TileX, TileY + 1] || mask[TileX, TileY + 1];
						UpLeft = TileX == 0 || TileY == 0 || bits[TileX - 1, TileY - 1] || mask[TileX - 1, TileY - 1];
						UpRight = TileX == length - 1 || TileY == 0 || bits[TileX + 1, TileY - 1] || mask[TileX + 1, TileY - 1];
						DownLeft = TileX == 0 || TileY == length2 - 1 || bits[TileX - 1, TileY + 1] || mask[TileX - 1, TileY + 1];
						DownRight = TileX == length - 1 || TileY == length2 - 1 || bits[TileX + 1, TileY + 1] || mask[TileX + 1, TileY + 1];
						break;
					case EdgeBehavior.False:
						Left = TileX != 0 && (bits[TileX - 1, TileY] || mask[TileX - 1, TileY]);
						Right = TileX != length - 1 && (bits[TileX + 1, TileY] || mask[TileX + 1, TileY]);
						Up = TileY != 0 && (bits[TileX, TileY - 1] || mask[TileX, TileY - 1]);
						Down = TileY != length2 - 1 && (bits[TileX, TileY + 1] || mask[TileX, TileY + 1]);
						UpLeft = TileX != 0 && TileY != 0 && (bits[TileX - 1, TileY - 1] || mask[TileX - 1, TileY - 1]);
						UpRight = TileX != length - 1 && TileY != 0 && (bits[TileX + 1, TileY - 1] || mask[TileX + 1, TileY - 1]);
						DownLeft = TileX != 0 && TileY != length2 - 1 && (bits[TileX - 1, TileY + 1] || mask[TileX - 1, TileY + 1]);
						DownRight = TileX != length - 1 && TileY != length2 - 1 && (bits[TileX + 1, TileY + 1] || mask[TileX + 1, TileY + 1]);
						break;
					case EdgeBehavior.Wrap:
						Left = bits[(TileX + length - 1) % length, TileY] || mask[(TileX + length - 1) % length, TileY];
						Right = bits[(TileX + 1) % length, TileY] || mask[(TileX + 1) % length, TileY];
						Up = bits[TileX, (TileY + length2 - 1) % length2] || mask[TileX, (TileY + length2 - 1) % length2];
						Down = bits[TileX, (TileY + 1) % length2] || mask[TileX, (TileY + 1) % length2];
						UpLeft = bits[(TileX + length - 1) % length, (TileY + length2 - 1) % length2] || mask[(TileX + length - 1) % length, (TileY + length2 - 1) % length2];
						UpRight = bits[(TileX + 1) % length, (TileY + length2 - 1) % length2] || mask[(TileX + 1) % length, (TileY + length2 - 1) % length2];
						DownLeft = bits[(TileX + length - 1) % length, (TileY + 1) % length2] || mask[(TileX + length - 1) % length, (TileY + 1) % length2];
						DownRight = bits[(TileX + 1) % length, (TileY + 1) % length2] || mask[(TileX + 1) % length, (TileY + 1) % length2];
						break;
					}
					int num = tileDecider();
					tileOutput(num);
					array[TileX, TileY] = num;
				}
			}
		}
		return array;
	}

	public static int[,] Tile(bool[,] bits, AutotileData autotileData, Action<int> tileOutput, int tileWidth, int tileHeight, EdgeBehavior edges)
	{
		return Tile(bits, autotileData.TileHandler, tileOutput, tileWidth, tileHeight, edges);
	}

	public static int[,] Tile(bool[,] bits, bool[,] mask, AutotileData autotileData, Action<int> tileOutput, int tileWidth, int tileHeight, EdgeBehavior edges)
	{
		return Tile(bits, mask, autotileData.TileHandler, tileOutput, tileWidth, tileHeight, edges);
	}
}
