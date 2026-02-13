using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_SeeTheo : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SeeTheo _003C_003E4__this;

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
		public _003CCutscene_003Ed__7(int _003C_003E1__state)
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
			CS05_SeeTheo cS05_SeeTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0052;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0052;
			case 2:
				_003C_003E1__state = -1;
				cS05_SeeTheo.theo = cS05_SeeTheo.Scene.Tracker.GetEntity<TheoCrystal>();
				if (cS05_SeeTheo.theo != null && Math.Sign(cS05_SeeTheo.player.X - cS05_SeeTheo.theo.X) != 0)
				{
					cS05_SeeTheo.player.Facing = (Facings)Math.Sign(cS05_SeeTheo.theo.X - cS05_SeeTheo.player.X);
				}
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				if (cS05_SeeTheo.index == 0)
				{
					_003C_003E2__current = Textbox.Say("ch5_see_theo", cS05_SeeTheo.ZoomIn, cS05_SeeTheo.MadelineTurnsAround, cS05_SeeTheo.WaitABit, cS05_SeeTheo.MadelineTurnsBackAndBrighten);
					_003C_003E1__state = 4;
					return true;
				}
				if (cS05_SeeTheo.index == 1)
				{
					_003C_003E2__current = Textbox.Say("ch5_see_theo_b");
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_01ce;
			case 4:
				_003C_003E1__state = -1;
				goto IL_01ce;
			case 5:
				_003C_003E1__state = -1;
				goto IL_01ce;
			case 6:
				{
					_003C_003E1__state = -1;
					cS05_SeeTheo.EndCutscene(level);
					return false;
				}
				IL_01ce:
				_003C_003E2__current = cS05_SeeTheo.Level.ZoomBack(0.5f);
				_003C_003E1__state = 6;
				return true;
				IL_0052:
				if (cS05_SeeTheo.player.Scene == null || !cS05_SeeTheo.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS05_SeeTheo.player.StateMachine.State = 11;
				cS05_SeeTheo.player.StateMachine.Locked = true;
				_003C_003E2__current = 0.25f;
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
	private sealed class _003CZoomIn_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SeeTheo _003C_003E4__this;

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
		public _003CZoomIn_003Ed__8(int _003C_003E1__state)
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
			CS05_SeeTheo cS05_SeeTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_SeeTheo.Level.ZoomTo(Vector2.Lerp(cS05_SeeTheo.player.Position, cS05_SeeTheo.theo.Position, 0.5f) - cS05_SeeTheo.Level.Camera.Position + new Vector2(0f, -20f), 2f, 0.5f);
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
	private sealed class _003CMadelineTurnsAround_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SeeTheo _003C_003E4__this;

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
		public _003CMadelineTurnsAround_003Ed__9(int _003C_003E1__state)
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
			CS05_SeeTheo cS05_SeeTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS05_SeeTheo.player.Facing = Facings.Left;
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CWaitABit_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CWaitABit_003Ed__10(int _003C_003E1__state)
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
				_003C_003E2__current = 1f;
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
	private sealed class _003CMadelineTurnsBackAndBrighten_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SeeTheo _003C_003E4__this;

		private Coroutine _003Ccoroutine_003E5__2;

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
		public _003CMadelineTurnsBackAndBrighten_003Ed__11(int _003C_003E1__state)
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
			CS05_SeeTheo cS05_SeeTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Ccoroutine_003E5__2 = new Coroutine(cS05_SeeTheo.Brighten());
				cS05_SeeTheo.Add(_003Ccoroutine_003E5__2);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS05_SeeTheo.player.Facing = Facings.Right;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				break;
			case 4:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Ccoroutine_003E5__2.Active)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
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

	[CompilerGenerated]
	private sealed class _003CBrighten_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SeeTheo _003C_003E4__this;

		private float _003Cdarkness_003E5__2;

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
		public _003CBrighten_003Ed__12(int _003C_003E1__state)
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
			CS05_SeeTheo cS05_SeeTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_SeeTheo.Level.ZoomBack(0.5f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS05_SeeTheo.Level.Session.DarkRoomAlpha = 0.3f;
				_003Cdarkness_003E5__2 = cS05_SeeTheo.Level.Session.DarkRoomAlpha;
				break;
			case 3:
				_003C_003E1__state = -1;
				break;
			}
			if (cS05_SeeTheo.Level.Lighting.Alpha != _003Cdarkness_003E5__2)
			{
				cS05_SeeTheo.Level.Lighting.Alpha = Calc.Approach(cS05_SeeTheo.Level.Lighting.Alpha, _003Cdarkness_003E5__2, Engine.DeltaTime * 0.5f);
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
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

	private const float NewDarknessAlpha = 0.3f;

	public const string Flag = "seeTheoInCrystal";

	private int index;

	private Player player;

	private TheoCrystal theo;

	public CS05_SeeTheo(Player player, int index)
	{
		this.player = player;
		this.index = index;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene(Level level)
	{
		while (player.Scene == null || !player.OnGround())
		{
			yield return null;
		}
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return 0.25f;
		theo = base.Scene.Tracker.GetEntity<TheoCrystal>();
		if (theo != null && Math.Sign(player.X - theo.X) != 0)
		{
			player.Facing = (Facings)Math.Sign(theo.X - player.X);
		}
		yield return 0.25f;
		if (index == 0)
		{
			yield return Textbox.Say("ch5_see_theo", ZoomIn, MadelineTurnsAround, WaitABit, MadelineTurnsBackAndBrighten);
		}
		else if (index == 1)
		{
			yield return Textbox.Say("ch5_see_theo_b");
		}
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CZoomIn_003Ed__8))]
	private IEnumerator ZoomIn()
	{
		yield return Level.ZoomTo(Vector2.Lerp(player.Position, theo.Position, 0.5f) - Level.Camera.Position + new Vector2(0f, -20f), 2f, 0.5f);
	}

	[IteratorStateMachine(typeof(_003CMadelineTurnsAround_003Ed__9))]
	private IEnumerator MadelineTurnsAround()
	{
		yield return 0.3f;
		player.Facing = Facings.Left;
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CWaitABit_003Ed__10))]
	private IEnumerator WaitABit()
	{
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CMadelineTurnsBackAndBrighten_003Ed__11))]
	private IEnumerator MadelineTurnsBackAndBrighten()
	{
		yield return 0.1f;
		Coroutine coroutine = new Coroutine(Brighten());
		Add(coroutine);
		yield return 0.2f;
		player.Facing = Facings.Right;
		yield return 0.1f;
		while (coroutine.Active)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CBrighten_003Ed__12))]
	private IEnumerator Brighten()
	{
		yield return Level.ZoomBack(0.5f);
		yield return 0.3f;
		Level.Session.DarkRoomAlpha = 0.3f;
		float darkness = Level.Session.DarkRoomAlpha;
		while (Level.Lighting.Alpha != darkness)
		{
			Level.Lighting.Alpha = Calc.Approach(Level.Lighting.Alpha, darkness, Engine.DeltaTime * 0.5f);
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		player.ForceCameraUpdate = false;
		player.DummyAutoAnimate = true;
		level.Session.DarkRoomAlpha = 0.3f;
		level.Lighting.Alpha = level.Session.DarkRoomAlpha;
		level.Session.SetFlag("seeTheoInCrystal");
	}
}
