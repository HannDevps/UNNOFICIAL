using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class Water : Entity
{
	public class Ripple
	{
		public float Position;

		public float Speed;

		public float Height;

		public float Percent;

		public float Duration;
	}

	public class Tension
	{
		public float Position;

		public float Strength;
	}

	public class Ray
	{
		public float Position;

		public float Percent;

		public float Duration;

		public float Width;

		public float Length;

		private float MaxWidth;

		public Ray(float maxWidth)
		{
			MaxWidth = maxWidth;
			Reset(Calc.Random.NextFloat());
		}

		public void Reset(float percent)
		{
			Position = Calc.Random.NextFloat() * MaxWidth;
			Percent = percent;
			Duration = Calc.Random.Range(2f, 8f);
			Width = Calc.Random.Range(2, 16);
			Length = Calc.Random.Range(8f, 128f);
		}
	}

	public class Surface
	{
		public const int Resolution = 4;

		public const float RaysPerPixel = 0.2f;

		public const float BaseHeight = 6f;

		public readonly Vector2 Outwards;

		public readonly int Width;

		public readonly int BodyHeight;

		public Vector2 Position;

		public List<Ripple> Ripples = new List<Ripple>();

		public List<Ray> Rays = new List<Ray>();

		public List<Tension> Tensions = new List<Tension>();

		private float timer;

		private VertexPositionColor[] mesh;

		private int fillStartIndex;

		private int rayStartIndex;

		private int surfaceStartIndex;

		public Surface(Vector2 position, Vector2 outwards, float width, float bodyHeight)
		{
			Position = position;
			Outwards = outwards;
			Width = (int)width;
			BodyHeight = (int)bodyHeight;
			int num = (int)(width / 4f);
			int num2 = (int)(width * 0.2f);
			Rays = new List<Ray>();
			for (int i = 0; i < num2; i++)
			{
				Rays.Add(new Ray(width));
			}
			fillStartIndex = 0;
			rayStartIndex = num * 6;
			surfaceStartIndex = (num + num2) * 6;
			mesh = new VertexPositionColor[(num * 2 + num2) * 6];
			for (int j = fillStartIndex; j < fillStartIndex + num * 6; j++)
			{
				mesh[j].Color = FillColor;
			}
			for (int k = rayStartIndex; k < rayStartIndex + num2 * 6; k++)
			{
				mesh[k].Color = Color.Transparent;
			}
			for (int l = surfaceStartIndex; l < surfaceStartIndex + num * 6; l++)
			{
				mesh[l].Color = SurfaceColor;
			}
		}

		public float GetPointAlong(Vector2 position)
		{
			Vector2 vector = Outwards.Perpendicular();
			Vector2 vector2 = Position + vector * (-Width / 2);
			Vector2 lineB = Position + vector * (Width / 2);
			Vector2 vector3 = Calc.ClosestPointOnLine(vector2, lineB, position);
			return (vector2 - vector3).Length();
		}

		public Tension SetTension(Vector2 position, float strength)
		{
			Tension tension = new Tension
			{
				Position = GetPointAlong(position),
				Strength = strength
			};
			Tensions.Add(tension);
			return tension;
		}

		public void RemoveTension(Tension tension)
		{
			Tensions.Remove(tension);
		}

		public void DoRipple(Vector2 position, float multiplier)
		{
			float num = 80f;
			float num2 = 3f;
			float pointAlong = GetPointAlong(position);
			int num3 = 2;
			if (Width < 200)
			{
				num2 *= Calc.ClampedMap(Width, 0f, 200f, 0.25f);
				multiplier *= Calc.ClampedMap(Width, 0f, 200f, 0.5f);
			}
			Ripples.Add(new Ripple
			{
				Position = pointAlong,
				Speed = 0f - num,
				Height = (float)num3 * multiplier,
				Percent = 0f,
				Duration = num2
			});
			Ripples.Add(new Ripple
			{
				Position = pointAlong,
				Speed = num,
				Height = (float)num3 * multiplier,
				Percent = 0f,
				Duration = num2
			});
		}

		public void Update()
		{
			timer += Engine.DeltaTime;
			Vector2 vector = Outwards.Perpendicular();
			for (int num = Ripples.Count - 1; num >= 0; num--)
			{
				Ripple ripple = Ripples[num];
				if (ripple.Percent > 1f)
				{
					Ripples.RemoveAt(num);
				}
				else
				{
					ripple.Position += ripple.Speed * Engine.DeltaTime;
					if (ripple.Position < 0f || ripple.Position > (float)Width)
					{
						ripple.Speed = 0f - ripple.Speed;
						ripple.Position = Calc.Clamp(ripple.Position, 0f, Width);
					}
					ripple.Percent += Engine.DeltaTime / ripple.Duration;
				}
			}
			int num2 = 0;
			int num3 = fillStartIndex;
			int num4 = surfaceStartIndex;
			while (num2 < Width)
			{
				int num5 = num2;
				float surfaceHeight = GetSurfaceHeight(num5);
				int num6 = Math.Min(num2 + 4, Width);
				float surfaceHeight2 = GetSurfaceHeight(num6);
				mesh[num3].Position = new Vector3(Position + vector * (-Width / 2 + num5) + Outwards * surfaceHeight, 0f);
				mesh[num3 + 1].Position = new Vector3(Position + vector * (-Width / 2 + num6) + Outwards * surfaceHeight2, 0f);
				mesh[num3 + 2].Position = new Vector3(Position + vector * (-Width / 2 + num5), 0f);
				mesh[num3 + 3].Position = new Vector3(Position + vector * (-Width / 2 + num6) + Outwards * surfaceHeight2, 0f);
				mesh[num3 + 4].Position = new Vector3(Position + vector * (-Width / 2 + num6), 0f);
				mesh[num3 + 5].Position = new Vector3(Position + vector * (-Width / 2 + num5), 0f);
				mesh[num4].Position = new Vector3(Position + vector * (-Width / 2 + num5) + Outwards * (surfaceHeight + 1f), 0f);
				mesh[num4 + 1].Position = new Vector3(Position + vector * (-Width / 2 + num6) + Outwards * (surfaceHeight2 + 1f), 0f);
				mesh[num4 + 2].Position = new Vector3(Position + vector * (-Width / 2 + num5) + Outwards * surfaceHeight, 0f);
				mesh[num4 + 3].Position = new Vector3(Position + vector * (-Width / 2 + num6) + Outwards * (surfaceHeight2 + 1f), 0f);
				mesh[num4 + 4].Position = new Vector3(Position + vector * (-Width / 2 + num6) + Outwards * surfaceHeight2, 0f);
				mesh[num4 + 5].Position = new Vector3(Position + vector * (-Width / 2 + num5) + Outwards * surfaceHeight, 0f);
				num2 += 4;
				num3 += 6;
				num4 += 6;
			}
			Vector2 vector2 = Position + vector * ((float)(-Width) / 2f);
			int num7 = rayStartIndex;
			foreach (Ray ray in Rays)
			{
				if (ray.Percent > 1f)
				{
					ray.Reset(0f);
				}
				ray.Percent += Engine.DeltaTime / ray.Duration;
				float num8 = 1f;
				if (ray.Percent < 0.1f)
				{
					num8 = Calc.ClampedMap(ray.Percent, 0f, 0.1f);
				}
				else if (ray.Percent > 0.9f)
				{
					num8 = Calc.ClampedMap(ray.Percent, 0.9f, 1f, 1f, 0f);
				}
				float num9 = Math.Max(0f, ray.Position - ray.Width / 2f);
				float num10 = Math.Min(Width, ray.Position + ray.Width / 2f);
				float num11 = Math.Min(BodyHeight, 0.7f * ray.Length);
				float num12 = 0.3f * ray.Length;
				Vector2 value = vector2 + vector * num9 + Outwards * GetSurfaceHeight(num9);
				Vector2 value2 = vector2 + vector * num10 + Outwards * GetSurfaceHeight(num10);
				Vector2 value3 = vector2 + vector * (num10 - num12) - Outwards * num11;
				Vector2 value4 = vector2 + vector * (num9 - num12) - Outwards * num11;
				mesh[num7].Position = new Vector3(value, 0f);
				mesh[num7].Color = RayTopColor * num8;
				mesh[num7 + 1].Position = new Vector3(value2, 0f);
				mesh[num7 + 1].Color = RayTopColor * num8;
				mesh[num7 + 2].Position = new Vector3(value4, 0f);
				mesh[num7 + 3].Position = new Vector3(value2, 0f);
				mesh[num7 + 3].Color = RayTopColor * num8;
				mesh[num7 + 4].Position = new Vector3(value3, 0f);
				mesh[num7 + 5].Position = new Vector3(value4, 0f);
				num7 += 6;
			}
		}

		public float GetSurfaceHeight(Vector2 position)
		{
			return GetSurfaceHeight(GetPointAlong(position));
		}

		public float GetSurfaceHeight(float position)
		{
			if (position < 0f || position > (float)Width)
			{
				return 0f;
			}
			float num = 0f;
			foreach (Ripple ripple in Ripples)
			{
				float num2 = Math.Abs(ripple.Position - position);
				float num3 = 0f;
				num3 = ((!(num2 < 12f)) ? Calc.ClampedMap(num2, 16f, 32f, -0.75f, 0f) : Calc.ClampedMap(num2, 0f, 16f, 1f, -0.75f));
				num += num3 * ripple.Height * Ease.CubeIn(1f - ripple.Percent);
			}
			num = Calc.Clamp(num, -4f, 4f);
			foreach (Tension tension in Tensions)
			{
				float t = Calc.ClampedMap(Math.Abs(tension.Position - position), 0f, 24f, 1f, 0f);
				num += Ease.CubeOut(t) * tension.Strength * 12f;
			}
			float val = position / (float)Width;
			num *= Calc.ClampedMap(val, 0f, 0.1f, 0.5f);
			num *= Calc.ClampedMap(val, 0.9f, 1f, 1f, 0.5f);
			num += (float)Math.Sin(timer + position * 0.1f);
			return num + 6f;
		}

		public void Render(Camera camera)
		{
			GFX.DrawVertices(camera.Matrix, mesh, mesh.Length);
		}
	}

	public static ParticleType P_Splash;

	public static readonly Color FillColor = Color.LightSkyBlue * 0.3f;

	public static readonly Color SurfaceColor = Color.LightSkyBlue * 0.8f;

	public static readonly Color RayTopColor = Color.LightSkyBlue * 0.6f;

	public static readonly Vector2 RayAngle = new Vector2(-4f, 8f).SafeNormalize();

	public Surface TopSurface;

	public Surface BottomSurface;

	public List<Surface> Surfaces = new List<Surface>();

	private Rectangle fill;

	private bool[,] grid;

	private Tension playerBottomTension;

	private HashSet<WaterInteraction> contains = new HashSet<WaterInteraction>();

	public Water(EntityData data, Vector2 offset)
		: this(data.Position + offset, topSurface: true, data.Bool("hasBottom"), data.Width, data.Height)
	{
	}

	public Water(Vector2 position, bool topSurface, bool bottomSurface, float width, float height)
	{
		Position = position;
		base.Tag = Tags.TransitionUpdate;
		base.Depth = -9999;
		base.Collider = new Hitbox(width, height);
		grid = new bool[(int)(width / 8f), (int)(height / 8f)];
		fill = new Rectangle(0, 0, (int)width, (int)height);
		int num = 8;
		if (topSurface)
		{
			TopSurface = new Surface(Position + new Vector2(width / 2f, num), new Vector2(0f, -1f), width, height);
			Surfaces.Add(TopSurface);
			fill.Y += num;
			fill.Height -= num;
		}
		if (bottomSurface)
		{
			BottomSurface = new Surface(Position + new Vector2(width / 2f, height - (float)num), new Vector2(0f, 1f), width, height);
			Surfaces.Add(BottomSurface);
			fill.Height -= num;
		}
		Add(new DisplacementRenderHook(RenderDisplacement));
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		int i = 0;
		for (int length = grid.GetLength(0); i < length; i++)
		{
			int j = 0;
			for (int length2 = grid.GetLength(1); j < length2; j++)
			{
				grid[i, j] = !base.Scene.CollideCheck<Solid>(new Rectangle((int)base.X + i * 8, (int)base.Y + j * 8, 8, 8));
			}
		}
	}

	public override void Update()
	{
		base.Update();
		foreach (Surface surface in Surfaces)
		{
			surface.Update();
		}
		foreach (WaterInteraction component in base.Scene.Tracker.GetComponents<WaterInteraction>())
		{
			Entity entity = component.Entity;
			bool flag = contains.Contains(component);
			bool flag2 = CollideCheck(entity);
			if (flag != flag2)
			{
				if (entity.Center.Y <= base.Center.Y && TopSurface != null)
				{
					TopSurface.DoRipple(entity.Center, 1f);
				}
				else if (entity.Center.Y > base.Center.Y && BottomSurface != null)
				{
					BottomSurface.DoRipple(entity.Center, 1f);
				}
				bool flag3 = component.IsDashing();
				int num = ((entity.Center.Y < base.Center.Y && !base.Scene.CollideCheck<Solid>(new Rectangle((int)entity.Center.X - 4, (int)entity.Center.Y, 8, 16))) ? 1 : 0);
				if (flag)
				{
					if (flag3)
					{
						Audio.Play("event:/char/madeline/water_dash_out", entity.Center, "deep", num);
					}
					else
					{
						Audio.Play("event:/char/madeline/water_out", entity.Center, "deep", num);
					}
					component.DrippingTimer = 2f;
				}
				else
				{
					if (flag3 && num == 1)
					{
						Audio.Play("event:/char/madeline/water_dash_in", entity.Center, "deep", num);
					}
					else
					{
						Audio.Play("event:/char/madeline/water_in", entity.Center, "deep", num);
					}
					component.DrippingTimer = 0f;
				}
				if (flag)
				{
					contains.Remove(component);
				}
				else
				{
					contains.Add(component);
				}
			}
			if (BottomSurface == null || !(entity is Player))
			{
				continue;
			}
			if (flag2 && entity.Y > base.Bottom - 8f)
			{
				if (playerBottomTension == null)
				{
					playerBottomTension = BottomSurface.SetTension(entity.Position, 0f);
				}
				playerBottomTension.Position = BottomSurface.GetPointAlong(entity.Position);
				playerBottomTension.Strength = Calc.ClampedMap(entity.Y, base.Bottom - 8f, base.Bottom + 4f);
			}
			else if (playerBottomTension != null)
			{
				BottomSurface.RemoveTension(playerBottomTension);
				playerBottomTension = null;
			}
		}
	}

	public void RenderDisplacement()
	{
		Color color = new Color(0.5f, 0.5f, 0.25f, 1f);
		int i = 0;
		int length = grid.GetLength(0);
		int length2 = grid.GetLength(1);
		for (; i < length; i++)
		{
			if (length2 > 0 && grid[i, 0])
			{
				Draw.Rect(base.X + (float)(i * 8), base.Y + 3f, 8f, 5f, color);
			}
			for (int j = 1; j < length2; j++)
			{
				if (grid[i, j])
				{
					int k;
					for (k = 1; j + k < length2 && grid[i, j + k]; k++)
					{
					}
					Draw.Rect(base.X + (float)(i * 8), base.Y + (float)(j * 8), 8f, k * 8, color);
					j += k - 1;
				}
			}
		}
	}

	public override void Render()
	{
		Draw.Rect(base.X + (float)fill.X, base.Y + (float)fill.Y, fill.Width, fill.Height, FillColor);
		GameplayRenderer.End();
		foreach (Surface surface in Surfaces)
		{
			surface.Render((base.Scene as Level).Camera);
		}
		GameplayRenderer.Begin();
	}
}
