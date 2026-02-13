using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class WaveDashPage02 : WaveDashPage
{
	private class TitleText
	{
		[CompilerGenerated]
		private sealed class _003CStamp_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public TitleText _003C_003E4__this;

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
			public _003CStamp_003Ed__6(int _003C_003E1__state)
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
				TitleText titleText = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					goto IL_0063;
				case 1:
					_003C_003E1__state = -1;
					goto IL_0063;
				case 2:
					{
						_003C_003E1__state = -1;
						return false;
					}
					IL_0063:
					if (titleText.ease < 1f)
					{
						titleText.ease = Calc.Approach(titleText.ease, 1f, Engine.DeltaTime * 4f);
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					_003C_003E2__current = 0.2f;
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

		public const float Scale = 1.5f;

		public string Text;

		public Vector2 Position;

		public float Width;

		private float ease;

		public TitleText(Vector2 pos, string text)
		{
			Position = pos;
			Text = text;
			Width = ActiveFont.Measure(text).X * 1.5f;
		}

		[IteratorStateMachine(typeof(_003CStamp_003Ed__6))]
		public IEnumerator Stamp()
		{
			while (ease < 1f)
			{
				ease = Calc.Approach(ease, 1f, Engine.DeltaTime * 4f);
				yield return null;
			}
			yield return 0.2f;
		}

		public void Render()
		{
			if (!(ease <= 0f))
			{
				Vector2 scale = Vector2.One * (1f + (1f - Ease.CubeOut(ease))) * 1.5f;
				ActiveFont.DrawOutline(Text, Position + new Vector2(Width / 2f, ActiveFont.LineHeight * 0.5f * 1.5f), new Vector2(0.5f, 0.5f), scale, Color.White, 2f, Color.Black);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPage02 _003C_003E4__this;

		private string[] _003Ctext_003E5__2;

		private Vector2 _003Cpos_003E5__3;

		private int _003Ci_003E5__4;

		private TitleText _003Citem_003E5__5;

		private float _003Cdelay_003E5__6;

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
			WaveDashPage02 waveDashPage = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ctext_003E5__2 = Dialog.Clean("WAVEDASH_PAGE2_TITLE").Split('|');
				_003Cpos_003E5__3 = new Vector2(128f, 128f);
				_003Ci_003E5__4 = 0;
				goto IL_010f;
			case 1:
				_003C_003E1__state = -1;
				_003Cpos_003E5__3.X += _003Citem_003E5__5.Width + ActiveFont.Measure(' ').X * 1.5f;
				_003Citem_003E5__5 = null;
				_003Ci_003E5__4++;
				goto IL_010f;
			case 2:
				_003C_003E1__state = -1;
				waveDashPage.list = FancyText.Parse(Dialog.Get("WAVEDASH_PAGE2_LIST"), waveDashPage.Width, 32, 1f, Color.Black * 0.7f);
				_003Cdelay_003E5__6 = 0f;
				goto IL_0233;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0225;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0225;
			case 5:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/game/10_farewell/ppt_impossible");
				break;
			case 6:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0233:
				if (waveDashPage.listIndex < waveDashPage.list.Nodes.Count)
				{
					if (waveDashPage.list.Nodes[waveDashPage.listIndex] is FancyText.NewLine)
					{
						_003C_003E2__current = waveDashPage.PressButton();
						_003C_003E1__state = 3;
						return true;
					}
					_003Cdelay_003E5__6 += 0.008f;
					if (_003Cdelay_003E5__6 >= 0.016f)
					{
						_003Cdelay_003E5__6 -= 0.016f;
						_003C_003E2__current = 0.016f;
						_003C_003E1__state = 4;
						return true;
					}
					goto IL_0225;
				}
				_003C_003E2__current = waveDashPage.PressButton();
				_003C_003E1__state = 5;
				return true;
				IL_0225:
				waveDashPage.listIndex++;
				goto IL_0233;
				IL_010f:
				if (_003Ci_003E5__4 < _003Ctext_003E5__2.Length)
				{
					_003Citem_003E5__5 = new TitleText(_003Cpos_003E5__3, _003Ctext_003E5__2[_003Ci_003E5__4]);
					waveDashPage.title.Add(_003Citem_003E5__5);
					_003C_003E2__current = _003Citem_003E5__5.Stamp();
					_003C_003E1__state = 1;
					return true;
				}
				_003Ctext_003E5__2 = null;
				_003Cpos_003E5__3 = default(Vector2);
				_003C_003E2__current = waveDashPage.PressButton();
				_003C_003E1__state = 2;
				return true;
			}
			if (waveDashPage.impossibleEase < 1f)
			{
				waveDashPage.impossibleEase = Calc.Approach(waveDashPage.impossibleEase, 1f, Engine.DeltaTime);
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
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

	private List<TitleText> title = new List<TitleText>();

	private FancyText.Text list;

	private int listIndex;

	private float impossibleEase;

	public WaveDashPage02()
	{
		Transition = Transitions.Rotate3D;
		ClearColor = Calc.HexToColor("fff2cc");
	}

	public override void Added(WaveDashPresentation presentation)
	{
		base.Added(presentation);
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__7))]
	public override IEnumerator Routine()
	{
		string[] text = Dialog.Clean("WAVEDASH_PAGE2_TITLE").Split('|');
		Vector2 pos = new Vector2(128f, 128f);
		for (int i = 0; i < text.Length; i++)
		{
			TitleText item = new TitleText(pos, text[i]);
			title.Add(item);
			yield return item.Stamp();
			pos.X += item.Width + ActiveFont.Measure(' ').X * 1.5f;
		}
		yield return PressButton();
		list = FancyText.Parse(Dialog.Get("WAVEDASH_PAGE2_LIST"), base.Width, 32, 1f, Color.Black * 0.7f);
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
		yield return PressButton();
		Audio.Play("event:/new_content/game/10_farewell/ppt_impossible");
		while (impossibleEase < 1f)
		{
			impossibleEase = Calc.Approach(impossibleEase, 1f, Engine.DeltaTime);
			yield return null;
		}
	}

	public override void Update()
	{
	}

	public override void Render()
	{
		foreach (TitleText item in title)
		{
			item.Render();
		}
		if (list != null)
		{
			list.Draw(new Vector2(160f, 260f), new Vector2(0f, 0f), Vector2.One, 1f, 0, listIndex);
		}
		if (impossibleEase > 0f)
		{
			MTexture mTexture = Presentation.Gfx["Guy Clip Art"];
			float num = 0.75f;
			mTexture.Draw(new Vector2((float)base.Width - (float)mTexture.Width * num, (float)base.Height - 640f * impossibleEase), Vector2.Zero, Color.White, num);
			Matrix transformMatrix = Matrix.CreateRotationZ(-0.5f + Ease.CubeIn(1f - impossibleEase) * 8f) * Matrix.CreateTranslation(base.Width - 500, base.Height - 600, 0f);
			Draw.SpriteBatch.End();
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null, transformMatrix);
			ActiveFont.Draw(Dialog.Clean("WAVEDASH_PAGE2_IMPOSSIBLE"), Vector2.Zero, new Vector2(0.5f, 0.5f), Vector2.One * (2f + (1f - impossibleEase) * 0.5f), Color.Black * impossibleEase);
			Draw.SpriteBatch.End();
			Draw.SpriteBatch.Begin();
		}
	}
}
