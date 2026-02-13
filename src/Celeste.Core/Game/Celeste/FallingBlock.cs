using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class FallingBlock : Solid
{
	[CompilerGenerated]
	private sealed class _003CSequence_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FallingBlock _003C_003E4__this;

		private float _003Cspeed_003E5__2;

		private float _003CmaxSpeed_003E5__3;

		private float _003Ctimer_003E5__4;

		private Level _003Clevel_003E5__5;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CSequence_003Ed__21(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			FallingBlock fallingBlock = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0045;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0045;
			case 2:
				_003C_003E1__state = -1;
				goto IL_009f;
			case 3:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__4 = 0.4f;
				if (fallingBlock.finalBoss)
				{
					_003Ctimer_003E5__4 = 0.2f;
				}
				goto IL_0153;
			case 4:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__4 -= Engine.DeltaTime;
				goto IL_0153;
			case 5:
				_003C_003E1__state = -1;
				fallingBlock.StopShaking();
				if (fallingBlock.CollideCheck<SolidTiles>(fallingBlock.Position + new Vector2(0f, 1f)))
				{
					fallingBlock.Safe = true;
					return false;
				}
				goto IL_049d;
			case 6:
				_003C_003E1__state = -1;
				if (_003Clevel_003E5__5.Session.MapData.CanTransitionTo(_003Clevel_003E5__5, new Vector2(fallingBlock.Center.X, fallingBlock.Bottom + 12f)))
				{
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 7;
					return true;
				}
				goto IL_0420;
			case 7:
				_003C_003E1__state = -1;
				fallingBlock.SceneAs<Level>().Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				goto IL_0420;
			case 8:
				_003C_003E1__state = -1;
				_003Clevel_003E5__5 = null;
				goto IL_0246;
			case 9:
				{
					_003C_003E1__state = -1;
					goto IL_049d;
				}
				IL_00b3:
				fallingBlock.ShakeSfx();
				fallingBlock.StartShaking();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				if (fallingBlock.finalBoss)
				{
					fallingBlock.Add(new Coroutine(fallingBlock.HighlightFade(1f)));
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
				IL_0420:
				fallingBlock.RemoveSelf();
				fallingBlock.DestroyStaticMovers();
				return false;
				IL_049d:
				if (fallingBlock.CollideCheck<Platform>(fallingBlock.Position + new Vector2(0f, 1f)))
				{
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 9;
					return true;
				}
				goto IL_00b3;
				IL_0153:
				if (_003Ctimer_003E5__4 > 0f && fallingBlock.PlayerWaitCheck())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				fallingBlock.StopShaking();
				for (int i = 2; (float)i < fallingBlock.Width; i += 4)
				{
					if (fallingBlock.Scene.CollideCheck<Solid>(fallingBlock.TopLeft + new Vector2(i, -2f)))
					{
						fallingBlock.SceneAs<Level>().Particles.Emit(P_FallDustA, 2, new Vector2(fallingBlock.X + (float)i, fallingBlock.Y), Vector2.One * 4f, (float)Math.PI / 2f);
					}
					fallingBlock.SceneAs<Level>().Particles.Emit(P_FallDustB, 2, new Vector2(fallingBlock.X + (float)i, fallingBlock.Y), Vector2.One * 4f);
				}
				_003Cspeed_003E5__2 = 0f;
				_003CmaxSpeed_003E5__3 = (fallingBlock.finalBoss ? 130f : 160f);
				goto IL_0246;
				IL_009f:
				if (fallingBlock.FallDelay > 0f)
				{
					fallingBlock.FallDelay -= Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				fallingBlock.HasStartedFalling = true;
				goto IL_00b3;
				IL_0045:
				if (!fallingBlock.Triggered && (fallingBlock.finalBoss || !fallingBlock.PlayerFallCheck()))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_009f;
				IL_0246:
				_003Clevel_003E5__5 = fallingBlock.SceneAs<Level>();
				_003Cspeed_003E5__2 = Calc.Approach(_003Cspeed_003E5__2, _003CmaxSpeed_003E5__3, 500f * Engine.DeltaTime);
				if (fallingBlock.MoveVCollideSolids(_003Cspeed_003E5__2 * Engine.DeltaTime, thruDashBlocks: true))
				{
					fallingBlock.ImpactSfx();
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					fallingBlock.SceneAs<Level>().DirectionalShake(Vector2.UnitY, fallingBlock.finalBoss ? 0.2f : 0.3f);
					if (fallingBlock.finalBoss)
					{
						fallingBlock.Add(new Coroutine(fallingBlock.HighlightFade(0f)));
					}
					fallingBlock.StartShaking();
					fallingBlock.LandParticles();
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 5;
					return true;
				}
				if (fallingBlock.Top > (float)(_003Clevel_003E5__5.Bounds.Bottom + 16) || (fallingBlock.Top > (float)(_003Clevel_003E5__5.Bounds.Bottom - 1) && fallingBlock.CollideCheck<Solid>(fallingBlock.Position + new Vector2(0f, 1f))))
				{
					fallingBlock.Collidable = (fallingBlock.Visible = false);
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 6;
					return true;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 8;
				return true;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CHighlightFade_003Ed__22 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FallingBlock _003C_003E4__this;

		public float to;

		private float _003Cfrom_003E5__2;

		private float _003Cp_003E5__3;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CHighlightFade_003Ed__22(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			FallingBlock fallingBlock = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = fallingBlock.highlight.Alpha;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / 0.5f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				fallingBlock.highlight.Alpha = MathHelper.Lerp(_003Cfrom_003E5__2, to, Ease.CubeInOut(_003Cp_003E5__3));
				fallingBlock.tiles.Alpha = 1f - fallingBlock.highlight.Alpha;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			fallingBlock.highlight.Alpha = to;
			fallingBlock.tiles.Alpha = 1f - to;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static ParticleType P_FallDustA;

	public static ParticleType P_FallDustB;

	public static ParticleType P_LandDust;

	public bool Triggered;

	public float FallDelay;

	private char TileType;

	private TileGrid tiles;

	private TileGrid highlight;

	private bool finalBoss;

	private bool climbFall;

	public bool HasStartedFalling { get; private set; }

	public FallingBlock(Vector2 position, char tile, int width, int height, bool finalBoss, bool behind, bool climbFall)
		: base(position, width, height, safe: false)
	{
		this.finalBoss = finalBoss;
		this.climbFall = climbFall;
		int newSeed = Calc.Random.Next();
		Calc.PushRandom(newSeed);
		Add(tiles = GFX.FGAutotiler.GenerateBox(tile, width / 8, height / 8).TileGrid);
		Calc.PopRandom();
		if (finalBoss)
		{
			Calc.PushRandom(newSeed);
			Add(highlight = GFX.FGAutotiler.GenerateBox('G', width / 8, height / 8).TileGrid);
			Calc.PopRandom();
			highlight.Alpha = 0f;
		}
		Add(new Coroutine(Sequence()));
		Add(new LightOcclude());
		Add(new TileInterceptor(tiles, highPriority: false));
		TileType = tile;
		SurfaceSoundIndex = SurfaceIndex.TileToIndex[tile];
		if (behind)
		{
			base.Depth = 5000;
		}
	}

	public FallingBlock(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Char("tiletype", '3'), data.Width, data.Height, finalBoss: false, data.Bool("behind"), data.Bool("climbFall", defaultValue: true))
	{
	}

	public static FallingBlock CreateFinalBossBlock(EntityData data, Vector2 offset)
	{
		return new FallingBlock(data.Position + offset, 'g', data.Width, data.Height, finalBoss: true, behind: false, climbFall: false);
	}

	public override void OnShake(Vector2 amount)
	{
		base.OnShake(amount);
		tiles.Position += amount;
		if (highlight != null)
		{
			highlight.Position += amount;
		}
	}

	public override void OnStaticMoverTrigger(StaticMover sm)
	{
		if (!finalBoss)
		{
			Triggered = true;
		}
	}

	private bool PlayerFallCheck()
	{
		if (climbFall)
		{
			return HasPlayerRider();
		}
		return HasPlayerOnTop();
	}

	private bool PlayerWaitCheck()
	{
		if (Triggered)
		{
			return true;
		}
		if (PlayerFallCheck())
		{
			return true;
		}
		if (climbFall)
		{
			if (!CollideCheck<Player>(Position - Vector2.UnitX))
			{
				return CollideCheck<Player>(Position + Vector2.UnitX);
			}
			return true;
		}
		return false;
	}

	[IteratorStateMachine(typeof(_003CSequence_003Ed__21))]
	private IEnumerator Sequence()
	{
		while (!Triggered && (finalBoss || !PlayerFallCheck()))
		{
			yield return null;
		}
		while (FallDelay > 0f)
		{
			FallDelay -= Engine.DeltaTime;
			yield return null;
		}
		HasStartedFalling = true;
		while (true)
		{
			ShakeSfx();
			StartShaking();
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
			if (finalBoss)
			{
				Add(new Coroutine(HighlightFade(1f)));
			}
			yield return 0.2f;
			float timer = 0.4f;
			if (finalBoss)
			{
				timer = 0.2f;
			}
			while (timer > 0f && PlayerWaitCheck())
			{
				yield return null;
				timer -= Engine.DeltaTime;
			}
			StopShaking();
			for (int i = 2; (float)i < base.Width; i += 4)
			{
				if (base.Scene.CollideCheck<Solid>(base.TopLeft + new Vector2(i, -2f)))
				{
					SceneAs<Level>().Particles.Emit(P_FallDustA, 2, new Vector2(base.X + (float)i, base.Y), Vector2.One * 4f, (float)Math.PI / 2f);
				}
				SceneAs<Level>().Particles.Emit(P_FallDustB, 2, new Vector2(base.X + (float)i, base.Y), Vector2.One * 4f);
			}
			float speed = 0f;
			float maxSpeed = (finalBoss ? 130f : 160f);
			while (true)
			{
				Level level = SceneAs<Level>();
				speed = Calc.Approach(speed, maxSpeed, 500f * Engine.DeltaTime);
				if (MoveVCollideSolids(speed * Engine.DeltaTime, thruDashBlocks: true))
				{
					break;
				}
				if (base.Top > (float)(level.Bounds.Bottom + 16) || (base.Top > (float)(level.Bounds.Bottom - 1) && CollideCheck<Solid>(Position + new Vector2(0f, 1f))))
				{
					FallingBlock fallingBlock = this;
					FallingBlock fallingBlock2 = this;
					bool collidable = false;
					fallingBlock2.Visible = false;
					fallingBlock.Collidable = collidable;
					yield return 0.2f;
					if (level.Session.MapData.CanTransitionTo(level, new Vector2(base.Center.X, base.Bottom + 12f)))
					{
						yield return 0.2f;
						SceneAs<Level>().Shake();
						Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					}
					RemoveSelf();
					DestroyStaticMovers();
					yield break;
				}
				yield return null;
			}
			ImpactSfx();
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			SceneAs<Level>().DirectionalShake(Vector2.UnitY, finalBoss ? 0.2f : 0.3f);
			if (finalBoss)
			{
				Add(new Coroutine(HighlightFade(0f)));
			}
			StartShaking();
			LandParticles();
			yield return 0.2f;
			StopShaking();
			if (CollideCheck<SolidTiles>(Position + new Vector2(0f, 1f)))
			{
				break;
			}
			while (CollideCheck<Platform>(Position + new Vector2(0f, 1f)))
			{
				yield return 0.1f;
			}
		}
		Safe = true;
	}

	[IteratorStateMachine(typeof(_003CHighlightFade_003Ed__22))]
	private IEnumerator HighlightFade(float to)
	{
		float from = highlight.Alpha;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.5f)
		{
			highlight.Alpha = MathHelper.Lerp(from, to, Ease.CubeInOut(p));
			tiles.Alpha = 1f - highlight.Alpha;
			yield return null;
		}
		highlight.Alpha = to;
		tiles.Alpha = 1f - to;
	}

	private void LandParticles()
	{
		for (int i = 2; (float)i <= base.Width; i += 4)
		{
			if (base.Scene.CollideCheck<Solid>(base.BottomLeft + new Vector2(i, 3f)))
			{
				SceneAs<Level>().ParticlesFG.Emit(P_FallDustA, 1, new Vector2(base.X + (float)i, base.Bottom), Vector2.One * 4f, -(float)Math.PI / 2f);
				float direction = ((!((float)i < base.Width / 2f)) ? 0f : ((float)Math.PI));
				SceneAs<Level>().ParticlesFG.Emit(P_LandDust, 1, new Vector2(base.X + (float)i, base.Bottom), Vector2.One * 4f, direction);
			}
		}
	}

	private void ShakeSfx()
	{
		if (TileType == '3')
		{
			Audio.Play("event:/game/01_forsaken_city/fallblock_ice_shake", base.Center);
		}
		else if (TileType == '9')
		{
			Audio.Play("event:/game/03_resort/fallblock_wood_shake", base.Center);
		}
		else if (TileType == 'g')
		{
			Audio.Play("event:/game/06_reflection/fallblock_boss_shake", base.Center);
		}
		else
		{
			Audio.Play("event:/game/general/fallblock_shake", base.Center);
		}
	}

	private void ImpactSfx()
	{
		if (TileType == '3')
		{
			Audio.Play("event:/game/01_forsaken_city/fallblock_ice_impact", base.BottomCenter);
		}
		else if (TileType == '9')
		{
			Audio.Play("event:/game/03_resort/fallblock_wood_impact", base.BottomCenter);
		}
		else if (TileType == 'g')
		{
			Audio.Play("event:/game/06_reflection/fallblock_boss_impact", base.BottomCenter);
		}
		else
		{
			Audio.Play("event:/game/general/fallblock_impact", base.BottomCenter);
		}
	}
}
