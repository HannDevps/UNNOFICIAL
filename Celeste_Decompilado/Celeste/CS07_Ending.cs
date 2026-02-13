using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS07_Ending : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Ending _003C_003E4__this;

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
		public _003CCutscene_003Ed__5(int _003C_003E1__state)
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
			CS07_Ending cS07_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusic(null);
				cS07_Ending.player.StateMachine.State = 11;
				_003C_003E2__current = cS07_Ending.player.DummyWalkTo(cS07_Ending.target.X);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS07_Ending.Add(new Coroutine(CutsceneEntity.CameraTo(cS07_Ending.target + new Vector2(-160f, -130f), 3f, Ease.CubeInOut)));
				cS07_Ending.player.Facing = Facings.Right;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS07_Ending.player.Sprite.Play("idle");
				cS07_Ending.player.DummyAutoAnimate = false;
				cS07_Ending.player.Dashes = 1;
				level.Session.Inventory.Dashes = 1;
				level.Add(cS07_Ending.badeline = new BadelineDummy(cS07_Ending.player.Center));
				cS07_Ending.player.CreateSplitParticles();
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				cS07_Ending.Level.Displacement.AddBurst(cS07_Ending.player.Center, 0.4f, 8f, 32f, 0.5f);
				cS07_Ending.badeline.Sprite.Scale.X = 1f;
				Audio.Play("event:/char/badeline/maddy_split", cS07_Ending.player.Position);
				_003C_003E2__current = cS07_Ending.badeline.FloatTo(cS07_Ending.target + new Vector2(-10f, -30f), 1, faceDirection: false);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH7_ENDING", cS07_Ending.WaitABit, cS07_Ending.SitDown, cS07_Ending.BadelineApproaches);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				cS07_Ending.EndCutscene(level);
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
	private sealed class _003CWaitABit_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
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
		public _003CWaitABit_003Ed__6(int _003C_003E1__state)
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
				_003C_003E2__current = 3f;
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
	private sealed class _003CSitDown_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Ending _003C_003E4__this;

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
		public _003CSitDown_003Ed__7(int _003C_003E1__state)
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
			CS07_Ending cS07_Ending = _003C_003E4__this;
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
				cS07_Ending.player.DummyAutoAnimate = true;
				_003C_003E2__current = cS07_Ending.player.DummyWalkTo(cS07_Ending.player.X + 16f, walkBackwards: false, 0.25f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS07_Ending.player.DummyAutoAnimate = false;
				cS07_Ending.player.Sprite.Play("sitDown");
				_003C_003E2__current = 1f;
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
	private sealed class _003CBadelineApproaches_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS07_Ending _003C_003E4__this;

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
		public _003CBadelineApproaches_003Ed__8(int _003C_003E1__state)
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
			CS07_Ending cS07_Ending = _003C_003E4__this;
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
				cS07_Ending.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS07_Ending.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS07_Ending.Add(new Coroutine(CutsceneEntity.CameraTo(cS07_Ending.Level.Camera.Position + new Vector2(88f, 0f), 6f, Ease.CubeInOut)));
				cS07_Ending.badeline.FloatSpeed = 40f;
				_003C_003E2__current = cS07_Ending.badeline.FloatTo(new Vector2(cS07_Ending.player.X - 10f, cS07_Ending.player.Y - 4f));
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
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

	private Player player;

	private BadelineDummy badeline;

	private Vector2 target;

	public CS07_Ending(Player player, Vector2 target)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.player = player;
		this.target = target;
	}

	public override void OnBegin(Level level)
	{
		level.RegisterAreaComplete();
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__5))]
	private IEnumerator Cutscene(Level level)
	{
		Audio.SetMusic(null);
		player.StateMachine.State = 11;
		yield return player.DummyWalkTo(target.X);
		yield return 0.25f;
		Add(new Coroutine(CutsceneEntity.CameraTo(target + new Vector2(-160f, -130f), 3f, Ease.CubeInOut)));
		player.Facing = Facings.Right;
		yield return 1f;
		player.Sprite.Play("idle");
		player.DummyAutoAnimate = false;
		player.Dashes = 1;
		level.Session.Inventory.Dashes = 1;
		level.Add(badeline = new BadelineDummy(player.Center));
		player.CreateSplitParticles();
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		Level.Displacement.AddBurst(player.Center, 0.4f, 8f, 32f, 0.5f);
		badeline.Sprite.Scale.X = 1f;
		Audio.Play("event:/char/badeline/maddy_split", player.Position);
		yield return badeline.FloatTo(target + new Vector2(-10f, -30f), 1, faceDirection: false);
		yield return 0.5f;
		yield return Textbox.Say("CH7_ENDING", WaitABit, SitDown, BadelineApproaches);
		yield return 1f;
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CWaitABit_003Ed__6))]
	private IEnumerator WaitABit()
	{
		yield return 3f;
	}

	[IteratorStateMachine(typeof(_003CSitDown_003Ed__7))]
	private IEnumerator SitDown()
	{
		yield return 0.5f;
		player.DummyAutoAnimate = true;
		yield return player.DummyWalkTo(player.X + 16f, walkBackwards: false, 0.25f);
		yield return 0.1f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("sitDown");
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CBadelineApproaches_003Ed__8))]
	private IEnumerator BadelineApproaches()
	{
		yield return 0.5f;
		badeline.Sprite.Scale.X = -1f;
		yield return 1f;
		badeline.Sprite.Scale.X = 1f;
		yield return 1f;
		Add(new Coroutine(CutsceneEntity.CameraTo(Level.Camera.Position + new Vector2(88f, 0f), 6f, Ease.CubeInOut)));
		badeline.FloatSpeed = 40f;
		yield return badeline.FloatTo(new Vector2(player.X - 10f, player.Y - 4f));
		yield return 0.5f;
	}

	public override void OnEnd(Level level)
	{
		Audio.SetMusic(null);
		ScreenWipe screenWipe = level.CompleteArea(spotlightWipe: false);
		if (screenWipe != null)
		{
			screenWipe.Duration = 2f;
			screenWipe.EndTimer = 1f;
		}
	}
}
