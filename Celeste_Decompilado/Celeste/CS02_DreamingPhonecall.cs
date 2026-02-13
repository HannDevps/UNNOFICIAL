using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS02_DreamingPhonecall : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public CS02_DreamingPhonecall _003C_003E4__this;

		public Level level;

		internal void _003CCutscene_003Eb__0()
		{
			_003C_003E4__this.ringtone.Param("end", 1f);
		}

		internal void _003CCutscene_003Eb__1()
		{
			_003C_003E4__this.EndCutscene(level);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass6_1
	{
		public VertexLight light;

		internal void _003CCutscene_003Eb__2(Tween t)
		{
			light.Alpha = t.Eased;
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_DreamingPhonecall _003C_003E4__this;

		public Level level;

		private _003C_003Ec__DisplayClass6_0 _003C_003E8__1;

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
		public _003CCutscene_003Ed__6(int _003C_003E1__state)
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
			CS02_DreamingPhonecall cS02_DreamingPhonecall = _003C_003E4__this;
			_003C_003Ec__DisplayClass6_1 CS_0024_003C_003E8__locals2;
			Tween tween;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass6_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.level = level;
				cS02_DreamingPhonecall.player.StateMachine.State = 11;
				cS02_DreamingPhonecall.player.Dashes = 1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS02_DreamingPhonecall.ringtone.Play("event:/game/02_old_site/sequence_phone_ring_loop");
				goto IL_0122;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0122;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS02_DreamingPhonecall.player.DummyWalkTo(cS02_DreamingPhonecall.payphone.X - 24f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS02_DreamingPhonecall.player.Facing = Facings.Left;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS02_DreamingPhonecall.player.Facing = Facings.Right;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS02_DreamingPhonecall.player.DummyWalkTo(cS02_DreamingPhonecall.payphone.X - 4f);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				cS02_DreamingPhonecall.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
				{
					_003C_003E8__1._003C_003E4__this.ringtone.Param("end", 1f);
				}, 0.43f, start: true));
				cS02_DreamingPhonecall.player.Visible = false;
				Audio.Play("event:/game/02_old_site/sequence_phone_pickup", cS02_DreamingPhonecall.player.Position);
				_003C_003E2__current = cS02_DreamingPhonecall.payphone.Sprite.PlayRoutine("pickUp");
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				if (_003C_003E8__1.level.Session.Area.Mode == AreaMode.Normal)
				{
					Audio.SetMusic("event:/music/lvl2/phone_loop");
				}
				cS02_DreamingPhonecall.payphone.Sprite.Play("talkPhone");
				_003C_003E2__current = Textbox.Say("CH2_DREAM_PHONECALL", cS02_DreamingPhonecall.ShowShadowMadeline);
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				if (cS02_DreamingPhonecall.evil != null)
				{
					if (_003C_003E8__1.level.Session.Area.Mode == AreaMode.Normal)
					{
						Audio.SetMusic("event:/music/lvl2/phone_end");
					}
					cS02_DreamingPhonecall.evil.Vanish();
					cS02_DreamingPhonecall.evil = null;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 13;
					return true;
				}
				goto IL_03d1;
			case 13:
				_003C_003E1__state = -1;
				goto IL_03d1;
			case 14:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 15;
				return true;
			case 15:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS02_DreamingPhonecall.payphone.Sprite.PlayRoutine("eat");
				_003C_003E1__state = 16;
				return true;
			case 16:
				_003C_003E1__state = -1;
				cS02_DreamingPhonecall.payphone.Sprite.Play("monsterIdle");
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 17;
				return true;
			case 17:
				{
					_003C_003E1__state = -1;
					_003C_003E8__1.level.EndCutscene();
					new FadeWipe(_003C_003E8__1.level, wipeIn: false, delegate
					{
						_003C_003E8__1._003C_003E4__this.EndCutscene(_003C_003E8__1.level);
					});
					return false;
				}
				IL_0122:
				if (cS02_DreamingPhonecall.player.Light.Alpha > 0f)
				{
					cS02_DreamingPhonecall.player.Light.Alpha -= Engine.DeltaTime * 2f;
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 3.2f;
				_003C_003E1__state = 3;
				return true;
				IL_03d1:
				cS02_DreamingPhonecall.Add(new Coroutine(cS02_DreamingPhonecall.WireFalls()));
				cS02_DreamingPhonecall.payphone.Broken = true;
				_003C_003E8__1.level.Shake(0.2f);
				CS_0024_003C_003E8__locals2 = new _003C_003Ec__DisplayClass6_1
				{
					light = new VertexLight(new Vector2(16f, -28f), Color.White, 0f, 32, 48)
				};
				cS02_DreamingPhonecall.payphone.Add(CS_0024_003C_003E8__locals2.light);
				tween = Tween.Create(Tween.TweenMode.Oneshot, null, 2f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals2.light.Alpha = t.Eased;
				};
				cS02_DreamingPhonecall.Add(tween);
				Audio.Play("event:/game/02_old_site/sequence_phone_transform", cS02_DreamingPhonecall.payphone.Position);
				_003C_003E2__current = cS02_DreamingPhonecall.payphone.Sprite.PlayRoutine("transform");
				_003C_003E1__state = 14;
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
	private sealed class _003CShowShadowMadeline_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_DreamingPhonecall _003C_003E4__this;

		private Payphone _003Cpayphone_003E5__2;

		private Level _003Clevel_003E5__3;

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
		public _003CShowShadowMadeline_003Ed__7(int _003C_003E1__state)
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
			CS02_DreamingPhonecall cS02_DreamingPhonecall = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cpayphone_003E5__2 = cS02_DreamingPhonecall.Scene.Tracker.GetEntity<Payphone>();
				_003Clevel_003E5__3 = cS02_DreamingPhonecall.Scene as Level;
				_003C_003E2__current = _003Clevel_003E5__3.ZoomTo(new Vector2(240f, 116f), 2f, 0.5f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS02_DreamingPhonecall.evil = new BadelineDummy(_003Cpayphone_003E5__2.Position + new Vector2(32f, -24f));
				cS02_DreamingPhonecall.evil.Appear(_003Clevel_003E5__3);
				cS02_DreamingPhonecall.Scene.Add(cS02_DreamingPhonecall.evil);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003Cpayphone_003E5__2.Blink.X += 1f;
				_003C_003E2__current = _003Cpayphone_003E5__2.Sprite.PlayRoutine("jumpBack");
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003Cpayphone_003E5__2.Sprite.PlayRoutine("scare");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
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
	private sealed class _003CWireFalls_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_DreamingPhonecall _003C_003E4__this;

		private Wire _003Cwire_003E5__2;

		private Vector2 _003Cspeed_003E5__3;

		private Level _003Clevel_003E5__4;

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
		public _003CWireFalls_003Ed__8(int _003C_003E1__state)
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
			CS02_DreamingPhonecall cS02_DreamingPhonecall = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cwire_003E5__2 = cS02_DreamingPhonecall.Scene.Entities.FindFirst<Wire>();
				_003Cspeed_003E5__3 = Vector2.Zero;
				_003Clevel_003E5__4 = cS02_DreamingPhonecall.SceneAs<Level>();
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Cwire_003E5__2 != null && _003Cwire_003E5__2.Curve.Begin.X < (float)_003Clevel_003E5__4.Bounds.Right)
			{
				_003Cspeed_003E5__3 += new Vector2(0.7f, 1f) * 200f * Engine.DeltaTime;
				_003Cwire_003E5__2.Curve.Begin += _003Cspeed_003E5__3 * Engine.DeltaTime;
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
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

	private BadelineDummy evil;

	private Player player;

	private Payphone payphone;

	private SoundSource ringtone;

	public CS02_DreamingPhonecall(Player player)
		: base(fadeInOnSkip: false)
	{
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		payphone = base.Scene.Tracker.GetEntity<Payphone>();
		Add(new Coroutine(Cutscene(level)));
		Add(ringtone = new SoundSource());
		ringtone.Position = payphone.Position;
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.Dashes = 1;
		yield return 0.3f;
		ringtone.Play("event:/game/02_old_site/sequence_phone_ring_loop");
		while (player.Light.Alpha > 0f)
		{
			player.Light.Alpha -= Engine.DeltaTime * 2f;
			yield return null;
		}
		yield return 3.2f;
		yield return player.DummyWalkTo(payphone.X - 24f);
		yield return 1.5f;
		player.Facing = Facings.Left;
		yield return 1.5f;
		player.Facing = Facings.Right;
		yield return 0.25f;
		yield return player.DummyWalkTo(payphone.X - 4f);
		yield return 1.5f;
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			ringtone.Param("end", 1f);
		}, 0.43f, start: true));
		player.Visible = false;
		Audio.Play("event:/game/02_old_site/sequence_phone_pickup", player.Position);
		yield return payphone.Sprite.PlayRoutine("pickUp");
		yield return 1f;
		if (level.Session.Area.Mode == AreaMode.Normal)
		{
			Audio.SetMusic("event:/music/lvl2/phone_loop");
		}
		payphone.Sprite.Play("talkPhone");
		yield return Textbox.Say("CH2_DREAM_PHONECALL", ShowShadowMadeline);
		if (evil != null)
		{
			if (level.Session.Area.Mode == AreaMode.Normal)
			{
				Audio.SetMusic("event:/music/lvl2/phone_end");
			}
			evil.Vanish();
			evil = null;
			yield return 1f;
		}
		Add(new Coroutine(WireFalls()));
		payphone.Broken = true;
		level.Shake(0.2f);
		VertexLight light = new VertexLight(new Vector2(16f, -28f), Color.White, 0f, 32, 48);
		payphone.Add(light);
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 2f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			light.Alpha = t.Eased;
		};
		Add(tween);
		Audio.Play("event:/game/02_old_site/sequence_phone_transform", payphone.Position);
		yield return payphone.Sprite.PlayRoutine("transform");
		yield return 0.4f;
		yield return payphone.Sprite.PlayRoutine("eat");
		payphone.Sprite.Play("monsterIdle");
		yield return 1.2f;
		level.EndCutscene();
		new FadeWipe(level, wipeIn: false, delegate
		{
			EndCutscene(level);
		});
	}

	[IteratorStateMachine(typeof(_003CShowShadowMadeline_003Ed__7))]
	private IEnumerator ShowShadowMadeline()
	{
		Payphone payphone = base.Scene.Tracker.GetEntity<Payphone>();
		Level level = base.Scene as Level;
		yield return level.ZoomTo(new Vector2(240f, 116f), 2f, 0.5f);
		evil = new BadelineDummy(payphone.Position + new Vector2(32f, -24f));
		evil.Appear(level);
		base.Scene.Add(evil);
		yield return 0.2f;
		payphone.Blink.X += 1f;
		yield return payphone.Sprite.PlayRoutine("jumpBack");
		yield return payphone.Sprite.PlayRoutine("scare");
		yield return 1.2f;
	}

	[IteratorStateMachine(typeof(_003CWireFalls_003Ed__8))]
	private IEnumerator WireFalls()
	{
		yield return 0.5f;
		Wire wire = base.Scene.Entities.FindFirst<Wire>();
		Vector2 speed = Vector2.Zero;
		Level level = SceneAs<Level>();
		while (wire != null && wire.Curve.Begin.X < (float)level.Bounds.Right)
		{
			speed += new Vector2(0.7f, 1f) * 200f * Engine.DeltaTime;
			wire.Curve.Begin += speed * Engine.DeltaTime;
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		Leader.StoreStrawberries(player.Leader);
		level.ResetZoom();
		level.Bloom.Base = 0f;
		level.Remove(player);
		level.UnloadLevel();
		level.Session.Dreaming = false;
		level.Session.Level = "end_0";
		level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Bottom));
		level.Session.Audio.Music.Event = "event:/music/lvl2/awake";
		level.Session.Audio.Ambience.Event = "event:/env/amb/02_awake";
		level.LoadLevel(Player.IntroTypes.WakeUp);
		level.EndCutscene();
		Leader.RestoreStrawberries(level.Tracker.GetEntity<Player>().Leader);
	}
}
