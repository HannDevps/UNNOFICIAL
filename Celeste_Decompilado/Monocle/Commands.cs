using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monocle;

public class Commands
{
	private struct CommandInfo
	{
		public Action<string[]> Action;

		public string Help;

		public string Usage;
	}

	private struct Line
	{
		public string Text;

		public Color Color;

		public Line(string text)
		{
			Text = text;
			Color = Color.White;
		}

		public Line(string text, Color color)
		{
			Text = text;
			Color = color;
		}
	}

	private const float UNDERSCORE_TIME = 0.5f;

	private const float REPEAT_DELAY = 0.5f;

	private const float REPEAT_EVERY = 1f / 30f;

	private const float OPACITY = 0.8f;

	public bool Enabled = true;

	public bool Open;

	private Dictionary<string, CommandInfo> commands;

	private List<string> sorted;

	private KeyboardState oldState;

	private KeyboardState currentState;

	private string currentText = "";

	private List<Line> drawCommands;

	private bool underscore;

	private float underscoreCounter;

	private List<string> commandHistory;

	private int seekIndex = -1;

	private int tabIndex = -1;

	private string tabSearch;

	private float repeatCounter;

	private Keys? repeatKey;

	private bool canOpen;

	public Action[] FunctionKeyActions { get; private set; }

	public Commands()
	{
		commandHistory = new List<string>();
		drawCommands = new List<Line>();
		commands = new Dictionary<string, CommandInfo>();
		sorted = new List<string>();
		FunctionKeyActions = new Action[12];
		BuildCommandsList();
	}

	public void Log(object obj, Color color)
	{
		string text = obj.ToString();
		if (text.Contains("\n"))
		{
			string[] array = text.Split('\n');
			foreach (string obj2 in array)
			{
				Log(obj2, color);
			}
			return;
		}
		int num = Engine.Instance.Window.ClientBounds.Width - 40;
		while (Draw.DefaultFont.MeasureString(text).X > (float)num)
		{
			int num2 = -1;
			for (int j = 0; j < text.Length; j++)
			{
				if (text[j] == ' ')
				{
					if (!(Draw.DefaultFont.MeasureString(text.Substring(0, j)).X <= (float)num))
					{
						break;
					}
					num2 = j;
				}
			}
			if (num2 == -1)
			{
				break;
			}
			drawCommands.Insert(0, new Line(text.Substring(0, num2), color));
			text = text.Substring(num2 + 1);
		}
		drawCommands.Insert(0, new Line(text, color));
		int num3 = (Engine.Instance.Window.ClientBounds.Height - 100) / 30;
		while (drawCommands.Count > num3)
		{
			drawCommands.RemoveAt(drawCommands.Count - 1);
		}
	}

	public void Log(object obj)
	{
		Log(obj, Color.White);
	}

	internal void UpdateClosed()
	{
		if (!canOpen)
		{
			canOpen = true;
		}
		else if (MInput.Keyboard.Pressed(Keys.OemTilde, Keys.Oem8))
		{
			Open = true;
			currentState = Keyboard.GetState();
		}
		for (int i = 0; i < FunctionKeyActions.Length; i++)
		{
			if (MInput.Keyboard.Pressed((Keys)(112 + i)))
			{
				ExecuteFunctionKeyAction(i);
			}
		}
	}

	internal void UpdateOpen()
	{
		oldState = currentState;
		currentState = Keyboard.GetState();
		underscoreCounter += Engine.DeltaTime;
		while (underscoreCounter >= 0.5f)
		{
			underscoreCounter -= 0.5f;
			underscore = !underscore;
		}
		if (repeatKey.HasValue)
		{
			if (currentState[repeatKey.Value] == KeyState.Down)
			{
				repeatCounter += Engine.DeltaTime;
				while (repeatCounter >= 0.5f)
				{
					HandleKey(repeatKey.Value);
					repeatCounter -= 1f / 30f;
				}
			}
			else
			{
				repeatKey = null;
			}
		}
		Keys[] pressedKeys = currentState.GetPressedKeys();
		foreach (Keys key in pressedKeys)
		{
			if (oldState[key] == KeyState.Up)
			{
				HandleKey(key);
				break;
			}
		}
	}

