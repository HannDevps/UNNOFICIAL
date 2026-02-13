using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class BirdNPC : Actor
{
	public enum Modes
	{
		ClimbingTutorial,
		DashingTutorial,
		DreamJumpTutorial,
		SuperWallJumpTutorial,
		HyperJumpTutorial,
		FlyAway,
		None,
		Sleeping,
		MoveToNodes,
		WaitForLightningOff
	}

	[CompilerGenerated]
	private sealed class _003CCaw_003Ed__27 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

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
		public _003CCaw_003Ed__27(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				birdNPC.Sprite.Play("croak");
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (birdNPC.Sprite.CurrentAnimationFrame < 9)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			Audio.Play("event:/game/general/bird_squawk", birdNPC.Position);
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
	private sealed class _003CShowTutorial_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool caw;

		public BirdNPC _003C_003E4__this;

		public BirdTutorialGui gui;

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
		public _003CShowTutorial_003Ed__28(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (caw)
				{
					_003C_003E2__current = birdNPC.Caw();
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_004d;
			case 1:
				_003C_003E1__state = -1;
				goto IL_004d;
			case 2:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_004d:
				birdNPC.gui = gui;
				gui.Open = true;
				birdNPC.Scene.Add(gui);
				break;
			}
			if (gui.Scale < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
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
	private sealed class _003CHideTutorial_003Ed__29 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

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
		public _003CHideTutorial_003Ed__29(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if (birdNPC.gui == null)
				{
					goto IL_0075;
				}
				birdNPC.gui.Open = false;
			}
			if (birdNPC.gui.Scale > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			birdNPC.Scene.Remove(birdNPC.gui);
			birdNPC.gui = null;
			goto IL_0075;
			IL_0075:
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
	private sealed class _003CStartleAndFlyAway_003Ed__30 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

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
		public _003CStartleAndFlyAway_003Ed__30(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				birdNPC.Depth = -1000000;
				birdNPC.level.Session.SetFlag(FlownFlag + birdNPC.level.Session.Level);
				_003C_003E2__current = birdNPC.Startle("event:/game/general/bird_startle");
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = birdNPC.FlyAway();
				_003C_003E1__state = 2;
				return true;
			case 2:
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
	private sealed class _003CFlyAway_003Ed__31 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		public float upwardsMultiplier;

		private Vector2 _003Cspeed_003E5__2;

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
		public _003CFlyAway_003Ed__31(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (birdNPC.staticMover != null)
				{
					birdNPC.staticMover.RemoveSelf();
					birdNPC.staticMover = null;
				}
				birdNPC.Sprite.Play("fly");
				birdNPC.Facing = (Facings)(0 - birdNPC.Facing);
				_003Cspeed_003E5__2 = new Vector2((int)birdNPC.Facing * 20, -40f * upwardsMultiplier);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (birdNPC.Y > (float)birdNPC.level.Bounds.Top)
			{
				_003Cspeed_003E5__2 += new Vector2((int)birdNPC.Facing * 140, -120f * upwardsMultiplier) * Engine.DeltaTime;
				birdNPC.Position += _003Cspeed_003E5__2 * Engine.DeltaTime;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			birdNPC.RemoveSelf();
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
	private sealed class _003CClimbingTutorial_003Ed__32 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		private Player _003Cp_003E5__2;

		private BirdTutorialGui _003Ctut1_003E5__3;

		private BirdTutorialGui _003Ctut2_003E5__4;

		private bool _003Cfirst_003E5__5;

		private bool _003CwillEnd_003E5__6;

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
		public _003CClimbingTutorial_003Ed__32(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = birdNPC.Scene.Tracker.GetEntity<Player>();
				goto IL_0094;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0094;
			case 3:
				_003C_003E1__state = -1;
				_003Cfirst_003E5__5 = false;
				goto IL_0198;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0198;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = birdNPC.ShowTutorial(_003Ctut2_003E5__4);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				goto IL_0234;
			case 7:
				_003C_003E1__state = -1;
				goto IL_0234;
			case 8:
				_003C_003E1__state = -1;
				if (_003CwillEnd_003E5__6)
				{
					_003C_003E2__current = birdNPC.StartleAndFlyAway();
					_003C_003E1__state = 9;
					return true;
				}
				goto IL_0150;
			case 9:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0198:
				if (_003Cp_003E5__2.StateMachine.State != 1 && _003Cp_003E5__2.Y > birdNPC.Y)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				if (_003Cp_003E5__2.Y > birdNPC.Y)
				{
					Audio.Play("event:/ui/game/tutorial_note_flip_back");
					_003C_003E2__current = birdNPC.HideTutorial();
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_0234;
				IL_0094:
				if (Math.Abs(_003Cp_003E5__2.X - birdNPC.X) > 120f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Ctut1_003E5__3 = new BirdTutorialGui(birdNPC, new Vector2(0f, -16f), Dialog.Clean("tutorial_climb"), Dialog.Clean("tutorial_hold"), BirdTutorialGui.ButtonPrompt.Grab);
				_003Ctut2_003E5__4 = new BirdTutorialGui(birdNPC, new Vector2(0f, -16f), Dialog.Clean("tutorial_climb"), BirdTutorialGui.ButtonPrompt.Grab, "+", new Vector2(0f, -1f));
				_003Cfirst_003E5__5 = true;
				goto IL_0150;
				IL_0150:
				_003C_003E2__current = birdNPC.ShowTutorial(_003Ctut1_003E5__3, _003Cfirst_003E5__5);
				_003C_003E1__state = 3;
				return true;
				IL_0234:
				if (_003Cp_003E5__2.Scene != null && (!_003Cp_003E5__2.OnGround() || _003Cp_003E5__2.StateMachine.State == 1))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				_003CwillEnd_003E5__6 = _003Cp_003E5__2.Y <= birdNPC.Y + 4f;
				if (!_003CwillEnd_003E5__6)
				{
					Audio.Play("event:/ui/game/tutorial_note_flip_front");
				}
				_003C_003E2__current = birdNPC.HideTutorial();
				_003C_003E1__state = 8;
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
	private sealed class _003CDashingTutorial_003Ed__33 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		private Player _003Cplayer_003E5__2;

		private Bridge _003Cbridge_003E5__3;

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
		public _003CDashingTutorial_003Ed__33(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				birdNPC.Y = birdNPC.level.Bounds.Top;
				birdNPC.X += 32f;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cplayer_003E5__2 = birdNPC.Scene.Tracker.GetEntity<Player>();
				_003Cbridge_003E5__3 = birdNPC.Scene.Entities.FindFirst<Bridge>();
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if ((_003Cplayer_003E5__2 == null || !(_003Cplayer_003E5__2.X > birdNPC.StartPosition.X - 92f) || !(_003Cplayer_003E5__2.Y > birdNPC.StartPosition.Y - 20f) || !(_003Cplayer_003E5__2.Y < birdNPC.StartPosition.Y - 10f)) && (!SaveData.Instance.Assists.Invincible || _003Cplayer_003E5__2 == null || !(_003Cplayer_003E5__2.X > birdNPC.StartPosition.X - 60f) || !(_003Cplayer_003E5__2.Y > birdNPC.StartPosition.Y) || !(_003Cplayer_003E5__2.Y < birdNPC.StartPosition.Y + 34f)))
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			birdNPC.Scene.Add(new CS00_Ending(_003Cplayer_003E5__2, birdNPC, _003Cbridge_003E5__3));
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
	private sealed class _003CDreamJumpTutorial_003Ed__34 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

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
		public _003CDreamJumpTutorial_003Ed__34(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			Player entity;
			Player entity2;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = birdNPC.ShowTutorial(new BirdTutorialGui(birdNPC, new Vector2(0f, -16f), Dialog.Clean("tutorial_dreamjump"), new Vector2(1f, 0f), "+", BirdTutorialGui.ButtonPrompt.Jump), caw: true);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00a0;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00a0;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0117;
			case 4:
				_003C_003E1__state = -1;
				goto IL_0117;
			case 5:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0117:
				entity = birdNPC.Scene.Tracker.GetEntity<Player>();
				if (entity == null || !((birdNPC.Position - entity.Position).Length() < 24f))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = birdNPC.StartleAndFlyAway();
				_003C_003E1__state = 5;
				return true;
				IL_00a0:
				entity2 = birdNPC.Scene.Tracker.GetEntity<Player>();
				if (entity2 == null || (!(entity2.X > birdNPC.X) && !((birdNPC.Position - entity2.Position).Length() < 32f)))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = birdNPC.HideTutorial();
				_003C_003E1__state = 3;
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
	private sealed class _003CSuperWallJumpTutorial_003Ed__35 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		private BirdTutorialGui _003Ctut1_003E5__2;

		private BirdTutorialGui _003Ctut2_003E5__3;

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
		public _003CSuperWallJumpTutorial_003Ed__35(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			bool caw;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				birdNPC.Facing = Facings.Right;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				caw = true;
				_003Ctut1_003E5__2 = new BirdTutorialGui(birdNPC, new Vector2(0f, -16f), GFX.Gui["hyperjump/tutorial00"], Dialog.Clean("TUTORIAL_DASH"), new Vector2(0f, -1f));
				_003Ctut2_003E5__3 = new BirdTutorialGui(birdNPC, new Vector2(0f, -16f), GFX.Gui["hyperjump/tutorial01"], Dialog.Clean("TUTORIAL_DREAMJUMP"));
				goto IL_00fd;
			case 2:
				_003C_003E1__state = -1;
				birdNPC.Sprite.Play("idleRarePeck");
				_003C_003E2__current = 2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				birdNPC.gui = _003Ctut2_003E5__3;
				birdNPC.gui.Open = true;
				birdNPC.gui.Scale = 1f;
				birdNPC.Scene.Add(birdNPC.gui);
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003Ctut1_003E5__2.Open = false;
				_003Ctut1_003E5__2.Scale = 0f;
				birdNPC.Scene.Remove(_003Ctut1_003E5__2);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = birdNPC.HideTutorial();
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 7;
				return true;
			case 7:
			{
				_003C_003E1__state = -1;
				caw = false;
				Player entity = birdNPC.Scene.Tracker.GetEntity<Player>();
				if (entity != null && entity.Y <= birdNPC.Y && entity.X > birdNPC.X + 144f)
				{
					_003C_003E2__current = birdNPC.StartleAndFlyAway();
					_003C_003E1__state = 8;
					return true;
				}
				goto IL_00fd;
			}
			case 8:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00fd:
				_003C_003E2__current = birdNPC.ShowTutorial(_003Ctut1_003E5__2, caw);
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
	private sealed class _003CHyperJumpTutorial_003Ed__36 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		private BirdTutorialGui _003Ctut_003E5__2;

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
		public _003CHyperJumpTutorial_003Ed__36(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				birdNPC.Facing = Facings.Left;
				_003Ctut_003E5__2 = new BirdTutorialGui(birdNPC, new Vector2(0f, -16f), Dialog.Clean("TUTORIAL_DREAMJUMP"), new Vector2(1f, 1f), "+", BirdTutorialGui.ButtonPrompt.Dash, GFX.Gui["tinyarrow"], BirdTutorialGui.ButtonPrompt.Jump);
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = birdNPC.ShowTutorial(_003Ctut_003E5__2, caw: true);
				_003C_003E1__state = 2;
				return true;
			case 2:
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
	private sealed class _003CWaitRoutine_003Ed__37 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

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
		public _003CWaitRoutine_003Ed__37(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0077;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0077;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00e9;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00e9;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00e9:
				if (!birdNPC.AutoFly)
				{
					Player entity = birdNPC.Scene.Tracker.GetEntity<Player>();
					if (entity == null || !((entity.Center - birdNPC.Position).Length() < 32f))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 3;
						return true;
					}
				}
				_003C_003E2__current = birdNPC.StartleAndFlyAway();
				_003C_003E1__state = 4;
				return true;
				IL_0077:
				if (!birdNPC.AutoFly)
				{
					Player entity2 = birdNPC.Scene.Tracker.GetEntity<Player>();
					if (entity2 == null || !(Math.Abs(entity2.X - birdNPC.X) < 120f))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_003C_003E2__current = birdNPC.Caw();
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
	private sealed class _003C_003Ec__DisplayClass38_0
	{
		public BirdNPC _003C_003E4__this;

		public Vector2? multiplier;

		internal void _003CStartle_003Eb__0(Tween t)
		{
			if (t.Eased < 0.5f && _003C_003E4__this.Scene.OnInterval(0.05f))
			{
				_003C_003E4__this.level.Particles.Emit(P_Feather, 2, _003C_003E4__this.Position + Vector2.UnitY * -6f, Vector2.One * 4f);
			}
			Vector2 vector = Vector2.Lerp(new Vector2(100f, -100f) * multiplier.Value, new Vector2(20f, -20f) * multiplier.Value, t.Eased);
			vector.X *= 0 - _003C_003E4__this.Facing;
			_003C_003E4__this.Position += vector * Engine.DeltaTime;
		}
	}

	[CompilerGenerated]
	private sealed class _003CStartle_003Ed__38 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		public Vector2? multiplier;

		public string startleSound;

		public float duration;

		private Tween _003Ctween_003E5__2;

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
		public _003CStartle_003Ed__38(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass38_0 CS_0024_003C_003E8__locals10 = new _003C_003Ec__DisplayClass38_0
				{
					_003C_003E4__this = _003C_003E4__this,
					multiplier = multiplier
				};
				if (!CS_0024_003C_003E8__locals10.multiplier.HasValue)
				{
					CS_0024_003C_003E8__locals10.multiplier = new Vector2(1f, 1f);
				}
				if (!string.IsNullOrWhiteSpace(startleSound))
				{
					Audio.Play(startleSound, birdNPC.Position);
				}
				Dust.Burst(birdNPC.Position, -(float)Math.PI / 2f, 8);
				birdNPC.Sprite.Play("jump");
				_003Ctween_003E5__2 = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, duration, start: true);
				_003Ctween_003E5__2.OnUpdate = delegate(Tween t)
				{
					if (t.Eased < 0.5f && CS_0024_003C_003E8__locals10._003C_003E4__this.Scene.OnInterval(0.05f))
					{
						CS_0024_003C_003E8__locals10._003C_003E4__this.level.Particles.Emit(P_Feather, 2, CS_0024_003C_003E8__locals10._003C_003E4__this.Position + Vector2.UnitY * -6f, Vector2.One * 4f);
					}
					Vector2 vector = Vector2.Lerp(new Vector2(100f, -100f) * CS_0024_003C_003E8__locals10.multiplier.Value, new Vector2(20f, -20f) * CS_0024_003C_003E8__locals10.multiplier.Value, t.Eased);
					vector.X *= 0 - CS_0024_003C_003E8__locals10._003C_003E4__this.Facing;
					CS_0024_003C_003E8__locals10._003C_003E4__this.Position = CS_0024_003C_003E8__locals10._003C_003E4__this.Position + vector * Engine.DeltaTime;
				};
				birdNPC.Add(_003Ctween_003E5__2);
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Ctween_003E5__2.Active)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
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
	private sealed class _003CFlyTo_003Ed__39 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		public bool relocateSfx;

		public Vector2 target;

		public float durationMult;

		private SimpleCurve _003Ccurve_003E5__2;

		private float _003Cduration_003E5__3;

		private float _003Cp_003E5__4;

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
		public _003CFlyTo_003Ed__39(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				birdNPC.Sprite.Play("fly");
				if (relocateSfx)
				{
					birdNPC.Add(new SoundSource().Play("event:/new_content/game/10_farewell/bird_relocate"));
				}
				int num2 = Math.Sign(target.X - birdNPC.X);
				if (num2 != 0)
				{
					birdNPC.Facing = (Facings)num2;
				}
				Vector2 position = birdNPC.Position;
				Vector2 vector = target;
				_003Ccurve_003E5__2 = new SimpleCurve(position, vector, position + (vector - position) * 0.75f - Vector2.UnitY * 30f);
				_003Cduration_003E5__3 = (vector - position).Length() / 100f * durationMult;
				_003Cp_003E5__4 = 0f;
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime / _003Cduration_003E5__3;
				break;
			}
			if (_003Cp_003E5__4 < 0.95f)
			{
				birdNPC.Position = _003Ccurve_003E5__2.GetPoint(Ease.SineInOut(_003Cp_003E5__4)).Floor();
				birdNPC.Sprite.Rate = 1f - _003Cp_003E5__4 * 0.5f;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			Dust.Burst(birdNPC.Position, -(float)Math.PI / 2f, 8);
			birdNPC.Position = target;
			birdNPC.Facing = Facings.Left;
			birdNPC.Sprite.Rate = 1f;
			birdNPC.Sprite.Play("idle");
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
	private sealed class _003CMoveToNodesRoutine_003Ed__40 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		private int _003Cindex_003E5__2;

		private Vector2 _003Cspeed_003E5__3;

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
		public _003CMoveToNodesRoutine_003Ed__40(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			Player entity;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cindex_003E5__2 = 0;
				goto IL_0038;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0038;
			case 2:
				_003C_003E1__state = -1;
				if (_003Cindex_003E5__2 < birdNPC.nodes.Length)
				{
					_003C_003E2__current = birdNPC.FlyTo(birdNPC.nodes[_003Cindex_003E5__2], 0.6f);
					_003C_003E1__state = 3;
					return true;
				}
				birdNPC.Tag = Tags.Persistent;
				birdNPC.Add(new SoundSource().Play("event:/new_content/game/10_farewell/bird_relocate"));
				if (birdNPC.onlyOnce)
				{
					birdNPC.level.Session.DoNotLoad.Add(birdNPC.EntityID);
				}
				birdNPC.Sprite.Play("fly");
				birdNPC.Facing = Facings.Right;
				_003Cspeed_003E5__3 = new Vector2((int)birdNPC.Facing * 20, -40f);
				goto IL_0208;
			case 3:
				_003C_003E1__state = -1;
				_003Cindex_003E5__2++;
				goto IL_0038;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_0208;
				}
				IL_0038:
				entity = birdNPC.Scene.Tracker.GetEntity<Player>();
				if (entity == null || !((entity.Center - birdNPC.Position).Length() < 80f))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				birdNPC.Depth = -1000000;
				_003C_003E2__current = birdNPC.Startle("event:/new_content/game/10_farewell/bird_startle", 0.2f);
				_003C_003E1__state = 2;
				return true;
				IL_0208:
				if (birdNPC.Y > (float)(birdNPC.level.Bounds.Top - 200))
				{
					_003Cspeed_003E5__3 += new Vector2((int)birdNPC.Facing * 140, -60f) * Engine.DeltaTime;
					birdNPC.Position += _003Cspeed_003E5__3 * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				birdNPC.RemoveSelf();
				_003Cspeed_003E5__3 = default(Vector2);
				goto IL_0038;
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
	private sealed class _003CWaitForLightningOffRoutine_003Ed__41 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdNPC _003C_003E4__this;

		private Vector2 _003Cspeed_003E5__2;

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
		public _003CWaitForLightningOffRoutine_003Ed__41(int _003C_003E1__state)
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
			BirdNPC birdNPC = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				birdNPC.Sprite.Play("hoverStressed");
				goto IL_0043;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0043;
			case 2:
				_003C_003E1__state = -1;
				goto IL_009c;
			case 3:
				_003C_003E1__state = -1;
				goto IL_013f;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_01fa;
				}
				IL_0043:
				if (birdNPC.Scene.Entities.FindFirst<Lightning>() != null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (birdNPC.WaitForLightningPostDelay > 0f)
				{
					_003C_003E2__current = birdNPC.WaitForLightningPostDelay;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_009c;
				IL_013f:
				if (birdNPC.Y > (float)birdNPC.level.Bounds.Top)
				{
					_003Cspeed_003E5__2 += new Vector2((int)birdNPC.Facing * 140, -10f) * Engine.DeltaTime;
					birdNPC.Position += _003Cspeed_003E5__2 * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003Cspeed_003E5__2 = default(Vector2);
				break;
				IL_01fa:
				if (birdNPC.Y > (float)birdNPC.level.Bounds.Top)
				{
					_003Cspeed_003E5__2 += new Vector2(0f, -100f) * Engine.DeltaTime;
					birdNPC.Position += _003Cspeed_003E5__2 * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003Cspeed_003E5__2 = default(Vector2);
				break;
				IL_009c:
				if (!birdNPC.FlyAwayUp)
				{
					birdNPC.Sprite.Play("fly");
					_003Cspeed_003E5__2 = new Vector2((int)birdNPC.Facing * 20, -10f);
					goto IL_013f;
				}
				birdNPC.Sprite.Play("flyup");
				_003Cspeed_003E5__2 = new Vector2(0f, -32f);
				goto IL_01fa;
			}
			birdNPC.RemoveSelf();
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

	public static ParticleType P_Feather;

	private static string FlownFlag = "bird_fly_away_";

	public Facings Facing = Facings.Left;

	public Sprite Sprite;

	public Vector2 StartPosition;

	public VertexLight Light;

	public bool AutoFly;

	public EntityID EntityID;

	public bool FlyAwayUp = true;

	public float WaitForLightningPostDelay;

	public bool DisableFlapSfx;

	private Coroutine tutorialRoutine;

	private Modes mode;

	private BirdTutorialGui gui;

	private Level level;

	private Vector2[] nodes;

	private StaticMover staticMover;

	private bool onlyOnce;

	private bool onlyIfPlayerLeft;

	public BirdNPC(Vector2 position, Modes mode)
		: base(position)
	{
		Add(Sprite = GFX.SpriteBank.Create("bird"));
		Sprite.Scale.X = (float)Facing;
		Sprite.UseRawDeltaTime = true;
		Sprite.OnFrameChange = delegate(string spr)
		{
			if (level != null && base.X > level.Camera.Left + 64f && base.X < level.Camera.Right - 64f && (spr.Equals("peck") || spr.Equals("peckRare")) && Sprite.CurrentAnimationFrame == 6)
			{
				Audio.Play("event:/game/general/bird_peck", Position);
			}
			if (level != null && level.Session.Area.ID == 10 && !DisableFlapSfx)
			{
				FlapSfxCheck(Sprite);
			}
		};
		Add(Light = new VertexLight(new Vector2(0f, -8f), Color.White, 1f, 8, 32));
		StartPosition = Position;
		SetMode(mode);
	}

	public BirdNPC(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Enum("mode", Modes.None))
	{
		EntityID = new EntityID(data.Level.Name, data.ID);
		nodes = data.NodesOffset(offset);
		onlyOnce = data.Bool("onlyOnce");
		onlyIfPlayerLeft = data.Bool("onlyIfPlayerLeft");
	}

	public void SetMode(Modes mode)
	{
		this.mode = mode;
		if (tutorialRoutine != null)
		{
			tutorialRoutine.RemoveSelf();
		}
		switch (mode)
		{
		case Modes.ClimbingTutorial:
			Add(tutorialRoutine = new Coroutine(ClimbingTutorial()));
			break;
		case Modes.DashingTutorial:
			Add(tutorialRoutine = new Coroutine(DashingTutorial()));
			break;
		case Modes.DreamJumpTutorial:
			Add(tutorialRoutine = new Coroutine(DreamJumpTutorial()));
			break;
		case Modes.SuperWallJumpTutorial:
			Add(tutorialRoutine = new Coroutine(SuperWallJumpTutorial()));
			break;
		case Modes.HyperJumpTutorial:
			Add(tutorialRoutine = new Coroutine(HyperJumpTutorial()));
			break;
		case Modes.FlyAway:
			Add(tutorialRoutine = new Coroutine(WaitRoutine()));
			break;
		case Modes.Sleeping:
			Sprite.Play("sleep");
			Facing = Facings.Right;
			break;
		case Modes.MoveToNodes:
			Add(tutorialRoutine = new Coroutine(MoveToNodesRoutine()));
			break;
		case Modes.WaitForLightningOff:
			Add(tutorialRoutine = new Coroutine(WaitForLightningOffRoutine()));
			break;
		}
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = scene as Level;
		if (mode == Modes.ClimbingTutorial && level.Session.GetLevelFlag("2"))
		{
			RemoveSelf();
		}
		else if (mode == Modes.FlyAway && level.Session.GetFlag(FlownFlag + level.Session.Level))
		{
			RemoveSelf();
		}
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (mode == Modes.SuperWallJumpTutorial)
		{
			Player entity = scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.Y < base.Y + 32f)
			{
				RemoveSelf();
			}
		}
		if (onlyIfPlayerLeft)
		{
			Player entity2 = level.Tracker.GetEntity<Player>();
			if (entity2 != null && entity2.X > base.X)
			{
				RemoveSelf();
			}
		}
	}

	public override bool IsRiding(Solid solid)
	{
		return base.Scene.CollideCheck(new Rectangle((int)base.X - 4, (int)base.Y, 8, 2), solid);
	}

	public override void Update()
	{
		Sprite.Scale.X = (float)Facing;
		base.Update();
	}

	[IteratorStateMachine(typeof(_003CCaw_003Ed__27))]
	public IEnumerator Caw()
	{
		Sprite.Play("croak");
		while (Sprite.CurrentAnimationFrame < 9)
		{
			yield return null;
		}
		Audio.Play("event:/game/general/bird_squawk", Position);
	}

	[IteratorStateMachine(typeof(_003CShowTutorial_003Ed__28))]
	public IEnumerator ShowTutorial(BirdTutorialGui gui, bool caw = false)
	{
		if (caw)
		{
			yield return Caw();
		}
		this.gui = gui;
		gui.Open = true;
		base.Scene.Add(gui);
		while (gui.Scale < 1f)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CHideTutorial_003Ed__29))]
	public IEnumerator HideTutorial()
	{
		if (gui != null)
		{
			gui.Open = false;
			while (gui.Scale > 0f)
			{
				yield return null;
			}
			base.Scene.Remove(gui);
			gui = null;
		}
	}

	[IteratorStateMachine(typeof(_003CStartleAndFlyAway_003Ed__30))]
	public IEnumerator StartleAndFlyAway()
	{
		base.Depth = -1000000;
		level.Session.SetFlag(FlownFlag + level.Session.Level);
		yield return Startle("event:/game/general/bird_startle");
		yield return FlyAway();
	}

	[IteratorStateMachine(typeof(_003CFlyAway_003Ed__31))]
	public IEnumerator FlyAway(float upwardsMultiplier = 1f)
	{
		if (staticMover != null)
		{
			staticMover.RemoveSelf();
			staticMover = null;
		}
		Sprite.Play("fly");
		Facing = (Facings)(0 - Facing);
		Vector2 speed = new Vector2((int)Facing * 20, -40f * upwardsMultiplier);
		while (base.Y > (float)level.Bounds.Top)
		{
			speed += new Vector2((int)Facing * 140, -120f * upwardsMultiplier) * Engine.DeltaTime;
			Position += speed * Engine.DeltaTime;
			yield return null;
		}
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CClimbingTutorial_003Ed__32))]
	private IEnumerator ClimbingTutorial()
	{
		yield return 0.25f;
		Player p = base.Scene.Tracker.GetEntity<Player>();
		while (Math.Abs(p.X - base.X) > 120f)
		{
			yield return null;
		}
		BirdTutorialGui tut1 = new BirdTutorialGui(this, new Vector2(0f, -16f), Dialog.Clean("tutorial_climb"), Dialog.Clean("tutorial_hold"), BirdTutorialGui.ButtonPrompt.Grab);
		BirdTutorialGui tut2 = new BirdTutorialGui(this, new Vector2(0f, -16f), Dialog.Clean("tutorial_climb"), BirdTutorialGui.ButtonPrompt.Grab, "+", new Vector2(0f, -1f));
		bool first = true;
		bool willEnd;
		do
		{
			yield return ShowTutorial(tut1, first);
			first = false;
			while (p.StateMachine.State != 1 && p.Y > base.Y)
			{
				yield return null;
			}
			if (p.Y > base.Y)
			{
				Audio.Play("event:/ui/game/tutorial_note_flip_back");
				yield return HideTutorial();
				yield return ShowTutorial(tut2);
			}
			while (p.Scene != null && (!p.OnGround() || p.StateMachine.State == 1))
			{
				yield return null;
			}
			willEnd = p.Y <= base.Y + 4f;
			if (!willEnd)
			{
				Audio.Play("event:/ui/game/tutorial_note_flip_front");
			}
			yield return HideTutorial();
		}
		while (!willEnd);
		yield return StartleAndFlyAway();
	}

	[IteratorStateMachine(typeof(_003CDashingTutorial_003Ed__33))]
	private IEnumerator DashingTutorial()
	{
		base.Y = level.Bounds.Top;
		base.X += 32f;
		yield return 1f;
		Player player = base.Scene.Tracker.GetEntity<Player>();
		Bridge bridge = base.Scene.Entities.FindFirst<Bridge>();
		while ((player == null || !(player.X > StartPosition.X - 92f) || !(player.Y > StartPosition.Y - 20f) || !(player.Y < StartPosition.Y - 10f)) && (!SaveData.Instance.Assists.Invincible || player == null || !(player.X > StartPosition.X - 60f) || !(player.Y > StartPosition.Y) || !(player.Y < StartPosition.Y + 34f)))
		{
			yield return null;
		}
		base.Scene.Add(new CS00_Ending(player, this, bridge));
	}

	[IteratorStateMachine(typeof(_003CDreamJumpTutorial_003Ed__34))]
	private IEnumerator DreamJumpTutorial()
	{
		yield return ShowTutorial(new BirdTutorialGui(this, new Vector2(0f, -16f), Dialog.Clean("tutorial_dreamjump"), new Vector2(1f, 0f), "+", BirdTutorialGui.ButtonPrompt.Jump), caw: true);
		while (true)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && (entity.X > base.X || (Position - entity.Position).Length() < 32f))
			{
				break;
			}
			yield return null;
		}
		yield return HideTutorial();
		while (true)
		{
			Player entity2 = base.Scene.Tracker.GetEntity<Player>();
			if (entity2 != null && (Position - entity2.Position).Length() < 24f)
			{
				break;
			}
			yield return null;
		}
		yield return StartleAndFlyAway();
	}

	[IteratorStateMachine(typeof(_003CSuperWallJumpTutorial_003Ed__35))]
	private IEnumerator SuperWallJumpTutorial()
	{
		Facing = Facings.Right;
		yield return 0.25f;
		bool caw = true;
		BirdTutorialGui tut1 = new BirdTutorialGui(this, new Vector2(0f, -16f), GFX.Gui["hyperjump/tutorial00"], Dialog.Clean("TUTORIAL_DASH"), new Vector2(0f, -1f));
		BirdTutorialGui tut2 = new BirdTutorialGui(this, new Vector2(0f, -16f), GFX.Gui["hyperjump/tutorial01"], Dialog.Clean("TUTORIAL_DREAMJUMP"));
		Player entity;
		do
		{
			yield return ShowTutorial(tut1, caw);
			Sprite.Play("idleRarePeck");
			yield return 2f;
			gui = tut2;
			gui.Open = true;
			gui.Scale = 1f;
			base.Scene.Add(gui);
			yield return null;
			tut1.Open = false;
			tut1.Scale = 0f;
			base.Scene.Remove(tut1);
			yield return 2f;
			yield return HideTutorial();
			yield return 2f;
			caw = false;
			entity = base.Scene.Tracker.GetEntity<Player>();
		}
		while (entity == null || !(entity.Y <= base.Y) || !(entity.X > base.X + 144f));
		yield return StartleAndFlyAway();
	}

	[IteratorStateMachine(typeof(_003CHyperJumpTutorial_003Ed__36))]
	private IEnumerator HyperJumpTutorial()
	{
		Facing = Facings.Left;
		BirdTutorialGui tut = new BirdTutorialGui(this, new Vector2(0f, -16f), Dialog.Clean("TUTORIAL_DREAMJUMP"), new Vector2(1f, 1f), "+", BirdTutorialGui.ButtonPrompt.Dash, GFX.Gui["tinyarrow"], BirdTutorialGui.ButtonPrompt.Jump);
		yield return 0.3f;
		yield return ShowTutorial(tut, caw: true);
	}

	[IteratorStateMachine(typeof(_003CWaitRoutine_003Ed__37))]
	private IEnumerator WaitRoutine()
	{
		while (!AutoFly)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && Math.Abs(entity.X - base.X) < 120f)
			{
				break;
			}
			yield return null;
		}
		yield return Caw();
		while (!AutoFly)
		{
			Player entity2 = base.Scene.Tracker.GetEntity<Player>();
			if (entity2 != null && (entity2.Center - Position).Length() < 32f)
			{
				break;
			}
			yield return null;
		}
		yield return StartleAndFlyAway();
	}

	[IteratorStateMachine(typeof(_003CStartle_003Ed__38))]
	public IEnumerator Startle(string startleSound, float duration = 0.8f, Vector2? multiplier = null)
	{
		if (!multiplier.HasValue)
		{
			multiplier = new Vector2(1f, 1f);
		}
		if (!string.IsNullOrWhiteSpace(startleSound))
		{
			Audio.Play(startleSound, Position);
		}
		Dust.Burst(Position, -(float)Math.PI / 2f, 8);
		Sprite.Play("jump");
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, duration, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			if (t.Eased < 0.5f && base.Scene.OnInterval(0.05f))
			{
				level.Particles.Emit(P_Feather, 2, Position + Vector2.UnitY * -6f, Vector2.One * 4f);
			}
			Vector2 vector = Vector2.Lerp(new Vector2(100f, -100f) * multiplier.Value, new Vector2(20f, -20f) * multiplier.Value, t.Eased);
			vector.X *= 0 - Facing;
			Position += vector * Engine.DeltaTime;
		};
		Add(tween);
		while (tween.Active)
		{
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CFlyTo_003Ed__39))]
	public IEnumerator FlyTo(Vector2 target, float durationMult = 1f, bool relocateSfx = true)
	{
		Sprite.Play("fly");
		if (relocateSfx)
		{
			Add(new SoundSource().Play("event:/new_content/game/10_farewell/bird_relocate"));
		}
		int num = Math.Sign(target.X - base.X);
		if (num != 0)
		{
			Facing = (Facings)num;
		}
		Vector2 position = Position;
		Vector2 vector = target;
		SimpleCurve curve = new SimpleCurve(position, vector, position + (vector - position) * 0.75f - Vector2.UnitY * 30f);
		float duration = (vector - position).Length() / 100f * durationMult;
		for (float p = 0f; p < 0.95f; p += Engine.DeltaTime / duration)
		{
			Position = curve.GetPoint(Ease.SineInOut(p)).Floor();
			Sprite.Rate = 1f - p * 0.5f;
			yield return null;
		}
		Dust.Burst(Position, -(float)Math.PI / 2f, 8);
		Position = target;
		Facing = Facings.Left;
		Sprite.Rate = 1f;
		Sprite.Play("idle");
	}

	[IteratorStateMachine(typeof(_003CMoveToNodesRoutine_003Ed__40))]
	private IEnumerator MoveToNodesRoutine()
	{
		int index = 0;
		while (true)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity == null || !((entity.Center - Position).Length() < 80f))
			{
				yield return null;
				continue;
			}
			base.Depth = -1000000;
			yield return Startle("event:/new_content/game/10_farewell/bird_startle", 0.2f);
			if (index < nodes.Length)
			{
				yield return FlyTo(nodes[index], 0.6f);
				index++;
				continue;
			}
			base.Tag = Tags.Persistent;
			Add(new SoundSource().Play("event:/new_content/game/10_farewell/bird_relocate"));
			if (onlyOnce)
			{
				level.Session.DoNotLoad.Add(EntityID);
			}
			Sprite.Play("fly");
			Facing = Facings.Right;
			Vector2 speed = new Vector2((int)Facing * 20, -40f);
			while (base.Y > (float)(level.Bounds.Top - 200))
			{
				speed += new Vector2((int)Facing * 140, -60f) * Engine.DeltaTime;
				Position += speed * Engine.DeltaTime;
				yield return null;
			}
			RemoveSelf();
		}
	}

	[IteratorStateMachine(typeof(_003CWaitForLightningOffRoutine_003Ed__41))]
	private IEnumerator WaitForLightningOffRoutine()
	{
		Sprite.Play("hoverStressed");
		while (base.Scene.Entities.FindFirst<Lightning>() != null)
		{
			yield return null;
		}
		if (WaitForLightningPostDelay > 0f)
		{
			yield return WaitForLightningPostDelay;
		}
		if (!FlyAwayUp)
		{
			Sprite.Play("fly");
			Vector2 speed = new Vector2((int)Facing * 20, -10f);
			while (base.Y > (float)level.Bounds.Top)
			{
				speed += new Vector2((int)Facing * 140, -10f) * Engine.DeltaTime;
				Position += speed * Engine.DeltaTime;
				yield return null;
			}
		}
		else
		{
			Sprite.Play("flyup");
			Vector2 speed = new Vector2(0f, -32f);
			while (base.Y > (float)level.Bounds.Top)
			{
				speed += new Vector2(0f, -100f) * Engine.DeltaTime;
				Position += speed * Engine.DeltaTime;
				yield return null;
			}
		}
		RemoveSelf();
	}

	public override void SceneEnd(Scene scene)
	{
		Engine.TimeRate = 1f;
		base.SceneEnd(scene);
	}

	public override void DebugRender(Camera camera)
	{
		base.DebugRender(camera);
		if (mode == Modes.DashingTutorial)
		{
			float x = StartPosition.X - 92f;
			float x2 = level.Bounds.Right;
			float y = StartPosition.Y - 20f;
			float y2 = StartPosition.Y - 10f;
			Draw.Line(new Vector2(x, y), new Vector2(x, y2), Color.Aqua);
			Draw.Line(new Vector2(x, y), new Vector2(x2, y), Color.Aqua);
			Draw.Line(new Vector2(x2, y), new Vector2(x2, y2), Color.Aqua);
			Draw.Line(new Vector2(x, y2), new Vector2(x2, y2), Color.Aqua);
			float x3 = StartPosition.X - 60f;
			float x4 = level.Bounds.Right;
			float y3 = StartPosition.Y;
			float y4 = StartPosition.Y + 34f;
			Draw.Line(new Vector2(x3, y3), new Vector2(x3, y4), Color.Aqua);
			Draw.Line(new Vector2(x3, y3), new Vector2(x4, y3), Color.Aqua);
			Draw.Line(new Vector2(x4, y3), new Vector2(x4, y4), Color.Aqua);
			Draw.Line(new Vector2(x3, y4), new Vector2(x4, y4), Color.Aqua);
		}
	}

	public static void FlapSfxCheck(Sprite sprite)
	{
		if (sprite.Entity != null && sprite.Entity.Scene != null)
		{
			Camera camera = (sprite.Entity.Scene as Level).Camera;
			Vector2 renderPosition = sprite.RenderPosition;
			if (renderPosition.X < camera.X - 32f || renderPosition.Y < camera.Y - 32f || renderPosition.X > camera.X + 320f + 32f || renderPosition.Y > camera.Y + 180f + 32f)
			{
				return;
			}
		}
		string currentAnimationID = sprite.CurrentAnimationID;
		int currentAnimationFrame = sprite.CurrentAnimationFrame;
		if ((currentAnimationID == "hover" && currentAnimationFrame == 0) || (currentAnimationID == "hoverStressed" && currentAnimationFrame == 0) || (currentAnimationID == "fly" && currentAnimationFrame == 0))
		{
			Audio.Play("event:/new_content/game/10_farewell/bird_wingflap", sprite.RenderPosition);
		}
	}
}
