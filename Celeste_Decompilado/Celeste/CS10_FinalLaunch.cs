using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_FinalLaunch : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_FinalLaunch _003C_003E4__this;

		private FadeWipe _003Cwipe_003E5__2;

		private float _003Cp_003E5__3;

		private int _003Cto_003E5__4;

		private float _003Cp_003E5__5;

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
		public _003CCutscene_003Ed__14(int _003C_003E1__state)
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
			CS10_FinalLaunch cS10_FinalLaunch = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Engine.TimeRate = 1f;
				cS10_FinalLaunch.boost.Active = false;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (cS10_FinalLaunch.sayDialog)
				{
					_003C_003E2__current = Textbox.Say("CH9_LAST_BOOST");
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 0.152f;
				_003C_003E1__state = 3;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00be;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00be;
			case 4:
			{
				_003C_003E1__state = -1;
				BlackholeBG blackholeBG = cS10_FinalLaunch.Level.Background.Get<BlackholeBG>();
				blackholeBG.Direction = -2.5f;
				blackholeBG.SnapStrength(cS10_FinalLaunch.Level, BlackholeBG.Strengths.High);
				blackholeBG.CenterOffset.Y = 100f;
				blackholeBG.OffsetOffset.Y = -50f;
				cS10_FinalLaunch.Add(cS10_FinalLaunch.wave = new Coroutine(cS10_FinalLaunch.WaveCamera()));
				cS10_FinalLaunch.Add(new Coroutine(cS10_FinalLaunch.BirdRoutine(0.8f)));
				cS10_FinalLaunch.Level.Add(cS10_FinalLaunch.streaks = new AscendManager.Streaks(null));
				_003Cp_003E5__3 = 0f;
				goto IL_023e;
			}
			case 5:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / 12f;
				goto IL_023e;
			case 6:
				_003C_003E1__state = -1;
				goto IL_0267;
			case 7:
				_003C_003E1__state = -1;
				_003Cp_003E5__5 += Engine.DeltaTime / 2f;
				goto IL_033a;
			case 8:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_023e:
				if (_003Cp_003E5__3 < 1f)
				{
					cS10_FinalLaunch.fadeToWhite = _003Cp_003E5__3;
					cS10_FinalLaunch.streaks.Alpha = _003Cp_003E5__3;
					foreach (Parallax item in cS10_FinalLaunch.Level.Foreground.GetEach<Parallax>("blackhole"))
					{
						item.FadeAlphaMultiplier = 1f - _003Cp_003E5__3;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_0267;
				IL_0267:
				if (cS10_FinalLaunch.bird != null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				_003Cwipe_003E5__2 = new FadeWipe(cS10_FinalLaunch.Level, wipeIn: false);
				_003Cwipe_003E5__2.Duration = 4f;
				ScreenWipe.WipeColor = Color.White;
				if (!cS10_FinalLaunch.hasGolden)
				{
					Audio.SetMusic("event:/new_content/music/lvl10/granny_farewell");
				}
				_003Cp_003E5__3 = cS10_FinalLaunch.cameraOffset.Y;
				_003Cto_003E5__4 = 180;
				_003Cp_003E5__5 = 0f;
				goto IL_033a;
				IL_033a:
				if (_003Cp_003E5__5 < 1f)
				{
					cS10_FinalLaunch.cameraOffset.Y = _003Cp_003E5__3 + ((float)_003Cto_003E5__4 - _003Cp_003E5__3) * Ease.BigBackIn(_003Cp_003E5__5);
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				break;
				IL_00be:
				cS10_FinalLaunch.cameraOffset = new Vector2(0f, -20f);
				cS10_FinalLaunch.boost.Active = true;
				cS10_FinalLaunch.player.EnforceLevelBounds = false;
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
			}
			if (_003Cwipe_003E5__2.Percent < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 8;
				return true;
			}
			cS10_FinalLaunch.EndCutscene(cS10_FinalLaunch.Level);
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
	private sealed class _003CWaveCamera_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_FinalLaunch _003C_003E4__this;

		private float _003Ctimer_003E5__2;

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
		public _003CWaveCamera_003Ed__16(int _003C_003E1__state)
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
			CS10_FinalLaunch cS10_FinalLaunch = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				_003Ctimer_003E5__2 = 0f;
			}
			cS10_FinalLaunch.cameraWaveOffset.X = (float)Math.Sin(_003Ctimer_003E5__2) * 16f;
			cS10_FinalLaunch.cameraWaveOffset.Y = (float)Math.Sin(_003Ctimer_003E5__2 * 0.5f) * 16f + (float)Math.Sin(_003Ctimer_003E5__2 * 0.25f) * 8f;
			_003Ctimer_003E5__2 += Engine.DeltaTime * 2f;
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
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
	private sealed class _003CBirdRoutine_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CS10_FinalLaunch _003C_003E4__this;

		private Vector2 _003CtopCenter_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private Vector2 _003Cto_003E5__4;

		private float _003Ct_003E5__5;

		private bool _003CplayedAnim_003E5__6;

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
		public _003CBirdRoutine_003Ed__17(int _003C_003E1__state)
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
			CS10_FinalLaunch cS10_FinalLaunch = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				cS10_FinalLaunch.Level.Add(cS10_FinalLaunch.bird = new BirdNPC(Vector2.Zero, BirdNPC.Modes.None));
				cS10_FinalLaunch.bird.Sprite.Play("flyupIdle");
				Vector2 vector = new Vector2(320f, 180f) / 2f;
				_003CtopCenter_003E5__2 = new Vector2(vector.X, 0f);
				Vector2 vector2 = new Vector2(vector.X, 180f);
				_003Cfrom_003E5__3 = vector2 + new Vector2(40f, 40f);
				_003Cto_003E5__4 = vector + new Vector2(-32f, -24f);
				_003Ct_003E5__5 = 0f;
				goto IL_0175;
			}
			case 2:
				_003C_003E1__state = -1;
				_003Ct_003E5__5 += Engine.DeltaTime / 4f;
				goto IL_0175;
			case 3:
				_003C_003E1__state = -1;
				_003Ct_003E5__5 += Engine.DeltaTime / 2f;
				goto IL_020a;
			case 4:
				{
					_003C_003E1__state = -1;
					_003Ct_003E5__5 += Engine.DeltaTime / 4f;
					break;
				}
				IL_0175:
				if (_003Ct_003E5__5 < 1f)
				{
					cS10_FinalLaunch.birdScreenPosition = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.BackOut(_003Ct_003E5__5);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				cS10_FinalLaunch.bird.Sprite.Play("flyupRoll");
				_003Ct_003E5__5 = 0f;
				goto IL_020a;
				IL_020a:
				if (_003Ct_003E5__5 < 1f)
				{
					cS10_FinalLaunch.birdScreenPosition = _003Cto_003E5__4 + new Vector2(64f, 0f) * Ease.CubeInOut(_003Ct_003E5__5);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003Cfrom_003E5__3 = default(Vector2);
				_003Cto_003E5__4 = default(Vector2);
				_003Cto_003E5__4 = cS10_FinalLaunch.birdScreenPosition;
				_003Cfrom_003E5__3 = _003CtopCenter_003E5__2 + new Vector2(-40f, -100f);
				_003CplayedAnim_003E5__6 = false;
				_003Ct_003E5__5 = 0f;
				break;
			}
			if (_003Ct_003E5__5 < 1f)
			{
				if (_003Ct_003E5__5 >= 0.35f && !_003CplayedAnim_003E5__6)
				{
					cS10_FinalLaunch.bird.Sprite.Play("flyupRoll");
					_003CplayedAnim_003E5__6 = true;
				}
				cS10_FinalLaunch.birdScreenPosition = _003Cto_003E5__4 + (_003Cfrom_003E5__3 - _003Cto_003E5__4) * Ease.BigBackIn(_003Ct_003E5__5);
				cS10_FinalLaunch.birdScreenPosition.X += _003Ct_003E5__5 * 32f;
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
			}
			cS10_FinalLaunch.bird.RemoveSelf();
			cS10_FinalLaunch.bird = null;
			_003Cto_003E5__4 = default(Vector2);
			_003Cfrom_003E5__3 = default(Vector2);
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

	private BadelineBoost boost;

	private BirdNPC bird;

	private float fadeToWhite;

	private Vector2 birdScreenPosition;

	private AscendManager.Streaks streaks;

	private Vector2 cameraWaveOffset;

	private Vector2 cameraOffset;

	private float timer;

	private Coroutine wave;

	private bool hasGolden;

	private bool sayDialog;

	public CS10_FinalLaunch(Player player, BadelineBoost boost, bool sayDialog = true)
		: base(fadeInOnSkip: false)
	{
		this.player = player;
		this.boost = boost;
		this.sayDialog = sayDialog;
		base.Depth = 10010;
	}

	public override void OnBegin(Level level)
	{
		Audio.SetMusic(null);
		ScreenWipe.WipeColor = Color.White;
		foreach (Follower follower in player.Leader.Followers)
		{
			if (follower.Entity is Strawberry { Golden: not false })
			{
				hasGolden = true;
				break;
			}
		}
		Add(new Coroutine(Cutscene()));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__14))]
	private IEnumerator Cutscene()
	{
		Engine.TimeRate = 1f;
		boost.Active = false;
		yield return null;
		if (sayDialog)
		{
			yield return Textbox.Say("CH9_LAST_BOOST");
		}
		else
		{
			yield return 0.152f;
		}
		cameraOffset = new Vector2(0f, -20f);
		boost.Active = true;
		player.EnforceLevelBounds = false;
		yield return null;
		BlackholeBG blackholeBG = Level.Background.Get<BlackholeBG>();
		blackholeBG.Direction = -2.5f;
		blackholeBG.SnapStrength(Level, BlackholeBG.Strengths.High);
		blackholeBG.CenterOffset.Y = 100f;
		blackholeBG.OffsetOffset.Y = -50f;
		Add(wave = new Coroutine(WaveCamera()));
		Add(new Coroutine(BirdRoutine(0.8f)));
		Level.Add(streaks = new AscendManager.Streaks(null));
		float p;
		for (p = 0f; p < 1f; p += Engine.DeltaTime / 12f)
		{
			fadeToWhite = p;
			streaks.Alpha = p;
			foreach (Parallax item in Level.Foreground.GetEach<Parallax>("blackhole"))
			{
				item.FadeAlphaMultiplier = 1f - p;
			}
			yield return null;
		}
		while (bird != null)
		{
			yield return null;
		}
		FadeWipe wipe = new FadeWipe(Level, wipeIn: false)
		{
			Duration = 4f
		};
		ScreenWipe.WipeColor = Color.White;
		if (!hasGolden)
		{
			Audio.SetMusic("event:/new_content/music/lvl10/granny_farewell");
		}
		p = cameraOffset.Y;
		int to = 180;
		for (float p2 = 0f; p2 < 1f; p2 += Engine.DeltaTime / 2f)
		{
			cameraOffset.Y = p + ((float)to - p) * Ease.BigBackIn(p2);
			yield return null;
		}
		while (wipe.Percent < 1f)
		{
			yield return null;
		}
		EndCutscene(Level);
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped && boost != null && boost.Ch9FinalBoostSfx != null)
		{
			boost.Ch9FinalBoostSfx.stop(STOP_MODE.ALLOWFADEOUT);
			boost.Ch9FinalBoostSfx.release();
		}
		string nextLevelName = "end-granny";
		Player.IntroTypes nextLevelIntro = Player.IntroTypes.Transition;
		if (hasGolden)
		{
			nextLevelName = "end-golden";
			nextLevelIntro = Player.IntroTypes.Jump;
		}
		player.Active = true;
		player.Speed = Vector2.Zero;
		player.EnforceLevelBounds = true;
		player.StateMachine.State = 0;
		player.DummyFriction = true;
		player.DummyGravity = true;
		player.DummyAutoAnimate = true;
		player.ForceCameraUpdate = false;
		Engine.TimeRate = 1f;
		Level.OnEndOfFrame += delegate
		{
			Level.TeleportTo(player, nextLevelName, nextLevelIntro);
			if (hasGolden)
			{
				if (Level.Wipe != null)
				{
					Level.Wipe.Cancel();
				}
				Level.SnapColorGrade("golden");
				new FadeWipe(level, wipeIn: true).Duration = 2f;
				ScreenWipe.WipeColor = Color.White;
			}
		};
	}

	[IteratorStateMachine(typeof(_003CWaveCamera_003Ed__16))]
	private IEnumerator WaveCamera()
	{
		float timer = 0f;
		while (true)
		{
			cameraWaveOffset.X = (float)Math.Sin(timer) * 16f;
			cameraWaveOffset.Y = (float)Math.Sin(timer * 0.5f) * 16f + (float)Math.Sin(timer * 0.25f) * 8f;
			timer += Engine.DeltaTime * 2f;
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CBirdRoutine_003Ed__17))]
	private IEnumerator BirdRoutine(float delay)
	{
		yield return delay;
		Level.Add(bird = new BirdNPC(Vector2.Zero, BirdNPC.Modes.None));
		bird.Sprite.Play("flyupIdle");
		Vector2 vector = new Vector2(320f, 180f) / 2f;
		Vector2 topCenter = new Vector2(vector.X, 0f);
		Vector2 vector2 = new Vector2(vector.X, 180f);
		Vector2 from = vector2 + new Vector2(40f, 40f);
		Vector2 to = vector + new Vector2(-32f, -24f);
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / 4f)
		{
			birdScreenPosition = from + (to - from) * Ease.BackOut(t);
			yield return null;
		}
		bird.Sprite.Play("flyupRoll");
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / 2f)
		{
			birdScreenPosition = to + new Vector2(64f, 0f) * Ease.CubeInOut(t);
			yield return null;
		}
		to = birdScreenPosition;
		from = topCenter + new Vector2(-40f, -100f);
		bool playedAnim = false;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / 4f)
		{
			if (t >= 0.35f && !playedAnim)
			{
				bird.Sprite.Play("flyupRoll");
				playedAnim = true;
			}
			birdScreenPosition = to + (from - to) * Ease.BigBackIn(t);
			birdScreenPosition.X += t * 32f;
			yield return null;
		}
		bird.RemoveSelf();
		bird = null;
	}

	public override void Update()
	{
		base.Update();
		timer += Engine.DeltaTime;
		if (bird != null)
		{
			bird.Position = Level.Camera.Position + birdScreenPosition;
			bird.Position.X += (float)Math.Sin(timer) * 4f;
			bird.Position.Y += (float)Math.Sin(timer * 0.1f) * 4f + (float)Math.Sin(timer * 0.25f) * 4f;
		}
		Level.CameraOffset = cameraOffset + cameraWaveOffset;
	}

	public override void Render()
	{
		Camera camera = Level.Camera;
		Draw.Rect(camera.X - 1f, camera.Y - 1f, 322f, 322f, Color.White * fadeToWhite);
	}
}
