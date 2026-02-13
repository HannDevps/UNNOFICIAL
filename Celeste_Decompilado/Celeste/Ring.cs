using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class Ring
{
	public VertexPositionColorTexture[] Verts;

	public VirtualTexture Texture;

	public Color TopColor;

	public Color BotColor;

	public Ring(float top, float bottom, float distance, float wavy, int steps, Color color, VirtualTexture texture, float texScale = 1f)
		: this(top, bottom, distance, wavy, steps, color, color, texture, texScale)
	{
	}

	public Ring(float top, float bottom, float distance, float wavy, int steps, Color topColor, Color botColor, VirtualTexture texture, float texScale = 1f)
	{
		Texture = texture;
		TopColor = topColor;
		BotColor = botColor;
		Verts = new VertexPositionColorTexture[steps * 24];
		float y = (1f - texScale) * 0.5f + 0.01f;
		float y2 = 1f - (1f - texScale) * 0.5f;
		for (int i = 0; i < steps; i++)
		{
			float num = (float)(i - 1) / (float)steps;
			float num2 = (float)i / (float)steps;
			Vector2 vector = Calc.AngleToVector(num * ((float)Math.PI * 2f), distance);
			Vector2 vector2 = Calc.AngleToVector(num2 * ((float)Math.PI * 2f), distance);
			float num3 = 0f;
			float num4 = 0f;
			if (wavy > 0f)
			{
				num3 = (float)Math.Sin(num * ((float)Math.PI * 2f) * 3f + wavy) * Math.Abs(top - bottom) * 0.4f;
				num4 = (float)Math.Sin(num2 * ((float)Math.PI * 2f) * 3f + wavy) * Math.Abs(top - bottom) * 0.4f;
			}
			int num5 = i * 6;
			Verts[num5].Color = topColor;
			Verts[num5].TextureCoordinate = new Vector2(num * texScale, y);
			Verts[num5].Position = new Vector3(vector.X, top + num3, vector.Y);
			Verts[num5 + 1].Color = topColor;
			Verts[num5 + 1].TextureCoordinate = new Vector2(num2 * texScale, y);
			Verts[num5 + 1].Position = new Vector3(vector2.X, top + num4, vector2.Y);
			Verts[num5 + 2].Color = botColor;
			Verts[num5 + 2].TextureCoordinate = new Vector2(num2 * texScale, y2);
			Verts[num5 + 2].Position = new Vector3(vector2.X, bottom + num4, vector2.Y);
			Verts[num5 + 3].Color = topColor;
			Verts[num5 + 3].TextureCoordinate = new Vector2(num * texScale, y);
			Verts[num5 + 3].Position = new Vector3(vector.X, top + num3, vector.Y);
			Verts[num5 + 4].Color = botColor;
			Verts[num5 + 4].TextureCoordinate = new Vector2(num2 * texScale, y2);
			Verts[num5 + 4].Position = new Vector3(vector2.X, bottom + num4, vector2.Y);
			Verts[num5 + 5].Color = botColor;
			Verts[num5 + 5].TextureCoordinate = new Vector2(num * texScale, y2);
			Verts[num5 + 5].Position = new Vector3(vector.X, bottom + num3, vector.Y);
		}
	}

	public void Rotate(float amount)
	{
		for (int i = 0; i < Verts.Length; i++)
		{
			Verts[i].TextureCoordinate.X += amount;
		}
	}

	public void Draw(Matrix matrix, RasterizerState rstate = null, float alpha = 1f)
	{
		Engine.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
		Engine.Graphics.GraphicsDevice.RasterizerState = ((rstate == null) ? MountainModel.CullCCRasterizer : rstate);
		Engine.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
		Engine.Graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
		Engine.Graphics.GraphicsDevice.Textures[0] = Texture.Texture;
		Color color = TopColor * alpha;
		Color color2 = BotColor * alpha;
		for (int i = 0; i < Verts.Length; i += 6)
		{
			Verts[i].Color = color;
			Verts[i + 1].Color = color;
			Verts[i + 2].Color = color2;
			Verts[i + 3].Color = color;
			Verts[i + 4].Color = color2;
			Verts[i + 5].Color = color2;
		}
		GFX.FxTexture.Parameters["World"].SetValue(matrix);
		foreach (EffectPass pass in GFX.FxTexture.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Verts, 0, Verts.Length / 3);
		}
	}
}
