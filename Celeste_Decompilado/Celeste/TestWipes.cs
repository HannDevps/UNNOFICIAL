using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class TestWipes : Scene
{
	[CompilerGenerated]
	private sealed class _003Croutine_003Ed__3 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TestWipes _003C_003E4__this;

		private float _003Cdur_003E5__2;

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
		public _003Croutine_003Ed__3(int _003C_003E1__state)
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
			TestWipes testWipes = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cdur_003E5__2 = 1f;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0074;
			case 2:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("ff0034");
				new AngledWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("0b0960");
				new DreamWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("39bf00");
				new KeyDoorWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("4376b3");
				new WindWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("ffae00");
				new DropWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("cc54ff");
				new FallWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Calc.HexToColor("ff007a");
				new MountainWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				testWipes.lastColor = ScreenWipe.WipeColor;
				ScreenWipe.WipeColor = Color.White;
				new HeartWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 10;
				return true;
			case 10:
				{
					_003C_003E1__state = -1;
					testWipes.lastColor = ScreenWipe.WipeColor;
					goto IL_0074;
				}
				IL_0074:
				ScreenWipe.WipeColor = Color.Black;
				new CurtainWipe(testWipes, wipeIn: false).Duration = _003Cdur_003E5__2;
				_003C_003E2__current = _003Cdur_003E5__2;
				_003C_003E1__state = 2;
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

	private Coroutine coroutine;

	private Color lastColor = Color.White;

	public TestWipes()
	{
		coroutine = new Coroutine(routine());
	}

	[IteratorStateMachine(typeof(_003Croutine_003Ed__3))]
	private IEnumerator routine()
	{
		float dur = 1f;
		yield return 1f;
		while (true)
		{
			ScreenWipe.WipeColor = Color.Black;
			new CurtainWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("ff0034");
			new AngledWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("0b0960");
			new DreamWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("39bf00");
			new KeyDoorWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("4376b3");
			new WindWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("ffae00");
			new DropWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("cc54ff");
			new FallWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Calc.HexToColor("ff007a");
			new MountainWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
			ScreenWipe.WipeColor = Color.White;
			new HeartWipe(this, wipeIn: false).Duration = dur;
			yield return dur;
			lastColor = ScreenWipe.WipeColor;
		}
	}

	public override void Update()
	{
		base.Update();
		coroutine.Update();
	}

	public override void Render()
	{
		Draw.SpriteBatch.Begin();
		Draw.Rect(-1f, -1f, 1920f, 1080f, lastColor);
		Draw.SpriteBatch.End();
		base.Render();
	}
}
