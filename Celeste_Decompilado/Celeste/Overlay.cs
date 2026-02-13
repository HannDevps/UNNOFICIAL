using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Overlay : Entity
{
	[CompilerGenerated]
	private sealed class _003CFadeIn_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Overlay _003C_003E4__this;

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
		public _003CFadeIn_003Ed__5(int _003C_003E1__state)
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
			Overlay overlay = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				overlay.Fade += Engine.DeltaTime * 4f;
				break;
			}
			if (overlay.Fade < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			overlay.Fade = 1f;
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
	private sealed class _003CFadeOut_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Overlay _003C_003E4__this;

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
		public _003CFadeOut_003Ed__6(int _003C_003E1__state)
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
			Overlay overlay = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				overlay.Fade -= Engine.DeltaTime * 4f;
				break;
			}
			if (overlay.Fade > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
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

	public float Fade;

	public bool XboxOverlay;

	public Overlay()
	{
		base.Tag = Tags.HUD;
		base.Depth = -100000;
	}

	public override void Added(Scene scene)
	{
		if (scene is IOverlayHandler overlayHandler)
		{
			overlayHandler.Overlay = this;
		}
		base.Added(scene);
	}

	public override void Removed(Scene scene)
	{
		if (scene is IOverlayHandler overlayHandler && overlayHandler.Overlay == this)
		{
			overlayHandler.Overlay = null;
		}
		base.Removed(scene);
	}

	[IteratorStateMachine(typeof(_003CFadeIn_003Ed__5))]
	public IEnumerator FadeIn()
	{
		while (Fade < 1f)
		{
			yield return null;
			Fade += Engine.DeltaTime * 4f;
		}
		Fade = 1f;
	}

	[IteratorStateMachine(typeof(_003CFadeOut_003Ed__6))]
	public IEnumerator FadeOut()
	{
		while (Fade > 0f)
		{
			yield return null;
			Fade -= Engine.DeltaTime * 4f;
		}
	}

	public void RenderFade()
	{
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeInOut(Fade) * 0.95f);
	}
}
