using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Monocle;

namespace Celeste;

public class FileErrorOverlay : Overlay
{
	public enum Error
	{
		Load,
		Save
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public int option;

		public bool waiting;

		internal void _003CRoutine_003Eb__0()
		{
			option = 0;
			waiting = false;
		}

		internal void _003CRoutine_003Eb__1()
		{
			option = 1;
			waiting = false;
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FileErrorOverlay _003C_003E4__this;

		private _003C_003Ec__DisplayClass16_0 _003C_003E8__1;

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
		public _003CRoutine_003Ed__16(int _003C_003E1__state)
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
			FileErrorOverlay fileErrorOverlay = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass16_0();
				_003C_003E2__current = fileErrorOverlay.FadeIn();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E8__1.waiting = true;
				_003C_003E8__1.option = 0;
				Audio.Play("event:/ui/main/message_confirm");
				fileErrorOverlay.menu = new TextMenu();
				fileErrorOverlay.menu.Add(new TextMenu.Header(Dialog.Clean("savefailed_title")));
				fileErrorOverlay.menu.Add(new TextMenu.Button(Dialog.Clean((fileErrorOverlay.mode == Error.Save) ? "savefailed_retry" : "loadfailed_goback")).Pressed(delegate
				{
					_003C_003E8__1.option = 0;
					_003C_003E8__1.waiting = false;
				}));
				fileErrorOverlay.menu.Add(new TextMenu.Button(Dialog.Clean("savefailed_ignore")).Pressed(delegate
				{
					_003C_003E8__1.option = 1;
					_003C_003E8__1.waiting = false;
				}));
				goto IL_012b;
			case 2:
				_003C_003E1__state = -1;
				goto IL_012b;
			case 3:
				{
					_003C_003E1__state = -1;
					fileErrorOverlay.Open = false;
					fileErrorOverlay.RemoveSelf();
					return false;
				}
				IL_012b:
				if (_003C_003E8__1.waiting)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				fileErrorOverlay.menu = null;
				fileErrorOverlay.Ignore = _003C_003E8__1.option == 1;
				fileErrorOverlay.TryAgain = _003C_003E8__1.option == 0;
				_003C_003E2__current = fileErrorOverlay.FadeOut();
				_003C_003E1__state = 3;
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

	private Error mode;

	private TextMenu menu;

	public bool Open { get; private set; }

	public bool TryAgain { get; private set; }

	public bool Ignore { get; private set; }

	public FileErrorOverlay(Error mode)
	{
		Open = true;
		this.mode = mode;
		Add(new Coroutine(Routine()));
		Engine.Scene.Add(this);
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__16))]
	private IEnumerator Routine()
	{
		yield return FadeIn();
		bool waiting = true;
		int option = 0;
		Audio.Play("event:/ui/main/message_confirm");
		menu = new TextMenu();
		menu.Add(new TextMenu.Header(Dialog.Clean("savefailed_title")));
		menu.Add(new TextMenu.Button(Dialog.Clean((mode == Error.Save) ? "savefailed_retry" : "loadfailed_goback")).Pressed(delegate
		{
			option = 0;
			waiting = false;
		}));
		menu.Add(new TextMenu.Button(Dialog.Clean("savefailed_ignore")).Pressed(delegate
		{
			option = 1;
			waiting = false;
		}));
		while (waiting)
		{
			yield return null;
		}
		menu = null;
		Ignore = option == 1;
		TryAgain = option == 0;
		yield return FadeOut();
		Open = false;
		RemoveSelf();
	}

	public override void Update()
	{
		base.Update();
		if (menu != null)
		{
			menu.Update();
		}
		if (SaveLoadIcon.Instance != null && SaveLoadIcon.Instance.Scene == base.Scene)
		{
			SaveLoadIcon.Instance.Update();
		}
	}

	public override void Render()
	{
		RenderFade();
		if (menu != null)
		{
			menu.Render();
		}
		base.Render();
	}
}
