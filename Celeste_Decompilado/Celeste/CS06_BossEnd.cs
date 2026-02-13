using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class CS06_BossEnd : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

		public Level level;

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
		public _003CCutscene_003Ed__12(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_BossEnd.player.StateMachine.State = 11;
				cS06_BossEnd.player.StateMachine.Locked = true;
				goto IL_00b1;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00b1;
			case 2:
			{
				_003C_003E1__state = -1;
				Level obj = cS06_BossEnd.SceneAs<Level>();
				obj.Session.Audio.Music.Event = "event:/music/lvl6/badeline_acoustic";
				obj.Session.Audio.Apply();
				_003C_003E2__current = Textbox.Say("ch6_boss_ending", cS06_BossEnd.StartMusic, cS06_BossEnd.PlayerHug, cS06_BossEnd.BadelineCalmDown);
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				goto IL_01a7;
			case 5:
				_003C_003E1__state = -1;
				goto IL_01a7;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_BossEnd.WaitForPress();
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				cS06_BossEnd.sfx = Audio.Play("event:/game/06_reflection/hug_image_2");
				_003C_003E2__current = cS06_BossEnd.PictureFade(0f, 0.5f);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS06_BossEnd.picture = GFX.Portraits["hug2"];
				_003C_003E2__current = cS06_BossEnd.PictureFade(1f);
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_BossEnd.WaitForPress();
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				cS06_BossEnd.sfx = Audio.Play("event:/game/06_reflection/hug_image_3");
				goto IL_02e3;
			case 11:
				_003C_003E1__state = -1;
				goto IL_02e3;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_BossEnd.PictureFade(0f, 0.5f);
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				goto IL_0367;
			case 14:
				_003C_003E1__state = -1;
				goto IL_0367;
			case 15:
				_003C_003E1__state = -1;
				cS06_BossEnd.player.Sprite.Play("idle");
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 16;
				return true;
			case 16:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_BossEnd.player.DummyWalkToExact((int)cS06_BossEnd.player.X - 8, walkBackwards: true);
				_003C_003E1__state = 17;
				return true;
			case 17:
				_003C_003E1__state = -1;
				cS06_BossEnd.Add(new Coroutine(cS06_BossEnd.CenterCameraOnPlayer()));
				_003C_003E2__current = cS06_BossEnd.badeline.Disperse();
				_003C_003E1__state = 18;
				return true;
			case 18:
				_003C_003E1__state = -1;
				(cS06_BossEnd.Scene as Level).Session.SetFlag("badeline_connection");
				level.Flash(Color.White);
				level.Session.Inventory.Dashes = 2;
				cS06_BossEnd.badeline.RemoveSelf();
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 19;
				return true;
			case 19:
				_003C_003E1__state = -1;
				level.Add(new LevelUpEffect(cS06_BossEnd.player.Position));
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 20;
				return true;
			case 20:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomBack(0.5f);
				_003C_003E1__state = 21;
				return true;
			case 21:
				{
					_003C_003E1__state = -1;
					cS06_BossEnd.EndCutscene(level);
					return false;
				}
				IL_0367:
				if ((cS06_BossEnd.fade -= Engine.DeltaTime * 12f) > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 14;
					return true;
				}
				level.Session.Audio.Music.Param("levelup", 1f);
				level.Session.Audio.Apply();
				cS06_BossEnd.Add(new Coroutine(cS06_BossEnd.badeline.TurnWhite(1f)));
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 15;
				return true;
				IL_02e3:
				if ((cS06_BossEnd.pictureGlow += Engine.DeltaTime / 2f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 11;
					return true;
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 12;
				return true;
				IL_01a7:
				if ((cS06_BossEnd.fade += Engine.DeltaTime) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				cS06_BossEnd.picture = GFX.Portraits["hug1"];
				cS06_BossEnd.sfx = Audio.Play("event:/game/06_reflection/hug_image_1");
				_003C_003E2__current = cS06_BossEnd.PictureFade(1f);
				_003C_003E1__state = 6;
				return true;
				IL_00b1:
				if (!cS06_BossEnd.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS06_BossEnd.player.Facing = Facings.Right;
				_003C_003E2__current = 1f;
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
	private sealed class _003CStartMusic_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

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
		public _003CStartMusic_003Ed__13(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Level level = cS06_BossEnd.SceneAs<Level>();
				level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_acoustic";
				level.Session.Audio.Apply();
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			}
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
	private sealed class _003CPlayerHug_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

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
		public _003CPlayerHug_003Ed__14(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_BossEnd.Add(new Coroutine(cS06_BossEnd.Level.ZoomTo(cS06_BossEnd.badeline.Center + new Vector2(0f, -24f) - cS06_BossEnd.Level.Camera.Position, 2f, 0.5f)));
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_BossEnd.player.DummyWalkToExact((int)cS06_BossEnd.badeline.X - 10);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS06_BossEnd.player.Facing = Facings.Right;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS06_BossEnd.player.DummyAutoAnimate = false;
				cS06_BossEnd.player.Sprite.Play("hug");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
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
	private sealed class _003CBadelineCalmDown_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

		private FinalBossStarfield _003CbossBg_003E5__2;

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
		public _003CBadelineCalmDown_003Ed__15(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "postboss", 0f);
				cS06_BossEnd.badeline.LoopingSfx.Param("end", 1f);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS06_BossEnd.badeline.Sprite.Play("scaredTransition");
				Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
				_003CbossBg_003E5__2 = cS06_BossEnd.Level.Background.Get<FinalBossStarfield>();
				if (_003CbossBg_003E5__2 != null)
				{
					goto IL_00e8;
				}
				goto IL_00fa;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00e8;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00e8:
				if (_003CbossBg_003E5__2.Alpha > 0f)
				{
					_003CbossBg_003E5__2.Alpha -= Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_00fa;
				IL_00fa:
				_003C_003E2__current = 1.5f;
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
	private sealed class _003CCenterCameraOnPlayer_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

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
		public _003CCenterCameraOnPlayer_003Ed__16(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
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
				_003Cfrom_003E5__2 = cS06_BossEnd.Level.ZoomFocusPoint;
				_003Cto_003E5__3 = new Vector2(cS06_BossEnd.Level.Bounds.Left + 580, cS06_BossEnd.Level.Bounds.Top + 124) - cS06_BossEnd.Level.Camera.Position;
				_003Cp_003E5__4 = 0f;
				break;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
			{
				cS06_BossEnd.Level.ZoomFocusPoint = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.SineInOut(_003Cp_003E5__4);
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

	[CompilerGenerated]
	private sealed class _003CPictureFade_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

		public float to;

		public float duration;

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
		public _003CPictureFade_003Ed__17(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
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
			if ((cS06_BossEnd.pictureFade = Calc.Approach(cS06_BossEnd.pictureFade, to, Engine.DeltaTime / duration)) != to)
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
	private sealed class _003CWaitForPress_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossEnd _003C_003E4__this;

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
		public _003CWaitForPress_003Ed__18(int _003C_003E1__state)
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
			CS06_BossEnd cS06_BossEnd = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_BossEnd.waitForKeyPress = true;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (!Input.MenuConfirm.Pressed)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS06_BossEnd.waitForKeyPress = false;
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

	public const string Flag = "badeline_connection";

	private Player player;

	private NPC06_Badeline_Crying badeline;

	private float fade;

	private float pictureFade;

	private float pictureGlow;

	private MTexture picture;

	private bool waitForKeyPress;

	private float timer;

	private FMOD.Studio.EventInstance sfx;

	public CS06_BossEnd(Player player, NPC06_Badeline_Crying badeline)
	{
		base.Tag = Tags.HUD;
		this.player = player;
		this.badeline = badeline;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__12))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		while (!player.OnGround())
		{
			yield return null;
		}
		player.Facing = Facings.Right;
		yield return 1f;
		Level level2 = SceneAs<Level>();
		level2.Session.Audio.Music.Event = "event:/music/lvl6/badeline_acoustic";
		level2.Session.Audio.Apply();
		yield return Textbox.Say("ch6_boss_ending", StartMusic, PlayerHug, BadelineCalmDown);
		yield return 0.5f;
		while ((fade += Engine.DeltaTime) < 1f)
		{
			yield return null;
		}
		picture = GFX.Portraits["hug1"];
		sfx = Audio.Play("event:/game/06_reflection/hug_image_1");
		yield return PictureFade(1f);
		yield return WaitForPress();
		sfx = Audio.Play("event:/game/06_reflection/hug_image_2");
		yield return PictureFade(0f, 0.5f);
		picture = GFX.Portraits["hug2"];
		yield return PictureFade(1f);
		yield return WaitForPress();
		sfx = Audio.Play("event:/game/06_reflection/hug_image_3");
		while ((pictureGlow += Engine.DeltaTime / 2f) < 1f)
		{
			yield return null;
		}
		yield return 0.2f;
		yield return PictureFade(0f, 0.5f);
		while ((fade -= Engine.DeltaTime * 12f) > 0f)
		{
			yield return null;
		}
		level.Session.Audio.Music.Param("levelup", 1f);
		level.Session.Audio.Apply();
		Add(new Coroutine(badeline.TurnWhite(1f)));
		yield return 0.5f;
		player.Sprite.Play("idle");
		yield return 0.25f;
		yield return player.DummyWalkToExact((int)player.X - 8, walkBackwards: true);
		Add(new Coroutine(CenterCameraOnPlayer()));
		yield return badeline.Disperse();
		(base.Scene as Level).Session.SetFlag("badeline_connection");
		level.Flash(Color.White);
		level.Session.Inventory.Dashes = 2;
		badeline.RemoveSelf();
		yield return 0.1f;
		level.Add(new LevelUpEffect(player.Position));
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		yield return 2f;
		yield return level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CStartMusic_003Ed__13))]
	private IEnumerator StartMusic()
	{
		Level level = SceneAs<Level>();
		level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_acoustic";
		level.Session.Audio.Apply();
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CPlayerHug_003Ed__14))]
	private IEnumerator PlayerHug()
	{
		Add(new Coroutine(Level.ZoomTo(badeline.Center + new Vector2(0f, -24f) - Level.Camera.Position, 2f, 0.5f)));
		yield return 0.6f;
		yield return player.DummyWalkToExact((int)badeline.X - 10);
		player.Facing = Facings.Right;
		yield return 0.25f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("hug");
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CBadelineCalmDown_003Ed__15))]
	private IEnumerator BadelineCalmDown()
	{
		Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "postboss", 0f);
		badeline.LoopingSfx.Param("end", 1f);
		yield return 0.5f;
		badeline.Sprite.Play("scaredTransition");
		Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
		FinalBossStarfield bossBg = Level.Background.Get<FinalBossStarfield>();
		if (bossBg != null)
		{
			while (bossBg.Alpha > 0f)
			{
				bossBg.Alpha -= Engine.DeltaTime;
				yield return null;
			}
		}
		yield return 1.5f;
	}

	[IteratorStateMachine(typeof(_003CCenterCameraOnPlayer_003Ed__16))]
	private IEnumerator CenterCameraOnPlayer()
	{
		yield return 0.5f;
		Vector2 from = Level.ZoomFocusPoint;
		Vector2 to = new Vector2(Level.Bounds.Left + 580, Level.Bounds.Top + 124) - Level.Camera.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			Level.ZoomFocusPoint = from + (to - from) * Ease.SineInOut(p);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CPictureFade_003Ed__17))]
	private IEnumerator PictureFade(float to, float duration = 1f)
	{
		while ((pictureFade = Calc.Approach(pictureFade, to, Engine.DeltaTime / duration)) != to)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CWaitForPress_003Ed__18))]
	private IEnumerator WaitForPress()
	{
		waitForKeyPress = true;
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		waitForKeyPress = false;
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped && sfx != null)
		{
			Audio.Stop(sfx);
		}
		Audio.SetParameter(Audio.CurrentAmbienceEventInstance, "postboss", 0f);
		level.ResetZoom();
		level.Session.Inventory.Dashes = 2;
		level.Session.Audio.Music.Event = "event:/music/lvl6/badeline_acoustic";
		if (WasSkipped)
		{
			level.Session.Audio.Music.Param("levelup", 2f);
		}
		level.Session.Audio.Apply();
		if (WasSkipped)
		{
			level.Add(new LevelUpEffect(player.Position));
		}
		player.DummyAutoAnimate = true;
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		FinalBossStarfield finalBossStarfield = Level.Background.Get<FinalBossStarfield>();
		if (finalBossStarfield != null)
		{
			finalBossStarfield.Alpha = 0f;
		}
		badeline.RemoveSelf();
		level.Session.SetFlag("badeline_connection");
	}

	public override void Update()
	{
		timer += Engine.DeltaTime;
		base.Update();
	}

	public override void Render()
	{
		if (!(fade > 0f))
		{
			return;
		}
		Draw.Rect(-10f, -10f, 1940f, 1100f, Color.Black * Ease.CubeOut(fade) * 0.8f);
		if (picture != null && pictureFade > 0f)
		{
			float num = Ease.CubeOut(pictureFade);
			Vector2 position = new Vector2(960f, 540f);
			float scale = 1f + (1f - num) * 0.025f;
			picture.DrawCentered(position, Color.White * Ease.CubeOut(pictureFade), scale, 0f);
			if (pictureGlow > 0f)
			{
				GFX.Portraits["hug-light2a"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
				GFX.Portraits["hug-light2b"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
				GFX.Portraits["hug-light2c"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
				HiresRenderer.EndRender();
				HiresRenderer.BeginRender(BlendState.Additive);
				GFX.Portraits["hug-light2a"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
				GFX.Portraits["hug-light2b"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
				GFX.Portraits["hug-light2c"].DrawCentered(position, Color.White * Ease.CubeOut(pictureFade * pictureGlow), scale);
				HiresRenderer.EndRender();
				HiresRenderer.BeginRender();
			}
			if (waitForKeyPress)
			{
				GFX.Gui["textboxbutton"].DrawCentered(new Vector2(1520f, 880 + ((timer % 1f < 0.25f) ? 6 : 0)));
			}
		}
	}
}
