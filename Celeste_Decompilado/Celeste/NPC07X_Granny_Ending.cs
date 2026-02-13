using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC07X_Granny_Ending : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalkRoutine_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Player player;

		public NPC07X_Granny_Ending _003C_003E4__this;

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
		public _003CTalkRoutine_003Ed__11(int _003C_003E1__state)
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
			NPC07X_Granny_Ending nPC07X_Granny_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				player.StateMachine.State = 11;
				player.ForceCameraUpdate = true;
				goto IL_0088;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0088;
			case 2:
				_003C_003E1__state = -1;
				player.Facing = Facings.Right;
				if (nPC07X_Granny_Ending.ch9EasterEgg)
				{
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 3;
					return true;
				}
				if (nPC07X_Granny_Ending.conversation == 0)
				{
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 6;
					return true;
				}
				if (nPC07X_Granny_Ending.conversation == 1)
				{
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 9;
					return true;
				}
				goto IL_0350;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC07X_Granny_Ending.Level.ZoomTo(nPC07X_Granny_Ending.Position - nPC07X_Granny_Ending.Level.Camera.Position + new Vector2(0f, -32f), 2f, 0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Dialog.Language.Dialog["CH10_GRANNY_EASTEREGG"] = "{portrait GRANNY right mock} I see you have discovered Debug Mode.";
				_003C_003E2__current = Textbox.Say("CH10_GRANNY_EASTEREGG");
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				nPC07X_Granny_Ending.talker.Enabled = false;
				goto IL_0350;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC07X_Granny_Ending.Level.ZoomTo(nPC07X_Granny_Ending.Position - nPC07X_Granny_Ending.Level.Camera.Position + new Vector2(0f, -32f), 2f, 0.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH7_CSIDE_OLDLADY", nPC07X_Granny_Ending.StartLaughing, nPC07X_Granny_Ending.StopLaughing);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				goto IL_0350;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC07X_Granny_Ending.Level.ZoomTo(nPC07X_Granny_Ending.Position - nPC07X_Granny_Ending.Level.Camera.Position + new Vector2(0f, -32f), 2f, 0.5f);
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH7_CSIDE_OLDLADY_B", nPC07X_Granny_Ending.StartLaughing, nPC07X_Granny_Ending.StopLaughing);
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				nPC07X_Granny_Ending.talker.Enabled = false;
				goto IL_0350;
			case 12:
				{
					_003C_003E1__state = -1;
					nPC07X_Granny_Ending.Level.EndCutscene();
					nPC07X_Granny_Ending.EndTalking(nPC07X_Granny_Ending.Level);
					return false;
				}
				IL_0350:
				_003C_003E2__current = nPC07X_Granny_Ending.Level.ZoomBack(0.5f);
				_003C_003E1__state = 12;
				return true;
				IL_0088:
				if (!player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = player.DummyWalkToExact((int)nPC07X_Granny_Ending.X - 16);
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

	[CompilerGenerated]
	private sealed class _003CStartLaughing_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC07X_Granny_Ending _003C_003E4__this;

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
		public _003CStartLaughing_003Ed__12(int _003C_003E1__state)
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
			NPC07X_Granny_Ending nPC07X_Granny_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC07X_Granny_Ending.Sprite.Play("laugh");
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
	private sealed class _003CStopLaughing_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC07X_Granny_Ending _003C_003E4__this;

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
		public _003CStopLaughing_003Ed__13(int _003C_003E1__state)
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
			NPC07X_Granny_Ending nPC07X_Granny_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC07X_Granny_Ending.Sprite.Play("idle");
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

	public Hahaha Hahaha;

	public GrannyLaughSfx LaughSfx;

	private Player player;

	private TalkComponent talker;

	private Coroutine talkRoutine;

	private int conversation;

	private bool ch9EasterEgg;

	public NPC07X_Granny_Ending(EntityData data, Vector2 offset, bool ch9EasterEgg = false)
		: base(data.Position + offset)
	{
		Add(Sprite = GFX.SpriteBank.Create("granny"));
		Sprite.Play("idle");
		Sprite.Scale.X = -1f;
		Add(LaughSfx = new GrannyLaughSfx(Sprite));
		Add(talker = new TalkComponent(new Rectangle(-20, -8, 40, 8), new Vector2(0f, -24f), OnTalk));
		MoveAnim = "walk";
		Maxspeed = 40f;
		this.ch9EasterEgg = ch9EasterEgg;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		scene.Add(Hahaha = new Hahaha(Position + new Vector2(8f, -4f)));
		Hahaha.Enabled = false;
	}

	public override void Update()
	{
		Hahaha.Enabled = Sprite.CurrentAnimationID == "laugh";
		base.Update();
	}

	private void OnTalk(Player player)
	{
		this.player = player;
		(base.Scene as Level).StartCutscene(EndTalking);
		Add(talkRoutine = new Coroutine(TalkRoutine(player)));
	}

	[IteratorStateMachine(typeof(_003CTalkRoutine_003Ed__11))]
	private IEnumerator TalkRoutine(Player player)
	{
		player.StateMachine.State = 11;
		player.ForceCameraUpdate = true;
		while (!player.OnGround())
		{
			yield return null;
		}
		yield return player.DummyWalkToExact((int)base.X - 16);
		player.Facing = Facings.Right;
		if (ch9EasterEgg)
		{
			yield return 0.5f;
			yield return Level.ZoomTo(Position - Level.Camera.Position + new Vector2(0f, -32f), 2f, 0.5f);
			Dialog.Language.Dialog["CH10_GRANNY_EASTEREGG"] = "{portrait GRANNY right mock} I see you have discovered Debug Mode.";
			yield return Textbox.Say("CH10_GRANNY_EASTEREGG");
			talker.Enabled = false;
		}
		else if (conversation == 0)
		{
			yield return 0.5f;
			yield return Level.ZoomTo(Position - Level.Camera.Position + new Vector2(0f, -32f), 2f, 0.5f);
			yield return Textbox.Say("CH7_CSIDE_OLDLADY", StartLaughing, StopLaughing);
		}
		else if (conversation == 1)
		{
			yield return 0.5f;
			yield return Level.ZoomTo(Position - Level.Camera.Position + new Vector2(0f, -32f), 2f, 0.5f);
			yield return Textbox.Say("CH7_CSIDE_OLDLADY_B", StartLaughing, StopLaughing);
			talker.Enabled = false;
		}
		yield return Level.ZoomBack(0.5f);
		Level.EndCutscene();
		EndTalking(Level);
	}

	[IteratorStateMachine(typeof(_003CStartLaughing_003Ed__12))]
	private IEnumerator StartLaughing()
	{
		Sprite.Play("laugh");
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CStopLaughing_003Ed__13))]
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
		conversation++;
		if (talkRoutine != null)
		{
			talkRoutine.RemoveSelf();
			talkRoutine = null;
		}
		Sprite.Play("idle");
	}
}
