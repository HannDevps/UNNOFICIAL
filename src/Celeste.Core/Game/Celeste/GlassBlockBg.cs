using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class GlassBlockBg : Entity
{
	private struct Star
	{
		public Vector2 Position;

		public MTexture Texture;

		public Color Color;

		public Vector2 Scroll;
	}

	private struct Ray
	{
		public Vector2 Position;

		public float Width;

		public float Length;

		public Color Color;
	}

	private static readonly Color[] starColors = new Color[3]
	{
		Calc.HexToColor("7f9fba"),
		Calc.HexToColor("9bd1cd"),
		Calc.HexToColor("bacae3")
	};

	private const int StarCount = 100;

	private const int RayCount = 50;

	private Star[] stars = new Star[100];

	private Ray[] rays = new Ray[50];

	private VertexPositionColor[] verts = new VertexPositionColor[2700];

	private Vector2 rayNormal = new Vector2(-5f, -8f).SafeNormalize();

	private Color bgColor = Calc.HexToColor("0d2e89");

	private VirtualRenderTarget beamsTarget;

	private VirtualRenderTarget starsTarget;

	private bool hasBlocks;

	public GlassBlockBg()
	{
		base.Tag = Tags.Global;
		Add(new BeforeRenderHook(BeforeRender));
		Add(new DisplacementRenderHook(OnDisplacementRender));
		base.Depth = -9990;
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("particles/stars/");
		for (int i = 0; i < stars.Length; i++)
		{
			stars[i].Position.X = Calc.Random.Next(320);
			stars[i].Position.Y = Calc.Random.Next(180);
			stars[i].Texture = Calc.Random.Choose(atlasSubtextures);
			stars[i].Color = Calc.Random.Choose(starColors);
			stars[i].Scroll = Vector2.One * Calc.Random.NextFloat(0.05f);
		}
		for (int j = 0; j < rays.Length; j++)
		{
			rays[j].Position.X = Calc.Random.Next(320);
			rays[j].Position.Y = Calc.Random.Next(180);
			rays[j].Width = Calc.Random.Range(4f, 16f);
			rays[j].Length = Calc.Random.Choose(48, 96, 128);
			rays[j].Color = Color.White * Calc.Random.Range(0.2f, 0.4f);
		}
	}

	private void BeforeRender()
	{
		List<Entity> entities = base.Scene.Tracker.GetEntities<GlassBlock>();
		if (!(hasBlocks = entities.Count > 0))
		{
			return;
		}
		Camera camera = (base.Scene as Level).Camera;
		int num = 320;
		int num2 = 180;
		if (starsTarget == null)
		{
			starsTarget = VirtualContent.CreateRenderTarget("glass-block-surfaces", 320, 180);
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(starsTarget);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Matrix.Identity);
		Vector2 origin = new Vector2(8f, 8f);
		for (int i = 0; i < stars.Length; i++)
		{
			MTexture texture = stars[i].Texture;
			Color color = stars[i].Color;
			Vector2 scroll = stars[i].Scroll;
			Vector2 vector = new Vector2
			{
				X = Mod(stars[i].Position.X - camera.X * (1f - scroll.X), num),
				Y = Mod(stars[i].Position.Y - camera.Y * (1f - scroll.Y), num2)
			};
			texture.Draw(vector, origin, color);
			if (vector.X < origin.X)
			{
				texture.Draw(vector + new Vector2(num, 0f), origin, color);
			}
			else if (vector.X > (float)num - origin.X)
			{
				texture.Draw(vector - new Vector2(num, 0f), origin, color);
			}
			if (vector.Y < origin.Y)
			{
				texture.Draw(vector + new Vector2(0f, num2), origin, color);
			}
			else if (vector.Y > (float)num2 - origin.Y)
			{
				texture.Draw(vector - new Vector2(0f, num2), origin, color);
			}
		}
		Draw.SpriteBatch.End();
		int vertex = 0;
		for (int j = 0; j < rays.Length; j++)
		{
			Vector2 vector2 = new Vector2
			{
				X = Mod(rays[j].Position.X - camera.X * 0.9f, num),
				Y = Mod(rays[j].Position.Y - camera.Y * 0.9f, num2)
			};
			DrawRay(vector2, ref vertex, ref rays[j]);
			if (vector2.X < 64f)
			{
				DrawRay(vector2 + new Vector2(num, 0f), ref vertex, ref rays[j]);
			}
			else if (vector2.X > (float)(num - 64))
			{
				DrawRay(vector2 - new Vector2(num, 0f), ref vertex, ref rays[j]);
			}
			if (vector2.Y < 64f)
			{
				DrawRay(vector2 + new Vector2(0f, num2), ref vertex, ref rays[j]);
			}
			else if (vector2.Y > (float)(num2 - 64))
			{
				DrawRay(vector2 - new Vector2(0f, num2), ref vertex, ref rays[j]);
			}
		}
		if (beamsTarget == null)
		{
			beamsTarget = VirtualContent.CreateRenderTarget("glass-block-beams", 320, 180);
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(beamsTarget);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		GFX.DrawVertices(Matrix.Identity, verts, vertex);
	}

	private void OnDisplacementRender()
	{
		foreach (Entity entity in base.Scene.Tracker.GetEntities<GlassBlock>())
		{
			Draw.Rect(entity.X, entity.Y, entity.Width, entity.Height, new Color(0.5f, 0.5f, 0.2f, 1f));
		}
	}

	private void DrawRay(Vector2 position, ref int vertex, ref Ray ray)
	{
		Vector2 vector = new Vector2(0f - rayNormal.Y, rayNormal.X);
		Vector2 vector2 = rayNormal * ray.Width * 0.5f;
		Vector2 vector3 = vector * ray.Length * 0.25f * 0.5f;
		Vector2 vector4 = vector * ray.Length * 0.5f * 0.5f;
		Vector2 v = position + vector2 - vector3 - vector4;
		Vector2 v2 = position - vector2 - vector3 - vector4;
		Vector2 vector5 = position + vector2 - vector3;
		Vector2 vector6 = position - vector2 - vector3;
		Vector2 vector7 = position + vector2 + vector3;
		Vector2 vector8 = position - vector2 + vector3;
		Vector2 v3 = position + vector2 + vector3 + vector4;
		Vector2 v4 = position - vector2 + vector3 + vector4;
		Color transparent = Color.Transparent;
		Color color = ray.Color;
		Quad(ref vertex, v, vector5, vector6, v2, transparent, color, color, transparent);
		Quad(ref vertex, vector5, vector7, vector8, vector6, color, color, color, color);
		Quad(ref vertex, vector7, v3, v4, vector8, color, transparent, transparent, color);
	}

	private void Quad(ref int vertex, Vector2 v0, Vector2 v1, Vector2 v2, Vector2 v3, Color c0, Color c1, Color c2, Color c3)
	{
		verts[vertex].Position.X = v0.X;
		verts[vertex].Position.Y = v0.Y;
		verts[vertex++].Color = c0;
		verts[vertex].Position.X = v1.X;
		verts[vertex].Position.Y = v1.Y;
		verts[vertex++].Color = c1;
		verts[vertex].Position.X = v2.X;
		verts[vertex].Position.Y = v2.Y;
		verts[vertex++].Color = c2;
		verts[vertex].Position.X = v0.X;
		verts[vertex].Position.Y = v0.Y;
		verts[vertex++].Color = c0;
		verts[vertex].Position.X = v2.X;
		verts[vertex].Position.Y = v2.Y;
		verts[vertex++].Color = c2;
		verts[vertex].Position.X = v3.X;
		verts[vertex].Position.Y = v3.Y;
		verts[vertex++].Color = c3;
	}

	public override void Render()
	{
		if (!hasBlocks)
		{
			return;
		}
		Vector2 position = (base.Scene as Level).Camera.Position;
		List<Entity> entities = base.Scene.Tracker.GetEntities<GlassBlock>();
		foreach (Entity item in entities)
		{
			Draw.Rect(item.X, item.Y, item.Width, item.Height, bgColor);
		}
		if (starsTarget != null && !starsTarget.IsDisposed)
		{
			foreach (Entity item2 in entities)
			{
				Rectangle value = new Rectangle((int)(item2.X - position.X), (int)(item2.Y - position.Y), (int)item2.Width, (int)item2.Height);
				Draw.SpriteBatch.Draw((RenderTarget2D)starsTarget, item2.Position, value, Color.White);
			}
		}
		if (beamsTarget == null || beamsTarget.IsDisposed)
		{
			return;
		}
		foreach (Entity item3 in entities)
		{
			Rectangle value2 = new Rectangle((int)(item3.X - position.X), (int)(item3.Y - position.Y), (int)item3.Width, (int)item3.Height);
			Draw.SpriteBatch.Draw((RenderTarget2D)beamsTarget, item3.Position, value2, Color.White);
		}
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
		if (starsTarget != null && !starsTarget.IsDisposed)
		{
			starsTarget.Dispose();
		}
		if (beamsTarget != null && !beamsTarget.IsDisposed)
		{
			beamsTarget.Dispose();
		}
		starsTarget = null;
		beamsTarget = null;
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
