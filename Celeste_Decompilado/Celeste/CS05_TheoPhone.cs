using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_TheoPhone : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_TheoPhone _003C_003E4__this;

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
		public _003CRoutine_003Ed__4(int _003C_003E1__state)
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
			CS05_TheoPhone cS05_TheoPhone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_TheoPhone.player.StateMachine.State = 11;
				if (cS05_TheoPhone.player.X != cS05_TheoPhone.targetX)
				{
					cS05_TheoPhone.player.Facing = (Facings)Math.Sign(cS05_TheoPhone.targetX - cS05_TheoPhone.player.X);
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_TheoPhone.Level.ZoomTo(new Vector2(80f, 60f), 2f, 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH5_PHONE", cS05_TheoPhone.WalkToPhone, cS05_TheoPhone.StandBackUp);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_TheoPhone.Level.ZoomBack(0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS05_TheoPhone.EndCutscene(cS05_TheoPhone.Level);
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
	private sealed class _003CWalkToPhone_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_TheoPhone _003C_003E4__this;

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
		public _003CWalkToPhone_003Ed__5(int _003C_003E1__state)
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
			CS05_TheoPhone cS05_TheoPhone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_TheoPhone.player.DummyWalkToExact((int)cS05_TheoPhone.targetX);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS05_TheoPhone.player.Facing = Facings.Left;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS05_TheoPhone.player.DummyAutoAnimate = false;
				cS05_TheoPhone.player.Sprite.Play("duck");
				_003C_003E2__current = 0.5f;
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
	private sealed class _003CStandBackUp_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_TheoPhone _003C_003E4__this;

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
		public _003CStandBackUp_003Ed__6(int _003C_003E1__state)
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
			CS05_TheoPhone cS05_TheoPhone = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_TheoPhone.RemovePhone();
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS05_TheoPhone.player.Sprite.Play("idle");
				_003C_003E2__current = 0.2f;
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

	private Player player;

	private float targetX;

	public CS05_TheoPhone(Player player, float targetX)
	{
		this.player = player;
		this.targetX = targetX;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Routine()));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__4))]
	private IEnumerator Routine()
	{
		player.StateMachine.State = 11;
		if (player.X != targetX)
		{
			player.Facing = (Facings)Math.Sign(targetX - player.X);
		}
		yield return 0.5f;
		yield return Level.ZoomTo(new Vector2(80f, 60f), 2f, 0.5f);
		yield return Textbox.Say("CH5_PHONE", WalkToPhone, StandBackUp);
		yield return Level.ZoomBack(0.5f);
		EndCutscene(Level);
	}

	[IteratorStateMachine(typeof(_003CWalkToPhone_003Ed__5))]
	private IEnumerator WalkToPhone()
	{
		yield return 0.25f;
		yield return player.DummyWalkToExact((int)targetX);
		player.Facing = Facings.Left;
		yield return 0.5f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("duck");
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CStandBackUp_003Ed__6))]
	private IEnumerator StandBackUp()
	{
		RemovePhone();
		yield return 0.6f;
		player.Sprite.Play("idle");
		yield return 0.2f;
	}

	public override void OnEnd(Level level)
	{
		RemovePhone();
		player.StateMachine.State = 0;
	}

	private void RemovePhone()
	{
		base.Scene.Entities.FindFirst<TheoPhone>()?.RemoveSelf();
	}
}
