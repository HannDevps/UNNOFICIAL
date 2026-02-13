using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(true)]
public class Solid : Platform
{
	public Vector2 Speed;

	public bool AllowStaticMovers = true;

	public bool EnableAssistModeChecks = true;

	public bool DisableLightsInside = true;

	public bool StopPlayerRunIntoAnimation = true;

	public bool SquishEvenInAssistMode;

	private static HashSet<Actor> riders = new HashSet<Actor>();

	public Solid(Vector2 position, float width, float height, bool safe)
		: base(position, safe)
	{
		base.Collider = new Hitbox(width, height);
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (!AllowStaticMovers)
		{
			return;
		}
		bool collidable = Collidable;
		Collidable = true;
		foreach (StaticMover component in scene.Tracker.GetComponents<StaticMover>())
		{
			if (component.IsRiding(this) && component.Platform == null)
			{
				staticMovers.Add(component);
				component.Platform = this;
				if (component.OnAttach != null)
				{
					component.OnAttach(this);
				}
			}
		}
		Collidable = collidable;
	}

	public override void Update()
	{
		base.Update();
		MoveH(Speed.X * Engine.DeltaTime);
		MoveV(Speed.Y * Engine.DeltaTime);
		if (!EnableAssistModeChecks || SaveData.Instance == null || !SaveData.Instance.Assists.Invincible || base.Components.Get<SolidOnInvinciblePlayer>() != null || !Collidable)
		{
			return;
		}
		Player player = CollideFirst<Player>();
		Level level = base.Scene as Level;
		if (player == null && base.Bottom > (float)level.Bounds.Bottom)
		{
			player = CollideFirst<Player>(Position + Vector2.UnitY);
		}
		if (player != null && player.StateMachine.State != 9 && player.StateMachine.State != 21)
		{
			Add(new SolidOnInvinciblePlayer());
			return;
		}
		TheoCrystal theoCrystal = CollideFirst<TheoCrystal>();
		if (theoCrystal != null && !theoCrystal.Hold.IsHeld)
		{
			Add(new SolidOnInvinciblePlayer());
		}
	}

	public bool HasRider()
	{
		foreach (Actor entity in base.Scene.Tracker.GetEntities<Actor>())
		{
			if (entity.IsRiding(this))
			{
				return true;
			}
		}
		return false;
	}

	public Player GetPlayerRider()
	{
		foreach (Player entity in base.Scene.Tracker.GetEntities<Player>())
		{
			if (entity.IsRiding(this))
			{
				return entity;
			}
		}
		return null;
	}

	public bool HasPlayerRider()
	{
		return GetPlayerRider() != null;
	}

	public bool HasPlayerOnTop()
	{
		return GetPlayerOnTop() != null;
	}

	public Player GetPlayerOnTop()
	{
		return CollideFirst<Player>(Position - Vector2.UnitY);
	}

	public bool HasPlayerClimbing()
	{
		return GetPlayerClimbing() != null;
	}

	public Player GetPlayerClimbing()
	{
		foreach (Player entity in base.Scene.Tracker.GetEntities<Player>())
		{
			if (entity.StateMachine.State == 1)
			{
				if (entity.Facing == Facings.Left && CollideCheck(entity, Position + Vector2.UnitX))
				{
					return entity;
				}
				if (entity.Facing == Facings.Right && CollideCheck(entity, Position - Vector2.UnitX))
				{
					return entity;
				}
			}
		}
		return null;
	}

	public void GetRiders()
	{
		foreach (Actor entity in base.Scene.Tracker.GetEntities<Actor>())
		{
			if (entity.IsRiding(this))
			{
				riders.Add(entity);
			}
		}
	}

	public override void MoveHExact(int move)
	{
		GetRiders();
		float right = base.Right;
		float left = base.Left;
		Player player = null;
		player = base.Scene.Tracker.GetEntity<Player>();
		if (player != null && Input.MoveX.Value == Math.Sign(move) && Math.Sign(player.Speed.X) == Math.Sign(move) && !riders.Contains(player) && CollideCheck(player, Position + Vector2.UnitX * move - Vector2.UnitY))
		{
			player.MoveV(1f);
		}
		base.X += move;
		MoveStaticMovers(Vector2.UnitX * move);
		if (Collidable)
		{
			foreach (Actor entity in base.Scene.Tracker.GetEntities<Actor>())
			{
				if (!entity.AllowPushing)
				{
					continue;
				}
				bool collidable = entity.Collidable;
				entity.Collidable = true;
				if (!entity.TreatNaive && CollideCheck(entity, Position))
				{
					int moveH = ((move <= 0) ? (move - (int)(entity.Right - left)) : (move - (int)(entity.Left - right)));
					Collidable = false;
					entity.MoveHExact(moveH, entity.SquishCallback, this);
					entity.LiftSpeed = LiftSpeed;
					Collidable = true;
				}
				else if (riders.Contains(entity))
				{
					Collidable = false;
					if (entity.TreatNaive)
					{
						entity.NaiveMove(Vector2.UnitX * move);
					}
					else
					{
						entity.MoveHExact(move);
					}
					entity.LiftSpeed = LiftSpeed;
					Collidable = true;
				}
				entity.Collidable = collidable;
			}
		}
		riders.Clear();
	}

	public override void MoveVExact(int move)
	{
		GetRiders();
		float bottom = base.Bottom;
		float top = base.Top;
		base.Y += move;
		MoveStaticMovers(Vector2.UnitY * move);
		if (Collidable)
		{
			foreach (Actor entity in base.Scene.Tracker.GetEntities<Actor>())
			{
				if (!entity.AllowPushing)
				{
					continue;
				}
				bool collidable = entity.Collidable;
				entity.Collidable = true;
				if (!entity.TreatNaive && CollideCheck(entity, Position))
				{
					int moveV = ((move <= 0) ? (move - (int)(entity.Bottom - top)) : (move - (int)(entity.Top - bottom)));
					Collidable = false;
					entity.MoveVExact(moveV, entity.SquishCallback, this);
					entity.LiftSpeed = LiftSpeed;
					Collidable = true;
				}
				else if (riders.Contains(entity))
				{
					Collidable = false;
					if (entity.TreatNaive)
					{
						entity.NaiveMove(Vector2.UnitY * move);
					}
					else
					{
						entity.MoveVExact(move);
					}
					entity.LiftSpeed = LiftSpeed;
					Collidable = true;
				}
				entity.Collidable = collidable;
			}
		}
		riders.Clear();
	}
}
