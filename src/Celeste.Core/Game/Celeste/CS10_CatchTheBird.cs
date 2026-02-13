using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_CatchTheBird : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

		public Level level;

		private BadelineBoost _003Cboost_003E5__2;

		private float _003Cground_003E5__3;

		private float _003Cp_003E5__4;

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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/new_content/music/lvl10/cinematic/bird_crash_second");
				_003Cboost_003E5__2 = cS10_CatchTheBird.Scene.Entities.FindFirst<BadelineBoost>();
				if (_003Cboost_003E5__2 != null)
				{
					_003Cboost_003E5__2.Active = (_003Cboost_003E5__2.Visible = (_003Cboost_003E5__2.Collidable = false));
				}
				_003C_003E2__current = cS10_CatchTheBird.flingBird.DoGrabbingRoutine(cS10_CatchTheBird.player);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.flingBird.Sprite.Play("hurt");
				cS10_CatchTheBird.flingBird.X += 8f;
				while (!cS10_CatchTheBird.player.OnGround())
				{
					cS10_CatchTheBird.player.MoveVExact(1);
				}
				while (cS10_CatchTheBird.player.CollideCheck<Solid>())
				{
					cS10_CatchTheBird.player.Y--;
				}
				Engine.TimeRate = 0.65f;
				_003Cground_003E5__3 = cS10_CatchTheBird.player.Position.Y;
				cS10_CatchTheBird.player.Dashes = 1;
				cS10_CatchTheBird.player.Sprite.Play("roll");
				cS10_CatchTheBird.player.Speed.X = 200f;
				cS10_CatchTheBird.player.DummyFriction = false;
				_003Cp_003E5__4 = 0f;
				goto IL_029b;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime;
				goto IL_029b;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0330;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0330;
			case 5:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.player.Sprite.Play("rollGetUp");
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/reconciliation";
				level.Session.Audio.Apply();
				_003C_003E2__current = Textbox.Say("CH9_CATCH_THE_BIRD", cS10_CatchTheBird.BirdLooksHurt, cS10_CatchTheBird.BirdSquakOnGround, cS10_CatchTheBird.ApproachBird, cS10_CatchTheBird.ApproachBirdAgain, cS10_CatchTheBird.BadelineAppears, cS10_CatchTheBird.WaitABeat, cS10_CatchTheBird.MadelineSits, cS10_CatchTheBird.BadelineHugs, cS10_CatchTheBird.StandUp, cS10_CatchTheBird.ShiftCameraToBird);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomBack(0.5f);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				if (cS10_CatchTheBird.badeline != null)
				{
					cS10_CatchTheBird.badeline.Vanish();
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				if (_003Cboost_003E5__2 != null)
				{
					cS10_CatchTheBird.Level.Displacement.AddBurst(_003Cboost_003E5__2.Center, 0.5f, 8f, 32f, 0.5f);
					Audio.Play("event:/new_content/char/badeline/booster_first_appear", _003Cboost_003E5__2.Center);
					_003Cboost_003E5__2.Active = (_003Cboost_003E5__2.Visible = (_003Cboost_003E5__2.Collidable = true));
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 10;
					return true;
				}
				break;
			case 10:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_029b:
				if (_003Cp_003E5__4 < 1f)
				{
					cS10_CatchTheBird.player.Speed.X = Calc.Approach(cS10_CatchTheBird.player.Speed.X, 0f, 160f * Engine.DeltaTime);
					if (cS10_CatchTheBird.player.Speed.X != 0f && cS10_CatchTheBird.Scene.OnInterval(0.1f))
					{
						Dust.BurstFG(cS10_CatchTheBird.player.Position, -(float)Math.PI / 2f, 2);
					}
					cS10_CatchTheBird.flingBird.Position.X += Engine.DeltaTime * 80f * Ease.CubeOut(1f - _003Cp_003E5__4);
					cS10_CatchTheBird.flingBird.Position.Y = _003Cground_003E5__3;
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				cS10_CatchTheBird.player.Speed.X = 0f;
				cS10_CatchTheBird.player.DummyFriction = true;
				cS10_CatchTheBird.player.DummyGravity = true;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
				IL_0330:
				if (Engine.TimeRate < 1f)
				{
					Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 4f * Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				cS10_CatchTheBird.player.ForceCameraUpdate = false;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 5;
				return true;
			}
			cS10_CatchTheBird.EndCutscene(level);
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
	private sealed class _003CBirdTwitches_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

		public string sfx;

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
		public _003CBirdTwitches_003Ed__9(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.flingBird.Sprite.Scale.Y = 1.6f;
				if (!string.IsNullOrWhiteSpace(sfx))
				{
					Audio.Play(sfx, cS10_CatchTheBird.flingBird.Position);
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS10_CatchTheBird.flingBird.Sprite.Scale.Y > 1f)
			{
				cS10_CatchTheBird.flingBird.Sprite.Scale.Y = Calc.Approach(cS10_CatchTheBird.flingBird.Sprite.Scale.Y, 1f, 2f * Engine.DeltaTime);
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
	private sealed class _003CBirdLooksHurt_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CBirdLooksHurt_003Ed__10(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.BirdTwitches("event:/new_content/game/10_farewell/bird_crashscene_twitch_1");
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.BirdTwitches("event:/new_content/game/10_farewell/bird_crashscene_twitch_2");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
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
	private sealed class _003CBirdSquakOnGround_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CBirdSquakOnGround_003Ed__11(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.BirdTwitches("event:/new_content/game/10_farewell/bird_crashscene_twitch_3");
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/game/10_farewell/bird_crashscene_recover", cS10_CatchTheBird.flingBird.Position);
				cS10_CatchTheBird.flingBird.RemoveSelf();
				cS10_CatchTheBird.Scene.Add(cS10_CatchTheBird.bird = new BirdNPC(cS10_CatchTheBird.flingBird.Position, BirdNPC.Modes.None));
				cS10_CatchTheBird.bird.Facing = Facings.Right;
				cS10_CatchTheBird.bird.Sprite.Play("recover");
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.bird.Facing = Facings.Left;
				cS10_CatchTheBird.bird.Sprite.Play("idle");
				cS10_CatchTheBird.bird.X += 3f;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.bird.Caw();
				_003C_003E1__state = 6;
				return true;
			case 6:
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
	private sealed class _003CApproachBird_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CApproachBird_003Ed__12(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.player.DummyAutoAnimate = true;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.bird.Caw();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.Add(new Coroutine(cS10_CatchTheBird.player.DummyWalkTo(cS10_CatchTheBird.player.X + 20f)));
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/general/bird_startle", cS10_CatchTheBird.bird.Position);
				_003C_003E2__current = cS10_CatchTheBird.bird.Startle("event:/new_content/game/10_farewell/bird_crashscene_relocate");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.bird.FlyTo(new Vector2(cS10_CatchTheBird.player.X + 80f, cS10_CatchTheBird.player.Y), 3f, relocateSfx: false);
				_003C_003E1__state = 5;
				return true;
			case 5:
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
	private sealed class _003CApproachBirdAgain_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CApproachBirdAgain_003Ed__13(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/game/10_farewell/bird_crashscene_leave", cS10_CatchTheBird.bird.Position);
				cS10_CatchTheBird.Add(new Coroutine(cS10_CatchTheBird.bird.FlyTo(cS10_CatchTheBird.birdWaitPosition, 2f, relocateSfx: false)));
				_003C_003E2__current = cS10_CatchTheBird.player.DummyWalkTo(cS10_CatchTheBird.player.X + 20f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.snapshot = Audio.CreateSnapshot("snapshot:/game_10_bird_wings_silenced");
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.bird.RemoveSelf();
				cS10_CatchTheBird.Scene.Add(cS10_CatchTheBird.bird = new BirdNPC(cS10_CatchTheBird.birdWaitPosition, BirdNPC.Modes.WaitForLightningOff));
				cS10_CatchTheBird.bird.Facing = Facings.Right;
				cS10_CatchTheBird.bird.FlyAwayUp = false;
				cS10_CatchTheBird.bird.WaitForLightningPostDelay = 1f;
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
	private sealed class _003CBadelineAppears_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CBadelineAppears_003Ed__14(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.player.DummyWalkToExact((int)cS10_CatchTheBird.player.X + 20, walkBackwards: false, 0.5f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.Level.Add(cS10_CatchTheBird.badeline = new BadelineDummy(cS10_CatchTheBird.player.Position + new Vector2(24f, -8f)));
				cS10_CatchTheBird.Level.Displacement.AddBurst(cS10_CatchTheBird.badeline.Center, 0.5f, 8f, 32f, 0.5f);
				Audio.Play("event:/char/badeline/maddy_split", cS10_CatchTheBird.player.Position);
				cS10_CatchTheBird.badeline.Sprite.Scale.X = -1f;
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
	private sealed class _003CWaitABeat_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CWaitABeat_003Ed__15(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.player.DummyWalkToExact((int)cS10_CatchTheBird.player.X - 4, walkBackwards: true, 0.5f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
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
	private sealed class _003CMadelineSits_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CMadelineSits_003Ed__16(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.player.DummyWalkToExact((int)cS10_CatchTheBird.player.X - 16, walkBackwards: false, 0.25f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.player.DummyAutoAnimate = false;
				cS10_CatchTheBird.player.Sprite.Play("sitDown");
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003CBadelineHugs_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CBadelineHugs_003Ed__17(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
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
				_003C_003E2__current = cS10_CatchTheBird.badeline.FloatTo(cS10_CatchTheBird.badeline.Position + new Vector2(0f, 8f), null, faceDirection: true, fadeLight: false, quickEnd: true);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.badeline.Floatness = 0f;
				cS10_CatchTheBird.badeline.AutoAnimator.Enabled = false;
				cS10_CatchTheBird.badeline.Sprite.Play("idle");
				Audio.Play("event:/char/badeline/landing", cS10_CatchTheBird.badeline.Position);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_CatchTheBird.badeline.WalkTo(cS10_CatchTheBird.player.X - 9f, 40f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/badeline/duck", cS10_CatchTheBird.badeline.Position);
				cS10_CatchTheBird.badeline.Depth = cS10_CatchTheBird.player.Depth + 5;
				cS10_CatchTheBird.badeline.Sprite.Play("hug");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 6;
				return true;
			case 6:
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
	private sealed class _003CStandUp_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CStandUp_003Ed__18(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/badeline/stand", cS10_CatchTheBird.badeline.Position);
				_003C_003E2__current = cS10_CatchTheBird.badeline.WalkTo(cS10_CatchTheBird.badeline.X - 8f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_CatchTheBird.player.DummyAutoAnimate = true;
				cS10_CatchTheBird.Level.NextColorGrade("none", 0.25f);
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003CShiftCameraToBird_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_CatchTheBird _003C_003E4__this;

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
		public _003CShiftCameraToBird_003Ed__19(int _003C_003E1__state)
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
			CS10_CatchTheBird cS10_CatchTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Audio.ReleaseSnapshot(cS10_CatchTheBird.snapshot);
				cS10_CatchTheBird.snapshot = null;
				Audio.Play("event:/new_content/char/badeline/birdcrash_scene_float", cS10_CatchTheBird.badeline.Position);
				cS10_CatchTheBird.Add(new Coroutine(cS10_CatchTheBird.badeline.FloatTo(cS10_CatchTheBird.player.Position + new Vector2(-16f, -16f), 1)));
				Level level = cS10_CatchTheBird.Scene as Level;
				cS10_CatchTheBird.player.Facing = Facings.Right;
				_003C_003E2__current = level.ZoomAcross(level.ZoomFocusPoint + new Vector2(70f, 0f), 1.5f, 1f);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4;
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

	private Player player;

	private FlingBirdIntro flingBird;

	private BadelineDummy badeline;

	private BirdNPC bird;

	private Vector2 birdWaitPosition;

	private FMOD.Studio.EventInstance snapshot;

	public CS10_CatchTheBird(Player player, FlingBirdIntro flingBird)
	{
		this.player = player;
		this.flingBird = flingBird;
		birdWaitPosition = flingBird.BirdEndPosition;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene(Level level)
	{
		Audio.SetMusic("event:/new_content/music/lvl10/cinematic/bird_crash_second");
		BadelineBoost boost = base.Scene.Entities.FindFirst<BadelineBoost>();
		if (boost != null)
		{
			bool visible = false;
			boost.Collidable = false;
			boost.Active = (boost.Visible = visible);
		}
		yield return flingBird.DoGrabbingRoutine(player);
		flingBird.Sprite.Play("hurt");
		flingBird.X += 8f;
		while (!player.OnGround())
		{
			player.MoveVExact(1);
		}
		while (player.CollideCheck<Solid>())
		{
			player.Y--;
		}
		Engine.TimeRate = 0.65f;
		float ground = player.Position.Y;
		player.Dashes = 1;
		player.Sprite.Play("roll");
		player.Speed.X = 200f;
		player.DummyFriction = false;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			player.Speed.X = Calc.Approach(player.Speed.X, 0f, 160f * Engine.DeltaTime);
			if (player.Speed.X != 0f && base.Scene.OnInterval(0.1f))
			{
				Dust.BurstFG(player.Position, -(float)Math.PI / 2f, 2);
			}
			flingBird.Position.X += Engine.DeltaTime * 80f * Ease.CubeOut(1f - p);
			flingBird.Position.Y = ground;
			yield return null;
		}
		player.Speed.X = 0f;
		player.DummyFriction = true;
		player.DummyGravity = true;
		yield return 0.25f;
		while (Engine.TimeRate < 1f)
		{
			Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 4f * Engine.DeltaTime);
			yield return null;
		}
		player.ForceCameraUpdate = false;
		yield return 0.6f;
		player.Sprite.Play("rollGetUp");
		yield return 0.8f;
		level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/reconciliation";
		level.Session.Audio.Apply();
		yield return Textbox.Say("CH9_CATCH_THE_BIRD", BirdLooksHurt, BirdSquakOnGround, ApproachBird, ApproachBirdAgain, BadelineAppears, WaitABeat, MadelineSits, BadelineHugs, StandUp, ShiftCameraToBird);
		yield return level.ZoomBack(0.5f);
		if (badeline != null)
		{
			badeline.Vanish();
		}
		yield return 0.5f;
		if (boost != null)
		{
			Level.Displacement.AddBurst(boost.Center, 0.5f, 8f, 32f, 0.5f);
			Audio.Play("event:/new_content/char/badeline/booster_first_appear", boost.Center);
			bool visible = true;
			boost.Collidable = true;
			boost.Active = (boost.Visible = visible);
			yield return 0.2f;
		}
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CBirdTwitches_003Ed__9))]
	private IEnumerator BirdTwitches(string sfx = null)
	{
		flingBird.Sprite.Scale.Y = 1.6f;
		if (!string.IsNullOrWhiteSpace(sfx))
		{
			Audio.Play(sfx, flingBird.Position);
		}
		while (flingBird.Sprite.Scale.Y > 1f)
		{
			flingBird.Sprite.Scale.Y = Calc.Approach(flingBird.Sprite.Scale.Y, 1f, 2f * Engine.DeltaTime);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CBirdLooksHurt_003Ed__10))]
	private IEnumerator BirdLooksHurt()
	{
		yield return 0.8f;
		yield return BirdTwitches("event:/new_content/game/10_farewell/bird_crashscene_twitch_1");
		yield return 0.4f;
		yield return BirdTwitches("event:/new_content/game/10_farewell/bird_crashscene_twitch_2");
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CBirdSquakOnGround_003Ed__11))]
	private IEnumerator BirdSquakOnGround()
	{
		yield return 0.6f;
		yield return BirdTwitches("event:/new_content/game/10_farewell/bird_crashscene_twitch_3");
		yield return 0.8f;
		Audio.Play("event:/new_content/game/10_farewell/bird_crashscene_recover", flingBird.Position);
		flingBird.RemoveSelf();
		base.Scene.Add(bird = new BirdNPC(flingBird.Position, BirdNPC.Modes.None));
		bird.Facing = Facings.Right;
		bird.Sprite.Play("recover");
		yield return 0.6f;
		bird.Facing = Facings.Left;
		bird.Sprite.Play("idle");
		bird.X += 3f;
		yield return 0.4f;
		yield return bird.Caw();
	}

	[IteratorStateMachine(typeof(_003CApproachBird_003Ed__12))]
	private IEnumerator ApproachBird()
	{
		player.DummyAutoAnimate = true;
		yield return 0.25f;
		yield return bird.Caw();
		Add(new Coroutine(player.DummyWalkTo(player.X + 20f)));
		yield return 0.1f;
		Audio.Play("event:/game/general/bird_startle", bird.Position);
		yield return bird.Startle("event:/new_content/game/10_farewell/bird_crashscene_relocate");
		yield return bird.FlyTo(new Vector2(player.X + 80f, player.Y), 3f, relocateSfx: false);
	}

	[IteratorStateMachine(typeof(_003CApproachBirdAgain_003Ed__13))]
	private IEnumerator ApproachBirdAgain()
	{
		Audio.Play("event:/new_content/game/10_farewell/bird_crashscene_leave", bird.Position);
		Add(new Coroutine(bird.FlyTo(birdWaitPosition, 2f, relocateSfx: false)));
		yield return player.DummyWalkTo(player.X + 20f);
		snapshot = Audio.CreateSnapshot("snapshot:/game_10_bird_wings_silenced");
		yield return 0.8f;
		bird.RemoveSelf();
		base.Scene.Add(bird = new BirdNPC(birdWaitPosition, BirdNPC.Modes.WaitForLightningOff));
		bird.Facing = Facings.Right;
		bird.FlyAwayUp = false;
		bird.WaitForLightningPostDelay = 1f;
	}

	[IteratorStateMachine(typeof(_003CBadelineAppears_003Ed__14))]
	private IEnumerator BadelineAppears()
	{
		yield return player.DummyWalkToExact((int)player.X + 20, walkBackwards: false, 0.5f);
		Level.Add(badeline = new BadelineDummy(player.Position + new Vector2(24f, -8f)));
		Level.Displacement.AddBurst(badeline.Center, 0.5f, 8f, 32f, 0.5f);
		Audio.Play("event:/char/badeline/maddy_split", player.Position);
		badeline.Sprite.Scale.X = -1f;
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CWaitABeat_003Ed__15))]
	private IEnumerator WaitABeat()
	{
		yield return player.DummyWalkToExact((int)player.X - 4, walkBackwards: true, 0.5f);
		yield return 0.8f;
	}

	[IteratorStateMachine(typeof(_003CMadelineSits_003Ed__16))]
	private IEnumerator MadelineSits()
	{
		yield return 0.5f;
		yield return player.DummyWalkToExact((int)player.X - 16, walkBackwards: false, 0.25f);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("sitDown");
		yield return 1.5f;
	}

	[IteratorStateMachine(typeof(_003CBadelineHugs_003Ed__17))]
	private IEnumerator BadelineHugs()
	{
		yield return 1f;
		yield return badeline.FloatTo(badeline.Position + new Vector2(0f, 8f), null, faceDirection: true, fadeLight: false, quickEnd: true);
		badeline.Floatness = 0f;
		badeline.AutoAnimator.Enabled = false;
		badeline.Sprite.Play("idle");
		Audio.Play("event:/char/badeline/landing", badeline.Position);
		yield return 0.5f;
		yield return badeline.WalkTo(player.X - 9f, 40f);
		badeline.Sprite.Scale.X = 1f;
		yield return 0.2f;
		Audio.Play("event:/char/badeline/duck", badeline.Position);
		badeline.Depth = player.Depth + 5;
		badeline.Sprite.Play("hug");
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CStandUp_003Ed__18))]
	private IEnumerator StandUp()
	{
		Audio.Play("event:/char/badeline/stand", badeline.Position);
		yield return badeline.WalkTo(badeline.X - 8f);
		badeline.Sprite.Scale.X = 1f;
		yield return 0.2f;
		player.DummyAutoAnimate = true;
		Level.NextColorGrade("none", 0.25f);
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CShiftCameraToBird_003Ed__19))]
	private IEnumerator ShiftCameraToBird()
	{
		Audio.ReleaseSnapshot(snapshot);
		snapshot = null;
		Audio.Play("event:/new_content/char/badeline/birdcrash_scene_float", badeline.Position);
		Add(new Coroutine(badeline.FloatTo(player.Position + new Vector2(-16f, -16f), 1)));
		Level level = base.Scene as Level;
		player.Facing = Facings.Right;
		yield return level.ZoomAcross(level.ZoomFocusPoint + new Vector2(70f, 0f), 1.5f, 1f);
		yield return 0.4;
	}

	public override void OnEnd(Level level)
	{
		Audio.ReleaseSnapshot(snapshot);
		snapshot = null;
		if (WasSkipped)
		{
			CutsceneNode cutsceneNode = CutsceneNode.Find("player_skip");
			if (cutsceneNode != null)
			{
				player.Sprite.Play("idle");
				player.Position = cutsceneNode.Position.FloorV2();
				level.Camera.Position = player.CameraTarget;
			}
			foreach (Lightning item in base.Scene.Entities.FindAll<Lightning>())
			{
				item.ToggleCheck();
			}
			base.Scene.Tracker.GetEntity<LightningRenderer>()?.ToggleEdges(immediate: true);
			level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/reconciliation";
			level.Session.Audio.Apply();
		}
		player.Speed = Vector2.Zero;
		player.DummyGravity = true;
		player.DummyFriction = true;
		player.DummyAutoAnimate = true;
		player.ForceCameraUpdate = false;
		player.StateMachine.State = 0;
		BadelineBoost badelineBoost = base.Scene.Entities.FindFirst<BadelineBoost>();
		if (badelineBoost != null)
		{
			badelineBoost.Active = (badelineBoost.Visible = (badelineBoost.Collidable = true));
		}
		if (badeline != null)
		{
			badeline.RemoveSelf();
		}
		if (flingBird != null)
		{
			if (flingBird.CrashSfxEmitter != null)
			{
				flingBird.CrashSfxEmitter.RemoveSelf();
			}
			flingBird.RemoveSelf();
		}
		if (WasSkipped)
		{
			if (bird != null)
			{
				bird.RemoveSelf();
			}
			base.Scene.Add(bird = new BirdNPC(birdWaitPosition, BirdNPC.Modes.WaitForLightningOff));
			bird.Facing = Facings.Right;
			bird.FlyAwayUp = false;
			bird.WaitForLightningPostDelay = 1f;
			level.SnapColorGrade("none");
		}
		level.ResetZoom();
	}

	public override void Removed(Scene scene)
	{
		Audio.ReleaseSnapshot(snapshot);
		snapshot = null;
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Audio.ReleaseSnapshot(snapshot);
		snapshot = null;
		base.SceneEnd(scene);
	}

	public static void HandlePostCutsceneSpawn(FlingBirdIntro flingBird, Level level)
	{
		BadelineBoost badelineBoost = level.Entities.FindFirst<BadelineBoost>();
		if (badelineBoost != null)
		{
			badelineBoost.Active = (badelineBoost.Visible = (badelineBoost.Collidable = true));
		}
		flingBird?.RemoveSelf();
		BirdNPC birdNPC;
		level.Add(birdNPC = new BirdNPC(flingBird.BirdEndPosition, BirdNPC.Modes.WaitForLightningOff));
		birdNPC.Facing = Facings.Right;
		birdNPC.FlyAwayUp = false;
	}
}
