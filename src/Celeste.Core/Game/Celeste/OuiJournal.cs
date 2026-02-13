using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class OuiJournal : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiJournal _003C_003E4__this;

		public Oui from;

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
		public _003CEnter_003Ed__26(int _003C_003E1__state)
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
			OuiJournal ouiJournal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Stats.MakeRequest();
				ouiJournal.Overworld.ShowConfirmUI = false;
				ouiJournal.fromAreaInspect = from is OuiChapterPanel;
				ouiJournal.PageIndex = 0;
				ouiJournal.Visible = true;
				ouiJournal.X = -1920f;
				ouiJournal.turningPage = false;
				ouiJournal.turningScale = 1f;
				ouiJournal.rotation = 0f;
				ouiJournal.dot = 0f;
				ouiJournal.dotTarget = 0f;
				ouiJournal.dotEase = 0f;
				ouiJournal.leftArrowEase = 0f;
				ouiJournal.rightArrowEase = 0f;
				ouiJournal.NextPageBuffer = VirtualContent.CreateRenderTarget("journal-a", 1610, 1000);
				ouiJournal.CurrentPageBuffer = VirtualContent.CreateRenderTarget("journal-b", 1610, 1000);
				ouiJournal.Pages.Add(new OuiJournalCover(ouiJournal));
				ouiJournal.Pages.Add(new OuiJournalProgress(ouiJournal));
				ouiJournal.Pages.Add(new OuiJournalSpeedrun(ouiJournal));
				ouiJournal.Pages.Add(new OuiJournalDeaths(ouiJournal));
				ouiJournal.Pages.Add(new OuiJournalPoem(ouiJournal));
				if (Stats.Has())
				{
					ouiJournal.Pages.Add(new OuiJournalGlobal(ouiJournal));
				}
				int num2 = 0;
				foreach (OuiJournalPage page in ouiJournal.Pages)
				{
					page.PageIndex = num2++;
				}
				ouiJournal.Pages[0].Redraw(ouiJournal.CurrentPageBuffer);
				ouiJournal.cameraStart = ouiJournal.Overworld.Mountain.UntiltedCamera;
				ouiJournal.cameraEnd = ouiJournal.cameraStart;
				ouiJournal.cameraEnd.Position += -ouiJournal.cameraStart.Rotation.Forward() * 1f;
				ouiJournal.Overworld.Mountain.EaseCamera(ouiJournal.Overworld.Mountain.Area, ouiJournal.cameraEnd, 2f);
				ouiJournal.Overworld.Mountain.AllowUserRotation = false;
				_003Cp_003E5__2 = 0f;
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.4f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				ouiJournal.rotation = -0.025f * Ease.BackOut(_003Cp_003E5__2);
				ouiJournal.X = -1920f + 1920f * Ease.CubeInOut(_003Cp_003E5__2);
				ouiJournal.dotEase = _003Cp_003E5__2;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiJournal.dotEase = 1f;
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
	private sealed class _003CTurnPage_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiJournal _003C_003E4__this;

		public int direction;

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
		public _003CTurnPage_003Ed__28(int _003C_003E1__state)
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
			OuiJournal ouiJournal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiJournal.turningPage = true;
				if (direction < 0)
				{
					ouiJournal.PageIndex--;
					ouiJournal.turningScale = -1f;
					ouiJournal.dotTarget -= 1f;
					ouiJournal.Page.Redraw(ouiJournal.CurrentPageBuffer);
					ouiJournal.NextPage.Redraw(ouiJournal.NextPageBuffer);
					goto IL_00a2;
				}
				ouiJournal.NextPage.Redraw(ouiJournal.NextPageBuffer);
				ouiJournal.turningScale = 1f;
				ouiJournal.dotTarget += 1f;
				goto IL_0119;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00a2;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0119;
				}
				IL_0119:
				if ((ouiJournal.turningScale = Calc.Approach(ouiJournal.turningScale, -1f, Engine.DeltaTime * 8f)) > -1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				ouiJournal.PageIndex++;
				ouiJournal.Page.Redraw(ouiJournal.CurrentPageBuffer);
				break;
				IL_00a2:
				if ((ouiJournal.turningScale = Calc.Approach(ouiJournal.turningScale, 1f, Engine.DeltaTime * 8f)) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				break;
			}
			ouiJournal.turningScale = 1f;
			ouiJournal.turningPage = false;
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
	private sealed class _003CLeave_003Ed__29 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiJournal _003C_003E4__this;

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
		public _003CLeave_003Ed__29(int _003C_003E1__state)
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
			OuiJournal ouiJournal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/ui/world_map/journal/back");
				ouiJournal.Overworld.Mountain.EaseCamera(ouiJournal.Overworld.Mountain.Area, ouiJournal.cameraStart, 0.4f);
				UserIO.SaveHandler(file: false, settings: true);
				_003C_003E2__current = ouiJournal.EaseOut(0.4f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if (UserIO.Saving)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			ouiJournal.CurrentPageBuffer.Dispose();
			ouiJournal.NextPageBuffer.Dispose();
			ouiJournal.Overworld.ShowConfirmUI = true;
			ouiJournal.Pages.Clear();
			ouiJournal.Visible = false;
			ouiJournal.Overworld.Mountain.AllowUserRotation = true;
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
	private sealed class _003CEaseOut_003Ed__30 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiJournal _003C_003E4__this;

		public float duration;

		private float _003CrotFrom_003E5__2;

		private float _003Cp_003E5__3;

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
		public _003CEaseOut_003Ed__30(int _003C_003E1__state)
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
			OuiJournal ouiJournal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003CrotFrom_003E5__2 = ouiJournal.rotation;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				ouiJournal.rotation = _003CrotFrom_003E5__2 * (1f - Ease.BackOut(_003Cp_003E5__3));
				ouiJournal.X = 0f + -1920f * Ease.CubeInOut(_003Cp_003E5__3);
				ouiJournal.dotEase = 1f - _003Cp_003E5__3;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiJournal.dotEase = 0f;
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

	private const float onScreenX = 0f;

	private const float offScreenX = -1920f;

	public bool PageTurningLocked;

	public List<OuiJournalPage> Pages = new List<OuiJournalPage>();

	public int PageIndex;

	public VirtualRenderTarget CurrentPageBuffer;

	public VirtualRenderTarget NextPageBuffer;

	private bool turningPage;

	private float turningScale;

	private Color backColor = Color.Lerp(Color.White, Color.Black, 0.2f);

	private bool fromAreaInspect;

	private float rotation;

	private MountainCamera cameraStart;

	private MountainCamera cameraEnd;

	private MTexture arrow = MTN.Journal["pageArrow"];

	private float dot;

	private float dotTarget;

	private float dotEase;

	private float leftArrowEase;

	private float rightArrowEase;

	public OuiJournalPage Page => Pages[PageIndex];

	public OuiJournalPage NextPage => Pages[PageIndex + 1];

	public OuiJournalPage PrevPage => Pages[PageIndex - 1];

	[IteratorStateMachine(typeof(_003CEnter_003Ed__26))]
	public override IEnumerator Enter(Oui from)
	{
		Stats.MakeRequest();
		base.Overworld.ShowConfirmUI = false;
		fromAreaInspect = from is OuiChapterPanel;
		PageIndex = 0;
		Visible = true;
		base.X = -1920f;
		turningPage = false;
		turningScale = 1f;
		rotation = 0f;
		dot = 0f;
		dotTarget = 0f;
		dotEase = 0f;
		leftArrowEase = 0f;
		rightArrowEase = 0f;
		NextPageBuffer = VirtualContent.CreateRenderTarget("journal-a", 1610, 1000);
		CurrentPageBuffer = VirtualContent.CreateRenderTarget("journal-b", 1610, 1000);
		Pages.Add(new OuiJournalCover(this));
		Pages.Add(new OuiJournalProgress(this));
		Pages.Add(new OuiJournalSpeedrun(this));
		Pages.Add(new OuiJournalDeaths(this));
		Pages.Add(new OuiJournalPoem(this));
		if (Stats.Has())
		{
			Pages.Add(new OuiJournalGlobal(this));
		}
		int num = 0;
		foreach (OuiJournalPage page in Pages)
		{
			page.PageIndex = num++;
		}
		Pages[0].Redraw(CurrentPageBuffer);
		cameraStart = base.Overworld.Mountain.UntiltedCamera;
		cameraEnd = cameraStart;
		cameraEnd.Position += -cameraStart.Rotation.Forward() * 1f;
		base.Overworld.Mountain.EaseCamera(base.Overworld.Mountain.Area, cameraEnd, 2f);
		base.Overworld.Mountain.AllowUserRotation = false;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.4f)
		{
			rotation = -0.025f * Ease.BackOut(p);
			base.X = -1920f + 1920f * Ease.CubeInOut(p);
			dotEase = p;
			yield return null;
		}
		dotEase = 1f;
	}

	public override void HandleGraphicsReset()
	{
		base.HandleGraphicsReset();
		if (Pages.Count > 0)
		{
			Page.Redraw(CurrentPageBuffer);
		}
	}

	[IteratorStateMachine(typeof(_003CTurnPage_003Ed__28))]
	public IEnumerator TurnPage(int direction)
	{
		turningPage = true;
		if (direction < 0)
		{
			PageIndex--;
			turningScale = -1f;
			dotTarget -= 1f;
			Page.Redraw(CurrentPageBuffer);
			NextPage.Redraw(NextPageBuffer);
			while ((turningScale = Calc.Approach(turningScale, 1f, Engine.DeltaTime * 8f)) < 1f)
			{
				yield return null;
			}
		}
		else
		{
			NextPage.Redraw(NextPageBuffer);
			turningScale = 1f;
			dotTarget += 1f;
			while ((turningScale = Calc.Approach(turningScale, -1f, Engine.DeltaTime * 8f)) > -1f)
			{
				yield return null;
			}
			PageIndex++;
			Page.Redraw(CurrentPageBuffer);
		}
		turningScale = 1f;
		turningPage = false;
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__29))]
	public override IEnumerator Leave(Oui next)
	{
		Audio.Play("event:/ui/world_map/journal/back");
		base.Overworld.Mountain.EaseCamera(base.Overworld.Mountain.Area, cameraStart, 0.4f);
		UserIO.SaveHandler(file: false, settings: true);
		yield return EaseOut(0.4f);
		while (UserIO.Saving)
		{
			yield return null;
		}
		CurrentPageBuffer.Dispose();
		NextPageBuffer.Dispose();
		base.Overworld.ShowConfirmUI = true;
		Pages.Clear();
		Visible = false;
		base.Overworld.Mountain.AllowUserRotation = true;
	}

	[IteratorStateMachine(typeof(_003CEaseOut_003Ed__30))]
	private IEnumerator EaseOut(float duration)
	{
		float rotFrom = rotation;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			rotation = rotFrom * (1f - Ease.BackOut(p));
			base.X = 0f + -1920f * Ease.CubeInOut(p);
			dotEase = 1f - p;
			yield return null;
		}
		dotEase = 0f;
	}

	public override void Update()
	{
		base.Update();
		dot = Calc.Approach(dot, dotTarget, Engine.DeltaTime * 8f);
		leftArrowEase = Calc.Approach(leftArrowEase, (dotTarget > 0f) ? 1 : 0, Engine.DeltaTime * 5f) * dotEase;
		rightArrowEase = Calc.Approach(rightArrowEase, (dotTarget < (float)(Pages.Count - 1)) ? 1 : 0, Engine.DeltaTime * 5f) * dotEase;
		if (!Focused || turningPage)
		{
			return;
		}
		Page.Update();
		if (!PageTurningLocked)
		{
			if (Input.MenuLeft.Pressed && PageIndex > 0)
			{
				if (PageIndex == 1)
				{
					Audio.Play("event:/ui/world_map/journal/page_cover_back");
				}
				else
				{
					Audio.Play("event:/ui/world_map/journal/page_main_back");
				}
				Add(new Coroutine(TurnPage(-1)));
			}
			else if (Input.MenuRight.Pressed && PageIndex < Pages.Count - 1)
			{
				if (PageIndex == 0)
				{
					Audio.Play("event:/ui/world_map/journal/page_cover_forward");
				}
				else
				{
					Audio.Play("event:/ui/world_map/journal/page_main_forward");
				}
				Add(new Coroutine(TurnPage(1)));
			}
		}
		if (!PageTurningLocked && (Input.MenuJournal.Pressed || Input.MenuCancel.Pressed))
		{
			Close();
		}
	}

	private void Close()
	{
		if (fromAreaInspect)
		{
			base.Overworld.Goto<OuiChapterPanel>();
		}
		else
		{
			base.Overworld.Goto<OuiChapterSelect>();
		}
	}

	public override void Render()
	{
		Vector2 vector = Position + new Vector2(128f, 120f);
		float num = Ease.CubeInOut(Math.Max(0f, turningScale));
		float num2 = Ease.CubeInOut(Math.Abs(Math.Min(0f, turningScale)));
		if (SaveData.Instance.CheatMode)
		{
			MTN.FileSelect["cheatmode"].DrawCentered(vector + new Vector2(80f, 360f), Color.White, 1f, (float)Math.PI / 2f);
		}
		if (SaveData.Instance.AssistMode)
		{
			MTN.FileSelect["assist"].DrawCentered(vector + new Vector2(100f, 370f), Color.White, 1f, (float)Math.PI / 2f);
		}
		MTexture mTexture = MTN.Journal["edge"];
		mTexture.Draw(vector + new Vector2(-mTexture.Width, 0f), Vector2.Zero, Color.White, 1f, rotation);
		if (PageIndex > 0)
		{
			MTN.Journal[PrevPage.PageTexture].Draw(vector, Vector2.Zero, backColor, new Vector2(-1f, 1f), rotation);
		}
		if (turningPage)
		{
			MTN.Journal[NextPage.PageTexture].Draw(vector, Vector2.Zero, Color.White, 1f, rotation);
			Draw.SpriteBatch.Draw((RenderTarget2D)NextPageBuffer, vector, NextPageBuffer.Bounds, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
		}
		if (turningPage && num2 > 0f)
		{
			MTN.Journal[Page.PageTexture].Draw(vector, Vector2.Zero, backColor, new Vector2(-1f * num2, 1f), rotation);
		}
		if (num > 0f)
		{
			MTN.Journal[Page.PageTexture].Draw(vector, Vector2.Zero, Color.White, new Vector2(num, 1f), rotation);
			Draw.SpriteBatch.Draw((RenderTarget2D)CurrentPageBuffer, vector, CurrentPageBuffer.Bounds, Color.White, rotation, Vector2.Zero, new Vector2(num, 1f), SpriteEffects.None, 0f);
		}
		if (Pages.Count > 0)
		{
			int count = Pages.Count;
			MTexture mTexture2 = GFX.Gui["dot_outline"];
			int num3 = mTexture2.Width * count;
			Vector2 vector2 = new Vector2(960f, 1040f - 40f * Ease.CubeOut(dotEase));
			for (int i = 0; i < count; i++)
			{
				mTexture2.DrawCentered(vector2 + new Vector2((float)(-num3 / 2) + (float)mTexture2.Width * ((float)i + 0.5f), 0f), Color.White * 0.25f);
			}
			float x = 1f + Calc.YoYo(dot % 1f) * 4f;
			mTexture2.DrawCentered(vector2 + new Vector2((float)(-num3 / 2) + (float)mTexture2.Width * (dot + 0.5f), 0f), Color.White, new Vector2(x, 1f));
			GFX.Gui["dotarrow_outline"].DrawCentered(vector2 + new Vector2(-num3 / 2 - 50, 32f * (1f - Ease.CubeOut(leftArrowEase))), Color.White * leftArrowEase, new Vector2(-1f, 1f));
			GFX.Gui["dotarrow_outline"].DrawCentered(vector2 + new Vector2(num3 / 2 + 50, 32f * (1f - Ease.CubeOut(rightArrowEase))), Color.White * rightArrowEase);
		}
	}
}
