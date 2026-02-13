using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class LightingRenderer : Renderer
{
	private struct VertexPositionColorMaskTexture : IVertexType
	{
		public Vector3 Position;

		public Color Color;

		public Color Mask;

		public Vector2 Texcoord;

		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0), new VertexElement(16, VertexElementFormat.Color, VertexElementUsage.Color, 1), new VertexElement(20, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0));

		VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
	}

	public static BlendState GradientBlendState = new BlendState
	{
		AlphaBlendFunction = BlendFunction.Max,
		ColorBlendFunction = BlendFunction.Max,
		ColorSourceBlend = Blend.One,
		ColorDestinationBlend = Blend.One,
		AlphaSourceBlend = Blend.One,
		AlphaDestinationBlend = Blend.One
	};

	public static BlendState OccludeBlendState = new BlendState
	{
		AlphaBlendFunction = BlendFunction.Min,
		ColorBlendFunction = BlendFunction.Min,
		ColorSourceBlend = Blend.One,
		ColorDestinationBlend = Blend.One,
		AlphaSourceBlend = Blend.One,
		AlphaDestinationBlend = Blend.One
	};

	public const int TextureSize = 1024;

	public const int TextureSplit = 4;

	public const int Channels = 4;

	public const int Padding = 8;

	public const int CircleSegments = 20;

	private const int Cells = 16;

	private const int MaxLights = 64;

	private const int Radius = 128;

	private const int LightRadius = 120;

	public Color BaseColor = Color.Black;

	public float Alpha = 0.1f;

	private VertexPositionColor[] verts = new VertexPositionColor[11520];

	private VertexPositionColorMaskTexture[] resultVerts = new VertexPositionColorMaskTexture[384];

	private int[] indices = new int[11520];

	private int vertexCount;

	private int indexCount;

	private VertexLight[] lights;

	private VertexLight spotlight;

	private bool inSpotlight;

	private float nonSpotlightAlphaMultiplier = 1f;

	private Vector3[] angles = new Vector3[20];

	public LightingRenderer()
	{
		lights = new VertexLight[64];
		for (int i = 0; i < 20; i++)
		{
			angles[i] = new Vector3(Calc.AngleToVector((float)i / 20f * ((float)Math.PI * 2f), 1f), 0f);
		}
	}

	public VertexLight SetSpotlight(VertexLight light)
	{
		spotlight = light;
		inSpotlight = true;
		return light;
	}

	public void UnsetSpotlight()
	{
		inSpotlight = false;
	}

	public override void Update(Scene scene)
	{
		nonSpotlightAlphaMultiplier = Calc.Approach(nonSpotlightAlphaMultiplier, inSpotlight ? 0f : 1f, Engine.DeltaTime * 2f);
		base.Update(scene);
	}

	public override void BeforeRender(Scene scene)
	{
		Level level = scene as Level;
		Camera camera = level.Camera;
		for (int i = 0; i < 64; i++)
		{
			if (lights[i] != null && lights[i].Entity.Scene != scene)
			{
				lights[i].Index = -1;
				lights[i] = null;
			}
		}
		foreach (VertexLight component in scene.Tracker.GetComponents<VertexLight>())
		{
			if (component.Entity != null && component.Entity.Visible && component.Visible && component.Alpha > 0f && component.Color.A > 0 && component.Center.X + component.EndRadius > camera.X && component.Center.Y + component.EndRadius > camera.Y && component.Center.X - component.EndRadius < camera.X + 320f && component.Center.Y - component.EndRadius < camera.Y + 180f)
			{
				if (component.Index < 0)
				{
					component.Dirty = true;
					for (int j = 0; j < 64; j++)
					{
						if (lights[j] == null)
						{
							lights[j] = component;
							component.Index = j;
							break;
						}
					}
				}
				if (component.LastPosition != component.Position || component.LastEntityPosition != component.Entity.Position || component.Dirty)
				{
					component.LastPosition = component.Position;
					component.InSolid = false;
					foreach (Solid item in scene.CollideAll<Solid>(component.Center))
					{
						if (item.DisableLightsInside)
						{
							component.InSolid = true;
							break;
						}
					}
					if (!component.InSolid)
					{
						component.LastNonSolidPosition = component.Center;
					}
					if (component.InSolid && !component.Started)
					{
						component.InSolidAlphaMultiplier = 0f;
					}
				}
				if (component.Entity.Position != component.LastEntityPosition)
				{
					component.Dirty = true;
					component.LastEntityPosition = component.Entity.Position;
				}
				component.Started = true;
			}
			else if (component.Index >= 0)
			{
				lights[component.Index] = null;
				component.Index = -1;
				component.Started = false;
			}
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.LightBuffer);
		Engine.Instance.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
		Matrix matrix = Matrix.CreateScale(0.0009765625f) * Matrix.CreateScale(2f, -2f, 1f) * Matrix.CreateTranslation(-1f, 1f, 0f);
		ClearDirtyLights(matrix);
		DrawLightGradients(matrix);
		DrawLightOccluders(matrix, level);
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.Light);
		Engine.Graphics.GraphicsDevice.Clear(BaseColor);
		Engine.Graphics.GraphicsDevice.Textures[0] = (RenderTarget2D)GameplayBuffers.LightBuffer;
		StartDrawingPrimitives();
		for (int k = 0; k < 64; k++)
		{
			VertexLight vertexLight2 = lights[k];
			if (vertexLight2 == null)
			{
				continue;
			}
			vertexLight2.Dirty = false;
			float num = vertexLight2.Alpha * vertexLight2.InSolidAlphaMultiplier;
			if (nonSpotlightAlphaMultiplier < 1f && vertexLight2 != spotlight)
			{
				num *= nonSpotlightAlphaMultiplier;
			}
			if (num > 0f && vertexLight2.Color.A > 0 && vertexLight2.EndRadius >= 2f)
			{
				int num2 = 128;
				while (vertexLight2.EndRadius <= (float)(num2 / 2))
				{
					num2 /= 2;
				}
				DrawLight(k, vertexLight2.InSolid ? vertexLight2.LastNonSolidPosition : vertexLight2.Center, vertexLight2.Color * num, num2);
			}
		}
		if (vertexCount > 0)
		{
			GFX.DrawIndexedVertices(camera.Matrix, resultVerts, vertexCount, indices, indexCount / 3, GFX.FxLighting, BlendState.Additive);
		}
		GaussianBlur.Blur((RenderTarget2D)GameplayBuffers.Light, GameplayBuffers.TempA, GameplayBuffers.Light);
	}

	private void ClearDirtyLights(Matrix matrix)
	{
		StartDrawingPrimitives();
		for (int i = 0; i < 64; i++)
		{
			VertexLight vertexLight = lights[i];
			if (vertexLight != null && vertexLight.Dirty)
			{
				SetClear(i);
			}
		}
		if (vertexCount <= 0)
		{
			return;
		}
		Engine.Instance.GraphicsDevice.BlendState = OccludeBlendState;
		GFX.FxPrimitive.Parameters["World"].SetValue(matrix);
		foreach (EffectPass pass in GFX.FxPrimitive.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verts, 0, vertexCount, indices, 0, indexCount / 3);
		}
	}

	private void DrawLightGradients(Matrix matrix)
	{
		StartDrawingPrimitives();
		int num = 0;
		for (int i = 0; i < 64; i++)
		{
			VertexLight vertexLight = lights[i];
			if (vertexLight != null && vertexLight.Dirty)
			{
				num++;
				SetGradient(i, Calc.Clamp(vertexLight.StartRadius, 0f, 120f), Calc.Clamp(vertexLight.EndRadius, 0f, 120f));
			}
		}
		if (vertexCount <= 0)
		{
			return;
		}
		Engine.Instance.GraphicsDevice.BlendState = GradientBlendState;
		GFX.FxPrimitive.Parameters["World"].SetValue(matrix);
		foreach (EffectPass pass in GFX.FxPrimitive.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verts, 0, vertexCount, indices, 0, indexCount / 3);
		}
	}

	private void DrawLightOccluders(Matrix matrix, Level level)
	{
		StartDrawingPrimitives();
		Rectangle tileBounds = level.Session.MapData.TileBounds;
		List<Component> components = level.Tracker.GetComponents<LightOcclude>();
		List<Component> components2 = level.Tracker.GetComponents<EffectCutout>();
		foreach (LightOcclude item in components)
		{
			if (item.Visible && item.Entity.Visible)
			{
				item.RenderBounds = new Rectangle(item.Left, item.Top, item.Width, item.Height);
			}
		}
		for (int i = 0; i < 64; i++)
		{
			VertexLight vertexLight = lights[i];
			if (vertexLight == null || !vertexLight.Dirty)
			{
				continue;
			}
			Vector2 light = (vertexLight.InSolid ? vertexLight.LastNonSolidPosition : vertexLight.Center);
			Rectangle rectangle = new Rectangle((int)(light.X - vertexLight.EndRadius), (int)(light.Y - vertexLight.EndRadius), (int)vertexLight.EndRadius * 2, (int)vertexLight.EndRadius * 2);
			Vector3 center = GetCenter(i);
			Color mask = GetMask(i, 0f, 1f);
			foreach (LightOcclude item2 in components)
			{
				if (!item2.Visible || !item2.Entity.Visible || item2.Alpha <= 0f)
				{
					continue;
				}
				Rectangle renderBounds = item2.RenderBounds;
				if (renderBounds.Intersects(rectangle))
				{
					renderBounds = renderBounds.ClampTo(rectangle);
					Color mask2 = GetMask(i, 1f - item2.Alpha, 1f);
					if (renderBounds.Bottom > rectangle.Top && renderBounds.Bottom < rectangle.Center.Y)
					{
						SetOccluder(center, mask2, light, new Vector2(renderBounds.Left, renderBounds.Bottom), new Vector2(renderBounds.Right, renderBounds.Bottom));
					}
					if (renderBounds.Top < rectangle.Bottom && renderBounds.Top > rectangle.Center.Y)
					{
						SetOccluder(center, mask2, light, new Vector2(renderBounds.Left, renderBounds.Top), new Vector2(renderBounds.Right, renderBounds.Top));
					}
					if (renderBounds.Right > rectangle.Left && renderBounds.Right < rectangle.Center.X)
					{
						SetOccluder(center, mask2, light, new Vector2(renderBounds.Right, renderBounds.Top), new Vector2(renderBounds.Right, renderBounds.Bottom));
					}
					if (renderBounds.Left < rectangle.Right && renderBounds.Left > rectangle.Center.X)
					{
						SetOccluder(center, mask2, light, new Vector2(renderBounds.Left, renderBounds.Top), new Vector2(renderBounds.Left, renderBounds.Bottom));
					}
				}
			}
			int num = rectangle.Left / 8 - tileBounds.Left;
			int num2 = rectangle.Top / 8 - tileBounds.Top;
			int num3 = rectangle.Height / 8;
			int num4 = rectangle.Width / 8;
			int num5 = num + num4;
			int num6 = num2 + num3;
			for (int j = num2; j < num2 + num3 / 2; j++)
			{
				for (int k = num; k < num5; k++)
				{
					if (level.SolidsData.SafeCheck(k, j) != '0' && level.SolidsData.SafeCheck(k, j + 1) == '0')
					{
						int num7 = k;
						do
						{
							k++;
						}
						while (k < num5 && level.SolidsData.SafeCheck(k, j) != '0' && level.SolidsData.SafeCheck(k, j + 1) == '0');
						SetOccluder(center, mask, light, new Vector2(tileBounds.X + num7, tileBounds.Y + j + 1) * 8f, new Vector2(tileBounds.X + k, tileBounds.Y + j + 1) * 8f);
					}
				}
			}
			for (int l = num; l < num + num4 / 2; l++)
			{
				for (int m = num2; m < num6; m++)
				{
					if (level.SolidsData.SafeCheck(l, m) != '0' && level.SolidsData.SafeCheck(l + 1, m) == '0')
					{
						int num8 = m;
						do
						{
							m++;
						}
						while (m < num6 && level.SolidsData.SafeCheck(l, m) != '0' && level.SolidsData.SafeCheck(l + 1, m) == '0');
						SetOccluder(center, mask, light, new Vector2(tileBounds.X + l + 1, tileBounds.Y + num8) * 8f, new Vector2(tileBounds.X + l + 1, tileBounds.Y + m) * 8f);
					}
				}
			}
			for (int n = num2 + num3 / 2; n < num6; n++)
			{
				for (int num9 = num; num9 < num5; num9++)
				{
					if (level.SolidsData.SafeCheck(num9, n) != '0' && level.SolidsData.SafeCheck(num9, n - 1) == '0')
					{
						int num10 = num9;
						do
						{
							num9++;
						}
						while (num9 < num5 && level.SolidsData.SafeCheck(num9, n) != '0' && level.SolidsData.SafeCheck(num9, n - 1) == '0');
						SetOccluder(center, mask, light, new Vector2(tileBounds.X + num10, tileBounds.Y + n) * 8f, new Vector2(tileBounds.X + num9, tileBounds.Y + n) * 8f);
					}
				}
			}
			for (int num11 = num + num4 / 2; num11 < num5; num11++)
			{
				for (int num12 = num2; num12 < num6; num12++)
				{
					if (level.SolidsData.SafeCheck(num11, num12) != '0' && level.SolidsData.SafeCheck(num11 - 1, num12) == '0')
					{
						int num13 = num12;
						do
						{
							num12++;
						}
						while (num12 < num6 && level.SolidsData.SafeCheck(num11, num12) != '0' && level.SolidsData.SafeCheck(num11 - 1, num12) == '0');
						SetOccluder(center, mask, light, new Vector2(tileBounds.X + num11, tileBounds.Y + num13) * 8f, new Vector2(tileBounds.X + num11, tileBounds.Y + num12) * 8f);
					}
				}
			}
			foreach (EffectCutout item3 in components2)
			{
				if (item3.Visible && item3.Entity.Visible && !(item3.Alpha <= 0f))
				{
					Rectangle bounds = item3.Bounds;
					if (bounds.Intersects(rectangle))
					{
						bounds = bounds.ClampTo(rectangle);
						Color mask3 = GetMask(i, 1f - item3.Alpha, 1f);
						SetCutout(center, mask3, light, bounds.X, bounds.Y, bounds.Width, bounds.Height);
					}
				}
			}
			for (int num14 = num; num14 < num5; num14++)
			{
				for (int num15 = num2; num15 < num6; num15++)
				{
					if (level.FgTilesLightMask.Tiles.SafeCheck(num14, num15) != null)
					{
						SetCutout(center, mask, light, (tileBounds.X + num14) * 8, (tileBounds.Y + num15) * 8, 8f, 8f);
					}
				}
			}
		}
		if (vertexCount <= 0)
		{
			return;
		}
		Engine.Instance.GraphicsDevice.BlendState = OccludeBlendState;
		GFX.FxPrimitive.Parameters["World"].SetValue(matrix);
		foreach (EffectPass pass in GFX.FxPrimitive.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Instance.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verts, 0, vertexCount, indices, 0, indexCount / 3);
		}
	}

	private Color GetMask(int index, float maskOn, float maskOff)
	{
		int num = index / 16;
		return new Color((num == 0) ? maskOn : maskOff, (num == 1) ? maskOn : maskOff, (num == 2) ? maskOn : maskOff, (num == 3) ? maskOn : maskOff);
	}

	private Vector3 GetCenter(int index)
	{
		int num = index % 16;
		return new Vector3(128f * ((float)(num % 4) + 0.5f) * 2f, 128f * ((float)(num / 4) + 0.5f) * 2f, 0f);
	}

	private void StartDrawingPrimitives()
	{
		vertexCount = 0;
		indexCount = 0;
	}

	private void SetClear(int index)
	{
		Vector3 center = GetCenter(index);
		Color mask = GetMask(index, 0f, 1f);
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = vertexCount + 1;
		indices[indexCount++] = vertexCount + 2;
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = vertexCount + 2;
		indices[indexCount++] = vertexCount + 3;
		verts[vertexCount].Position = center + new Vector3(-128f, -128f, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(128f, -128f, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(128f, 128f, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(-128f, 128f, 0f);
		verts[vertexCount++].Color = mask;
	}

	private void SetGradient(int index, float startFade, float endFade)
	{
		Vector3 center = GetCenter(index);
		Color mask = GetMask(index, 1f, 0f);
		int num = vertexCount;
		verts[vertexCount].Position = center;
		verts[vertexCount].Color = mask;
		vertexCount++;
		for (int i = 0; i < 20; i++)
		{
			verts[vertexCount].Position = center + angles[i] * startFade;
			verts[vertexCount].Color = mask;
			vertexCount++;
			verts[vertexCount].Position = center + angles[i] * endFade;
			verts[vertexCount].Color = Color.Transparent;
			vertexCount++;
			int num2 = i;
			int num3 = (i + 1) % 20;
			indices[indexCount++] = num;
			indices[indexCount++] = num + 1 + num2 * 2;
			indices[indexCount++] = num + 1 + num3 * 2;
			indices[indexCount++] = num + 1 + num2 * 2;
			indices[indexCount++] = num + 2 + num2 * 2;
			indices[indexCount++] = num + 2 + num3 * 2;
			indices[indexCount++] = num + 1 + num2 * 2;
			indices[indexCount++] = num + 2 + num3 * 2;
			indices[indexCount++] = num + 1 + num3 * 2;
		}
	}

	private void SetOccluder(Vector3 center, Color mask, Vector2 light, Vector2 edgeA, Vector2 edgeB)
	{
		Vector2 vector = (edgeA - light).Floor();
		Vector2 vector2 = (edgeB - light).Floor();
		float num = vector.Angle();
		float num2 = vector2.Angle();
		int num3 = vertexCount;
		verts[vertexCount].Position = center + new Vector3(vector, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(vector2, 0f);
		verts[vertexCount++].Color = mask;
		while (num != num2)
		{
			verts[vertexCount].Position = center + new Vector3(Calc.AngleToVector(num, 128f), 0f);
			verts[vertexCount].Color = mask;
			indices[indexCount++] = num3;
			indices[indexCount++] = vertexCount;
			indices[indexCount++] = vertexCount + 1;
			vertexCount++;
			num = Calc.AngleApproach(num, num2, (float)Math.PI / 4f);
		}
		verts[vertexCount].Position = center + new Vector3(Calc.AngleToVector(num, 128f), 0f);
		verts[vertexCount].Color = mask;
		indices[indexCount++] = num3;
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = num3 + 1;
		vertexCount++;
	}

	private void SetCutout(Vector3 center, Color mask, Vector2 light, float x, float y, float width, float height)
	{
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = vertexCount + 1;
		indices[indexCount++] = vertexCount + 2;
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = vertexCount + 2;
		indices[indexCount++] = vertexCount + 3;
		verts[vertexCount].Position = center + new Vector3(x - light.X, y - light.Y, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(x + width - light.X, y - light.Y, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(x + width - light.X, y + height - light.Y, 0f);
		verts[vertexCount++].Color = mask;
		verts[vertexCount].Position = center + new Vector3(x - light.X, y + height - light.Y, 0f);
		verts[vertexCount++].Color = mask;
	}

	private void DrawLight(int index, Vector2 position, Color color, float radius)
	{
		Vector3 center = GetCenter(index);
		Color mask = GetMask(index, 1f, 0f);
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = vertexCount + 1;
		indices[indexCount++] = vertexCount + 2;
		indices[indexCount++] = vertexCount;
		indices[indexCount++] = vertexCount + 2;
		indices[indexCount++] = vertexCount + 3;
		resultVerts[vertexCount].Position = new Vector3(position + new Vector2(0f - radius, 0f - radius), 0f);
		resultVerts[vertexCount].Color = color;
		resultVerts[vertexCount].Mask = mask;
		resultVerts[vertexCount++].Texcoord = new Vector2(center.X - radius, center.Y - radius) / 1024f;
		resultVerts[vertexCount].Position = new Vector3(position + new Vector2(radius, 0f - radius), 0f);
		resultVerts[vertexCount].Color = color;
		resultVerts[vertexCount].Mask = mask;
		resultVerts[vertexCount++].Texcoord = new Vector2(center.X + radius, center.Y - radius) / 1024f;
		resultVerts[vertexCount].Position = new Vector3(position + new Vector2(radius, radius), 0f);
		resultVerts[vertexCount].Color = color;
		resultVerts[vertexCount].Mask = mask;
		resultVerts[vertexCount++].Texcoord = new Vector2(center.X + radius, center.Y + radius) / 1024f;
		resultVerts[vertexCount].Position = new Vector3(position + new Vector2(0f - radius, radius), 0f);
		resultVerts[vertexCount].Color = color;
		resultVerts[vertexCount].Mask = mask;
		resultVerts[vertexCount++].Texcoord = new Vector2(center.X - radius, center.Y + radius) / 1024f;
	}

	public override void Render(Scene scene)
	{
		GFX.FxDither.CurrentTechnique = GFX.FxDither.Techniques["InvertDither"];
		GFX.FxDither.Parameters["size"].SetValue(new Vector2(GameplayBuffers.Light.Width, GameplayBuffers.Light.Height));
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, GFX.DestinationTransparencySubtract, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, GFX.FxDither, Matrix.Identity);
		Draw.SpriteBatch.Draw((RenderTarget2D)GameplayBuffers.Light, Vector2.Zero, Color.White * MathHelper.Clamp(Alpha, 0f, 1f));
		Draw.SpriteBatch.End();
	}
}
