using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiChapterSelectIcon : Entity
{
	[CompilerGenerated]
	private sealed class _003CAssistModeUnlockRoutine_003Ed__37 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelectIcon _003C_003E4__this;

		public Action onComplete;

		private float _003Cp_003E5__2;

		private int _003Ci_003E5__3;

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
		public _003CAssistModeUnlockRoutine_003Ed__37(int _003C_003E1__state)
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
			OuiChapterSelectIcon ouiChapterSelectIcon = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 0f;
				goto IL_00a9;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				goto IL_00a9;
			case 2:
				_003C_003E1__state = -1;
				_003Ci_003E5__3++;
				goto IL_0116;
			case 3:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 4f;
				goto IL_01bb;
			case 4:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 1f;
				break;
			case 5:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__2 -= Engine.DeltaTime * 4f;
					break;
				}
				IL_00a9:
				if (_003Cp_003E5__2 < 1f)
				{
					ouiChapterSelectIcon.spotlightRadius = Ease.CubeOut(_003Cp_003E5__2) * 128f;
					ouiChapterSelectIcon.spotlightAlpha = Ease.CubeOut(_003Cp_003E5__2) * 0.8f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				ouiChapterSelectIcon.shake.X = 6f;
				_003Ci_003E5__3 = 0;
				goto IL_0116;
				IL_01bb:
				if (_003Cp_003E5__2 < 1f)
				{
					float num2 = Ease.CubeIn(_003Cp_003E5__2);
					ouiChapterSelectIcon.shake = new Vector2(0f, -160f * num2);
					ouiChapterSelectIcon.Scale = new Vector2(1f - _003Cp_003E5__2, 1f + _003Cp_003E5__2 * 0.25f);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				ouiChapterSelectIcon.shake = Vector2.Zero;
				ouiChapterSelectIcon.Scale = Vector2.One;
				ouiChapterSelectIcon.AssistModeUnlockable = false;
				SaveData.Instance.UnlockedAreas++;
				ouiChapterSelectIcon.wiggler.Start();
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
				return true;
				IL_0116:
				if (_003Ci_003E5__3 < 10)
				{
					ouiChapterSelectIcon.shake.X = 0f - ouiChapterSelectIcon.shake.X;
					_003C_003E2__current = 0.01f;
					_003C_003E1__state = 2;
					return true;
				}
				ouiChapterSelectIcon.shake = Vector2.Zero;
				_003Cp_003E5__2 = 0f;
				goto IL_01bb;
			}
			if (_003Cp_003E5__2 > 0f)
			{
				ouiChapterSelectIcon.spotlightRadius = 128f + (1f - Ease.CubeOut(_003Cp_003E5__2)) * 128f;
				ouiChapterSelectIcon.spotlightAlpha = Ease.CubeOut(_003Cp_003E5__2) * 0.8f;
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
				return true;
			}
			ouiChapterSelectIcon.spotlightAlpha = 0f;
			if (onComplete != null)
			{
				onComplete();
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

	[CompilerGenerated]
	private sealed class _003CHighlightUnlockRoutine_003Ed__39 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OuiChapterSelectIcon _003C_003E4__this;

		public Action onComplete;

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
		public _003CHighlightUnlockRoutine_003Ed__39(int _003C_003E1__state)
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
			OuiChapterSelectIcon ouiChapterSelectIcon = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 0f;
				goto IL_00ad;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime * 2f;
				goto IL_00ad;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 1f;
				break;
			case 3:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__2 -= Engine.DeltaTime * 2f;
					break;
				}
				IL_00ad:
				if (_003Cp_003E5__2 < 1f)
				{
					ouiChapterSelectIcon.spotlightRadius = 128f + (1f - Ease.CubeOut(_003Cp_003E5__2)) * 128f;
					ouiChapterSelectIcon.spotlightAlpha = Ease.CubeOut(_003Cp_003E5__2) * 0.8f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				Audio.Play("event:/ui/postgame/unlock_newchapter_icon");
				ouiChapterSelectIcon.HideIcon = false;
				ouiChapterSelectIcon.wiggler.Start();
				_003C_003E2__current = 2f;
				_003C_003E1__state = 2;
				return true;
			}
			if (_003Cp_003E5__2 > 0f)
			{
				ouiChapterSelectIcon.spotlightRadius = 128f + (1f - Ease.CubeOut(_003Cp_003E5__2)) * 128f;
				ouiChapterSelectIcon.spotlightAlpha = Ease.CubeOut(_003Cp_003E5__2) * 0.8f;
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			ouiChapterSelectIcon.spotlightAlpha = 0f;
			if (onComplete != null)
			{
				onComplete();
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

	public const float IdleSize = 100f;

	public const float HoverSize = 144f;

	public const float HoverSpacing = 80f;

	public const float IdleY = 130f;

	public const float HoverY = 140f;

	public const float Spacing = 32f;

	public int Area;

	public bool New;

	public Vector2 Scale = Vector2.One;

	public float Rotation;

	public float sizeEase = 1f;

	public bool AssistModeUnlockable;

	public bool HideIcon;

	private Wiggler newWiggle;

	private bool hidden = true;

	private bool selected;

	private Tween tween;

	private Wiggler wiggler;

	private bool wiggleLeft;

	private int rotateDir = -1;

	private Vector2 shake;

	private float spotlightAlpha;

	private float spotlightRadius;

	private MTexture front;

	private MTexture back;

	public Vector2 IdlePosition
	{
		get
		{
			float num = 960f + (float)(Area - SaveData.Instance.LastArea.ID) * 132f;
			if (Area < SaveData.Instance.LastArea.ID)
			{
				num -= 80f;
			}
			else if (Area > SaveData.Instance.LastArea.ID)
			{
				num += 80f;
			}
			float y = 130f;
			if (Area == SaveData.Instance.LastArea.ID)
			{
				y = 140f;
			}
			return new Vector2(num, y);
		}
	}

	public Vector2 HiddenPosition => new Vector2(IdlePosition.X, -100f);

	public OuiChapterSelectIcon(int area, MTexture front, MTexture back)
	{
		base.Tag = (int)Tags.HUD | (int)Tags.PauseUpdate;
		Position = new Vector2(0f, -100f);
		Area = area;
		this.front = front;
		this.back = back;
		Add(wiggler = Wiggler.Create(0.35f, 2f, delegate(float f)
		{
			Rotation = (wiggleLeft ? (0f - f) : f) * 0.4f;
			Scale = Vector2.One * (1f + f * 0.5f);
		}));
		Add(newWiggle = Wiggler.Create(0.8f, 2f));
		newWiggle.StartZero = true;
	}

	public void Hovered(int dir)
	{
		wiggleLeft = dir < 0;
		wiggler.Start();
	}

	public void Select()
	{
		Audio.Play("event:/ui/world_map/icon/flip_right");
		selected = true;
		hidden = false;
		Vector2 from = Position;
		StartTween(0.6f, delegate(Tween t)
		{
			SetSelectedPercent(from, t.Percent);
		});
	}

	public void SnapToSelected()
	{
		selected = true;
		hidden = false;
		StopTween();
	}

	public void Unselect()
	{
		Audio.Play("event:/ui/world_map/icon/flip_left");
		hidden = false;
		selected = false;
		Vector2 to = IdlePosition;
		StartTween(0.6f, delegate(Tween t)
		{
			SetSelectedPercent(to, 1f - t.Percent);
		});
	}

	public void Hide()
	{
		Scale = Vector2.One;
		hidden = true;
		selected = false;
		Vector2 from = Position;
		StartTween(0.25f, delegate
		{
			Position = Vector2.Lerp(from, HiddenPosition, tween.Eased);
		});
	}

	public void Show()
	{
		if (SaveData.Instance != null)
		{
			New = SaveData.Instance.Areas[Area].Modes[0].TimePlayed <= 0;
		}
		Scale = Vector2.One;
		hidden = false;
		selected = false;
		Vector2 from = Position;
		StartTween(0.25f, delegate
		{
			Position = Vector2.Lerp(from, IdlePosition, tween.Eased);
		});
	}

	public void AssistModeUnlock(Action onComplete)
	{
		Add(new Coroutine(AssistModeUnlockRoutine(onComplete)));
	}

	[IteratorStateMachine(typeof(_003CAssistModeUnlockRoutine_003Ed__37))]
	private IEnumerator AssistModeUnlockRoutine(Action onComplete)
	{
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			spotlightRadius = Ease.CubeOut(p) * 128f;
			spotlightAlpha = Ease.CubeOut(p) * 0.8f;
			yield return null;
		}
		shake.X = 6f;
		for (int i = 0; i < 10; i++)
		{
			shake.X = 0f - shake.X;
			yield return 0.01f;
		}
		shake = Vector2.Zero;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			float num = Ease.CubeIn(p);
			shake = new Vector2(0f, -160f * num);
			Scale = new Vector2(1f - p, 1f + p * 0.25f);
			yield return null;
		}
		shake = Vector2.Zero;
		Scale = Vector2.One;
		AssistModeUnlockable = false;
		SaveData.Instance.UnlockedAreas++;
		wiggler.Start();
		yield return 1f;
		for (float p = 1f; p > 0f; p -= Engine.DeltaTime * 4f)
		{
			spotlightRadius = 128f + (1f - Ease.CubeOut(p)) * 128f;
			spotlightAlpha = Ease.CubeOut(p) * 0.8f;
			yield return null;
		}
		spotlightAlpha = 0f;
		onComplete?.Invoke();
	}

	public void HighlightUnlock(Action onComplete)
	{
		HideIcon = true;
		Add(new Coroutine(HighlightUnlockRoutine(onComplete)));
	}

	[IteratorStateMachine(typeof(_003CHighlightUnlockRoutine_003Ed__39))]
	private IEnumerator HighlightUnlockRoutine(Action onComplete)
	{
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 2f)
		{
			spotlightRadius = 128f + (1f - Ease.CubeOut(p)) * 128f;
			spotlightAlpha = Ease.CubeOut(p) * 0.8f;
			yield return null;
		}
		Audio.Play("event:/ui/postgame/unlock_newchapter_icon");
		HideIcon = false;
		wiggler.Start();
		yield return 2f;
		for (float p = 1f; p > 0f; p -= Engine.DeltaTime * 2f)
		{
			spotlightRadius = 128f + (1f - Ease.CubeOut(p)) * 128f;
			spotlightAlpha = Ease.CubeOut(p) * 0.8f;
			yield return null;
		}
		spotlightAlpha = 0f;
		onComplete?.Invoke();
	}

	private void StartTween(float duration, Action<Tween> callback)
	{
		StopTween();
		Add(tween = Tween.Create(Tween.TweenMode.Oneshot, null, duration, start: true));
		tween.OnUpdate = callback;
		tween.OnComplete = delegate
		{
			tween = null;
		};
	}

	private void StopTween()
	{
		if (tween != null)
		{
			Remove(tween);
		}
		tween = null;
	}

	private void SetSelectedPercent(Vector2 from, float p)
	{
		OuiChapterPanel uI = (base.Scene as Overworld).GetUI<OuiChapterPanel>();
		Vector2 vector = uI.OpenPosition + uI.IconOffset;
		SimpleCurve simpleCurve = new SimpleCurve(from, vector, (from + vector) / 2f + new Vector2(0f, 30f));
		float num = 1f + ((p < 0.5f) ? (p * 2f) : ((1f - p) * 2f));
		Scale.X = (float)Math.Cos(Ease.SineInOut(p) * ((float)Math.PI * 2f)) * num;
		Scale.Y = num;
		Position = simpleCurve.GetPoint(Ease.Invert(Ease.CubeInOut)(p));
		Rotation = Ease.UpDown(Ease.SineInOut(p)) * ((float)Math.PI / 180f) * 15f * (float)rotateDir;
		if (p <= 0f)
		{
			rotateDir = -1;
		}
		else if (p >= 1f)
		{
			rotateDir = 1;
		}
	}

	public override void Update()
	{
		if (SaveData.Instance == null)
		{
			return;
		}
		sizeEase = Calc.Approach(sizeEase, (SaveData.Instance.LastArea.ID == Area) ? 1f : 0f, Engine.DeltaTime * 4f);
		if (SaveData.Instance.LastArea.ID == Area)
		{
			base.Depth = -50;
		}
		else
		{
			base.Depth = -45;
		}
		if (tween == null)
		{
			if (selected)
			{
				OuiChapterPanel uI = (base.Scene as Overworld).GetUI<OuiChapterPanel>();
				Position = ((!uI.EnteringChapter) ? uI.OpenPosition : uI.Position) + uI.IconOffset;
			}
			else if (!hidden)
			{
				Position = Calc.Approach(Position, IdlePosition, 2400f * Engine.DeltaTime);
			}
		}
		if (New && base.Scene.OnInterval(1.5f))
		{
			newWiggle.Start();
		}
		base.Update();
	}

	public override void Render()
	{
		MTexture mTexture = front;
		Vector2 scale = Scale;
		int num = mTexture.Width;
		if (scale.X < 0f)
		{
			mTexture = back;
		}
		if (AssistModeUnlockable)
		{
			mTexture = GFX.Gui["areas/lock"];
			num -= 32;
		}
		if (!HideIcon)
		{
			scale *= (100f + 44f * Ease.CubeInOut(sizeEase)) / (float)num;
			if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
			{
				scale.X = 0f - scale.X;
			}
			mTexture.DrawCentered(Position + shake, Color.White, scale, Rotation);
			if (New && SaveData.Instance != null && !SaveData.Instance.CheatMode && Area == SaveData.Instance.UnlockedAreas && !selected && tween == null && !AssistModeUnlockable && Celeste.PlayMode != Celeste.PlayModes.Event)
			{
				Vector2 position = Position + new Vector2((float)num * 0.25f, (float)(-mTexture.Height) * 0.25f);
				position += Vector2.UnitY * (0f - Math.Abs(newWiggle.Value * 30f));
				GFX.Gui["areas/new"].DrawCentered(position);
			}
		}
		if (spotlightAlpha > 0f)
		{
			HiresRenderer.EndRender();
			SpotlightWipe.DrawSpotlight(new Vector2(Position.X, IdlePosition.Y), spotlightRadius, Color.Black * spotlightAlpha);
			HiresRenderer.BeginRender();
		}
		else if (AssistModeUnlockable && SaveData.Instance.LastArea.ID == Area && !hidden)
		{
			ActiveFont.DrawOutline(Dialog.Clean("ASSIST_SKIP"), Position + new Vector2(0f, 100f), new Vector2(0.5f, 0f), Vector2.One * 0.7f, Color.White, 2f, Color.Black);
		}
	}
}
