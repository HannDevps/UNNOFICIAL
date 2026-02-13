using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC04_Granny : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalkRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC04_Granny _003C_003E4__this;

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
		public _003CTalkRoutine_003Ed__9(int _003C_003E1__state)
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
			NPC04_Granny nPC04_Granny = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC04_Granny.Sprite.Play("idle");
				player.ForceCameraUpdate = true;
				_003C_003E2__current = nPC04_Granny.PlayerApproachLeftSide(player, turnToFace: true, 20f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC04_Granny.Level.ZoomTo(new Vector2((player.X + nPC04_Granny.X) / 2f - nPC04_Granny.Level.Camera.X, 116f), 2f, 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				if (!nPC04_Granny.Session.GetFlag("granny_2"))
				{
					_003C_003E2__current = Textbox.Say("CH4_GRANNY_2");
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = Textbox.Say("CH4_GRANNY_3");
				_003C_003E1__state = 4;
				return true;
			case 3:
				_003C_003E1__state = -1;
				goto IL_013c;
			case 4:
				_003C_003E1__state = -1;
				goto IL_013c;
			case 5:
				{
					_003C_003E1__state = -1;
					nPC04_Granny.Level.EndCutscene();
					nPC04_Granny.TalkEnd(nPC04_Granny.Level);
					return false;
				}
				IL_013c:
				_003C_003E2__current = nPC04_Granny.Level.ZoomBack(0.5f);
				_003C_003E1__state = 5;
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

	public Hahaha Hahaha;

	private bool cutscene;

	private Coroutine talkRoutine;

	private const string talkedFlagA = "granny_2";

	private const string talkedFlagB = "granny_3";

	public NPC04_Granny(Vector2 position)
		: base(position)
	{
		Add(Sprite = GFX.SpriteBank.Create("granny"));
		Sprite.Scale.X = -1f;
		Sprite.Play("idle");
		Add(new GrannyLaughSfx(Sprite));
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		scene.Add(Hahaha = new Hahaha(Position + new Vector2(8f, -4f)));
		Hahaha.Enabled = false;
		if (base.Session.GetFlag("granny_1") && !base.Session.GetFlag("granny_2"))
		{
			Sprite.Play("laugh");
		}
		if (!base.Session.GetFlag("granny_3"))
		{
			Add(Talker = new TalkComponent(new Rectangle(-20, -16, 40, 16), new Vector2(0f, -24f), OnTalk));
			if (!base.Session.GetFlag("granny_1"))
			{
				Talker.Enabled = false;
			}
		}
	}

	public override void Update()
	{
		Player entity = Level.Tracker.GetEntity<Player>();
		if (entity != null && !base.Session.GetFlag("granny_1") && !cutscene && entity.X > base.X - 40f)
		{
			cutscene = true;
			base.Scene.Add(new CS04_Granny(this, entity));
			if (Talker != null)
			{
				Talker.Enabled = true;
			}
		}
		Hahaha.Enabled = Sprite.CurrentAnimationID == "laugh";
		base.Update();
	}

	private void OnTalk(Player player)
	{
		Level.StartCutscene(TalkEnd);
		Add(talkRoutine = new Coroutine(TalkRoutine(player)));
	}

	[IteratorStateMachine(typeof(_003CTalkRoutine_003Ed__9))]
	private IEnumerator TalkRoutine(Player player)
	{
		Sprite.Play("idle");
		player.ForceCameraUpdate = true;
		yield return PlayerApproachLeftSide(player, turnToFace: true, 20f);
		yield return Level.ZoomTo(new Vector2((player.X + base.X) / 2f - Level.Camera.X, 116f), 2f, 0.5f);
		if (!base.Session.GetFlag("granny_2"))
		{
			yield return Textbox.Say("CH4_GRANNY_2");
		}
		else
		{
			yield return Textbox.Say("CH4_GRANNY_3");
		}
		yield return Level.ZoomBack(0.5f);
		Level.EndCutscene();
		TalkEnd(Level);
	}

	private void TalkEnd(Level level)
	{
		if (!base.Session.GetFlag("granny_2"))
		{
			base.Session.SetFlag("granny_2");
		}
		else if (!base.Session.GetFlag("granny_3"))
		{
			base.Session.SetFlag("granny_3");
			Remove(Talker);
		}
		if (talkRoutine != null)
		{
			talkRoutine.RemoveSelf();
			talkRoutine = null;
		}
		Player entity = Level.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.StateMachine.Locked = false;
			entity.StateMachine.State = 0;
			entity.ForceCameraUpdate = false;
		}
	}
}
