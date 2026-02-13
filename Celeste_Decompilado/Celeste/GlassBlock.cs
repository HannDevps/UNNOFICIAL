using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class GlassBlock : Solid
{
	private struct Line
	{
		public Vector2 A;

		public Vector2 B;

		public Line(Vector2 a, Vector2 b)
		{
			A = a;
			B = b;
		}
	}

	private bool sinks;

	private float startY;

	private List<Line> lines = new List<Line>();

	private Color lineColor = Color.White;

	public GlassBlock(Vector2 position, float width, float height, bool sinks)
		: base(position, width, height, safe: false)
	{
		this.sinks = sinks;
		startY = base.Y;
		base.Depth = -10000;
		Add(new LightOcclude());
		Add(new MirrorSurface());
		SurfaceSoundIndex = 32;
	}

	public GlassBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Bool("sinks"))
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		int num = (int)base.Width / 8;
		int num2 = (int)base.Height / 8;
		AddSide(new Vector2(0f, 0f), new Vector2(0f, -1f), num);
		AddSide(new Vector2(num - 1, 0f), new Vector2(1f, 0f), num2);
		AddSide(new Vector2(num - 1, num2 - 1), new Vector2(0f, 1f), num);
		AddSide(new Vector2(0f, num2 - 1), new Vector2(-1f, 0f), num2);
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}

	private void AddSide(Vector2 start, Vector2 normal, int tiles)
	{
		Vector2 vector = new Vector2(0f - normal.Y, normal.X);
		for (int i = 0; i < tiles; i++)
		{
			if (Open(start + vector * i + normal))
			{
				Vector2 vector2 = (start + vector * i) * 8f + new Vector2(4f) - vector * 4f + normal * 4f;
				if (!Open(start + vector * (i - 1)))
				{
					vector2 -= vector;
				}
				for (; i < tiles && Open(start + vector * i + normal); i++)
				{
				}
				Vector2 vector3 = (start + vector * i) * 8f + new Vector2(4f) - vector * 4f + normal * 4f;
				if (!Open(start + vector * i))
				{
					vector3 += vector;
				}
				lines.Add(new Line(vector2 + normal, vector3 + normal));
			}
		}
	}

	private bool Open(Vector2 tile)
	{
		Vector2 point = new Vector2(base.X + tile.X * 8f + 4f, base.Y + tile.Y * 8f + 4f);
		if (!base.Scene.CollideCheck<SolidTiles>(point))
		{
			return !base.Scene.CollideCheck<GlassBlock>(point);
		}
		return false;
	}

	public override void Render()
	{
		foreach (Line line in lines)
		{
			Draw.Line(Position + line.A, Position + line.B, lineColor);
		}
	}
}
