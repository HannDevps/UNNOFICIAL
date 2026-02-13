using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Celeste.Pico8;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Celeste : Engine
{
	public enum PlayModes
	{
		Normal,
		Debug,
		Event,
		Demo
	}

	public const int GameWidth = 320;
	public const int GameHeight = 180;
	public const int TargetWidth = 1920;
	public const int TargetHeight = 1080;

	public static PlayModes PlayMode = PlayModes.Normal;

	public const string EventName = "";
	public const bool Beta = false;

	// Para Android/Linux você pode trocar isso depois, mas não quebra nada por enquanto.
	public const string PLATFORM = "PC";

	public new static Celeste Instance;

	public static VirtualRenderTarget HudTarget;
	public static VirtualRenderTarget WipeTarget;

	public static DisconnectedControllerUI DisconnectUI;

	private bool firstLoad = true;

	public AutoSplitterInfo AutoSplitterInfo = new AutoSplitterInfo();

	public static Coroutine SaveRoutine;

	public static Stopwatch LoadTimer;

	private static int _mainThreadId;

	public static Vector2 TargetCenter => new Vector2(1920f, 1080f) / 2f;

	public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == _mainThreadId;

	public Celeste()
		: base(1920, 1080, 960, 540, "Celeste", Settings.Instance.Fullscreen, Settings.Instance.VSync)
	{
		Version = new System.Version(1, 4, 0, 0);
		Instance = this;
		Engine.ExitOnEscapeKeypress = false;
		base.IsFixedTimeStep = true;
		Stats.MakeRequest();
		StatsForStadia.MakeRequest();
		Console.WriteLine("CELESTE : " + Version);
	}

	protected override void Initialize()
	{
		base.Initialize();
		Settings.Instance.AfterLoad();

		if (Settings.Instance.Fullscreen)
		{
			Engine.ViewPadding = Settings.Instance.ViewportPadding;
		}

		Settings.Instance.ApplyScreen();
		SFX.Initialize();
		Tags.Initialize();
		Input.Initialize();

		Engine.Commands.Enabled = PlayMode == PlayModes.Debug;
		Engine.Scene = new GameLoader();
	}

	protected override void LoadContent()
	{
		base.LoadContent();
		Console.WriteLine("BEGIN LOAD");
		LoadTimer = Stopwatch.StartNew();

		PlaybackData.Load();

		if (firstLoad)
		{
			firstLoad = false;

			HudTarget = VirtualContent.CreateRenderTarget("hud-target", 1922, 1082);
			WipeTarget = VirtualContent.CreateRenderTarget("wipe-target", 1922, 1082);

			OVR.Load();
			GFX.Load();
			MTN.Load();
		}

		if (GFX.Game != null)
		{
			Monocle.Draw.Particle = GFX.Game["util/particle"];
			Monocle.Draw.Pixel = new MTexture(GFX.Game["util/pixel"], 1, 1, 1, 1);
		}

		GFX.LoadEffects();
	}

	protected override void Update(GameTime gameTime)
	{
		if (SaveRoutine != null)
		{
			SaveRoutine.Update();
		}

		AutoSplitterInfo.Update();
		Audio.Update();

		base.Update(gameTime);

		Input.UpdateGrab();
	}

	protected override void OnSceneTransition(Scene last, Scene next)
	{
		if (!(last is OverworldLoader) || !(next is Overworld))
		{
			base.OnSceneTransition(last, next);
		}

		Engine.TimeRate = 1f;
		Audio.PauseGameplaySfx = false;
		Audio.SetMusicParam("fade", 1f);

		Distort.Anxiety = 0f;
		Distort.GameRate = 1f;
		Glitch.Value = 0f;
	}

	protected override void RenderCore()
	{
		base.RenderCore();

		if (DisconnectUI != null)
		{
			DisconnectUI.Render();
		}
	}

	public static void Freeze(float time)
	{
		if (Engine.FreezeTimer < time)
		{
			Engine.FreezeTimer = time;

			if (Engine.Scene != null)
			{
				Engine.Scene.Tracker.GetEntity<CassetteBlockManager>()?.AdvanceMusic(time);
			}
		}
	}

	// =========================================================
	// MAIN (corrigido: remove SteamAPI.Init/RestartAppIfNecessary)
	// =========================================================
	private static void Main(string[] args)
	{
		Celeste celeste;

		try
		{
			_mainThreadId = Thread.CurrentThread.ManagedThreadId;

			// Android/Linux: sem Steam. Inicializa settings e continua.
			Settings.Initialize();

			if (!Settings.Existed)
			{
				Settings.Instance.Language = "english";
			}

			for (int i = 0; i < args.Length - 1; i++)
			{
				if (args[i] == "--language" || args[i] == "-l")
				{
					Settings.Instance.Language = args[++i];
				}
				else if (args[i] == "--default-language" || args[i] == "-dl")
				{
					if (!Settings.Existed)
					{
						Settings.Instance.Language = args[++i];
					}
				}
				else if (args[i] == "--gui" || args[i] == "-g")
				{
					Input.OverrideInputPrefix = args[++i];
				}
			}

			celeste = new Celeste();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			ErrorLog.Write(ex);

			try
			{
				ErrorLog.Open();
				return;
			}
			catch
			{
				Console.WriteLine("Failed to open the log!");
				return;
			}
		}

		celeste.RunWithLogging();
		RunThread.WaitAll();
		celeste.Dispose();
		Audio.Unload();
	}

	public static void ReloadAssets(bool levels, bool graphics, bool hires, AreaKey? area = null)
	{
		if (levels)
		{
			ReloadLevels(area);
		}

		if (graphics)
		{
			ReloadGraphics(hires);
		}
	}

	public static void ReloadLevels(AreaKey? area = null)
	{
	}

	public static void ReloadGraphics(bool hires)
	{
	}

	public static void ReloadPortraits()
	{
	}

	public static void ReloadDialog()
	{
	}

	private static void CallProcess(string path, string args = "", bool createWindow = false)
	{
		Process process = new Process();

		process.StartInfo = new ProcessStartInfo
		{
			FileName = path,
			WorkingDirectory = Path.GetDirectoryName(path),
			RedirectStandardOutput = false,
			CreateNoWindow = !createWindow,
			UseShellExecute = false,
			Arguments = args
		};

		process.Start();
		process.WaitForExit();
	}

	public static bool PauseAnywhere()
	{
		if (Engine.Scene is Level)
		{
			Level level = Engine.Scene as Level;

			if (level.CanPause)
			{
				level.Pause();
				return true;
			}
		}
		else if (Engine.Scene is Emulator)
		{
			Emulator emulator = Engine.Scene as Emulator;

			if (emulator.CanPause)
			{
				emulator.CreatePauseMenu();
				return true;
			}
		}
		else if (Engine.Scene is IntroVignette)
		{
			IntroVignette introVignette = Engine.Scene as IntroVignette;

			if (introVignette.CanPause)
			{
				introVignette.OpenMenu();
				return true;
			}
		}
		else if (Engine.Scene is CoreVignette)
		{
			CoreVignette coreVignette = Engine.Scene as CoreVignette;

			if (coreVignette.CanPause)
			{
				coreVignette.OpenMenu();
				return true;
			}
		}

		return false;
	}
}