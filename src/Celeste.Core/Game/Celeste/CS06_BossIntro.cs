using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_BossIntro : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossIntro _003C_003E4__this;

		public Level level;

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
		public _003CCutscene_003Ed__8(int _003C_003E1__state)
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
			CS06_BossIntro cS06_BossIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_BossIntro.player.StateMachine.State = 11;
				cS06_BossIntro.player.StateMachine.Locked = true;
				goto IL_0075;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0075;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00a9;
			case 3:
				_003C_003E1__state = -1;
				if (!cS06_BossIntro.player.Dead)
				{
					_003C_003E2__current = cS06_BossIntro.player.DummyWalkToExact((int)(cS06_BossIntro.playerTargetX - 8f));
					_003C_003E1__state = 4;
					return true;
				}
				goto IL_0182;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0182;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomBack(0.5f);
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					cS06_BossIntro.EndCutscene(level);
					return false;
				}
				IL_0182:
				cS06_BossIntro.player.Facing = Facings.Right;
				_003C_003E2__current = Textbox.Say("ch6_boss_start", cS06_BossIntro.BadelineFloat, cS06_BossIntro.PlayerStepForward);
				_003C_003E1__state = 5;
				return true;
				IL_0075:
				if (!cS06_BossIntro.player.Dead && !cS06_BossIntro.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00a9;
				IL_00a9:
				if (cS06_BossIntro.player.Dead)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				cS06_BossIntro.player.Facing = Facings.Right;
				cS06_BossIntro.Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2((cS06_BossIntro.player.X + cS06_BossIntro.boss.X) / 2f - 160f, level.Bounds.Bottom - 180), 1f)));
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
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
	private sealed class _003CBadelineFloat_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossIntro _003C_003E4__this;

		private float _003CfromY_003E5__2;

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
		public _003CBadelineFloat_003Ed__9(int _003C_003E1__state)
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
			CS06_BossIntro cS06_BossIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_BossIntro.Add(new Coroutine(cS06_BossIntro.Level.ZoomTo(new Vector2(170f, 110f), 2f, 1f)));
				Audio.Play("event:/char/badeline/boss_prefight_getup", cS06_BossIntro.boss.Position);
				cS06_BossIntro.boss.Sitting = false;
				cS06_BossIntro.boss.NormalSprite.Play("fallSlow");
				cS06_BossIntro.boss.NormalSprite.Scale.X = -1f;
				cS06_BossIntro.boss.Add(cS06_BossIntro.animator = new BadelineAutoAnimator());
				_003CfromY_003E5__2 = cS06_BossIntro.boss.Y;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				cS06_BossIntro.boss.Position.Y = MathHelper.Lerp(_003CfromY_003E5__2, cS06_BossIntro.bossEndPosition.Y, Ease.CubeInOut(_003Cp_003E5__3));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
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

	[CompilerGenerated]
	private sealed class _003CPlayerStepForward_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossIntro _003C_003E4__this;

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
		public _003CPlayerStepForward_003Ed__10(int _003C_003E1__state)
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
			CS06_BossIntro cS06_BossIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_BossIntro.player.DummyWalkToExact((int)cS06_BossIntro.player.X + 8);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				return false;
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

	public const string Flag = "boss_intro";

	private Player player;

	private FinalBoss boss;

	private Vector2 bossEndPosition;

	private BadelineAutoAnimator animator;

	private float playerTargetX;

	public CS06_BossIntro(float playerTargetX, Player player, FinalBoss boss)
	{
		this.player = player;
		this.boss = boss;
		this.playerTargetX = playerTargetX;
		bossEndPosition = boss.Position + new Vector2(0f, -16f);
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		while (!player.Dead && !player.OnGround())
		{
			yield return null;
		}
		while (player.Dead)
		{
			yield return null;
		}
		player.Facing = Facings.Right;
		Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2((player.X + boss.X) / 2f - 160f, level.Bounds.Bottom - 180), 1f)));
		yield return 0.5f;
		if (!player.Dead)
		{
			yield return player.DummyWalkToExact((int)(playerTargetX - 8f));
		}
		player.Facing = Facings.Right;
		yield return Textbox.Say("ch6_boss_start", BadelineFloat, PlayerStepForward);
		yield return level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CBadelineFloat_003Ed__9))]
	private IEnumerator BadelineFloat()
	{
		Add(new Coroutine(Level.ZoomTo(new Vector2(170f, 110f), 2f, 1f)));
		Audio.Play("event:/char/badeline/boss_prefight_getup", boss.Position);
		boss.Sitting = false;
		boss.NormalSprite.Play("fallSlow");
		boss.NormalSprite.Scale.X = -1f;
		boss.Add(animator = new BadelineAutoAnimator());
		float fromY = boss.Y;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			boss.Position.Y = MathHelper.Lerp(fromY, bossEndPosition.Y, Ease.CubeInOut(p));
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CPlayerStepForward_003Ed__10))]
	private IEnumerator PlayerStepForward()
	{
		yield return player.DummyWalkToExact((int)player.X + 8);
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped && player != null)
		{
			player.X = playerTargetX;
			while (!player.OnGround() && player.Y < (float)level.Bounds.Bottom)
			{
				player.Y++;
			}
		}
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		boss.Position = bossEndPosition;
		if (boss.NormalSprite != null)
		{
			boss.NormalSprite.Scale.X = -1f;
			boss.NormalSprite.Play("laugh");
		}
		boss.Sitting = false;
		if (animator != null)
		{
			boss.Remove(animator);
		}
		level.Session.SetFlag("boss_intro");
	}
}
