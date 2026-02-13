using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_Ending : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__30 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public CS10_Ending _003C_003E4__this;

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
		public _003CCutscene_003Ed__30(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (level.Wipe != null)
				{
					level.Wipe.Cancel();
				}
				goto IL_008f;
			case 1:
				_003C_003E1__state = -1;
				goto IL_008f;
			case 2:
				_003C_003E1__state = -1;
				cS10_Ending.Atlas = Atlas.FromAtlas(Path.Combine("Graphics", "Atlases", "Farewell"), Atlas.AtlasDataFormat.PackerNoAtlas);
				cS10_Ending.Frames = cS10_Ending.Atlas.GetAtlasSubtextures("");
				cS10_Ending.Add(cS10_Ending.attachment = new Image(cS10_Ending.Atlas["21-window"]));
				cS10_Ending.Add(cS10_Ending.picture = new Image(cS10_Ending.Atlas["21-picture"]));
				cS10_Ending.Add(cS10_Ending.ok = new Image(cS10_Ending.Atlas["21-button"]));
				cS10_Ending.Add(cS10_Ending.cursor = new Image(cS10_Ending.Atlas["21-cursor"]));
				cS10_Ending.attachment.Visible = false;
				cS10_Ending.picture.Visible = false;
				cS10_Ending.ok.Visible = false;
				cS10_Ending.cursor.Visible = false;
				level.PauseLock = false;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS10_Ending.cinIntro = Audio.Play("event:/new_content/music/lvl10/cinematic/end_intro");
				Audio.SetAmbience(null);
				cS10_Ending.counting = true;
				cS10_Ending.Add(new Coroutine(cS10_Ending.Fade(1f, 0f, 4f)));
				cS10_Ending.Add(new Coroutine(cS10_Ending.Zoom(1.38f, 1.2f, 4f)));
				_003C_003E2__current = cS10_Ending.Loop("0", 2f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Climb, RumbleLength.TwoSeconds);
				_003C_003E2__current = cS10_Ending.Loop("0,1,1,0,0,1,1,0*8", 2f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
				Audio.SetMusic("event:/new_content/music/lvl10/cinematic/end", startPlaying: true, allowFadeOut: false);
				cS10_Ending.endAmbience = Audio.Play("event:/new_content/env/10_endscene");
				cS10_Ending.Add(new Coroutine(cS10_Ending.Zoom(1.2f, 1.05f, 0.06f, Ease.CubeOut)));
				_003C_003E2__current = cS10_Ending.Play("2-7");
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Ending.Loop("7", 1.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Ending.Play("8-10,10*20");
				_003C_003E1__state = 8;
				return true;
			case 8:
			{
				_003C_003E1__state = -1;
				List<int> frameData = cS10_Ending.GetFrameData("10-13,13*16,14*28,14-17,14*24");
				float num2 = (float)(frameData.Count + 3) * (1f / 12f);
				cS10_Ending.fadeColor = Color.Black;
				cS10_Ending.Add(new Coroutine(cS10_Ending.Zoom(1.05f, 1f, num2, Ease.Linear)));
				cS10_Ending.Add(new Coroutine(cS10_Ending.Fade(0f, 1f, num2 * 0.1f, num2 * 0.85f)));
				cS10_Ending.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
				{
					Audio.Play("event:/new_content/game/10_farewell/endscene_dial_theo");
				}, num2, start: true));
				_003C_003E2__current = cS10_Ending.Play(frameData);
				_003C_003E1__state = 9;
				return true;
			}
			case 9:
				_003C_003E1__state = -1;
				cS10_Ending.frame = 18;
				cS10_Ending.fade = 1f;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Ending.Fade(1f, 0f, 1.2f);
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				cS10_Ending.Add(cS10_Ending.talkingLoop = new Coroutine(cS10_Ending.Loop("18*24,19,19,18*6,20,20")));
				_003C_003E2__current = 1f;
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_END_CINEMATIC", cS10_Ending.ShowPicture);
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				Audio.SetMusicParam("end", 1f);
				Audio.Play("event:/new_content/game/10_farewell/endscene_photo_zoom");
				_003Cp_003E5__2 = 0f;
				break;
			case 14:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__2 += Engine.DeltaTime / 4f;
					break;
				}
				IL_008f:
				if (level.IsAutoSaving())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				Audio.SetParameter(cS10_Ending.endAmbience, "fade", 1f - _003Cp_003E5__2);
				cS10_Ending.computerFade = _003Cp_003E5__2;
				cS10_Ending.picture.Scale = Vector2.One * (0.9f + 0.100000024f * _003Cp_003E5__2);
				_003C_003E2__current = null;
				_003C_003E1__state = 14;
				return true;
			}
			cS10_Ending.EndCutscene(level, removeSelf: false);
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
	private sealed class _003CShowPicture_003Ed__31 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Ending _003C_003E4__this;

		private float _003Cp_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private Vector2 _003Cto_003E5__4;

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
		public _003CShowPicture_003Ed__31(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_Ending.center = new Vector2(1230f, 312f);
				cS10_Ending.Add(new Coroutine(cS10_Ending.Fade(0f, 1f, 0.25f)));
				cS10_Ending.Add(new Coroutine(cS10_Ending.Zoom(1f, 1.1f, 0.25f)));
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (cS10_Ending.talkingLoop != null)
				{
					cS10_Ending.talkingLoop.RemoveSelf();
				}
				cS10_Ending.talkingLoop = null;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_Ending.frame = 21;
				cS10_Ending.cursor.Visible = true;
				cS10_Ending.center = Celeste.TargetCenter;
				cS10_Ending.Add(new Coroutine(cS10_Ending.Fade(1f, 0f, 0.25f)));
				cS10_Ending.Add(new Coroutine(cS10_Ending.Zoom(1.1f, 1f, 0.25f)));
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/game/10_farewell/endscene_attachment_notify");
				cS10_Ending.attachment.Origin = Celeste.TargetCenter;
				cS10_Ending.attachment.Position = Celeste.TargetCenter;
				cS10_Ending.attachment.Visible = true;
				cS10_Ending.attachment.Scale = Vector2.Zero;
				_003Cp_003E5__2 = 0f;
				goto IL_026c;
			case 4:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.3f;
				goto IL_026c;
			case 5:
				_003C_003E1__state = -1;
				cS10_Ending.ok.Position = new Vector2(1208f, 620f);
				cS10_Ending.ok.Origin = cS10_Ending.ok.Position;
				cS10_Ending.ok.Visible = true;
				_003Cp_003E5__2 = 0f;
				goto IL_036f;
			case 6:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.25f;
				goto IL_036f;
			case 7:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__3 = cS10_Ending.cursor.Position;
				_003Cto_003E5__4 = _003Cfrom_003E5__3 + new Vector2(-160f, -190f);
				_003Cp_003E5__2 = 0f;
				goto IL_0448;
			case 8:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.5f;
				goto IL_0448;
			case 9:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__3 = default(Vector2);
				_003Cto_003E5__4 = default(Vector2);
				Audio.Play("event:/new_content/game/10_farewell/endscene_attachment_click");
				_003Cp_003E5__2 = 0f;
				goto IL_0522;
			case 10:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.25f;
				goto IL_0522;
			case 11:
				_003C_003E1__state = -1;
				cS10_Ending.picture.Origin = Celeste.TargetCenter;
				cS10_Ending.picture.Position = Celeste.TargetCenter;
				cS10_Ending.picture.Visible = true;
				_003Cp_003E5__2 = 0f;
				goto IL_0689;
			case 12:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.4f;
				goto IL_0689;
			case 13:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 0.5f;
				goto IL_0754;
			case 14:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0689:
				if (_003Cp_003E5__2 < 1f)
				{
					cS10_Ending.picture.Scale.Y = (0.9f + 0.1f * Ease.BigBackOut(_003Cp_003E5__2)) * 0.9f;
					cS10_Ending.picture.Scale.X = (1.1f - 0.1f * Ease.BigBackOut(_003Cp_003E5__2)) * 0.9f;
					cS10_Ending.picture.Position = Celeste.TargetCenter + Vector2.UnitY * 120f * (1f - Ease.CubeOut(_003Cp_003E5__2));
					cS10_Ending.picture.Color = Color.White * _003Cp_003E5__2;
					_003C_003E2__current = null;
					_003C_003E1__state = 12;
					return true;
				}
				cS10_Ending.picture.Position = Celeste.TargetCenter;
				cS10_Ending.attachment.Visible = false;
				_003Cto_003E5__4 = cS10_Ending.cursor.Position;
				_003Cfrom_003E5__3 = new Vector2(120f, 30f);
				_003Cp_003E5__2 = 0f;
				goto IL_0754;
				IL_036f:
				if (_003Cp_003E5__2 < 1f)
				{
					cS10_Ending.ok.Scale.Y = 0.25f + 0.75f * Ease.BigBackOut(_003Cp_003E5__2);
					cS10_Ending.ok.Scale.X = 1.5f - 0.5f * Ease.BigBackOut(_003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 7;
				return true;
				IL_0754:
				if (_003Cp_003E5__2 < 1f)
				{
					cS10_Ending.cursor.Position = _003Cto_003E5__4 + (_003Cfrom_003E5__3 - _003Cto_003E5__4) * Ease.CubeInOut(_003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 13;
					return true;
				}
				cS10_Ending.cursor.Visible = false;
				_003Cto_003E5__4 = default(Vector2);
				_003Cfrom_003E5__3 = default(Vector2);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 14;
				return true;
				IL_0522:
				if (_003Cp_003E5__2 < 1f)
				{
					cS10_Ending.ok.Scale.Y = 1f - Ease.BigBackIn(_003Cp_003E5__2);
					cS10_Ending.ok.Scale.X = 1f - Ease.BigBackIn(_003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 10;
					return true;
				}
				cS10_Ending.ok.Visible = false;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 11;
				return true;
				IL_026c:
				if (_003Cp_003E5__2 < 1f)
				{
					cS10_Ending.attachment.Scale.Y = 0.25f + 0.75f * Ease.BigBackOut(_003Cp_003E5__2);
					cS10_Ending.attachment.Scale.X = 1.5f - 0.5f * Ease.BigBackOut(_003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
				return true;
				IL_0448:
				if (_003Cp_003E5__2 < 1f)
				{
					cS10_Ending.cursor.Position = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.CubeInOut(_003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 9;
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
	private sealed class _003CEndingRoutine_003Ed__33 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Ending _003C_003E4__this;

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
		public _003CEndingRoutine_003Ed__33(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_Ending.Level.InCutscene = true;
				cS10_Ending.Level.PauseLock = true;
				cS10_Ending.Level.TimerHidden = true;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (Settings.Instance.SpeedrunClock != SpeedrunType.Off)
				{
					cS10_Ending.showTimer = true;
				}
				goto IL_00a5;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00a5;
			case 3:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 0f;
				goto IL_0181;
			case 4:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / 3f;
				goto IL_0181;
			case 5:
				{
					_003C_003E1__state = -1;
					if (cS10_Ending.Atlas != null)
					{
						cS10_Ending.Atlas.Dispose();
					}
					cS10_Ending.Atlas = null;
					cS10_Ending.Level.CompleteArea(spotlightWipe: false, skipScreenWipe: true, skipCompleteScreen: true);
					return false;
				}
				IL_0181:
				if (_003Cp_003E5__2 < 1f)
				{
					Audio.SetMusicParam("fade", 1f - _003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				Audio.SetMusic(null);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 5;
				return true;
				IL_00a5:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				Audio.Play("event:/new_content/game/10_farewell/endscene_final_input");
				cS10_Ending.showTimer = false;
				cS10_Ending.Add(new Coroutine(cS10_Ending.Zoom(1f, 0.75f, 5f, Ease.CubeIn)));
				cS10_Ending.Add(new Coroutine(cS10_Ending.Fade(0f, 1f, 5f)));
				_003C_003E2__current = 4f;
				_003C_003E1__state = 3;
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
	private sealed class _003CZoom_003Ed__37 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Ease.Easer ease;

		public CS10_Ending _003C_003E4__this;

		public float from;

		public float to;

		public float duration;

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
		public _003CZoom_003Ed__37(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (ease == null)
				{
					ease = Ease.Linear;
				}
				cS10_Ending.zoom = from;
				_003Cp_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				cS10_Ending.zoom = from + (to - from) * ease(_003Cp_003E5__2);
				if (cS10_Ending.picture != null)
				{
					cS10_Ending.picture.Scale = Vector2.One * cS10_Ending.zoom;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS10_Ending.zoom = to;
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
	private sealed class _003CPlay_003Ed__39 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Ending _003C_003E4__this;

		public List<int> frames;

		private int _003Ci_003E5__2;

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
		public _003CPlay_003Ed__39(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ci_003E5__2 = 0;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ci_003E5__2++;
				break;
			}
			if (_003Ci_003E5__2 < frames.Count)
			{
				cS10_Ending.frame = frames[_003Ci_003E5__2];
				_003C_003E2__current = 1f / 12f;
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
	private sealed class _003CLoop_003Ed__40 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Ending _003C_003E4__this;

		public string data;

		public float duration;

		private List<int> _003Cframes_003E5__2;

		private float _003Ctime_003E5__3;

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
		public _003CLoop_003Ed__40(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cframes_003E5__2 = cS10_Ending.GetFrameData(data);
				_003Ctime_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Ctime_003E5__3 < duration || duration < 0f)
			{
				cS10_Ending.frame = _003Cframes_003E5__2[(int)(_003Ctime_003E5__3 / (1f / 12f)) % _003Cframes_003E5__2.Count];
				_003Ctime_003E5__3 += Engine.DeltaTime;
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
	private sealed class _003CFade_003Ed__41 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Ending _003C_003E4__this;

		public float from;

		public float delay;

		public float to;

		public float duration;

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
		public _003CFade_003Ed__41(int _003C_003E1__state)
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
			CS10_Ending cS10_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_Ending.fade = from;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 0f;
				break;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Cp_003E5__2 < 1f)
			{
				cS10_Ending.fade = from + (to - from) * _003Cp_003E5__2;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			cS10_Ending.fade = to;
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

	private const int FPS = 12;

	private const float DELAY = 1f / 12f;

	private Atlas Atlas;

	private List<MTexture> Frames;

	private int frame;

	private float fade = 1f;

	private float zoom = 1f;

	private float computerFade;

	private Coroutine talkingLoop;

	private Vector2 center = Celeste.TargetCenter;

	private Coroutine cutscene;

	private Color fadeColor = Color.White;

	private Image attachment;

	private Image cursor;

	private Image ok;

	private Image picture;

	private const float PictureIdleScale = 0.9f;

	private float speedrunTimerEase;

	private string speedrunTimerChapterString;

	private string speedrunTimerFileString;

	private string chapterSpeedrunText = Dialog.Get("OPTIONS_SPEEDRUN_CHAPTER") + ":";

	private string version = Celeste.Instance.Version.ToString();

	private bool showTimer;

	private FMOD.Studio.EventInstance endAmbience;

	private FMOD.Studio.EventInstance cinIntro;

	private bool counting;

	private float timer;

	public CS10_Ending(Player player)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		base.Tag = Tags.HUD;
		player.StateMachine.State = 11;
		player.DummyAutoAnimate = false;
		player.Sprite.Rate = 0f;
		RemoveOnSkipped = false;
		Add(new LevelEndingHook(delegate
		{
			Audio.Stop(cinIntro);
		}));
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Level obj = scene as Level;
		obj.TimerStopped = true;
		obj.TimerHidden = true;
		obj.SaveQuitDisabled = true;
		obj.PauseLock = true;
		obj.AllowHudHide = false;
	}

	public override void OnBegin(Level level)
	{
		Audio.SetAmbience(null);
		level.AutoSave();
		speedrunTimerChapterString = TimeSpan.FromTicks(level.Session.Time).ShortGameplayFormat();
		speedrunTimerFileString = Dialog.FileTime(SaveData.Instance.Time);
		SpeedrunTimerDisplay.CalculateBaseSizes();
		Add(cutscene = new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__30))]
	private IEnumerator Cutscene(Level level)
	{
		if (level.Wipe != null)
		{
			level.Wipe.Cancel();
		}
		while (level.IsAutoSaving())
		{
			yield return null;
		}
		yield return 1f;
		Atlas = Atlas.FromAtlas(Path.Combine("Graphics", "Atlases", "Farewell"), Atlas.AtlasDataFormat.PackerNoAtlas);
		Frames = Atlas.GetAtlasSubtextures("");
		Add(attachment = new Image(Atlas["21-window"]));
		Add(picture = new Image(Atlas["21-picture"]));
		Add(ok = new Image(Atlas["21-button"]));
		Add(cursor = new Image(Atlas["21-cursor"]));
		attachment.Visible = false;
		picture.Visible = false;
		ok.Visible = false;
		cursor.Visible = false;
		level.PauseLock = false;
		yield return 2f;
		cinIntro = Audio.Play("event:/new_content/music/lvl10/cinematic/end_intro");
		Audio.SetAmbience(null);
		counting = true;
		Add(new Coroutine(Fade(1f, 0f, 4f)));
		Add(new Coroutine(Zoom(1.38f, 1.2f, 4f)));
		yield return Loop("0", 2f);
		Input.Rumble(RumbleStrength.Climb, RumbleLength.TwoSeconds);
		yield return Loop("0,1,1,0,0,1,1,0*8", 2f);
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
		Audio.SetMusic("event:/new_content/music/lvl10/cinematic/end", startPlaying: true, allowFadeOut: false);
		endAmbience = Audio.Play("event:/new_content/env/10_endscene");
		Add(new Coroutine(Zoom(1.2f, 1.05f, 0.06f, Ease.CubeOut)));
		yield return Play("2-7");
		yield return Loop("7", 1.5f);
		yield return Play("8-10,10*20");
		List<int> frameData = GetFrameData("10-13,13*16,14*28,14-17,14*24");
		float num = (float)(frameData.Count + 3) * (1f / 12f);
		fadeColor = Color.Black;
		Add(new Coroutine(Zoom(1.05f, 1f, num, Ease.Linear)));
		Add(new Coroutine(Fade(0f, 1f, num * 0.1f, num * 0.85f)));
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			Audio.Play("event:/new_content/game/10_farewell/endscene_dial_theo");
		}, num, start: true));
		yield return Play(frameData);
		frame = 18;
		fade = 1f;
		yield return 0.5f;
		yield return Fade(1f, 0f, 1.2f);
		Add(talkingLoop = new Coroutine(Loop("18*24,19,19,18*6,20,20")));
		yield return 1f;
		yield return Textbox.Say("CH9_END_CINEMATIC", ShowPicture);
		Audio.SetMusicParam("end", 1f);
		Audio.Play("event:/new_content/game/10_farewell/endscene_photo_zoom");
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 4f)
		{
			Audio.SetParameter(endAmbience, "fade", 1f - p);
			computerFade = p;
			picture.Scale = Vector2.One * (0.9f + 0.100000024f * p);
			yield return null;
		}
		EndCutscene(level, removeSelf: false);
	}

	[IteratorStateMachine(typeof(_003CShowPicture_003Ed__31))]
	private IEnumerator ShowPicture()
	{
		center = new Vector2(1230f, 312f);
		Add(new Coroutine(Fade(0f, 1f, 0.25f)));
		Add(new Coroutine(Zoom(1f, 1.1f, 0.25f)));
		yield return 0.25f;
		if (talkingLoop != null)
		{
			talkingLoop.RemoveSelf();
		}
		talkingLoop = null;
		yield return null;
		frame = 21;
		cursor.Visible = true;
		center = Celeste.TargetCenter;
		Add(new Coroutine(Fade(1f, 0f, 0.25f)));
		Add(new Coroutine(Zoom(1.1f, 1f, 0.25f)));
		yield return 0.25f;
		Audio.Play("event:/new_content/game/10_farewell/endscene_attachment_notify");
		attachment.Origin = Celeste.TargetCenter;
		attachment.Position = Celeste.TargetCenter;
		attachment.Visible = true;
		attachment.Scale = Vector2.Zero;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.3f)
		{
			attachment.Scale.Y = 0.25f + 0.75f * Ease.BigBackOut(p);
			attachment.Scale.X = 1.5f - 0.5f * Ease.BigBackOut(p);
			yield return null;
		}
		yield return 0.25f;
		ok.Position = new Vector2(1208f, 620f);
		ok.Origin = ok.Position;
		ok.Visible = true;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.25f)
		{
			ok.Scale.Y = 0.25f + 0.75f * Ease.BigBackOut(p);
			ok.Scale.X = 1.5f - 0.5f * Ease.BigBackOut(p);
			yield return null;
		}
		yield return 0.8f;
		Vector2 from = cursor.Position;
		Vector2 to = from + new Vector2(-160f, -190f);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.5f)
		{
			cursor.Position = from + (to - from) * Ease.CubeInOut(p);
			yield return null;
		}
		yield return 0.2f;
		Audio.Play("event:/new_content/game/10_farewell/endscene_attachment_click");
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.25f)
		{
			ok.Scale.Y = 1f - Ease.BigBackIn(p);
			ok.Scale.X = 1f - Ease.BigBackIn(p);
			yield return null;
		}
		ok.Visible = false;
		yield return 0.1f;
		picture.Origin = Celeste.TargetCenter;
		picture.Position = Celeste.TargetCenter;
		picture.Visible = true;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.4f)
		{
			picture.Scale.Y = (0.9f + 0.1f * Ease.BigBackOut(p)) * 0.9f;
			picture.Scale.X = (1.1f - 0.1f * Ease.BigBackOut(p)) * 0.9f;
			picture.Position = Celeste.TargetCenter + Vector2.UnitY * 120f * (1f - Ease.CubeOut(p));
			picture.Color = Color.White * p;
			yield return null;
		}
		picture.Position = Celeste.TargetCenter;
		attachment.Visible = false;
		to = cursor.Position;
		from = new Vector2(120f, 30f);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.5f)
		{
			cursor.Position = to + (from - to) * Ease.CubeInOut(p);
			yield return null;
		}
		cursor.Visible = false;
		yield return 2f;
	}

	public override void OnEnd(Level level)
	{
		ScreenWipe.WipeColor = Color.Black;
		if (Audio.CurrentMusicEventInstance == null)
		{
			Audio.SetMusic("event:/new_content/music/lvl10/cinematic/end");
		}
		Audio.SetMusicParam("end", 1f);
		frame = 21;
		zoom = 1f;
		fade = 0f;
		fadeColor = Color.Black;
		center = Celeste.TargetCenter;
		picture.Scale = Vector2.One;
		picture.Visible = true;
		picture.Position = Celeste.TargetCenter;
		picture.Origin = Celeste.TargetCenter;
		computerFade = 1f;
		attachment.Visible = false;
		cursor.Visible = false;
		ok.Visible = false;
		Audio.Stop(cinIntro);
		cinIntro = null;
		Audio.Stop(endAmbience);
		endAmbience = null;
		List<Coroutine> list = new List<Coroutine>();
		foreach (Coroutine item in base.Components.GetAll<Coroutine>())
		{
			list.Add(item);
		}
		foreach (Coroutine item2 in list)
		{
			item2.RemoveSelf();
		}
		base.Scene.Entities.FindFirst<Textbox>()?.RemoveSelf();
		Level.InCutscene = true;
		Level.PauseLock = true;
		Level.TimerHidden = true;
		Add(new Coroutine(EndingRoutine()));
	}

	[IteratorStateMachine(typeof(_003CEndingRoutine_003Ed__33))]
	private IEnumerator EndingRoutine()
	{
		Level.InCutscene = true;
		Level.PauseLock = true;
		Level.TimerHidden = true;
		yield return 0.5f;
		if (Settings.Instance.SpeedrunClock != SpeedrunType.Off)
		{
			showTimer = true;
		}
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		Audio.Play("event:/new_content/game/10_farewell/endscene_final_input");
		showTimer = false;
		Add(new Coroutine(Zoom(1f, 0.75f, 5f, Ease.CubeIn)));
		Add(new Coroutine(Fade(0f, 1f, 5f)));
		yield return 4f;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 3f)
		{
			Audio.SetMusicParam("fade", 1f - p);
			yield return null;
		}
		Audio.SetMusic(null);
		yield return 1f;
		if (Atlas != null)
		{
			Atlas.Dispose();
		}
		Atlas = null;
		Level.CompleteArea(spotlightWipe: false, skipScreenWipe: true, skipCompleteScreen: true);
	}

	public override void Update()
	{
		if (counting)
		{
			timer += Engine.DeltaTime;
		}
		speedrunTimerEase = Calc.Approach(speedrunTimerEase, showTimer ? 1f : 0f, Engine.DeltaTime * 4f);
		base.Update();
	}

	public override void Render()
	{
		Draw.Rect(-100f, -100f, 2120f, 1280f, Color.Black);
		if (Atlas != null && Frames != null && frame < Frames.Count)
		{
			MTexture mTexture = Frames[frame];
			MTexture linkedTexture = Atlas.GetLinkedTexture(mTexture.AtlasPath);
			linkedTexture?.DrawJustified(center, new Vector2(center.X / (float)linkedTexture.Width, center.Y / (float)linkedTexture.Height), Color.White, zoom);
			mTexture.DrawJustified(center, new Vector2(center.X / (float)mTexture.Width, center.Y / (float)mTexture.Height), Color.White, zoom);
			if (computerFade > 0f)
			{
				Draw.Rect(0f, 0f, 1920f, 1080f, Color.Black * computerFade);
			}
			base.Render();
			AreaComplete.Info(speedrunTimerEase, speedrunTimerChapterString, speedrunTimerFileString, chapterSpeedrunText, version);
		}
		Draw.Rect(0f, 0f, 1920f, 1080f, fadeColor * fade);
		if ((base.Scene as Level).Paused)
		{
			Draw.Rect(0f, 0f, 1920f, 1080f, Color.Black * 0.5f);
		}
	}

	private List<int> GetFrameData(string data)
	{
		List<int> list = new List<int>();
		string[] array = data.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains('*'))
			{
				string[] array2 = array[i].Split('*');
				int item = int.Parse(array2[0]);
				int num = int.Parse(array2[1]);
				for (int j = 0; j < num; j++)
				{
					list.Add(item);
				}
			}
			else if (array[i].Contains('-'))
			{
				string[] array3 = array[i].Split('-');
				int num2 = int.Parse(array3[0]);
				int num3 = int.Parse(array3[1]);
				for (int k = num2; k <= num3; k++)
				{
					list.Add(k);
				}
			}
			else
			{
				list.Add(int.Parse(array[i]));
			}
		}
		return list;
	}

	[IteratorStateMachine(typeof(_003CZoom_003Ed__37))]
	private IEnumerator Zoom(float from, float to, float duration, Ease.Easer ease = null)
	{
		if (ease == null)
		{
			ease = Ease.Linear;
		}
		zoom = from;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			zoom = from + (to - from) * ease(p);
			if (picture != null)
			{
				picture.Scale = Vector2.One * zoom;
			}
			yield return null;
		}
		zoom = to;
	}

	private IEnumerator Play(string data)
	{
		return Play(GetFrameData(data));
	}

	[IteratorStateMachine(typeof(_003CPlay_003Ed__39))]
	private IEnumerator Play(List<int> frames)
	{
		for (int i = 0; i < frames.Count; i++)
		{
			frame = frames[i];
			yield return 1f / 12f;
		}
	}

	[IteratorStateMachine(typeof(_003CLoop_003Ed__40))]
	private IEnumerator Loop(string data, float duration = -1f)
	{
		List<int> frames = GetFrameData(data);
		float time = 0f;
		while (time < duration || duration < 0f)
		{
			frame = frames[(int)(time / (1f / 12f)) % frames.Count];
			time += Engine.DeltaTime;
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CFade_003Ed__41))]
	private IEnumerator Fade(float from, float to, float duration, float delay = 0f)
	{
		fade = from;
		yield return delay;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / duration)
		{
			fade = from + (to - from) * p;
			yield return null;
		}
		fade = to;
	}
}
