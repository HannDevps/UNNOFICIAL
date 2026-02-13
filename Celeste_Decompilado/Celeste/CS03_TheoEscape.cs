using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_TheoEscape : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_TheoEscape _003C_003E4__this;

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
			CS03_TheoEscape cS03_TheoEscape = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_TheoEscape.player.StateMachine.State = 11;
				cS03_TheoEscape.player.StateMachine.Locked = true;
				_003C_003E2__current = cS03_TheoEscape.player.DummyWalkTo(cS03_TheoEscape.theo.X - 64f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_TheoEscape.player.Facing = Facings.Right;
				_003C_003E2__current = cS03_TheoEscape.Level.ZoomTo(new Vector2(240f, 135f), 2f, 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				Func<IEnumerator>[] events = new Func<IEnumerator>[4] { cS03_TheoEscape.StopRemovingVent, cS03_TheoEscape.StartRemoveVent, cS03_TheoEscape.RemoveVent, cS03_TheoEscape.GivePhone };
				string dialog = "CH3_THEO_INTRO";
				if (!SaveData.Instance.HasFlag("MetTheo"))
				{
					dialog = "CH3_THEO_NEVER_MET";
				}
				else if (!SaveData.Instance.HasFlag("TheoKnowsName"))
				{
					dialog = "CH3_THEO_NEVER_INTRODUCED";
				}
				_003C_003E2__current = Textbox.Say(dialog, events);
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.Sprite.Play("walk");
				goto IL_0200;
			case 5:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.X += 48f * Engine.DeltaTime;
				goto IL_0200;
			case 6:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/theo/resort_standtocrawl", cS03_TheoEscape.theo.Position);
				cS03_TheoEscape.theo.Sprite.Play("duck");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				if (cS03_TheoEscape.theo.Talker != null)
				{
					cS03_TheoEscape.theo.Talker.Active = false;
				}
				level.Session.SetFlag("resort_theo");
				cS03_TheoEscape.player.StateMachine.Locked = false;
				cS03_TheoEscape.player.StateMachine.State = 0;
				cS03_TheoEscape.theo.CrawlUntilOut();
				_003C_003E2__current = level.ZoomBack(0.5f);
				_003C_003E1__state = 8;
				return true;
			case 8:
				{
					_003C_003E1__state = -1;
					cS03_TheoEscape.EndCutscene(level);
					return false;
				}
				IL_0200:
				if (!cS03_TheoEscape.theo.CollideCheck<Solid>(cS03_TheoEscape.theo.Position + new Vector2(2f, 0f)))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				cS03_TheoEscape.theo.Sprite.Play("idle");
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 6;
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
	private sealed class _003CStartRemoveVent_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_TheoEscape _003C_003E4__this;

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
		public _003CStartRemoveVent_003Ed__7(int _003C_003E1__state)
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
			CS03_TheoEscape cS03_TheoEscape = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/theo/resort_vent_grab", cS03_TheoEscape.theo.Position);
				cS03_TheoEscape.theo.Sprite.Play("goToVent");
				_003C_003E2__current = 0.25f;
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
	private sealed class _003CStopRemovingVent_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_TheoEscape _003C_003E4__this;

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
		public _003CStopRemovingVent_003Ed__8(int _003C_003E1__state)
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
			CS03_TheoEscape cS03_TheoEscape = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.Sprite.Play("idle");
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.Sprite.Scale.X = -1f;
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
	private sealed class _003CRemoveVent_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_TheoEscape _003C_003E4__this;

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
		public _003CRemoveVent_003Ed__9(int _003C_003E1__state)
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
			CS03_TheoEscape cS03_TheoEscape = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/theo/resort_vent_rip", cS03_TheoEscape.theo.Position);
				cS03_TheoEscape.theo.Sprite.Play("fallVent");
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.grate.Fall();
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS03_TheoEscape.theo.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.25f;
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
	private sealed class _003CGivePhone_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_TheoEscape _003C_003E4__this;

		private Player _003Cplayer_003E5__2;

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
		public _003CGivePhone_003Ed__10(int _003C_003E1__state)
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
			CS03_TheoEscape cS03_TheoEscape = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cplayer_003E5__2 = cS03_TheoEscape.Scene.Tracker.GetEntity<Player>();
				if (_003Cplayer_003E5__2 != null)
				{
					cS03_TheoEscape.theo.Sprite.Play("walk");
					cS03_TheoEscape.theo.Sprite.Scale.X = -1f;
					goto IL_00b1;
				}
				goto IL_00cf;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00b1;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00cf:
				cS03_TheoEscape.theo.Sprite.Play("idle");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
				IL_00b1:
				if (cS03_TheoEscape.theo.X > _003Cplayer_003E5__2.X + 24f)
				{
					cS03_TheoEscape.theo.X -= 48f * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00cf;
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

	public const string Flag = "resort_theo";

	private NPC03_Theo_Escaping theo;

	private Player player;

	private Vector2 theoStart;

	public CS03_TheoEscape(NPC03_Theo_Escaping theo, Player player)
	{
		this.theo = theo;
		theoStart = theo.Position;
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return player.DummyWalkTo(theo.X - 64f);
		player.Facing = Facings.Right;
		yield return Level.ZoomTo(new Vector2(240f, 135f), 2f, 0.5f);
		Func<IEnumerator>[] events = new Func<IEnumerator>[4] { StopRemovingVent, StartRemoveVent, RemoveVent, GivePhone };
		string dialog = "CH3_THEO_INTRO";
		if (!SaveData.Instance.HasFlag("MetTheo"))
		{
			dialog = "CH3_THEO_NEVER_MET";
		}
		else if (!SaveData.Instance.HasFlag("TheoKnowsName"))
		{
			dialog = "CH3_THEO_NEVER_INTRODUCED";
		}
		yield return Textbox.Say(dialog, events);
		theo.Sprite.Scale.X = 1f;
		yield return 0.2f;
		theo.Sprite.Play("walk");
		while (!theo.CollideCheck<Solid>(theo.Position + new Vector2(2f, 0f)))
		{
			yield return null;
			theo.X += 48f * Engine.DeltaTime;
		}
		theo.Sprite.Play("idle");
		yield return 0.2f;
		Audio.Play("event:/char/theo/resort_standtocrawl", theo.Position);
		theo.Sprite.Play("duck");
		yield return 0.5f;
		if (theo.Talker != null)
		{
			theo.Talker.Active = false;
		}
		level.Session.SetFlag("resort_theo");
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		theo.CrawlUntilOut();
		yield return level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CStartRemoveVent_003Ed__7))]
	private IEnumerator StartRemoveVent()
	{
		theo.Sprite.Scale.X = 1f;
		yield return 0.1f;
		Audio.Play("event:/char/theo/resort_vent_grab", theo.Position);
		theo.Sprite.Play("goToVent");
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CStopRemovingVent_003Ed__8))]
	private IEnumerator StopRemovingVent()
	{
		theo.Sprite.Play("idle");
		yield return 0.1f;
		theo.Sprite.Scale.X = -1f;
	}

	[IteratorStateMachine(typeof(_003CRemoveVent_003Ed__9))]
	private IEnumerator RemoveVent()
	{
		yield return 0.8f;
		Audio.Play("event:/char/theo/resort_vent_rip", theo.Position);
		theo.Sprite.Play("fallVent");
		yield return 0.8f;
		theo.grate.Fall();
		yield return 0.8f;
		theo.Sprite.Scale.X = -1f;
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CGivePhone_003Ed__10))]
	private IEnumerator GivePhone()
	{
		Player player = base.Scene.Tracker.GetEntity<Player>();
		if (player != null)
		{
			theo.Sprite.Play("walk");
			theo.Sprite.Scale.X = -1f;
			while (theo.X > player.X + 24f)
			{
				theo.X -= 48f * Engine.DeltaTime;
				yield return null;
			}
		}
		theo.Sprite.Play("idle");
		yield return 1f;
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		level.Session.SetFlag("resort_theo");
		SaveData.Instance.SetFlag("MetTheo");
		SaveData.Instance.SetFlag("TheoKnowsName");
		if (theo != null && WasSkipped)
		{
			theo.Position = theoStart;
			theo.CrawlUntilOut();
			if (theo.grate != null)
			{
				theo.grate.RemoveSelf();
			}
		}
	}
}
