using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class OuiFileNaming : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__49 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiFileNaming _003C_003E4__this;

		private Vector2 _003CposFrom_003E5__2;

		private Vector2 _003CposTo_003E5__3;

		private float _003Ct_003E5__4;

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
		public _003CEnter_003Ed__49(int _003C_003E1__state)
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
			OuiFileNaming ouiFileNaming = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (ouiFileNaming.Name == Dialog.Clean("FILE_DEFAULT") || (Settings.Instance != null && ouiFileNaming.Name == Settings.Instance.DefaultFileName))
				{
					ouiFileNaming.Name = "";
				}
				ouiFileNaming.Overworld.ShowInputUI = false;
				ouiFileNaming.selectingOptions = false;
				ouiFileNaming.optionsIndex = 0;
				ouiFileNaming.index = 0;
				ouiFileNaming.line = 0;
				ouiFileNaming.ReloadLetters(Dialog.Clean("name_letters"));
				ouiFileNaming.optionsScale = 0.75f;
				ouiFileNaming.cancel = Dialog.Clean("name_back");
				ouiFileNaming.space = Dialog.Clean("name_space");
				ouiFileNaming.backspace = Dialog.Clean("name_backspace");
				ouiFileNaming.accept = Dialog.Clean("name_accept");
				ouiFileNaming.cancelWidth = ActiveFont.Measure(ouiFileNaming.cancel).X * ouiFileNaming.optionsScale;
				ouiFileNaming.spaceWidth = ActiveFont.Measure(ouiFileNaming.space).X * ouiFileNaming.optionsScale;
				ouiFileNaming.backspaceWidth = ActiveFont.Measure(ouiFileNaming.backspace).X * ouiFileNaming.optionsScale;
				ouiFileNaming.beginWidth = ActiveFont.Measure(ouiFileNaming.accept).X * ouiFileNaming.optionsScale * 1.25f;
				ouiFileNaming.optionsWidth = ouiFileNaming.cancelWidth + ouiFileNaming.spaceWidth + ouiFileNaming.backspaceWidth + ouiFileNaming.beginWidth + ouiFileNaming.widestLetter * 3f;
				ouiFileNaming.Visible = true;
				_003CposFrom_003E5__2 = ouiFileNaming.Position;
				_003CposTo_003E5__3 = Vector2.Zero;
				_003Ct_003E5__4 = 0f;
				goto IL_0245;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 += Engine.DeltaTime * 3f;
				goto IL_0245;
			case 2:
				_003C_003E1__state = -1;
				ouiFileNaming.Focused = true;
				_003C_003E2__current = 0.05f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					ouiFileNaming.wiggler.Start();
					return false;
				}
				IL_0245:
				if (_003Ct_003E5__4 < 1f)
				{
					ouiFileNaming.ease = Ease.CubeIn(_003Ct_003E5__4);
					ouiFileNaming.Position = _003CposFrom_003E5__2 + (_003CposTo_003E5__3 - _003CposFrom_003E5__2) * Ease.CubeInOut(_003Ct_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				ouiFileNaming.ease = 1f;
				_003CposFrom_003E5__2 = default(Vector2);
				_003CposTo_003E5__3 = default(Vector2);
				_003C_003E2__current = 0.05f;
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

	[CompilerGenerated]
	private sealed class _003CLeave_003Ed__51 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiFileNaming _003C_003E4__this;

		private Vector2 _003CposFrom_003E5__2;

		private Vector2 _003CposTo_003E5__3;

		private float _003Ct_003E5__4;

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
		public _003CLeave_003Ed__51(int _003C_003E1__state)
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
			OuiFileNaming ouiFileNaming = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiFileNaming.Overworld.ShowInputUI = true;
				ouiFileNaming.Focused = false;
				_003CposFrom_003E5__2 = ouiFileNaming.Position;
				_003CposTo_003E5__3 = new Vector2(0f, 1080f);
				_003Ct_003E5__4 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 += Engine.DeltaTime * 2f;
				break;
			}
			if (_003Ct_003E5__4 < 1f)
			{
				ouiFileNaming.ease = 1f - Ease.CubeIn(_003Ct_003E5__4);
				ouiFileNaming.Position = _003CposFrom_003E5__2 + (_003CposTo_003E5__3 - _003CposFrom_003E5__2) * Ease.CubeInOut(_003Ct_003E5__4);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiFileNaming.Visible = false;
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

	public string StartingName;

	public OuiFileSelectSlot FileSlot;

	public const int MinNameLength = 1;

	public const int MaxNameLengthNormal = 12;

	public const int MaxNameLengthJP = 8;

	private string[] letters;

	private int index;

	private int line;

	private float widestLetter;

	private float widestLine;

	private int widestLineCount;

	private bool selectingOptions = true;

	private int optionsIndex;

	private bool hiragana = true;

	private float lineHeight;

	private float lineSpacing;

	private float boxPadding;

	private float optionsScale;

	private string cancel;

	private string space;

	private string backspace;

	private string accept;

	private float cancelWidth;

	private float spaceWidth;

	private float backspaceWidth;

	private float beginWidth;

	private float optionsWidth;

	private float boxWidth;

	private float boxHeight;

	private float pressedTimer;

	private float timer;

	private float ease;

	private Wiggler wiggler;

	private static int[] dakuten_able = new int[40]
	{
		12363, 12365, 12367, 12369, 12371, 12373, 12375, 12377, 12379, 12381,
		12383, 12385, 12388, 12390, 12392, 12399, 12402, 12405, 12408, 12411,
		12459, 12461, 12463, 12465, 12467, 12469, 12471, 12473, 12475, 12477,
		12479, 12481, 12484, 12486, 12488, 12495, 12498, 12501, 12504, 12507
	};

	private static int[] handakuten_able = new int[10] { 12400, 12403, 12406, 12409, 12412, 12496, 12499, 12502, 12505, 12508 };

	private Color unselectColor = Color.LightGray;

	private Color selectColorA = Calc.HexToColor("84FF54");

	private Color selectColorB = Calc.HexToColor("FCFF59");

	private Color disableColor = Color.DarkSlateBlue;

	public string Name
	{
		get
		{
			return FileSlot.Name;
		}
		set
		{
			FileSlot.Name = value;
		}
	}

	public int MaxNameLength
	{
		get
		{
			if (!Japanese)
			{
				return 12;
			}
			return 8;
		}
	}

	public bool Japanese => Settings.Instance.Language == "japanese";

	private Vector2 boxtopleft => Position + new Vector2((1920f - boxWidth) / 2f, 360f + (680f - boxHeight) / 2f);

	public OuiFileNaming()
	{
		wiggler = Wiggler.Create(0.25f, 4f);
		Position = new Vector2(0f, 1080f);
		Visible = false;
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__49))]
	public override IEnumerator Enter(Oui from)
	{
		if (Name == Dialog.Clean("FILE_DEFAULT") || (Settings.Instance != null && Name == Settings.Instance.DefaultFileName))
		{
			Name = "";
		}
		base.Overworld.ShowInputUI = false;
		selectingOptions = false;
		optionsIndex = 0;
		index = 0;
		line = 0;
		ReloadLetters(Dialog.Clean("name_letters"));
		optionsScale = 0.75f;
		cancel = Dialog.Clean("name_back");
		space = Dialog.Clean("name_space");
		backspace = Dialog.Clean("name_backspace");
		accept = Dialog.Clean("name_accept");
		cancelWidth = ActiveFont.Measure(cancel).X * optionsScale;
		spaceWidth = ActiveFont.Measure(space).X * optionsScale;
		backspaceWidth = ActiveFont.Measure(backspace).X * optionsScale;
		beginWidth = ActiveFont.Measure(accept).X * optionsScale * 1.25f;
		optionsWidth = cancelWidth + spaceWidth + backspaceWidth + beginWidth + widestLetter * 3f;
		Visible = true;
		Vector2 posFrom = Position;
		Vector2 posTo = Vector2.Zero;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 3f)
		{
			ease = Ease.CubeIn(t);
			Position = posFrom + (posTo - posFrom) * Ease.CubeInOut(t);
			yield return null;
		}
		ease = 1f;
		yield return 0.05f;
		Focused = true;
		yield return 0.05f;
		wiggler.Start();
	}

	private void ReloadLetters(string chars)
	{
		letters = chars.Split('\n');
		widestLetter = 0f;
		for (int i = 0; i < chars.Length; i++)
		{
			float x = ActiveFont.Measure(chars[i]).X;
			if (x > widestLetter)
			{
				widestLetter = x;
			}
		}
		if (Japanese)
		{
			widestLetter *= 1.5f;
		}
		widestLineCount = 0;
		string[] array = letters;
		foreach (string text in array)
		{
			if (text.Length > widestLineCount)
			{
				widestLineCount = text.Length;
			}
		}
		widestLine = (float)widestLineCount * widestLetter;
		lineHeight = ActiveFont.LineHeight;
		lineSpacing = ActiveFont.LineHeight * 0.1f;
		boxPadding = widestLetter;
		boxWidth = Math.Max(widestLine, optionsWidth) + boxPadding * 2f;
		boxHeight = (float)(letters.Length + 1) * lineHeight + (float)letters.Length * lineSpacing + boxPadding * 3f;
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__51))]
	public override IEnumerator Leave(Oui next)
	{
		base.Overworld.ShowInputUI = true;
		Focused = false;
		Vector2 posFrom = Position;
		Vector2 posTo = new Vector2(0f, 1080f);
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 2f)
		{
			ease = 1f - Ease.CubeIn(t);
			Position = posFrom + (posTo - posFrom) * Ease.CubeInOut(t);
			yield return null;
		}
		Visible = false;
	}

	public override void Update()
	{
		base.Update();
		if (base.Selected && Focused)
		{
			if (!string.IsNullOrWhiteSpace(Name) && MInput.Keyboard.Check(Keys.LeftControl) && MInput.Keyboard.Pressed(Keys.S))
			{
				ResetDefaultName();
			}
			if (Input.MenuJournal.Pressed && Japanese)
			{
				SwapType();
			}
			if (Input.MenuRight.Pressed && (optionsIndex < 3 || !selectingOptions) && (Name.Length > 0 || !selectingOptions))
			{
				if (selectingOptions)
				{
					optionsIndex = Math.Min(optionsIndex + 1, 3);
				}
				else
				{
					do
					{
						index = (index + 1) % letters[line].Length;
					}
					while (letters[line][index] == ' ');
				}
				wiggler.Start();
				Audio.Play("event:/ui/main/rename_entry_rollover");
			}
			else if (Input.MenuLeft.Pressed && (optionsIndex > 0 || !selectingOptions))
			{
				if (selectingOptions)
				{
					optionsIndex = Math.Max(optionsIndex - 1, 0);
				}
				else
				{
					do
					{
						index = (index + letters[line].Length - 1) % letters[line].Length;
					}
					while (letters[line][index] == ' ');
				}
				wiggler.Start();
				Audio.Play("event:/ui/main/rename_entry_rollover");
			}
			else if (Input.MenuDown.Pressed && !selectingOptions)
			{
				int num = line + 1;
				while (true)
				{
					if (num >= letters.Length)
					{
						selectingOptions = true;
						break;
					}
					if (index < letters[num].Length && letters[num][index] != ' ')
					{
						line = num;
						break;
					}
					num++;
				}
				if (selectingOptions)
				{
					float num2 = (float)index * widestLetter;
					float num3 = boxWidth - boxPadding * 2f;
					if (Name.Length == 0 || num2 < cancelWidth + (num3 - cancelWidth - beginWidth - backspaceWidth - spaceWidth - widestLetter * 3f) / 2f)
					{
						optionsIndex = 0;
					}
					else if (num2 < num3 - beginWidth - backspaceWidth - widestLetter * 2f)
					{
						optionsIndex = 1;
					}
					else if (num2 < num3 - beginWidth - widestLetter)
					{
						optionsIndex = 2;
					}
					else
					{
						optionsIndex = 3;
					}
				}
				wiggler.Start();
				Audio.Play("event:/ui/main/rename_entry_rollover");
			}
			else if ((Input.MenuUp.Pressed || (selectingOptions && Name.Length <= 0 && optionsIndex > 0)) && (line > 0 || selectingOptions))
			{
				if (selectingOptions)
				{
					line = letters.Length;
					selectingOptions = false;
					float num4 = boxWidth - boxPadding * 2f;
					if (optionsIndex == 0)
					{
						index = (int)(cancelWidth / 2f / widestLetter);
					}
					else if (optionsIndex == 1)
					{
						index = (int)((num4 - beginWidth - backspaceWidth - spaceWidth / 2f - widestLetter * 2f) / widestLetter);
					}
					else if (optionsIndex == 2)
					{
						index = (int)((num4 - beginWidth - backspaceWidth / 2f - widestLetter) / widestLetter);
					}
					else if (optionsIndex == 3)
					{
						index = (int)((num4 - beginWidth / 2f) / widestLetter);
					}
				}
				line--;
				while (line > 0 && (index >= letters[line].Length || letters[line][index] == ' '))
				{
					line--;
				}
				while (index >= letters[line].Length || letters[line][index] == ' ')
				{
					index--;
				}
				wiggler.Start();
				Audio.Play("event:/ui/main/rename_entry_rollover");
			}
			else if (Input.MenuConfirm.Pressed)
			{
				if (selectingOptions)
				{
					if (optionsIndex == 0)
					{
						Cancel();
					}
					else if (optionsIndex == 1 && Name.Length > 0)
					{
						Space();
					}
					else if (optionsIndex == 2)
					{
						Backspace();
					}
					else if (optionsIndex == 3)
					{
						Finish();
					}
				}
				else if (Japanese && letters[line][index] == '\u309b' && Name.Length > 0 && dakuten_able.Contains(Name.Last()))
				{
					int num5 = Name[Name.Length - 1];
					num5++;
					Name = Name.Substring(0, Name.Length - 1);
					Name += (char)num5;
					wiggler.Start();
					Audio.Play("event:/ui/main/rename_entry_char");
				}
				else if (Japanese && letters[line][index] == '\u309c' && Name.Length > 0 && (handakuten_able.Contains(Name.Last()) || handakuten_able.Contains(Name.Last() + 1)))
				{
					int num6 = Name[Name.Length - 1];
					num6 = ((!handakuten_able.Contains(num6)) ? (num6 + 2) : (num6 + 1));
					Name = Name.Substring(0, Name.Length - 1);
					Name += (char)num6;
					wiggler.Start();
					Audio.Play("event:/ui/main/rename_entry_char");
				}
				else if (Name.Length < MaxNameLength)
				{
					Name += letters[line][index];
					wiggler.Start();
					Audio.Play("event:/ui/main/rename_entry_char");
				}
				else
				{
					Audio.Play("event:/ui/main/button_invalid");
				}
			}
			else if (Input.MenuCancel.Pressed)
			{
				if (Name.Length > 0)
				{
					Backspace();
				}
				else
				{
					Cancel();
				}
			}
			else if (Input.Pause.Pressed)
			{
				Finish();
			}
		}
		pressedTimer -= Engine.DeltaTime;
		timer += Engine.DeltaTime;
		wiggler.Update();
	}

	private void ResetDefaultName()
	{
		if (StartingName == Settings.Instance.DefaultFileName || StartingName == Dialog.Clean("FILE_DEFAULT"))
		{
			StartingName = Name;
		}
		Settings.Instance.DefaultFileName = Name;
		Audio.Play("event:/new_content/ui/rename_entry_accept_locked");
	}

	private void Space()
	{
		if (Name.Length < MaxNameLength && Name.Length > 0)
		{
			Name += " ";
			wiggler.Start();
			Audio.Play("event:/ui/main/rename_entry_char");
		}
		else
		{
			Audio.Play("event:/ui/main/button_invalid");
		}
	}

	private void Backspace()
	{
		if (Name.Length > 0)
		{
			Name = Name.Substring(0, Name.Length - 1);
			Audio.Play("event:/ui/main/rename_entry_backspace");
		}
		else
		{
			Audio.Play("event:/ui/main/button_invalid");
		}
	}

	private void Finish()
	{
		if (Name.Length >= 1)
		{
			if (MInput.GamePads.Length != 0 && MInput.GamePads[0] != null && (MInput.GamePads[0].Check(Buttons.LeftTrigger) || MInput.GamePads[0].Check(Buttons.LeftShoulder)) && (MInput.GamePads[0].Check(Buttons.RightTrigger) || MInput.GamePads[0].Check(Buttons.RightShoulder)))
			{
				ResetDefaultName();
			}
			Focused = false;
			base.Overworld.Goto<OuiFileSelect>();
			Audio.Play("event:/ui/main/rename_entry_accept");
		}
		else
		{
			Audio.Play("event:/ui/main/button_invalid");
		}
	}

	private void SwapType()
	{
		hiragana = !hiragana;
		if (hiragana)
		{
			ReloadLetters(Dialog.Clean("name_letters"));
		}
		else
		{
			ReloadLetters(Dialog.Clean("name_letters_katakana"));
		}
	}

	private void Cancel()
	{
		FileSlot.Name = StartingName;
		Focused = false;
		base.Overworld.Goto<OuiFileSelect>();
		Audio.Play("event:/ui/main/button_back");
	}

	public override void Render()
	{
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * 0.8f * ease);
		Vector2 vector = boxtopleft + new Vector2(boxPadding, boxPadding);
		int num = 0;
		string[] array = letters;
		foreach (string text in array)
		{
			for (int j = 0; j < text.Length; j++)
			{
				bool flag = num == line && j == index && !selectingOptions;
				Vector2 scale = Vector2.One * (flag ? 1.2f : 1f);
				Vector2 at = vector + new Vector2(widestLetter, lineHeight) / 2f;
				if (flag)
				{
					at += new Vector2(0f, wiggler.Value) * 8f;
				}
				DrawOptionText(text[j].ToString(), at, new Vector2(0.5f, 0.5f), scale, flag);
				vector.X += widestLetter;
			}
			vector.X = boxtopleft.X + boxPadding;
			vector.Y += lineHeight + lineSpacing;
			num++;
		}
		float num2 = wiggler.Value * 8f;
		vector.Y = boxtopleft.Y + boxHeight - lineHeight - boxPadding;
		Draw.Rect(vector.X, vector.Y - boxPadding * 0.5f, boxWidth - boxPadding * 2f, 4f, Color.White);
		DrawOptionText(cancel, vector + new Vector2(0f, lineHeight + ((selectingOptions && optionsIndex == 0) ? num2 : 0f)), new Vector2(0f, 1f), Vector2.One * optionsScale, selectingOptions && optionsIndex == 0);
		vector.X = boxtopleft.X + boxWidth - backspaceWidth - widestLetter - spaceWidth - widestLetter - beginWidth - boxPadding;
		DrawOptionText(space, vector + new Vector2(0f, lineHeight + ((selectingOptions && optionsIndex == 1) ? num2 : 0f)), new Vector2(0f, 1f), Vector2.One * optionsScale, selectingOptions && optionsIndex == 1, Name.Length == 0 || !Focused);
		vector.X += spaceWidth + widestLetter;
		DrawOptionText(backspace, vector + new Vector2(0f, lineHeight + ((selectingOptions && optionsIndex == 2) ? num2 : 0f)), new Vector2(0f, 1f), Vector2.One * optionsScale, selectingOptions && optionsIndex == 2, Name.Length <= 0 || !Focused);
		vector.X += backspaceWidth + widestLetter;
		DrawOptionText(accept, vector + new Vector2(0f, lineHeight + ((selectingOptions && optionsIndex == 3) ? num2 : 0f)), new Vector2(0f, 1f), Vector2.One * optionsScale * 1.25f, selectingOptions && optionsIndex == 3, Name.Length < 1 || !Focused);
		if (Japanese)
		{
			float num3 = 1f;
			string text2 = Dialog.Clean(hiragana ? "NAME_LETTERS_SWAP_KATAKANA" : "NAME_LETTERS_SWAP_HIRAGANA");
			MTexture mTexture = Input.GuiButton(Input.MenuJournal);
			ActiveFont.Measure(text2);
			float num4 = (float)mTexture.Width * num3;
			Vector2 vector2 = new Vector2(70f, 1144f - 154f * ease);
			mTexture.DrawJustified(vector2, new Vector2(0f, 0.5f), Color.White, num3, 0f);
			ActiveFont.DrawOutline(text2, vector2 + new Vector2(16f + num4, 0f), new Vector2(0f, 0.5f), Vector2.One * num3, Color.White, 2f, Color.Black);
		}
	}

	private void DrawOptionText(string text, Vector2 at, Vector2 justify, Vector2 scale, bool selected, bool disabled = false)
	{
		bool num = selected && pressedTimer > 0f;
		Color color = (disabled ? disableColor : GetTextColor(selected));
		Color edgeColor = (disabled ? Color.Lerp(disableColor, Color.Black, 0.7f) : Color.Gray);
		if (num)
		{
			ActiveFont.Draw(text, at + Vector2.UnitY, justify, scale, color);
		}
		else
		{
			ActiveFont.DrawEdgeOutline(text, at, justify, scale, color, 4f, edgeColor);
		}
	}

	private Color GetTextColor(bool selected)
	{
		if (selected)
		{
			if (Settings.Instance.DisableFlashes)
			{
				return selectColorA;
			}
			if (!Calc.BetweenInterval(timer, 0.1f))
			{
				return selectColorB;
			}
			return selectColorA;
		}
		return unselectColor;
	}
}
