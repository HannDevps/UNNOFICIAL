using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS06_Ending : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CCutscene_003Ed__6(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_Ending.player.StateMachine.State = 11;
				cS06_Ending.player.StateMachine.Locked = true;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS06_Ending.player.Dashes = 1;
				level.Session.Inventory.Dashes = 1;
				level.Add(cS06_Ending.badeline = new BadelineDummy(cS06_Ending.player.Center));
				cS06_Ending.badeline.Appear(level, silent: true);
				cS06_Ending.badeline.FloatSpeed = 80f;
				cS06_Ending.badeline.Sprite.Scale.X = -1f;
				Audio.Play("event:/char/badeline/maddy_split", cS06_Ending.player.Center);
				_003C_003E2__current = cS06_Ending.badeline.FloatTo(cS06_Ending.player.Position + new Vector2(24f, -20f), -1, faceDirection: false);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomTo(new Vector2(160f, 120f), 2f, 1f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("ch6_ending", cS06_Ending.GrannyEnter, cS06_Ending.TheoEnter, cS06_Ending.MaddyTurnsRight, cS06_Ending.BadelineTurnsRight, cS06_Ending.BadelineTurnsLeft, cS06_Ending.WaitAbit, cS06_Ending.TurnToLeft, cS06_Ending.TheoRaiseFist, cS06_Ending.TheoStopTired);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/madeline/backpack_drop", cS06_Ending.player.Position);
				cS06_Ending.player.DummyAutoAnimate = false;
				cS06_Ending.player.Sprite.Play("bagdown");
				cS06_Ending.EndCutscene(level);
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
	private sealed class _003CGrannyEnter_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CGrannyEnter_003Ed__7(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
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
				cS06_Ending.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS06_Ending.granny.Visible = true;
				cS06_Ending.Add(new Coroutine(cS06_Ending.badeline.FloatTo(new Vector2(cS06_Ending.badeline.X - 10f, cS06_Ending.badeline.Y), 1, faceDirection: false)));
				_003C_003E2__current = cS06_Ending.granny.MoveTo(cS06_Ending.player.Position + new Vector2(40f, 0f));
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003CTheoEnter_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CTheoEnter_003Ed__8(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_Ending.player.Facing = Facings.Left;
				cS06_Ending.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(cS06_Ending.Level.Camera.X - 40f, cS06_Ending.Level.Camera.Y), 1f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS06_Ending.theo.Visible = true;
				cS06_Ending.Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(cS06_Ending.Level.Camera.X + 40f, cS06_Ending.Level.Camera.Y), 2f, null, 1f)));
				cS06_Ending.Add(new Coroutine(cS06_Ending.badeline.FloatTo(new Vector2(cS06_Ending.badeline.X + 6f, cS06_Ending.badeline.Y + 4f), -1, faceDirection: false)));
				_003C_003E2__current = cS06_Ending.theo.MoveTo(cS06_Ending.player.Position + new Vector2(-32f, 0f));
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS06_Ending.theo.Sprite.Play("tired");
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
	private sealed class _003CMaddyTurnsRight_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CMaddyTurnsRight_003Ed__9(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
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
				cS06_Ending.player.Facing = Facings.Right;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS06_Ending.badeline.FloatTo(cS06_Ending.badeline.Position + new Vector2(-2f, 10f), -1, faceDirection: false);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
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

	[CompilerGenerated]
	private sealed class _003CBadelineTurnsRight_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CBadelineTurnsRight_003Ed__10(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
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
				cS06_Ending.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CBadelineTurnsLeft_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CBadelineTurnsLeft_003Ed__11(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
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
				cS06_Ending.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CWaitAbit_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CWaitAbit_003Ed__12(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
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
	private sealed class _003CTurnToLeft_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CTurnToLeft_003Ed__13(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
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
				cS06_Ending.player.Facing = Facings.Left;
				_003C_003E2__current = 0.05f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS06_Ending.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003CTheoRaiseFist_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CTheoRaiseFist_003Ed__14(int _003C_003E1__state)
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
			CS06_Ending CS_0024_003C_003E8__locals3 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals3.theo.Sprite.Play("yolo");
				CS_0024_003C_003E8__locals3.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
				{
					CS_0024_003C_003E8__locals3.theo.Sprite.Play("yoloEnd");
				}, 0.8f, start: true));
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
	private sealed class _003CTheoStopTired_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS06_Ending _003C_003E4__this;

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
		public _003CTheoStopTired_003Ed__15(int _003C_003E1__state)
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
			CS06_Ending cS06_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS06_Ending.theo.Sprite.Play("idle");
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

	private Player player;

	private BadelineDummy badeline;

	private NPC granny;

	private NPC theo;

	public CS06_Ending(Player player, NPC granny)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.player = player;
		this.granny = granny;
	}

	public override void OnBegin(Level level)
	{
		level.RegisterAreaComplete();
		theo = base.Scene.Entities.FindFirst<NPC06_Theo_Ending>();
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return 1f;
		player.Dashes = 1;
		level.Session.Inventory.Dashes = 1;
		level.Add(badeline = new BadelineDummy(player.Center));
		badeline.Appear(level, silent: true);
		badeline.FloatSpeed = 80f;
		badeline.Sprite.Scale.X = -1f;
		Audio.Play("event:/char/badeline/maddy_split", player.Center);
		yield return badeline.FloatTo(player.Position + new Vector2(24f, -20f), -1, faceDirection: false);
		yield return level.ZoomTo(new Vector2(160f, 120f), 2f, 1f);
		yield return Textbox.Say("ch6_ending", GrannyEnter, TheoEnter, MaddyTurnsRight, BadelineTurnsRight, BadelineTurnsLeft, WaitAbit, TurnToLeft, TheoRaiseFist, TheoStopTired);
		Audio.Play("event:/char/madeline/backpack_drop", player.Position);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("bagdown");
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CGrannyEnter_003Ed__7))]
	private IEnumerator GrannyEnter()
	{
		yield return 0.25f;
		badeline.Sprite.Scale.X = 1f;
		yield return 0.1f;
		granny.Visible = true;
		Add(new Coroutine(badeline.FloatTo(new Vector2(badeline.X - 10f, badeline.Y), 1, faceDirection: false)));
		yield return granny.MoveTo(player.Position + new Vector2(40f, 0f));
	}

	[IteratorStateMachine(typeof(_003CTheoEnter_003Ed__8))]
	private IEnumerator TheoEnter()
	{
		player.Facing = Facings.Left;
		badeline.Sprite.Scale.X = -1f;
		yield return 0.25f;
		yield return CutsceneEntity.CameraTo(new Vector2(Level.Camera.X - 40f, Level.Camera.Y), 1f);
		theo.Visible = true;
		Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(Level.Camera.X + 40f, Level.Camera.Y), 2f, null, 1f)));
		Add(new Coroutine(badeline.FloatTo(new Vector2(badeline.X + 6f, badeline.Y + 4f), -1, faceDirection: false)));
		yield return theo.MoveTo(player.Position + new Vector2(-32f, 0f));
		theo.Sprite.Play("tired");
	}

	[IteratorStateMachine(typeof(_003CMaddyTurnsRight_003Ed__9))]
	private IEnumerator MaddyTurnsRight()
	{
		yield return 0.1f;
		player.Facing = Facings.Right;
		yield return 0.1f;
		yield return badeline.FloatTo(badeline.Position + new Vector2(-2f, 10f), -1, faceDirection: false);
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CBadelineTurnsRight_003Ed__10))]
	private IEnumerator BadelineTurnsRight()
	{
		yield return 0.1f;
		badeline.Sprite.Scale.X = 1f;
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CBadelineTurnsLeft_003Ed__11))]
	private IEnumerator BadelineTurnsLeft()
	{
		yield return 0.1f;
		badeline.Sprite.Scale.X = -1f;
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CWaitAbit_003Ed__12))]
	private IEnumerator WaitAbit()
	{
		yield return 0.4f;
	}

	[IteratorStateMachine(typeof(_003CTurnToLeft_003Ed__13))]
	private IEnumerator TurnToLeft()
	{
		yield return 0.1f;
		player.Facing = Facings.Left;
		yield return 0.05f;
		badeline.Sprite.Scale.X = -1f;
		yield return 0.1f;
	}

	[IteratorStateMachine(typeof(_003CTheoRaiseFist_003Ed__14))]
	private IEnumerator TheoRaiseFist()
	{
		theo.Sprite.Play("yolo");
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			theo.Sprite.Play("yoloEnd");
		}, 0.8f, start: true));
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CTheoStopTired_003Ed__15))]
	private IEnumerator TheoStopTired()
	{
		theo.Sprite.Play("idle");
		yield return null;
	}

	public override void OnEnd(Level level)
	{
		level.CompleteArea();
		SpotlightWipe.FocusPoint += new Vector2(0f, -20f);
	}
}
