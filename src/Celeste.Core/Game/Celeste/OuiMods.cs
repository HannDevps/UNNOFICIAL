using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiMods : Oui
{
	private TextMenu menu;

	private float alpha;

	private string currentLanguage;

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Visible = false;
	}

	public override IEnumerator Enter(Oui from)
	{
		ReloadMenu();
		Visible = true;
		menu.Visible = true;
		menu.Focused = false;
		currentLanguage = Settings.Instance.Language;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			menu.X = 2880f + -1920f * Ease.CubeOut(p);
			alpha = Ease.CubeOut(p);
			yield return null;
		}

		menu.X = 960f;
		menu.Focused = true;
	}

	public override IEnumerator Leave(Oui next)
	{
		Audio.Play("event:/ui/main/whoosh_large_out");
		menu.Focused = false;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			menu.X = 960f + 1920f * Ease.CubeIn(p);
			alpha = 1f - Ease.CubeIn(p);
			yield return null;
		}

		Visible = false;
		menu.Visible = false;
		menu.RemoveSelf();
		menu = null;
	}

	public override void Update()
	{
		if (menu != null && base.Selected && menu.Focused && Input.MenuCancel.Pressed)
		{
			OnBack();
		}

		if (base.Selected && currentLanguage != Settings.Instance.Language)
		{
			currentLanguage = Settings.Instance.Language;
			ReloadMenu();
			menu.X = 960f;
			menu.Focused = true;
		}

		base.Update();
	}

	public override void Render()
	{
		if (alpha > 0f)
		{
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * alpha * 0.4f);
		}

		base.Render();
	}

	private void ReloadMenu()
	{
		Vector2 position = new Vector2(2880f, 540f);
		if (menu != null)
		{
			position = menu.Position;
			menu.RemoveSelf();
		}

		menu = new TextMenu();
		menu.MinWidth = 1280f;
		menu.ItemSpacing = 12f;
		menu.Position = position;
		menu.Add(new TextMenu.Header(Dialog.Clean("mods_title")));
		menu.Add(new TextMenuWrappedText(Dialog.Clean("mods_status"), 1120, 0.85f));
		menu.Add(new TextMenu.Button(Dialog.Clean("mods_back")).Pressed(OnBack));
		base.Scene.Add(menu);
	}

	private void OnBack()
	{
		Audio.Play("event:/ui/main/button_back");
		base.Overworld.Goto<OuiMainMenu>();
	}
}
