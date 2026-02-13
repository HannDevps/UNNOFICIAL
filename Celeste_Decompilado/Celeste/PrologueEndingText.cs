using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class PrologueEndingText : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__2 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool instant;

		public PrologueEndingText _003C_003E4__this;

		private int _003Ci_003E5__2;

		private FancyText.Char _003Cc_003E5__3;

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
		public _003CRoutine_003Ed__2(int _003C_003E1__state)
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
			PrologueEndingText prologueEndingText = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (!instant)
				{
					_003C_003E2__current = 4f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0051;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0051;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_009a;
				}
				IL_009a:
				if ((_003Cc_003E5__3.Fade += Engine.DeltaTime * 20f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Cc_003E5__3.Fade = 1f;
				_003Cc_003E5__3 = null;
				goto IL_00d8;
				IL_0051:
				_003Ci_003E5__2 = 0;
				goto IL_00e8;
				IL_00e8:
				if (_003Ci_003E5__2 < prologueEndingText.text.Count)
				{
					_003Cc_003E5__3 = prologueEndingText.text[_003Ci_003E5__2] as FancyText.Char;
					if (_003Cc_003E5__3 != null)
					{
						goto IL_009a;
					}
					goto IL_00d8;
				}
				return false;
				IL_00d8:
				_003Ci_003E5__2++;
				goto IL_00e8;
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

	private FancyText.Text text;

	public PrologueEndingText(bool instant)
	{
		base.Tag = Tags.HUD;
		text = FancyText.Parse(Dialog.Clean("CH0_END"), 960, 4, 0f);
		Add(new Coroutine(Routine(instant)));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__2))]
	private IEnumerator Routine(bool instant)
	{
		if (!instant)
		{
			yield return 4f;
		}
		for (int i = 0; i < text.Count; i++)
		{
			if (text[i] is FancyText.Char c)
			{
				while ((c.Fade += Engine.DeltaTime * 20f) < 1f)
				{
					yield return null;
				}
				c.Fade = 1f;
			}
		}
	}

	public override void Render()
	{
		text.Draw(Position, new Vector2(0.5f, 0.5f), Vector2.One, 1f);
	}
}
