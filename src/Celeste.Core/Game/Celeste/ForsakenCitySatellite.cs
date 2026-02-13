using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ForsakenCitySatellite : Entity
{
	private class CodeBird : Entity
	{
		[CompilerGenerated]
		private sealed class _003CAimlessFlightRoutine_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CodeBird _003C_003E4__this;

			private Vector2 _003Ctarget_003E5__2;

			private float _003Creset_003E5__3;

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
			public _003CAimlessFlightRoutine_003Ed__12(int _003C_003E1__state)
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
				CodeBird codeBird = _003C_003E4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					_003C_003E1__state = -1;
					goto IL_014e;
				}
				_003C_003E1__state = -1;
				codeBird.speed = Vector2.Zero;
				goto IL_002c;
				IL_014e:
				if (_003Creset_003E5__3 < 1f && (_003Ctarget_003E5__2 - codeBird.Position).Length() > 8f)
				{
					Vector2 vector = (_003Ctarget_003E5__2 - codeBird.Position).SafeNormalize();
					codeBird.speed += vector * 420f * Engine.DeltaTime;
					if (codeBird.speed.Length() > 90f)
					{
						codeBird.speed = codeBird.speed.SafeNormalize(90f);
					}
					codeBird.Position += codeBird.speed * Engine.DeltaTime;
					_003Creset_003E5__3 += Engine.DeltaTime;
					if (Math.Sign(vector.X) != 0)
					{
						codeBird.sprite.Scale.X = Math.Sign(vector.X);
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003Ctarget_003E5__2 = default(Vector2);
				goto IL_002c;
				IL_002c:
				_003Ctarget_003E5__2 = codeBird.origin + Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 16f + Calc.Random.NextFloat(40f));
				_003Creset_003E5__3 = 0f;
				goto IL_014e;
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
		private sealed class _003CDashRoutine_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CodeBird _003C_003E4__this;

			private float _003Ct_003E5__2;

			private Vector2 _003Cfrom_003E5__3;

			private Vector2 _003Cto_003E5__4;

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
			public _003CDashRoutine_003Ed__13(int _003C_003E1__state)
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
				CodeBird codeBird = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Ct_003E5__2 = 0.25f;
					goto IL_00ad;
				case 1:
					_003C_003E1__state = -1;
					_003Ct_003E5__2 -= Engine.DeltaTime;
					goto IL_00ad;
				case 2:
					_003C_003E1__state = -1;
					_003Ct_003E5__2 += Engine.DeltaTime * 1.5f;
					goto IL_01a5;
				case 3:
					_003C_003E1__state = -1;
					_003Cfrom_003E5__3 = default(Vector2);
					_003Cto_003E5__4 = default(Vector2);
					if (codeBird.dash.X != 0f)
					{
						codeBird.sprite.Scale.X = Math.Sign(codeBird.dash.X);
					}
					(codeBird.Scene as Level).Displacement.AddBurst(codeBird.Position, 0.25f, 4f, 24f, 0.4f);
					codeBird.speed = codeBird.dash * 300f;
					_003Ct_003E5__2 = 0.4f;
					goto IL_034c;
				case 4:
					_003C_003E1__state = -1;
					_003Ct_003E5__2 -= Engine.DeltaTime;
					goto IL_034c;
				case 5:
					{
						_003C_003E1__state = -1;
						codeBird.routine.Replace(codeBird.AimlessFlightRoutine());
						return false;
					}
					IL_00ad:
					if (_003Ct_003E5__2 > 0f)
					{
						codeBird.speed = Calc.Approach(codeBird.speed, Vector2.Zero, 200f * Engine.DeltaTime);
						codeBird.Position += codeBird.speed * Engine.DeltaTime;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					_003Cfrom_003E5__3 = codeBird.Position;
					_003Cto_003E5__4 = codeBird.origin + codeBird.dash * 8f;
					if (Math.Sign(_003Cto_003E5__4.X - _003Cfrom_003E5__3.X) != 0)
					{
						codeBird.sprite.Scale.X = Math.Sign(_003Cto_003E5__4.X - _003Cfrom_003E5__3.X);
					}
					_003Ct_003E5__2 = 0f;
					goto IL_01a5;
					IL_034c:
					if (_003Ct_003E5__2 > 0f)
					{
						if (_003Ct_003E5__2 > 0.1f && codeBird.Scene.OnInterval(0.02f))
						{
							codeBird.SceneAs<Level>().ParticlesBG.Emit(Particles[codeBird.code], 1, codeBird.Position, Vector2.One * 2f, codeBird.dash.Angle());
						}
						codeBird.speed = Calc.Approach(codeBird.speed, Vector2.Zero, 800f * Engine.DeltaTime);
						codeBird.Position += codeBird.speed * Engine.DeltaTime;
						_003C_003E2__current = null;
						_003C_003E1__state = 4;
						return true;
					}
					_003C_003E2__current = 0.4f;
					_003C_003E1__state = 5;
					return true;
					IL_01a5:
					if (_003Ct_003E5__2 < 1f)
					{
						codeBird.Position = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.CubeInOut(_003Ct_003E5__2);
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
						return true;
					}
					codeBird.Position = _003Cto_003E5__4;
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

		[CompilerGenerated]
		private sealed class _003CTransformRoutine_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CodeBird _003C_003E4__this;

			public float duration;

			private Color _003CcolorFrom_003E5__2;

			private Color _003CcolorTo_003E5__3;

			private Vector2 _003Ctarget_003E5__4;

			private float _003Ct_003E5__5;

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
			public _003CTransformRoutine_003Ed__14(int _003C_003E1__state)
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
				CodeBird codeBird = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003CcolorFrom_003E5__2 = codeBird.sprite.Color;
					_003CcolorTo_003E5__3 = Color.White;
					_003Ctarget_003E5__4 = codeBird.origin;
					codeBird.Add(codeBird.heartImage = new Image(GFX.Game["collectables/heartGem/shape"]));
					codeBird.heartImage.CenterOrigin();
					codeBird.heartImage.Scale = Vector2.Zero;
					_003Ct_003E5__5 = 0f;
					break;
				case 1:
					_003C_003E1__state = -1;
					_003Ct_003E5__5 += Engine.DeltaTime / duration;
					break;
				}
				if (_003Ct_003E5__5 < 1f)
				{
					Vector2 vector = (_003Ctarget_003E5__4 - codeBird.Position).SafeNormalize();
					codeBird.speed += 400f * vector * Engine.DeltaTime;
					float num2 = Math.Max(20f, (1f - _003Ct_003E5__5) * 200f);
					if (codeBird.speed.Length() > num2)
					{
						codeBird.speed = codeBird.speed.SafeNormalize(num2);
					}
					codeBird.Position += codeBird.speed * Engine.DeltaTime;
					codeBird.sprite.Color = Color.Lerp(_003CcolorFrom_003E5__2, _003CcolorTo_003E5__3, _003Ct_003E5__5);
					codeBird.heartImage.Scale = Vector2.One * Math.Max(0f, (_003Ct_003E5__5 - 0.75f) * 4f);
					if (vector.X != 0f)
					{
						codeBird.sprite.Scale.X = Math.Abs(codeBird.sprite.Scale.X) * (float)Math.Sign(vector.X);
					}
					codeBird.sprite.Scale.X = (float)Math.Sign(codeBird.sprite.Scale.X) * (1f - codeBird.heartImage.Scale.X);
					codeBird.sprite.Scale.Y = 1f - codeBird.heartImage.Scale.X;
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

		private Sprite sprite;

		private Coroutine routine;

		private float timer = Calc.Random.NextFloat();

		private Vector2 speed;

		private Image heartImage;

		private readonly string code;

		private readonly Vector2 origin;

		private readonly Vector2 dash;

		public CodeBird(Vector2 origin, string code)
			: base(origin)
		{
			this.code = code;
			this.origin = origin;
			Add(sprite = new Sprite(GFX.Game, "scenery/flutterbird/"));
			sprite.AddLoop("fly", "flap", 0.08f);
			sprite.Play("fly");
			sprite.CenterOrigin();
			sprite.Color = Colors[code];
			Vector2 zero = Vector2.Zero;
			zero.X = (code.Contains('L') ? (-1) : (code.Contains('R') ? 1 : 0));
			zero.Y = (code.Contains('U') ? (-1) : (code.Contains('D') ? 1 : 0));
			dash = zero.SafeNormalize();
			Add(routine = new Coroutine(AimlessFlightRoutine()));
		}

		public override void Update()
		{
			timer += Engine.DeltaTime;
			sprite.Y = (float)Math.Sin(timer * 2f);
			base.Update();
		}

		public void Dash()
		{
			routine.Replace(DashRoutine());
		}

		public void Transform(float duration)
		{
			base.Tag = Tags.FrozenUpdate;
			routine.Replace(TransformRoutine(duration));
		}

		[IteratorStateMachine(typeof(_003CAimlessFlightRoutine_003Ed__12))]
		private IEnumerator AimlessFlightRoutine()
		{
			speed = Vector2.Zero;
			while (true)
			{
				Vector2 target = origin + Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 16f + Calc.Random.NextFloat(40f));
				float reset = 0f;
				while (reset < 1f && (target - Position).Length() > 8f)
				{
					Vector2 vector = (target - Position).SafeNormalize();
					speed += vector * 420f * Engine.DeltaTime;
					if (speed.Length() > 90f)
					{
						speed = speed.SafeNormalize(90f);
					}
					Position += speed * Engine.DeltaTime;
					reset += Engine.DeltaTime;
					if (Math.Sign(vector.X) != 0)
					{
						sprite.Scale.X = Math.Sign(vector.X);
					}
					yield return null;
				}
			}
		}

		[IteratorStateMachine(typeof(_003CDashRoutine_003Ed__13))]
		private IEnumerator DashRoutine()
		{
			for (float t = 0.25f; t > 0f; t -= Engine.DeltaTime)
			{
				speed = Calc.Approach(speed, Vector2.Zero, 200f * Engine.DeltaTime);
				Position += speed * Engine.DeltaTime;
				yield return null;
			}
			Vector2 from = Position;
			Vector2 to = origin + dash * 8f;
			if (Math.Sign(to.X - from.X) != 0)
			{
				sprite.Scale.X = Math.Sign(to.X - from.X);
			}
			for (float t = 0f; t < 1f; t += Engine.DeltaTime * 1.5f)
			{
				Position = from + (to - from) * Ease.CubeInOut(t);
				yield return null;
			}
			Position = to;
			yield return 0.2f;
			if (dash.X != 0f)
			{
				sprite.Scale.X = Math.Sign(dash.X);
			}
			(base.Scene as Level).Displacement.AddBurst(Position, 0.25f, 4f, 24f, 0.4f);
			speed = dash * 300f;
			for (float t = 0.4f; t > 0f; t -= Engine.DeltaTime)
			{
				if (t > 0.1f && base.Scene.OnInterval(0.02f))
				{
					SceneAs<Level>().ParticlesBG.Emit(Particles[code], 1, Position, Vector2.One * 2f, dash.Angle());
				}
				speed = Calc.Approach(speed, Vector2.Zero, 800f * Engine.DeltaTime);
				Position += speed * Engine.DeltaTime;
				yield return null;
			}
			yield return 0.4f;
			routine.Replace(AimlessFlightRoutine());
		}

		[IteratorStateMachine(typeof(_003CTransformRoutine_003Ed__14))]
		private IEnumerator TransformRoutine(float duration)
		{
			Color colorFrom = sprite.Color;
			Color colorTo = Color.White;
			Vector2 target = origin;
			Add(heartImage = new Image(GFX.Game["collectables/heartGem/shape"]));
			heartImage.CenterOrigin();
			heartImage.Scale = Vector2.Zero;
			for (float t = 0f; t < 1f; t += Engine.DeltaTime / duration)
			{
				Vector2 vector = (target - Position).SafeNormalize();
				speed += 400f * vector * Engine.DeltaTime;
				float num = Math.Max(20f, (1f - t) * 200f);
				if (speed.Length() > num)
				{
					speed = speed.SafeNormalize(num);
				}
				Position += speed * Engine.DeltaTime;
				sprite.Color = Color.Lerp(colorFrom, colorTo, t);
				heartImage.Scale = Vector2.One * Math.Max(0f, (t - 0.75f) * 4f);
				if (vector.X != 0f)
				{
					sprite.Scale.X = Math.Abs(sprite.Scale.X) * (float)Math.Sign(vector.X);
				}
				sprite.Scale.X = (float)Math.Sign(sprite.Scale.X) * (1f - heartImage.Scale.X);
				sprite.Scale.Y = 1f - heartImage.Scale.X;
				yield return null;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CPulseRoutine_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ForsakenCitySatellite _003C_003E4__this;

		private int _003Ci_003E5__2;

		private List<CodeBird>.Enumerator _003C_003E7__wrap2;

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
		public _003CPulseRoutine_003Ed__28(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 4)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				ForsakenCitySatellite CS_0024_003C_003E8__locals25 = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals25.pulseBloom.Visible = (CS_0024_003C_003E8__locals25.pulse.Visible = false);
					break;
				case 1:
					_003C_003E1__state = -1;
					_003Ci_003E5__2 = 0;
					goto IL_01b2;
				case 2:
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals25.pulseBloom.Visible = (CS_0024_003C_003E8__locals25.pulse.Visible = false);
					Audio.Play((_003Ci_003E5__2 < Code.Length - 1) ? "event:/game/01_forsaken_city/console_static_short" : "event:/game/01_forsaken_city/console_static_long", CS_0024_003C_003E8__locals25.Position + CS_0024_003C_003E8__locals25.computer.Position);
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 3;
					return true;
				case 3:
					_003C_003E1__state = -1;
					_003Ci_003E5__2++;
					goto IL_01b2;
				case 4:
					{
						_003C_003E1__state = -3;
						goto IL_024f;
					}
					IL_024f:
					while (_003C_003E7__wrap2.MoveNext())
					{
						CodeBird current = _003C_003E7__wrap2.Current;
						if (CS_0024_003C_003E8__locals25.enabled)
						{
							current.Dash();
							_003C_003E2__current = 0.02f;
							_003C_003E1__state = 4;
							return true;
						}
					}
					_003C_003Em__Finally1();
					_003C_003E7__wrap2 = default(List<CodeBird>.Enumerator);
					break;
					IL_01b2:
					if (_003Ci_003E5__2 < Code.Length && CS_0024_003C_003E8__locals25.enabled)
					{
						CS_0024_003C_003E8__locals25.pulse.Color = (CS_0024_003C_003E8__locals25.computerScreen.Color = Colors[Code[_003Ci_003E5__2]]);
						CS_0024_003C_003E8__locals25.pulseBloom.Visible = (CS_0024_003C_003E8__locals25.pulse.Visible = true);
						Audio.Play(Sounds[Code[_003Ci_003E5__2]], CS_0024_003C_003E8__locals25.Position + CS_0024_003C_003E8__locals25.computer.Position);
						_003C_003E2__current = 0.5f;
						_003C_003E1__state = 2;
						return true;
					}
					CS_0024_003C_003E8__locals25.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
					{
						if (CS_0024_003C_003E8__locals25.enabled)
						{
							CS_0024_003C_003E8__locals25.birdThrustSfx.Position = CS_0024_003C_003E8__locals25.birdFlyPosition - CS_0024_003C_003E8__locals25.Position;
							CS_0024_003C_003E8__locals25.birdThrustSfx.Play("event:/game/01_forsaken_city/birdbros_thrust");
						}
					}, 1.1f, start: true));
					CS_0024_003C_003E8__locals25.birds.Shuffle();
					_003C_003E7__wrap2 = CS_0024_003C_003E8__locals25.birds.GetEnumerator();
					_003C_003E1__state = -3;
					goto IL_024f;
				}
				if (CS_0024_003C_003E8__locals25.enabled)
				{
					_003C_003E2__current = 2f;
					_003C_003E1__state = 1;
					return true;
				}
				CS_0024_003C_003E8__locals25.pulseBloom.Visible = (CS_0024_003C_003E8__locals25.pulse.Visible = false);
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap2/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CUnlockGem_003Ed__29 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ForsakenCitySatellite _003C_003E4__this;

		private BloomPoint _003Cbloom_003E5__2;

		private ParticleSystem _003Cparticles_003E5__3;

		private HeartGem _003Cgem_003E5__4;

		private SimpleCurve _003Ccurve_003E5__5;

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
		public _003CUnlockGem_003Ed__29(int _003C_003E1__state)
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
			ForsakenCitySatellite forsakenCitySatellite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				forsakenCitySatellite.level.Session.SetFlag("unlocked_satellite");
				forsakenCitySatellite.birdFinishSfx.Position = forsakenCitySatellite.birdFlyPosition - forsakenCitySatellite.Position;
				forsakenCitySatellite.birdFinishSfx.Play("event:/game/01_forsaken_city/birdbros_finish");
				forsakenCitySatellite.staticLoopSfx.Play("event:/game/01_forsaken_city/console_static_loop");
				forsakenCitySatellite.enabled = false;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				forsakenCitySatellite.level.Displacement.Clear();
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				forsakenCitySatellite.birdFlyingSfx.Stop();
				forsakenCitySatellite.level.Frozen = true;
				forsakenCitySatellite.Tag = Tags.FrozenUpdate;
				_003Cbloom_003E5__2 = new BloomPoint(forsakenCitySatellite.birdFlyPosition - forsakenCitySatellite.Position, 0f, 32f);
				forsakenCitySatellite.Add(_003Cbloom_003E5__2);
				foreach (CodeBird bird in forsakenCitySatellite.birds)
				{
					bird.Transform(3f);
				}
				goto IL_01b6;
			case 3:
				_003C_003E1__state = -1;
				goto IL_01b6;
			case 4:
				_003C_003E1__state = -1;
				foreach (CodeBird bird2 in forsakenCitySatellite.birds)
				{
					bird2.RemoveSelf();
				}
				_003Cparticles_003E5__3 = new ParticleSystem(-10000, 100);
				_003Cparticles_003E5__3.Tag = Tags.FrozenUpdate;
				_003Cparticles_003E5__3.Emit(BirdNPC.P_Feather, 24, forsakenCitySatellite.birdFlyPosition, new Vector2(4f, 4f));
				forsakenCitySatellite.level.Add(_003Cparticles_003E5__3);
				_003Cgem_003E5__4 = new HeartGem(forsakenCitySatellite.birdFlyPosition);
				_003Cgem_003E5__4.Tag = Tags.FrozenUpdate;
				forsakenCitySatellite.level.Add(_003Cgem_003E5__4);
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003Cgem_003E5__4.ScaleWiggler.Start();
				_003C_003E2__current = 0.85f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003Ccurve_003E5__5 = new SimpleCurve(_003Cgem_003E5__4.Position, forsakenCitySatellite.gemSpawnPosition, (_003Cgem_003E5__4.Position + forsakenCitySatellite.gemSpawnPosition) / 2f + new Vector2(0f, -64f));
				_003Ct_003E5__6 = 0f;
				goto IL_03a4;
			case 7:
				_003C_003E1__state = -1;
				_003Cgem_003E5__4.Position = _003Ccurve_003E5__5.GetPoint(Ease.CubeInOut(_003Ct_003E5__6));
				_003Ct_003E5__6 += Engine.DeltaTime;
				goto IL_03a4;
			case 8:
				{
					_003C_003E1__state = -1;
					_003Cparticles_003E5__3.RemoveSelf();
					forsakenCitySatellite.Remove(_003Cbloom_003E5__2);
					forsakenCitySatellite.level.Frozen = false;
					return false;
				}
				IL_03a4:
				if (_003Ct_003E5__6 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 8;
				return true;
				IL_01b6:
				if (_003Cbloom_003E5__2.Alpha < 1f)
				{
					_003Cbloom_003E5__2.Alpha += Engine.DeltaTime / 3f;
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 4;
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

	private const string UnlockedFlag = "unlocked_satellite";

	public static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>
	{
		{
			"U",
			Calc.HexToColor("f0f0f0")
		},
		{
			"L",
			Calc.HexToColor("9171f2")
		},
		{
			"DR",
			Calc.HexToColor("0a44e0")
		},
		{
			"UR",
			Calc.HexToColor("b32d00")
		},
		{
			"UL",
			Calc.HexToColor("ffcd37")
		}
	};

	public static readonly Dictionary<string, string> Sounds = new Dictionary<string, string>
	{
		{ "U", "event:/game/01_forsaken_city/console_white" },
		{ "L", "event:/game/01_forsaken_city/console_purple" },
		{ "DR", "event:/game/01_forsaken_city/console_blue" },
		{ "UR", "event:/game/01_forsaken_city/console_red" },
		{ "UL", "event:/game/01_forsaken_city/console_yellow" }
	};

	public static readonly Dictionary<string, ParticleType> Particles = new Dictionary<string, ParticleType>();

	private static readonly string[] Code = new string[6] { "U", "L", "DR", "UR", "L", "UL" };

	private static List<string> uniqueCodes = new List<string>();

	private bool enabled;

	private List<string> currentInputs = new List<string>();

	private List<CodeBird> birds = new List<CodeBird>();

	private Vector2 gemSpawnPosition;

	private Vector2 birdFlyPosition;

	private Image sprite;

	private Image pulse;

	private Image computer;

	private Image computerScreen;

	private Sprite computerScreenNoise;

	private Image computerScreenShine;

	private BloomPoint pulseBloom;

	private BloomPoint screenBloom;

	private Level level;

	private DashListener dashListener;

	private SoundSource birdFlyingSfx;

	private SoundSource birdThrustSfx;

	private SoundSource birdFinishSfx;

	private SoundSource staticLoopSfx;

	public ForsakenCitySatellite(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(sprite = new Image(GFX.Game["objects/citysatellite/dish"]));
		Add(pulse = new Image(GFX.Game["objects/citysatellite/light"]));
		Add(computer = new Image(GFX.Game["objects/citysatellite/computer"]));
		Add(computerScreen = new Image(GFX.Game["objects/citysatellite/computerscreen"]));
		Add(computerScreenNoise = new Sprite(GFX.Game, "objects/citysatellite/computerScreenNoise"));
		Add(computerScreenShine = new Image(GFX.Game["objects/citysatellite/computerscreenShine"]));
		sprite.JustifyOrigin(0.5f, 1f);
		pulse.JustifyOrigin(0.5f, 1f);
		Add(new Coroutine(PulseRoutine()));
		Add(pulseBloom = new BloomPoint(new Vector2(-12f, -44f), 1f, 8f));
		Add(screenBloom = new BloomPoint(new Vector2(32f, 20f), 1f, 8f));
		computerScreenNoise.AddLoop("static", "", 0.05f);
		computerScreenNoise.Play("static");
		computer.Position = (computerScreen.Position = (computerScreenShine.Position = (computerScreenNoise.Position = new Vector2(8f, 8f))));
		birdFlyPosition = offset + data.Nodes[0];
		gemSpawnPosition = offset + data.Nodes[1];
		Add(dashListener = new DashListener());
		dashListener.OnDash = delegate(Vector2 dir)
		{
			string text = "";
			if (dir.Y < 0f)
			{
				text = "U";
			}
			else if (dir.Y > 0f)
			{
				text = "D";
			}
			if (dir.X < 0f)
			{
				text += "L";
			}
			else if (dir.X > 0f)
			{
				text += "R";
			}
			currentInputs.Add(text);
			if (currentInputs.Count > Code.Length)
			{
				currentInputs.RemoveAt(0);
			}
			if (currentInputs.Count == Code.Length)
			{
				bool flag = true;
				for (int i = 0; i < Code.Length; i++)
				{
					if (!currentInputs[i].Equals(Code[i]))
					{
						flag = false;
					}
				}
				if (flag && level.Camera.Left + 32f < gemSpawnPosition.X && enabled)
				{
					Add(new Coroutine(UnlockGem()));
				}
			}
		};
		string[] code = Code;
		foreach (string item in code)
		{
			if (!uniqueCodes.Contains(item))
			{
				uniqueCodes.Add(item);
			}
		}
		base.Depth = 8999;
		Add(staticLoopSfx = new SoundSource());
		staticLoopSfx.Position = computer.Position;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = scene as Level;
		enabled = !level.Session.HeartGem && !level.Session.GetFlag("unlocked_satellite");
		if (enabled)
		{
			foreach (string uniqueCode in uniqueCodes)
			{
				CodeBird codeBird = new CodeBird(birdFlyPosition, uniqueCode);
				birds.Add(codeBird);
				level.Add(codeBird);
			}
			Add(birdFlyingSfx = new SoundSource());
			Add(birdFinishSfx = new SoundSource());
			Add(birdThrustSfx = new SoundSource());
			birdFlyingSfx.Position = birdFlyPosition - Position;
			birdFlyingSfx.Play("event:/game/01_forsaken_city/birdbros_fly_loop");
		}
		else
		{
			staticLoopSfx.Play("event:/game/01_forsaken_city/console_static_loop");
		}
		if (!level.Session.HeartGem && level.Session.GetFlag("unlocked_satellite"))
		{
			HeartGem entity = new HeartGem(gemSpawnPosition);
			level.Add(entity);
		}
	}

	public override void Update()
	{
		base.Update();
		computerScreenNoise.Visible = !pulse.Visible;
		computerScreen.Visible = pulse.Visible;
		screenBloom.Visible = pulseBloom.Visible;
	}

	[IteratorStateMachine(typeof(_003CPulseRoutine_003Ed__28))]
	private IEnumerator PulseRoutine()
	{
		BloomPoint bloomPoint = pulseBloom;
		Image image = pulse;
		bool visible = false;
		image.Visible = false;
		bloomPoint.Visible = visible;
		while (enabled)
		{
			yield return 2f;
			for (int i = 0; i < Code.Length; i++)
			{
				if (!enabled)
				{
					break;
				}
				pulse.Color = (computerScreen.Color = Colors[Code[i]]);
				BloomPoint bloomPoint2 = pulseBloom;
				Image image2 = pulse;
				visible = true;
				image2.Visible = true;
				bloomPoint2.Visible = visible;
				Audio.Play(Sounds[Code[i]], Position + computer.Position);
				yield return 0.5f;
				BloomPoint bloomPoint3 = pulseBloom;
				Image image3 = pulse;
				visible = false;
				image3.Visible = false;
				bloomPoint3.Visible = visible;
				Audio.Play((i < Code.Length - 1) ? "event:/game/01_forsaken_city/console_static_short" : "event:/game/01_forsaken_city/console_static_long", Position + computer.Position);
				yield return 0.2f;
			}
			Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
			{
				if (enabled)
				{
					birdThrustSfx.Position = birdFlyPosition - Position;
					birdThrustSfx.Play("event:/game/01_forsaken_city/birdbros_thrust");
				}
			}, 1.1f, start: true));
			birds.Shuffle();
			foreach (CodeBird bird in birds)
			{
				if (enabled)
				{
					bird.Dash();
					yield return 0.02f;
				}
			}
		}
		BloomPoint bloomPoint4 = pulseBloom;
		Image image4 = pulse;
		visible = false;
		image4.Visible = false;
		bloomPoint4.Visible = visible;
	}

	[IteratorStateMachine(typeof(_003CUnlockGem_003Ed__29))]
	private IEnumerator UnlockGem()
	{
		level.Session.SetFlag("unlocked_satellite");
		birdFinishSfx.Position = birdFlyPosition - Position;
		birdFinishSfx.Play("event:/game/01_forsaken_city/birdbros_finish");
		staticLoopSfx.Play("event:/game/01_forsaken_city/console_static_loop");
		enabled = false;
		yield return 0.25f;
		level.Displacement.Clear();
		yield return null;
		birdFlyingSfx.Stop();
		level.Frozen = true;
		base.Tag = Tags.FrozenUpdate;
		BloomPoint bloom = new BloomPoint(birdFlyPosition - Position, 0f, 32f);
		Add(bloom);
		foreach (CodeBird bird in birds)
		{
			bird.Transform(3f);
		}
		while (bloom.Alpha < 1f)
		{
			bloom.Alpha += Engine.DeltaTime / 3f;
			yield return null;
		}
		yield return 0.25f;
		foreach (CodeBird bird2 in birds)
		{
			bird2.RemoveSelf();
		}
		ParticleSystem particles = new ParticleSystem(-10000, 100);
		particles.Tag = Tags.FrozenUpdate;
		particles.Emit(BirdNPC.P_Feather, 24, birdFlyPosition, new Vector2(4f, 4f));
		level.Add(particles);
		HeartGem gem = new HeartGem(birdFlyPosition)
		{
			Tag = Tags.FrozenUpdate
		};
		level.Add(gem);
		yield return null;
		gem.ScaleWiggler.Start();
		yield return 0.85f;
		SimpleCurve curve = new SimpleCurve(gem.Position, gemSpawnPosition, (gem.Position + gemSpawnPosition) / 2f + new Vector2(0f, -64f));
		for (float t = 0f; t < 1f; t += Engine.DeltaTime)
		{
			yield return null;
			gem.Position = curve.GetPoint(Ease.CubeInOut(t));
		}
		yield return 0.5f;
		particles.RemoveSelf();
		Remove(bloom);
		level.Frozen = false;
	}
}
