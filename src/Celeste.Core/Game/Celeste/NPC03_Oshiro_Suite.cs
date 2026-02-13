using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC03_Oshiro_Suite : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalk_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Oshiro_Suite _003C_003E4__this;

		public Player player;

		private int _003Cconversation_003E5__2;

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
		public _003CTalk_003Ed__5(int _003C_003E1__state)
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
			NPC03_Oshiro_Suite nPC03_Oshiro_Suite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cconversation_003E5__2 = nPC03_Oshiro_Suite.Session.GetCounter("oshiroSuiteSadConversation");
				_003C_003E2__current = nPC03_Oshiro_Suite.PlayerApproach(player, turnToFace: false, 12f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_SUITE_SAD" + _003Cconversation_003E5__2);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Oshiro_Suite.PlayerLeave(player);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				nPC03_Oshiro_Suite.EndTalking(nPC03_Oshiro_Suite.SceneAs<Level>());
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

	private const string ConversationCounter = "oshiroSuiteSadConversation";

	private bool finishedTalking;

	public NPC03_Oshiro_Suite(Vector2 position)
		: base(position)
	{
		Add(Sprite = new OshiroSprite(1));
		Add(Light = new VertexLight(-Vector2.UnitY * 16f, Color.White, 1f, 32, 64));
		Add(Talker = new TalkComponent(new Rectangle(-16, -8, 32, 8), new Vector2(0f, -24f), OnTalk));
		Talker.Enabled = false;
		MoveAnim = "move";
		IdleAnim = "idle";
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (!base.Session.GetFlag("oshiro_resort_suite"))
		{
			base.Scene.Add(new CS03_OshiroMasterSuite(this));
			return;
		}
		Sprite.Play("idle_ground");
		Talker.Enabled = true;
	}

	private void OnTalk(Player player)
	{
		finishedTalking = false;
		Level.StartCutscene(EndTalking);
		Add(new Coroutine(Talk(player)));
	}

	[IteratorStateMachine(typeof(_003CTalk_003Ed__5))]
	private IEnumerator Talk(Player player)
	{
		int conversation = base.Session.GetCounter("oshiroSuiteSadConversation");
		yield return PlayerApproach(player, turnToFace: false, 12f);
		yield return Textbox.Say("CH3_OSHIRO_SUITE_SAD" + conversation);
		yield return PlayerLeave(player);
		EndTalking(SceneAs<Level>());
	}

	private void EndTalking(Level level)
	{
		Player player = base.Scene.Entities.FindFirst<Player>();
		if (player != null)
		{
			player.StateMachine.Locked = false;
			player.StateMachine.State = 0;
		}
		if (!finishedTalking)
		{
			int counter = base.Session.GetCounter("oshiroSuiteSadConversation");
			counter++;
			counter %= 7;
			if (counter == 0)
			{
				counter++;
			}
			base.Session.SetCounter("oshiroSuiteSadConversation", counter);
			finishedTalking = true;
		}
	}
}
