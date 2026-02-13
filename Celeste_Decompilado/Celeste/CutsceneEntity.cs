using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public abstract class CutsceneEntity : Entity
{
	[CompilerGenerated]
	private sealed class _003CCameraTo_003Ed__20 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Ease.Easer ease;

		public float delay;

		public Vector2 target;

		public float duration;

		private Level _003Clevel_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

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
		public _003CCameraTo_003Ed__20(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (ease == null)
				{
					ease = Ease.CubeInOut;
				}
				if (delay > 0f)
				{
					_003C_003E2__current = delay;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0063;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0063;
			case 2:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__4 += Engine.DeltaTime / duration;
					break;
				}
				IL_0063:
				_003Clevel_003E5__2 = Engine.Scene as Level;
				_003Cfrom_003E5__3 = _003Clevel_003E5__2.Camera.Position;
				_003Cp_003E5__4 = 0f;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
			{
				_003Clevel_003E5__2.Camera.Position = _003Cfrom_003E5__3 + (target - _003Cfrom_003E5__3) * ease(_003Cp_003E5__4);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			_003Clevel_003E5__2.Camera.Position = target;
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

	public bool WasSkipped;

	public bool RemoveOnSkipped = true;

	public bool EndingChapterAfter;

	public Level Level;

	public bool Running { get; private set; }

	public bool FadeInOnSkip { get; private set; }

	public CutsceneEntity(bool fadeInOnSkip = true, bool endingChapterAfter = false)
	{
		FadeInOnSkip = fadeInOnSkip;
		EndingChapterAfter = endingChapterAfter;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Level = scene as Level;
		Start();
	}

	public void Start()
	{
		Running = true;
		Level.StartCutscene(SkipCutscene, FadeInOnSkip, EndingChapterAfter);
		OnBegin(Level);
	}

	public override void Update()
	{
		if (Level.RetryPlayerCorpse != null)
		{
			Active = false;
		}
		else
		{
			base.Update();
		}
	}

	private void SkipCutscene(Level level)
	{
		WasSkipped = true;
		EndCutscene(level, RemoveOnSkipped);
	}

	public void EndCutscene(Level level, bool removeSelf = true)
	{
		Running = false;
		OnEnd(level);
		level.EndCutscene();
		if (removeSelf)
		{
			RemoveSelf();
		}
	}

	public abstract void OnBegin(Level level);

	public abstract void OnEnd(Level level);

	[IteratorStateMachine(typeof(_003CCameraTo_003Ed__20))]
	public static IEnumerator CameraTo(Vector2 target, float duration, Ease.Easer ease = null, float delay = 0f)
	{
		if (ease == null)
		{
			ease = Ease.CubeInOut;
		}
		if (delay > 0f)
		{
			yield return delay;
		}
		Level level = Engine.Scene as Level;
		Vector2 from = level.Camera.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			level.Camera.Position = from + (target - from) * ease(p);
			yield return null;
		}
		level.Camera.Position = target;
	}
}
