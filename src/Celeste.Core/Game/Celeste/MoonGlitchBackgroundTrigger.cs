using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class MoonGlitchBackgroundTrigger : Trigger
{
	private enum Duration
	{
		Short,
		Medium,
		Long
	}

	[CompilerGenerated]
	private sealed class _003CInternalGlitchRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MoonGlitchBackgroundTrigger _003C_003E4__this;

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
		public _003CInternalGlitchRoutine_003Ed__9(int _003C_003E1__state)
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
			MoonGlitchBackgroundTrigger moonGlitchBackgroundTrigger = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				moonGlitchBackgroundTrigger.running = true;
				moonGlitchBackgroundTrigger.Tag = Tags.Persistent;
				float num2 = 0f;
				if (moonGlitchBackgroundTrigger.duration == Duration.Short)
				{
					num2 = 0.2f;
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					Audio.Play("event:/new_content/game/10_farewell/glitch_short");
				}
				else if (moonGlitchBackgroundTrigger.duration == Duration.Medium)
				{
					num2 = 0.5f;
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					Audio.Play("event:/new_content/game/10_farewell/glitch_medium");
				}
				else
				{
					num2 = 1.25f;
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
					Audio.Play("event:/new_content/game/10_farewell/glitch_long");
				}
				_003C_003E2__current = GlitchRoutine(num2, moonGlitchBackgroundTrigger.stayOn);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				moonGlitchBackgroundTrigger.Tag = 0;
				moonGlitchBackgroundTrigger.running = false;
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
	private sealed class _003CGlitchRoutine_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public bool stayOn;

		private float _003Ca_003E5__2;

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
		public _003CGlitchRoutine_003Ed__12(int _003C_003E1__state)
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
				Toggle(on: true);
				if (Settings.Instance.DisableFlashes)
				{
					_003Ca_003E5__2 = 0f;
					goto IL_0093;
				}
				if (duration > 0.4f)
				{
					Glitch.Value = 0.3f;
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 4;
					return true;
				}
				Glitch.Value = 0.3f;
				_003C_003E2__current = duration;
				_003C_003E1__state = 7;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Ca_003E5__2 += Engine.DeltaTime / 0.1f;
				goto IL_0093;
			case 2:
				_003C_003E1__state = -1;
				if (stayOn)
				{
					break;
				}
				_003Ca_003E5__2 = 0f;
				goto IL_0125;
			case 3:
				_003C_003E1__state = -1;
				_003Ca_003E5__2 += Engine.DeltaTime / 0.1f;
				goto IL_0125;
			case 4:
				_003C_003E1__state = -1;
				Glitch.Value = 0f;
				_003C_003E2__current = duration - 0.4f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				if (!stayOn)
				{
					Glitch.Value = 0.3f;
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				Glitch.Value = 0f;
				break;
			case 7:
				{
					_003C_003E1__state = -1;
					Glitch.Value = 0f;
					break;
				}
				IL_0093:
				if (_003Ca_003E5__2 < 1f)
				{
					Fade(_003Ca_003E5__2, max: true);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				Fade(1f);
				_003C_003E2__current = duration;
				_003C_003E1__state = 2;
				return true;
				IL_0125:
				if (_003Ca_003E5__2 < 1f)
				{
					Fade(1f - _003Ca_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				Fade(1f);
				break;
			}
			if (!stayOn)
			{
				Toggle(on: false);
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

	private Duration duration;

	private bool triggered;

	private bool stayOn;

	private bool running;

	private bool doGlitch;

	public MoonGlitchBackgroundTrigger(EntityData data, Vector2 offset)
		: base(data, offset)
	{
		duration = data.Enum("duration", Duration.Short);
		stayOn = data.Bool("stay");
		doGlitch = data.Bool("glitch", defaultValue: true);
	}

	public override void OnEnter(Player player)
	{
		Invoke();
	}

	public void Invoke()
	{
		if (!triggered)
		{
			triggered = true;
			if (doGlitch)
			{
				Add(new Coroutine(InternalGlitchRoutine()));
			}
			else if (!stayOn)
			{
				Toggle(on: false);
			}
		}
	}

	[IteratorStateMachine(typeof(_003CInternalGlitchRoutine_003Ed__9))]
	private IEnumerator InternalGlitchRoutine()
	{
		running = true;
		base.Tag = Tags.Persistent;
		float num;
		if (duration == Duration.Short)
		{
			num = 0.2f;
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			Audio.Play("event:/new_content/game/10_farewell/glitch_short");
		}
		else if (duration == Duration.Medium)
		{
			num = 0.5f;
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			Audio.Play("event:/new_content/game/10_farewell/glitch_medium");
		}
		else
		{
			num = 1.25f;
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
			Audio.Play("event:/new_content/game/10_farewell/glitch_long");
		}
		yield return GlitchRoutine(num, stayOn);
		base.Tag = 0;
		running = false;
	}

	private static void Toggle(bool on)
	{
		Level level = Engine.Scene as Level;
		foreach (Backdrop item in level.Background.GetEach<Backdrop>("blackhole"))
		{
			item.ForceVisible = on;
		}
		foreach (Backdrop item2 in level.Foreground.GetEach<Backdrop>("blackhole"))
		{
			item2.ForceVisible = on;
		}
	}

	private static void Fade(float alpha, bool max = false)
	{
		Level level = Engine.Scene as Level;
		foreach (Backdrop item in level.Background.GetEach<Backdrop>("blackhole"))
		{
			item.FadeAlphaMultiplier = (max ? Math.Max(item.FadeAlphaMultiplier, alpha) : alpha);
		}
		foreach (Backdrop item2 in level.Foreground.GetEach<Backdrop>("blackhole"))
		{
			item2.FadeAlphaMultiplier = (max ? Math.Max(item2.FadeAlphaMultiplier, alpha) : alpha);
		}
	}

	[IteratorStateMachine(typeof(_003CGlitchRoutine_003Ed__12))]
	public static IEnumerator GlitchRoutine(float duration, bool stayOn)
	{
		Toggle(on: true);
		if (Settings.Instance.DisableFlashes)
		{
			for (float a = 0f; a < 1f; a += Engine.DeltaTime / 0.1f)
			{
				Fade(a, max: true);
				yield return null;
			}
			Fade(1f);
			yield return duration;
			if (!stayOn)
			{
				for (float a = 0f; a < 1f; a += Engine.DeltaTime / 0.1f)
				{
					Fade(1f - a);
					yield return null;
				}
				Fade(1f);
			}
		}
		else if (duration > 0.4f)
		{
			Glitch.Value = 0.3f;
			yield return 0.2f;
			Glitch.Value = 0f;
			yield return duration - 0.4f;
			if (!stayOn)
			{
				Glitch.Value = 0.3f;
			}
			yield return 0.2f;
			Glitch.Value = 0f;
		}
		else
		{
			Glitch.Value = 0.3f;
			yield return duration;
			Glitch.Value = 0f;
		}
		if (!stayOn)
		{
			Toggle(on: false);
		}
	}

	public override void Removed(Scene scene)
	{
		if (running)
		{
			Glitch.Value = 0f;
			Fade(1f);
			if (!stayOn)
			{
				Toggle(on: false);
			}
		}
		base.Removed(scene);
	}
}
