using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashPage00 : WaveDashPage
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage00 _003C_003E4__this;

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
		public _003CRoutine_003Ed__6(int _003C_003E1__state)
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
			WaveDashPage00 waveDashPage = _003C_003E4__this;
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
				_003C_003E2__current = waveDashPage.MoveCursor(waveDashPage.cursor + new Vector2(0f, -80f), 0.3f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = waveDashPage.MoveCursor(waveDashPage.pptIcon, 0.8f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				waveDashPage.selected = true;
				Audio.Play("event:/new_content/game/10_farewell/ppt_doubleclick");
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				waveDashPage.selected = false;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				waveDashPage.selected = true;
				_003C_003E2__current = 0.08f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				waveDashPage.selected = false;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				waveDashPage.Presentation.ScaleInPoint = waveDashPage.pptIcon;
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

	[CompilerGenerated]
	private sealed class _003CMoveCursor_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage00 _003C_003E4__this;

		public Vector2 to;

		public float time;

		private Vector2 _003Cfrom_003E5__2;

		private float _003Ct_003E5__3;

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
		public _003CMoveCursor_003Ed__7(int _003C_003E1__state)
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
			WaveDashPage00 waveDashPage = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = waveDashPage.cursor;
				_003Ct_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__3 += Engine.DeltaTime / time;
				break;
			}
			if (_003Ct_003E5__3 < 1f)
			{
				waveDashPage.cursor = _003Cfrom_003E5__2 + (to - _003Cfrom_003E5__2) * Ease.SineOut(_003Ct_003E5__3);
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

	private Color taskbarColor = Calc.HexToColor("d9d3b1");

	private string time;

	private Vector2 pptIcon;

	private Vector2 cursor;

	private bool selected;

	public WaveDashPage00()
	{
		AutoProgress = true;
		ClearColor = Calc.HexToColor("118475");
		time = DateTime.Now.ToString("h:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
		pptIcon = new Vector2(600f, 500f);
		cursor = new Vector2(1000f, 700f);
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__6))]
	public override IEnumerator Routine()
	{
		yield return 1f;
		yield return MoveCursor(cursor + new Vector2(0f, -80f), 0.3f);
		yield return 0.2f;
		yield return MoveCursor(pptIcon, 0.8f);
		yield return 0.7f;
		selected = true;
		Audio.Play("event:/new_content/game/10_farewell/ppt_doubleclick");
		yield return 0.1f;
		selected = false;
		yield return 0.1f;
		selected = true;
		yield return 0.08f;
		selected = false;
		yield return 0.5f;
		Presentation.ScaleInPoint = pptIcon;
	}

	[IteratorStateMachine(typeof(_003CMoveCursor_003Ed__7))]
	private IEnumerator MoveCursor(Vector2 to, float time)
	{
		Vector2 from = cursor;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / time)
		{
			cursor = from + (to - from) * Ease.SineOut(t);
			yield return null;
		}
	}

	public override void Update()
	{
	}

	public override void Render()
	{
		DrawIcon(new Vector2(160f, 120f), "desktop/mymountain_icon", Dialog.Clean("WAVEDASH_DESKTOP_MYPC"));
		DrawIcon(new Vector2(160f, 320f), "desktop/recyclebin_icon", Dialog.Clean("WAVEDASH_DESKTOP_RECYCLEBIN"));
		DrawIcon(pptIcon, "desktop/wavedashing_icon", Dialog.Clean("WAVEDASH_DESKTOP_POWERPOINT"));
		DrawTaskbar();
		Presentation.Gfx["desktop/cursor"].DrawCentered(cursor);
	}

	public void DrawTaskbar()
	{
		Draw.Rect(0f, (float)base.Height - 80f, base.Width, 80f, taskbarColor);
		Draw.Rect(0f, (float)base.Height - 80f, base.Width, 4f, Color.White * 0.5f);
		MTexture mTexture = Presentation.Gfx["desktop/startberry"];
		float num = 64f;
		float num2 = num / (float)mTexture.Height * 0.7f;
		string text = Dialog.Clean("WAVEDASH_DESKTOP_STARTBUTTON");
		float num3 = 0.6f;
		float width = (float)mTexture.Width * num2 + ActiveFont.Measure(text).X * num3 + 32f;
		Vector2 vector = new Vector2(8f, (float)base.Height - 80f + 8f);
		Draw.Rect(vector.X, vector.Y, width, num, Color.White * 0.5f);
		mTexture.DrawJustified(vector + new Vector2(8f, num / 2f), new Vector2(0f, 0.5f), Color.White, Vector2.One * num2);
		ActiveFont.Draw(text, vector + new Vector2((float)mTexture.Width * num2 + 16f, num / 2f), new Vector2(0f, 0.5f), Vector2.One * num3, Color.Black * 0.8f);
		ActiveFont.Draw(time, new Vector2((float)base.Width - 24f, (float)base.Height - 40f), new Vector2(1f, 0.5f), Vector2.One * 0.6f, Color.Black * 0.8f);
	}

	private void DrawIcon(Vector2 position, string icon, string text)
	{
		bool flag = cursor.X > position.X - 64f && cursor.Y > position.Y - 64f && cursor.X < position.X + 64f && cursor.Y < position.Y + 80f;
		if (selected && flag)
		{
			Draw.Rect(position.X - 80f, position.Y - 80f, 160f, 200f, Color.White * 0.25f);
		}
		if (flag)
		{
			DrawDottedRect(position.X - 80f, position.Y - 80f, 160f, 200f);
		}
		MTexture mTexture = Presentation.Gfx[icon];
		float scale = 128f / (float)mTexture.Height;
		mTexture.DrawCentered(position, Color.White, scale);
		ActiveFont.Draw(text, position + new Vector2(0f, 80f), new Vector2(0.5f, 0f), Vector2.One * 0.6f, (selected && flag) ? Color.Black : Color.White);
	}

	private void DrawDottedRect(float x, float y, float w, float h)
	{
		float num = 4f;
		Draw.Rect(x, y, w, num, Color.White);
		Draw.Rect(x + w - num, y, num, h, Color.White);
		Draw.Rect(x, y, num, h, Color.White);
		Draw.Rect(x, y + h - num, w, num, Color.White);
		if (!selected)
		{
			for (float num2 = 4f; num2 < w; num2 += num * 2f)
			{
				Draw.Rect(x + num2, y, num, num, ClearColor);
				Draw.Rect(x + w - num2, y + h - num, num, num, ClearColor);
			}
			for (float num3 = 4f; num3 < h; num3 += num * 2f)
			{
				Draw.Rect(x, y + num3, num, num, ClearColor);
				Draw.Rect(x + w - num, y + h - num3, num, num, ClearColor);
			}
		}
	}
}
