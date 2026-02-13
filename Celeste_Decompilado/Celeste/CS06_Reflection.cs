using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_Reflection : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Reflection _003C_003E4__this;

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
			CS06_Reflection cS06_Reflection = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_Reflection.player.StateMachine.State = 11;
				cS06_Reflection.player.StateMachine.Locked = true;
				cS06_Reflection.player.ForceCameraUpdate = true;
				_003C_003E2__current = cS06_Reflection.player.DummyWalkToExact((int)cS06_Reflection.targetX);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS06_Reflection.player.Facing = Facings.Right;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_Reflection.Level.ZoomTo(new Vector2(200f, 90f), 2f, 1f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH6_REFLECT_AFTER");
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_Reflection.Level.ZoomBack(0.5f);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS06_Reflection.EndCutscene(level);
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

	public const string Flag = "reflection";

	private Player player;

	private float targetX;

	public CS06_Reflection(Player player, float targetX)
	{
		this.player = player;
		this.targetX = targetX;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__5))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.ForceCameraUpdate = true;
		yield return player.DummyWalkToExact((int)targetX);
		yield return 0.1f;
		player.Facing = Facings.Right;
		yield return 0.1f;
		yield return Level.ZoomTo(new Vector2(200f, 90f), 2f, 1f);
		yield return Textbox.Say("CH6_REFLECT_AFTER");
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		player.ForceCameraUpdate = false;
		player.FlipInReflection = false;
		level.Session.SetFlag("reflection");
	}
}
