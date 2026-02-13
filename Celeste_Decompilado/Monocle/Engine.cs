using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using Celeste;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monocle;

public class Engine : Game
{
	public string Title;

	public Version Version;

	public static Action OverloadGameLoop;

	private static int viewPadding = 0;

	private static bool resizing;

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

	public static string ContentDirectory => Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory);

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
		Graphics.GraphicsProfile = GraphicsProfile.HiDef;
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
		base.Content.RootDirectory = "Content";
		base.IsMouseVisible = false;
		ExitOnEscapeKeypress = true;
		GCSettings.LatencyMode = (GCLatencyMode)3;
	}

	protected virtual void OnClientSizeChanged(object sender, EventArgs e)
	{
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
		if (scene != null)
		{
			scene.HandleGraphicsCreate();
		}
		if (nextScene != null && nextScene != scene)
		{
			nextScene.HandleGraphicsCreate();
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
		if (num / (float)Width > num2 / (float)Height)
		{
			ViewWidth = (int)(num2 / (float)Height * (float)Width);
			ViewHeight = (int)num2;
		}
		else
		{
			ViewWidth = (int)num;
			ViewHeight = (int)(num / (float)Width * (float)Height);
		}
		float num3 = (float)ViewHeight / (float)ViewWidth;
		ViewWidth -= ViewPadding * 2;
		ViewHeight -= (int)(num3 * (float)ViewPadding * 2f);
		ScreenMatrix = Matrix.CreateScale((float)ViewWidth / (float)Width);
		Viewport = new Viewport
		{
			X = (int)(num / 2f - (float)(ViewWidth / 2)),
			Y = (int)(num2 / 2f - (float)(ViewHeight / 2)),
			Width = ViewWidth,
			Height = ViewHeight,
			MinDepth = 0f,
			MaxDepth = 1f
		};
	}
}
