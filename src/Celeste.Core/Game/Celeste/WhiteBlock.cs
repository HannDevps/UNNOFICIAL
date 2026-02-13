using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WhiteBlock : JumpThru
{
	private const float duckDuration = 3f;

	private float playerDuckTimer;

	private bool enabled = true;

	private bool activated;

	private Image sprite;

	private Entity bgSolidTiles;

	public WhiteBlock(EntityData data, Vector2 offset)
		: base(data.Position + offset, 48, safe: true)
	{
		Add(sprite = new Image(GFX.Game["objects/whiteblock"]));
		base.Depth = 8990;
		SurfaceSoundIndex = 27;
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if ((scene as Level).Session.HeartGem)
		{
			Disable();
		}
	}

	private void Disable()
	{
		enabled = false;
		sprite.Color = Color.White * 0.25f;
		Collidable = false;
	}

	private void Activate(Player player)
	{
		Audio.Play("event:/game/04_cliffside/whiteblock_fallthru", base.Center);
		activated = true;
		Collidable = false;
		player.Depth = 10001;
		base.Depth = -9000;
		Level level = base.Scene as Level;
		Rectangle rectangle = new Rectangle(level.Bounds.Left / 8, level.Bounds.Y / 8, level.Bounds.Width / 8, level.Bounds.Height / 8);
		Rectangle tileBounds = level.Session.MapData.TileBounds;
		bool[,] array = new bool[rectangle.Width, rectangle.Height];
		for (int i = 0; i < rectangle.Width; i++)
		{
			for (int j = 0; j < rectangle.Height; j++)
			{
				array[i, j] = level.BgData[i + rectangle.Left - tileBounds.Left, j + rectangle.Top - tileBounds.Top] != '0';
			}
		}
		bgSolidTiles = new Solid(new Vector2(level.Bounds.Left, level.Bounds.Top), 1f, 1f, safe: true);
		bgSolidTiles.Collider = new Grid(8f, 8f, array);
		base.Scene.Add(bgSolidTiles);
	}

	public override void Update()
	{
		base.Update();
		if (!enabled)
		{
			return;
		}
		if (!activated)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (HasPlayerRider() && entity != null && entity.Ducking)
			{
				playerDuckTimer += Engine.DeltaTime;
				if (playerDuckTimer >= 3f)
				{
					Activate(entity);
				}
			}
			else
			{
				playerDuckTimer = 0f;
			}
			if ((base.Scene as Level).Session.HeartGem)
			{
				Disable();
			}
		}
		else if (base.Scene.Tracker.GetEntity<HeartGem>() == null)
		{
			Player entity2 = base.Scene.Tracker.GetEntity<Player>();
			if (entity2 != null)
			{
				Disable();
				entity2.Depth = 0;
				base.Scene.Remove(bgSolidTiles);
			}
		}
	}
}
