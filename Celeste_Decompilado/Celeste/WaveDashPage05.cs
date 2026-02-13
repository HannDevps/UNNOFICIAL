using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashPage05 : WaveDashPage
{
	private class Display
	{
		[CompilerGenerated]
		private sealed class _003CRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Display _003C_003E4__this;

			private PlayerPlayback _003Cplayback_003E5__2;

			private int _003Cstep_003E5__3;

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
			public _003CRoutine_003Ed__7(int _003C_003E1__state)
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
				Display display = _003C_003E4__this;
				int frameIndex;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Cplayback_003E5__2 = display.Tutorial.Playback;
					_003Cstep_003E5__3 = 0;
					goto IL_004d;
				case 1:
					_003C_003E1__state = -1;
					goto IL_00b3;
				case 2:
					_003C_003E1__state = -1;
					goto IL_011a;
				case 3:
					_003C_003E1__state = -1;
					goto IL_011a;
				case 4:
					_003C_003E1__state = -1;
					display.xEase = 0f;
					display.time = 0f;
					break;
				case 5:
					{
						_003C_003E1__state = -1;
						goto IL_004d;
					}
					IL_011a:
					if (display.xEase < 1f)
					{
						display.xEase = Calc.Approach(display.xEase, 1f, Engine.DeltaTime * 4f);
						_003C_003E2__current = null;
						_003C_003E1__state = 3;
						return true;
					}
					display.xEase = 1f;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 4;
					return true;
					IL_00b3:
					if (display.time < 3f)
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 2;
					return true;
					IL_004d:
					frameIndex = _003Cplayback_003E5__2.FrameIndex;
					if (_003Cstep_003E5__3 % 2 == 0)
					{
						display.Tutorial.Update();
					}
					if (frameIndex == _003Cplayback_003E5__2.FrameIndex || _003Cplayback_003E5__2.FrameIndex != _003Cplayback_003E5__2.FrameCount - 1)
					{
						break;
					}
					goto IL_00b3;
				}
				_003Cstep_003E5__3++;
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
				return true;
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

		public Vector2 Position;

		public FancyText.Text Info;

		public WaveDashPlaybackTutorial Tutorial;

		private Coroutine routine;

		private float xEase;

		private float time;

		public Display(Vector2 position, string text, string tutorial, Vector2 tutorialOffset)
		{
			Position = position;
			Info = FancyText.Parse(text, 896, 8, 1f, Color.Black * 0.6f);
			Tutorial = new WaveDashPlaybackTutorial(tutorial, tutorialOffset, new Vector2(1f, 1f), new Vector2(1f, 1f));
			Tutorial.OnRender = delegate
			{
				Draw.Line(-64f, 20f, 64f, 20f, Color.Black);
			};
			routine = new Coroutine(Routine());
		}

		[IteratorStateMachine(typeof(_003CRoutine_003Ed__7))]
		private IEnumerator Routine()
		{
			PlayerPlayback playback = Tutorial.Playback;
			int step = 0;
			while (true)
			{
				int frameIndex = playback.FrameIndex;
				if (step % 2 == 0)
				{
					Tutorial.Update();
				}
				if (frameIndex != playback.FrameIndex && playback.FrameIndex == playback.FrameCount - 1)
				{
					while (time < 3f)
					{
						yield return null;
					}
					yield return 0.1f;
					while (xEase < 1f)
					{
						xEase = Calc.Approach(xEase, 1f, Engine.DeltaTime * 4f);
						yield return null;
					}
					xEase = 1f;
					yield return 0.5f;
					xEase = 0f;
					time = 0f;
				}
				step++;
				yield return null;
			}
		}

		public void Update()
		{
			time += Engine.DeltaTime;
			routine.Update();
		}

		public void Render()
		{
			Tutorial.Render(Position, 4f);
			Info.DrawJustifyPerLine(Position + Vector2.UnitY * 200f, new Vector2(0.5f, 0f), Vector2.One * 0.8f, 1f);
			if (xEase > 0f)
			{
				Vector2 vector = Calc.AngleToVector((1f - xEase) * 0.1f + (float)Math.PI / 4f, 1f);
				Vector2 vector2 = vector.Perpendicular();
				float num = 0.5f + (1f - xEase) * 0.5f;
				float thickness = 64f * num;
				float num2 = 300f * num;
				Vector2 position = Position;
				Draw.Line(position - vector * num2, position + vector * num2, Color.Red, thickness);
				Draw.Line(position - vector2 * num2, position + vector2 * num2, Color.Red, thickness);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CRoutine_003Ed__4(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
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

	private List<Display> displays = new List<Display>();

	public WaveDashPage05()
	{
		Transition = Transitions.Spiral;
		ClearColor = Calc.HexToColor("fff2cc");
	}

	public override void Added(WaveDashPresentation presentation)
	{
		base.Added(presentation);
		displays.Add(new Display(new Vector2((float)base.Width * 0.28f, base.Height - 600), Dialog.Get("WAVEDASH_PAGE5_INFO1"), "too_close", new Vector2(-50f, 20f)));
		displays.Add(new Display(new Vector2((float)base.Width * 0.72f, base.Height - 600), Dialog.Get("WAVEDASH_PAGE5_INFO2"), "too_far", new Vector2(-50f, -35f)));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__4))]
	public override IEnumerator Routine()
	{
		yield return 0.5f;
	}

	public override void Update()
	{
		foreach (Display display in displays)
		{
			display.Update();
		}
	}

	public override void Render()
	{
		ActiveFont.DrawOutline(Dialog.Clean("WAVEDASH_PAGE5_TITLE"), new Vector2(128f, 100f), Vector2.Zero, Vector2.One * 1.5f, Color.White, 2f, Color.Black);
		foreach (Display display in displays)
		{
			display.Render();
		}
	}
}
