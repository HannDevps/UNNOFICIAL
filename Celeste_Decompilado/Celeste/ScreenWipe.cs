using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public abstract class ScreenWipe : Renderer
{
	[CompilerGenerated]
	private sealed class _003CWait_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ScreenWipe _003C_003E4__this;

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
		public _003CWait_003Ed__16(int _003C_003E1__state)
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
			ScreenWipe screenWipe = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (screenWipe.Percent < 1f)
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

	public static Color WipeColor = Color.Black;

	public Scene Scene;

	public bool WipeIn;

	public float Percent;

	public Action OnComplete;

	public bool Completed;

	public float Duration = 0.5f;

	public float EndTimer;

	private bool ending;

	public const int Left = -10;

	public const int Top = -10;

	public int Right => 1930;

	public int Bottom => 1090;

	public ScreenWipe(Scene scene, bool wipeIn, Action onComplete = null)
	{
		Scene = scene;
		WipeIn = wipeIn;
		if (Scene is Level)
		{
			(Scene as Level).Wipe = this;
		}
		Scene.Add(this);
		OnComplete = onComplete;
	}

	[IteratorStateMachine(typeof(_003CWait_003Ed__16))]
	public IEnumerator Wait()
	{
		while (Percent < 1f)
		{
			yield return null;
		}
	}

	public override void Update(Scene scene)
	{
		if (!Completed)
		{
			if (Percent < 1f)
			{
				Percent = Calc.Approach(Percent, 1f, Engine.RawDeltaTime / Duration);
			}
			else if (EndTimer > 0f)
			{
				EndTimer -= Engine.RawDeltaTime;
			}
			else
			{
				Completed = true;
			}
		}
		else if (!ending)
		{
			ending = true;
			scene.Remove(this);
			if (scene is Level && (scene as Level).Wipe == this)
			{
				(scene as Level).Wipe = null;
			}
			if (OnComplete != null)
			{
				OnComplete();
			}
		}
	}

	public virtual void Cancel()
	{
		Scene.Remove(this);
		if (Scene is Level)
		{
			(Scene as Level).Wipe = null;
		}
	}

	public static void DrawPrimitives(VertexPositionColor[] vertices)
	{
		GFX.DrawVertices(Matrix.CreateScale((float)Engine.Graphics.GraphicsDevice.Viewport.Width / 1920f), vertices, vertices.Length);
	}
}
