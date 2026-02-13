using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS02_BadelineIntro : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_BadelineIntro _003C_003E4__this;

		public Level level;

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
		public _003CCutscene_003Ed__11(int _003C_003E1__state)
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
			CS02_BadelineIntro cS02_BadelineIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.anxietyFadeTarget = 1f;
				goto IL_0040;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0040;
			case 2:
				_003C_003E1__state = -1;
				goto IL_008e;
			case 3:
				_003C_003E1__state = -1;
				if (level.Session.Area.Mode == AreaMode.Normal)
				{
					Audio.SetMusic("event:/music/lvl2/evil_madeline");
				}
				_003C_003E2__current = Textbox.Say("CH2_BADELINE_INTRO", cS02_BadelineIntro.TurnAround, cS02_BadelineIntro.RevealBadeline, cS02_BadelineIntro.StartLaughing, cS02_BadelineIntro.StopLaughing);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.anxietyFadeTarget = 0f;
				_003C_003E2__current = cS02_BadelineIntro.Level.ZoomBack(0.5f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					cS02_BadelineIntro.EndCutscene(level);
					return false;
				}
				IL_008e:
				if (!cS02_BadelineIntro.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				cS02_BadelineIntro.player.StateMachine.State = 11;
				cS02_BadelineIntro.player.StateMachine.Locked = true;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
				IL_0040:
				cS02_BadelineIntro.player = level.Tracker.GetEntity<Player>();
				if (cS02_BadelineIntro.player == null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_008e;
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
	private sealed class _003CTurnAround_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_BadelineIntro _003C_003E4__this;

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
		public _003CTurnAround_003Ed__12(int _003C_003E1__state)
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
			CS02_BadelineIntro cS02_BadelineIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.player.Facing = Facings.Left;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(cS02_BadelineIntro.Level.Bounds.X, cS02_BadelineIntro.Level.Camera.Y), 0.5f)));
				_003C_003E2__current = cS02_BadelineIntro.Level.ZoomTo(new Vector2(84f, 135f), 2f, 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003CRevealBadeline_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_BadelineIntro _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

		private Vector2 _003Cto_003E5__3;

		private float _003Ct_003E5__4;

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
		public _003CRevealBadeline_003Ed__13(int _003C_003E1__state)
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
			CS02_BadelineIntro cS02_BadelineIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/02_old_site/sequence_badeline_intro", cS02_BadelineIntro.badeline.Position);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.Level.Displacement.AddBurst(cS02_BadelineIntro.badeline.Position + new Vector2(0f, -4f), 0.8f, 8f, 48f, 0.5f);
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.badeline.Hovering = true;
				cS02_BadelineIntro.badeline.Hair.Visible = true;
				cS02_BadelineIntro.badeline.Sprite.Play("fallSlow");
				_003Cfrom_003E5__2 = cS02_BadelineIntro.badeline.Position;
				_003Cto_003E5__3 = cS02_BadelineIntro.badelineEndPosition;
				_003Ct_003E5__4 = 0f;
				goto IL_0197;
			case 3:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 += Engine.DeltaTime;
				goto IL_0197;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0197:
				if (_003Ct_003E5__4 < 1f)
				{
					cS02_BadelineIntro.badeline.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeInOut(_003Ct_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				cS02_BadelineIntro.player.Facing = (Facings)Math.Sign(cS02_BadelineIntro.badeline.X - cS02_BadelineIntro.player.X);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
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
	private sealed class _003CStartLaughing_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_BadelineIntro _003C_003E4__this;

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
		public _003CStartLaughing_003Ed__14(int _003C_003E1__state)
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
			CS02_BadelineIntro cS02_BadelineIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.badeline.Sprite.Play("laugh", restart: true);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
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
	private sealed class _003CStopLaughing_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_BadelineIntro _003C_003E4__this;

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
		public _003CStopLaughing_003Ed__15(int _003C_003E1__state)
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
			CS02_BadelineIntro cS02_BadelineIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS02_BadelineIntro.badeline.Sprite.Play("fallSlow", restart: true);
				_003C_003E2__current = null;
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

	public const string Flag = "evil_maddy_intro";

	private Player player;

	private BadelineOldsite badeline;

	private Vector2 badelineEndPosition;

	private float anxietyFade;

	private float anxietyFadeTarget;

	private SineWave anxietySine;

	private float anxietyJitter;

	public CS02_BadelineIntro(BadelineOldsite badeline)
	{
		this.badeline = badeline;
		badelineEndPosition = badeline.Position + new Vector2(8f, -24f);
		Add(anxietySine = new SineWave(0.3f));
		Distort.AnxietyOrigin = new Vector2(0.5f, 0.75f);
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	public override void Update()
	{
		base.Update();
		anxietyFade = Calc.Approach(anxietyFade, anxietyFadeTarget, 2.5f * Engine.DeltaTime);
		if (base.Scene.OnInterval(0.1f))
		{
			anxietyJitter = Calc.Random.Range(-0.1f, 0.1f);
		}
		Distort.Anxiety = anxietyFade * Math.Max(0f, 0f + anxietyJitter + anxietySine.Value * 0.3f);
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__11))]
	private IEnumerator Cutscene(Level level)
	{
		anxietyFadeTarget = 1f;
		while (true)
		{
			player = level.Tracker.GetEntity<Player>();
			if (player != null)
			{
				break;
			}
			yield return null;
		}
		while (!player.OnGround())
		{
			yield return null;
		}
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return 1f;
		if (level.Session.Area.Mode == AreaMode.Normal)
		{
			Audio.SetMusic("event:/music/lvl2/evil_madeline");
		}
		yield return Textbox.Say("CH2_BADELINE_INTRO", TurnAround, RevealBadeline, StartLaughing, StopLaughing);
		anxietyFadeTarget = 0f;
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CTurnAround_003Ed__12))]
	private IEnumerator TurnAround()
	{
		player.Facing = Facings.Left;
		yield return 0.2f;
		Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(Level.Bounds.X, Level.Camera.Y), 0.5f)));
		yield return Level.ZoomTo(new Vector2(84f, 135f), 2f, 0.5f);
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CRevealBadeline_003Ed__13))]
	private IEnumerator RevealBadeline()
	{
		Audio.Play("event:/game/02_old_site/sequence_badeline_intro", badeline.Position);
		yield return 0.1f;
		Level.Displacement.AddBurst(badeline.Position + new Vector2(0f, -4f), 0.8f, 8f, 48f, 0.5f);
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		yield return 0.1f;
		badeline.Hovering = true;
		badeline.Hair.Visible = true;
		badeline.Sprite.Play("fallSlow");
		Vector2 from = badeline.Position;
		Vector2 to = badelineEndPosition;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime)
		{
			badeline.Position = from + (to - from) * Ease.CubeInOut(t);
			yield return null;
		}
		player.Facing = (Facings)Math.Sign(badeline.X - player.X);
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CStartLaughing_003Ed__14))]
	private IEnumerator StartLaughing()
	{
		yield return 0.2f;
		badeline.Sprite.Play("laugh", restart: true);
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CStopLaughing_003Ed__15))]
	private IEnumerator StopLaughing()
	{
		badeline.Sprite.Play("fallSlow", restart: true);
		yield return null;
	}

	public override void OnEnd(Level level)
	{
		Audio.SetMusic(null);
		Distort.Anxiety = 0f;
		if (player != null)
		{
			player.StateMachine.Locked = false;
			player.Facing = Facings.Left;
			player.StateMachine.State = 0;
			player.JustRespawned = true;
		}
		badeline.Position = badelineEndPosition;
		badeline.Visible = true;
		badeline.Hair.Visible = true;
		badeline.Sprite.Play("fallSlow");
		badeline.Hovering = false;
		badeline.Add(new Coroutine(badeline.StartChasingRoutine(level)));
		level.Session.SetFlag("evil_maddy_intro");
	}
}
