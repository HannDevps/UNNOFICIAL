using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class StarJumpBlock : Solid
{
	private Level level;

	private bool sinks;

	private float startY;

	private float yLerp;

	private float sinkTimer;

	public StarJumpBlock(Vector2 position, float width, float height, bool sinks)
		: base(position, width, height, safe: false)
	{
		base.Depth = -10000;
		this.sinks = sinks;
		Add(new LightOcclude());
		startY = base.Y;
		SurfaceSoundIndex = 32;
	}

	public StarJumpBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Bool("sinks"))
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		level = SceneAs<Level>();
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("objects/starjumpBlock/leftrailing");
		List<MTexture> atlasSubtextures2 = GFX.Game.GetAtlasSubtextures("objects/starjumpBlock/railing");
		List<MTexture> atlasSubtextures3 = GFX.Game.GetAtlasSubtextures("objects/starjumpBlock/rightrailing");
		List<MTexture> atlasSubtextures4 = GFX.Game.GetAtlasSubtextures("objects/starjumpBlock/edgeH");
		for (int i = 8; (float)i < base.Width - 8f; i += 8)
		{
			if (Open(i, -8f))
			{
				Image image = new Image(Calc.Random.Choose(atlasSubtextures4));
				image.CenterOrigin();
				image.Position = new Vector2(i + 4, 4f);
				Add(image);
				Image image2 = new Image(atlasSubtextures2[mod((int)(base.X + (float)i) / 8, atlasSubtextures2.Count)]);
				image2.Position = new Vector2(i, -8f);
				Add(image2);
			}
			if (Open(i, base.Height))
			{
				Image image3 = new Image(Calc.Random.Choose(atlasSubtextures4));
				image3.CenterOrigin();
				image3.Scale.Y = -1f;
				image3.Position = new Vector2(i + 4, base.Height - 4f);
				Add(image3);
			}
		}
		List<MTexture> atlasSubtextures5 = GFX.Game.GetAtlasSubtextures("objects/starjumpBlock/edgeV");
		for (int j = 8; (float)j < base.Height - 8f; j += 8)
		{
			if (Open(-8f, j))
			{
				Image image4 = new Image(Calc.Random.Choose(atlasSubtextures5));
				image4.CenterOrigin();
				image4.Scale.X = -1f;
				image4.Position = new Vector2(4f, j + 4);
				Add(image4);
			}
			if (Open(base.Width, j))
			{
				Image image5 = new Image(Calc.Random.Choose(atlasSubtextures5));
				image5.CenterOrigin();
				image5.Position = new Vector2(base.Width - 4f, j + 4);
				Add(image5);
			}
		}
		List<MTexture> atlasSubtextures6 = GFX.Game.GetAtlasSubtextures("objects/starjumpBlock/corner");
		Image image6 = null;
		if (Open(-8f, 0f) && Open(0f, -8f))
		{
			image6 = new Image(Calc.Random.Choose(atlasSubtextures6));
			image6.Scale.X = -1f;
			Image image7 = new Image(atlasSubtextures[mod((int)base.X / 8, atlasSubtextures.Count)]);
			image7.Position = new Vector2(0f, -8f);
			Add(image7);
		}
		else if (Open(-8f, 0f))
		{
			image6 = new Image(Calc.Random.Choose(atlasSubtextures5));
			image6.Scale.X = -1f;
		}
		else if (Open(0f, -8f))
		{
			image6 = new Image(Calc.Random.Choose(atlasSubtextures4));
			Image image8 = new Image(atlasSubtextures2[mod((int)base.X / 8, atlasSubtextures2.Count)]);
			image8.Position = new Vector2(0f, -8f);
			Add(image8);
		}
		image6.CenterOrigin();
		image6.Position = new Vector2(4f, 4f);
		Add(image6);
		Image image9 = null;
		if (Open(base.Width, 0f) && Open(base.Width - 8f, -8f))
		{
			image9 = new Image(Calc.Random.Choose(atlasSubtextures6));
			Image image10 = new Image(atlasSubtextures3[mod((int)(base.X + base.Width) / 8 - 1, atlasSubtextures3.Count)]);
			image10.Position = new Vector2(base.Width - 8f, -8f);
			Add(image10);
		}
		else if (Open(base.Width, 0f))
		{
			image9 = new Image(Calc.Random.Choose(atlasSubtextures5));
		}
		else if (Open(base.Width - 8f, -8f))
		{
			image9 = new Image(Calc.Random.Choose(atlasSubtextures4));
			Image image11 = new Image(atlasSubtextures2[mod((int)(base.X + base.Width) / 8 - 1, atlasSubtextures2.Count)]);
			image11.Position = new Vector2(base.Width - 8f, -8f);
			Add(image11);
		}
		image9.CenterOrigin();
		image9.Position = new Vector2(base.Width - 4f, 4f);
		Add(image9);
		Image image12 = null;
		if (Open(-8f, base.Height - 8f) && Open(0f, base.Height))
		{
			image12 = new Image(Calc.Random.Choose(atlasSubtextures6));
			image12.Scale.X = -1f;
		}
		else if (Open(-8f, base.Height - 8f))
		{
			image12 = new Image(Calc.Random.Choose(atlasSubtextures5));
			image12.Scale.X = -1f;
		}
		else if (Open(0f, base.Height))
		{
			image12 = new Image(Calc.Random.Choose(atlasSubtextures4));
		}
		image12.Scale.Y = -1f;
		image12.CenterOrigin();
		image12.Position = new Vector2(4f, base.Height - 4f);
		Add(image12);
		Image image13 = null;
		if (Open(base.Width, base.Height - 8f) && Open(base.Width - 8f, base.Height))
		{
			image13 = new Image(Calc.Random.Choose(atlasSubtextures6));
		}
		else if (Open(base.Width, base.Height - 8f))
		{
			image13 = new Image(Calc.Random.Choose(atlasSubtextures5));
		}
		else if (Open(base.Width - 8f, base.Height))
		{
			image13 = new Image(Calc.Random.Choose(atlasSubtextures4));
		}
		image13.Scale.Y = -1f;
		image13.CenterOrigin();
		image13.Position = new Vector2(base.Width - 4f, base.Height - 4f);
		Add(image13);
	}

	private int mod(int x, int m)
	{
		return (x % m + m) % m;
	}

	private bool Open(float x, float y)
	{
		return !base.Scene.CollideCheck<StarJumpBlock>(new Vector2(base.X + x + 4f, base.Y + y + 4f));
	}

	public override void Update()
	{
		base.Update();
		if (sinks)
		{
			if (HasPlayerRider())
			{
				sinkTimer = 0.1f;
			}
			else if (sinkTimer > 0f)
			{
				sinkTimer -= Engine.DeltaTime;
			}
			if (sinkTimer > 0f)
			{
				yLerp = Calc.Approach(yLerp, 1f, 1f * Engine.DeltaTime);
			}
			else
			{
				yLerp = Calc.Approach(yLerp, 0f, 1f * Engine.DeltaTime);
			}
			float y = MathHelper.Lerp(startY, startY + 12f, Ease.SineInOut(yLerp));
			MoveToY(y);
		}
	}

	public override void Render()
	{
		StarJumpController entity = base.Scene.Tracker.GetEntity<StarJumpController>();
		if (entity != null)
		{
			Vector2 vector = level.Camera.Position.Floor();
			VirtualRenderTarget blockFill = entity.BlockFill;
			Draw.SpriteBatch.Draw((RenderTarget2D)blockFill, Position, new Rectangle((int)(base.X - vector.X), (int)(base.Y - vector.Y), (int)base.Width, (int)base.Height), Color.White);
		}
		base.Render();
	}
}
