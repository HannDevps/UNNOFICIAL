using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Monocle;

namespace Celeste;

public class CS03_Diary : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__3 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Diary _003C_003E4__this;

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
		public _003CRoutine_003Ed__3(int _003C_003E1__state)
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
			CS03_Diary cS03_Diary = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_Diary.player.StateMachine.State = 11;
				cS03_Diary.player.StateMachine.Locked = true;
				_003C_003E2__current = Textbox.Say("CH3_DIARY");
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS03_Diary.EndCutscene(cS03_Diary.Level);
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

	private Player player;

	public CS03_Diary(Player player)
	{
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Routine()));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__3))]
	private IEnumerator Routine()
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return Textbox.Say("CH3_DIARY");
		yield return 0.1f;
		EndCutscene(Level);
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
	}
}
