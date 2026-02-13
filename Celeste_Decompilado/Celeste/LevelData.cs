using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Celeste;

public class LevelData
{
	public string Name;

	public bool Dummy;

	public int Strawberries;

	public bool HasGem;

	public bool HasHeartGem;

	public bool HasCheckpoint;

	public bool DisableDownTransition;

	public Rectangle Bounds;

	public List<EntityData> Entities;

	public List<EntityData> Triggers;

	public List<Vector2> Spawns;

	public List<DecalData> FgDecals;

	public List<DecalData> BgDecals;

	public string Solids = "";

	public string Bg = "";

	public string FgTiles = "";

	public string BgTiles = "";

	public string ObjTiles = "";

	public WindController.Patterns WindPattern;

	public Vector2 CameraOffset;

	public bool Dark;

	public bool Underwater;

	public bool Space;

	public string Music = "";

	public string AltMusic = "";

	public string Ambience = "";

	public float[] MusicLayers = new float[4];

	public int MusicProgress = -1;

	public int AmbienceProgress = -1;

	public bool MusicWhispers;

	public bool DelayAltMusic;

	public int EnforceDashNumber;

	public int EditorColorIndex;

	public Rectangle TileBounds => new Rectangle(Bounds.X / 8, Bounds.Y / 8, (int)Math.Ceiling((float)Bounds.Width / 8f), (int)Math.Ceiling((float)Bounds.Height / 8f));

	public Vector2 Position
	{
		get
		{
			return new Vector2(Bounds.X, Bounds.Y);
		}
		set
		{
			for (int i = 0; i < Spawns.Count; i++)
			{
				Spawns[i] -= Position;
			}
			Bounds.X = (int)value.X;
			Bounds.Y = (int)value.Y;
			for (int j = 0; j < Spawns.Count; j++)
			{
				Spawns[j] += Position;
			}
		}
	}

	public int LoadSeed
	{
		get
		{
			int num = 0;
			string name = Name;
			foreach (char c in name)
			{
				num += c;
			}
			return num;
		}
	}

	public LevelData(BinaryPacker.Element data)
	{
		Bounds = default(Rectangle);
		foreach (KeyValuePair<string, object> attribute in data.Attributes)
		{
			switch (attribute.Key)
			{
			case "name":
				Name = attribute.Value.ToString().Substring(4);
				break;
			case "x":
				Bounds.X = (int)attribute.Value;
				break;
			case "y":
				Bounds.Y = (int)attribute.Value;
				break;
			case "width":
				Bounds.Width = (int)attribute.Value;
				break;
			case "height":
				Bounds.Height = (int)attribute.Value;
				if (Bounds.Height == 184)
				{
					Bounds.Height = 180;
				}
				break;
			case "c":
				EditorColorIndex = (int)attribute.Value;
				break;
			case "music":
				Music = (string)attribute.Value;
				break;
			case "alt_music":
				AltMusic = (string)attribute.Value;
				break;
			case "ambience":
				Ambience = (string)attribute.Value;
				break;
			case "windPattern":
				WindPattern = (WindController.Patterns)Enum.Parse(typeof(WindController.Patterns), (string)attribute.Value);
				break;
			case "dark":
				Dark = (bool)attribute.Value;
				break;
			case "underwater":
				Underwater = (bool)attribute.Value;
				break;
			case "space":
				Space = (bool)attribute.Value;
				break;
			case "cameraOffsetX":
				CameraOffset.X = Convert.ToSingle(attribute.Value, CultureInfo.InvariantCulture);
				break;
			case "cameraOffsetY":
				CameraOffset.Y = Convert.ToSingle(attribute.Value, CultureInfo.InvariantCulture);
				break;
			case "musicLayer1":
				MusicLayers[0] = (((bool)attribute.Value) ? 1f : 0f);
				break;
			case "musicLayer2":
				MusicLayers[1] = (((bool)attribute.Value) ? 1f : 0f);
				break;
			case "musicLayer3":
				MusicLayers[2] = (((bool)attribute.Value) ? 1f : 0f);
				break;
			case "musicLayer4":
				MusicLayers[3] = (((bool)attribute.Value) ? 1f : 0f);
				break;
			case "musicProgress":
			{
				string text2 = attribute.Value.ToString();
				if (string.IsNullOrEmpty(text2) || !int.TryParse(text2, out MusicProgress))
				{
					MusicProgress = -1;
				}
				break;
			}
			case "ambienceProgress":
			{
				string text = attribute.Value.ToString();
				if (string.IsNullOrEmpty(text) || !int.TryParse(text, out AmbienceProgress))
				{
					AmbienceProgress = -1;
				}
				break;
			}
			case "whisper":
				MusicWhispers = (bool)attribute.Value;
				break;
			case "disableDownTransition":
				DisableDownTransition = (bool)attribute.Value;
				break;
			case "delayAltMusicFade":
				DelayAltMusic = (bool)attribute.Value;
				break;
			case "enforceDashNumber":
				EnforceDashNumber = (int)attribute.Value;
				break;
			}
		}
		Spawns = new List<Vector2>();
		Entities = new List<EntityData>();
		Triggers = new List<EntityData>();
		BgDecals = new List<DecalData>();
		FgDecals = new List<DecalData>();
		foreach (BinaryPacker.Element child in data.Children)
		{
			if (child.Name == "entities")
			{
				if (child.Children == null)
				{
					continue;
				}
				foreach (BinaryPacker.Element child2 in child.Children)
				{
					if (child2.Name == "player")
					{
						Spawns.Add(new Vector2((float)Bounds.X + Convert.ToSingle(child2.Attributes["x"], CultureInfo.InvariantCulture), (float)Bounds.Y + Convert.ToSingle(child2.Attributes["y"], CultureInfo.InvariantCulture)));
					}
					else if (child2.Name == "strawberry" || child2.Name == "snowberry")
					{
						Strawberries++;
					}
					else if (child2.Name == "shard")
					{
						HasGem = true;
					}
					else if (child2.Name == "blackGem")
					{
						HasHeartGem = true;
					}
					else if (child2.Name == "checkpoint")
					{
						HasCheckpoint = true;
					}
					if (!child2.Name.Equals("player"))
					{
						Entities.Add(CreateEntityData(child2));
					}
				}
			}
			else if (child.Name == "triggers")
			{
				if (child.Children == null)
				{
					continue;
				}
				foreach (BinaryPacker.Element child3 in child.Children)
				{
					Triggers.Add(CreateEntityData(child3));
				}
			}
			else if (child.Name == "bgdecals")
			{
				if (child.Children == null)
				{
					continue;
				}
				foreach (BinaryPacker.Element child4 in child.Children)
				{
					BgDecals.Add(new DecalData
					{
						Position = new Vector2(Convert.ToSingle(child4.Attributes["x"], CultureInfo.InvariantCulture), Convert.ToSingle(child4.Attributes["y"], CultureInfo.InvariantCulture)),
						Scale = new Vector2(Convert.ToSingle(child4.Attributes["scaleX"], CultureInfo.InvariantCulture), Convert.ToSingle(child4.Attributes["scaleY"], CultureInfo.InvariantCulture)),
						Texture = (string)child4.Attributes["texture"]
					});
				}
			}
			else if (child.Name == "fgdecals")
			{
				if (child.Children == null)
				{
					continue;
				}
				foreach (BinaryPacker.Element child5 in child.Children)
				{
					FgDecals.Add(new DecalData
					{
						Position = new Vector2(Convert.ToSingle(child5.Attributes["x"], CultureInfo.InvariantCulture), Convert.ToSingle(child5.Attributes["y"], CultureInfo.InvariantCulture)),
						Scale = new Vector2(Convert.ToSingle(child5.Attributes["scaleX"], CultureInfo.InvariantCulture), Convert.ToSingle(child5.Attributes["scaleY"], CultureInfo.InvariantCulture)),
						Texture = (string)child5.Attributes["texture"]
					});
				}
			}
			else if (child.Name == "solids")
			{
				Solids = child.Attr("innerText");
			}
			else if (child.Name == "bg")
			{
				Bg = child.Attr("innerText");
			}
			else if (child.Name == "fgtiles")
			{
				FgTiles = child.Attr("innerText");
			}
			else if (child.Name == "bgtiles")
			{
				BgTiles = child.Attr("innerText");
			}
			else if (child.Name == "objtiles")
			{
				ObjTiles = child.Attr("innerText");
			}
		}
		Dummy = Spawns.Count <= 0;
	}

