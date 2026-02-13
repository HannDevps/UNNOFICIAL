using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_OshiroClutter : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroClutter _003C_003E4__this;

		public Level level;

		private List<ClutterDoor>.Enumerator _003C_003E7__wrap1;

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
		public _003CCutscene_003Ed__6(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 6)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				CS03_OshiroClutter cS03_OshiroClutter = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
				{
					_003C_003E1__state = -1;
					cS03_OshiroClutter.player.StateMachine.State = 11;
					cS03_OshiroClutter.player.StateMachine.Locked = true;
					int num2 = ((cS03_OshiroClutter.index != 1 && cS03_OshiroClutter.index != 2) ? 1 : (-1));
					if (num2 == -1)
					{
						_003C_003E2__current = cS03_OshiroClutter.player.DummyWalkToExact((int)cS03_OshiroClutter.oshiro.X - 24);
						_003C_003E1__state = 1;
						return true;
					}
					cS03_OshiroClutter.Add(new Coroutine(cS03_OshiroClutter.oshiro.PaceRight()));
					_003C_003E2__current = cS03_OshiroClutter.player.DummyWalkToExact((int)cS03_OshiroClutter.oshiro.HomePosition.X + 24);
					_003C_003E1__state = 2;
					return true;
				}
				case 1:
					_003C_003E1__state = -1;
					cS03_OshiroClutter.player.Facing = Facings.Right;
					cS03_OshiroClutter.oshiro.Sprite.Scale.X = -1f;
					goto IL_0173;
				case 2:
					_003C_003E1__state = -1;
					cS03_OshiroClutter.player.Facing = Facings.Left;
					cS03_OshiroClutter.oshiro.Sprite.Scale.X = 1f;
					goto IL_0173;
				case 3:
					_003C_003E1__state = -1;
					_003C_003E2__current = Textbox.Say("CH3_OSHIRO_CLUTTER" + cS03_OshiroClutter.index, cS03_OshiroClutter.Collapse, cS03_OshiroClutter.oshiro.PaceLeft, cS03_OshiroClutter.oshiro.PaceRight);
					_003C_003E1__state = 4;
					return true;
				case 4:
					_003C_003E1__state = -1;
					_003C_003E2__current = cS03_OshiroClutter.Level.ZoomBack(0.5f);
					_003C_003E1__state = 5;
					return true;
				case 5:
					_003C_003E1__state = -1;
					level.Session.SetFlag("oshiro_clutter_door_open");
					if (cS03_OshiroClutter.index == 0)
					{
						cS03_OshiroClutter.SetMusic();
					}
					_003C_003E7__wrap1 = cS03_OshiroClutter.doors.GetEnumerator();
					_003C_003E1__state = -3;
					goto IL_02da;
				case 6:
					_003C_003E1__state = -3;
					goto IL_02da;
				case 7:
					_003C_003E1__state = -1;
					_003C_003E2__current = cS03_OshiroClutter.Level.ZoomTo(new Vector2(90f, 60f), 2f, 0.5f);
					_003C_003E1__state = 8;
					return true;
				case 8:
					_003C_003E1__state = -1;
					_003C_003E2__current = Textbox.Say("CH3_OSHIRO_CLUTTER_ENDING");
					_003C_003E1__state = 9;
					return true;
				case 9:
					_003C_003E1__state = -1;
					_003C_003E2__current = cS03_OshiroClutter.oshiro.MoveTo(new Vector2(cS03_OshiroClutter.oshiro.X, level.Bounds.Top - 32));
					_003C_003E1__state = 10;
					return true;
				case 10:
					_003C_003E1__state = -1;
					cS03_OshiroClutter.oshiro.Add(new SoundSource("event:/char/oshiro/move_05_09b_exit"));
					_003C_003E2__current = cS03_OshiroClutter.Level.ZoomBack(0.5f);
					_003C_003E1__state = 11;
					return true;
				case 11:
					{
						_003C_003E1__state = -1;
						break;
					}
					IL_0173:
					if (cS03_OshiroClutter.index < 3)
					{
						_003C_003E2__current = cS03_OshiroClutter.Level.ZoomTo(cS03_OshiroClutter.oshiro.ZoomPoint, 2f, 0.5f);
						_003C_003E1__state = 3;
						return true;
					}
					_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(cS03_OshiroClutter.Level.Bounds.X, cS03_OshiroClutter.Level.Bounds.Y), 0.5f);
					_003C_003E1__state = 7;
					return true;
					IL_02da:
					while (_003C_003E7__wrap1.MoveNext())
					{
						ClutterDoor current = _003C_003E7__wrap1.Current;
						if (!current.IsLocked(level.Session))
						{
							_003C_003E2__current = current.UnlockRoutine();
							_003C_003E1__state = 6;
							return true;
						}
					}
					_003C_003Em__Finally1();
					_003C_003E7__wrap1 = default(List<ClutterDoor>.Enumerator);
					break;
				}
				cS03_OshiroClutter.EndCutscene(level);
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap1/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class _003CCollapse_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroClutter _003C_003E4__this;

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
		public _003CCollapse_003Ed__7(int _003C_003E1__state)
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
			CS03_OshiroClutter cS03_OshiroClutter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/oshiro/chat_collapse", cS03_OshiroClutter.oshiro.Position);
				cS03_OshiroClutter.oshiro.Sprite.Play("fall");
				_003C_003E2__current = 0.5f;
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

	private int index;

	private Player player;

	private NPC03_Oshiro_Cluttter oshiro;

	private List<ClutterDoor> doors;

	public CS03_OshiroClutter(Player player, NPC03_Oshiro_Cluttter oshiro, int index)
	{
		this.player = player;
		this.oshiro = oshiro;
		this.index = index;
	}

	public override void OnBegin(Level level)
	{
		doors = base.Scene.Entities.FindAll<ClutterDoor>();
		doors.Sort((ClutterDoor a, ClutterDoor b) => (int)(a.Y - b.Y));
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		int num = ((index != 1 && index != 2) ? 1 : (-1));
		if (num == -1)
		{
			yield return player.DummyWalkToExact((int)oshiro.X - 24);
			player.Facing = Facings.Right;
			oshiro.Sprite.Scale.X = -1f;
		}
		else
		{
			Add(new Coroutine(oshiro.PaceRight()));
			yield return player.DummyWalkToExact((int)oshiro.HomePosition.X + 24);
			player.Facing = Facings.Left;
			oshiro.Sprite.Scale.X = 1f;
		}
		if (index < 3)
		{
			yield return Level.ZoomTo(oshiro.ZoomPoint, 2f, 0.5f);
			yield return Textbox.Say("CH3_OSHIRO_CLUTTER" + index, Collapse, oshiro.PaceLeft, oshiro.PaceRight);
			yield return Level.ZoomBack(0.5f);
			level.Session.SetFlag("oshiro_clutter_door_open");
			if (index == 0)
			{
				SetMusic();
			}
			foreach (ClutterDoor door in doors)
			{
				if (!door.IsLocked(level.Session))
				{
					yield return door.UnlockRoutine();
				}
			}
		}
		else
		{
			yield return CutsceneEntity.CameraTo(new Vector2(Level.Bounds.X, Level.Bounds.Y), 0.5f);
			yield return Level.ZoomTo(new Vector2(90f, 60f), 2f, 0.5f);
			yield return Textbox.Say("CH3_OSHIRO_CLUTTER_ENDING");
			yield return oshiro.MoveTo(new Vector2(oshiro.X, level.Bounds.Top - 32));
			oshiro.Add(new SoundSource("event:/char/oshiro/move_05_09b_exit"));
			yield return Level.ZoomBack(0.5f);
		}
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CCollapse_003Ed__7))]
	private IEnumerator Collapse()
	{
		Audio.Play("event:/char/oshiro/chat_collapse", oshiro.Position);
		oshiro.Sprite.Play("fall");
		yield return 0.5f;
	}

	private void SetMusic()
	{
		Level obj = base.Scene as Level;
		obj.Session.Audio.Music.Event = "event:/music/lvl3/clean";
		obj.Session.Audio.Music.Progress = 1;
		obj.Session.Audio.Apply();
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		if (oshiro.Sprite.CurrentAnimationID == "side")
		{
			(oshiro.Sprite as OshiroSprite).Pop("idle", flip: true);
		}
		if (index < 3)
		{
			level.Session.SetFlag("oshiro_clutter_door_open");
			level.Session.SetFlag("oshiro_clutter_" + index);
			if (index == 0 && WasSkipped)
			{
				SetMusic();
			}
			foreach (ClutterDoor door in doors)
			{
				if (!door.IsLocked(level.Session))
				{
					door.InstantUnlock();
				}
			}
			if (WasSkipped && index == 0)
			{
				oshiro.Sprite.Play("idle_ground");
			}
		}
		else
		{
			level.Session.SetFlag("oshiro_clutter_finished");
			base.Scene.Remove(oshiro);
		}
	}
}
