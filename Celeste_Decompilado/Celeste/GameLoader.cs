using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class GameLoader : Scene
{
	[CompilerGenerated]
	private sealed class _003CIntroRoutine_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameLoader _003C_003E4__this;

		private float _003Cp_003E5__2;

		private AutoSavingNotice _003Cnotice_003E5__3;

		private Image _003Cimg_003E5__4;

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
		public _003CIntroRoutine_003Ed__16(int _003C_003E1__state)
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
			GameLoader gameLoader = _003C_003E4__this;
			bool flag;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (Celeste.PlayMode != Celeste.PlayModes.Debug)
				{
					_003Cp_003E5__2 = 0f;
					goto IL_007e;
				}
				goto IL_01f6;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime;
				goto IL_007e;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00ce;
			case 3:
				_003C_003E1__state = -1;
				goto IL_010b;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0136;
			case 5:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime;
				goto IL_018b;
			case 6:
				_003C_003E1__state = -1;
				goto IL_01d6;
			case 7:
				{
					_003C_003E1__state = -1;
					goto IL_0348;
				}
				IL_0136:
				if (!gameLoader.dialogLoaded)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003Cnotice_003E5__3 = new AutoSavingNotice();
				gameLoader.Add(_003Cnotice_003E5__3);
				_003Cp_003E5__2 = 0f;
				goto IL_018b;
				IL_007e:
				if (_003Cp_003E5__2 > 1f && !gameLoader.skipped)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (!gameLoader.skipped)
				{
					Image img = new Image(gameLoader.opening["presentedby"]);
					_003C_003E2__current = gameLoader.FadeInOut(img);
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_00ce;
				IL_018b:
				if (_003Cp_003E5__2 < 1f && !gameLoader.skipped)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				_003Cnotice_003E5__3.Display = false;
				goto IL_01d6;
				IL_01d6:
				if (_003Cnotice_003E5__3.StillVisible)
				{
					_003Cnotice_003E5__3.ForceClose = gameLoader.skipped;
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				gameLoader.Remove(_003Cnotice_003E5__3);
				_003Cnotice_003E5__3 = null;
				goto IL_01f6;
				IL_01f6:
				gameLoader.ready = true;
				if (gameLoader.loaded)
				{
					break;
				}
				gameLoader.loadingTextures = OVR.Atlas.GetAtlasSubtextures("loading/");
				_003Cimg_003E5__4 = new Image(gameLoader.loadingTextures[0]);
				_003Cimg_003E5__4.CenterOrigin();
				_003Cimg_003E5__4.Scale = Vector2.One * 0.5f;
				gameLoader.handler.Add(_003Cimg_003E5__4);
				goto IL_0348;
				IL_00ce:
				if (!gameLoader.skipped)
				{
					Image img2 = new Image(gameLoader.opening["gameby"]);
					_003C_003E2__current = gameLoader.FadeInOut(img2);
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_010b;
				IL_0348:
				if (!gameLoader.loaded || gameLoader.loadingAlpha > 0f)
				{
					gameLoader.loadingFrame += Engine.DeltaTime * 10f;
					gameLoader.loadingAlpha = Calc.Approach(gameLoader.loadingAlpha, (!gameLoader.loaded) ? 1 : 0, Engine.DeltaTime * 4f);
					_003Cimg_003E5__4.Texture = gameLoader.loadingTextures[(int)(gameLoader.loadingFrame % (float)gameLoader.loadingTextures.Count)];
					_003Cimg_003E5__4.Color = Color.White * Ease.CubeOut(gameLoader.loadingAlpha);
					_003Cimg_003E5__4.Position = new Vector2(1792f, 1080f - 128f * Ease.CubeOut(gameLoader.loadingAlpha));
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				_003Cimg_003E5__4 = null;
				break;
				IL_010b:
				flag = true;
				if (!gameLoader.skipped && flag)
				{
					goto IL_0136;
				}
				goto IL_01f6;
			}
			gameLoader.opening.Dispose();
			gameLoader.activeThread.Priority = ThreadPriority.Normal;
			MInput.Disabled = false;
			Engine.Scene = new OverworldLoader(Overworld.StartMode.Titlescreen, gameLoader.Snow);
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
	private sealed class _003CFadeInOut_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Image img;

		public GameLoader _003C_003E4__this;

		private float _003Calpha_003E5__2;

		private float _003Ci_003E5__3;

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
		public _003CFadeInOut_003Ed__17(int _003C_003E1__state)
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
			GameLoader gameLoader = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Calpha_003E5__2 = 0f;
				img.Color = Color.White * 0f;
				gameLoader.handler.Add(img);
				_003Ci_003E5__3 = 0f;
				goto IL_00d0;
			case 1:
				_003C_003E1__state = -1;
				_003Ci_003E5__3 += Engine.DeltaTime;
				goto IL_00d0;
			case 2:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_00d0:
				if (_003Ci_003E5__3 < 4.5f && !gameLoader.skipped)
				{
					_003Calpha_003E5__2 = Ease.CubeOut(Math.Min(_003Ci_003E5__3, 1f));
					img.Color = Color.White * _003Calpha_003E5__2;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				break;
			}
			if (_003Calpha_003E5__2 > 0f)
			{
				_003Calpha_003E5__2 -= Engine.DeltaTime * (float)((!gameLoader.skipped) ? 1 : 8);
				img.Color = Color.White * _003Calpha_003E5__2;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
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

	public HiresSnow Snow;

	private Atlas opening;

	private bool loaded;

	private bool audioLoaded;

	private bool audioStarted;

	private bool dialogLoaded;

	private Entity handler;

	private Thread activeThread;

	private bool skipped;

	private bool ready;

	private List<MTexture> loadingTextures;

	private float loadingFrame;

	private float loadingAlpha;

	public GameLoader()
	{
		Console.WriteLine("GAME DISPLAYED (in " + Celeste.LoadTimer.ElapsedMilliseconds + "ms)");
		Snow = new HiresSnow();
		opening = Atlas.FromAtlas(Path.Combine("Graphics", "Atlases", "Opening"), Atlas.AtlasDataFormat.PackerNoAtlas);
	}

	public override void Begin()
	{
		Add(new HudRenderer());
		Add(Snow);
		new FadeWipe(this, wipeIn: true);
		base.RendererList.UpdateLists();
		Add(handler = new Entity());
		handler.Tag = Tags.HUD;
		handler.Add(new Coroutine(IntroRoutine()));
		activeThread = Thread.CurrentThread;
		activeThread.Priority = ThreadPriority.Lowest;
		RunThread.Start(LoadThread, "GAME_LOADER", highPriority: true);
	}

	private void LoadThread()
	{
		MInput.Disabled = true;
		Stopwatch stopwatch = Stopwatch.StartNew();
		Audio.Init();
		Audio.Banks.Master = Audio.Banks.Load("Master Bank", loadStrings: true);
		Audio.Banks.Music = Audio.Banks.Load("music", loadStrings: false);
		Audio.Banks.Sfxs = Audio.Banks.Load("sfx", loadStrings: false);
		Audio.Banks.UI = Audio.Banks.Load("ui", loadStrings: false);
		Audio.Banks.DlcMusic = Audio.Banks.Load("dlc_music", loadStrings: false);
		Audio.Banks.DlcSfxs = Audio.Banks.Load("dlc_sfx", loadStrings: false);
		Settings.Instance.ApplyVolumes();
		audioLoaded = true;
		Console.WriteLine(" - AUDIO LOAD: " + stopwatch.ElapsedMilliseconds + "ms");
		GFX.Load();
		MTN.Load();
		GFX.LoadData();
		MTN.LoadData();
		Stopwatch stopwatch2 = Stopwatch.StartNew();
		Fonts.Prepare();
		Dialog.Load();
		Fonts.Load(Dialog.Languages["english"].FontFace);
		Fonts.Load(Dialog.Languages[Settings.Instance.Language].FontFace);
		dialogLoaded = true;
		Console.WriteLine(" - DIA/FONT LOAD: " + stopwatch2.ElapsedMilliseconds + "ms");
		MInput.Disabled = false;
		Stopwatch stopwatch3 = Stopwatch.StartNew();
		AreaData.Load();
		Console.WriteLine(" - LEVELS LOAD: " + stopwatch3.ElapsedMilliseconds + "ms");
		Console.WriteLine("DONE LOADING (in " + Celeste.LoadTimer.ElapsedMilliseconds + "ms)");
		Celeste.LoadTimer.Stop();
		Celeste.LoadTimer = null;
		loaded = true;
	}

	[IteratorStateMachine(typeof(_003CIntroRoutine_003Ed__16))]
	public IEnumerator IntroRoutine()
	{
		if (Celeste.PlayMode != Celeste.PlayModes.Debug)
		{
			for (float p = 0f; p > 1f; p += Engine.DeltaTime)
			{
				if (skipped)
				{
					break;
				}
				yield return null;
			}
			if (!skipped)
			{
				Image img = new Image(opening["presentedby"]);
				yield return FadeInOut(img);
			}
			if (!skipped)
			{
				Image img2 = new Image(opening["gameby"]);
				yield return FadeInOut(img2);
			}
			bool flag = true;
			if (!skipped && flag)
			{
				while (!dialogLoaded)
				{
					yield return null;
				}
				AutoSavingNotice notice = new AutoSavingNotice();
				Add(notice);
				for (float p = 0f; p < 1f; p += Engine.DeltaTime)
				{
					if (skipped)
					{
						break;
					}
					yield return null;
				}
				notice.Display = false;
				while (notice.StillVisible)
				{
					notice.ForceClose = skipped;
					yield return null;
				}
				Remove(notice);
			}
		}
		ready = true;
		if (!loaded)
		{
			loadingTextures = OVR.Atlas.GetAtlasSubtextures("loading/");
			Image img3 = new Image(loadingTextures[0]);
			img3.CenterOrigin();
			img3.Scale = Vector2.One * 0.5f;
			handler.Add(img3);
			while (!loaded || loadingAlpha > 0f)
			{
				loadingFrame += Engine.DeltaTime * 10f;
				loadingAlpha = Calc.Approach(loadingAlpha, (!loaded) ? 1 : 0, Engine.DeltaTime * 4f);
				img3.Texture = loadingTextures[(int)(loadingFrame % (float)loadingTextures.Count)];
				img3.Color = Color.White * Ease.CubeOut(loadingAlpha);
				img3.Position = new Vector2(1792f, 1080f - 128f * Ease.CubeOut(loadingAlpha));
				yield return null;
			}
		}
		opening.Dispose();
		activeThread.Priority = ThreadPriority.Normal;
		MInput.Disabled = false;
		Engine.Scene = new OverworldLoader(Overworld.StartMode.Titlescreen, Snow);
	}

	[IteratorStateMachine(typeof(_003CFadeInOut_003Ed__17))]
	private IEnumerator FadeInOut(Image img)
	{
		float alpha = 0f;
		img.Color = Color.White * 0f;
		handler.Add(img);
		for (float i = 0f; i < 4.5f; i += Engine.DeltaTime)
		{
			if (skipped)
			{
				break;
			}
			alpha = Ease.CubeOut(Math.Min(i, 1f));
			img.Color = Color.White * alpha;
			yield return null;
		}
		while (alpha > 0f)
		{
			alpha -= Engine.DeltaTime * (float)((!skipped) ? 1 : 8);
			img.Color = Color.White * alpha;
			yield return null;
		}
	}

	public override void Update()
	{
		if (audioLoaded && !audioStarted)
		{
			Audio.SetAmbience("event:/env/amb/worldmap");
			audioStarted = true;
		}
		if (!ready)
		{
			bool disabled = MInput.Disabled;
			MInput.Disabled = false;
			if (Input.MenuConfirm.Pressed)
			{
				skipped = true;
			}
			MInput.Disabled = disabled;
		}
		base.Update();
	}
}
