using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Celeste.Pico8;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiMainMenu : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Oui from;

		public OuiMainMenu _003C_003E4__this;

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
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			OuiMainMenu ouiMainMenu = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (from is OuiTitleScreen || from is OuiFileSelect)
				{
					Audio.Play("event:/ui/main/whoosh_list_in");
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0072;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0072;
			case 2:
				_003C_003E1__state = -1;
				ouiMainMenu.Focused = true;
				ouiMainMenu.mountainStartFront = false;
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0072:
				if (from is OuiTitleScreen)
				{
					MenuButton.ClearSelection(ouiMainMenu.Scene);
					ouiMainMenu.climbButton.StartSelected();
				}
				ouiMainMenu.Visible = true;
				if (ouiMainMenu.mountainStartFront)
				{
					ouiMainMenu.Overworld.Mountain.SnapCamera(-1, new MountainCamera(new Vector3(0f, 6f, 12f), MountainRenderer.RotateLookAt));
				}
				ouiMainMenu.Overworld.Mountain.GotoRotationMode();
				ouiMainMenu.Overworld.Maddy.Hide();
				foreach (MenuButton button in ouiMainMenu.buttons)
				{
					button.TweenIn(0.2f);
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

	[CompilerGenerated]
	private sealed class _003CLeave_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiMainMenu _003C_003E4__this;

		public Oui next;

		private bool _003CkeepClimb_003E5__2;

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
			OuiMainMenu CS_0024_003C_003E8__locals10 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals10.Focused = false;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.2f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals10.ease = 1f - t.Eased;
					CS_0024_003C_003E8__locals10.Position = Vector2.Lerp(TargetPosition, TweenFrom, t.Eased);
				};
				CS_0024_003C_003E8__locals10.Add(tween);
				_003CkeepClimb_003E5__2 = CS_0024_003C_003E8__locals10.climbButton.Selected && !(next is OuiTitleScreen);
				foreach (MenuButton button in CS_0024_003C_003E8__locals10.buttons)
				{
					if (!((button == CS_0024_003C_003E8__locals10.climbButton) & _003CkeepClimb_003E5__2))
					{
						button.TweenOut(0.2f);
					}
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				if (_003CkeepClimb_003E5__2)
				{
					CS_0024_003C_003E8__locals10.Add(new Coroutine(CS_0024_003C_003E8__locals10.SlideClimbOutLate()));
				}
				else
				{
					CS_0024_003C_003E8__locals10.Visible = false;
				}
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

	[CompilerGenerated]
	private sealed class _003CSlideClimbOutLate_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiMainMenu _003C_003E4__this;

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
		public _003CSlideClimbOutLate_003Ed__19(int _003C_003E1__state)
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
			OuiMainMenu ouiMainMenu = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				ouiMainMenu.climbButton.TweenOut(0.2f);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				ouiMainMenu.Visible = false;
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

	private static readonly Vector2 TargetPosition = new Vector2(160f, 160f);

	private static readonly Vector2 TweenFrom = new Vector2(-500f, 160f);

	private static readonly Color UnselectedColor = Color.White;

	private static readonly Color SelectedColorA = TextMenu.HighlightColorA;

	private static readonly Color SelectedColorB = TextMenu.HighlightColorB;

	private const float IconWidth = 64f;

	private const float IconSpacing = 20f;

	private float ease;

	private MainMenuClimb climbButton;

	private List<MenuButton> buttons;

	private bool startOnOptions;

	private bool mountainStartFront;

	public Color SelectionColor
	{
		get
		{
			if (!Settings.Instance.DisableFlashes && !base.Scene.BetweenInterval(0.1f))
			{
				return SelectedColorB;
			}
			return SelectedColorA;
		}
	}

	public OuiMainMenu()
	{
		buttons = new List<MenuButton>();
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Position = TweenFrom;
		CreateButtons();
	}

	public void CreateButtons()
	{
		foreach (MenuButton button in buttons)
		{
			button.RemoveSelf();
		}
		buttons.Clear();
		Vector2 vector = new Vector2(320f, 160f);
		Vector2 vector2 = new Vector2(-640f, 0f);
		climbButton = new MainMenuClimb(this, vector, vector + vector2, OnBegin);
		if (!startOnOptions)
		{
			climbButton.StartSelected();
		}
		buttons.Add(climbButton);
		vector += Vector2.UnitY * climbButton.ButtonHeight;
		vector.X -= 140f;
		if (Celeste.PlayMode == Celeste.PlayModes.Debug)
		{
			MainMenuSmallButton mainMenuSmallButton = new MainMenuSmallButton("menu_debug", "menu/options", this, vector, vector + vector2, OnDebug);
			buttons.Add(mainMenuSmallButton);
			vector += Vector2.UnitY * mainMenuSmallButton.ButtonHeight;
		}
		if (Settings.Instance.Pico8OnMainMenu || Celeste.PlayMode == Celeste.PlayModes.Debug || Celeste.PlayMode == Celeste.PlayModes.Event)
		{
			MainMenuSmallButton mainMenuSmallButton2 = new MainMenuSmallButton("menu_pico8", "menu/pico8", this, vector, vector + vector2, OnPico8);
			buttons.Add(mainMenuSmallButton2);
			vector += Vector2.UnitY * mainMenuSmallButton2.ButtonHeight;
		}
		MainMenuSmallButton mainMenuSmallButton3 = new MainMenuSmallButton("menu_options", "menu/options", this, vector, vector + vector2, OnOptions);
		if (startOnOptions)
		{
			mainMenuSmallButton3.StartSelected();
		}
		buttons.Add(mainMenuSmallButton3);
		vector += Vector2.UnitY * mainMenuSmallButton3.ButtonHeight;
		MainMenuSmallButton mainMenuSmallButton4 = new MainMenuSmallButton("menu_credits", "menu/credits", this, vector, vector + vector2, OnCredits);
		buttons.Add(mainMenuSmallButton4);
		vector += Vector2.UnitY * mainMenuSmallButton4.ButtonHeight;
		MainMenuSmallButton mainMenuSmallButton5 = new MainMenuSmallButton("menu_exit", "menu/exit", this, vector, vector + vector2, OnExit);
		buttons.Add(mainMenuSmallButton5);
		vector += Vector2.UnitY * mainMenuSmallButton5.ButtonHeight;
		for (int i = 0; i < buttons.Count; i++)
		{
			if (i > 0)
			{
				buttons[i].UpButton = buttons[i - 1];
			}
			if (i < buttons.Count - 1)
			{
				buttons[i].DownButton = buttons[i + 1];
			}
			base.Scene.Add(buttons[i]);
		}
		if (!Visible || !Focused)
		{
			return;
		}
		foreach (MenuButton button2 in buttons)
		{
			button2.Position = button2.TargetPosition;
		}
	}

	public override void Removed(Scene scene)
	{
		foreach (MenuButton button in buttons)
		{
			scene.Remove(button);
		}
		base.Removed(scene);
	}

	public override bool IsStart(Overworld overworld, Overworld.StartMode start)
	{
		switch (start)
		{
		case Overworld.StartMode.ReturnFromOptions:
			startOnOptions = true;
			Add(new Coroutine(Enter(null)));
			return true;
		case Overworld.StartMode.MainMenu:
			mountainStartFront = true;
			Add(new Coroutine(Enter(null)));
			return true;
		default:
			if (start != Overworld.StartMode.ReturnFromOptions)
			{
				return start == Overworld.StartMode.ReturnFromPico8;
			}
			return true;
		}
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__17))]
	public override IEnumerator Enter(Oui from)
	{
		if (from is OuiTitleScreen || from is OuiFileSelect)
		{
			Audio.Play("event:/ui/main/whoosh_list_in");
			yield return 0.1f;
		}
		if (from is OuiTitleScreen)
		{
			MenuButton.ClearSelection(base.Scene);
			climbButton.StartSelected();
		}
		Visible = true;
		if (mountainStartFront)
		{
			base.Overworld.Mountain.SnapCamera(-1, new MountainCamera(new Vector3(0f, 6f, 12f), MountainRenderer.RotateLookAt));
		}
		base.Overworld.Mountain.GotoRotationMode();
		base.Overworld.Maddy.Hide();
		foreach (MenuButton button in buttons)
		{
			button.TweenIn(0.2f);
		}
		yield return 0.2f;
		Focused = true;
		mountainStartFront = false;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__18))]
	public override IEnumerator Leave(Oui next)
	{
		Focused = false;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.2f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			ease = 1f - t.Eased;
			Position = Vector2.Lerp(TargetPosition, TweenFrom, t.Eased);
		};
		Add(tween);
		bool keepClimb = climbButton.Selected && !(next is OuiTitleScreen);
		foreach (MenuButton button in buttons)
		{
			if (!(button == climbButton && keepClimb))
			{
				button.TweenOut(0.2f);
			}
		}
		yield return 0.2f;
		if (keepClimb)
		{
			Add(new Coroutine(SlideClimbOutLate()));
		}
		else
		{
			Visible = false;
		}
	}

	[IteratorStateMachine(typeof(_003CSlideClimbOutLate_003Ed__19))]
	private IEnumerator SlideClimbOutLate()
	{
		yield return 0.2f;
		climbButton.TweenOut(0.2f);
		yield return 0.2f;
		Visible = false;
	}

	public override void Update()
	{
		if (base.Selected && Focused && Input.MenuCancel.Pressed)
		{
			Focused = false;
			Audio.Play("event:/ui/main/whoosh_list_out");
			Audio.Play("event:/ui/main/button_back");
			base.Overworld.Goto<OuiTitleScreen>();
		}
		base.Update();
	}

	public override void Render()
	{
		foreach (MenuButton button in buttons)
		{
			if (button.Scene == base.Scene)
			{
				button.Render();
			}
		}
	}

	private void OnDebug()
	{
		Audio.Play("event:/ui/main/whoosh_list_out");
		Audio.Play("event:/ui/main/button_select");
		SaveData.InitializeDebugMode();
		base.Overworld.Goto<OuiChapterSelect>();
	}

	private void OnBegin()
	{
		Audio.Play("event:/ui/main/whoosh_list_out");
		Audio.Play("event:/ui/main/button_climb");
		if (Celeste.PlayMode == Celeste.PlayModes.Event)
		{
			SaveData.InitializeDebugMode(loadExisting: false);
			base.Overworld.Goto<OuiChapterSelect>();
		}
		else
		{
			base.Overworld.Goto<OuiFileSelect>();
		}
	}

	private void OnPico8()
	{
		Audio.Play("event:/ui/main/button_select");
		Focused = false;
		new FadeWipe(base.Scene, wipeIn: false, delegate
		{
			Focused = true;
			base.Overworld.EnteringPico8 = true;
			SaveData.Instance = null;
			SaveData.NoFileAssistChecks();
			Engine.Scene = new Emulator(base.Overworld);
		});
	}

	private void OnOptions()
	{
		Audio.Play("event:/ui/main/button_select");
		Audio.Play("event:/ui/main/whoosh_large_in");
		base.Overworld.Goto<OuiOptions>();
	}

	private void OnCredits()
	{
		Audio.Play("event:/ui/main/button_select");
		Audio.Play("event:/ui/main/whoosh_large_in");
		base.Overworld.Goto<OuiCredits>();
	}

	private void OnExit()
	{
		Audio.Play("event:/ui/main/button_select");
		Focused = false;
		new FadeWipe(base.Scene, wipeIn: false, delegate
		{
			Engine.Scene = new Scene();
			Engine.Instance.Exit();
		});
	}
}
