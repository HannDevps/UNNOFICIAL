using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class Parallax : Backdrop
{
	public Vector2 CameraOffset = Vector2.Zero;

	public BlendState BlendState = BlendState.AlphaBlend;

	public MTexture Texture;

	public bool DoFadeIn;

	public float Alpha = 1f;

	private float fadeIn = 1f;

	public Parallax(MTexture texture)
	{
		Name = texture.AtlasPath;
		Texture = texture;
	}

	public override void Update(Scene scene)
	{
		base.Update(scene);
		Position += Speed * Engine.DeltaTime;
		Position += WindMultiplier * (scene as Level).Wind * Engine.DeltaTime;
		if (DoFadeIn)
		{
			fadeIn = Calc.Approach(fadeIn, Visible ? 1 : 0, Engine.DeltaTime);
		}
		else
		{
			fadeIn = (Visible ? 1 : 0);
		}
	}

	public override void Render(Scene scene)
	{
		Vector2 vector = ((scene as Level).Camera.Position + CameraOffset).FloorV2();
		Vector2 vector2 = (Position - vector * Scroll).FloorV2();
		float num = fadeIn * Alpha * FadeAlphaMultiplier;
		if (FadeX != null)
		{
			num *= FadeX.Value(vector.X + 160f);
		}
		if (FadeY != null)
		{
			num *= FadeY.Value(vector.Y + 90f);
		}
		Color color = Color;
		if (num < 1f)
		{
			color *= num;
		}
		if (color.A <= 1)
		{
			return;
		}
		if (LoopX)
		{
			while (vector2.X < 0f)
			{
				vector2.X += Texture.Width;
			}
			while (vector2.X > 0f)
			{
				vector2.X -= Texture.Width;
			}
		}
		if (LoopY)
		{
			while (vector2.Y < 0f)
			{
				vector2.Y += Texture.Height;
			}
			while (vector2.Y > 0f)
			{
				vector2.Y -= Texture.Height;
			}
		}
		SpriteEffects flip = SpriteEffects.None;
		if (FlipX && FlipY)
		{
			flip = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
		}
		else if (FlipX)
		{
			flip = SpriteEffects.FlipHorizontally;
		}
		else if (FlipY)
		{
			flip = SpriteEffects.FlipVertically;
		}
		for (float num2 = vector2.X; num2 < 320f; num2 += (float)Texture.Width)
		{
			for (float num3 = vector2.Y; num3 < 180f; num3 += (float)Texture.Height)
			{
				Texture.Draw(new Vector2(num2, num3), Vector2.Zero, color, 1f, 0f, flip);
				if (!LoopY)
				{
					break;
				}
			}
			if (!LoopX)
			{
				break;
			}
		}
	}
}
