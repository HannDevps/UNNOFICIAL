using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS00_Granny : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS00_Granny.player.StateMachine.State = 11;
				if (Math.Abs(cS00_Granny.player.X - cS00_Granny.granny.X) < 20f)
				{
					_003C_003E2__current = cS00_Granny.player.DummyWalkTo(cS00_Granny.granny.X - 48f);
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_009f;
			case 1:
				_003C_003E1__state = -1;
				goto IL_009f;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH0_GRANNY", cS00_Granny.Meet, cS00_Granny.RunAlong, cS00_Granny.LaughAndAirQuotes, cS00_Granny.Laugh, cS00_Granny.StopLaughing, cS00_Granny.OminousZoom, cS00_Granny.PanToMaddy);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS00_Granny.Level.ZoomBack(0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					cS00_Granny.EndCutscene(cS00_Granny.Level);
					return false;
				}
				IL_009f:
				cS00_Granny.player.Facing = Facings.Right;
				_003C_003E2__current = 0.5f;
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
	private sealed class _003CMeet_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003CMeet_003Ed__8(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS00_Granny.granny.Sprite.Scale.X = Math.Sign(cS00_Granny.player.X - cS00_Granny.granny.X);
				_003C_003E2__current = cS00_Granny.player.DummyWalkTo(cS00_Granny.granny.X - 20f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS00_Granny.player.Facing = Facings.Right;
				_003C_003E2__current = 0.8f;
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
	private sealed class _003CRunAlong_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003CRunAlong_003Ed__9(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS00_Granny.player.DummyWalkToExact((int)cS00_Granny.endPlayerPosition.X);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS00_Granny.player.Facing = Facings.Left;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS00_Granny.granny.Sprite.Scale.X = 1f;
				_003C_003E2__current = cS00_Granny.Level.ZoomTo(new Vector2(210f, 90f), 2f, 0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
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
	private sealed class _003CLaughAndAirQuotes_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003CLaughAndAirQuotes_003Ed__10(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS00_Granny.granny.LaughSfx.FirstPlay = true;
				cS00_Granny.granny.Sprite.Play("laugh");
				_003C_003E2__current = 2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS00_Granny.granny.Sprite.Play("airQuotes");
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
	private sealed class _003CLaugh_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003CLaugh_003Ed__11(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS00_Granny.granny.LaughSfx.FirstPlay = false;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS00_Granny.granny.Sprite.Play("laugh");
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
	private sealed class _003CStopLaughing_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003CStopLaughing_003Ed__12(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS00_Granny.granny.Sprite.Play("idle");
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
	private sealed class _003COminousZoom_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003COminousZoom_003Ed__13(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Vector2 screenSpaceFocusPoint = new Vector2(210f, 100f);
				cS00_Granny.zoomCoroutine = new Coroutine(cS00_Granny.Level.ZoomAcross(screenSpaceFocusPoint, 4f, 3f));
				cS00_Granny.Add(cS00_Granny.zoomCoroutine);
				cS00_Granny.granny.Sprite.Play("idle");
				_003C_003E2__current = 0.2f;
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
	private sealed class _003CPanToMaddy_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Granny _003C_003E4__this;

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
		public _003CPanToMaddy_003Ed__14(int _003C_003E1__state)
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
			CS00_Granny cS00_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_004a;
			case 1:
				_003C_003E1__state = -1;
				goto IL_004a;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS00_Granny.Level.ZoomAcross(new Vector2(210f, 90f), 2f, 0.5f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_004a:
				if (cS00_Granny.zoomCoroutine != null && cS00_Granny.zoomCoroutine.Active)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = 0.2f;
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

	public const string Flag = "granny";

	private NPC00_Granny granny;

	private Player player;

	private Vector2 endPlayerPosition;

	private Coroutine zoomCoroutine;

	public CS00_Granny(NPC00_Granny granny, Player player)
	{
		this.granny = granny;
		this.player = player;
		endPlayerPosition = granny.Position + new Vector2(48f, 0f);
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene()));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene()
	{
		player.StateMachine.State = 11;
		if (Math.Abs(player.X - granny.X) < 20f)
		{
			yield return player.DummyWalkTo(granny.X - 48f);
		}
		player.Facing = Facings.Right;
		yield return 0.5f;
		yield return Textbox.Say("CH0_GRANNY", Meet, RunAlong, LaughAndAirQuotes, Laugh, StopLaughing, OminousZoom, PanToMaddy);
		yield return Level.ZoomBack(0.5f);
		EndCutscene(Level);
	}

	[IteratorStateMachine(typeof(_003CMeet_003Ed__8))]
	private IEnumerator Meet()
	{
		yield return 0.25f;
		granny.Sprite.Scale.X = Math.Sign(player.X - granny.X);
		yield return player.DummyWalkTo(granny.X - 20f);
		player.Facing = Facings.Right;
		yield return 0.8f;
	}

	[IteratorStateMachine(typeof(_003CRunAlong_003Ed__9))]
	private IEnumerator RunAlong()
	{
		yield return player.DummyWalkToExact((int)endPlayerPosition.X);
		yield return 0.8f;
		player.Facing = Facings.Left;
		yield return 0.4f;
		granny.Sprite.Scale.X = 1f;
		yield return Level.ZoomTo(new Vector2(210f, 90f), 2f, 0.5f);
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CLaughAndAirQuotes_003Ed__10))]
	private IEnumerator LaughAndAirQuotes()
	{
		yield return 0.6f;
		granny.LaughSfx.FirstPlay = true;
		granny.Sprite.Play("laugh");
		yield return 2f;
		granny.Sprite.Play("airQuotes");
	}

	[IteratorStateMachine(typeof(_003CLaugh_003Ed__11))]
	private IEnumerator Laugh()
	{
		granny.LaughSfx.FirstPlay = false;
		yield return null;
		granny.Sprite.Play("laugh");
	}

	[IteratorStateMachine(typeof(_003CStopLaughing_003Ed__12))]
	private IEnumerator StopLaughing()
	{
		granny.Sprite.Play("idle");
		yield break;
	}

	[IteratorStateMachine(typeof(_003COminousZoom_003Ed__13))]
	private IEnumerator OminousZoom()
	{
		Vector2 screenSpaceFocusPoint = new Vector2(210f, 100f);
		zoomCoroutine = new Coroutine(Level.ZoomAcross(screenSpaceFocusPoint, 4f, 3f));
		Add(zoomCoroutine);
		granny.Sprite.Play("idle");
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CPanToMaddy_003Ed__14))]
	private IEnumerator PanToMaddy()
	{
		while (zoomCoroutine != null && zoomCoroutine.Active)
		{
			yield return null;
		}
		yield return 0.2f;
		yield return Level.ZoomAcross(new Vector2(210f, 90f), 2f, 0.5f);
		yield return 0.2f;
	}

	public override void OnEnd(Level level)
	{
		granny.Hahaha.Enabled = true;
		granny.Sprite.Play("laugh");
		granny.Sprite.Scale.X = 1f;
		player.Position.X = endPlayerPosition.X;
		player.Facing = Facings.Left;
		player.StateMachine.State = 0;
		level.Session.SetFlag("granny");
		level.ResetZoom();
	}
}
