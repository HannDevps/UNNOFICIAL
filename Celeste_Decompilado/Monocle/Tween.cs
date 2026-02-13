using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Monocle;

public class Tween : Component
{
	public enum TweenMode
	{
		Persist,
		Oneshot,
		Looping,
		YoyoOneshot,
		YoyoLooping
	}

	[CompilerGenerated]
	private sealed class _003CWait_003Ed__45 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Tween _003C_003E4__this;

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
		public _003CWait_003Ed__45(int _003C_003E1__state)
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
			Tween tween = _003C_003E4__this;
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
			if (tween.Active)
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

	public Ease.Easer Easer;

	public Action<Tween> OnUpdate;

	public Action<Tween> OnComplete;

	public Action<Tween> OnStart;

	public bool UseRawDeltaTime;

	private bool startedReversed;

	private ulong cachedFrame;

	private static List<Tween> cached = new List<Tween>();

	public TweenMode Mode { get; private set; }

	public float Duration { get; private set; }

	public float TimeLeft { get; private set; }

	public float Percent { get; private set; }

	public float Eased { get; private set; }

	public bool Reverse { get; private set; }

	public float Inverted => 1f - Eased;

	public static Tween Create(TweenMode mode, Ease.Easer easer = null, float duration = 1f, bool start = false)
	{
		Tween tween = null;
		foreach (Tween item in cached)
		{
			if (Engine.FrameCounter > item.cachedFrame + 3)
			{
				tween = item;
				cached.Remove(item);
				break;
			}
		}
		if (tween == null)
		{
			tween = new Tween();
		}
		tween.OnUpdate = (tween.OnComplete = (tween.OnStart = null));
		tween.Init(mode, easer, duration, start);
		return tween;
	}

	public static Tween Set(Entity entity, TweenMode tweenMode, float duration, Ease.Easer easer, Action<Tween> onUpdate, Action<Tween> onComplete = null)
	{
		Tween tween = Create(tweenMode, easer, duration, start: true);
		tween.OnUpdate = (Action<Tween>)Delegate.Combine(tween.OnUpdate, onUpdate);
		tween.OnComplete = (Action<Tween>)Delegate.Combine(tween.OnComplete, onComplete);
		entity.Add(tween);
		return tween;
	}

	public static Tween Position(Entity entity, Vector2 targetPosition, float duration, Ease.Easer easer, TweenMode tweenMode = TweenMode.Oneshot)
	{
		Vector2 startPosition = entity.Position;
		Tween tween = Create(tweenMode, easer, duration, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			entity.Position = Vector2.Lerp(startPosition, targetPosition, t.Eased);
		};
		entity.Add(tween);
		return tween;
	}

	private Tween()
		: base(active: false, visible: false)
	{
	}

	private void Init(TweenMode mode, Ease.Easer easer, float duration, bool start)
	{
		if (duration <= 0f)
		{
			duration = 1E-06f;
		}
		UseRawDeltaTime = false;
		Mode = mode;
		Easer = easer;
		Duration = duration;
		TimeLeft = 0f;
		Percent = 0f;
		Active = false;
		if (start)
		{
			Start();
		}
	}

	public override void Removed(Entity entity)
	{
		base.Removed(entity);
		cached.Add(this);
		cachedFrame = Engine.FrameCounter;
	}

	public override void Update()
	{
		TimeLeft -= (UseRawDeltaTime ? Engine.RawDeltaTime : Engine.DeltaTime);
		Percent = Math.Max(0f, TimeLeft) / Duration;
		if (!Reverse)
		{
			Percent = 1f - Percent;
		}
		if (Easer != null)
		{
			Eased = Easer(Percent);
		}
		else
		{
			Eased = Percent;
		}
		if (OnUpdate != null)
		{
			OnUpdate(this);
		}
		if (!(TimeLeft <= 0f))
		{
			return;
		}
		TimeLeft = 0f;
		if (OnComplete != null)
		{
			OnComplete(this);
		}
		switch (Mode)
		{
		case TweenMode.Persist:
			Active = false;
			break;
		case TweenMode.Oneshot:
			Active = false;
			RemoveSelf();
			break;
		case TweenMode.Looping:
			Start(Reverse);
			break;
		case TweenMode.YoyoOneshot:
			if (Reverse == startedReversed)
			{
				Start(!Reverse);
				startedReversed = !Reverse;
			}
			else
			{
				Active = false;
				RemoveSelf();
			}
			break;
		case TweenMode.YoyoLooping:
			Start(!Reverse);
			break;
		}
	}

	public void Start()
	{
		Start(reverse: false);
	}

	public void Start(bool reverse)
	{
		bool flag = (Reverse = reverse);
		startedReversed = flag;
		TimeLeft = Duration;
		float eased = (Percent = (Reverse ? 1 : 0));
		Eased = eased;
		Active = true;
		if (OnStart != null)
		{
			OnStart(this);
		}
	}

	public void Start(float duration, bool reverse = false)
	{
		Duration = duration;
		Start(reverse);
	}

	public void Stop()
	{
		Active = false;
	}

	public void Reset()
	{
		TimeLeft = Duration;
		float eased = (Percent = (Reverse ? 1 : 0));
		Eased = eased;
	}

	[IteratorStateMachine(typeof(_003CWait_003Ed__45))]
	public IEnumerator Wait()
	{
		while (Active)
		{
			yield return null;
		}
	}
}
