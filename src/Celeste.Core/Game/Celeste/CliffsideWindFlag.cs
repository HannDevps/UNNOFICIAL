using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CliffsideWindFlag : Entity
{
	private class Segment
	{
		public MTexture Texture;

		public Vector2 Offset;
	}

	private Segment[] segments;

	private float sine;

	private float random;

	private int sign;

	private float wind => Calc.ClampedMap(Math.Abs((base.Scene as Level).Wind.X), 0f, 800f);

	public CliffsideWindFlag(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		MTexture atlasSubtexturesAt = GFX.Game.GetAtlasSubtexturesAt("scenery/cliffside/flag", data.Int("index"));
		segments = new Segment[atlasSubtexturesAt.Width];
		for (int i = 0; i < segments.Length; i++)
		{
			Segment segment = new Segment
			{
				Texture = atlasSubtexturesAt.GetSubtexture(i, 0, 1, atlasSubtexturesAt.Height),
				Offset = new Vector2(i, 0f)
			};
			segments[i] = segment;
		}
		sine = Calc.Random.NextFloat((float)Math.PI * 2f);
		random = Calc.Random.NextFloat();
		base.Depth = 8999;
		base.Tag = Tags.TransitionUpdate;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		sign = 1;
		if (wind != 0f)
		{
			sign = Math.Sign((base.Scene as Level).Wind.X);
		}
		for (int i = 0; i < segments.Length; i++)
		{
			SetFlagSegmentPosition(i, snap: true);
		}
	}

	public override void Update()
	{
		base.Update();
		if (wind != 0f)
		{
			sign = Math.Sign((base.Scene as Level).Wind.X);
		}
		sine += Engine.DeltaTime * (4f + wind * 4f) * (0.8f + random * 0.2f);
		for (int i = 0; i < segments.Length; i++)
		{
			SetFlagSegmentPosition(i, snap: false);
		}
	}

	private float Sin(float timer)
	{
		return (float)Math.Sin(0f - timer);
	}

	private void SetFlagSegmentPosition(int i, bool snap)
	{
		Segment segment = segments[i];
		float value = (float)(i * sign) * (0.2f + wind * 0.8f * (0.8f + random * 0.2f)) * (0.9f + Sin(sine) * 0.1f);
		float num = Calc.LerpClamp(Sin(sine * 0.5f - (float)i * 0.1f) * ((float)i / (float)segments.Length) * (float)i * 0.2f, value, (float)Math.Ceiling(wind));
		float num2 = (float)i / (float)segments.Length * Math.Max(0.1f, 1f - wind) * 16f;
		if (!snap)
		{
			segment.Offset.X = Calc.Approach(segment.Offset.X, num, Engine.DeltaTime * 40f);
			segment.Offset.Y = Calc.Approach(segment.Offset.Y, num2, Engine.DeltaTime * 40f);
		}
		else
		{
			segment.Offset.X = num;
			segment.Offset.Y = num2;
		}
	}

	public override void Render()
	{
		base.Render();
		for (int i = 0; i < segments.Length; i++)
		{
			Segment segment = segments[i];
			float num = (float)i / (float)segments.Length * Sin((float)(-i) * 0.1f + sine) * 2f;
			segment.Texture.Draw(Position + segment.Offset + Vector2.UnitY * num);
		}
	}
}
