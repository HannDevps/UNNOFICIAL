using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using Celeste.Core.Platform.Interop;
using Celeste.Core.Platform.Runtime;
using Celeste;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monocle;

public class Engine : Game
{
	public enum PresentationAspectModes
	{
		Fit,
		Fill,
		Stretch
	}

	public string Title;

	public Version Version;

	public static Action OverloadGameLoop;

	private static int viewPadding = 0;

	private static bool resizing;

	private bool recoveringGraphicsResources;

	private DateTime lastGraphicsRecoveryUtc;

	private static PresentationAspectModes presentationAspectMode = PresentationAspectModes.Fit;

	private static SamplerState finalBlitSampler = SamplerState.PointClamp;

	private static bool useEdgeToEdgeOnAndroid = true;

	public static float TimeRate = 1f;

	public static float TimeRateB = 1f;

	public static float FreezeTimer;

	public static bool DashAssistFreeze;

	public static bool DashAssistFreezePress;

	public static int FPS;

	private TimeSpan counterElapsed = TimeSpan.Zero;

	private int fpsCounter;

	private static string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

	public static Color ClearColor;

	public static bool ExitOnEscapeKeypress;

	private Scene scene;

	private Scene nextScene;

	public static Matrix ScreenMatrix;

	public static Engine Instance { get; private set; }

	public static GraphicsDeviceManager Graphics { get; private set; }

	public static Commands Commands { get; private set; }

	public static Pooler Pooler { get; private set; }

	public static int Width { get; private set; }

	public static int Height { get; private set; }

	public static int ViewWidth { get; private set; }

	public static int ViewHeight { get; private set; }

	public static int ViewPadding
	{
		get
		{
			return viewPadding;
		}
		set
		{
			viewPadding = value;
			Instance.UpdateView();
		}
	}

	public static float DeltaTime { get; private set; }

	public static float RawDeltaTime { get; private set; }

	public static ulong FrameCounter { get; private set; }

