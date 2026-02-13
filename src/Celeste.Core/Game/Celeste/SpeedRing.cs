using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

[Pooled]
[Tracked(false)]
public class SpeedRing : Entity
{
	private int index;

	private float angle;

	private float lerp;

	private Color color;

	private Vector2 normal;

	public SpeedRing Init(Vector2 position, float angle, Color color)
	{
		Position = position;
		this.angle = angle;
		this.color = color;
		lerp = 0f;
		normal = Calc.AngleToVector(angle, 1f);
		return this;
	}

	public override void Update()
	{
		lerp += 3f * Engine.DeltaTime;
		Position += normal * 10f * Engine.DeltaTime;
		if (lerp >= 1f)
		{
			RemoveSelf();
		}
	}

	public override void Render()
	{
		Color color = this.color * MathHelper.Lerp(0.6f, 0f, lerp);
		if (color.A > 0)
		{
			Draw.SpriteBatch.Draw((RenderTarget2D)GameplayBuffers.SpeedRings, Position + new Vector2(-32f, -32f), new Rectangle(index % 4 * 64, index / 4 * 64, 64, 64), color);
		}
	}

	private void DrawRing(Vector2 position)
	{
		float maxRadius = MathHelper.Lerp(4f, 14f, lerp);
		Vector2 vector = GetVectorAtAngle(0f, maxRadius);
		for (int i = 1; i <= 8; i++)
		{
			float radians = (float)i * ((float)Math.PI / 8f);
			Vector2 vectorAtAngle = GetVectorAtAngle(radians, maxRadius);
			Draw.Line(position + vector, position + vectorAtAngle, Color.White);
			Draw.Line(position - vector, position - vectorAtAngle, Color.White);
			vector = vectorAtAngle;
		}
	}

	private Vector2 GetVectorAtAngle(float radians, float maxRadius)
	{
		Vector2 vector = Calc.AngleToVector(radians, 1f);
		float num = MathHelper.Lerp(maxRadius, maxRadius * 0.5f, Math.Abs(Vector2.Dot(vector, normal)));
		return vector * num;
	}

	public static void DrawToBuffer(Level level)
	{
		List<Entity> entities = level.Tracker.GetEntities<SpeedRing>();
		int num = 0;
		if (entities.Count <= 0)
		{
			return;
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.SpeedRings);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
		foreach (SpeedRing item in entities)
		{
			item.index = num;
			item.DrawRing(new Vector2(num % 4 * 64 + 32, num / 4 * 64 + 32));
			num++;
		}
		Draw.SpriteBatch.End();
	}
}
