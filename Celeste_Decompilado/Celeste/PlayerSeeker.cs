using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class PlayerSeeker : Actor
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass11_0
	{
		public PlayerSeeker _003C_003E4__this;

		public Level level;

		public Vector2 from;

		internal void _003CIntroSequence_003Eb__0(Tween f)
		{
			Vector2 cameraTarget = _003C_003E4__this.CameraTarget;
			level.Camera.Position = from + (cameraTarget - from) * f.Eased;
		}
	}

	[CompilerGenerated]
	private sealed class _003CIntroSequence_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerSeeker _003C_003E4__this;

		private _003C_003Ec__DisplayClass11_0 _003C_003E8__1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CIntroSequence_003Ed__11(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			PlayerSeeker playerSeeker = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass11_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.level = playerSeeker.Scene as Level;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Glitch.Value = 0.05f;
				_003C_003E8__1.level.Tracker.GetEntity<Player>()?.StartTempleMirrorVoidSleep();
				_003C_003E2__current = 3f;
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1.from = _003C_003E8__1.level.Camera.Position;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
				tween.OnUpdate = delegate(Tween f)
				{
					Vector2 cameraTarget = _003C_003E8__1._003C_003E4__this.CameraTarget;
					_003C_003E8__1.level.Camera.Position = _003C_003E8__1.from + (cameraTarget - _003C_003E8__1.from) * f.Eased;
				};
				playerSeeker.Add(tween);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
				_003C_003E1__state = -1;
				playerSeeker.shaker.ShakeFor(0.5f, removeOnFinish: false);
				playerSeeker.BreakOutParticles();
				Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				playerSeeker.shaker.ShakeFor(0.5f, removeOnFinish: false);
				playerSeeker.BreakOutParticles();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Long);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				playerSeeker.BreakOutParticles();
				Audio.Play("event:/game/05_mirror_temple/seeker_statue_break", playerSeeker.Position);
				playerSeeker.shaker.ShakeFor(1f, removeOnFinish: false);
				playerSeeker.sprite.Play("hatch");
				Input.Rumble(RumbleStrength.Strong, RumbleLength.FullSecond);
				playerSeeker.enabled = true;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				playerSeeker.BreakOutParticles();
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private Facings facing;

	private Sprite sprite;

	private Vector2 speed;

	private bool enabled;

	private float dashTimer;

	private Vector2 dashDirection;

	private float trailTimerA;

	private float trailTimerB;

	private Shaker shaker;

	public Vector2 CameraTarget
	{
		get
		{
			Rectangle bounds = (base.Scene as Level).Bounds;
			return (Position + new Vector2(-160f, -90f)).Clamp(bounds.Left, bounds.Top, bounds.Right - 320, bounds.Bottom - 180);
		}
	}

	public PlayerSeeker(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(sprite = GFX.SpriteBank.Create("seeker"));
		sprite.Play("statue");
		sprite.OnLastFrame = delegate(string a)
		{
			if (a == "flipMouth" || a == "flipEyes")
			{
				facing = (Facings)(0 - facing);
			}
		};
		base.Collider = new Hitbox(10f, 10f, -5f, -5f);
		Add(new MirrorReflection());
		Add(new PlayerCollider(OnPlayer));
		Add(new VertexLight(Color.White, 1f, 32, 64));
		facing = Facings.Right;
		Add(shaker = new Shaker(on: false));
		Add(new Coroutine(IntroSequence()));
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Level obj = scene as Level;
		obj.Session.ColorGrade = "templevoid";
		obj.ScreenPadding = 32f;
		obj.CanRetry = false;
	}

	[IteratorStateMachine(typeof(_003CIntroSequence_003Ed__11))]
	private IEnumerator IntroSequence()
	{
		Level level = base.Scene as Level;
		yield return null;
		Glitch.Value = 0.05f;
		level.Tracker.GetEntity<Player>()?.StartTempleMirrorVoidSleep();
		yield return 3f;
		Vector2 from = level.Camera.Position;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
		tween.OnUpdate = delegate(Tween f)
		{
			Vector2 cameraTarget = CameraTarget;
			level.Camera.Position = from + (cameraTarget - from) * f.Eased;
		};
		Add(tween);
		yield return 2f;
		shaker.ShakeFor(0.5f, removeOnFinish: false);
		BreakOutParticles();
		Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
		yield return 1f;
		shaker.ShakeFor(0.5f, removeOnFinish: false);
		BreakOutParticles();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Long);
		yield return 1f;
		BreakOutParticles();
		Audio.Play("event:/game/05_mirror_temple/seeker_statue_break", Position);
		shaker.ShakeFor(1f, removeOnFinish: false);
		sprite.Play("hatch");
		Input.Rumble(RumbleStrength.Strong, RumbleLength.FullSecond);
		enabled = true;
		yield return 0.8f;
		BreakOutParticles();
		yield return 0.7f;
	}

	private void BreakOutParticles()
	{
		Level level = SceneAs<Level>();
		for (float num = 0f; num < (float)Math.PI * 2f; num += 0.17453292f)
		{
			Vector2 position = base.Center + Calc.AngleToVector(num + Calc.Random.Range(-(float)Math.PI / 90f, (float)Math.PI / 90f), Calc.Random.Range(12, 20));
			level.Particles.Emit(Seeker.P_BreakOut, position, num);
		}
	}

	private void OnPlayer(Player player)
	{
		if (!player.Dead)
		{
			Leader.StoreStrawberries(player.Leader);
			PlayerDeadBody playerDeadBody = player.Die((player.Position - Position).SafeNormalize(), evenIfInvincible: true, registerDeathInStats: false);
			playerDeadBody.DeathAction = End;
			playerDeadBody.ActionDelay = 0.3f;
			Engine.TimeRate = 0.25f;
		}
	}

	private void End()
	{
		Level level = base.Scene as Level;
		level.OnEndOfFrame += delegate
		{
			Glitch.Value = 0f;
			Distort.Anxiety = 0f;
			Engine.TimeRate = 1f;
			level.Session.ColorGrade = null;
			level.UnloadLevel();
			level.CanRetry = true;
			level.Session.Level = "c-00";
			level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));
			level.LoadLevel(Player.IntroTypes.WakeUp);
			Leader.RestoreStrawberries(level.Tracker.GetEntity<Player>().Leader);
		};
	}

	public override void Update()
	{
		foreach (Entity entity2 in base.Scene.Tracker.GetEntities<SeekerBarrier>())
		{
			entity2.Collidable = true;
		}
		Level level = base.Scene as Level;
		base.Update();
		sprite.Scale.X = Calc.Approach(sprite.Scale.X, 1f, 2f * Engine.DeltaTime);
		sprite.Scale.Y = Calc.Approach(sprite.Scale.Y, 1f, 2f * Engine.DeltaTime);
		if (enabled && sprite.CurrentAnimationID != "hatch")
		{
			if (dashTimer > 0f)
			{
				speed = Calc.Approach(speed, Vector2.Zero, 800f * Engine.DeltaTime);
				dashTimer -= Engine.DeltaTime;
				if (dashTimer <= 0f)
				{
					sprite.Play("spotted");
				}
				if (trailTimerA > 0f)
				{
					trailTimerA -= Engine.DeltaTime;
					if (trailTimerA <= 0f)
					{
						CreateTrail();
					}
				}
				if (trailTimerB > 0f)
				{
					trailTimerB -= Engine.DeltaTime;
					if (trailTimerB <= 0f)
					{
						CreateTrail();
					}
				}
				if (base.Scene.OnInterval(0.04f))
				{
					Vector2 vector = speed.SafeNormalize();
					SceneAs<Level>().Particles.Emit(Seeker.P_Attack, 2, Position + vector * 4f, Vector2.One * 4f, vector.Angle());
				}
			}
			else
			{
				Vector2 vector2 = Input.Aim.Value.SafeNormalize();
				speed += vector2 * 600f * Engine.DeltaTime;
				float num = speed.Length();
				if (num > 120f)
				{
					num = Calc.Approach(num, 120f, Engine.DeltaTime * 700f);
					speed = speed.SafeNormalize(num);
				}
				if (vector2.Y == 0f)
				{
					speed.Y = Calc.Approach(speed.Y, 0f, 400f * Engine.DeltaTime);
				}
				if (vector2.X == 0f)
				{
					speed.X = Calc.Approach(speed.X, 0f, 400f * Engine.DeltaTime);
				}
				if (vector2.Length() > 0f && sprite.CurrentAnimationID == "idle")
				{
					level.Displacement.AddBurst(Position, 0.5f, 8f, 32f);
					sprite.Play("spotted");
					Audio.Play("event:/game/05_mirror_temple/seeker_playercontrolstart");
				}
				int num2 = Math.Sign((int)facing);
				int num3 = Math.Sign(speed.X);
				if (num3 != 0 && num2 != num3 && Math.Sign(Input.Aim.Value.X) == Math.Sign(speed.X) && Math.Abs(speed.X) > 20f && sprite.CurrentAnimationID != "flipMouth" && sprite.CurrentAnimationID != "flipEyes")
				{
					sprite.Play("flipMouth");
				}
				if (Input.Dash.Pressed)
				{
					Dash(Input.Aim.Value.EightWayNormal());
				}
			}
			MoveH(speed.X * Engine.DeltaTime, OnCollide);
			MoveV(speed.Y * Engine.DeltaTime, OnCollide);
			Position = Position.Clamp(level.Bounds.X, level.Bounds.Y, level.Bounds.Right, level.Bounds.Bottom);
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				float num4 = (Position - entity.Position).Length();
				if (num4 < 200f && entity.Sprite.CurrentAnimationID == "asleep")
				{
					entity.Sprite.Rate = 2f;
					entity.Sprite.Play("wakeUp");
				}
				else if (num4 < 100f && entity.Sprite.CurrentAnimationID != "wakeUp")
				{
					entity.Sprite.Rate = 1f;
					entity.Sprite.Play("runFast");
					entity.Facing = ((!(base.X > entity.X)) ? Facings.Right : Facings.Left);
				}
				if (num4 < 50f && dashTimer <= 0f)
				{
					Dash((entity.Center - base.Center).SafeNormalize());
				}
				Engine.TimeRate = Calc.ClampedMap(num4, 60f, 220f, 0.5f);
				Camera camera = level.Camera;
				Vector2 cameraTarget = CameraTarget;
				camera.Position += (cameraTarget - camera.Position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
				Distort.Anxiety = Calc.ClampedMap(num4, 0f, 200f, 0.25f, 0f) + Calc.Random.NextFloat(0.05f);
				Distort.AnxietyOrigin = (new Vector2(entity.X, level.Camera.Top) - level.Camera.Position) / new Vector2(320f, 180f);
			}
			else
			{
				Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 1f * Engine.DeltaTime);
			}
		}
		foreach (Entity entity3 in base.Scene.Tracker.GetEntities<SeekerBarrier>())
		{
			entity3.Collidable = false;
		}
	}

	private void CreateTrail()
	{
		Vector2 scale = sprite.Scale;
		sprite.Scale.X *= (float)facing;
		TrailManager.Add(this, Seeker.TrailColor);
		sprite.Scale = scale;
	}

	private void OnCollide(CollisionData data)
	{
		if (dashTimer <= 0f)
		{
			if (data.Direction.X != 0f)
			{
				speed.X = 0f;
			}
			if (data.Direction.Y != 0f)
			{
				speed.Y = 0f;
			}
			return;
		}
		float direction;
		Vector2 position;
		Vector2 positionRange;
		if (data.Direction.X > 0f)
		{
			direction = (float)Math.PI;
			position = new Vector2(base.Right, base.Y);
			positionRange = Vector2.UnitY * 4f;
		}
		else if (data.Direction.X < 0f)
		{
			direction = 0f;
			position = new Vector2(base.Left, base.Y);
			positionRange = Vector2.UnitY * 4f;
		}
		else if (data.Direction.Y > 0f)
		{
			direction = -(float)Math.PI / 2f;
			position = new Vector2(base.X, base.Bottom);
			positionRange = Vector2.UnitX * 4f;
		}
		else
		{
			direction = (float)Math.PI / 2f;
			position = new Vector2(base.X, base.Top);
			positionRange = Vector2.UnitX * 4f;
		}
		SceneAs<Level>().Particles.Emit(Seeker.P_HitWall, 12, position, positionRange, direction);
		if (data.Hit is SeekerBarrier)
		{
			(data.Hit as SeekerBarrier).OnReflectSeeker();
			Audio.Play("event:/game/05_mirror_temple/seeker_hit_lightwall", Position);
		}
		else
		{
			Audio.Play("event:/game/05_mirror_temple/seeker_hit_normal", Position);
		}
		if (data.Direction.X != 0f)
		{
			speed.X *= -0.8f;
			sprite.Scale = new Vector2(0.6f, 1.4f);
		}
		else if (data.Direction.Y != 0f)
		{
			speed.Y *= -0.8f;
			sprite.Scale = new Vector2(1.4f, 0.6f);
		}
		if (data.Hit is TempleCrackedBlock)
		{
			Celeste.Freeze(0.15f);
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
			(data.Hit as TempleCrackedBlock).Break(Position);
		}
	}

	private void Dash(Vector2 dir)
	{
		if (dashTimer <= 0f)
		{
			CreateTrail();
			trailTimerA = 0.1f;
			trailTimerB = 0.25f;
		}
		dashTimer = 0.3f;
		dashDirection = dir;
		if (dashDirection == Vector2.Zero)
		{
			dashDirection.X = Math.Sign((int)facing);
		}
		if (dashDirection.X != 0f)
		{
			facing = (Facings)Math.Sign(dashDirection.X);
		}
		speed = dashDirection * 400f;
		sprite.Play("attacking");
		SceneAs<Level>().DirectionalShake(dashDirection);
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		Audio.Play("event:/game/05_mirror_temple/seeker_dash", Position);
		if (dashDirection.X == 0f)
		{
			sprite.Scale = new Vector2(0.6f, 1.4f);
		}
		else
		{
			sprite.Scale = new Vector2(1.4f, 0.6f);
		}
	}

	public override void Render()
	{
		if (!SaveData.Instance.Assists.InvisibleMotion || !enabled || !(speed.LengthSquared() > 100f))
		{
			Vector2 position = Position;
			Position += shaker.Value;
			Vector2 scale = sprite.Scale;
			sprite.Scale.X *= (float)facing;
			base.Render();
			Position = position;
			sprite.Scale = scale;
		}
	}
}
