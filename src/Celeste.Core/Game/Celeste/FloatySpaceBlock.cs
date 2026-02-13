using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class FloatySpaceBlock : Solid
{
	private TileGrid tiles;

	private char tileType;

	private float yLerp;

	private float sinkTimer;

	private float sineWave;

	private float dashEase;

	private Vector2 dashDirection;

	private FloatySpaceBlock master;

	private bool awake;

	public List<FloatySpaceBlock> Group;

	public List<JumpThru> Jumpthrus;

	public Dictionary<Platform, Vector2> Moves;

	public Point GroupBoundsMin;

	public Point GroupBoundsMax;

	public bool HasGroup { get; private set; }

	public bool MasterOfGroup { get; private set; }

	public FloatySpaceBlock(Vector2 position, float width, float height, char tileType, bool disableSpawnOffset)
		: base(position, width, height, safe: true)
	{
		this.tileType = tileType;
		base.Depth = -9000;
		Add(new LightOcclude());
		SurfaceSoundIndex = SurfaceIndex.TileToIndex[tileType];
		if (!disableSpawnOffset)
		{
			sineWave = Calc.Random.NextFloat((float)Math.PI * 2f);
		}
		else
		{
			sineWave = 0f;
		}
	}

	public FloatySpaceBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Char("tiletype", '3'), data.Bool("disableSpawnOffset"))
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		awake = true;
		if (!HasGroup)
		{
			MasterOfGroup = true;
			Moves = new Dictionary<Platform, Vector2>();
			Group = new List<FloatySpaceBlock>();
			Jumpthrus = new List<JumpThru>();
			GroupBoundsMin = new Point((int)base.X, (int)base.Y);
			GroupBoundsMax = new Point((int)base.Right, (int)base.Bottom);
			AddToGroupAndFindChildren(this);
			_ = base.Scene;
			Rectangle rectangle = new Rectangle(GroupBoundsMin.X / 8, GroupBoundsMin.Y / 8, (GroupBoundsMax.X - GroupBoundsMin.X) / 8 + 1, (GroupBoundsMax.Y - GroupBoundsMin.Y) / 8 + 1);
			VirtualMap<char> virtualMap = new VirtualMap<char>(rectangle.Width, rectangle.Height, '0');
			foreach (FloatySpaceBlock item in Group)
			{
				int num = (int)(item.X / 8f) - rectangle.X;
				int num2 = (int)(item.Y / 8f) - rectangle.Y;
				int num3 = (int)(item.Width / 8f);
				int num4 = (int)(item.Height / 8f);
				for (int i = num; i < num + num3; i++)
				{
					for (int j = num2; j < num2 + num4; j++)
					{
						virtualMap[i, j] = tileType;
					}
				}
			}
			tiles = GFX.FGAutotiler.GenerateMap(virtualMap, new Autotiler.Behaviour
			{
				EdgesExtend = false,
				EdgesIgnoreOutOfLevel = false,
				PaddingIgnoreOutOfLevel = false
			}).TileGrid;
			tiles.Position = new Vector2((float)GroupBoundsMin.X - base.X, (float)GroupBoundsMin.Y - base.Y);
			Add(tiles);
		}
		TryToInitPosition();
	}

	public override void OnStaticMoverTrigger(StaticMover sm)
	{
		if (sm.Entity is Spring)
		{
			switch ((sm.Entity as Spring).Orientation)
			{
			case Spring.Orientations.Floor:
				sinkTimer = 0.5f;
				break;
			case Spring.Orientations.WallLeft:
				dashEase = 1f;
				dashDirection = -Vector2.UnitX;
				break;
			case Spring.Orientations.WallRight:
				dashEase = 1f;
				dashDirection = Vector2.UnitX;
				break;
			}
		}
	}

	private void TryToInitPosition()
	{
		if (MasterOfGroup)
		{
			foreach (FloatySpaceBlock item in Group)
			{
				if (!item.awake)
				{
					return;
				}
			}
			MoveToTarget();
		}
		else
		{
			master.TryToInitPosition();
		}
	}

	private void AddToGroupAndFindChildren(FloatySpaceBlock from)
	{
		if (from.X < (float)GroupBoundsMin.X)
		{
			GroupBoundsMin.X = (int)from.X;
		}
		if (from.Y < (float)GroupBoundsMin.Y)
		{
			GroupBoundsMin.Y = (int)from.Y;
		}
		if (from.Right > (float)GroupBoundsMax.X)
		{
			GroupBoundsMax.X = (int)from.Right;
		}
		if (from.Bottom > (float)GroupBoundsMax.Y)
		{
			GroupBoundsMax.Y = (int)from.Bottom;
		}
		from.HasGroup = true;
		from.OnDashCollide = OnDash;
		Group.Add(from);
		Moves.Add(from, from.Position);
		if (from != this)
		{
			from.master = this;
		}
		foreach (JumpThru item in base.Scene.CollideAll<JumpThru>(new Rectangle((int)from.X - 1, (int)from.Y, (int)from.Width + 2, (int)from.Height)))
		{
			if (!Jumpthrus.Contains(item))
			{
				AddJumpThru(item);
			}
		}
		foreach (JumpThru item2 in base.Scene.CollideAll<JumpThru>(new Rectangle((int)from.X, (int)from.Y - 1, (int)from.Width, (int)from.Height + 2)))
		{
			if (!Jumpthrus.Contains(item2))
			{
				AddJumpThru(item2);
			}
		}
		foreach (FloatySpaceBlock entity in base.Scene.Tracker.GetEntities<FloatySpaceBlock>())
		{
			if (!entity.HasGroup && entity.tileType == tileType && (base.Scene.CollideCheck(new Rectangle((int)from.X - 1, (int)from.Y, (int)from.Width + 2, (int)from.Height), entity) || base.Scene.CollideCheck(new Rectangle((int)from.X, (int)from.Y - 1, (int)from.Width, (int)from.Height + 2), entity)))
			{
				AddToGroupAndFindChildren(entity);
			}
		}
	}

	private void AddJumpThru(JumpThru jp)
	{
		jp.OnDashCollide = OnDash;
		Jumpthrus.Add(jp);
		Moves.Add(jp, jp.Position);
		foreach (FloatySpaceBlock entity in base.Scene.Tracker.GetEntities<FloatySpaceBlock>())
		{
			if (!entity.HasGroup && entity.tileType == tileType && base.Scene.CollideCheck(new Rectangle((int)jp.X - 1, (int)jp.Y, (int)jp.Width + 2, (int)jp.Height), entity))
			{
				AddToGroupAndFindChildren(entity);
			}
		}
	}

	private DashCollisionResults OnDash(Player player, Vector2 direction)
	{
		if (MasterOfGroup && dashEase <= 0.2f)
		{
			dashEase = 1f;
			dashDirection = direction;
		}
		return DashCollisionResults.NormalOverride;
	}

	public override void Update()
	{
		base.Update();
		if (MasterOfGroup)
		{
			bool flag = false;
			foreach (FloatySpaceBlock item in Group)
			{
				if (item.HasPlayerRider())
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				foreach (JumpThru jumpthru in Jumpthrus)
				{
					if (jumpthru.HasPlayerRider())
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				sinkTimer = 0.3f;
			}
			else if (sinkTimer > 0f)
			{
				sinkTimer -= Engine.DeltaTime;
			}
			if (sinkTimer > 0f)
			{
				yLerp = Calc.Approach(yLerp, 1f, 1f * Engine.DeltaTime);
			}
			else
			{
				yLerp = Calc.Approach(yLerp, 0f, 1f * Engine.DeltaTime);
			}
			sineWave += Engine.DeltaTime;
			dashEase = Calc.Approach(dashEase, 0f, Engine.DeltaTime * 1.5f);
			MoveToTarget();
		}
		LiftSpeed = Vector2.Zero;
	}

	private void MoveToTarget()
	{
		float num = (float)Math.Sin(sineWave) * 4f;
		Vector2 vector = Calc.YoYo(Ease.QuadIn(dashEase)) * dashDirection * 8f;
		for (int i = 0; i < 2; i++)
		{
			foreach (KeyValuePair<Platform, Vector2> move in Moves)
			{
				Platform key = move.Key;
				bool flag = false;
				JumpThru jumpThru = key as JumpThru;
				Solid solid = key as Solid;
				if ((jumpThru != null && jumpThru.HasRider()) || (solid != null && solid.HasRider()))
				{
					flag = true;
				}
				if ((flag || i != 0) && (!flag || i != 1))
				{
					Vector2 value = move.Value;
					float num2 = MathHelper.Lerp(value.Y, value.Y + 12f, Ease.SineInOut(yLerp)) + num;
					key.MoveToY(num2 + vector.Y);
					key.MoveToX(value.X + vector.X);
				}
			}
		}
	}

	public override void OnShake(Vector2 amount)
	{
		if (!MasterOfGroup)
		{
			return;
		}
		base.OnShake(amount);
		tiles.Position += amount;
		foreach (JumpThru jumpthru in Jumpthrus)
		{
			foreach (Component component in jumpthru.Components)
			{
				if (component is Image image)
				{
					image.Position += amount;
				}
			}
		}
	}
}
