using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class BadelineOldsite : Entity
{
	[CompilerGenerated]
	private sealed class _003CStartChasingRoutine_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineOldsite _003C_003E4__this;

		public Level level;

		private Vector2 _003Cto_003E5__2;

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
		public _003CStartChasingRoutine_003Ed__18(int _003C_003E1__state)
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
			BadelineOldsite badelineOldsite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				badelineOldsite.Hovering = true;
				goto IL_004d;
			case 1:
				_003C_003E1__state = -1;
				goto IL_004d;
			case 2:
				_003C_003E1__state = -1;
				if (!badelineOldsite.Visible)
				{
					badelineOldsite.PopIntoExistance(0.5f);
				}
				badelineOldsite.Sprite.Play("fallSlow");
				badelineOldsite.Hair.Visible = true;
				badelineOldsite.Hovering = false;
				if (level.Session.Area.Mode == AreaMode.Normal)
				{
					level.Session.Audio.Music.Event = "event:/music/lvl2/chase";
					level.Session.Audio.Apply();
				}
				_003C_003E2__current = badelineOldsite.TweenToPlayer(_003Cto_003E5__2);
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					badelineOldsite.Collidable = true;
					badelineOldsite.following = true;
					badelineOldsite.Add(badelineOldsite.occlude = new LightOcclude());
					if (level.Session.Level == "2")
					{
						badelineOldsite.Add(new Coroutine(badelineOldsite.StopChasing()));
					}
					return false;
				}
				IL_004d:
				if ((badelineOldsite.player = badelineOldsite.Scene.Tracker.GetEntity<Player>()) == null || badelineOldsite.player.JustRespawned)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003Cto_003E5__2 = badelineOldsite.player.Position;
				_003C_003E2__current = badelineOldsite.followBehindIndexDelay;
				_003C_003E1__state = 2;
				return true;
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
	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public BadelineOldsite _003C_003E4__this;

		public Vector2 from;

		public Vector2 to;

		internal void _003CTweenToPlayer_003Eb__0(Tween t)
		{
			_003C_003E4__this.Position = Vector2.Lerp(from, to, t.Eased);
			if (to.X != from.X)
			{
				_003C_003E4__this.Sprite.Scale.X = Math.Abs(_003C_003E4__this.Sprite.Scale.X) * (float)Math.Sign(to.X - from.X);
			}
			_003C_003E4__this.Trail();
		}
	}

	[CompilerGenerated]
	private sealed class _003CTweenToPlayer_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineOldsite _003C_003E4__this;

		public Vector2 to;

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
		public _003CTweenToPlayer_003Ed__19(int _003C_003E1__state)
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
			BadelineOldsite badelineOldsite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass19_0 CS_0024_003C_003E8__locals11 = new _003C_003Ec__DisplayClass19_0
				{
					_003C_003E4__this = _003C_003E4__this,
					to = to
				};
				Audio.Play("event:/char/badeline/level_entry", badelineOldsite.Position, "chaser_count", badelineOldsite.index);
				CS_0024_003C_003E8__locals11.from = badelineOldsite.Position;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, badelineOldsite.followBehindTime - 0.1f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals11._003C_003E4__this.Position = Vector2.Lerp(CS_0024_003C_003E8__locals11.from, CS_0024_003C_003E8__locals11.to, t.Eased);
					if (CS_0024_003C_003E8__locals11.to.X != CS_0024_003C_003E8__locals11.from.X)
					{
						CS_0024_003C_003E8__locals11._003C_003E4__this.Sprite.Scale.X = Math.Abs(CS_0024_003C_003E8__locals11._003C_003E4__this.Sprite.Scale.X) * (float)Math.Sign(CS_0024_003C_003E8__locals11.to.X - CS_0024_003C_003E8__locals11.from.X);
					}
					CS_0024_003C_003E8__locals11._003C_003E4__this.Trail();
				};
				badelineOldsite.Add(tween);
				_003C_003E2__current = tween.Duration;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
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

	[CompilerGenerated]
	private sealed class _003CStopChasing_003Ed__20 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineOldsite _003C_003E4__this;

		private Level _003Clevel_003E5__2;

		private int _003CboundsRight_003E5__3;

		private int _003CboundsBottom_003E5__4;

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
		public _003CStopChasing_003Ed__20(int _003C_003E1__state)
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
			BadelineOldsite badelineOldsite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = badelineOldsite.SceneAs<Level>();
				_003CboundsRight_003E5__3 = _003Clevel_003E5__2.Bounds.X + 148;
				_003CboundsBottom_003E5__4 = _003Clevel_003E5__2.Bounds.Y + 168 + 184;
				goto IL_00c4;
			case 1:
				_003C_003E1__state = -1;
				if (badelineOldsite.X > (float)_003CboundsRight_003E5__3)
				{
					badelineOldsite.X = _003CboundsRight_003E5__3;
				}
				if (badelineOldsite.Y > (float)_003CboundsBottom_003E5__4)
				{
					badelineOldsite.Y = _003CboundsBottom_003E5__4;
				}
				goto IL_00c4;
			case 2:
				{
					_003C_003E1__state = -1;
					Audio.Play("event:/char/badeline/disappear", badelineOldsite.Position);
					_003Clevel_003E5__2.Displacement.AddBurst(badelineOldsite.Center, 0.5f, 24f, 96f, 0.4f);
					_003Clevel_003E5__2.Particles.Emit(P_Vanish, 12, badelineOldsite.Center, Vector2.One * 6f);
					badelineOldsite.RemoveSelf();
					return false;
				}
				IL_00c4:
				if (badelineOldsite.X != (float)_003CboundsRight_003E5__3 || badelineOldsite.Y != (float)_003CboundsBottom_003E5__4)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				badelineOldsite.following = false;
				badelineOldsite.ignorePlayerAnim = true;
				badelineOldsite.Sprite.Play("laugh");
				badelineOldsite.Sprite.Scale.X = 1f;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
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

	public static ParticleType P_Vanish;

	public static readonly Color HairColor = Calc.HexToColor("9B3FB5");

	public PlayerSprite Sprite;

	public PlayerHair Hair;

	private LightOcclude occlude;

	private bool ignorePlayerAnim;

	private int index;

	private Player player;

	private bool following;

	private float followBehindTime;

	private float followBehindIndexDelay;

	public bool Hovering;

	private float hoveringTimer;

	private Dictionary<string, SoundSource> loopingSounds = new Dictionary<string, SoundSource>();

	private List<SoundSource> inactiveLoopingSounds = new List<SoundSource>();

	public BadelineOldsite(Vector2 position, int index)
		: base(position)
	{
		this.index = index;
		base.Depth = -1;
		base.Collider = new Hitbox(6f, 6f, -3f, -7f);
		Collidable = false;
		Sprite = new PlayerSprite(PlayerSpriteMode.Badeline);
		Sprite.Play("fallSlow", restart: true);
		Hair = new PlayerHair(Sprite);
		Hair.Color = Color.Lerp(HairColor, Color.White, (float)index / 6f);
		Hair.Border = Color.Black;
		Add(Hair);
		Add(Sprite);
		Visible = false;
		followBehindTime = 1.55f;
		followBehindIndexDelay = 0.4f * (float)index;
		Add(new PlayerCollider(OnPlayer));
	}

	public BadelineOldsite(EntityData data, Vector2 offset, int index)
		: this(data.Position + offset, index)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Session session = SceneAs<Level>().Session;
		if (session.GetLevelFlag("11") && session.Area.Mode == AreaMode.Normal)
		{
			RemoveSelf();
		}
		else if (!session.GetLevelFlag("3") && session.Area.Mode == AreaMode.Normal)
		{
			RemoveSelf();
		}
		else if (!session.GetFlag("evil_maddy_intro") && session.Level == "3" && session.Area.Mode == AreaMode.Normal)
		{
			Hovering = false;
			Visible = true;
			Hair.Visible = false;
			Sprite.Play("pretendDead");
			if (session.Area.Mode == AreaMode.Normal)
			{
				session.Audio.Music.Event = null;
				session.Audio.Apply();
			}
			base.Scene.Add(new CS02_BadelineIntro(this));
		}
		else
		{
			Add(new Coroutine(StartChasingRoutine(base.Scene as Level)));
		}
	}

	[IteratorStateMachine(typeof(_003CStartChasingRoutine_003Ed__18))]
	public IEnumerator StartChasingRoutine(Level level)
	{
		Hovering = true;
		while ((player = base.Scene.Tracker.GetEntity<Player>()) == null || player.JustRespawned)
		{
			yield return null;
		}
		Vector2 to = player.Position;
		yield return followBehindIndexDelay;
		if (!Visible)
		{
			PopIntoExistance(0.5f);
		}
		Sprite.Play("fallSlow");
		Hair.Visible = true;
		Hovering = false;
		if (level.Session.Area.Mode == AreaMode.Normal)
		{
			level.Session.Audio.Music.Event = "event:/music/lvl2/chase";
			level.Session.Audio.Apply();
		}
		yield return TweenToPlayer(to);
		Collidable = true;
		following = true;
		Add(occlude = new LightOcclude());
		if (level.Session.Level == "2")
		{
			Add(new Coroutine(StopChasing()));
		}
	}

	[IteratorStateMachine(typeof(_003CTweenToPlayer_003Ed__19))]
	private IEnumerator TweenToPlayer(Vector2 to)
	{
		Audio.Play("event:/char/badeline/level_entry", Position, "chaser_count", index);
		Vector2 from = Position;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, followBehindTime - 0.1f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			Position = Vector2.Lerp(from, to, t.Eased);
			if (to.X != from.X)
			{
				Sprite.Scale.X = Math.Abs(Sprite.Scale.X) * (float)Math.Sign(to.X - from.X);
			}
			Trail();
		};
		Add(tween);
		yield return tween.Duration;
	}

	[IteratorStateMachine(typeof(_003CStopChasing_003Ed__20))]
	private IEnumerator StopChasing()
	{
		Level level = SceneAs<Level>();
		int boundsRight = level.Bounds.X + 148;
		int boundsBottom = level.Bounds.Y + 168 + 184;
		while (base.X != (float)boundsRight || base.Y != (float)boundsBottom)
		{
			yield return null;
			if (base.X > (float)boundsRight)
			{
				base.X = boundsRight;
			}
			if (base.Y > (float)boundsBottom)
			{
				base.Y = boundsBottom;
			}
		}
		following = false;
		ignorePlayerAnim = true;
		Sprite.Play("laugh");
		Sprite.Scale.X = 1f;
		yield return 1f;
		Audio.Play("event:/char/badeline/disappear", Position);
		level.Displacement.AddBurst(base.Center, 0.5f, 24f, 96f, 0.4f);
		level.Particles.Emit(P_Vanish, 12, base.Center, Vector2.One * 6f);
		RemoveSelf();
	}

	public override void Update()
	{
		Player.ChaserState chaseState;
		if (player != null && player.Dead)
		{
			Sprite.Play("laugh");
			Sprite.X = (float)(Math.Sin(hoveringTimer) * 4.0);
			Hovering = true;
			hoveringTimer += Engine.DeltaTime * 2f;
			base.Depth = -12500;
			foreach (KeyValuePair<string, SoundSource> loopingSound in loopingSounds)
			{
				loopingSound.Value.Stop();
			}
			Trail();
		}
		else if (following && player.GetChasePosition(base.Scene.TimeActive, followBehindTime + followBehindIndexDelay, out chaseState))
		{
			Position = Calc.Approach(Position, chaseState.Position, 500f * Engine.DeltaTime);
			if (!ignorePlayerAnim && chaseState.Animation != Sprite.CurrentAnimationID && chaseState.Animation != null && Sprite.Has(chaseState.Animation))
			{
				Sprite.Play(chaseState.Animation, restart: true);
			}
			if (!ignorePlayerAnim)
			{
				Sprite.Scale.X = Math.Abs(Sprite.Scale.X) * (float)chaseState.Facing;
			}
			for (int i = 0; i < chaseState.Sounds; i++)
			{
				if (chaseState[i].Action == Player.ChaserStateSound.Actions.Oneshot)
				{
					Audio.Play(chaseState[i].Event, Position, chaseState[i].Parameter, chaseState[i].ParameterValue, "chaser_count", index);
				}
				else if (chaseState[i].Action == Player.ChaserStateSound.Actions.Loop && !loopingSounds.ContainsKey(chaseState[i].Event))
				{
					SoundSource soundSource;
					if (inactiveLoopingSounds.Count > 0)
					{
						soundSource = inactiveLoopingSounds[0];
						inactiveLoopingSounds.RemoveAt(0);
					}
					else
					{
						Add(soundSource = new SoundSource());
					}
					soundSource.Play(chaseState[i].Event, "chaser_count", index);
					loopingSounds.Add(chaseState[i].Event, soundSource);
				}
				else if (chaseState[i].Action == Player.ChaserStateSound.Actions.Stop)
				{
					SoundSource value = null;
					if (loopingSounds.TryGetValue(chaseState[i].Event, out value))
					{
						value.Stop();
						loopingSounds.Remove(chaseState[i].Event);
						inactiveLoopingSounds.Add(value);
					}
				}
			}
			base.Depth = chaseState.Depth;
			Trail();
		}
		if (Sprite.Scale.X != 0f)
		{
			Hair.Facing = (Facings)Math.Sign(Sprite.Scale.X);
		}
		if (Hovering)
		{
			hoveringTimer += Engine.DeltaTime;
			Sprite.Y = (float)(Math.Sin(hoveringTimer * 2f) * 4.0);
		}
		else
		{
			Sprite.Y = Calc.Approach(Sprite.Y, 0f, Engine.DeltaTime * 4f);
		}
		if (occlude != null)
		{
			occlude.Visible = !CollideCheck<Solid>();
		}
		base.Update();
	}

	private void Trail()
	{
		if (base.Scene.OnInterval(0.1f))
		{
			TrailManager.Add(this, Player.NormalHairColor);
		}
	}

	private void OnPlayer(Player player)
	{
		player.Die((player.Position - Position).SafeNormalize());
	}

	private void Die()
	{
		RemoveSelf();
	}

	private void PopIntoExistance(float duration)
	{
		Visible = true;
		Sprite.Scale = Vector2.Zero;
		Sprite.Color = Color.Transparent;
		Hair.Visible = true;
		Hair.Alpha = 0f;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, duration, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			Sprite.Scale = Vector2.One * t.Eased;
			Sprite.Color = Color.White * t.Eased;
			Hair.Alpha = t.Eased;
		};
		Add(tween);
	}

	private bool OnGround(int dist = 1)
	{
		for (int i = 1; i <= dist; i++)
		{
			if (CollideCheck<Solid>(Position + new Vector2(0f, i)))
			{
				return true;
			}
		}
		return false;
	}
}
