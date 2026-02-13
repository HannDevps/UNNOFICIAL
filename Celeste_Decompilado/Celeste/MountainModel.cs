using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class MountainModel : IDisposable
{
	public MountainCamera Camera;

	public Vector3 Forward;

	public float SkyboxOffset;

	public bool LockBufferResizing;

	private VirtualRenderTarget buffer;

	private VirtualRenderTarget blurA;

	private VirtualRenderTarget blurB;

	public static RasterizerState MountainRasterizer = new RasterizerState
	{
		CullMode = CullMode.CullClockwiseFace,
		MultiSampleAntiAlias = true
	};

	public static RasterizerState CullNoneRasterizer = new RasterizerState
	{
		CullMode = CullMode.None,
		MultiSampleAntiAlias = false
	};

	public static RasterizerState CullCCRasterizer = new RasterizerState
	{
		CullMode = CullMode.CullCounterClockwiseFace,
		MultiSampleAntiAlias = false
	};

	public static RasterizerState CullCRasterizer = new RasterizerState
	{
		CullMode = CullMode.CullClockwiseFace,
		MultiSampleAntiAlias = false
	};

	private int currState;

	private int nextState;

	private int targetState;

	private float easeState = 1f;

	private MountainState[] mountainStates = new MountainState[4];

	public Vector3 CoreWallPosition = Vector3.Zero;

	private VertexBuffer billboardVertices;

	private IndexBuffer billboardIndices;

	private VertexPositionColorTexture[] billboardInfo = new VertexPositionColorTexture[2048];

	private Texture2D[] billboardTextures = new Texture2D[512];

	private Ring fog;

	private Ring fog2;

	public float NearFogAlpha;

	public float StarEase;

	public float SnowStretch;

	public float SnowSpeedAddition = 1f;

	public float SnowForceFloat;

	private Ring starsky;

	private Ring starfog;

	private Ring stardots0;

	private Ring starstream0;

	private Ring starstream1;

	private Ring starstream2;

	private bool ignoreCameraRotation;

	private Quaternion lastCameraRotation;

	private Vector3 starCenter = new Vector3(0f, 32f, 0f);

	private float birdTimer;

	public List<VertexPositionColor> DebugPoints = new List<VertexPositionColor>();

	public bool DrawDebugPoints;

	public MountainModel()
	{
		mountainStates[0] = new MountainState(MTN.MountainTerrainTextures[0], MTN.MountainBuildingTextures[0], MTN.MountainSkyboxTextures[0], Calc.HexToColor("010817"));
		mountainStates[1] = new MountainState(MTN.MountainTerrainTextures[1], MTN.MountainBuildingTextures[1], MTN.MountainSkyboxTextures[1], Calc.HexToColor("13203E"));
		mountainStates[2] = new MountainState(MTN.MountainTerrainTextures[2], MTN.MountainBuildingTextures[2], MTN.MountainSkyboxTextures[2], Calc.HexToColor("281A35"));
		mountainStates[3] = new MountainState(MTN.MountainTerrainTextures[0], MTN.MountainBuildingTextures[0], MTN.MountainSkyboxTextures[0], Calc.HexToColor("010817"));
		fog = new Ring(6f, -1f, 20f, 0f, 24, Color.White, MTN.MountainFogTexture);
		fog2 = new Ring(6f, -4f, 10f, 0f, 24, Color.White, MTN.MountainFogTexture);
		starsky = new Ring(18f, -18f, 20f, 0f, 24, Color.White, Color.Transparent, MTN.MountainStarSky);
		starfog = new Ring(10f, -18f, 19.5f, 0f, 24, Calc.HexToColor("020915"), Color.Transparent, MTN.MountainFogTexture);
		stardots0 = new Ring(16f, -18f, 19f, 0f, 24, Color.White, Color.Transparent, MTN.MountainStars, 4f);
		starstream0 = new Ring(5f, -8f, 18.5f, 0.2f, 80, Color.Black, MTN.MountainStarStream);
		starstream1 = new Ring(4f, -6f, 18f, 1f, 80, Calc.HexToColor("9228e2") * 0.5f, MTN.MountainStarStream);
		starstream2 = new Ring(3f, -4f, 17.9f, 1.4f, 80, Calc.HexToColor("30ffff") * 0.5f, MTN.MountainStarStream);
		ResetRenderTargets();
		ResetBillboardBuffers();
	}

	public void SnapState(int state)
	{
		currState = (nextState = (targetState = state % mountainStates.Length));
		easeState = 1f;
		if (state == 3)
		{
			StarEase = 1f;
		}
	}

	public void EaseState(int state)
	{
		targetState = state % mountainStates.Length;
		lastCameraRotation = Camera.Rotation;
	}

	public void Update()
	{
		if (currState != nextState)
		{
			easeState = Calc.Approach(easeState, 1f, (float)((nextState == targetState) ? 1 : 4) * Engine.DeltaTime);
			if (easeState >= 1f)
			{
				currState = nextState;
			}
		}
		else if (nextState != targetState)
		{
			nextState = targetState;
			easeState = 0f;
		}
		StarEase = Calc.Approach(StarEase, (nextState == 3) ? 1f : 0f, ((nextState == 3) ? 1.5f : 1f) * Engine.DeltaTime);
		SnowForceFloat = Calc.ClampedMap(StarEase, 0.95f, 1f);
		ignoreCameraRotation = (nextState == 3 && currState != 3 && StarEase < 0.5f) || (nextState != 3 && currState == 3 && StarEase > 0.5f);
		if (nextState == 3)
		{
			SnowStretch = Calc.ClampedMap(StarEase, 0f, 0.25f) * 50f;
			SnowSpeedAddition = SnowStretch * 4f;
		}
		else
		{
			SnowStretch = Calc.ClampedMap(StarEase, 0.25f, 1f) * 50f;
			SnowSpeedAddition = (0f - SnowStretch) * 4f;
		}
		starfog.Rotate((0f - Engine.DeltaTime) * 0.01f);
		fog.Rotate((0f - Engine.DeltaTime) * 0.01f);
		fog.TopColor = (fog.BotColor = Color.Lerp(mountainStates[currState].FogColor, mountainStates[nextState].FogColor, easeState));
		fog2.Rotate((0f - Engine.DeltaTime) * 0.01f);
		fog2.TopColor = (fog2.BotColor = Color.White * 0.3f * NearFogAlpha);
		starstream1.Rotate(Engine.DeltaTime * 0.01f);
		starstream2.Rotate(Engine.DeltaTime * 0.02f);
		birdTimer += Engine.DeltaTime;
	}

	public void ResetRenderTargets()
	{
		int num = Math.Min(1920, Engine.ViewWidth);
		int num2 = Math.Min(1080, Engine.ViewHeight);
		if (buffer == null || buffer.IsDisposed || (buffer.Width != num && !LockBufferResizing))
		{
			DisposeTargets();
			buffer = VirtualContent.CreateRenderTarget("mountain-a", num, num2, depth: true, preserve: false);
			blurA = VirtualContent.CreateRenderTarget("mountain-blur-a", num / 2, num2 / 2);
			blurB = VirtualContent.CreateRenderTarget("mountain-blur-b", num / 2, num2 / 2);
		}
	}

	public void ResetBillboardBuffers()
	{
		if (billboardVertices == null || billboardIndices.IsDisposed || billboardIndices.GraphicsDevice.IsDisposed || billboardVertices.IsDisposed || billboardVertices.GraphicsDevice.IsDisposed || billboardInfo.Length > billboardVertices.VertexCount)
		{
			DisposeBillboardBuffers();
			billboardVertices = new VertexBuffer(Engine.Graphics.GraphicsDevice, typeof(VertexPositionColorTexture), billboardInfo.Length, BufferUsage.None);
			billboardIndices = new IndexBuffer(Engine.Graphics.GraphicsDevice, typeof(short), billboardInfo.Length / 4 * 6, BufferUsage.None);
			short[] array = new short[billboardIndices.IndexCount];
			int num = 0;
			int num2 = 0;
			while (num < array.Length)
			{
				array[num] = (short)num2;
				array[num + 1] = (short)(num2 + 1);
				array[num + 2] = (short)(num2 + 2);
				array[num + 3] = (short)num2;
				array[num + 4] = (short)(num2 + 2);
				array[num + 5] = (short)(num2 + 3);
				num += 6;
				num2 += 4;
			}
			billboardIndices.SetData(array);
		}
	}

	public void Dispose()
	{
		DisposeTargets();
		DisposeBillboardBuffers();
	}

	public void DisposeTargets()
	{
		if (buffer != null && !buffer.IsDisposed)
		{
			buffer.Dispose();
			blurA.Dispose();
			blurB.Dispose();
		}
	}

	public void DisposeBillboardBuffers()
	{
		if (billboardVertices != null && !billboardVertices.IsDisposed)
		{
			billboardVertices.Dispose();
		}
		if (billboardIndices != null && !billboardIndices.IsDisposed)
		{
			billboardIndices.Dispose();
		}
	}

	public void BeforeRender(Scene scene)
	{
		ResetRenderTargets();
		Quaternion rotation = Camera.Rotation;
		if (ignoreCameraRotation)
		{
			rotation = lastCameraRotation;
		}
		Matrix matrix = Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4f, (float)Engine.Width / (float)Engine.Height, 0.25f, 50f);
		Matrix matrix2 = Matrix.CreateTranslation(-Camera.Position) * Matrix.CreateFromQuaternion(rotation);
		Matrix matrix3 = matrix2 * matrix;
		Forward = Vector3.Transform(Vector3.Forward, Camera.Rotation.Conjugated());
		Engine.Graphics.GraphicsDevice.SetRenderTarget(buffer);
		if (StarEase < 1f)
		{
			Matrix matrix4 = Matrix.CreateTranslation(0f, 5f - Camera.Position.Y * 1.1f, 0f) * Matrix.CreateFromQuaternion(rotation) * matrix;
			if (currState == nextState)
			{
				mountainStates[currState].Skybox.Draw(matrix4, Color.White);
			}
			else
			{
				mountainStates[currState].Skybox.Draw(matrix4, Color.White);
				mountainStates[nextState].Skybox.Draw(matrix4, Color.White * easeState);
			}
			if (currState != nextState)
			{
				GFX.FxMountain.Parameters["ease"].SetValue(easeState);
				GFX.FxMountain.CurrentTechnique = GFX.FxMountain.Techniques["Easing"];
			}
			else
			{
				GFX.FxMountain.CurrentTechnique = GFX.FxMountain.Techniques["Single"];
			}
			Engine.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			Engine.Graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			Engine.Graphics.GraphicsDevice.RasterizerState = MountainRasterizer;
			GFX.FxMountain.Parameters["WorldViewProj"].SetValue(matrix3);
			GFX.FxMountain.Parameters["fog"].SetValue(fog.TopColor.ToVector3());
			Engine.Graphics.GraphicsDevice.Textures[0] = mountainStates[currState].TerrainTexture.Texture;
			Engine.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
			if (currState != nextState)
			{
				Engine.Graphics.GraphicsDevice.Textures[1] = mountainStates[nextState].TerrainTexture.Texture;
				Engine.Graphics.GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
			}
			MTN.MountainTerrain.Draw(GFX.FxMountain);
			GFX.FxMountain.Parameters["WorldViewProj"].SetValue(Matrix.CreateTranslation(CoreWallPosition) * matrix3);
			MTN.MountainCoreWall.Draw(GFX.FxMountain);
			GFX.FxMountain.Parameters["WorldViewProj"].SetValue(matrix3);
			Engine.Graphics.GraphicsDevice.Textures[0] = mountainStates[currState].BuildingsTexture.Texture;
			Engine.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
			if (currState != nextState)
			{
				Engine.Graphics.GraphicsDevice.Textures[1] = mountainStates[nextState].BuildingsTexture.Texture;
				Engine.Graphics.GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
			}
			MTN.MountainBuildings.Draw(GFX.FxMountain);
			fog.Draw(matrix3);
		}
		if (StarEase > 0f)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null);
			Draw.Rect(0f, 0f, buffer.Width, buffer.Height, Color.Black * Ease.CubeInOut(Calc.ClampedMap(StarEase, 0f, 0.6f)));
			Draw.SpriteBatch.End();
			Matrix matrix5 = Matrix.CreateTranslation(starCenter - Camera.Position) * Matrix.CreateFromQuaternion(rotation) * matrix;
			float alpha = Calc.ClampedMap(StarEase, 0.8f, 1f);
			starsky.Draw(matrix5, CullCCRasterizer, alpha);
			starfog.Draw(matrix5, CullCCRasterizer, alpha);
			stardots0.Draw(matrix5, CullCCRasterizer, alpha);
			starstream0.Draw(matrix5, CullCCRasterizer, alpha);
			starstream1.Draw(matrix5, CullCCRasterizer, alpha);
			starstream2.Draw(matrix5, CullCCRasterizer, alpha);
			Engine.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			Engine.Graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
			Engine.Graphics.GraphicsDevice.RasterizerState = CullCRasterizer;
			Engine.Graphics.GraphicsDevice.Textures[0] = MTN.MountainMoonTexture.Texture;
			Engine.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
			GFX.FxMountain.CurrentTechnique = GFX.FxMountain.Techniques["Single"];
			GFX.FxMountain.Parameters["WorldViewProj"].SetValue(matrix3);
			GFX.FxMountain.Parameters["fog"].SetValue(fog.TopColor.ToVector3());
			MTN.MountainMoon.Draw(GFX.FxMountain);
			float num = birdTimer * 0.2f;
			Matrix matrix6 = Matrix.CreateScale(0.25f) * Matrix.CreateRotationZ((float)Math.Cos(num * 2f) * 0.5f) * Matrix.CreateRotationX(0.4f + (float)Math.Sin(num) * 0.05f) * Matrix.CreateRotationY(0f - num - (float)Math.PI / 2f) * Matrix.CreateTranslation((float)Math.Cos(num) * 2.2f, 31f + (float)Math.Sin(num * 2f) * 0.8f, (float)Math.Sin(num) * 2.2f);
			GFX.FxMountain.Parameters["WorldViewProj"].SetValue(matrix6 * matrix3);
			GFX.FxMountain.Parameters["fog"].SetValue(fog.TopColor.ToVector3());
			MTN.MountainBird.Draw(GFX.FxMountain);
		}
		DrawBillboards(matrix3, scene.Tracker.GetComponents<Billboard>());
		if (StarEase < 1f)
		{
			fog2.Draw(matrix3, CullCRasterizer);
		}
		if (DrawDebugPoints && DebugPoints.Count > 0)
		{
			GFX.FxDebug.World = Matrix.Identity;
			GFX.FxDebug.View = matrix2;
			GFX.FxDebug.Projection = matrix;
			GFX.FxDebug.TextureEnabled = false;
			GFX.FxDebug.VertexColorEnabled = true;
			VertexPositionColor[] array = DebugPoints.ToArray();
			foreach (EffectPass pass in GFX.FxDebug.CurrentTechnique.Passes)
			{
				pass.Apply();
				Engine.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, array, 0, array.Length / 3);
			}
		}
		GaussianBlur.Blur((RenderTarget2D)buffer, blurA, blurB, 0.75f, clear: true, GaussianBlur.Samples.Five);
	}

	private void DrawBillboards(Matrix matrix, List<Component> billboards)
	{
		int num = 0;
		int num2 = billboardInfo.Length / 4;
		Vector3 vector = Vector3.Transform(Vector3.Left, Camera.Rotation.LookAt(Vector3.Zero, Forward, Vector3.Up).Conjugated());
		Vector3 vector2 = Vector3.Transform(Vector3.Up, Camera.Rotation.LookAt(Vector3.Zero, Forward, Vector3.Up).Conjugated());
		foreach (Billboard billboard in billboards)
		{
			if (!billboard.Entity.Visible || !billboard.Visible)
			{
				continue;
			}
			if (billboard.BeforeRender != null)
			{
				billboard.BeforeRender();
			}
			if (billboard.Color.A >= 0 && billboard.Size.X != 0f && billboard.Size.Y != 0f && billboard.Scale.X != 0f && billboard.Scale.Y != 0f && billboard.Texture != null)
			{
				if (num < num2)
				{
					Vector3 position = billboard.Position;
					Vector3 vector3 = vector * billboard.Size.X * billboard.Scale.X;
					Vector3 vector4 = vector2 * billboard.Size.Y * billboard.Scale.Y;
					Vector3 vector5 = -vector3;
					Vector3 vector6 = -vector4;
					int num3 = num * 4;
					int num4 = num * 4 + 1;
					int num5 = num * 4 + 2;
					int num6 = num * 4 + 3;
					billboardInfo[num3].Color = billboard.Color;
					billboardInfo[num3].TextureCoordinate.X = billboard.Texture.LeftUV;
					billboardInfo[num3].TextureCoordinate.Y = billboard.Texture.BottomUV;
					billboardInfo[num3].Position = position + vector3 + vector6;
					billboardInfo[num4].Color = billboard.Color;
					billboardInfo[num4].TextureCoordinate.X = billboard.Texture.LeftUV;
					billboardInfo[num4].TextureCoordinate.Y = billboard.Texture.TopUV;
					billboardInfo[num4].Position = position + vector3 + vector4;
					billboardInfo[num5].Color = billboard.Color;
					billboardInfo[num5].TextureCoordinate.X = billboard.Texture.RightUV;
					billboardInfo[num5].TextureCoordinate.Y = billboard.Texture.TopUV;
					billboardInfo[num5].Position = position + vector5 + vector4;
					billboardInfo[num6].Color = billboard.Color;
					billboardInfo[num6].TextureCoordinate.X = billboard.Texture.RightUV;
					billboardInfo[num6].TextureCoordinate.Y = billboard.Texture.BottomUV;
					billboardInfo[num6].Position = position + vector5 + vector6;
					billboardTextures[num] = billboard.Texture.Texture.Texture;
				}
				num++;
			}
		}
		ResetBillboardBuffers();
		if (num <= 0)
		{
			return;
		}
		billboardVertices.SetData(billboardInfo);
		Engine.Graphics.GraphicsDevice.SetVertexBuffer(billboardVertices);
		Engine.Graphics.GraphicsDevice.Indices = billboardIndices;
		Engine.Graphics.GraphicsDevice.RasterizerState = CullNoneRasterizer;
		Engine.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
		Engine.Graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
		Engine.Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
		GFX.FxTexture.Parameters["World"].SetValue(matrix);
		int num7 = Math.Min(num, billboardInfo.Length / 4);
		Texture2D texture2D = billboardTextures[0];
		int num8 = 0;
		for (int i = 1; i < num7; i++)
		{
			if (billboardTextures[i] != texture2D)
			{
				DrawBillboardBatch(texture2D, num8, i - num8);
				texture2D = billboardTextures[i];
				num8 = i;
			}
		}
		DrawBillboardBatch(texture2D, num8, num7 - num8);
		if (num * 4 > billboardInfo.Length)
		{
			billboardInfo = new VertexPositionColorTexture[billboardInfo.Length * 2];
			billboardTextures = new Texture2D[billboardInfo.Length / 4];
		}
	}

	private void DrawBillboardBatch(Texture2D texture, int offset, int sprites)
	{
		Engine.Graphics.GraphicsDevice.Textures[0] = texture;
		foreach (EffectPass pass in GFX.FxTexture.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Graphics.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, offset * 4, 0, sprites * 4, 0, sprites * 2);
		}
	}

	public void Render()
	{
		float num = (float)Engine.ViewWidth / (float)buffer.Width;
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null);
		Draw.SpriteBatch.Draw((RenderTarget2D)buffer, Vector2.Zero, buffer.Bounds, Color.White * 1f, 0f, Vector2.Zero, num, SpriteEffects.None, 0f);
		Draw.SpriteBatch.Draw((RenderTarget2D)blurB, Vector2.Zero, blurB.Bounds, Color.White, 0f, Vector2.Zero, num * 2f, SpriteEffects.None, 0f);
		Draw.SpriteBatch.End();
	}
}
