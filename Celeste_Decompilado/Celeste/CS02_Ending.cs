using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Monocle;

namespace Celeste;

public class CS02_Ending : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_Ending _003C_003E4__this;

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
			CS02_Ending cS02_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS02_Ending.player.StateMachine.State = 11;
				cS02_Ending.player.Dashes = 1;
				goto IL_00a2;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00a2;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS02_Ending.player.DummyWalkTo(cS02_Ending.payphone.X - 4f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS02_Ending.player.Facing = Facings.Right;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS02_Ending.player.Visible = false;
				Audio.Play("event:/game/02_old_site/sequence_phone_pickup", cS02_Ending.player.Position);
				_003C_003E2__current = cS02_Ending.payphone.Sprite.PlayRoutine("pickUp");
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				cS02_Ending.phoneSfx.Position = cS02_Ending.player.Position;
				cS02_Ending.phoneSfx.Play("event:/game/02_old_site/sequence_phone_ringtone_loop");
				_003C_003E2__current = 6f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS02_Ending.phoneSfx.Stop();
				cS02_Ending.payphone.Sprite.Play("talkPhone");
				_003C_003E2__current = Textbox.Say("CH2_END_PHONECALL");
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				{
					_003C_003E1__state = -1;
					cS02_Ending.EndCutscene(level);
					return false;
				}
				IL_00a2:
				if (cS02_Ending.player.Light.Alpha > 0f)
				{
					cS02_Ending.player.Light.Alpha -= Engine.DeltaTime * 1.25f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = 1f;
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

	private Player player;

	private Payphone payphone;

	private SoundSource phoneSfx;

	public CS02_Ending(Player player)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.player = player;
		Add(phoneSfx = new SoundSource());
	}

	public override void OnBegin(Level level)
	{
		level.RegisterAreaComplete();
		payphone = base.Scene.Tracker.GetEntity<Payphone>();
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__5))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.Dashes = 1;
		while (player.Light.Alpha > 0f)
		{
			player.Light.Alpha -= Engine.DeltaTime * 1.25f;
			yield return null;
		}
		yield return 1f;
		yield return player.DummyWalkTo(payphone.X - 4f);
		yield return 0.2f;
		player.Facing = Facings.Right;
		yield return 0.5f;
		player.Visible = false;
		Audio.Play("event:/game/02_old_site/sequence_phone_pickup", player.Position);
		yield return payphone.Sprite.PlayRoutine("pickUp");
		yield return 0.25f;
		phoneSfx.Position = player.Position;
		phoneSfx.Play("event:/game/02_old_site/sequence_phone_ringtone_loop");
		yield return 6f;
		phoneSfx.Stop();
		payphone.Sprite.Play("talkPhone");
		yield return Textbox.Say("CH2_END_PHONECALL");
		yield return 0.3f;
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		level.CompleteArea();
	}
}
