using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class CrumbleWallOnRumble : Solid
{
	private bool permanent;

	private EntityID id;

	private char tileType;

	private bool blendIn;

	public CrumbleWallOnRumble(Vector2 position, char tileType, float width, float height, bool blendIn, bool persistent, EntityID id)
		: base(position, width, height, safe: true)
	{
		base.Depth = -12999;
		this.id = id;
		this.tileType = tileType;
		this.blendIn = blendIn;
		permanent = persistent;
		SurfaceSoundIndex = SurfaceIndex.TileToIndex[this.tileType];
	}

	public CrumbleWallOnRumble(EntityData data, Vector2 offset, EntityID id)
		: this(data.Position + offset, data.Char("tiletype", 'm'), data.Width, data.Height, data.Bool("blendin"), data.Bool("persistent"), id)
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		TileGrid tileGrid;
		if (!blendIn)
		{
			tileGrid = GFX.FGAutotiler.GenerateBox(tileType, (int)base.Width / 8, (int)base.Height / 8).TileGrid;
		}
		else
		{
			Level level = SceneAs<Level>();
			Rectangle tileBounds = level.Session.MapData.TileBounds;
			VirtualMap<char> solidsData = level.SolidsData;
			int x = (int)(base.X / 8f) - tileBounds.Left;
			int y = (int)(base.Y / 8f) - tileBounds.Top;
			int tilesX = (int)base.Width / 8;
			int tilesY = (int)base.Height / 8;
			tileGrid = GFX.FGAutotiler.GenerateOverlay(tileType, x, y, tilesX, tilesY, solidsData).TileGrid;
			base.Depth = -10501;
		}
		Add(tileGrid);
		Add(new TileInterceptor(tileGrid, highPriority: true));
		Add(new LightOcclude());
		if (CollideCheck<Player>())
		{
			RemoveSelf();
		}
	}

	public void Break()
	{
		if (!Collidable || base.Scene == null)
		{
			return;
		}
		Audio.Play("event:/new_content/game/10_farewell/quake_rockbreak", Position);
		Collidable = false;
		for (int i = 0; (float)i < base.Width / 8f; i++)
		{
			for (int j = 0; (float)j < base.Height / 8f; j++)
			{
				if (!base.Scene.CollideCheck<Solid>(new Rectangle((int)base.X + i * 8, (int)base.Y + j * 8, 8, 8)))
				{
					base.Scene.Add(Engine.Pooler.Create<Debris>().Init(Position + new Vector2(4 + i * 8, 4 + j * 8), tileType).BlastFrom(base.TopCenter));
				}
			}
		}
		if (permanent)
		{
			SceneAs<Level>().Session.DoNotLoad.Add(id);
		}
		RemoveSelf();
	}
}
