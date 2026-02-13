using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class LevelExit : Scene
{
	public enum Mode
	{
		SaveAndQuit,
		GiveUp,
		Restart,
		GoldenBerryRestart,
		Completed,
		CompletedInterlude
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LevelExit _003C_003E4__this;

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
		public _003CRoutine_003Ed__14(int _003C_003E1__state)
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
			LevelExit levelExit = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (levelExit.mode != Mode.GoldenBerryRestart)
				{
					UserIO.SaveHandler(file: true, settings: true);
					goto IL_005a;
				}
				goto IL_00ab;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005a;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0083;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00a4;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_00cd;
				}
				IL_005a:
				if (UserIO.Saving)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (levelExit.mode == Mode.Completed)
				{
					goto IL_0083;
				}
				goto IL_00a4;
				IL_00ab:
				if (levelExit.mode == Mode.Completed)
				{
					goto IL_00cd;
				}
				if (levelExit.mode == Mode.GiveUp || levelExit.mode == Mode.SaveAndQuit || levelExit.mode == Mode.CompletedInterlude)
				{
					Engine.Scene = levelExit.overworldLoader;
				}
				else
				{
					if (levelExit.mode != Mode.Restart && levelExit.mode != Mode.GoldenBerryRestart)
					{
						break;
					}
					Session session = null;
					if (levelExit.mode == Mode.GoldenBerryRestart)
					{
						if ((levelExit.session.Audio.Music.Event == "event:/music/lvl7/main" || levelExit.session.Audio.Music.Event == "event:/music/lvl7/final_ascent") && levelExit.session.Audio.Music.Progress > 0)
						{
							Audio.SetMusic(null);
						}
						session = levelExit.session.Restart(levelExit.GoldenStrawberryEntryLevel);
					}
					else
					{
						session = levelExit.session.Restart();
					}
					LevelLoader levelLoader = new LevelLoader(session);
					if (levelExit.mode == Mode.GoldenBerryRestart)
					{
						levelLoader.PlayerIntroTypeOverride = Player.IntroTypes.Respawn;
					}
					Engine.Scene = levelLoader;
				}
				break;
				IL_0083:
				if (!levelExit.completeLoaded)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_00a4;
				IL_00cd:
				if (levelExit.timer < 3.3f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				Audio.SetMusicParam("end", 1f);
				Engine.Scene = new AreaComplete(levelExit.session, levelExit.completeXml, levelExit.completeAtlas, levelExit.snow);
				break;
				IL_00a4:
				if (SaveLoadIcon.OnScreen)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_00ab;
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

	private Mode mode;

	private Session session;

	private float timer;

	private XmlElement completeXml;

	private Atlas completeAtlas;

	private bool completeLoaded;

	private HiresSnow snow;

	private OverworldLoader overworldLoader;

	public string GoldenStrawberryEntryLevel;

	private const float MinTimeForCompleteScreen = 3.3f;

	public LevelExit(Mode mode, Session session, HiresSnow snow = null)
	{
		Add(new HudRenderer());
		this.session = session;
		this.mode = mode;
		this.snow = snow;
	}

	public override void Begin()
	{
		base.Begin();
		if (mode != Mode.GoldenBerryRestart)
		{
			SaveLoadIcon.Show(this);
		}
		bool flag = snow == null;
		if (flag)
		{
			snow = new HiresSnow();
		}
		if (mode == Mode.Completed)
		{
			snow.Direction = new Vector2(0f, 16f);
			if (flag)
			{
				snow.Reset();
			}
			RunThread.Start(LoadCompleteThread, "COMPLETE_LEVEL");
			if (session.Area.Mode != AreaMode.Normal)
			{
				Audio.SetMusic("event:/music/menu/complete_bside");
			}
			else if (session.Area.ID == 7)
			{
				Audio.SetMusic("event:/music/menu/complete_summit");
			}
			else
			{
				Audio.SetMusic("event:/music/menu/complete_area");
			}
			Audio.SetAmbience(null);
		}
		if (mode == Mode.GiveUp)
		{
			overworldLoader = new OverworldLoader(Overworld.StartMode.AreaQuit, snow);
		}
		else if (mode == Mode.SaveAndQuit)
		{
			overworldLoader = new OverworldLoader(Overworld.StartMode.MainMenu, snow);
		}
		else if (mode == Mode.CompletedInterlude)
		{
			overworldLoader = new OverworldLoader(Overworld.StartMode.AreaComplete, snow);
		}
		Entity entity;
		Add(entity = new Entity());
		entity.Add(new Coroutine(Routine()));
		if (mode != Mode.Restart && mode != Mode.GoldenBerryRestart)
		{
			Add(snow);
			if (flag)
			{
				new FadeWipe(this, wipeIn: true);
			}
		}
		Stats.Store();
		base.RendererList.UpdateLists();
	}

	private void LoadCompleteThread()
	{
		completeXml = AreaData.Get(session).CompleteScreenXml;
		if (completeXml != null && completeXml.HasAttr("atlas"))
		{
			string path = Path.Combine("Graphics", "Atlases", completeXml.Attr("atlas"));
			completeAtlas = Atlas.FromAtlas(path, Atlas.AtlasDataFormat.PackerNoAtlas);
		}
		completeLoaded = true;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__14))]
	private IEnumerator Routine()
	{
		if (mode != Mode.GoldenBerryRestart)
		{
			UserIO.SaveHandler(file: true, settings: true);
			while (UserIO.Saving)
			{
				yield return null;
			}
			if (mode == Mode.Completed)
			{
				while (!completeLoaded)
				{
					yield return null;
				}
			}
			while (SaveLoadIcon.OnScreen)
			{
				yield return null;
			}
		}
		if (mode == Mode.Completed)
		{
			while (timer < 3.3f)
			{
				yield return null;
			}
			Audio.SetMusicParam("end", 1f);
			Engine.Scene = new AreaComplete(this.session, completeXml, completeAtlas, snow);
		}
		else if (mode == Mode.GiveUp || mode == Mode.SaveAndQuit || mode == Mode.CompletedInterlude)
		{
			Engine.Scene = overworldLoader;
		}
		else
		{
			if (mode != Mode.Restart && mode != Mode.GoldenBerryRestart)
			{
				yield break;
			}
			Session session;
			if (mode == Mode.GoldenBerryRestart)
			{
				if ((this.session.Audio.Music.Event == "event:/music/lvl7/main" || this.session.Audio.Music.Event == "event:/music/lvl7/final_ascent") && this.session.Audio.Music.Progress > 0)
				{
					Audio.SetMusic(null);
				}
				session = this.session.Restart(GoldenStrawberryEntryLevel);
			}
			else
			{
				session = this.session.Restart();
			}
			LevelLoader levelLoader = new LevelLoader(session);
			if (mode == Mode.GoldenBerryRestart)
			{
				levelLoader.PlayerIntroTypeOverride = Player.IntroTypes.Respawn;
			}
			Engine.Scene = levelLoader;
		}
	}

	public override void Update()
	{
		timer += Engine.DeltaTime;
		base.Update();
	}
}
