using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class LevelLoader : Scene
{
	private Session session;

	private Vector2? startPosition;

	private bool started;

	public Player.IntroTypes? PlayerIntroTypeOverride;

	public Level Level { get; private set; }

	public bool Loaded { get; private set; }

	public LevelLoader(Session session, Vector2? startPosition = null)
	{
		this.session = session;
		if (!startPosition.HasValue)
		{
			this.startPosition = session.RespawnPoint;
		}
		else
		{
			this.startPosition = startPosition;
		}
		Level = new Level();
		RunThread.Start(LoadingThread, "LEVEL_LOADER");
	}

	private void LoadingThread()
	{
		MapData mapData = session.MapData;
		AreaData areaData = AreaData.Get(session);
		if (session.Area.ID == 0)
		{
			SaveData.Instance.Assists.DashMode = Assists.DashModes.Normal;
		}
		Level.Add(Level.GameplayRenderer = new GameplayRenderer());
		Level.Add(Level.Lighting = new LightingRenderer());
		Level.Add(Level.Bloom = new BloomRenderer());
		Level.Add(Level.Displacement = new DisplacementRenderer());
		Level.Add(Level.Background = new BackdropRenderer());
		Level.Add(Level.Foreground = new BackdropRenderer());
		Level.Add(new DustEdges());
		Level.Add(new WaterSurface());
		Level.Add(new MirrorSurfaces());
		Level.Add(new GlassBlockBg());
		Level.Add(new LightningRenderer());
		Level.Add(new SeekerBarrierRenderer());
		Level.Add(Level.HudRenderer = new HudRenderer());
		if (session.Area.ID == 9)
		{
			Level.Add(new IceTileOverlay());
		}
		Level.BaseLightingAlpha = (Level.Lighting.Alpha = areaData.DarknessAlpha);
		Level.Bloom.Base = areaData.BloomBase;
		Level.Bloom.Strength = areaData.BloomStrength;
		Level.BackgroundColor = mapData.BackgroundColor;
		Level.Background.Backdrops = mapData.CreateBackdrops(mapData.Background);
		foreach (Backdrop backdrop in Level.Background.Backdrops)
		{
			backdrop.Renderer = Level.Background;
		}
		Level.Foreground.Backdrops = mapData.CreateBackdrops(mapData.Foreground);
		foreach (Backdrop backdrop2 in Level.Foreground.Backdrops)
		{
			backdrop2.Renderer = Level.Foreground;
		}
		Level.RendererList.UpdateLists();
		Level.Add(Level.FormationBackdrop = new FormationBackdrop());
		Level.Camera = Level.GameplayRenderer.Camera;
		Audio.SetCamera(Level.Camera);
		Level.Session = session;
		SaveData.Instance.StartSession(Level.Session);
		Level.Particles = new ParticleSystem(-8000, 400);
		Level.Particles.Tag = Tags.Global;
		Level.Add(Level.Particles);
		Level.ParticlesBG = new ParticleSystem(8000, 400);
		Level.ParticlesBG.Tag = Tags.Global;
		Level.Add(Level.ParticlesBG);
		Level.ParticlesFG = new ParticleSystem(-50000, 800);
		Level.ParticlesFG.Tag = Tags.Global;
		Level.ParticlesFG.Add(new MirrorReflection());
		Level.Add(Level.ParticlesFG);
		Level.Add(Level.strawberriesDisplay = new TotalStrawberriesDisplay());
		Level.Add(new SpeedrunTimerDisplay());
		Level.Add(new GameplayStats());
		Level.Add(new GrabbyIcon());
		Rectangle tileBounds = mapData.TileBounds;
		GFX.FGAutotiler.LevelBounds.Clear();
		VirtualMap<char> virtualMap = new VirtualMap<char>(tileBounds.Width, tileBounds.Height, '0');
		VirtualMap<char> virtualMap2 = new VirtualMap<char>(tileBounds.Width, tileBounds.Height, '0');
		VirtualMap<bool> virtualMap3 = new VirtualMap<bool>(tileBounds.Width, tileBounds.Height, emptyValue: false);
		Regex regex = new Regex("\\r\\n|\\n\\r|\\n|\\r");
		foreach (LevelData level in mapData.Levels)
		{
			int left = level.TileBounds.Left;
			int top = level.TileBounds.Top;
			string[] array = regex.Split(level.Bg);
			for (int i = top; i < top + array.Length; i++)
			{
				for (int j = left; j < left + array[i - top].Length; j++)
				{
					virtualMap[j - tileBounds.X, i - tileBounds.Y] = array[i - top][j - left];
				}
			}
			string[] array2 = regex.Split(level.Solids);
			for (int k = top; k < top + array2.Length; k++)
			{
				for (int l = left; l < left + array2[k - top].Length; l++)
				{
					virtualMap2[l - tileBounds.X, k - tileBounds.Y] = array2[k - top][l - left];
				}
			}
			for (int m = level.TileBounds.Left; m < level.TileBounds.Right; m++)
			{
				for (int n = level.TileBounds.Top; n < level.TileBounds.Bottom; n++)
				{
					virtualMap3[m - tileBounds.Left, n - tileBounds.Top] = true;
				}
			}
			GFX.FGAutotiler.LevelBounds.Add(new Rectangle(level.TileBounds.X - tileBounds.X, level.TileBounds.Y - tileBounds.Y, level.TileBounds.Width, level.TileBounds.Height));
		}
		foreach (Rectangle item in mapData.Filler)
		{
			for (int num = item.Left; num < item.Right; num++)
			{
				for (int num2 = item.Top; num2 < item.Bottom; num2++)
				{
					char c = '0';
					if (item.Top - tileBounds.Y > 0)
					{
						char c2 = virtualMap2[num - tileBounds.X, item.Top - tileBounds.Y - 1];
						if (c2 != '0')
						{
							c = c2;
						}
					}
					if (c == '0' && item.Left - tileBounds.X > 0)
					{
						char c3 = virtualMap2[item.Left - tileBounds.X - 1, num2 - tileBounds.Y];
						if (c3 != '0')
						{
							c = c3;
						}
					}
					if (c == '0' && item.Right - tileBounds.X < tileBounds.Width - 1)
					{
						char c4 = virtualMap2[item.Right - tileBounds.X, num2 - tileBounds.Y];
						if (c4 != '0')
						{
							c = c4;
						}
					}
					if (c == '0' && item.Bottom - tileBounds.Y < tileBounds.Height - 1)
					{
						char c5 = virtualMap2[num - tileBounds.X, item.Bottom - tileBounds.Y];
						if (c5 != '0')
						{
							c = c5;
						}
					}
					if (c == '0')
					{
						c = '1';
					}
					virtualMap2[num - tileBounds.X, num2 - tileBounds.Y] = c;
					virtualMap3[num - tileBounds.X, num2 - tileBounds.Y] = true;
				}
			}
		}
		foreach (LevelData level2 in mapData.Levels)
		{
			for (int num3 = level2.TileBounds.Left; num3 < level2.TileBounds.Right; num3++)
			{
				int top2 = level2.TileBounds.Top;
				char value = virtualMap[num3 - tileBounds.X, top2 - tileBounds.Y];
				for (int num4 = 1; num4 < 4 && !virtualMap3[num3 - tileBounds.X, top2 - tileBounds.Y - num4]; num4++)
				{
					virtualMap[num3 - tileBounds.X, top2 - tileBounds.Y - num4] = value;
				}
				top2 = level2.TileBounds.Bottom - 1;
				char value2 = virtualMap[num3 - tileBounds.X, top2 - tileBounds.Y];
				for (int num5 = 1; num5 < 4 && !virtualMap3[num3 - tileBounds.X, top2 - tileBounds.Y + num5]; num5++)
				{
					virtualMap[num3 - tileBounds.X, top2 - tileBounds.Y + num5] = value2;
				}
			}
			for (int num6 = level2.TileBounds.Top - 4; num6 < level2.TileBounds.Bottom + 4; num6++)
			{
				int left2 = level2.TileBounds.Left;
				char value3 = virtualMap[left2 - tileBounds.X, num6 - tileBounds.Y];
				for (int num7 = 1; num7 < 4 && !virtualMap3[left2 - tileBounds.X - num7, num6 - tileBounds.Y]; num7++)
				{
					virtualMap[left2 - tileBounds.X - num7, num6 - tileBounds.Y] = value3;
				}
				left2 = level2.TileBounds.Right - 1;
				char value4 = virtualMap[left2 - tileBounds.X, num6 - tileBounds.Y];
				for (int num8 = 1; num8 < 4 && !virtualMap3[left2 - tileBounds.X + num8, num6 - tileBounds.Y]; num8++)
				{
					virtualMap[left2 - tileBounds.X + num8, num6 - tileBounds.Y] = value4;
				}
			}
		}
		foreach (LevelData level3 in mapData.Levels)
		{
			for (int num9 = level3.TileBounds.Left; num9 < level3.TileBounds.Right; num9++)
			{
				int top3 = level3.TileBounds.Top;
				if (virtualMap2[num9 - tileBounds.X, top3 - tileBounds.Y] == '0')
				{
					for (int num10 = 1; num10 < 8; num10++)
					{
						virtualMap3[num9 - tileBounds.X, top3 - tileBounds.Y - num10] = true;
					}
				}
				top3 = level3.TileBounds.Bottom - 1;
				if (virtualMap2[num9 - tileBounds.X, top3 - tileBounds.Y] == '0')
				{
					for (int num11 = 1; num11 < 8; num11++)
					{
						virtualMap3[num9 - tileBounds.X, top3 - tileBounds.Y + num11] = true;
					}
				}
			}
		}
		foreach (LevelData level4 in mapData.Levels)
		{
			for (int num12 = level4.TileBounds.Left; num12 < level4.TileBounds.Right; num12++)
			{
				int top4 = level4.TileBounds.Top;
				char value5 = virtualMap2[num12 - tileBounds.X, top4 - tileBounds.Y];
				for (int num13 = 1; num13 < 4 && !virtualMap3[num12 - tileBounds.X, top4 - tileBounds.Y - num13]; num13++)
				{
					virtualMap2[num12 - tileBounds.X, top4 - tileBounds.Y - num13] = value5;
				}
				top4 = level4.TileBounds.Bottom - 1;
				char value6 = virtualMap2[num12 - tileBounds.X, top4 - tileBounds.Y];
				for (int num14 = 1; num14 < 4 && !virtualMap3[num12 - tileBounds.X, top4 - tileBounds.Y + num14]; num14++)
				{
					virtualMap2[num12 - tileBounds.X, top4 - tileBounds.Y + num14] = value6;
				}
			}
			for (int num15 = level4.TileBounds.Top - 4; num15 < level4.TileBounds.Bottom + 4; num15++)
			{
				int left3 = level4.TileBounds.Left;
				char value7 = virtualMap2[left3 - tileBounds.X, num15 - tileBounds.Y];
				for (int num16 = 1; num16 < 4 && !virtualMap3[left3 - tileBounds.X - num16, num15 - tileBounds.Y]; num16++)
				{
					virtualMap2[left3 - tileBounds.X - num16, num15 - tileBounds.Y] = value7;
				}
				left3 = level4.TileBounds.Right - 1;
				char value8 = virtualMap2[left3 - tileBounds.X, num15 - tileBounds.Y];
				for (int num17 = 1; num17 < 4 && !virtualMap3[left3 - tileBounds.X + num17, num15 - tileBounds.Y]; num17++)
				{
					virtualMap2[left3 - tileBounds.X + num17, num15 - tileBounds.Y] = value8;
				}
			}
		}
		Vector2 position = new Vector2(tileBounds.X, tileBounds.Y) * 8f;
		Calc.PushRandom(mapData.LoadSeed);
		BackgroundTiles backgroundTiles = null;
		SolidTiles solidTiles = null;
		Level.Add(Level.BgTiles = (backgroundTiles = new BackgroundTiles(position, virtualMap)));
		Level.Add(Level.SolidTiles = (solidTiles = new SolidTiles(position, virtualMap2)));
		Level.BgData = virtualMap;
		Level.SolidsData = virtualMap2;
		Calc.PopRandom();
		new Entity(position).Add(Level.FgTilesLightMask = new TileGrid(8, 8, tileBounds.Width, tileBounds.Height));
		Level.FgTilesLightMask.Color = Color.Black;
		foreach (LevelData level5 in mapData.Levels)
		{
			int left4 = level5.TileBounds.Left;
			int top5 = level5.TileBounds.Top;
			int width = level5.TileBounds.Width;
			int height = level5.TileBounds.Height;
			if (!string.IsNullOrEmpty(level5.BgTiles))
			{
				int[,] tiles = Calc.ReadCSVIntGrid(level5.BgTiles, width, height);
				backgroundTiles.Tiles.Overlay(GFX.SceneryTiles, tiles, left4 - tileBounds.X, top5 - tileBounds.Y);
			}
			if (!string.IsNullOrEmpty(level5.FgTiles))
			{
				int[,] tiles2 = Calc.ReadCSVIntGrid(level5.FgTiles, width, height);
				solidTiles.Tiles.Overlay(GFX.SceneryTiles, tiles2, left4 - tileBounds.X, top5 - tileBounds.Y);
				Level.FgTilesLightMask.Overlay(GFX.SceneryTiles, tiles2, left4 - tileBounds.X, top5 - tileBounds.Y);
			}
		}
		if (areaData.OnLevelBegin != null)
		{
			areaData.OnLevelBegin(Level);
		}
		Level.StartPosition = startPosition;
		Level.Pathfinder = new Pathfinder(Level);
		Loaded = true;
	}

	private void StartLevel()
	{
		started = true;
		Session session = Level.Session;
		Player.IntroTypes playerIntro = (PlayerIntroTypeOverride.HasValue ? PlayerIntroTypeOverride.Value : ((!session.FirstLevel || !session.StartedFromBeginning || !session.JustStarted) ? Player.IntroTypes.Respawn : ((session.Area.Mode != AreaMode.CSide) ? AreaData.Get(Level).IntroType : Player.IntroTypes.WalkInRight)));
		Level.LoadLevel(playerIntro, isFromLoader: true);
		Level.Session.JustStarted = false;
		if (Engine.Scene == this)
		{
			Engine.Scene = Level;
		}
	}

	public override void Update()
	{
		base.Update();
		if (Loaded && !started)
		{
			StartLevel();
		}
	}
}
