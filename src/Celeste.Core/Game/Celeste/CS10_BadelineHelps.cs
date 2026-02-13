using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_BadelineHelps : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public CS10_BadelineHelps _003C_003E4__this;

		private Vector2 _003Cspawn_003E5__2;

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
			CS10_BadelineHelps cS10_BadelineHelps = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cspawn_003E5__2 = level.GetSpawnPoint(cS10_BadelineHelps.player.Position);
				cS10_BadelineHelps.player.Dashes = 2;
				cS10_BadelineHelps.player.StateMachine.State = 11;
				cS10_BadelineHelps.player.DummyGravity = false;
				cS10_BadelineHelps.entrySfx = Audio.Play("event:/new_content/char/madeline/screenentry_stubborn", cS10_BadelineHelps.player.Position);
				_003C_003E2__current = cS10_BadelineHelps.player.MoonLanding(_003Cspawn_003E5__2);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomTo(new Vector2(_003Cspawn_003E5__2.X - level.Camera.X, 134f), 2f, 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_BadelineHelps.BadelineAppears();
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_HELPING_HAND", cS10_BadelineHelps.MadelineFacesAway, cS10_BadelineHelps.MadelineFacesBadeline, cS10_BadelineHelps.MadelineStepsForwards);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				if (cS10_BadelineHelps.badeline != null)
				{
					_003C_003E2__current = cS10_BadelineHelps.BadelineVanishes();
					_003C_003E1__state = 7;
					return true;
				}
				goto IL_01ed;
			case 7:
				_003C_003E1__state = -1;
				goto IL_01ed;
			case 8:
				{
					_003C_003E1__state = -1;
					cS10_BadelineHelps.EndCutscene(level);
					return false;
				}
				IL_01ed:
				_003C_003E2__current = level.ZoomBack(0.5f);
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
	private sealed class _003CBadelineAppears_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_BadelineHelps _003C_003E4__this;

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
		public _003CBadelineAppears_003Ed__7(int _003C_003E1__state)
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
			CS10_BadelineHelps cS10_BadelineHelps = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_BadelineHelps.StartMusic();
				Audio.Play("event:/char/badeline/maddy_split", cS10_BadelineHelps.player.Position);
				cS10_BadelineHelps.Level.Add(cS10_BadelineHelps.badeline = new BadelineDummy(cS10_BadelineHelps.player.Center));
				cS10_BadelineHelps.Level.Displacement.AddBurst(cS10_BadelineHelps.badeline.Center, 0.5f, 8f, 32f, 0.5f);
				cS10_BadelineHelps.player.Dashes = 1;
				cS10_BadelineHelps.badeline.Sprite.Scale.X = -1f;
				_003C_003E2__current = cS10_BadelineHelps.badeline.FloatTo(cS10_BadelineHelps.player.Center + new Vector2(18f, -10f), -1, faceDirection: false);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_BadelineHelps.player.Facing = Facings.Right;
				_003C_003E2__current = null;
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
	private sealed class _003CMadelineFacesAway_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_BadelineHelps _003C_003E4__this;

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
		public _003CMadelineFacesAway_003Ed__8(int _003C_003E1__state)
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
			CS10_BadelineHelps cS10_BadelineHelps = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_BadelineHelps.Level.NextColorGrade("feelingdown", 0.1f);
				_003C_003E2__current = cS10_BadelineHelps.player.DummyWalkTo(cS10_BadelineHelps.player.X - 16f);
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
	private sealed class _003CMadelineFacesBadeline_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_BadelineHelps _003C_003E4__this;

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
		public _003CMadelineFacesBadeline_003Ed__9(int _003C_003E1__state)
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
			CS10_BadelineHelps cS10_BadelineHelps = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_BadelineHelps.player.Facing = Facings.Right;
				_003C_003E2__current = 0.2f;
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
	private sealed class _003CMadelineStepsForwards_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_BadelineHelps _003C_003E4__this;

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
		public _003CMadelineStepsForwards_003Ed__10(int _003C_003E1__state)
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
			CS10_BadelineHelps cS10_BadelineHelps = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Vector2 spawnPoint = cS10_BadelineHelps.Level.GetSpawnPoint(cS10_BadelineHelps.player.Position);
				cS10_BadelineHelps.Add(new Coroutine(cS10_BadelineHelps.player.DummyWalkToExact((int)spawnPoint.X)));
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_BadelineHelps.badeline.FloatTo(cS10_BadelineHelps.badeline.Position + new Vector2(20f, 0f), null, faceDirection: false);
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
	private sealed class _003CBadelineVanishes_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_BadelineHelps _003C_003E4__this;

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
		public _003CBadelineVanishes_003Ed__11(int _003C_003E1__state)
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
			CS10_BadelineHelps cS10_BadelineHelps = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_BadelineHelps.badeline.Vanish();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				cS10_BadelineHelps.badeline = null;
				_003C_003E2__current = 0.2f;
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

	public const string Flag = "badeline_helps";

	private Player player;

	private BadelineDummy badeline;

	private FMOD.Studio.EventInstance entrySfx;

	public CS10_BadelineHelps(Player player)
	{
		base.Depth = -8500;
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		Vector2 spawn = level.GetSpawnPoint(player.Position);
		player.Dashes = 2;
		player.StateMachine.State = 11;
		player.DummyGravity = false;
		entrySfx = Audio.Play("event:/new_content/char/madeline/screenentry_stubborn", player.Position);
		yield return player.MoonLanding(spawn);
		yield return level.ZoomTo(new Vector2(spawn.X - level.Camera.X, 134f), 2f, 0.5f);
		yield return 1f;
		yield return BadelineAppears();
		yield return 0.3f;
		yield return Textbox.Say("CH9_HELPING_HAND", MadelineFacesAway, MadelineFacesBadeline, MadelineStepsForwards);
		if (badeline != null)
		{
			yield return BadelineVanishes();
		}
		yield return level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CBadelineAppears_003Ed__7))]
	private IEnumerator BadelineAppears()
	{
		StartMusic();
		Audio.Play("event:/char/badeline/maddy_split", player.Position);
		Level.Add(badeline = new BadelineDummy(player.Center));
		Level.Displacement.AddBurst(badeline.Center, 0.5f, 8f, 32f, 0.5f);
		player.Dashes = 1;
		badeline.Sprite.Scale.X = -1f;
		yield return badeline.FloatTo(player.Center + new Vector2(18f, -10f), -1, faceDirection: false);
		yield return 0.2f;
		player.Facing = Facings.Right;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CMadelineFacesAway_003Ed__8))]
	private IEnumerator MadelineFacesAway()
	{
		Level.NextColorGrade("feelingdown", 0.1f);
		yield return player.DummyWalkTo(player.X - 16f);
	}

	[IteratorStateMachine(typeof(_003CMadelineFacesBadeline_003Ed__9))]
	private IEnumerator MadelineFacesBadeline()
	{
		player.Facing = Facings.Right;
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CMadelineStepsForwards_003Ed__10))]
	private IEnumerator MadelineStepsForwards()
	{
		Vector2 spawnPoint = Level.GetSpawnPoint(player.Position);
		Add(new Coroutine(player.DummyWalkToExact((int)spawnPoint.X)));
		yield return 0.1f;
		yield return badeline.FloatTo(badeline.Position + new Vector2(20f, 0f), null, faceDirection: false);
	}

	[IteratorStateMachine(typeof(_003CBadelineVanishes_003Ed__11))]
	private IEnumerator BadelineVanishes()
	{
		yield return 0.2f;
		badeline.Vanish();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		badeline = null;
		yield return 0.2f;
	}

	private void StartMusic()
	{
		if (Level.Session.Audio.Music.Event != "event:/new_content/music/lvl10/cassette_rooms")
		{
			int num = 0;
			CassetteBlockManager entity = Level.Tracker.GetEntity<CassetteBlockManager>();
			if (entity != null)
			{
				num = entity.GetSixteenthNote();
			}
			Level.Session.Audio.Music.Event = "event:/new_content/music/lvl10/cassette_rooms";
			Level.Session.Audio.Music.Param("sixteenth_note", num);
			Level.Session.Audio.Apply(forceSixteenthNoteHack: true);
			Level.Session.Audio.Music.Param("sixteenth_note", 7f);
		}
	}

	public override void OnEnd(Level level)
	{
		Level.Session.Inventory.Dashes = 1;
		player.Dashes = 1;
		player.Depth = 0;
		player.Dashes = 1;
		player.Speed = Vector2.Zero;
		player.Position = level.GetSpawnPoint(player.Position);
		player.Position -= Vector2.UnitY * 12f;
		player.MoveVExact(100);
		player.Active = true;
		player.Visible = true;
		player.StateMachine.State = 0;
		if (badeline != null)
		{
			badeline.RemoveSelf();
		}
		level.ResetZoom();
		level.Session.SetFlag("badeline_helps");
		if (WasSkipped)
		{
			Audio.Stop(entrySfx);
			StartMusic();
			level.SnapColorGrade("feelingdown");
		}
	}
}
