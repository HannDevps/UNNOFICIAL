using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(true)]
public abstract class Platform : Entity
{
	private Vector2 movementCounter;

	private Vector2 shakeAmount;

	private bool shaking;

	private float shakeTimer;

	protected List<StaticMover> staticMovers = new List<StaticMover>();

	public Vector2 LiftSpeed;

	public bool Safe;

	public bool BlockWaterfalls = true;

	public int SurfaceSoundIndex = 8;

	public int SurfaceSoundPriority;

	public DashCollision OnDashCollide;

	public Action<Vector2> OnCollide;

	public Vector2 Shake => shakeAmount;

	public Hitbox Hitbox => base.Collider as Hitbox;

	public Vector2 ExactPosition => Position + movementCounter;

	public Platform(Vector2 position, bool safe)
		: base(position)
	{
		Safe = safe;
		base.Depth = -9000;
	}

	public void ClearRemainder()
	{
		movementCounter = Vector2.Zero;
	}

	public override void Update()
	{
		base.Update();
		LiftSpeed = Vector2.Zero;
		if (!shaking)
		{
			return;
		}
		if (base.Scene.OnInterval(0.04f))
		{
			Vector2 vector = shakeAmount;
			shakeAmount = Calc.Random.ShakeVector();
			OnShake(shakeAmount - vector);
		}
		if (shakeTimer > 0f)
		{
			shakeTimer -= Engine.DeltaTime;
			if (shakeTimer <= 0f)
			{
				shaking = false;
				StopShaking();
			}
		}
	}

	public void StartShaking(float time = 0f)
	{
		shaking = true;
		shakeTimer = time;
	}

	public void StopShaking()
	{
		shaking = false;
		if (shakeAmount != Vector2.Zero)
		{
			OnShake(-shakeAmount);
			shakeAmount = Vector2.Zero;
		}
	}

	public virtual void OnShake(Vector2 amount)
	{
		ShakeStaticMovers(amount);
	}

	public void ShakeStaticMovers(Vector2 amount)
	{
		foreach (StaticMover staticMover in staticMovers)
		{
			staticMover.Shake(amount);
		}
	}

	public void MoveStaticMovers(Vector2 amount)
	{
		foreach (StaticMover staticMover in staticMovers)
		{
			staticMover.Move(amount);
		}
	}

	public void DestroyStaticMovers()
	{
		foreach (StaticMover staticMover in staticMovers)
		{
			staticMover.Destroy();
		}
		staticMovers.Clear();
	}

	public void DisableStaticMovers()
	{
		foreach (StaticMover staticMover in staticMovers)
		{
			staticMover.Disable();
		}
	}

	public void EnableStaticMovers()
	{
		foreach (StaticMover staticMover in staticMovers)
		{
			staticMover.Enable();
		}
	}

	public virtual void OnStaticMoverTrigger(StaticMover sm)
	{
	}

	public virtual int GetLandSoundIndex(Entity entity)
	{
		return SurfaceSoundIndex;
	}

	public virtual int GetWallSoundIndex(Player player, int side)
	{
		return SurfaceSoundIndex;
	}

	public virtual int GetStepSoundIndex(Entity entity)
	{
		return SurfaceSoundIndex;
	}

