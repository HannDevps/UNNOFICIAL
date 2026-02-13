using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class FinalBossStarfield : Backdrop
{
	private struct Particle
	{
		public Vector2 Position;

		public Vector2 Direction;

		public float Speed;

		public Color Color;

		public float DirectionApproach;
	}

	public float Alpha = 1f;

	private const int particleCount = 200;

	private Particle[] particles = new Particle[200];

	private VertexPositionColor[] verts = new VertexPositionColor[1206];

	private static readonly Color[] colors = new Color[4]
	{
		Calc.HexToColor("030c1b"),
		Calc.HexToColor("0b031b"),
		Calc.HexToColor("1b0319"),
		Calc.HexToColor("0f0301")
	};

	public FinalBossStarfield()
	{
		UseSpritebatch = false;
		for (int i = 0; i < 200; i++)
		{
			particles[i].Speed = Calc.Random.Range(500f, 1200f);
			particles[i].Direction = new Vector2(-1f, 0f);
			particles[i].DirectionApproach = Calc.Random.Range(0.25f, 4f);
			particles[i].Position.X = Calc.Random.Range(0, 384);
			particles[i].Position.Y = Calc.Random.Range(0, 244);
			particles[i].Color = Calc.Random.Choose(colors);
		}
	}

	public override void Update(Scene scene)
	{
		base.Update(scene);
		if (Visible && Alpha > 0f)
		{
			Vector2 vector = new Vector2(-1f, 0f);
			Level level = scene as Level;
			if (level.Bounds.Height > level.Bounds.Width)
			{
				vector = new Vector2(0f, -1f);
			}
			float target = vector.Angle();
			for (int i = 0; i < 200; i++)
			{
				particles[i].Position += particles[i].Direction * particles[i].Speed * Engine.DeltaTime;
				float val = particles[i].Direction.Angle();
				val = Calc.AngleApproach(val, target, particles[i].DirectionApproach * Engine.DeltaTime);
				particles[i].Direction = Calc.AngleToVector(val, 1f);
			}
		}
	}

	public override void Render(Scene scene)
	{
		Vector2 position = (scene as Level).Camera.Position;
		Color color = Color.Black * Alpha;
		verts[0].Color = color;
		verts[0].Position = new Vector3(-10f, -10f, 0f);
		verts[1].Color = color;
		verts[1].Position = new Vector3(330f, -10f, 0f);
		verts[2].Color = color;
		verts[2].Position = new Vector3(330f, 190f, 0f);
		verts[3].Color = color;
		verts[3].Position = new Vector3(-10f, -10f, 0f);
		verts[4].Color = color;
		verts[4].Position = new Vector3(330f, 190f, 0f);
		verts[5].Color = color;
		verts[5].Position = new Vector3(-10f, 190f, 0f);
		for (int i = 0; i < 200; i++)
		{
			int num = (i + 1) * 6;
			float num2 = Calc.ClampedMap(particles[i].Speed, 0f, 1200f, 1f, 64f);
			float num3 = Calc.ClampedMap(particles[i].Speed, 0f, 1200f, 3f, 0.6f);
			Vector2 direction = particles[i].Direction;
			Vector2 vector = direction.Perpendicular();
			Vector2 position2 = particles[i].Position;
			position2.X = -32f + Mod(position2.X - position.X * 0.9f, 384f);
			position2.Y = -32f + Mod(position2.Y - position.Y * 0.9f, 244f);
			Vector2 value = position2 - direction * num2 * 0.5f - vector * num3;
			Vector2 value2 = position2 + direction * num2 * 1f - vector * num3;
			Vector2 value3 = position2 + direction * num2 * 0.5f + vector * num3;
			Vector2 value4 = position2 - direction * num2 * 1f + vector * num3;
			Color color2 = particles[i].Color * Alpha;
			verts[num].Color = color2;
			verts[num].Position = new Vector3(value, 0f);
			verts[num + 1].Color = color2;
			verts[num + 1].Position = new Vector3(value2, 0f);
			verts[num + 2].Color = color2;
			verts[num + 2].Position = new Vector3(value3, 0f);
			verts[num + 3].Color = color2;
			verts[num + 3].Position = new Vector3(value, 0f);
			verts[num + 4].Color = color2;
			verts[num + 4].Position = new Vector3(value3, 0f);
			verts[num + 5].Color = color2;
			verts[num + 5].Position = new Vector3(value4, 0f);
		}
		GFX.DrawVertices(Matrix.Identity, verts, verts.Length);
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
