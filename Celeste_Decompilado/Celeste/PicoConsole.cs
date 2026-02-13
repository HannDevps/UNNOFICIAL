using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Celeste.Pico8;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class PicoConsole : Entity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public bool wasUnlocked;

		public PicoConsole _003C_003E4__this;

		public bool done;

		public Action _003C_003E9__1;

		internal void _003CInteractRoutine_003Eb__0()
		{
			if (!wasUnlocked)
			{
				_003C_003E4__this.Scene.Add(new UnlockedPico8Message(delegate
				{
					done = true;
				}));
			}
			else
			{
				done = true;
			}
			Engine.Scene = new Emulator(_003C_003E4__this.Scene as Level);
		}

		internal void _003CInteractRoutine_003Eb__1()
		{
			done = true;
		}
	}

	[CompilerGenerated]
	private sealed class _003CInteractRoutine_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PicoConsole _003C_003E4__this;

		public Player player;

		private _003C_003Ec__DisplayClass8_0 _003C_003E8__1;

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
		public _003CInteractRoutine_003Ed__8(int _003C_003E1__state)
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
			PicoConsole picoConsole = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass8_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				player.StateMachine.State = 11;
				_003C_003E2__current = player.DummyWalkToExact((int)picoConsole.X - 6);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				player.Facing = Facings.Right;
				_003C_003E8__1.wasUnlocked = Settings.Instance.Pico8OnMainMenu;
				Settings.Instance.Pico8OnMainMenu = true;
				if (!_003C_003E8__1.wasUnlocked)
				{
					UserIO.SaveHandler(file: false, settings: true);
					goto IL_00ed;
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 3;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00ed;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0116;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0197;
			case 5:
				{
					_003C_003E1__state = -1;
					picoConsole.talking = false;
					(picoConsole.Scene as Level).PauseLock = false;
					player.StateMachine.State = 0;
					return false;
				}
				IL_00ed:
				if (UserIO.Saving)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_0116;
				IL_0116:
				_003C_003E8__1.done = false;
				SpotlightWipe.FocusPoint = player.Position - (picoConsole.Scene as Level).Camera.Position + new Vector2(0f, -8f);
				new SpotlightWipe(picoConsole.Scene, wipeIn: false, delegate
				{
					if (!_003C_003E8__1.wasUnlocked)
					{
						_003C_003E8__1._003C_003E4__this.Scene.Add(new UnlockedPico8Message(delegate
						{
							_003C_003E8__1.done = true;
						}));
					}
					else
					{
						_003C_003E8__1.done = true;
					}
					Engine.Scene = new Emulator(_003C_003E8__1._003C_003E4__this.Scene as Level);
				});
				goto IL_0197;
				IL_0197:
				if (!_003C_003E8__1.done)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = 0.25f;
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

	private Image sprite;

	private TalkComponent talk;

	private bool talking;

	private SoundSource sfx;

	public PicoConsole(Vector2 position)
		: base(position)
	{
		base.Depth = 1000;
		AddTag(Tags.TransitionUpdate);
		AddTag(Tags.PauseUpdate);
		Add(sprite = new Image(GFX.Game["objects/pico8Console"]));
		sprite.JustifyOrigin(0.5f, 1f);
		Add(talk = new TalkComponent(new Rectangle(-12, -8, 24, 8), new Vector2(0f, -24f), OnInteract));
	}

	public PicoConsole(EntityData data, Vector2 position)
		: this(data.Position + position)
	{
	}

	public override void Update()
	{
		base.Update();
		if (sfx == null)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.Y < base.Y + 16f)
			{
				Add(sfx = new SoundSource("event:/env/local/03_resort/pico8_machine"));
			}
		}
	}

	private void OnInteract(Player player)
	{
		if (!talking)
		{
			(base.Scene as Level).PauseLock = true;
			talking = true;
			Add(new Coroutine(InteractRoutine(player)));
		}
	}

	[IteratorStateMachine(typeof(_003CInteractRoutine_003Ed__8))]
	private IEnumerator InteractRoutine(Player player)
	{
		player.StateMachine.State = 11;
		yield return player.DummyWalkToExact((int)base.X - 6);
		player.Facing = Facings.Right;
		bool wasUnlocked = Settings.Instance.Pico8OnMainMenu;
		Settings.Instance.Pico8OnMainMenu = true;
		if (!wasUnlocked)
		{
			UserIO.SaveHandler(file: false, settings: true);
			while (UserIO.Saving)
			{
				yield return null;
			}
		}
		else
		{
			yield return 0.5f;
		}
		bool done = false;
		SpotlightWipe.FocusPoint = player.Position - (base.Scene as Level).Camera.Position + new Vector2(0f, -8f);
		new SpotlightWipe(base.Scene, wipeIn: false, delegate
		{
			if (!wasUnlocked)
			{
				base.Scene.Add(new UnlockedPico8Message(delegate
				{
					done = true;
				}));
			}
			else
			{
				done = true;
			}
			Engine.Scene = new Emulator(base.Scene as Level);
		});
		while (!done)
		{
			yield return null;
		}
		yield return 0.25f;
		talking = false;
		(base.Scene as Level).PauseLock = false;
		player.StateMachine.State = 0;
	}

	public override void SceneEnd(Scene scene)
	{
		if (sfx != null)
		{
			sfx.Stop();
			sfx.RemoveSelf();
			sfx = null;
		}
		base.SceneEnd(scene);
	}
}
