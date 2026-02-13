using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_BossMid : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_BossMid _003C_003E4__this;

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
		public _003CCutscene_003Ed__4(int _003C_003E1__state)
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
			CS06_BossMid cS06_BossMid = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_006c;
			case 1:
				_003C_003E1__state = -1;
				goto IL_006c;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00b0;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomTo(new Vector2(80f, 110f), 2f, 0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch6_boss_middle");
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomBack(0.4f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				{
					_003C_003E1__state = -1;
					cS06_BossMid.EndCutscene(level);
					return false;
				}
				IL_006c:
				if (cS06_BossMid.player == null)
				{
					cS06_BossMid.player = cS06_BossMid.Scene.Tracker.GetEntity<Player>();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS06_BossMid.player.StateMachine.State = 11;
				cS06_BossMid.player.StateMachine.Locked = true;
				goto IL_00b0;
				IL_00b0:
				if (!cS06_BossMid.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = cS06_BossMid.player.DummyWalkToExact((int)cS06_BossMid.player.X + 20);
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

	public const string Flag = "boss_mid";

	private Player player;

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__4))]
	private IEnumerator Cutscene(Level level)
	{
		while (player == null)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			yield return null;
		}
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		while (!player.OnGround())
		{
			yield return null;
		}
		yield return player.DummyWalkToExact((int)player.X + 20);
		yield return level.ZoomTo(new Vector2(80f, 110f), 2f, 0.5f);
		yield return Textbox.Say("ch6_boss_middle");
		yield return 0.1f;
		yield return level.ZoomBack(0.4f);
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped && player != null)
		{
			while (!player.OnGround() && player.Y < (float)level.Bounds.Bottom)
			{
				player.Y++;
			}
		}
		if (player != null)
		{
			player.StateMachine.Locked = false;
			player.StateMachine.State = 0;
		}
		level.Entities.FindFirst<FinalBoss>()?.OnPlayer(null);
		level.Session.SetFlag("boss_mid");
	}
}
