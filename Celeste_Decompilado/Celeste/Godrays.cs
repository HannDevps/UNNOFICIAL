using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class Godrays : Backdrop
{
	private struct Ray
	{
		public float X;

		public float Y;

		public float Percent;

		public float Duration;

		public float Width;

		public float Length;

		public void Reset()
		{
			Percent = 0f;
			X = Calc.Random.NextFloat(384f);
			Y = Calc.Random.NextFloat(244f);
			Duration = 4f + Calc.Random.NextFloat() * 8f;
			Width = Calc.Random.Next(8, 16);
			Length = Calc.Random.Next(20, 40);
		}
	}

	private const int RayCount = 6;

	private VertexPositionColor[] vertices = new VertexPositionColor[36];

	private int vertexCount;

	private Color rayColor = Calc.HexToColor("f52b63") * 0.5f;

	private Ray[] rays = new Ray[6];

	private float fade;

	public Godrays()
	{
		UseSpritebatch = false;
		for (int i = 0; i < rays.Length; i++)
		{
			rays[i].Reset();
			rays[i].Percent = Calc.Random.NextFloat();
		}
	}

	public override void Update(Scene scene)
	{
		Level level = scene as Level;
		bool flag = IsVisible(level);
		fade = Calc.Approach(fade, flag ? 1 : 0, Engine.DeltaTime);
		Visible = fade > 0f;
		if (!Visible)
		{
			return;
		}
		Player entity = level.Tracker.GetEntity<Player>();
		Vector2 vector = Calc.AngleToVector(-1.6707964f, 1f);
		Vector2 vector2 = new Vector2(0f - vector.Y, vector.X);
		int num = 0;
		for (int i = 0; i < rays.Length; i++)
		{
			if (rays[i].Percent >= 1f)
			{
				rays[i].Reset();
			}
			rays[i].Percent += Engine.DeltaTime / rays[i].Duration;
			rays[i].Y += 8f * Engine.DeltaTime;
			float percent = rays[i].Percent;
			float num2 = -32f + Mod(rays[i].X - level.Camera.X * 0.9f, 384f);
			float num3 = -32f + Mod(rays[i].Y - level.Camera.Y * 0.9f, 244f);
			float width = rays[i].Width;
			float length = rays[i].Length;
			Vector2 vector3 = new Vector2((int)num2, (int)num3);
			Color color = rayColor * Ease.CubeInOut(Calc.Clamp(((percent < 0.5f) ? percent : (1f - percent)) * 2f, 0f, 1f)) * fade;
			if (entity != null)
			{
				float num4 = (vector3 + level.Camera.Position - entity.Position).Length();
				if (num4 < 64f)
				{
					color *= 0.25f + 0.75f * (num4 / 64f);
				}
			}
			VertexPositionColor vertexPositionColor = new VertexPositionColor(new Vector3(vector3 + vector2 * width + vector * length, 0f), color);
			VertexPositionColor vertexPositionColor2 = new VertexPositionColor(new Vector3(vector3 - vector2 * width, 0f), color);
			VertexPositionColor vertexPositionColor3 = new VertexPositionColor(new Vector3(vector3 + vector2 * width, 0f), color);
			VertexPositionColor vertexPositionColor4 = new VertexPositionColor(new Vector3(vector3 - vector2 * width - vector * length, 0f), color);
			vertices[num++] = vertexPositionColor;
			vertices[num++] = vertexPositionColor2;
			vertices[num++] = vertexPositionColor3;
			vertices[num++] = vertexPositionColor2;
			vertices[num++] = vertexPositionColor3;
			vertices[num++] = vertexPositionColor4;
		}
		vertexCount = num;
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}

	public override void Render(Scene scene)
	{
		if (vertexCount > 0 && fade > 0f)
		{
			GFX.DrawVertices(Matrix.Identity, vertices, vertexCount);
		}
	}
}
