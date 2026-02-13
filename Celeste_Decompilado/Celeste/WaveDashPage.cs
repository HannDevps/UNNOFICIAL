using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Celeste;

public abstract class WaveDashPage
{
	public enum Transitions
	{
		ScaleIn,
		FadeIn,
		Rotate3D,
		Blocky,
		Spiral
	}

	[CompilerGenerated]
	private sealed class _003CPressButton_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage _003C_003E4__this;

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
		public _003CPressButton_003Ed__14(int _003C_003E1__state)
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
			WaveDashPage waveDashPage = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				waveDashPage.WaitingForInput = true;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (!Input.MenuConfirm.Pressed)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			waveDashPage.WaitingForInput = false;
			Audio.Play("event:/new_content/game/10_farewell/ppt_mouseclick");
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

	public WaveDashPresentation Presentation;

	public Color ClearColor;

	public Transitions Transition;

	public bool AutoProgress;

	public bool WaitingForInput;

	public int Width => Presentation.ScreenWidth;

	public int Height => Presentation.ScreenHeight;

	public abstract IEnumerator Routine();

	public virtual void Added(WaveDashPresentation presentation)
	{
		Presentation = presentation;
	}

	public virtual void Update()
	{
	}

	public virtual void Render()
	{
	}

	[IteratorStateMachine(typeof(_003CPressButton_003Ed__14))]
	protected IEnumerator PressButton()
	{
		WaitingForInput = true;
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		WaitingForInput = false;
		Audio.Play("event:/new_content/game/10_farewell/ppt_mouseclick");
	}
}
