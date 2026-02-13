using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class MirrorSurfaces : Entity
{
	public const int MaxMirrorOffset = 32;

	private bool hasReflections;

	private VirtualRenderTarget target;

	public MirrorSurfaces()
	{
		base.Depth = 9490;
		base.Tag = Tags.Global;
		Add(new BeforeRenderHook(BeforeRender));
	}

	public void BeforeRender()
	{
		Level level = base.Scene as Level;
		List<Component> components = base.Scene.Tracker.GetComponents<MirrorReflection>();
		List<Component> components2 = base.Scene.Tracker.GetComponents<MirrorSurface>();
		if (!(hasReflections = components2.Count > 0 && components.Count > 0))
		{
			return;
		}
		if (target == null)
		{
			target = VirtualContent.CreateRenderTarget("mirror-surfaces", 320, 180);
		}
		Matrix transformMatrix = Matrix.CreateTranslation(32f, 32f, 0f) * level.Camera.Matrix;
		components.Sort((Component a, Component b) => b.Entity.Depth - a.Entity.Depth);
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.MirrorSources);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, transformMatrix);
		foreach (MirrorReflection item in components)
		{
			if ((item.Entity.Visible || item.IgnoreEntityVisible) && item.Visible)
			{
				item.IsRendering = true;
				item.Entity.Render();
				item.IsRendering = false;
			}
		}
		Draw.SpriteBatch.End();
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.MirrorMasks);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, transformMatrix);
		foreach (MirrorSurface item2 in components2)
		{
			if (item2.Visible && item2.OnRender != null)
			{
				item2.OnRender();
			}
		}
		Draw.SpriteBatch.End();
		Engine.Graphics.GraphicsDevice.SetRenderTarget(target);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Engine.Graphics.GraphicsDevice.Textures[1] = (RenderTarget2D)GameplayBuffers.MirrorSources;
		GFX.FxMirrors.Parameters["pixel"].SetValue(new Vector2(1f / (float)GameplayBuffers.MirrorMasks.Width, 1f / (float)GameplayBuffers.MirrorMasks.Height));
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, GFX.FxMirrors, Matrix.Identity);
		Draw.SpriteBatch.Draw((RenderTarget2D)GameplayBuffers.MirrorMasks, new Vector2(-32f, -32f), Color.White);
		Draw.SpriteBatch.End();
	}

	public override void Render()
	{
		if (hasReflections)
		{
			Vector2 position = FlooredCamera();
			Draw.SpriteBatch.Draw((RenderTarget2D)target, position, Color.White * 0.5f);
		}
	}

	private Vector2 FlooredCamera()
	{
		Vector2 position = (base.Scene as Level).Camera.Position;
		position.X = (int)Math.Floor(position.X);
		position.Y = (int)Math.Floor(position.Y);
		return position;
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		Dispose();
	}

	public override void SceneEnd(Scene scene)
	{
		base.SceneEnd(scene);
		Dispose();
	}

	public void Dispose()
	{
		if (target != null && !target.IsDisposed)
		{
			target.Dispose();
		}
		target = null;
	}
}
