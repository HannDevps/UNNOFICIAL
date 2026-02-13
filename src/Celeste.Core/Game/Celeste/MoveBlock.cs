using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class MoveBlock : Solid
{
	public enum Directions
	{
		Left,
		Right,
		Up,
		Down
	}

	private enum MovementState
	{
		Idling,
		Moving,
		Breaking
	}

	private class Border : Entity
	{
		public MoveBlock Parent;

		public Border(MoveBlock parent)
		{
			Parent = parent;
			base.Depth = 1;
		}

		public override void Update()
		{
			if (Parent.Scene != base.Scene)
			{
				RemoveSelf();
			}
			base.Update();
		}

		public override void Render()
		{
			Draw.Rect(Parent.X + Parent.Shake.X - 1f, Parent.Y + Parent.Shake.Y - 1f, Parent.Width + 2f, Parent.Height + 2f, Color.Black);
		}
	}

	[Pooled]
	private class Debris : Actor
	{
		private Image sprite;

		private Vector2 home;

		private Vector2 speed;

		private bool shaking;

		private bool returning;

		private float returnEase;

		private float returnDuration;

		private SimpleCurve returnCurve;

		private bool firstHit;

		private float alpha;

		private Collision onCollideH;

		private Collision onCollideV;

		private float spin;

		public Debris()
			: base(Vector2.Zero)
		{
			base.Tag = Tags.TransitionUpdate;
			base.Collider = new Hitbox(4f, 4f, -2f, -2f);
			Add(sprite = new Image(Calc.Random.Choose(GFX.Game.GetAtlasSubtextures("objects/moveblock/debris"))));
			sprite.CenterOrigin();
			sprite.FlipX = Calc.Random.Chance(0.5f);
			onCollideH = delegate
			{
				speed.X = (0f - speed.X) * 0.5f;
			};
			onCollideV = delegate
			{
				if (firstHit || speed.Y > 50f)
				{
					Audio.Play("event:/game/general/debris_stone", Position, "debris_velocity", Calc.ClampedMap(speed.Y, 0f, 600f));
				}
				if (speed.Y > 0f && speed.Y < 40f)
				{
					speed.Y = 0f;
				}
				else
				{
					speed.Y = (0f - speed.Y) * 0.25f;
				}
				firstHit = false;
			};
		}

		protected override void OnSquish(CollisionData data)
		{
		}

		public Debris Init(Vector2 position, Vector2 center, Vector2 returnTo)
		{
			Collidable = true;
			Position = position;
			speed = (position - center).SafeNormalize(60f + Calc.Random.NextFloat(60f));
			home = returnTo;
			sprite.Position = Vector2.Zero;
			sprite.Rotation = Calc.Random.NextAngle();
			returning = false;
			shaking = false;
			sprite.Scale.X = 1f;
			sprite.Scale.Y = 1f;
			sprite.Color = Color.White;
			alpha = 1f;
			firstHit = false;
			spin = Calc.Random.Range(3.4906585f, 10.471975f) * (float)Calc.Random.Choose(1, -1);
			return this;
		}

		public override void Update()
		{
			base.Update();
			if (!returning)
			{
				if (Collidable)
				{
					speed.X = Calc.Approach(speed.X, 0f, Engine.DeltaTime * 100f);
					if (!OnGround())
					{
						speed.Y += 400f * Engine.DeltaTime;
					}
					MoveH(speed.X * Engine.DeltaTime, onCollideH);
					MoveV(speed.Y * Engine.DeltaTime, onCollideV);
				}
				if (shaking && base.Scene.OnInterval(0.05f))
				{
					sprite.X = -1 + Calc.Random.Next(3);
					sprite.Y = -1 + Calc.Random.Next(3);
				}
			}
			else
			{
				Position = returnCurve.GetPoint(Ease.CubeOut(returnEase));
				returnEase = Calc.Approach(returnEase, 1f, Engine.DeltaTime / returnDuration);
				sprite.Scale = Vector2.One * (1f + returnEase * 0.5f);
			}
			if ((base.Scene as Level).Transitioning)
			{
				alpha = Calc.Approach(alpha, 0f, Engine.DeltaTime * 4f);
				sprite.Color = Color.White * alpha;
			}
			sprite.Rotation += spin * Calc.ClampedMap(Math.Abs(speed.Y), 50f, 150f) * Engine.DeltaTime;
		}

		public void StopMoving()
		{
			Collidable = false;
		}

		public void StartShaking()
		{
			shaking = true;
		}

		public void ReturnHome(float duration)
		{
			if (base.Scene != null)
			{
				Camera camera = (base.Scene as Level).Camera;
				if (base.X < camera.X)
				{
					base.X = camera.X - 8f;
				}
				if (base.Y < camera.Y)
				{
					base.Y = camera.Y - 8f;
				}
				if (base.X > camera.X + 320f)
				{
					base.X = camera.X + 320f + 8f;
				}
				if (base.Y > camera.Y + 180f)
				{
					base.Y = camera.Y + 180f + 8f;
				}
			}
			returning = true;
			returnEase = 0f;
			returnDuration = duration;
			Vector2 vector = (home - Position).SafeNormalize();
			Vector2 control = (Position + home) / 2f + new Vector2(vector.Y, 0f - vector.X) * (Calc.Random.NextFloat(16f) + 16f) * Calc.Random.Facing();
			returnCurve = new SimpleCurve(Position, home, control);
		}
	}

	[CompilerGenerated]
	private sealed class _003CController_003Ed__45 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoveBlock _003C_003E4__this;

		private float _003CcrashTimer_003E5__2;

		private float _003CcrashResetTimer_003E5__3;

		private float _003CnoSteerTimer_003E5__4;

		private List<Debris> _003Cdebris_003E5__5;

		private Coroutine _003Croutine_003E5__6;

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
		public _003CController_003Ed__45(int _003C_003E1__state)
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
			MoveBlock moveBlock = _003C_003E4__this;
			FMOD.Studio.EventInstance instance;
			Vector2 vector;
			Vector2 vec;
			bool flag2;
			Level level;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0041;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0068;
			case 2:
				_003C_003E1__state = -1;
				moveBlock.targetSpeed = (moveBlock.fast ? 75f : 60f);
				moveBlock.moveSfx.Play("event:/game/04_cliffside/arrowblock_move");
				moveBlock.moveSfx.Param("arrow_stop", 0f);
				moveBlock.StopPlayerRunIntoAnimation = false;
				_003CcrashTimer_003E5__2 = 0.15f;
				_003CcrashResetTimer_003E5__3 = 0.1f;
				_003CnoSteerTimer_003E5__4 = 0.2f;
				goto IL_0130;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0130;
			case 4:
			{
				_003C_003E1__state = -1;
				moveBlock.BreakParticles();
				_003Cdebris_003E5__5 = new List<Debris>();
				for (int i = 0; (float)i < moveBlock.Width; i += 8)
				{
					for (int j = 0; (float)j < moveBlock.Height; j += 8)
					{
						Vector2 vector2 = new Vector2((float)i + 4f, (float)j + 4f);
						Debris debris = Engine.Pooler.Create<Debris>().Init(moveBlock.Position + vector2, moveBlock.Center, moveBlock.startPosition + vector2);
						_003Cdebris_003E5__5.Add(debris);
						moveBlock.Scene.Add(debris);
					}
				}
				moveBlock.MoveStaticMovers(moveBlock.startPosition - moveBlock.Position);
				moveBlock.DisableStaticMovers();
				moveBlock.Position = moveBlock.startPosition;
				moveBlock.Visible = (moveBlock.Collidable = false);
				_003C_003E2__current = 2.2f;
				_003C_003E1__state = 5;
				return true;
			}
			case 5:
				_003C_003E1__state = -1;
				foreach (Debris item in _003Cdebris_003E5__5)
				{
					item.StopMoving();
				}
				goto IL_06be;
			case 6:
				_003C_003E1__state = -1;
				goto IL_06be;
			case 7:
				_003C_003E1__state = -1;
				foreach (Debris item2 in _003Cdebris_003E5__5)
				{
					item2.ReturnHome(0.65f);
				}
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				{
					_003C_003E1__state = -1;
					_003Croutine_003E5__6.RemoveSelf();
					foreach (Debris item3 in _003Cdebris_003E5__5)
					{
						item3.RemoveSelf();
					}
					_003Croutine_003E5__6 = null;
					Audio.Play("event:/game/04_cliffside/arrowblock_reappear", moveBlock.Position);
					moveBlock.Visible = true;
					moveBlock.EnableStaticMovers();
					moveBlock.speed = (moveBlock.targetSpeed = 0f);
					moveBlock.angle = (moveBlock.targetAngle = moveBlock.homeAngle);
					moveBlock.noSquish = null;
					moveBlock.fillColor = idleBgFill;
					moveBlock.UpdateColors();
					moveBlock.flash = 1f;
					_003Cdebris_003E5__5 = null;
					goto IL_0041;
				}
				IL_06be:
				if (moveBlock.CollideCheck<Actor>() || moveBlock.CollideCheck<Solid>())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				moveBlock.Collidable = true;
				instance = Audio.Play("event:/game/04_cliffside/arrowblock_reform_begin", _003Cdebris_003E5__5[0].Position);
				moveBlock.Add(_003Croutine_003E5__6 = new Coroutine(moveBlock.SoundFollowsDebrisCenter(instance, _003Cdebris_003E5__5)));
				foreach (Debris item4 in _003Cdebris_003E5__5)
				{
					item4.StartShaking();
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 7;
				return true;
				IL_04ed:
				Audio.Play("event:/game/04_cliffside/arrowblock_break", moveBlock.Position);
				moveBlock.moveSfx.Stop();
				moveBlock.state = MovementState.Breaking;
				moveBlock.speed = (moveBlock.targetSpeed = 0f);
				moveBlock.angle = (moveBlock.targetAngle = moveBlock.homeAngle);
				moveBlock.StartShaking(0.2f);
				moveBlock.StopPlayerRunIntoAnimation = true;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
				IL_0130:
				if (moveBlock.canSteer)
				{
					moveBlock.targetAngle = moveBlock.homeAngle;
					bool flag = ((moveBlock.direction != Directions.Right && moveBlock.direction != Directions.Left) ? moveBlock.HasPlayerClimbing() : moveBlock.HasPlayerOnTop());
					if (flag && _003CnoSteerTimer_003E5__4 > 0f)
					{
						_003CnoSteerTimer_003E5__4 -= Engine.DeltaTime;
					}
					if (flag)
					{
						if (_003CnoSteerTimer_003E5__4 <= 0f)
						{
							if (moveBlock.direction == Directions.Right || moveBlock.direction == Directions.Left)
							{
								moveBlock.targetAngle = moveBlock.homeAngle + (float)Math.PI / 4f * (float)moveBlock.angleSteerSign * (float)Input.MoveY.Value;
							}
							else
							{
								moveBlock.targetAngle = moveBlock.homeAngle + (float)Math.PI / 4f * (float)moveBlock.angleSteerSign * (float)Input.MoveX.Value;
							}
						}
					}
					else
					{
						_003CnoSteerTimer_003E5__4 = 0.2f;
					}
				}
				if (moveBlock.Scene.OnInterval(0.02f))
				{
					moveBlock.MoveParticles();
				}
				moveBlock.speed = Calc.Approach(moveBlock.speed, moveBlock.targetSpeed, 300f * Engine.DeltaTime);
				moveBlock.angle = Calc.Approach(moveBlock.angle, moveBlock.targetAngle, (float)Math.PI * 16f * Engine.DeltaTime);
				vector = Calc.AngleToVector(moveBlock.angle, moveBlock.speed);
				vec = vector * Engine.DeltaTime;
				flag2 = false;
				if (moveBlock.direction == Directions.Right || moveBlock.direction == Directions.Left)
				{
					flag2 = moveBlock.MoveCheck(vec.XComp());
					moveBlock.noSquish = moveBlock.Scene.Tracker.GetEntity<Player>();
					moveBlock.MoveVCollideSolids(vec.Y, thruDashBlocks: false);
					moveBlock.noSquish = null;
					moveBlock.LiftSpeed = vector;
					if (moveBlock.Scene.OnInterval(0.03f))
					{
						if (vec.Y > 0f)
						{
							moveBlock.ScrapeParticles(Vector2.UnitY);
						}
						else if (vec.Y < 0f)
						{
							moveBlock.ScrapeParticles(-Vector2.UnitY);
						}
					}
				}
				else
				{
					flag2 = moveBlock.MoveCheck(vec.YComp());
					moveBlock.noSquish = moveBlock.Scene.Tracker.GetEntity<Player>();
					moveBlock.MoveHCollideSolids(vec.X, thruDashBlocks: false);
					moveBlock.noSquish = null;
					moveBlock.LiftSpeed = vector;
					if (moveBlock.Scene.OnInterval(0.03f))
					{
						if (vec.X > 0f)
						{
							moveBlock.ScrapeParticles(Vector2.UnitX);
						}
						else if (vec.X < 0f)
						{
							moveBlock.ScrapeParticles(-Vector2.UnitX);
						}
					}
					if (moveBlock.direction == Directions.Down && moveBlock.Top > (float)(moveBlock.SceneAs<Level>().Bounds.Bottom + 32))
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					moveBlock.moveSfx.Param("arrow_stop", 1f);
					_003CcrashResetTimer_003E5__3 = 0.1f;
					if (!(_003CcrashTimer_003E5__2 > 0f))
					{
						goto IL_04ed;
					}
					_003CcrashTimer_003E5__2 -= Engine.DeltaTime;
				}
				else
				{
					moveBlock.moveSfx.Param("arrow_stop", 0f);
					if (_003CcrashResetTimer_003E5__3 > 0f)
					{
						_003CcrashResetTimer_003E5__3 -= Engine.DeltaTime;
					}
					else
					{
						_003CcrashTimer_003E5__2 = 0.15f;
					}
				}
				level = moveBlock.Scene as Level;
				if (!(moveBlock.Left < (float)level.Bounds.Left) && !(moveBlock.Top < (float)level.Bounds.Top) && !(moveBlock.Right > (float)level.Bounds.Right))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_04ed;
				IL_0068:
				if (!moveBlock.triggered && !moveBlock.HasPlayerRider())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				Audio.Play("event:/game/04_cliffside/arrowblock_activate", moveBlock.Position);
				moveBlock.state = MovementState.Moving;
				moveBlock.StartShaking(0.2f);
				moveBlock.ActivateParticles();
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
				IL_0041:
				moveBlock.triggered = false;
				moveBlock.state = MovementState.Idling;
				goto IL_0068;
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

	[CompilerGenerated]
	private sealed class _003CSoundFollowsDebrisCenter_003Ed__46 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FMOD.Studio.EventInstance instance;

		public List<Debris> debris;

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
		public _003CSoundFollowsDebrisCenter_003Ed__46(int _003C_003E1__state)
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
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
			}
			instance.getPlaybackState(out var state);
			if (state != PLAYBACK_STATE.STOPPED)
			{
				Vector2 zero = Vector2.Zero;
				foreach (Debris item in debris)
				{
					zero += item.Position;
				}
				zero /= (float)debris.Count;
				Audio.Position(instance, zero);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			return false;
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

	public static ParticleType P_Activate;

	public static ParticleType P_Break;

	public static ParticleType P_Move;

	private const float Accel = 300f;

	private const float MoveSpeed = 60f;

	private const float FastMoveSpeed = 75f;

	private const float SteerSpeed = (float)Math.PI * 16f;

	private const float MaxAngle = (float)Math.PI / 4f;

	private const float NoSteerTime = 0.2f;

	private const float CrashTime = 0.15f;

	private const float CrashResetTime = 0.1f;

	private const float RegenTime = 3f;

	private bool canSteer;

	private bool fast;

	private Directions direction;

	private float homeAngle;

	private int angleSteerSign;

	private Vector2 startPosition;

	private MovementState state;

	private bool leftPressed;

	private bool rightPressed;

	private bool topPressed;

	private float speed;

	private float targetSpeed;

	private float angle;

	private float targetAngle;

	private Player noSquish;

	private List<Image> body = new List<Image>();

	private List<Image> topButton = new List<Image>();

	private List<Image> leftButton = new List<Image>();

	private List<Image> rightButton = new List<Image>();

	private List<MTexture> arrows = new List<MTexture>();

	private Border border;

	private Color fillColor = idleBgFill;

	private float flash;

	private SoundSource moveSfx;

	private bool triggered;

	private static readonly Color idleBgFill = Calc.HexToColor("474070");

	private static readonly Color pressedBgFill = Calc.HexToColor("30b335");

	private static readonly Color breakingBgFill = Calc.HexToColor("cc2541");

	private float particleRemainder;

	public MoveBlock(Vector2 position, int width, int height, Directions direction, bool canSteer, bool fast)
		: base(position, width, height, safe: false)
	{
		base.Depth = -1;
		startPosition = position;
		this.canSteer = canSteer;
		this.direction = direction;
		this.fast = fast;
		switch (direction)
		{
		default:
			homeAngle = (targetAngle = (angle = 0f));
			angleSteerSign = 1;
			break;
		case Directions.Left:
			homeAngle = (targetAngle = (angle = (float)Math.PI));
			angleSteerSign = -1;
			break;
		case Directions.Up:
			homeAngle = (targetAngle = (angle = -(float)Math.PI / 2f));
			angleSteerSign = 1;
			break;
		case Directions.Down:
			homeAngle = (targetAngle = (angle = (float)Math.PI / 2f));
			angleSteerSign = -1;
			break;
		}
		int num = width / 8;
		int num2 = height / 8;
		MTexture mTexture = GFX.Game["objects/moveBlock/base"];
		MTexture mTexture2 = GFX.Game["objects/moveBlock/button"];
		if (canSteer && (direction == Directions.Left || direction == Directions.Right))
		{
			for (int i = 0; i < num; i++)
			{
				int num3 = ((i != 0) ? ((i < num - 1) ? 1 : 2) : 0);
				AddImage(mTexture2.GetSubtexture(num3 * 8, 0, 8, 8), new Vector2(i * 8, -4f), 0f, new Vector2(1f, 1f), topButton);
			}
			mTexture = GFX.Game["objects/moveBlock/base_h"];
		}
		else if (canSteer && (direction == Directions.Up || direction == Directions.Down))
		{
			for (int j = 0; j < num2; j++)
			{
				int num4 = ((j != 0) ? ((j < num2 - 1) ? 1 : 2) : 0);
				AddImage(mTexture2.GetSubtexture(num4 * 8, 0, 8, 8), new Vector2(-4f, j * 8), (float)Math.PI / 2f, new Vector2(1f, -1f), leftButton);
				AddImage(mTexture2.GetSubtexture(num4 * 8, 0, 8, 8), new Vector2((num - 1) * 8 + 4, j * 8), (float)Math.PI / 2f, new Vector2(1f, 1f), rightButton);
			}
			mTexture = GFX.Game["objects/moveBlock/base_v"];
		}
		for (int k = 0; k < num; k++)
		{
			for (int l = 0; l < num2; l++)
			{
				int num5 = ((k != 0) ? ((k < num - 1) ? 1 : 2) : 0);
				int num6 = ((l != 0) ? ((l < num2 - 1) ? 1 : 2) : 0);
				AddImage(mTexture.GetSubtexture(num5 * 8, num6 * 8, 8, 8), new Vector2(k, l) * 8f, 0f, new Vector2(1f, 1f), body);
			}
		}
		arrows = GFX.Game.GetAtlasSubtextures("objects/moveBlock/arrow");
		Add(moveSfx = new SoundSource());
		Add(new Coroutine(Controller()));
		UpdateColors();
		Add(new LightOcclude(0.5f));
	}

	public MoveBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Enum("direction", Directions.Left), data.Bool("canSteer", defaultValue: true), data.Bool("fast"))
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		scene.Add(border = new Border(this));
	}

	[IteratorStateMachine(typeof(_003CController_003Ed__45))]
	private IEnumerator Controller()
	{
		while (true)
		{
			triggered = false;
			state = MovementState.Idling;
			while (!triggered && !HasPlayerRider())
			{
				yield return null;
			}
			Audio.Play("event:/game/04_cliffside/arrowblock_activate", Position);
			state = MovementState.Moving;
			StartShaking(0.2f);
			ActivateParticles();
			yield return 0.2f;
			targetSpeed = (fast ? 75f : 60f);
			moveSfx.Play("event:/game/04_cliffside/arrowblock_move");
			moveSfx.Param("arrow_stop", 0f);
			StopPlayerRunIntoAnimation = false;
			float crashTimer = 0.15f;
			float crashResetTimer = 0.1f;
			float noSteerTimer = 0.2f;
			while (true)
			{
				if (canSteer)
				{
					targetAngle = homeAngle;
					bool flag = ((direction != Directions.Right && direction != Directions.Left) ? HasPlayerClimbing() : HasPlayerOnTop());
					if (flag && noSteerTimer > 0f)
					{
						noSteerTimer -= Engine.DeltaTime;
					}
					if (flag)
					{
						if (noSteerTimer <= 0f)
						{
							if (direction == Directions.Right || direction == Directions.Left)
							{
								targetAngle = homeAngle + (float)Math.PI / 4f * (float)angleSteerSign * (float)Input.MoveY.Value;
							}
							else
							{
								targetAngle = homeAngle + (float)Math.PI / 4f * (float)angleSteerSign * (float)Input.MoveX.Value;
							}
						}
					}
					else
					{
						noSteerTimer = 0.2f;
					}
				}
				if (base.Scene.OnInterval(0.02f))
				{
					MoveParticles();
				}
				speed = Calc.Approach(speed, targetSpeed, 300f * Engine.DeltaTime);
				angle = Calc.Approach(angle, targetAngle, (float)Math.PI * 16f * Engine.DeltaTime);
				Vector2 vector = Calc.AngleToVector(angle, speed);
				Vector2 vec = vector * Engine.DeltaTime;
				bool flag2;
				if (direction == Directions.Right || direction == Directions.Left)
				{
					flag2 = MoveCheck(vec.XComp());
					noSquish = base.Scene.Tracker.GetEntity<Player>();
					MoveVCollideSolids(vec.Y, thruDashBlocks: false);
					noSquish = null;
					LiftSpeed = vector;
					if (base.Scene.OnInterval(0.03f))
					{
						if (vec.Y > 0f)
						{
							ScrapeParticles(Vector2.UnitY);
						}
						else if (vec.Y < 0f)
						{
							ScrapeParticles(-Vector2.UnitY);
						}
					}
				}
				else
				{
					flag2 = MoveCheck(vec.YComp());
					noSquish = base.Scene.Tracker.GetEntity<Player>();
					MoveHCollideSolids(vec.X, thruDashBlocks: false);
					noSquish = null;
					LiftSpeed = vector;
					if (base.Scene.OnInterval(0.03f))
					{
						if (vec.X > 0f)
						{
							ScrapeParticles(Vector2.UnitX);
						}
						else if (vec.X < 0f)
						{
							ScrapeParticles(-Vector2.UnitX);
						}
					}
					if (direction == Directions.Down && base.Top > (float)(SceneAs<Level>().Bounds.Bottom + 32))
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					moveSfx.Param("arrow_stop", 1f);
					crashResetTimer = 0.1f;
					if (!(crashTimer > 0f))
					{
						break;
					}
					crashTimer -= Engine.DeltaTime;
				}
				else
				{
					moveSfx.Param("arrow_stop", 0f);
					if (crashResetTimer > 0f)
					{
						crashResetTimer -= Engine.DeltaTime;
					}
					else
					{
						crashTimer = 0.15f;
					}
				}
				Level level = base.Scene as Level;
				if (base.Left < (float)level.Bounds.Left || base.Top < (float)level.Bounds.Top || base.Right > (float)level.Bounds.Right)
				{
					break;
				}
				yield return null;
			}
			Audio.Play("event:/game/04_cliffside/arrowblock_break", Position);
			moveSfx.Stop();
			state = MovementState.Breaking;
			speed = (targetSpeed = 0f);
			angle = (targetAngle = homeAngle);
			StartShaking(0.2f);
			StopPlayerRunIntoAnimation = true;
			yield return 0.2f;
			BreakParticles();
			List<Debris> debris = new List<Debris>();
			for (int i = 0; (float)i < base.Width; i += 8)
			{
				for (int j = 0; (float)j < base.Height; j += 8)
				{
					Vector2 vector2 = new Vector2((float)i + 4f, (float)j + 4f);
					Debris debris2 = Engine.Pooler.Create<Debris>().Init(Position + vector2, base.Center, startPosition + vector2);
					debris.Add(debris2);
					base.Scene.Add(debris2);
				}
			}
			MoveStaticMovers(startPosition - Position);
			DisableStaticMovers();
			Position = startPosition;
			MoveBlock moveBlock = this;
			MoveBlock moveBlock2 = this;
			bool visible = false;
			moveBlock2.Collidable = false;
			moveBlock.Visible = visible;
			yield return 2.2f;
			foreach (Debris item in debris)
			{
				item.StopMoving();
			}
			while (CollideCheck<Actor>() || CollideCheck<Solid>())
			{
				yield return null;
			}
			Collidable = true;
			FMOD.Studio.EventInstance instance = Audio.Play("event:/game/04_cliffside/arrowblock_reform_begin", debris[0].Position);
			MoveBlock moveBlock3 = this;
			Coroutine component;
			Coroutine routine = (component = new Coroutine(SoundFollowsDebrisCenter(instance, debris)));
			moveBlock3.Add(component);
			foreach (Debris item2 in debris)
			{
				item2.StartShaking();
			}
			yield return 0.2f;
			foreach (Debris item3 in debris)
			{
				item3.ReturnHome(0.65f);
			}
			yield return 0.6f;
			routine.RemoveSelf();
			foreach (Debris item4 in debris)
			{
				item4.RemoveSelf();
			}
			Audio.Play("event:/game/04_cliffside/arrowblock_reappear", Position);
			Visible = true;
			EnableStaticMovers();
			speed = (targetSpeed = 0f);
			angle = (targetAngle = homeAngle);
			noSquish = null;
			fillColor = idleBgFill;
			UpdateColors();
			flash = 1f;
		}
	}

	[IteratorStateMachine(typeof(_003CSoundFollowsDebrisCenter_003Ed__46))]
	private IEnumerator SoundFollowsDebrisCenter(FMOD.Studio.EventInstance instance, List<Debris> debris)
	{
		while (true)
		{
			instance.getPlaybackState(out var pLAYBACK_STATE);
			if (pLAYBACK_STATE == PLAYBACK_STATE.STOPPED)
			{
				break;
			}
			Vector2 zero = Vector2.Zero;
			foreach (Debris debri in debris)
			{
				zero += debri.Position;
			}
			zero /= (float)debris.Count;
			Audio.Position(instance, zero);
			yield return null;
		}
	}

	public override void Update()
	{
		base.Update();
		if (canSteer)
		{
			bool flag = (direction == Directions.Up || direction == Directions.Down) && CollideCheck<Player>(Position + new Vector2(-1f, 0f));
			bool flag2 = (direction == Directions.Up || direction == Directions.Down) && CollideCheck<Player>(Position + new Vector2(1f, 0f));
			bool flag3 = (direction == Directions.Left || direction == Directions.Right) && CollideCheck<Player>(Position + new Vector2(0f, -1f));
			foreach (Image item in topButton)
			{
				item.Y = (flag3 ? 2 : 0);
			}
			foreach (Image item2 in leftButton)
			{
				item2.X = (flag ? 2 : 0);
			}
			foreach (Image item3 in rightButton)
			{
				item3.X = base.Width + (float)(flag2 ? (-2) : 0);
			}
			if ((flag && !leftPressed) || (flag3 && !topPressed) || (flag2 && !rightPressed))
			{
				Audio.Play("event:/game/04_cliffside/arrowblock_side_depress", Position);
			}
			if ((!flag && leftPressed) || (!flag3 && topPressed) || (!flag2 && rightPressed))
			{
				Audio.Play("event:/game/04_cliffside/arrowblock_side_release", Position);
			}
			leftPressed = flag;
			rightPressed = flag2;
			topPressed = flag3;
		}
		if (moveSfx != null && moveSfx.Playing)
		{
			int num = (int)Math.Floor((0f - (Calc.AngleToVector(angle, 1f) * new Vector2(-1f, 1f)).Angle() + (float)Math.PI * 2f) % ((float)Math.PI * 2f) / ((float)Math.PI * 2f) * 8f + 0.5f);
			moveSfx.Param("arrow_influence", num + 1);
		}
		border.Visible = Visible;
		flash = Calc.Approach(flash, 0f, Engine.DeltaTime * 5f);
		UpdateColors();
	}

	public override void OnStaticMoverTrigger(StaticMover sm)
	{
		triggered = true;
	}

	public override void MoveHExact(int move)
	{
		if (noSquish != null && ((move < 0 && noSquish.X < base.X) || (move > 0 && noSquish.X > base.X)))
		{
			while (move != 0 && noSquish.CollideCheck<Solid>(noSquish.Position + Vector2.UnitX * move))
			{
				move -= Math.Sign(move);
			}
		}
		base.MoveHExact(move);
	}

	public override void MoveVExact(int move)
	{
		if (noSquish != null && move < 0 && noSquish.Y <= base.Y)
		{
			while (move != 0 && noSquish.CollideCheck<Solid>(noSquish.Position + Vector2.UnitY * move))
			{
				move -= Math.Sign(move);
			}
		}
		base.MoveVExact(move);
	}

	private bool MoveCheck(Vector2 speed)
	{
		if (speed.X != 0f)
		{
			if (MoveHCollideSolids(speed.X, thruDashBlocks: false))
			{
				for (int i = 1; i <= 3; i++)
				{
					for (int num = 1; num >= -1; num -= 2)
					{
						Vector2 vector = new Vector2(Math.Sign(speed.X), i * num);
						if (!CollideCheck<Solid>(Position + vector))
						{
							MoveVExact(i * num);
							MoveHExact(Math.Sign(speed.X));
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}
		if (speed.Y != 0f)
		{
			if (MoveVCollideSolids(speed.Y, thruDashBlocks: false))
			{
				for (int j = 1; j <= 3; j++)
				{
					for (int num2 = 1; num2 >= -1; num2 -= 2)
					{
						Vector2 vector2 = new Vector2(j * num2, Math.Sign(speed.Y));
						if (!CollideCheck<Solid>(Position + vector2))
						{
							MoveHExact(j * num2);
							MoveVExact(Math.Sign(speed.Y));
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}
		return false;
	}

	private void UpdateColors()
	{
		Color value = idleBgFill;
		if (state == MovementState.Moving)
		{
			value = pressedBgFill;
		}
		else if (state == MovementState.Breaking)
		{
			value = breakingBgFill;
		}
		fillColor = Color.Lerp(fillColor, value, 10f * Engine.DeltaTime);
		foreach (Image item in topButton)
		{
			item.Color = fillColor;
		}
		foreach (Image item2 in leftButton)
		{
			item2.Color = fillColor;
		}
		foreach (Image item3 in rightButton)
		{
			item3.Color = fillColor;
		}
	}

	private void AddImage(MTexture tex, Vector2 position, float rotation, Vector2 scale, List<Image> addTo)
	{
		Image image = new Image(tex);
		image.Position = position + new Vector2(4f, 4f);
		image.CenterOrigin();
		image.Rotation = rotation;
		image.Scale = scale;
		Add(image);
		addTo?.Add(image);
	}

	private void SetVisible(List<Image> images, bool visible)
	{
		foreach (Image image in images)
		{
			image.Visible = visible;
		}
	}

	public override void Render()
	{
		Vector2 position = Position;
		Position += base.Shake;
		foreach (Image item in leftButton)
		{
			item.Render();
		}
		foreach (Image item2 in rightButton)
		{
			item2.Render();
		}
		foreach (Image item3 in topButton)
		{
			item3.Render();
		}
		Draw.Rect(base.X + 3f, base.Y + 3f, base.Width - 6f, base.Height - 6f, fillColor);
		foreach (Image item4 in body)
		{
			item4.Render();
		}
		Draw.Rect(base.Center.X - 4f, base.Center.Y - 4f, 8f, 8f, fillColor);
		if (state != MovementState.Breaking)
		{
			int value = (int)Math.Floor((0f - angle + (float)Math.PI * 2f) % ((float)Math.PI * 2f) / ((float)Math.PI * 2f) * 8f + 0.5f);
			arrows[Calc.Clamp(value, 0, 7)].DrawCentered(base.Center);
		}
		else
		{
			GFX.Game["objects/moveBlock/x"].DrawCentered(base.Center);
		}
		float num = flash * 4f;
		Draw.Rect(base.X - num, base.Y - num, base.Width + num * 2f, base.Height + num * 2f, Color.White * flash);
		Position = position;
	}

	private void ActivateParticles()
	{
		bool flag = direction == Directions.Down || direction == Directions.Up;
		bool num = (!canSteer || !flag) && !CollideCheck<Player>(Position - Vector2.UnitX);
		bool flag2 = (!canSteer || !flag) && !CollideCheck<Player>(Position + Vector2.UnitX);
		bool flag3 = (!canSteer || flag) && !CollideCheck<Player>(Position - Vector2.UnitY);
		if (num)
		{
			SceneAs<Level>().ParticlesBG.Emit(P_Activate, (int)(base.Height / 2f), base.CenterLeft, Vector2.UnitY * (base.Height - 4f) * 0.5f, (float)Math.PI);
		}
		if (flag2)
		{
			SceneAs<Level>().ParticlesBG.Emit(P_Activate, (int)(base.Height / 2f), base.CenterRight, Vector2.UnitY * (base.Height - 4f) * 0.5f, 0f);
		}
		if (flag3)
		{
			SceneAs<Level>().ParticlesBG.Emit(P_Activate, (int)(base.Width / 2f), base.TopCenter, Vector2.UnitX * (base.Width - 4f) * 0.5f, -(float)Math.PI / 2f);
		}
		SceneAs<Level>().ParticlesBG.Emit(P_Activate, (int)(base.Width / 2f), base.BottomCenter, Vector2.UnitX * (base.Width - 4f) * 0.5f, (float)Math.PI / 2f);
	}

	private void BreakParticles()
	{
		Vector2 center = base.Center;
		for (int i = 0; (float)i < base.Width; i += 4)
		{
			for (int j = 0; (float)j < base.Height; j += 4)
			{
				Vector2 vector = Position + new Vector2(2 + i, 2 + j);
				SceneAs<Level>().Particles.Emit(P_Break, 1, vector, Vector2.One * 2f, (vector - center).Angle());
			}
		}
	}

	private void MoveParticles()
	{
		Vector2 position;
		Vector2 positionRange;
		float num;
		float num2;
		if (direction == Directions.Right)
		{
			position = base.CenterLeft + Vector2.UnitX;
			positionRange = Vector2.UnitY * (base.Height - 4f);
			num = (float)Math.PI;
			num2 = base.Height / 32f;
		}
		else if (direction == Directions.Left)
		{
			position = base.CenterRight;
			positionRange = Vector2.UnitY * (base.Height - 4f);
			num = 0f;
			num2 = base.Height / 32f;
		}
		else if (direction == Directions.Down)
		{
			position = base.TopCenter + Vector2.UnitY;
			positionRange = Vector2.UnitX * (base.Width - 4f);
			num = -(float)Math.PI / 2f;
			num2 = base.Width / 32f;
		}
		else
		{
			position = base.BottomCenter;
			positionRange = Vector2.UnitX * (base.Width - 4f);
			num = (float)Math.PI / 2f;
			num2 = base.Width / 32f;
		}
		particleRemainder += num2;
		int num3 = (int)particleRemainder;
		particleRemainder -= num3;
		positionRange *= 0.5f;
		if (num3 > 0)
		{
			SceneAs<Level>().ParticlesBG.Emit(P_Move, num3, position, positionRange, num);
		}
	}

	private void ScrapeParticles(Vector2 dir)
	{
		_ = Collidable;
		Collidable = false;
		if (dir.X != 0f)
		{
			float x = ((!(dir.X > 0f)) ? (base.Left - 1f) : base.Right);
			for (int i = 0; (float)i < base.Height; i += 8)
			{
				Vector2 vector = new Vector2(x, base.Top + 4f + (float)i);
				if (base.Scene.CollideCheck<Solid>(vector))
				{
					SceneAs<Level>().ParticlesFG.Emit(ZipMover.P_Scrape, vector);
				}
			}
		}
		else
		{
			float y = ((!(dir.Y > 0f)) ? (base.Top - 1f) : base.Bottom);
			for (int j = 0; (float)j < base.Width; j += 8)
			{
				Vector2 vector2 = new Vector2(base.Left + 4f + (float)j, y);
				if (base.Scene.CollideCheck<Solid>(vector2))
				{
					SceneAs<Level>().ParticlesFG.Emit(ZipMover.P_Scrape, vector2);
				}
			}
		}
		Collidable = true;
	}
}
