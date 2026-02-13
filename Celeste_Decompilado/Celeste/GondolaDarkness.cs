using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class GondolaDarkness : Entity
{
	private class Blackness : Entity
	{
		public float Fade;

		public Blackness()
		{
			base.Depth = 9001;
		}

		public override void Render()
		{
			base.Render();
			Camera camera = (base.Scene as Level).Camera;
			Draw.Rect(camera.Left - 1f, camera.Top - 1f, 322f, 182f, Color.Black * Fade);
		}
	}

	[CompilerGenerated]
	private sealed class _003CAppear_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GondolaDarkness _003C_003E4__this;

		public WindSnowFG windSnowFG;

		private float _003Ct_003E5__2;

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
		public _003CAppear_003Ed__7(int _003C_003E1__state)
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
			GondolaDarkness gondolaDarkness = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				gondolaDarkness.windSnowFG = windSnowFG;
				gondolaDarkness.Visible = true;
				gondolaDarkness.Scene.Add(gondolaDarkness.blackness = new Blackness());
				_003Ct_003E5__2 = 0f;
				goto IL_00cd;
			case 1:
				_003C_003E1__state = -1;
				gondolaDarkness.blackness.Fade = _003Ct_003E5__2;
				gondolaDarkness.anxiety = _003Ct_003E5__2;
				if (windSnowFG != null)
				{
					windSnowFG.Alpha = 1f - _003Ct_003E5__2;
				}
				_003Ct_003E5__2 += Engine.DeltaTime / 2f;
				goto IL_00cd;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00cd:
				if (_003Ct_003E5__2 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = null;
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
	private sealed class _003CExpand_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GondolaDarkness _003C_003E4__this;

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
		public _003CExpand_003Ed__8(int _003C_003E1__state)
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
			GondolaDarkness gondolaDarkness = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				gondolaDarkness.hands.Visible = true;
				gondolaDarkness.hands.Play("appear");
				_003C_003E2__current = 1f;
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
	private sealed class _003CReach_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GondolaDarkness _003C_003E4__this;

		public Gondola gondola;

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
		public _003CReach_003Ed__9(int _003C_003E1__state)
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
			GondolaDarkness gondolaDarkness = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				gondolaDarkness.hands.Play("grab");
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				gondolaDarkness.hands.Play("pull");
				gondola.PullSides();
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

	private Sprite sprite;

	private Sprite hands;

	private Blackness blackness;

	private float anxiety;

	private float anxietyStutter;

	private WindSnowFG windSnowFG;

	public GondolaDarkness()
	{
		Add(sprite = GFX.SpriteBank.Create("gondolaDarkness"));
		sprite.Play("appear");
		Add(hands = GFX.SpriteBank.Create("gondolaHands"));
		hands.Visible = false;
		Visible = false;
		base.Depth = -999900;
	}

	[IteratorStateMachine(typeof(_003CAppear_003Ed__7))]
	public IEnumerator Appear(WindSnowFG windSnowFG = null)
	{
		this.windSnowFG = windSnowFG;
		Visible = true;
		base.Scene.Add(blackness = new Blackness());
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / 2f)
		{
			yield return null;
			blackness.Fade = t;
			anxiety = t;
			if (windSnowFG != null)
			{
				windSnowFG.Alpha = 1f - t;
			}
		}
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CExpand_003Ed__8))]
	public IEnumerator Expand()
	{
		hands.Visible = true;
		hands.Play("appear");
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CReach_003Ed__9))]
	public IEnumerator Reach(Gondola gondola)
	{
		hands.Play("grab");
		yield return 0.4f;
		hands.Play("pull");
		gondola.PullSides();
	}

	public override void Update()
	{
		base.Update();
		if (base.Scene.OnInterval(0.05f))
		{
			anxietyStutter = Calc.Random.NextFloat(0.1f);
		}
		Distort.AnxietyOrigin = new Vector2(0.5f, 0.5f);
		Distort.Anxiety = anxiety * 0.2f + anxietyStutter * anxiety;
	}

	public override void Render()
	{
		Position = (base.Scene as Level).Camera.Position + (base.Scene as Level).ZoomFocusPoint;
		base.Render();
	}

	public override void Removed(Scene scene)
	{
		anxiety = 0f;
		Distort.Anxiety = 0f;
		if (blackness != null)
		{
			blackness.RemoveSelf();
		}
		if (windSnowFG != null)
		{
			windSnowFG.Alpha = 1f;
		}
		base.Removed(scene);
	}
}
