using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class GoldenBlock : Solid
{
	private MTexture[,] nineSlice;

	private Image berry;

	private float startY;

	private float yLerp;

	private float sinkTimer;

	private float renderLerp;

	public GoldenBlock(Vector2 position, float width, float height)
		: base(position, width, height, safe: false)
	{
		startY = base.Y;
		berry = new Image(GFX.Game["collectables/goldberry/idle00"]);
		berry.CenterOrigin();
		berry.Position = new Vector2(width / 2f, height / 2f);
		MTexture mTexture = GFX.Game["objects/goldblock"];
		nineSlice = new MTexture[3, 3];
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				nineSlice[i, j] = mTexture.GetSubtexture(new Rectangle(i * 8, j * 8, 8, 8));
			}
		}
		base.Depth = -10000;
		Add(new LightOcclude());
		Add(new MirrorSurface());
		SurfaceSoundIndex = 32;
	}

	public GoldenBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height)
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Visible = false;
		Collidable = false;
		renderLerp = 1f;
		bool flag = false;
		foreach (Strawberry item in scene.Entities.FindAll<Strawberry>())
		{
			if (item.Golden && item.Follower.Leader != null)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			RemoveSelf();
		}
	}

	public override void Update()
	{
		base.Update();
		if (!Visible)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.X > base.X - 80f)
			{
				Visible = true;
				Collidable = true;
				renderLerp = 1f;
			}
		}
		if (Visible)
		{
			renderLerp = Calc.Approach(renderLerp, 0f, Engine.DeltaTime * 3f);
		}
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

	private void DrawBlock(Vector2 offset, Color color)
	{
		float num = base.Collider.Width / 8f - 1f;
		float num2 = base.Collider.Height / 8f - 1f;
		for (int i = 0; (float)i <= num; i++)
		{
			for (int j = 0; (float)j <= num2; j++)
			{
				int num3 = (((float)i < num) ? Math.Min(i, 1) : 2);
				int num4 = (((float)j < num2) ? Math.Min(j, 1) : 2);
				nineSlice[num3, num4].Draw(Position + offset + base.Shake + new Vector2(i * 8, j * 8), Vector2.Zero, color);
			}
		}
	}

	public override void Render()
	{
		Level level = base.Scene as Level;
		Vector2 vector = new Vector2(0f, ((float)level.Bounds.Bottom - startY + 32f) * Ease.CubeIn(renderLerp));
		Vector2 position = Position;
		Position += vector;
		DrawBlock(new Vector2(-1f, 0f), Color.Black);
		DrawBlock(new Vector2(1f, 0f), Color.Black);
		DrawBlock(new Vector2(0f, -1f), Color.Black);
		DrawBlock(new Vector2(0f, 1f), Color.Black);
		DrawBlock(Vector2.Zero, Color.White);
		berry.Color = Color.White;
		berry.RenderPosition = base.Center;
		berry.Render();
		Position = position;
	}
}
