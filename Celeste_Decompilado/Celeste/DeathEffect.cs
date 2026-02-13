using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class DeathEffect : Component
{
	public Vector2 Position;

	public Color Color;

	public float Percent;

	public float Duration = 0.834f;

	public Action<float> OnUpdate;

	public Action OnEnd;

	public DeathEffect(Color color, Vector2? offset = null)
		: base(active: true, visible: true)
	{
		Color = color;
		Position = (offset.HasValue ? offset.Value : Vector2.Zero);
		Percent = 0f;
	}

	public override void Update()
	{
		base.Update();
		if (Percent > 1f)
		{
			RemoveSelf();
			if (OnEnd != null)
			{
				OnEnd();
			}
		}
		Percent = Calc.Approach(Percent, 1f, Engine.DeltaTime / Duration);
		if (OnUpdate != null)
		{
			OnUpdate(Percent);
		}
	}

	public override void Render()
	{
		Draw(base.Entity.Position + Position, Color, Percent);
	}

	public static void Draw(Vector2 position, Color color, float ease)
	{
		Color color2 = ((Math.Floor(ease * 10f) % 2.0 == 0.0) ? color : Color.White);
		MTexture mTexture = GFX.Game["characters/player/hair00"];
		float num = ((ease < 0.5f) ? (0.5f + ease) : Ease.CubeOut(1f - (ease - 0.5f) * 2f));
		for (int i = 0; i < 8; i++)
		{
			Vector2 vector = Calc.AngleToVector(((float)i / 8f + ease * 0.25f) * ((float)Math.PI * 2f), Ease.CubeOut(ease) * 24f);
			mTexture.DrawCentered(position + vector + new Vector2(-1f, 0f), Color.Black, new Vector2(num, num));
			mTexture.DrawCentered(position + vector + new Vector2(1f, 0f), Color.Black, new Vector2(num, num));
			mTexture.DrawCentered(position + vector + new Vector2(0f, -1f), Color.Black, new Vector2(num, num));
			mTexture.DrawCentered(position + vector + new Vector2(0f, 1f), Color.Black, new Vector2(num, num));
		}
		for (int j = 0; j < 8; j++)
		{
			Vector2 vector2 = Calc.AngleToVector(((float)j / 8f + ease * 0.25f) * ((float)Math.PI * 2f), Ease.CubeOut(ease) * 24f);
			mTexture.DrawCentered(position + vector2, color2, new Vector2(num, num));
		}
	}
}
