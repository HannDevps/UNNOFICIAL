using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS05_SaveTheo : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SaveTheo _003C_003E4__this;

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
		public _003CCutscene_003Ed__7(int _003C_003E1__state)
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
			CS05_SaveTheo cS05_SaveTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_SaveTheo.player.StateMachine.State = 11;
				cS05_SaveTheo.player.StateMachine.Locked = true;
				cS05_SaveTheo.player.ForceCameraUpdate = true;
				level.Session.Audio.Music.Layer(6, 0f);
				level.Session.Audio.Apply();
				_003C_003E2__current = cS05_SaveTheo.player.DummyWalkTo(cS05_SaveTheo.theo.X - 18f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS05_SaveTheo.player.Facing = Facings.Right;
				_003C_003E2__current = Textbox.Say("ch5_found_theo", cS05_SaveTheo.TryToBreakCrystal);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_SaveTheo.Level.ZoomBack(0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS05_SaveTheo.EndCutscene(level);
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
	private sealed class _003CTryToBreakCrystal_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS05_SaveTheo _003C_003E4__this;

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
		public _003CTryToBreakCrystal_003Ed__8(int _003C_003E1__state)
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
			CS05_SaveTheo cS05_SaveTheo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS05_SaveTheo.Scene.Entities.FindFirst<TheoCrystalPedestal>().Collidable = true;
				_003C_003E2__current = cS05_SaveTheo.player.DummyWalkTo(cS05_SaveTheo.theo.X);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS05_SaveTheo.Level.ZoomTo(new Vector2(160f, 90f), 2f, 0.5f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS05_SaveTheo.player.DummyAutoAnimate = false;
				cS05_SaveTheo.player.Sprite.Play("lookUp");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS05_SaveTheo.wasDashAssistOn = SaveData.Instance.Assists.DashAssist;
				SaveData.Instance.Assists.DashAssist = false;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				MInput.Disabled = true;
				cS05_SaveTheo.player.OverrideDashDirection = new Vector2(0f, -1f);
				cS05_SaveTheo.player.StateMachine.Locked = false;
				cS05_SaveTheo.player.StateMachine.State = cS05_SaveTheo.player.StartDash();
				cS05_SaveTheo.player.Dashes = 0;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				goto IL_020b;
			case 6:
				_003C_003E1__state = -1;
				goto IL_020b;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_020b:
				if (!cS05_SaveTheo.player.OnGround() || cS05_SaveTheo.player.Speed.Y < 0f)
				{
					cS05_SaveTheo.player.Dashes = 0;
					Input.MoveY.Value = -1;
					Input.MoveX.Value = 0;
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				cS05_SaveTheo.player.OverrideDashDirection = null;
				cS05_SaveTheo.player.StateMachine.State = 11;
				cS05_SaveTheo.player.StateMachine.Locked = true;
				MInput.Disabled = false;
				cS05_SaveTheo.player.DummyAutoAnimate = true;
				_003C_003E2__current = cS05_SaveTheo.player.DummyWalkToExact((int)cS05_SaveTheo.playerEndPosition.X, walkBackwards: true);
				_003C_003E1__state = 7;
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

	public const string Flag = "foundTheoInCrystal";

	private Player player;

	private TheoCrystal theo;

	private Vector2 playerEndPosition;

	private bool wasDashAssistOn;

	public CS05_SaveTheo(Player player)
	{
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		theo = level.Tracker.GetEntity<TheoCrystal>();
		playerEndPosition = theo.Position + new Vector2(-24f, 0f);
		wasDashAssistOn = SaveData.Instance.Assists.DashAssist;
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.ForceCameraUpdate = true;
		level.Session.Audio.Music.Layer(6, 0f);
		level.Session.Audio.Apply();
		yield return player.DummyWalkTo(theo.X - 18f);
		player.Facing = Facings.Right;
		yield return Textbox.Say("ch5_found_theo", TryToBreakCrystal);
		yield return 0.25f;
		yield return Level.ZoomBack(0.5f);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CTryToBreakCrystal_003Ed__8))]
	private IEnumerator TryToBreakCrystal()
	{
		base.Scene.Entities.FindFirst<TheoCrystalPedestal>().Collidable = true;
		yield return player.DummyWalkTo(theo.X);
		yield return 0.1f;
		yield return Level.ZoomTo(new Vector2(160f, 90f), 2f, 0.5f);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("lookUp");
		yield return 1f;
		wasDashAssistOn = SaveData.Instance.Assists.DashAssist;
		SaveData.Instance.Assists.DashAssist = false;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		MInput.Disabled = true;
		player.OverrideDashDirection = new Vector2(0f, -1f);
		player.StateMachine.Locked = false;
		player.StateMachine.State = player.StartDash();
		player.Dashes = 0;
		yield return 0.1f;
		while (!player.OnGround() || player.Speed.Y < 0f)
		{
			player.Dashes = 0;
			Input.MoveY.Value = -1;
			Input.MoveX.Value = 0;
			yield return null;
		}
		player.OverrideDashDirection = null;
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		MInput.Disabled = false;
		player.DummyAutoAnimate = true;
		yield return player.DummyWalkToExact((int)playerEndPosition.X, walkBackwards: true);
		yield return 1.5f;
	}

	public override void OnEnd(Level level)
	{
		SaveData.Instance.Assists.DashAssist = wasDashAssistOn;
		player.Position = playerEndPosition;
		while (!player.OnGround())
		{
			player.MoveV(1f);
		}
		level.Camera.Position = player.CameraTarget;
		level.Session.SetFlag("foundTheoInCrystal");
		level.ResetZoom();
		level.Session.Audio.Music.Layer(6, 1f);
		level.Session.Audio.Apply();
		List<Follower> list = new List<Follower>(player.Leader.Followers);
		player.RemoveSelf();
		level.Add(player = new Player(player.Position, player.DefaultSpriteMode));
		foreach (Follower item in list)
		{
			player.Leader.Followers.Add(item);
			item.Leader = player.Leader;
		}
		player.Facing = Facings.Right;
		player.IntroType = Player.IntroTypes.None;
		TheoCrystalPedestal theoCrystalPedestal = base.Scene.Entities.FindFirst<TheoCrystalPedestal>();
		theoCrystalPedestal.Collidable = false;
		theoCrystalPedestal.DroppedTheo = true;
		theo.Depth = 100;
		theo.OnPedestal = false;
		theo.Speed = Vector2.Zero;
		while (!theo.OnGround())
		{
			theo.MoveV(1f);
		}
	}
}
