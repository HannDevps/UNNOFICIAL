using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste.Editor;

public class MapEditor : Scene
{
	private enum MouseModes
	{
		Hover,
		Pan,
		Select,
		Move,
		Resize
	}

	private static readonly Color gridColor = new Color(0.1f, 0.1f, 0.1f);

	private static Camera Camera;

	private static AreaKey area = AreaKey.None;

	private static float saveFlash = 0f;

	private MapData mapData;

	private List<LevelTemplate> levels = new List<LevelTemplate>();

	private Vector2 mousePosition;

	private MouseModes mouseMode;

	private Vector2 lastMouseScreenPosition;

	private Vector2 mouseDragStart;

	private HashSet<LevelTemplate> selection = new HashSet<LevelTemplate>();

	private HashSet<LevelTemplate> hovered = new HashSet<LevelTemplate>();

	private float fade;

	private List<Vector2[]> undoStack = new List<Vector2[]>();

	private List<Vector2[]> redoStack = new List<Vector2[]>();

	public MapEditor(AreaKey area, bool reloadMapData = true)
	{
		area.ID = Calc.Clamp(area.ID, 0, AreaData.Areas.Count - 1);
		mapData = AreaData.Areas[area.ID].Mode[(int)area.Mode].MapData;
		if (reloadMapData)
		{
			mapData.Reload();
		}
		foreach (LevelData level in mapData.Levels)
		{
			levels.Add(new LevelTemplate(level));
		}
		foreach (Rectangle item in mapData.Filler)
		{
			levels.Add(new LevelTemplate(item.X, item.Y, item.Width, item.Height));
		}
		if (area != MapEditor.area)
		{
			MapEditor.area = area;
			Camera = new Camera();
			Camera.Zoom = 6f;
			Camera.CenterOrigin();
		}
		if (SaveData.Instance == null)
		{
			SaveData.InitializeDebugMode();
		}
	}

	public override void GainFocus()
	{
		base.GainFocus();
		SaveAndReload();
	}

	private void SelectAll()
	{
		selection.Clear();
		foreach (LevelTemplate level in levels)
		{
			selection.Add(level);
		}
	}

	public void Rename(string oldName, string newName)
	{
		LevelTemplate levelTemplate = null;
		LevelTemplate levelTemplate2 = null;
		foreach (LevelTemplate level in levels)
		{
			if (levelTemplate == null && level.Name == oldName)
			{
				levelTemplate = level;
				if (levelTemplate2 != null)
				{
					break;
				}
			}
			else if (levelTemplate2 == null && level.Name == newName)
			{
				levelTemplate2 = level;
				if (levelTemplate != null)
				{
					break;
				}
			}
		}
		string path = Path.Combine("..", "..", "..", "Content", "Levels", mapData.Filename);
		if (levelTemplate2 == null)
		{
			File.Move(Path.Combine(path, levelTemplate.Name + ".xml"), Path.Combine(path, newName + ".xml"));
			levelTemplate.Name = newName;
		}
		else
		{
			string text = Path.Combine(path, "TEMP.xml");
			File.Move(Path.Combine(path, levelTemplate.Name + ".xml"), text);
			File.Move(Path.Combine(path, levelTemplate2.Name + ".xml"), Path.Combine(path, oldName + ".xml"));
			File.Move(text, Path.Combine(path, newName + ".xml"));
			levelTemplate.Name = newName;
			levelTemplate2.Name = oldName;
		}
		Save();
	}

	private void Save()
	{
	}

	private void SaveAndReload()
	{
	}

	private void UpdateMouse()
	{
		mousePosition = Vector2.Transform(MInput.Mouse.Position, Matrix.Invert(Camera.Matrix));
	}

