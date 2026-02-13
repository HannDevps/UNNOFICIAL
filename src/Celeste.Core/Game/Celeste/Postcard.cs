using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class Postcard : Entity
{
	[CompilerGenerated]
	private sealed class _003CDisplayRoutine_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Postcard _003C_003E4__this;

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
		public _003CDisplayRoutine_003Ed__13(int _003C_003E1__state)
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
			Postcard postcard = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = postcard.EaseIn();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.75f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_008a;
			case 3:
				_003C_003E1__state = -1;
				goto IL_008a;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_008a:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				Audio.Play("event:/ui/main/button_lowkey");
				_003C_003E2__current = postcard.EaseOut();
				_003C_003E1__state = 4;
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

	[CompilerGenerated]
	private sealed class _003CEaseIn_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Postcard _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

		private Vector2 _003Cto_003E5__3;

		private float _003CrFrom_003E5__4;

		private float _003CrTo_003E5__5;

		private float _003Cp_003E5__6;

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
		public _003CEaseIn_003Ed__14(int _003C_003E1__state)
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
			Postcard postcard = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Audio.Play(postcard.sfxEventIn);
				Vector2 vector = new Vector2(Engine.Width, Engine.Height) / 2f;
				_003Cfrom_003E5__2 = vector + new Vector2(0f, 200f);
				_003Cto_003E5__3 = vector;
				_003CrFrom_003E5__4 = -0.1f;
				_003CrTo_003E5__5 = 0.05f;
				postcard.Visible = true;
				_003Cp_003E5__6 = 0f;
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__6 += Engine.DeltaTime * 0.8f;
				break;
			}
			if (_003Cp_003E5__6 < 1f)
			{
				postcard.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeOut(_003Cp_003E5__6);
				postcard.alpha = Ease.CubeOut(_003Cp_003E5__6);
				postcard.rotation = _003CrFrom_003E5__4 + (_003CrTo_003E5__5 - _003CrFrom_003E5__4) * Ease.CubeOut(_003Cp_003E5__6);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			postcard.Add(postcard.easeButtonIn = new Coroutine(postcard.EaseButtinIn()));
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
	private sealed class _003CEaseButtinIn_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Postcard _003C_003E4__this;

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
		public _003CEaseButtinIn_003Ed__15(int _003C_003E1__state)
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
			Postcard postcard = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.75f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if ((postcard.buttonEase += Engine.DeltaTime * 2f) < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
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

	[CompilerGenerated]
	private sealed class _003CEaseOut_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Postcard _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

		private Vector2 _003Cto_003E5__3;

		private float _003CrFrom_003E5__4;

		private float _003CrTo_003E5__5;

		private float _003Cp_003E5__6;

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
		public _003CEaseOut_003Ed__16(int _003C_003E1__state)
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
			Postcard postcard = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play(postcard.sfxEventOut);
				if (postcard.easeButtonIn != null)
				{
					postcard.easeButtonIn.RemoveSelf();
				}
				_003Cfrom_003E5__2 = postcard.Position;
				_003Cto_003E5__3 = new Vector2(Engine.Width, Engine.Height) / 2f + new Vector2(0f, -200f);
				_003CrFrom_003E5__4 = postcard.rotation;
				_003CrTo_003E5__5 = postcard.rotation + 0.1f;
				_003Cp_003E5__6 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__6 += Engine.DeltaTime;
				break;
			}
			if (_003Cp_003E5__6 < 1f)
			{
				postcard.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeIn(_003Cp_003E5__6);
				postcard.alpha = 1f - Ease.CubeIn(_003Cp_003E5__6);
				postcard.rotation = _003CrFrom_003E5__4 + (_003CrTo_003E5__5 - _003CrFrom_003E5__4) * Ease.CubeIn(_003Cp_003E5__6);
				postcard.buttonEase = Calc.Approach(postcard.buttonEase, 0f, Engine.DeltaTime * 8f);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			postcard.alpha = 0f;
			postcard.RemoveSelf();
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

	private const float TextScale = 0.7f;

	private MTexture postcard;

	private VirtualRenderTarget target;

	private FancyText.Text text;

	private float alpha = 1f;

	private float scale = 1f;

	private float rotation;

	private float buttonEase;

	private string sfxEventIn;

	private string sfxEventOut;

	private Coroutine easeButtonIn;

	public Postcard(string msg, int area)
		: this(msg, "event:/ui/main/postcard_ch" + area + "_in", "event:/ui/main/postcard_ch" + area + "_out")
	{
	}

	public Postcard(string msg, string sfxEventIn, string sfxEventOut)
	{
		Visible = false;
		base.Tag = Tags.HUD;
		this.sfxEventIn = sfxEventIn;
		this.sfxEventOut = sfxEventOut;
		postcard = GFX.Gui["postcard"];
		text = FancyText.Parse(msg, (int)((float)(postcard.Width - 120) / 0.7f), -1, 1f, Color.Black * 0.6f);
	}

	[IteratorStateMachine(typeof(_003CDisplayRoutine_003Ed__13))]
	public IEnumerator DisplayRoutine()
	{
		yield return EaseIn();
		yield return 0.75f;
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		Audio.Play("event:/ui/main/button_lowkey");
		yield return EaseOut();
		yield return 1.2f;
	}

	[IteratorStateMachine(typeof(_003CEaseIn_003Ed__14))]
	public IEnumerator EaseIn()
	{
		Audio.Play(sfxEventIn);
		Vector2 vector = new Vector2(Engine.Width, Engine.Height) / 2f;
		Vector2 from = vector + new Vector2(0f, 200f);
		Vector2 to = vector;
		float rFrom = -0.1f;
		float rTo = 0.05f;
		Visible = true;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 0.8f)
		{
			Position = from + (to - from) * Ease.CubeOut(p);
			alpha = Ease.CubeOut(p);
			rotation = rFrom + (rTo - rFrom) * Ease.CubeOut(p);
			yield return null;
		}
		Add(easeButtonIn = new Coroutine(EaseButtinIn()));
	}

	[IteratorStateMachine(typeof(_003CEaseButtinIn_003Ed__15))]
	private IEnumerator EaseButtinIn()
	{
		yield return 0.75f;
		while ((buttonEase += Engine.DeltaTime * 2f) < 1f)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CEaseOut_003Ed__16))]
	public IEnumerator EaseOut()
	{
		Audio.Play(sfxEventOut);
		if (easeButtonIn != null)
		{
			easeButtonIn.RemoveSelf();
		}
		Vector2 from = Position;
		Vector2 to = new Vector2(Engine.Width, Engine.Height) / 2f + new Vector2(0f, -200f);
		float rFrom = rotation;
		float rTo = rotation + 0.1f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			Position = from + (to - from) * Ease.CubeIn(p);
			alpha = 1f - Ease.CubeIn(p);
			rotation = rFrom + (rTo - rFrom) * Ease.CubeIn(p);
			buttonEase = Calc.Approach(buttonEase, 0f, Engine.DeltaTime * 8f);
			yield return null;
		}
		alpha = 0f;
		RemoveSelf();
	}

	public void BeforeRender()
	{
		if (target == null)
		{
			target = VirtualContent.CreateRenderTarget("postcard", postcard.Width, postcard.Height);
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(target);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin();
		string text = Dialog.Clean("FILE_DEFAULT");
		if (SaveData.Instance != null && Dialog.Language.CanDisplay(SaveData.Instance.Name))
		{
			text = SaveData.Instance.Name;
		}
		postcard.Draw(Vector2.Zero);
		ActiveFont.Draw(text, new Vector2(115f, 30f), Vector2.Zero, Vector2.One * 0.9f, Color.Black * 0.7f);
		this.text.DrawJustifyPerLine(new Vector2(postcard.Width, postcard.Height) / 2f + new Vector2(0f, 40f), new Vector2(0.5f, 0.5f), Vector2.One * 0.7f, 1f);
		Draw.SpriteBatch.End();
	}

	public override void Render()
	{
		if (target != null)
		{
			Draw.SpriteBatch.Draw((RenderTarget2D)target, Position, target.Bounds, Color.White * alpha, rotation, new Vector2(target.Width, target.Height) / 2f, scale, SpriteEffects.None, 0f);
		}
		if (buttonEase > 0f)
		{
			Input.GuiButton(Input.MenuConfirm).DrawCentered(new Vector2(Engine.Width - 120, (float)(Engine.Height - 100) - 20f * Ease.CubeOut(buttonEase)), Color.White * Ease.CubeOut(buttonEase));
		}
	}

	public override void Removed(Scene scene)
	{
		Dispose();
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Dispose();
		base.SceneEnd(scene);
	}

	private void Dispose()
	{
		if (target != null)
		{
			target.Dispose();
		}
		target = null;
	}
}
