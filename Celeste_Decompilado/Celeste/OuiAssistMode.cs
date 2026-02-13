using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiAssistMode : Oui
{
	private class Page
	{
		public FancyText.Text Text;

		public float Ease;

		public float Direction;
	}

	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiAssistMode _003C_003E4__this;

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
		public _003CEnter_003Ed__16(int _003C_003E1__state)
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
			OuiAssistMode ouiAssistMode = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiAssistMode.Focused = false;
				ouiAssistMode.Visible = true;
				ouiAssistMode.pageIndex = 0;
				ouiAssistMode.questionIndex = 1;
				ouiAssistMode.questionEase = 0f;
				ouiAssistMode.dot = 0f;
				ouiAssistMode.questionText = FancyText.Parse(Dialog.Get("ASSIST_ASK"), 1600, -1, 1f, Color.White);
				if (!ouiAssistMode.FileSlot.AssistModeEnabled)
				{
					for (int i = 0; Dialog.Has("ASSIST_MODE_" + i); i++)
					{
						Page item = new Page
						{
							Text = FancyText.Parse(Dialog.Get("ASSIST_MODE_" + i), 2000, -1, 1f, Color.White * 0.9f),
							Ease = 0f
						};
						ouiAssistMode.pages.Add(item);
					}
					ouiAssistMode.pages[0].Ease = 1f;
					ouiAssistMode.mainSfx = Audio.Play("event:/ui/main/assist_info_whistle");
				}
				else
				{
					ouiAssistMode.questionEase = 1f;
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (ouiAssistMode.fade < 1f)
			{
				ouiAssistMode.fade += Engine.DeltaTime * 4f;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiAssistMode.Focused = true;
			ouiAssistMode.Add(new Coroutine(ouiAssistMode.InputRoutine()));
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
	private sealed class _003CLeave_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiAssistMode _003C_003E4__this;

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
		public _003CLeave_003Ed__17(int _003C_003E1__state)
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
			OuiAssistMode ouiAssistMode = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiAssistMode.Focused = false;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (ouiAssistMode.fade > 0f)
			{
				ouiAssistMode.fade -= Engine.DeltaTime * 4f;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (ouiAssistMode.mainSfx != null)
			{
				ouiAssistMode.mainSfx.release();
			}
			ouiAssistMode.pages.Clear();
			ouiAssistMode.Visible = false;
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
	private sealed class _003CInputRoutine_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiAssistMode _003C_003E4__this;

		private int _003Cwas_003E5__2;

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
		public _003CInputRoutine_003Ed__18(int _003C_003E1__state)
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
			OuiAssistMode ouiAssistMode = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0035;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0179;
			case 2:
				_003C_003E1__state = -1;
				goto IL_01dd;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0260;
			case 4:
				_003C_003E1__state = -1;
				goto IL_02c4;
			case 5:
				{
					_003C_003E1__state = -1;
					goto IL_0035;
				}
				IL_0035:
				if (Input.MenuCancel.Pressed)
				{
					ouiAssistMode.Focused = false;
					ouiAssistMode.Overworld.Goto<OuiFileSelect>();
					Audio.Play("event:/ui/main/button_back");
					Audio.SetParameter(ouiAssistMode.mainSfx, "assist_progress", 6f);
					break;
				}
				_003Cwas_003E5__2 = ouiAssistMode.pageIndex;
				if ((Input.MenuConfirm.Pressed || Input.MenuRight.Pressed) && ouiAssistMode.pageIndex < ouiAssistMode.pages.Count)
				{
					ouiAssistMode.pageIndex++;
					Audio.Play("event:/ui/main/rollover_down");
					Audio.SetParameter(ouiAssistMode.mainSfx, "assist_progress", ouiAssistMode.pageIndex);
				}
				else if (Input.MenuLeft.Pressed && ouiAssistMode.pageIndex > 0)
				{
					Audio.Play("event:/ui/main/rollover_up");
					ouiAssistMode.pageIndex--;
				}
				if (_003Cwas_003E5__2 != ouiAssistMode.pageIndex)
				{
					if (_003Cwas_003E5__2 < ouiAssistMode.pages.Count)
					{
						ouiAssistMode.pages[_003Cwas_003E5__2].Direction = Math.Sign(_003Cwas_003E5__2 - ouiAssistMode.pageIndex);
						goto IL_0179;
					}
					goto IL_01dd;
				}
				goto IL_02ef;
				IL_0260:
				if ((ouiAssistMode.pages[ouiAssistMode.pageIndex].Ease = Calc.Approach(ouiAssistMode.pages[ouiAssistMode.pageIndex].Ease, 1f, Engine.DeltaTime * 8f)) != 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_02ef;
				IL_02c4:
				if ((ouiAssistMode.questionEase = Calc.Approach(ouiAssistMode.questionEase, 1f, Engine.DeltaTime * 8f)) != 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				goto IL_02ef;
				IL_0179:
				if ((ouiAssistMode.pages[_003Cwas_003E5__2].Ease = Calc.Approach(ouiAssistMode.pages[_003Cwas_003E5__2].Ease, 0f, Engine.DeltaTime * 8f)) != 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0208;
				IL_01dd:
				if ((ouiAssistMode.questionEase = Calc.Approach(ouiAssistMode.questionEase, 0f, Engine.DeltaTime * 8f)) != 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_0208;
				IL_02ef:
				if (ouiAssistMode.pageIndex >= ouiAssistMode.pages.Count)
				{
					if (Input.MenuConfirm.Pressed)
					{
						ouiAssistMode.FileSlot.AssistModeEnabled = ouiAssistMode.questionIndex == 0;
						if (ouiAssistMode.FileSlot.AssistModeEnabled)
						{
							ouiAssistMode.FileSlot.VariantModeEnabled = false;
						}
						ouiAssistMode.FileSlot.CreateButtons();
						ouiAssistMode.Focused = false;
						ouiAssistMode.Overworld.Goto<OuiFileSelect>();
						Audio.Play((ouiAssistMode.questionIndex == 0) ? "event:/ui/main/assist_button_yes" : "event:/ui/main/assist_button_no");
						Audio.SetParameter(ouiAssistMode.mainSfx, "assist_progress", (ouiAssistMode.questionIndex == 0) ? 4 : 5);
						break;
					}
					if (Input.MenuUp.Pressed && ouiAssistMode.questionIndex > 0)
					{
						Audio.Play("event:/ui/main/rollover_up");
						ouiAssistMode.questionIndex--;
						ouiAssistMode.wiggler.Start();
					}
					else if (Input.MenuDown.Pressed && ouiAssistMode.questionIndex < 1)
					{
						Audio.Play("event:/ui/main/rollover_down");
						ouiAssistMode.questionIndex++;
						ouiAssistMode.wiggler.Start();
					}
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
				return true;
				IL_0208:
				if (ouiAssistMode.pageIndex < ouiAssistMode.pages.Count)
				{
					ouiAssistMode.pages[ouiAssistMode.pageIndex].Direction = Math.Sign(ouiAssistMode.pageIndex - _003Cwas_003E5__2);
					goto IL_0260;
				}
				goto IL_02c4;
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

	public OuiFileSelectSlot FileSlot;

	private float fade;

	private List<Page> pages = new List<Page>();

	private int pageIndex;

	private int questionIndex = 1;

	private float questionEase;

	private Wiggler wiggler;

	private float dot;

	private FancyText.Text questionText;

	private Color iconColor = Calc.HexToColor("44adf7");

	private float leftArrowEase;

	private float rightArrowEase;

	private FMOD.Studio.EventInstance mainSfx;

	private const float textScale = 0.8f;

	public OuiAssistMode()
	{
		Visible = false;
		Add(wiggler = Wiggler.Create(0.4f, 4f));
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__16))]
	public override IEnumerator Enter(Oui from)
	{
		Focused = false;
		Visible = true;
		pageIndex = 0;
		questionIndex = 1;
		questionEase = 0f;
		dot = 0f;
		questionText = FancyText.Parse(Dialog.Get("ASSIST_ASK"), 1600, -1, 1f, Color.White);
		if (!FileSlot.AssistModeEnabled)
		{
			for (int i = 0; Dialog.Has("ASSIST_MODE_" + i); i++)
			{
				Page page = new Page();
				page.Text = FancyText.Parse(Dialog.Get("ASSIST_MODE_" + i), 2000, -1, 1f, Color.White * 0.9f);
				page.Ease = 0f;
				pages.Add(page);
			}
			pages[0].Ease = 1f;
			mainSfx = Audio.Play("event:/ui/main/assist_info_whistle");
		}
		else
		{
			questionEase = 1f;
		}
		while (fade < 1f)
		{
			fade += Engine.DeltaTime * 4f;
			yield return null;
		}
		Focused = true;
		Add(new Coroutine(InputRoutine()));
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__17))]
	public override IEnumerator Leave(Oui next)
	{
		Focused = false;
		while (fade > 0f)
		{
			fade -= Engine.DeltaTime * 4f;
			yield return null;
		}
		if (mainSfx != null)
		{
			mainSfx.release();
		}
		pages.Clear();
		Visible = false;
	}

	[IteratorStateMachine(typeof(_003CInputRoutine_003Ed__18))]
	private IEnumerator InputRoutine()
	{
		while (true)
		{
			if (Input.MenuCancel.Pressed)
			{
				Focused = false;
				base.Overworld.Goto<OuiFileSelect>();
				Audio.Play("event:/ui/main/button_back");
				Audio.SetParameter(mainSfx, "assist_progress", 6f);
				yield break;
			}
			int was = pageIndex;
			if ((Input.MenuConfirm.Pressed || Input.MenuRight.Pressed) && pageIndex < pages.Count)
			{
				pageIndex++;
				Audio.Play("event:/ui/main/rollover_down");
				Audio.SetParameter(mainSfx, "assist_progress", pageIndex);
			}
			else if (Input.MenuLeft.Pressed && pageIndex > 0)
			{
				Audio.Play("event:/ui/main/rollover_up");
				pageIndex--;
			}
			if (was != pageIndex)
			{
				if (was < pages.Count)
				{
					pages[was].Direction = Math.Sign(was - pageIndex);
					while ((pages[was].Ease = Calc.Approach(pages[was].Ease, 0f, Engine.DeltaTime * 8f)) != 0f)
					{
						yield return null;
					}
				}
				else
				{
					while ((questionEase = Calc.Approach(questionEase, 0f, Engine.DeltaTime * 8f)) != 0f)
					{
						yield return null;
					}
				}
				if (pageIndex < pages.Count)
				{
					pages[pageIndex].Direction = Math.Sign(pageIndex - was);
					while ((pages[pageIndex].Ease = Calc.Approach(pages[pageIndex].Ease, 1f, Engine.DeltaTime * 8f)) != 1f)
					{
						yield return null;
					}
				}
				else
				{
					while ((questionEase = Calc.Approach(questionEase, 1f, Engine.DeltaTime * 8f)) != 1f)
					{
						yield return null;
					}
				}
			}
			if (pageIndex >= pages.Count)
			{
				if (Input.MenuConfirm.Pressed)
				{
					break;
				}
				if (Input.MenuUp.Pressed && questionIndex > 0)
				{
					Audio.Play("event:/ui/main/rollover_up");
					questionIndex--;
					wiggler.Start();
				}
				else if (Input.MenuDown.Pressed && questionIndex < 1)
				{
					Audio.Play("event:/ui/main/rollover_down");
					questionIndex++;
					wiggler.Start();
				}
			}
			yield return null;
		}
		FileSlot.AssistModeEnabled = questionIndex == 0;
		if (FileSlot.AssistModeEnabled)
		{
			FileSlot.VariantModeEnabled = false;
		}
		FileSlot.CreateButtons();
		Focused = false;
		base.Overworld.Goto<OuiFileSelect>();
		Audio.Play((questionIndex == 0) ? "event:/ui/main/assist_button_yes" : "event:/ui/main/assist_button_no");
		Audio.SetParameter(mainSfx, "assist_progress", (questionIndex == 0) ? 4 : 5);
	}

	public override void Update()
	{
		dot = Calc.Approach(dot, pageIndex, Engine.DeltaTime * 8f);
		leftArrowEase = Calc.Approach(leftArrowEase, (pageIndex > 0) ? 1 : 0, Engine.DeltaTime * 4f);
		rightArrowEase = Calc.Approach(rightArrowEase, (pageIndex < pages.Count) ? 1 : 0, Engine.DeltaTime * 4f);
		base.Update();
	}

	public override void Render()
	{
		if (!Visible)
		{
			return;
		}
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * fade * 0.9f);
		for (int i = 0; i < pages.Count; i++)
		{
			Page page = pages[i];
			float num = Ease.CubeOut(page.Ease);
			if (num > 0f)
			{
				Vector2 position = new Vector2(960f, 620f);
				position.X += page.Direction * (1f - num) * 256f;
				page.Text.DrawJustifyPerLine(position, new Vector2(0.5f, 0f), Vector2.One * 0.8f, num * fade);
			}
		}
		if (questionEase > 0f)
		{
			float num2 = Ease.CubeOut(questionEase);
			float num3 = wiggler.Value * 8f;
			Vector2 vector = new Vector2(960f + (1f - num2) * 256f, 620f);
			float lineHeight = ActiveFont.LineHeight;
			questionText.DrawJustifyPerLine(vector, new Vector2(0.5f, 0f), Vector2.One, num2 * fade);
			ActiveFont.DrawOutline(Dialog.Clean("ASSIST_YES"), vector + new Vector2(((questionIndex == 0) ? num3 : 0f) * 1.2f * num2, lineHeight * 1.4f + 10f), new Vector2(0.5f, 0f), Vector2.One * 0.8f, SelectionColor(questionIndex == 0), 2f, Color.Black * num2 * fade);
			ActiveFont.DrawOutline(Dialog.Clean("ASSIST_NO"), vector + new Vector2(((questionIndex == 1) ? num3 : 0f) * 1.2f * num2, lineHeight * 2.2f + 20f), new Vector2(0.5f, 0f), Vector2.One * 0.8f, SelectionColor(questionIndex == 1), 2f, Color.Black * num2 * fade);
		}
		if (pages.Count > 0)
		{
			int num4 = pages.Count + 1;
			MTexture mTexture = GFX.Gui["dot"];
			int num5 = mTexture.Width * num4;
			Vector2 vector2 = new Vector2(960f, 960f - 40f * Ease.CubeOut(fade));
			for (int j = 0; j < num4; j++)
			{
				mTexture.DrawCentered(vector2 + new Vector2((float)(-num5 / 2) + (float)mTexture.Width * ((float)j + 0.5f), 0f), Color.White * 0.25f);
			}
			float x = 1f + Calc.YoYo(dot % 1f) * 4f;
			mTexture.DrawCentered(vector2 + new Vector2((float)(-num5 / 2) + (float)mTexture.Width * (dot + 0.5f), 0f), iconColor, new Vector2(x, 1f));
			GFX.Gui["dotarrow"].DrawCentered(vector2 + new Vector2(-num5 / 2 - 50, 32f * (1f - Ease.CubeOut(leftArrowEase))), iconColor * leftArrowEase, new Vector2(-1f, 1f));
			GFX.Gui["dotarrow"].DrawCentered(vector2 + new Vector2(num5 / 2 + 50, 32f * (1f - Ease.CubeOut(rightArrowEase))), iconColor * rightArrowEase);
		}
		GFX.Gui["assistmode"].DrawJustified(new Vector2(960f, 540f + 64f * Ease.CubeOut(fade)), new Vector2(0.5f, 1f), iconColor * fade);
	}

	private Color SelectionColor(bool selected)
	{
		if (selected)
		{
			return ((Settings.Instance.DisableFlashes || base.Scene.BetweenInterval(0.1f)) ? TextMenu.HighlightColorA : TextMenu.HighlightColorB) * fade;
		}
		return Color.White * fade;
	}
}
