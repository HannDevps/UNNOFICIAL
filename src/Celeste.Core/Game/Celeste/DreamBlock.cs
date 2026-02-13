using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class DreamBlock : Solid
{
	private struct DreamParticle
	{
		public Vector2 Position;

		public int Layer;

		public Color Color;

		public float TimeOffset;
	}

	[CompilerGenerated]
	private sealed class _003CActivate_003Ed__34 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DreamBlock _003C_003E4__this;

		private Level _003Clevel_003E5__2;

		private float _003Cp_003E5__3;

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
		public _003CActivate_003Ed__34(int _003C_003E1__state)
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
			DreamBlock CS_0024_003C_003E8__locals19 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = CS_0024_003C_003E8__locals19.SceneAs<Level>();
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
				CS_0024_003C_003E8__locals19.Add(CS_0024_003C_003E8__locals19.shaker = new Shaker(on: true, delegate(Vector2 t)
				{
					CS_0024_003C_003E8__locals19.shake = t;
				}));
				CS_0024_003C_003E8__locals19.shaker.Interval = 0.02f;
				CS_0024_003C_003E8__locals19.shaker.On = true;
				_003Cp_003E5__3 = 0f;
				goto IL_00f1;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime;
				goto IL_00f1;
			case 3:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals19.ActivateNoRoutine();
				CS_0024_003C_003E8__locals19.whiteHeight = 1f;
				CS_0024_003C_003E8__locals19.whiteFill = 1f;
				_003Cp_003E5__3 = 1f;
				goto IL_021b;
			case 4:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 -= Engine.DeltaTime * 0.5f;
				goto IL_021b;
			case 5:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_021b:
				if (!(_003Cp_003E5__3 > 0f))
				{
					break;
				}
				CS_0024_003C_003E8__locals19.whiteHeight = _003Cp_003E5__3;
				if (_003Clevel_003E5__2.OnInterval(0.1f))
				{
					for (int i = 0; (float)i < CS_0024_003C_003E8__locals19.Width; i += 4)
					{
						_003Clevel_003E5__2.ParticlesFG.Emit(Strawberry.P_WingsBurst, new Vector2(CS_0024_003C_003E8__locals19.X + (float)i, CS_0024_003C_003E8__locals19.Y + CS_0024_003C_003E8__locals19.Height * CS_0024_003C_003E8__locals19.whiteHeight + 1f));
					}
				}
				if (_003Clevel_003E5__2.OnInterval(0.1f))
				{
					_003Clevel_003E5__2.Shake();
				}
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
				IL_00f1:
				if (_003Cp_003E5__3 < 1f)
				{
					CS_0024_003C_003E8__locals19.whiteFill = Ease.CubeIn(_003Cp_003E5__3);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				CS_0024_003C_003E8__locals19.shaker.On = false;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			}
			if (CS_0024_003C_003E8__locals19.whiteFill > 0f)
			{
				CS_0024_003C_003E8__locals19.whiteFill -= Engine.DeltaTime * 3f;
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
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

	private static readonly Color activeBackColor = Color.Black;

	private static readonly Color disabledBackColor = Calc.HexToColor("1f2e2d");

	private static readonly Color activeLineColor = Color.White;

	private static readonly Color disabledLineColor = Calc.HexToColor("6a8480");

	private bool playerHasDreamDash;

	private Vector2? node;

	private LightOcclude occlude;

	private MTexture[] particleTextures;

	private DreamParticle[] particles;

	private float whiteFill;

	private float whiteHeight = 1f;

	private Vector2 shake;

	private float animTimer;

	private Shaker shaker;

	private bool fastMoving;

	private bool oneUse;

	private float wobbleFrom = Calc.Random.NextFloat((float)Math.PI * 2f);

	private float wobbleTo = Calc.Random.NextFloat((float)Math.PI * 2f);

	private float wobbleEase;

	public DreamBlock(Vector2 position, float width, float height, Vector2? node, bool fastMoving, bool oneUse, bool below)
		: base(position, width, height, safe: true)
	{
		base.Depth = -11000;
		this.node = node;
		this.fastMoving = fastMoving;
		this.oneUse = oneUse;
		if (below)
		{
			base.Depth = 5000;
		}
		SurfaceSoundIndex = 11;
		particleTextures = new MTexture[4]
		{
			GFX.Game["objects/dreamblock/particles"].GetSubtexture(14, 0, 7, 7),
			GFX.Game["objects/dreamblock/particles"].GetSubtexture(7, 0, 7, 7),
			GFX.Game["objects/dreamblock/particles"].GetSubtexture(0, 0, 7, 7),
			GFX.Game["objects/dreamblock/particles"].GetSubtexture(7, 0, 7, 7)
		};
	}

	public DreamBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.FirstNodeNullable(offset), data.Bool("fastMoving"), data.Bool("oneUse"), data.Bool("below"))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		playerHasDreamDash = SceneAs<Level>().Session.Inventory.DreamDash;
		if (playerHasDreamDash && node.HasValue)
		{
			Vector2 start = Position;
			Vector2 end = node.Value;
			float num = Vector2.Distance(start, end) / 12f;
			if (fastMoving)
			{
				num /= 3f;
			}
			Tween tween = Tween.Create(Tween.TweenMode.YoyoLooping, Ease.SineInOut, num, start: true);
			tween.OnUpdate = delegate(Tween t)
			{
				if (Collidable)
				{
					MoveTo(Vector2.Lerp(start, end, t.Eased));
				}
				else
				{
					MoveToNaive(Vector2.Lerp(start, end, t.Eased));
				}
			};
			Add(tween);
		}
		if (!playerHasDreamDash)
		{
			Add(occlude = new LightOcclude());
		}
		Setup();
	}

	public void Setup()
	{
		particles = new DreamParticle[(int)(base.Width / 8f * (base.Height / 8f) * 0.7f)];
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Position = new Vector2(Calc.Random.NextFloat(base.Width), Calc.Random.NextFloat(base.Height));
			particles[i].Layer = Calc.Random.Choose(0, 1, 1, 2, 2, 2);
			particles[i].TimeOffset = Calc.Random.NextFloat();
			particles[i].Color = Color.LightGray * (0.5f + (float)particles[i].Layer / 2f * 0.5f);
			if (playerHasDreamDash)
			{
				switch (particles[i].Layer)
				{
				case 0:
					particles[i].Color = Calc.Random.Choose(Calc.HexToColor("FFEF11"), Calc.HexToColor("FF00D0"), Calc.HexToColor("08a310"));
					break;
				case 1:
					particles[i].Color = Calc.Random.Choose(Calc.HexToColor("5fcde4"), Calc.HexToColor("7fb25e"), Calc.HexToColor("E0564C"));
					break;
				case 2:
					particles[i].Color = Calc.Random.Choose(Calc.HexToColor("5b6ee1"), Calc.HexToColor("CC3B3B"), Calc.HexToColor("7daa64"));
					break;
				}
			}
		}
	}

	public void OnPlayerExit(Player player)
	{
		Dust.Burst(player.Position, player.Speed.Angle(), 16);
		Vector2 vector = Vector2.Zero;
		if (CollideCheck(player, Position + Vector2.UnitX * 4f))
		{
			vector = Vector2.UnitX;
		}
		else if (CollideCheck(player, Position - Vector2.UnitX * 4f))
		{
			vector = -Vector2.UnitX;
		}
		else if (CollideCheck(player, Position + Vector2.UnitY * 4f))
		{
			vector = Vector2.UnitY;
		}
		else if (CollideCheck(player, Position - Vector2.UnitY * 4f))
		{
			vector = -Vector2.UnitY;
		}
		_ = vector != Vector2.Zero;
		if (oneUse)
		{
			OneUseDestroy();
		}
	}

	private void OneUseDestroy()
	{
		Collidable = (Visible = false);
		DisableStaticMovers();
		RemoveSelf();
	}

	public override void Update()
	{
		base.Update();
		if (playerHasDreamDash)
		{
			animTimer += 6f * Engine.DeltaTime;
			wobbleEase += Engine.DeltaTime * 2f;
			if (wobbleEase > 1f)
			{
				wobbleEase = 0f;
				wobbleFrom = wobbleTo;
				wobbleTo = Calc.Random.NextFloat((float)Math.PI * 2f);
			}
			SurfaceSoundIndex = 12;
		}
	}

	public bool BlockedCheck()
	{
		TheoCrystal theoCrystal = CollideFirst<TheoCrystal>();
		if (theoCrystal != null && !TryActorWiggleUp(theoCrystal))
		{
			return true;
		}
		Player player = CollideFirst<Player>();
		if (player != null && !TryActorWiggleUp(player))
		{
			return true;
		}
		return false;
	}

	private bool TryActorWiggleUp(Entity actor)
	{
		bool collidable = Collidable;
		Collidable = true;
		for (int i = 1; i <= 4; i++)
		{
			if (!actor.CollideCheck<Solid>(actor.Position - Vector2.UnitY * i))
			{
				actor.Position -= Vector2.UnitY * i;
				Collidable = collidable;
				return true;
			}
		}
		Collidable = collidable;
		return false;
	}

	public override void Render()
	{
		Camera camera = SceneAs<Level>().Camera;
		if (base.Right < camera.Left || base.Left > camera.Right || base.Bottom < camera.Top || base.Top > camera.Bottom)
		{
			return;
		}
		Draw.Rect(shake.X + base.X, shake.Y + base.Y, base.Width, base.Height, playerHasDreamDash ? activeBackColor : disabledBackColor);
		Vector2 position = SceneAs<Level>().Camera.Position;
		for (int i = 0; i < particles.Length; i++)
		{
			int layer = particles[i].Layer;
			Vector2 position2 = particles[i].Position;
			position2 += position * (0.3f + 0.25f * (float)layer);
			position2 = PutInside(position2);
			Color color = particles[i].Color;
			MTexture mTexture;
			switch (layer)
			{
			case 0:
			{
				int num2 = (int)((particles[i].TimeOffset * 4f + animTimer) % 4f);
				mTexture = particleTextures[3 - num2];
				break;
			}
			case 1:
			{
				int num = (int)((particles[i].TimeOffset * 2f + animTimer) % 2f);
				mTexture = particleTextures[1 + num];
				break;
			}
			default:
				mTexture = particleTextures[2];
				break;
			}
			if (position2.X >= base.X + 2f && position2.Y >= base.Y + 2f && position2.X < base.Right - 2f && position2.Y < base.Bottom - 2f)
			{
				mTexture.DrawCentered(position2 + shake, color);
			}
		}
		if (whiteFill > 0f)
		{
			Draw.Rect(base.X + shake.X, base.Y + shake.Y, base.Width, base.Height * whiteHeight, Color.White * whiteFill);
		}
		WobbleLine(shake + new Vector2(base.X, base.Y), shake + new Vector2(base.X + base.Width, base.Y), 0f);
		WobbleLine(shake + new Vector2(base.X + base.Width, base.Y), shake + new Vector2(base.X + base.Width, base.Y + base.Height), 0.7f);
		WobbleLine(shake + new Vector2(base.X + base.Width, base.Y + base.Height), shake + new Vector2(base.X, base.Y + base.Height), 1.5f);
		WobbleLine(shake + new Vector2(base.X, base.Y + base.Height), shake + new Vector2(base.X, base.Y), 2.5f);
		Draw.Rect(shake + new Vector2(base.X, base.Y), 2f, 2f, playerHasDreamDash ? activeLineColor : disabledLineColor);
		Draw.Rect(shake + new Vector2(base.X + base.Width - 2f, base.Y), 2f, 2f, playerHasDreamDash ? activeLineColor : disabledLineColor);
		Draw.Rect(shake + new Vector2(base.X, base.Y + base.Height - 2f), 2f, 2f, playerHasDreamDash ? activeLineColor : disabledLineColor);
		Draw.Rect(shake + new Vector2(base.X + base.Width - 2f, base.Y + base.Height - 2f), 2f, 2f, playerHasDreamDash ? activeLineColor : disabledLineColor);
	}

	private Vector2 PutInside(Vector2 pos)
	{
		while (pos.X < base.X)
		{
			pos.X += base.Width;
		}
		while (pos.X > base.X + base.Width)
		{
			pos.X -= base.Width;
		}
		while (pos.Y < base.Y)
		{
			pos.Y += base.Height;
		}
		while (pos.Y > base.Y + base.Height)
		{
			pos.Y -= base.Height;
		}
		return pos;
	}

	private void WobbleLine(Vector2 from, Vector2 to, float offset)
	{
		float num = (to - from).Length();
		Vector2 vector = Vector2.Normalize(to - from);
		Vector2 vector2 = new Vector2(vector.Y, 0f - vector.X);
		Color color = (playerHasDreamDash ? activeLineColor : disabledLineColor);
		Color color2 = (playerHasDreamDash ? activeBackColor : disabledBackColor);
		if (whiteFill > 0f)
		{
			color = Color.Lerp(color, Color.White, whiteFill);
			color2 = Color.Lerp(color2, Color.White, whiteFill);
		}
		float num2 = 0f;
		int num3 = 16;
		for (int i = 2; (float)i < num - 2f; i += num3)
		{
			float num4 = Lerp(LineAmplitude(wobbleFrom + offset, i), LineAmplitude(wobbleTo + offset, i), wobbleEase);
			if ((float)(i + num3) >= num)
			{
				num4 = 0f;
			}
			float num5 = Math.Min(num3, num - 2f - (float)i);
			Vector2 vector3 = from + vector * i + vector2 * num2;
			Vector2 vector4 = from + vector * ((float)i + num5) + vector2 * num4;
			Draw.Line(vector3 - vector2, vector4 - vector2, color2);
			Draw.Line(vector3 - vector2 * 2f, vector4 - vector2 * 2f, color2);
			Draw.Line(vector3, vector4, color);
			num2 = num4;
		}
	}

	private float LineAmplitude(float seed, float index)
	{
		return (float)(Math.Sin((double)(seed + index / 16f) + Math.Sin(seed * 2f + index / 32f) * 6.2831854820251465) + 1.0) * 1.5f;
	}

	private float Lerp(float a, float b, float percent)
	{
		return a + (b - a) * percent;
	}

	[IteratorStateMachine(typeof(_003CActivate_003Ed__34))]
	public IEnumerator Activate()
	{
		Level level = SceneAs<Level>();
		yield return 1f;
		Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
		Add(shaker = new Shaker(on: true, delegate(Vector2 t)
		{
			shake = t;
		}));
		shaker.Interval = 0.02f;
		shaker.On = true;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			whiteFill = Ease.CubeIn(p);
			yield return null;
		}
		shaker.On = false;
		yield return 0.5f;
		ActivateNoRoutine();
		whiteHeight = 1f;
		whiteFill = 1f;
		for (float p = 1f; p > 0f; p -= Engine.DeltaTime * 0.5f)
		{
			whiteHeight = p;
			if (level.OnInterval(0.1f))
			{
				for (int num = 0; (float)num < base.Width; num += 4)
				{
					level.ParticlesFG.Emit(Strawberry.P_WingsBurst, new Vector2(base.X + (float)num, base.Y + base.Height * whiteHeight + 1f));
				}
			}
			if (level.OnInterval(0.1f))
			{
				level.Shake();
			}
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
			yield return null;
		}
		while (whiteFill > 0f)
		{
			whiteFill -= Engine.DeltaTime * 3f;
			yield return null;
		}
	}

	public void ActivateNoRoutine()
	{
		if (!playerHasDreamDash)
		{
			playerHasDreamDash = true;
			Setup();
			Remove(occlude);
		}
		whiteHeight = 0f;
		whiteFill = 0f;
		if (shaker != null)
		{
			shaker.On = false;
		}
	}

	public void FootstepRipple(Vector2 position)
	{
		if (playerHasDreamDash)
		{
			DisplacementRenderer.Burst burst = (base.Scene as Level).Displacement.AddBurst(position, 0.5f, 0f, 40f);
			burst.WorldClipCollider = base.Collider;
			burst.WorldClipPadding = 1;
		}
	}
}
