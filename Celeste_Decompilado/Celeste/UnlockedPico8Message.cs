using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class UnlockedPico8Message : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UnlockedPico8Message _003C_003E4__this;

		private Level _003Clevel_003E5__2;

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
			UnlockedPico8Message unlockedPico8Message = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = unlockedPico8Message.Scene as Level;
				_003Clevel_003E5__2.PauseLock = true;
				_003Clevel_003E5__2.Paused = true;
				goto IL_006f;
			case 1:
				_003C_003E1__state = -1;
				goto IL_006f;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00bc;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_006f:
				if ((unlockedPico8Message.alpha += Engine.DeltaTime / 0.5f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				unlockedPico8Message.alpha = 1f;
				unlockedPico8Message.waitForKeyPress = true;
				goto IL_00bc;
				IL_00bc:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				unlockedPico8Message.waitForKeyPress = false;
				break;
			}
			if ((unlockedPico8Message.alpha -= Engine.DeltaTime / 0.5f) > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			unlockedPico8Message.alpha = 0f;
			_003Clevel_003E5__2.PauseLock = false;
			_003Clevel_003E5__2.Paused = false;
			unlockedPico8Message.RemoveSelf();
			if (unlockedPico8Message.callback != null)
			{
				unlockedPico8Message.callback();
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

	private float alpha;

	private string text;

	private bool waitForKeyPress;

	private float timer;

	private Action callback;

	public UnlockedPico8Message(Action callback = null)
	{
		this.callback = callback;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		base.Tag = (int)Tags.HUD | (int)Tags.PauseUpdate;
		text = ActiveFont.FontSize.AutoNewline(Dialog.Clean("PICO8_UNLOCKED"), 900);
		base.Depth = -10000;
		Add(new Coroutine(Routine()));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__7))]
	private IEnumerator Routine()
	{
		Level level = base.Scene as Level;
		level.PauseLock = true;
		level.Paused = true;
		while ((alpha += Engine.DeltaTime / 0.5f) < 1f)
		{
			yield return null;
		}
		alpha = 1f;
		waitForKeyPress = true;
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		waitForKeyPress = false;
		while ((alpha -= Engine.DeltaTime / 0.5f) > 0f)
		{
			yield return null;
		}
		alpha = 0f;
		level.PauseLock = false;
		level.Paused = false;
		RemoveSelf();
		if (callback != null)
		{
			callback();
		}
	}

	public override void Update()
	{
		timer += Engine.DeltaTime;
		base.Update();
	}

	public override void Render()
	{
		float num = Ease.CubeOut(alpha);
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * num * 0.8f);
		GFX.Gui["pico8"].DrawJustified(Celeste.TargetCenter + new Vector2(0f, -64f * (1f - num) - 16f), new Vector2(0.5f, 1f), Color.White * num);
		Vector2 position = Celeste.TargetCenter + new Vector2(0f, 64f * (1f - num) + 16f);
		Vector2 vector = ActiveFont.Measure(text);
		ActiveFont.Draw(text, position, new Vector2(0.5f, 0f), Vector2.One, Color.White * num);
		if (waitForKeyPress)
		{
			GFX.Gui["textboxbutton"].DrawCentered(Celeste.TargetCenter + new Vector2(vector.X / 2f + 32f, vector.Y + 48f + (float)((timer % 1f < 0.25f) ? 6 : 0)));
		}
	}
}
