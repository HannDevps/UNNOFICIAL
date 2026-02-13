using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_Reflection1 : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Reflection1 _003C_003E4__this;

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
		public _003CCutscene_003Ed__4(int _003C_003E1__state)
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
			CS05_Reflection1 cS05_Reflection = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				cS05_Reflection.player.StateMachine.State = 11;
				cS05_Reflection.player.StateMachine.Locked = true;
				cS05_Reflection.player.ForceCameraUpdate = true;
				TempleMirror templeMirror = cS05_Reflection.Scene.Entities.FindFirst<TempleMirror>();
				_003C_003E2__current = cS05_Reflection.player.DummyWalkTo(templeMirror.Center.X + 8f);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS05_Reflection.player.Facing = Facings.Left;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				if (!cS05_Reflection.player.Dead)
				{
					_003C_003E2__current = Textbox.Say("ch5_reflection", cS05_Reflection.MadelineFallsToKnees, cS05_Reflection.MadelineStopsPanicking, cS05_Reflection.MadelineGetsUp);
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = 100f;
				_003C_003E1__state = 5;
				return true;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0180;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0180;
			case 6:
				{
					_003C_003E1__state = -1;
					cS05_Reflection.EndCutscene(level);
					return false;
				}
				IL_0180:
				_003C_003E2__current = cS05_Reflection.Level.ZoomBack(0.5f);
				_003C_003E1__state = 6;
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
	private sealed class _003CMadelineFallsToKnees_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Reflection1 _003C_003E4__this;

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
		public _003CMadelineFallsToKnees_003Ed__5(int _003C_003E1__state)
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
			CS05_Reflection1 cS05_Reflection = _003C_003E4__this;
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
				cS05_Reflection.player.DummyAutoAnimate = false;
				cS05_Reflection.player.Sprite.Play("tired");
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_Reflection.Level.ZoomTo(new Vector2(90f, 116f), 2f, 0.5f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
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
	private sealed class _003CMadelineStopsPanicking_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Reflection1 _003C_003E4__this;

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
		public _003CMadelineStopsPanicking_003Ed__6(int _003C_003E1__state)
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
			CS05_Reflection1 cS05_Reflection = _003C_003E4__this;
			switch (num)
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
				cS05_Reflection.player.Sprite.Play("tiredStill");
				_003C_003E2__current = 0.4f;
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
	private sealed class _003CMadelineGetsUp_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Reflection1 _003C_003E4__this;

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
		public _003CMadelineGetsUp_003Ed__7(int _003C_003E1__state)
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
			CS05_Reflection1 cS05_Reflection = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS05_Reflection.player.DummyAutoAnimate = true;
			cS05_Reflection.player.Sprite.Play("idle");
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

	public const string Flag = "reflection";

	private Player player;

	public CS05_Reflection1(Player player)
	{
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__4))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.ForceCameraUpdate = true;
		TempleMirror templeMirror = base.Scene.Entities.FindFirst<TempleMirror>();
		yield return player.DummyWalkTo(templeMirror.Center.X + 8f);
		yield return 0.2f;
		player.Facing = Facings.Left;
		yield return 0.3f;
		if (!player.Dead)
		{
			yield return Textbox.Say("ch5_reflection", MadelineFallsToKnees, MadelineStopsPanicking, MadelineGetsUp);
		}
		else
		{
			yield return 100f;
		}
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CMadelineFallsToKnees_003Ed__5))]
	private IEnumerator MadelineFallsToKnees()
	{
		yield return 0.2f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("tired");
		yield return 0.2f;
		yield return Level.ZoomTo(new Vector2(90f, 116f), 2f, 0.5f);
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CMadelineStopsPanicking_003Ed__6))]
	private IEnumerator MadelineStopsPanicking()
	{
		yield return 0.8f;
		player.Sprite.Play("tiredStill");
		yield return 0.4f;
	}

	[IteratorStateMachine(typeof(_003CMadelineGetsUp_003Ed__7))]
	private IEnumerator MadelineGetsUp()
	{
		player.DummyAutoAnimate = true;
		player.Sprite.Play("idle");
		yield break;
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		player.ForceCameraUpdate = false;
		player.FlipInReflection = false;
		level.Session.SetFlag("reflection");
	}
}
