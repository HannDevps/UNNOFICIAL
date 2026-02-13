using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public static class Glitch
{
	public static float Value;

	public static void Apply(VirtualRenderTarget source, float timer, float seed, float amplitude)
	{
		if (Value > 0f && !Settings.Instance.DisableFlashes)
		{
			Effect fxGlitch = GFX.FxGlitch;
			Vector2 value = new Vector2(Engine.Graphics.GraphicsDevice.Viewport.Width, Engine.Graphics.GraphicsDevice.Viewport.Height);
			fxGlitch.Parameters["dimensions"].SetValue(value);
			fxGlitch.Parameters["amplitude"].SetValue(amplitude);
			fxGlitch.Parameters["minimum"].SetValue(-1f);
			fxGlitch.Parameters["glitch"].SetValue(Value);
			fxGlitch.Parameters["timer"].SetValue(timer);
			fxGlitch.Parameters["seed"].SetValue(seed);
			VirtualRenderTarget tempA = GameplayBuffers.TempA;
			Engine.Instance.GraphicsDevice.SetRenderTarget(tempA);
			Engine.Instance.GraphicsDevice.Clear(Color.Transparent);
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, fxGlitch);
			Draw.SpriteBatch.Draw((RenderTarget2D)source, Vector2.Zero, Color.White);
			Draw.SpriteBatch.End();
			Engine.Instance.GraphicsDevice.SetRenderTarget(source);
			Engine.Instance.GraphicsDevice.Clear(Color.Transparent);
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, fxGlitch);
			Draw.SpriteBatch.Draw((RenderTarget2D)tempA, Vector2.Zero, Color.White);
			Draw.SpriteBatch.End();
		}
	}
}
