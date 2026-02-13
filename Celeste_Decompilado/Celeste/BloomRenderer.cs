using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class BloomRenderer : Renderer
{
	public float Strength = 1f;

	public float Base;

	private MTexture gradient;

	public static readonly BlendState BlurredScreenToMask = new BlendState
	{
		ColorSourceBlend = Blend.One,
		ColorDestinationBlend = Blend.Zero,
		ColorBlendFunction = BlendFunction.Add,
		AlphaSourceBlend = Blend.Zero,
		AlphaDestinationBlend = Blend.One,
		AlphaBlendFunction = BlendFunction.Add
	};

	public static readonly BlendState AdditiveMaskToScreen = new BlendState
	{
		ColorSourceBlend = Blend.SourceAlpha,
		ColorDestinationBlend = Blend.One,
		ColorBlendFunction = BlendFunction.Add,
		AlphaSourceBlend = Blend.Zero,
		AlphaDestinationBlend = Blend.One,
		AlphaBlendFunction = BlendFunction.Add
	};

	public static readonly BlendState CutoutBlendstate = new BlendState
	{
		ColorSourceBlend = Blend.One,
		ColorDestinationBlend = Blend.One,
		AlphaSourceBlend = Blend.One,
		AlphaDestinationBlend = Blend.One,
		ColorBlendFunction = BlendFunction.Min,
		AlphaBlendFunction = BlendFunction.Min
	};

	public BloomRenderer()
	{
		gradient = GFX.Game["util/bloomgradient"];
	}

	public void Apply(VirtualRenderTarget target, Scene scene)
	{
		if (!(Strength > 0f))
		{
			return;
		}
		VirtualRenderTarget tempA = GameplayBuffers.TempA;
		Texture2D texture = GaussianBlur.Blur((RenderTarget2D)target, GameplayBuffers.TempA, GameplayBuffers.TempB);
		List<Component> components = scene.Tracker.GetComponents<BloomPoint>();
		List<Component> components2 = scene.Tracker.GetComponents<EffectCutout>();
		Engine.Instance.GraphicsDevice.SetRenderTarget(tempA);
		Engine.Instance.GraphicsDevice.Clear(Color.Transparent);
		if (Base < 1f)
		{
			Camera camera = (scene as Level).Camera;
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, camera.Matrix);
			float num = 1f / (float)gradient.Width;
			foreach (Component item in components)
			{
				BloomPoint bloomPoint = item as BloomPoint;
				if (bloomPoint.Visible && !(bloomPoint.Radius <= 0f) && !(bloomPoint.Alpha <= 0f))
				{
					gradient.DrawCentered(bloomPoint.Entity.Position + bloomPoint.Position, Color.White * bloomPoint.Alpha, bloomPoint.Radius * 2f * num);
				}
			}
			foreach (CustomBloom component in scene.Tracker.GetComponents<CustomBloom>())
			{
				if (component.Visible && component.OnRenderBloom != null)
				{
					component.OnRenderBloom();
				}
			}
			foreach (Entity entity in scene.Tracker.GetEntities<SeekerBarrier>())
			{
				Draw.Rect(entity.Collider, Color.White);
			}
			Draw.SpriteBatch.End();
			if (components2.Count > 0)
			{
				Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, CutoutBlendstate, SamplerState.PointClamp, null, null, null, camera.Matrix);
				foreach (Component item2 in components2)
				{
					EffectCutout effectCutout = item2 as EffectCutout;
					if (effectCutout.Visible)
					{
						Draw.Rect(effectCutout.Left, effectCutout.Top, effectCutout.Right - effectCutout.Left, effectCutout.Bottom - effectCutout.Top, Color.White * (1f - effectCutout.Alpha));
					}
				}
				Draw.SpriteBatch.End();
			}
		}
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
		Draw.Rect(-10f, -10f, 340f, 200f, Color.White * Base);
		Draw.SpriteBatch.End();
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlurredScreenToMask);
		Draw.SpriteBatch.Draw(texture, Vector2.Zero, Color.White);
		Draw.SpriteBatch.End();
		Engine.Instance.GraphicsDevice.SetRenderTarget(target);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, AdditiveMaskToScreen);
		for (int i = 0; (float)i < Strength; i++)
		{
			float num2 = (((float)i < Strength - 1f) ? 1f : (Strength - (float)i));
			Draw.SpriteBatch.Draw((RenderTarget2D)tempA, Vector2.Zero, Color.White * num2);
		}
		Draw.SpriteBatch.End();
	}
}