	public override void Update()
	{
		Vector2 vector = default(Vector2);
		vector.X = (lastMouseScreenPosition.X - MInput.Mouse.Position.X) / Camera.Zoom;
		vector.Y = (lastMouseScreenPosition.Y - MInput.Mouse.Position.Y) / Camera.Zoom;
		if (MInput.Keyboard.Pressed(Keys.Space) && MInput.Keyboard.Check(Keys.LeftControl))
		{
			Camera.Zoom = 6f;
			Camera.Position = Vector2.Zero;
		}
		int num = Math.Sign(MInput.Mouse.WheelDelta);
		if ((num > 0 && Camera.Zoom >= 1f) || Camera.Zoom > 1f)
		{
			Camera.Zoom += num;
		}
		else
		{
			Camera.Zoom += (float)num * 0.25f;
		}
		Camera.Zoom = Math.Max(0.25f, Math.Min(24f, Camera.Zoom));
		Camera.Position += new Vector2(Input.MoveX.Value, Input.MoveY.Value) * 300f * Engine.DeltaTime;
		UpdateMouse();
		hovered.Clear();
		if (mouseMode == MouseModes.Hover)
		{
			mouseDragStart = mousePosition;
			if (MInput.Mouse.PressedLeftButton)
			{
				bool flag = LevelCheck(mousePosition);
				if (MInput.Keyboard.Check(Keys.Space))
				{
					mouseMode = MouseModes.Pan;
				}
				else if (MInput.Keyboard.Check(Keys.LeftControl))
				{
					if (flag)
					{
						ToggleSelection(mousePosition);
					}
					else
					{
						mouseMode = MouseModes.Select;
					}
				}
				else if (MInput.Keyboard.Check(Keys.F))
				{
					levels.Add(new LevelTemplate((int)mousePosition.X, (int)mousePosition.Y, 32, 32));
				}
				else if (flag)
				{
					if (!SelectionCheck(mousePosition))
					{
						SetSelection(mousePosition);
					}
					bool flag2 = false;
					if (selection.Count == 1)
					{
						foreach (LevelTemplate item in selection)
						{
							if (item.ResizePosition(mousePosition) && item.Type == LevelTemplateType.Filler)
							{
								flag2 = true;
							}
						}
					}
					if (flag2)
					{
						foreach (LevelTemplate item2 in selection)
						{
							item2.StartResizing();
						}
						mouseMode = MouseModes.Resize;
					}
					else
					{
						StoreUndo();
						foreach (LevelTemplate item3 in selection)
						{
							item3.StartMoving();
						}
						mouseMode = MouseModes.Move;
					}
				}
				else
				{
					mouseMode = MouseModes.Select;
				}
			}
			else if (MInput.Mouse.PressedRightButton)
			{
				LevelTemplate levelTemplate = TestCheck(mousePosition);
				if (levelTemplate != null)
				{
					if (levelTemplate.Type == LevelTemplateType.Filler)
					{
						if (MInput.Keyboard.Check(Keys.F))
						{
							levels.Remove(levelTemplate);
						}
					}
					else
					{
						LoadLevel(levelTemplate, mousePosition * 8f);
					}
					return;
				}
			}
			else if (MInput.Mouse.PressedMiddleButton)
			{
				mouseMode = MouseModes.Pan;
			}
			else if (!MInput.Keyboard.Check(Keys.Space))
			{
				foreach (LevelTemplate level in levels)
				{
					if (level.Check(mousePosition))
					{
						hovered.Add(level);
					}
				}
				if (MInput.Keyboard.Check(Keys.LeftControl))
				{
					if (MInput.Keyboard.Pressed(Keys.Z))
					{
						Undo();
					}
					else if (MInput.Keyboard.Pressed(Keys.Y))
					{
						Redo();
					}
					else if (MInput.Keyboard.Pressed(Keys.A))
					{
						SelectAll();
					}
				}
			}
		}
		else if (mouseMode == MouseModes.Pan)
		{
			Camera.Position += vector;
			if (!MInput.Mouse.CheckLeftButton && !MInput.Mouse.CheckMiddleButton)
			{
				mouseMode = MouseModes.Hover;
			}
		}
		else if (mouseMode == MouseModes.Select)
		{
			Rectangle mouseRect = GetMouseRect(mouseDragStart, mousePosition);
			foreach (LevelTemplate level2 in levels)
			{
				if (level2.Check(mouseRect))
				{
					hovered.Add(level2);
				}
			}
			if (!MInput.Mouse.CheckLeftButton)
			{
				if (MInput.Keyboard.Check(Keys.LeftControl))
				{
					ToggleSelection(mouseRect);
				}
				else
				{
					SetSelection(mouseRect);
				}
				mouseMode = MouseModes.Hover;
			}
		}
		else if (mouseMode == MouseModes.Move)
		{
			Vector2 relativeMove = (mousePosition - mouseDragStart).Round();
			bool snap = selection.Count == 1 && !MInput.Keyboard.Check(Keys.LeftAlt);
			foreach (LevelTemplate item4 in selection)
			{
				item4.Move(relativeMove, levels, snap);
			}
			if (!MInput.Mouse.CheckLeftButton)
			{
				mouseMode = MouseModes.Hover;
			}
		}
		else if (mouseMode == MouseModes.Resize)
		{
			Vector2 relativeMove2 = (mousePosition - mouseDragStart).Round();
			foreach (LevelTemplate item5 in selection)
			{
				item5.Resize(relativeMove2);
			}
			if (!MInput.Mouse.CheckLeftButton)
			{
				mouseMode = MouseModes.Hover;
			}
		}
		if (MInput.Keyboard.Pressed(Keys.D1))
		{
			SetEditorColor(0);
		}
		else if (MInput.Keyboard.Pressed(Keys.D2))
		{
			SetEditorColor(1);
		}
		else if (MInput.Keyboard.Pressed(Keys.D3))
		{
			SetEditorColor(2);
		}
		else if (MInput.Keyboard.Pressed(Keys.D4))
		{
			SetEditorColor(3);
		}
		else if (MInput.Keyboard.Pressed(Keys.D5))
		{
			SetEditorColor(4);
		}
		else if (MInput.Keyboard.Pressed(Keys.D6))
		{
			SetEditorColor(5);
		}
		else if (MInput.Keyboard.Pressed(Keys.D7))
		{
			SetEditorColor(6);
		}
		if (MInput.Keyboard.Pressed(Keys.F1) || (MInput.Keyboard.Check(Keys.LeftControl) && MInput.Keyboard.Pressed(Keys.S)))
		{
			SaveAndReload();
			return;
		}
		if (saveFlash > 0f)
		{
			saveFlash -= Engine.DeltaTime * 4f;
		}
		lastMouseScreenPosition = MInput.Mouse.Position;
		base.Update();
	}

