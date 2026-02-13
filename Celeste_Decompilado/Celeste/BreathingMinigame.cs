using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class BreathingMinigame : Entity
{
	private struct Particle
	{
		public Vector2 Position;

		public float Speed;

		public float Scale;

		public float Sin;

		public void Reset()
		{
			float num = Calc.Random.NextFloat();
			num *= num * num * num;
			Position = new Vector2(Calc.Random.NextFloat() * 1920f, Calc.Random.NextFloat() * 1080f);
			Scale = Calc.Map(num, 0f, 1f, 0.05f, 0.8f);
			Speed = Scale * Calc.Random.Range(2f, 8f);
			Sin = Calc.Random.NextFloat((float)Math.PI * 2f);
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__56 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BreathingMinigame _003C_003E4__this;

		private float _003CactiveBounds_003E5__2;

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
		public _003CRoutine_003Ed__56(int _003C_003E1__state)
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
			BreathingMinigame breathingMinigame = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				breathingMinigame.insideTargetTimer = 1f;
				_003Cp_003E5__3 = 0f;
				goto IL_00cc;
			case 1:
				_003C_003E1__state = -1;
				if (_003Cp_003E5__3 > 1f)
				{
					_003Cp_003E5__3 = 1f;
				}
				breathingMinigame.bgAlpha = _003Cp_003E5__3 * 0.65f;
				_003Cp_003E5__3 += Engine.DeltaTime;
				goto IL_00cc;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.FadeGameIn();
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.ShowText(2);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.ShowText(3);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.ShowText(4);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.ShowText(5);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				goto IL_01af;
			case 8:
				_003C_003E1__state = -1;
				goto IL_01af;
			case 9:
				_003C_003E1__state = -1;
				goto IL_076a;
			case 10:
				_003C_003E1__state = -1;
				goto IL_084c;
			case 11:
				_003C_003E1__state = -1;
				goto IL_097b;
			case 12:
				_003C_003E1__state = -1;
				breathingMinigame.boxAlpha -= Engine.DeltaTime;
				breathingMinigame.particleAlpha = breathingMinigame.boxAlpha;
				goto IL_08e8;
			case 13:
				_003C_003E1__state = -1;
				goto IL_094d;
			case 14:
				_003C_003E1__state = -1;
				breathingMinigame.featherAlpha -= Engine.DeltaTime;
				goto IL_094d;
			case 15:
				_003C_003E1__state = -1;
				goto IL_097b;
			case 16:
				{
					_003C_003E1__state = -1;
					breathingMinigame.bgAlpha -= Engine.DeltaTime * (breathingMinigame.winnable ? 1f : 10f);
					break;
				}
				IL_084c:
				if (breathingMinigame.Pausing)
				{
					if (breathingMinigame.rumbler != null)
					{
						breathingMinigame.rumbler.Strength = Calc.Approach(breathingMinigame.rumbler.Strength, 1f, 2f * Engine.DeltaTime);
					}
					breathingMinigame.featherSprite.Position += (breathingMinigame.screenCenter - breathingMinigame.featherSprite.Position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
					breathingMinigame.boxAlpha -= Engine.DeltaTime * 10f;
					breathingMinigame.particleAlpha = breathingMinigame.boxAlpha;
					_003C_003E2__current = null;
					_003C_003E1__state = 10;
					return true;
				}
				breathingMinigame.losing = false;
				breathingMinigame.losingTimer = 0f;
				_003C_003E2__current = breathingMinigame.PopFeather();
				_003C_003E1__state = 11;
				return true;
				IL_094d:
				if (breathingMinigame.featherAlpha > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 14;
					return true;
				}
				_003C_003E2__current = 1f;
				_003C_003E1__state = 15;
				return true;
				IL_097b:
				breathingMinigame.Completed = true;
				break;
				IL_01af:
				breathingMinigame.Add(new Coroutine(breathingMinigame.FadeBoxIn()));
				_003CactiveBounds_003E5__2 = 450f;
				goto IL_076a;
				IL_076a:
				if (breathingMinigame.stablizedTimer < 30f)
				{
					float num2 = breathingMinigame.stablizedTimer / 30f;
					bool flag = Input.Jump.Check || Input.Dash.Check || Input.Aim.Value.Y < 0f;
					if (breathingMinigame.winnable)
					{
						Audio.SetMusicParam("calm", num2);
						Audio.SetMusicParam("gondola_idle", num2);
					}
					else
					{
						Level level = breathingMinigame.Scene as Level;
						if (!breathingMinigame.losing)
						{
							float num3 = num2 / 0.4f;
							level.Session.Audio.Music.Layer(1, num3);
							level.Session.Audio.Music.Layer(3, 1f - num3);
							level.Session.Audio.Apply();
						}
						else
						{
							level.Session.Audio.Music.Layer(1, 1f - breathingMinigame.losingTimer);
							level.Session.Audio.Music.Layer(3, breathingMinigame.losingTimer);
							level.Session.Audio.Apply();
						}
					}
					if (!breathingMinigame.winnable && breathingMinigame.losing)
					{
						if (Calc.BetweenInterval(breathingMinigame.losingTimer * 10f, 0.5f))
						{
							flag = !flag;
						}
						_003CactiveBounds_003E5__2 = 450f - Ease.CubeIn(breathingMinigame.losingTimer) * 200f;
					}
					if (flag)
					{
						if (breathingMinigame.feather > 0f - _003CactiveBounds_003E5__2)
						{
							breathingMinigame.speed -= 280f * Engine.DeltaTime;
						}
						breathingMinigame.particleSpeed -= 2800f * Engine.DeltaTime;
					}
					else
					{
						if (breathingMinigame.feather < _003CactiveBounds_003E5__2)
						{
							breathingMinigame.speed += 280f * Engine.DeltaTime;
						}
						breathingMinigame.particleSpeed += 2800f * Engine.DeltaTime;
					}
					breathingMinigame.speed = Calc.Clamp(breathingMinigame.speed, -200f, 200f);
					if (breathingMinigame.feather > _003CactiveBounds_003E5__2 && breathingMinigame.speedMultiplier == 0f && breathingMinigame.speed > 0f)
					{
						breathingMinigame.speed = 0f;
					}
					if (breathingMinigame.feather < _003CactiveBounds_003E5__2 && breathingMinigame.speedMultiplier == 0f && breathingMinigame.speed < 0f)
					{
						breathingMinigame.speed = 0f;
					}
					breathingMinigame.particleSpeed = Calc.Clamp(breathingMinigame.particleSpeed, -1600f, 120f);
					breathingMinigame.speedMultiplier = Calc.Approach(breathingMinigame.speedMultiplier, ((!(breathingMinigame.feather < 0f - _003CactiveBounds_003E5__2) || !(breathingMinigame.speed < 0f)) && (!(breathingMinigame.feather > _003CactiveBounds_003E5__2) || !(breathingMinigame.speed > 0f))) ? 1 : 0, Engine.DeltaTime * 4f);
					breathingMinigame.currentTargetBounds = Calc.Approach(breathingMinigame.currentTargetBounds, 160f + -60f * num2, Engine.DeltaTime * 16f);
					breathingMinigame.feather += breathingMinigame.speed * breathingMinigame.speedMultiplier * Engine.DeltaTime;
					if (breathingMinigame.boxEnabled)
					{
						breathingMinigame.currentTargetCenter = (0f - breathingMinigame.sine.Value) * 300f * MathHelper.Lerp(1f, 0f, Ease.CubeIn(num2));
						float num4 = breathingMinigame.currentTargetCenter - breathingMinigame.currentTargetBounds;
						float num5 = breathingMinigame.currentTargetCenter + breathingMinigame.currentTargetBounds;
						if (breathingMinigame.feather > num4 && breathingMinigame.feather < num5)
						{
							breathingMinigame.insideTargetTimer += Engine.DeltaTime;
							if (breathingMinigame.insideTargetTimer > 0.2f)
							{
								breathingMinigame.stablizedTimer += Engine.DeltaTime;
							}
							if (breathingMinigame.rumbler != null)
							{
								breathingMinigame.rumbler.Strength = 0.3f * (1f - num2);
							}
						}
						else
						{
							if (breathingMinigame.insideTargetTimer > 0.2f)
							{
								breathingMinigame.stablizedTimer = Math.Max(0f, breathingMinigame.stablizedTimer - 0.5f);
							}
							if (breathingMinigame.stablizedTimer > 0f)
							{
								breathingMinigame.stablizedTimer -= 0.5f * Engine.DeltaTime;
							}
							breathingMinigame.insideTargetTimer = 0f;
							if (breathingMinigame.rumbler != null)
							{
								breathingMinigame.rumbler.Strength = 0.5f * (1f - num2);
							}
						}
					}
					else if (breathingMinigame.rumbler != null)
					{
						breathingMinigame.rumbler.Strength = 0.2f;
					}
					float target = 0.65f + Math.Min(1f, num2 / 0.8f) * 0.35000002f;
					breathingMinigame.bgAlpha = Calc.Approach(breathingMinigame.bgAlpha, target, Engine.DeltaTime);
					breathingMinigame.featherSprite.Position = breathingMinigame.screenCenter + Vector2.UnitY * (breathingMinigame.feather + -128f);
					breathingMinigame.featherSprite.Play((breathingMinigame.insideTargetTimer > 0f || !breathingMinigame.boxEnabled) ? "hover" : "flutter");
					breathingMinigame.particleAlpha = Calc.Approach(breathingMinigame.particleAlpha, 1f, Engine.DeltaTime);
					if (!breathingMinigame.winnable && breathingMinigame.stablizedTimer > 12f)
					{
						breathingMinigame.losing = true;
					}
					if (breathingMinigame.losing)
					{
						breathingMinigame.losingTimer += Engine.DeltaTime / 5f;
						if (breathingMinigame.losingTimer > 1f)
						{
							goto IL_077a;
						}
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 9;
					return true;
				}
				goto IL_077a;
				IL_08e8:
				if (breathingMinigame.boxAlpha > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 12;
					return true;
				}
				breathingMinigame.particleAlpha = 0f;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 13;
				return true;
				IL_00cc:
				if (_003Cp_003E5__3 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (breathingMinigame.winnable)
				{
					_003C_003E2__current = breathingMinigame.ShowText(1);
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = breathingMinigame.FadeGameIn();
				_003C_003E1__state = 8;
				return true;
				IL_077a:
				if (!breathingMinigame.winnable)
				{
					breathingMinigame.Pausing = true;
					goto IL_084c;
				}
				breathingMinigame.bgAlpha = 1f;
				if (breathingMinigame.rumbler != null)
				{
					breathingMinigame.rumbler.RemoveSelf();
					breathingMinigame.rumbler = null;
				}
				goto IL_08e8;
			}
			if (breathingMinigame.bgAlpha > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 16;
				return true;
			}
			breathingMinigame.RemoveSelf();
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
	private sealed class _003CShowText_003Ed__57 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BreathingMinigame _003C_003E4__this;

		public int num;

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
		public _003CShowText_003Ed__57(int _003C_003E1__state)
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
			BreathingMinigame breathingMinigame = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.FadeTextTo(0f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				breathingMinigame.text = Dialog.Clean("CH4_GONDOLA_FEATHER_" + this.num);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = breathingMinigame.FadeTextTo(1f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00d1;
			case 4:
				_003C_003E1__state = -1;
				goto IL_00d1;
			case 5:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00d1:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = breathingMinigame.FadeTextTo(0f);
				_003C_003E1__state = 5;
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
	private sealed class _003CFadeGameIn_003Ed__58 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BreathingMinigame _003C_003E4__this;

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
		public _003CFadeGameIn_003Ed__58(int _003C_003E1__state)
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
			BreathingMinigame breathingMinigame = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (breathingMinigame.featherAlpha < 1f)
			{
				breathingMinigame.featherAlpha += Engine.DeltaTime;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			breathingMinigame.featherAlpha = 1f;
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
	private sealed class _003CFadeBoxIn_003Ed__59 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BreathingMinigame _003C_003E4__this;

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
		public _003CFadeBoxIn_003Ed__59(int _003C_003E1__state)
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
			BreathingMinigame breathingMinigame = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = (breathingMinigame.winnable ? 5f : 2f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0075;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0075;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0075:
				if (Math.Abs(breathingMinigame.feather) > 300f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				breathingMinigame.boxEnabled = true;
				breathingMinigame.Add(breathingMinigame.sine = new SineWave(0.12f));
				break;
			}
			if (breathingMinigame.boxAlpha < 1f)
			{
				breathingMinigame.boxAlpha += Engine.DeltaTime;
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			breathingMinigame.boxAlpha = 1f;
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
	private sealed class _003CFadeTextTo_003Ed__60 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BreathingMinigame _003C_003E4__this;

		public float v;

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
		public _003CFadeTextTo_003Ed__60(int _003C_003E1__state)
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
			BreathingMinigame breathingMinigame = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (breathingMinigame.textAlpha == v)
				{
					return false;
				}
				_003Cfrom_003E5__2 = breathingMinigame.textAlpha;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				breathingMinigame.textAlpha = _003Cfrom_003E5__2 + (v - _003Cfrom_003E5__2) * _003Cp_003E5__3;
				_003Cp_003E5__3 += Engine.DeltaTime * 4f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			breathingMinigame.textAlpha = v;
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
	private sealed class _003CPopFeather_003Ed__61 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BreathingMinigame _003C_003E4__this;

		private float _003Cp_003E5__2;

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
		public _003CPopFeather_003Ed__61(int _003C_003E1__state)
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
			BreathingMinigame breathingMinigame = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/06_reflection/badeline_feather_slice");
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				if (breathingMinigame.rumbler != null)
				{
					breathingMinigame.rumbler.RemoveSelf();
					breathingMinigame.rumbler = null;
				}
				breathingMinigame.featherSprite.Rotation = 0f;
				breathingMinigame.featherSprite.Play("hover");
				breathingMinigame.featherSprite.CenterOrigin();
				breathingMinigame.featherSprite.Y += breathingMinigame.featherSprite.Height / 2f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				breathingMinigame.featherSlice = new Image(GFX.Gui["feather/slice"]);
				breathingMinigame.featherSlice.CenterOrigin();
				breathingMinigame.featherSlice.Position = breathingMinigame.featherSprite.Position;
				breathingMinigame.featherSlice.Rotation = Calc.Angle(new Vector2(96f, 165f), new Vector2(140f, 112f));
				_003Cp_003E5__2 = 0f;
				goto IL_0217;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 8f;
				goto IL_0217;
			case 3:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__2 += Engine.DeltaTime;
					break;
				}
				IL_0217:
				if (_003Cp_003E5__2 < 1f)
				{
					breathingMinigame.featherSlice.Scale.X = (0.25f + Calc.YoYo(_003Cp_003E5__2) * 0.75f) * 8f;
					breathingMinigame.featherSlice.Scale.Y = (0.5f + (1f - Calc.YoYo(_003Cp_003E5__2)) * 0.5f) * 8f;
					breathingMinigame.featherSlice.Position = breathingMinigame.featherSprite.Position + Vector2.Lerp(new Vector2(128f, -128f), new Vector2(-128f, 128f), _003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				breathingMinigame.featherSlice.Visible = false;
				(breathingMinigame.Scene as Level).Shake();
				(breathingMinigame.Scene as Level).Flash(Color.White);
				breathingMinigame.featherSprite.Visible = false;
				breathingMinigame.featherHalfLeft = new Image(GFX.Gui["feather/feather_half0"]);
				breathingMinigame.featherHalfLeft.CenterOrigin();
				breathingMinigame.featherHalfRight = new Image(GFX.Gui["feather/feather_half1"]);
				breathingMinigame.featherHalfRight.CenterOrigin();
				_003Cp_003E5__2 = 0f;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				breathingMinigame.featherHalfLeft.Position = breathingMinigame.featherSprite.Position + Vector2.Lerp(Vector2.Zero, new Vector2(-128f, -32f), _003Cp_003E5__2);
				breathingMinigame.featherHalfRight.Position = breathingMinigame.featherSprite.Position + Vector2.Lerp(Vector2.Zero, new Vector2(128f, 32f), _003Cp_003E5__2);
				breathingMinigame.featherAlpha = 1f - _003Cp_003E5__2;
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
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

	private const float StablizeDuration = 30f;

	private const float StablizeLossRate = 0.5f;

	private const float StablizeIncreaseDelay = 0.2f;

	private const float StablizeLossPenalty = 0.5f;

	private const float Acceleration = 280f;

	private const float Gravity = 280f;

	private const float Maxspeed = 200f;

	private const float Bounds = 450f;

	private const float BGFadeStart = 0.65f;

	private const float featherSpriteOffset = -128f;

	private const float FadeBoxInMargin = 300f;

	private const float TargetSineAmplitude = 300f;

	private const float TargetSineFreq = 0.25f;

	private const float TargetBoundsAtStart = 160f;

	private const float TargetBoundsAtEnd = 100f;

	public const float MaxRumble = 0.5f;

	private const float PercentBeforeStartLosing = 0.4f;

	private const float LoseDuration = 5f;

	public bool Completed;

	public bool Pausing;

	private bool winnable;

	private float boxAlpha;

	private float featherAlpha;

	private float bgAlpha;

	private float feather;

	private float speed;

	private float stablizedTimer;

	private float currentTargetBounds = 160f;

	private float currentTargetCenter;

	private float speedMultiplier = 1f;

	private float insideTargetTimer;

	private bool boxEnabled;

	private float trailSpeed;

	private bool losing;

	private float losingTimer;

	private Sprite featherSprite;

	private Image featherSlice;

	private Image featherHalfLeft;

	private Image featherHalfRight;

	private SineWave sine;

	private SineWave featherWave;

	private BreathingRumbler rumbler;

	private string text;

	private float textAlpha;

	private VirtualRenderTarget featherBuffer;

	private VirtualRenderTarget smokeBuffer;

	private VirtualRenderTarget tempBuffer;

	private float timer;

	private Particle[] particles;

	private MTexture particleTexture = OVR.Atlas["snow"].GetSubtexture(1, 1, 254, 254);

	private float particleSpeed;

	private float particleAlpha;

	private Vector2 screenCenter => new Vector2(1920f, 1080f) / 2f;

	public BreathingMinigame(bool winnable = true, BreathingRumbler rumbler = null)
	{
		this.rumbler = rumbler;
		this.winnable = winnable;
		base.Tag = Tags.HUD;
		base.Depth = 100;
		Add(featherSprite = GFX.GuiSpriteBank.Create("feather"));
		featherSprite.Position = screenCenter + Vector2.UnitY * (feather + -128f);
		Add(new Coroutine(Routine()));
		Add(featherWave = new SineWave(0.25f));
		Add(new BeforeRenderHook(BeforeRender));
		particles = new Particle[50];
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Reset();
		}
		particleSpeed = 120f;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__56))]
	public IEnumerator Routine()
	{
		insideTargetTimer = 1f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			yield return null;
			if (p > 1f)
			{
				p = 1f;
			}
			bgAlpha = p * 0.65f;
		}
		if (winnable)
		{
			yield return ShowText(1);
			yield return FadeGameIn();
			yield return ShowText(2);
			yield return ShowText(3);
			yield return ShowText(4);
			yield return ShowText(5);
		}
		else
		{
			yield return FadeGameIn();
		}
		Add(new Coroutine(FadeBoxIn()));
		float activeBounds = 450f;
		while (stablizedTimer < 30f)
		{
			float num = stablizedTimer / 30f;
			bool flag = Input.Jump.Check || Input.Dash.Check || Input.Aim.Value.Y < 0f;
			if (winnable)
			{
				Audio.SetMusicParam("calm", num);
				Audio.SetMusicParam("gondola_idle", num);
			}
			else
			{
				Level level = base.Scene as Level;
				if (!losing)
				{
					float num2 = num / 0.4f;
					level.Session.Audio.Music.Layer(1, num2);
					level.Session.Audio.Music.Layer(3, 1f - num2);
					level.Session.Audio.Apply();
				}
				else
				{
					level.Session.Audio.Music.Layer(1, 1f - losingTimer);
					level.Session.Audio.Music.Layer(3, losingTimer);
					level.Session.Audio.Apply();
				}
			}
			if (!winnable && losing)
			{
				if (Calc.BetweenInterval(losingTimer * 10f, 0.5f))
				{
					flag = !flag;
				}
				activeBounds = 450f - Ease.CubeIn(losingTimer) * 200f;
			}
			if (flag)
			{
				if (feather > 0f - activeBounds)
				{
					speed -= 280f * Engine.DeltaTime;
				}
				particleSpeed -= 2800f * Engine.DeltaTime;
			}
			else
			{
				if (feather < activeBounds)
				{
					speed += 280f * Engine.DeltaTime;
				}
				particleSpeed += 2800f * Engine.DeltaTime;
			}
			speed = Calc.Clamp(speed, -200f, 200f);
			if (feather > activeBounds && speedMultiplier == 0f && speed > 0f)
			{
				speed = 0f;
			}
			if (feather < activeBounds && speedMultiplier == 0f && speed < 0f)
			{
				speed = 0f;
			}
			particleSpeed = Calc.Clamp(particleSpeed, -1600f, 120f);
			speedMultiplier = Calc.Approach(speedMultiplier, ((!(feather < 0f - activeBounds) || !(speed < 0f)) && (!(feather > activeBounds) || !(speed > 0f))) ? 1 : 0, Engine.DeltaTime * 4f);
			currentTargetBounds = Calc.Approach(currentTargetBounds, 160f + -60f * num, Engine.DeltaTime * 16f);
			feather += speed * speedMultiplier * Engine.DeltaTime;
			if (boxEnabled)
			{
				currentTargetCenter = (0f - sine.Value) * 300f * MathHelper.Lerp(1f, 0f, Ease.CubeIn(num));
				float num3 = currentTargetCenter - currentTargetBounds;
				float num4 = currentTargetCenter + currentTargetBounds;
				if (feather > num3 && feather < num4)
				{
					insideTargetTimer += Engine.DeltaTime;
					if (insideTargetTimer > 0.2f)
					{
						stablizedTimer += Engine.DeltaTime;
					}
					if (rumbler != null)
					{
						rumbler.Strength = 0.3f * (1f - num);
					}
				}
				else
				{
					if (insideTargetTimer > 0.2f)
					{
						stablizedTimer = Math.Max(0f, stablizedTimer - 0.5f);
					}
					if (stablizedTimer > 0f)
					{
						stablizedTimer -= 0.5f * Engine.DeltaTime;
					}
					insideTargetTimer = 0f;
					if (rumbler != null)
					{
						rumbler.Strength = 0.5f * (1f - num);
					}
				}
			}
			else if (rumbler != null)
			{
				rumbler.Strength = 0.2f;
			}
			float target = 0.65f + Math.Min(1f, num / 0.8f) * 0.35000002f;
			bgAlpha = Calc.Approach(bgAlpha, target, Engine.DeltaTime);
			featherSprite.Position = screenCenter + Vector2.UnitY * (feather + -128f);
			featherSprite.Play((insideTargetTimer > 0f || !boxEnabled) ? "hover" : "flutter");
			particleAlpha = Calc.Approach(particleAlpha, 1f, Engine.DeltaTime);
			if (!winnable && stablizedTimer > 12f)
			{
				losing = true;
			}
			if (losing)
			{
				losingTimer += Engine.DeltaTime / 5f;
				if (losingTimer > 1f)
				{
					break;
				}
			}
			yield return null;
		}
		if (!winnable)
		{
			Pausing = true;
			while (Pausing)
			{
				if (rumbler != null)
				{
					rumbler.Strength = Calc.Approach(rumbler.Strength, 1f, 2f * Engine.DeltaTime);
				}
				featherSprite.Position += (screenCenter - featherSprite.Position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
				boxAlpha -= Engine.DeltaTime * 10f;
				particleAlpha = boxAlpha;
				yield return null;
			}
			losing = false;
			losingTimer = 0f;
			yield return PopFeather();
		}
		else
		{
			bgAlpha = 1f;
			if (rumbler != null)
			{
				rumbler.RemoveSelf();
				rumbler = null;
			}
			while (boxAlpha > 0f)
			{
				yield return null;
				boxAlpha -= Engine.DeltaTime;
				particleAlpha = boxAlpha;
			}
			particleAlpha = 0f;
			yield return 2f;
			while (featherAlpha > 0f)
			{
				yield return null;
				featherAlpha -= Engine.DeltaTime;
			}
			yield return 1f;
		}
		for (Completed = true; bgAlpha > 0f; bgAlpha -= Engine.DeltaTime * (winnable ? 1f : 10f))
		{
			yield return null;
		}
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CShowText_003Ed__57))]
	private IEnumerator ShowText(int num)
	{
		yield return FadeTextTo(0f);
		text = Dialog.Clean("CH4_GONDOLA_FEATHER_" + num);
		yield return 0.1f;
		yield return FadeTextTo(1f);
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		yield return FadeTextTo(0f);
	}

	[IteratorStateMachine(typeof(_003CFadeGameIn_003Ed__58))]
	private IEnumerator FadeGameIn()
	{
		while (featherAlpha < 1f)
		{
			featherAlpha += Engine.DeltaTime;
			yield return null;
		}
		featherAlpha = 1f;
	}

	[IteratorStateMachine(typeof(_003CFadeBoxIn_003Ed__59))]
	private IEnumerator FadeBoxIn()
	{
		yield return winnable ? 5f : 2f;
		while (Math.Abs(feather) > 300f)
		{
			yield return null;
		}
		boxEnabled = true;
		Add(sine = new SineWave(0.12f));
		while (boxAlpha < 1f)
		{
			boxAlpha += Engine.DeltaTime;
			yield return null;
		}
		boxAlpha = 1f;
	}

	[IteratorStateMachine(typeof(_003CFadeTextTo_003Ed__60))]
	private IEnumerator FadeTextTo(float v)
	{
		if (textAlpha != v)
		{
			float from = textAlpha;
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
			{
				yield return null;
				textAlpha = from + (v - from) * p;
			}
			textAlpha = v;
		}
	}

	[IteratorStateMachine(typeof(_003CPopFeather_003Ed__61))]
	private IEnumerator PopFeather()
	{
		Audio.Play("event:/game/06_reflection/badeline_feather_slice");
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		if (rumbler != null)
		{
			rumbler.RemoveSelf();
			rumbler = null;
		}
		featherSprite.Rotation = 0f;
		featherSprite.Play("hover");
		featherSprite.CenterOrigin();
		featherSprite.Y += featherSprite.Height / 2f;
		yield return 0.25f;
		featherSlice = new Image(GFX.Gui["feather/slice"]);
		featherSlice.CenterOrigin();
		featherSlice.Position = featherSprite.Position;
		featherSlice.Rotation = Calc.Angle(new Vector2(96f, 165f), new Vector2(140f, 112f));
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 8f)
		{
			featherSlice.Scale.X = (0.25f + Calc.YoYo(p) * 0.75f) * 8f;
			featherSlice.Scale.Y = (0.5f + (1f - Calc.YoYo(p)) * 0.5f) * 8f;
			featherSlice.Position = featherSprite.Position + Vector2.Lerp(new Vector2(128f, -128f), new Vector2(-128f, 128f), p);
			yield return null;
		}
		featherSlice.Visible = false;
		(base.Scene as Level).Shake();
		(base.Scene as Level).Flash(Color.White);
		featherSprite.Visible = false;
		featherHalfLeft = new Image(GFX.Gui["feather/feather_half0"]);
		featherHalfLeft.CenterOrigin();
		featherHalfRight = new Image(GFX.Gui["feather/feather_half1"]);
		featherHalfRight.CenterOrigin();
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			featherHalfLeft.Position = featherSprite.Position + Vector2.Lerp(Vector2.Zero, new Vector2(-128f, -32f), p);
			featherHalfRight.Position = featherSprite.Position + Vector2.Lerp(Vector2.Zero, new Vector2(128f, 32f), p);
			featherAlpha = 1f - p;
			yield return null;
		}
	}

	public override void Update()
	{
		timer += Engine.DeltaTime;
		trailSpeed = Calc.Approach(trailSpeed, speed, Engine.DeltaTime * 200f * 8f);
		if (featherWave != null)
		{
			featherSprite.Rotation = featherWave.Value * 0.25f + 0.1f;
		}
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Position.Y += particles[i].Speed * particleSpeed * Engine.DeltaTime;
			if (particleSpeed > -400f)
			{
				particles[i].Position.X += (particleSpeed + 400f) * (float)Math.Sin(particles[i].Sin) * 0.1f * Engine.DeltaTime;
			}
			particles[i].Sin += Engine.DeltaTime;
			if (particles[i].Position.Y < -128f || particles[i].Position.Y > 1208f)
			{
				particles[i].Reset();
				if (particleSpeed < 0f)
				{
					particles[i].Position.Y = 1208f;
				}
				else
				{
					particles[i].Position.Y = -128f;
				}
			}
		}
		base.Update();
	}

	public void BeforeRender()
	{
		if (featherBuffer == null)
		{
			int num = Math.Min(1920, Engine.ViewWidth);
			int num2 = Math.Min(1080, Engine.ViewHeight);
			featherBuffer = VirtualContent.CreateRenderTarget("breathing-minigame-a", num, num2);
			smokeBuffer = VirtualContent.CreateRenderTarget("breathing-minigame-b", num / 2, num2 / 2);
			tempBuffer = VirtualContent.CreateRenderTarget("breathing-minigame-c", num / 2, num2 / 2);
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(featherBuffer.Target);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Matrix transformMatrix = Matrix.CreateScale((float)featherBuffer.Width / 1920f);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null, transformMatrix);
		if (losing)
		{
			featherSprite.Position += new Vector2(Calc.Random.Range(-1, 1), Calc.Random.Range(-1, 1)).SafeNormalize() * losingTimer * 10f;
			featherSprite.Rotation += (float)Calc.Random.Range(-1, 1) * losingTimer * 0.1f;
		}
		featherSprite.Color = Color.White * featherAlpha;
		if (featherSprite.Visible)
		{
			featherSprite.Render();
		}
		if (featherSlice != null && featherSlice.Visible)
		{
			featherSlice.Render();
		}
		if (featherHalfLeft != null && featherHalfLeft.Visible)
		{
			featherHalfLeft.Color = Color.White * featherAlpha;
			featherHalfRight.Color = Color.White * featherAlpha;
			featherHalfLeft.Render();
			featherHalfRight.Render();
		}
		Draw.SpriteBatch.End();
		Engine.Graphics.GraphicsDevice.SetRenderTarget(smokeBuffer.Target);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		MagicGlow.Render(featherBuffer.Target, timer, (0f - trailSpeed) / 200f * 2f, Matrix.CreateScale(0.5f));
		GaussianBlur.Blur(smokeBuffer.Target, tempBuffer, smokeBuffer);
	}

	public override void Render()
	{
		Color color = ((insideTargetTimer > 0.2f) ? Color.White : (Color.White * 0.6f));
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * bgAlpha);
		if (!(base.Scene is Level level) || (!level.FrozenOrPaused && level.RetryPlayerCorpse == null && !level.SkippingCutscene))
		{
			MTexture mTexture = GFX.Gui["feather/border"];
			MTexture mTexture2 = GFX.Gui["feather/box"];
			mTexture2.DrawCentered(scale: new Vector2(((float)mTexture.Width * 2f - 32f) / (float)mTexture2.Width, (currentTargetBounds * 2f - 32f) / (float)mTexture2.Height), position: screenCenter + new Vector2(0f, currentTargetCenter), color: Color.White * boxAlpha * 0.25f);
			mTexture.Draw(screenCenter + new Vector2(-mTexture.Width, currentTargetCenter - currentTargetBounds), Vector2.Zero, color * boxAlpha, Vector2.One);
			mTexture.Draw(screenCenter + new Vector2(mTexture.Width, currentTargetCenter + currentTargetBounds), Vector2.Zero, color * boxAlpha, new Vector2(-1f, -1f));
			if (featherBuffer != null && !featherBuffer.IsDisposed)
			{
				float num = 1920f / (float)featherBuffer.Width;
				Draw.SpriteBatch.Draw(smokeBuffer.Target, Vector2.Zero, smokeBuffer.Bounds, Color.White * 0.3f, 0f, Vector2.Zero, num * 2f, SpriteEffects.None, 0f);
				Draw.SpriteBatch.Draw(featherBuffer.Target, Vector2.Zero, featherBuffer.Bounds, Color.White, 0f, Vector2.Zero, num, SpriteEffects.None, 0f);
			}
			Vector2 vector = new Vector2(1f, 1f);
			if (particleSpeed < 0f)
			{
				vector = new Vector2(Math.Min(1f, 1f / ((0f - particleSpeed) * 0.004f)), Math.Max(1f, 1f * (0f - particleSpeed) * 0.004f));
			}
			for (int i = 0; i < particles.Length; i++)
			{
				Vector2 position = particles[i].Position;
				Vector2 scale = particles[i].Scale * vector;
				particleTexture.DrawCentered(position, Color.White * (0.5f * particleAlpha), scale);
			}
			if (!string.IsNullOrEmpty(text) && textAlpha > 0f)
			{
				ActiveFont.Draw(text, new Vector2(960f, 920f), new Vector2(0.5f, 0.5f), Vector2.One, Color.White * textAlpha);
			}
			if (!string.IsNullOrEmpty(text) && textAlpha >= 1f)
			{
				Vector2 vector2 = ActiveFont.Measure(text);
				Vector2 position2 = new Vector2((1920f + vector2.X) / 2f + 40f, 920f + vector2.Y / 2f - 16f) + new Vector2(0f, (timer % 1f < 0.25f) ? 6 : 0);
				GFX.Gui["textboxbutton"].DrawCentered(position2);
			}
		}
	}

	public override void Removed(Scene scene)
	{
		Dispose();
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Dispose();
		base.SceneEnd(scene);
	}

	private void Dispose()
	{
		if (featherBuffer != null && !featherBuffer.IsDisposed)
		{
			featherBuffer.Dispose();
			featherBuffer = null;
			smokeBuffer.Dispose();
			smokeBuffer = null;
			tempBuffer.Dispose();
			tempBuffer = null;
		}
	}
}
