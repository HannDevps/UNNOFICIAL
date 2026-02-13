using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Monocle;

namespace Celeste;

public class PreviewPostcard : Scene
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__2 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PreviewPostcard _003C_003E4__this;

		public Postcard postcard;

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
			PreviewPostcard previewPostcard = _003C_003E4__this;
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
				previewPostcard.Add(postcard);
				_003C_003E2__current = postcard.DisplayRoutine();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				Engine.Scene = new OverworldLoader(Overworld.StartMode.MainMenu);
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

	private Postcard postcard;

	public PreviewPostcard(Postcard postcard)
	{
		Audio.SetMusic(null);
		Audio.SetAmbience(null);
		this.postcard = postcard;
		Add(new Entity
		{
			new Coroutine(Routine(postcard))
		});
		Add(new HudRenderer());
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__2))]
	private IEnumerator Routine(Postcard postcard)
	{
		yield return 0.25f;
		Add(postcard);
		yield return postcard.DisplayRoutine();
		Engine.Scene = new OverworldLoader(Overworld.StartMode.MainMenu);
	}

	public override void BeforeRender()
	{
		base.BeforeRender();
		if (postcard != null)
		{
			postcard.BeforeRender();
		}
	}
}
