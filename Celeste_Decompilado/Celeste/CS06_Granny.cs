using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_Granny : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CCutscene_003Ed__8(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_Granny.player.StateMachine.State = 11;
				cS06_Granny.player.StateMachine.Locked = true;
				cS06_Granny.player.ForceCameraUpdate = true;
				if (cS06_Granny.index == 0)
				{
					_003C_003E2__current = cS06_Granny.player.DummyWalkTo(cS06_Granny.granny.X - 40f);
					_003C_003E1__state = 1;
					return true;
				}
				if (cS06_Granny.index == 1)
				{
					_003C_003E2__current = cS06_Granny.ZoomIn();
					_003C_003E1__state = 3;
					return true;
				}
				if (cS06_Granny.index == 2)
				{
					_003C_003E2__current = cS06_Granny.ZoomIn();
					_003C_003E1__state = 6;
					return true;
				}
				goto IL_029b;
			case 1:
				_003C_003E1__state = -1;
				cS06_Granny.startX = cS06_Granny.player.X;
				cS06_Granny.player.Facing = Facings.Right;
				cS06_Granny.firstLaugh = true;
				_003C_003E2__current = Textbox.Say("ch6_oldlady", cS06_Granny.ZoomIn, cS06_Granny.Laughs, cS06_Granny.StopLaughing, cS06_Granny.MaddyWalksRight, cS06_Granny.MaddyWalksLeft, cS06_Granny.WaitABit, cS06_Granny.MaddyTurnsRight);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_029b;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_Granny.player.DummyWalkTo(cS06_Granny.granny.X - 20f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS06_Granny.player.Facing = Facings.Right;
				_003C_003E2__current = Textbox.Say("ch6_oldlady_b");
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				goto IL_029b;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_Granny.player.DummyWalkTo(cS06_Granny.granny.X - 20f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				cS06_Granny.player.Facing = Facings.Right;
				_003C_003E2__current = Textbox.Say("ch6_oldlady_c");
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				goto IL_029b;
			case 9:
				{
					_003C_003E1__state = -1;
					cS06_Granny.EndCutscene(level);
					return false;
				}
				IL_029b:
				_003C_003E2__current = cS06_Granny.Level.ZoomBack(0.5f);
				_003C_003E1__state = 9;
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
	private sealed class _003CZoomIn_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CZoomIn_003Ed__9(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Vector2 screenSpaceFocusPoint = Vector2.Lerp(cS06_Granny.granny.Position, cS06_Granny.player.Position, 0.5f) - cS06_Granny.Level.Camera.Position + new Vector2(0f, -20f);
				_003C_003E2__current = cS06_Granny.Level.ZoomTo(screenSpaceFocusPoint, 2f, 0.5f);
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
	private sealed class _003CLaughs_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CLaughs_003Ed__10(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (cS06_Granny.firstLaugh)
				{
					cS06_Granny.firstLaugh = false;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0058;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0058;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0058:
				cS06_Granny.granny.Sprite.Play("laugh");
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

	[CompilerGenerated]
	private sealed class _003CStopLaughing_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CStopLaughing_003Ed__11(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_Granny.granny.Sprite.Play("idle");
				_003C_003E2__current = 0.25f;
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
	private sealed class _003CMaddyWalksLeft_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CMaddyWalksLeft_003Ed__12(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
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
				cS06_Granny.player.Facing = Facings.Left;
				_003C_003E2__current = cS06_Granny.player.DummyWalkToExact((int)cS06_Granny.player.X - 8);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CMaddyWalksRight_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CMaddyWalksRight_003Ed__13(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
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
				cS06_Granny.player.Facing = Facings.Right;
				_003C_003E2__current = cS06_Granny.player.DummyWalkToExact((int)cS06_Granny.player.X + 8);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CWaitABit_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
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
		public _003CWaitABit_003Ed__14(int _003C_003E1__state)
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
				_003C_003E2__current = 0.8f;
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
	private sealed class _003CMaddyTurnsRight_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Granny _003C_003E4__this;

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
		public _003CMaddyTurnsRight_003Ed__15(int _003C_003E1__state)
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
			CS06_Granny cS06_Granny = _003C_003E4__this;
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
				cS06_Granny.player.Facing = Facings.Right;
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

	public const string FlagPrefix = "granny_";

	private NPC06_Granny granny;

	private Player player;

	private float startX;

	private int index;

	private bool firstLaugh;

	public CS06_Granny(NPC06_Granny granny, Player player, int index)
	{
		this.granny = granny;
		this.player = player;
		this.index = index;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.ForceCameraUpdate = true;
		if (index == 0)
		{
			yield return player.DummyWalkTo(granny.X - 40f);
			startX = player.X;
			player.Facing = Facings.Right;
			firstLaugh = true;
			yield return Textbox.Say("ch6_oldlady", ZoomIn, Laughs, StopLaughing, MaddyWalksRight, MaddyWalksLeft, WaitABit, MaddyTurnsRight);
		}
		else if (index == 1)
		{
			yield return ZoomIn();
			yield return player.DummyWalkTo(granny.X - 20f);
			player.Facing = Facings.Right;
			yield return Textbox.Say("ch6_oldlady_b");
		}
		else if (index == 2)
		{
			yield return ZoomIn();
			yield return player.DummyWalkTo(granny.X - 20f);
			player.Facing = Facings.Right;
			yield return Textbox.Say("ch6_oldlady_c");
		}
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CZoomIn_003Ed__9))]
	private IEnumerator ZoomIn()
	{
		Vector2 screenSpaceFocusPoint = Vector2.Lerp(granny.Position, player.Position, 0.5f) - Level.Camera.Position + new Vector2(0f, -20f);
		yield return Level.ZoomTo(screenSpaceFocusPoint, 2f, 0.5f);
	}

	[IteratorStateMachine(typeof(_003CLaughs_003Ed__10))]
	private IEnumerator Laughs()
	{
		if (firstLaugh)
		{
			firstLaugh = false;
			yield return 0.5f;
		}
		granny.Sprite.Play("laugh");
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CStopLaughing_003Ed__11))]
	private IEnumerator StopLaughing()
	{
		granny.Sprite.Play("idle");
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CMaddyWalksLeft_003Ed__12))]
	private IEnumerator MaddyWalksLeft()
	{
		yield return 0.1f;
		player.Facing = Facings.Left;
		yield return player.DummyWalkToExact((int)player.X - 8);
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CMaddyWalksRight_003Ed__13))]
	private IEnumerator MaddyWalksRight()
	{
		yield return 0.1f;
		player.Facing = Facings.Right;
		yield return player.DummyWalkToExact((int)player.X + 8);
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CWaitABit_003Ed__14))]
	private IEnumerator WaitABit()
	{
		yield return 0.8f;
	}

	[IteratorStateMachine(typeof(_003CMaddyTurnsRight_003Ed__15))]
	private IEnumerator MaddyTurnsRight()
	{
		yield return 0.1f;
		player.Facing = Facings.Right;
		yield return 0.1f;
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		player.ForceCameraUpdate = false;
		granny.Sprite.Play("idle");
		level.Session.SetFlag("granny_" + index);
	}
}
