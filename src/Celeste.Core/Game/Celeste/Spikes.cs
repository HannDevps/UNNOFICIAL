using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class Spikes : Entity
{
	public enum Directions
	{
		Up,
		Down,
		Left,
		Right
	}

	public const string TentacleType = "tentacles";

	public Directions Direction;

	private PlayerCollider pc;

	private Vector2 imageOffset;

	private int size;

	private string overrideType;

	private string spikeType;

	public Color EnabledColor = Color.White;

	public Color DisabledColor = Color.White;

	public bool VisibleWhenDisabled;

	public Spikes(Vector2 position, int size, Directions direction, string type)
		: base(position)
	{
		base.Depth = -1;
		Direction = direction;
		this.size = size;
		overrideType = type;
		switch (direction)
		{
		case Directions.Up:
			base.Collider = new Hitbox(size, 3f, 0f, -3f);
			Add(new LedgeBlocker());
			break;
		case Directions.Down:
			base.Collider = new Hitbox(size, 3f);
			break;
		case Directions.Left:
			base.Collider = new Hitbox(3f, size, -3f);
			Add(new LedgeBlocker());
			break;
		case Directions.Right:
			base.Collider = new Hitbox(3f, size);
			Add(new LedgeBlocker());
			break;
		}
		Add(pc = new PlayerCollider(OnCollide));
		Add(new StaticMover
		{
			OnShake = OnShake,
			SolidChecker = IsRiding,
			JumpThruChecker = IsRiding,
			OnEnable = OnEnable,
			OnDisable = OnDisable
		});
	}

	public Spikes(EntityData data, Vector2 offset, Directions dir)
		: this(data.Position + offset, GetSize(data, dir), dir, data.Attr("type", "default"))
	{
	}

	public void SetSpikeColor(Color color)
	{
		foreach (Component component in base.Components)
		{
			if (component is Image image)
			{
				image.Color = color;
			}
		}
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		AreaData areaData = AreaData.Get(scene);
		spikeType = areaData.Spike;
		if (!string.IsNullOrEmpty(overrideType) && !overrideType.Equals("default"))
		{
			spikeType = overrideType;
		}
		string text = Direction.ToString().ToLower();
		if (spikeType == "tentacles")
		{
			for (int i = 0; i < size / 16; i++)
			{
				AddTentacle(i);
			}
			if (size / 8 % 2 == 1)
			{
				AddTentacle((float)(size / 16) - 0.5f);
			}
			return;
		}
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("danger/spikes/" + spikeType + "_" + text);
		for (int j = 0; j < size / 8; j++)
		{
			Image image = new Image(Calc.Random.Choose(atlasSubtextures));
			switch (Direction)
			{
			case Directions.Up:
				image.JustifyOrigin(0.5f, 1f);
				image.Position = Vector2.UnitX * ((float)j + 0.5f) * 8f + Vector2.UnitY;
				break;
			case Directions.Down:
				image.JustifyOrigin(0.5f, 0f);
				image.Position = Vector2.UnitX * ((float)j + 0.5f) * 8f - Vector2.UnitY;
				break;
			case Directions.Right:
				image.JustifyOrigin(0f, 0.5f);
				image.Position = Vector2.UnitY * ((float)j + 0.5f) * 8f - Vector2.UnitX;
				break;
			case Directions.Left:
				image.JustifyOrigin(1f, 0.5f);
				image.Position = Vector2.UnitY * ((float)j + 0.5f) * 8f + Vector2.UnitX;
				break;
			}
			Add(image);
		}
	}

	private void AddTentacle(float i)
	{
		Sprite sprite = GFX.SpriteBank.Create("tentacles");
		sprite.Play(Calc.Random.Next(3).ToString(), restart: true, randomizeFrame: true);
		sprite.Position = ((Direction == Directions.Up || Direction == Directions.Down) ? Vector2.UnitX : Vector2.UnitY) * (i + 0.5f) * 16f;
		sprite.Scale.X = Calc.Random.Choose(-1, 1);
		sprite.SetAnimationFrame(Calc.Random.Next(sprite.CurrentAnimationTotalFrames));
		if (Direction == Directions.Up)
		{
			sprite.Rotation = -(float)Math.PI / 2f;
			sprite.Y++;
		}
		else if (Direction == Directions.Right)
		{
			sprite.Rotation = 0f;
			sprite.X--;
		}
		else if (Direction == Directions.Left)
		{
			sprite.Rotation = (float)Math.PI;
			sprite.X++;
		}
		else if (Direction == Directions.Down)
		{
			sprite.Rotation = (float)Math.PI / 2f;
			sprite.Y--;
		}
		sprite.Rotation += (float)Math.PI / 2f;
		Add(sprite);
	}

	private void OnEnable()
	{
		Active = (Visible = (Collidable = true));
		SetSpikeColor(EnabledColor);
	}

	private void OnDisable()
	{
		Active = (Collidable = false);
		if (VisibleWhenDisabled)
		{
			foreach (Component component in base.Components)
			{
				if (component is Image image)
				{
					image.Color = DisabledColor;
				}
			}
			return;
		}
		Visible = false;
	}

	private void OnShake(Vector2 amount)
	{
		imageOffset += amount;
	}

	public override void Render()
	{
		Vector2 position = Position;
		Position += imageOffset;
		base.Render();
		Position = position;
	}

	public void SetOrigins(Vector2 origin)
	{
		foreach (Component component in base.Components)
		{
			if (component is Image image)
			{
				Vector2 vector = origin - Position;
				image.Origin = image.Origin + vector - image.Position;
				image.Position = vector;
			}
		}
	}

	private void OnCollide(Player player)
	{
		switch (Direction)
		{
		case Directions.Up:
			if (player.Speed.Y >= 0f && player.Bottom <= base.Bottom)
			{
				player.Die(new Vector2(0f, -1f));
			}
			break;
		case Directions.Down:
			if (player.Speed.Y <= 0f)
			{
				player.Die(new Vector2(0f, 1f));
			}
			break;
		case Directions.Left:
			if (player.Speed.X >= 0f)
			{
				player.Die(new Vector2(-1f, 0f));
			}
			break;
		case Directions.Right:
			if (player.Speed.X <= 0f)
			{
				player.Die(new Vector2(1f, 0f));
			}
			break;
		}
	}

	private static int GetSize(EntityData data, Directions dir)
	{
		if ((uint)dir > 1u)
		{
			_ = dir - 2;
			_ = 1;
			return data.Height;
		}
		return data.Width;
	}

	private bool IsRiding(Solid solid)
	{
		return Direction switch
		{
			Directions.Up => CollideCheckOutside(solid, Position + Vector2.UnitY), 
			Directions.Down => CollideCheckOutside(solid, Position - Vector2.UnitY), 
			Directions.Left => CollideCheckOutside(solid, Position + Vector2.UnitX), 
			Directions.Right => CollideCheckOutside(solid, Position - Vector2.UnitX), 
			_ => false, 
		};
	}

	private bool IsRiding(JumpThru jumpThru)
	{
		if (Direction != Directions.Up)
		{
			return false;
		}
		return CollideCheck(jumpThru, Position + Vector2.UnitY);
	}
}