	private EntityData CreateEntityData(BinaryPacker.Element entity)
	{
		EntityData entityData = new EntityData();
		entityData.Name = entity.Name;
		entityData.Level = this;
		if (entity.Attributes != null)
		{
			foreach (KeyValuePair<string, object> attribute in entity.Attributes)
			{
				if (attribute.Key == "id")
				{
					entityData.ID = (int)attribute.Value;
					continue;
				}
				if (attribute.Key == "x")
				{
					entityData.Position.X = Convert.ToSingle(attribute.Value, CultureInfo.InvariantCulture);
					continue;
				}
				if (attribute.Key == "y")
				{
					entityData.Position.Y = Convert.ToSingle(attribute.Value, CultureInfo.InvariantCulture);
					continue;
				}
				if (attribute.Key == "width")
				{
					entityData.Width = (int)attribute.Value;
					continue;
				}
				if (attribute.Key == "height")
				{
					entityData.Height = (int)attribute.Value;
					continue;
				}
				if (attribute.Key == "originX")
				{
					entityData.Origin.X = Convert.ToSingle(attribute.Value, CultureInfo.InvariantCulture);
					continue;
				}
				if (attribute.Key == "originY")
				{
					entityData.Origin.Y = Convert.ToSingle(attribute.Value, CultureInfo.InvariantCulture);
					continue;
				}
				if (entityData.Values == null)
				{
					entityData.Values = new Dictionary<string, object>();
				}
				entityData.Values.Add(attribute.Key, attribute.Value);
			}
		}
		entityData.Nodes = new Vector2[(entity.Children != null) ? entity.Children.Count : 0];
		for (int i = 0; i < entityData.Nodes.Length; i++)
		{
			foreach (KeyValuePair<string, object> attribute2 in entity.Children[i].Attributes)
			{
				if (attribute2.Key == "x")
				{
					entityData.Nodes[i].X = Convert.ToSingle(attribute2.Value, CultureInfo.InvariantCulture);
				}
				else if (attribute2.Key == "y")
				{
					entityData.Nodes[i].Y = Convert.ToSingle(attribute2.Value, CultureInfo.InvariantCulture);
				}
			}
		}
		return entityData;
	}

	public bool Check(Vector2 at)
	{
		if (at.X >= (float)Bounds.Left && at.Y >= (float)Bounds.Top && at.X < (float)Bounds.Right)
		{
			return at.Y < (float)Bounds.Bottom;
		}
		return false;
	}
}
