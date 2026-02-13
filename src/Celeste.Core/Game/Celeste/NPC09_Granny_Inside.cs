using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC09_Granny_Inside : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalkRoutine_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Player player;

		public NPC09_Granny_Inside _003C_003E4__this;

		private Vector2 _003CzoomPoint_003E5__2;

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
		public _003CTalkRoutine_003Ed__19(int _003C_003E1__state)
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
			NPC09_Granny_Inside nPC09_Granny_Inside = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				player.StateMachine.State = 11;
				player.Dashes = 1;
				player.ForceCameraUpdate = true;
				goto IL_00a0;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00a0;
			case 2:
				_003C_003E1__state = -1;
				player.Facing = Facings.Right;
				player.ForceCameraUpdate = false;
				_003CzoomPoint_003E5__2 = new Vector2(nPC09_Granny_Inside.X - 8f - nPC09_Granny_Inside.Level.Camera.X, 110f);
				if (nPC09_Granny_Inside.HasDoorConversation)
				{
					nPC09_Granny_Inside.Sprite.Scale.X = -1f;
					_003C_003E2__current = nPC09_Granny_Inside.Level.ZoomTo(_003CzoomPoint_003E5__2, 2f, 0.5f);
					_003C_003E1__state = 3;
					return true;
				}
				if (nPC09_Granny_Inside.conversation == 0)
				{
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 5;
					return true;
				}
				if (nPC09_Granny_Inside.conversation == 1)
				{
					nPC09_Granny_Inside.Sprite.Scale.X = -1f;
					_003C_003E2__current = nPC09_Granny_Inside.Level.ZoomTo(_003CzoomPoint_003E5__2, 2f, 0.5f);
					_003C_003E1__state = 9;
					return true;
				}
				if (nPC09_Granny_Inside.conversation == 2)
				{
					nPC09_Granny_Inside.Sprite.Scale.X = -1f;
					_003C_003E2__current = nPC09_Granny_Inside.Level.ZoomTo(_003CzoomPoint_003E5__2, 2f, 0.5f);
					_003C_003E1__state = 11;
					return true;
				}
				if (nPC09_Granny_Inside.conversation == 3)
				{
					nPC09_Granny_Inside.Sprite.Scale.X = -1f;
					_003C_003E2__current = nPC09_Granny_Inside.Level.ZoomTo(_003CzoomPoint_003E5__2, 2f, 0.5f);
					_003C_003E1__state = 13;
					return true;
				}
				goto IL_044a;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("APP_OLDLADY_LOCKED");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				goto IL_044a;
			case 5:
				_003C_003E1__state = -1;
				nPC09_Granny_Inside.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC09_Granny_Inside.Level.ZoomTo(_003CzoomPoint_003E5__2, 2f, 0.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("APP_OLDLADY_B", nPC09_Granny_Inside.StartLaughing, nPC09_Granny_Inside.StopLaughing);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				goto IL_044a;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("APP_OLDLADY_C", nPC09_Granny_Inside.StartLaughing, nPC09_Granny_Inside.StopLaughing);
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				goto IL_044a;
			case 11:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("APP_OLDLADY_D", nPC09_Granny_Inside.StartLaughing, nPC09_Granny_Inside.StopLaughing);
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				goto IL_044a;
			case 13:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("APP_OLDLADY_E", nPC09_Granny_Inside.StartLaughing, nPC09_Granny_Inside.StopLaughing);
				_003C_003E1__state = 14;
				return true;
			case 14:
				_003C_003E1__state = -1;
				goto IL_044a;
			case 15:
				{
					_003C_003E1__state = -1;
					nPC09_Granny_Inside.Level.EndCutscene();
					nPC09_Granny_Inside.EndTalking(nPC09_Granny_Inside.Level);
					return false;
				}
				IL_00a0:
				if (!player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = player.DummyWalkToExact((int)nPC09_Granny_Inside.X - 16);
				_003C_003E1__state = 2;
				return true;
				IL_044a:
				nPC09_Granny_Inside.talker.Enabled = nPC09_Granny_Inside.talkerEnabled;
				_003C_003E2__current = nPC09_Granny_Inside.Level.ZoomBack(0.5f);
				_003C_003E1__state = 15;
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
	private sealed class _003CStartLaughing_003Ed__20 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC09_Granny_Inside _003C_003E4__this;

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
		public _003CStartLaughing_003Ed__20(int _003C_003E1__state)
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
			NPC09_Granny_Inside nPC09_Granny_Inside = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC09_Granny_Inside.Sprite.Play("laugh");
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
	private sealed class _003CStopLaughing_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC09_Granny_Inside _003C_003E4__this;

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
		public _003CStopLaughing_003Ed__21(int _003C_003E1__state)
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
			NPC09_Granny_Inside nPC09_Granny_Inside = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC09_Granny_Inside.Sprite.Play("idle");
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

	public const string DoorConversationAvailable = "granny_door";

	private const string DoorConversationDone = "granny_door_done";

	private const string CounterFlag = "granny";

	private int conversation;

	private const int MaxConversation = 4;

	public Hahaha Hahaha;

	public GrannyLaughSfx LaughSfx;

	private Player player;

	private TalkComponent talker;

	private bool talking;

	private Coroutine talkRoutine;

	private bool HasDoorConversation
	{
		get
		{
			if (Level.Session.GetFlag("granny_door"))
			{
				return !Level.Session.GetFlag("granny_door_done");
			}
			return false;
		}
	}

	private bool talkerEnabled
	{
		get
		{
			if (conversation <= 0 || conversation >= 4)
			{
				return HasDoorConversation;
			}
			return true;
		}
	}

	public NPC09_Granny_Inside(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(Sprite = GFX.SpriteBank.Create("granny"));
		Sprite.Play("idle");
		Add(LaughSfx = new GrannyLaughSfx(Sprite));
		MoveAnim = "walk";
		Maxspeed = 40f;
		Add(talker = new TalkComponent(new Rectangle(-20, -8, 40, 8), new Vector2(0f, -24f), OnTalk));
		talker.Enabled = false;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		conversation = Level.Session.GetCounter("granny");
		scene.Add(Hahaha = new Hahaha(Position + new Vector2(8f, -4f)));
		Hahaha.Enabled = false;
	}

	public override void Update()
	{
		if (!talking && conversation == 0)
		{
			player = Level.Tracker.GetEntity<Player>();
			if (player != null && Math.Abs(player.X - base.X) < 48f)
			{
				OnTalk(player);
			}
		}
		talker.Enabled = talkerEnabled;
		Hahaha.Enabled = Sprite.CurrentAnimationID == "laugh";
		base.Update();
	}

	private void OnTalk(Player player)
	{
		this.player = player;
		(base.Scene as Level).StartCutscene(EndTalking);
		Add(talkRoutine = new Coroutine(TalkRoutine(player)));
		talking = true;
	}

	[IteratorStateMachine(typeof(_003CTalkRoutine_003Ed__19))]
	private IEnumerator TalkRoutine(Player player)
	{
		player.StateMachine.State = 11;
		player.Dashes = 1;
		player.ForceCameraUpdate = true;
		while (!player.OnGround())
		{
			yield return null;
		}
		yield return player.DummyWalkToExact((int)base.X - 16);
		player.Facing = Facings.Right;
		player.ForceCameraUpdate = false;
		Vector2 zoomPoint = new Vector2(base.X - 8f - Level.Camera.X, 110f);
		if (HasDoorConversation)
		{
			Sprite.Scale.X = -1f;
			yield return Level.ZoomTo(zoomPoint, 2f, 0.5f);
			yield return Textbox.Say("APP_OLDLADY_LOCKED");
		}
		else if (conversation == 0)
		{
			yield return 0.5f;
			Sprite.Scale.X = -1f;
			yield return 0.25f;
			yield return Level.ZoomTo(zoomPoint, 2f, 0.5f);
			yield return Textbox.Say("APP_OLDLADY_B", StartLaughing, StopLaughing);
		}
		else if (conversation == 1)
		{
			Sprite.Scale.X = -1f;
			yield return Level.ZoomTo(zoomPoint, 2f, 0.5f);
			yield return Textbox.Say("APP_OLDLADY_C", StartLaughing, StopLaughing);
		}
		else if (conversation == 2)
		{
			Sprite.Scale.X = -1f;
			yield return Level.ZoomTo(zoomPoint, 2f, 0.5f);
			yield return Textbox.Say("APP_OLDLADY_D", StartLaughing, StopLaughing);
		}
		else if (conversation == 3)
		{
			Sprite.Scale.X = -1f;
			yield return Level.ZoomTo(zoomPoint, 2f, 0.5f);
			yield return Textbox.Say("APP_OLDLADY_E", StartLaughing, StopLaughing);
		}
		talker.Enabled = talkerEnabled;
		yield return Level.ZoomBack(0.5f);
		Level.EndCutscene();
		EndTalking(Level);
	}

	[IteratorStateMachine(typeof(_003CStartLaughing_003Ed__20))]
	private IEnumerator StartLaughing()
	{
		Sprite.Play("laugh");
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CStopLaughing_003Ed__21))]
	private IEnumerator StopLaughing()
	{
		Sprite.Play("idle");
		yield return null;
	}

	private void EndTalking(Level level)
	{
		if (player != null)
		{
			player.StateMachine.State = 0;
			player.ForceCameraUpdate = false;
		}
		if (HasDoorConversation)
		{
			Level.Session.SetFlag("granny_door_done");
		}
		else
		{
			Level.Session.IncrementCounter("granny");
			conversation++;
		}
		if (talkRoutine != null)
		{
			talkRoutine.RemoveSelf();
			talkRoutine = null;
		}
		Sprite.Play("idle");
		talking = false;
	}
}
