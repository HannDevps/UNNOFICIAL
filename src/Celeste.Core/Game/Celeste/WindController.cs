using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class WindController : Entity
{
	public enum Patterns
	{
		None,
		Left,
		Right,
		LeftStrong,
		RightStrong,
		LeftOnOff,
		RightOnOff,
		LeftOnOffFast,
		RightOnOffFast,
		Alternating,
		LeftGemsOnly,
		RightCrazy,
		Down,
		Up,
		Space
	}

	[CompilerGenerated]
	private sealed class _003CAlternatingSequence_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WindController _003C_003E4__this;

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
		public _003CAlternatingSequence_003Ed__21(int _003C_003E1__state)
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
			WindController windController = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0031;
			case 1:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 0f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 400f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 3f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 0f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_0031;
				}
				IL_0031:
				windController.targetSpeed.X = -400f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 3f;
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
	private sealed class _003CRightOnOffSequence_003Ed__22 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WindController _003C_003E4__this;

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
		public _003CRightOnOffSequence_003Ed__22(int _003C_003E1__state)
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
			WindController windController = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 0f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 3f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				windController.targetSpeed.X = 800f;
				windController.SetAmbienceStrength(strong: true);
				_003C_003E2__current = 3f;
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
	private sealed class _003CLeftOnOffSequence_003Ed__23 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WindController _003C_003E4__this;

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
		public _003CLeftOnOffSequence_003Ed__23(int _003C_003E1__state)
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
			WindController windController = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 0f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 3f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				windController.targetSpeed.X = -800f;
				windController.SetAmbienceStrength(strong: true);
				_003C_003E2__current = 3f;
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
	private sealed class _003CRightOnOffFastSequence_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WindController _003C_003E4__this;

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
		public _003CRightOnOffFastSequence_003Ed__24(int _003C_003E1__state)
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
			WindController windController = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 0f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				windController.targetSpeed.X = 800f;
				windController.SetAmbienceStrength(strong: true);
				_003C_003E2__current = 2f;
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
	private sealed class _003CLeftOnOffFastSequence_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WindController _003C_003E4__this;

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
		public _003CLeftOnOffFastSequence_003Ed__25(int _003C_003E1__state)
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
			WindController windController = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				windController.targetSpeed.X = 0f;
				windController.SetAmbienceStrength(strong: false);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				windController.targetSpeed.X = -800f;
				windController.SetAmbienceStrength(strong: true);
				_003C_003E2__current = 2f;
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

	private const float Weak = 400f;

	private const float Strong = 800f;

	private const float Crazy = 1200f;

	private const float Accel = 1000f;

	private const float Down = 300f;

	private const float Up = -400f;

	private const float Space = -600f;

	private Level level;

	private Patterns pattern;

	private Vector2 targetSpeed;

	private Coroutine coroutine;

	private Patterns startPattern;

	private bool everSetPattern;

	public WindController(Patterns pattern)
	{
		base.Tag = Tags.TransitionUpdate;
		startPattern = pattern;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = SceneAs<Level>();
	}

	public void SetStartPattern()
	{
		if (!everSetPattern)
		{
			SetPattern(startPattern);
		}
	}

	public void SetPattern(Patterns pattern)
	{
		if (this.pattern != pattern || !everSetPattern)
		{
			everSetPattern = true;
			this.pattern = pattern;
			if (coroutine != null)
			{
				Remove(coroutine);
				coroutine = null;
			}
			switch (pattern)
			{
			case Patterns.None:
				targetSpeed = Vector2.Zero;
				SetAmbienceStrength(strong: false);
				break;
			case Patterns.Left:
				targetSpeed.X = -400f;
				SetAmbienceStrength(strong: false);
				break;
			case Patterns.Right:
				targetSpeed.X = 400f;
				SetAmbienceStrength(strong: false);
				break;
			case Patterns.LeftStrong:
				targetSpeed.X = -800f;
				SetAmbienceStrength(strong: true);
				break;
			case Patterns.RightStrong:
				targetSpeed.X = 800f;
				SetAmbienceStrength(strong: true);
				break;
			case Patterns.Alternating:
				Add(coroutine = new Coroutine(AlternatingSequence()));
				break;
			case Patterns.RightOnOff:
				Add(coroutine = new Coroutine(RightOnOffSequence()));
				break;
			case Patterns.LeftOnOff:
				Add(coroutine = new Coroutine(LeftOnOffSequence()));
				break;
			case Patterns.RightOnOffFast:
				Add(coroutine = new Coroutine(RightOnOffFastSequence()));
				break;
			case Patterns.LeftOnOffFast:
				Add(coroutine = new Coroutine(LeftOnOffFastSequence()));
				break;
			case Patterns.RightCrazy:
				targetSpeed.X = 1200f;
				SetAmbienceStrength(strong: true);
				break;
			case Patterns.Down:
				targetSpeed.Y = 300f;
				SetAmbienceStrength(strong: false);
				break;
			case Patterns.Up:
				targetSpeed.Y = -400f;
				SetAmbienceStrength(strong: false);
				break;
			case Patterns.Space:
				targetSpeed.Y = -600f;
				SetAmbienceStrength(strong: false);
				break;
			case Patterns.LeftGemsOnly:
				break;
			}
		}
	}

	private void SetAmbienceStrength(bool strong)
	{
		int num = 0;
		if (targetSpeed.X != 0f)
		{
			num = Math.Sign(targetSpeed.X);
		}
		else if (targetSpeed.Y != 0f)
		{
			num = Math.Sign(targetSpeed.Y);
		}
		Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "wind_direction", num);
		Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "strong_wind", strong ? 1 : 0);
	}

	public void SnapWind()
	{
		if (coroutine != null && coroutine.Active)
		{
			coroutine.Update();
		}
		level.Wind = targetSpeed;
	}

	public override void Update()
	{
		base.Update();
		if (pattern == Patterns.LeftGemsOnly)
		{
			bool flag = false;
			foreach (StrawberrySeed entity in base.Scene.Tracker.GetEntities<StrawberrySeed>())
			{
				if (entity.Collected)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				targetSpeed.X = -400f;
				SetAmbienceStrength(strong: false);
			}
			else
			{
				targetSpeed.X = 0f;
				SetAmbienceStrength(strong: false);
			}
		}
		level.Wind = Calc.Approach(level.Wind, targetSpeed, 1000f * Engine.DeltaTime);
		if (!(level.Wind != Vector2.Zero) || level.Transitioning)
		{
			return;
		}
		foreach (WindMover component in base.Scene.Tracker.GetComponents<WindMover>())
		{
			component.Move(level.Wind * 0.1f * Engine.DeltaTime);
		}
	}

	[IteratorStateMachine(typeof(_003CAlternatingSequence_003Ed__21))]
	private IEnumerator AlternatingSequence()
	{
		while (true)
		{
			targetSpeed.X = -400f;
			SetAmbienceStrength(strong: false);
			yield return 3f;
			targetSpeed.X = 0f;
			SetAmbienceStrength(strong: false);
			yield return 2f;
			targetSpeed.X = 400f;
			SetAmbienceStrength(strong: false);
			yield return 3f;
			targetSpeed.X = 0f;
			SetAmbienceStrength(strong: false);
			yield return 2f;
		}
	}

	[IteratorStateMachine(typeof(_003CRightOnOffSequence_003Ed__22))]
	private IEnumerator RightOnOffSequence()
	{
		while (true)
		{
			targetSpeed.X = 800f;
			SetAmbienceStrength(strong: true);
			yield return 3f;
			targetSpeed.X = 0f;
			SetAmbienceStrength(strong: false);
			yield return 3f;
		}
	}

	[IteratorStateMachine(typeof(_003CLeftOnOffSequence_003Ed__23))]
	private IEnumerator LeftOnOffSequence()
	{
		while (true)
		{
			targetSpeed.X = -800f;
			SetAmbienceStrength(strong: true);
			yield return 3f;
			targetSpeed.X = 0f;
			SetAmbienceStrength(strong: false);
			yield return 3f;
		}
	}

	[IteratorStateMachine(typeof(_003CRightOnOffFastSequence_003Ed__24))]
	private IEnumerator RightOnOffFastSequence()
	{
		while (true)
		{
			targetSpeed.X = 800f;
			SetAmbienceStrength(strong: true);
			yield return 2f;
			targetSpeed.X = 0f;
			SetAmbienceStrength(strong: false);
			yield return 2f;
		}
	}

	[IteratorStateMachine(typeof(_003CLeftOnOffFastSequence_003Ed__25))]
	private IEnumerator LeftOnOffFastSequence()
	{
		while (true)
		{
			targetSpeed.X = -800f;
			SetAmbienceStrength(strong: true);
			yield return 2f;
			targetSpeed.X = 0f;
			SetAmbienceStrength(strong: false);
			yield return 2f;
		}
	}
}
