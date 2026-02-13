using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiCredits : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiCredits _003C_003E4__this;

		private float _003Cp_003E5__2;

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
		public _003CEnter_003Ed__5(int _003C_003E1__state)
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
			OuiCredits ouiCredits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/music/menu/credits");
				ouiCredits.Overworld.ShowConfirmUI = false;
				Credits.BorderColor = Color.Black;
				ouiCredits.credits = new Credits();
				ouiCredits.credits.Enabled = false;
				ouiCredits.Visible = true;
				ouiCredits.vignetteAlpha = 0f;
				_003Cp_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				ouiCredits.Position = ouiCredits.offScreen + (ouiCredits.onScreen - ouiCredits.offScreen) * Ease.CubeOut(_003Cp_003E5__2);
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
	private sealed class _003CLeave_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiCredits _003C_003E4__this;

		private float _003Cp_003E5__2;

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
		public _003CLeave_003Ed__6(int _003C_003E1__state)
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
			OuiCredits ouiCredits = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/ui/main/whoosh_large_out");
				ouiCredits.Overworld.SetNormalMusic();
				ouiCredits.Overworld.ShowConfirmUI = true;
				_003Cp_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				ouiCredits.Position = ouiCredits.onScreen + (ouiCredits.offScreen - ouiCredits.onScreen) * Ease.CubeIn(_003Cp_003E5__2);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiCredits.Visible = false;
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

	private readonly Vector2 onScreen = new Vector2(960f, 0f);

	private readonly Vector2 offScreen = new Vector2(3840f, 0f);

	private Credits credits;

	private float vignetteAlpha;

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Position = offScreen;
		Visible = false;
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__5))]
	public override IEnumerator Enter(Oui from)
	{
		Audio.SetMusic("event:/music/menu/credits");
		base.Overworld.ShowConfirmUI = false;
		Credits.BorderColor = Color.Black;
		credits = new Credits();
		credits.Enabled = false;
		Visible = true;
		vignetteAlpha = 0f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			Position = offScreen + (onScreen - offScreen) * Ease.CubeOut(p);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__6))]
	public override IEnumerator Leave(Oui next)
	{
		Audio.Play("event:/ui/main/whoosh_large_out");
		base.Overworld.SetNormalMusic();
		base.Overworld.ShowConfirmUI = true;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			Position = onScreen + (offScreen - onScreen) * Ease.CubeIn(p);
			yield return null;
		}
		Visible = false;
	}

	public override void Update()
	{
		if (Focused && (Input.MenuCancel.Pressed || credits.BottomTimer > 3f))
		{
			base.Overworld.Goto<OuiMainMenu>();
		}
		if (credits != null)
		{
			credits.Update();
			credits.Enabled = Focused && base.Selected;
		}
		vignetteAlpha = Calc.Approach(vignetteAlpha, base.Selected ? 1 : 0, Engine.DeltaTime * (base.Selected ? 1f : 4f));
		base.Update();
	}

	public override void Render()
	{
		if (vignetteAlpha > 0f)
		{
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * vignetteAlpha * 0.4f);
			OVR.Atlas["vignette"].Draw(Vector2.Zero, Vector2.Zero, Color.White * Ease.CubeInOut(vignetteAlpha), 1f);
		}
		if (credits != null)
		{
			credits.Render(Position);
		}
	}
}
