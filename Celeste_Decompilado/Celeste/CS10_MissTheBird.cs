using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_MissTheBird : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MissTheBird _003C_003E4__this;

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
		public _003CCutscene_003Ed__8(int _003C_003E1__state)
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
			CS10_MissTheBird CS_0024_003C_003E8__locals44 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusicParam("bird_grab", 1f);
				CS_0024_003C_003E8__locals44.crashMusicSfx = Audio.Play("event:/new_content/music/lvl10/cinematic/bird_crash_first");
				_003C_003E2__current = CS_0024_003C_003E8__locals44.flingBird.DoGrabbingRoutine(CS_0024_003C_003E8__locals44.player);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals44.bird = new BirdNPC(CS_0024_003C_003E8__locals44.flingBird.Position, BirdNPC.Modes.None);
				level.Add(CS_0024_003C_003E8__locals44.bird);
				CS_0024_003C_003E8__locals44.flingBird.RemoveSelf();
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				level.ResetZoom();
				level.Shake(0.5f);
				CS_0024_003C_003E8__locals44.player.Position = CS_0024_003C_003E8__locals44.player.Position.Floor();
				CS_0024_003C_003E8__locals44.player.DummyGravity = true;
				CS_0024_003C_003E8__locals44.player.DummyAutoAnimate = false;
				CS_0024_003C_003E8__locals44.player.DummyFriction = false;
				CS_0024_003C_003E8__locals44.player.ForceCameraUpdate = true;
				CS_0024_003C_003E8__locals44.player.Speed = new Vector2(200f, 200f);
				CS_0024_003C_003E8__locals44.bird.Position += Vector2.UnitX * 16f;
				CS_0024_003C_003E8__locals44.bird.Add(new Coroutine(CS_0024_003C_003E8__locals44.bird.Startle(null, 0.5f, new Vector2(3f, 0.25f))));
				CS_0024_003C_003E8__locals44.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
				{
					CS_0024_003C_003E8__locals44.bird.Sprite.Play("hoverStressed");
					CS_0024_003C_003E8__locals44.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
					{
						CS_0024_003C_003E8__locals44.Add(new Coroutine(CS_0024_003C_003E8__locals44.bird.FlyAway(0.2f)));
						CS_0024_003C_003E8__locals44.bird.Position += new Vector2(0f, -4f);
					}, 0.8f, start: true));
				}, 0.1f, start: true));
				while (!CS_0024_003C_003E8__locals44.player.OnGround())
				{
					CS_0024_003C_003E8__locals44.player.MoveVExact(1);
				}
				Engine.TimeRate = 0.5f;
				CS_0024_003C_003E8__locals44.player.Sprite.Play("roll");
				goto IL_0286;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0286;
			case 4:
				_003C_003E1__state = -1;
				goto IL_02d8;
			case 5:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals44.Add(CS_0024_003C_003E8__locals44.zoomRoutine = new Coroutine(level.ZoomTo(new Vector2(160f, 110f), 1.5f, 6f)));
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals44.player.Sprite.Play("rollGetUp");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals44.player.ForceCameraUpdate = false;
				_003C_003E2__current = Textbox.Say("CH9_MISS_THE_BIRD", CS_0024_003C_003E8__locals44.StandUpFaceLeft, CS_0024_003C_003E8__locals44.TakeStepLeft, CS_0024_003C_003E8__locals44.TakeStepRight, CS_0024_003C_003E8__locals44.FlickerBlackhole, CS_0024_003C_003E8__locals44.OpenBlackhole);
				_003C_003E1__state = 8;
				return true;
			case 8:
				{
					_003C_003E1__state = -1;
					CS_0024_003C_003E8__locals44.StartMusic();
					CS_0024_003C_003E8__locals44.EndCutscene(level);
					return false;
				}
				IL_0286:
				if (CS_0024_003C_003E8__locals44.player.Speed.X != 0f)
				{
					CS_0024_003C_003E8__locals44.player.Speed.X = Calc.Approach(CS_0024_003C_003E8__locals44.player.Speed.X, 0f, 120f * Engine.DeltaTime);
					if (CS_0024_003C_003E8__locals44.Scene.OnInterval(0.1f))
					{
						Dust.BurstFG(CS_0024_003C_003E8__locals44.player.Position, -(float)Math.PI / 2f, 2);
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_02d8;
				IL_02d8:
				if (Engine.TimeRate < 1f)
				{
					Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 4f * Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				CS_0024_003C_003E8__locals44.player.Speed.X = 0f;
				CS_0024_003C_003E8__locals44.player.DummyFriction = true;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
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
	private sealed class _003CStandUpFaceLeft_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MissTheBird _003C_003E4__this;

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
		public _003CStandUpFaceLeft_003Ed__9(int _003C_003E1__state)
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
			CS10_MissTheBird cS10_MissTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_004a;
			case 1:
				_003C_003E1__state = -1;
				goto IL_004a;
			case 2:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/madeline/stand", cS10_MissTheBird.player.Position);
				cS10_MissTheBird.player.DummyAutoAnimate = true;
				cS10_MissTheBird.player.Sprite.Play("idle");
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS10_MissTheBird.player.Facing = Facings.Left;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_004a:
				if (!cS10_MissTheBird.zoomRoutine.Finished)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = 0.2f;
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
	private sealed class _003CTakeStepLeft_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MissTheBird _003C_003E4__this;

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
		public _003CTakeStepLeft_003Ed__10(int _003C_003E1__state)
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
			CS10_MissTheBird cS10_MissTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_MissTheBird.player.DummyWalkTo(cS10_MissTheBird.player.X - 16f);
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
	private sealed class _003CTakeStepRight_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MissTheBird _003C_003E4__this;

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
		public _003CTakeStepRight_003Ed__11(int _003C_003E1__state)
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
			CS10_MissTheBird cS10_MissTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_MissTheBird.player.DummyWalkTo(cS10_MissTheBird.player.X + 32f);
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
	private sealed class _003CFlickerBlackhole_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MissTheBird _003C_003E4__this;

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
		public _003CFlickerBlackhole_003Ed__12(int _003C_003E1__state)
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
			CS10_MissTheBird cS10_MissTheBird = _003C_003E4__this;
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
				Audio.Play("event:/new_content/game/10_farewell/glitch_medium");
				_003C_003E2__current = MoonGlitchBackgroundTrigger.GlitchRoutine(0.5f, stayOn: false);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_MissTheBird.player.DummyWalkTo(cS10_MissTheBird.player.X - 8f, walkBackwards: true);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
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
	private sealed class _003COpenBlackhole_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MissTheBird _003C_003E4__this;

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
		public _003COpenBlackhole_003Ed__13(int _003C_003E1__state)
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
			CS10_MissTheBird cS10_MissTheBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_MissTheBird.Level.ResetZoom();
				cS10_MissTheBird.Level.Flash(Color.White);
				cS10_MissTheBird.Level.Shake(0.4f);
				cS10_MissTheBird.Level.Add(new LightningStrike(new Vector2(cS10_MissTheBird.player.X, cS10_MissTheBird.Level.Bounds.Top), 80, 240f));
				cS10_MissTheBird.Level.Add(new LightningStrike(new Vector2(cS10_MissTheBird.player.X - 100f, cS10_MissTheBird.Level.Bounds.Top), 90, 240f, 0.5f));
				Audio.Play("event:/new_content/game/10_farewell/lightning_strike");
				cS10_MissTheBird.TriggerEnvironmentalEvents();
				cS10_MissTheBird.StartMusic();
				_003C_003E2__current = 1.2f;
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

	public const string Flag = "MissTheBird";

	private Player player;

	private FlingBirdIntro flingBird;

	private BirdNPC bird;

	private Coroutine zoomRoutine;

	private FMOD.Studio.EventInstance crashMusicSfx;

	public CS10_MissTheBird(Player player, FlingBirdIntro flingBird)
	{
		this.player = player;
		this.flingBird = flingBird;
		Add(new LevelEndingHook(delegate
		{
			Audio.Stop(crashMusicSfx);
		}));
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene(Level level)
	{
		Audio.SetMusicParam("bird_grab", 1f);
		crashMusicSfx = Audio.Play("event:/new_content/music/lvl10/cinematic/bird_crash_first");
		yield return flingBird.DoGrabbingRoutine(player);
		bird = new BirdNPC(flingBird.Position, BirdNPC.Modes.None);
		level.Add(bird);
		flingBird.RemoveSelf();
		yield return null;
		level.ResetZoom();
		level.Shake(0.5f);
		player.Position = player.Position.Floor();
		player.DummyGravity = true;
		player.DummyAutoAnimate = false;
		player.DummyFriction = false;
		player.ForceCameraUpdate = true;
		player.Speed = new Vector2(200f, 200f);
		bird.Position += Vector2.UnitX * 16f;
		bird.Add(new Coroutine(bird.Startle(null, 0.5f, new Vector2(3f, 0.25f))));
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			bird.Sprite.Play("hoverStressed");
			Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
			{
				Add(new Coroutine(bird.FlyAway(0.2f)));
				bird.Position += new Vector2(0f, -4f);
			}, 0.8f, start: true));
		}, 0.1f, start: true));
		while (!player.OnGround())
		{
			player.MoveVExact(1);
		}
		Engine.TimeRate = 0.5f;
		player.Sprite.Play("roll");
		while (player.Speed.X != 0f)
		{
			player.Speed.X = Calc.Approach(player.Speed.X, 0f, 120f * Engine.DeltaTime);
			if (base.Scene.OnInterval(0.1f))
			{
				Dust.BurstFG(player.Position, -(float)Math.PI / 2f, 2);
			}
			yield return null;
		}
		while (Engine.TimeRate < 1f)
		{
			Engine.TimeRate = Calc.Approach(Engine.TimeRate, 1f, 4f * Engine.DeltaTime);
			yield return null;
		}
		player.Speed.X = 0f;
		player.DummyFriction = true;
		yield return 0.25f;
		Add(zoomRoutine = new Coroutine(level.ZoomTo(new Vector2(160f, 110f), 1.5f, 6f)));
		yield return 1.5f;
		player.Sprite.Play("rollGetUp");
		yield return 0.5f;
		player.ForceCameraUpdate = false;
		yield return Textbox.Say("CH9_MISS_THE_BIRD", StandUpFaceLeft, TakeStepLeft, TakeStepRight, FlickerBlackhole, OpenBlackhole);
		StartMusic();
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CStandUpFaceLeft_003Ed__9))]
	private IEnumerator StandUpFaceLeft()
	{
		while (!zoomRoutine.Finished)
		{
			yield return null;
		}
		yield return 0.2f;
		Audio.Play("event:/char/madeline/stand", player.Position);
		player.DummyAutoAnimate = true;
		player.Sprite.Play("idle");
		yield return 0.2f;
		player.Facing = Facings.Left;
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CTakeStepLeft_003Ed__10))]
	private IEnumerator TakeStepLeft()
	{
		yield return player.DummyWalkTo(player.X - 16f);
	}

	[IteratorStateMachine(typeof(_003CTakeStepRight_003Ed__11))]
	private IEnumerator TakeStepRight()
	{
		yield return player.DummyWalkTo(player.X + 32f);
	}

	[IteratorStateMachine(typeof(_003CFlickerBlackhole_003Ed__12))]
	private IEnumerator FlickerBlackhole()
	{
		yield return 0.5f;
		Audio.Play("event:/new_content/game/10_farewell/glitch_medium");
		yield return MoonGlitchBackgroundTrigger.GlitchRoutine(0.5f, stayOn: false);
		yield return player.DummyWalkTo(player.X - 8f, walkBackwards: true);
		yield return 0.4f;
	}

	[IteratorStateMachine(typeof(_003COpenBlackhole_003Ed__13))]
	private IEnumerator OpenBlackhole()
	{
		yield return 0.2f;
		Level.ResetZoom();
		Level.Flash(Color.White);
		Level.Shake(0.4f);
		Level.Add(new LightningStrike(new Vector2(player.X, Level.Bounds.Top), 80, 240f));
		Level.Add(new LightningStrike(new Vector2(player.X - 100f, Level.Bounds.Top), 90, 240f, 0.5f));
		Audio.Play("event:/new_content/game/10_farewell/lightning_strike");
		TriggerEnvironmentalEvents();
		StartMusic();
		yield return 1.2f;
	}

	private void StartMusic()
	{
		Level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/part03";
		Level.Session.Audio.Ambience.Event = "event:/new_content/env/10_voidspiral";
		Level.Session.Audio.Apply();
	}

	private void TriggerEnvironmentalEvents()
	{
		CutsceneNode cutsceneNode = CutsceneNode.Find("player_skip");
		if (cutsceneNode != null)
		{
			RumbleTrigger.ManuallyTrigger(cutsceneNode.X, 0f);
		}
		base.Scene.Entities.FindFirst<MoonGlitchBackgroundTrigger>()?.Invoke();
	}

	public override void OnEnd(Level level)
	{
		Audio.Stop(crashMusicSfx);
		Engine.TimeRate = 1f;
		level.Session.SetFlag("MissTheBird");
		if (WasSkipped)
		{
			player.Sprite.Play("idle");
			CutsceneNode cutsceneNode = CutsceneNode.Find("player_skip");
			if (cutsceneNode != null)
			{
				player.Position = cutsceneNode.Position.Floor();
				level.Camera.Position = player.CameraTarget;
			}
			if (flingBird != null)
			{
				if (flingBird.CrashSfxEmitter != null)
				{
					flingBird.CrashSfxEmitter.RemoveSelf();
				}
				flingBird.RemoveSelf();
			}
			if (bird != null)
			{
				bird.RemoveSelf();
			}
			TriggerEnvironmentalEvents();
			StartMusic();
		}
		player.Speed = Vector2.Zero;
		player.DummyAutoAnimate = true;
		player.DummyFriction = true;
		player.DummyGravity = true;
		player.ForceCameraUpdate = false;
		player.StateMachine.State = 0;
	}
}
