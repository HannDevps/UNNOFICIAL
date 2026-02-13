using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_StarJumpEnd : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass27_0
	{
		public NorthernLights bg;

		public CS06_StarJumpEnd _003C_003E4__this;

		public Level level;

		internal void _003CCutscene_003Eb__0(Tween t)
		{
			bg.OffsetY = t.Eased * 32f;
		}

		internal void _003CCutscene_003Eb__1()
		{
			_003C_003E4__this.EndCutscene(level);
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__27 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

		public Level level;

		private _003C_003Ec__DisplayClass27_0 _003C_003E8__1;

		private int _003Ccenter_003E5__2;

		private Vector2 _003CcutsceneCenter_003E5__3;

		private Vector2 _003Cstart_003E5__4;

		private Vector2 _003Ctarget_003E5__5;

		private float _003Cp_003E5__6;

		private FadeWipe _003Cwipe_003E5__7;

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
		public _003CCutscene_003Ed__27(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass27_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.level = level;
				_003C_003E8__1.level.Entities.FindFirst<StarJumpController>()?.RemoveSelf();
				foreach (StarJumpBlock item in _003C_003E8__1.level.Entities.FindAll<StarJumpBlock>())
				{
					item.Collidable = false;
				}
				_003Ccenter_003E5__2 = _003C_003E8__1.level.Bounds.X + 160;
				_003CcutsceneCenter_003E5__3 = new Vector2(_003Ccenter_003E5__2, _003C_003E8__1.level.Bounds.Top + 150);
				_003C_003E8__1.bg = _003C_003E8__1.level.Background.Get<NorthernLights>();
				_003C_003E8__1.level.CameraOffset.Y = -30f;
				cS06_StarJumpEnd.Add(new Coroutine(CutsceneEntity.CameraTo(_003CcutsceneCenter_003E5__3 + new Vector2(-160f, -70f), 1.5f, Ease.CubeOut)));
				cS06_StarJumpEnd.Add(new Coroutine(CutsceneEntity.CameraTo(_003CcutsceneCenter_003E5__3 + new Vector2(-160f, -120f), 2f, Ease.CubeInOut, 1.5f)));
				Tween.Set(cS06_StarJumpEnd, Tween.TweenMode.Oneshot, 3f, Ease.CubeInOut, delegate(Tween t)
				{
					_003C_003E8__1.bg.OffsetY = t.Eased * 32f;
				});
				if (cS06_StarJumpEnd.player.StateMachine.State == 19)
				{
					Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				}
				cS06_StarJumpEnd.player.Dashes = 0;
				cS06_StarJumpEnd.player.StateMachine.State = 11;
				cS06_StarJumpEnd.player.DummyGravity = false;
				cS06_StarJumpEnd.player.DummyAutoAnimate = false;
				cS06_StarJumpEnd.player.Sprite.Play("fallSlow");
				cS06_StarJumpEnd.player.Dashes = 1;
				cS06_StarJumpEnd.player.Speed = new Vector2(0f, -80f);
				cS06_StarJumpEnd.player.Facing = Facings.Right;
				cS06_StarJumpEnd.player.ForceCameraUpdate = false;
				goto IL_030f;
			case 1:
				_003C_003E1__state = -1;
				goto IL_030f;
			case 2:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.player.Facing = Facings.Right;
				_003C_003E8__1.level.Add(cS06_StarJumpEnd.badeline = new BadelineDummy(cS06_StarJumpEnd.player.Position));
				_003C_003E8__1.level.Displacement.AddBurst(cS06_StarJumpEnd.player.Position, 0.5f, 8f, 48f, 0.5f);
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				cS06_StarJumpEnd.player.CreateSplitParticles();
				Audio.Play("event:/char/badeline/maddy_split");
				cS06_StarJumpEnd.badeline.Sprite.Scale.X = -1f;
				_003Cstart_003E5__4 = cS06_StarJumpEnd.player.Position;
				_003Ctarget_003E5__5 = _003CcutsceneCenter_003E5__3 + new Vector2(-30f, 0f);
				cS06_StarJumpEnd.maddySineAnchorY = _003CcutsceneCenter_003E5__3.Y;
				_003Cp_003E5__6 = 0f;
				goto IL_051b;
			case 3:
				_003C_003E1__state = -1;
				if (_003Cp_003E5__6 > 1f)
				{
					_003Cp_003E5__6 = 1f;
				}
				cS06_StarJumpEnd.player.Position = Vector2.Lerp(_003Cstart_003E5__4, _003Ctarget_003E5__5, Ease.CubeOut(_003Cp_003E5__6));
				cS06_StarJumpEnd.badeline.Position = new Vector2((float)_003Ccenter_003E5__2 + ((float)_003Ccenter_003E5__2 - cS06_StarJumpEnd.player.X), cS06_StarJumpEnd.player.Y);
				_003Cp_003E5__6 += 2f * Engine.DeltaTime;
				goto IL_051b;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch6_dreaming", cS06_StarJumpEnd.TentaclesAppear, cS06_StarJumpEnd.TentaclesGrab, cS06_StarJumpEnd.FeatherMinigame, cS06_StarJumpEnd.EndFeatherMinigame, cS06_StarJumpEnd.StartCirclingPlayer);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/06_reflection/badeline_pull_whooshdown");
				cS06_StarJumpEnd.Add(new Coroutine(cS06_StarJumpEnd.BadelineFlyDown()));
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				foreach (FlyFeather item2 in _003C_003E8__1.level.Entities.FindAll<FlyFeather>())
				{
					item2.RemoveSelf();
				}
				foreach (StarJumpBlock item3 in _003C_003E8__1.level.Entities.FindAll<StarJumpBlock>())
				{
					item3.RemoveSelf();
				}
				foreach (JumpThru item4 in _003C_003E8__1.level.Entities.FindAll<JumpThru>())
				{
					item4.RemoveSelf();
				}
				_003C_003E8__1.level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
				_003C_003E8__1.level.CameraOffset.Y = 0f;
				cS06_StarJumpEnd.player.Sprite.Play("tentacle_pull");
				cS06_StarJumpEnd.player.Speed.Y = 160f;
				FallEffects.Show(visible: true);
				_003Cp_003E5__6 = 0f;
				goto IL_095f;
			case 7:
				_003C_003E1__state = -1;
				_003Cp_003E5__6 += Engine.DeltaTime / 3f;
				goto IL_095f;
			case 8:
			{
				_003C_003E1__state = -1;
				Audio.Play("event:/game/06_reflection/badeline_pull_cliffbreak");
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Long);
				cS06_StarJumpEnd.shakingLoopSfx.Stop();
				cS06_StarJumpEnd.shaking = false;
				for (int i = 0; (float)i < cS06_StarJumpEnd.plateau.Width; i += 8)
				{
					_003C_003E8__1.level.Add(Engine.Pooler.Create<Debris>().Init(cS06_StarJumpEnd.plateau.Position + new Vector2((float)i + Calc.Random.NextFloat(8f), Calc.Random.NextFloat(8f)), '3').BlastFrom(cS06_StarJumpEnd.plateau.Center + new Vector2(0f, 8f)));
					_003C_003E8__1.level.Add(Engine.Pooler.Create<Debris>().Init(cS06_StarJumpEnd.plateau.Position + new Vector2((float)i + Calc.Random.NextFloat(8f), Calc.Random.NextFloat(8f)), '3').BlastFrom(cS06_StarJumpEnd.plateau.Center + new Vector2(0f, 8f)));
				}
				cS06_StarJumpEnd.plateau.RemoveSelf();
				cS06_StarJumpEnd.bonfire.RemoveSelf();
				_003C_003E8__1.level.Shake();
				cS06_StarJumpEnd.player.Speed.Y = 160f;
				cS06_StarJumpEnd.player.Sprite.Play("tentacle_pull");
				cS06_StarJumpEnd.player.ForceCameraUpdate = false;
				_003Cwipe_003E5__7 = new FadeWipe(_003C_003E8__1.level, wipeIn: false, delegate
				{
					_003C_003E8__1._003C_003E4__this.EndCutscene(_003C_003E8__1.level);
				});
				_003Cwipe_003E5__7.Duration = 3f;
				_003Ctarget_003E5__5 = _003C_003E8__1.level.Camera.Position;
				_003Cstart_003E5__4 = _003C_003E8__1.level.Camera.Position + new Vector2(0f, 400f);
				break;
			}
			case 9:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_051b:
				if (_003Cp_003E5__6 <= 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003Cstart_003E5__4 = default(Vector2);
				_003Ctarget_003E5__5 = default(Vector2);
				cS06_StarJumpEnd.charactersSpinning = true;
				cS06_StarJumpEnd.Add(new Coroutine(cS06_StarJumpEnd.SpinCharacters()));
				cS06_StarJumpEnd.SetMusicLayer(2);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
				return true;
				IL_095f:
				if (_003Cp_003E5__6 < 1f)
				{
					cS06_StarJumpEnd.player.Speed.Y += Engine.DeltaTime * 100f;
					if (cS06_StarJumpEnd.player.X < (float)(_003C_003E8__1.level.Bounds.X + 32))
					{
						cS06_StarJumpEnd.player.X = _003C_003E8__1.level.Bounds.X + 32;
					}
					if (cS06_StarJumpEnd.player.X > (float)(_003C_003E8__1.level.Bounds.Right - 32))
					{
						cS06_StarJumpEnd.player.X = _003C_003E8__1.level.Bounds.Right - 32;
					}
					if (_003Cp_003E5__6 > 0.7f)
					{
						_003C_003E8__1.level.CameraOffset.Y -= 100f * Engine.DeltaTime;
					}
					foreach (ReflectionTentacles tentacle in cS06_StarJumpEnd.tentacles)
					{
						tentacle.Nodes[0] = new Vector2(_003C_003E8__1.level.Bounds.Center.X, cS06_StarJumpEnd.player.Y + 300f);
						tentacle.Nodes[1] = new Vector2(_003C_003E8__1.level.Bounds.Center.X, cS06_StarJumpEnd.player.Y + 600f);
					}
					FallEffects.SpeedMultiplier += Engine.DeltaTime * 0.75f;
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				Audio.Play("event:/game/06_reflection/badeline_pull_impact");
				FallEffects.Show(visible: false);
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				_003C_003E8__1.level.Flash(Color.White);
				_003C_003E8__1.level.Session.Dreaming = false;
				_003C_003E8__1.level.CameraOffset.Y = 0f;
				_003C_003E8__1.level.Camera.Position = cS06_StarJumpEnd.cameraStart;
				cS06_StarJumpEnd.SetBloom(0f);
				cS06_StarJumpEnd.bonfire.SetMode(Bonfire.Mode.Smoking);
				cS06_StarJumpEnd.plateau.Depth = cS06_StarJumpEnd.player.Depth + 10;
				cS06_StarJumpEnd.plateau.Remove(cS06_StarJumpEnd.plateau.Occluder);
				cS06_StarJumpEnd.player.Position = cS06_StarJumpEnd.playerStart + new Vector2(0f, 8f);
				cS06_StarJumpEnd.player.Speed = Vector2.Zero;
				cS06_StarJumpEnd.player.Sprite.Play("tentacle_dangling");
				cS06_StarJumpEnd.player.Facing = Facings.Left;
				cS06_StarJumpEnd.theo.Position.X -= 24f;
				cS06_StarJumpEnd.theo.Sprite.Play("alert");
				foreach (ReflectionTentacles tentacle2 in cS06_StarJumpEnd.tentacles)
				{
					tentacle2.Index = 0;
					tentacle2.Nodes[0] = new Vector2(_003C_003E8__1.level.Bounds.Center.X, cS06_StarJumpEnd.player.Y + 32f);
					tentacle2.Nodes[1] = new Vector2(_003C_003E8__1.level.Bounds.Center.X, cS06_StarJumpEnd.player.Y + 400f);
					tentacle2.SnapTentacles();
				}
				cS06_StarJumpEnd.shaking = true;
				cS06_StarJumpEnd.Add(cS06_StarJumpEnd.shakingLoopSfx = new SoundSource());
				cS06_StarJumpEnd.shakingLoopSfx.Play("event:/game/06_reflection/badeline_pull_rumble_loop");
				_003C_003E2__current = Textbox.Say("ch6_theo_watchout");
				_003C_003E1__state = 8;
				return true;
				IL_030f:
				if (cS06_StarJumpEnd.player.Speed.Length() > 0f || cS06_StarJumpEnd.player.Position != _003CcutsceneCenter_003E5__3)
				{
					cS06_StarJumpEnd.player.Speed = Calc.Approach(cS06_StarJumpEnd.player.Speed, Vector2.Zero, 200f * Engine.DeltaTime);
					cS06_StarJumpEnd.player.Position = Calc.Approach(cS06_StarJumpEnd.player.Position, _003CcutsceneCenter_003E5__3, 64f * Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS06_StarJumpEnd.player.Sprite.Play("spin");
				_003C_003E2__current = 3.5f;
				_003C_003E1__state = 2;
				return true;
			}
			if (_003Cwipe_003E5__7.Percent < 1f)
			{
				_003C_003E8__1.level.Camera.Position = Vector2.Lerp(_003Ctarget_003E5__5, _003Cstart_003E5__4, Ease.CubeIn(_003Cwipe_003E5__7.Percent));
				cS06_StarJumpEnd.player.Speed.Y += 400f * Engine.DeltaTime;
				foreach (ReflectionTentacles tentacle3 in cS06_StarJumpEnd.tentacles)
				{
					tentacle3.Nodes[0] = new Vector2(_003C_003E8__1.level.Bounds.Center.X, cS06_StarJumpEnd.player.Y + 300f);
					tentacle3.Nodes[1] = new Vector2(_003C_003E8__1.level.Bounds.Center.X, cS06_StarJumpEnd.player.Y + 600f);
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 9;
				return true;
			}
			_003Cwipe_003E5__7 = null;
			_003Ctarget_003E5__5 = default(Vector2);
			_003Cstart_003E5__4 = default(Vector2);
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
	private sealed class _003CTentaclesAppear_003Ed__30 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CTentaclesAppear_003Ed__30(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				if (cS06_StarJumpEnd.tentacleIndex == 0)
				{
					Audio.Play("event:/game/06_reflection/badeline_freakout_1");
				}
				else if (cS06_StarJumpEnd.tentacleIndex == 1)
				{
					Audio.Play("event:/game/06_reflection/badeline_freakout_2");
				}
				else if (cS06_StarJumpEnd.tentacleIndex == 2)
				{
					Audio.Play("event:/game/06_reflection/badeline_freakout_3");
				}
				else
				{
					Audio.Play("event:/game/06_reflection/badeline_freakout_4");
				}
				if (!cS06_StarJumpEnd.hidingNorthingLights)
				{
					cS06_StarJumpEnd.Add(new Coroutine(cS06_StarJumpEnd.NothernLightsDown()));
					cS06_StarJumpEnd.hidingNorthingLights = true;
				}
				cS06_StarJumpEnd.Level.Shake();
				cS06_StarJumpEnd.anxietyFade += 0.1f;
				if (cS06_StarJumpEnd.tentacleIndex == 0)
				{
					cS06_StarJumpEnd.SetMusicLayer(3);
				}
				int num2 = 400;
				int num3 = 140;
				List<Vector2> startNodes = new List<Vector2>
				{
					new Vector2(cS06_StarJumpEnd.Level.Camera.X + 160f, cS06_StarJumpEnd.Level.Camera.Y + (float)num2),
					new Vector2(cS06_StarJumpEnd.Level.Camera.X + 160f, cS06_StarJumpEnd.Level.Camera.Y + (float)num2 + 200f)
				};
				ReflectionTentacles reflectionTentacles = new ReflectionTentacles();
				reflectionTentacles.Create(0f, 0, cS06_StarJumpEnd.tentacles.Count, startNodes);
				reflectionTentacles.Nodes[0] = new Vector2(reflectionTentacles.Nodes[0].X, cS06_StarJumpEnd.Level.Camera.Y + (float)num3);
				cS06_StarJumpEnd.Level.Add(reflectionTentacles);
				cS06_StarJumpEnd.tentacles.Add(reflectionTentacles);
				cS06_StarJumpEnd.charactersSpinning = false;
				cS06_StarJumpEnd.tentacleIndex++;
				cS06_StarJumpEnd.badeline.Sprite.Play("angry");
				cS06_StarJumpEnd.maddySineTarget = 1f;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
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
	private sealed class _003CTentaclesGrab_003Ed__31 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CTentaclesGrab_003Ed__31(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.maddySineTarget = 0f;
				Audio.Play("event:/game/06_reflection/badeline_freakout_5");
				cS06_StarJumpEnd.player.Sprite.Play("tentacle_grab");
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				cS06_StarJumpEnd.Level.Shake();
				cS06_StarJumpEnd.rumbler = new BreathingRumbler();
				cS06_StarJumpEnd.Level.Add(cS06_StarJumpEnd.rumbler);
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
	private sealed class _003C_003Ec__DisplayClass32_0
	{
		public CS06_StarJumpEnd _003C_003E4__this;

		public Vector2 from;

		public Vector2 to;

		internal void _003CStartCirclingPlayer_003Eb__0(Tween t)
		{
			_003C_003E4__this.player.Position = Vector2.Lerp(from, to, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003CStartCirclingPlayer_003Ed__32 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CStartCirclingPlayer_003Ed__32(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass32_0 CS_0024_003C_003E8__locals5 = new _003C_003Ec__DisplayClass32_0
				{
					_003C_003E4__this = _003C_003E4__this
				};
				cS06_StarJumpEnd.Add(new Coroutine(cS06_StarJumpEnd.BadelineCirclePlayer()));
				CS_0024_003C_003E8__locals5.from = cS06_StarJumpEnd.player.Position;
				CS_0024_003C_003E8__locals5.to = new Vector2(cS06_StarJumpEnd.Level.Bounds.Center.X, cS06_StarJumpEnd.player.Y);
				Tween.Set(cS06_StarJumpEnd, Tween.TweenMode.Oneshot, 0.5f, Ease.CubeOut, delegate(Tween t)
				{
					CS_0024_003C_003E8__locals5._003C_003E4__this.player.Position = Vector2.Lerp(CS_0024_003C_003E8__locals5.from, CS_0024_003C_003E8__locals5.to, t.Eased);
				});
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
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
	private sealed class _003CEndCirclingPlayer_003Ed__33 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CEndCirclingPlayer_003Ed__33(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.baddyCircling = false;
				_003C_003E2__current = null;
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
	private sealed class _003CBadelineCirclePlayer_003Ed__34 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

		private float _003Coffset_003E5__2;

		private float _003Cdist_003E5__3;

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
		public _003CBadelineCirclePlayer_003Ed__34(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Coffset_003E5__2 = 0f;
				_003Cdist_003E5__3 = (cS06_StarJumpEnd.badeline.Position - cS06_StarJumpEnd.player.Position).Length();
				cS06_StarJumpEnd.baddyCircling = true;
				goto IL_0145;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0145;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0145:
				if (cS06_StarJumpEnd.baddyCircling)
				{
					_003Coffset_003E5__2 -= Engine.DeltaTime * 4f;
					_003Cdist_003E5__3 = Calc.Approach(_003Cdist_003E5__3, 24f, Engine.DeltaTime * 32f);
					cS06_StarJumpEnd.badeline.Position = cS06_StarJumpEnd.player.Position + Calc.AngleToVector(_003Coffset_003E5__2, _003Cdist_003E5__3);
					int num2 = Math.Sign(cS06_StarJumpEnd.player.X - cS06_StarJumpEnd.badeline.X);
					if (num2 != 0)
					{
						cS06_StarJumpEnd.badeline.Sprite.Scale.X = num2;
					}
					if (cS06_StarJumpEnd.Level.OnInterval(0.1f))
					{
						TrailManager.Add(cS06_StarJumpEnd.badeline, Player.NormalHairColor);
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS06_StarJumpEnd.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = cS06_StarJumpEnd.badeline.FloatTo(cS06_StarJumpEnd.player.Position + new Vector2(40f, -16f), -1, faceDirection: false);
				_003C_003E1__state = 2;
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
	private sealed class _003CFeatherMinigame_003Ed__35 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CFeatherMinigame_003Ed__35(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.breathing = new BreathingMinigame(winnable: false, cS06_StarJumpEnd.rumbler);
				cS06_StarJumpEnd.Level.Add(cS06_StarJumpEnd.breathing);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (!cS06_StarJumpEnd.breathing.Pausing)
			{
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
	private sealed class _003CEndFeatherMinigame_003Ed__36 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CEndFeatherMinigame_003Ed__36(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.baddyCircling = false;
				cS06_StarJumpEnd.breathing.Pausing = false;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (!cS06_StarJumpEnd.breathing.Completed)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS06_StarJumpEnd.breathing = null;
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
	private sealed class _003CBadelineFlyDown_003Ed__37 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

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
		public _003CBadelineFlyDown_003Ed__37(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.badeline.Sprite.Play("fallFast");
				cS06_StarJumpEnd.badeline.FloatSpeed = 600f;
				cS06_StarJumpEnd.badeline.FloatAccel = 1200f;
				_003C_003E2__current = cS06_StarJumpEnd.badeline.FloatTo(new Vector2(cS06_StarJumpEnd.badeline.X, cS06_StarJumpEnd.Level.Camera.Y + 200f), null, faceDirection: true, fadeLight: true);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS06_StarJumpEnd.badeline.RemoveSelf();
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
	private sealed class _003CNothernLightsDown_003Ed__38 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

		private NorthernLights _003Cbg_003E5__2;

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
		public _003CNothernLightsDown_003Ed__38(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				_003Cbg_003E5__2 = cS06_StarJumpEnd.Level.Background.Get<NorthernLights>();
				if (_003Cbg_003E5__2 == null)
				{
					goto IL_0084;
				}
			}
			if (_003Cbg_003E5__2.NorthernLightsAlpha > 0f)
			{
				_003Cbg_003E5__2.NorthernLightsAlpha -= Engine.DeltaTime * 0.5f;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_0084;
			IL_0084:
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
	private sealed class _003CSpinCharacters_003Ed__39 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_StarJumpEnd _003C_003E4__this;

		private Vector2 _003CmaddyStart_003E5__2;

		private Vector2 _003CbaddyStart_003E5__3;

		private Vector2 _003Ccenter_003E5__4;

		private float _003Cdist_003E5__5;

		private float _003Ctimer_003E5__6;

		private Vector2 _003CmaddyFrom_003E5__7;

		private Vector2 _003CbaddyFrom_003E5__8;

		private float _003Cp_003E5__9;

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
		public _003CSpinCharacters_003Ed__39(int _003C_003E1__state)
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
			CS06_StarJumpEnd cS06_StarJumpEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003CmaddyStart_003E5__2 = cS06_StarJumpEnd.player.Position;
				_003CbaddyStart_003E5__3 = cS06_StarJumpEnd.badeline.Position;
				_003Ccenter_003E5__4 = (_003CmaddyStart_003E5__2 + _003CbaddyStart_003E5__3) / 2f;
				_003Cdist_003E5__5 = Math.Abs(_003CmaddyStart_003E5__2.X - _003Ccenter_003E5__4.X);
				_003Ctimer_003E5__6 = (float)Math.PI / 2f;
				cS06_StarJumpEnd.player.Sprite.Play("spin");
				cS06_StarJumpEnd.badeline.Sprite.Play("spin");
				cS06_StarJumpEnd.badeline.Sprite.Scale.X = 1f;
				goto IL_01c6;
			case 1:
				_003C_003E1__state = -1;
				goto IL_01c6;
			case 2:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__9 += Engine.DeltaTime * 3f;
					break;
				}
				IL_01c6:
				if (cS06_StarJumpEnd.charactersSpinning)
				{
					int num2 = (int)(_003Ctimer_003E5__6 / ((float)Math.PI * 2f) * 14f + 10f);
					cS06_StarJumpEnd.player.Sprite.SetAnimationFrame(num2);
					cS06_StarJumpEnd.badeline.Sprite.SetAnimationFrame(num2 + 7);
					float num3 = (float)Math.Sin(_003Ctimer_003E5__6);
					float num4 = (float)Math.Cos(_003Ctimer_003E5__6);
					cS06_StarJumpEnd.player.Position = _003Ccenter_003E5__4 - new Vector2(num3 * _003Cdist_003E5__5, num4 * 8f);
					cS06_StarJumpEnd.badeline.Position = _003Ccenter_003E5__4 + new Vector2(num3 * _003Cdist_003E5__5, num4 * 8f);
					_003Ctimer_003E5__6 += Engine.DeltaTime * 2f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS06_StarJumpEnd.player.Facing = Facings.Right;
				cS06_StarJumpEnd.player.Sprite.Play("fallSlow");
				cS06_StarJumpEnd.badeline.Sprite.Scale.X = -1f;
				cS06_StarJumpEnd.badeline.Sprite.Play("angry");
				cS06_StarJumpEnd.badeline.AutoAnimator.Enabled = false;
				_003CmaddyFrom_003E5__7 = cS06_StarJumpEnd.player.Position;
				_003CbaddyFrom_003E5__8 = cS06_StarJumpEnd.badeline.Position;
				_003Cp_003E5__9 = 0f;
				break;
			}
			if (_003Cp_003E5__9 < 1f)
			{
				cS06_StarJumpEnd.player.Position = Vector2.Lerp(_003CmaddyFrom_003E5__7, _003CmaddyStart_003E5__2, Ease.CubeOut(_003Cp_003E5__9));
				cS06_StarJumpEnd.badeline.Position = Vector2.Lerp(_003CbaddyFrom_003E5__8, _003CbaddyStart_003E5__3, Ease.CubeOut(_003Cp_003E5__9));
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
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

	public const string Flag = "plateau_2";

	private bool waiting = true;

	private bool shaking;

	private NPC theo;

	private Player player;

	private Bonfire bonfire;

	private BadelineDummy badeline;

	private Plateau plateau;

	private BreathingMinigame breathing;

	private List<ReflectionTentacles> tentacles = new List<ReflectionTentacles>();

	private Vector2 playerStart;

	private Vector2 cameraStart;

	private float anxietyFade;

	private SineWave anxietySine;

	private float anxietyJitter;

	private bool hidingNorthingLights;

	private bool charactersSpinning;

	private float maddySine;

	private float maddySineTarget;

	private float maddySineAnchorY;

	private SoundSource shakingLoopSfx;

	private bool baddyCircling;

	private BreathingRumbler rumbler;

	private int tentacleIndex;

	public CS06_StarJumpEnd(NPC theo, Player player, Vector2 playerStart, Vector2 cameraStart)
	{
		base.Depth = 10100;
		this.theo = theo;
		this.player = player;
		this.playerStart = playerStart;
		this.cameraStart = cameraStart;
		Add(anxietySine = new SineWave(0.3f));
	}

	public override void Added(Scene scene)
	{
		Level = scene as Level;
		bonfire = scene.Entities.FindFirst<Bonfire>();
		plateau = scene.Entities.FindFirst<Plateau>();
	}

	public override void Update()
	{
		base.Update();
		if (waiting && player.Y <= (float)(Level.Bounds.Top + 160))
		{
			waiting = false;
			Start();
		}
		if (shaking)
		{
			Level.Shake(0.2f);
		}
		if (Level != null && Level.OnInterval(0.1f))
		{
			anxietyJitter = Calc.Random.Range(-0.1f, 0.1f);
		}
		Distort.Anxiety = anxietyFade * Math.Max(0f, 0f + anxietyJitter + anxietySine.Value * 0.6f);
		maddySine = Calc.Approach(maddySine, maddySineTarget, 12f * Engine.DeltaTime);
		if (maddySine > 0f)
		{
			player.Y = maddySineAnchorY + (float)Math.Sin(Level.TimeActive * 2f) * 3f * maddySine;
		}
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__27))]
	private IEnumerator Cutscene(Level level)
	{
		level.Entities.FindFirst<StarJumpController>()?.RemoveSelf();
		foreach (StarJumpBlock item in level.Entities.FindAll<StarJumpBlock>())
		{
			item.Collidable = false;
		}
		int center = level.Bounds.X + 160;
		Vector2 cutsceneCenter = new Vector2(center, level.Bounds.Top + 150);
		NorthernLights bg = level.Background.Get<NorthernLights>();
		level.CameraOffset.Y = -30f;
		Add(new Coroutine(CutsceneEntity.CameraTo(cutsceneCenter + new Vector2(-160f, -70f), 1.5f, Ease.CubeOut)));
		Add(new Coroutine(CutsceneEntity.CameraTo(cutsceneCenter + new Vector2(-160f, -120f), 2f, Ease.CubeInOut, 1.5f)));
		Tween.Set(this, Tween.TweenMode.Oneshot, 3f, Ease.CubeInOut, delegate(Tween t)
		{
			bg.OffsetY = t.Eased * 32f;
		});
		if (player.StateMachine.State == 19)
		{
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		}
		player.Dashes = 0;
		player.StateMachine.State = 11;
		player.DummyGravity = false;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("fallSlow");
		player.Dashes = 1;
		player.Speed = new Vector2(0f, -80f);
		player.Facing = Facings.Right;
		player.ForceCameraUpdate = false;
		while (player.Speed.Length() > 0f || player.Position != cutsceneCenter)
		{
			player.Speed = Calc.Approach(player.Speed, Vector2.Zero, 200f * Engine.DeltaTime);
			player.Position = Calc.Approach(player.Position, cutsceneCenter, 64f * Engine.DeltaTime);
			yield return null;
		}
		player.Sprite.Play("spin");
		yield return 3.5f;
		player.Facing = Facings.Right;
		level.Add(badeline = new BadelineDummy(player.Position));
		level.Displacement.AddBurst(player.Position, 0.5f, 8f, 48f, 0.5f);
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		player.CreateSplitParticles();
		Audio.Play("event:/char/badeline/maddy_split");
		badeline.Sprite.Scale.X = -1f;
		Vector2 start = player.Position;
		Vector2 target = cutsceneCenter + new Vector2(-30f, 0f);
		maddySineAnchorY = cutsceneCenter.Y;
		for (float p = 0f; p <= 1f; p += 2f * Engine.DeltaTime)
		{
			yield return null;
			if (p > 1f)
			{
				p = 1f;
			}
			player.Position = Vector2.Lerp(start, target, Ease.CubeOut(p));
			badeline.Position = new Vector2((float)center + ((float)center - player.X), player.Y);
		}
		charactersSpinning = true;
		Add(new Coroutine(SpinCharacters()));
		SetMusicLayer(2);
		yield return 1f;
		yield return Textbox.Say("ch6_dreaming", TentaclesAppear, TentaclesGrab, FeatherMinigame, EndFeatherMinigame, StartCirclingPlayer);
		Audio.Play("event:/game/06_reflection/badeline_pull_whooshdown");
		Add(new Coroutine(BadelineFlyDown()));
		yield return 0.7f;
		foreach (FlyFeather item2 in level.Entities.FindAll<FlyFeather>())
		{
			item2.RemoveSelf();
		}
		foreach (StarJumpBlock item3 in level.Entities.FindAll<StarJumpBlock>())
		{
			item3.RemoveSelf();
		}
		foreach (JumpThru item4 in level.Entities.FindAll<JumpThru>())
		{
			item4.RemoveSelf();
		}
		level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
		level.CameraOffset.Y = 0f;
		player.Sprite.Play("tentacle_pull");
		player.Speed.Y = 160f;
		FallEffects.Show(visible: true);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 3f)
		{
			player.Speed.Y += Engine.DeltaTime * 100f;
			if (player.X < (float)(level.Bounds.X + 32))
			{
				player.X = level.Bounds.X + 32;
			}
			if (player.X > (float)(level.Bounds.Right - 32))
			{
				player.X = level.Bounds.Right - 32;
			}
			if (p > 0.7f)
			{
				level.CameraOffset.Y -= 100f * Engine.DeltaTime;
			}
			foreach (ReflectionTentacles tentacle in tentacles)
			{
				tentacle.Nodes[0] = new Vector2(level.Bounds.Center.X, player.Y + 300f);
				tentacle.Nodes[1] = new Vector2(level.Bounds.Center.X, player.Y + 600f);
			}
			FallEffects.SpeedMultiplier += Engine.DeltaTime * 0.75f;
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
			yield return null;
		}
		Audio.Play("event:/game/06_reflection/badeline_pull_impact");
		FallEffects.Show(visible: false);
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		level.Flash(Color.White);
		level.Session.Dreaming = false;
		level.CameraOffset.Y = 0f;
		level.Camera.Position = cameraStart;
		SetBloom(0f);
		bonfire.SetMode(Bonfire.Mode.Smoking);
		plateau.Depth = player.Depth + 10;
		plateau.Remove(plateau.Occluder);
		player.Position = playerStart + new Vector2(0f, 8f);
		player.Speed = Vector2.Zero;
		player.Sprite.Play("tentacle_dangling");
		player.Facing = Facings.Left;
		theo.Position.X -= 24f;
		theo.Sprite.Play("alert");
		foreach (ReflectionTentacles tentacle2 in tentacles)
		{
			tentacle2.Index = 0;
			tentacle2.Nodes[0] = new Vector2(level.Bounds.Center.X, player.Y + 32f);
			tentacle2.Nodes[1] = new Vector2(level.Bounds.Center.X, player.Y + 400f);
			tentacle2.SnapTentacles();
		}
		shaking = true;
		Add(shakingLoopSfx = new SoundSource());
		shakingLoopSfx.Play("event:/game/06_reflection/badeline_pull_rumble_loop");
		yield return Textbox.Say("ch6_theo_watchout");
		Audio.Play("event:/game/06_reflection/badeline_pull_cliffbreak");
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Long);
		shakingLoopSfx.Stop();
		shaking = false;
		for (int num = 0; (float)num < plateau.Width; num += 8)
		{
			level.Add(Engine.Pooler.Create<Debris>().Init(plateau.Position + new Vector2((float)num + Calc.Random.NextFloat(8f), Calc.Random.NextFloat(8f)), '3').BlastFrom(plateau.Center + new Vector2(0f, 8f)));
			level.Add(Engine.Pooler.Create<Debris>().Init(plateau.Position + new Vector2((float)num + Calc.Random.NextFloat(8f), Calc.Random.NextFloat(8f)), '3').BlastFrom(plateau.Center + new Vector2(0f, 8f)));
		}
		plateau.RemoveSelf();
		bonfire.RemoveSelf();
		level.Shake();
		player.Speed.Y = 160f;
		player.Sprite.Play("tentacle_pull");
		player.ForceCameraUpdate = false;
		FadeWipe wipe = new FadeWipe(level, wipeIn: false, delegate
		{
			EndCutscene(level);
		})
		{
			Duration = 3f
		};
		target = level.Camera.Position;
		start = level.Camera.Position + new Vector2(0f, 400f);
		while (wipe.Percent < 1f)
		{
			level.Camera.Position = Vector2.Lerp(target, start, Ease.CubeIn(wipe.Percent));
			player.Speed.Y += 400f * Engine.DeltaTime;
			foreach (ReflectionTentacles tentacle3 in tentacles)
			{
				tentacle3.Nodes[0] = new Vector2(level.Bounds.Center.X, player.Y + 300f);
				tentacle3.Nodes[1] = new Vector2(level.Bounds.Center.X, player.Y + 600f);
			}
			yield return null;
		}
	}

	private void SetMusicLayer(int index)
	{
		for (int i = 1; i <= 3; i++)
		{
			Level.Session.Audio.Music.Layer(i, index == i);
		}
		Level.Session.Audio.Apply();
	}

	[IteratorStateMachine(typeof(_003CTentaclesAppear_003Ed__30))]
	private IEnumerator TentaclesAppear()
	{
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		if (tentacleIndex == 0)
		{
			Audio.Play("event:/game/06_reflection/badeline_freakout_1");
		}
		else if (tentacleIndex == 1)
		{
			Audio.Play("event:/game/06_reflection/badeline_freakout_2");
		}
		else if (tentacleIndex == 2)
		{
			Audio.Play("event:/game/06_reflection/badeline_freakout_3");
		}
		else
		{
			Audio.Play("event:/game/06_reflection/badeline_freakout_4");
		}
		if (!hidingNorthingLights)
		{
			Add(new Coroutine(NothernLightsDown()));
			hidingNorthingLights = true;
		}
		Level.Shake();
		anxietyFade += 0.1f;
		if (tentacleIndex == 0)
		{
			SetMusicLayer(3);
		}
		int num = 400;
		int num2 = 140;
		List<Vector2> list = new List<Vector2>();
		list.Add(new Vector2(Level.Camera.X + 160f, Level.Camera.Y + (float)num));
		list.Add(new Vector2(Level.Camera.X + 160f, Level.Camera.Y + (float)num + 200f));
		ReflectionTentacles reflectionTentacles = new ReflectionTentacles();
		reflectionTentacles.Create(0f, 0, tentacles.Count, list);
		reflectionTentacles.Nodes[0] = new Vector2(reflectionTentacles.Nodes[0].X, Level.Camera.Y + (float)num2);
		Level.Add(reflectionTentacles);
		tentacles.Add(reflectionTentacles);
		charactersSpinning = false;
		tentacleIndex++;
		badeline.Sprite.Play("angry");
		maddySineTarget = 1f;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CTentaclesGrab_003Ed__31))]
	private IEnumerator TentaclesGrab()
	{
		maddySineTarget = 0f;
		Audio.Play("event:/game/06_reflection/badeline_freakout_5");
		player.Sprite.Play("tentacle_grab");
		yield return 0.1f;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		Level.Shake();
		rumbler = new BreathingRumbler();
		Level.Add(rumbler);
	}

	[IteratorStateMachine(typeof(_003CStartCirclingPlayer_003Ed__32))]
	private IEnumerator StartCirclingPlayer()
	{
		Add(new Coroutine(BadelineCirclePlayer()));
		Vector2 from = player.Position;
		Vector2 to = new Vector2(Level.Bounds.Center.X, player.Y);
		Tween.Set(this, Tween.TweenMode.Oneshot, 0.5f, Ease.CubeOut, delegate(Tween t)
		{
			player.Position = Vector2.Lerp(from, to, t.Eased);
		});
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CEndCirclingPlayer_003Ed__33))]
	private IEnumerator EndCirclingPlayer()
	{
		baddyCircling = false;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CBadelineCirclePlayer_003Ed__34))]
	private IEnumerator BadelineCirclePlayer()
	{
		float offset = 0f;
		float dist = (badeline.Position - player.Position).Length();
		baddyCircling = true;
		while (baddyCircling)
		{
			offset -= Engine.DeltaTime * 4f;
			dist = Calc.Approach(dist, 24f, Engine.DeltaTime * 32f);
			badeline.Position = player.Position + Calc.AngleToVector(offset, dist);
			int num = Math.Sign(player.X - badeline.X);
			if (num != 0)
			{
				badeline.Sprite.Scale.X = num;
			}
			if (Level.OnInterval(0.1f))
			{
				TrailManager.Add(badeline, Player.NormalHairColor);
			}
			yield return null;
		}
		badeline.Sprite.Scale.X = -1f;
		yield return badeline.FloatTo(player.Position + new Vector2(40f, -16f), -1, faceDirection: false);
	}

	[IteratorStateMachine(typeof(_003CFeatherMinigame_003Ed__35))]
	private IEnumerator FeatherMinigame()
	{
		breathing = new BreathingMinigame(winnable: false, rumbler);
		Level.Add(breathing);
		while (!breathing.Pausing)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CEndFeatherMinigame_003Ed__36))]
	private IEnumerator EndFeatherMinigame()
	{
		baddyCircling = false;
		breathing.Pausing = false;
		while (!breathing.Completed)
		{
			yield return null;
		}
		breathing = null;
	}

	[IteratorStateMachine(typeof(_003CBadelineFlyDown_003Ed__37))]
	private IEnumerator BadelineFlyDown()
	{
		badeline.Sprite.Play("fallFast");
		badeline.FloatSpeed = 600f;
		badeline.FloatAccel = 1200f;
		yield return badeline.FloatTo(new Vector2(badeline.X, Level.Camera.Y + 200f), null, faceDirection: true, fadeLight: true);
		badeline.RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CNothernLightsDown_003Ed__38))]
	private IEnumerator NothernLightsDown()
	{
		NorthernLights bg = Level.Background.Get<NorthernLights>();
		if (bg != null)
		{
			while (bg.NorthernLightsAlpha > 0f)
			{
				bg.NorthernLightsAlpha -= Engine.DeltaTime * 0.5f;
				yield return null;
			}
		}
	}

	[IteratorStateMachine(typeof(_003CSpinCharacters_003Ed__39))]
	private IEnumerator SpinCharacters()
	{
		Vector2 maddyStart = player.Position;
		Vector2 baddyStart = badeline.Position;
		Vector2 center = (maddyStart + baddyStart) / 2f;
		float dist = Math.Abs(maddyStart.X - center.X);
		float timer = (float)Math.PI / 2f;
		player.Sprite.Play("spin");
		badeline.Sprite.Play("spin");
		badeline.Sprite.Scale.X = 1f;
		while (charactersSpinning)
		{
			int num = (int)(timer / ((float)Math.PI * 2f) * 14f + 10f);
			player.Sprite.SetAnimationFrame(num);
			badeline.Sprite.SetAnimationFrame(num + 7);
			float num2 = (float)Math.Sin(timer);
			float num3 = (float)Math.Cos(timer);
			player.Position = center - new Vector2(num2 * dist, num3 * 8f);
			badeline.Position = center + new Vector2(num2 * dist, num3 * 8f);
			timer += Engine.DeltaTime * 2f;
			yield return null;
		}
		player.Facing = Facings.Right;
		player.Sprite.Play("fallSlow");
		badeline.Sprite.Scale.X = -1f;
		badeline.Sprite.Play("angry");
		badeline.AutoAnimator.Enabled = false;
		Vector2 maddyFrom = player.Position;
		Vector2 baddyFrom = badeline.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 3f)
		{
			player.Position = Vector2.Lerp(maddyFrom, maddyStart, Ease.CubeOut(p));
			badeline.Position = Vector2.Lerp(baddyFrom, baddyStart, Ease.CubeOut(p));
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		if (rumbler != null)
		{
			rumbler.RemoveSelf();
			rumbler = null;
		}
		if (breathing != null)
		{
			breathing.RemoveSelf();
		}
		SetBloom(0f);
		level.Session.Audio.Music.Event = null;
		level.Session.Audio.Apply();
		level.Remove(player);
		level.UnloadLevel();
		level.EndCutscene();
		level.Session.SetFlag("plateau_2");
		level.SnapColorGrade(AreaData.Get(level).ColorGrade);
		level.Session.Dreaming = false;
		level.Session.FirstLevel = false;
		if (WasSkipped)
		{
			level.OnEndOfFrame += delegate
			{
				level.Session.Level = "00";
				level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Bottom));
				level.LoadLevel(Player.IntroTypes.None);
				FallEffects.Show(visible: false);
				level.Session.Audio.Music.Event = "event:/music/lvl6/main";
				level.Session.Audio.Apply();
			};
			return;
		}
		Engine.Scene = new OverworldReflectionsFall(level, delegate
		{
			Audio.SetAmbience(null);
			level.Session.Level = "04";
			level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Center.X, level.Bounds.Top));
			level.LoadLevel(Player.IntroTypes.Fall);
			level.Add(new BackgroundFadeIn(Color.Black, 2f, 30f));
			level.Entities.UpdateLists();
			foreach (CrystalStaticSpinner entity in level.Tracker.GetEntities<CrystalStaticSpinner>())
			{
				entity.ForceInstantiate();
			}
		});
	}

	private void SetBloom(float add)
	{
		Level.Session.BloomBaseAdd = add;
		Level.Bloom.Base = AreaData.Get(Level).BloomBase + add;
	}
}
