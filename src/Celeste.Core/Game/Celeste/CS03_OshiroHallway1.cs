using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_OshiroHallway1 : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public CS03_OshiroHallway1 _003C_003E4__this;

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
			CS03_OshiroHallway1 cS03_OshiroHallway = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				level.Session.Audio.Music.Layer(1, value: false);
				level.Session.Audio.Music.Layer(2, value: true);
				level.Session.Audio.Apply();
				cS03_OshiroHallway.player.StateMachine.State = 11;
				cS03_OshiroHallway.player.StateMachine.Locked = true;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_HALLWAY_A");
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_OshiroHallway.oshiro.MoveToAndRemove(new Vector2(cS03_OshiroHallway.SceneAs<Level>().Bounds.Right + 64, cS03_OshiroHallway.oshiro.Y));
				cS03_OshiroHallway.oshiro.Add(new SoundSource("event:/char/oshiro/move_02_03a_exit"));
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS03_OshiroHallway.EndCutscene(level);
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

	public const string Flag = "oshiro_resort_talked_2";

	private Player player;

	private NPC oshiro;

	public CS03_OshiroHallway1(Player player, NPC oshiro)
	{
		this.player = player;
		this.oshiro = oshiro;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__5))]
	private IEnumerator Cutscene(Level level)
	{
		level.Session.Audio.Music.Layer(1, value: false);
		level.Session.Audio.Music.Layer(2, value: true);
		level.Session.Audio.Apply();
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return Textbox.Say("CH3_OSHIRO_HALLWAY_A");
		oshiro.MoveToAndRemove(new Vector2(SceneAs<Level>().Bounds.Right + 64, oshiro.Y));
		oshiro.Add(new SoundSource("event:/char/oshiro/move_02_03a_exit"));
		yield return 1f;
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		level.Session.Audio.Music.Layer(1, value: true);
		level.Session.Audio.Music.Layer(2, value: false);
		level.Session.Audio.Apply();
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		level.Session.SetFlag("oshiro_resort_talked_2");
		if (WasSkipped)
		{
			level.Remove(oshiro);
		}
	}
}
