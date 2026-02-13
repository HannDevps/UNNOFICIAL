using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WaveDashPage04 : WaveDashPage
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage04 _003C_003E4__this;

		private float _003Cdelay_003E5__2;

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
			WaveDashPage04 waveDashPage = _003C_003E4__this;
			switch (num)
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
				waveDashPage.list = FancyText.Parse(Dialog.Get("WAVEDASH_PAGE4_LIST"), waveDashPage.Width, 32, 1f, Color.Black * 0.7f);
				_003Cdelay_003E5__2 = 0f;
				goto IL_012f;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0121;
			case 3:
				{
					_003C_003E1__state = -1;
					goto IL_0121;
				}
				IL_012f:
				if (waveDashPage.listIndex < waveDashPage.list.Nodes.Count)
				{
					if (waveDashPage.list.Nodes[waveDashPage.listIndex] is FancyText.NewLine)
					{
						_003C_003E2__current = waveDashPage.PressButton();
						_003C_003E1__state = 2;
						return true;
					}
					_003Cdelay_003E5__2 += 0.008f;
					if (_003Cdelay_003E5__2 >= 0.016f)
					{
						_003Cdelay_003E5__2 -= 0.016f;
						_003C_003E2__current = 0.016f;
						_003C_003E1__state = 3;
						return true;
					}
					goto IL_0121;
				}
				return false;
				IL_0121:
				waveDashPage.listIndex++;
				goto IL_012f;
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

	private WaveDashPlaybackTutorial tutorial;

	private FancyText.Text list;

	private int listIndex;

	private float time;

	public WaveDashPage04()
	{
		Transition = Transitions.FadeIn;
		ClearColor = Calc.HexToColor("f4cccc");
	}

	public override void Added(WaveDashPresentation presentation)
	{
		base.Added(presentation);
		List<MTexture> textures = Presentation.Gfx.GetAtlasSubtextures("playback/platforms");
		tutorial = new WaveDashPlaybackTutorial("wavedashppt", new Vector2(-126f, 0f), new Vector2(1f, 1f), new Vector2(1f, -1f));
		tutorial.OnRender = delegate
		{
			textures[(int)(time % (float)textures.Count)].DrawCentered(Vector2.Zero);
		};
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__6))]
	public override IEnumerator Routine()
	{
		yield return 0.5f;
		list = FancyText.Parse(Dialog.Get("WAVEDASH_PAGE4_LIST"), base.Width, 32, 1f, Color.Black * 0.7f);
		float delay = 0f;
		while (listIndex < list.Nodes.Count)
		{
			if (list.Nodes[listIndex] is FancyText.NewLine)
			{
				yield return PressButton();
			}
			else
			{
				delay += 0.008f;
				if (delay >= 0.016f)
				{
					delay -= 0.016f;
					yield return 0.016f;
				}
			}
			listIndex++;
		}
	}

	public override void Update()
	{
		time += Engine.DeltaTime * 4f;
		tutorial.Update();
	}

	public override void Render()
	{
		ActiveFont.DrawOutline(Dialog.Clean("WAVEDASH_PAGE4_TITLE"), new Vector2(128f, 100f), Vector2.Zero, Vector2.One * 1.5f, Color.White, 2f, Color.Black);
		tutorial.Render(new Vector2((float)base.Width / 2f, (float)base.Height / 2f - 100f), 4f);
		if (list != null)
		{
			list.Draw(new Vector2(160f, base.Height - 400), new Vector2(0f, 0f), Vector2.One, 1f, 0, listIndex);
		}
	}
}
