using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class FinalBoss : Entity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass42_0
	{
		public FinalBoss _003C_003E4__this;

		public bool lastHit;

		internal void _003CMoveSequence_003Eb__4(Tween t)
		{
			_003C_003E4__this.Sprite.Play("recoverHit");
			_003C_003E4__this.Moving = false;
			_003C_003E4__this.Collidable = true;
			Player entity = _003C_003E4__this.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				_003C_003E4__this.facing = Math.Sign(entity.X - _003C_003E4__this.X);
				if (_003C_003E4__this.facing == 0)
				{
					_003C_003E4__this.facing = -1;
				}
			}
			_003C_003E4__this.StartAttacking();
			_003C_003E4__this.floatSine.Reset();
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass42_1
	{
		public float from;

		public _003C_003Ec__DisplayClass42_0 CS_0024_003C_003E8__locals1;

		internal void _003CMoveSequence_003Eb__2(Tween t)
		{
			if (CS_0024_003C_003E8__locals1._003C_003E4__this.bossBg != null && CS_0024_003C_003E8__locals1._003C_003E4__this.bossBg.Alpha < t.Eased)
			{
				CS_0024_003C_003E8__locals1._003C_003E4__this.bossBg.Alpha = t.Eased;
			}
			Engine.TimeRate = MathHelper.Lerp(from, 1f, t.Eased);
			if (CS_0024_003C_003E8__locals1.lastHit)
			{
				Glitch.Value = 0.6f * (1f - t.Eased);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass42_2
	{
		public Vector2 from;

		public Vector2 to;

		public float dir;

		public _003C_003Ec__DisplayClass42_0 CS_0024_003C_003E8__locals2;

		internal void _003CMoveSequence_003Eb__3(Tween t)
		{
			CS_0024_003C_003E8__locals2._003C_003E4__this.Position = Vector2.Lerp(from, to, t.Eased);
			if (t.Eased >= 0.1f && t.Eased <= 0.9f && CS_0024_003C_003E8__locals2._003C_003E4__this.Scene.OnInterval(0.02f))
			{
				TrailManager.Add(CS_0024_003C_003E8__locals2._003C_003E4__this, Player.NormalHairColor, 0.5f);
				CS_0024_003C_003E8__locals2._003C_003E4__this.level.Particles.Emit(Player.P_DashB, 2, CS_0024_003C_003E8__locals2._003C_003E4__this.Center, Vector2.One * 3f, dir);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CMoveSequence_003Ed__42 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

		public bool lastHit;

		public Player player;

		private _003C_003Ec__DisplayClass42_0 _003C_003E8__1;

		private float _003Ctimer_003E5__2;

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
		public _003CMoveSequence_003Ed__42(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass42_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.lastHit = lastHit;
				if (_003C_003E8__1.lastHit)
				{
					Audio.SetMusicParam("boss_pitch", 1f);
					Tween tween3 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.3f, start: true);
					tween3.OnUpdate = delegate(Tween t)
					{
						Glitch.Value = 0.6f * t.Eased;
					};
					finalBoss.Add(tween3);
				}
				else
				{
					Tween tween4 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.3f, start: true);
					tween4.OnUpdate = delegate(Tween t)
					{
						Glitch.Value = 0.5f * (1f - t.Eased);
					};
					finalBoss.Add(tween4);
				}
				if (player != null && !player.Dead)
				{
					player.StartAttract(finalBoss.Center + Vector2.UnitY * 4f);
				}
				_003Ctimer_003E5__2 = 0.15f;
				goto IL_0164;
			case 1:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__2 -= Engine.DeltaTime;
				goto IL_0164;
			case 2:
				_003C_003E1__state = -1;
				goto IL_01b4;
			case 3:
			{
				_003C_003E1__state = -1;
				for (float num2 = 0f; num2 < (float)Math.PI * 2f; num2 += 0.17453292f)
				{
					Vector2 position = finalBoss.Center + finalBoss.Sprite.Position + Calc.AngleToVector(num2 + Calc.Random.Range(-(float)Math.PI / 90f, (float)Math.PI / 90f), Calc.Random.Range(16, 20));
					finalBoss.level.Particles.Emit(P_Burst, position, num2);
				}
				_003C_003E2__current = 0.05f;
				_003C_003E1__state = 4;
				return true;
			}
			case 4:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass42_1 CS_0024_003C_003E8__locals30 = new _003C_003Ec__DisplayClass42_1
				{
					CS_0024_003C_003E8__locals1 = _003C_003E8__1
				};
				Audio.SetMusicParam("boss_pitch", 0f);
				CS_0024_003C_003E8__locals30.from = Engine.TimeRate;
				Tween tween2 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.35f / Engine.TimeRateB, start: true);
				tween2.UseRawDeltaTime = true;
				tween2.OnUpdate = delegate(Tween t)
				{
					if (CS_0024_003C_003E8__locals30.CS_0024_003C_003E8__locals1._003C_003E4__this.bossBg != null && CS_0024_003C_003E8__locals30.CS_0024_003C_003E8__locals1._003C_003E4__this.bossBg.Alpha < t.Eased)
					{
						CS_0024_003C_003E8__locals30.CS_0024_003C_003E8__locals1._003C_003E4__this.bossBg.Alpha = t.Eased;
					}
					Engine.TimeRate = MathHelper.Lerp(CS_0024_003C_003E8__locals30.from, 1f, t.Eased);
					if (CS_0024_003C_003E8__locals30.CS_0024_003C_003E8__locals1.lastHit)
					{
						Glitch.Value = 0.6f * (1f - t.Eased);
					}
				};
				finalBoss.Add(tween2);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 5;
				return true;
			}
			case 5:
				{
					_003C_003E1__state = -1;
					_003C_003Ec__DisplayClass42_2 CS_0024_003C_003E8__locals25 = new _003C_003Ec__DisplayClass42_2
					{
						CS_0024_003C_003E8__locals2 = _003C_003E8__1,
						from = finalBoss.Position,
						to = finalBoss.nodes[finalBoss.nodeIndex]
					};
					float duration = Vector2.Distance(CS_0024_003C_003E8__locals25.from, CS_0024_003C_003E8__locals25.to) / 600f;
					CS_0024_003C_003E8__locals25.dir = (CS_0024_003C_003E8__locals25.to - CS_0024_003C_003E8__locals25.from).Angle();
					Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, duration, start: true);
					tween.OnUpdate = delegate(Tween t)
					{
						CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Position = Vector2.Lerp(CS_0024_003C_003E8__locals25.from, CS_0024_003C_003E8__locals25.to, t.Eased);
						if (t.Eased >= 0.1f && t.Eased <= 0.9f && CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Scene.OnInterval(0.02f))
						{
							TrailManager.Add(CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this, Player.NormalHairColor, 0.5f);
							CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.level.Particles.Emit(Player.P_DashB, 2, CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Center, Vector2.One * 3f, CS_0024_003C_003E8__locals25.dir);
						}
					};
					tween.OnComplete = delegate
					{
						CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Sprite.Play("recoverHit");
						CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Moving = false;
						CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Collidable = true;
						Player entity = CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.Scene.Tracker.GetEntity<Player>();
						if (entity != null)
						{
							CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.facing = Math.Sign(entity.X - CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.X);
							if (CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.facing == 0)
							{
								CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.facing = -1;
							}
						}
						CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.StartAttacking();
						CS_0024_003C_003E8__locals25.CS_0024_003C_003E8__locals2._003C_003E4__this.floatSine.Reset();
					};
					finalBoss.Add(tween);
					return false;
				}
				IL_01b4:
				foreach (ReflectionTentacles entity2 in finalBoss.Scene.Tracker.GetEntities<ReflectionTentacles>())
				{
					entity2.Retreat();
				}
				if (player != null)
				{
					Celeste.Freeze(0.1f);
					if (_003C_003E8__1.lastHit)
					{
						Engine.TimeRate = 0.5f;
					}
					else
					{
						Engine.TimeRate = 0.75f;
					}
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				}
				finalBoss.PushPlayer(player);
				finalBoss.level.Shake();
				_003C_003E2__current = 0.05f;
				_003C_003E1__state = 3;
				return true;
				IL_0164:
				if (player != null && !player.Dead && !player.AtAttractTarget)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (_003Ctimer_003E5__2 > 0f)
				{
					_003C_003E2__current = _003Ctimer_003E5__2;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_01b4;
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
	private sealed class _003CAttack01Sequence_003Ed__49 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack01Sequence_003Ed__49(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				goto IL_0037;
			case 1:
				_003C_003E1__state = -1;
				finalBoss.Shoot();
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.15f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_0037;
				}
				IL_0037:
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack02Sequence_003Ed__50 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack02Sequence_003Ed__50(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0039;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				finalBoss.Shoot();
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					goto IL_0039;
				}
				IL_0039:
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack03Sequence_003Ed__51 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

		private int _003Ci_003E5__2;

		private Vector2 _003Cat_003E5__3;

		private int _003Cj_003E5__4;

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
		public _003CAttack03Sequence_003Ed__51(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005b;
			case 2:
				_003C_003E1__state = -1;
				_003Cj_003E5__4++;
				goto IL_00cc;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0110;
			case 4:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					goto IL_005b;
				}
				IL_005b:
				_003Ci_003E5__2 = 0;
				goto IL_0120;
				IL_0120:
				if (_003Ci_003E5__2 < 5)
				{
					Player entity = finalBoss.level.Tracker.GetEntity<Player>();
					if (entity != null)
					{
						_003Cat_003E5__3 = entity.Center;
						_003Cj_003E5__4 = 0;
						goto IL_00cc;
					}
					goto IL_00e1;
				}
				_003C_003E2__current = 2f;
				_003C_003E1__state = 4;
				return true;
				IL_00e1:
				if (_003Ci_003E5__2 < 4)
				{
					finalBoss.StartShootCharge();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_0110;
				IL_00cc:
				if (_003Cj_003E5__4 < 2)
				{
					finalBoss.ShootAt(_003Cat_003E5__3);
					_003C_003E2__current = 0.15f;
					_003C_003E1__state = 2;
					return true;
				}
				_003Cat_003E5__3 = default(Vector2);
				goto IL_00e1;
				IL_0110:
				_003Ci_003E5__2++;
				goto IL_0120;
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
	private sealed class _003CAttack04Sequence_003Ed__52 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

		private int _003Ci_003E5__2;

		private Vector2 _003Cat_003E5__3;

		private int _003Cj_003E5__4;

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
		public _003CAttack04Sequence_003Ed__52(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005f;
			case 2:
				_003C_003E1__state = -1;
				_003Cj_003E5__4++;
				goto IL_00d0;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0114;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					finalBoss.StartShootCharge();
					goto IL_005f;
				}
				IL_005f:
				_003Ci_003E5__2 = 0;
				goto IL_0124;
				IL_0124:
				if (_003Ci_003E5__2 < 5)
				{
					Player entity = finalBoss.level.Tracker.GetEntity<Player>();
					if (entity != null)
					{
						_003Cat_003E5__3 = entity.Center;
						_003Cj_003E5__4 = 0;
						goto IL_00d0;
					}
					goto IL_00e5;
				}
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 4;
				return true;
				IL_00e5:
				if (_003Ci_003E5__2 < 4)
				{
					finalBoss.StartShootCharge();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_0114;
				IL_00d0:
				if (_003Cj_003E5__4 < 2)
				{
					finalBoss.ShootAt(_003Cat_003E5__3);
					_003C_003E2__current = 0.15f;
					_003C_003E1__state = 2;
					return true;
				}
				_003Cat_003E5__3 = default(Vector2);
				goto IL_00e5;
				IL_0114:
				_003Ci_003E5__2++;
				goto IL_0124;
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
	private sealed class _003CAttack05Sequence_003Ed__53 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

		private int _003Ci_003E5__2;

		private Vector2 _003Cat_003E5__3;

		private int _003Cj_003E5__4;

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
		public _003CAttack05Sequence_003Ed__53(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005d;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003Ci_003E5__2 = 0;
				goto IL_0184;
			case 5:
				_003C_003E1__state = -1;
				_003Cj_003E5__4++;
				goto IL_0130;
			case 6:
				_003C_003E1__state = -1;
				goto IL_0174;
			case 7:
				{
					_003C_003E1__state = -1;
					goto IL_005d;
				}
				IL_0184:
				if (_003Ci_003E5__2 < 3)
				{
					Player entity = finalBoss.level.Tracker.GetEntity<Player>();
					if (entity != null)
					{
						_003Cat_003E5__3 = entity.Center;
						_003Cj_003E5__4 = 0;
						goto IL_0130;
					}
					goto IL_0145;
				}
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 7;
				return true;
				IL_0130:
				if (_003Cj_003E5__4 < 2)
				{
					finalBoss.ShootAt(_003Cat_003E5__3);
					_003C_003E2__current = 0.15f;
					_003C_003E1__state = 5;
					return true;
				}
				_003Cat_003E5__3 = default(Vector2);
				goto IL_0145;
				IL_005d:
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 2;
				return true;
				IL_0145:
				if (_003Ci_003E5__2 < 2)
				{
					finalBoss.StartShootCharge();
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 6;
					return true;
				}
				goto IL_0174;
				IL_0174:
				_003Ci_003E5__2++;
				goto IL_0184;
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
	private sealed class _003CAttack06Sequence_003Ed__54 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack06Sequence_003Ed__54(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack07Sequence_003Ed__55 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack07Sequence_003Ed__55(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				finalBoss.Shoot();
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack08Sequence_003Ed__56 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack08Sequence_003Ed__56(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_002d;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					goto IL_002d;
				}
				IL_002d:
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack09Sequence_003Ed__57 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack09Sequence_003Ed__57(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				goto IL_0037;
			case 1:
				_003C_003E1__state = -1;
				finalBoss.Shoot();
				_003C_003E2__current = 0.15f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				finalBoss.Shoot();
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_0037;
				}
				IL_0037:
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack10Sequence_003Ed__58 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CAttack10Sequence_003Ed__58(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			if (_003C_003E1__state != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
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
	private sealed class _003CAttack11Sequence_003Ed__59 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack11Sequence_003Ed__59(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (finalBoss.nodeIndex == 0)
				{
					finalBoss.StartShootCharge();
					_003C_003E2__current = 0.6f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_005b;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005b;
			case 2:
				_003C_003E1__state = -1;
				finalBoss.StartShootCharge();
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					goto IL_005b;
				}
				IL_005b:
				finalBoss.Shoot();
				_003C_003E2__current = 1.9f;
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
	private sealed class _003CAttack13Sequence_003Ed__60 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack13Sequence_003Ed__60(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (finalBoss.nodeIndex == 0)
				{
					return false;
				}
				_003C_003E2__current = finalBoss.Attack01Sequence();
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
	private sealed class _003CAttack14Sequence_003Ed__61 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack14Sequence_003Ed__61(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_002d;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					goto IL_002d;
				}
				IL_002d:
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
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
	private sealed class _003CAttack15Sequence_003Ed__62 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CAttack15Sequence_003Ed__62(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_002d;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = finalBoss.Beam();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					goto IL_002d;
				}
				IL_002d:
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
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
	private sealed class _003CBeam_003Ed__65 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBoss _003C_003E4__this;

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
		public _003CBeam_003Ed__65(int _003C_003E1__state)
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
			FinalBoss finalBoss = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				finalBoss.laserSfx.Play("event:/char/badeline/boss_laser_charge");
				finalBoss.Sprite.Play("attack2Begin", restart: true);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				Player entity = finalBoss.level.Tracker.GetEntity<Player>();
				if (entity != null)
				{
					finalBoss.level.Add(Engine.Pooler.Create<FinalBossBeam>().Init(finalBoss, entity));
				}
				_003C_003E2__current = 0.9f;
				_003C_003E1__state = 2;
				return true;
			}
			case 2:
				_003C_003E1__state = -1;
				finalBoss.Sprite.Play("attack2Lock", restart: true);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				finalBoss.laserSfx.Stop();
				Audio.Play("event:/char/badeline/boss_laser_fire", finalBoss.Position);
				finalBoss.Sprite.Play("attack2Recoil");
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

	public static ParticleType P_Burst;

	public const float CameraXPastMax = 140f;

	private const float MoveSpeed = 600f;

	private const float AvoidRadius = 12f;

	public Sprite Sprite;

	public PlayerSprite NormalSprite;

	private PlayerHair normalHair;

	private Vector2 avoidPos;

	public float CameraYPastMax;

	public bool Moving;

	public bool Sitting;

	private int facing;

	private Level level;

	private Circle circle;

	private Vector2[] nodes;

	private int nodeIndex;

	private int patternIndex;

	private Coroutine attackCoroutine;

	private Coroutine triggerBlocksCoroutine;

	private List<Entity> fallingBlocks;

	private List<Entity> movingBlocks;

	private bool playerHasMoved;

	private SineWave floatSine;

	private bool dialog;

	private bool startHit;

	private VertexLight light;

	private Wiggler scaleWiggler;

	private FinalBossStarfield bossBg;

	private SoundSource chargeSfx;

	private SoundSource laserSfx;

	public Vector2 BeamOrigin => base.Center + Sprite.Position + new Vector2(0f, -14f);

	public Vector2 ShotOrigin => base.Center + Sprite.Position + new Vector2(6f * Sprite.Scale.X, 2f);

	public FinalBoss(Vector2 position, Vector2[] nodes, int patternIndex, float cameraYPastMax, bool dialog, bool startHit, bool cameraLockY)
		: base(position)
	{
		this.patternIndex = patternIndex;
		CameraYPastMax = cameraYPastMax;
		this.dialog = dialog;
		this.startHit = startHit;
		Add(light = new VertexLight(Color.White, 1f, 32, 64));
		base.Collider = (circle = new Circle(14f, 0f, -6f));
		Add(new PlayerCollider(OnPlayer));
		this.nodes = new Vector2[nodes.Length + 1];
		this.nodes[0] = Position;
		for (int i = 0; i < nodes.Length; i++)
		{
			this.nodes[i + 1] = nodes[i];
		}
		attackCoroutine = new Coroutine(removeOnComplete: false);
		Add(attackCoroutine);
		triggerBlocksCoroutine = new Coroutine(removeOnComplete: false);
		Add(triggerBlocksCoroutine);
		Add(new CameraLocker(cameraLockY ? Level.CameraLockModes.FinalBoss : Level.CameraLockModes.FinalBossNoY, 140f, cameraYPastMax));
		Add(floatSine = new SineWave(0.6f));
		Add(scaleWiggler = Wiggler.Create(0.6f, 3f));
		Add(chargeSfx = new SoundSource());
		Add(laserSfx = new SoundSource());
	}

	public FinalBoss(EntityData e, Vector2 offset)
		: this(e.Position + offset, e.NodesOffset(offset), e.Int("patternIndex"), e.Float("cameraPastY", 120f), e.Bool("dialog"), e.Bool("startHit"), e.Bool("cameraLockY", defaultValue: true))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = SceneAs<Level>();
		if (patternIndex == 0)
		{
			NormalSprite = new PlayerSprite(PlayerSpriteMode.Badeline);
			NormalSprite.Scale.X = -1f;
			NormalSprite.Play("laugh");
			normalHair = new PlayerHair(NormalSprite);
			normalHair.Color = BadelineOldsite.HairColor;
			normalHair.Border = Color.Black;
			normalHair.Facing = Facings.Left;
			Add(normalHair);
			Add(NormalSprite);
		}
		else
		{
			CreateBossSprite();
		}
		bossBg = level.Background.Get<FinalBossStarfield>();
		if (patternIndex == 0 && !level.Session.GetFlag("boss_intro") && level.Session.Level.Equals("boss-00"))
		{
			level.Session.Audio.Music.Event = "event:/music/lvl2/phone_loop";
			level.Session.Audio.Apply();
			if (bossBg != null)
			{
				bossBg.Alpha = 0f;
			}
			Sitting = true;
			Position.Y += 16f;
			NormalSprite.Play("pretendDead");
			NormalSprite.Scale.X = 1f;
		}
		else if (patternIndex == 0 && !level.Session.GetFlag("boss_mid") && level.Session.Level.Equals("boss-14"))
		{
			level.Add(new CS06_BossMid());
		}
		else if (startHit)
		{
			Alarm.Set(this, 0.5f, delegate
			{
				OnPlayer(null);
			});
		}
		light.Position = ((Sprite != null) ? Sprite : NormalSprite).Position + new Vector2(0f, -10f);
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		fallingBlocks = base.Scene.Tracker.GetEntitiesCopy<FallingBlock>();
		fallingBlocks.Sort((Entity a, Entity b) => (int)(a.X - b.X));
		movingBlocks = base.Scene.Tracker.GetEntitiesCopy<FinalBossMovingBlock>();
		movingBlocks.Sort((Entity a, Entity b) => (int)(a.X - b.X));
	}

	private void CreateBossSprite()
	{
		Add(Sprite = GFX.SpriteBank.Create("badeline_boss"));
		Sprite.OnFrameChange = delegate(string anim)
		{
			if (anim == "idle" && Sprite.CurrentAnimationFrame == 18)
			{
				Audio.Play("event:/char/badeline/boss_idle_air", Position);
			}
		};
		facing = -1;
		if (NormalSprite != null)
		{
			Sprite.Position = NormalSprite.Position;
			Remove(NormalSprite);
		}
		if (normalHair != null)
		{
			Remove(normalHair);
		}
		NormalSprite = null;
		normalHair = null;
	}

	public override void Update()
	{
		base.Update();
		Sprite sprite = ((Sprite != null) ? Sprite : NormalSprite);
		if (!Sitting)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (!Moving && entity != null)
			{
				if (facing == -1 && entity.X > base.X + 20f)
				{
					facing = 1;
					scaleWiggler.Start();
				}
				else if (facing == 1 && entity.X < base.X - 20f)
				{
					facing = -1;
					scaleWiggler.Start();
				}
			}
			if (!playerHasMoved && entity != null && entity.Speed != Vector2.Zero)
			{
				playerHasMoved = true;
				if (patternIndex != 0)
				{
					StartAttacking();
				}
				TriggerMovingBlocks(0);
			}
			if (!Moving)
			{
				sprite.Position = avoidPos + new Vector2(floatSine.Value * 3f, floatSine.ValueOverTwo * 4f);
			}
			else
			{
				sprite.Position = Calc.Approach(sprite.Position, Vector2.Zero, 12f * Engine.DeltaTime);
			}
			float radius = circle.Radius;
			circle.Radius = 6f;
			CollideFirst<DashBlock>()?.Break(base.Center, -Vector2.UnitY);
			circle.Radius = radius;
			if (!level.IsInBounds(Position, 24f))
			{
				Active = (Visible = (Collidable = false));
				return;
			}
			Vector2 target;
			if (!Moving && entity != null)
			{
				float val = (base.Center - entity.Center).Length();
				val = Calc.ClampedMap(val, 32f, 88f, 12f, 0f);
				target = ((!(val <= 0f)) ? (base.Center - entity.Center).SafeNormalize(val) : Vector2.Zero);
			}
			else
			{
				target = Vector2.Zero;
			}
			avoidPos = Calc.Approach(avoidPos, target, 40f * Engine.DeltaTime);
		}
		light.Position = sprite.Position + new Vector2(0f, -10f);
	}

	public override void Render()
	{
		if (Sprite != null)
		{
			Sprite.Scale.X = facing;
			Sprite.Scale.Y = 1f;
			Sprite.Scale *= 1f + scaleWiggler.Value * 0.2f;
		}
		if (NormalSprite != null)
		{
			Vector2 position = NormalSprite.Position;
			NormalSprite.Position = NormalSprite.Position.FloorV2();
			base.Render();
			NormalSprite.Position = position;
		}
		else
		{
			base.Render();
		}
	}

	public void OnPlayer(Player player)
	{
		if (Sprite == null)
		{
			CreateBossSprite();
		}
		Sprite.Play("getHit");
		Audio.Play("event:/char/badeline/boss_hug", Position);
		chargeSfx.Stop();
		if (laserSfx.EventName == "event:/char/badeline/boss_laser_charge" && laserSfx.Playing)
		{
			laserSfx.Stop();
		}
		Collidable = false;
		avoidPos = Vector2.Zero;
		nodeIndex++;
		if (dialog)
		{
			if (nodeIndex == 1)
			{
				base.Scene.Add(new MiniTextbox("ch6_boss_tired_a"));
			}
			else if (nodeIndex == 2)
			{
				base.Scene.Add(new MiniTextbox("ch6_boss_tired_b"));
			}
			else if (nodeIndex == 3)
			{
				base.Scene.Add(new MiniTextbox("ch6_boss_tired_c"));
			}
		}
		foreach (FinalBossShot entity in level.Tracker.GetEntities<FinalBossShot>())
		{
			entity.Destroy();
		}
		foreach (FinalBossBeam entity2 in level.Tracker.GetEntities<FinalBossBeam>())
		{
			entity2.Destroy();
		}
		TriggerFallingBlocks(base.X);
		TriggerMovingBlocks(nodeIndex);
		attackCoroutine.Active = false;
		Moving = true;
		bool flag = nodeIndex == nodes.Length - 1;
		if (level.Session.Area.Mode == AreaMode.Normal)
		{
			if (flag && level.Session.Level.Equals("boss-19"))
			{
				Alarm.Set(this, 0.25f, delegate
				{
					Audio.Play("event:/game/06_reflection/boss_spikes_burst");
					foreach (CrystalStaticSpinner entity3 in base.Scene.Tracker.GetEntities<CrystalStaticSpinner>())
					{
						entity3.Destroy(boss: true);
					}
				});
				Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "postboss", 1f);
				Audio.SetMusic(null);
			}
			else if (startHit && level.Session.Audio.Music.Event != "event:/music/lvl6/badeline_glitch")
			{
				level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_glitch";
				level.Session.Audio.Apply();
			}
			else if (level.Session.Audio.Music.Event != "event:/music/lvl6/badeline_fight" && level.Session.Audio.Music.Event != "event:/music/lvl6/badeline_glitch")
			{
				level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_fight";
				level.Session.Audio.Apply();
			}
		}
		Add(new Coroutine(MoveSequence(player, flag)));
	}

	[IteratorStateMachine(typeof(_003CMoveSequence_003Ed__42))]
	private IEnumerator MoveSequence(Player player, bool lastHit)
	{
		if (lastHit)
		{
			Audio.SetMusicParam("boss_pitch", 1f);
			Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 0.3f, start: true);
			tween.OnUpdate = delegate(Tween t)
			{
				Glitch.Value = 0.6f * t.Eased;
			};
			Add(tween);
		}
		else
		{
			Tween tween2 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.3f, start: true);
			tween2.OnUpdate = delegate(Tween t)
			{
				Glitch.Value = 0.5f * (1f - t.Eased);
			};
			Add(tween2);
		}
		if (player != null && !player.Dead)
		{
			player.StartAttract(base.Center + Vector2.UnitY * 4f);
		}
		float timer = 0.15f;
		while (player != null && !player.Dead && !player.AtAttractTarget)
		{
			yield return null;
			timer -= Engine.DeltaTime;
		}
		if (timer > 0f)
		{
			yield return timer;
		}
		foreach (ReflectionTentacles entity2 in base.Scene.Tracker.GetEntities<ReflectionTentacles>())
		{
			entity2.Retreat();
		}
		if (player != null)
		{
			Celeste.Freeze(0.1f);
			if (lastHit)
			{
				Engine.TimeRate = 0.5f;
			}
			else
			{
				Engine.TimeRate = 0.75f;
			}
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		}
		PushPlayer(player);
		level.Shake();
		yield return 0.05f;
		for (float num = 0f; num < (float)Math.PI * 2f; num += 0.17453292f)
		{
			Vector2 position = base.Center + Sprite.Position + Calc.AngleToVector(num + Calc.Random.Range(-(float)Math.PI / 90f, (float)Math.PI / 90f), Calc.Random.Range(16, 20));
			level.Particles.Emit(P_Burst, position, num);
		}
		yield return 0.05f;
		Audio.SetMusicParam("boss_pitch", 0f);
		float from = Engine.TimeRate;
		Tween tween3 = Tween.Create(Tween.TweenMode.Oneshot, null, 0.35f / Engine.TimeRateB, start: true);
		tween3.UseRawDeltaTime = true;
		tween3.OnUpdate = delegate(Tween t)
		{
			if (bossBg != null && bossBg.Alpha < t.Eased)
			{
				bossBg.Alpha = t.Eased;
			}
			Engine.TimeRate = MathHelper.Lerp(from, 1f, t.Eased);
			if (lastHit)
			{
				Glitch.Value = 0.6f * (1f - t.Eased);
			}
		};
		Add(tween3);
		yield return 0.2f;
		Vector2 from2 = Position;
		Vector2 to = nodes[nodeIndex];
		float duration = Vector2.Distance(from2, to) / 600f;
		float dir = (to - from2).Angle();
		Tween tween4 = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, duration, start: true);
		tween4.OnUpdate = delegate(Tween t)
		{
			Position = Vector2.Lerp(from2, to, t.Eased);
			if (t.Eased >= 0.1f && t.Eased <= 0.9f && base.Scene.OnInterval(0.02f))
			{
				TrailManager.Add(this, Player.NormalHairColor, 0.5f);
				level.Particles.Emit(Player.P_DashB, 2, base.Center, Vector2.One * 3f, dir);
			}
		};
		tween4.OnComplete = delegate
		{
			Sprite.Play("recoverHit");
			Moving = false;
			Collidable = true;
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				facing = Math.Sign(entity.X - base.X);
				if (facing == 0)
				{
					facing = -1;
				}
			}
			StartAttacking();
			floatSine.Reset();
		};
		Add(tween4);
	}

	private void PushPlayer(Player player)
	{
		if (player != null && !player.Dead)
		{
			int num = Math.Sign(base.X - nodes[nodeIndex].X);
			if (num == 0)
			{
				num = -1;
			}
			player.FinalBossPushLaunch(num);
		}
		SceneAs<Level>().Displacement.AddBurst(Position, 0.4f, 12f, 36f, 0.5f);
		SceneAs<Level>().Displacement.AddBurst(Position, 0.4f, 24f, 48f, 0.5f);
		SceneAs<Level>().Displacement.AddBurst(Position, 0.4f, 36f, 60f, 0.5f);
	}

	private void TriggerFallingBlocks(float leftOfX)
	{
		while (fallingBlocks.Count > 0 && fallingBlocks[0].Scene == null)
		{
			fallingBlocks.RemoveAt(0);
		}
		int num = 0;
		while (fallingBlocks.Count > 0 && fallingBlocks[0].X < leftOfX)
		{
			FallingBlock obj = fallingBlocks[0] as FallingBlock;
			obj.StartShaking();
			obj.Triggered = true;
			obj.FallDelay = 0.4f * (float)num;
			num++;
			fallingBlocks.RemoveAt(0);
		}
	}

	private void TriggerMovingBlocks(int nodeIndex)
	{
		if (nodeIndex > 0)
		{
			DestroyMovingBlocks(nodeIndex - 1);
		}
		float num = 0f;
		foreach (FinalBossMovingBlock movingBlock in movingBlocks)
		{
			if (movingBlock.BossNodeIndex == nodeIndex)
			{
				movingBlock.StartMoving(num);
				num += 0.15f;
			}
		}
	}

	private void DestroyMovingBlocks(int nodeIndex)
	{
		float num = 0f;
		foreach (FinalBossMovingBlock movingBlock in movingBlocks)
		{
			if (movingBlock.BossNodeIndex == nodeIndex)
			{
				movingBlock.Destroy(num);
				num += 0.05f;
			}
		}
	}

	private void StartAttacking()
	{
		switch (patternIndex)
		{
		case 0:
		case 1:
			attackCoroutine.Replace(Attack01Sequence());
			break;
		case 2:
			attackCoroutine.Replace(Attack02Sequence());
			break;
		case 3:
			attackCoroutine.Replace(Attack03Sequence());
			break;
		case 4:
			attackCoroutine.Replace(Attack04Sequence());
			break;
		case 5:
			attackCoroutine.Replace(Attack05Sequence());
			break;
		case 6:
			attackCoroutine.Replace(Attack06Sequence());
			break;
		case 7:
			attackCoroutine.Replace(Attack07Sequence());
			break;
		case 8:
			attackCoroutine.Replace(Attack08Sequence());
			break;
		case 9:
			attackCoroutine.Replace(Attack09Sequence());
			break;
		case 10:
			attackCoroutine.Replace(Attack10Sequence());
			break;
		case 11:
			attackCoroutine.Replace(Attack11Sequence());
			break;
		case 13:
			attackCoroutine.Replace(Attack13Sequence());
			break;
		case 14:
			attackCoroutine.Replace(Attack14Sequence());
			break;
		case 15:
			attackCoroutine.Replace(Attack15Sequence());
			break;
		case 12:
			break;
		}
	}

	private void StartShootCharge()
	{
		Sprite.Play("attack1Begin");
		chargeSfx.Play("event:/char/badeline/boss_bullet");
	}

	[IteratorStateMachine(typeof(_003CAttack01Sequence_003Ed__49))]
	private IEnumerator Attack01Sequence()
	{
		StartShootCharge();
		while (true)
		{
			yield return 0.5f;
			Shoot();
			yield return 1f;
			StartShootCharge();
			yield return 0.15f;
			yield return 0.3f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack02Sequence_003Ed__50))]
	private IEnumerator Attack02Sequence()
	{
		while (true)
		{
			yield return 0.5f;
			yield return Beam();
			yield return 0.4f;
			StartShootCharge();
			yield return 0.3f;
			Shoot();
			yield return 0.5f;
			yield return 0.3f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack03Sequence_003Ed__51))]
	private IEnumerator Attack03Sequence()
	{
		StartShootCharge();
		yield return 0.1f;
		while (true)
		{
			for (int i = 0; i < 5; i++)
			{
				Player entity = level.Tracker.GetEntity<Player>();
				if (entity != null)
				{
					Vector2 at = entity.Center;
					for (int j = 0; j < 2; j++)
					{
						ShootAt(at);
						yield return 0.15f;
					}
				}
				if (i < 4)
				{
					StartShootCharge();
					yield return 0.5f;
				}
			}
			yield return 2f;
			StartShootCharge();
			yield return 0.7f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack04Sequence_003Ed__52))]
	private IEnumerator Attack04Sequence()
	{
		StartShootCharge();
		yield return 0.1f;
		while (true)
		{
			for (int i = 0; i < 5; i++)
			{
				Player entity = level.Tracker.GetEntity<Player>();
				if (entity != null)
				{
					Vector2 at = entity.Center;
					for (int j = 0; j < 2; j++)
					{
						ShootAt(at);
						yield return 0.15f;
					}
				}
				if (i < 4)
				{
					StartShootCharge();
					yield return 0.5f;
				}
			}
			yield return 1.5f;
			yield return Beam();
			yield return 1.5f;
			StartShootCharge();
		}
	}

	[IteratorStateMachine(typeof(_003CAttack05Sequence_003Ed__53))]
	private IEnumerator Attack05Sequence()
	{
		yield return 0.2f;
		while (true)
		{
			yield return Beam();
			yield return 0.6f;
			StartShootCharge();
			yield return 0.3f;
			for (int i = 0; i < 3; i++)
			{
				Player entity = level.Tracker.GetEntity<Player>();
				if (entity != null)
				{
					Vector2 at = entity.Center;
					for (int j = 0; j < 2; j++)
					{
						ShootAt(at);
						yield return 0.15f;
					}
				}
				if (i < 2)
				{
					StartShootCharge();
					yield return 0.5f;
				}
			}
			yield return 0.8f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack06Sequence_003Ed__54))]
	private IEnumerator Attack06Sequence()
	{
		while (true)
		{
			yield return Beam();
			yield return 0.7f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack07Sequence_003Ed__55))]
	private IEnumerator Attack07Sequence()
	{
		while (true)
		{
			Shoot();
			yield return 0.8f;
			StartShootCharge();
			yield return 0.8f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack08Sequence_003Ed__56))]
	private IEnumerator Attack08Sequence()
	{
		while (true)
		{
			yield return 0.1f;
			yield return Beam();
			yield return 0.8f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack09Sequence_003Ed__57))]
	private IEnumerator Attack09Sequence()
	{
		StartShootCharge();
		while (true)
		{
			yield return 0.5f;
			Shoot();
			yield return 0.15f;
			StartShootCharge();
			Shoot();
			yield return 0.4f;
			StartShootCharge();
			yield return 0.1f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack10Sequence_003Ed__58))]
	private IEnumerator Attack10Sequence()
	{
		yield break;
	}

	[IteratorStateMachine(typeof(_003CAttack11Sequence_003Ed__59))]
	private IEnumerator Attack11Sequence()
	{
		if (nodeIndex == 0)
		{
			StartShootCharge();
			yield return 0.6f;
		}
		while (true)
		{
			Shoot();
			yield return 1.9f;
			StartShootCharge();
			yield return 0.6f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack13Sequence_003Ed__60))]
	private IEnumerator Attack13Sequence()
	{
		if (nodeIndex != 0)
		{
			yield return Attack01Sequence();
		}
	}

	[IteratorStateMachine(typeof(_003CAttack14Sequence_003Ed__61))]
	private IEnumerator Attack14Sequence()
	{
		while (true)
		{
			yield return 0.2f;
			yield return Beam();
			yield return 0.3f;
		}
	}

	[IteratorStateMachine(typeof(_003CAttack15Sequence_003Ed__62))]
	private IEnumerator Attack15Sequence()
	{
		while (true)
		{
			yield return 0.2f;
			yield return Beam();
			yield return 1.2f;
		}
	}

	private void Shoot(float angleOffset = 0f)
	{
		if (!chargeSfx.Playing)
		{
			chargeSfx.Play("event:/char/badeline/boss_bullet", "end", 1f);
		}
		else
		{
			chargeSfx.Param("end", 1f);
		}
		Sprite.Play("attack1Recoil", restart: true);
		Player entity = level.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			level.Add(Engine.Pooler.Create<FinalBossShot>().Init(this, entity, angleOffset));
		}
	}

	private void ShootAt(Vector2 at)
	{
		if (!chargeSfx.Playing)
		{
			chargeSfx.Play("event:/char/badeline/boss_bullet", "end", 1f);
		}
		else
		{
			chargeSfx.Param("end", 1f);
		}
		Sprite.Play("attack1Recoil", restart: true);
		level.Add(Engine.Pooler.Create<FinalBossShot>().Init(this, at));
	}

	[IteratorStateMachine(typeof(_003CBeam_003Ed__65))]
	private IEnumerator Beam()
	{
		laserSfx.Play("event:/char/badeline/boss_laser_charge");
		Sprite.Play("attack2Begin", restart: true);
		yield return 0.1f;
		Player entity = level.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			level.Add(Engine.Pooler.Create<FinalBossBeam>().Init(this, entity));
		}
		yield return 0.9f;
		Sprite.Play("attack2Lock", restart: true);
		yield return 0.5f;
		laserSfx.Stop();
		Audio.Play("event:/char/badeline/boss_laser_fire", Position);
		Sprite.Play("attack2Recoil");
	}

	public override void Removed(Scene scene)
	{
		if (bossBg != null && patternIndex == 0)
		{
			bossBg.Alpha = 1f;
		}
		base.Removed(scene);
	}
}
