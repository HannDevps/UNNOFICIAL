using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class CoreVignette : Scene
{
	[CompilerGenerated]
	private sealed class _003CTextSequence_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CoreVignette _003C_003E4__this;

		private float _003CfadeTimePerCharacter_003E5__2;

		private int _003Ci_003E5__3;

		private FancyText.Char _003Cc_003E5__4;

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
		public _003CTextSequence_003Ed__15(int _003C_003E1__state)
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
			CoreVignette coreVignette = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_01f2;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00d3;
			case 3:
				_003C_003E1__state = -1;
				goto IL_01a3;
			case 4:
				_003C_003E1__state = -1;
				goto IL_01a3;
			case 5:
				{
					_003C_003E1__state = -1;
					goto IL_01f2;
				}
				IL_01a3:
				if (coreVignette.textAlpha > 0f)
				{
					coreVignette.textAlpha -= 1f * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				coreVignette.textAlpha = 0f;
				coreVignette.textStart = coreVignette.text.GetNextPageStart(coreVignette.textStart);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
				IL_0112:
				_003Ci_003E5__3++;
				goto IL_0124;
				IL_0124:
				if (_003Ci_003E5__3 < coreVignette.text.Count && !(coreVignette.text[_003Ci_003E5__3] is FancyText.NewPage))
				{
					_003Cc_003E5__4 = coreVignette.text[_003Ci_003E5__3] as FancyText.Char;
					if (_003Cc_003E5__4 != null)
					{
						goto IL_00d3;
					}
					goto IL_0112;
				}
				_003C_003E2__current = 2.5f;
				_003C_003E1__state = 3;
				return true;
				IL_01f2:
				if (coreVignette.textStart < coreVignette.text.Count)
				{
					coreVignette.textAlpha = 1f;
					int charactersOnPage = coreVignette.text.GetCharactersOnPage(coreVignette.textStart);
					_003CfadeTimePerCharacter_003E5__2 = 1f / (float)charactersOnPage;
					_003Ci_003E5__3 = coreVignette.textStart;
					goto IL_0124;
				}
				if (!coreVignette.started)
				{
					coreVignette.StartGame();
				}
				coreVignette.textStart = int.MaxValue;
				return false;
				IL_00d3:
				if ((_003Cc_003E5__4.Fade += Engine.DeltaTime / _003CfadeTimePerCharacter_003E5__2) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Cc_003E5__4.Fade = 1f;
				_003Cc_003E5__4 = null;
				goto IL_0112;
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

	private Session session;

	private Coroutine textCoroutine;

	private FancyText.Text text;

	private int textStart;

	private float textAlpha;

	private HiresSnow snow;

	private HudRenderer hud;

	private TextMenu menu;

	private float fade;

	private float pauseFade;

	private bool started;

	private bool exiting;

	public bool CanPause => menu == null;

	public CoreVignette(Session session, HiresSnow snow = null)
	{
		this.session = session;
		if (snow == null)
		{
			snow = new HiresSnow();
		}
		Add(hud = new HudRenderer());
		Add(this.snow = snow);
		base.RendererList.UpdateLists();
		text = FancyText.Parse(Dialog.Get("APP_INTRO"), 960, 8, 0f);
		textCoroutine = new Coroutine(TextSequence());
	}

	[IteratorStateMachine(typeof(_003CTextSequence_003Ed__15))]
	private IEnumerator TextSequence()
	{
		yield return 1f;
		while (textStart < text.Count)
		{
			textAlpha = 1f;
			int charactersOnPage = text.GetCharactersOnPage(textStart);
			float fadeTimePerCharacter = 1f / (float)charactersOnPage;
			for (int i = textStart; i < text.Count && !(text[i] is FancyText.NewPage); i++)
			{
				if (text[i] is FancyText.Char c)
				{
					while ((c.Fade += Engine.DeltaTime / fadeTimePerCharacter) < 1f)
					{
						yield return null;
					}
					c.Fade = 1f;
				}
			}
			yield return 2.5f;
			while (textAlpha > 0f)
			{
				textAlpha -= 1f * Engine.DeltaTime;
				yield return null;
			}
			textAlpha = 0f;
			textStart = text.GetNextPageStart(textStart);
			yield return 0.5f;
		}
		if (!started)
		{
			StartGame();
		}
		textStart = int.MaxValue;
	}

	public override void Update()
	{
		if (menu == null)
		{
			base.Update();
			if (!exiting)
			{
				if (textCoroutine != null && textCoroutine.Active)
				{
					textCoroutine.Update();
				}
				if (menu == null && (Input.Pause.Pressed || Input.ESC.Pressed))
				{
					Input.Pause.ConsumeBuffer();
					Input.ESC.ConsumeBuffer();
					OpenMenu();
				}
			}
		}
		else if (!exiting)
		{
			menu.Update();
		}
		pauseFade = Calc.Approach(pauseFade, (menu != null) ? 1 : 0, Engine.DeltaTime * 8f);
		hud.BackgroundFade = Calc.Approach(hud.BackgroundFade, (menu != null) ? 0.6f : 0f, Engine.DeltaTime * 3f);
		fade = Calc.Approach(fade, 0f, Engine.DeltaTime);
	}

	public void OpenMenu()
	{
		Audio.Play("event:/ui/game/pause");
		Add(menu = new TextMenu());
		menu.Add(new TextMenu.Button(Dialog.Clean("intro_vignette_resume")).Pressed(CloseMenu));
		menu.Add(new TextMenu.Button(Dialog.Clean("intro_vignette_skip")).Pressed(StartGame));
		menu.Add(new TextMenu.Button(Dialog.Clean("intro_vignette_quit")).Pressed(ReturnToMap));
		menu.OnCancel = (menu.OnESC = (menu.OnPause = CloseMenu));
	}

	private void CloseMenu()
	{
		Audio.Play("event:/ui/game/unpause");
		if (menu != null)
		{
			menu.RemoveSelf();
		}
		menu = null;
	}

	private void StartGame()
	{
		textCoroutine = null;
		if (menu != null)
		{
			menu.RemoveSelf();
			menu = null;
		}
		new FadeWipe(this, wipeIn: false, delegate
		{
			Engine.Scene = new LevelLoader(session);
		}).OnUpdate = delegate(float f)
		{
			textAlpha = Math.Min(textAlpha, 1f - f);
		};
		started = true;
		exiting = true;
	}

	private void ReturnToMap()
	{
		menu.RemoveSelf();
		menu = null;
		exiting = true;
		bool toAreaQuit = SaveData.Instance.Areas[0].Modes[0].Completed && Celeste.PlayMode != Celeste.PlayModes.Event;
		new FadeWipe(this, wipeIn: false, delegate
		{
			if (toAreaQuit)
			{
				Engine.Scene = new OverworldLoader(Overworld.StartMode.AreaQuit, snow);
			}
			else
			{
				Engine.Scene = new OverworldLoader(Overworld.StartMode.Titlescreen, snow);
			}
		}).OnUpdate = delegate(float f)
		{
			textAlpha = Math.Min(textAlpha, 1f - f);
		};
		base.RendererList.UpdateLists();
		base.RendererList.MoveToFront(snow);
	}

	public override void Render()
	{
		base.Render();
		if (fade > 0f || textAlpha > 0f)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, RasterizerState.CullNone, null, Engine.ScreenMatrix);
			if (fade > 0f)
			{
				Draw.Rect(-1f, -1f, 1922f, 1082f, Color.Black * fade);
			}
			if (textStart < text.Nodes.Count && textAlpha > 0f)
			{
				text.Draw(new Vector2(1920f, 1080f) * 0.5f, new Vector2(0.5f, 0.5f), Vector2.One, textAlpha * (1f - pauseFade), textStart);
			}
			Draw.SpriteBatch.End();
		}
	}
}
