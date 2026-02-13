using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class HeartGem : Entity
{
	[CompilerGenerated]
	private sealed class _003CCollectRoutine_003Ed__35 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeartGem _003C_003E4__this;

		public Player player;

		private Level _003Clevel_003E5__2;

		private AreaKey _003Carea_003E5__3;

		private string _003CpoemID_003E5__4;

		private bool _003CcompleteArea_003E5__5;

		private float _003Ct_003E5__6;

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
		public _003CCollectRoutine_003Ed__35(int _003C_003E1__state)
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
			HeartGem CS_0024_003C_003E8__locals38 = _003C_003E4__this;
			string text;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = CS_0024_003C_003E8__locals38.Scene as Level;
				_003Carea_003E5__3 = _003Clevel_003E5__2.Session.Area;
				_003CpoemID_003E5__4 = AreaData.Get(_003Clevel_003E5__2).Mode[(int)_003Carea_003E5__3.Mode].PoemID;
				_003CcompleteArea_003E5__5 = !CS_0024_003C_003E8__locals38.IsFake && (_003Carea_003E5__3.Mode != AreaMode.Normal || _003Carea_003E5__3.ID == 9);
				if (CS_0024_003C_003E8__locals38.IsFake)
				{
					_003Clevel_003E5__2.StartCutscene(CS_0024_003C_003E8__locals38.SkipFakeHeartCutscene);
				}
				else
				{
					_003Clevel_003E5__2.CanRetry = false;
				}
				if (_003CcompleteArea_003E5__5 || CS_0024_003C_003E8__locals38.IsFake)
				{
					Audio.SetMusic(null);
					Audio.SetAmbience(null);
				}
				if (_003CcompleteArea_003E5__5)
				{
					List<Strawberry> list = new List<Strawberry>();
					foreach (Follower follower in player.Leader.Followers)
					{
						if (follower.Entity is Strawberry)
						{
							list.Add(follower.Entity as Strawberry);
						}
					}
					foreach (Strawberry item in list)
					{
						item.OnCollect();
					}
				}
				string sfx = "event:/game/general/crystalheart_blue_get";
				if (CS_0024_003C_003E8__locals38.IsFake)
				{
					sfx = "event:/new_content/game/10_farewell/fakeheart_get";
				}
				else if (_003Carea_003E5__3.Mode == AreaMode.BSide)
				{
					sfx = "event:/game/general/crystalheart_red_get";
				}
				else if (_003Carea_003E5__3.Mode == AreaMode.CSide)
				{
					sfx = "event:/game/general/crystalheart_gold_get";
				}
				CS_0024_003C_003E8__locals38.sfx = SoundEmitter.Play(sfx, CS_0024_003C_003E8__locals38);
				CS_0024_003C_003E8__locals38.Add(new LevelEndingHook(delegate
				{
					CS_0024_003C_003E8__locals38.sfx.Source.Stop();
				}));
				CS_0024_003C_003E8__locals38.walls.Add(new InvisibleBarrier(new Vector2(_003Clevel_003E5__2.Bounds.Right, _003Clevel_003E5__2.Bounds.Top), 8f, _003Clevel_003E5__2.Bounds.Height));
				CS_0024_003C_003E8__locals38.walls.Add(new InvisibleBarrier(new Vector2(_003Clevel_003E5__2.Bounds.Left - 8, _003Clevel_003E5__2.Bounds.Top), 8f, _003Clevel_003E5__2.Bounds.Height));
				CS_0024_003C_003E8__locals38.walls.Add(new InvisibleBarrier(new Vector2(_003Clevel_003E5__2.Bounds.Left, _003Clevel_003E5__2.Bounds.Top - 8), _003Clevel_003E5__2.Bounds.Width, 8f));
				foreach (InvisibleBarrier wall in CS_0024_003C_003E8__locals38.walls)
				{
					CS_0024_003C_003E8__locals38.Scene.Add(wall);
				}
				CS_0024_003C_003E8__locals38.Add(CS_0024_003C_003E8__locals38.white = GFX.SpriteBank.Create("heartGemWhite"));
				CS_0024_003C_003E8__locals38.Depth = -2000000;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				Celeste.Freeze(0.2f);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				Engine.TimeRate = 0.5f;
				player.Depth = -2000000;
				for (int num2 = 0; num2 < 10; num2++)
				{
					CS_0024_003C_003E8__locals38.Scene.Add(new AbsorbOrb(CS_0024_003C_003E8__locals38.Position));
				}
				_003Clevel_003E5__2.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				_003Clevel_003E5__2.Flash(Color.White);
				_003Clevel_003E5__2.FormationBackdrop.Display = true;
				_003Clevel_003E5__2.FormationBackdrop.Alpha = 1f;
				CS_0024_003C_003E8__locals38.light.Alpha = (CS_0024_003C_003E8__locals38.bloom.Alpha = 0f);
				CS_0024_003C_003E8__locals38.Visible = false;
				_003Ct_003E5__6 = 0f;
				goto IL_04db;
			}
			case 3:
				_003C_003E1__state = -1;
				_003Ct_003E5__6 += Engine.RawDeltaTime;
				goto IL_04db;
			case 4:
				_003C_003E1__state = -1;
				if (player.Dead)
				{
					_003C_003E2__current = 100f;
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_052c;
			case 5:
				_003C_003E1__state = -1;
				goto IL_052c;
			case 6:
				_003C_003E1__state = -1;
				_003Ct_003E5__6 += Engine.RawDeltaTime;
				goto IL_066a;
			case 7:
				_003C_003E1__state = -1;
				break;
			case 8:
				_003C_003E1__state = -1;
				goto IL_06bd;
			case 9:
				_003C_003E1__state = -1;
				_003Ct_003E5__6 += Engine.RawDeltaTime * 2f;
				goto IL_076a;
			case 10:
				{
					_003C_003E1__state = -1;
					_003Clevel_003E5__2.CompleteArea(spotlightWipe: false, skipScreenWipe: true);
					break;
				}
				IL_052c:
				Engine.TimeRate = 1f;
				CS_0024_003C_003E8__locals38.Tag = Tags.FrozenUpdate;
				_003Clevel_003E5__2.Frozen = true;
				if (!CS_0024_003C_003E8__locals38.IsFake)
				{
					CS_0024_003C_003E8__locals38.RegisterAsCollected(_003Clevel_003E5__2, _003CpoemID_003E5__4);
					if (_003CcompleteArea_003E5__5)
					{
						_003Clevel_003E5__2.TimerStopped = true;
						_003Clevel_003E5__2.RegisterAreaComplete();
					}
				}
				text = null;
				if (!string.IsNullOrEmpty(_003CpoemID_003E5__4))
				{
					text = Dialog.Clean("poem_" + _003CpoemID_003E5__4);
				}
				CS_0024_003C_003E8__locals38.poem = new Poem(text, (int)(CS_0024_003C_003E8__locals38.IsFake ? ((AreaMode)3) : _003Carea_003E5__3.Mode), (_003Carea_003E5__3.Mode == AreaMode.CSide || CS_0024_003C_003E8__locals38.IsFake) ? 1f : 0.6f);
				CS_0024_003C_003E8__locals38.poem.Alpha = 0f;
				CS_0024_003C_003E8__locals38.Scene.Add(CS_0024_003C_003E8__locals38.poem);
				_003Ct_003E5__6 = 0f;
				goto IL_066a;
				IL_076a:
				if (_003Ct_003E5__6 < 1f)
				{
					CS_0024_003C_003E8__locals38.poem.Alpha = Ease.CubeIn(1f - _003Ct_003E5__6);
					_003C_003E2__current = null;
					_003C_003E1__state = 9;
					return true;
				}
				player.Depth = 0;
				CS_0024_003C_003E8__locals38.EndCutscene();
				break;
				IL_04db:
				if (_003Ct_003E5__6 < 2f)
				{
					Engine.TimeRate = Calc.Approach(Engine.TimeRate, 0f, Engine.RawDeltaTime * 0.25f);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
				IL_066a:
				if (_003Ct_003E5__6 < 1f)
				{
					CS_0024_003C_003E8__locals38.poem.Alpha = Ease.CubeOut(_003Ct_003E5__6);
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				if (CS_0024_003C_003E8__locals38.IsFake)
				{
					_003C_003E2__current = CS_0024_003C_003E8__locals38.DoFakeRoutineWithBird(player);
					_003C_003E1__state = 7;
					return true;
				}
				goto IL_06bd;
				IL_06bd:
				if (!Input.MenuConfirm.Pressed && !Input.MenuCancel.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				CS_0024_003C_003E8__locals38.sfx.Source.Param("end", 1f);
				if (!_003CcompleteArea_003E5__5)
				{
					_003Clevel_003E5__2.FormationBackdrop.Display = false;
					_003Ct_003E5__6 = 0f;
					goto IL_076a;
				}
				_003C_003E2__current = new FadeWipe(_003Clevel_003E5__2, wipeIn: false)
				{
					Duration = 3.25f
				}.Duration;
				_003C_003E1__state = 10;
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
	private sealed class _003CDoFakeRoutineWithBird_003Ed__38 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeartGem _003C_003E4__this;

		public Player player;

		private Level _003Clevel_003E5__2;

		private int _003CpanAmount_003E5__3;

		private Vector2 _003CpanFrom_003E5__4;

		private Vector2 _003CpanTo_003E5__5;

		private Vector2 _003CbirdFrom_003E5__6;

		private Vector2 _003CbirdTo_003E5__7;

		private float _003Cp_003E5__8;

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
		public _003CDoFakeRoutineWithBird_003Ed__38(int _003C_003E1__state)
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
			HeartGem heartGem = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = heartGem.Scene as Level;
				_003CpanAmount_003E5__3 = 64;
				_003CpanFrom_003E5__4 = _003Clevel_003E5__2.Camera.Position;
				_003CpanTo_003E5__5 = _003Clevel_003E5__2.Camera.Position + new Vector2(-_003CpanAmount_003E5__3, 0f);
				_003CbirdFrom_003E5__6 = new Vector2(_003CpanTo_003E5__5.X - 16f, player.Y - 20f);
				_003CbirdTo_003E5__7 = new Vector2(_003CpanFrom_003E5__4.X + 320f + 16f, player.Y - 20f);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Glitch.Value = 0.75f;
				goto IL_0193;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0193;
			case 3:
				_003C_003E1__state = -1;
				Glitch.Value = 0.75f;
				goto IL_0211;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0211;
			case 5:
				_003C_003E1__state = -1;
				_003Cp_003E5__8 = 0f;
				goto IL_02f0;
			case 6:
				_003C_003E1__state = -1;
				_003Cp_003E5__8 += Engine.RawDeltaTime / 2f;
				goto IL_02f0;
			case 7:
				_003C_003E1__state = -1;
				_003Cp_003E5__8 += Engine.RawDeltaTime / 2.6f;
				goto IL_051c;
			case 8:
				_003C_003E1__state = -1;
				goto IL_05aa;
			case 9:
				_003C_003E1__state = -1;
				heartGem.sfx.Source.Param("end", 1f);
				_003C_003E2__current = 0.283f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2.FormationBackdrop.Display = false;
				_003Cp_003E5__8 = 0f;
				goto IL_06a8;
			case 11:
				_003C_003E1__state = -1;
				_003Cp_003E5__8 += Engine.RawDeltaTime / 0.2f;
				goto IL_06a8;
			case 12:
				_003C_003E1__state = -1;
				goto IL_06fd;
			case 13:
				_003C_003E1__state = -1;
				goto IL_07ec;
			case 14:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2.Session.Audio.Music.Event = "event:/new_content/music/lvl10/intermission_heartgroove";
				_003Clevel_003E5__2.Session.Audio.Apply();
				player.Active = true;
				player.Depth = 0;
				player.StateMachine.State = 11;
				goto IL_0892;
			case 15:
				_003C_003E1__state = -1;
				goto IL_0892;
			case 16:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_KEEP_GOING", heartGem.PlayerStepForward);
				_003C_003E1__state = 17;
				return true;
			case 17:
				{
					_003C_003E1__state = -1;
					heartGem.SkipFakeHeartCutscene(_003Clevel_003E5__2);
					_003Clevel_003E5__2.EndCutscene();
					return false;
				}
				IL_0892:
				if (!player.OnGround() && player.Bottom < (float)_003Clevel_003E5__2.Bounds.Bottom)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 15;
					return true;
				}
				player.Facing = Facings.Right;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 16;
				return true;
				IL_051c:
				if (_003Cp_003E5__8 < 1f)
				{
					_003Clevel_003E5__2.Camera.Position = _003CpanTo_003E5__5 + (_003CpanFrom_003E5__4 - _003CpanTo_003E5__5) * Ease.CubeInOut(_003Cp_003E5__8);
					heartGem.poem.Offset = new Vector2(_003CpanAmount_003E5__3 * 8, 0f) * Ease.CubeInOut(1f - _003Cp_003E5__8);
					float num2 = 0.1f;
					float num3 = 0.9f;
					if (_003Cp_003E5__8 > num2 && _003Cp_003E5__8 <= num3)
					{
						float num4 = (_003Cp_003E5__8 - num2) / (num3 - num2);
						heartGem.bird.Position = _003CbirdFrom_003E5__6 + (_003CbirdTo_003E5__7 - _003CbirdFrom_003E5__6) * num4 + Vector2.UnitY * (float)Math.Sin(num4 * 8f) * 8f;
					}
					if (_003Clevel_003E5__2.OnRawInterval(0.2f))
					{
						TrailManager.Add(heartGem.bird, Calc.HexToColor("639bff"), 1f, frozenUpdate: true, useRawDeltaTime: true);
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				heartGem.bird.RemoveSelf();
				heartGem.bird = null;
				Engine.TimeRate = 0f;
				_003Clevel_003E5__2.Frozen = false;
				player.Active = false;
				player.StateMachine.State = 11;
				goto IL_05aa;
				IL_02f0:
				if (_003Cp_003E5__8 < 1f)
				{
					_003Clevel_003E5__2.Camera.Position = _003CpanFrom_003E5__4 + (_003CpanTo_003E5__5 - _003CpanFrom_003E5__4) * Ease.CubeInOut(_003Cp_003E5__8);
					heartGem.poem.Offset = new Vector2(_003CpanAmount_003E5__3 * 8, 0f) * Ease.CubeInOut(_003Cp_003E5__8);
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				heartGem.bird = new BirdNPC(_003CbirdFrom_003E5__6, BirdNPC.Modes.None);
				heartGem.bird.Sprite.Play("fly");
				heartGem.bird.Sprite.UseRawDeltaTime = true;
				heartGem.bird.Facing = Facings.Right;
				heartGem.bird.Depth = -2000100;
				heartGem.bird.Tag = Tags.FrozenUpdate;
				heartGem.bird.Add(new VertexLight(Color.White, 0.5f, 8, 32));
				heartGem.bird.Add(new BloomPoint(0.5f, 12f));
				_003Clevel_003E5__2.Add(heartGem.bird);
				_003Cp_003E5__8 = 0f;
				goto IL_051c;
				IL_06a8:
				if (_003Cp_003E5__8 < 1f)
				{
					heartGem.poem.TextAlpha = Ease.CubeIn(1f - _003Cp_003E5__8);
					heartGem.poem.ParticleSpeed = heartGem.poem.TextAlpha;
					_003C_003E2__current = null;
					_003C_003E1__state = 11;
					return true;
				}
				heartGem.poem.Heart.Play("break");
				goto IL_06fd;
				IL_06fd:
				if (heartGem.poem.Heart.Animating)
				{
					heartGem.poem.Shake += Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 12;
					return true;
				}
				heartGem.poem.RemoveSelf();
				heartGem.poem = null;
				for (int i = 0; i < 10; i++)
				{
					Vector2 position = _003Clevel_003E5__2.Camera.Position + new Vector2(320f, 180f) * 0.5f;
					Vector2 value = _003Clevel_003E5__2.Camera.Position + new Vector2(160f, -64f);
					heartGem.Scene.Add(new AbsorbOrb(position, null, value));
				}
				_003Clevel_003E5__2.Shake();
				Glitch.Value = 0.8f;
				goto IL_07ec;
				IL_0211:
				if (Glitch.Value > 0f)
				{
					Glitch.Value = Calc.Approach(Glitch.Value, 0f, Engine.RawDeltaTime * 4f);
					_003Clevel_003E5__2.Shake();
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 5;
				return true;
				IL_07ec:
				if (Glitch.Value > 0f)
				{
					Glitch.Value -= Engine.DeltaTime * 4f;
					_003C_003E2__current = null;
					_003C_003E1__state = 13;
					return true;
				}
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 14;
				return true;
				IL_0193:
				if (Glitch.Value > 0f)
				{
					Glitch.Value = Calc.Approach(Glitch.Value, 0f, Engine.RawDeltaTime * 4f);
					_003Clevel_003E5__2.Shake();
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 1.1f;
				_003C_003E1__state = 3;
				return true;
				IL_05aa:
				if (Engine.TimeRate != 1f)
				{
					Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 0.5f * Engine.RawDeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				Engine.TimeRate = 1f;
				_003C_003E2__current = Textbox.Say("CH9_FAKE_HEART");
				_003C_003E1__state = 9;
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
	private sealed class _003CPlayerStepForward_003Ed__39 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeartGem _003C_003E4__this;

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
		public _003CPlayerStepForward_003Ed__39(int _003C_003E1__state)
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
			HeartGem heartGem = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				Player entity = heartGem.Scene.Tracker.GetEntity<Player>();
				if (entity != null && entity.CollideCheck<Solid>(entity.Position + new Vector2(12f, 1f)))
				{
					_003C_003E2__current = entity.DummyWalkToExact((int)entity.X + 10);
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_00b0;
			}
			case 2:
				_003C_003E1__state = -1;
				goto IL_00b0;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00b0:
				_003C_003E2__current = 0.2f;
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

	private const string FAKE_HEART_FLAG = "fake_heart";

	public static ParticleType P_BlueShine;

	public static ParticleType P_RedShine;

	public static ParticleType P_GoldShine;

	public static ParticleType P_FakeShine;

	public bool IsGhost;

	public const float GhostAlpha = 0.8f;

	public bool IsFake;

	private Sprite sprite;

	private Sprite white;

	private ParticleType shineParticle;

	public Wiggler ScaleWiggler;

	private Wiggler moveWiggler;

	private Vector2 moveWiggleDir;

	private BloomPoint bloom;

	private VertexLight light;

	private Poem poem;

	private BirdNPC bird;

	private float timer;

	private bool collected;

	private bool autoPulse = true;

	private float bounceSfxDelay;

	private bool removeCameraTriggers;

	private SoundEmitter sfx;

	private List<InvisibleBarrier> walls = new List<InvisibleBarrier>();

	private HoldableCollider holdableCollider;

	private EntityID entityID;

	private InvisibleBarrier fakeRightWall;

	public HeartGem(Vector2 position)
		: base(position)
	{
		Add(holdableCollider = new HoldableCollider(OnHoldable));
		Add(new MirrorReflection());
	}

	public HeartGem(EntityData data, Vector2 offset)
		: this(data.Position + offset)
	{
		removeCameraTriggers = data.Bool("removeCameraTriggers");
		IsFake = data.Bool("fake");
		entityID = new EntityID(data.Level.Name, data.ID);
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		AreaKey area = (base.Scene as Level).Session.Area;
		IsGhost = !IsFake && SaveData.Instance.Areas[area.ID].Modes[(int)area.Mode].HeartGem;
		string id = (IsFake ? "heartgem3" : ((!IsGhost) ? ("heartgem" + (int)area.Mode) : "heartGemGhost"));
		Add(sprite = GFX.SpriteBank.Create(id));
		sprite.Play("spin");
		sprite.OnLoop = delegate(string anim)
		{
			if (Visible && anim == "spin" && autoPulse)
			{
				if (IsFake)
				{
					Audio.Play("event:/new_content/game/10_farewell/fakeheart_pulse", Position);
				}
				else
				{
					Audio.Play("event:/game/general/crystalheart_pulse", Position);
				}
				ScaleWiggler.Start();
				(base.Scene as Level).Displacement.AddBurst(Position, 0.35f, 8f, 48f, 0.25f);
			}
		};
		if (IsGhost)
		{
			sprite.Color = Color.White * 0.8f;
		}
		base.Collider = new Hitbox(16f, 16f, -8f, -8f);
		Add(new PlayerCollider(OnPlayer));
		Add(ScaleWiggler = Wiggler.Create(0.5f, 4f, delegate(float f)
		{
			sprite.Scale = Vector2.One * (1f + f * 0.25f);
		}));
		Add(bloom = new BloomPoint(0.75f, 16f));
		Color value;
		if (IsFake)
		{
			value = Calc.HexToColor("dad8cc");
			shineParticle = P_FakeShine;
		}
		else if (area.Mode == AreaMode.Normal)
		{
			value = Color.Aqua;
			shineParticle = P_BlueShine;
		}
		else if (area.Mode == AreaMode.BSide)
		{
			value = Color.Red;
			shineParticle = P_RedShine;
		}
		else
		{
			value = Color.Gold;
			shineParticle = P_GoldShine;
		}
		value = Color.Lerp(value, Color.White, 0.5f);
		Add(light = new VertexLight(value, 1f, 32, 64));
		if (IsFake)
		{
			bloom.Alpha = 0f;
			light.Alpha = 0f;
		}
		moveWiggler = Wiggler.Create(0.8f, 2f);
		moveWiggler.StartZero = true;
		Add(moveWiggler);
		if (!IsFake)
		{
			return;
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if ((entity != null && entity.X > base.X) || (scene as Level).Session.GetFlag("fake_heart"))
		{
			Visible = false;
			Alarm.Set(this, 0.0001f, delegate
			{
				FakeRemoveCameraTrigger();
				RemoveSelf();
			});
		}
		else
		{
			scene.Add(fakeRightWall = new InvisibleBarrier(new Vector2(base.X + 160f, base.Y - 200f), 8f, 400f));
		}
	}

	public override void Update()
	{
		bounceSfxDelay -= Engine.DeltaTime;
		timer += Engine.DeltaTime;
		sprite.Position = Vector2.UnitY * (float)Math.Sin(timer * 2f) * 2f + moveWiggleDir * moveWiggler.Value * -8f;
		if (white != null)
		{
			white.Position = sprite.Position;
			white.Scale = sprite.Scale;
			if (white.CurrentAnimationID != sprite.CurrentAnimationID)
			{
				white.Play(sprite.CurrentAnimationID);
			}
			white.SetAnimationFrame(sprite.CurrentAnimationFrame);
		}
		if (collected)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity == null || entity.Dead)
			{
				EndCutscene();
			}
		}
		base.Update();
		if (!collected && base.Scene.OnInterval(0.1f))
		{
			SceneAs<Level>().Particles.Emit(shineParticle, 1, base.Center, Vector2.One * 8f);
		}
	}

	public void OnHoldable(Holdable h)
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (!collected && entity != null && h.Dangerous(holdableCollider))
		{
			Collect(entity);
		}
	}

	public void OnPlayer(Player player)
	{
		if (collected || (base.Scene as Level).Frozen)
		{
			return;
		}
		if (player.DashAttacking)
		{
			Collect(player);
			return;
		}
		if (bounceSfxDelay <= 0f)
		{
			if (IsFake)
			{
				Audio.Play("event:/new_content/game/10_farewell/fakeheart_bounce", Position);
			}
			else
			{
				Audio.Play("event:/game/general/crystalheart_bounce", Position);
			}
			bounceSfxDelay = 0.1f;
		}
		player.PointBounce(base.Center);
		moveWiggler.Start();
		ScaleWiggler.Start();
		moveWiggleDir = (base.Center - player.Center).SafeNormalize(Vector2.UnitY);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
	}

	private void Collect(Player player)
	{
		base.Scene.Tracker.GetEntity<AngryOshiro>()?.StopControllingTime();
		Coroutine coroutine = new Coroutine(CollectRoutine(player));
		coroutine.UseRawDeltaTime = true;
		Add(coroutine);
		collected = true;
		if (!removeCameraTriggers)
		{
			return;
		}
		foreach (CameraOffsetTrigger item in base.Scene.Entities.FindAll<CameraOffsetTrigger>())
		{
			item.RemoveSelf();
		}
	}

	[IteratorStateMachine(typeof(_003CCollectRoutine_003Ed__35))]
	private IEnumerator CollectRoutine(Player player)
	{
		Level level = base.Scene as Level;
		AreaKey area = level.Session.Area;
		string poemID = AreaData.Get(level).Mode[(int)area.Mode].PoemID;
		bool completeArea = !IsFake && (area.Mode != AreaMode.Normal || area.ID == 9);
		if (IsFake)
		{
			level.StartCutscene(SkipFakeHeartCutscene);
		}
		else
		{
			level.CanRetry = false;
		}
		if (completeArea || IsFake)
		{
			Audio.SetMusic(null);
			Audio.SetAmbience(null);
		}
		if (completeArea)
		{
			List<Strawberry> list = new List<Strawberry>();
			foreach (Follower follower in player.Leader.Followers)
			{
				if (follower.Entity is Strawberry)
				{
					list.Add(follower.Entity as Strawberry);
				}
			}
			foreach (Strawberry item in list)
			{
				item.OnCollect();
			}
		}
		string text = "event:/game/general/crystalheart_blue_get";
		if (IsFake)
		{
			text = "event:/new_content/game/10_farewell/fakeheart_get";
		}
		else if (area.Mode == AreaMode.BSide)
		{
			text = "event:/game/general/crystalheart_red_get";
		}
		else if (area.Mode == AreaMode.CSide)
		{
			text = "event:/game/general/crystalheart_gold_get";
		}
		sfx = SoundEmitter.Play(text, this);
		Add(new LevelEndingHook(delegate
		{
			sfx.Source.Stop();
		}));
		walls.Add(new InvisibleBarrier(new Vector2(level.Bounds.Right, level.Bounds.Top), 8f, level.Bounds.Height));
		walls.Add(new InvisibleBarrier(new Vector2(level.Bounds.Left - 8, level.Bounds.Top), 8f, level.Bounds.Height));
		walls.Add(new InvisibleBarrier(new Vector2(level.Bounds.Left, level.Bounds.Top - 8), level.Bounds.Width, 8f));
		foreach (InvisibleBarrier wall in walls)
		{
			base.Scene.Add(wall);
		}
		Add(white = GFX.SpriteBank.Create("heartGemWhite"));
		base.Depth = -2000000;
		yield return null;
		Celeste.Freeze(0.2f);
		yield return null;
		Engine.TimeRate = 0.5f;
		player.Depth = -2000000;
		for (int num = 0; num < 10; num++)
		{
			base.Scene.Add(new AbsorbOrb(Position));
		}
		level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		level.Flash(Color.White);
		level.FormationBackdrop.Display = true;
		level.FormationBackdrop.Alpha = 1f;
		light.Alpha = (bloom.Alpha = 0f);
		Visible = false;
		for (float t = 0f; t < 2f; t += Engine.RawDeltaTime)
		{
			Engine.TimeRate = Calc.Approach(Engine.TimeRate, 0f, Engine.RawDeltaTime * 0.25f);
			yield return null;
		}
		yield return null;
		if (player.Dead)
		{
			yield return 100f;
		}
		Engine.TimeRate = 1f;
		base.Tag = Tags.FrozenUpdate;
		level.Frozen = true;
		if (!IsFake)
		{
			RegisterAsCollected(level, poemID);
			if (completeArea)
			{
				level.TimerStopped = true;
				level.RegisterAreaComplete();
			}
		}
		string text2 = null;
		if (!string.IsNullOrEmpty(poemID))
		{
			text2 = Dialog.Clean("poem_" + poemID);
		}
		poem = new Poem(text2, (int)(IsFake ? ((AreaMode)3) : area.Mode), (area.Mode == AreaMode.CSide || IsFake) ? 1f : 0.6f);
		poem.Alpha = 0f;
		base.Scene.Add(poem);
		for (float t = 0f; t < 1f; t += Engine.RawDeltaTime)
		{
			poem.Alpha = Ease.CubeOut(t);
			yield return null;
		}
		if (IsFake)
		{
			yield return DoFakeRoutineWithBird(player);
			yield break;
		}
		while (!Input.MenuConfirm.Pressed && !Input.MenuCancel.Pressed)
		{
			yield return null;
		}
		sfx.Source.Param("end", 1f);
		if (!completeArea)
		{
			level.FormationBackdrop.Display = false;
			for (float t = 0f; t < 1f; t += Engine.RawDeltaTime * 2f)
			{
				poem.Alpha = Ease.CubeIn(1f - t);
				yield return null;
			}
			player.Depth = 0;
			EndCutscene();
		}
		else
		{
			FadeWipe fadeWipe = new FadeWipe(level, wipeIn: false);
			fadeWipe.Duration = 3.25f;
			yield return fadeWipe.Duration;
			level.CompleteArea(spotlightWipe: false, skipScreenWipe: true);
		}
	}

	private void EndCutscene()
	{
		Level obj = base.Scene as Level;
		obj.Frozen = false;
		obj.CanRetry = true;
		obj.FormationBackdrop.Display = false;
		Engine.TimeRate = 1f;
		if (poem != null)
		{
			poem.RemoveSelf();
		}
		foreach (InvisibleBarrier wall in walls)
		{
			wall.RemoveSelf();
		}
		RemoveSelf();
	}

	private void RegisterAsCollected(Level level, string poemID)
	{
		level.Session.HeartGem = true;
		level.Session.UpdateLevelStartDashes();
		int unlockedModes = SaveData.Instance.UnlockedModes;
		SaveData.Instance.RegisterHeartGem(level.Session.Area);
		if (!string.IsNullOrEmpty(poemID))
		{
			SaveData.Instance.RegisterPoemEntry(poemID);
		}
		if (unlockedModes < 3 && SaveData.Instance.UnlockedModes >= 3)
		{
			level.Session.UnlockedCSide = true;
		}
		if (SaveData.Instance.TotalHeartGems >= 24)
		{
			Achievements.Register(Achievement.CSIDES);
		}
	}

	[IteratorStateMachine(typeof(_003CDoFakeRoutineWithBird_003Ed__38))]
	private IEnumerator DoFakeRoutineWithBird(Player player)
	{
		Level level = base.Scene as Level;
		int panAmount = 64;
		Vector2 panFrom = level.Camera.Position;
		Vector2 panTo = level.Camera.Position + new Vector2(-panAmount, 0f);
		Vector2 birdFrom = new Vector2(panTo.X - 16f, player.Y - 20f);
		Vector2 birdTo = new Vector2(panFrom.X + 320f + 16f, player.Y - 20f);
		yield return 2f;
		Glitch.Value = 0.75f;
		while (Glitch.Value > 0f)
		{
			Glitch.Value = Calc.Approach(Glitch.Value, 0f, Engine.RawDeltaTime * 4f);
			level.Shake();
			yield return null;
		}
		yield return 1.1f;
		Glitch.Value = 0.75f;
		while (Glitch.Value > 0f)
		{
			Glitch.Value = Calc.Approach(Glitch.Value, 0f, Engine.RawDeltaTime * 4f);
			level.Shake();
			yield return null;
		}
		yield return 0.4f;
		for (float p = 0f; p < 1f; p += Engine.RawDeltaTime / 2f)
		{
			level.Camera.Position = panFrom + (panTo - panFrom) * Ease.CubeInOut(p);
			poem.Offset = new Vector2(panAmount * 8, 0f) * Ease.CubeInOut(p);
			yield return null;
		}
		bird = new BirdNPC(birdFrom, BirdNPC.Modes.None);
		bird.Sprite.Play("fly");
		bird.Sprite.UseRawDeltaTime = true;
		bird.Facing = Facings.Right;
		bird.Depth = -2000100;
		bird.Tag = Tags.FrozenUpdate;
		bird.Add(new VertexLight(Color.White, 0.5f, 8, 32));
		bird.Add(new BloomPoint(0.5f, 12f));
		level.Add(bird);
		for (float p = 0f; p < 1f; p += Engine.RawDeltaTime / 2.6f)
		{
			level.Camera.Position = panTo + (panFrom - panTo) * Ease.CubeInOut(p);
			poem.Offset = new Vector2(panAmount * 8, 0f) * Ease.CubeInOut(1f - p);
			float num = 0.1f;
			float num2 = 0.9f;
			if (p > num && p <= num2)
			{
				float num3 = (p - num) / (num2 - num);
				bird.Position = birdFrom + (birdTo - birdFrom) * num3 + Vector2.UnitY * (float)Math.Sin(num3 * 8f) * 8f;
			}
			if (level.OnRawInterval(0.2f))
			{
				TrailManager.Add(bird, Calc.HexToColor("639bff"), 1f, frozenUpdate: true, useRawDeltaTime: true);
			}
			yield return null;
		}
		bird.RemoveSelf();
		bird = null;
		Engine.TimeRate = 0f;
		level.Frozen = false;
		player.Active = false;
		player.StateMachine.State = 11;
		while (Engine.TimeRate != 1f)
		{
			Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 0.5f * Engine.RawDeltaTime);
			yield return null;
		}
		Engine.TimeRate = 1f;
		yield return Textbox.Say("CH9_FAKE_HEART");
		sfx.Source.Param("end", 1f);
		yield return 0.283f;
		level.FormationBackdrop.Display = false;
		for (float p = 0f; p < 1f; p += Engine.RawDeltaTime / 0.2f)
		{
			poem.TextAlpha = Ease.CubeIn(1f - p);
			poem.ParticleSpeed = poem.TextAlpha;
			yield return null;
		}
		poem.Heart.Play("break");
		while (poem.Heart.Animating)
		{
			poem.Shake += Engine.DeltaTime;
			yield return null;
		}
		poem.RemoveSelf();
		poem = null;
		for (int i = 0; i < 10; i++)
		{
			Vector2 position = level.Camera.Position + new Vector2(320f, 180f) * 0.5f;
			Vector2 value = level.Camera.Position + new Vector2(160f, -64f);
			base.Scene.Add(new AbsorbOrb(position, null, value));
		}
		level.Shake();
		Glitch.Value = 0.8f;
		while (Glitch.Value > 0f)
		{
			Glitch.Value -= Engine.DeltaTime * 4f;
			yield return null;
		}
		yield return 0.25f;
		level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/intermission_heartgroove";
		level.Session.Audio.Apply();
		player.Active = true;
		player.Depth = 0;
		player.StateMachine.State = 11;
		while (!player.OnGround() && player.Bottom < (float)level.Bounds.Bottom)
		{
			yield return null;
		}
		player.Facing = Facings.Right;
		yield return 0.5f;
		yield return Textbox.Say("CH9_KEEP_GOING", PlayerStepForward);
		SkipFakeHeartCutscene(level);
		level.EndCutscene();
	}

	[IteratorStateMachine(typeof(_003CPlayerStepForward_003Ed__39))]
	private IEnumerator PlayerStepForward()
	{
		yield return 0.1f;
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null && entity.CollideCheck<Solid>(entity.Position + new Vector2(12f, 1f)))
		{
			yield return entity.DummyWalkToExact((int)entity.X + 10);
		}
		yield return 0.2f;
	}

	private void SkipFakeHeartCutscene(Level level)
	{
		Engine.TimeRate = 1f;
		Glitch.Value = 0f;
		if (sfx != null)
		{
			sfx.Source.Stop();
		}
		level.Session.SetFlag("fake_heart");
		level.Frozen = false;
		level.FormationBackdrop.Display = false;
		level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/intermission_heartgroove";
		level.Session.Audio.Apply();
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.Sprite.Play("idle");
			entity.Active = true;
			entity.StateMachine.State = 0;
			entity.Dashes = 1;
			entity.Speed = Vector2.Zero;
			entity.MoveV(200f);
			entity.Depth = 0;
			for (int i = 0; i < 10; i++)
			{
				entity.UpdateHair(applyGravity: true);
			}
		}
		foreach (AbsorbOrb item in base.Scene.Entities.FindAll<AbsorbOrb>())
		{
			item.RemoveSelf();
		}
		if (poem != null)
		{
			poem.RemoveSelf();
		}
		if (bird != null)
		{
			bird.RemoveSelf();
		}
		if (fakeRightWall != null)
		{
			fakeRightWall.RemoveSelf();
		}
		FakeRemoveCameraTrigger();
		foreach (InvisibleBarrier wall in walls)
		{
			wall.RemoveSelf();
		}
		RemoveSelf();
	}

	private void FakeRemoveCameraTrigger()
	{
		CameraTargetTrigger cameraTargetTrigger = CollideFirst<CameraTargetTrigger>();
		if (cameraTargetTrigger != null)
		{
			cameraTargetTrigger.LerpStrength = 0f;
		}
	}
}
