using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashPage06 : WaveDashPage
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__2 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage06 _003C_003E4__this;

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
			WaveDashPage06 waveDashPage = _003C_003E4__this;
			switch (num)
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
				Audio.Play("event:/new_content/game/10_farewell/ppt_happy_wavedashing");
				waveDashPage.title = new AreaCompleteTitle(new Vector2((float)waveDashPage.Width / 2f, 150f), Dialog.Clean("WAVEDASH_PAGE6_TITLE"), 2f, rainbow: true);
				_003C_003E2__current = 1.5f;
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

	private AreaCompleteTitle title;

	public WaveDashPage06()
	{
		Transition = Transitions.Rotate3D;
		ClearColor = Calc.HexToColor("d9d2e9");
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__2))]
	public override IEnumerator Routine()
	{
		yield return 1f;
		Audio.Play("event:/new_content/game/10_farewell/ppt_happy_wavedashing");
		title = new AreaCompleteTitle(new Vector2((float)base.Width / 2f, 150f), Dialog.Clean("WAVEDASH_PAGE6_TITLE"), 2f, rainbow: true);
		yield return 1.5f;
	}

	public override void Update()
	{
		if (title != null)
		{
			title.Update();
		}
	}

	public override void Render()
	{
		Presentation.Gfx["Bird Clip Art"].DrawCentered(new Vector2(base.Width, base.Height) / 2f, Color.White, 1.5f);
		if (title != null)
		{
			title.Render();
		}
	}
}
