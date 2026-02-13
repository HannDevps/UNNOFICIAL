using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC : Entity
{
	[CompilerGenerated]
	private sealed class _003CPlayerApproachRightSide_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC _003C_003E4__this;

		public Player player;

		public bool turnToFace;

		public float? spacing;

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
		public _003CPlayerApproachRightSide_003Ed__21(int _003C_003E1__state)
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
			NPC nPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC.PlayerApproach(player, turnToFace, spacing, 1);
				_003C_003E1__state = 1;
				return true;
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
	private sealed class _003CPlayerApproachLeftSide_003Ed__22 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC _003C_003E4__this;

		public Player player;

		public bool turnToFace;

		public float? spacing;

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
		public _003CPlayerApproachLeftSide_003Ed__22(int _003C_003E1__state)
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
			NPC nPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC.PlayerApproach(player, turnToFace, spacing, -1);
				_003C_003E1__state = 1;
				return true;
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
	private sealed class _003CPlayerApproach_003Ed__23 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int? side;

		public Player player;

		public NPC _003C_003E4__this;

		public float? spacing;

		public bool turnToFace;

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
		public _003CPlayerApproach_003Ed__23(int _003C_003E1__state)
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
			NPC nPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (!side.HasValue)
				{
					side = Math.Sign(player.X - nPC.X);
				}
				if (side == 0)
				{
					side = 1;
				}
				player.StateMachine.State = 11;
				player.StateMachine.Locked = true;
				if (spacing.HasValue)
				{
					_003C_003E2__current = player.DummyWalkToExact((int)(nPC.X + (float?)side * spacing).Value);
					_003C_003E1__state = 1;
					return true;
				}
				if (Math.Abs(nPC.X - player.X) < 12f || Math.Sign(player.X - nPC.X) != side.Value)
				{
					_003C_003E2__current = player.DummyWalkToExact((int)(nPC.X + (float?)(side * 12)).Value);
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_0252;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0252;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0252;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0252:
				player.Facing = (Facings)(-side.Value);
				if (turnToFace && nPC.Sprite != null)
				{
					nPC.Sprite.Scale.X = side.Value;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
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
	private sealed class _003CPlayerApproach48px_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC _003C_003E4__this;

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
		public _003CPlayerApproach48px_003Ed__24(int _003C_003E1__state)
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
			NPC nPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Player entity = nPC.Scene.Tracker.GetEntity<Player>();
				_003C_003E2__current = nPC.PlayerApproach(entity, turnToFace: true, 48f);
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
	private sealed class _003CPlayerLeave_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float? to;

		public Player player;

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
		public _003CPlayerLeave_003Ed__25(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (to.HasValue)
				{
					_003C_003E2__current = player.DummyWalkToExact((int)to.Value);
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0063;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0063;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0063:
				player.StateMachine.Locked = false;
				player.StateMachine.State = 0;
				_003C_003E2__current = null;
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
	private sealed class _003CMoveTo_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool removeAtEnd;

		public NPC _003C_003E4__this;

		public Vector2 target;

		public bool fadeIn;

		public int? turnAtEndTo;

		private float _003Calpha_003E5__2;

		private float _003Cspeed_003E5__3;

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
		public _003CMoveTo_003Ed__26(int _003C_003E1__state)
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
			NPC nPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (removeAtEnd)
				{
					nPC.Tag |= Tags.TransitionUpdate;
				}
				if (Math.Sign(target.X - nPC.X) != 0 && nPC.Sprite != null)
				{
					nPC.Sprite.Scale.X = Math.Sign(target.X - nPC.X);
				}
				(target - nPC.Position).SafeNormalize();
				_003Calpha_003E5__2 = (fadeIn ? 0f : 1f);
				if (nPC.Sprite != null && nPC.Sprite.Has(nPC.MoveAnim))
				{
					nPC.Sprite.Play(nPC.MoveAnim);
				}
				_003Cspeed_003E5__3 = 0f;
				goto IL_01d0;
			case 1:
				_003C_003E1__state = -1;
				goto IL_01d0;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0291;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0291:
				if (_003Calpha_003E5__2 < 1f)
				{
					if (nPC.Sprite != null)
					{
						nPC.Sprite.Color = Color.White * _003Calpha_003E5__2;
					}
					_003Calpha_003E5__2 = Calc.Approach(_003Calpha_003E5__2, 1f, Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				if (turnAtEndTo.HasValue && nPC.Sprite != null)
				{
					nPC.Sprite.Scale.X = turnAtEndTo.Value;
				}
				if (removeAtEnd)
				{
					nPC.Scene.Remove(nPC);
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
				IL_01d0:
				if ((nPC.MoveY && nPC.Position != target) || (!nPC.MoveY && nPC.X != target.X))
				{
					_003Cspeed_003E5__3 = Calc.Approach(_003Cspeed_003E5__3, nPC.Maxspeed, 160f * Engine.DeltaTime);
					if (nPC.MoveY)
					{
						nPC.Position = Calc.Approach(nPC.Position, target, _003Cspeed_003E5__3 * Engine.DeltaTime);
					}
					else
					{
						nPC.X = Calc.Approach(nPC.X, target.X, _003Cspeed_003E5__3 * Engine.DeltaTime);
					}
					if (nPC.Sprite != null)
					{
						nPC.Sprite.Color = Color.White * _003Calpha_003E5__2;
					}
					_003Calpha_003E5__2 = Calc.Approach(_003Calpha_003E5__2, 1f, Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (nPC.Sprite != null && nPC.Sprite.Has(nPC.IdleAnim))
				{
					nPC.Sprite.Play(nPC.IdleAnim);
				}
				goto IL_0291;
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

	public const string MetTheo = "MetTheo";

	public const string TheoKnowsName = "TheoKnowsName";

	public const float TheoMaxSpeed = 48f;

	public Sprite Sprite;

	public TalkComponent Talker;

	public VertexLight Light;

	public Level Level;

	public SoundSource PhoneTapSfx;

	public float Maxspeed = 80f;

	public string MoveAnim = "";

	public string IdleAnim = "";

	public bool MoveY = true;

	public bool UpdateLight = true;

	private List<Entity> temp = new List<Entity>();

	public Session Session => Level.Session;

	public NPC(Vector2 position)
	{
		Position = position;
		base.Depth = 1000;
		base.Collider = new Hitbox(8f, 8f, -4f, -8f);
		Add(new MirrorReflection());
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Level = scene as Level;
	}

	public override void Update()
	{
		base.Update();
		if (UpdateLight && Light != null)
		{
			Rectangle bounds = Level.Bounds;
			bool flag = base.X > (float)(bounds.Left - 16) && base.Y > (float)(bounds.Top - 16) && base.X < (float)(bounds.Right + 16) && base.Y < (float)(bounds.Bottom + 16);
			Light.Alpha = Calc.Approach(Light.Alpha, (flag && !Level.Transitioning) ? 1 : 0, Engine.DeltaTime * 2f);
		}
		if (Sprite != null && Sprite.CurrentAnimationID == "usePhone")
		{
			if (PhoneTapSfx == null)
			{
				Add(PhoneTapSfx = new SoundSource());
			}
			if (!PhoneTapSfx.Playing)
			{
				PhoneTapSfx.Play("event:/char/theo/phone_taps_loop");
			}
		}
		else if (PhoneTapSfx != null && PhoneTapSfx.Playing)
		{
			PhoneTapSfx.Stop();
		}
	}

	public void SetupTheoSpriteSounds()
	{
		Sprite.OnFrameChange = delegate(string anim)
		{
			int currentAnimationFrame = Sprite.CurrentAnimationFrame;
			if ((anim == "walk" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "run" && (currentAnimationFrame == 0 || currentAnimationFrame == 4)))
			{
				Platform platformByPriority = SurfaceIndex.GetPlatformByPriority(CollideAll<Platform>(Position + Vector2.UnitY, temp));
				if (platformByPriority != null)
				{
					Audio.Play("event:/char/madeline/footstep", base.Center, "surface_index", platformByPriority.GetStepSoundIndex(this));
				}
			}
			else if (anim == "crawl" && currentAnimationFrame == 0)
			{
				if (!Level.Transitioning)
				{
					Audio.Play("event:/char/theo/resort_crawl", Position);
				}
			}
			else if (anim == "pullVent" && currentAnimationFrame == 0)
			{
				Audio.Play("event:/char/theo/resort_vent_tug", Position);
			}
		};
	}

	public void SetupGrannySpriteSounds()
	{
		Sprite.OnFrameChange = delegate(string anim)
		{
			int currentAnimationFrame = Sprite.CurrentAnimationFrame;
			if (anim == "walk" && (currentAnimationFrame == 0 || currentAnimationFrame == 4))
			{
				Platform platformByPriority = SurfaceIndex.GetPlatformByPriority(CollideAll<Platform>(Position + Vector2.UnitY, temp));
				if (platformByPriority != null)
				{
					Audio.Play("event:/char/madeline/footstep", base.Center, "surface_index", platformByPriority.GetStepSoundIndex(this));
				}
			}
			else if (anim == "walk" && currentAnimationFrame == 2)
			{
				Audio.Play("event:/char/granny/cane_tap", Position);
			}
		};
	}

	[IteratorStateMachine(typeof(_003CPlayerApproachRightSide_003Ed__21))]
	public IEnumerator PlayerApproachRightSide(Player player, bool turnToFace = true, float? spacing = null)
	{
		yield return PlayerApproach(player, turnToFace, spacing, 1);
	}

	[IteratorStateMachine(typeof(_003CPlayerApproachLeftSide_003Ed__22))]
	public IEnumerator PlayerApproachLeftSide(Player player, bool turnToFace = true, float? spacing = null)
	{
		yield return PlayerApproach(player, turnToFace, spacing, -1);
	}

	[IteratorStateMachine(typeof(_003CPlayerApproach_003Ed__23))]
	public IEnumerator PlayerApproach(Player player, bool turnToFace = true, float? spacing = null, int? side = null)
	{
		if (!side.HasValue)
		{
			side = Math.Sign(player.X - base.X);
		}
		if (side == 0)
		{
			side = 1;
		}
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		if (spacing.HasValue)
		{
			yield return player.DummyWalkToExact((int)(base.X + (float?)side * spacing).Value);
		}
		else if (Math.Abs(base.X - player.X) < 12f || Math.Sign(player.X - base.X) != side.Value)
		{
			yield return player.DummyWalkToExact((int)(base.X + (float?)(side * 12)).Value);
		}
		player.Facing = (Facings)(-side.Value);
		if (turnToFace && Sprite != null)
		{
			Sprite.Scale.X = side.Value;
		}
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CPlayerApproach48px_003Ed__24))]
	public IEnumerator PlayerApproach48px()
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		yield return PlayerApproach(entity, turnToFace: true, 48f);
	}

	[IteratorStateMachine(typeof(_003CPlayerLeave_003Ed__25))]
	public IEnumerator PlayerLeave(Player player, float? to = null)
	{
		if (to.HasValue)
		{
			yield return player.DummyWalkToExact((int)to.Value);
		}
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CMoveTo_003Ed__26))]
	public IEnumerator MoveTo(Vector2 target, bool fadeIn = false, int? turnAtEndTo = null, bool removeAtEnd = false)
	{
		if (removeAtEnd)
		{
			base.Tag |= Tags.TransitionUpdate;
		}
		if (Math.Sign(target.X - base.X) != 0 && Sprite != null)
		{
			Sprite.Scale.X = Math.Sign(target.X - base.X);
		}
		(target - Position).SafeNormalize();
		float alpha = (fadeIn ? 0f : 1f);
		if (Sprite != null && Sprite.Has(MoveAnim))
		{
			Sprite.Play(MoveAnim);
		}
		float speed = 0f;
		while ((MoveY && Position != target) || (!MoveY && base.X != target.X))
		{
			speed = Calc.Approach(speed, Maxspeed, 160f * Engine.DeltaTime);
			if (MoveY)
			{
				Position = Calc.Approach(Position, target, speed * Engine.DeltaTime);
			}
			else
			{
				base.X = Calc.Approach(base.X, target.X, speed * Engine.DeltaTime);
			}
			if (Sprite != null)
			{
				Sprite.Color = Color.White * alpha;
			}
			alpha = Calc.Approach(alpha, 1f, Engine.DeltaTime);
			yield return null;
		}
		if (Sprite != null && Sprite.Has(IdleAnim))
		{
			Sprite.Play(IdleAnim);
		}
		while (alpha < 1f)
		{
			if (Sprite != null)
			{
				Sprite.Color = Color.White * alpha;
			}
			alpha = Calc.Approach(alpha, 1f, Engine.DeltaTime);
			yield return null;
		}
		if (turnAtEndTo.HasValue && Sprite != null)
		{
			Sprite.Scale.X = turnAtEndTo.Value;
		}
		if (removeAtEnd)
		{
			base.Scene.Remove(this);
		}
		yield return null;
	}

	public void MoveToAndRemove(Vector2 target)
	{
		Add(new Coroutine(MoveTo(target, fadeIn: false, null, removeAtEnd: true)));
	}
}
