using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_Farewell : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CCutscene_003Ed__9(int _003C_003E1__state)
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
			CS10_Farewell CS_0024_003C_003E8__locals54 = _003C_003E4__this;
			GrannyLaughSfx component;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals54.player.Dashes = 1;
				CS_0024_003C_003E8__locals54.player.StateMachine.State = 11;
				CS_0024_003C_003E8__locals54.player.Sprite.Play("idle");
				CS_0024_003C_003E8__locals54.player.Visible = false;
				Audio.SetMusic("event:/new_content/music/lvl10/granny_farewell");
				FadeWipe fadeWipe = new FadeWipe(CS_0024_003C_003E8__locals54.Level, wipeIn: true)
				{
					Duration = 2f
				};
				ScreenWipe.WipeColor = Color.White;
				_003C_003E2__current = fadeWipe.Duration;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals54.Add(new Coroutine(CS_0024_003C_003E8__locals54.Level.ZoomTo(new Vector2(160f, 125f), 2f, 5f)));
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/char/madeline/screenentry_gran");
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_ = CS_0024_003C_003E8__locals54.player.Position;
				CS_0024_003C_003E8__locals54.player.Position = new Vector2(CS_0024_003C_003E8__locals54.player.X, level.Bounds.Bottom + 8);
				CS_0024_003C_003E8__locals54.player.Speed.Y = -160f;
				CS_0024_003C_003E8__locals54.player.Visible = true;
				CS_0024_003C_003E8__locals54.player.DummyGravity = false;
				CS_0024_003C_003E8__locals54.player.MuffleLanding = true;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				goto IL_02a7;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0290;
			case 6:
				_003C_003E1__state = -1;
				goto IL_02a7;
			case 7:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals54.player.Facing = Facings.Left;
				_003C_003E2__current = 1.6f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals54.player.Facing = Facings.Right;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = CS_0024_003C_003E8__locals54.player.DummyWalkToExact((int)CS_0024_003C_003E8__locals54.player.X + 4, walkBackwards: false, 0.4f);
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_FAREWELL", CS_0024_003C_003E8__locals54.Laugh, CS_0024_003C_003E8__locals54.StopLaughing, CS_0024_003C_003E8__locals54.StepForward, CS_0024_003C_003E8__locals54.GrannyDisappear, CS_0024_003C_003E8__locals54.FadeToWhite, CS_0024_003C_003E8__locals54.WaitForGranny);
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				break;
			case 14:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_02a7:
				if (!CS_0024_003C_003E8__locals54.player.OnGround() || CS_0024_003C_003E8__locals54.player.Speed.Y < 0f)
				{
					float y = CS_0024_003C_003E8__locals54.player.Speed.Y;
					CS_0024_003C_003E8__locals54.player.Speed.Y += Engine.DeltaTime * 900f * 0.2f;
					if (y < 0f && CS_0024_003C_003E8__locals54.player.Speed.Y >= 0f)
					{
						CS_0024_003C_003E8__locals54.player.Speed.Y = 0f;
						_003C_003E2__current = 0.2f;
						_003C_003E1__state = 5;
						return true;
					}
					goto IL_0290;
				}
				Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
				Audio.Play("event:/new_content/char/madeline/screenentry_gran_landing", CS_0024_003C_003E8__locals54.player.Position);
				CS_0024_003C_003E8__locals54.granny = new NPC(CS_0024_003C_003E8__locals54.player.Position + new Vector2(164f, 0f));
				CS_0024_003C_003E8__locals54.granny.IdleAnim = "idle";
				CS_0024_003C_003E8__locals54.granny.MoveAnim = "walk";
				CS_0024_003C_003E8__locals54.granny.Maxspeed = 15f;
				CS_0024_003C_003E8__locals54.granny.Add(CS_0024_003C_003E8__locals54.granny.Sprite = GFX.SpriteBank.Create("granny"));
				component = new GrannyLaughSfx(CS_0024_003C_003E8__locals54.granny.Sprite)
				{
					FirstPlay = false
				};
				CS_0024_003C_003E8__locals54.granny.Add(component);
				CS_0024_003C_003E8__locals54.granny.Sprite.OnFrameChange = delegate(string anim)
				{
					int currentAnimationFrame = CS_0024_003C_003E8__locals54.granny.Sprite.CurrentAnimationFrame;
					if (anim == "walk" && currentAnimationFrame == 2)
					{
						float volume = Calc.ClampedMap((CS_0024_003C_003E8__locals54.player.Position - CS_0024_003C_003E8__locals54.granny.Position).Length(), 64f, 128f, 1f, 0f);
						Audio.Play("event:/new_content/char/granny/cane_tap_ending", CS_0024_003C_003E8__locals54.granny.Position).setVolume(volume);
					}
				};
				CS_0024_003C_003E8__locals54.Scene.Add(CS_0024_003C_003E8__locals54.granny);
				CS_0024_003C_003E8__locals54.grannyWalk = new Coroutine(CS_0024_003C_003E8__locals54.granny.MoveTo(CS_0024_003C_003E8__locals54.player.Position + new Vector2(32f, 0f)));
				CS_0024_003C_003E8__locals54.Add(CS_0024_003C_003E8__locals54.grannyWalk);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 7;
				return true;
				IL_0290:
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
				return true;
			}
			if (CS_0024_003C_003E8__locals54.fade < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 14;
				return true;
			}
			CS_0024_003C_003E8__locals54.EndCutscene(level);
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
	private sealed class _003CWaitForGranny_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CWaitForGranny_003Ed__10(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
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
			if (cS10_Farewell.grannyWalk != null && !cS10_Farewell.grannyWalk.Finished)
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
	private sealed class _003CLaugh_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CLaugh_003Ed__11(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS10_Farewell.granny.Sprite.Play("laugh");
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
	private sealed class _003CStopLaughing_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CStopLaughing_003Ed__12(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS10_Farewell.granny.Sprite.Play("idle");
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
	private sealed class _003CStepForward_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CStepForward_003Ed__13(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Farewell.player.DummyWalkToExact((int)cS10_Farewell.player.X + 8, walkBackwards: false, 0.4f);
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
	private sealed class _003CGrannyDisappear_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CGrannyDisappear_003Ed__14(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusicParam("end", 1f);
				cS10_Farewell.Add(new Coroutine(cS10_Farewell.player.DummyWalkToExact((int)cS10_Farewell.player.X + 8, walkBackwards: false, 0.4f)));
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				cS10_Farewell.dissipate = Audio.Play("event:/new_content/char/granny/dissipate", cS10_Farewell.granny.Position);
				MTexture frame = cS10_Farewell.granny.Sprite.GetFrame(cS10_Farewell.granny.Sprite.CurrentAnimationID, cS10_Farewell.granny.Sprite.CurrentAnimationFrame);
				cS10_Farewell.Level.Add(new DisperseImage(cS10_Farewell.granny.Position, new Vector2(1f, -0.1f), cS10_Farewell.granny.Sprite.Origin, cS10_Farewell.granny.Sprite.Scale, frame));
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			case 2:
				_003C_003E1__state = -1;
				cS10_Farewell.granny.Visible = false;
				_003C_003E2__current = 3.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003CFadeToWhite_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CFadeToWhite_003Ed__15(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
			if (num != 0)
			{
				return false;
			}
			_003C_003E1__state = -1;
			cS10_Farewell.Add(new Coroutine(cS10_Farewell.DoFadeToWhite()));
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
	private sealed class _003CDoFadeToWhite_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Farewell _003C_003E4__this;

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
		public _003CDoFadeToWhite_003Ed__16(int _003C_003E1__state)
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
			CS10_Farewell cS10_Farewell = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_Farewell.Add(new Coroutine(cS10_Farewell.Level.ZoomBack(8f)));
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS10_Farewell.fade < 1f)
			{
				cS10_Farewell.fade = Calc.Approach(cS10_Farewell.fade, 1f, Engine.DeltaTime / 8f);
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

	private NPC granny;

	private float fade;

	private Coroutine grannyWalk;

	private FMOD.Studio.EventInstance snapshot;

	private FMOD.Studio.EventInstance dissipate;

	public CS10_Farewell(Player player)
		: base(fadeInOnSkip: false)
	{
		this.player = player;
		base.Depth = -1000000;
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Level obj = scene as Level;
		obj.TimerStopped = true;
		obj.TimerHidden = true;
		obj.SaveQuitDisabled = true;
		obj.SnapColorGrade("none");
		snapshot = Audio.CreateSnapshot("snapshot:/game_10_granny_clouds_dialogue");
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__9))]
	private IEnumerator Cutscene(Level level)
	{
		player.Dashes = 1;
		player.StateMachine.State = 11;
		player.Sprite.Play("idle");
		player.Visible = false;
		Audio.SetMusic("event:/new_content/music/lvl10/granny_farewell");
		FadeWipe fadeWipe = new FadeWipe(Level, wipeIn: true);
		fadeWipe.Duration = 2f;
		ScreenWipe.WipeColor = Color.White;
		yield return fadeWipe.Duration;
		yield return 1.5f;
		Add(new Coroutine(Level.ZoomTo(new Vector2(160f, 125f), 2f, 5f)));
		yield return 0.2f;
		Audio.Play("event:/new_content/char/madeline/screenentry_gran");
		yield return 0.3f;
		_ = player.Position;
		player.Position = new Vector2(player.X, level.Bounds.Bottom + 8);
		player.Speed.Y = -160f;
		player.Visible = true;
		player.DummyGravity = false;
		player.MuffleLanding = true;
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		while (!player.OnGround() || player.Speed.Y < 0f)
		{
			float y = player.Speed.Y;
			player.Speed.Y += Engine.DeltaTime * 900f * 0.2f;
			if (y < 0f && player.Speed.Y >= 0f)
			{
				player.Speed.Y = 0f;
				yield return 0.2f;
			}
			yield return null;
		}
		Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
		Audio.Play("event:/new_content/char/madeline/screenentry_gran_landing", player.Position);
		granny = new NPC(player.Position + new Vector2(164f, 0f));
		granny.IdleAnim = "idle";
		granny.MoveAnim = "walk";
		granny.Maxspeed = 15f;
		granny.Add(granny.Sprite = GFX.SpriteBank.Create("granny"));
		GrannyLaughSfx grannyLaughSfx = new GrannyLaughSfx(granny.Sprite);
		grannyLaughSfx.FirstPlay = false;
		granny.Add(grannyLaughSfx);
		granny.Sprite.OnFrameChange = delegate(string anim)
		{
			int currentAnimationFrame = granny.Sprite.CurrentAnimationFrame;
			if (anim == "walk" && currentAnimationFrame == 2)
			{
				float volume = Calc.ClampedMap((player.Position - granny.Position).Length(), 64f, 128f, 1f, 0f);
				Audio.Play("event:/new_content/char/granny/cane_tap_ending", granny.Position).setVolume(volume);
			}
		};
		base.Scene.Add(granny);
		grannyWalk = new Coroutine(granny.MoveTo(player.Position + new Vector2(32f, 0f)));
		Add(grannyWalk);
		yield return 2f;
		player.Facing = Facings.Left;
		yield return 1.6f;
		player.Facing = Facings.Right;
		yield return 0.8f;
		yield return player.DummyWalkToExact((int)player.X + 4, walkBackwards: false, 0.4f);
		yield return 0.8f;
		yield return Textbox.Say("CH9_FAREWELL", Laugh, StopLaughing, StepForward, GrannyDisappear, FadeToWhite, WaitForGranny);
		yield return 2f;
		while (fade < 1f)
		{
			yield return null;
		}
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CWaitForGranny_003Ed__10))]
	private IEnumerator WaitForGranny()
	{
		while (grannyWalk != null && !grannyWalk.Finished)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CLaugh_003Ed__11))]
	private IEnumerator Laugh()
	{
		granny.Sprite.Play("laugh");
		yield break;
	}

	[IteratorStateMachine(typeof(_003CStopLaughing_003Ed__12))]
	private IEnumerator StopLaughing()
	{
		granny.Sprite.Play("idle");
		yield break;
	}

	[IteratorStateMachine(typeof(_003CStepForward_003Ed__13))]
	private IEnumerator StepForward()
	{
		yield return player.DummyWalkToExact((int)player.X + 8, walkBackwards: false, 0.4f);
	}

	[IteratorStateMachine(typeof(_003CGrannyDisappear_003Ed__14))]
	private IEnumerator GrannyDisappear()
	{
		Audio.SetMusicParam("end", 1f);
		Add(new Coroutine(player.DummyWalkToExact((int)player.X + 8, walkBackwards: false, 0.4f)));
		yield return 0.1f;
		dissipate = Audio.Play("event:/new_content/char/granny/dissipate", granny.Position);
		MTexture frame = granny.Sprite.GetFrame(granny.Sprite.CurrentAnimationID, granny.Sprite.CurrentAnimationFrame);
		Level.Add(new DisperseImage(granny.Position, new Vector2(1f, -0.1f), granny.Sprite.Origin, granny.Sprite.Scale, frame));
		yield return null;
		granny.Visible = false;
		yield return 3.5f;
	}

	[IteratorStateMachine(typeof(_003CFadeToWhite_003Ed__15))]
	private IEnumerator FadeToWhite()
	{
		Add(new Coroutine(DoFadeToWhite()));
		yield break;
	}

	[IteratorStateMachine(typeof(_003CDoFadeToWhite_003Ed__16))]
	private IEnumerator DoFadeToWhite()
	{
		Add(new Coroutine(Level.ZoomBack(8f)));
		while (fade < 1f)
		{
			fade = Calc.Approach(fade, 1f, Engine.DeltaTime / 8f);
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		Dispose();
		if (WasSkipped)
		{
			Audio.Stop(dissipate);
		}
		Level.OnEndOfFrame += delegate
		{
			Achievements.Register(Achievement.FAREWELL);
			Level.TeleportTo(player, "end-cinematic", Player.IntroTypes.Transition);
		};
	}

	public override void SceneEnd(Scene scene)
	{
		base.SceneEnd(scene);
		Dispose();
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		Dispose();
	}

	private void Dispose()
	{
		Audio.ReleaseSnapshot(snapshot);
		snapshot = null;
	}

	public override void Render()
	{
		if (fade > 0f)
		{
			Draw.Rect(Level.Camera.X - 1f, Level.Camera.Y - 1f, 322f, 182f, Color.White * fade);
		}
	}
}