	private void SetEditorColor(int index)
	{
		foreach (LevelTemplate item in selection)
		{
			item.EditorColorIndex = index;
		}
	}

	public override void Render()
	{
		UpdateMouse();
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Camera.Matrix * Engine.ScreenMatrix);
		float num = 1920f / Camera.Zoom;
		float num2 = 1080f / Camera.Zoom;
		int num3 = 5;
		float num4 = (float)Math.Floor(Camera.Left / (float)num3 - 1f) * (float)num3;
		float num5 = (float)Math.Floor(Camera.Top / (float)num3 - 1f) * (float)num3;
		for (float num6 = num4; num6 <= num4 + num + 10f; num6 += 5f)
		{
			Draw.Line(num6, Camera.Top, num6, Camera.Top + num2, gridColor);
		}
		for (float num7 = num5; num7 <= num5 + num2 + 10f; num7 += 5f)
		{
			Draw.Line(Camera.Left, num7, Camera.Left + num, num7, gridColor);
		}
		Draw.Line(0f, Camera.Top, 0f, Camera.Top + num2, Color.DarkSlateBlue, 1f / Camera.Zoom);
		Draw.Line(Camera.Left, 0f, Camera.Left + num, 0f, Color.DarkSlateBlue, 1f / Camera.Zoom);
		foreach (LevelTemplate level in levels)
		{
			level.RenderContents(Camera, levels);
		}
		foreach (LevelTemplate level2 in levels)
		{
			level2.RenderOutline(Camera);
		}
		foreach (LevelTemplate level3 in levels)
		{
			level3.RenderHighlight(Camera, selection.Contains(level3), hovered.Contains(level3));
		}
		if (mouseMode == MouseModes.Hover)
		{
			Draw.Line(mousePosition.X - 12f / Camera.Zoom, mousePosition.Y, mousePosition.X + 12f / Camera.Zoom, mousePosition.Y, Color.Yellow, 3f / Camera.Zoom);
			Draw.Line(mousePosition.X, mousePosition.Y - 12f / Camera.Zoom, mousePosition.X, mousePosition.Y + 12f / Camera.Zoom, Color.Yellow, 3f / Camera.Zoom);
		}
		else if (mouseMode == MouseModes.Select)
		{
			Draw.Rect(GetMouseRect(mouseDragStart, mousePosition), Color.Lime * 0.25f);
		}
		if (saveFlash > 0f)
		{
			Draw.Rect(Camera.Left, Camera.Top, num, num2, Color.White * Ease.CubeInOut(saveFlash));
		}
		if (fade > 0f)
		{
			Draw.Rect(0f, 0f, 320f, 180f, Color.Black * fade);
		}
		Draw.SpriteBatch.End();
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Engine.ScreenMatrix);
		Draw.Rect(0f, 0f, 1920f, 72f, Color.Black);
		Vector2 position = new Vector2(16f, 4f);
		Vector2 position2 = new Vector2(1904f, 4f);
		if (MInput.Keyboard.Check(Keys.Q))
		{
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * 0.25f);
			foreach (LevelTemplate level4 in levels)
			{
				int num8 = 0;
				while (level4.Strawberries != null && num8 < level4.Strawberries.Count)
				{
					Vector2 vector = level4.Strawberries[num8];
					ActiveFont.DrawOutline(level4.StrawberryMetadata[num8], (new Vector2((float)level4.X + vector.X, (float)level4.Y + vector.Y) - Camera.Position) * Camera.Zoom + new Vector2(960f, 532f), new Vector2(0.5f, 1f), Vector2.One * 1f, Color.Red, 2f, Color.Black);
					num8++;
				}
			}
		}
		if (hovered.Count == 0)
		{
			if (selection.Count > 0)
			{
				ActiveFont.Draw(selection.Count + " levels selected", position, Color.Red);
			}
			else
			{
				ActiveFont.Draw(Dialog.Clean(mapData.Data.Name), position, Color.Aqua);
				ActiveFont.Draw(string.Concat(mapData.Area.Mode, " MODE"), position2, Vector2.UnitX, Vector2.One, Color.Red);
			}
		}
		else if (hovered.Count == 1)
		{
			LevelTemplate levelTemplate = null;
			using (HashSet<LevelTemplate>.Enumerator enumerator2 = hovered.GetEnumerator())
			{
				if (enumerator2.MoveNext())
				{
					levelTemplate = enumerator2.Current;
				}
			}
			string text = levelTemplate.ActualWidth.ToString() + "x" + levelTemplate.ActualHeight.ToString() + "   " + levelTemplate.X + "," + levelTemplate.Y + "   " + levelTemplate.X * 8 + "," + levelTemplate.Y * 8;
			ActiveFont.Draw(levelTemplate.Name, position, Color.Yellow);
			ActiveFont.Draw(text, position2, Vector2.UnitX, Vector2.One, Color.Green);
		}
		else
		{
			ActiveFont.Draw(hovered.Count + " levels", position, Color.Yellow);
		}
		Draw.SpriteBatch.End();
	}

	private void LoadLevel(LevelTemplate level, Vector2 at)
	{
		Save();
		Engine.Scene = new LevelLoader(new Session(area)
		{
			FirstLevel = false,
			Level = level.Name,
			StartedFromBeginning = false
		}, at);
	}

	private void StoreUndo()
	{
		Vector2[] array = new Vector2[levels.Count];
		for (int i = 0; i < levels.Count; i++)
		{
			array[i] = new Vector2(levels[i].X, levels[i].Y);
		}
		undoStack.Add(array);
		while (undoStack.Count > 30)
		{
			undoStack.RemoveAt(0);
		}
		redoStack.Clear();
	}

	private void Undo()
	{
		if (undoStack.Count > 0)
		{
			Vector2[] array = new Vector2[levels.Count];
			for (int i = 0; i < levels.Count; i++)
			{
				array[i] = new Vector2(levels[i].X, levels[i].Y);
			}
			redoStack.Add(array);
			Vector2[] array2 = undoStack[undoStack.Count - 1];
			undoStack.RemoveAt(undoStack.Count - 1);
			for (int j = 0; j < array2.Length; j++)
			{
				levels[j].X = (int)array2[j].X;
				levels[j].Y = (int)array2[j].Y;
			}
		}
	}

	private void Redo()
	{
		if (redoStack.Count > 0)
		{
			Vector2[] array = new Vector2[levels.Count];
			for (int i = 0; i < levels.Count; i++)
			{
				array[i] = new Vector2(levels[i].X, levels[i].Y);
			}
			undoStack.Add(array);
			Vector2[] array2 = redoStack[undoStack.Count - 1];
			redoStack.RemoveAt(undoStack.Count - 1);
			for (int j = 0; j < array2.Length; j++)
			{
				levels[j].X = (int)array2[j].X;
				levels[j].Y = (int)array2[j].Y;
			}
		}
	}

	private Rectangle GetMouseRect(Vector2 a, Vector2 b)
	{
		Vector2 vector = new Vector2(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
		Vector2 vector2 = new Vector2(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
		return new Rectangle((int)vector.X, (int)vector.Y, (int)(vector2.X - vector.X), (int)(vector2.Y - vector.Y));
	}

	private LevelTemplate TestCheck(Vector2 point)
	{
		foreach (LevelTemplate level in levels)
		{
			if (!level.Dummy && level.Check(point))
			{
				return level;
			}
		}
		return null;
	}

	private bool LevelCheck(Vector2 point)
	{
		foreach (LevelTemplate level in levels)
		{
			if (level.Check(point))
			{
				return true;
			}
		}
		return false;
	}

	private bool SelectionCheck(Vector2 point)
	{
		foreach (LevelTemplate item in selection)
		{
			if (item.Check(point))
			{
				return true;
			}
		}
		return false;
	}

	private bool SetSelection(Vector2 point)
	{
		selection.Clear();
		foreach (LevelTemplate level in levels)
		{
			if (level.Check(point))
			{
				selection.Add(level);
			}
		}
		return selection.Count > 0;
	}

	private bool ToggleSelection(Vector2 point)
	{
		bool result = false;
		foreach (LevelTemplate level in levels)
		{
			if (level.Check(point))
			{
				result = true;
				if (selection.Contains(level))
				{
					selection.Remove(level);
				}
				else
				{
					selection.Add(level);
				}
			}
		}
		return result;
	}

	private void SetSelection(Rectangle rect)
	{
		selection.Clear();
		foreach (LevelTemplate level in levels)
		{
			if (level.Check(rect))
			{
				selection.Add(level);
			}
		}
	}

	private void ToggleSelection(Rectangle rect)
	{
		foreach (LevelTemplate level in levels)
		{
			if (level.Check(rect))
			{
				if (selection.Contains(level))
				{
					selection.Remove(level);
				}
				else
				{
					selection.Add(level);
				}
			}
		}
	}
}