	public static string ContentDirectory => CelestePathBridge.ResolveContentDirectory(Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory));

	public static Scene Scene
	{
		get
		{
			return Instance.scene;
		}
		set
		{
			Instance.nextScene = value;
		}
	}

	public static Viewport Viewport { get; private set; }

	public static PresentationAspectModes PresentationAspectMode => presentationAspectMode;

	public static SamplerState FinalBlitSampler => finalBlitSampler;

	public static bool UseEdgeToEdgeOnAndroid
	{
		get => useEdgeToEdgeOnAndroid;
		set
		{
			useEdgeToEdgeOnAndroid = value;
			if (Instance != null)
			{
				Instance.UpdateView();
			}
		}
	}

	public static void ConfigurePresentation(PresentationAspectModes aspectMode, bool useLinearFilter, bool? useEdgeToEdgeOnAndroid = null)
	{
		presentationAspectMode = aspectMode;
		finalBlitSampler = (useLinearFilter ? SamplerState.LinearClamp : SamplerState.PointClamp);
		if (useEdgeToEdgeOnAndroid.HasValue)
		{
			Engine.useEdgeToEdgeOnAndroid = useEdgeToEdgeOnAndroid.Value;
		}
		if (Instance != null)
		{
			Instance.UpdateView();
		}
	}

	public Engine(int width, int height, int windowWidth, int windowHeight, string windowTitle, bool fullscreen, bool vsync)
	{
		Instance = this;
		Title = (base.Window.Title = windowTitle);
		Width = width;
		Height = height;
		ClearColor = Color.Black;
		base.InactiveSleepTime = new TimeSpan(0L);
		Graphics = new GraphicsDeviceManager(this);
		Graphics.DeviceReset += OnGraphicsReset;
		Graphics.DeviceCreated += OnGraphicsCreate;
		Graphics.SynchronizeWithVerticalRetrace = vsync;
		Graphics.PreferMultiSampling = false;
		Graphics.GraphicsProfile = ResolveGraphicsProfile();
		Graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
		Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
		base.Window.AllowUserResizing = true;
		base.Window.ClientSizeChanged += OnClientSizeChanged;
		if (fullscreen)
		{
			Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			Graphics.IsFullScreen = true;
		}
		else
		{
			Graphics.PreferredBackBufferWidth = windowWidth;
			Graphics.PreferredBackBufferHeight = windowHeight;
			Graphics.IsFullScreen = false;
		}
		base.Content = new ExternalContentManager(base.Services, "Content");
		base.IsMouseVisible = false;
		ExitOnEscapeKeypress = true;
		TryConfigureGcLatency();
	}

	private static GraphicsProfile ResolveGraphicsProfile()
	{
		if (OperatingSystem.IsAndroid() && AndroidRuntimePolicy.ShouldPreferReachGraphicsProfile())
		{
			CelestePathBridge.LogWarn("GPU", "Android compatibility policy enabled: using Reach graphics profile.");
			return GraphicsProfile.Reach;
		}

		if (OperatingSystem.IsAndroid())
		{
			CelestePathBridge.LogInfo("GPU", "Android compatibility policy: using HiDef graphics profile.");
		}

		return GraphicsProfile.HiDef;
	}

	private static void TryConfigureGcLatency()
	{
		try
		{
			GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
		}
		catch (PlatformNotSupportedException)
		{
			CelestePathBridge.LogWarn("RUNTIME", "GCSettings.LatencyMode is not supported on this platform; continuing with default GC mode.");
		}
		catch (Exception ex)
		{
			CelestePathBridge.LogWarn("RUNTIME", "Failed to apply GC latency mode: " + ex.Message);
		}
	}

	protected virtual void OnClientSizeChanged(object sender, EventArgs e)
	{
		if (OperatingSystem.IsAndroid())
		{
			UpdateView();
			return;
		}

		if (base.Window.ClientBounds.Width > 0 && base.Window.ClientBounds.Height > 0 && !resizing)
		{
			resizing = true;
			Graphics.PreferredBackBufferWidth = base.Window.ClientBounds.Width;
			Graphics.PreferredBackBufferHeight = base.Window.ClientBounds.Height;
			UpdateView();
			resizing = false;
		}
	}

	protected virtual void OnGraphicsReset(object sender, EventArgs e)
	{
		UpdateView();
		TryRecoverVirtualContentResources("DeviceReset");
		if (scene != null)
		{
			scene.HandleGraphicsReset();
		}
		if (nextScene != null && nextScene != scene)
		{
			nextScene.HandleGraphicsReset();
		}
	}

	protected virtual void OnGraphicsCreate(object sender, EventArgs e)
	{
		UpdateView();
		TryRecoverVirtualContentResources("DeviceCreated");
		if (scene != null)
		{
			scene.HandleGraphicsCreate();
		}
		if (nextScene != null && nextScene != scene)
		{
			nextScene.HandleGraphicsCreate();
		}
	}

	private void TryRecoverVirtualContentResources(string source)
	{
		if (!OperatingSystem.IsAndroid() || recoveringGraphicsResources)
		{
			return;
		}

		DateTime utcNow = DateTime.UtcNow;
		if (lastGraphicsRecoveryUtc != DateTime.MinValue && (utcNow - lastGraphicsRecoveryUtc).TotalMilliseconds < 250.0)
		{
			return;
		}

		recoveringGraphicsResources = true;
		try
		{
			CelestePathBridge.LogWarn("GPU", "Android graphics event '" + source + "' detected. Recreating virtual assets.");
			VirtualContent.Unload();
			VirtualContent.Reload();
		}
		catch (Exception ex)
		{
			CelestePathBridge.LogWarn("GPU", "Virtual asset recreation failed after '" + source + "': " + ex.Message);
		}
		finally
		{
			lastGraphicsRecoveryUtc = utcNow;
			recoveringGraphicsResources = false;
		}
	}

	protected override void OnActivated(object sender, EventArgs args)
	{
		base.OnActivated(sender, args);
		if (scene != null)
		{
			scene.GainFocus();
		}
	}

	protected override void OnDeactivated(object sender, EventArgs args)
	{
		base.OnDeactivated(sender, args);
		if (scene != null)
		{
			scene.LoseFocus();
		}
	}

	protected override void Initialize()
	{
		base.Initialize();
		MInput.Initialize();
		Tracker.Initialize();
		Pooler = new Pooler();
		Commands = new Commands();
	}

	protected override void LoadContent()
	{
		base.LoadContent();
		VirtualContent.Reload();
		Monocle.Draw.Initialize(base.GraphicsDevice);
	}

	protected override void UnloadContent()
	{
		base.UnloadContent();
		VirtualContent.Unload();
	}

	protected override void Update(GameTime gameTime)
	{
		RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
		DeltaTime = RawDeltaTime * TimeRate * TimeRateB;
		FrameCounter++;
		MInput.Update();
		if (ExitOnEscapeKeypress && MInput.Keyboard.Pressed(Keys.Escape))
		{
			Exit();
			return;
		}
		if (OverloadGameLoop != null)
		{
			OverloadGameLoop();
			base.Update(gameTime);
			return;
		}
		if (DashAssistFreeze)
		{
			if (Input.Dash.Check || !DashAssistFreezePress)
			{
				if (Input.Dash.Check)
				{
					DashAssistFreezePress = true;
				}
				if (this.scene != null)
				{
					this.scene.Tracker.GetEntity<PlayerDashAssist>()?.Update();
					if (this.scene is Level)
					{
						(this.scene as Level).UpdateTime();
					}
					this.scene.Entities.UpdateLists();
				}
			}
			else
			{
				DashAssistFreeze = false;
			}
		}
		if (!DashAssistFreeze)
		{
			if (FreezeTimer > 0f)
			{
				FreezeTimer = Math.Max(FreezeTimer - RawDeltaTime, 0f);
			}
			else if (this.scene != null)
			{
				this.scene.BeforeUpdate();
				this.scene.Update();
				this.scene.AfterUpdate();
			}
		}
		if (Commands.Open)
		{
			Commands.UpdateOpen();
		}
		else if (Commands.Enabled)
		{
			Commands.UpdateClosed();
		}
		if (this.scene != nextScene)
		{
			Scene scene = this.scene;
			if (this.scene != null)
			{
				this.scene.End();
			}
			this.scene = nextScene;
			OnSceneTransition(scene, nextScene);
			if (this.scene != null)
			{
				this.scene.Begin();
			}
		}
		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		RenderCore();
		base.Draw(gameTime);
		if (Commands.Open)
		{
			Commands.Render();
		}
		fpsCounter++;
		counterElapsed += gameTime.ElapsedGameTime;
		if (counterElapsed >= TimeSpan.FromSeconds(1.0))
		{
			FPS = fpsCounter;
			fpsCounter = 0;
			counterElapsed -= TimeSpan.FromSeconds(1.0);
		}
	}

	protected virtual void RenderCore()
	{
		if (scene != null)
		{
			scene.BeforeRender();
		}
		base.GraphicsDevice.SetRenderTarget(null);
		base.GraphicsDevice.Viewport = Viewport;
		base.GraphicsDevice.Clear(ClearColor);
		if (scene != null)
		{
			scene.Render();
			scene.AfterRender();
		}
	}

	protected override void OnExiting(object sender, EventArgs args)
	{
		base.OnExiting(sender, args);
		MInput.Shutdown();
	}

	public void RunWithLogging()
	{
		try
		{
			Run();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			ErrorLog.Write(ex);
			ErrorLog.Open();
		}
	}

	protected virtual void OnSceneTransition(Scene from, Scene to)
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		TimeRate = 1f;
		DashAssistFreeze = false;
	}

	public static void SetWindowed(int width, int height)
	{
		if (width > 0 && height > 0)
		{
			resizing = true;
			Graphics.PreferredBackBufferWidth = width;
			Graphics.PreferredBackBufferHeight = height;
			Graphics.IsFullScreen = false;
			Graphics.ApplyChanges();
			Console.WriteLine("WINDOW-" + width + "x" + height);
			resizing = false;
		}
	}

	public static void SetFullscreen()
	{
		resizing = true;
		Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
		Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
		Graphics.IsFullScreen = true;
		Graphics.ApplyChanges();
		Console.WriteLine("FULLSCREEN");
		resizing = false;
	}

	private void UpdateView()
	{
		float num = base.GraphicsDevice.PresentationParameters.BackBufferWidth;
		float num2 = base.GraphicsDevice.PresentationParameters.BackBufferHeight;
		if (num <= 0f || num2 <= 0f)
		{
			CelestePathBridge.LogWarn("GPU", "UpdateView skipped due invalid backbuffer size: " + num + "x" + num2 + ".");
			return;
		}

		int num3 = (int)num;
		int num4 = (int)num2;
		// Edge-to-edge no Android: ignora ViewPadding para usar 100% da tela
		if (ViewPadding > 0 && !(OperatingSystem.IsAndroid() && useEdgeToEdgeOnAndroid))
		{
			num3 = Math.Max(1, num3 - ViewPadding * 2);
			float num5 = (float)num2 / num;
			num4 = Math.Max(1, num4 - (int)(num5 * (float)ViewPadding * 2f));
		}

		int num6 = (int)((num - (float)num3) * 0.5f);
		int num7 = (int)((num2 - (float)num4) * 0.5f);
		Matrix matrix = Matrix.Identity;

		switch (presentationAspectMode)
		{
		case PresentationAspectModes.Stretch:
		{
			ViewWidth = num3;
			ViewHeight = num4;
			float x2 = (float)ViewWidth / (float)Width;
			float y2 = (float)ViewHeight / (float)Height;
			matrix = Matrix.CreateScale(x2, y2, 1f);
			break;
		}
		case PresentationAspectModes.Fill:
		{
			ViewWidth = num3;
			ViewHeight = num4;
			float num8 = Math.Max((float)ViewWidth / (float)Width, (float)ViewHeight / (float)Height);
			float x = ((float)ViewWidth - (float)Width * num8) * 0.5f;
			float y = ((float)ViewHeight - (float)Height * num8) * 0.5f;
			matrix = Matrix.CreateScale(num8, num8, 1f) * Matrix.CreateTranslation(x, y, 0f);
			break;
		}
		default:
		{
			if ((float)num3 / (float)Width > (float)num4 / (float)Height)
			{
				ViewWidth = (int)((float)num4 / (float)Height * (float)Width);
				ViewHeight = num4;
			}
			else
			{
				ViewWidth = num3;
				ViewHeight = (int)((float)num3 / (float)Width * (float)Height);
			}

			ViewWidth = Math.Max(1, ViewWidth);
			ViewHeight = Math.Max(1, ViewHeight);
			num6 += (num3 - ViewWidth) / 2;
			num7 += (num4 - ViewHeight) / 2;
			float num9 = Math.Min((float)ViewWidth / (float)Width, (float)ViewHeight / (float)Height);
			matrix = Matrix.CreateScale(num9, num9, 1f);
			break;
		}
		}

		ScreenMatrix = matrix;
		Viewport = new Viewport
		{
			X = num6,
			Y = num7,
			Width = Math.Max(1, ViewWidth),
			Height = Math.Max(1, ViewHeight),
			MinDepth = 0f,
			MaxDepth = 1f
		};

		// Logs diagnósticos quando habilitado (config geral)
		if (OperatingSystem.IsAndroid())
		{
			CelestePathBridge.LogInfo("GPU", $"UpdateView: backbuffer={num}x{num2} viewport={Viewport.X},{Viewport.Y},{Viewport.Width},{Viewport.Height} view={ViewWidth}x{ViewHeight} aspectMode={presentationAspectMode} edgeToEdge={useEdgeToEdgeOnAndroid}");
		}
	}
}
