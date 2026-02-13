using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class LavaRect : Component
{
	public enum OnlyModes
	{
		None,
		OnlyTop,
		OnlyBottom
	}

	private struct Bubble
	{
		public Vector2 Position;

		public float Speed;

		public float Alpha;
	}

	private struct SurfaceBubble
	{
		public float X;

		public float Frame;

		public byte Animation;
	}

	public Vector2 Position;

	public float Fade = 16f;

	public float Spikey;

	public OnlyModes OnlyMode;

	public float SmallWaveAmplitude = 1f;

	public float BigWaveAmplitude = 4f;

	public float CurveAmplitude = 12f;

	public float UpdateMultiplier = 1f;

	public Color SurfaceColor = Color.White;

	public Color EdgeColor = Color.LightGray;

	public Color CenterColor = Color.DarkGray;

	private float timer = Calc.Random.NextFloat(100f);

	private VertexPositionColor[] verts;

	private bool dirty;

	private int vertCount;

	private Bubble[] bubbles;

	private SurfaceBubble[] surfaceBubbles;

	private int surfaceBubbleIndex;

	private List<List<MTexture>> surfaceBubbleAnimations;

	public int SurfaceStep { get; private set; }

	public float Width { get; private set; }

	public float Height { get; private set; }

	public LavaRect(float width, float height, int step)
		: base(active: true, visible: true)
	{
		Resize(width, height, step);
	}

	public void Resize(float width, float height, int step)
	{
		Width = width;
		Height = height;
		SurfaceStep = step;
		dirty = true;
		int num = (int)(width / (float)SurfaceStep * 2f + height / (float)SurfaceStep * 2f + 4f);
		verts = new VertexPositionColor[num * 3 * 6 + 6];
		bubbles = new Bubble[(int)(width * height * 0.005f)];
		surfaceBubbles = new SurfaceBubble[(int)Math.Max(4f, (float)bubbles.Length * 0.25f)];
		for (int i = 0; i < bubbles.Length; i++)
		{
			bubbles[i].Position = new Vector2(1f + Calc.Random.NextFloat(Width - 2f), Calc.Random.NextFloat(Height));
			bubbles[i].Speed = Calc.Random.Range(4, 12);
			bubbles[i].Alpha = Calc.Random.Range(0.4f, 0.8f);
		}
		for (int j = 0; j < surfaceBubbles.Length; j++)
		{
			surfaceBubbles[j].X = -1f;
		}
		surfaceBubbleAnimations = new List<List<MTexture>>();
		surfaceBubbleAnimations.Add(GFX.Game.GetAtlasSubtextures("danger/lava/bubble_a"));
	}

	public override void Update()
	{
		timer += UpdateMultiplier * Engine.DeltaTime;
		if (UpdateMultiplier != 0f)
		{
			dirty = true;
		}
		for (int i = 0; i < bubbles.Length; i++)
		{
			bubbles[i].Position.Y -= UpdateMultiplier * bubbles[i].Speed * Engine.DeltaTime;
			if (bubbles[i].Position.Y < 2f - Wave((int)(bubbles[i].Position.X / (float)SurfaceStep), Width))
			{
				bubbles[i].Position.Y = Height - 1f;
				if (Calc.Random.Chance(0.75f))
				{
					surfaceBubbles[surfaceBubbleIndex].X = bubbles[i].Position.X;
					surfaceBubbles[surfaceBubbleIndex].Frame = 0f;
					surfaceBubbles[surfaceBubbleIndex].Animation = (byte)Calc.Random.Next(surfaceBubbleAnimations.Count);
					surfaceBubbleIndex = (surfaceBubbleIndex + 1) % surfaceBubbles.Length;
				}
			}
		}
		for (int j = 0; j < surfaceBubbles.Length; j++)
		{
			if (surfaceBubbles[j].X >= 0f)
			{
				surfaceBubbles[j].Frame += Engine.DeltaTime * 6f;
				if (surfaceBubbles[j].Frame >= (float)surfaceBubbleAnimations[surfaceBubbles[j].Animation].Count)
				{
					surfaceBubbles[j].X = -1f;
				}
			}
		}
		base.Update();
	}

	private float Sin(float value)
	{
		return (1f + (float)Math.Sin(value)) / 2f;
	}

	private float Wave(int step, float length)
	{
		int num = step * SurfaceStep;
		float num2 = ((OnlyMode != OnlyModes.None) ? 1f : (Calc.ClampedMap(num, 0f, length * 0.1f) * Calc.ClampedMap(num, length * 0.9f, length, 1f, 0f)));
		float num3 = Sin((float)num * 0.25f + timer * 4f) * SmallWaveAmplitude;
		num3 += Sin((float)num * 0.05f + timer * 0.5f) * BigWaveAmplitude;
		if (step % 2 == 0)
		{
			num3 += Spikey;
		}
		if (OnlyMode != OnlyModes.None)
		{
			num3 += (1f - Calc.YoYo((float)num / length)) * CurveAmplitude;
		}
		return num3 * num2;
	}

	private void Quad(ref int vert, Vector2 va, Vector2 vb, Vector2 vc, Vector2 vd, Color color)
	{
		Quad(ref vert, va, color, vb, color, vc, color, vd, color);
	}

	private void Quad(ref int vert, Vector2 va, Color ca, Vector2 vb, Color cb, Vector2 vc, Color cc, Vector2 vd, Color cd)
	{
		verts[vert].Position.X = va.X;
		verts[vert].Position.Y = va.Y;
		verts[vert++].Color = ca;
		verts[vert].Position.X = vb.X;
		verts[vert].Position.Y = vb.Y;
		verts[vert++].Color = cb;
		verts[vert].Position.X = vc.X;
		verts[vert].Position.Y = vc.Y;
		verts[vert++].Color = cc;
		verts[vert].Position.X = va.X;
		verts[vert].Position.Y = va.Y;
		verts[vert++].Color = ca;
		verts[vert].Position.X = vc.X;
		verts[vert].Position.Y = vc.Y;
		verts[vert++].Color = cc;
		verts[vert].Position.X = vd.X;
		verts[vert].Position.Y = vd.Y;
		verts[vert++].Color = cd;
	}

	private void Edge(ref int vert, Vector2 a, Vector2 b, float fade, float insetFade)
	{
		float num = (a - b).Length();
		float num2 = ((OnlyMode == OnlyModes.None) ? (insetFade / num) : 0f);
		float num3 = num / (float)SurfaceStep;
		Vector2 vector = (b - a).SafeNormalize().Perpendicular();
		for (int i = 1; (float)i <= num3; i++)
		{
			Vector2 vector2 = Vector2.Lerp(a, b, (float)(i - 1) / num3);
			float num4 = Wave(i - 1, num);
			Vector2 vector3 = vector2 - vector * num4;
			Vector2 vector4 = Vector2.Lerp(a, b, (float)i / num3);
			float num5 = Wave(i, num);
			Vector2 vector5 = vector4 - vector * num5;
			Vector2 vector6 = Vector2.Lerp(a, b, Calc.ClampedMap((float)(i - 1) / num3, 0f, 1f, num2, 1f - num2));
			Vector2 vector7 = Vector2.Lerp(a, b, Calc.ClampedMap((float)i / num3, 0f, 1f, num2, 1f - num2));
			Quad(ref vert, vector3 + vector, EdgeColor, vector5 + vector, EdgeColor, vector7 + vector * (fade - num5), CenterColor, vector6 + vector * (fade - num4), CenterColor);
			Quad(ref vert, vector6 + vector * (fade - num4), vector7 + vector * (fade - num5), vector7 + vector * fade, vector6 + vector * fade, CenterColor);
			Quad(ref vert, vector3, vector5, vector5 + vector * 1f, vector3 + vector * 1f, SurfaceColor);
		}
	}

	public override void Render()
	{
		GameplayRenderer.End();
		if (dirty)
		{
			Vector2 zero = Vector2.Zero;
			Vector2 vector = zero;
			Vector2 vector2 = new Vector2(zero.X + Width, zero.Y);
			Vector2 vector3 = new Vector2(zero.X, zero.Y + Height);
			Vector2 vector4 = zero + new Vector2(Width, Height);
			Vector2 vector5 = new Vector2(Math.Min(Fade, Width / 2f), Math.Min(Fade, Height / 2f));
			vertCount = 0;
			if (OnlyMode == OnlyModes.None)
			{
				Edge(ref vertCount, vector, vector2, vector5.Y, vector5.X);
				Edge(ref vertCount, vector2, vector4, vector5.X, vector5.Y);
				Edge(ref vertCount, vector4, vector3, vector5.Y, vector5.X);
				Edge(ref vertCount, vector3, vector, vector5.X, vector5.Y);
				Quad(ref vertCount, vector + vector5, vector2 + new Vector2(0f - vector5.X, vector5.Y), vector4 - vector5, vector3 + new Vector2(vector5.X, 0f - vector5.Y), CenterColor);
			}
			else if (OnlyMode == OnlyModes.OnlyTop)
			{
				Edge(ref vertCount, vector, vector2, vector5.Y, 0f);
				Quad(ref vertCount, vector + new Vector2(0f, vector5.Y), vector2 + new Vector2(0f, vector5.Y), vector4, vector3, CenterColor);
			}
			else if (OnlyMode == OnlyModes.OnlyBottom)
			{
				Edge(ref vertCount, vector4, vector3, vector5.Y, 0f);
				Quad(ref vertCount, vector, vector2, vector4 + new Vector2(0f, 0f - vector5.Y), vector3 + new Vector2(0f, 0f - vector5.Y), CenterColor);
			}
			dirty = false;
		}
		Camera camera = (base.Scene as Level).Camera;
		GFX.DrawVertices(Matrix.CreateTranslation(new Vector3(base.Entity.Position + Position, 0f)) * camera.Matrix, verts, vertCount);
		GameplayRenderer.Begin();
		Vector2 vector6 = base.Entity.Position + Position;
		MTexture mTexture = GFX.Game["particles/bubble"];
		for (int i = 0; i < bubbles.Length; i++)
		{
			mTexture.DrawCentered(vector6 + bubbles[i].Position, SurfaceColor * bubbles[i].Alpha);
		}
		for (int j = 0; j < surfaceBubbles.Length; j++)
		{
			if (surfaceBubbles[j].X >= 0f)
			{
				MTexture mTexture2 = surfaceBubbleAnimations[surfaceBubbles[j].Animation][(int)surfaceBubbles[j].Frame];
				int num = (int)(surfaceBubbles[j].X / (float)SurfaceStep);
				mTexture2.DrawJustified(vector6 + new Vector2(y: 1f - Wave(num, Width), x: num * SurfaceStep), new Vector2(0.5f, 1f), SurfaceColor);
			}
		}
	}
}
