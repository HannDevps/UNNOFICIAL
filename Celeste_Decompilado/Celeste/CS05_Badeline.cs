using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_Badeline : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Badeline _003C_003E4__this;

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
		public _003CCutscene_003Ed__8(int _003C_003E1__state)
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
			CS05_Badeline cS05_Badeline = _003C_003E4__this;
			Vector2 screenSpaceFocusPoint;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_Badeline.player.StateMachine.State = 11;
				cS05_Badeline.player.StateMachine.Locked = true;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (cS05_Badeline.index == 3)
				{
					cS05_Badeline.player.DummyAutoAnimate = false;
					cS05_Badeline.player.Sprite.Play("tired");
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_00e1;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00e1;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00e1;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch5_shadow_maddy_" + cS05_Badeline.index, cS05_Badeline.BadelineLeaves);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				if (!cS05_Badeline.moved)
				{
					cS05_Badeline.npc.MoveToNode(cS05_Badeline.index);
				}
				_003C_003E2__current = cS05_Badeline.Level.ZoomBack(0.5f);
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					cS05_Badeline.EndCutscene(level);
					return false;
				}
				IL_00e1:
				if (cS05_Badeline.player.Scene != null && !cS05_Badeline.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				screenSpaceFocusPoint = (cS05_Badeline.badeline.Center + cS05_Badeline.player.Center) * 0.5f - cS05_Badeline.Level.Camera.Position + new Vector2(0f, -12f);
				_003C_003E2__current = cS05_Badeline.Level.ZoomTo(screenSpaceFocusPoint, 2f, 0.5f);
				_003C_003E1__state = 4;
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
	private sealed class _003CBadelineLeaves_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_Badeline _003C_003E4__this;

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
		public _003CBadelineLeaves_003Ed__10(int _003C_003E1__state)
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
			CS05_Badeline cS05_Badeline = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS05_Badeline.moved = true;
				cS05_Badeline.npc.MoveToNode(cS05_Badeline.index);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS05_Badeline.player.Sprite.Play("tiredStill");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS05_Badeline.player.Sprite.Play("idle");
				_003C_003E2__current = 0.6f;
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

	private Player player;

	private NPC05_Badeline npc;

	private BadelineDummy badeline;

	private int index;

	private bool moved;

	public static string GetFlag(int index)
	{
		return "badeline_" + index;
	}

	public CS05_Badeline(Player player, NPC05_Badeline npc, BadelineDummy badeline, int index)
	{
		this.player = player;
		this.npc = npc;
		this.badeline = badeline;
		this.index = index;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return 0.25f;
		if (index == 3)
		{
			player.DummyAutoAnimate = false;
			player.Sprite.Play("tired");
			yield return 0.2f;
		}
		while (player.Scene != null && !player.OnGround())
		{
			yield return null;
		}
		Vector2 screenSpaceFocusPoint = (badeline.Center + player.Center) * 0.5f - Level.Camera.Position + new Vector2(0f, -12f);
		yield return Level.ZoomTo(screenSpaceFocusPoint, 2f, 0.5f);
		yield return Textbox.Say("ch5_shadow_maddy_" + index, BadelineLeaves);
		if (!moved)
		{
			npc.MoveToNode(index);
		}
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		npc.SnapToNode(index);
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		level.Session.SetFlag(GetFlag(index));
	}

	[IteratorStateMachine(typeof(_003CBadelineLeaves_003Ed__10))]
	private IEnumerator BadelineLeaves()
	{
		yield return 0.1f;
		moved = true;
		npc.MoveToNode(index);
		yield return 0.5f;
		player.Sprite.Play("tiredStill");
		yield return 0.5f;
		player.Sprite.Play("idle");
		yield return 0.6f;
	}
}
