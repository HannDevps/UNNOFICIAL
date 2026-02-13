using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_FinalRoom : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_FinalRoom _003C_003E4__this;

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
			CS10_FinalRoom cS10_FinalRoom = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_FinalRoom.player.StateMachine.State = 11;
				if (cS10_FinalRoom.first)
				{
					_003C_003E2__current = cS10_FinalRoom.player.DummyWalkToExact((int)(cS10_FinalRoom.player.X + 16f));
					_003C_003E1__state = 1;
					return true;
				}
				cS10_FinalRoom.player.DummyAutoAnimate = false;
				cS10_FinalRoom.player.Sprite.Play("sitDown");
				cS10_FinalRoom.player.Sprite.SetAnimationFrame(cS10_FinalRoom.player.Sprite.CurrentAnimationTotalFrames - 1);
				_003C_003E2__current = 1.25f;
				_003C_003E1__state = 3;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0118;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0118;
			case 4:
				_003C_003E1__state = -1;
				if (cS10_FinalRoom.first)
				{
					_003C_003E2__current = Textbox.Say("CH9_LAST_ROOM");
					_003C_003E1__state = 5;
					return true;
				}
				_003C_003E2__current = Textbox.Say("CH9_LAST_ROOM_ALT");
				_003C_003E1__state = 6;
				return true;
			case 5:
				_003C_003E1__state = -1;
				goto IL_018a;
			case 6:
				_003C_003E1__state = -1;
				goto IL_018a;
			case 7:
				{
					_003C_003E1__state = -1;
					cS10_FinalRoom.EndCutscene(level);
					return false;
				}
				IL_018a:
				_003C_003E2__current = cS10_FinalRoom.BadelineVanishes();
				_003C_003E1__state = 7;
				return true;
				IL_0118:
				_003C_003E2__current = cS10_FinalRoom.BadelineAppears();
				_003C_003E1__state = 4;
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
	private sealed class _003CBadelineAppears_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_FinalRoom _003C_003E4__this;

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
		public _003CBadelineAppears_003Ed__6(int _003C_003E1__state)
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
			CS10_FinalRoom cS10_FinalRoom = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_FinalRoom.Level.Add(cS10_FinalRoom.badeline = new BadelineDummy(cS10_FinalRoom.player.Position + new Vector2(18f, -8f)));
				cS10_FinalRoom.Level.Displacement.AddBurst(cS10_FinalRoom.badeline.Center, 0.5f, 8f, 32f, 0.5f);
				Audio.Play("event:/char/badeline/maddy_split", cS10_FinalRoom.badeline.Position);
				cS10_FinalRoom.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = null;
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
	private sealed class _003CBadelineVanishes_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_FinalRoom _003C_003E4__this;

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
		public _003CBadelineVanishes_003Ed__7(int _003C_003E1__state)
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
			CS10_FinalRoom cS10_FinalRoom = _003C_003E4__this;
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
				cS10_FinalRoom.badeline.Vanish();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				cS10_FinalRoom.badeline = null;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_FinalRoom.player.Facing = Facings.Right;
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

	private bool first;

	public CS10_FinalRoom(Player player, bool first)
	{
		base.Depth = -8500;
		this.player = player;
		this.first = first;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__5))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		if (first)
		{
			yield return player.DummyWalkToExact((int)(player.X + 16f));
			yield return 0.5f;
		}
		else
		{
			player.DummyAutoAnimate = false;
			player.Sprite.Play("sitDown");
			player.Sprite.SetAnimationFrame(player.Sprite.CurrentAnimationTotalFrames - 1);
			yield return 1.25f;
		}
		yield return BadelineAppears();
		if (first)
		{
			yield return Textbox.Say("CH9_LAST_ROOM");
		}
		else
		{
			yield return Textbox.Say("CH9_LAST_ROOM_ALT");
		}
		yield return BadelineVanishes();
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CBadelineAppears_003Ed__6))]
	private IEnumerator BadelineAppears()
	{
		Level.Add(badeline = new BadelineDummy(player.Position + new Vector2(18f, -8f)));
		Level.Displacement.AddBurst(badeline.Center, 0.5f, 8f, 32f, 0.5f);
		Audio.Play("event:/char/badeline/maddy_split", badeline.Position);
		badeline.Sprite.Scale.X = -1f;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CBadelineVanishes_003Ed__7))]
	private IEnumerator BadelineVanishes()
	{
		yield return 0.2f;
		badeline.Vanish();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		badeline = null;
		yield return 0.5f;
		player.Facing = Facings.Right;
	}

	public override void OnEnd(Level level)
	{
		Level.Session.Inventory.Dashes = 1;
		player.StateMachine.State = 0;
		if (!first && !WasSkipped)
		{
			Audio.Play("event:/char/madeline/stand", player.Position);
		}
		if (badeline != null)
		{
			badeline.RemoveSelf();
		}
	}
}
