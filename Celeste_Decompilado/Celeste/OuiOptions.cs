using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiOptions : Oui
{
	[CompilerGenerated]
	private sealed class _003CEnter_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiOptions _003C_003E4__this;

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
		public _003CEnter_003Ed__8(int _003C_003E1__state)
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
			OuiOptions ouiOptions = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				ouiOptions.ReloadMenu();
				ouiOptions.menu.Visible = (ouiOptions.Visible = true);
				ouiOptions.menu.Focused = false;
				ouiOptions.currentLanguage = (ouiOptions.startLanguage = Settings.Instance.Language);
				_003Cp_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				ouiOptions.menu.X = 2880f + -1920f * Ease.CubeOut(_003Cp_003E5__2);
				ouiOptions.alpha = Ease.CubeOut(_003Cp_003E5__2);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ouiOptions.menu.Focused = true;
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

		public OuiOptions _003C_003E4__this;

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
			OuiOptions ouiOptions = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/ui/main/whoosh_large_out");
				ouiOptions.menu.Focused = false;
				UserIO.SaveHandler(file: false, settings: true);
				goto IL_0064;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0064;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				goto IL_00ea;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0064:
				if (UserIO.Saving)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003Cp_003E5__2 = 0f;
				goto IL_00ea;
				IL_00ea:
				if (_003Cp_003E5__2 < 1f)
				{
					ouiOptions.menu.X = 960f + 1920f * Ease.CubeIn(_003Cp_003E5__2);
					ouiOptions.alpha = 1f - Ease.CubeIn(_003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				if (ouiOptions.startLanguage != Settings.Instance.Language)
				{
					ouiOptions.Overworld.ReloadMenus(Overworld.StartMode.ReturnFromOptions);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				break;
			}
			ouiOptions.menu.Visible = (ouiOptions.Visible = false);
			ouiOptions.menu.RemoveSelf();
			ouiOptions.menu = null;
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

	private TextMenu menu;

	private const float onScreenX = 960f;

	private const float offScreenX = 2880f;

	private string startLanguage;

	private string currentLanguage;

	private float alpha;

	public override void Added(Scene scene)
	{
		base.Added(scene);
	}

	private void ReloadMenu()
	{
		Vector2 position = Vector2.Zero;
		int num = -1;
		if (menu != null)
		{
			position = menu.Position;
			num = menu.Selection;
			base.Scene.Remove(menu);
		}
		menu = MenuOptions.Create();
		if (num >= 0)
		{
			menu.Selection = num;
			menu.Position = position;
		}
		base.Scene.Add(menu);
	}

	[IteratorStateMachine(typeof(_003CEnter_003Ed__8))]
	public override IEnumerator Enter(Oui from)
	{
		ReloadMenu();
		TextMenu textMenu = menu;
		OuiOptions ouiOptions = this;
		bool visible = true;
		ouiOptions.Visible = true;
		textMenu.Visible = visible;
		menu.Focused = false;
		currentLanguage = (startLanguage = Settings.Instance.Language);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			menu.X = 2880f + -1920f * Ease.CubeOut(p);
			alpha = Ease.CubeOut(p);
			yield return null;
		}
		menu.Focused = true;
	}

	[IteratorStateMachine(typeof(_003CLeave_003Ed__9))]
	public override IEnumerator Leave(Oui next)
	{
		Audio.Play("event:/ui/main/whoosh_large_out");
		menu.Focused = false;
		UserIO.SaveHandler(file: false, settings: true);
		while (UserIO.Saving)
		{
			yield return null;
		}
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			menu.X = 960f + 1920f * Ease.CubeIn(p);
			alpha = 1f - Ease.CubeIn(p);
			yield return null;
		}
		if (startLanguage != Settings.Instance.Language)
		{
			base.Overworld.ReloadMenus(Overworld.StartMode.ReturnFromOptions);
			yield return null;
		}
		TextMenu textMenu = menu;
		OuiOptions ouiOptions = this;
		bool visible = false;
		ouiOptions.Visible = false;
		textMenu.Visible = visible;
		menu.RemoveSelf();
		menu = null;
	}

	public override void Update()
	{
		if (menu != null && menu.Focused && base.Selected && Input.MenuCancel.Pressed)
		{
			Audio.Play("event:/ui/main/button_back");
			base.Overworld.Goto<OuiMainMenu>();
		}
		if (base.Selected && currentLanguage != Settings.Instance.Language)
		{
			currentLanguage = Settings.Instance.Language;
			ReloadMenu();
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
}
