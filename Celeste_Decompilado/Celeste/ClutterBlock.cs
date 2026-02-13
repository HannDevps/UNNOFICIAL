using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ClutterBlock : Entity
{
	public enum Colors
	{
		Red,
		Green,
		Yellow,
		Lightning
	}

	public Colors BlockColor;

	public Image Image;

	public HashSet<ClutterBlock> HasBelow = new HashSet<ClutterBlock>();

	public List<ClutterBlock> Below = new List<ClutterBlock>();

	public List<ClutterBlock> Above = new List<ClutterBlock>();

	public bool OnTheGround;

	public bool TopSideOpen;

	public bool LeftSideOpen;

	public bool RightSideOpen;

	private float floatTarget;

	private float floatDelay;

	private float floatTimer;

	private float WaveTarget => 0f - ((float)Math.Sin((float)((int)Position.X / 16) * 0.25f + floatTimer * 2f) + 1f) / 2f - 1f;

	public ClutterBlock(Vector2 position, MTexture texture, Colors color)
		: base(position)
	{
		BlockColor = color;
		Add(Image = new Image(texture));
		base.Collider = new Hitbox(texture.Width, texture.Height);
		base.Depth = -9998;
	}

	public void WeightDown()
	{
		foreach (ClutterBlock item in Below)
		{
			item.WeightDown();
		}
		floatTarget = 0f;
		floatDelay = 0.1f;
	}

	public override void Update()
	{
		base.Update();
		if (OnTheGround)
		{
			return;
		}
		if (floatDelay <= 0f)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && ((TopSideOpen && entity.Right > base.Left && entity.Left < base.Right && entity.Bottom >= base.Top - 1f && entity.Bottom <= base.Top + 4f) | (entity.StateMachine.State == 1 && LeftSideOpen && entity.Right >= base.Left - 1f && entity.Right < base.Left + 4f && entity.Bottom > base.Top && entity.Top < base.Bottom) | (entity.StateMachine.State == 1 && RightSideOpen && entity.Left <= base.Right + 1f && entity.Left > base.Right - 4f && entity.Bottom > base.Top && entity.Top < base.Bottom)))
			{
				WeightDown();
			}
		}
		floatTimer += Engine.DeltaTime;
		floatDelay -= Engine.DeltaTime;
		if (floatDelay <= 0f)
		{
			floatTarget = Calc.Approach(floatTarget, WaveTarget, Engine.DeltaTime * 4f);
		}
		Image.Y = floatTarget;
	}

	public void Absorb(ClutterAbsorbEffect effect)
	{
		effect.FlyClutter(Position + new Vector2(Image.Width * 0.5f, Image.Height * 0.5f + floatTarget), Image.Texture, shake: true, Calc.Random.NextFloat(0.5f));
		base.Scene.Remove(this);
	}
}
