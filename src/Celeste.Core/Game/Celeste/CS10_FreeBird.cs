using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Monocle;

namespace Celeste;

public class CS10_FreeBird : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__2 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public CS10_FreeBird _003C_003E4__this;

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
		public _003CCutscene_003Ed__2(int _003C_003E1__state)
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
			CS10_FreeBird cS10_FreeBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_FREE_BIRD");
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = new FadeWipe(level, wipeIn: false)
				{
					Duration = 3f
				}.Duration;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_FreeBird.EndCutscene(level);
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

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__2))]
	private IEnumerator Cutscene(Level level)
	{
		yield return Textbox.Say("CH9_FREE_BIRD");
		FadeWipe fadeWipe = new FadeWipe(level, wipeIn: false);
		fadeWipe.Duration = 3f;
		yield return fadeWipe.Duration;
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		level.CompleteArea(spotlightWipe: false, skipScreenWipe: true);
	}
}
