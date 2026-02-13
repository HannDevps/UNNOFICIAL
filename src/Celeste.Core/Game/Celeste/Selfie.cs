using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Selfie : Entity
{
	[CompilerGenerated]
	private sealed class _003CPictureRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Selfie _003C_003E4__this;

		public string photo;

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
		public _003CPictureRoutine_003Ed__7(int _003C_003E1__state)
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
			Selfie selfie = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				selfie.level.Flash(Color.White);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = selfie.OpenRoutine(photo);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = selfie.WaitForInput();
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = selfie.EndRoutine();
				_003C_003E1__state = 4;
				return true;
			case 4:
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

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public MTexture tex;

		public int atWidth;

		public Selfie _003C_003E4__this;

		internal void _003CFilterRoutine_003Eb__0(Tween t)
		{
			int num = (int)Math.Round(MathHelper.Lerp(0f, tex.Width, t.Eased));
			if (num != atWidth)
			{
				atWidth = num;
				_003C_003E4__this.overImage.Texture = tex.GetSubtexture(tex.Width - atWidth, 0, atWidth, tex.Height);
				_003C_003E4__this.overImage.Visible = true;
				_003C_003E4__this.overImage.Origin.X = atWidth - tex.Width / 2;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CFilterRoutine_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Selfie _003C_003E4__this;

		private _003C_003Ec__DisplayClass8_0 _003C_003E8__1;

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
		public _003CFilterRoutine_003Ed__8(int _003C_003E1__state)
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
			Selfie selfie = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass8_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E2__current = selfie.OpenRoutine();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E8__1.tex = GFX.Portraits["selfieFilter"];
				selfie.overImage = new Image(_003C_003E8__1.tex);
				selfie.overImage.Visible = false;
				selfie.overImage.CenterOrigin();
				_003C_003E8__1.atWidth = 0;
				selfie.tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 0.4f, start: true);
				selfie.tween.OnUpdate = delegate(Tween t)
				{
					int num2 = (int)Math.Round(MathHelper.Lerp(0f, _003C_003E8__1.tex.Width, t.Eased));
					if (num2 != _003C_003E8__1.atWidth)
					{
						_003C_003E8__1.atWidth = num2;
						_003C_003E8__1._003C_003E4__this.overImage.Texture = _003C_003E8__1.tex.GetSubtexture(_003C_003E8__1.tex.Width - _003C_003E8__1.atWidth, 0, _003C_003E8__1.atWidth, _003C_003E8__1.tex.Height);
						_003C_003E8__1._003C_003E4__this.overImage.Visible = true;
						_003C_003E8__1._003C_003E4__this.overImage.Origin.X = _003C_003E8__1.atWidth - _003C_003E8__1.tex.Width / 2;
					}
				};
				Audio.Play("event:/game/02_old_site/theoselfie_photo_filter");
				_003C_003E2__current = selfie.tween.Wait();
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = selfie.WaitForInput();
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = selfie.EndRoutine();
				_003C_003E1__state = 5;
				return true;
			case 5:
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

	[CompilerGenerated]
	private sealed class _003COpenRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Selfie _003C_003E4__this;

		public string selfie;

		private float _003Cpercent_003E5__2;

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
		public _003COpenRoutine_003Ed__9(int _003C_003E1__state)
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
			Selfie selfie = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/02_old_site/theoselfie_photo_in");
				selfie.image = new Image(GFX.Portraits[this.selfie]);
				selfie.image.CenterOrigin();
				_003Cpercent_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Cpercent_003E5__2 < 1f)
			{
				_003Cpercent_003E5__2 += Engine.DeltaTime;
				selfie.image.Position = Vector2.Lerp(new Vector2(992f, 1080f + selfie.image.Height / 2f), new Vector2(960f, 540f), Ease.CubeOut(_003Cpercent_003E5__2));
				selfie.image.Rotation = MathHelper.Lerp(0.5f, 0f, Ease.BackOut(_003Cpercent_003E5__2));
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

	[CompilerGenerated]
	private sealed class _003CWaitForInput_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Selfie _003C_003E4__this;

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
		public _003CWaitForInput_003Ed__10(int _003C_003E1__state)
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
			Selfie selfie = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				selfie.waitForKeyPress = true;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (!Input.MenuCancel.Pressed && !Input.MenuConfirm.Pressed)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			Audio.Play("event:/ui/main/button_lowkey");
			selfie.waitForKeyPress = false;
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
	private sealed class _003CEndRoutine_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Selfie _003C_003E4__this;

		private float _003Cpercent_003E5__2;

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
		public _003CEndRoutine_003Ed__11(int _003C_003E1__state)
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
			Selfie selfie = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/02_old_site/theoselfie_photo_out");
				_003Cpercent_003E5__2 = 0f;
				goto IL_00e8;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00e8;
			case 2:
				{
					_003C_003E1__state = -1;
					selfie.level.Remove(selfie);
					return false;
				}
				IL_00e8:
				if (_003Cpercent_003E5__2 < 1f)
				{
					_003Cpercent_003E5__2 += Engine.DeltaTime * 2f;
					selfie.image.Position = Vector2.Lerp(new Vector2(960f, 540f), new Vector2(928f, (0f - selfie.image.Height) / 2f), Ease.BackIn(_003Cpercent_003E5__2));
					selfie.image.Rotation = MathHelper.Lerp(0f, -0.15f, Ease.BackIn(_003Cpercent_003E5__2));
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = null;
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

	private Level level;

	private Image image;

	private Image overImage;

	private bool waitForKeyPress;

	private float timer;

	private Tween tween;

	public Selfie(Level level)
	{
		base.Tag = Tags.HUD;
		this.level = level;
	}

	[IteratorStateMachine(typeof(_003CPictureRoutine_003Ed__7))]
	public IEnumerator PictureRoutine(string photo = "selfie")
	{
		level.Flash(Color.White);
		yield return 0.5f;
		yield return OpenRoutine(photo);
		yield return WaitForInput();
		yield return EndRoutine();
	}

	[IteratorStateMachine(typeof(_003CFilterRoutine_003Ed__8))]
	public IEnumerator FilterRoutine()
	{
		yield return OpenRoutine();
		yield return 0.5f;
		MTexture tex = GFX.Portraits["selfieFilter"];
		overImage = new Image(tex);
		overImage.Visible = false;
		overImage.CenterOrigin();
		int atWidth = 0;
		tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 0.4f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			int num = (int)Math.Round(MathHelper.Lerp(0f, tex.Width, t.Eased));
			if (num != atWidth)
			{
				atWidth = num;
				overImage.Texture = tex.GetSubtexture(tex.Width - atWidth, 0, atWidth, tex.Height);
				overImage.Visible = true;
				overImage.Origin.X = atWidth - tex.Width / 2;
			}
		};
		Audio.Play("event:/game/02_old_site/theoselfie_photo_filter");
		yield return tween.Wait();
		yield return WaitForInput();
		yield return EndRoutine();
	}

	[IteratorStateMachine(typeof(_003COpenRoutine_003Ed__9))]
	public IEnumerator OpenRoutine(string selfie = "selfie")
	{
		Audio.Play("event:/game/02_old_site/theoselfie_photo_in");
		image = new Image(GFX.Portraits[selfie]);
		image.CenterOrigin();
		float percent = 0f;
		while (percent < 1f)
		{
			percent += Engine.DeltaTime;
			image.Position = Vector2.Lerp(new Vector2(992f, 1080f + image.Height / 2f), new Vector2(960f, 540f), Ease.CubeOut(percent));
			image.Rotation = MathHelper.Lerp(0.5f, 0f, Ease.BackOut(percent));
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CWaitForInput_003Ed__10))]
	public IEnumerator WaitForInput()
	{
		waitForKeyPress = true;
		while (!Input.MenuCancel.Pressed && !Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		Audio.Play("event:/ui/main/button_lowkey");
		waitForKeyPress = false;
	}

	[IteratorStateMachine(typeof(_003CEndRoutine_003Ed__11))]
	public IEnumerator EndRoutine()
	{
		Audio.Play("event:/game/02_old_site/theoselfie_photo_out");
		float percent = 0f;
		while (percent < 1f)
		{
			percent += Engine.DeltaTime * 2f;
			image.Position = Vector2.Lerp(new Vector2(960f, 540f), new Vector2(928f, (0f - image.Height) / 2f), Ease.BackIn(percent));
			image.Rotation = MathHelper.Lerp(0f, -0.15f, Ease.BackIn(percent));
			yield return null;
		}
		yield return null;
		level.Remove(this);
	}

	public override void Update()
	{
		if (tween != null && tween.Active)
		{
			tween.Update();
		}
		if (waitForKeyPress)
		{
			timer += Engine.DeltaTime;
		}
	}

	public override void Render()
	{
		if (base.Scene is Level level && (level.FrozenOrPaused || level.RetryPlayerCorpse != null || level.SkippingCutscene))
		{
			return;
		}
		if (image != null && image.Visible)
		{
			image.Render();
			if (overImage != null && overImage.Visible)
			{
				overImage.Position = image.Position;
				overImage.Rotation = image.Rotation;
				overImage.Scale = image.Scale;
				overImage.Render();
			}
		}
		if (waitForKeyPress)
		{
			GFX.Gui["textboxbutton"].DrawCentered(image.Position + new Vector2(image.Width / 2f + 40f, image.Height / 2f + (float)((timer % 1f < 0.25f) ? 6 : 0)));
		}
	}
}
