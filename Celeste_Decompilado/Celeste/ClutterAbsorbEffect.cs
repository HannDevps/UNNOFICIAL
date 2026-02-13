using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ClutterAbsorbEffect : Entity
{
	[CompilerGenerated]
	private sealed class _003CFlyClutterRoutine_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public ClutterAbsorbEffect _003C_003E4__this;

		public Image img;

		public bool shake;

		private ClutterCabinet _003Ccabinet_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private SimpleCurve _003Ccurve_003E5__4;

		private float _003Ctime_003E5__5;

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
		public _003CFlyClutterRoutine_003Ed__5(int _003C_003E1__state)
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
			ClutterAbsorbEffect clutterAbsorbEffect = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				_003Ccabinet_003E5__2 = Calc.Random.Choose(clutterAbsorbEffect.cabinets);
				Vector2 vector = _003Ccabinet_003E5__2.Position + new Vector2(8f);
				_003Cfrom_003E5__3 = img.Position;
				Vector2 vector2 = vector + new Vector2(Calc.Random.Next(16) - 8, Calc.Random.Next(4) - 2);
				Vector2 vector3 = (vector2 - _003Cfrom_003E5__3).SafeNormalize();
				float num2 = (vector2 - _003Cfrom_003E5__3).Length();
				Vector2 vector4 = new Vector2(0f - vector3.Y, vector3.X) * (num2 / 4f + Calc.Random.NextFloat(40f)) * ((!Calc.Random.Chance(0.5f)) ? 1 : (-1));
				_003Ccurve_003E5__4 = new SimpleCurve(_003Cfrom_003E5__3, vector2, (vector2 + _003Cfrom_003E5__3) / 2f + vector4);
				if (shake)
				{
					_003Ctime_003E5__5 = 0.25f;
					goto IL_01ee;
				}
				goto IL_01fe;
			}
			case 2:
				_003C_003E1__state = -1;
				_003Ctime_003E5__5 -= Engine.DeltaTime;
				goto IL_01ee;
			case 3:
				{
					_003C_003E1__state = -1;
					_003Ctime_003E5__5 += Engine.DeltaTime;
					break;
				}
				IL_01fe:
				_003Ctime_003E5__5 = 0f;
				break;
				IL_01ee:
				if (_003Ctime_003E5__5 > 0f)
				{
					img.X = _003Cfrom_003E5__3.X + (float)Calc.Random.Next(3) - 1f;
					img.Y = _003Cfrom_003E5__3.Y + (float)Calc.Random.Next(3) - 1f;
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_01fe;
			}
			if (_003Ctime_003E5__5 < 1f)
			{
				img.Position = _003Ccurve_003E5__4.GetPoint(Ease.CubeInOut(_003Ctime_003E5__5));
				img.Scale = Vector2.One * Ease.CubeInOut(1f - _003Ctime_003E5__5 * 0.5f);
				if (_003Ctime_003E5__5 > 0.5f && !_003Ccabinet_003E5__2.Opened)
				{
					_003Ccabinet_003E5__2.Open();
				}
				if (clutterAbsorbEffect.level.OnInterval(0.25f))
				{
					clutterAbsorbEffect.level.ParticlesFG.Emit(ClutterSwitch.P_ClutterFly, img.Position);
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			clutterAbsorbEffect.Remove(img);
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
	private sealed class _003CCloseCabinetsRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClutterAbsorbEffect _003C_003E4__this;

		private int _003Ci_003E5__2;

		private List<ClutterCabinet>.Enumerator _003C_003E7__wrap2;

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
		public _003CCloseCabinetsRoutine_003Ed__7(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 1)
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
				ClutterAbsorbEffect clutterAbsorbEffect = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					clutterAbsorbEffect.cabinets.Sort((ClutterCabinet a, ClutterCabinet b) => (Math.Abs(a.Y - b.Y) < 24f) ? Math.Sign(a.X - b.X) : Math.Sign(a.Y - b.Y));
					_003Ci_003E5__2 = 0;
					_003C_003E7__wrap2 = clutterAbsorbEffect.cabinets.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				while (_003C_003E7__wrap2.MoveNext())
				{
					_003C_003E7__wrap2.Current.Close();
					if (_003Ci_003E5__2++ % 3 == 0)
					{
						_003C_003E2__current = 0.1f;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap2 = default(List<ClutterCabinet>.Enumerator);
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

	private Level level;

	private List<ClutterCabinet> cabinets = new List<ClutterCabinet>();

	public ClutterAbsorbEffect()
	{
		Position = Vector2.Zero;
		base.Tag = Tags.TransitionUpdate;
		base.Depth = -10001;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = SceneAs<Level>();
		foreach (Entity entity in level.Tracker.GetEntities<ClutterCabinet>())
		{
			cabinets.Add(entity as ClutterCabinet);
		}
	}

	public void FlyClutter(Vector2 position, MTexture texture, bool shake, float delay)
	{
		Image image = new Image(texture);
		image.Position = position - Position;
		image.CenterOrigin();
		Add(image);
		Coroutine coroutine = new Coroutine(FlyClutterRoutine(image, shake, delay));
		coroutine.RemoveOnComplete = true;
		Add(coroutine);
	}

	[IteratorStateMachine(typeof(_003CFlyClutterRoutine_003Ed__5))]
	private IEnumerator FlyClutterRoutine(Image img, bool shake, float delay)
	{
		yield return delay;
		ClutterCabinet cabinet = Calc.Random.Choose(cabinets);
		Vector2 vector = cabinet.Position + new Vector2(8f);
		Vector2 from = img.Position;
		Vector2 vector2 = vector + new Vector2(Calc.Random.Next(16) - 8, Calc.Random.Next(4) - 2);
		Vector2 vector3 = (vector2 - from).SafeNormalize();
		float num = (vector2 - from).Length();
		Vector2 vector4 = new Vector2(0f - vector3.Y, vector3.X) * (num / 4f + Calc.Random.NextFloat(40f)) * ((!Calc.Random.Chance(0.5f)) ? 1 : (-1));
		SimpleCurve curve = new SimpleCurve(from, vector2, (vector2 + from) / 2f + vector4);
		if (shake)
		{
			for (float time = 0.25f; time > 0f; time -= Engine.DeltaTime)
			{
				img.X = from.X + (float)Calc.Random.Next(3) - 1f;
				img.Y = from.Y + (float)Calc.Random.Next(3) - 1f;
				yield return null;
			}
		}
		for (float time = 0f; time < 1f; time += Engine.DeltaTime)
		{
			img.Position = curve.GetPoint(Ease.CubeInOut(time));
			img.Scale = Vector2.One * Ease.CubeInOut(1f - time * 0.5f);
			if (time > 0.5f && !cabinet.Opened)
			{
				cabinet.Open();
			}
			if (level.OnInterval(0.25f))
			{
				level.ParticlesFG.Emit(ClutterSwitch.P_ClutterFly, img.Position);
			}
			yield return null;
		}
		Remove(img);
	}

	public void CloseCabinets()
	{
		Add(new Coroutine(CloseCabinetsRoutine()));
	}

	[IteratorStateMachine(typeof(_003CCloseCabinetsRoutine_003Ed__7))]
	private IEnumerator CloseCabinetsRoutine()
	{
		cabinets.Sort((ClutterCabinet a, ClutterCabinet b) => (Math.Abs(a.Y - b.Y) < 24f) ? Math.Sign(a.X - b.X) : Math.Sign(a.Y - b.Y));
		int i = 0;
		foreach (ClutterCabinet cabinet in cabinets)
		{
			cabinet.Close();
			if (i++ % 3 == 0)
			{
				yield return 0.1f;
			}
		}
	}
}
