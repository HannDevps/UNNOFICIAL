using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_Entrance : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Entrance _003C_003E4__this;

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
		public _003CCutscene_003Ed__6(int _003C_003E1__state)
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
			CS05_Entrance cS05_Entrance = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_Entrance.player = level.Tracker.GetEntity<Player>();
				cS05_Entrance.player.StateMachine.State = 11;
				cS05_Entrance.player.StateMachine.Locked = true;
				cS05_Entrance.player.X = cS05_Entrance.theo.X - 32f;
				cS05_Entrance.playerMoveTo = new Vector2(cS05_Entrance.theo.X - 32f, cS05_Entrance.player.Y);
				cS05_Entrance.player.Facing = Facings.Left;
				SpotlightWipe.FocusPoint = cS05_Entrance.theo.TopCenter - Vector2.UnitX * 16f - level.Camera.Position;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS05_Entrance.player.Facing = Facings.Right;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_Entrance.theo.MoveTo(new Vector2(cS05_Entrance.theo.X + 48f, cS05_Entrance.theo.Y));
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch5_entrance", cS05_Entrance.MaddyTurnsRight, cS05_Entrance.TheoTurns, cS05_Entrance.TheoLeaves);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS05_Entrance.EndCutscene(level);
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
	private sealed class _003CMaddyTurnsRight_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Entrance _003C_003E4__this;

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
		public _003CMaddyTurnsRight_003Ed__7(int _003C_003E1__state)
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
			CS05_Entrance cS05_Entrance = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS05_Entrance.player.Facing = Facings.Right;
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
	private sealed class _003CTheoTurns_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Entrance _003C_003E4__this;

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
		public _003CTheoTurns_003Ed__8(int _003C_003E1__state)
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
			CS05_Entrance cS05_Entrance = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS05_Entrance.theo.Sprite.Scale.X *= -1f;
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
	private sealed class _003CTheoLeaves_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Entrance _003C_003E4__this;

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
		public _003CTheoLeaves_003Ed__9(int _003C_003E1__state)
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
			CS05_Entrance cS05_Entrance = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_Entrance.theo.MoveTo(new Vector2(cS05_Entrance.Level.Bounds.Right + 32, cS05_Entrance.theo.Y));
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

	public const string Flag = "entrance";

	private NPC theo;

	private Player player;

	private Vector2 playerMoveTo;

	public CS05_Entrance(NPC theo)
	{
		this.theo = theo;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player = level.Tracker.GetEntity<Player>();
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.X = theo.X - 32f;
		playerMoveTo = new Vector2(theo.X - 32f, player.Y);
		player.Facing = Facings.Left;
		SpotlightWipe.FocusPoint = theo.TopCenter - Vector2.UnitX * 16f - level.Camera.Position;
		yield return 2f;
		player.Facing = Facings.Right;
		yield return 0.3f;
		yield return theo.MoveTo(new Vector2(theo.X + 48f, theo.Y));
		yield return Textbox.Say("ch5_entrance", MaddyTurnsRight, TheoTurns, TheoLeaves);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CMaddyTurnsRight_003Ed__7))]
	private IEnumerator MaddyTurnsRight()
	{
		player.Facing = Facings.Right;
		yield break;
	}

	[IteratorStateMachine(typeof(_003CTheoTurns_003Ed__8))]
	private IEnumerator TheoTurns()
	{
		theo.Sprite.Scale.X *= -1f;
		yield break;
	}

	[IteratorStateMachine(typeof(_003CTheoLeaves_003Ed__9))]
	private IEnumerator TheoLeaves()
	{
		yield return theo.MoveTo(new Vector2(Level.Bounds.Right + 32, theo.Y));
	}

	public override void OnEnd(Level level)
	{
		if (player != null)
		{
			player.StateMachine.Locked = false;
			player.StateMachine.State = 0;
			player.ForceCameraUpdate = false;
			player.Position = playerMoveTo;
			player.Facing = Facings.Right;
		}
		base.Scene.Remove(theo);
		level.Session.SetFlag("entrance");
	}
}
