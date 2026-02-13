using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Slider : Entity
{
	public enum Surfaces
	{
		Floor,
		Ceiling,
		LeftWall,
		RightWall
	}

	private const float MaxSpeed = 80f;

	private const float Accel = 400f;

	private Vector2 dir;

	private Vector2 surface;

	private bool foundSurfaceAfterCorner;

	private bool gotOutOfWall;

	private float speed;

	private bool moving;

	public Slider(Vector2 position, bool clockwise, Surfaces surface)
		: base(position)
	{
		base.Collider = new Circle(10f);
		Add(new StaticMover());
		switch (surface)
		{
		default:
			dir = Vector2.UnitX;
			this.surface = Vector2.UnitY;
			break;
		case Surfaces.Ceiling:
			dir = -Vector2.UnitX;
			this.surface = -Vector2.UnitY;
			break;
		case Surfaces.LeftWall:
			dir = -Vector2.UnitY;
			this.surface = -Vector2.UnitX;
			break;
		case Surfaces.RightWall:
			dir = Vector2.UnitY;
			this.surface = Vector2.UnitX;
			break;
		}
		if (!clockwise)
		{
			dir *= -1f;
		}
		moving = true;
		foundSurfaceAfterCorner = (gotOutOfWall = true);
		speed = 80f;
		Add(new PlayerCollider(OnPlayer));
	}

	public Slider(EntityData e, Vector2 offset)
		: this(e.Position + offset, e.Bool("clockwise", defaultValue: true), e.Enum("surface", Surfaces.Floor))
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		int num = 0;
		while (!base.Scene.CollideCheck<Solid>(Position))
		{
			Position += surface;
			if (num >= 100)
			{
				throw new Exception("Couldn't find surface");
			}
		}
	}

	private void OnPlayer(Player Player)
	{
		Player.Die((Player.Center - base.Center).SafeNormalize(-Vector2.UnitY));
		moving = false;
	}

	public override void Update()
	{
		base.Update();
		if (!moving)
		{
			return;
		}
		speed = Calc.Approach(speed, 80f, 400f * Engine.DeltaTime);
		Position += dir * speed * Engine.DeltaTime;
		if (!OnSurfaceCheck())
		{
			if (!foundSurfaceAfterCorner)
			{
				return;
			}
			Position = Position.RoundV2();
			int num = 0;
			while (!OnSurfaceCheck())
			{
				Position -= dir;
				num++;
				if (num >= 100)
				{
					throw new Exception("Couldn't get back onto corner!");
				}
			}
			foundSurfaceAfterCorner = false;
			Vector2 vector = dir;
			dir = surface;
			surface = -vector;
			return;
		}
		foundSurfaceAfterCorner = true;
		if (InWallCheck())
		{
			if (!gotOutOfWall)
			{
				return;
			}
			Position = Position.RoundV2();
			int num2 = 0;
			while (InWallCheck())
			{
				Position -= dir;
				num2++;
				if (num2 >= 100)
				{
					throw new Exception("Couldn't get out of wall!");
				}
			}
			Position += dir - surface;
			gotOutOfWall = false;
			Vector2 vector2 = surface;
			surface = dir;
			dir = -vector2;
		}
		else
		{
			gotOutOfWall = true;
		}
	}

	private bool OnSurfaceCheck()
	{
		return base.Scene.CollideCheck<Solid>(Position.RoundV2() + surface);
	}

	private bool InWallCheck()
	{
		return base.Scene.CollideCheck<Solid>(Position.RoundV2() - surface);
	}

	public override void Render()
	{
		Draw.Circle(Position, 12f, Color.Red, 8);
	}
}
