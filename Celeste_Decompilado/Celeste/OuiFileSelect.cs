using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiFileSelect : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiFileSelect _003C_003E4__this;

		public Oui from;

		private float _003Celapsed_003E5__2;

		private FileErrorOverlay _003Cerror_003E5__3;

		private int _003Ci_003E5__4;

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
		public _003CEnter_003Ed__7(int _003C_003E1__state)
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
			OuiFileSelect ouiFileSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiFileSelect.SlotSelected = false;
				if (!Loaded)
				{
					for (int i = 0; i < ouiFileSelect.Slots.Length; i++)
					{
						if (ouiFileSelect.Slots[i] != null)
						{
							ouiFileSelect.Scene.Remove(ouiFileSelect.Slots[i]);
						}
					}
					RunThread.Start(ouiFileSelect.LoadThread, "FILE_LOADING");
					_003Celapsed_003E5__2 = 0f;
					goto IL_00bf;
				}
				if (!(from is OuiFileNaming) && !(from is OuiAssistMode))
				{
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_019e;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00bf;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0133;
			case 3:
				_003C_003E1__state = -1;
				goto IL_019e;
			case 4:
				{
					_003C_003E1__state = -1;
					_003Ci_003E5__4++;
					goto IL_033f;
				}
				IL_019e:
				ouiFileSelect.HasSlots = false;
				for (int j = 0; j < ouiFileSelect.Slots.Length; j++)
				{
					if (ouiFileSelect.Slots[j].Exists)
					{
						ouiFileSelect.HasSlots = true;
					}
				}
				Audio.Play("event:/ui/main/whoosh_savefile_in");
				if (from is OuiFileNaming || from is OuiAssistMode)
				{
					if (!ouiFileSelect.SlotSelected)
					{
						ouiFileSelect.SelectSlot(reset: false);
					}
					break;
				}
				if (!ouiFileSelect.HasSlots)
				{
					ouiFileSelect.SlotIndex = 0;
					ouiFileSelect.Slots[ouiFileSelect.SlotIndex].Position = new Vector2(ouiFileSelect.Slots[ouiFileSelect.SlotIndex].HiddenPosition(1, 0).X, ouiFileSelect.Slots[ouiFileSelect.SlotIndex].SelectedPosition.Y);
					ouiFileSelect.SelectSlot(reset: true);
					break;
				}
				if (ouiFileSelect.SlotSelected)
				{
					break;
				}
				Alarm.Set(ouiFileSelect, 0.4f, delegate
				{
					Audio.Play("event:/ui/main/savefile_rollover_first");
				});
				_003Ci_003E5__4 = 0;
				goto IL_033f;
				IL_0133:
				if (_003Cerror_003E5__3.Open)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				if (!_003Cerror_003E5__3.Ignore)
				{
					ouiFileSelect.Overworld.Goto<OuiMainMenu>();
					return false;
				}
				_003Cerror_003E5__3 = null;
				goto IL_019e;
				IL_00bf:
				if (!Loaded || _003Celapsed_003E5__2 < 0.5f)
				{
					_003Celapsed_003E5__2 += Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				for (int num2 = 0; num2 < ouiFileSelect.Slots.Length; num2++)
				{
					if (ouiFileSelect.Slots[num2] != null)
					{
						ouiFileSelect.Scene.Add(ouiFileSelect.Slots[num2]);
					}
				}
				if (!ouiFileSelect.loadedSuccess)
				{
					_003Cerror_003E5__3 = new FileErrorOverlay(FileErrorOverlay.Error.Load);
					goto IL_0133;
				}
				goto IL_019e;
				IL_033f:
				if (_003Ci_003E5__4 < ouiFileSelect.Slots.Length)
				{
					ouiFileSelect.Slots[_003Ci_003E5__4].Position = new Vector2(ouiFileSelect.Slots[_003Ci_003E5__4].HiddenPosition(1, 0).X, ouiFileSelect.Slots[_003Ci_003E5__4].IdlePosition.Y);
					ouiFileSelect.Slots[_003Ci_003E5__4].Show();
					_003C_003E2__current = 0.02f;
					_003C_003E1__state = 4;
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
	private sealed class _003CLeave_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Oui next;

		public OuiFileSelect _003C_003E4__this;

		private int _003CslideTo_003E5__2;

		private int _003Ci_003E5__3;

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
		public _003CLeave_003Ed__9(int _003C_003E1__state)
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
			OuiFileSelect ouiFileSelect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/ui/main/whoosh_savefile_out");
				_003CslideTo_003E5__2 = 1;
				if (next == null || next is OuiChapterSelect || next is OuiFileNaming || next is OuiAssistMode)
				{
					_003CslideTo_003E5__2 = -1;
				}
				_003Ci_003E5__3 = 0;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ci_003E5__3++;
				break;
			}
			if (_003Ci_003E5__3 < ouiFileSelect.Slots.Length)
			{
				if (next is OuiFileNaming && ouiFileSelect.SlotIndex == _003Ci_003E5__3)
				{
					ouiFileSelect.Slots[_003Ci_003E5__3].MoveTo(ouiFileSelect.Slots[_003Ci_003E5__3].IdlePosition.X, ouiFileSelect.Slots[0].IdlePosition.Y);
				}
				else if (next is OuiAssistMode && ouiFileSelect.SlotIndex == _003Ci_003E5__3)
				{
					ouiFileSelect.Slots[_003Ci_003E5__3].MoveTo(ouiFileSelect.Slots[_003Ci_003E5__3].IdlePosition.X, -400f);
				}
				else
				{
					ouiFileSelect.Slots[_003Ci_003E5__3].Hide(_003CslideTo_003E5__2, 0);
				}
				_003C_003E2__current = 0.02f;
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

	public OuiFileSelectSlot[] Slots = new OuiFileSelectSlot[3];

	public int SlotIndex;

	public bool SlotSelected;

	public static bool Loaded;

	private bool loadedSuccess;

	public bool HasSlots;

	public OuiFileSelect()
	{
		Loaded = false;
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__7))]
	public override IEnumerator Enter(Oui from)
	{
		SlotSelected = false;
		if (!Loaded)
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				if (Slots[i] != null)
				{
					base.Scene.Remove(Slots[i]);
				}
			}
			RunThread.Start(LoadThread, "FILE_LOADING");
			float elapsed = 0f;
			while (!Loaded || elapsed < 0.5f)
			{
				elapsed += Engine.DeltaTime;
				yield return null;
			}
			for (int j = 0; j < Slots.Length; j++)
			{
				if (Slots[j] != null)
				{
					base.Scene.Add(Slots[j]);
				}
			}
			if (!loadedSuccess)
			{
				FileErrorOverlay error = new FileErrorOverlay(FileErrorOverlay.Error.Load);
				while (error.Open)
				{
					yield return null;
				}
				if (!error.Ignore)
				{
					base.Overworld.Goto<OuiMainMenu>();
					yield break;
				}
			}
		}
		else if (!(from is OuiFileNaming) && !(from is OuiAssistMode))
		{
			yield return 0.2f;
		}
		HasSlots = false;
		for (int k = 0; k < Slots.Length; k++)
		{
			if (Slots[k].Exists)
			{
				HasSlots = true;
			}
		}
		Audio.Play("event:/ui/main/whoosh_savefile_in");
		if (from is OuiFileNaming || from is OuiAssistMode)
		{
			if (!SlotSelected)
			{
				SelectSlot(reset: false);
			}
		}
		else if (!HasSlots)
		{
			SlotIndex = 0;
			Slots[SlotIndex].Position = new Vector2(Slots[SlotIndex].HiddenPosition(1, 0).X, Slots[SlotIndex].SelectedPosition.Y);
			SelectSlot(reset: true);
		}
		else if (!SlotSelected)
		{
			Alarm.Set(this, 0.4f, delegate
			{
				Audio.Play("event:/ui/main/savefile_rollover_first");
			});
			for (int i2 = 0; i2 < Slots.Length; i2++)
			{
				Slots[i2].Position = new Vector2(Slots[i2].HiddenPosition(1, 0).X, Slots[i2].IdlePosition.Y);
				Slots[i2].Show();
				yield return 0.02f;
			}
		}
	}

	private void LoadThread()
	{
		if (UserIO.Open(UserIO.Mode.Read))
		{
			for (int i = 0; i < Slots.Length; i++)
			{
				OuiFileSelectSlot ouiFileSelectSlot;
				if (!UserIO.Exists(SaveData.GetFilename(i)))
				{
					ouiFileSelectSlot = new OuiFileSelectSlot(i, this, corrupted: false);
				}
				else
				{
					SaveData saveData = UserIO.Load<SaveData>(SaveData.GetFilename(i));
					if (saveData != null)
					{
						saveData.AfterInitialize();
						ouiFileSelectSlot = new OuiFileSelectSlot(i, this, saveData);
					}
					else
					{
						ouiFileSelectSlot = new OuiFileSelectSlot(i, this, corrupted: true);
					}
				}
				Slots[i] = ouiFileSelectSlot;
			}
			UserIO.Close();
			loadedSuccess = true;
		}
		Loaded = true;
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__9))]
	public override IEnumerator Leave(Oui next)
	{
		Audio.Play("event:/ui/main/whoosh_savefile_out");
		int slideTo = 1;
		if (next == null || next is OuiChapterSelect || next is OuiFileNaming || next is OuiAssistMode)
		{
			slideTo = -1;
		}
		for (int i = 0; i < Slots.Length; i++)
		{
			if (next is OuiFileNaming && SlotIndex == i)
			{
				Slots[i].MoveTo(Slots[i].IdlePosition.X, Slots[0].IdlePosition.Y);
			}
			else if (next is OuiAssistMode && SlotIndex == i)
			{
				Slots[i].MoveTo(Slots[i].IdlePosition.X, -400f);
			}
			else
			{
				Slots[i].Hide(slideTo, 0);
			}
			yield return 0.02f;
		}
	}

	public void UnselectHighlighted()
	{
		SlotSelected = false;
		Slots[SlotIndex].Unselect();
		for (int i = 0; i < Slots.Length; i++)
		{
			if (SlotIndex != i)
			{
				Slots[i].Show();
			}
		}
	}

	public void SelectSlot(bool reset)
	{
		if (!Slots[SlotIndex].Exists && reset)
		{
			if (Settings.Instance != null && !string.IsNullOrWhiteSpace(Settings.Instance.DefaultFileName))
			{
				Slots[SlotIndex].Name = Settings.Instance.DefaultFileName;
			}
			else
			{
				Slots[SlotIndex].Name = Dialog.Clean("FILE_DEFAULT");
			}
			Slots[SlotIndex].AssistModeEnabled = false;
			Slots[SlotIndex].VariantModeEnabled = false;
		}
		SlotSelected = true;
		Slots[SlotIndex].Select(reset);
		for (int i = 0; i < Slots.Length; i++)
		{
			if (SlotIndex != i)
			{
				Slots[i].Hide(0, (i >= SlotIndex) ? 1 : (-1));
			}
		}
	}

	public override void Update()
	{
		base.Update();
		if (!Focused)
		{
			return;
		}
		if (!SlotSelected)
		{
			if (Input.MenuUp.Pressed && SlotIndex > 0)
			{
				Audio.Play("event:/ui/main/savefile_rollover_up");
				SlotIndex--;
			}
			else if (Input.MenuDown.Pressed && SlotIndex < Slots.Length - 1)
			{
				Audio.Play("event:/ui/main/savefile_rollover_down");
				SlotIndex++;
			}
			else if (Input.MenuConfirm.Pressed)
			{
				Audio.Play("event:/ui/main/button_select");
				Audio.Play("event:/ui/main/whoosh_savefile_out");
				SelectSlot(reset: true);
			}
			else if (Input.MenuCancel.Pressed)
			{
				Audio.Play("event:/ui/main/button_back");
				base.Overworld.Goto<OuiMainMenu>();
			}
		}
		else if (Input.MenuCancel.Pressed && !HasSlots && !Slots[SlotIndex].StartingGame)
		{
			Audio.Play("event:/ui/main/button_back");
			base.Overworld.Goto<OuiMainMenu>();
		}
	}
}
