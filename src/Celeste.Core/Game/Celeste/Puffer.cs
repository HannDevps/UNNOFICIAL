using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Puffer : Actor
{
	private enum States
	{
		Idle,
		Hit,
		Gone
	}

	private const float RespawnTime = 2.5f;

	private const float RespawnMoveTime = 0.5f;

	private const float BounceSpeed = 200f;

	private const float ExplodeRadius = 40f;

	private const float DetectRadius = 32f;

	private const float StunnedAccel = 320f;

	private const float AlertedRadius = 60f;

	private const float CantExplodeTime = 0.5f;

	private Sprite sprite;

	private States state;

	private Vector2 startPosition;

	private Vector2 anchorPosition;

	private Vector2 lastSpeedPosition;

	private Vector2 lastSinePosition;

	private Circle pushRadius;

	private Circle breakWallsRadius;

	private Circle detectRadius;

	private SineWave idleSine;

	private Vector2 hitSpeed;

	private float goneTimer;

	private float cannotHitTimer;

	private Collision onCollideV;

	private Collision onCollideH;

	private float alertTimer;

	private Wiggler bounceWiggler;

	private Wiggler inflateWiggler;

	private Vector2 scale;

	private SimpleCurve returnCurve;

	private float cantExplodeTimer;

	private Vector2 lastPlayerPos;

	private float playerAliveFade;

	private Vector2 facing = Vector2.One;

	private float eyeSpin;

	public Puffer(Vector2 position, bool faceRight)
		: base(position)
	{
		base.Collider = new Hitbox(12f, 10f, -6f, -5f);
		Add(new PlayerCollider(OnPlayer, new Hitbox(14f, 12f, -7f, -7f)));
		Add(sprite = GFX.SpriteBank.Create("pufferFish"));
		sprite.Play("idle");
		if (!faceRight)
		{
			facing.X = -1f;
		}
		idleSine = new SineWave(0.5f);
		idleSine.Randomize();
		Add(idleSine);
		anchorPosition = Position;
		Position += new Vector2(idleSine.Value * 3f, idleSine.ValueOverTwo * 2f);
		state = States.Idle;
		startPosition = (lastSinePosition = (lastSpeedPosition = Position));
		pushRadius = new Circle(40f);
		detectRadius = new Circle(32f);
		breakWallsRadius = new Circle(16f);
		onCollideV = OnCollideV;
		onCollideH = OnCollideH;
		scale = Vector2.One;
		bounceWiggler = Wiggler.Create(0.6f, 2.5f, delegate(float v)
		{
			sprite.Rotation = v * 20f * ((float)Math.PI / 180f);
		});
		Add(bounceWiggler);
		inflateWiggler = Wiggler.Create(0.6f, 2f);
		Add(inflateWiggler);
	}

	public Puffer(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Bool("right"))
	{
	}

	public override bool IsRiding(JumpThru jumpThru)
	{
		return false;
	}

	public override bool IsRiding(Solid solid)
	{
		return false;
	}

	protected override void OnSquish(CollisionData data)
	{
		Explode();
		GotoGone();
	}

	private void OnCollideH(CollisionData data)
	{
		hitSpeed.X *= -0.8f;
	}

	private void OnCollideV(CollisionData data)
	{
		if (!(data.Direction.Y > 0f))
		{
			return;
		}
		for (int i = -1; i <= 1; i += 2)
		{
			for (int j = 1; j <= 2; j++)
			{
				Vector2 vector = Position + Vector2.UnitX * j * i;
				if (!CollideCheck<Solid>(vector) && !OnGround(vector))
				{
					Position = vector;
					return;
				}
			}
		}
		hitSpeed.Y *= -0.2f;
	}

	private void GotoIdle()
	{
		if (state == States.Gone)
		{
			Position = startPosition;
			cantExplodeTimer = 0.5f;
			sprite.Play("recover");
			Audio.Play("event:/new_content/game/10_farewell/puffer_reform", Position);
		}
		lastSinePosition = (lastSpeedPosition = (anchorPosition = Position));
		hitSpeed = Vector2.Zero;
		idleSine.Reset();
		state = States.Idle;
	}

	private void GotoHit(Vector2 from)
	{
		scale = new Vector2(1.2f, 0.8f);
		hitSpeed = Vector2.UnitY * 200f;
		state = States.Hit;
		bounceWiggler.Start();
		Alert(restart: true, playSfx: false);
		Audio.Play("event:/new_content/game/10_farewell/puffer_boop", Position);
	}

	private void GotoHitSpeed(Vector2 speed)
	{
		hitSpeed = speed;
		state = States.Hit;
	}

	private void GotoGone()
	{
		Vector2 control = Position + (startPosition - Position) * 0.5f;
		if ((startPosition - Position).LengthSquared() > 100f)
		{
			if (Math.Abs(Position.Y - startPosition.Y) > Math.Abs(Position.X - startPosition.X))
			{
				if (Position.X > startPosition.X)
				{
					control += Vector2.UnitX * -24f;
				}
				else
				{
					control += Vector2.UnitX * 24f;
				}
			}
			else if (Position.Y > startPosition.Y)
			{
				control += Vector2.UnitY * -24f;
			}
			else
			{
				control += Vector2.UnitY * 24f;
			}
		}
		returnCurve = new SimpleCurve(Position, startPosition, control);
		Collidable = false;
		goneTimer = 2.5f;
		state = States.Gone;
	}

	private void Explode()
	{
		Collider collider = base.Collider;
		base.Collider = pushRadius;
		Audio.Play("event:/new_content/game/10_farewell/puffer_splode", Position);
		sprite.Play("explode");
		Player player = CollideFirst<Player>();
		if (player != null && !base.Scene.CollideCheck<Solid>(Position, player.Center))
		{
			player.ExplodeLaunch(Position, snapUp: false, sidesOnly: true);
		}
		TheoCrystal theoCrystal = CollideFirst<TheoCrystal>();
		if (theoCrystal != null && !base.Scene.CollideCheck<Solid>(Position, theoCrystal.Center))
		{
			theoCrystal.ExplodeLaunch(Position);
		}
		foreach (TempleCrackedBlock entity in base.Scene.Tracker.GetEntities<TempleCrackedBlock>())
		{
			if (CollideCheck(entity))
			{
				entity.Break(Position);
			}
		}
		foreach (TouchSwitch entity2 in base.Scene.Tracker.GetEntities<TouchSwitch>())
		{
			if (CollideCheck(entity2))
			{
				entity2.TurnOn();
			}
		}
		foreach (FloatingDebris entity3 in base.Scene.Tracker.GetEntities<FloatingDebris>())
		{
			if (CollideCheck(entity3))
			{
				entity3.OnExplode(Position);
			}
		}
		base.Collider = collider;
		Level level = SceneAs<Level>();
		level.Shake();
		level.Displacement.AddBurst(Position, 0.4f, 12f, 36f, 0.5f);
		level.Displacement.AddBurst(Position, 0.4f, 24f, 48f, 0.5f);
		level.Displacement.AddBurst(Position, 0.4f, 36f, 60f, 0.5f);
		for (float num = 0f; num < (float)Math.PI * 2f; num += 0.17453292f)
		{
			Vector2 position = base.Center + Calc.AngleToVector(num + Calc.Random.Range(-(float)Math.PI / 90f, (float)Math.PI / 90f), Calc.Random.Range(12, 18));
			level.Particles.Emit(Seeker.P_Regen, position, num);
		}
	}

	public override void Render()
	{
		sprite.Scale = scale * (1f + inflateWiggler.Value * 0.4f);
		sprite.Scale *= facing;
		bool flag = false;
		if (sprite.CurrentAnimationID != "hidden" && sprite.CurrentAnimationID != "explode" && sprite.CurrentAnimationID != "recover")
		{
			flag = true;
		}
		else if (sprite.CurrentAnimationID == "explode" && sprite.CurrentAnimationFrame <= 1)
		{
			flag = true;
		}
		else if (sprite.CurrentAnimationID == "recover" && sprite.CurrentAnimationFrame >= 4)
		{
			flag = true;
		}
		if (flag)
		{
			sprite.DrawSimpleOutline();
		}
		float num = playerAliveFade * Calc.ClampedMap((Position - lastPlayerPos).Length(), 128f, 96f);
		if (num > 0f && state != States.Gone)
		{
			bool flag2 = false;
			Vector2 vector = lastPlayerPos;
			if (vector.Y < base.Y)
			{
				vector.Y = base.Y - (vector.Y - base.Y) * 0.5f;
				vector.X += vector.X - base.X;
				flag2 = true;
			}
			float radiansB = (vector - Position).Angle();
			for (int i = 0; i < 28; i++)
			{
				float num2 = (float)Math.Sin(base.Scene.TimeActive * 0.5f) * 0.02f;
				float num3 = Calc.Map((float)i / 28f + num2, 0f, 1f, -(float)Math.PI / 30f, 3.2463126f);
				num3 += bounceWiggler.Value * 20f * ((float)Math.PI / 180f);
				Vector2 vector2 = Calc.AngleToVector(num3, 1f);
				Vector2 vector3 = Position + vector2 * 32f;
				float t = Calc.ClampedMap(Calc.AbsAngleDiff(num3, radiansB), (float)Math.PI / 2f, 0.17453292f);
				t = Ease.CubeOut(t) * 0.8f * num;
				if (!(t > 0f))
				{
					continue;
				}
				if (i == 0 || i == 27)
				{
					Draw.Line(vector3, vector3 - vector2 * 10f, Color.White * t);
					continue;
				}
				Vector2 vector4 = vector2 * (float)Math.Sin(base.Scene.TimeActive * 2f + (float)i * 0.6f);
				if (i % 2 == 0)
				{
					vector4 *= -1f;
				}
				vector3 += vector4;
				if (!flag2 && Calc.AbsAngleDiff(num3, radiansB) <= 0.17453292f)
				{
					Draw.Line(vector3, vector3 - vector2 * 3f, Color.White * t);
				}
				else
				{
					Draw.Point(vector3, Color.White * t);
				}
			}
		}
		base.Render();
		if (sprite.CurrentAnimationID == "alerted")
		{
			Vector2 vector5 = Position + new Vector2(3f, (facing.X < 0f) ? (-5) : (-4)) * sprite.Scale;
			Vector2 to = lastPlayerPos + new Vector2(0f, -4f);
			Vector2 vector6 = Calc.AngleToVector(Calc.Angle(vector5, to) + eyeSpin * ((float)Math.PI * 2f) * 2f, 1f);
			Vector2 vector7 = vector5 + new Vector2((float)Math.Round(vector6.X), (float)Math.Round(Calc.ClampedMap(vector6.Y, -1f, 1f, -1f, 2f)));
			Draw.Rect(vector7.X, vector7.Y, 1f, 1f, Color.Black);
		}
		sprite.Scale /= facing;
	}

	public override void Update()
	{
		base.Update();
		eyeSpin = Calc.Approach(eyeSpin, 0f, Engine.DeltaTime * 1.5f);
		scale = Calc.Approach(scale, Vector2.One, 1f * Engine.DeltaTime);
		if (cannotHitTimer > 0f)
		{
			cannotHitTimer -= Engine.DeltaTime;
		}
		if (state != States.Gone && cantExplodeTimer > 0f)
		{
			cantExplodeTimer -= Engine.DeltaTime;
		}
		if (alertTimer > 0f)
		{
			alertTimer -= Engine.DeltaTime;
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity == null)
		{
			playerAliveFade = Calc.Approach(playerAliveFade, 0f, 1f * Engine.DeltaTime);
		}
		else
		{
			playerAliveFade = Calc.Approach(playerAliveFade, 1f, 1f * Engine.DeltaTime);
			lastPlayerPos = entity.Center;
		}
		switch (state)
		{
		case States.Idle:
		{
			if (Position != lastSinePosition)
			{
				anchorPosition += Position - lastSinePosition;
			}
			Vector2 vector = anchorPosition + new Vector2(idleSine.Value * 3f, idleSine.ValueOverTwo * 2f);
			MoveToX(vector.X);
			MoveToY(vector.Y);
			lastSinePosition = Position;
			if (ProximityExplodeCheck())
			{
				Explode();
				GotoGone();
				break;
			}
			if (AlertedCheck())
			{
				Alert(restart: false, playSfx: true);
			}
			else if (sprite.CurrentAnimationID == "alerted" && alertTimer <= 0f)
			{
				Audio.Play("event:/new_content/game/10_farewell/puffer_shrink", Position);
				sprite.Play("unalert");
			}
			{
				foreach (PufferCollider component in base.Scene.Tracker.GetComponents<PufferCollider>())
				{
					component.Check(this);
				}
				break;
			}
		}
		case States.Hit:
			lastSpeedPosition = Position;
			MoveH(hitSpeed.X * Engine.DeltaTime, onCollideH);
			MoveV(hitSpeed.Y * Engine.DeltaTime, OnCollideV);
			anchorPosition = Position;
			hitSpeed.X = Calc.Approach(hitSpeed.X, 0f, 150f * Engine.DeltaTime);
			hitSpeed = Calc.Approach(hitSpeed, Vector2.Zero, 320f * Engine.DeltaTime);
			if (ProximityExplodeCheck())
			{
				Explode();
				GotoGone();
				break;
			}
			if (base.Top >= (float)(SceneAs<Level>().Bounds.Bottom + 5))
			{
				sprite.Play("hidden");
				GotoGone();
				break;
			}
			foreach (PufferCollider component2 in base.Scene.Tracker.GetComponents<PufferCollider>())
			{
				component2.Check(this);
			}
			if (hitSpeed == Vector2.Zero)
			{
				ZeroRemainderX();
				ZeroRemainderY();
				GotoIdle();
			}
			break;
		case States.Gone:
		{
			float num = goneTimer;
			goneTimer -= Engine.DeltaTime;
			if (goneTimer <= 0.5f)
			{
				if (num > 0.5f && returnCurve.GetLengthParametric(8) > 8f)
				{
					Audio.Play("event:/new_content/game/10_farewell/puffer_return", Position);
				}
				Position = returnCurve.GetPoint(Ease.CubeInOut(Calc.ClampedMap(goneTimer, 0.5f, 0f)));
			}
			if (goneTimer <= 0f)
			{
				Visible = (Collidable = true);
				GotoIdle();
			}
			break;
		}
		}
	}

	public bool HitSpring(Spring spring)
	{
		switch (spring.Orientation)
		{
		default:
			if (hitSpeed.Y >= 0f)
			{
				GotoHitSpeed(224f * -Vector2.UnitY);
				MoveTowardsX(spring.CenterX, 4f);
				bounceWiggler.Start();
				Alert(restart: true, playSfx: false);
				return true;
			}
			return false;
		case Spring.Orientations.WallLeft:
			if (hitSpeed.X <= 60f)
			{
				facing.X = 1f;
				GotoHitSpeed(280f * Vector2.UnitX);
				MoveTowardsY(spring.CenterY, 4f);
				bounceWiggler.Start();
				Alert(restart: true, playSfx: false);
				return true;
			}
			return false;
		case Spring.Orientations.WallRight:
			if (hitSpeed.X >= -60f)
			{
				facing.X = -1f;
				GotoHitSpeed(280f * -Vector2.UnitX);
				MoveTowardsY(spring.CenterY, 4f);
				bounceWiggler.Start();
				Alert(restart: true, playSfx: false);
				return true;
			}
			return false;
		}
	}

	private bool ProximityExplodeCheck()
	{
		if (cantExplodeTimer > 0f)
		{
			return false;
		}
		bool result = false;
		Collider collider = base.Collider;
		base.Collider = detectRadius;
		Player player;
		if ((player = CollideFirst<Player>()) != null && player.CenterY >= base.Y + collider.Bottom - 4f && !base.Scene.CollideCheck<Solid>(Position, player.Center))
		{
			result = true;
		}
		base.Collider = collider;
		return result;
	}

	private bool AlertedCheck()
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			return (entity.Center - base.Center).Length() < 60f;
		}
		return false;
	}

	private void Alert(bool restart, bool playSfx)
	{
		if (sprite.CurrentAnimationID == "idle")
		{
			if (playSfx)
			{
				Audio.Play("event:/new_content/game/10_farewell/puffer_expand", Position);
			}
			sprite.Play("alert");
			inflateWiggler.Start();
		}
		else if (restart && playSfx)
		{
			Audio.Play("event:/new_content/game/10_farewell/puffer_expand", Position);
		}
		alertTimer = 2f;
	}

	private void OnPlayer(Player player)
	{
		if (state == States.Gone || !(cantExplodeTimer <= 0f))
		{
			return;
		}
		if (cannotHitTimer <= 0f)
		{
			if (player.Bottom > lastSpeedPosition.Y + 3f)
			{
				Explode();
				GotoGone();
			}
			else
			{
				player.Bounce(base.Top);
				GotoHit(player.Center);
				MoveToX(anchorPosition.X);
				idleSine.Reset();
				anchorPosition = (lastSinePosition = Position);
				eyeSpin = 1f;
			}
		}
		cannotHitTimer = 0.1f;
	}
}
