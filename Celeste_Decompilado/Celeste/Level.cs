using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Celeste.Editor;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class Level : Scene, IOverlayHandler
{
	public enum CameraLockModes
	{
		None,
		BoostSequence,
		FinalBoss,
		FinalBossNoY,
		Lava
	}

	private enum ConditionBlockModes
	{
		Key,
		Button,
		Strawberry
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass126_0
	{
		public List<Component> transitionOut;

		internal bool _003CTransitionRoutine_003Eb__0(Component c)
		{
			return transitionOut.Contains(c);
		}
	}

	[CompilerGenerated]
	private sealed class _003CTransitionRoutine_003Ed__126 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level _003C_003E4__this;

		public LevelData next;

		public Vector2 direction;

		private _003C_003Ec__DisplayClass126_0 _003C_003E8__1;

		private Player _003Cplayer_003E5__2;

		private List<Entity> _003CtoRemove_003E5__3;

		private List<Component> _003CtransitionIn_003E5__4;

		private float _003CcameraAt_003E5__5;

		private Vector2 _003CcameraFrom_003E5__6;

		private Vector2 _003CplayerTo_003E5__7;

		private Vector2 _003CcameraTo_003E5__8;

		private float _003ClightingStart_003E5__9;

		private float _003ClightingEnd_003E5__10;

		private bool _003ClightingWait_003E5__11;

		private bool _003CcameraFinished_003E5__12;

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
		public _003CTransitionRoutine_003Ed__126(int _003C_003E1__state)
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
			Level level = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass126_0();
				_003Cplayer_003E5__2 = level.Tracker.GetEntity<Player>();
				_003CtoRemove_003E5__3 = level.GetEntitiesExcludingTagMask((int)Tags.Persistent | (int)Tags.Global);
				_003C_003E8__1.transitionOut = level.Tracker.GetComponentsCopy<TransitionListener>();
				_003Cplayer_003E5__2.CleanUpTriggers();
				foreach (SoundSource component in level.Tracker.GetComponents<SoundSource>())
				{
					if (component.DisposeOnTransition)
					{
						component.Stop();
					}
				}
				level.PreviousBounds = level.Bounds;
				level.Session.Level = next.Name;
				level.Session.FirstLevel = false;
				level.Session.DeathsInCurrentLevel = 0;
				level.LoadLevel(Player.IntroTypes.Transition);
				Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "has_conveyors", (level.Tracker.GetEntities<WallBooster>().Count > 0) ? 1 : 0);
				_003CtransitionIn_003E5__4 = level.Tracker.GetComponentsCopy<TransitionListener>();
				_003CtransitionIn_003E5__4.RemoveAll((Component c) => _003C_003E8__1.transitionOut.Contains(c));
				GC.Collect();
				_003CcameraAt_003E5__5 = 0f;
				_003CcameraFrom_003E5__6 = level.Camera.Position;
				Vector2 dirPad = direction * 4f;
				if (direction == Vector2.UnitY)
				{
					dirPad = direction * 12f;
				}
				_003CplayerTo_003E5__7 = _003Cplayer_003E5__2.Position;
				while (direction.X != 0f && _003CplayerTo_003E5__7.Y >= (float)level.Bounds.Bottom)
				{
					_003CplayerTo_003E5__7.Y -= 1f;
				}
				while (!level.IsInBounds(_003CplayerTo_003E5__7, dirPad))
				{
					_003CplayerTo_003E5__7 += direction;
				}
				_003CcameraTo_003E5__8 = level.GetFullCameraTargetAt(_003Cplayer_003E5__2, _003CplayerTo_003E5__7);
				Vector2 position = _003Cplayer_003E5__2.Position;
				_003Cplayer_003E5__2.Position = _003CplayerTo_003E5__7;
				foreach (Entity item in _003Cplayer_003E5__2.CollideAll<WindTrigger>())
				{
					if (!_003CtoRemove_003E5__3.Contains(item))
					{
						level.windController.SetPattern((item as WindTrigger).Pattern);
						break;
					}
				}
				level.windController.SetStartPattern();
				_003Cplayer_003E5__2.Position = position;
				foreach (TransitionListener item2 in _003C_003E8__1.transitionOut)
				{
					if (item2.OnOutBegin != null)
					{
						item2.OnOutBegin();
					}
				}
				foreach (TransitionListener item3 in _003CtransitionIn_003E5__4)
				{
					if (item3.OnInBegin != null)
					{
						item3.OnInBegin();
					}
				}
				_003ClightingStart_003E5__9 = level.Lighting.Alpha;
				_003ClightingEnd_003E5__10 = (level.DarkRoom ? level.Session.DarkRoomAlpha : (level.BaseLightingAlpha + level.Session.LightingAlphaAdd));
				_003ClightingWait_003E5__11 = _003ClightingStart_003E5__9 >= level.Session.DarkRoomAlpha || _003ClightingEnd_003E5__10 >= level.Session.DarkRoomAlpha;
				if ((_003ClightingEnd_003E5__10 > _003ClightingStart_003E5__9) & _003ClightingWait_003E5__11)
				{
					Audio.Play("event:/game/05_mirror_temple/room_lightlevel_down");
					goto IL_0462;
				}
				goto IL_0475;
			}
			case 1:
				_003C_003E1__state = -1;
				level.Lighting.Alpha = Calc.Approach(level.Lighting.Alpha, _003ClightingEnd_003E5__10, 2f * Engine.DeltaTime);
				goto IL_0462;
			case 2:
				_003C_003E1__state = -1;
				if (!_003CcameraFinished_003E5__12)
				{
					_003CcameraAt_003E5__5 = Calc.Approach(_003CcameraAt_003E5__5, 1f, Engine.DeltaTime / level.NextTransitionDuration);
					if (_003CcameraAt_003E5__5 > 0.9f)
					{
						level.Camera.Position = _003CcameraTo_003E5__8;
					}
					else
					{
						level.Camera.Position = Vector2.Lerp(_003CcameraFrom_003E5__6, _003CcameraTo_003E5__8, Ease.CubeOut(_003CcameraAt_003E5__5));
					}
					if (!_003ClightingWait_003E5__11 && _003ClightingStart_003E5__9 < _003ClightingEnd_003E5__10)
					{
						level.Lighting.Alpha = _003ClightingStart_003E5__9 + (_003ClightingEnd_003E5__10 - _003ClightingStart_003E5__9) * _003CcameraAt_003E5__5;
					}
					foreach (TransitionListener item4 in _003C_003E8__1.transitionOut)
					{
						if (item4.OnOut != null)
						{
							item4.OnOut(_003CcameraAt_003E5__5);
						}
					}
					foreach (TransitionListener item5 in _003CtransitionIn_003E5__4)
					{
						if (item5.OnIn != null)
						{
							item5.OnIn(_003CcameraAt_003E5__5);
						}
					}
					if (_003CcameraAt_003E5__5 >= 1f)
					{
						_003CcameraFinished_003E5__12 = true;
					}
				}
				goto IL_0606;
			case 3:
				{
					_003C_003E1__state = -1;
					level.Lighting.Alpha = Calc.Approach(level.Lighting.Alpha, _003ClightingEnd_003E5__10, 2f * Engine.DeltaTime);
					goto IL_0699;
				}
				IL_0462:
				if (level.Lighting.Alpha != _003ClightingEnd_003E5__10)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0475;
				IL_0475:
				_003CcameraFinished_003E5__12 = false;
				goto IL_0606;
				IL_0606:
				if (!_003Cplayer_003E5__2.TransitionTo(_003CplayerTo_003E5__7, direction) || _003CcameraAt_003E5__5 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				if (!((_003ClightingEnd_003E5__10 < _003ClightingStart_003E5__9) & _003ClightingWait_003E5__11))
				{
					break;
				}
				Audio.Play("event:/game/05_mirror_temple/room_lightlevel_up");
				goto IL_0699;
				IL_0699:
				if (level.Lighting.Alpha != _003ClightingEnd_003E5__10)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				break;
			}
			level.UnloadEntities(_003CtoRemove_003E5__3);
			level.Entities.UpdateLists();
			Rectangle bounds = level.Bounds;
			bounds.Inflate(16, 16);
			level.Particles.ClearRect(bounds, inside: false);
			level.ParticlesBG.ClearRect(bounds, inside: false);
			level.ParticlesFG.ClearRect(bounds, inside: false);
			Vector2 to = _003Cplayer_003E5__2.CollideFirst<RespawnTargetTrigger>()?.Target ?? _003Cplayer_003E5__2.Position;
			level.Session.RespawnPoint = level.Session.LevelData.Spawns.ClosestTo(to);
			_003Cplayer_003E5__2.OnTransition();
			foreach (TransitionListener item6 in _003CtransitionIn_003E5__4)
			{
				if (item6.OnInEnd != null)
				{
					item6.OnInEnd();
				}
			}
			if (level.Session.LevelData.DelayAltMusic)
			{
				Audio.SetAltMusic(SFX.EventnameByHandle(level.Session.LevelData.AltMusic));
			}
			_003CcameraFrom_003E5__6 = default(Vector2);
			_003CplayerTo_003E5__7 = default(Vector2);
			_003CcameraTo_003E5__8 = default(Vector2);
			level.NextTransitionDuration = 0.65f;
			level.transition = null;
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
	private sealed class _003CSavingRoutine_003Ed__141 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level _003C_003E4__this;

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
		public _003CSavingRoutine_003Ed__141(int _003C_003E1__state)
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
			Level level = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				UserIO.SaveHandler(file: true, settings: false);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (UserIO.Saving)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			level.saving = null;
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
	private sealed class _003CSaveFromOptions_003Ed__152 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CSaveFromOptions_003Ed__152(int _003C_003E1__state)
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
				UserIO.SaveHandler(file: false, settings: true);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (UserIO.Saving)
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

	[CompilerGenerated]
	private sealed class _003CZoomTo_003Ed__162 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level _003C_003E4__this;

		public Vector2 screenSpaceFocusPoint;

		public float zoom;

		public float duration;

		private float _003Cfrom_003E5__2;

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
		public _003CZoomTo_003Ed__162(int _003C_003E1__state)
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
			Level level = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				level.ZoomFocusPoint = screenSpaceFocusPoint;
				level.ZoomTarget = zoom;
				_003Cfrom_003E5__2 = level.Zoom;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				level.Zoom = MathHelper.Lerp(_003Cfrom_003E5__2, level.ZoomTarget, Ease.SineInOut(MathHelper.Clamp(_003Cp_003E5__3, 0f, 1f)));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			level.Zoom = level.ZoomTarget;
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
	private sealed class _003CZoomAcross_003Ed__163 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level _003C_003E4__this;

		public float zoom;

		public Vector2 screenSpaceFocusPoint;

		public float duration;

		private float _003CfromZoom_003E5__2;

		private Vector2 _003CfromFocus_003E5__3;

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
		public _003CZoomAcross_003Ed__163(int _003C_003E1__state)
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
			Level level = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003CfromZoom_003E5__2 = level.Zoom;
				_003CfromFocus_003E5__3 = level.ZoomFocusPoint;
				_003Cp_003E5__4 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
			{
				float amount = Ease.SineInOut(MathHelper.Clamp(_003Cp_003E5__4, 0f, 1f));
				level.Zoom = (level.ZoomTarget = MathHelper.Lerp(_003CfromZoom_003E5__2, zoom, amount));
				level.ZoomFocusPoint = Vector2.Lerp(_003CfromFocus_003E5__3, screenSpaceFocusPoint, amount);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			level.Zoom = level.ZoomTarget;
			level.ZoomFocusPoint = screenSpaceFocusPoint;
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
	private sealed class _003CZoomBack_003Ed__164 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level _003C_003E4__this;

		public float duration;

		private float _003Cfrom_003E5__2;

		private float _003Cto_003E5__3;

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
		public _003CZoomBack_003Ed__164(int _003C_003E1__state)
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
			Level level = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = level.Zoom;
				_003Cto_003E5__3 = 1f;
				_003Cp_003E5__4 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
			{
				level.Zoom = MathHelper.Lerp(_003Cfrom_003E5__2, _003Cto_003E5__3, Ease.SineInOut(MathHelper.Clamp(_003Cp_003E5__4, 0f, 1f)));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			level.ResetZoom();
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
	private sealed class _003CSkipCutsceneRoutine_003Ed__181 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level _003C_003E4__this;

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
		public _003CSkipCutsceneRoutine_003Ed__181(int _003C_003E1__state)
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
			Level level = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = new FadeWipe(level, wipeIn: false)
				{
					Duration = 0.25f
				}.Wait();
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				level.onCutsceneSkip(level);
				level.strawberriesDisplay.DrawLerp = 0f;
				if (level.onCutsceneSkipResetZoom)
				{
					level.ResetZoom();
				}
				GameplayStats gameplayStats = level.Entities.FindFirst<GameplayStats>();
				if (gameplayStats != null)
				{
					gameplayStats.DrawLerp = 0f;
				}
				if (level.onCutsceneSkipFadeIn)
				{
					FadeWipe fadeWipe = new FadeWipe(level, wipeIn: true)
					{
						Duration = 0.25f
					};
					level.RendererList.UpdateLists();
					_003C_003E2__current = fadeWipe.Wait();
					_003C_003E1__state = 2;
					return true;
				}
				break;
			}
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			level.SkippingCutscene = false;
			level.EndCutscene();
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

	public bool Completed;

	public bool NewLevel;

	public bool TimerStarted;

	public bool TimerStopped;

	public bool TimerHidden;

	public Session Session;

	public Vector2? StartPosition;

	public bool DarkRoom;

	public Player.IntroTypes LastIntroType;

	public bool InCredits;

	public bool AllowHudHide = true;

	public VirtualMap<char> SolidsData;

	public VirtualMap<char> BgData;

	public float NextTransitionDuration = 0.65f;

	public const float DefaultTransitionDuration = 0.65f;

	public ScreenWipe Wipe;

	private Coroutine transition;

	private Coroutine saving;

	public FormationBackdrop FormationBackdrop;

	public SolidTiles SolidTiles;

	public BackgroundTiles BgTiles;

	public Color BackgroundColor = Color.Black;

	public BackdropRenderer Background;

	public BackdropRenderer Foreground;

	public GameplayRenderer GameplayRenderer;

	public HudRenderer HudRenderer;

	public LightingRenderer Lighting;

	public DisplacementRenderer Displacement;

	public BloomRenderer Bloom;

	public TileGrid FgTilesLightMask;

	public ParticleSystem Particles;

	public ParticleSystem ParticlesBG;

	public ParticleSystem ParticlesFG;

	public HiresSnow HiresSnow;

	public TotalStrawberriesDisplay strawberriesDisplay;

	private WindController windController;

	public const float CameraOffsetXInterval = 48f;

	public const float CameraOffsetYInterval = 32f;

	public Camera Camera;

	public CameraLockModes CameraLockMode;

	public Vector2 CameraOffset;

	public float CameraUpwardMaxY;

	private Vector2 shakeDirection;

	private int lastDirectionalShake;

	private float shakeTimer;

	private Vector2 cameraPreShake;

	public float ScreenPadding;

	private float flash;

	private Color flashColor = Color.White;

	private bool doFlash;

	private bool flashDrawPlayer;

	private float glitchTimer;

	private float glitchSeed;

	public float Zoom = 1f;

	public float ZoomTarget = 1f;

	public Vector2 ZoomFocusPoint;

	private string lastColorGrade;

	private float colorGradeEase;

	private float colorGradeEaseSpeed = 1f;

	public Vector2 Wind;

	public float WindSine;

	public float WindSineTimer;

	public bool Frozen;

	public bool PauseLock;

	public bool CanRetry = true;

	public bool PauseMainMenuOpen;

	private bool wasPaused;

	private float wasPausedTimer;

	private float unpauseTimer;

	public bool SaveQuitDisabled;

	public bool InCutscene;

	public bool SkippingCutscene;

	private Coroutine skipCoroutine;

	private Action<Level> onCutsceneSkip;

	private bool onCutsceneSkipFadeIn;

	private bool onCutsceneSkipResetZoom;

	private bool endingChapterAfterCutscene;

	public static FMOD.Studio.EventInstance DialogSnapshot;

	private static FMOD.Studio.EventInstance PauseSnapshot;

	private static FMOD.Studio.EventInstance AssistSpeedSnapshot;

	private static int AssistSpeedSnapshotValue = -1;

	public Pathfinder Pathfinder;

	public PlayerDeadBody RetryPlayerCorpse;

	public float BaseLightingAlpha;

	private bool updateHair = true;

	public bool InSpace;

	public bool HasCassetteBlocks;

	public float CassetteBlockTempo;

	public int CassetteBlockBeats;

	public Random HiccupRandom;

	public bool Raining;

	private Session.CoreModes coreMode;

	public Vector2 LevelOffset => new Vector2(Bounds.Left, Bounds.Top);

	public Point LevelSolidOffset => new Point(Bounds.Left / 8 - TileBounds.X, Bounds.Top / 8 - TileBounds.Y);

	public Rectangle TileBounds => Session.MapData.TileBounds;

	public bool Transitioning => transition != null;

	public Vector2 ShakeVector { get; private set; }

	public float VisualWind => Wind.X + WindSine;

	public bool FrozenOrPaused
	{
		get
		{
			if (!Frozen)
			{
				return Paused;
			}
			return true;
		}
	}

	public bool CanPause
	{
		get
		{
			Player entity = base.Tracker.GetEntity<Player>();
			if (entity != null && !entity.Dead && !wasPaused && !Paused && !PauseLock && !SkippingCutscene && !Transitioning && Wipe == null && !UserIO.Saving)
			{
				if (entity.LastBooster != null && entity.LastBooster.Ch9HubTransition)
				{
					return !entity.LastBooster.BoostingPlayer;
				}
				return true;
			}
			return false;
		}
	}

	public Overlay Overlay { get; set; }

	public bool ShowHud
	{
		get
		{
			if (Completed)
			{
				return false;
			}
			if (Paused)
			{
				return true;
			}
			if (base.Tracker.GetEntity<Textbox>() == null && base.Tracker.GetEntity<MiniTextbox>() == null && !Frozen)
			{
				return !InCutscene;
			}
			return false;
		}
	}

	private bool ShouldCreateCassetteManager
	{
		get
		{
			if (Session.Area.Mode == AreaMode.Normal)
			{
				return !Session.Cassette;
			}
			return true;
		}
	}

	public Vector2 DefaultSpawnPoint => GetSpawnPoint(new Vector2(Bounds.Left, Bounds.Bottom));

	public Rectangle Bounds => Session.LevelData.Bounds;

	public Rectangle? PreviousBounds { get; private set; }

	public Session.CoreModes CoreMode
	{
		get
		{
			return coreMode;
		}
		set
		{
			if (coreMode == value)
			{
				return;
			}
			coreMode = value;
			Session.SetFlag("cold", coreMode == Session.CoreModes.Cold);
			Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "room_state", (coreMode != Session.CoreModes.Hot) ? 1 : 0);
			if (Audio.CurrentMusic == "event:/music/lvl9/main")
			{
				Session.Audio.Music.Layer(1, coreMode == Session.CoreModes.Hot);
				Session.Audio.Music.Layer(2, coreMode == Session.CoreModes.Cold);
				Session.Audio.Apply();
			}
			foreach (CoreModeListener component in base.Tracker.GetComponents<CoreModeListener>())
			{
				if (component.OnChange != null)
				{
					component.OnChange(value);
				}
			}
		}
	}

	public override void Begin()
	{
		ScreenWipe.WipeColor = Color.Black;
		GameplayBuffers.Create();
		Distort.WaterAlpha = 1f;
		Distort.WaterSineDirection = 1f;
		Audio.MusicUnderwater = false;
		Audio.EndSnapshot(DialogSnapshot);
		base.Begin();
	}

	public override void End()
	{
		base.End();
		Foreground.Ended(this);
		Background.Ended(this);
		EndPauseEffects();
		Audio.BusStopAll("bus:/gameplay_sfx");
		Audio.MusicUnderwater = false;
		Audio.SetAmbience(null);
		Audio.SetAltMusic(null);
		Audio.EndSnapshot(DialogSnapshot);
		Audio.ReleaseSnapshot(AssistSpeedSnapshot);
		AssistSpeedSnapshot = null;
		AssistSpeedSnapshotValue = -1;
		GameplayBuffers.Unload();
		ClutterBlockGenerator.Dispose();
		Engine.TimeRateB = 1f;
	}

	public void LoadLevel(Player.IntroTypes playerIntro, bool isFromLoader = false)
	{
		TimerHidden = false;
		TimerStopped = false;
		LastIntroType = playerIntro;
		Background.Fade = 0f;
		CanRetry = true;
		ScreenPadding = 0f;
		Displacement.Enabled = true;
		PauseLock = false;
		Frozen = false;
		CameraLockMode = CameraLockModes.None;
		RetryPlayerCorpse = null;
		FormationBackdrop.Display = false;
		SaveQuitDisabled = false;
		lastColorGrade = Session.ColorGrade;
		colorGradeEase = 0f;
		colorGradeEaseSpeed = 1f;
		HasCassetteBlocks = false;
		CassetteBlockTempo = 1f;
		CassetteBlockBeats = 2;
		Raining = false;
		bool flag = false;
		bool flag2 = false;
		if (HiccupRandom == null)
		{
			HiccupRandom = new Random(Session.Area.ID * 77 + (int)Session.Area.Mode * 999);
		}
		base.Entities.FindFirst<LightningRenderer>()?.Reset();
		Calc.PushRandom(Session.LevelData.LoadSeed);
		_ = Session.MapData;
		LevelData levelData = Session.LevelData;
		Vector2 vector = new Vector2(levelData.Bounds.Left, levelData.Bounds.Top);
		bool flag3 = playerIntro != Player.IntroTypes.Fall || levelData.Name == "0";
		DarkRoom = levelData.Dark && !Session.GetFlag("ignore_darkness_" + levelData.Name);
		Zoom = 1f;
		if (Session.Audio == null)
		{
			Session.Audio = AreaData.Get(Session).Mode[(int)Session.Area.Mode].AudioState.Clone();
		}
		if (!levelData.DelayAltMusic)
		{
			Audio.SetAltMusic(SFX.EventnameByHandle(levelData.AltMusic));
		}
		if (levelData.Music.Length > 0)
		{
			Session.Audio.Music.Event = SFX.EventnameByHandle(levelData.Music);
		}
		if (!AreaData.GetMode(Session.Area).IgnoreLevelAudioLayerData)
		{
			for (int i = 0; i < 4; i++)
			{
				Session.Audio.Music.Layer(i + 1, levelData.MusicLayers[i]);
			}
		}
		if (levelData.MusicProgress >= 0)
		{
			Session.Audio.Music.Progress = levelData.MusicProgress;
		}
		Session.Audio.Music.Layer(6, levelData.MusicWhispers);
		if (levelData.Ambience.Length > 0)
		{
			Session.Audio.Ambience.Event = SFX.EventnameByHandle(levelData.Ambience);
		}
		if (levelData.AmbienceProgress >= 0)
		{
			Session.Audio.Ambience.Progress = levelData.AmbienceProgress;
		}
		Session.Audio.Apply(isFromLoader);
		CoreMode = Session.CoreMode;
		NewLevel = !Session.LevelFlags.Contains(levelData.Name);
		if (flag3)
		{
			if (!Session.LevelFlags.Contains(levelData.Name))
			{
				Session.FurthestSeenLevel = levelData.Name;
			}
			Session.LevelFlags.Add(levelData.Name);
			Session.UpdateLevelStartDashes();
		}
		Vector2? startPosition = null;
		CameraOffset = new Vector2(48f, 32f) * levelData.CameraOffset;
		base.Entities.FindFirst<WindController>()?.RemoveSelf();
		Add(windController = new WindController(levelData.WindPattern));
		if (playerIntro != Player.IntroTypes.Transition)
		{
			windController.SetStartPattern();
		}
		if (levelData.Underwater)
		{
			Add(new Water(vector, topSurface: false, bottomSurface: false, levelData.Bounds.Width, levelData.Bounds.Height));
		}
		InSpace = levelData.Space;
		if (InSpace)
		{
			Add(new SpaceController());
		}
		if (levelData.Name == "-1" && Session.Area.ID == 0 && !SaveData.Instance.CheatMode)
		{
			Add(new UnlockEverythingThingy());
		}
		int num = 0;
		List<EntityID> list = new List<EntityID>();
		Player entity = base.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			foreach (Follower follower in entity.Leader.Followers)
			{
				list.Add(follower.ParentEntityID);
			}
		}
		foreach (EntityData entity3 in levelData.Entities)
		{
			int iD = entity3.ID;
			EntityID entityID = new EntityID(levelData.Name, iD);
			if (Session.DoNotLoad.Contains(entityID) || list.Contains(entityID))
			{
				continue;
			}
			switch (entity3.Name)
			{
			case "checkpoint":
				if (flag3)
				{
					Checkpoint checkpoint = new Checkpoint(entity3, vector);
					Add(checkpoint);
					startPosition = entity3.Position + vector + checkpoint.SpawnOffset;
				}
				break;
			case "jumpThru":
				Add(new JumpthruPlatform(entity3, vector));
				break;
			case "refill":
				Add(new Refill(entity3, vector));
				break;
			case "infiniteStar":
				Add(new FlyFeather(entity3, vector));
				break;
			case "strawberry":
				Add(new Strawberry(entity3, vector, entityID));
				break;
			case "memorialTextController":
				if (Session.Dashes == 0 && Session.StartedFromBeginning)
				{
					Add(new Strawberry(entity3, vector, entityID));
				}
				break;
			case "goldenBerry":
			{
				bool cheatMode = SaveData.Instance.CheatMode;
				bool flag4 = Session.FurthestSeenLevel == Session.Level || Session.Deaths == 0;
				bool flag5 = SaveData.Instance.UnlockedModes >= 3 || SaveData.Instance.DebugMode;
				bool completed = SaveData.Instance.Areas[Session.Area.ID].Modes[(int)Session.Area.Mode].Completed;
				if ((cheatMode || (flag5 && completed)) && flag4)
				{
					Add(new Strawberry(entity3, vector, entityID));
				}
				break;
			}
			case "summitgem":
				Add(new SummitGem(entity3, vector, entityID));
				break;
			case "blackGem":
				if (!Session.HeartGem || Session.Area.Mode != AreaMode.Normal)
				{
					Add(new HeartGem(entity3, vector));
				}
				break;
			case "dreamHeartGem":
				if (!Session.HeartGem)
				{
					Add(new DreamHeartGem(entity3, vector));
				}
				break;
			case "spring":
				Add(new Spring(entity3, vector, Spring.Orientations.Floor));
				break;
			case "wallSpringLeft":
				Add(new Spring(entity3, vector, Spring.Orientations.WallLeft));
				break;
			case "wallSpringRight":
				Add(new Spring(entity3, vector, Spring.Orientations.WallRight));
				break;
			case "fallingBlock":
				Add(new FallingBlock(entity3, vector));
				break;
			case "zipMover":
				Add(new ZipMover(entity3, vector));
				break;
			case "crumbleBlock":
				Add(new CrumblePlatform(entity3, vector));
				break;
			case "dreamBlock":
				Add(new DreamBlock(entity3, vector));
				break;
			case "touchSwitch":
				Add(new TouchSwitch(entity3, vector));
				break;
			case "switchGate":
				Add(new SwitchGate(entity3, vector));
				break;
			case "negaBlock":
				Add(new NegaBlock(entity3, vector));
				break;
			case "key":
				Add(new Key(entity3, vector, entityID));
				break;
			case "lockBlock":
				Add(new LockBlock(entity3, vector, entityID));
				break;
			case "movingPlatform":
				Add(new MovingPlatform(entity3, vector));
				break;
			case "rotatingPlatforms":
			{
				Vector2 vector2 = entity3.Position + vector;
				Vector2 vector3 = entity3.Nodes[0] + vector;
				int width = entity3.Width;
				int num2 = entity3.Int("platforms");
				bool clockwise = entity3.Bool("clockwise");
				float length = (vector2 - vector3).Length();
				float num3 = (vector2 - vector3).Angle();
				float num4 = (float)Math.PI * 2f / (float)num2;
				for (int j = 0; j < num2; j++)
				{
					float angleRadians = num3 + num4 * (float)j;
					angleRadians = Calc.WrapAngle(angleRadians);
					Vector2 position2 = vector3 + Calc.AngleToVector(angleRadians, length);
					Add(new RotatingPlatform(position2, width, vector3, clockwise));
				}
				break;
			}
			case "blockField":
				Add(new BlockField(entity3, vector));
				break;
			case "cloud":
				Add(new Cloud(entity3, vector));
				break;
			case "booster":
				Add(new Booster(entity3, vector));
				break;
			case "moveBlock":
				Add(new MoveBlock(entity3, vector));
				break;
			case "light":
				Add(new PropLight(entity3, vector));
				break;
			case "switchBlock":
			case "swapBlock":
				Add(new SwapBlock(entity3, vector));
				break;
			case "dashSwitchH":
			case "dashSwitchV":
				Add(DashSwitch.Create(entity3, vector, entityID));
				break;
			case "templeGate":
				Add(new TempleGate(entity3, vector, levelData.Name));
				break;
			case "torch":
				Add(new Torch(entity3, vector, entityID));
				break;
			case "templeCrackedBlock":
				Add(new TempleCrackedBlock(entityID, entity3, vector));
				break;
			case "seekerBarrier":
				Add(new SeekerBarrier(entity3, vector));
				break;
			case "theoCrystal":
				Add(new TheoCrystal(entity3, vector));
				break;
			case "glider":
				Add(new Glider(entity3, vector));
				break;
			case "theoCrystalPedestal":
				Add(new TheoCrystalPedestal(entity3, vector));
				break;
			case "badelineBoost":
				Add(new BadelineBoost(entity3, vector));
				break;
			case "cassette":
				if (!Session.Cassette)
				{
					Add(new Cassette(entity3, vector));
				}
				break;
			case "cassetteBlock":
			{
				CassetteBlock cassetteBlock = new CassetteBlock(entity3, vector, entityID);
				Add(cassetteBlock);
				HasCassetteBlocks = true;
				if (CassetteBlockTempo == 1f)
				{
					CassetteBlockTempo = cassetteBlock.Tempo;
				}
				CassetteBlockBeats = Math.Max(cassetteBlock.Index + 1, CassetteBlockBeats);
				if (!flag)
				{
					flag = true;
					if (base.Tracker.GetEntity<CassetteBlockManager>() == null && ShouldCreateCassetteManager)
					{
						Add(new CassetteBlockManager());
					}
				}
				break;
			}
			case "wallBooster":
				Add(new WallBooster(entity3, vector));
				break;
			case "bounceBlock":
				Add(new BounceBlock(entity3, vector));
				break;
			case "coreModeToggle":
				Add(new CoreModeToggle(entity3, vector));
				break;
			case "iceBlock":
				Add(new IceBlock(entity3, vector));
				break;
			case "fireBarrier":
				Add(new FireBarrier(entity3, vector));
				break;
			case "eyebomb":
				Add(new Puffer(entity3, vector));
				break;
			case "flingBird":
				Add(new FlingBird(entity3, vector));
				break;
			case "flingBirdIntro":
				Add(new FlingBirdIntro(entity3, vector));
				break;
			case "birdPath":
				Add(new BirdPath(entityID, entity3, vector));
				break;
			case "lightningBlock":
				Add(new LightningBreakerBox(entity3, vector));
				break;
			case "spikesUp":
				Add(new Spikes(entity3, vector, Spikes.Directions.Up));
				break;
			case "spikesDown":
				Add(new Spikes(entity3, vector, Spikes.Directions.Down));
				break;
			case "spikesLeft":
				Add(new Spikes(entity3, vector, Spikes.Directions.Left));
				break;
			case "spikesRight":
				Add(new Spikes(entity3, vector, Spikes.Directions.Right));
				break;
			case "triggerSpikesUp":
				Add(new TriggerSpikes(entity3, vector, TriggerSpikes.Directions.Up));
				break;
			case "triggerSpikesDown":
				Add(new TriggerSpikes(entity3, vector, TriggerSpikes.Directions.Down));
				break;
			case "triggerSpikesRight":
				Add(new TriggerSpikes(entity3, vector, TriggerSpikes.Directions.Right));
				break;
			case "triggerSpikesLeft":
				Add(new TriggerSpikes(entity3, vector, TriggerSpikes.Directions.Left));
				break;
			case "darkChaser":
				Add(new BadelineOldsite(entity3, vector, num));
				num++;
				break;
			case "rotateSpinner":
				if (Session.Area.ID == 10)
				{
					Add(new StarRotateSpinner(entity3, vector));
				}
				else if (Session.Area.ID == 3 || (Session.Area.ID == 7 && Session.Level.StartsWith("d-")))
				{
					Add(new DustRotateSpinner(entity3, vector));
				}
				else
				{
					Add(new BladeRotateSpinner(entity3, vector));
				}
				break;
			case "trackSpinner":
				if (Session.Area.ID == 10)
				{
					Add(new StarTrackSpinner(entity3, vector));
				}
				else if (Session.Area.ID == 3 || (Session.Area.ID == 7 && Session.Level.StartsWith("d-")))
				{
					Add(new DustTrackSpinner(entity3, vector));
				}
				else
				{
					Add(new BladeTrackSpinner(entity3, vector));
				}
				break;
			case "spinner":
			{
				if (Session.Area.ID == 3 || (Session.Area.ID == 7 && Session.Level.StartsWith("d-")))
				{
					Add(new DustStaticSpinner(entity3, vector));
					break;
				}
				CrystalColor color = CrystalColor.Blue;
				if (Session.Area.ID == 5)
				{
					color = CrystalColor.Red;
				}
				else if (Session.Area.ID == 6)
				{
					color = CrystalColor.Purple;
				}
				else if (Session.Area.ID == 10)
				{
					color = CrystalColor.Rainbow;
				}
				Add(new CrystalStaticSpinner(entity3, vector, color));
				break;
			}
			case "sinkingPlatform":
				Add(new SinkingPlatform(entity3, vector));
				break;
			case "friendlyGhost":
				Add(new AngryOshiro(entity3, vector));
				break;
			case "seeker":
				Add(new Seeker(entity3, vector));
				break;
			case "seekerStatue":
				Add(new SeekerStatue(entity3, vector));
				break;
			case "slider":
				Add(new Slider(entity3, vector));
				break;
			case "templeBigEyeball":
				Add(new TempleBigEyeball(entity3, vector));
				break;
			case "crushBlock":
				Add(new CrushBlock(entity3, vector));
				break;
			case "bigSpinner":
				Add(new Bumper(entity3, vector));
				break;
			case "starJumpBlock":
				Add(new StarJumpBlock(entity3, vector));
				break;
			case "floatySpaceBlock":
				Add(new FloatySpaceBlock(entity3, vector));
				break;
			case "glassBlock":
				Add(new GlassBlock(entity3, vector));
				break;
			case "goldenBlock":
				Add(new GoldenBlock(entity3, vector));
				break;
			case "fireBall":
				Add(new FireBall(entity3, vector));
				break;
			case "risingLava":
				Add(new RisingLava(entity3, vector));
				break;
			case "sandwichLava":
				Add(new SandwichLava(entity3, vector));
				break;
			case "killbox":
				Add(new Killbox(entity3, vector));
				break;
			case "fakeHeart":
				Add(new FakeHeart(entity3, vector));
				break;
			case "lightning":
				if (entity3.Bool("perLevel") || !Session.GetFlag("disable_lightning"))
				{
					Add(new Lightning(entity3, vector));
					flag2 = true;
				}
				break;
			case "finalBoss":
				Add(new FinalBoss(entity3, vector));
				break;
			case "finalBossFallingBlock":
				Add(FallingBlock.CreateFinalBossBlock(entity3, vector));
				break;
			case "finalBossMovingBlock":
				Add(new FinalBossMovingBlock(entity3, vector));
				break;
			case "fakeWall":
				Add(new FakeWall(entityID, entity3, vector, FakeWall.Modes.Wall));
				break;
			case "fakeBlock":
				Add(new FakeWall(entityID, entity3, vector, FakeWall.Modes.Block));
				break;
			case "dashBlock":
				Add(new DashBlock(entity3, vector, entityID));
				break;
			case "invisibleBarrier":
				Add(new InvisibleBarrier(entity3, vector));
				break;
			case "exitBlock":
				Add(new ExitBlock(entity3, vector));
				break;
			case "conditionBlock":
			{
				ConditionBlockModes conditionBlockModes = entity3.Enum("condition", ConditionBlockModes.Key);
				EntityID none = EntityID.None;
				string[] array = entity3.Attr("conditionID").Split(':');
				none.Level = array[0];
				none.ID = Convert.ToInt32(array[1]);
				if (conditionBlockModes switch
				{
					ConditionBlockModes.Button => Session.GetFlag(DashSwitch.GetFlagName(none)), 
					ConditionBlockModes.Key => Session.DoNotLoad.Contains(none), 
					ConditionBlockModes.Strawberry => Session.Strawberries.Contains(none), 
					_ => throw new Exception("Condition type not supported!"), 
				})
				{
					Add(new ExitBlock(entity3, vector));
				}
				break;
			}
			case "coverupWall":
				Add(new CoverupWall(entity3, vector));
				break;
			case "crumbleWallOnRumble":
				Add(new CrumbleWallOnRumble(entity3, vector, entityID));
				break;
			case "ridgeGate":
				if (GotCollectables(entity3))
				{
					Add(new RidgeGate(entity3, vector));
				}
				break;
			case "tentacles":
				Add(new ReflectionTentacles(entity3, vector));
				break;
			case "starClimbController":
				Add(new StarJumpController());
				break;
			case "playerSeeker":
				Add(new PlayerSeeker(entity3, vector));
				break;
			case "chaserBarrier":
				Add(new ChaserBarrier(entity3, vector));
				break;
			case "introCrusher":
				Add(new IntroCrusher(entity3, vector));
				break;
			case "bridge":
				Add(new Bridge(entity3, vector));
				break;
			case "bridgeFixed":
				Add(new BridgeFixed(entity3, vector));
				break;
			case "bird":
				Add(new BirdNPC(entity3, vector));
				break;
			case "introCar":
				Add(new IntroCar(entity3, vector));
				break;
			case "memorial":
				Add(new Memorial(entity3, vector));
				break;
			case "wire":
				Add(new Wire(entity3, vector));
				break;
			case "cobweb":
				Add(new Cobweb(entity3, vector));
				break;
			case "lamp":
				Add(new Lamp(vector + entity3.Position, entity3.Bool("broken")));
				break;
			case "hanginglamp":
				Add(new HangingLamp(entity3, vector + entity3.Position));
				break;
			case "hahaha":
				Add(new Hahaha(entity3, vector));
				break;
			case "bonfire":
				Add(new Bonfire(entity3, vector));
				break;
			case "payphone":
				Add(new Payphone(vector + entity3.Position));
				break;
			case "colorSwitch":
				Add(new ClutterSwitch(entity3, vector));
				break;
			case "clutterDoor":
				Add(new ClutterDoor(entity3, vector, Session));
				break;
			case "dreammirror":
				Add(new DreamMirror(vector + entity3.Position));
				break;
			case "resortmirror":
				Add(new ResortMirror(entity3, vector));
				break;
			case "towerviewer":
				Add(new Lookout(entity3, vector));
				break;
			case "picoconsole":
				Add(new PicoConsole(entity3, vector));
				break;
			case "wavedashmachine":
				Add(new WaveDashTutorialMachine(entity3, vector));
				break;
			case "yellowBlocks":
				ClutterBlockGenerator.Init(this);
				ClutterBlockGenerator.Add((int)(entity3.Position.X / 8f), (int)(entity3.Position.Y / 8f), entity3.Width / 8, entity3.Height / 8, ClutterBlock.Colors.Yellow);
				break;
			case "redBlocks":
				ClutterBlockGenerator.Init(this);
				ClutterBlockGenerator.Add((int)(entity3.Position.X / 8f), (int)(entity3.Position.Y / 8f), entity3.Width / 8, entity3.Height / 8, ClutterBlock.Colors.Red);
				break;
			case "greenBlocks":
				ClutterBlockGenerator.Init(this);
				ClutterBlockGenerator.Add((int)(entity3.Position.X / 8f), (int)(entity3.Position.Y / 8f), entity3.Width / 8, entity3.Height / 8, ClutterBlock.Colors.Green);
				break;
			case "oshirodoor":
				Add(new MrOshiroDoor(entity3, vector));
				break;
			case "templeMirrorPortal":
				Add(new TempleMirrorPortal(entity3, vector));
				break;
			case "reflectionHeartStatue":
				Add(new ReflectionHeartStatue(entity3, vector));
				break;
			case "resortRoofEnding":
				Add(new ResortRoofEnding(entity3, vector));
				break;
			case "gondola":
				Add(new Gondola(entity3, vector));
				break;
			case "birdForsakenCityGem":
				Add(new ForsakenCitySatellite(entity3, vector));
				break;
			case "whiteblock":
				Add(new WhiteBlock(entity3, vector));
				break;
			case "plateau":
				Add(new Plateau(entity3, vector));
				break;
			case "soundSource":
				Add(new SoundSourceEntity(entity3, vector));
				break;
			case "templeMirror":
				Add(new TempleMirror(entity3, vector));
				break;
			case "templeEye":
				Add(new TempleEye(entity3, vector));
				break;
			case "clutterCabinet":
				Add(new ClutterCabinet(entity3, vector));
				break;
			case "floatingDebris":
				Add(new FloatingDebris(entity3, vector));
				break;
			case "foregroundDebris":
				Add(new ForegroundDebris(entity3, vector));
				break;
			case "moonCreature":
				Add(new MoonCreature(entity3, vector));
				break;
			case "lightbeam":
				Add(new LightBeam(entity3, vector));
				break;
			case "door":
				Add(new Door(entity3, vector));
				break;
			case "trapdoor":
				Add(new Trapdoor(entity3, vector));
				break;
			case "resortLantern":
				Add(new ResortLantern(entity3, vector));
				break;
			case "water":
				Add(new Water(entity3, vector));
				break;
			case "waterfall":
				Add(new WaterFall(entity3, vector));
				break;
			case "bigWaterfall":
				Add(new BigWaterfall(entity3, vector));
				break;
			case "clothesline":
				Add(new Clothesline(entity3, vector));
				break;
			case "cliffflag":
				Add(new CliffFlags(entity3, vector));
				break;
			case "cliffside_flag":
				Add(new CliffsideWindFlag(entity3, vector));
				break;
			case "flutterbird":
				Add(new FlutterBird(entity3, vector));
				break;
			case "SoundTest3d":
				Add(new _3dSoundTest(entity3, vector));
				break;
			case "SummitBackgroundManager":
				Add(new AscendManager(entity3, vector));
				break;
			case "summitGemManager":
				Add(new SummitGemManager(entity3, vector));
				break;
			case "heartGemDoor":
				Add(new HeartGemDoor(entity3, vector));
				break;
			case "summitcheckpoint":
				Add(new SummitCheckpoint(entity3, vector));
				break;
			case "summitcloud":
				Add(new SummitCloud(entity3, vector));
				break;
			case "coreMessage":
				Add(new CoreMessage(entity3, vector));
				break;
			case "playbackTutorial":
				Add(new PlayerPlayback(entity3, vector));
				break;
			case "playbackBillboard":
				Add(new PlaybackBillboard(entity3, vector));
				break;
			case "cutsceneNode":
				Add(new CutsceneNode(entity3, vector));
				break;
			case "kevins_pc":
				Add(new KevinsPC(entity3, vector));
				break;
			case "powerSourceNumber":
				Add(new PowerSourceNumber(entity3.Position + vector, entity3.Int("number", 1), GotCollectables(entity3)));
				break;
			case "npc":
			{
				string text = entity3.Attr("npc").ToLower();
				Vector2 position = entity3.Position + vector;
				switch (text)
				{
				case "granny_00_house":
					Add(new NPC00_Granny(position));
					break;
				case "theo_01_campfire":
					Add(new NPC01_Theo(position));
					break;
				case "theo_02_campfire":
					Add(new NPC02_Theo(position));
					break;
				case "theo_03_escaping":
					if (!Session.GetFlag("resort_theo"))
					{
						Add(new NPC03_Theo_Escaping(position));
					}
					break;
				case "theo_03_vents":
					Add(new NPC03_Theo_Vents(position));
					break;
				case "oshiro_03_lobby":
					Add(new NPC03_Oshiro_Lobby(position));
					break;
				case "oshiro_03_hallway":
					Add(new NPC03_Oshiro_Hallway1(position));
					break;
				case "oshiro_03_hallway2":
					Add(new NPC03_Oshiro_Hallway2(position));
					break;
				case "oshiro_03_bigroom":
					Add(new NPC03_Oshiro_Cluttter(entity3, vector));
					break;
				case "oshiro_03_breakdown":
					Add(new NPC03_Oshiro_Breakdown(position));
					break;
				case "oshiro_03_suite":
					Add(new NPC03_Oshiro_Suite(position));
					break;
				case "oshiro_03_rooftop":
					Add(new NPC03_Oshiro_Rooftop(position));
					break;
				case "granny_04_cliffside":
					Add(new NPC04_Granny(position));
					break;
				case "theo_04_cliffside":
					Add(new NPC04_Theo(position));
					break;
				case "theo_05_entrance":
					Add(new NPC05_Theo_Entrance(position));
					break;
				case "theo_05_inmirror":
					Add(new NPC05_Theo_Mirror(position));
					break;
				case "evil_05":
					Add(new NPC05_Badeline(entity3, vector));
					break;
				case "theo_06_plateau":
					Add(new NPC06_Theo_Plateau(entity3, vector));
					break;
				case "granny_06_intro":
					Add(new NPC06_Granny(entity3, vector));
					break;
				case "badeline_06_crying":
					Add(new NPC06_Badeline_Crying(entity3, vector));
					break;
				case "granny_06_ending":
					Add(new NPC06_Granny_Ending(entity3, vector));
					break;
				case "theo_06_ending":
					Add(new NPC06_Theo_Ending(entity3, vector));
					break;
				case "granny_07x":
					Add(new NPC07X_Granny_Ending(entity3, vector));
					break;
				case "theo_08_inside":
					Add(new NPC08_Theo(entity3, vector));
					break;
				case "granny_08_inside":
					Add(new NPC08_Granny(entity3, vector));
					break;
				case "granny_09_outside":
					Add(new NPC09_Granny_Outside(entity3, vector));
					break;
				case "granny_09_inside":
					Add(new NPC09_Granny_Inside(entity3, vector));
					break;
				case "gravestone_10":
					Add(new NPC10_Gravestone(entity3, vector));
					break;
				case "granny_10_never":
					Add(new NPC07X_Granny_Ending(entity3, vector, ch9EasterEgg: true));
					break;
				}
				break;
			}
			}
		}
		ClutterBlockGenerator.Generate();
		foreach (EntityData trigger in levelData.Triggers)
		{
			int entityID2 = trigger.ID + 10000000;
			EntityID entityID3 = new EntityID(levelData.Name, entityID2);
			if (Session.DoNotLoad.Contains(entityID3))
			{
				continue;
			}
			switch (trigger.Name)
			{
			case "eventTrigger":
				Add(new EventTrigger(trigger, vector));
				break;
			case "musicFadeTrigger":
				Add(new MusicFadeTrigger(trigger, vector));
				break;
			case "musicTrigger":
				Add(new MusicTrigger(trigger, vector));
				break;
			case "altMusicTrigger":
				Add(new AltMusicTrigger(trigger, vector));
				break;
			case "cameraOffsetTrigger":
				Add(new CameraOffsetTrigger(trigger, vector));
				break;
			case "lightFadeTrigger":
				Add(new LightFadeTrigger(trigger, vector));
				break;
			case "bloomFadeTrigger":
				Add(new BloomFadeTrigger(trigger, vector));
				break;
			case "cameraTargetTrigger":
			{
				string text2 = trigger.Attr("deleteFlag");
				if (string.IsNullOrEmpty(text2) || !Session.GetFlag(text2))
				{
					Add(new CameraTargetTrigger(trigger, vector));
				}
				break;
			}
			case "cameraAdvanceTargetTrigger":
				Add(new CameraAdvanceTargetTrigger(trigger, vector));
				break;
			case "respawnTargetTrigger":
				Add(new RespawnTargetTrigger(trigger, vector));
				break;
			case "changeRespawnTrigger":
				Add(new ChangeRespawnTrigger(trigger, vector));
				break;
			case "windTrigger":
				Add(new WindTrigger(trigger, vector));
				break;
			case "windAttackTrigger":
				Add(new WindAttackTrigger(trigger, vector));
				break;
			case "minitextboxTrigger":
				Add(new MiniTextboxTrigger(trigger, vector, entityID3));
				break;
			case "oshiroTrigger":
				Add(new OshiroTrigger(trigger, vector));
				break;
			case "interactTrigger":
				Add(new InteractTrigger(trigger, vector));
				break;
			case "checkpointBlockerTrigger":
				Add(new CheckpointBlockerTrigger(trigger, vector));
				break;
			case "lookoutBlocker":
				Add(new LookoutBlocker(trigger, vector));
				break;
			case "stopBoostTrigger":
				Add(new StopBoostTrigger(trigger, vector));
				break;
			case "noRefillTrigger":
				Add(new NoRefillTrigger(trigger, vector));
				break;
			case "ambienceParamTrigger":
				Add(new AmbienceParamTrigger(trigger, vector));
				break;
			case "creditsTrigger":
				Add(new CreditsTrigger(trigger, vector));
				break;
			case "goldenBerryCollectTrigger":
				Add(new GoldBerryCollectTrigger(trigger, vector));
				break;
			case "moonGlitchBackgroundTrigger":
				Add(new MoonGlitchBackgroundTrigger(trigger, vector));
				break;
			case "blackholeStrength":
				Add(new BlackholeStrengthTrigger(trigger, vector));
				break;
			case "rumbleTrigger":
				Add(new RumbleTrigger(trigger, vector, entityID3));
				break;
			case "birdPathTrigger":
				Add(new BirdPathTrigger(trigger, vector));
				break;
			case "spawnFacingTrigger":
				Add(new SpawnFacingTrigger(trigger, vector));
				break;
			case "detachFollowersTrigger":
				Add(new DetachStrawberryTrigger(trigger, vector));
				break;
			}
		}
		foreach (DecalData fgDecal in levelData.FgDecals)
		{
			Add(new Decal(fgDecal.Texture, vector + fgDecal.Position, fgDecal.Scale, -10500));
		}
		foreach (DecalData bgDecal in levelData.BgDecals)
		{
			Add(new Decal(bgDecal.Texture, vector + bgDecal.Position, bgDecal.Scale, 9000));
		}
		if (playerIntro != Player.IntroTypes.Transition)
		{
			if (Session.JustStarted && !Session.StartedFromBeginning && startPosition.HasValue && !StartPosition.HasValue)
			{
				StartPosition = startPosition;
			}
			if (!Session.RespawnPoint.HasValue)
			{
				if (StartPosition.HasValue)
				{
					Session.RespawnPoint = GetSpawnPoint(StartPosition.Value);
				}
				else
				{
					Session.RespawnPoint = DefaultSpawnPoint;
				}
			}
			PlayerSpriteMode spriteMode = ((!Session.Inventory.Backpack) ? PlayerSpriteMode.MadelineNoBackpack : PlayerSpriteMode.Madeline);
			Player player = new Player(Session.RespawnPoint.Value, spriteMode);
			player.IntroType = playerIntro;
			Add(player);
			base.Entities.UpdateLists();
			CameraLockModes cameraLockMode = CameraLockMode;
			CameraLockMode = CameraLockModes.None;
			Camera.Position = GetFullCameraTargetAt(player, player.Position);
			CameraLockMode = cameraLockMode;
			CameraUpwardMaxY = Camera.Y + 180f;
			foreach (EntityID key in Session.Keys)
			{
				Add(new Key(player, key));
			}
			SpotlightWipe.FocusPoint = Session.RespawnPoint.Value - Camera.Position;
			if (playerIntro != Player.IntroTypes.Respawn && playerIntro != Player.IntroTypes.Fall)
			{
				new SpotlightWipe(this, wipeIn: true);
			}
			else
			{
				DoScreenWipe(wipeIn: true);
			}
			if (isFromLoader)
			{
				base.RendererList.UpdateLists();
			}
			if (DarkRoom)
			{
				Lighting.Alpha = Session.DarkRoomAlpha;
			}
			else
			{
				Lighting.Alpha = BaseLightingAlpha + Session.LightingAlphaAdd;
			}
			Bloom.Base = AreaData.Get(Session).BloomBase + Session.BloomBaseAdd;
		}
		else
		{
			base.Entities.UpdateLists();
		}
		if (HasCassetteBlocks && ShouldCreateCassetteManager)
		{
			base.Tracker.GetEntity<CassetteBlockManager>()?.OnLevelStart();
		}
		if (!string.IsNullOrEmpty(levelData.ObjTiles))
		{
			Tileset tileset = new Tileset(GFX.Game["tilesets/scenery"], 8, 8);
			int[,] array2 = Calc.ReadCSVIntGrid(levelData.ObjTiles, Bounds.Width / 8, Bounds.Height / 8);
			for (int k = 0; k < array2.GetLength(0); k++)
			{
				for (int l = 0; l < array2.GetLength(1); l++)
				{
					if (array2[k, l] != -1)
					{
						TileInterceptor.TileCheck(this, tileset[array2[k, l]], new Vector2(k * 8, l * 8) + LevelOffset);
					}
				}
			}
		}
		LightningRenderer entity2 = base.Tracker.GetEntity<LightningRenderer>();
		if (entity2 != null)
		{
			if (flag2)
			{
				entity2.StartAmbience();
			}
			else
			{
				entity2.StopAmbience();
			}
		}
		Calc.PopRandom();
	}

	public void UnloadLevel()
	{
		List<Entity> entitiesExcludingTagMask = GetEntitiesExcludingTagMask(Tags.Global);
		foreach (Entity entity in base.Tracker.GetEntities<Textbox>())
		{
			entitiesExcludingTagMask.Add(entity);
		}
		UnloadEntities(entitiesExcludingTagMask);
		base.Entities.UpdateLists();
	}

	public void Reload()
	{
		if (!Completed)
		{
			if (Session.FirstLevel && Session.Strawberries.Count <= 0 && !Session.Cassette && !Session.HeartGem && !Session.HitCheckpoint)
			{
				Session.Time = 0L;
				Session.Deaths = 0;
				TimerStarted = false;
			}
			Session.Dashes = Session.DashesAtLevelStart;
			Glitch.Value = 0f;
			Engine.TimeRate = 1f;
			Distort.Anxiety = 0f;
			Distort.GameRate = 1f;
			Audio.SetMusicParam("fade", 1f);
			ParticlesBG.Clear();
			Particles.Clear();
			ParticlesFG.Clear();
			TrailManager.Clear();
			UnloadLevel();
			GC.Collect();
			GC.WaitForPendingFinalizers();
			LoadLevel(Player.IntroTypes.Respawn);
			strawberriesDisplay.DrawLerp = 0f;
			WindController windController = base.Entities.FindFirst<WindController>();
			if (windController != null)
			{
				windController.SnapWind();
			}
			else
			{
				Wind = Vector2.Zero;
			}
		}
	}

	private bool GotCollectables(EntityData e)
	{
		bool flag = true;
		bool flag2 = true;
		List<EntityID> list = new List<EntityID>();
		if (e.Attr("strawberries").Length > 0)
		{
			string[] array = e.Attr("strawberries").Split(',');
			foreach (string obj in array)
			{
				EntityID none = EntityID.None;
				string[] array2 = obj.Split(':');
				none.Level = array2[0];
				none.ID = Convert.ToInt32(array2[1]);
				list.Add(none);
			}
		}
		foreach (EntityID item in list)
		{
			if (!Session.Strawberries.Contains(item))
			{
				flag = false;
				break;
			}
		}
		List<EntityID> list2 = new List<EntityID>();
		if (e.Attr("keys").Length > 0)
		{
			string[] array = e.Attr("keys").Split(',');
			foreach (string obj2 in array)
			{
				EntityID none2 = EntityID.None;
				string[] array3 = obj2.Split(':');
				none2.Level = array3[0];
				none2.ID = Convert.ToInt32(array3[1]);
				list2.Add(none2);
			}
		}
		foreach (EntityID item2 in list2)
		{
			if (!Session.DoNotLoad.Contains(item2))
			{
				flag2 = false;
				break;
			}
		}
		return flag2 && flag;
	}

	public void TransitionTo(LevelData next, Vector2 direction)
	{
		Session.CoreMode = CoreMode;
		transition = new Coroutine(TransitionRoutine(next, direction));
	}

	[IteratorStateMachine(typeof(_003CTransitionRoutine_003Ed__126))]
	private IEnumerator TransitionRoutine(LevelData next, Vector2 direction)
	{
		Player player = base.Tracker.GetEntity<Player>();
		List<Entity> toRemove = GetEntitiesExcludingTagMask((int)Tags.Persistent | (int)Tags.Global);
		List<Component> transitionOut = base.Tracker.GetComponentsCopy<TransitionListener>();
		player.CleanUpTriggers();
		foreach (SoundSource component in base.Tracker.GetComponents<SoundSource>())
		{
			if (component.DisposeOnTransition)
			{
				component.Stop();
			}
		}
		PreviousBounds = Bounds;
		Session.Level = next.Name;
		Session.FirstLevel = false;
		Session.DeathsInCurrentLevel = 0;
		LoadLevel(Player.IntroTypes.Transition);
		Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "has_conveyors", (base.Tracker.GetEntities<WallBooster>().Count > 0) ? 1 : 0);
		List<Component> transitionIn = base.Tracker.GetComponentsCopy<TransitionListener>();
		transitionIn.RemoveAll((Component c) => transitionOut.Contains(c));
		GC.Collect();
		float cameraAt = 0f;
		Vector2 cameraFrom = Camera.Position;
		Vector2 dirPad = direction * 4f;
		if (direction == Vector2.UnitY)
		{
			dirPad = direction * 12f;
		}
		Vector2 playerTo = player.Position;
		while (direction.X != 0f && playerTo.Y >= (float)Bounds.Bottom)
		{
			playerTo.Y -= 1f;
		}
		for (; !IsInBounds(playerTo, dirPad); playerTo += direction)
		{
		}
		Vector2 cameraTo = GetFullCameraTargetAt(player, playerTo);
		Vector2 position = player.Position;
		player.Position = playerTo;
		foreach (Entity item in player.CollideAll<WindTrigger>())
		{
			if (!toRemove.Contains(item))
			{
				windController.SetPattern((item as WindTrigger).Pattern);
				break;
			}
		}
		windController.SetStartPattern();
		player.Position = position;
		foreach (TransitionListener item2 in transitionOut)
		{
			if (item2.OnOutBegin != null)
			{
				item2.OnOutBegin();
			}
		}
		foreach (TransitionListener item3 in transitionIn)
		{
			if (item3.OnInBegin != null)
			{
				item3.OnInBegin();
			}
		}
		float lightingStart = Lighting.Alpha;
		float lightingEnd = (DarkRoom ? Session.DarkRoomAlpha : (BaseLightingAlpha + Session.LightingAlphaAdd));
		bool lightingWait = lightingStart >= Session.DarkRoomAlpha || lightingEnd >= Session.DarkRoomAlpha;
		if (lightingEnd > lightingStart && lightingWait)
		{
			Audio.Play("event:/game/05_mirror_temple/room_lightlevel_down");
			while (Lighting.Alpha != lightingEnd)
			{
				yield return null;
				Lighting.Alpha = Calc.Approach(Lighting.Alpha, lightingEnd, 2f * Engine.DeltaTime);
			}
		}
		bool cameraFinished = false;
		while (!player.TransitionTo(playerTo, direction) || cameraAt < 1f)
		{
			yield return null;
			if (cameraFinished)
			{
				continue;
			}
			cameraAt = Calc.Approach(cameraAt, 1f, Engine.DeltaTime / NextTransitionDuration);
			if (cameraAt > 0.9f)
			{
				Camera.Position = cameraTo;
			}
			else
			{
				Camera.Position = Vector2.Lerp(cameraFrom, cameraTo, Ease.CubeOut(cameraAt));
			}
			if (!lightingWait && lightingStart < lightingEnd)
			{
				Lighting.Alpha = lightingStart + (lightingEnd - lightingStart) * cameraAt;
			}
			foreach (TransitionListener item4 in transitionOut)
			{
				if (item4.OnOut != null)
				{
					item4.OnOut(cameraAt);
				}
			}
			foreach (TransitionListener item5 in transitionIn)
			{
				if (item5.OnIn != null)
				{
					item5.OnIn(cameraAt);
				}
			}
			if (cameraAt >= 1f)
			{
				cameraFinished = true;
			}
		}
		if (lightingEnd < lightingStart && lightingWait)
		{
			Audio.Play("event:/game/05_mirror_temple/room_lightlevel_up");
			while (Lighting.Alpha != lightingEnd)
			{
				yield return null;
				Lighting.Alpha = Calc.Approach(Lighting.Alpha, lightingEnd, 2f * Engine.DeltaTime);
			}
		}
		UnloadEntities(toRemove);
		base.Entities.UpdateLists();
		Rectangle bounds = Bounds;
		bounds.Inflate(16, 16);
		Particles.ClearRect(bounds, inside: false);
		ParticlesBG.ClearRect(bounds, inside: false);
		ParticlesFG.ClearRect(bounds, inside: false);
		Vector2 to = player.CollideFirst<RespawnTargetTrigger>()?.Target ?? player.Position;
		Session.RespawnPoint = Session.LevelData.Spawns.ClosestTo(to);
		player.OnTransition();
		foreach (TransitionListener item6 in transitionIn)
		{
			if (item6.OnInEnd != null)
			{
				item6.OnInEnd();
			}
		}
		if (Session.LevelData.DelayAltMusic)
		{
			Audio.SetAltMusic(SFX.EventnameByHandle(Session.LevelData.AltMusic));
		}
		NextTransitionDuration = 0.65f;
		transition = null;
	}

	public void UnloadEntities(List<Entity> entities)
	{
		foreach (Entity entity in entities)
		{
			Remove(entity);
		}
	}

	public Vector2 GetSpawnPoint(Vector2 from)
	{
		return Session.GetSpawnPoint(from);
	}

	public Vector2 GetFullCameraTargetAt(Player player, Vector2 at)
	{
		Vector2 position = player.Position;
		player.Position = at;
		foreach (Entity entity in base.Tracker.GetEntities<Trigger>())
		{
			if (entity is CameraTargetTrigger && player.CollideCheck(entity))
			{
				(entity as CameraTargetTrigger).OnStay(player);
			}
			else if (entity is CameraOffsetTrigger && player.CollideCheck(entity))
			{
				(entity as CameraOffsetTrigger).OnEnter(player);
			}
		}
		Vector2 cameraTarget = player.CameraTarget;
		player.Position = position;
		return cameraTarget;
	}

	public void TeleportTo(Player player, string nextLevel, Player.IntroTypes introType, Vector2? nearestSpawn = null)
	{
		Leader.StoreStrawberries(player.Leader);
		Vector2 position = player.Position;
		Remove(player);
		UnloadLevel();
		Session.Level = nextLevel;
		Session.RespawnPoint = GetSpawnPoint(new Vector2(Bounds.Left, Bounds.Top) + (nearestSpawn.HasValue ? nearestSpawn.Value : Vector2.Zero));
		if (introType == Player.IntroTypes.Transition)
		{
			player.Position = Session.RespawnPoint.Value;
			player.Hair.MoveHairBy(player.Position - position);
			player.MuffleLanding = true;
			Add(player);
			LoadLevel(Player.IntroTypes.Transition);
			base.Entities.UpdateLists();
		}
		else
		{
			LoadLevel(introType);
			base.Entities.UpdateLists();
			player = base.Tracker.GetEntity<Player>();
		}
		Camera.Position = player.CameraTarget;
		Update();
		Leader.RestoreStrawberries(player.Leader);
	}

	public void AutoSave()
	{
		if (saving == null)
		{
			saving = new Coroutine(SavingRoutine());
		}
	}

	public bool IsAutoSaving()
	{
		return saving != null;
	}

	[IteratorStateMachine(typeof(_003CSavingRoutine_003Ed__141))]
	private IEnumerator SavingRoutine()
	{
		UserIO.SaveHandler(file: true, settings: false);
		while (UserIO.Saving)
		{
			yield return null;
		}
		saving = null;
	}

	public void UpdateTime()
	{
		if (InCredits || Session.Area.ID == 8 || TimerStopped)
		{
			return;
		}
		long ticks = TimeSpan.FromSeconds(Engine.RawDeltaTime).Ticks;
		SaveData.Instance.AddTime(Session.Area, ticks);
		if (!TimerStarted && !InCutscene)
		{
			Player entity = base.Tracker.GetEntity<Player>();
			if (entity != null && !entity.TimePaused)
			{
				TimerStarted = true;
			}
		}
		if (!Completed && TimerStarted)
		{
			Session.Time += ticks;
		}
	}

	public override void Update()
	{
		if (unpauseTimer > 0f)
		{
			unpauseTimer -= Engine.RawDeltaTime;
			UpdateTime();
			return;
		}
		if (Overlay != null)
		{
			Overlay.Update();
			base.Entities.UpdateLists();
			return;
		}
		int num = 10;
		if (!InCutscene && base.Tracker.GetEntity<Player>() != null && Wipe == null && !Frozen)
		{
			num = SaveData.Instance.Assists.GameSpeed;
		}
		Engine.TimeRateB = (float)num / 10f;
		if (num != 10)
		{
			if (AssistSpeedSnapshot == null || AssistSpeedSnapshotValue != num)
			{
				Audio.ReleaseSnapshot(AssistSpeedSnapshot);
				AssistSpeedSnapshot = null;
				AssistSpeedSnapshotValue = num;
				if (AssistSpeedSnapshotValue < 10)
				{
					AssistSpeedSnapshot = Audio.CreateSnapshot("snapshot:/assist_game_speed/assist_speed_" + AssistSpeedSnapshotValue * 10);
				}
				else if (AssistSpeedSnapshotValue <= 16)
				{
					AssistSpeedSnapshot = Audio.CreateSnapshot("snapshot:/variant_speed/variant_speed_" + AssistSpeedSnapshotValue * 10);
				}
			}
		}
		else if (AssistSpeedSnapshot != null)
		{
			Audio.ReleaseSnapshot(AssistSpeedSnapshot);
			AssistSpeedSnapshot = null;
			AssistSpeedSnapshotValue = -1;
		}
		if (wasPaused && !Paused)
		{
			EndPauseEffects();
		}
		if (CanPause && Input.QuickRestart.Pressed)
		{
			Input.QuickRestart.ConsumeBuffer();
			Pause(0, minimal: false, quickReset: true);
		}
		else if (CanPause && (Input.Pause.Pressed || Input.ESC.Pressed))
		{
			Input.Pause.ConsumeBuffer();
			Input.ESC.ConsumeBuffer();
			Pause();
		}
		if (wasPaused && !Paused)
		{
			wasPaused = false;
		}
		if (Paused)
		{
			wasPausedTimer = 0f;
		}
		else
		{
			wasPausedTimer += Engine.DeltaTime;
		}
		UpdateTime();
		if (saving != null)
		{
			saving.Update();
		}
		if (!Paused)
		{
			glitchTimer += Engine.DeltaTime;
			glitchSeed = Calc.Random.NextFloat();
		}
		if (SkippingCutscene)
		{
			if (skipCoroutine != null)
			{
				skipCoroutine.Update();
			}
			base.RendererList.Update();
		}
		else if (FrozenOrPaused)
		{
			bool disabled = MInput.Disabled;
			MInput.Disabled = false;
			if (!Paused)
			{
				foreach (Entity item in base[Tags.FrozenUpdate])
				{
					if (item.Active)
					{
						item.Update();
					}
				}
			}
			foreach (Entity item2 in base[Tags.PauseUpdate])
			{
				if (item2.Active)
				{
					item2.Update();
				}
			}
			MInput.Disabled = disabled;
			if (Wipe != null)
			{
				Wipe.Update(this);
			}
			if (HiresSnow != null)
			{
				HiresSnow.Update(this);
			}
			base.Entities.UpdateLists();
		}
		else if (!Transitioning)
		{
			if (RetryPlayerCorpse == null)
			{
				base.Update();
			}
			else
			{
				RetryPlayerCorpse.Update();
				base.RendererList.Update();
				foreach (Entity item3 in base[Tags.PauseUpdate])
				{
					if (item3.Active)
					{
						item3.Update();
					}
				}
			}
		}
		else
		{
			foreach (Entity item4 in base[Tags.TransitionUpdate])
			{
				item4.Update();
			}
			transition.Update();
			base.RendererList.Update();
		}
		HudRenderer.BackgroundFade = Calc.Approach(HudRenderer.BackgroundFade, Paused ? 1f : 0f, 8f * Engine.RawDeltaTime);
		if (!FrozenOrPaused)
		{
			WindSineTimer += Engine.DeltaTime;
			WindSine = (float)(Math.Sin(WindSineTimer) + 1.0) / 2f;
		}
		foreach (PostUpdateHook component in base.Tracker.GetComponents<PostUpdateHook>())
		{
			if (component.Entity.Active)
			{
				component.OnPostUpdate();
			}
		}
		if (updateHair)
		{
			foreach (Component component2 in base.Tracker.GetComponents<PlayerHair>())
			{
				if (component2.Active && component2.Entity.Active)
				{
					(component2 as PlayerHair).AfterUpdate();
				}
			}
			if (FrozenOrPaused)
			{
				updateHair = false;
			}
		}
		else if (!FrozenOrPaused)
		{
			updateHair = true;
		}
		if (shakeTimer > 0f)
		{
			if (OnRawInterval(0.04f))
			{
				int num2 = (int)Math.Ceiling(shakeTimer * 10f);
				if (shakeDirection == Vector2.Zero)
				{
					ShakeVector = new Vector2(-num2 + Calc.Random.Next(num2 * 2 + 1), -num2 + Calc.Random.Next(num2 * 2 + 1));
				}
				else
				{
					if (lastDirectionalShake == 0)
					{
						lastDirectionalShake = 1;
					}
					else
					{
						lastDirectionalShake *= -1;
					}
					ShakeVector = -shakeDirection * lastDirectionalShake * num2;
				}
				if (Settings.Instance.ScreenShake == ScreenshakeAmount.Half)
				{
					float x = Math.Sign(ShakeVector.X);
					float y = Math.Sign(ShakeVector.Y);
					ShakeVector = new Vector2(x, y);
				}
			}
			float num3 = ((Settings.Instance.ScreenShake == ScreenshakeAmount.Half) ? 1.5f : 1f);
			shakeTimer -= Engine.RawDeltaTime * num3;
		}
		else
		{
			ShakeVector = Vector2.Zero;
		}
		if (doFlash)
		{
			flash = Calc.Approach(flash, 1f, Engine.DeltaTime * 10f);
			if (flash >= 1f)
			{
				doFlash = false;
			}
		}
		else if (flash > 0f)
		{
			flash = Calc.Approach(flash, 0f, Engine.DeltaTime * 3f);
		}
		if (lastColorGrade != Session.ColorGrade)
		{
			if (colorGradeEase >= 1f)
			{
				colorGradeEase = 0f;
				lastColorGrade = Session.ColorGrade;
			}
			else
			{
				colorGradeEase = Calc.Approach(colorGradeEase, 1f, Engine.DeltaTime * colorGradeEaseSpeed);
			}
		}
		if (Celeste.PlayMode == Celeste.PlayModes.Debug)
		{
			if (MInput.Keyboard.Pressed(Keys.Tab) && Engine.Scene.Tracker.GetEntity<KeyboardConfigUI>() == null && Engine.Scene.Tracker.GetEntity<ButtonConfigUI>() == null)
			{
				Engine.Scene = new MapEditor(Session.Area);
			}
			if (MInput.Keyboard.Pressed(Keys.F1))
			{
				Celeste.ReloadAssets(levels: true, graphics: false, hires: false, Session.Area);
				Engine.Scene = new LevelLoader(Session);
			}
			else if (MInput.Keyboard.Pressed(Keys.F2))
			{
				Celeste.ReloadAssets(levels: true, graphics: true, hires: false, Session.Area);
				Engine.Scene = new LevelLoader(Session);
			}
			else if (MInput.Keyboard.Pressed(Keys.F3))
			{
				Celeste.ReloadAssets(levels: true, graphics: true, hires: true, Session.Area);
				Engine.Scene = new LevelLoader(Session);
			}
		}
	}

	public override void BeforeRender()
	{
		cameraPreShake = Camera.Position;
		Camera.Position += ShakeVector;
		Camera.Position = Camera.Position.Floor();
		foreach (BeforeRenderHook component in base.Tracker.GetComponents<BeforeRenderHook>())
		{
			if (component.Visible)
			{
				component.Callback();
			}
		}
		SpeedRing.DrawToBuffer(this);
		base.BeforeRender();
	}

	public override void Render()
	{
		Engine.Instance.GraphicsDevice.SetRenderTarget(GameplayBuffers.Gameplay);
		Engine.Instance.GraphicsDevice.Clear(Color.Transparent);
		GameplayRenderer.Render(this);
		Lighting.Render(this);
		Engine.Instance.GraphicsDevice.SetRenderTarget(GameplayBuffers.Level);
		Engine.Instance.GraphicsDevice.Clear(BackgroundColor);
		Background.Render(this);
		Distort.Render((RenderTarget2D)GameplayBuffers.Gameplay, (RenderTarget2D)GameplayBuffers.Displacement, Displacement.HasDisplacement(this));
		Bloom.Apply(GameplayBuffers.Level, this);
		Foreground.Render(this);
		Glitch.Apply(GameplayBuffers.Level, glitchTimer * 2f, glitchSeed, (float)Math.PI * 2f);
		if (Engine.DashAssistFreeze)
		{
			PlayerDashAssist entity = base.Tracker.GetEntity<PlayerDashAssist>();
			if (entity != null)
			{
				Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.Matrix);
				entity.Render();
				Draw.SpriteBatch.End();
			}
		}
		if (flash > 0f)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
			Draw.Rect(-1f, -1f, 322f, 182f, flashColor * flash);
			Draw.SpriteBatch.End();
			if (flashDrawPlayer)
			{
				Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.Matrix);
				Player entity2 = base.Tracker.GetEntity<Player>();
				if (entity2 != null && entity2.Visible)
				{
					entity2.Render();
				}
				Draw.SpriteBatch.End();
			}
		}
		Engine.Instance.GraphicsDevice.SetRenderTarget(null);
		Engine.Instance.GraphicsDevice.Clear(Color.Black);
		Engine.Instance.GraphicsDevice.Viewport = Engine.Viewport;
		Matrix matrix = Matrix.CreateScale(6f) * Engine.ScreenMatrix;
		Vector2 vector = new Vector2(320f, 180f);
		Vector2 vector2 = vector / ZoomTarget;
		Vector2 vector3 = ((ZoomTarget != 1f) ? ((ZoomFocusPoint - vector2 / 2f) / (vector - vector2) * vector) : Vector2.Zero);
		MTexture orDefault = GFX.ColorGrades.GetOrDefault(lastColorGrade, GFX.ColorGrades["none"]);
		MTexture orDefault2 = GFX.ColorGrades.GetOrDefault(Session.ColorGrade, GFX.ColorGrades["none"]);
		if (colorGradeEase > 0f && orDefault != orDefault2)
		{
			ColorGrade.Set(orDefault, orDefault2, colorGradeEase);
		}
		else
		{
			ColorGrade.Set(orDefault2);
		}
		float scale = Zoom * ((320f - ScreenPadding * 2f) / 320f);
		Vector2 vector4 = new Vector2(ScreenPadding, ScreenPadding * 0.5625f);
		if (SaveData.Instance.Assists.MirrorMode)
		{
			vector4.X = 0f - vector4.X;
			vector3.X = 160f - (vector3.X - 160f);
		}
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, ColorGrade.Effect, matrix);
		Draw.SpriteBatch.Draw((RenderTarget2D)GameplayBuffers.Level, vector3 + vector4, GameplayBuffers.Level.Bounds, Color.White, 0f, vector3, scale, SaveData.Instance.Assists.MirrorMode ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		Draw.SpriteBatch.End();
		if (Pathfinder != null && Pathfinder.DebugRenderEnabled)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.Matrix * matrix);
			Pathfinder.Render();
			Draw.SpriteBatch.End();
		}
		if (((!Paused || !PauseMainMenuOpen) && !(wasPausedTimer < 1f)) || !Input.MenuJournal.Check || !AllowHudHide)
		{
			HudRenderer.Render(this);
		}
		if (Wipe != null)
		{
			Wipe.Render(this);
		}
		if (HiresSnow != null)
		{
			HiresSnow.Render(this);
		}
	}

	public override void AfterRender()
	{
		base.AfterRender();
		Camera.Position = cameraPreShake;
	}

	private void StartPauseEffects()
	{
		if (Audio.CurrentMusic == "event:/music/lvl0/bridge")
		{
			Audio.PauseMusic = true;
		}
		Audio.PauseGameplaySfx = true;
		Audio.Play("event:/ui/game/pause");
		if (PauseSnapshot == null)
		{
			PauseSnapshot = Audio.CreateSnapshot("snapshot:/pause_menu");
		}
	}

	private void EndPauseEffects()
	{
		Audio.PauseMusic = false;
		Audio.PauseGameplaySfx = false;
		Audio.ReleaseSnapshot(PauseSnapshot);
		PauseSnapshot = null;
	}

	public void Pause(int startIndex = 0, bool minimal = false, bool quickReset = false)
	{
		wasPaused = true;
		Player player = base.Tracker.GetEntity<Player>();
		if (!Paused)
		{
			StartPauseEffects();
		}
		Paused = true;
		if (quickReset)
		{
			Audio.Play("event:/ui/main/message_confirm");
			PauseMainMenuOpen = false;
			GiveUp(0, restartArea: true, minimal, showHint: false);
			return;
		}
		PauseMainMenuOpen = true;
		TextMenu menu = new TextMenu();
		if (!minimal)
		{
			menu.Add(new TextMenu.Header(Dialog.Clean("menu_pause_title")));
		}
		menu.Add(new TextMenu.Button(Dialog.Clean("menu_pause_resume")).Pressed(delegate
		{
			menu.OnCancel();
		}));
		if (InCutscene && !SkippingCutscene)
		{
			menu.Add(new TextMenu.Button(Dialog.Clean("menu_pause_skip_cutscene")).Pressed(delegate
			{
				SkipCutscene();
				Paused = false;
				PauseMainMenuOpen = false;
				menu.RemoveSelf();
			}));
		}
		if (!minimal && !InCutscene && !SkippingCutscene)
		{
			TextMenu.Item item;
			menu.Add(item = new TextMenu.Button(Dialog.Clean("menu_pause_retry")).Pressed(delegate
			{
				if (player != null && !player.Dead)
				{
					Engine.TimeRate = 1f;
					Distort.GameRate = 1f;
					Distort.Anxiety = 0f;
					InCutscene = (SkippingCutscene = false);
					RetryPlayerCorpse = player.Die(Vector2.Zero, evenIfInvincible: true);
					foreach (LevelEndingHook component in base.Tracker.GetComponents<LevelEndingHook>())
					{
						if (component.OnEnd != null)
						{
							component.OnEnd();
						}
					}
				}
				Paused = false;
				PauseMainMenuOpen = false;
				EndPauseEffects();
				menu.RemoveSelf();
			}));
			item.Disabled = !CanRetry || (player != null && !player.CanRetry) || Frozen || Completed;
		}
		if (!minimal && SaveData.Instance.AssistMode)
		{
			TextMenu.Item item2 = null;
			menu.Add(item2 = new TextMenu.Button(Dialog.Clean("menu_pause_assist")).Pressed(delegate
			{
				menu.RemoveSelf();
				PauseMainMenuOpen = false;
				AssistMode(menu.IndexOf(item2), minimal);
			}));
		}
		if (!minimal && SaveData.Instance.VariantMode)
		{
			TextMenu.Item item3 = null;
			menu.Add(item3 = new TextMenu.Button(Dialog.Clean("menu_pause_variant")).Pressed(delegate
			{
				menu.RemoveSelf();
				PauseMainMenuOpen = false;
				VariantMode(menu.IndexOf(item3), minimal);
			}));
		}
		TextMenu.Item item4 = null;
		menu.Add(item4 = new TextMenu.Button(Dialog.Clean("menu_pause_options")).Pressed(delegate
		{
			menu.RemoveSelf();
			PauseMainMenuOpen = false;
			Options(menu.IndexOf(item4), minimal);
		}));
		if (!minimal && Celeste.PlayMode != Celeste.PlayModes.Event)
		{
			TextMenu.Item item5 = null;
			menu.Add(item5 = new TextMenu.Button(Dialog.Clean("menu_pause_savequit")).Pressed(delegate
			{
				menu.Focused = false;
				Engine.TimeRate = 1f;
				Audio.SetMusic(null);
				Audio.BusStopAll("bus:/gameplay_sfx", immediate: true);
				Session.InArea = true;
				Session.Deaths++;
				Session.DeathsInCurrentLevel++;
				SaveData.Instance.AddDeath(Session.Area);
				DoScreenWipe(wipeIn: false, delegate
				{
					Engine.Scene = new LevelExit(LevelExit.Mode.SaveAndQuit, Session, HiresSnow);
				}, hiresSnow: true);
				foreach (LevelEndingHook component2 in base.Tracker.GetComponents<LevelEndingHook>())
				{
					if (component2.OnEnd != null)
					{
						component2.OnEnd();
					}
				}
			}));
			if (SaveQuitDisabled || (player != null && player.StateMachine.State == 18))
			{
				item5.Disabled = true;
			}
		}
		if (!minimal)
		{
			menu.Add(new TextMenu.SubHeader(""));
			TextMenu.Item item6 = null;
			menu.Add(item6 = new TextMenu.Button(Dialog.Clean("menu_pause_restartarea")).Pressed(delegate
			{
				PauseMainMenuOpen = false;
				menu.RemoveSelf();
				GiveUp(menu.IndexOf(item6), restartArea: true, minimal, showHint: true);
			}));
			(item6 as TextMenu.Button).ConfirmSfx = "event:/ui/main/message_confirm";
			if (SaveData.Instance.Areas[0].Modes[0].Completed || SaveData.Instance.DebugMode || SaveData.Instance.CheatMode)
			{
				TextMenu.Item item7 = null;
				menu.Add(item7 = new TextMenu.Button(Dialog.Clean("menu_pause_return")).Pressed(delegate
				{
					PauseMainMenuOpen = false;
					menu.RemoveSelf();
					GiveUp(menu.IndexOf(item7), restartArea: false, minimal, showHint: false);
				}));
				(item7 as TextMenu.Button).ConfirmSfx = "event:/ui/main/message_confirm";
			}
			if (Celeste.PlayMode == Celeste.PlayModes.Event)
			{
				menu.Add(new TextMenu.Button(Dialog.Clean("menu_pause_restartdemo")).Pressed(delegate
				{
					EndPauseEffects();
					Audio.SetMusic(null);
					menu.Focused = false;
					DoScreenWipe(wipeIn: false, delegate
					{
						LevelEnter.Go(new Session(new AreaKey(0)), fromSaveData: false);
					});
				}));
			}
		}
		menu.OnESC = (menu.OnCancel = (menu.OnPause = delegate
		{
			PauseMainMenuOpen = false;
			menu.RemoveSelf();
			Paused = false;
			Audio.Play("event:/ui/game/unpause");
			unpauseTimer = 0.15f;
		}));
		if (startIndex > 0)
		{
			menu.Selection = startIndex;
		}
		Add(menu);
	}

	private void GiveUp(int returnIndex, bool restartArea, bool minimal, bool showHint)
	{
		Paused = true;
		QuickResetHint quickHint = null;
		ReturnMapHint returnHint = null;
		if (!restartArea)
		{
			Add(returnHint = new ReturnMapHint());
		}
		TextMenu menu = new TextMenu();
		menu.AutoScroll = false;
		menu.Position = new Vector2((float)Engine.Width / 2f, (float)Engine.Height / 2f - 100f);
		menu.Add(new TextMenu.Header(Dialog.Clean(restartArea ? "menu_restart_title" : "menu_return_title")));
		menu.Add(new TextMenu.Button(Dialog.Clean(restartArea ? "menu_restart_continue" : "menu_return_continue")).Pressed(delegate
		{
			Engine.TimeRate = 1f;
			menu.Focused = false;
			Session.InArea = false;
			Audio.SetMusic(null);
			Audio.BusStopAll("bus:/gameplay_sfx", immediate: true);
			if (restartArea)
			{
				DoScreenWipe(wipeIn: false, delegate
				{
					Engine.Scene = new LevelExit(LevelExit.Mode.Restart, Session);
				});
			}
			else
			{
				DoScreenWipe(wipeIn: false, delegate
				{
					Engine.Scene = new LevelExit(LevelExit.Mode.GiveUp, Session, HiresSnow);
				}, hiresSnow: true);
			}
			foreach (LevelEndingHook component in base.Tracker.GetComponents<LevelEndingHook>())
			{
				if (component.OnEnd != null)
				{
					component.OnEnd();
				}
			}
		}));
		menu.Add(new TextMenu.Button(Dialog.Clean(restartArea ? "menu_restart_cancel" : "menu_return_cancel")).Pressed(delegate
		{
			menu.OnCancel();
		}));
		menu.OnPause = (menu.OnESC = delegate
		{
			menu.RemoveSelf();
			if (quickHint != null)
			{
				quickHint.RemoveSelf();
			}
			if (returnHint != null)
			{
				returnHint.RemoveSelf();
			}
			Paused = false;
			unpauseTimer = 0.15f;
			Audio.Play("event:/ui/game/unpause");
		});
		menu.OnCancel = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			menu.RemoveSelf();
			if (quickHint != null)
			{
				quickHint.RemoveSelf();
			}
			if (returnHint != null)
			{
				returnHint.RemoveSelf();
			}
			Pause(returnIndex, minimal);
		};
		Add(menu);
	}

	private void Options(int returnIndex, bool minimal)
	{
		Paused = true;
		bool oldAllowHudHide = AllowHudHide;
		AllowHudHide = false;
		TextMenu options = MenuOptions.Create(inGame: true, PauseSnapshot);
		options.OnESC = (options.OnCancel = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			AllowHudHide = oldAllowHudHide;
			options.CloseAndRun(SaveFromOptions(), delegate
			{
				Pause(returnIndex, minimal);
			});
		});
		options.OnPause = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			options.CloseAndRun(SaveFromOptions(), delegate
			{
				AllowHudHide = oldAllowHudHide;
				Paused = false;
				unpauseTimer = 0.15f;
			});
		};
		Add(options);
	}

	[IteratorStateMachine(typeof(_003CSaveFromOptions_003Ed__152))]
	private IEnumerator SaveFromOptions()
	{
		UserIO.SaveHandler(file: false, settings: true);
		while (UserIO.Saving)
		{
			yield return null;
		}
	}

	private void AssistMode(int returnIndex, bool minimal)
	{
		Paused = true;
		TextMenu menu = new TextMenu();
		menu.Add(new TextMenu.Header(Dialog.Clean("MENU_ASSIST_TITLE")));
		menu.Add(new TextMenu.Slider(Dialog.Clean("MENU_ASSIST_GAMESPEED"), (int i) => i * 10 + "%", 5, 10, SaveData.Instance.Assists.GameSpeed).Change(delegate(int i)
		{
			SaveData.Instance.Assists.GameSpeed = i;
			Engine.TimeRateB = (float)SaveData.Instance.Assists.GameSpeed / 10f;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_ASSIST_INFINITE_STAMINA"), SaveData.Instance.Assists.InfiniteStamina).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.InfiniteStamina = on;
		}));
		TextMenu.Option<int> option;
		menu.Add(option = new TextMenu.Slider(Dialog.Clean("MENU_ASSIST_AIR_DASHES"), (int i) => i switch
		{
			0 => Dialog.Clean("MENU_ASSIST_AIR_DASHES_NORMAL"), 
			1 => Dialog.Clean("MENU_ASSIST_AIR_DASHES_TWO"), 
			_ => Dialog.Clean("MENU_ASSIST_AIR_DASHES_INFINITE"), 
		}, 0, 2, (int)SaveData.Instance.Assists.DashMode).Change(delegate(int on)
		{
			SaveData.Instance.Assists.DashMode = (Assists.DashModes)on;
			Player entity = base.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				entity.Dashes = Math.Min(entity.Dashes, entity.MaxDashes);
			}
		}));
		if (Session.Area.ID == 0)
		{
			option.Disabled = true;
		}
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_ASSIST_DASH_ASSIST"), SaveData.Instance.Assists.DashAssist).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.DashAssist = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_ASSIST_INVINCIBLE"), SaveData.Instance.Assists.Invincible).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.Invincible = on;
		}));
		menu.OnESC = (menu.OnCancel = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			Pause(returnIndex, minimal);
			menu.Close();
		});
		menu.OnPause = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			Paused = false;
			unpauseTimer = 0.15f;
			menu.Close();
		};
		Add(menu);
	}

	private void VariantMode(int returnIndex, bool minimal)
	{
		Paused = true;
		TextMenu menu = new TextMenu();
		menu.Add(new TextMenu.Header(Dialog.Clean("MENU_VARIANT_TITLE")));
		menu.Add(new TextMenu.SubHeader(Dialog.Clean("MENU_VARIANT_SUBTITLE")));
		TextMenu.Slider speed;
		menu.Add(speed = new TextMenu.Slider(Dialog.Clean("MENU_ASSIST_GAMESPEED"), (int i) => i * 10 + "%", 5, 16, SaveData.Instance.Assists.GameSpeed));
		speed.Change(delegate(int i)
		{
			if (i > 10)
			{
				i = ((speed.Values[speed.PreviousIndex].Item2 <= i) ? (i + 1) : (i - 1));
			}
			speed.Index = i - 5;
			SaveData.Instance.Assists.GameSpeed = i;
			Engine.TimeRateB = (float)SaveData.Instance.Assists.GameSpeed / 10f;
		});
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_MIRROR"), SaveData.Instance.Assists.MirrorMode).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.MirrorMode = on;
			Input.MoveX.Inverted = (Input.Aim.InvertedX = (Input.Feather.InvertedX = on));
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_360DASHING"), SaveData.Instance.Assists.ThreeSixtyDashing).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.ThreeSixtyDashing = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_INVISMOTION"), SaveData.Instance.Assists.InvisibleMotion).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.InvisibleMotion = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_NOGRABBING"), SaveData.Instance.Assists.NoGrabbing).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.NoGrabbing = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_LOWFRICTION"), SaveData.Instance.Assists.LowFriction).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.LowFriction = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_SUPERDASHING"), SaveData.Instance.Assists.SuperDashing).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.SuperDashing = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_HICCUPS"), SaveData.Instance.Assists.Hiccups).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.Hiccups = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_VARIANT_PLAYASBADELINE"), SaveData.Instance.Assists.PlayAsBadeline).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.PlayAsBadeline = on;
			Player entity = base.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				PlayerSpriteMode mode = (SaveData.Instance.Assists.PlayAsBadeline ? PlayerSpriteMode.MadelineAsBadeline : entity.DefaultSpriteMode);
				if (entity.Active)
				{
					entity.ResetSpriteNextFrame(mode);
				}
				else
				{
					entity.ResetSprite(mode);
				}
			}
		}));
		menu.Add(new TextMenu.SubHeader(Dialog.Clean("MENU_ASSIST_SUBTITLE")));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_ASSIST_INFINITE_STAMINA"), SaveData.Instance.Assists.InfiniteStamina).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.InfiniteStamina = on;
		}));
		TextMenu.Option<int> option;
		menu.Add(option = new TextMenu.Slider(Dialog.Clean("MENU_ASSIST_AIR_DASHES"), (int i) => i switch
		{
			0 => Dialog.Clean("MENU_ASSIST_AIR_DASHES_NORMAL"), 
			1 => Dialog.Clean("MENU_ASSIST_AIR_DASHES_TWO"), 
			_ => Dialog.Clean("MENU_ASSIST_AIR_DASHES_INFINITE"), 
		}, 0, 2, (int)SaveData.Instance.Assists.DashMode).Change(delegate(int on)
		{
			SaveData.Instance.Assists.DashMode = (Assists.DashModes)on;
			Player entity = base.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				entity.Dashes = Math.Min(entity.Dashes, entity.MaxDashes);
			}
		}));
		if (Session.Area.ID == 0)
		{
			option.Disabled = true;
		}
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_ASSIST_DASH_ASSIST"), SaveData.Instance.Assists.DashAssist).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.DashAssist = on;
		}));
		menu.Add(new TextMenu.OnOff(Dialog.Clean("MENU_ASSIST_INVINCIBLE"), SaveData.Instance.Assists.Invincible).Change(delegate(bool on)
		{
			SaveData.Instance.Assists.Invincible = on;
		}));
		menu.OnESC = (menu.OnCancel = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			Pause(returnIndex, minimal);
			menu.Close();
		});
		menu.OnPause = delegate
		{
			Audio.Play("event:/ui/main/button_back");
			Paused = false;
			unpauseTimer = 0.15f;
			menu.Close();
		};
		Add(menu);
	}

	public void SnapColorGrade(string next)
	{
		if (Session.ColorGrade != next)
		{
			lastColorGrade = next;
			colorGradeEase = 0f;
			colorGradeEaseSpeed = 1f;
			Session.ColorGrade = next;
		}
	}

	public void NextColorGrade(string next, float speed = 1f)
	{
		if (Session.ColorGrade != next)
		{
			colorGradeEase = 0f;
			colorGradeEaseSpeed = speed;
			Session.ColorGrade = next;
		}
	}

	public void Shake(float time = 0.3f)
	{
		if (Settings.Instance.ScreenShake != ScreenshakeAmount.Off)
		{
			shakeDirection = Vector2.Zero;
			shakeTimer = Math.Max(shakeTimer, time);
		}
	}

	public void StopShake()
	{
		shakeTimer = 0f;
	}

	public void DirectionalShake(Vector2 dir, float time = 0.3f)
	{
		if (Settings.Instance.ScreenShake != ScreenshakeAmount.Off)
		{
			shakeDirection = dir.SafeNormalize();
			lastDirectionalShake = 0;
			shakeTimer = Math.Max(shakeTimer, time);
		}
	}

	public void Flash(Color color, bool drawPlayerOver = false)
	{
		if (!Settings.Instance.DisableFlashes)
		{
			doFlash = true;
			flashDrawPlayer = drawPlayerOver;
			flash = 1f;
			flashColor = color;
		}
	}

	public void ZoomSnap(Vector2 screenSpaceFocusPoint, float zoom)
	{
		ZoomFocusPoint = screenSpaceFocusPoint;
		ZoomTarget = (Zoom = zoom);
	}

	[IteratorStateMachine(typeof(_003CZoomTo_003Ed__162))]
	public IEnumerator ZoomTo(Vector2 screenSpaceFocusPoint, float zoom, float duration)
	{
		ZoomFocusPoint = screenSpaceFocusPoint;
		ZoomTarget = zoom;
		float from = Zoom;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			Zoom = MathHelper.Lerp(from, ZoomTarget, Ease.SineInOut(MathHelper.Clamp(p, 0f, 1f)));
			yield return null;
		}
		Zoom = ZoomTarget;
	}

	[IteratorStateMachine(typeof(_003CZoomAcross_003Ed__163))]
	public IEnumerator ZoomAcross(Vector2 screenSpaceFocusPoint, float zoom, float duration)
	{
		float fromZoom = Zoom;
		Vector2 fromFocus = ZoomFocusPoint;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			float amount = Ease.SineInOut(MathHelper.Clamp(p, 0f, 1f));
			Zoom = (ZoomTarget = MathHelper.Lerp(fromZoom, zoom, amount));
			ZoomFocusPoint = Vector2.Lerp(fromFocus, screenSpaceFocusPoint, amount);
			yield return null;
		}
		Zoom = ZoomTarget;
		ZoomFocusPoint = screenSpaceFocusPoint;
	}

	[IteratorStateMachine(typeof(_003CZoomBack_003Ed__164))]
	public IEnumerator ZoomBack(float duration)
	{
		float from = Zoom;
		float to = 1f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			Zoom = MathHelper.Lerp(from, to, Ease.SineInOut(MathHelper.Clamp(p, 0f, 1f)));
			yield return null;
		}
		ResetZoom();
	}

	public void ResetZoom()
	{
		Zoom = 1f;
		ZoomTarget = 1f;
		ZoomFocusPoint = new Vector2(320f, 180f) / 2f;
	}

	public void DoScreenWipe(bool wipeIn, Action onComplete = null, bool hiresSnow = false)
	{
		AreaData.Get(Session).DoScreenWipe(this, wipeIn, onComplete);
		if (hiresSnow)
		{
			Add(HiresSnow = new HiresSnow());
			HiresSnow.Alpha = 0f;
			HiresSnow.AttachAlphaTo = Wipe;
		}
	}

	public bool InsideCamera(Vector2 position, float expand = 0f)
	{
		if (position.X >= Camera.Left - expand && position.X < Camera.Right + expand && position.Y >= Camera.Top - expand)
		{
			return position.Y < Camera.Bottom + expand;
		}
		return false;
	}

	public void EnforceBounds(Player player)
	{
		Rectangle bounds = Bounds;
		Rectangle rectangle = new Rectangle((int)Camera.Left, (int)Camera.Top, 320, 180);
		if (transition != null)
		{
			return;
		}
		if (CameraLockMode == CameraLockModes.FinalBoss && player.Left < (float)rectangle.Left)
		{
			player.Left = rectangle.Left;
			player.OnBoundsH();
		}
		else if (player.Left < (float)bounds.Left)
		{
			if (player.Top >= (float)bounds.Top && player.Bottom < (float)bounds.Bottom && Session.MapData.CanTransitionTo(this, player.Center + Vector2.UnitX * -8f))
			{
				player.BeforeSideTransition();
				NextLevel(player.Center + Vector2.UnitX * -8f, -Vector2.UnitX);
				return;
			}
			player.Left = bounds.Left;
			player.OnBoundsH();
		}
		TheoCrystal entity = base.Tracker.GetEntity<TheoCrystal>();
		if (CameraLockMode == CameraLockModes.FinalBoss && player.Right > (float)rectangle.Right && rectangle.Right < bounds.Right - 4)
		{
			player.Right = rectangle.Right;
			player.OnBoundsH();
		}
		else if (entity != null && (player.Holding == null || !player.Holding.IsHeld) && player.Right > (float)(bounds.Right - 1))
		{
			player.Right = bounds.Right - 1;
		}
		else if (player.Right > (float)bounds.Right)
		{
			if (player.Top >= (float)bounds.Top && player.Bottom < (float)bounds.Bottom && Session.MapData.CanTransitionTo(this, player.Center + Vector2.UnitX * 8f))
			{
				player.BeforeSideTransition();
				NextLevel(player.Center + Vector2.UnitX * 8f, Vector2.UnitX);
				return;
			}
			player.Right = bounds.Right;
			player.OnBoundsH();
		}
		if (CameraLockMode != CameraLockModes.None && player.Top < (float)rectangle.Top)
		{
			player.Top = rectangle.Top;
			player.OnBoundsV();
		}
		else if (player.CenterY < (float)bounds.Top)
		{
			if (Session.MapData.CanTransitionTo(this, player.Center - Vector2.UnitY * 12f))
			{
				player.BeforeUpTransition();
				NextLevel(player.Center - Vector2.UnitY * 12f, -Vector2.UnitY);
				return;
			}
			if (player.Top < (float)(bounds.Top - 24))
			{
				player.Top = bounds.Top - 24;
				player.OnBoundsV();
			}
		}
		if (CameraLockMode != CameraLockModes.None && rectangle.Bottom < bounds.Bottom - 4 && player.Top > (float)rectangle.Bottom)
		{
			if (SaveData.Instance.Assists.Invincible)
			{
				player.Play("event:/game/general/assist_screenbottom");
				player.Bounce(rectangle.Bottom);
			}
			else
			{
				player.Die(Vector2.Zero);
			}
		}
		else if (player.Bottom > (float)bounds.Bottom && Session.MapData.CanTransitionTo(this, player.Center + Vector2.UnitY * 12f) && !Session.LevelData.DisableDownTransition)
		{
			if (!player.CollideCheck<Solid>(player.Position + Vector2.UnitY * 4f))
			{
				player.BeforeDownTransition();
				NextLevel(player.Center + Vector2.UnitY * 12f, Vector2.UnitY);
			}
		}
		else if (player.Top > (float)bounds.Bottom && SaveData.Instance.Assists.Invincible)
		{
			player.Play("event:/game/general/assist_screenbottom");
			player.Bounce(bounds.Bottom);
		}
		else if (player.Top > (float)(bounds.Bottom + 4))
		{
			player.Die(Vector2.Zero);
		}
	}

	public bool IsInBounds(Entity entity)
	{
		Rectangle bounds = Bounds;
		if (entity.Right > (float)bounds.Left && entity.Bottom > (float)bounds.Top && entity.Left < (float)bounds.Right)
		{
			return entity.Top < (float)bounds.Bottom;
		}
		return false;
	}

	public bool IsInBounds(Vector2 position)
	{
		Rectangle bounds = Bounds;
		if (position.X >= (float)bounds.Left && position.Y >= (float)bounds.Top && position.X < (float)bounds.Right)
		{
			return position.Y < (float)bounds.Bottom;
		}
		return false;
	}

	public bool IsInBounds(Vector2 position, float pad)
	{
		Rectangle bounds = Bounds;
		if (position.X >= (float)bounds.Left - pad && position.Y >= (float)bounds.Top - pad && position.X < (float)bounds.Right + pad)
		{
			return position.Y < (float)bounds.Bottom + pad;
		}
		return false;
	}

	public bool IsInBounds(Vector2 position, Vector2 dirPad)
	{
		float num = Math.Max(dirPad.X, 0f);
		float num2 = Math.Max(0f - dirPad.X, 0f);
		float num3 = Math.Max(dirPad.Y, 0f);
		float num4 = Math.Max(0f - dirPad.Y, 0f);
		Rectangle bounds = Bounds;
		if (position.X >= (float)bounds.Left + num && position.Y >= (float)bounds.Top + num3 && position.X < (float)bounds.Right - num2)
		{
			return position.Y < (float)bounds.Bottom - num4;
		}
		return false;
	}

	public bool IsInCamera(Vector2 position, float pad)
	{
		Rectangle rectangle = new Rectangle((int)Camera.X, (int)Camera.Y, 320, 180);
		if (position.X >= (float)rectangle.Left - pad && position.Y >= (float)rectangle.Top - pad && position.X < (float)rectangle.Right + pad)
		{
			return position.Y < (float)rectangle.Bottom + pad;
		}
		return false;
	}

	public void StartCutscene(Action<Level> onSkip, bool fadeInOnSkip = true, bool endingChapterAfterCutscene = false, bool resetZoomOnSkip = true)
	{
		this.endingChapterAfterCutscene = endingChapterAfterCutscene;
		InCutscene = true;
		onCutsceneSkip = onSkip;
		onCutsceneSkipFadeIn = fadeInOnSkip;
		onCutsceneSkipResetZoom = resetZoomOnSkip;
	}

	public void CancelCutscene()
	{
		InCutscene = false;
		SkippingCutscene = false;
	}

	public void SkipCutscene()
	{
		SkippingCutscene = true;
		Engine.TimeRate = 1f;
		Distort.Anxiety = 0f;
		Distort.GameRate = 1f;
		if (endingChapterAfterCutscene)
		{
			Audio.BusStopAll("bus:/gameplay_sfx", immediate: true);
		}
		List<Entity> list = new List<Entity>();
		foreach (Entity entity in base.Tracker.GetEntities<Textbox>())
		{
			list.Add(entity);
		}
		foreach (Entity item in list)
		{
			item.RemoveSelf();
		}
		skipCoroutine = new Coroutine(SkipCutsceneRoutine());
	}

	[IteratorStateMachine(typeof(_003CSkipCutsceneRoutine_003Ed__181))]
	private IEnumerator SkipCutsceneRoutine()
	{
		FadeWipe fadeWipe = new FadeWipe(this, wipeIn: false);
		fadeWipe.Duration = 0.25f;
		yield return fadeWipe.Wait();
		onCutsceneSkip(this);
		strawberriesDisplay.DrawLerp = 0f;
		if (onCutsceneSkipResetZoom)
		{
			ResetZoom();
		}
		GameplayStats gameplayStats = base.Entities.FindFirst<GameplayStats>();
		if (gameplayStats != null)
		{
			gameplayStats.DrawLerp = 0f;
		}
		if (onCutsceneSkipFadeIn)
		{
			FadeWipe fadeWipe2 = new FadeWipe(this, wipeIn: true);
			fadeWipe2.Duration = 0.25f;
			base.RendererList.UpdateLists();
			yield return fadeWipe2.Wait();
		}
		SkippingCutscene = false;
		EndCutscene();
	}

	public void EndCutscene()
	{
		if (!SkippingCutscene)
		{
			InCutscene = false;
		}
	}

	private void NextLevel(Vector2 at, Vector2 dir)
	{
		base.OnEndOfFrame += delegate
		{
			Engine.TimeRate = 1f;
			Distort.Anxiety = 0f;
			Distort.GameRate = 1f;
			TransitionTo(Session.MapData.GetAt(at), dir);
		};
	}

	public void RegisterAreaComplete()
	{
		if (Completed)
		{
			return;
		}
		Player entity = base.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			List<Strawberry> list = new List<Strawberry>();
			foreach (Follower follower in entity.Leader.Followers)
			{
				if (follower.Entity is Strawberry)
				{
					list.Add(follower.Entity as Strawberry);
				}
			}
			foreach (Strawberry item in list)
			{
				item.OnCollect();
			}
		}
		Completed = true;
		SaveData.Instance.RegisterCompletion(Session);
	}

	public ScreenWipe CompleteArea(bool spotlightWipe = true, bool skipScreenWipe = false, bool skipCompleteScreen = false)
	{
		RegisterAreaComplete();
		PauseLock = true;
		Action action = ((!(AreaData.Get(Session).Interlude || skipCompleteScreen)) ? ((Action)delegate
		{
			Engine.Scene = new LevelExit(LevelExit.Mode.Completed, Session);
		}) : ((Action)delegate
		{
			Engine.Scene = new LevelExit(LevelExit.Mode.CompletedInterlude, Session, HiresSnow);
		}));
		if (!SkippingCutscene && !skipScreenWipe)
		{
			if (spotlightWipe)
			{
				Player entity = base.Tracker.GetEntity<Player>();
				if (entity != null)
				{
					SpotlightWipe.FocusPoint = entity.Position - Camera.Position - new Vector2(0f, 8f);
				}
				return new SpotlightWipe(this, wipeIn: false, action);
			}
			return new FadeWipe(this, wipeIn: false, action);
		}
		Audio.BusStopAll("bus:/gameplay_sfx", immediate: true);
		action();
		return null;
	}
}
