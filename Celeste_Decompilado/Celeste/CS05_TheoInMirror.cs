using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_TheoInMirror : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_TheoInMirror _003C_003E4__this;

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
			CS05_TheoInMirror cS05_TheoInMirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_TheoInMirror.player.StateMachine.State = 11;
				cS05_TheoInMirror.player.StateMachine.Locked = true;
				_003C_003E2__current = cS05_TheoInMirror.player.DummyWalkTo(cS05_TheoInMirror.theo.X - 16f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS05_TheoInMirror.theo.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch5_theo_mirror");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS05_TheoInMirror.Add(new Coroutine(cS05_TheoInMirror.theo.MoveTo(cS05_TheoInMirror.theo.Position + new Vector2(64f, 0f))));
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_TheoInMirror.player.DummyWalkToExact(cS05_TheoInMirror.playerFinalX);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS05_TheoInMirror.EndCutscene(level);
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

	public const string Flag = "theoInMirror";

	private NPC theo;

	private Player player;

	private int playerFinalX;

	public CS05_TheoInMirror(NPC theo, Player player)
	{
		this.theo = theo;
		this.player = player;
		playerFinalX = (int)theo.Position.X + 24;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return player.DummyWalkTo(theo.X - 16f);
		yield return 0.5f;
		theo.Sprite.Scale.X = -1f;
		yield return 0.25f;
		yield return Textbox.Say("ch5_theo_mirror");
		Add(new Coroutine(theo.MoveTo(theo.Position + new Vector2(64f, 0f))));
		yield return 0.4f;
		yield return player.DummyWalkToExact(playerFinalX);
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		player.X = playerFinalX;
		player.MoveV(200f);
		player.Speed = Vector2.Zero;
		base.Scene.Remove(theo);
		level.Session.SetFlag("theoInMirror");
	}
}
