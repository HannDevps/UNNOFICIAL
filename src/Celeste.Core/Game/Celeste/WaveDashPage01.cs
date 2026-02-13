using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashPage01 : WaveDashPage
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage01 _003C_003E4__this;

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
			int num = _003C_003E1__state;
			WaveDashPage01 waveDashPage = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetAltMusic("event:/new_content/music/lvl10/intermission_powerpoint");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				waveDashPage.title = new AreaCompleteTitle(new Vector2((float)waveDashPage.Width / 2f, (float)waveDashPage.Height / 2f - 100f), Dialog.Clean("WAVEDASH_PAGE1_TITLE"), 2f, rainbow: true);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00f0;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00f0;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00f0:
				if (waveDashPage.subtitleEase < 1f)
				{
					waveDashPage.subtitleEase = Calc.Approach(waveDashPage.subtitleEase, 1f, Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = 0.1f;
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

	private AreaCompleteTitle title;

	private float subtitleEase;

	public WaveDashPage01()
	{
		Transition = Transitions.ScaleIn;
		ClearColor = Calc.HexToColor("9fc5e8");
	}

	public override void Added(WaveDashPresentation presentation)
	{
		base.Added(presentation);
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__4))]
	public override IEnumerator Routine()
	{
		Audio.SetAltMusic("event:/new_content/music/lvl10/intermission_powerpoint");
		yield return 1f;
		title = new AreaCompleteTitle(new Vector2((float)base.Width / 2f, (float)base.Height / 2f - 100f), Dialog.Clean("WAVEDASH_PAGE1_TITLE"), 2f, rainbow: true);
		yield return 1f;
		while (subtitleEase < 1f)
		{
			subtitleEase = Calc.Approach(subtitleEase, 1f, Engine.DeltaTime);
			yield return null;
		}
		yield return 0.1f;
	}

	public override void Update()
	{
		if (title != null)
		{
			title.Update();
		}
	}

	public override void Render()
	{
		if (title != null)
		{
			title.Render();
		}
		if (subtitleEase > 0f)
		{
			Vector2 position = new Vector2((float)base.Width / 2f, (float)base.Height / 2f + 80f);
			float x = 1f + Ease.BigBackIn(1f - subtitleEase) * 2f;
			float y = 0.25f + Ease.BigBackIn(subtitleEase) * 0.75f;
			ActiveFont.Draw(Dialog.Clean("WAVEDASH_PAGE1_SUBTITLE"), position, new Vector2(0.5f, 0.5f), new Vector2(x, y), Color.Black * 0.8f);
		}
	}
}
