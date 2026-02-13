using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS08_EnterDoor : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS08_EnterDoor _003C_003E4__this;

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
			CS08_EnterDoor cS08_EnterDoor = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS08_EnterDoor.player.StateMachine.State = 11;
				cS08_EnterDoor.Add(new Coroutine(cS08_EnterDoor.player.DummyWalkToExact((int)cS08_EnterDoor.targetX, walkBackwards: false, 0.7f)));
				cS08_EnterDoor.Add(new Coroutine(level.ZoomTo(new Vector2(cS08_EnterDoor.targetX - level.Camera.X, 90f), 2f, 2f)));
				_003C_003E2__current = new FadeWipe(level, wipeIn: false)
				{
					Duration = 2f
				}.Wait();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS08_EnterDoor.EndCutscene(level);
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

	private float targetX;

	public CS08_EnterDoor(Player player, float targetX)
	{
		this.player = player;
		this.targetX = targetX;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__4))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		Add(new Coroutine(player.DummyWalkToExact((int)targetX, walkBackwards: false, 0.7f)));
		Add(new Coroutine(level.ZoomTo(new Vector2(targetX - level.Camera.X, 90f), 2f, 2f)));
		FadeWipe fadeWipe = new FadeWipe(level, wipeIn: false);
		fadeWipe.Duration = 2f;
		yield return fadeWipe.Wait();
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		level.OnEndOfFrame += delegate
		{
			level.Remove(player);
			level.UnloadLevel();
			level.Session.Level = "inside";
			level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));
			level.LoadLevel(Player.IntroTypes.None);
			level.Add(new CS08_Ending());
		};
	}
}
