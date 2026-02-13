using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monocle;

public struct Particle
{
	public Entity Track;

	public ParticleType Type;

	public MTexture Source;

	public bool Active;

	public Color Color;

	public Color StartColor;

	public Vector2 Position;

	public Vector2 Speed;

	public float Size;

	public float StartSize;

	public float Life;

	public float StartLife;

	public float ColorSwitch;

	public float Rotation;

	public float Spin;

	public bool SimulateFor(float duration)
	{
		if (duration > Life)
		{
			Life = 0f;
			Active = false;
			return false;
		}
		float num = Engine.TimeRate * ((float)Engine.Instance.TargetElapsedTime.Milliseconds / 1000f);
		if (num > 0f)
		{
			for (float num2 = 0f; num2 < duration; num2 += num)
			{
				Update(num);
			}
		}
		return true;
	}

	public void Update(float? delta = null)
	{
		float num = 0f;
		num = ((!delta.HasValue) ? (Type.UseActualDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime) : delta.Value);
		float num2 = Life / StartLife;
		Life -= num;
		if (Life <= 0f)
		{
			Active = false;
			return;
		}
		if (Type.RotationMode == ParticleType.RotationModes.SameAsDirection)
		{
			if (Speed != Vector2.Zero)
			{
				Rotation = Speed.Angle();
			}
		}
		else
		{
			Rotation += Spin * num;
		}
		float num3 = ((Type.FadeMode == ParticleType.FadeModes.Linear) ? num2 : ((Type.FadeMode == ParticleType.FadeModes.Late) ? Math.Min(1f, num2 / 0.25f) : ((Type.FadeMode != ParticleType.FadeModes.InAndOut) ? 1f : ((num2 > 0.75f) ? (1f - (num2 - 0.75f) / 0.25f) : ((!(num2 < 0.25f)) ? 1f : (num2 / 0.25f))))));
		if (num3 == 0f)
		{
			Color = Color.Transparent;
		}
		else
		{
			if (Type.ColorMode == ParticleType.ColorModes.Static)
			{
				Color = StartColor;
			}
			else if (Type.ColorMode == ParticleType.ColorModes.Fade)
			{
				Color = Color.Lerp(Type.Color2, StartColor, num2);
			}
			else if (Type.ColorMode == ParticleType.ColorModes.Blink)
			{
				Color = (Calc.BetweenInterval(Life, 0.1f) ? StartColor : Type.Color2);
			}
			else if (Type.ColorMode == ParticleType.ColorModes.Choose)
			{
				Color = StartColor;
			}
			if (num3 < 1f)
			{
				Color *= num3;
			}
		}
		Position += Speed * num;
		Speed += Type.Acceleration * num;
		Speed = Calc.Approach(Speed, Vector2.Zero, Type.Friction * num);
		if (Type.SpeedMultiplier != 1f)
		{
			Speed *= (float)Math.Pow(Type.SpeedMultiplier, num);
		}
		if (Type.ScaleOut)
		{
			Size = StartSize * Ease.CubeOut(num2);
		}
	}

	public void Render()
	{
		Vector2 position = new Vector2((int)Position.X, (int)Position.Y);
		if (Track != null)
		{
			position += Track.Position;
		}
		Draw.SpriteBatch.Draw(Source.Texture.Texture, position, Source.ClipRect, Color, Rotation, Source.Center, Size, SpriteEffects.None, 0f);
	}

	public void Render(float alpha)
	{
		Vector2 position = new Vector2((int)Position.X, (int)Position.Y);
		if (Track != null)
		{
			position += Track.Position;
		}
		Draw.SpriteBatch.Draw(Source.Texture.Texture, position, Source.ClipRect, Color * alpha, Rotation, Source.Center, Size, SpriteEffects.None, 0f);
	}
}
