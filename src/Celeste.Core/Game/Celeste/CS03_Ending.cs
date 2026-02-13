using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_Ending : CutsceneEntity
{
	private class BgFlash : Entity
	{
		public float Alpha;

		public BgFlash()
		{
			base.Depth = 10100;
		}

		public override void Render()
		{
			Camera camera = (base.Scene as Level).Camera;
			Draw.Rect(camera.X - 10f, camera.Y - 10f, 340f, 200f, Color.Black * Alpha);
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Ending _003C_003E4__this;

		public Level level;

		private BgFlash _003CbgFlash_003E5__2;

		private Vector2 _003CoshiroFallSpeed_003E5__3;

		private float _003Ct_003E5__4;

		private float _003Ctarget_003E5__5;

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
		public _003CCutscene_003Ed__11(int _003C_003E1__state)
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
			CS03_Ending CS_0024_003C_003E8__locals73 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.player.StateMachine.State = 11;
				CS_0024_003C_003E8__locals73.player.StateMachine.Locked = true;
				CS_0024_003C_003E8__locals73.player.ForceCameraUpdate = false;
				CS_0024_003C_003E8__locals73.Add(new Coroutine(CS_0024_003C_003E8__locals73.player.DummyRunTo(CS_0024_003C_003E8__locals73.roof.X + CS_0024_003C_003E8__locals73.roof.Width - 32f, fastAnim: true)));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.player.DummyAutoAnimate = false;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.angryOshiro = CS_0024_003C_003E8__locals73.Scene.Entities.FindFirst<AngryOshiro>();
				CS_0024_003C_003E8__locals73.Add(new Coroutine(CS_0024_003C_003E8__locals73.MoveGhostTo(new Vector2(CS_0024_003C_003E8__locals73.roof.X + 40f, CS_0024_003C_003E8__locals73.roof.Y - 12f))));
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.player.DummyAutoAnimate = true;
				_003C_003E2__current = level.ZoomTo(new Vector2(130f, 60f), 2f, 0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.player.Facing = Facings.Left;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_CHASE_END", CS_0024_003C_003E8__locals73.GhostSmash);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = CS_0024_003C_003E8__locals73.GhostSmash(0.5f, final: true);
				_003C_003E1__state = 7;
				return true;
			case 7:
			{
				_003C_003E1__state = -1;
				Audio.SetMusic(null);
				CS_0024_003C_003E8__locals73.oshiroSprite = null;
				_003CbgFlash_003E5__2 = new BgFlash();
				_003CbgFlash_003E5__2.Alpha = 1f;
				level.Add(_003CbgFlash_003E5__2);
				Distort.GameRate = 0f;
				Sprite sprite = GFX.SpriteBank.Create("oshiro_boss_lightning");
				sprite.Position = CS_0024_003C_003E8__locals73.angryOshiro.Position + new Vector2(140f, -100f);
				sprite.Rotation = Calc.Angle(sprite.Position, CS_0024_003C_003E8__locals73.angryOshiro.Position + new Vector2(0f, 10f));
				sprite.Play("once");
				CS_0024_003C_003E8__locals73.Add(sprite);
				_003C_003E2__current = null;
				_003C_003E1__state = 8;
				return true;
			}
			case 8:
				_003C_003E1__state = -1;
				Celeste.Freeze(0.3f);
				_003C_003E2__current = null;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				CS_0024_003C_003E8__locals73.smashRumble = false;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				Distort.GameRate = 1f;
				level.Flash(Color.White);
				CS_0024_003C_003E8__locals73.player.DummyGravity = false;
				CS_0024_003C_003E8__locals73.angryOshiro.Sprite.Play("transformBack");
				CS_0024_003C_003E8__locals73.player.Sprite.Play("fall");
				CS_0024_003C_003E8__locals73.roof.BeginFalling = true;
				_003C_003E2__current = null;
				_003C_003E1__state = 11;
				return true;
			case 11:
			{
				_003C_003E1__state = -1;
				Engine.TimeRate = 0.01f;
				CS_0024_003C_003E8__locals73.player.Sprite.Play("fallFast");
				CS_0024_003C_003E8__locals73.player.DummyGravity = true;
				CS_0024_003C_003E8__locals73.player.Speed.Y = -200f;
				CS_0024_003C_003E8__locals73.player.Speed.X = 300f;
				_003CoshiroFallSpeed_003E5__3 = new Vector2(-100f, -250f);
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 1.5f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals73.angryOshiro.Sprite.Rotation = t.Eased * -100f * ((float)Math.PI / 180f);
				};
				CS_0024_003C_003E8__locals73.Add(tween);
				_003Ct_003E5__4 = 0f;
				goto IL_0598;
			}
			case 12:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 += Engine.DeltaTime;
				goto IL_0598;
			case 13:
				_003C_003E1__state = -1;
				_003CbgFlash_003E5__2 = null;
				_003CoshiroFallSpeed_003E5__3 = default(Vector2);
				while (!CS_0024_003C_003E8__locals73.player.OnGround())
				{
					CS_0024_003C_003E8__locals73.player.MoveV(1f);
				}
				CS_0024_003C_003E8__locals73.player.DummyAutoAnimate = false;
				CS_0024_003C_003E8__locals73.player.Sprite.Play("tired");
				CS_0024_003C_003E8__locals73.angryOshiro.RemoveSelf();
				CS_0024_003C_003E8__locals73.Scene.Add(CS_0024_003C_003E8__locals73.oshiro = new Entity(new Vector2(level.Bounds.Left + 110, CS_0024_003C_003E8__locals73.player.Y)));
				CS_0024_003C_003E8__locals73.oshiro.Add(CS_0024_003C_003E8__locals73.oshiroSprite = GFX.SpriteBank.Create("oshiro"));
				CS_0024_003C_003E8__locals73.oshiroSprite.Play("fall");
				CS_0024_003C_003E8__locals73.oshiroSprite.Scale.X = 1f;
				CS_0024_003C_003E8__locals73.oshiro.Collider = new Hitbox(8f, 8f, -4f, -8f);
				CS_0024_003C_003E8__locals73.oshiro.Add(new VertexLight(new Vector2(0f, -8f), Color.White, 1f, 16, 32));
				_003C_003E2__current = CutsceneEntity.CameraTo(CS_0024_003C_003E8__locals73.player.CameraTarget + new Vector2(0f, 40f), 1f, Ease.CubeOut);
				_003C_003E1__state = 14;
				return true;
			case 14:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 15;
				return true;
			case 15:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/music/lvl3/intro");
				_003C_003E2__current = 3f;
				_003C_003E1__state = 16;
				return true;
			case 16:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/oshiro/chat_get_up", CS_0024_003C_003E8__locals73.oshiro.Position);
				CS_0024_003C_003E8__locals73.oshiroSprite.Play("recover");
				_003Ctarget_003E5__5 = CS_0024_003C_003E8__locals73.oshiro.Y + 4f;
				goto IL_0851;
			case 17:
				_003C_003E1__state = -1;
				goto IL_0851;
			case 18:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH3_ENDING", CS_0024_003C_003E8__locals73.OshiroTurns);
				_003C_003E1__state = 19;
				return true;
			case 19:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.Add(new Coroutine(CutsceneEntity.CameraTo(level.Camera.Position + new Vector2(-80f, 0f), 3f)));
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 20;
				return true;
			case 20:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.oshiroSprite.Scale.X = -1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 21;
				return true;
			case 21:
				_003C_003E1__state = -1;
				_003Ct_003E5__4 = 0f;
				CS_0024_003C_003E8__locals73.oshiro.Add(new SoundSource("event:/char/oshiro/move_08_roof07_exit"));
				goto IL_0a04;
			case 22:
				_003C_003E1__state = -1;
				goto IL_0a04;
			case 23:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.player.DummyAutoAnimate = true;
				_003C_003E2__current = CS_0024_003C_003E8__locals73.player.DummyWalkTo(CS_0024_003C_003E8__locals73.player.X - 16f);
				_003C_003E1__state = 24;
				return true;
			case 24:
				_003C_003E1__state = -1;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 25;
				return true;
			case 25:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals73.player.Facing = Facings.Right;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 26;
				return true;
			case 26:
				{
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals73.player.ForceCameraUpdate = false;
					CS_0024_003C_003E8__locals73.player.Add(new Coroutine(CS_0024_003C_003E8__locals73.RunPlayerRight()));
					CS_0024_003C_003E8__locals73.EndCutscene(level);
					return false;
				}
				IL_0a04:
				if (CS_0024_003C_003E8__locals73.oshiro.X > (float)(level.Bounds.Left - 16))
				{
					CS_0024_003C_003E8__locals73.oshiro.X -= 40f * Engine.DeltaTime;
					CS_0024_003C_003E8__locals73.oshiroSprite.Y = (float)Math.Sin(_003Ct_003E5__4 += Engine.DeltaTime * 2f) * 2f;
					CS_0024_003C_003E8__locals73.oshiro.CollideFirst<Door>()?.Open(CS_0024_003C_003E8__locals73.oshiro.X);
					_003C_003E2__current = null;
					_003C_003E1__state = 22;
					return true;
				}
				CS_0024_003C_003E8__locals73.Add(new Coroutine(CutsceneEntity.CameraTo(level.Camera.Position + new Vector2(80f, 0f), 2f)));
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 23;
				return true;
				IL_0851:
				if (CS_0024_003C_003E8__locals73.oshiro.Y != _003Ctarget_003E5__5)
				{
					CS_0024_003C_003E8__locals73.oshiro.Y = Calc.Approach(CS_0024_003C_003E8__locals73.oshiro.Y, _003Ctarget_003E5__5, 6f * Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 17;
					return true;
				}
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 18;
				return true;
				IL_0598:
				if (_003Ct_003E5__4 < 2f)
				{
					_003CoshiroFallSpeed_003E5__3.X = Calc.Approach(_003CoshiroFallSpeed_003E5__3.X, 0f, Engine.DeltaTime * 400f);
					_003CoshiroFallSpeed_003E5__3.Y += Engine.DeltaTime * 800f;
					CS_0024_003C_003E8__locals73.angryOshiro.Position += _003CoshiroFallSpeed_003E5__3 * Engine.DeltaTime;
					_003CbgFlash_003E5__2.Alpha = Calc.Approach(_003CbgFlash_003E5__2.Alpha, 0f, Engine.RawDeltaTime);
					Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, Engine.RawDeltaTime * 0.6f);
					_003C_003E2__current = null;
					_003C_003E1__state = 12;
					return true;
				}
				level.DirectionalShake(new Vector2(0f, -1f), 0.5f);
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Long);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 13;
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
	private sealed class _003COshiroTurns_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Ending _003C_003E4__this;

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
		public _003COshiroTurns_003Ed__12(int _003C_003E1__state)
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
			CS03_Ending cS03_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_Ending.oshiroSprite.Scale.X = -1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
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

	[CompilerGenerated]
	private sealed class _003CMoveGhostTo_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Ending _003C_003E4__this;

		public Vector2 target;

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
		public _003CMoveGhostTo_003Ed__13(int _003C_003E1__state)
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
			CS03_Ending cS03_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (cS03_Ending.angryOshiro == null)
				{
					return false;
				}
				target.Y -= cS03_Ending.angryOshiro.Height / 2f;
				cS03_Ending.angryOshiro.EnterDummyMode();
				cS03_Ending.angryOshiro.Collidable = false;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS03_Ending.angryOshiro.Position != target)
			{
				cS03_Ending.angryOshiro.Position = Calc.Approach(cS03_Ending.angryOshiro.Position, target, 64f * Engine.DeltaTime);
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
	private sealed class _003CGhostSmash_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Ending _003C_003E4__this;

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
		public _003CGhostSmash_003Ed__14(int _003C_003E1__state)
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
			CS03_Ending cS03_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_Ending.GhostSmash(0f, final: false);
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

	[CompilerGenerated]
	private sealed class _003CGhostSmash_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Ending _003C_003E4__this;

		public bool final;

		public float topDelay;

		private float _003Cfrom_003E5__2;

		private float _003Cto_003E5__3;

		private float _003Cground_003E5__4;

		private float _003Cp_003E5__5;

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
		public _003CGhostSmash_003Ed__15(int _003C_003E1__state)
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
			CS03_Ending cS03_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (cS03_Ending.angryOshiro == null)
				{
					return false;
				}
				if (final)
				{
					cS03_Ending.smashSfx = Audio.Play("event:/char/oshiro/boss_slam_final", cS03_Ending.angryOshiro.Position);
				}
				else
				{
					cS03_Ending.smashSfx = Audio.Play("event:/char/oshiro/boss_slam_first", cS03_Ending.angryOshiro.Position);
				}
				_003Cfrom_003E5__2 = cS03_Ending.angryOshiro.Y;
				_003Cto_003E5__3 = cS03_Ending.angryOshiro.Y - 32f;
				_003Cp_003E5__5 = 0f;
				goto IL_010f;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__5 += Engine.DeltaTime * 2f;
				goto IL_010f;
			case 2:
				_003C_003E1__state = -1;
				_003Cground_003E5__4 = _003Cfrom_003E5__2 + 20f;
				_003Cp_003E5__5 = 0f;
				goto IL_01b7;
			case 3:
				_003C_003E1__state = -1;
				_003Cp_003E5__5 += Engine.DeltaTime * 8f;
				goto IL_01b7;
			case 4:
				_003C_003E1__state = -1;
				_003Cp_003E5__5 += Engine.DeltaTime * 16f;
				goto IL_026d;
			case 5:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_01b7:
				if (_003Cp_003E5__5 < 1f)
				{
					cS03_Ending.angryOshiro.Y = MathHelper.Lerp(_003Cto_003E5__3, _003Cground_003E5__4, Ease.CubeOut(_003Cp_003E5__5));
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				cS03_Ending.angryOshiro.Squish();
				cS03_Ending.Level.Shake(0.5f);
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				cS03_Ending.smashRumble = true;
				cS03_Ending.roof.StartShaking(0.5f);
				if (!final)
				{
					_003Cp_003E5__5 = 0f;
					goto IL_026d;
				}
				cS03_Ending.angryOshiro.Y = (_003Cground_003E5__4 + _003Cfrom_003E5__2) / 2f;
				goto IL_029a;
				IL_026d:
				if (_003Cp_003E5__5 < 1f)
				{
					cS03_Ending.angryOshiro.Y = MathHelper.Lerp(_003Cground_003E5__4, _003Cfrom_003E5__2, Ease.CubeOut(_003Cp_003E5__5));
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				goto IL_029a;
				IL_010f:
				if (_003Cp_003E5__5 < 1f)
				{
					cS03_Ending.angryOshiro.Y = MathHelper.Lerp(_003Cfrom_003E5__2, _003Cto_003E5__3, Ease.CubeOut(_003Cp_003E5__5));
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = topDelay;
				_003C_003E1__state = 2;
				return true;
				IL_029a:
				if (cS03_Ending.angryOshiro == null)
				{
					return false;
				}
				cS03_Ending.player.DummyAutoAnimate = false;
				cS03_Ending.player.Sprite.Play("shaking");
				cS03_Ending.roof.Wobble(cS03_Ending.angryOshiro, final);
				if (!final)
				{
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 5;
					return true;
				}
				break;
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
	private sealed class _003CRunPlayerRight_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Ending _003C_003E4__this;

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
		public _003CRunPlayerRight_003Ed__16(int _003C_003E1__state)
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
			CS03_Ending cS03_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.75f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_Ending.player.DummyRunTo(cS03_Ending.player.X + 128f);
				_003C_003E1__state = 2;
				return true;
			case 2:
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

	public const string Flag = "oshiroEnding";

	private ResortRoofEnding roof;

	private AngryOshiro angryOshiro;

	private Player player;

	private Entity oshiro;

	private Sprite oshiroSprite;

	private FMOD.Studio.EventInstance smashSfx;

	private bool smashRumble;

	public CS03_Ending(ResortRoofEnding roof, Player player)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.roof = roof;
		this.player = player;
		base.Depth = -1000000;
	}

	public override void OnBegin(Level level)
	{
		level.RegisterAreaComplete();
		Add(new Coroutine(Cutscene(level)));
	}

	public override void Update()
	{
		base.Update();
		if (smashRumble)
		{
			Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
		}
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__11))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.ForceCameraUpdate = false;
		Add(new Coroutine(player.DummyRunTo(roof.X + roof.Width - 32f, fastAnim: true)));
		yield return null;
		player.DummyAutoAnimate = false;
		yield return 0.5f;
		angryOshiro = base.Scene.Entities.FindFirst<AngryOshiro>();
		Add(new Coroutine(MoveGhostTo(new Vector2(roof.X + 40f, roof.Y - 12f))));
		yield return 1f;
		player.DummyAutoAnimate = true;
		yield return level.ZoomTo(new Vector2(130f, 60f), 2f, 0.5f);
		player.Facing = Facings.Left;
		yield return 0.5f;
		yield return Textbox.Say("CH3_OSHIRO_CHASE_END", GhostSmash);
		yield return GhostSmash(0.5f, final: true);
		Audio.SetMusic(null);
		oshiroSprite = null;
		BgFlash bgFlash = new BgFlash
		{
			Alpha = 1f
		};
		level.Add(bgFlash);
		Distort.GameRate = 0f;
		Sprite sprite = GFX.SpriteBank.Create("oshiro_boss_lightning");
		sprite.Position = angryOshiro.Position + new Vector2(140f, -100f);
		sprite.Rotation = Calc.Angle(sprite.Position, angryOshiro.Position + new Vector2(0f, 10f));
		sprite.Play("once");
		Add(sprite);
		yield return null;
		Celeste.Freeze(0.3f);
		yield return null;
		level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		smashRumble = false;
		yield return 0.2f;
		Distort.GameRate = 1f;
		level.Flash(Color.White);
		player.DummyGravity = false;
		angryOshiro.Sprite.Play("transformBack");
		player.Sprite.Play("fall");
		roof.BeginFalling = true;
		yield return null;
		Engine.TimeRate = 0.01f;
		player.Sprite.Play("fallFast");
		player.DummyGravity = true;
		player.Speed.Y = -200f;
		player.Speed.X = 300f;
		Vector2 oshiroFallSpeed = new Vector2(-100f, -250f);
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 1.5f, start: true);
		tween.OnUpdate = delegate(Tween tween2)
		{
			angryOshiro.Sprite.Rotation = tween2.Eased * -100f * ((float)Math.PI / 180f);
		};
		Add(tween);
		float t;
		for (t = 0f; t < 2f; t += Engine.DeltaTime)
		{
			oshiroFallSpeed.X = Calc.Approach(oshiroFallSpeed.X, 0f, Engine.DeltaTime * 400f);
			oshiroFallSpeed.Y += Engine.DeltaTime * 800f;
			angryOshiro.Position += oshiroFallSpeed * Engine.DeltaTime;
			bgFlash.Alpha = Calc.Approach(bgFlash.Alpha, 0f, Engine.RawDeltaTime);
			Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, Engine.RawDeltaTime * 0.6f);
			yield return null;
		}
		level.DirectionalShake(new Vector2(0f, -1f), 0.5f);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Long);
		yield return 1f;
		while (!player.OnGround())
		{
			player.MoveV(1f);
		}
		player.DummyAutoAnimate = false;
		player.Sprite.Play("tired");
		angryOshiro.RemoveSelf();
		base.Scene.Add(oshiro = new Entity(new Vector2(level.Bounds.Left + 110, player.Y)));
		oshiro.Add(oshiroSprite = GFX.SpriteBank.Create("oshiro"));
		oshiroSprite.Play("fall");
		oshiroSprite.Scale.X = 1f;
		oshiro.Collider = new Hitbox(8f, 8f, -4f, -8f);
		oshiro.Add(new VertexLight(new Vector2(0f, -8f), Color.White, 1f, 16, 32));
		yield return CutsceneEntity.CameraTo(player.CameraTarget + new Vector2(0f, 40f), 1f, Ease.CubeOut);
		yield return 1.5f;
		Audio.SetMusic("event:/music/lvl3/intro");
		yield return 3f;
		Audio.Play("event:/char/oshiro/chat_get_up", oshiro.Position);
		oshiroSprite.Play("recover");
		float target = oshiro.Y + 4f;
		while (oshiro.Y != target)
		{
			oshiro.Y = Calc.Approach(oshiro.Y, target, 6f * Engine.DeltaTime);
			yield return null;
		}
		yield return 0.6f;
		yield return Textbox.Say("CH3_ENDING", OshiroTurns);
		Add(new Coroutine(CutsceneEntity.CameraTo(level.Camera.Position + new Vector2(-80f, 0f), 3f)));
		yield return 0.5f;
		oshiroSprite.Scale.X = -1f;
		yield return 0.2f;
		t = 0f;
		oshiro.Add(new SoundSource("event:/char/oshiro/move_08_roof07_exit"));
		while (oshiro.X > (float)(level.Bounds.Left - 16))
		{
			oshiro.X -= 40f * Engine.DeltaTime;
			Sprite sprite2 = oshiroSprite;
			float num;
			t = (num = t + Engine.DeltaTime * 2f);
			sprite2.Y = (float)Math.Sin(num) * 2f;
			oshiro.CollideFirst<Door>()?.Open(oshiro.X);
			yield return null;
		}
		Add(new Coroutine(CutsceneEntity.CameraTo(level.Camera.Position + new Vector2(80f, 0f), 2f)));
		yield return 1.2f;
		player.DummyAutoAnimate = true;
		yield return player.DummyWalkTo(player.X - 16f);
		yield return 2f;
		player.Facing = Facings.Right;
		yield return 1f;
		player.ForceCameraUpdate = false;
		player.Add(new Coroutine(RunPlayerRight()));
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003COshiroTurns_003Ed__12))]
	private IEnumerator OshiroTurns()
	{
		yield return 1f;
		oshiroSprite.Scale.X = -1f;
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CMoveGhostTo_003Ed__13))]
	private IEnumerator MoveGhostTo(Vector2 target)
	{
		if (angryOshiro != null)
		{
			target.Y -= angryOshiro.Height / 2f;
			angryOshiro.EnterDummyMode();
			angryOshiro.Collidable = false;
			while (angryOshiro.Position != target)
			{
				angryOshiro.Position = Calc.Approach(angryOshiro.Position, target, 64f * Engine.DeltaTime);
				yield return null;
			}
		}
	}

	[IteratorStateMachine(typeof(_003CGhostSmash_003Ed__14))]
	private IEnumerator GhostSmash()
	{
		yield return GhostSmash(0f, final: false);
	}

	[IteratorStateMachine(typeof(_003CGhostSmash_003Ed__15))]
	private IEnumerator GhostSmash(float topDelay, bool final)
	{
		if (angryOshiro == null)
		{
			yield break;
		}
		if (final)
		{
			smashSfx = Audio.Play("event:/char/oshiro/boss_slam_final", angryOshiro.Position);
		}
		else
		{
			smashSfx = Audio.Play("event:/char/oshiro/boss_slam_first", angryOshiro.Position);
		}
		float from = angryOshiro.Y;
		float to = angryOshiro.Y - 32f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 2f)
		{
			angryOshiro.Y = MathHelper.Lerp(from, to, Ease.CubeOut(p));
			yield return null;
		}
		yield return topDelay;
		float ground = from + 20f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 8f)
		{
			angryOshiro.Y = MathHelper.Lerp(to, ground, Ease.CubeOut(p));
			yield return null;
		}
		angryOshiro.Squish();
		Level.Shake(0.5f);
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		smashRumble = true;
		roof.StartShaking(0.5f);
		if (!final)
		{
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 16f)
			{
				angryOshiro.Y = MathHelper.Lerp(ground, from, Ease.CubeOut(p));
				yield return null;
			}
		}
		else
		{
			angryOshiro.Y = (ground + from) / 2f;
		}
		if (angryOshiro != null)
		{
			player.DummyAutoAnimate = false;
			player.Sprite.Play("shaking");
			roof.Wobble(angryOshiro, final);
			if (!final)
			{
				yield return 0.5f;
			}
		}
	}

	[IteratorStateMachine(typeof(_003CRunPlayerRight_003Ed__16))]
	private IEnumerator RunPlayerRight()
	{
		yield return 0.75f;
		yield return player.DummyRunTo(player.X + 128f);
	}

	public override void OnEnd(Level level)
	{
		Audio.SetMusic("event:/music/lvl3/intro");
		Audio.Stop(smashSfx);
		level.CompleteArea();
		SpotlightWipe.FocusPoint = new Vector2(192f, 120f);
	}
}