	private void HandleKey(Keys key)
	{
		if (key != Keys.Tab && key != Keys.LeftShift && key != Keys.RightShift && key != Keys.RightAlt && key != Keys.LeftAlt && key != Keys.RightControl && key != Keys.LeftControl)
		{
			tabIndex = -1;
		}
		if (key != Keys.OemTilde && key != Keys.Oem8 && key != Keys.Enter && repeatKey != key)
		{
			repeatKey = key;
			repeatCounter = 0f;
		}
		switch (key)
		{
		case Keys.D1:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "!";
			}
			else
			{
				currentText += "1";
			}
			return;
		case Keys.D2:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "@";
			}
			else
			{
				currentText += "2";
			}
			return;
		case Keys.D3:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "#";
			}
			else
			{
				currentText += "3";
			}
			return;
		case Keys.D4:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "$";
			}
			else
			{
				currentText += "4";
			}
			return;
		case Keys.D5:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "%";
			}
			else
			{
				currentText += "5";
			}
			return;
		case Keys.D6:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "^";
			}
			else
			{
				currentText += "6";
			}
			return;
		case Keys.D7:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "&";
			}
			else
			{
				currentText += "7";
			}
			return;
		case Keys.D8:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "*";
			}
			else
			{
				currentText += "8";
			}
			return;
		case Keys.D9:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "(";
			}
			else
			{
				currentText += "9";
			}
			return;
		case Keys.D0:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += ")";
			}
			else
			{
				currentText += "0";
			}
			return;
		case Keys.OemComma:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "<";
			}
			else
			{
				currentText += ",";
			}
			return;
		case Keys.OemPeriod:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += ">";
			}
			else
			{
				currentText += ".";
			}
			return;
		case Keys.OemQuestion:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "?";
			}
			else
			{
				currentText += "/";
			}
			return;
		case Keys.OemSemicolon:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += ":";
			}
			else
			{
				currentText += ";";
			}
			return;
		case Keys.OemQuotes:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "\"";
			}
			else
			{
				currentText += "'";
			}
			return;
		case Keys.OemBackslash:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "|";
			}
			else
			{
				currentText += "\\";
			}
			return;
		case Keys.OemOpenBrackets:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "{";
			}
			else
			{
				currentText += "[";
			}
			return;
		case Keys.OemCloseBrackets:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "}";
			}
			else
			{
				currentText += "]";
			}
			return;
		case Keys.OemMinus:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "_";
			}
			else
			{
				currentText += "-";
			}
			return;
		case Keys.OemPlus:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += "+";
			}
			else
			{
				currentText += "=";
			}
			return;
		case Keys.Space:
			currentText += " ";
			return;
		case Keys.Back:
			if (currentText.Length > 0)
			{
				currentText = currentText.Substring(0, currentText.Length - 1);
			}
			return;
		case Keys.Delete:
			currentText = "";
			return;
		case Keys.Up:
			if (seekIndex < commandHistory.Count - 1)
			{
				seekIndex++;
				currentText = string.Join(" ", commandHistory[seekIndex]);
			}
			return;
		case Keys.Down:
			if (seekIndex > -1)
			{
				seekIndex--;
				if (seekIndex == -1)
				{
					currentText = "";
					return;
				}
				currentText = string.Join(" ", commandHistory[seekIndex]);
			}
			return;
		case Keys.Tab:
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				if (tabIndex == -1)
				{
					tabSearch = currentText;
					FindLastTab();
				}
				else
				{
					tabIndex--;
					if (tabIndex < 0 || (tabSearch != "" && sorted[tabIndex].IndexOf(tabSearch) != 0))
					{
						FindLastTab();
					}
				}
			}
			else if (tabIndex == -1)
			{
				tabSearch = currentText;
				FindFirstTab();
			}
			else
			{
				tabIndex++;
				if (tabIndex >= sorted.Count || (tabSearch != "" && sorted[tabIndex].IndexOf(tabSearch) != 0))
				{
					FindFirstTab();
				}
			}
			if (tabIndex != -1)
			{
				currentText = sorted[tabIndex];
			}
			return;
		case Keys.F1:
		case Keys.F2:
		case Keys.F3:
		case Keys.F4:
		case Keys.F5:
		case Keys.F6:
		case Keys.F7:
		case Keys.F8:
		case Keys.F9:
		case Keys.F10:
		case Keys.F11:
		case Keys.F12:
			ExecuteFunctionKeyAction((int)(key - 112));
			return;
		case Keys.Enter:
			if (currentText.Length > 0)
			{
				EnterCommand();
			}
			return;
		case Keys.OemTilde:
		case Keys.Oem8:
			Open = (canOpen = false);
			return;
		}
		if (key.ToString().Length == 1)
		{
			if (currentState[Keys.LeftShift] == KeyState.Down || currentState[Keys.RightShift] == KeyState.Down)
			{
				currentText += key;
			}
			else
			{
				currentText += key.ToString().ToLower();
			}
		}
	}

	private void EnterCommand()
	{
		string[] array = currentText.Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
		if (commandHistory.Count == 0 || commandHistory[0] != currentText)
		{
			commandHistory.Insert(0, currentText);
		}
		drawCommands.Insert(0, new Line(currentText, Color.Aqua));
		currentText = "";
		seekIndex = -1;
		string[] array2 = new string[array.Length - 1];
		for (int i = 1; i < array.Length; i++)
		{
			array2[i - 1] = array[i];
		}
		ExecuteCommand(array[0].ToLower(), array2);
	}

	private void FindFirstTab()
	{
		for (int i = 0; i < sorted.Count; i++)
		{
			if (tabSearch == "" || sorted[i].IndexOf(tabSearch) == 0)
			{
				tabIndex = i;
				break;
			}
		}
	}

	private void FindLastTab()
	{
		for (int i = 0; i < sorted.Count; i++)
		{
			if (tabSearch == "" || sorted[i].IndexOf(tabSearch) == 0)
			{
				tabIndex = i;
			}
		}
	}

	internal void Render()
	{
		int viewWidth = Engine.ViewWidth;
		int viewHeight = Engine.ViewHeight;
		Draw.SpriteBatch.Begin();
		Draw.Rect(10f, viewHeight - 50, viewWidth - 20, 40f, Color.Black * 0.8f);
		if (underscore)
		{
			Draw.SpriteBatch.DrawString(Draw.DefaultFont, ">" + currentText + "_", new Vector2(20f, viewHeight - 42), Color.White);
		}
		else
		{
			Draw.SpriteBatch.DrawString(Draw.DefaultFont, ">" + currentText, new Vector2(20f, viewHeight - 42), Color.White);
		}
		if (drawCommands.Count > 0)
		{
			int num = 10 + 30 * drawCommands.Count;
			Draw.Rect(10f, viewHeight - num - 60, viewWidth - 20, num, Color.Black * 0.8f);
			for (int i = 0; i < drawCommands.Count; i++)
			{
				Draw.SpriteBatch.DrawString(Draw.DefaultFont, drawCommands[i].Text, new Vector2(20f, viewHeight - 92 - 30 * i), drawCommands[i].Color);
			}
		}
		Draw.SpriteBatch.End();
	}

	public void ExecuteCommand(string command, string[] args)
	{
		if (commands.ContainsKey(command))
		{
			commands[command].Action(args);
		}
		else
		{
			Log("Command '" + command + "' not found! Type 'help' for list of commands", Color.Yellow);
		}
	}

	public void ExecuteFunctionKeyAction(int num)
	{
		if (FunctionKeyActions[num] != null)
		{
			FunctionKeyActions[num]();
		}
	}

	private void BuildCommandsList()
	{
		Type[] types = Assembly.GetCallingAssembly().GetTypes();
		for (int i = 0; i < types.Length; i++)
		{
			MethodInfo[] methods = types[i].GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			foreach (MethodInfo method in methods)
			{
				ProcessMethod(method);
			}
		}
		types = Assembly.GetExecutingAssembly().GetTypes();
		for (int i = 0; i < types.Length; i++)
		{
			MethodInfo[] methods = types[i].GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			foreach (MethodInfo method2 in methods)
			{
				ProcessMethod(method2);
			}
		}
		foreach (KeyValuePair<string, CommandInfo> command in commands)
		{
			sorted.Add(command.Key);
		}
		sorted.Sort();
	}

	private void ProcessMethod(MethodInfo method)
	{
		Command command = null;
		object[] customAttributes = method.GetCustomAttributes(typeof(Command), inherit: false);
		if (customAttributes.Length != 0)
		{
			command = customAttributes[0] as Command;
		}
		if (command == null)
		{
			return;
		}
		if (!method.IsStatic)
		{
			throw new Exception(method.DeclaringType.Name + "." + method.Name + " is marked as a command, but is not static");
		}
		CommandInfo value = new CommandInfo
		{
			Help = command.Help
		};
		ParameterInfo[] parameters = method.GetParameters();
		object[] defaults = new object[parameters.Length];
		string[] array = new string[parameters.Length];
		for (int i = 0; i < parameters.Length; i++)
		{
			ParameterInfo parameterInfo = parameters[i];
			array[i] = parameterInfo.Name + ":";
			if (parameterInfo.ParameterType == typeof(string))
			{
				array[i] += "string";
			}
			else if (parameterInfo.ParameterType == typeof(int))
			{
				array[i] += "int";
			}
			else if (parameterInfo.ParameterType == typeof(float))
			{
				array[i] += "float";
			}
			else
			{
				if (!(parameterInfo.ParameterType == typeof(bool)))
				{
					throw new Exception(method.DeclaringType.Name + "." + method.Name + " is marked as a command, but has an invalid parameter type. Allowed types are: string, int, float, and bool");
				}
				array[i] += "bool";
			}
			if (parameterInfo.DefaultValue == DBNull.Value)
			{
				defaults[i] = null;
			}
			else if (parameterInfo.DefaultValue != null)
			{
				defaults[i] = parameterInfo.DefaultValue;
				if (parameterInfo.ParameterType == typeof(string))
				{
					ref string reference = ref array[i];
					reference = string.Concat(reference, "=\"", parameterInfo.DefaultValue, "\"");
				}
				else
				{
					ref string reference2 = ref array[i];
					reference2 = reference2 + "=" + parameterInfo.DefaultValue;
				}
			}
			else
			{
				defaults[i] = null;
			}
		}
		if (array.Length == 0)
		{
			value.Usage = "";
		}
		else
		{
			value.Usage = "[" + string.Join(" ", array) + "]";
		}
		value.Action = delegate(string[] args)
		{
			if (parameters.Length == 0)
			{
				InvokeMethod(method);
			}
			else
			{
				object[] array2 = (object[])defaults.Clone();
				for (int j = 0; j < array2.Length && j < args.Length; j++)
				{
					if (parameters[j].ParameterType == typeof(string))
					{
						array2[j] = ArgString(args[j]);
					}
					else if (parameters[j].ParameterType == typeof(int))
					{
						array2[j] = ArgInt(args[j]);
					}
					else if (parameters[j].ParameterType == typeof(float))
					{
						array2[j] = ArgFloat(args[j]);
					}
					else if (parameters[j].ParameterType == typeof(bool))
					{
						array2[j] = ArgBool(args[j]);
					}
				}
				InvokeMethod(method, array2);
			}
		};
		commands[command.Name] = value;
	}

	private void InvokeMethod(MethodInfo method, object[] param = null)
	{
		try
		{
			method.Invoke(null, param);
		}
		catch (Exception ex)
		{
			Engine.Commands.Log(ex.InnerException.Message, Color.Yellow);
			LogStackTrace(ex.InnerException.StackTrace);
		}
	}

	private void LogStackTrace(string stackTrace)
	{
		string[] array = stackTrace.Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			int num = text.LastIndexOf(" in ") + 4;
			int num2 = text.LastIndexOf('\\') + 1;
			if (num != -1 && num2 != -1)
			{
				text = text.Substring(0, num) + text.Substring(num2);
			}
			int num3 = text.IndexOf('(') + 1;
			int num4 = text.IndexOf(')');
			if (num3 != -1 && num4 != -1)
			{
				text = text.Substring(0, num3) + text.Substring(num4);
			}
			int num5 = text.LastIndexOf(':');
			if (num5 != -1)
			{
				text = text.Insert(num5 + 1, " ").Insert(num5, " ");
			}
			text = text.TrimStart();
			text = "-> " + text;
			Engine.Commands.Log(text, Color.White);
		}
	}

	private static string ArgString(string arg)
	{
		if (arg == null)
		{
			return "";
		}
		return arg;
	}

	private static bool ArgBool(string arg)
	{
		if (arg != null)
		{
			if (!(arg == "0") && !(arg.ToLower() == "false"))
			{
				return !(arg.ToLower() == "f");
			}
			return false;
		}
		return false;
	}

	private static int ArgInt(string arg)
	{
		try
		{
			return Convert.ToInt32(arg);
		}
		catch
		{
			return 0;
		}
	}

	private static float ArgFloat(string arg)
	{
		try
		{
			return Convert.ToSingle(arg, CultureInfo.InvariantCulture);
		}
		catch
		{
			return 0f;
		}
	}

	[Command("clear", "Clears the terminal")]
	public static void Clear()
	{
		Engine.Commands.drawCommands.Clear();
	}

	[Command("exit", "Exits the game")]
	private static void Exit()
	{
		Engine.Instance.Exit();
	}

	[Command("vsync", "Enables or disables vertical sync")]
	private static void Vsync(bool enabled = true)
	{
		Engine.Graphics.SynchronizeWithVerticalRetrace = enabled;
		Engine.Graphics.ApplyChanges();
		Engine.Commands.Log("Vertical Sync " + (enabled ? "Enabled" : "Disabled"));
	}

	[Command("count", "Logs amount of Entities in the Scene. Pass a tagIndex to count only Entities with that tag")]
	private static void Count(int tagIndex = -1)
	{
		if (Engine.Scene == null)
		{
			Engine.Commands.Log("Current Scene is null!");
		}
		else if (tagIndex < 0)
		{
			Engine.Commands.Log(Engine.Scene.Entities.Count.ToString());
		}
		else
		{
			Engine.Commands.Log(Engine.Scene.TagLists[tagIndex].Count.ToString());
		}
	}

	[Command("tracker", "Logs all tracked objects in the scene. Set mode to 'e' for just entities, or 'c' for just components")]
	private static void Tracker(string mode)
	{
		if (Engine.Scene == null)
		{
			Engine.Commands.Log("Current Scene is null!");
		}
		else if (!(mode == "e"))
		{
			if (!(mode == "c"))
			{
				Engine.Commands.Log("-- Entities --");
				Engine.Scene.Tracker.LogEntities();
				Engine.Commands.Log("-- Components --");
				Engine.Scene.Tracker.LogComponents();
			}
			else
			{
				Engine.Scene.Tracker.LogComponents();
			}
		}
		else
		{
			Engine.Scene.Tracker.LogEntities();
		}
	}

	[Command("pooler", "Logs the pooled Entity counts")]
	private static void Pooler()
	{
		Engine.Pooler.Log();
	}

	[Command("fullscreen", "Switches to fullscreen mode")]
	private static void Fullscreen()
	{
		Engine.SetFullscreen();
	}

	[Command("window", "Switches to window mode")]
	private static void Window(int scale = 1)
	{
		Engine.SetWindowed(320 * scale, 180 * scale);
	}

	[Command("help", "Shows usage help for a given command")]
	private static void Help(string command)
	{
		if (Engine.Commands.sorted.Contains(command))
		{
			CommandInfo commandInfo = Engine.Commands.commands[command];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(":: ");
			stringBuilder.Append(command);
			if (!string.IsNullOrEmpty(commandInfo.Usage))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(commandInfo.Usage);
			}
			Engine.Commands.Log(stringBuilder.ToString());
			if (string.IsNullOrEmpty(commandInfo.Help))
			{
				Engine.Commands.Log("No help info set");
			}
			else
			{
				Engine.Commands.Log(commandInfo.Help);
			}
		}
		else
		{
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append("Commands list: ");
			stringBuilder2.Append(string.Join(", ", Engine.Commands.sorted));
			Engine.Commands.Log(stringBuilder2.ToString());
			Engine.Commands.Log("Type 'help command' for more info on that command!");
		}
	}
}
