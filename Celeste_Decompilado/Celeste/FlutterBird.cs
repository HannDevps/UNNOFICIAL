using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class FlutterBird : Entity
{
	[CompilerGenerated]
	private sealed class _003CIdleRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FlutterBird _003C_003E4__this;

		private Player _003Cplayer_003E5__2;

		private float _003Cdelay_003E5__3;

		private Vector2 _003Ctarget_003E5__4;

		private SimpleCurve _003Cbezier_003E5__5;

		private float _003Cp_003E5__6;

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
		public _003CIdleRoutine_003Ed__9(int _003C_003E1__state)
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
			FlutterBird flutterBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__6 += Engine.DeltaTime;
				goto IL_0117;
			case 2:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime * 4f;
					goto IL_022f;
				}
				IL_0029:
				_003Cplayer_003E5__2 = flutterBird.Scene.Tracker.GetEntity<Player>();
				_003Cdelay_003E5__3 = 0.25f + Calc.Random.NextFloat(1f);
				_003Cp_003E5__6 = 0f;
				goto IL_0117;
				IL_0117:
				if (_003Cp_003E5__6 < _003Cdelay_003E5__3)
				{
					if (_003Cplayer_003E5__2 != null && Math.Abs(_003Cplayer_003E5__2.X - flutterBird.X) < 48f && _003Cplayer_003E5__2.Y > flutterBird.Y - 40f && _003Cplayer_003E5__2.Y < flutterBird.Y + 8f)
					{
						flutterBird.FlyAway(Math.Sign(flutterBird.X - _003Cplayer_003E5__2.X), Calc.Random.NextFloat(0.2f));
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				Audio.Play("event:/game/general/birdbaby_hop", flutterBird.Position);
				_003Ctarget_003E5__4 = flutterBird.start + new Vector2(-4f + Calc.Random.NextFloat(8f), 0f);
				flutterBird.sprite.Scale.X = Math.Sign(_003Ctarget_003E5__4.X - flutterBird.Position.X);
				_003Cbezier_003E5__5 = new SimpleCurve(flutterBird.Position, _003Ctarget_003E5__4, (flutterBird.Position + _003Ctarget_003E5__4) / 2f - Vector2.UnitY * 14f);
				_003Cp_003E5__6 = 0f;
				goto IL_022f;
				IL_022f:
				if (_003Cp_003E5__6 < 1f)
				{
					flutterBird.Position = _003Cbezier_003E5__5.GetPoint(_003Cp_003E5__6);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				flutterBird.sprite.Scale.X = (float)Math.Sign(flutterBird.sprite.Scale.X) * 1.4f;
				flutterBird.sprite.Scale.Y = 0.6f;
				flutterBird.Position = _003Ctarget_003E5__4;
				_003Cplayer_003E5__2 = null;
				_003Ctarget_003E5__4 = default(Vector2);
				_003Cbezier_003E5__5 = default(SimpleCurve);
				goto IL_0029;
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
	private sealed class _003CFlyAwayRoutine_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FlutterBird _003C_003E4__this;

		public float delay;

		public int direction;

		private Level _003Clevel_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private Vector2 _003Cto_003E5__4;

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
		public _003CFlyAwayRoutine_003Ed__10(int _003C_003E1__state)
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
			FlutterBird flutterBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = flutterBird.Scene as Level;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				flutterBird.sprite.Play("fly");
				flutterBird.sprite.Scale.X = (float)(-direction) * 1.25f;
				flutterBird.sprite.Scale.Y = 1.25f;
				_003Clevel_003E5__2.ParticlesFG.Emit(Calc.Random.Choose<ParticleType>(ParticleTypes.Dust), flutterBird.Position, -(float)Math.PI / 2f);
				_003Cfrom_003E5__3 = flutterBird.Position;
				_003Cto_003E5__4 = flutterBird.Position + new Vector2(direction * 4, -8f);
				_003Cp_003E5__5 = 0f;
				goto IL_017a;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__5 += Engine.DeltaTime * 3f;
				goto IL_017a;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_017a:
				if (_003Cp_003E5__5 < 1f)
				{
					flutterBird.Position = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.CubeOut(_003Cp_003E5__5);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Cfrom_003E5__3 = default(Vector2);
				_003Cto_003E5__4 = default(Vector2);
				flutterBird.Depth = -10001;
				flutterBird.sprite.Scale.X = 0f - flutterBird.sprite.Scale.X;
				_003Cto_003E5__4 = new Vector2(direction, -4f) * 8f;
				break;
			}
			if (flutterBird.Y + 8f > (float)_003Clevel_003E5__2.Bounds.Top)
			{
				_003Cto_003E5__4 += new Vector2(direction * 64, -128f) * Engine.DeltaTime;
				flutterBird.Position += _003Cto_003E5__4 * Engine.DeltaTime;
				if (flutterBird.Scene.OnInterval(0.1f) && flutterBird.Y > _003Clevel_003E5__2.Camera.Top + 32f)
				{
					foreach (Entity entity in flutterBird.Scene.Tracker.GetEntities<FlutterBird>())
					{
						if (Math.Abs(flutterBird.X - entity.X) < 48f && Math.Abs(flutterBird.Y - entity.Y) < 48f && !(entity as FlutterBird).flyingAway)
						{
							(entity as FlutterBird).FlyAway(direction, Calc.Random.NextFloat(0.25f));
						}
					}
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			_003Cto_003E5__4 = default(Vector2);
			flutterBird.Scene.Remove(flutterBird);
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

	private static readonly Color[] colors = new Color[4]
	{
		Calc.HexToColor("89fbff"),
		Calc.HexToColor("f0fc6c"),
		Calc.HexToColor("f493ff"),
		Calc.HexToColor("93baff")
	};

	private Sprite sprite;

	private Vector2 start;

	private Coroutine routine;

	private bool flyingAway;

	private SoundSource tweetingSfx;

	private SoundSource flyawaySfx;

	public FlutterBird(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		base.Depth = -9999;
		start = Position;
		Add(sprite = GFX.SpriteBank.Create("flutterbird"));
		sprite.Color = Calc.Random.Choose(colors);
		Add(routine = new Coroutine(IdleRoutine()));
		Add(flyawaySfx = new SoundSource());
		Add(tweetingSfx = new SoundSource());
		tweetingSfx.Play("event:/game/general/birdbaby_tweet_loop");
	}

	public override void Update()
	{
		sprite.Scale.X = Calc.Approach(sprite.Scale.X, Math.Sign(sprite.Scale.X), 4f * Engine.DeltaTime);
		sprite.Scale.Y = Calc.Approach(sprite.Scale.Y, 1f, 4f * Engine.DeltaTime);
		base.Update();
	}

	[IteratorStateMachine(typeof(_003CIdleRoutine_003Ed__9))]
	private IEnumerator IdleRoutine()
	{
		while (true)
		{
			Player player = base.Scene.Tracker.GetEntity<Player>();
			float delay = 0.25f + Calc.Random.NextFloat(1f);
			for (float p = 0f; p < delay; p += Engine.DeltaTime)
			{
				if (player != null && Math.Abs(player.X - base.X) < 48f && player.Y > base.Y - 40f && player.Y < base.Y + 8f)
				{
					FlyAway(Math.Sign(base.X - player.X), Calc.Random.NextFloat(0.2f));
				}
				yield return null;
			}
			Audio.Play("event:/game/general/birdbaby_hop", Position);
			Vector2 target = start + new Vector2(-4f + Calc.Random.NextFloat(8f), 0f);
			sprite.Scale.X = Math.Sign(target.X - Position.X);
			SimpleCurve bezier = new SimpleCurve(Position, target, (Position + target) / 2f - Vector2.UnitY * 14f);
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
			{
				Position = bezier.GetPoint(p);
				yield return null;
			}
			sprite.Scale.X = (float)Math.Sign(sprite.Scale.X) * 1.4f;
			sprite.Scale.Y = 0.6f;
			Position = target;
		}
	}

	[IteratorStateMachine(typeof(_003CFlyAwayRoutine_003Ed__10))]
	private IEnumerator FlyAwayRoutine(int direction, float delay)
	{
		Level level = base.Scene as Level;
		yield return delay;
		sprite.Play("fly");
		sprite.Scale.X = (float)(-direction) * 1.25f;
		sprite.Scale.Y = 1.25f;
		level.ParticlesFG.Emit(Calc.Random.Choose<ParticleType>(ParticleTypes.Dust), Position, -(float)Math.PI / 2f);
		Vector2 from = Position;
		Vector2 to = Position + new Vector2(direction * 4, -8f);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 3f)
		{
			Position = from + (to - from) * Ease.CubeOut(p);
			yield return null;
		}
		base.Depth = -10001;
		sprite.Scale.X = 0f - sprite.Scale.X;
		to = new Vector2(direction, -4f) * 8f;
		while (base.Y + 8f > (float)level.Bounds.Top)
		{
			to += new Vector2(direction * 64, -128f) * Engine.DeltaTime;
			Position += to * Engine.DeltaTime;
			if (base.Scene.OnInterval(0.1f) && base.Y > level.Camera.Top + 32f)
			{
				foreach (Entity entity in base.Scene.Tracker.GetEntities<FlutterBird>())
				{
					if (Math.Abs(base.X - entity.X) < 48f && Math.Abs(base.Y - entity.Y) < 48f && !(entity as FlutterBird).flyingAway)
					{
						(entity as FlutterBird).FlyAway(direction, Calc.Random.NextFloat(0.25f));
					}
				}
			}
			yield return null;
		}
		base.Scene.Remove(this);
	}

	public void FlyAway(int direction, float delay)
	{
		if (!flyingAway)
		{
			tweetingSfx.Stop();
			flyingAway = true;
			flyawaySfx.Play("event:/game/general/birdbaby_flyaway");
			Remove(routine);
			Add(routine = new Coroutine(FlyAwayRoutine(direction, delay)));
		}
	}
}
