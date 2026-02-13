using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS08_Ending : CutsceneEntity
{
	public class TimeDisplay : Component
	{
		public Vector2 Position;

		public string Time;

		public Vector2 RenderPosition => (((base.Entity != null) ? base.Entity.Position : Vector2.Zero) + Position).RoundV2();

		public TimeDisplay(string time)
			: base(active: true, visible: true)
		{
			Time = time;
		}

		public override void Render()
		{
			SpeedrunTimerDisplay.DrawTime(RenderPosition, Time);
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public CS08_Ending _003C_003E4__this;

		private float _003Cp_003E5__2;

		private float _003Cp_003E5__3;

		private Vector2 _003CposFrom_003E5__4;

		private float _003CscaleFrom_003E5__5;

		private float _003Cp_003E5__6;

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
		public _003CCutscene_003Ed__15(int _003C_003E1__state)
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
			CS08_Ending cS08_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				level.ZoomSnap(new Vector2(164f, 120f), 2f);
				level.Wipe.Cancel();
				new FadeWipe(level, wipeIn: true);
				goto IL_00f1;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00f1;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS08_Ending.player.DummyWalkToExact((int)cS08_Ending.player.X + 16);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("EP_CABIN", cS08_Ending.BadelineEmerges, cS08_Ending.OshiroEnters, cS08_Ending.OshiroSettles, cS08_Ending.MaddyTurns);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = new FadeWipe(cS08_Ending.Level, wipeIn: false)
				{
					Duration = 1.5f
				}.Wait();
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS08_Ending.fade = 1f;
				_003C_003E2__current = Textbox.Say("EP_PIE_START");
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS08_Ending.vignettebg.Visible = true;
				cS08_Ending.vignette.Visible = true;
				cS08_Ending.vignettebg.Color = Color.Black;
				cS08_Ending.vignette.Color = Color.White * 0f;
				cS08_Ending.Add(cS08_Ending.vignette);
				_003Cp_003E5__2 = 0f;
				goto IL_0361;
			case 9:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime;
				goto IL_0361;
			case 10:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 1f;
				_003Cp_003E5__3 = 0f;
				goto IL_0478;
			case 11:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / _003Cp_003E5__2;
				goto IL_0478;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 2f;
				_003CposFrom_003E5__4 = cS08_Ending.vignette.Position;
				_003Cp_003E5__3 = cS08_Ending.vignette.Rotation;
				_003CscaleFrom_003E5__5 = cS08_Ending.vignette.Scale.X;
				_003Cp_003E5__6 = 0f;
				break;
			case 14:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime / _003Cp_003E5__2;
					break;
				}
				IL_0478:
				if (_003Cp_003E5__3 < 1f)
				{
					float num2 = Ease.CubeOut(_003Cp_003E5__3);
					cS08_Ending.vignette.Position = Vector2.Lerp(Celeste.TargetCenter, Celeste.TargetCenter + new Vector2(0f, 140f), num2);
					cS08_Ending.vignette.Scale = Vector2.One * (0.65f + 0.35f * (1f - num2));
					cS08_Ending.vignette.Rotation = -0.025f * num2;
					_003C_003E2__current = null;
					_003C_003E1__state = 11;
					return true;
				}
				_003C_003E2__current = Textbox.Say(cS08_Ending.endingDialog);
				_003C_003E1__state = 12;
				return true;
				IL_0361:
				if (_003Cp_003E5__2 < 1f)
				{
					cS08_Ending.vignette.Color = Color.White * Ease.CubeIn(_003Cp_003E5__2);
					cS08_Ending.vignette.Scale = Vector2.One * (1f + 0.25f * (1f - _003Cp_003E5__2));
					cS08_Ending.vignette.Rotation = 0.05f * (1f - _003Cp_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 9;
					return true;
				}
				cS08_Ending.vignette.Color = Color.White;
				cS08_Ending.vignettebg.Color = Color.White;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 10;
				return true;
				IL_00f1:
				if (cS08_Ending.player == null)
				{
					cS08_Ending.granny = level.Entities.FindFirst<NPC08_Granny>();
					cS08_Ending.theo = level.Entities.FindFirst<NPC08_Theo>();
					cS08_Ending.player = level.Tracker.GetEntity<Player>();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS08_Ending.player.StateMachine.State = 11;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			}
			if (_003Cp_003E5__6 < 1f)
			{
				float amount = Ease.CubeOut(_003Cp_003E5__6);
				cS08_Ending.vignette.Position = Vector2.Lerp(_003CposFrom_003E5__4, Celeste.TargetCenter, amount);
				cS08_Ending.vignette.Scale = Vector2.One * MathHelper.Lerp(_003CscaleFrom_003E5__5, 1f, amount);
				cS08_Ending.vignette.Rotation = MathHelper.Lerp(_003Cp_003E5__3, 0f, amount);
				_003C_003E2__current = null;
				_003C_003E1__state = 14;
				return true;
			}
			_003CposFrom_003E5__4 = default(Vector2);
			cS08_Ending.EndCutscene(level, removeSelf: false);
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
	private sealed class _003CEndingRoutine_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS08_Ending _003C_003E4__this;

		private StrawberriesCounter _003Cstrawbs_003E5__2;

		private DeathsCounter _003Cdeaths_003E5__3;

		private TimeDisplay _003Ctime_003E5__4;

		private float _003CtimeWidth_003E5__5;

		private Vector2 _003Cfrom_003E5__6;

		private Vector2 _003Cto_003E5__7;

		private float _003Cp_003E5__8;

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
		public _003CEndingRoutine_003Ed__17(int _003C_003E1__state)
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
			CS08_Ending cS08_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS08_Ending.Level.InCutscene = true;
				cS08_Ending.Level.PauseLock = true;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				TimeSpan timeSpan = TimeSpan.FromTicks(SaveData.Instance.Time);
				string text = (int)timeSpan.TotalHours + timeSpan.ToString("\\:mm\\:ss\\.fff");
				_003Cstrawbs_003E5__2 = new StrawberriesCounter(centeredX: true, SaveData.Instance.TotalStrawberries, 175, showOutOf: true);
				_003Cdeaths_003E5__3 = new DeathsCounter(AreaMode.Normal, centeredX: true, SaveData.Instance.TotalDeaths);
				_003Ctime_003E5__4 = new TimeDisplay(text);
				_003CtimeWidth_003E5__5 = SpeedrunTimerDisplay.GetTimeWidth(text);
				cS08_Ending.Add(_003Cstrawbs_003E5__2);
				cS08_Ending.Add(_003Cdeaths_003E5__3);
				cS08_Ending.Add(_003Ctime_003E5__4);
				_003Cfrom_003E5__6 = new Vector2(960f, 1180f);
				_003Cto_003E5__7 = new Vector2(960f, 940f);
				_003Cp_003E5__8 = 0f;
				goto IL_020e;
			}
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__8 += Engine.DeltaTime / 0.5f;
				goto IL_020e;
			case 3:
				_003C_003E1__state = -1;
				goto IL_028b;
			case 4:
				_003C_003E1__state = -1;
				goto IL_028b;
			case 5:
				{
					_003C_003E1__state = -1;
					cS08_Ending.Level.CompleteArea(spotlightWipe: false);
					return false;
				}
				IL_028b:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				cS08_Ending.showVersion = false;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
				return true;
				IL_020e:
				if (_003Cp_003E5__8 < 1f)
				{
					Vector2 vector = Vector2.Lerp(_003Cfrom_003E5__6, _003Cto_003E5__7, Ease.CubeOut(_003Cp_003E5__8));
					_003Cstrawbs_003E5__2.Position = vector + new Vector2(-170f, 0f);
					_003Cdeaths_003E5__3.Position = vector + new Vector2(170f, 0f);
					_003Ctime_003E5__4.Position = vector + new Vector2((0f - _003CtimeWidth_003E5__5) / 2f, 100f);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Cstrawbs_003E5__2 = null;
				_003Cdeaths_003E5__3 = null;
				_003Ctime_003E5__4 = null;
				_003Cfrom_003E5__6 = default(Vector2);
				_003Cto_003E5__7 = default(Vector2);
				cS08_Ending.showVersion = true;
				_003C_003E2__current = 0.25f;
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
	private sealed class _003CMaddyTurns_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS08_Ending _003C_003E4__this;

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
		public _003CMaddyTurns_003Ed__18(int _003C_003E1__state)
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
			CS08_Ending cS08_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS08_Ending.player.Facing = (Facings)(0 - cS08_Ending.player.Facing);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
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
	private sealed class _003CBadelineEmerges_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS08_Ending _003C_003E4__this;

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
		public _003CBadelineEmerges_003Ed__19(int _003C_003E1__state)
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
			CS08_Ending cS08_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS08_Ending.Level.Displacement.AddBurst(cS08_Ending.player.Center, 0.5f, 8f, 32f, 0.5f);
				cS08_Ending.Level.Session.Inventory.Dashes = 1;
				cS08_Ending.player.Dashes = 1;
				cS08_Ending.Level.Add(cS08_Ending.badeline = new BadelineDummy(cS08_Ending.player.Position));
				Audio.Play("event:/char/badeline/maddy_split", cS08_Ending.player.Position);
				cS08_Ending.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = cS08_Ending.badeline.FloatTo(cS08_Ending.player.Position + new Vector2(-12f, -16f), 1, faceDirection: false);
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
	private sealed class _003COshiroEnters_003Ed__20 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS08_Ending _003C_003E4__this;

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
		public _003COshiroEnters_003Ed__20(int _003C_003E1__state)
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
			CS08_Ending cS08_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = new FadeWipe(cS08_Ending.Level, wipeIn: false)
				{
					Duration = 1.5f
				}.Wait();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS08_Ending.fade = 1f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				float x = cS08_Ending.player.X;
				cS08_Ending.player.X = cS08_Ending.granny.X + 8f;
				cS08_Ending.badeline.X = cS08_Ending.player.X + 12f;
				cS08_Ending.player.Facing = Facings.Left;
				cS08_Ending.badeline.Sprite.Scale.X = -1f;
				cS08_Ending.granny.X = x + 8f;
				cS08_Ending.theo.X += 16f;
				cS08_Ending.Level.Add(cS08_Ending.oshiro = new Entity(new Vector2(cS08_Ending.granny.X - 24f, cS08_Ending.granny.Y + 4f)));
				OshiroSprite component = new OshiroSprite(1);
				cS08_Ending.oshiro.Add(component);
				cS08_Ending.fade = 0f;
				FadeWipe fadeWipe = new FadeWipe(cS08_Ending.Level, wipeIn: true)
				{
					Duration = 1f
				};
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
				_003C_003E1__state = -1;
				break;
			case 4:
				_003C_003E1__state = -1;
				break;
			}
			if (cS08_Ending.oshiro.Y > cS08_Ending.granny.Y - 4f)
			{
				cS08_Ending.oshiro.Y -= Engine.DeltaTime * 32f;
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
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
	private sealed class _003COshiroSettles_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS08_Ending _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

		private Vector2 _003Cto_003E5__3;

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
		public _003COshiroSettles_003Ed__21(int _003C_003E1__state)
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
			CS08_Ending cS08_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = cS08_Ending.oshiro.Position;
				_003Cto_003E5__3 = cS08_Ending.oshiro.Position + new Vector2(40f, 8f);
				_003Cp_003E5__4 = 0f;
				goto IL_00b7;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime;
				goto IL_00b7;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00b7:
				if (_003Cp_003E5__4 < 1f)
				{
					cS08_Ending.oshiro.Position = Vector2.Lerp(_003Cfrom_003E5__2, _003Cto_003E5__3, _003Cp_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS08_Ending.granny.Sprite.Scale.X = 1f;
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

	private Player player;

	private NPC08_Granny granny;

	private NPC08_Theo theo;

	private BadelineDummy badeline;

	private Entity oshiro;

	private Image vignette;

	private Image vignettebg;

	private string endingDialog;

	private float fade;

	private bool showVersion;

	private float versionAlpha;

	private Coroutine cutscene;

	private string version = Celeste.Instance.Version.ToString();

	public CS08_Ending()
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		base.Tag = (int)Tags.HUD | (int)Tags.PauseUpdate;
		RemoveOnSkipped = false;
	}

	public override void OnBegin(Level level)
	{
		level.SaveQuitDisabled = true;
		string text = "";
		int totalStrawberries = SaveData.Instance.TotalStrawberries;
		if (totalStrawberries < 20)
		{
			text = "final1";
			endingDialog = "EP_PIE_DISAPPOINTED";
		}
		else if (totalStrawberries < 50)
		{
			text = "final2";
			endingDialog = "EP_PIE_GROSSED_OUT";
		}
		else if (totalStrawberries < 90)
		{
			text = "final3";
			endingDialog = "EP_PIE_OKAY";
		}
		else if (totalStrawberries < 150)
		{
			text = "final4";
			endingDialog = "EP_PIE_REALLY_GOOD";
		}
		else
		{
			text = "final5";
			endingDialog = "EP_PIE_AMAZING";
		}
		Add(vignettebg = new Image(GFX.Portraits["finalbg"]));
		vignettebg.Visible = false;
		Add(vignette = new Image(GFX.Portraits[text]));
		vignette.Visible = false;
		vignette.CenterOrigin();
		vignette.Position = Celeste.TargetCenter;
		Add(cutscene = new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__15))]
	private IEnumerator Cutscene(Level level)
	{
		level.ZoomSnap(new Vector2(164f, 120f), 2f);
		level.Wipe.Cancel();
		new FadeWipe(level, wipeIn: true);
		while (player == null)
		{
			granny = level.Entities.FindFirst<NPC08_Granny>();
			theo = level.Entities.FindFirst<NPC08_Theo>();
			player = level.Tracker.GetEntity<Player>();
			yield return null;
		}
		player.StateMachine.State = 11;
		yield return 1f;
		yield return player.DummyWalkToExact((int)player.X + 16);
		yield return 0.25f;
		yield return Textbox.Say("EP_CABIN", BadelineEmerges, OshiroEnters, OshiroSettles, MaddyTurns);
		FadeWipe fadeWipe = new FadeWipe(Level, wipeIn: false);
		fadeWipe.Duration = 1.5f;
		yield return fadeWipe.Wait();
		fade = 1f;
		yield return Textbox.Say("EP_PIE_START");
		yield return 0.5f;
		vignettebg.Visible = true;
		vignette.Visible = true;
		vignettebg.Color = Color.Black;
		vignette.Color = Color.White * 0f;
		Add(vignette);
		float p;
		for (p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			vignette.Color = Color.White * Ease.CubeIn(p);
			vignette.Scale = Vector2.One * (1f + 0.25f * (1f - p));
			vignette.Rotation = 0.05f * (1f - p);
			yield return null;
		}
		vignette.Color = Color.White;
		vignettebg.Color = Color.White;
		yield return 2f;
		p = 1f;
		float p2;
		for (p2 = 0f; p2 < 1f; p2 += Engine.DeltaTime / p)
		{
			float num = Ease.CubeOut(p2);
			vignette.Position = Vector2.Lerp(Celeste.TargetCenter, Celeste.TargetCenter + new Vector2(0f, 140f), num);
			vignette.Scale = Vector2.One * (0.65f + 0.35f * (1f - num));
			vignette.Rotation = -0.025f * num;
			yield return null;
		}
		yield return Textbox.Say(endingDialog);
		yield return 0.25f;
		p = 2f;
		Vector2 posFrom = vignette.Position;
		p2 = vignette.Rotation;
		float scaleFrom = vignette.Scale.X;
		for (float p3 = 0f; p3 < 1f; p3 += Engine.DeltaTime / p)
		{
			float amount = Ease.CubeOut(p3);
			vignette.Position = Vector2.Lerp(posFrom, Celeste.TargetCenter, amount);
			vignette.Scale = Vector2.One * MathHelper.Lerp(scaleFrom, 1f, amount);
			vignette.Rotation = MathHelper.Lerp(p2, 0f, amount);
			yield return null;
		}
		EndCutscene(level, removeSelf: false);
	}

	public override void OnEnd(Level level)
	{
		vignette.Visible = true;
		vignette.Color = Color.White;
		vignette.Position = Celeste.TargetCenter;
		vignette.Scale = Vector2.One;
		vignette.Rotation = 0f;
		if (player != null)
		{
			player.Speed = Vector2.Zero;
		}
		base.Scene.Entities.FindFirst<Textbox>()?.RemoveSelf();
		cutscene.RemoveSelf();
		Add(new Coroutine(EndingRoutine()));
	}

	[IteratorStateMachine(typeof(_003CEndingRoutine_003Ed__17))]
	private IEnumerator EndingRoutine()
	{
		Level.InCutscene = true;
		Level.PauseLock = true;
		yield return 0.5f;
		TimeSpan timeSpan = TimeSpan.FromTicks(SaveData.Instance.Time);
		string text = (int)timeSpan.TotalHours + timeSpan.ToString("\\:mm\\:ss\\.fff");
		StrawberriesCounter strawbs = new StrawberriesCounter(centeredX: true, SaveData.Instance.TotalStrawberries, 175, showOutOf: true);
		DeathsCounter deaths = new DeathsCounter(AreaMode.Normal, centeredX: true, SaveData.Instance.TotalDeaths);
		TimeDisplay time = new TimeDisplay(text);
		float timeWidth = SpeedrunTimerDisplay.GetTimeWidth(text);
		Add(strawbs);
		Add(deaths);
		Add(time);
		Vector2 from = new Vector2(960f, 1180f);
		Vector2 to = new Vector2(960f, 940f);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.5f)
		{
			Vector2 vector = Vector2.Lerp(from, to, Ease.CubeOut(p));
			strawbs.Position = vector + new Vector2(-170f, 0f);
			deaths.Position = vector + new Vector2(170f, 0f);
			time.Position = vector + new Vector2((0f - timeWidth) / 2f, 100f);
			yield return null;
		}
		showVersion = true;
		yield return 0.25f;
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		showVersion = false;
		yield return 0.25f;
		Level.CompleteArea(spotlightWipe: false);
	}

	[IteratorStateMachine(typeof(_003CMaddyTurns_003Ed__18))]
	private IEnumerator MaddyTurns()
	{
		yield return 0.1f;
		player.Facing = (Facings)(0 - player.Facing);
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CBadelineEmerges_003Ed__19))]
	private IEnumerator BadelineEmerges()
	{
		Level.Displacement.AddBurst(player.Center, 0.5f, 8f, 32f, 0.5f);
		Level.Session.Inventory.Dashes = 1;
		player.Dashes = 1;
		Level.Add(badeline = new BadelineDummy(player.Position));
		Audio.Play("event:/char/badeline/maddy_split", player.Position);
		badeline.Sprite.Scale.X = 1f;
		yield return badeline.FloatTo(player.Position + new Vector2(-12f, -16f), 1, faceDirection: false);
	}

	[IteratorStateMachine(typeof(_003COshiroEnters_003Ed__20))]
	private IEnumerator OshiroEnters()
	{
		FadeWipe fadeWipe = new FadeWipe(Level, wipeIn: false);
		fadeWipe.Duration = 1.5f;
		yield return fadeWipe.Wait();
		fade = 1f;
		yield return 0.25f;
		float x = player.X;
		player.X = granny.X + 8f;
		badeline.X = player.X + 12f;
		player.Facing = Facings.Left;
		badeline.Sprite.Scale.X = -1f;
		granny.X = x + 8f;
		theo.X += 16f;
		Level.Add(oshiro = new Entity(new Vector2(granny.X - 24f, granny.Y + 4f)));
		OshiroSprite component = new OshiroSprite(1);
		oshiro.Add(component);
		fade = 0f;
		fadeWipe = new FadeWipe(Level, wipeIn: true);
		fadeWipe.Duration = 1f;
		yield return 0.25f;
		while (oshiro.Y > granny.Y - 4f)
		{
			oshiro.Y -= Engine.DeltaTime * 32f;
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003COshiroSettles_003Ed__21))]
	private IEnumerator OshiroSettles()
	{
		Vector2 from = oshiro.Position;
		Vector2 to = oshiro.Position + new Vector2(40f, 8f);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			oshiro.Position = Vector2.Lerp(from, to, p);
			yield return null;
		}
		granny.Sprite.Scale.X = 1f;
		yield return null;
	}

	public override void Update()
	{
		versionAlpha = Calc.Approach(versionAlpha, showVersion ? 1 : 0, Engine.DeltaTime * 5f);
		base.Update();
	}

	public override void Render()
	{
		if (fade > 0f)
		{
			Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * fade);
		}
		base.Render();
		if (Settings.Instance.SpeedrunClock != SpeedrunType.Off && versionAlpha > 0f)
		{
			AreaComplete.VersionNumberAndVariants(version, versionAlpha, 1f);
		}
	}
}