	public void MoveH(float moveH)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.X = 0f;
		}
		else
		{
			LiftSpeed.X = moveH / Engine.DeltaTime;
		}
		movementCounter.X += moveH;
		int num = (int)Math.Round(movementCounter.X);
		if (num != 0)
		{
			movementCounter.X -= num;
			MoveHExact(num);
		}
	}

	public void MoveH(float moveH, float liftSpeedH)
	{
		LiftSpeed.X = liftSpeedH;
		movementCounter.X += moveH;
		int num = (int)Math.Round(movementCounter.X);
		if (num != 0)
		{
			movementCounter.X -= num;
			MoveHExact(num);
		}
	}

	public void MoveV(float moveV)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.Y = 0f;
		}
		else
		{
			LiftSpeed.Y = moveV / Engine.DeltaTime;
		}
		movementCounter.Y += moveV;
		int num = (int)Math.Round(movementCounter.Y);
		if (num != 0)
		{
			movementCounter.Y -= num;
			MoveVExact(num);
		}
	}

	public void MoveV(float moveV, float liftSpeedV)
	{
		LiftSpeed.Y = liftSpeedV;
		movementCounter.Y += moveV;
		int num = (int)Math.Round(movementCounter.Y);
		if (num != 0)
		{
			movementCounter.Y -= num;
			MoveVExact(num);
		}
	}

	public void MoveToX(float x)
	{
		MoveH(x - ExactPosition.X);
	}

	public void MoveToX(float x, float liftSpeedX)
	{
		MoveH(x - ExactPosition.X, liftSpeedX);
	}

	public void MoveToY(float y)
	{
		MoveV(y - ExactPosition.Y);
	}

	public void MoveToY(float y, float liftSpeedY)
	{
		MoveV(y - ExactPosition.Y, liftSpeedY);
	}

	public void MoveTo(Vector2 position)
	{
		MoveToX(position.X);
		MoveToY(position.Y);
	}

	public void MoveTo(Vector2 position, Vector2 liftSpeed)
	{
		MoveToX(position.X, liftSpeed.X);
		MoveToY(position.Y, liftSpeed.Y);
	}

	public void MoveTowardsX(float x, float amount)
	{
		float x2 = Calc.Approach(ExactPosition.X, x, amount);
		MoveToX(x2);
	}

	public void MoveTowardsY(float y, float amount)
	{
		float y2 = Calc.Approach(ExactPosition.Y, y, amount);
		MoveToY(y2);
	}

	public abstract void MoveHExact(int move);

	public abstract void MoveVExact(int move);

	public void MoveToNaive(Vector2 position)
	{
		MoveToXNaive(position.X);
		MoveToYNaive(position.Y);
	}

	public void MoveToXNaive(float x)
	{
		MoveHNaive(x - ExactPosition.X);
	}

	public void MoveToYNaive(float y)
	{
		MoveVNaive(y - ExactPosition.Y);
	}

	public void MoveHNaive(float moveH)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.X = 0f;
		}
		else
		{
			LiftSpeed.X = moveH / Engine.DeltaTime;
		}
		movementCounter.X += moveH;
		int num = (int)Math.Round(movementCounter.X);
		if (num != 0)
		{
			movementCounter.X -= num;
			base.X += num;
			MoveStaticMovers(Vector2.UnitX * num);
		}
	}

	public void MoveVNaive(float moveV)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.Y = 0f;
		}
		else
		{
			LiftSpeed.Y = moveV / Engine.DeltaTime;
		}
		movementCounter.Y += moveV;
		int num = (int)Math.Round(movementCounter.Y);
		if (num != 0)
		{
			movementCounter.Y -= num;
			base.Y += num;
			MoveStaticMovers(Vector2.UnitY * num);
		}
	}

	public bool MoveHCollideSolids(float moveH, bool thruDashBlocks, Action<Vector2, Vector2, Platform> onCollide = null)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.X = 0f;
		}
		else
		{
			LiftSpeed.X = moveH / Engine.DeltaTime;
		}
		movementCounter.X += moveH;
		int num = (int)Math.Round(movementCounter.X);
		if (num != 0)
		{
			movementCounter.X -= num;
			return MoveHExactCollideSolids(num, thruDashBlocks, onCollide);
		}
		return false;
	}

	public bool MoveVCollideSolids(float moveV, bool thruDashBlocks, Action<Vector2, Vector2, Platform> onCollide = null)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.Y = 0f;
		}
		else
		{
			LiftSpeed.Y = moveV / Engine.DeltaTime;
		}
		movementCounter.Y += moveV;
		int num = (int)Math.Round(movementCounter.Y);
		if (num != 0)
		{
			movementCounter.Y -= num;
			return MoveVExactCollideSolids(num, thruDashBlocks, onCollide);
		}
		return false;
	}

	public bool MoveHCollideSolidsAndBounds(Level level, float moveH, bool thruDashBlocks, Action<Vector2, Vector2, Platform> onCollide = null)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.X = 0f;
		}
		else
		{
			LiftSpeed.X = moveH / Engine.DeltaTime;
		}
		movementCounter.X += moveH;
		int num = (int)Math.Round(movementCounter.X);
		if (num != 0)
		{
			movementCounter.X -= num;
			bool flag;
			if (base.Left + (float)num < (float)level.Bounds.Left)
			{
				flag = true;
				num = level.Bounds.Left - (int)base.Left;
			}
			else if (base.Right + (float)num > (float)level.Bounds.Right)
			{
				flag = true;
				num = level.Bounds.Right - (int)base.Right;
			}
			else
			{
				flag = false;
			}
			return MoveHExactCollideSolids(num, thruDashBlocks, onCollide) || flag;
		}
		return false;
	}

	public bool MoveVCollideSolidsAndBounds(Level level, float moveV, bool thruDashBlocks, Action<Vector2, Vector2, Platform> onCollide = null, bool checkBottom = true)
	{
		if (Engine.DeltaTime == 0f)
		{
			LiftSpeed.Y = 0f;
		}
		else
		{
			LiftSpeed.Y = moveV / Engine.DeltaTime;
		}
		movementCounter.Y += moveV;
		int num = (int)Math.Round(movementCounter.Y);
		if (num != 0)
		{
			movementCounter.Y -= num;
			int num2 = level.Bounds.Bottom + 32;
			bool flag;
			if (base.Top + (float)num < (float)level.Bounds.Top)
			{
				flag = true;
				num = level.Bounds.Top - (int)base.Top;
			}
			else if (checkBottom && base.Bottom + (float)num > (float)num2)
			{
				flag = true;
				num = num2 - (int)base.Bottom;
			}
			else
			{
				flag = false;
			}
			return MoveVExactCollideSolids(num, thruDashBlocks, onCollide) || flag;
		}
		return false;
	}

	public bool MoveHExactCollideSolids(int moveH, bool thruDashBlocks, Action<Vector2, Vector2, Platform> onCollide = null)
	{
		float x = base.X;
		int num = Math.Sign(moveH);
		int num2 = 0;
		Solid solid = null;
		while (moveH != 0)
		{
			if (thruDashBlocks)
			{
				foreach (DashBlock entity in base.Scene.Tracker.GetEntities<DashBlock>())
				{
					if (CollideCheck(entity, Position + Vector2.UnitX * num))
					{
						entity.Break(base.Center, Vector2.UnitX * num);
						SceneAs<Level>().Shake(0.2f);
						Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
					}
				}
			}
			solid = CollideFirst<Solid>(Position + Vector2.UnitX * num);
			if (solid != null)
			{
				break;
			}
			num2 += num;
			moveH -= num;
			base.X += num;
		}
		base.X = x;
		MoveHExact(num2);
		if (solid != null)
		{
			onCollide?.Invoke(Vector2.UnitX * num, Vector2.UnitX * num2, solid);
		}
		return solid != null;
	}

	public bool MoveVExactCollideSolids(int moveV, bool thruDashBlocks, Action<Vector2, Vector2, Platform> onCollide = null)
	{
		float y = base.Y;
		int num = Math.Sign(moveV);
		int num2 = 0;
		Platform platform = null;
		while (moveV != 0)
		{
			if (thruDashBlocks)
			{
				foreach (DashBlock entity in base.Scene.Tracker.GetEntities<DashBlock>())
				{
					if (CollideCheck(entity, Position + Vector2.UnitY * num))
					{
						entity.Break(base.Center, Vector2.UnitY * num);
						SceneAs<Level>().Shake(0.2f);
						Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
					}
				}
			}
			platform = CollideFirst<Solid>(Position + Vector2.UnitY * num);
			if (platform != null)
			{
				break;
			}
			if (moveV > 0)
			{
				platform = CollideFirstOutside<JumpThru>(Position + Vector2.UnitY * num);
				if (platform != null)
				{
					break;
				}
			}
			num2 += num;
			moveV -= num;
			base.Y += num;
		}
		base.Y = y;
		MoveVExact(num2);
		if (platform != null)
		{
			onCollide?.Invoke(Vector2.UnitY * num, Vector2.UnitY * num2, platform);
		}
		return platform != null;
	}
}
