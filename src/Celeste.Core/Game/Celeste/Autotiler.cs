using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Autotiler
{
	private class TerrainType
	{
		public char ID;

		public HashSet<char> Ignores = new HashSet<char>();

		public List<Masked> Masked = new List<Masked>();

		public Tiles Center = new Tiles();

		public Tiles Padded = new Tiles();

		public TerrainType(char id)
		{
			ID = id;
		}

		public bool Ignore(char c)
		{
			if (ID != c)
			{
				if (!Ignores.Contains(c))
				{
					return Ignores.Contains('*');
				}
				return true;
			}
			return false;
		}
	}

	private class Masked
	{
		public byte[] Mask = new byte[9];

		public Tiles Tiles = new Tiles();
	}

	private class Tiles
	{
		public List<MTexture> Textures = new List<MTexture>();

		public List<string> OverlapSprites = new List<string>();

		public bool HasOverlays;
	}

	public struct Generated
	{
		public TileGrid TileGrid;

		public AnimatedTiles SpriteOverlay;
	}

	public struct Behaviour
	{
		public bool PaddingIgnoreOutOfLevel;

		public bool EdgesIgnoreOutOfLevel;

		public bool EdgesExtend;
	}

	public List<Rectangle> LevelBounds = new List<Rectangle>();

	private Dictionary<char, TerrainType> lookup = new Dictionary<char, TerrainType>();

	private byte[] adjacent = new byte[9];

	public Autotiler(string filename)
	{
		Dictionary<char, XmlElement> dictionary = new Dictionary<char, XmlElement>();
		foreach (XmlElement item in Calc.LoadContentXML(filename).GetElementsByTagName("Tileset"))
		{
			char c = item.AttrChar("id");
			Tileset tileset = new Tileset(GFX.Game["tilesets/" + item.Attr("path")], 8, 8);
			TerrainType terrainType = new TerrainType(c);
			ReadInto(terrainType, tileset, item);
			if (item.HasAttr("copy"))
			{
				char key = item.AttrChar("copy");
				if (!dictionary.ContainsKey(key))
				{
					throw new Exception("Copied tilesets must be defined before the tilesets that copy them!");
				}
				ReadInto(terrainType, tileset, dictionary[key]);
			}
			if (item.HasAttr("ignores"))
			{
				string[] array = item.Attr("ignores").Split(',');
				foreach (string text in array)
				{
					if (text.Length > 0)
					{
						terrainType.Ignores.Add(text[0]);
					}
				}
			}
			dictionary.Add(c, item);
			lookup.Add(c, terrainType);
		}
	}

	private void ReadInto(TerrainType data, Tileset tileset, XmlElement xml)
	{
		foreach (object item3 in xml)
		{
			if (item3 is XmlComment)
			{
				continue;
			}
			XmlElement xml2 = item3 as XmlElement;
			string text = xml2.Attr("mask");
			Tiles tiles;
			if (text == "center")
			{
				tiles = data.Center;
			}
			else if (text == "padding")
			{
				tiles = data.Padded;
			}
			else
			{
				Masked masked = new Masked();
				tiles = masked.Tiles;
				int i = 0;
				int num = 0;
				for (; i < text.Length; i++)
				{
					if (text[i] == '0')
					{
						masked.Mask[num++] = 0;
					}
					else if (text[i] == '1')
					{
						masked.Mask[num++] = 1;
					}
					else if (text[i] == 'x' || text[i] == 'X')
					{
						masked.Mask[num++] = 2;
					}
				}
				data.Masked.Add(masked);
			}
			string[] array = xml2.Attr("tiles").Split(';');
			for (int j = 0; j < array.Length; j++)
			{
				string[] array2 = array[j].Split(',');
				int x = int.Parse(array2[0]);
				int y = int.Parse(array2[1]);
				MTexture item = tileset[x, y];
				tiles.Textures.Add(item);
			}
			if (xml2.HasAttr("sprites"))
			{
				array = xml2.Attr("sprites").Split(',');
				foreach (string item2 in array)
				{
					tiles.OverlapSprites.Add(item2);
				}
				tiles.HasOverlays = true;
			}
		}
		data.Masked.Sort(delegate(Masked a, Masked b)
		{
			int num2 = 0;
			int num3 = 0;
			for (int k = 0; k < 9; k++)
			{
				if (a.Mask[k] == 2)
				{
					num2++;
				}
				if (b.Mask[k] == 2)
				{
					num3++;
				}
			}
			return num2 - num3;
		});
	}

	public Generated GenerateMap(VirtualMap<char> mapData, bool paddingIgnoreOutOfLevel)
	{
		Behaviour behaviour = new Behaviour
		{
			EdgesExtend = true,
			EdgesIgnoreOutOfLevel = false,
			PaddingIgnoreOutOfLevel = paddingIgnoreOutOfLevel
		};
		return Generate(mapData, 0, 0, mapData.Columns, mapData.Rows, forceSolid: false, '0', behaviour);
	}

	public Generated GenerateMap(VirtualMap<char> mapData, Behaviour behaviour)
	{
		return Generate(mapData, 0, 0, mapData.Columns, mapData.Rows, forceSolid: false, '0', behaviour);
	}

	public Generated GenerateBox(char id, int tilesX, int tilesY)
	{
		return Generate(null, 0, 0, tilesX, tilesY, forceSolid: true, id, default(Behaviour));
	}

	public Generated GenerateOverlay(char id, int x, int y, int tilesX, int tilesY, VirtualMap<char> mapData)
	{
		Behaviour behaviour = new Behaviour
		{
			EdgesExtend = true,
			EdgesIgnoreOutOfLevel = true,
			PaddingIgnoreOutOfLevel = true
		};
		return Generate(mapData, x, y, tilesX, tilesY, forceSolid: true, id, behaviour);
	}

	private Generated Generate(VirtualMap<char> mapData, int startX, int startY, int tilesX, int tilesY, bool forceSolid, char forceID, Behaviour behaviour)
	{
		TileGrid tileGrid = new TileGrid(8, 8, tilesX, tilesY);
		AnimatedTiles animatedTiles = new AnimatedTiles(tilesX, tilesY, GFX.AnimatedTilesBank);
		Rectangle forceFill = Rectangle.Empty;
		if (forceSolid)
		{
			forceFill = new Rectangle(startX, startY, tilesX, tilesY);
		}
		if (mapData != null)
		{
			for (int i = startX; i < startX + tilesX; i += 50)
			{
				for (int j = startY; j < startY + tilesY; j += 50)
				{
					if (!mapData.AnyInSegmentAtTile(i, j))
					{
						j = j / 50 * 50;
						continue;
					}
					int k = i;
					for (int num = Math.Min(i + 50, startX + tilesX); k < num; k++)
					{
						int l = j;
						for (int num2 = Math.Min(j + 50, startY + tilesY); l < num2; l++)
						{
							Tiles tiles = TileHandler(mapData, k, l, forceFill, forceID, behaviour);
							if (tiles != null)
							{
								tileGrid.Tiles[k - startX, l - startY] = Calc.Random.Choose(tiles.Textures);
								if (tiles.HasOverlays)
								{
									animatedTiles.Set(k - startX, l - startY, Calc.Random.Choose(tiles.OverlapSprites));
								}
							}
						}
					}
				}
			}
		}
		else
		{
			for (int m = startX; m < startX + tilesX; m++)
			{
				for (int n = startY; n < startY + tilesY; n++)
				{
					Tiles tiles2 = TileHandler(null, m, n, forceFill, forceID, behaviour);
					if (tiles2 != null)
					{
						tileGrid.Tiles[m - startX, n - startY] = Calc.Random.Choose(tiles2.Textures);
						if (tiles2.HasOverlays)
						{
							animatedTiles.Set(m - startX, n - startY, Calc.Random.Choose(tiles2.OverlapSprites));
						}
					}
				}
			}
		}
		return new Generated
		{
			TileGrid = tileGrid,
			SpriteOverlay = animatedTiles
		};
	}

	private Tiles TileHandler(VirtualMap<char> mapData, int x, int y, Rectangle forceFill, char forceID, Behaviour behaviour)
	{
		char tile = GetTile(mapData, x, y, forceFill, forceID, behaviour);
		if (IsEmpty(tile))
		{
			return null;
		}
		TerrainType terrainType = lookup[tile];
		bool flag = true;
		int num = 0;
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				bool flag2 = CheckTile(terrainType, mapData, x + j, y + i, forceFill, behaviour);
				if (!flag2 && behaviour.EdgesIgnoreOutOfLevel && !CheckForSameLevel(x, y, x + j, y + i))
				{
					flag2 = true;
				}
				adjacent[num++] = (byte)(flag2 ? 1u : 0u);
				if (!flag2)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			bool flag3 = false;
			if (behaviour.PaddingIgnoreOutOfLevel ? ((!CheckTile(terrainType, mapData, x - 2, y, forceFill, behaviour) && CheckForSameLevel(x, y, x - 2, y)) || (!CheckTile(terrainType, mapData, x + 2, y, forceFill, behaviour) && CheckForSameLevel(x, y, x + 2, y)) || (!CheckTile(terrainType, mapData, x, y - 2, forceFill, behaviour) && CheckForSameLevel(x, y, x, y - 2)) || (!CheckTile(terrainType, mapData, x, y + 2, forceFill, behaviour) && CheckForSameLevel(x, y, x, y + 2))) : (!CheckTile(terrainType, mapData, x - 2, y, forceFill, behaviour) || !CheckTile(terrainType, mapData, x + 2, y, forceFill, behaviour) || !CheckTile(terrainType, mapData, x, y - 2, forceFill, behaviour) || !CheckTile(terrainType, mapData, x, y + 2, forceFill, behaviour)))
			{
				return lookup[tile].Padded;
			}
			return lookup[tile].Center;
		}
		foreach (Masked item in terrainType.Masked)
		{
			bool flag4 = true;
			for (int k = 0; k < 9 && flag4; k++)
			{
				if (item.Mask[k] != 2 && item.Mask[k] != adjacent[k])
				{
					flag4 = false;
				}
			}
			if (flag4)
			{
				return item.Tiles;
			}
		}
		return null;
	}

	private bool CheckForSameLevel(int x1, int y1, int x2, int y2)
	{
		foreach (Rectangle levelBound in LevelBounds)
		{
			if (levelBound.Contains(x1, y1) && levelBound.Contains(x2, y2))
			{
				return true;
			}
		}
		return false;
	}

	private bool CheckTile(TerrainType set, VirtualMap<char> mapData, int x, int y, Rectangle forceFill, Behaviour behaviour)
	{
		if (forceFill.Contains(x, y))
		{
			return true;
		}
		if (mapData == null)
		{
			return behaviour.EdgesExtend;
		}
		if (x < 0 || y < 0 || x >= mapData.Columns || y >= mapData.Rows)
		{
			if (!behaviour.EdgesExtend)
			{
				return false;
			}
			char c = mapData[Calc.Clamp(x, 0, mapData.Columns - 1), Calc.Clamp(y, 0, mapData.Rows - 1)];
			if (!IsEmpty(c))
			{
				return !set.Ignore(c);
			}
			return false;
		}
		char c2 = mapData[x, y];
		if (!IsEmpty(c2))
		{
			return !set.Ignore(c2);
		}
		return false;
	}

	private char GetTile(VirtualMap<char> mapData, int x, int y, Rectangle forceFill, char forceID, Behaviour behaviour)
	{
		if (forceFill.Contains(x, y))
		{
			return forceID;
		}
		if (mapData == null)
		{
			if (!behaviour.EdgesExtend)
			{
				return '0';
			}
			return forceID;
		}
		if (x < 0 || y < 0 || x >= mapData.Columns || y >= mapData.Rows)
		{
			if (!behaviour.EdgesExtend)
			{
				return '0';
			}
			int x2 = Calc.Clamp(x, 0, mapData.Columns - 1);
			int y2 = Calc.Clamp(y, 0, mapData.Rows - 1);
			return mapData[x2, y2];
		}
		return mapData[x, y];
	}

	private bool IsEmpty(char id)
	{
		if (id != '0')
		{
			return id == '\0';
		}
		return true;
	}
}
