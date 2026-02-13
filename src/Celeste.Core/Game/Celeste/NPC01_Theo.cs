using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC01_Theo : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalk_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC01_Theo _003C_003E4__this;

		public Player player;

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
		public _003CTalk_003Ed__7(int _003C_003E1__state)
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
			NPC01_Theo nPC01_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (nPC01_Theo.currentConversation == 0)
				{
					_003C_003E2__current = nPC01_Theo.PlayerApproachRightSide(player);
					_003C_003E1__state = 1;
					return true;
				}
				if (nPC01_Theo.currentConversation == 1)
				{
					_003C_003E2__current = nPC01_Theo.PlayerApproachRightSide(player);
					_003C_003E1__state = 3;
					return true;
				}
				if (nPC01_Theo.currentConversation == 2)
				{
					_003C_003E2__current = nPC01_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 7;
					return true;
				}
				if (nPC01_Theo.currentConversation == 3)
				{
					_003C_003E2__current = nPC01_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 9;
					return true;
				}
				if (nPC01_Theo.currentConversation == 4)
				{
					_003C_003E2__current = nPC01_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 11;
					return true;
				}
				if (nPC01_Theo.currentConversation == 5)
				{
					_003C_003E2__current = nPC01_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 13;
					return true;
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_THEO_A", nPC01_Theo.PlayerApproach48px);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				break;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC01_Theo.PlayerApproach(player, turnToFace: true, 48f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_THEO_B");
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				break;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_THEO_C");
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				break;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_THEO_D");
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				break;
			case 11:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_THEO_E");
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				break;
			case 13:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_THEO_F", nPC01_Theo.Yolo);
				_003C_003E1__state = 14;
				return true;
			case 14:
				_003C_003E1__state = -1;
				nPC01_Theo.Sprite.Play("yoloEnd");
				nPC01_Theo.Remove(nPC01_Theo.Talker);
				_003C_003E2__current = nPC01_Theo.Level.ZoomBack(0.5f);
				_003C_003E1__state = 15;
				return true;
			case 15:
				_003C_003E1__state = -1;
				break;
			}
			nPC01_Theo.Level.EndCutscene();
			nPC01_Theo.OnTalkEnd(nPC01_Theo.Level);
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

	[CompilerGenerated]
	private sealed class _003CYolo_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC01_Theo _003C_003E4__this;

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
		public _003CYolo_003Ed__9(int _003C_003E1__state)
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
			NPC01_Theo nPC01_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC01_Theo.Level.ZoomTo(new Vector2(128f, 128f), 2f, 0.5f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/theo/yolo_fist", nPC01_Theo.Position);
				nPC01_Theo.Sprite.Play("yolo");
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				nPC01_Theo.Level.DirectionalShake(-Vector2.UnitY);
				nPC01_Theo.Level.ParticlesFG.Emit(P_YOLO, 6, nPC01_Theo.Position + new Vector2(-3f, -24f), Vector2.One * 4f);
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

	public static ParticleType P_YOLO;

	private const string DoneTalking = "theoDoneTalking";

	private int currentConversation;

	private Coroutine talkRoutine;

	public NPC01_Theo(Vector2 position)
		: base(position)
	{
		Add(Sprite = GFX.SpriteBank.Create("theo"));
		Sprite.Play("idle");
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		currentConversation = base.Session.GetCounter("theo");
		if (!base.Session.GetFlag("theoDoneTalking"))
		{
			Add(Talker = new TalkComponent(new Rectangle(-8, -8, 88, 8), new Vector2(0f, -24f), OnTalk));
		}
	}

	private void OnTalk(Player player)
	{
		Level.StartCutscene(OnTalkEnd);
		Add(talkRoutine = new Coroutine(Talk(player)));
	}

	[IteratorStateMachine(typeof(_003CTalk_003Ed__7))]
	private IEnumerator Talk(Player player)
	{
		if (currentConversation == 0)
		{
			yield return PlayerApproachRightSide(player);
			yield return Textbox.Say("CH1_THEO_A", base.PlayerApproach48px);
		}
		else if (currentConversation == 1)
		{
			yield return PlayerApproachRightSide(player);
			yield return 0.2f;
			yield return PlayerApproach(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH1_THEO_B");
		}
		else if (currentConversation == 2)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH1_THEO_C");
		}
		else if (currentConversation == 3)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH1_THEO_D");
		}
		else if (currentConversation == 4)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH1_THEO_E");
		}
		else if (currentConversation == 5)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH1_THEO_F", Yolo);
			Sprite.Play("yoloEnd");
			Remove(Talker);
			yield return Level.ZoomBack(0.5f);
		}
		Level.EndCutscene();
		OnTalkEnd(Level);
	}

	private void OnTalkEnd(Level level)
	{
		if (currentConversation == 0)
		{
			SaveData.Instance.SetFlag("MetTheo");
		}
		else if (currentConversation == 1)
		{
			SaveData.Instance.SetFlag("TheoKnowsName");
		}
		else if (currentConversation == 5)
		{
			base.Session.SetFlag("theoDoneTalking");
			Remove(Talker);
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.StateMachine.Locked = false;
			entity.StateMachine.State = 0;
		}
		base.Session.IncrementCounter("theo");
		currentConversation++;
		talkRoutine.Cancel();
		talkRoutine.RemoveSelf();
		Sprite.Play("idle");
	}

	[IteratorStateMachine(typeof(_003CYolo_003Ed__9))]
	private IEnumerator Yolo()
	{
		yield return Level.ZoomTo(new Vector2(128f, 128f), 2f, 0.5f);
		yield return 0.2f;
		Audio.Play("event:/char/theo/yolo_fist", Position);
		Sprite.Play("yolo");
		yield return 0.1f;
		Level.DirectionalShake(-Vector2.UnitY);
		Level.ParticlesFG.Emit(P_YOLO, 6, Position + new Vector2(-3f, -24f), Vector2.One * 4f);
		yield return 0.5f;
	}
}
