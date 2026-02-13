using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class HeightDisplay : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeightDisplay _003C_003E4__this;

		private Player _003Cplayer_003E5__2;

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
		public _003CRoutine_003Ed__19(int _003C_003E1__state)
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
			HeightDisplay heightDisplay = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_003d;
			case 1:
				_003C_003E1__state = -1;
				goto IL_003d;
			case 2:
				_003C_003E1__state = -1;
				heightDisplay.Add(new Coroutine(heightDisplay.CameraUp()));
				if (!string.IsNullOrEmpty(heightDisplay.text) && heightDisplay.index >= 0)
				{
					Audio.Play("event:/game/07_summit/altitude_count");
				}
				goto IL_010f;
			case 3:
				_003C_003E1__state = -1;
				goto IL_010f;
			case 4:
				_003C_003E1__state = -1;
				goto IL_014a;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0198;
			case 6:
				_003C_003E1__state = -1;
				break;
			case 7:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0198:
				if ((heightDisplay.pulse -= Engine.DeltaTime * 4f) > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				heightDisplay.pulse = 0f;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 6;
				return true;
				IL_003d:
				_003Cplayer_003E5__2 = heightDisplay.Scene.Tracker.GetEntity<Player>();
				if (_003Cplayer_003E5__2 == null || !((heightDisplay.Scene as Level).Session.Level != heightDisplay.spawnedLevel))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				heightDisplay.StepAudioProgression();
				heightDisplay.easingCamera = false;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
				IL_010f:
				if ((heightDisplay.ease += Engine.DeltaTime / 0.15f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_014a;
				IL_014a:
				if (heightDisplay.approach < (float)heightDisplay.height && !_003Cplayer_003E5__2.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				heightDisplay.approach = heightDisplay.height;
				heightDisplay.pulse = 1f;
				goto IL_0198;
			}
			if ((heightDisplay.ease -= Engine.DeltaTime / 0.15f) > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 7;
				return true;
			}
			heightDisplay.RemoveSelf();
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
	private sealed class _003CCameraUp_003Ed__20 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeightDisplay _003C_003E4__this;

		private Level _003Clevel_003E5__2;

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
		public _003CCameraUp_003Ed__20(int _003C_003E1__state)
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
			HeightDisplay heightDisplay = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				heightDisplay.easingCamera = true;
				_003Clevel_003E5__2 = heightDisplay.Scene as Level;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime * 1.5f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				_003Clevel_003E5__2.Camera.Y = (float)(_003Clevel_003E5__2.Bounds.Bottom - 180) + 64f * (1f - Ease.CubeOut(_003Cp_003E5__3));
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

	private int index;

	private string text = "";

	private string leftText = "";

	private string rightText = "";

	private float leftSize;

	private float rightSize;

	private float numberSize;

	private Vector2 size;

	private int height;

	private float approach;

	private float ease;

	private float pulse;

	private string spawnedLevel;

	private bool setAudioProgression;

	private bool easingCamera = true;

	private bool drawText
	{
		get
		{
			if (index >= 0 && ease > 0f)
			{
				return !string.IsNullOrEmpty(text);
			}
			return false;
		}
	}

	public HeightDisplay(int index)
	{
		base.Tag = (int)Tags.HUD | (int)Tags.Persistent;
		this.index = index;
		string name = "CH7_HEIGHT_" + ((index < 0) ? "START" : index.ToString());
		if (index >= 0 && Dialog.Has(name))
		{
			text = Dialog.Get(name);
			text = text.ToUpper();
			height = (index + 1) * 500;
			approach = index * 500;
			int num = text.IndexOf("{X}");
			leftText = text.Substring(0, num);
			leftSize = ActiveFont.Measure(leftText).X;
			rightText = text.Substring(num + 3);
			numberSize = ActiveFont.Measure(height.ToString()).X;
			rightSize = ActiveFont.Measure(rightText).X;
			size = ActiveFont.Measure(leftText + height + rightText);
		}
		Add(new Coroutine(Routine()));
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		spawnedLevel = (scene as Level).Session.Level;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__19))]
	private IEnumerator Routine()
	{
		Player player;
		while (true)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			if (player != null && (base.Scene as Level).Session.Level != spawnedLevel)
			{
				break;
			}
			yield return null;
		}
		StepAudioProgression();
		easingCamera = false;
		yield return 0.1f;
		Add(new Coroutine(CameraUp()));
		if (!string.IsNullOrEmpty(text) && index >= 0)
		{
			Audio.Play("event:/game/07_summit/altitude_count");
		}
		while ((ease += Engine.DeltaTime / 0.15f) < 1f)
		{
			yield return null;
		}
		while (approach < (float)height && !player.OnGround())
		{
			yield return null;
		}
		approach = height;
		pulse = 1f;
		while ((pulse -= Engine.DeltaTime * 4f) > 0f)
		{
			yield return null;
		}
		pulse = 0f;
		yield return 1f;
		while ((ease -= Engine.DeltaTime / 0.15f) > 0f)
		{
			yield return null;
		}
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CCameraUp_003Ed__20))]
	private IEnumerator CameraUp()
	{
		easingCamera = true;
		Level level = base.Scene as Level;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 1.5f)
		{
			level.Camera.Y = (float)(level.Bounds.Bottom - 180) + 64f * (1f - Ease.CubeOut(p));
			yield return null;
		}
	}

	private void StepAudioProgression()
	{
		Session session = (base.Scene as Level).Session;
		if (!setAudioProgression && index >= 0 && session.Area.Mode == AreaMode.Normal)
		{
			setAudioProgression = true;
			int num = index + 1;
			if (num <= 5)
			{
				session.Audio.Music.Progress = num;
			}
			else
			{
				session.Audio.Music.Event = "event:/music/lvl7/final_ascent";
			}
			session.Audio.Apply();
		}
	}

	public override void Update()
	{
		if (index >= 0 && ease > 0f)
		{
			if ((float)height - approach > 100f)
			{
				approach += 1000f * Engine.DeltaTime;
			}
			else if ((float)height - approach > 25f)
			{
				approach += 200f * Engine.DeltaTime;
			}
			else if ((float)height - approach > 5f)
			{
				approach += 50f * Engine.DeltaTime;
			}
			else if ((float)height - approach > 0f)
			{
				approach += 10f * Engine.DeltaTime;
			}
			else
			{
				approach = height;
			}
		}
		Level level = base.Scene as Level;
		if (!easingCamera)
		{
			level.Camera.Y = level.Bounds.Bottom - 180 + 64;
		}
		base.Update();
	}

	public override void Render()
	{
		if (!base.Scene.Paused && drawText)
		{
			Vector2 vector = new Vector2(1920f, 1080f) / 2f;
			float num = 1.2f + pulse * 0.2f;
			Vector2 vector2 = size * num;
			float num2 = Ease.SineInOut(ease);
			Vector2 vector3 = new Vector2(1f, num2);
			Draw.Rect(vector.X - (vector2.X + 64f) * 0.5f * vector3.X, vector.Y - (vector2.Y + 32f) * 0.5f * vector3.Y, (vector2.X + 64f) * vector3.X, (vector2.Y + 32f) * vector3.Y, Color.Black);
			Vector2 vector4 = vector + new Vector2((0f - vector2.X) * 0.5f, 0f);
			Vector2 scale = vector3 * num;
			Color color = Color.White * num2;
			ActiveFont.Draw(leftText, vector4, new Vector2(0f, 0.5f), scale, color);
			ActiveFont.Draw(rightText, vector4 + Vector2.UnitX * (leftSize + numberSize) * num, new Vector2(0f, 0.5f), scale, color);
			ActiveFont.Draw(((int)approach).ToString(), vector4 + Vector2.UnitX * (leftSize + numberSize * 0.5f) * num, new Vector2(0.5f, 0.5f), scale, color);
		}
	}

	public override void Removed(Scene scene)
	{
		StepAudioProgression();
		base.Removed(scene);
	}
}
