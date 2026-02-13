using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class ButtonConfigUI : TextMenu
{
	private bool remapping;

	private float remappingEase;

	private Binding remappingBinding;

	private string remappingText;

	private float inputDelay;

	private float timeout;

	private bool closing;

	private float closingDelay;

	private bool waitingForController;

	private bool resetHeld;

	private float resetTime;

	private float resetDelay;

	private List<Buttons> all = new List<Buttons>
	{
		Buttons.A,
		Buttons.B,
		Buttons.X,
		Buttons.Y,
		Buttons.LeftShoulder,
		Buttons.RightShoulder,
		Buttons.LeftTrigger,
		Buttons.RightTrigger
	};

	public static readonly string StadiaControllerDisclaimer = "No endorsement or affiliation is intended between Stadia and the manufacturers\nof non-Stadia controllers or consoles. STADIA, the Stadia beacon, Google, and related\nmarks and logos are trademarks of Google LLC. All other trademarks are the\nproperty of their respective owners.";

	public ButtonConfigUI()
	{
		Add(new Header(Dialog.Clean("BTN_CONFIG_TITLE")));
		Add(new InputMappingInfo(controllerMode: true));
		Add(new SubHeader(Dialog.Clean("KEY_CONFIG_GAMEPLAY")));
		AddMap("LEFT", Settings.Instance.Left);
		AddMap("RIGHT", Settings.Instance.Right);
		AddMap("UP", Settings.Instance.Up);
		AddMap("DOWN", Settings.Instance.Down);
		AddMap("JUMP", Settings.Instance.Jump);
		AddMap("DASH", Settings.Instance.Dash);
		AddMap("GRAB", Settings.Instance.Grab);
		AddMap("TALK", Settings.Instance.Talk);
		Add(new SubHeader(Dialog.Clean("KEY_CONFIG_MENUS")));
		Add(new SubHeader(Dialog.Clean("KEY_CONFIG_MENU_NOTICE"), topPadding: false));
		AddMap("LEFT", Settings.Instance.MenuLeft);
		AddMap("RIGHT", Settings.Instance.MenuRight);
		AddMap("UP", Settings.Instance.MenuUp);
		AddMap("DOWN", Settings.Instance.MenuDown);
		AddMap("CONFIRM", Settings.Instance.Confirm);
		AddMap("CANCEL", Settings.Instance.Cancel);
		AddMap("JOURNAL", Settings.Instance.Journal);
		AddMap("PAUSE", Settings.Instance.Pause);
		Add(new SubHeader(""));
		Add(new Button(Dialog.Clean("KEY_CONFIG_RESET"))
		{
			IncludeWidthInMeasurement = false,
			AlwaysCenter = true,
			OnPressed = delegate
			{
				resetHeld = true;
				resetTime = 0f;
				resetDelay = 0f;
			},
			ConfirmSfx = "event:/ui/main/button_lowkey"
		});
		Add(new SubHeader(Dialog.Clean("KEY_CONFIG_ADVANCED")));
		AddMap("QUICKRESTART", Settings.Instance.QuickRestart);
		AddMap("DEMO", Settings.Instance.DemoDash);
		Add(new SubHeader(Dialog.Clean("KEY_CONFIG_MOVE_ONLY")));
		AddMap("LEFT", Settings.Instance.LeftMoveOnly);
		AddMap("RIGHT", Settings.Instance.RightMoveOnly);
		AddMap("UP", Settings.Instance.UpMoveOnly);
		AddMap("DOWN", Settings.Instance.DownMoveOnly);
		Add(new SubHeader(Dialog.Clean("KEY_CONFIG_DASH_ONLY")));
		AddMap("LEFT", Settings.Instance.LeftDashOnly);
		AddMap("RIGHT", Settings.Instance.RightDashOnly);
		AddMap("UP", Settings.Instance.UpDashOnly);
		AddMap("DOWN", Settings.Instance.DownDashOnly);
		OnESC = (OnCancel = delegate
		{
			MenuOptions.UpdateCrouchDashModeVisibility();
			Focused = false;
			closing = true;
		});
		MinWidth = 600f;
		Position.Y = base.ScrollTargetY;
		Alpha = 0f;
	}

	private void AddMap(string label, Binding binding)
	{
		string txt = Dialog.Clean("KEY_CONFIG_" + label);
		Add(new Setting(txt, binding, controllerMode: true).Pressed(delegate
		{
			remappingText = txt;
			Remap(binding);
		}).AltPressed(delegate
		{
			Clear(binding);
		}));
	}

	private void Remap(Binding binding)
	{
		if (Input.GuiInputController())
		{
			remapping = true;
			remappingBinding = binding;
			timeout = 5f;
			Focused = false;
		}
	}

	private void AddRemap(Buttons btn)
	{
		while (remappingBinding.Controller.Count >= Input.MaxBindings)
		{
			remappingBinding.Controller.RemoveAt(0);
		}
		remapping = false;
		inputDelay = 0.25f;
		if (!remappingBinding.Add(btn))
		{
			Audio.Play("event:/ui/main/button_invalid");
		}
		Input.Initialize();
	}

	private void Clear(Binding binding)
	{
		if (!binding.ClearGamepad())
		{
			Audio.Play("event:/ui/main/button_invalid");
		}
	}

	public override void Update()
	{
		if (resetHeld)
		{
			resetDelay += Engine.DeltaTime;
			resetTime += Engine.DeltaTime;
			if (resetTime > 1.5f)
			{
				resetDelay = 0f;
				resetHeld = false;
				Settings.Instance.SetDefaultButtonControls(reset: true);
				Input.Initialize();
				Audio.Play("event:/ui/main/button_select");
			}
			if (!Input.MenuConfirm.Check && resetDelay > 0.3f)
			{
				Audio.Play("event:/ui/main/button_invalid");
				resetHeld = false;
			}
			if (resetHeld)
			{
				return;
			}
		}
		base.Update();
		Focused = !closing && inputDelay <= 0f && !waitingForController && !remapping;
		if (!closing)
		{
			if (!MInput.GamePads[Input.Gamepad].Attached)
			{
				waitingForController = true;
			}
			else if (waitingForController)
			{
				waitingForController = false;
			}
			if (Input.MenuCancel.Pressed && !remapping)
			{
				OnCancel();
			}
		}
		if (inputDelay > 0f && !remapping)
		{
			inputDelay -= Engine.RawDeltaTime;
		}
		remappingEase = Calc.Approach(remappingEase, remapping ? 1 : 0, Engine.RawDeltaTime * 4f);
		if (remappingEase >= 0.25f && remapping)
		{
			if (Input.ESC.Pressed || timeout <= 0f || !Input.GuiInputController())
			{
				remapping = false;
				Focused = true;
			}
			else
			{
				MInput.GamePadData gamePadData = MInput.GamePads[Input.Gamepad];
				float num = 0.25f;
				if (gamePadData.LeftStickLeftPressed(num))
				{
					AddRemap(Buttons.LeftThumbstickLeft);
				}
				else if (gamePadData.LeftStickRightPressed(num))
				{
					AddRemap(Buttons.LeftThumbstickRight);
				}
				else if (gamePadData.LeftStickUpPressed(num))
				{
					AddRemap(Buttons.LeftThumbstickUp);
				}
				else if (gamePadData.LeftStickDownPressed(num))
				{
					AddRemap(Buttons.LeftThumbstickDown);
				}
				else if (gamePadData.RightStickLeftPressed(num))
				{
					AddRemap(Buttons.RightThumbstickLeft);
				}
				else if (gamePadData.RightStickRightPressed(num))
				{
					AddRemap(Buttons.RightThumbstickRight);
				}
				else if (gamePadData.RightStickDownPressed(num))
				{
					AddRemap(Buttons.RightThumbstickDown);
				}
				else if (gamePadData.RightStickUpPressed(num))
				{
					AddRemap(Buttons.RightThumbstickUp);
				}
				else if (gamePadData.LeftTriggerPressed(num))
				{
					AddRemap(Buttons.LeftTrigger);
				}
				else if (gamePadData.RightTriggerPressed(num))
				{
					AddRemap(Buttons.RightTrigger);
				}
				else if (gamePadData.Pressed(Buttons.DPadLeft))
				{
					AddRemap(Buttons.DPadLeft);
				}
				else if (gamePadData.Pressed(Buttons.DPadRight))
				{
					AddRemap(Buttons.DPadRight);
				}
				else if (gamePadData.Pressed(Buttons.DPadUp))
				{
					AddRemap(Buttons.DPadUp);
				}
				else if (gamePadData.Pressed(Buttons.DPadDown))
				{
					AddRemap(Buttons.DPadDown);
				}
				else if (gamePadData.Pressed(Buttons.A))
				{
					AddRemap(Buttons.A);
				}
				else if (gamePadData.Pressed(Buttons.B))
				{
					AddRemap(Buttons.B);
				}
				else if (gamePadData.Pressed(Buttons.X))
				{
					AddRemap(Buttons.X);
				}
				else if (gamePadData.Pressed(Buttons.Y))
				{
					AddRemap(Buttons.Y);
				}
				else if (gamePadData.Pressed(Buttons.Start))
				{
					AddRemap(Buttons.Start);
				}
				else if (gamePadData.Pressed(Buttons.Back))
				{
					AddRemap(Buttons.Back);
				}
				else if (gamePadData.Pressed(Buttons.LeftShoulder))
				{
					AddRemap(Buttons.LeftShoulder);
				}
				else if (gamePadData.Pressed(Buttons.RightShoulder))
				{
					AddRemap(Buttons.RightShoulder);
				}
				else if (gamePadData.Pressed(Buttons.LeftStick))
				{
					AddRemap(Buttons.LeftStick);
				}
				else if (gamePadData.Pressed(Buttons.RightStick))
				{
					AddRemap(Buttons.RightStick);
				}
			}
			timeout -= Engine.RawDeltaTime;
		}
		closingDelay -= Engine.RawDeltaTime;
		Alpha = Calc.Approach(Alpha, (!closing || !(closingDelay <= 0f)) ? 1 : 0, Engine.RawDeltaTime * 8f);
		if (closing && Alpha <= 0f)
		{
			Close();
		}
	}

	public override void Render()
	{
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeOut(Alpha));
		Vector2 vector = new Vector2(1920f, 1080f) * 0.5f;
		if (MInput.GamePads[Input.Gamepad].Attached)
		{
			base.Render();
			if (remappingEase > 0f)
			{
				Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * 0.95f * Ease.CubeInOut(remappingEase));
				ActiveFont.Draw(Dialog.Get("BTN_CONFIG_CHANGING"), vector + new Vector2(0f, -8f), new Vector2(0.5f, 1f), Vector2.One * 0.7f, Color.LightGray * Ease.CubeIn(remappingEase));
				ActiveFont.Draw(remappingText, vector + new Vector2(0f, 8f), new Vector2(0.5f, 0f), Vector2.One * 2f, Color.White * Ease.CubeIn(remappingEase));
			}
		}
		else
		{
			ActiveFont.Draw(Dialog.Clean("BTN_CONFIG_NOCONTROLLER"), vector, new Vector2(0.5f, 0.5f), Vector2.One, Color.White * Ease.CubeOut(Alpha));
		}
		if (resetHeld)
		{
			float num = Ease.CubeInOut(Calc.Min(1f, resetDelay / 0.2f));
			float num2 = Ease.SineOut(Calc.Min(1f, resetTime / 1.5f));
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * 0.95f * num);
			float num3 = 480f;
			float x = (1920f - num3) / 2f;
			Draw.Rect(x, 530f, num3, 20f, Color.White * 0.25f * num);
			Draw.Rect(x, 530f, num3 * num2, 20f, Color.White * num);
		}
	}
}
