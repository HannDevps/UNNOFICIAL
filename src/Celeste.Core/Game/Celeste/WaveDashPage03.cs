using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashPage03 : WaveDashPage
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage03 _003C_003E4__this;

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
		public _003CRoutine_003Ed__8(int _003C_003E1__state)
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
			WaveDashPage03 waveDashPage = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_008a;
			case 1:
				_003C_003E1__state = -1;
				goto IL_008a;
			case 2:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/game/10_farewell/ppt_wavedash_whoosh");
				goto IL_00fd;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00fd;
			case 4:
				_003C_003E1__state = -1;
				waveDashPage.infoText = FancyText.Parse(Dialog.Get("WAVEDASH_PAGE3_INFO"), waveDashPage.Width - 240, 32, 1f, Color.Black * 0.7f);
				_003C_003E2__current = waveDashPage.PressButton();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/game/10_farewell/ppt_its_easy");
				waveDashPage.easyText = new AreaCompleteTitle(new Vector2((float)waveDashPage.Width / 2f, waveDashPage.Height - 150), Dialog.Clean("WAVEDASH_PAGE3_EASY"), 2f, rainbow: true);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00fd:
				if (waveDashPage.clipArtEase < 1f)
				{
					waveDashPage.clipArtEase = Calc.Approach(waveDashPage.clipArtEase, 1f, Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 4;
				return true;
				IL_008a:
				if (waveDashPage.titleDisplayed.Length < waveDashPage.title.Length)
				{
					waveDashPage.titleDisplayed += waveDashPage.title[waveDashPage.titleDisplayed.Length];
					_003C_003E2__current = 0.05f;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = waveDashPage.PressButton();
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

	private string title;

	private string titleDisplayed;

	private MTexture clipArt;

	private float clipArtEase;

	private FancyText.Text infoText;

	private AreaCompleteTitle easyText;

	public WaveDashPage03()
	{
		Transition = Transitions.Blocky;
		ClearColor = Calc.HexToColor("d9ead3");
		title = Dialog.Clean("WAVEDASH_PAGE3_TITLE");
		titleDisplayed = "";
	}

	public override void Added(WaveDashPresentation presentation)
	{
		base.Added(presentation);
		clipArt = presentation.Gfx["moveset"];
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__8))]
	public override IEnumerator Routine()
	{
		while (titleDisplayed.Length < title.Length)
		{
			titleDisplayed += title[titleDisplayed.Length];
			yield return 0.05f;
		}
		yield return PressButton();
		Audio.Play("event:/new_content/game/10_farewell/ppt_wavedash_whoosh");
		while (clipArtEase < 1f)
		{
			clipArtEase = Calc.Approach(clipArtEase, 1f, Engine.DeltaTime);
			yield return null;
		}
		yield return 0.25f;
		infoText = FancyText.Parse(Dialog.Get("WAVEDASH_PAGE3_INFO"), base.Width - 240, 32, 1f, Color.Black * 0.7f);
		yield return PressButton();
		Audio.Play("event:/new_content/game/10_farewell/ppt_its_easy");
		easyText = new AreaCompleteTitle(new Vector2((float)base.Width / 2f, base.Height - 150), Dialog.Clean("WAVEDASH_PAGE3_EASY"), 2f, rainbow: true);
		yield return 1f;
	}

	public override void Update()
	{
		if (easyText != null)
		{
			easyText.Update();
		}
	}

	public override void Render()
	{
		ActiveFont.DrawOutline(titleDisplayed, new Vector2(128f, 100f), Vector2.Zero, Vector2.One * 1.5f, Color.White, 2f, Color.Black);
		if (clipArtEase > 0f)
		{
			Vector2 scale = Vector2.One * (1f + (1f - clipArtEase) * 3f) * 0.8f;
			float rotation = (1f - clipArtEase) * 8f;
			Color color = Color.White * clipArtEase;
			clipArt.DrawCentered(new Vector2((float)base.Width / 2f, (float)base.Height / 2f - 90f), color, scale, rotation);
		}
		if (infoText != null)
		{
			infoText.Draw(new Vector2((float)base.Width / 2f, base.Height - 350), new Vector2(0.5f, 0f), Vector2.One, 1f);
		}
		if (easyText != null)
		{
			easyText.Render();
		}
	}
}
