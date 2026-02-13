using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC09_Granny_Outside : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalkRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Player player;

		public NPC09_Granny_Outside _003C_003E4__this;

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
			NPC09_Granny_Outside nPC09_Granny_Outside = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				player.StateMachine.State = 11;
				goto IL_006c;
			case 1:
				_003C_003E1__state = -1;
				goto IL_006c;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC09_Granny_Outside.Level.ZoomTo(new Vector2(200f, 110f), 2f, 0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("APP_OLDLADY_A", nPC09_Granny_Outside.MoveRight, nPC09_Granny_Outside.ExitRight);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC09_Granny_Outside.Level.ZoomBack(0.5f);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				nPC09_Granny_Outside.Sprite.Scale.X = 1f;
				if (!nPC09_Granny_Outside.leaving)
				{
					_003C_003E2__current = nPC09_Granny_Outside.ExitRight();
					_003C_003E1__state = 7;
					return true;
				}
				break;
			case 7:
				_003C_003E1__state = -1;
				break;
			case 8:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_006c:
				if (!player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				nPC09_Granny_Outside.Sprite.Scale.X = -1f;
				_003C_003E2__current = player.DummyWalkToExact((int)nPC09_Granny_Outside.X - 16);
				_003C_003E1__state = 2;
				return true;
			}
			if (nPC09_Granny_Outside.X < (float)(nPC09_Granny_Outside.Level.Bounds.Right + 8))
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 8;
				return true;
			}
			nPC09_Granny_Outside.Level.EndCutscene();
			nPC09_Granny_Outside.EndTalking(nPC09_Granny_Outside.Level);
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
	private sealed class _003CMoveRight_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC09_Granny_Outside _003C_003E4__this;

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
		public _003CMoveRight_003Ed__10(int _003C_003E1__state)
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
			NPC09_Granny_Outside nPC09_Granny_Outside = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC09_Granny_Outside.MoveTo(new Vector2(nPC09_Granny_Outside.X + 8f, nPC09_Granny_Outside.Y));
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
	private sealed class _003CExitRight_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC09_Granny_Outside _003C_003E4__this;

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
		public _003CExitRight_003Ed__11(int _003C_003E1__state)
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
			NPC09_Granny_Outside nPC09_Granny_Outside = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC09_Granny_Outside.leaving = true;
				nPC09_Granny_Outside.Add(new Coroutine(nPC09_Granny_Outside.MoveTo(new Vector2(nPC09_Granny_Outside.Level.Bounds.Right + 16, nPC09_Granny_Outside.Y))));
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

	public const string Flag = "granny_outside";

	public Hahaha Hahaha;

	public GrannyLaughSfx LaughSfx;

	private bool talking;

	private Player player;

	private bool leaving;

	public NPC09_Granny_Outside(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(Sprite = GFX.SpriteBank.Create("granny"));
		Sprite.Play("idle");
		Add(LaughSfx = new GrannyLaughSfx(Sprite));
		MoveAnim = "walk";
		IdleAnim = "idle";
		Maxspeed = 40f;
		SetupGrannySpriteSounds();
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if ((scene as Level).Session.GetFlag("granny_outside"))
		{
			RemoveSelf();
		}
		scene.Add(Hahaha = new Hahaha(Position + new Vector2(8f, -4f)));
		Hahaha.Enabled = false;
	}

	public override void Update()
	{
		if (!talking)
		{
			player = Level.Tracker.GetEntity<Player>();
			if (player != null && player.X > base.X - 48f)
			{
				(base.Scene as Level).StartCutscene(EndTalking);
				Add(new Coroutine(TalkRoutine(player)));
				talking = true;
			}
		}
		Hahaha.Enabled = Sprite.CurrentAnimationID == "laugh";
		base.Update();
	}

	[IteratorStateMachine(typeof(_003CTalkRoutine_003Ed__9))]
	private IEnumerator TalkRoutine(Player player)
	{
		player.StateMachine.State = 11;
		while (!player.OnGround())
		{
			yield return null;
		}
		Sprite.Scale.X = -1f;
		yield return player.DummyWalkToExact((int)base.X - 16);
		yield return 0.5f;
		yield return Level.ZoomTo(new Vector2(200f, 110f), 2f, 0.5f);
		yield return Textbox.Say("APP_OLDLADY_A", MoveRight, ExitRight);
		yield return Level.ZoomBack(0.5f);
		Sprite.Scale.X = 1f;
		if (!leaving)
		{
			yield return ExitRight();
		}
		while (base.X < (float)(Level.Bounds.Right + 8))
		{
			yield return null;
		}
		Level.EndCutscene();
		EndTalking(Level);
	}

	[IteratorStateMachine(typeof(_003CMoveRight_003Ed__10))]
	private IEnumerator MoveRight()
	{
		yield return MoveTo(new Vector2(base.X + 8f, base.Y));
	}

	[IteratorStateMachine(typeof(_003CExitRight_003Ed__11))]
	private IEnumerator ExitRight()
	{
		leaving = true;
		Add(new Coroutine(MoveTo(new Vector2(Level.Bounds.Right + 16, base.Y))));
		yield return null;
	}

	private void EndTalking(Level level)
	{
		if (player != null)
		{
			player.StateMachine.State = 0;
		}
		Level.Session.SetFlag("granny_outside");
		RemoveSelf();
	}
}
