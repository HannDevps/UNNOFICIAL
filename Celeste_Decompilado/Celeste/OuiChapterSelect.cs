using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiChapterSelect : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelect _003C_003E4__this;

		public Oui from;

		private OuiChapterSelectIcon _003Cunselected_003E5__2;

		private List<OuiChapterSelectIcon>.Enumerator _003C_003E7__wrap2;

		private int _003Cch_003E5__4;

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
		public _003CEnter_003Ed__17(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
				{
					_003C_003E1__state = -1;
					ouiChapterSelect.Visible = true;
					ouiChapterSelect.EaseCamera();
					ouiChapterSelect.display = true;
					ouiChapterSelect.journalEnabled = Celeste.PlayMode == Celeste.PlayModes.Debug || SaveData.Instance.CheatMode;
					for (int i = 0; i <= SaveData.Instance.UnlockedAreas; i++)
					{
						if (ouiChapterSelect.journalEnabled)
						{
							break;
						}
						if (SaveData.Instance.Areas[i].Modes[0].TimePlayed > 0 && !AreaData.Get(i).Interlude)
						{
							ouiChapterSelect.journalEnabled = true;
						}
					}
					_003Cunselected_003E5__2 = null;
					if (from is OuiChapterPanel)
					{
						(_003Cunselected_003E5__2 = ouiChapterSelect.icons[ouiChapterSelect.area]).Unselect();
					}
					_003C_003E7__wrap2 = ouiChapterSelect.icons.GetEnumerator();
					_003C_003E1__state = -3;
					goto IL_01c9;
				}
				case 1:
					_003C_003E1__state = -3;
					goto IL_01c9;
				case 2:
					_003C_003E1__state = -1;
					_003C_003E2__current = ouiChapterSelect.PerformCh9Unlock(_003Cch_003E5__4 != 10);
					_003C_003E1__state = 3;
					return true;
				case 3:
					_003C_003E1__state = -1;
					goto IL_0262;
				case 4:
					{
						_003C_003E1__state = -1;
						break;
					}
					IL_01c9:
					if (_003C_003E7__wrap2.MoveNext())
					{
						OuiChapterSelectIcon current = _003C_003E7__wrap2.Current;
						if (current.Area <= SaveData.Instance.UnlockedAreas && current != _003Cunselected_003E5__2)
						{
							current.Position = current.HiddenPosition;
							current.Show();
							current.AssistModeUnlockable = false;
						}
						else if (SaveData.Instance.AssistMode && current.Area == SaveData.Instance.UnlockedAreas + 1 && current.Area <= SaveData.Instance.MaxAssistArea)
						{
							current.Position = current.HiddenPosition;
							current.Show();
							current.AssistModeUnlockable = true;
						}
						_003C_003E2__current = 0.01f;
						_003C_003E1__state = 1;
						return true;
					}
					_003C_003Em__Finally1();
					_003C_003E7__wrap2 = default(List<OuiChapterSelectIcon>.Enumerator);
					if (!ouiChapterSelect.autoAdvancing && SaveData.Instance.UnlockedAreas == 10 && !SaveData.Instance.RevealedChapter9)
					{
						_003Cch_003E5__4 = ouiChapterSelect.area;
						_003C_003E2__current = ouiChapterSelect.SetupCh9Unlock();
						_003C_003E1__state = 2;
						return true;
					}
					goto IL_0262;
					IL_0262:
					if (from is OuiChapterPanel)
					{
						_003C_003E2__current = 0.25f;
						_003C_003E1__state = 4;
						return true;
					}
					break;
				}
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap2/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CLeave_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelect _003C_003E4__this;

		public Oui next;

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
		public _003CLeave_003Ed__18(int _003C_003E1__state)
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
			OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiChapterSelect.display = false;
				if (next is OuiMainMenu)
				{
					while (ouiChapterSelect.area > SaveData.Instance.UnlockedAreas)
					{
						ouiChapterSelect.area--;
					}
					UserIO.SaveHandler(file: true, settings: false);
					_003C_003E2__current = ouiChapterSelect.EaseOut(next);
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = ouiChapterSelect.EaseOut(next);
				_003C_003E1__state = 3;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00a7;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00a7;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_00a7:
				if (UserIO.Saving)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				break;
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
	private sealed class _003CEaseOut_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Oui next;

		public OuiChapterSelect _003C_003E4__this;

		private OuiChapterSelectIcon _003Cselected_003E5__2;

		private List<OuiChapterSelectIcon>.Enumerator _003C_003E7__wrap2;

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
		public _003CEaseOut_003Ed__19(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Cselected_003E5__2 = null;
					if (next is OuiChapterPanel)
					{
						(_003Cselected_003E5__2 = ouiChapterSelect.icons[ouiChapterSelect.area]).Select();
					}
					_003C_003E7__wrap2 = ouiChapterSelect.icons.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				if (_003C_003E7__wrap2.MoveNext())
				{
					OuiChapterSelectIcon current = _003C_003E7__wrap2.Current;
					if (_003Cselected_003E5__2 != current)
					{
						current.Hide();
					}
					_003C_003E2__current = 0.01f;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap2 = default(List<OuiChapterSelectIcon>.Enumerator);
				ouiChapterSelect.Visible = false;
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap2/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAutoAdvanceRoutine_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelect _003C_003E4__this;

		private int _003CnextArea_003E5__2;

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
		public _003CAutoAdvanceRoutine_003Ed__21(int _003C_003E1__state)
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
			OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (ouiChapterSelect.area >= SaveData.Instance.MaxArea)
				{
					break;
				}
				_003CnextArea_003E5__2 = ouiChapterSelect.area + 1;
				if (_003CnextArea_003E5__2 == 9 || _003CnextArea_003E5__2 == 10)
				{
					ouiChapterSelect.icons[_003CnextArea_003E5__2].HideIcon = true;
				}
				goto IL_009c;
			case 1:
				_003C_003E1__state = -1;
				goto IL_009c;
			case 2:
				_003C_003E1__state = -1;
				if (_003CnextArea_003E5__2 == 10)
				{
					_003C_003E2__current = ouiChapterSelect.PerformCh9Unlock();
					_003C_003E1__state = 3;
					return true;
				}
				if (_003CnextArea_003E5__2 == 9)
				{
					_003C_003E2__current = ouiChapterSelect.PerformCh8Unlock();
					_003C_003E1__state = 4;
					return true;
				}
				Audio.Play("event:/ui/postgame/unlock_newchapter");
				Audio.Play("event:/ui/world_map/icon/roll_right");
				ouiChapterSelect.area = _003CnextArea_003E5__2;
				ouiChapterSelect.EaseCamera();
				ouiChapterSelect.Overworld.Maddy.Hide();
				goto IL_014e;
			case 3:
				_003C_003E1__state = -1;
				goto IL_014e;
			case 4:
				_003C_003E1__state = -1;
				goto IL_014e;
			case 5:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_009c:
				if (!ouiChapterSelect.Selected)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
				IL_014e:
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
				return true;
			}
			ouiChapterSelect.autoAdvancing = false;
			ouiChapterSelect.disableInput = false;
			ouiChapterSelect.Focused = true;
			ouiChapterSelect.Overworld.ShowInputUI = true;
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
	private sealed class _003C_003Ec__DisplayClass25_0
	{
		public bool ready;

		internal void _003CPerformCh8Unlock_003Eb__0()
		{
			ready = true;
		}
	}

	[CompilerGenerated]
	private sealed class _003CPerformCh8Unlock_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelect _003C_003E4__this;

		private _003C_003Ec__DisplayClass25_0 _003C_003E8__1;

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
		public _003CPerformCh8Unlock_003Ed__25(int _003C_003E1__state)
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
			OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass25_0();
				Audio.Play("event:/ui/postgame/unlock_newchapter");
				Audio.Play("event:/ui/world_map/icon/roll_right");
				ouiChapterSelect.area = 9;
				ouiChapterSelect.EaseCamera();
				ouiChapterSelect.Overworld.Maddy.Hide();
				_003C_003E8__1.ready = false;
				ouiChapterSelect.icons[9].HighlightUnlock(delegate
				{
					_003C_003E8__1.ready = true;
				});
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (!_003C_003E8__1.ready)
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

	[CompilerGenerated]
	private sealed class _003CSetupCh9Unlock_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelect _003C_003E4__this;

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
		public _003CSetupCh9Unlock_003Ed__26(int _003C_003E1__state)
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
			OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiChapterSelect.icons[10].HideIcon = true;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if (ouiChapterSelect.area < 9)
			{
				ouiChapterSelect.area++;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
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
	private sealed class _003C_003Ec__DisplayClass27_0
	{
		public bool ready;

		internal void _003CPerformCh9Unlock_003Eb__0()
		{
			ready = true;
		}
	}

	[CompilerGenerated]
	private sealed class _003CPerformCh9Unlock_003Ed__27 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelect _003C_003E4__this;

		private _003C_003Ec__DisplayClass27_0 _003C_003E8__1;

		public bool easeCamera;

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
		public _003CPerformCh9Unlock_003Ed__27(int _003C_003E1__state)
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
			OuiChapterSelect ouiChapterSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass27_0();
				Audio.Play("event:/ui/postgame/unlock_newchapter");
				Audio.Play("event:/ui/world_map/icon/roll_right");
				ouiChapterSelect.area = 10;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E8__1.ready = false;
				ouiChapterSelect.icons[10].HighlightUnlock(delegate
				{
					_003C_003E8__1.ready = true;
				});
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if (!_003C_003E8__1.ready)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			if (easeCamera)
			{
				ouiChapterSelect.EaseCamera();
			}
			ouiChapterSelect.Overworld.Maddy.Hide();
			SaveData.Instance.RevealedChapter9 = true;
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

	private List<OuiChapterSelectIcon> icons = new List<OuiChapterSelectIcon>();

	private int indexToSnap = -1;

	private const int scarfSegmentSize = 2;

	private MTexture scarf = GFX.Gui["areas/hover"];

	private MTexture[] scarfSegments;

	private float ease;

	private float journalEase;

	private bool journalEnabled;

	private bool disableInput;

	private bool display;

	private float inputDelay;

	private bool autoAdvancing;

	private int area
	{
		get
		{
			return SaveData.Instance.LastArea.ID;
		}
		set
		{
			SaveData.Instance.LastArea.ID = value;
		}
	}

	public override bool IsStart(Overworld overworld, Overworld.StartMode start)
	{
		if (start == Overworld.StartMode.AreaComplete || start == Overworld.StartMode.AreaQuit)
		{
			indexToSnap = area;
		}
		return false;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		int count = AreaData.Areas.Count;
		for (int i = 0; i < count; i++)
		{
			MTexture mTexture = GFX.Gui[AreaData.Areas[i].Icon];
			MTexture back = (GFX.Gui.Has(AreaData.Areas[i].Icon + "_back") ? GFX.Gui[AreaData.Areas[i].Icon + "_back"] : mTexture);
			icons.Add(new OuiChapterSelectIcon(i, mTexture, back));
			base.Scene.Add(icons[i]);
		}
		scarfSegments = new MTexture[scarf.Height / 2];
		for (int j = 0; j < scarfSegments.Length; j++)
		{
			scarfSegments[j] = scarf.GetSubtexture(0, j * 2, scarf.Width, 2);
		}
		if (indexToSnap >= 0)
		{
			area = indexToSnap;
			icons[indexToSnap].SnapToSelected();
		}
		base.Depth = -20;
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__17))]
	public override IEnumerator Enter(Oui from)
	{
		Visible = true;
		EaseCamera();
		display = true;
		journalEnabled = Celeste.PlayMode == Celeste.PlayModes.Debug || SaveData.Instance.CheatMode;
		for (int i = 0; i <= SaveData.Instance.UnlockedAreas; i++)
		{
			if (journalEnabled)
			{
				break;
			}
			if (SaveData.Instance.Areas[i].Modes[0].TimePlayed > 0 && !AreaData.Get(i).Interlude)
			{
				journalEnabled = true;
			}
		}
		OuiChapterSelectIcon unselected = null;
		if (from is OuiChapterPanel)
		{
			OuiChapterSelectIcon ouiChapterSelectIcon;
			unselected = (ouiChapterSelectIcon = icons[area]);
			ouiChapterSelectIcon.Unselect();
		}
		foreach (OuiChapterSelectIcon icon in icons)
		{
			if (icon.Area <= SaveData.Instance.UnlockedAreas && icon != unselected)
			{
				icon.Position = icon.HiddenPosition;
				icon.Show();
				icon.AssistModeUnlockable = false;
			}
			else if (SaveData.Instance.AssistMode && icon.Area == SaveData.Instance.UnlockedAreas + 1 && icon.Area <= SaveData.Instance.MaxAssistArea)
			{
				icon.Position = icon.HiddenPosition;
				icon.Show();
				icon.AssistModeUnlockable = true;
			}
			yield return 0.01f;
		}
		if (!autoAdvancing && SaveData.Instance.UnlockedAreas == 10 && !SaveData.Instance.RevealedChapter9)
		{
			int ch = area;
			yield return SetupCh9Unlock();
			yield return PerformCh9Unlock(ch != 10);
		}
		if (from is OuiChapterPanel)
		{
			yield return 0.25f;
		}
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__18))]
	public override IEnumerator Leave(Oui next)
	{
		display = false;
		if (next is OuiMainMenu)
		{
			while (area > SaveData.Instance.UnlockedAreas)
			{
				area--;
			}
			UserIO.SaveHandler(file: true, settings: false);
			yield return EaseOut(next);
			while (UserIO.Saving)
			{
				yield return null;
			}
		}
		else
		{
			yield return EaseOut(next);
		}
	}

	[IteratorStateMachine(typeof(_003CEaseOut_003Ed__19))]
	private IEnumerator EaseOut(Oui next)
	{
		OuiChapterSelectIcon selected = null;
		if (next is OuiChapterPanel)
		{
			OuiChapterSelectIcon ouiChapterSelectIcon;
			selected = (ouiChapterSelectIcon = icons[area]);
			ouiChapterSelectIcon.Select();
		}
		foreach (OuiChapterSelectIcon icon in icons)
		{
			if (selected != icon)
			{
				icon.Hide();
			}
			yield return 0.01f;
		}
		Visible = false;
	}

	public void AdvanceToNext()
	{
		autoAdvancing = true;
		base.Overworld.ShowInputUI = false;
		Focused = false;
		disableInput = true;
		Add(new Coroutine(AutoAdvanceRoutine()));
	}

	[IteratorStateMachine(typeof(_003CAutoAdvanceRoutine_003Ed__21))]
	private IEnumerator AutoAdvanceRoutine()
	{
		if (area < SaveData.Instance.MaxArea)
		{
			int nextArea = area + 1;
			if (nextArea == 9 || nextArea == 10)
			{
				icons[nextArea].HideIcon = true;
			}
			while (!base.Selected)
			{
				yield return null;
			}
			yield return 1f;
			switch (nextArea)
			{
			case 10:
				yield return PerformCh9Unlock();
				break;
			case 9:
				yield return PerformCh8Unlock();
				break;
			default:
				Audio.Play("event:/ui/postgame/unlock_newchapter");
				Audio.Play("event:/ui/world_map/icon/roll_right");
				area = nextArea;
				EaseCamera();
				base.Overworld.Maddy.Hide();
				break;
			}
			yield return 0.25f;
		}
		autoAdvancing = false;
		disableInput = false;
		Focused = true;
		base.Overworld.ShowInputUI = true;
	}

	public override void Update()
	{
		if (Focused && !disableInput)
		{
			inputDelay -= Engine.DeltaTime;
			if (area >= 0 && area < AreaData.Areas.Count)
			{
				Input.SetLightbarColor(AreaData.Get(area).TitleBaseColor);
			}
			if (Input.MenuCancel.Pressed)
			{
				Audio.Play("event:/ui/main/button_back");
				base.Overworld.Goto<OuiMainMenu>();
				base.Overworld.Maddy.Hide();
			}
			else if (Input.MenuJournal.Pressed && journalEnabled)
			{
				Audio.Play("event:/ui/world_map/journal/select");
				base.Overworld.Goto<OuiJournal>();
			}
			else if (inputDelay <= 0f)
			{
				if (area > 0 && Input.MenuLeft.Pressed)
				{
					Audio.Play("event:/ui/world_map/icon/roll_left");
					inputDelay = 0.15f;
					area--;
					icons[area].Hovered(-1);
					EaseCamera();
					base.Overworld.Maddy.Hide();
				}
				else if (Input.MenuRight.Pressed)
				{
					bool flag = SaveData.Instance.AssistMode && area == SaveData.Instance.UnlockedAreas && area < SaveData.Instance.MaxAssistArea;
					if (area < SaveData.Instance.UnlockedAreas || flag)
					{
						Audio.Play("event:/ui/world_map/icon/roll_right");
						inputDelay = 0.15f;
						area++;
						icons[area].Hovered(1);
						if (area <= SaveData.Instance.UnlockedAreas)
						{
							EaseCamera();
						}
						base.Overworld.Maddy.Hide();
					}
				}
				else if (Input.MenuConfirm.Pressed)
				{
					if (icons[area].AssistModeUnlockable)
					{
						Audio.Play("event:/ui/world_map/icon/assist_skip");
						Focused = false;
						base.Overworld.ShowInputUI = false;
						icons[area].AssistModeUnlock(delegate
						{
							Focused = true;
							base.Overworld.ShowInputUI = true;
							EaseCamera();
							if (area == 10)
							{
								SaveData.Instance.RevealedChapter9 = true;
							}
							if (area < SaveData.Instance.MaxAssistArea)
							{
								OuiChapterSelectIcon ouiChapterSelectIcon = icons[area + 1];
								ouiChapterSelectIcon.AssistModeUnlockable = true;
								ouiChapterSelectIcon.Position = ouiChapterSelectIcon.HiddenPosition;
								ouiChapterSelectIcon.Show();
							}
						});
					}
					else
					{
						Audio.Play("event:/ui/world_map/icon/select");
						SaveData.Instance.LastArea.Mode = AreaMode.Normal;
						base.Overworld.Goto<OuiChapterPanel>();
					}
				}
			}
		}
		ease = Calc.Approach(ease, display ? 1f : 0f, Engine.DeltaTime * 3f);
		journalEase = Calc.Approach(journalEase, (display && !disableInput && Focused && journalEnabled) ? 1f : 0f, Engine.DeltaTime * 4f);
		base.Update();
	}

	public override void Render()
	{
		Vector2 vector = new Vector2(960f, (float)(-scarf.Height) * Ease.CubeInOut(1f - ease));
		for (int i = 0; i < scarfSegments.Length; i++)
		{
			float num = Ease.CubeIn((float)i / (float)scarfSegments.Length);
			float x = num * (float)Math.Sin(base.Scene.RawTimeActive * 4f + (float)i * 0.05f) * 4f - num * 16f;
			scarfSegments[i].DrawJustified(vector + new Vector2(x, i * 2), new Vector2(0.5f, 0f));
		}
		if (journalEase > 0f)
		{
			Vector2 position = new Vector2(128f * Ease.CubeOut(journalEase), 952f);
			GFX.Gui["menu/journal"].DrawCentered(position, Color.White * Ease.CubeOut(journalEase));
			Input.GuiButton(Input.MenuJournal).Draw(position, Vector2.Zero, Color.White * Ease.CubeOut(journalEase));
		}
	}

	private void EaseCamera()
	{
		AreaData areaData = AreaData.Areas[area];
		base.Overworld.Mountain.EaseCamera(area, areaData.MountainIdle, null, nearTarget: true, area == 10);
		base.Overworld.Mountain.Model.EaseState(areaData.MountainState);
	}

	[IteratorStateMachine(typeof(_003CPerformCh8Unlock_003Ed__25))]
	private IEnumerator PerformCh8Unlock()
	{
		Audio.Play("event:/ui/postgame/unlock_newchapter");
		Audio.Play("event:/ui/world_map/icon/roll_right");
		area = 9;
		EaseCamera();
		base.Overworld.Maddy.Hide();
		bool ready = false;
		icons[9].HighlightUnlock(delegate
		{
			ready = true;
		});
		while (!ready)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CSetupCh9Unlock_003Ed__26))]
	private IEnumerator SetupCh9Unlock()
	{
		icons[10].HideIcon = true;
		yield return 0.25f;
		while (area < 9)
		{
			area++;
			yield return 0.1f;
		}
	}

	[IteratorStateMachine(typeof(_003CPerformCh9Unlock_003Ed__27))]
	private IEnumerator PerformCh9Unlock(bool easeCamera = true)
	{
		Audio.Play("event:/ui/postgame/unlock_newchapter");
		Audio.Play("event:/ui/world_map/icon/roll_right");
		area = 10;
		yield return 0.25f;
		bool ready = false;
		icons[10].HighlightUnlock(delegate
		{
			ready = true;
		});
		while (!ready)
		{
			yield return null;
		}
		if (easeCamera)
		{
			EaseCamera();
		}
		base.Overworld.Maddy.Hide();
		SaveData.Instance.RevealedChapter9 = true;
	}
}
