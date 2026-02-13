using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiJournalPoem : OuiJournalPage
{
	private class PoemLine
	{
		public float Index;

		public string Text;

		public float HoveringEase;

		public float HoldingEase;

		public bool Remix;

		public void Render()
		{
			float num = 100f + Ease.CubeInOut(HoveringEase) * 20f;
			float y = GetY(Index);
			Draw.Rect(num, y - 22f, 810f, 44f, Color.White * 0.25f);
			Vector2 scale = Vector2.One * (0.6f + HoldingEase * 0.4f);
			MTN.Journal[Remix ? "heartgem1" : "heartgem0"].DrawCentered(new Vector2(num + 20f, y), Color.White, scale);
			Color color = Color.Black * (0.7f + HoveringEase * 0.3f);
			Vector2 scale2 = Vector2.One * (0.5f + HoldingEase * 0.1f);
			ActiveFont.Draw(Text, new Vector2(num + 60f, y), new Vector2(0f, 0.5f), scale2, color);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public PoemLine poemA;

		public int a;

		public int b;

		public PoemLine poemB;

		public OuiJournalPoem _003C_003E4__this;

		internal void _003CSwap_003Eb__0(Tween t)
		{
			poemA.Index = MathHelper.Lerp(a, b, t.Eased);
			poemB.Index = MathHelper.Lerp(b, a, t.Eased);
		}

		internal void _003CSwap_003Eb__1(Tween t)
		{
			_003C_003E4__this.tween = null;
		}
	}

	[CompilerGenerated]
	private sealed class _003CSwap_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int a;

		public int b;

		public OuiJournalPoem _003C_003E4__this;

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
		public _003CSwap_003Ed__17(int _003C_003E1__state)
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
			OuiJournalPoem ouiJournalPoem = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass17_0 CS_0024_003C_003E8__locals19 = new _003C_003Ec__DisplayClass17_0
				{
					a = a,
					b = b,
					_003C_003E4__this = _003C_003E4__this
				};
				string value = SaveData.Instance.Poem[CS_0024_003C_003E8__locals19.a];
				SaveData.Instance.Poem[CS_0024_003C_003E8__locals19.a] = SaveData.Instance.Poem[CS_0024_003C_003E8__locals19.b];
				SaveData.Instance.Poem[CS_0024_003C_003E8__locals19.b] = value;
				CS_0024_003C_003E8__locals19.poemA = ouiJournalPoem.lines[CS_0024_003C_003E8__locals19.a];
				CS_0024_003C_003E8__locals19.poemB = ouiJournalPoem.lines[CS_0024_003C_003E8__locals19.b];
				PoemLine value2 = ouiJournalPoem.lines[CS_0024_003C_003E8__locals19.a];
				ouiJournalPoem.lines[CS_0024_003C_003E8__locals19.a] = ouiJournalPoem.lines[CS_0024_003C_003E8__locals19.b];
				ouiJournalPoem.lines[CS_0024_003C_003E8__locals19.b] = value2;
				ouiJournalPoem.tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.125f, start: true);
				ouiJournalPoem.tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals19.poemA.Index = MathHelper.Lerp(CS_0024_003C_003E8__locals19.a, CS_0024_003C_003E8__locals19.b, t.Eased);
					CS_0024_003C_003E8__locals19.poemB.Index = MathHelper.Lerp(CS_0024_003C_003E8__locals19.b, CS_0024_003C_003E8__locals19.a, t.Eased);
				};
				ouiJournalPoem.tween.OnComplete = delegate
				{
					CS_0024_003C_003E8__locals19._003C_003E4__this.tween = null;
				};
				_003C_003E2__current = ouiJournalPoem.tween.Wait();
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				ouiJournalPoem.swapping = false;
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

	private const float textScale = 0.5f;

	private const float holdingScaleAdd = 0.1f;

	private const float poemHeight = 44f;

	private const float poemSpacing = 4f;

	private const float poemStanzaSpacing = 16f;

	private List<PoemLine> lines = new List<PoemLine>();

	private int index;

	private float slider;

	private bool dragging;

	private bool swapping;

	private Coroutine swapRoutine = new Coroutine();

	private Wiggler wiggler = Wiggler.Create(0.4f, 4f);

	private Tween tween;

	public OuiJournalPoem(OuiJournal journal)
		: base(journal)
	{
		PageTexture = "page";
		swapRoutine.RemoveOnComplete = false;
		float num = 0f;
		foreach (string item in SaveData.Instance.Poem)
		{
			string text = Dialog.Clean("poem_" + item);
			text = text.Replace("\n", " - ");
			lines.Add(new PoemLine
			{
				Text = text,
				Index = num,
				Remix = AreaData.IsPoemRemix(item)
			});
			num += 1f;
		}
	}

	public static float GetY(float index)
	{
		return 120f + 44f * (index + 0.5f) + 4f * index + (float)((int)index / 4) * 16f;
	}

	public override void Redraw(VirtualRenderTarget buffer)
	{
		base.Redraw(buffer);
		Draw.SpriteBatch.Begin();
		ActiveFont.Draw(Dialog.Clean("journal_poem"), new Vector2(60f, 60f), new Vector2(0f, 0.5f), Vector2.One, Color.Black * 0.6f);
		foreach (PoemLine line in lines)
		{
			line.Render();
		}
		if (lines.Count > 0)
		{
			MTexture mTexture = MTN.Journal[dragging ? "poemSlider" : "poemArrow"];
			Vector2 position = new Vector2(50f, GetY(slider));
			mTexture.DrawCentered(position, Color.White, 1f + 0.25f * wiggler.Value);
		}
		Draw.SpriteBatch.End();
	}

	[IteratorStateMachine(typeof(_003CSwap_003Ed__17))]
	private IEnumerator Swap(int a, int b)
	{
		string value = SaveData.Instance.Poem[a];
		SaveData.Instance.Poem[a] = SaveData.Instance.Poem[b];
		SaveData.Instance.Poem[b] = value;
		PoemLine poemA = lines[a];
		PoemLine poemB = lines[b];
		PoemLine value2 = lines[a];
		lines[a] = lines[b];
		lines[b] = value2;
		tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.125f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			poemA.Index = MathHelper.Lerp(a, b, t.Eased);
			poemB.Index = MathHelper.Lerp(b, a, t.Eased);
		};
		tween.OnComplete = delegate
		{
			tween = null;
		};
		yield return tween.Wait();
		swapping = false;
	}

	public override void Update()
	{
		if (lines.Count <= 0)
		{
			return;
		}
		if (tween != null && tween.Active)
		{
			tween.Update();
		}
		if (Input.MenuDown.Pressed && index + 1 < lines.Count && !swapping)
		{
			if (dragging)
			{
				Audio.Play("event:/ui/world_map/journal/heart_shift_down");
				swapRoutine.Replace(Swap(index, index + 1));
				swapping = true;
			}
			else
			{
				Audio.Play("event:/ui/world_map/journal/heart_roll");
			}
			index++;
		}
		else if (Input.MenuUp.Pressed && index > 0 && !swapping)
		{
			if (dragging)
			{
				Audio.Play("event:/ui/world_map/journal/heart_shift_up");
				swapRoutine.Replace(Swap(index, index - 1));
				swapping = true;
			}
			else
			{
				Audio.Play("event:/ui/world_map/journal/heart_roll");
			}
			index--;
		}
		else if (Input.MenuConfirm.Pressed)
		{
			Journal.PageTurningLocked = true;
			Audio.Play("event:/ui/world_map/journal/heart_grab");
			dragging = true;
			wiggler.Start();
		}
		else if (!Input.MenuConfirm.Check && dragging)
		{
			Journal.PageTurningLocked = false;
			Audio.Play("event:/ui/world_map/journal/heart_release");
			dragging = false;
			wiggler.Start();
		}
		for (int i = 0; i < lines.Count; i++)
		{
			PoemLine poemLine = lines[i];
			poemLine.HoveringEase = Calc.Approach(poemLine.HoveringEase, (index == i) ? 1f : 0f, 8f * Engine.DeltaTime);
			poemLine.HoldingEase = Calc.Approach(poemLine.HoldingEase, (index == i && dragging) ? 1f : 0f, 8f * Engine.DeltaTime);
		}
		slider = Calc.Approach(slider, index, 16f * Engine.DeltaTime);
		if (swapRoutine != null && swapRoutine.Active)
		{
			swapRoutine.Update();
		}
		wiggler.Update();
		Redraw(Journal.CurrentPageBuffer);
	}
}
