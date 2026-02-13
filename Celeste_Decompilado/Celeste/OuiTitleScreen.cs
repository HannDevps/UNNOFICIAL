using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiTitleScreen : Oui
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public OuiTitleScreen _003C_003E4__this;

		public float start;

		internal void _003CEnter_003Eb__0(Tween t)
		{
			_003C_003E4__this.alpha = t.Percent;
			_003C_003E4__this.textY = MathHelper.Lerp(start, 1000f, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiTitleScreen _003C_003E4__this;

		private _003C_003Ec__DisplayClass14_0 _003C_003E8__1;

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
		public _003CEnter_003Ed__14(int _003C_003E1__state)
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
			OuiTitleScreen ouiTitleScreen = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass14_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				ouiTitleScreen.Overworld.ShowInputUI = false;
				MountainCamera camera = ouiTitleScreen.Overworld.Mountain.Camera;
				Vector3 rotateLookAt = MountainRenderer.RotateLookAt;
				Vector3 vector = (camera.Position - new Vector3(rotateLookAt.X, camera.Position.Y - 2f, rotateLookAt.Z)).SafeNormalize();
				MountainCamera transform = new MountainCamera(MountainRenderer.RotateLookAt + vector * 20f, camera.Target);
				ouiTitleScreen.Add(new Coroutine(ouiTitleScreen.FadeBgTo(1f)));
				ouiTitleScreen.hideConfirmButton = false;
				ouiTitleScreen.Visible = true;
				ouiTitleScreen.Overworld.Mountain.EaseCamera(-1, transform, 2f, nearTarget: false);
				_003C_003E8__1.start = ouiTitleScreen.textY;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 2;
				return true;
			}
			case 2:
			{
				_003C_003E1__state = -1;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.6f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1._003C_003E4__this.alpha = t.Percent;
					_003C_003E8__1._003C_003E4__this.textY = MathHelper.Lerp(_003C_003E8__1.start, 1000f, t.Eased);
				};
				ouiTitleScreen.Add(tween);
				_003C_003E2__current = tween.Wait();
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
				_003C_003E1__state = -1;
				ouiTitleScreen.Overworld.Mountain.SnapCamera(-1, MountainTarget);
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
	private sealed class _003C_003Ec__DisplayClass15_0
	{
		public OuiTitleScreen _003C_003E4__this;

		public float start;

		internal void _003CLeave_003Eb__0(Tween t)
		{
			_003C_003E4__this.alpha = 1f - t.Percent;
			_003C_003E4__this.textY = MathHelper.Lerp(start, 1200f, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003CLeave_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiTitleScreen _003C_003E4__this;

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
		public _003CLeave_003Ed__15(int _003C_003E1__state)
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
			OuiTitleScreen ouiTitleScreen = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass15_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass15_0
				{
					_003C_003E4__this = _003C_003E4__this
				};
				ouiTitleScreen.Overworld.ShowInputUI = true;
				ouiTitleScreen.Overworld.Mountain.GotoRotationMode();
				CS_0024_003C_003E8__locals4.start = ouiTitleScreen.textY;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.6f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals4._003C_003E4__this.alpha = 1f - t.Percent;
					CS_0024_003C_003E8__locals4._003C_003E4__this.textY = MathHelper.Lerp(CS_0024_003C_003E8__locals4.start, 1200f, t.Eased);
				};
				ouiTitleScreen.Add(tween);
				_003C_003E2__current = tween.Wait();
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = ouiTitleScreen.FadeBgTo(0f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				ouiTitleScreen.Visible = false;
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
	private sealed class _003CFadeBgTo_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiTitleScreen _003C_003E4__this;

		public float to;

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
		public _003CFadeBgTo_003Ed__16(int _003C_003E1__state)
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
			OuiTitleScreen ouiTitleScreen = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				ouiTitleScreen.fade = Calc.Approach(ouiTitleScreen.fade, to, Engine.DeltaTime * 2f);
				break;
			}
			if (ouiTitleScreen.fade != to)
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

	public static readonly MountainCamera MountainTarget = new MountainCamera(new Vector3(0f, 12f, 24f), MountainRenderer.RotateLookAt);

	private const float TextY = 1000f;

	private const float TextOutY = 1200f;

	private const int ReflectionSliceSize = 4;

	private float alpha;

	private float fade;

	private string version = "v." + Celeste.Instance.Version;

	private bool hideConfirmButton;

	private Image logo;

	private MTexture title;

	private List<MTexture> reflections;

	private float textY;

	public OuiTitleScreen()
	{
		logo = new Image(GFX.Gui["logo"]);
		logo.CenterOrigin();
		logo.Position = new Vector2(1920f, 1080f) / 2f;
		title = GFX.Gui["title"];
		reflections = new List<MTexture>();
		for (int num = title.Height - 4; num > 0; num -= 4)
		{
			reflections.Add(title.GetSubtexture(0, num, title.Width, 4));
		}
		if (Celeste.PlayMode != Celeste.PlayModes.Normal)
		{
			if ("".Length > 0)
			{
				version += "\n";
			}
			version = version + "\n" + Celeste.PlayMode.ToString() + " Build";
		}
		if (Settings.Instance.LaunchWithFMODLiveUpdate)
		{
			version += "\nFMOD Live Update Enabled";
		}
	}

	public override bool IsStart(Overworld overworld, Overworld.StartMode start)
	{
		if (start == Overworld.StartMode.Titlescreen)
		{
			overworld.ShowInputUI = false;
			overworld.Mountain.SnapCamera(-1, MountainTarget);
			textY = 1000f;
			alpha = 1f;
			fade = 1f;
			return true;
		}
		textY = 1200f;
		return false;
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__14))]
	public override IEnumerator Enter(Oui from)
	{
		yield return null;
		base.Overworld.ShowInputUI = false;
		MountainCamera camera = base.Overworld.Mountain.Camera;
		Vector3 rotateLookAt = MountainRenderer.RotateLookAt;
		Vector3 vector = (camera.Position - new Vector3(rotateLookAt.X, camera.Position.Y - 2f, rotateLookAt.Z)).SafeNormalize();
		MountainCamera transform = new MountainCamera(MountainRenderer.RotateLookAt + vector * 20f, camera.Target);
		Add(new Coroutine(FadeBgTo(1f)));
		hideConfirmButton = false;
		Visible = true;
		base.Overworld.Mountain.EaseCamera(-1, transform, 2f, nearTarget: false);
		float start = textY;
		yield return 0.4f;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.6f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			alpha = t.Percent;
			textY = MathHelper.Lerp(start, 1000f, t.Eased);
		};
		Add(tween);
		yield return tween.Wait();
		base.Overworld.Mountain.SnapCamera(-1, MountainTarget);
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__15))]
	public override IEnumerator Leave(Oui next)
	{
		base.Overworld.ShowInputUI = true;
		base.Overworld.Mountain.GotoRotationMode();
		float start = textY;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.6f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			alpha = 1f - t.Percent;
			textY = MathHelper.Lerp(start, 1200f, t.Eased);
		};
		Add(tween);
		yield return tween.Wait();
		yield return FadeBgTo(0f);
		Visible = false;
	}

	[IteratorStateMachine(typeof(_003CFadeBgTo_003Ed__16))]
	private IEnumerator FadeBgTo(float to)
	{
		while (fade != to)
		{
			yield return null;
			fade = Calc.Approach(fade, to, Engine.DeltaTime * 2f);
		}
	}

	public override void Update()
	{
		int gamepadIndex = -1;
		if (base.Selected && Input.AnyGamepadConfirmPressed(out gamepadIndex) && !hideConfirmButton)
		{
			Input.Gamepad = gamepadIndex;
			Audio.Play("event:/ui/main/title_firstinput");
			base.Overworld.Goto<OuiMainMenu>();
		}
		base.Update();
	}

	public override void Render()
	{
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * fade);
		if (!hideConfirmButton)
		{
			Input.GuiButton(Input.MenuConfirm).DrawJustified(new Vector2(1840f, textY), new Vector2(1f, 1f), Color.White * alpha, 1f);
		}
		ActiveFont.Draw(version, new Vector2(80f, textY), new Vector2(0f, 1f), Vector2.One * 0.5f, Color.DarkSlateBlue);
		if (alpha > 0f)
		{
			float num = MathHelper.Lerp(0.5f, 1f, Ease.SineOut(alpha));
			logo.Color = Color.White * alpha;
			logo.Scale = Vector2.One * num;
			logo.Render();
			float num2 = base.Scene.TimeActive * 3f;
			float num3 = 1f / (float)reflections.Count * ((float)Math.PI * 2f) * 2f;
			float num4 = (float)title.Width / logo.Width * num;
			for (int i = 0; i < reflections.Count; i++)
			{
				float num5 = (float)i / (float)reflections.Count;
				float x = (float)Math.Sin(num2) * 32f * num5;
				Vector2 position = new Vector2(1920f, 1080f) / 2f + new Vector2(x, logo.Height * 0.5f + (float)(i * 4)) * num4;
				float num6 = Ease.CubeIn(1f - num5) * alpha * 0.9f;
				reflections[i].DrawJustified(position, new Vector2(0.5f, 0.5f), Color.White * num6, new Vector2(1f, -1f) * num4);
				num2 += num3 * ((float)Math.Sin(base.Scene.TimeActive + (float)i * ((float)Math.PI * 2f) * 0.04f) + 1f);
			}
		}
	}
}
