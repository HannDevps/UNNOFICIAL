using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class Tentacles : Backdrop
{
	public enum Side
	{
		Right,
		Left,
		Top,
		Bottom
	}

	private struct Tentacle
	{
		public float Length;

		public float Offset;

		public float Step;

		public float Position;

		public float Approach;

		public float Width;
	}

	private const int NodesPerTentacle = 10;

	private Side side;

	private float width;

	private Vector2 origin;

	private Vector2 outwards;

	private float outwardsOffset;

	private VertexPositionColor[] vertices;

	private int vertexCount;

	private Tentacle[] tentacles;

	private int tentacleCount;

	private float hideTimer = 5f;

	public Tentacles(Side side, Color color, float outwardsOffset)
	{
		this.side = side;
		this.outwardsOffset = outwardsOffset;
		UseSpritebatch = false;
		switch (side)
		{
		case Side.Right:
			outwards = new Vector2(-1f, 0f);
			width = 180f;
			origin = new Vector2(320f, 90f);
			break;
		case Side.Left:
			outwards = new Vector2(1f, 0f);
			width = 180f;
			origin = new Vector2(0f, 90f);
			break;
		case Side.Top:
			outwards = new Vector2(0f, 1f);
			width = 320f;
			origin = new Vector2(160f, 0f);
			break;
		case Side.Bottom:
			outwards = new Vector2(0f, -1f);
			width = 320f;
			origin = new Vector2(160f, 180f);
			break;
		}
		float num = 0f;
		tentacles = new Tentacle[100];
		for (int i = 0; i < tentacles.Length; i++)
		{
			if (!(num < width + 40f))
			{
				break;
			}
			tentacles[i].Length = Calc.Random.NextFloat();
			tentacles[i].Offset = Calc.Random.NextFloat();
			tentacles[i].Step = Calc.Random.NextFloat();
			tentacles[i].Position = -200f;
			tentacles[i].Approach = Calc.Random.NextFloat();
			num += (tentacles[i].Width = 6f + Calc.Random.NextFloat(20f));
			tentacleCount++;
		}
		vertices = new VertexPositionColor[tentacleCount * 11 * 6];
		for (int j = 0; j < vertices.Length; j++)
		{
			vertices[j].Color = color;
		}
	}

	public override void Update(Scene scene)
	{
		bool num = IsVisible(scene as Level);
		float num2 = 0f;
		if (num)
		{
			Camera camera = (scene as Level).Camera;
			Player entity = scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				if (side == Side.Right)
				{
					num2 = 320f - (entity.X - camera.X) - 160f;
				}
				else if (side == Side.Bottom)
				{
					num2 = 180f - (entity.Y - camera.Y) - 180f;
				}
			}
			hideTimer = 0f;
		}
		else
		{
			num2 = -200f;
			hideTimer += Engine.DeltaTime;
		}
		num2 += outwardsOffset;
		Visible = hideTimer < 5f;
		if (!Visible)
		{
			return;
		}
		Vector2 vector = -outwards.Perpendicular();
		int num3 = 0;
		Vector2 vector2 = origin - vector * (width / 2f + 2f);
		for (int i = 0; i < tentacleCount; i++)
		{
			vector2 += vector * tentacles[i].Width * 0.5f;
			tentacles[i].Position += (num2 - tentacles[i].Position) * (1f - (float)Math.Pow(0.5f * (0.5f + tentacles[i].Approach * 0.5f), Engine.DeltaTime));
			Vector2 vector3 = (tentacles[i].Position + (float)Math.Sin(scene.TimeActive + tentacles[i].Offset * 4f) * 8f + (origin - vector2).Length() * 0.7f) * outwards;
			Vector2 vector4 = vector2 + vector3;
			float num4 = 2f + tentacles[i].Length * 8f;
			Vector2 vector5 = vector4;
			Vector2 vector6 = vector * tentacles[i].Width * 0.5f;
			vertices[num3++].Position = new Vector3(vector2 + vector6, 0f);
			vertices[num3++].Position = new Vector3(vector2 - vector6, 0f);
			vertices[num3++].Position = new Vector3(vector4 - vector6, 0f);
			vertices[num3++].Position = new Vector3(vector4 - vector6, 0f);
			vertices[num3++].Position = new Vector3(vector2 + vector6, 0f);
			vertices[num3++].Position = new Vector3(vector4 + vector6, 0f);
			for (int j = 1; j < 10; j++)
			{
				float num5 = scene.TimeActive * tentacles[i].Offset * (float)Math.Pow(1.100000023841858, j) * 2f;
				float num6 = tentacles[i].Offset * 3f + (float)j * (0.1f + tentacles[i].Step * 0.2f) + num4 * (float)j * 0.1f;
				float num7 = 2f + 4f * ((float)j / 10f);
				Vector2 vector7 = (float)Math.Sin(num5 + num6) * vector * num7;
				float num8 = (1f - (float)j / 10f) * tentacles[i].Width * 0.5f;
				Vector2 vector8 = vector5 + outwards * num4 + vector7;
				Vector2 vector9 = (vector5 - vector8).SafeNormalize().Perpendicular() * num8;
				vertices[num3++].Position = new Vector3(vector5 + vector6, 0f);
				vertices[num3++].Position = new Vector3(vector5 - vector6, 0f);
				vertices[num3++].Position = new Vector3(vector8 - vector9, 0f);
				vertices[num3++].Position = new Vector3(vector8 - vector9, 0f);
				vertices[num3++].Position = new Vector3(vector5 + vector6, 0f);
				vertices[num3++].Position = new Vector3(vector8 + vector9, 0f);
				vector5 = vector8;
				vector6 = vector9;
			}
			vector2 += vector * tentacles[i].Width * 0.5f;
		}
		vertexCount = num3;
	}

	public override void Render(Scene scene)
	{
		if (vertexCount > 0)
		{
			GFX.DrawVertices(Matrix.Identity, vertices, vertexCount);
		}
	}
}
