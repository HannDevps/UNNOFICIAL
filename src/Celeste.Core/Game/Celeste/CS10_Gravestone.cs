using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_Gravestone : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Gravestone _003C_003E4__this;

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
			CS10_Gravestone cS10_Gravestone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_Gravestone.player.StateMachine.State = 11;
				cS10_Gravestone.player.ForceCameraUpdate = true;
				cS10_Gravestone.player.DummyGravity = false;
				cS10_Gravestone.player.Speed.Y = 0f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.player.DummyWalkToExact((int)cS10_Gravestone.gravestone.X - 30);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS10_Gravestone.player.Facing = Facings.Right;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.Level.ZoomTo(new Vector2(160f, 90f), 2f, 3f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS10_Gravestone.player.ForceCameraUpdate = false;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_GRAVESTONE", cS10_Gravestone.StepForward, cS10_Gravestone.BadelineAppears, cS10_Gravestone.SitDown);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.BirdStuff();
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.BadelineRejoin();
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.Level.ZoomBack(0.5f);
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				cS10_Gravestone.addedBooster = true;
				cS10_Gravestone.Level.Displacement.AddBurst(cS10_Gravestone.boostTarget, 0.5f, 8f, 32f, 0.5f);
				Audio.Play("event:/new_content/char/badeline/booster_first_appear", cS10_Gravestone.boostTarget);
				cS10_Gravestone.Level.Add(new BadelineBoost(new Vector2[1] { cS10_Gravestone.boostTarget }, lockCamera: false));
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 14;
				return true;
			case 14:
				_003C_003E1__state = -1;
				cS10_Gravestone.EndCutscene(cS10_Gravestone.Level);
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
	private sealed class _003CStepForward_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Gravestone _003C_003E4__this;

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
		public _003CStepForward_003Ed__9(int _003C_003E1__state)
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
			CS10_Gravestone cS10_Gravestone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.player.DummyWalkTo(cS10_Gravestone.player.X + 8f);
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
	private sealed class _003CBadelineAppears_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Gravestone _003C_003E4__this;

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
		public _003CBadelineAppears_003Ed__10(int _003C_003E1__state)
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
			CS10_Gravestone cS10_Gravestone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				cS10_Gravestone.Level.Session.Inventory.Dashes = 1;
				cS10_Gravestone.player.Dashes = 1;
				Vector2 vector = cS10_Gravestone.player.Position + new Vector2(-12f, -10f);
				cS10_Gravestone.Level.Displacement.AddBurst(vector, 0.5f, 8f, 32f, 0.5f);
				cS10_Gravestone.Level.Add(cS10_Gravestone.badeline = new BadelineDummy(vector));
				Audio.Play("event:/char/badeline/maddy_split", vector);
				cS10_Gravestone.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = cS10_Gravestone.badeline.FloatTo(vector + new Vector2(0f, -6f), 1, faceDirection: false);
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
	private sealed class _003CSitDown_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Gravestone _003C_003E4__this;

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
		public _003CSitDown_003Ed__11(int _003C_003E1__state)
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
			CS10_Gravestone cS10_Gravestone = _003C_003E4__this;
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
				cS10_Gravestone.player.DummyAutoAnimate = false;
				cS10_Gravestone.player.Sprite.Play("sitDown");
				_003C_003E2__current = 0.3f;
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
	private sealed class _003CBirdStuff_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Gravestone _003C_003E4__this;

		private FMOD.Studio.EventInstance _003Cinstance_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private Vector2 _003Cto_003E5__4;

		private float _003Cpercent_003E5__5;

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
		public _003CBirdStuff_003Ed__12(int _003C_003E1__state)
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
			CS10_Gravestone cS10_Gravestone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_Gravestone.bird = new BirdNPC(cS10_Gravestone.player.Position + new Vector2(88f, -200f), BirdNPC.Modes.None);
				cS10_Gravestone.bird.DisableFlapSfx = true;
				cS10_Gravestone.Scene.Add(cS10_Gravestone.bird);
				_003Cinstance_003E5__2 = Audio.Play("event:/game/general/bird_in", cS10_Gravestone.bird.Position);
				cS10_Gravestone.bird.Facing = Facings.Left;
				cS10_Gravestone.bird.Sprite.Play("fall");
				_003Cfrom_003E5__3 = cS10_Gravestone.bird.Position;
				_003Cto_003E5__4 = cS10_Gravestone.gravestone.Position + new Vector2(1f, -16f);
				_003Cpercent_003E5__5 = 0f;
				goto IL_01aa;
			case 1:
				_003C_003E1__state = -1;
				goto IL_01aa;
			case 2:
				_003C_003E1__state = -1;
				cS10_Gravestone.bird.Sprite.Play("croak");
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/general/bird_squawk", cS10_Gravestone.bird.Position);
				_003C_003E2__current = 0.9f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/madeline/stand", cS10_Gravestone.player.Position);
				cS10_Gravestone.player.Sprite.Play("idle");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_Gravestone.bird.StartleAndFlyAway();
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_01aa:
				if (_003Cpercent_003E5__5 < 1f)
				{
					cS10_Gravestone.bird.Position = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.QuadOut(_003Cpercent_003E5__5);
					Audio.Position(_003Cinstance_003E5__2, cS10_Gravestone.bird.Position);
					if (_003Cpercent_003E5__5 > 0.5f)
					{
						cS10_Gravestone.bird.Sprite.Play("fly");
					}
					_003Cpercent_003E5__5 += Engine.DeltaTime * 0.5f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS10_Gravestone.bird.Position = _003Cto_003E5__4;
				cS10_Gravestone.bird.Sprite.Play("idle");
				_003C_003E2__current = 0.5f;
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
	private sealed class _003CBadelineRejoin_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_Gravestone _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

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
		public _003CBadelineRejoin_003Ed__13(int _003C_003E1__state)
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
			CS10_Gravestone cS10_Gravestone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/char/badeline/maddy_join_quick", cS10_Gravestone.badeline.Position);
				_003Cfrom_003E5__2 = cS10_Gravestone.badeline.Position;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / 0.25f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				cS10_Gravestone.badeline.Position = Vector2.Lerp(_003Cfrom_003E5__2, cS10_Gravestone.player.Position, Ease.CubeIn(_003Cp_003E5__3));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS10_Gravestone.Level.Displacement.AddBurst(cS10_Gravestone.player.Center, 0.5f, 8f, 32f, 0.5f);
			cS10_Gravestone.Level.Session.Inventory.Dashes = 2;
			cS10_Gravestone.player.Dashes = 2;
			cS10_Gravestone.badeline.RemoveSelf();
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

	private NPC10_Gravestone gravestone;

	private BadelineDummy badeline;

	private BirdNPC bird;

	private Vector2 boostTarget;

	private bool addedBooster;

	public CS10_Gravestone(Player player, NPC10_Gravestone gravestone, Vector2 boostTarget)
	{
		this.player = player;
		this.gravestone = gravestone;
		this.boostTarget = boostTarget;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene()));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene()
	{
		player.StateMachine.State = 11;
		player.ForceCameraUpdate = true;
		player.DummyGravity = false;
		player.Speed.Y = 0f;
		yield return 0.1f;
		yield return player.DummyWalkToExact((int)gravestone.X - 30);
		yield return 0.1f;
		player.Facing = Facings.Right;
		yield return 0.2f;
		yield return Level.ZoomTo(new Vector2(160f, 90f), 2f, 3f);
		player.ForceCameraUpdate = false;
		yield return 0.5f;
		yield return Textbox.Say("CH9_GRAVESTONE", StepForward, BadelineAppears, SitDown);
		yield return 1f;
		yield return BirdStuff();
		yield return BadelineRejoin();
		yield return 0.1f;
		yield return Level.ZoomBack(0.5f);
		yield return 0.3f;
		addedBooster = true;
		Level.Displacement.AddBurst(boostTarget, 0.5f, 8f, 32f, 0.5f);
		Audio.Play("event:/new_content/char/badeline/booster_first_appear", boostTarget);
		Level.Add(new BadelineBoost(new Vector2[1] { boostTarget }, lockCamera: false));
		yield return 0.2f;
		EndCutscene(Level);
	}

	[IteratorStateMachine(typeof(_003CStepForward_003Ed__9))]
	private IEnumerator StepForward()
	{
		yield return player.DummyWalkTo(player.X + 8f);
	}

	[IteratorStateMachine(typeof(_003CBadelineAppears_003Ed__10))]
	private IEnumerator BadelineAppears()
	{
		Level.Session.Inventory.Dashes = 1;
		player.Dashes = 1;
		Vector2 vector = player.Position + new Vector2(-12f, -10f);
		Level.Displacement.AddBurst(vector, 0.5f, 8f, 32f, 0.5f);
		Level.Add(badeline = new BadelineDummy(vector));
		Audio.Play("event:/char/badeline/maddy_split", vector);
		badeline.Sprite.Scale.X = 1f;
		yield return badeline.FloatTo(vector + new Vector2(0f, -6f), 1, faceDirection: false);
	}

	[IteratorStateMachine(typeof(_003CSitDown_003Ed__11))]
	private IEnumerator SitDown()
	{
		yield return 0.2f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("sitDown");
		yield return 0.3f;
	}

	[IteratorStateMachine(typeof(_003CBirdStuff_003Ed__12))]
	private IEnumerator BirdStuff()
	{
		bird = new BirdNPC(player.Position + new Vector2(88f, -200f), BirdNPC.Modes.None);
		bird.DisableFlapSfx = true;
		base.Scene.Add(bird);
		FMOD.Studio.EventInstance instance = Audio.Play("event:/game/general/bird_in", bird.Position);
		bird.Facing = Facings.Left;
		bird.Sprite.Play("fall");
		Vector2 from = bird.Position;
		Vector2 to = gravestone.Position + new Vector2(1f, -16f);
		float percent = 0f;
		while (percent < 1f)
		{
			bird.Position = from + (to - from) * Ease.QuadOut(percent);
			Audio.Position(instance, bird.Position);
			if (percent > 0.5f)
			{
				bird.Sprite.Play("fly");
			}
			percent += Engine.DeltaTime * 0.5f;
			yield return null;
		}
		bird.Position = to;
		bird.Sprite.Play("idle");
		yield return 0.5f;
		bird.Sprite.Play("croak");
		yield return 0.6f;
		Audio.Play("event:/game/general/bird_squawk", bird.Position);
		yield return 0.9f;
		Audio.Play("event:/char/madeline/stand", player.Position);
		player.Sprite.Play("idle");
		yield return 1f;
		yield return bird.StartleAndFlyAway();
	}

	[IteratorStateMachine(typeof(_003CBadelineRejoin_003Ed__13))]
	private IEnumerator BadelineRejoin()
	{
		Audio.Play("event:/new_content/char/badeline/maddy_join_quick", badeline.Position);
		Vector2 from = badeline.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.25f)
		{
			badeline.Position = Vector2.Lerp(from, player.Position, Ease.CubeIn(p));
			yield return null;
		}
		Level.Displacement.AddBurst(player.Center, 0.5f, 8f, 32f, 0.5f);
		Level.Session.Inventory.Dashes = 2;
		player.Dashes = 2;
		badeline.RemoveSelf();
	}

	public override void OnEnd(Level level)
	{
		player.Facing = Facings.Right;
		player.DummyAutoAnimate = true;
		player.DummyGravity = true;
		player.StateMachine.State = 0;
		Level.Session.Inventory.Dashes = 2;
		player.Dashes = 2;
		if (badeline != null)
		{
			badeline.RemoveSelf();
		}
		if (bird != null)
		{
			bird.RemoveSelf();
		}
		if (!addedBooster)
		{
			level.Add(new BadelineBoost(new Vector2[1] { boostTarget }, lockCamera: false));
		}
		level.ResetZoom();
	}
}
