using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class MoonCreature : Entity
{
	private struct TrailNode
	{
		public Vector2 Position;

		public Color Color;
	}

	private TrailNode[] trail;

	private Vector2 start;

	private Vector2 target;

	private float targetTimer;

	private Vector2 speed;

	private Vector2 bump;

	private Player following;

	private Vector2 followingOffset;

	private float followingTime;

	private Color OrbColor;

	private Color CenterColor;

	private Sprite Sprite;

	private const float Acceleration = 90f;

	private const float FollowAcceleration = 120f;

	private const float MaxSpeed = 40f;

	private const float MaxFollowSpeed = 70f;

	private const float MaxFollowDistance = 200f;

	private readonly int spawn;

	private Rectangle originLevelBounds;

	public MoonCreature(Vector2 position)
	{
		base.Tag = Tags.TransitionUpdate;
		base.Depth = -13010;
		base.Collider = new Hitbox(20f, 20f, -10f, -10f);
		start = position;
		targetTimer = 0f;
		GetRandomTarget();
		Position = target;
		Add(new PlayerCollider(OnPlayer));
		OrbColor = Calc.HexToColor("b0e6ff");
		CenterColor = Calc.Random.Choose(Calc.HexToColor("c34fc7"), Calc.HexToColor("4f95c7"), Calc.HexToColor("53c74f"));
		Color value = Color.Lerp(CenterColor, Calc.HexToColor("bde4ee"), 0.5f);
		Color value2 = Color.Lerp(CenterColor, Calc.HexToColor("2f2941"), 0.5f);
		trail = new TrailNode[10];
		for (int i = 0; i < 10; i++)
		{
			trail[i] = new TrailNode
			{
				Position = Position,
				Color = Color.Lerp(value, value2, (float)i / 9f)
			};
		}
		Add(Sprite = GFX.SpriteBank.Create("moonCreatureTiny"));
	}

	public MoonCreature(EntityData data, Vector2 offset)
		: this(data.Position + offset)
	{
		spawn = data.Int("number", 1) - 1;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		for (int i = 0; i < spawn; i++)
		{
			scene.Add(new MoonCreature(Position + new Vector2(Calc.Random.Range(-4, 4), Calc.Random.Range(-4, 4))));
		}
		originLevelBounds = (scene as Level).Bounds;
	}

	private void OnPlayer(Player player)
	{
		Vector2 vector = (Position - player.Center).SafeNormalize(player.Speed.Length() * 0.3f);
		if (vector.LengthSquared() > bump.LengthSquared())
		{
			bump = vector;
			if ((player.Center - start).Length() < 200f)
			{
				following = player;
				followingTime = Calc.Random.Range(6f, 12f);
				GetFollowOffset();
			}
		}
	}

	private void GetFollowOffset()
	{
		followingOffset = new Vector2(Calc.Random.Choose(-1, 1) * Calc.Random.Range(8, 16), Calc.Random.Range(-20f, 0f));
	}

	private void GetRandomTarget()
	{
		Vector2 vector = target;
		do
		{
			float length = Calc.Random.NextFloat(32f);
			float angleRadians = Calc.Random.NextFloat((float)Math.PI * 2f);
			target = start + Calc.AngleToVector(angleRadians, length);
		}
		while ((vector - target).Length() < 8f);
	}

	public override void Update()
	{
		base.Update();
		if (following == null)
		{
			targetTimer -= Engine.DeltaTime;
			if (targetTimer <= 0f)
			{
				targetTimer = Calc.Random.Range(0.8f, 4f);
				GetRandomTarget();
			}
		}
		else
		{
			followingTime -= Engine.DeltaTime;
			targetTimer -= Engine.DeltaTime;
			if (targetTimer <= 0f)
			{
				targetTimer = Calc.Random.Range(0.8f, 2f);
				GetFollowOffset();
			}
			target = following.Center + followingOffset;
			if ((Position - start).Length() > 200f || followingTime <= 0f)
			{
				following = null;
				targetTimer = 0f;
			}
		}
		Vector2 vector = (target - Position).SafeNormalize();
		speed += vector * ((following == null) ? 90f : 120f) * Engine.DeltaTime;
		speed = speed.SafeNormalize() * Math.Min(speed.Length(), (following == null) ? 40f : 70f);
		bump = bump.SafeNormalize() * Calc.Approach(bump.Length(), 0f, Engine.DeltaTime * 80f);
		Position += (speed + bump) * Engine.DeltaTime;
		Vector2 position = Position;
		for (int i = 0; i < trail.Length; i++)
		{
			Vector2 vector2 = (trail[i].Position - position).SafeNormalize();
			if (vector2 == Vector2.Zero)
			{
				vector2 = new Vector2(0f, 1f);
			}
			vector2.Y += 0.05f;
			Vector2 vector3 = position + vector2 * 2f;
			trail[i].Position = Calc.Approach(trail[i].Position, vector3, 128f * Engine.DeltaTime);
			position = trail[i].Position;
		}
		base.X = Calc.Clamp(base.X, originLevelBounds.Left + 4, originLevelBounds.Right - 4);
		base.Y = Calc.Clamp(base.Y, originLevelBounds.Top + 4, originLevelBounds.Bottom - 4);
	}

	public override void Render()
	{
		Vector2 position = Position;
		Position = Position.FloorV2();
		for (int num = trail.Length - 1; num >= 0; num--)
		{
			Vector2 position2 = trail[num].Position;
			float num2 = Calc.ClampedMap(num, 0f, trail.Length - 1, 3f);
			Draw.Rect(position2.X - num2 / 2f, position2.Y - num2 / 2f, num2, num2, trail[num].Color);
		}
		base.Render();
		Position = position;
	}
}
