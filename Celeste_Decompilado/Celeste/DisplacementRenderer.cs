using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class DisplacementRenderer : Renderer
{
	public class Burst
	{
		public MTexture Texture;

		public Entity Follow;

		public Vector2 Position;

		public Vector2 Origin;

		public float Duration;

		public float Percent;

		public float ScaleFrom;

		public float ScaleTo = 1f;

		public Ease.Easer ScaleEaser;

		public float AlphaFrom = 1f;

		public float AlphaTo;

		public Ease.Easer AlphaEaser;

		public Rectangle? WorldClipRect;

		public Collider WorldClipCollider;

		public int WorldClipPadding;

		public Burst(MTexture texture, Vector2 position, Vector2 origin, float duration)
		{
			Texture = texture;
			Position = position;
			Origin = origin;
			Duration = duration;
		}

		public void Update()
		{
			Percent += Engine.DeltaTime / Duration;
		}

		public void Render()
		{
			Vector2 position = Position;
			if (Follow != null)
			{
				position += Follow.Position;
			}
			float num = ((AlphaEaser == null) ? (AlphaFrom + (AlphaTo - AlphaFrom) * Percent) : (AlphaFrom + (AlphaTo - AlphaFrom) * AlphaEaser(Percent)));
			float num2 = ((ScaleEaser == null) ? (ScaleFrom + (ScaleTo - ScaleFrom) * Percent) : (ScaleFrom + (ScaleTo - ScaleFrom) * ScaleEaser(Percent)));
			Vector2 origin = Origin;
			Rectangle clip = new Rectangle(0, 0, Texture.Width, Texture.Height);
			if (WorldClipCollider != null)
			{
				WorldClipRect = WorldClipCollider.Bounds;
			}
			if (WorldClipRect.HasValue)
			{
				Rectangle value = WorldClipRect.Value;
				value.X -= 1 + WorldClipPadding;
				value.Y -= 1 + WorldClipPadding;
				value.Width += 1 + WorldClipPadding * 2;
				value.Height += 1 + WorldClipPadding * 2;
				float num3 = position.X - origin.X * num2;
				if (num3 < (float)value.Left)
				{
					int num4 = (int)(((float)value.Left - num3) / num2);
					origin.X -= num4;
					clip.X = num4;
					clip.Width -= num4;
				}
				float num5 = position.Y - origin.Y * num2;
				if (num5 < (float)value.Top)
				{
					int num6 = (int)(((float)value.Top - num5) / num2);
					origin.Y -= num6;
					clip.Y = num6;
					clip.Height -= num6;
				}
				float num7 = position.X + ((float)Texture.Width - origin.X) * num2;
				if (num7 > (float)value.Right)
				{
					int num8 = (int)((num7 - (float)value.Right) / num2);
					clip.Width -= num8;
				}
				float num9 = position.Y + ((float)Texture.Height - origin.Y) * num2;
				if (num9 > (float)value.Bottom)
				{
					int num10 = (int)((num9 - (float)value.Bottom) / num2);
					clip.Height -= num10;
				}
			}
			Texture.Draw(position, origin, Color.White * num, Vector2.One * num2, 0f, clip);
		}
	}

	public bool Enabled = true;

	private float timer;

	private List<Burst> points = new List<Burst>();

	public bool HasDisplacement(Scene scene)
	{
		if (points.Count <= 0 && scene.Tracker.GetComponent<DisplacementRenderHook>() == null)
		{
			return (scene as Level).Foreground.Get<HeatWave>() != null;
		}
		return true;
	}

	public Burst Add(Burst point)
	{
		points.Add(point);
		return point;
	}

	public Burst Remove(Burst point)
	{
		points.Remove(point);
		return point;
	}

	public Burst AddBurst(Vector2 position, float duration, float radiusFrom, float radiusTo, float alpha = 1f, Ease.Easer alphaEaser = null, Ease.Easer radiusEaser = null)
	{
		MTexture mTexture = GFX.Game["util/displacementcircle"];
		Burst burst = new Burst(mTexture, position, mTexture.Center, duration);
		burst.ScaleFrom = radiusFrom / (float)(mTexture.Width / 2);
		burst.ScaleTo = radiusTo / (float)(mTexture.Width / 2);
		burst.AlphaFrom = alpha;
		burst.AlphaTo = 0f;
		burst.AlphaEaser = alphaEaser;
		return Add(burst);
	}

	public override void Update(Scene scene)
	{
		timer += Engine.DeltaTime;
		for (int num = points.Count - 1; num >= 0; num--)
		{
			if (points[num].Percent >= 1f)
			{
				points.RemoveAt(num);
			}
			else
			{
				points[num].Update();
			}
		}
	}

	public void Clear()
	{
		points.Clear();
	}

	public override void BeforeRender(Scene scene)
	{
		Distort.WaterSine = timer * 16f;
		Distort.WaterCameraY = (int)Math.Floor((scene as Level).Camera.Y);
		Camera camera = (scene as Level).Camera;
		Color color = new Color(0.5f, 0.5f, 0f, 1f);
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.Displacement.Target);
		Engine.Graphics.GraphicsDevice.Clear(color);
		if (!Enabled)
		{
			return;
		}
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, camera.Matrix);
		(scene as Level).Foreground.Get<HeatWave>()?.RenderDisplacement(scene as Level);
		foreach (DisplacementRenderHook component in scene.Tracker.GetComponents<DisplacementRenderHook>())
		{
			if (component.Visible && component.RenderDisplacement != null)
			{
				component.RenderDisplacement();
			}
		}
		foreach (Burst point in points)
		{
			point.Render();
		}
		foreach (Entity entity in scene.Tracker.GetEntities<FakeWall>())
		{
			Draw.Rect(entity.X, entity.Y, entity.Width, entity.Height, color);
		}
		Draw.SpriteBatch.End();
	}
}
