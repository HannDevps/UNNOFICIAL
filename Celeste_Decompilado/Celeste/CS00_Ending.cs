using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS00_Ending : CutsceneEntity
{
	private class EndingCutsceneDelay : Entity
	{
		[CompilerGenerated]
		private sealed class _003CRoutine_003Ed__1 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public EndingCutsceneDelay _003C_003E4__this;

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
			public _003CRoutine_003Ed__1(int _003C_003E1__state)
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
				EndingCutsceneDelay endingCutsceneDelay = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E2__current = 3f;
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					(endingCutsceneDelay.Scene as Level).CompleteArea(spotlightWipe: false);
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

		public EndingCutsceneDelay()
		{
			Add(new Coroutine(Routine()));
		}

		[IteratorStateMachine(typeof(_003CRoutine_003Ed__1))]
		private IEnumerator Routine()
		{
			yield return 3f;
			(base.Scene as Level).CompleteArea(spotlightWipe: false);
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS00_Ending _003C_003E4__this;

		public Level level;

		private Snow _003CbgSnow_003E5__2;

		private Snow _003CfgSnow_003E5__3;

		private float _003Cease_003E5__4;

		private FMOD.Studio.EventInstance _003Cinstance_003E5__5;

		private float _003Cpercent_003E5__6;

		private Vector2 _003Cfrom_003E5__7;

		private Vector2 _003Cto_003E5__8;

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
		public _003CCutscene_003Ed__7(int _003C_003E1__state)
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
			CS00_Ending cS00_Ending = _003C_003E4__this;
			Vector2 aimVector;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_00ba;
			case 1:
				_003C_003E1__state = -1;
				if (Engine.TimeRate < 0.5f && cS00_Ending.bridge != null)
				{
					cS00_Ending.bridge.StopCollapseLoop();
				}
				level.StopShake();
				MInput.GamePads[Input.Gamepad].StopRumble();
				Engine.TimeRate -= Engine.RawDeltaTime * 2f;
				goto IL_00ba;
			case 2:
				_003C_003E1__state = -1;
				_003Cinstance_003E5__5 = Audio.Play("event:/game/general/bird_in", cS00_Ending.bird.Position);
				cS00_Ending.bird.Facing = Facings.Left;
				cS00_Ending.bird.Sprite.Play("fall");
				_003Cpercent_003E5__6 = 0f;
				_003Cfrom_003E5__7 = cS00_Ending.bird.Position;
				_003Cto_003E5__8 = cS00_Ending.bird.StartPosition;
				goto IL_0224;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0224;
			case 4:
				_003C_003E1__state = -1;
				cS00_Ending.bird.Sprite.Play("peck");
				_003C_003E2__current = cS00_Ending.WaitFor(1.1f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS00_Ending.bird.ShowTutorial(new BirdTutorialGui(cS00_Ending.bird, new Vector2(0f, -16f), Dialog.Clean("tutorial_dash"), new Vector2(1f, -1f), "+", BirdTutorialGui.ButtonPrompt.Dash), caw: true);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				goto IL_0377;
			case 7:
				_003C_003E1__state = -1;
				goto IL_0377;
			case 8:
				_003C_003E1__state = -1;
				cS00_Ending.bird.Add(new Coroutine(cS00_Ending.bird.StartleAndFlyAway()));
				goto IL_0484;
			case 9:
				_003C_003E1__state = -1;
				goto IL_0484;
			case 10:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/music/lvl0/title_ping");
				_003C_003E2__current = 2f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				cS00_Ending.endingText = new PrologueEndingText(instant: false);
				cS00_Ending.Scene.Add(cS00_Ending.endingText);
				_003CbgSnow_003E5__2 = level.Background.Get<Snow>();
				_003CfgSnow_003E5__3 = level.Foreground.Get<Snow>();
				level.Add(level.HiresSnow = new HiresSnow());
				level.HiresSnow.Alpha = 0f;
				_003Cease_003E5__4 = 0f;
				break;
			case 12:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0484:
				if (!cS00_Ending.player.Dead && !cS00_Ending.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 9;
					return true;
				}
				_003C_003E2__current = 2f;
				_003C_003E1__state = 10;
				return true;
				IL_0224:
				if (_003Cpercent_003E5__6 < 1f)
				{
					cS00_Ending.bird.Position = _003Cfrom_003E5__7 + (_003Cto_003E5__8 - _003Cfrom_003E5__7) * Ease.QuadOut(_003Cpercent_003E5__6);
					Audio.Position(_003Cinstance_003E5__5, cS00_Ending.bird.Position);
					if (_003Cpercent_003E5__6 > 0.5f)
					{
						cS00_Ending.bird.Sprite.Play("fly");
					}
					_003Cpercent_003E5__6 += Engine.RawDeltaTime * 0.5f;
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				cS00_Ending.bird.Position = _003Cto_003E5__8;
				_003Cinstance_003E5__5 = null;
				_003Cfrom_003E5__7 = default(Vector2);
				_003Cto_003E5__8 = default(Vector2);
				Audio.Play("event:/game/general/bird_land_dirt", cS00_Ending.bird.Position);
				Dust.Burst(cS00_Ending.bird.Position, -(float)Math.PI / 2f, 12);
				cS00_Ending.bird.Sprite.Play("idle");
				_003C_003E2__current = cS00_Ending.WaitFor(0.5f);
				_003C_003E1__state = 4;
				return true;
				IL_0377:
				aimVector = Input.GetAimVector();
				if (aimVector.X > 0f && aimVector.Y < 0f && Input.Dash.Pressed)
				{
					cS00_Ending.player.StateMachine.State = 16;
					cS00_Ending.player.Dashes = 0;
					level.Session.Inventory.Dashes = 1;
					Engine.TimeRate = 1f;
					cS00_Ending.keyOffed = true;
					Audio.CurrentMusicEventInstance.triggerCue();
					cS00_Ending.bird.Add(new Coroutine(cS00_Ending.bird.HideTutorial()));
					_003C_003E2__current = 0.25f;
					_003C_003E1__state = 8;
					return true;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 7;
				return true;
				IL_00ba:
				if (Engine.TimeRate > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				Engine.TimeRate = 0f;
				cS00_Ending.player.StateMachine.State = 11;
				cS00_Ending.player.Facing = Facings.Right;
				_003C_003E2__current = cS00_Ending.WaitFor(1f);
				_003C_003E1__state = 2;
				return true;
			}
			if (_003Cease_003E5__4 < 1f)
			{
				_003Cease_003E5__4 += Engine.DeltaTime * 0.25f;
				float num2 = Ease.CubeInOut(_003Cease_003E5__4);
				if (_003CfgSnow_003E5__3 != null)
				{
					_003CfgSnow_003E5__3.Alpha -= Engine.DeltaTime * 0.5f;
				}
				if (_003CbgSnow_003E5__2 != null)
				{
					_003CbgSnow_003E5__2.Alpha -= Engine.DeltaTime * 0.5f;
				}
				level.HiresSnow.Alpha = Calc.Approach(level.HiresSnow.Alpha, 1f, Engine.DeltaTime * 0.5f);
				cS00_Ending.endingText.Position = new Vector2(960f, 540f - 1080f * (1f - num2));
				level.Camera.Y = (float)level.Bounds.Top - 3900f * num2;
				_003C_003E2__current = null;
				_003C_003E1__state = 12;
				return true;
			}
			cS00_Ending.EndCutscene(level);
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
	private sealed class _003CWaitFor_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float time;

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
		public _003CWaitFor_003Ed__8(int _003C_003E1__state)
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
				_003Ct_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__2 += Engine.RawDeltaTime;
				break;
			}
			if (_003Ct_003E5__2 < time)
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

	private Player player;

	private BirdNPC bird;

	private Bridge bridge;

	private bool keyOffed;

	private PrologueEndingText endingText;

	public CS00_Ending(Player player, BirdNPC bird, Bridge bridge)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.player = player;
		this.bird = bird;
		this.bridge = bridge;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene(Level level)
	{
		while (Engine.TimeRate > 0f)
		{
			yield return null;
			if (Engine.TimeRate < 0.5f && bridge != null)
			{
				bridge.StopCollapseLoop();
			}
			level.StopShake();
			MInput.GamePads[Input.Gamepad].StopRumble();
			Engine.TimeRate -= Engine.RawDeltaTime * 2f;
		}
		Engine.TimeRate = 0f;
		player.StateMachine.State = 11;
		player.Facing = Facings.Right;
		yield return WaitFor(1f);
		FMOD.Studio.EventInstance instance = Audio.Play("event:/game/general/bird_in", bird.Position);
		bird.Facing = Facings.Left;
		bird.Sprite.Play("fall");
		float percent = 0f;
		Vector2 from = bird.Position;
		Vector2 to = bird.StartPosition;
		while (percent < 1f)
		{
			bird.Position = from + (to - from) * Ease.QuadOut(percent);
			Audio.Position(instance, bird.Position);
			if (percent > 0.5f)
			{
				bird.Sprite.Play("fly");
			}
			percent += Engine.RawDeltaTime * 0.5f;
			yield return null;
		}
		bird.Position = to;
		Audio.Play("event:/game/general/bird_land_dirt", bird.Position);
		Dust.Burst(bird.Position, -(float)Math.PI / 2f, 12);
		bird.Sprite.Play("idle");
		yield return WaitFor(0.5f);
		bird.Sprite.Play("peck");
		yield return WaitFor(1.1f);
		yield return bird.ShowTutorial(new BirdTutorialGui(bird, new Vector2(0f, -16f), Dialog.Clean("tutorial_dash"), new Vector2(1f, -1f), "+", BirdTutorialGui.ButtonPrompt.Dash), caw: true);
		while (true)
		{
			Vector2 aimVector = Input.GetAimVector();
			if (aimVector.X > 0f && aimVector.Y < 0f && Input.Dash.Pressed)
			{
				break;
			}
			yield return null;
		}
		player.StateMachine.State = 16;
		player.Dashes = 0;
		level.Session.Inventory.Dashes = 1;
		Engine.TimeRate = 1f;
		keyOffed = true;
		Audio.CurrentMusicEventInstance.triggerCue();
		bird.Add(new Coroutine(bird.HideTutorial()));
		yield return 0.25f;
		bird.Add(new Coroutine(bird.StartleAndFlyAway()));
		while (!player.Dead && !player.OnGround())
		{
			yield return null;
		}
		yield return 2f;
		Audio.SetMusic("event:/music/lvl0/title_ping");
		yield return 2f;
		endingText = new PrologueEndingText(instant: false);
		base.Scene.Add(endingText);
		Snow bgSnow = level.Background.Get<Snow>();
		Snow fgSnow = level.Foreground.Get<Snow>();
		level.Add(level.HiresSnow = new HiresSnow());
		level.HiresSnow.Alpha = 0f;
		float ease = 0f;
		while (ease < 1f)
		{
			ease += Engine.DeltaTime * 0.25f;
			float num = Ease.CubeInOut(ease);
			if (fgSnow != null)
			{
				fgSnow.Alpha -= Engine.DeltaTime * 0.5f;
			}
			if (bgSnow != null)
			{
				bgSnow.Alpha -= Engine.DeltaTime * 0.5f;
			}
			level.HiresSnow.Alpha = Calc.Approach(level.HiresSnow.Alpha, 1f, Engine.DeltaTime * 0.5f);
			endingText.Position = new Vector2(960f, 540f - 1080f * (1f - num));
			level.Camera.Y = (float)level.Bounds.Top - 3900f * num;
			yield return null;
		}
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CWaitFor_003Ed__8))]
	private IEnumerator WaitFor(float time)
	{
		for (float t = 0f; t < time; t += Engine.RawDeltaTime)
		{
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped)
		{
			if (bird != null)
			{
				bird.Visible = false;
			}
			if (player != null)
			{
				player.Position = new Vector2(2120f, 40f);
				player.StateMachine.State = 11;
				player.DummyAutoAnimate = false;
				player.Sprite.Play("tired");
				player.Speed = Vector2.Zero;
			}
			if (!keyOffed)
			{
				Audio.CurrentMusicEventInstance.triggerCue();
			}
			if (level.HiresSnow == null)
			{
				level.Add(level.HiresSnow = new HiresSnow());
			}
			level.HiresSnow.Alpha = 1f;
			Snow snow = level.Background.Get<Snow>();
			if (snow != null)
			{
				snow.Alpha = 0f;
			}
			Snow snow2 = level.Foreground.Get<Snow>();
			if (snow2 != null)
			{
				snow2.Alpha = 0f;
			}
			if (endingText != null)
			{
				level.Remove(endingText);
			}
			level.Add(endingText = new PrologueEndingText(instant: true));
			endingText.Position = new Vector2(960f, 540f);
			level.Camera.Y = level.Bounds.Top - 3900;
		}
		Engine.TimeRate = 1f;
		level.PauseLock = true;
		level.Entities.FindFirst<SpeedrunTimerDisplay>().CompleteTimer = 10f;
		level.Add(new EndingCutsceneDelay());
	}
}
