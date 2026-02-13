using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class SinkingPlatform : JumpThru
{
	private float speed;

	private float startY;

	private float riseTimer;

	private MTexture[] textures;

	private Shaker shaker;

	private SoundSource downSfx;

	private SoundSource upSfx;

	public SinkingPlatform(Vector2 position, int width)
		: base(position, width, safe: false)
	{
		startY = base.Y;
		base.Depth = 1;
		SurfaceSoundIndex = 15;
		Add(shaker = new Shaker(on: false));
		Add(new LightOcclude(0.2f));
		Add(downSfx = new SoundSource());
		Add(upSfx = new SoundSource());
	}

	public SinkingPlatform(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		MTexture mTexture = GFX.Game["objects/woodPlatform/" + AreaData.Get(scene).WoodPlatform];
		textures = new MTexture[mTexture.Width / 8];
		for (int i = 0; i < textures.Length; i++)
		{
			textures[i] = mTexture.GetSubtexture(i * 8, 0, 8, 8);
		}
		scene.Add(new SinkingPlatformLine(Position + new Vector2(base.Width / 2f, base.Height / 2f)));
	}

	public override void Render()
	{
		Vector2 value = shaker.Value;
		textures[0].Draw(Position + value);
		for (int i = 8; (float)i < base.Width - 8f; i += 8)
		{
			textures[1].Draw(Position + value + new Vector2(i, 0f));
		}
		textures[3].Draw(Position + value + new Vector2(base.Width - 8f, 0f));
		textures[2].Draw(Position + value + new Vector2(base.Width / 2f - 4f, 0f));
	}

	public override void Update()
	{
		base.Update();
		Player playerRider = GetPlayerRider();
		if (playerRider != null)
		{
			if (riseTimer <= 0f)
			{
				if (base.ExactPosition.Y <= startY)
				{
					Audio.Play("event:/game/03_resort/platform_vert_start", Position);
				}
				shaker.ShakeFor(0.15f, removeOnFinish: false);
			}
			riseTimer = 0.1f;
			speed = Calc.Approach(speed, playerRider.Ducking ? 60f : 30f, 400f * Engine.DeltaTime);
		}
		else if (riseTimer > 0f)
		{
			riseTimer -= Engine.DeltaTime;
			speed = Calc.Approach(speed, 45f, 600f * Engine.DeltaTime);
		}
		else
		{
			speed = Calc.Approach(speed, -50f, 400f * Engine.DeltaTime);
		}
		if (speed > 0f)
		{
			if (!downSfx.Playing)
			{
				downSfx.Play("event:/game/03_resort/platform_vert_down_loop");
			}
			downSfx.Param("ducking", (playerRider != null && playerRider.Ducking) ? 1 : 0);
			if (upSfx.Playing)
			{
				upSfx.Stop();
			}
			MoveV(speed * Engine.DeltaTime);
		}
		else if (speed < 0f && base.ExactPosition.Y > startY)
		{
			if (!upSfx.Playing)
			{
				upSfx.Play("event:/game/03_resort/platform_vert_up_loop");
			}
			if (downSfx.Playing)
			{
				downSfx.Stop();
			}
			MoveTowardsY(startY, (0f - speed) * Engine.DeltaTime);
			if (base.ExactPosition.Y <= startY)
			{
				upSfx.Stop();
				Audio.Play("event:/game/03_resort/platform_vert_end", Position);
				shaker.ShakeFor(0.1f, removeOnFinish: false);
			}
		}
	}
}
