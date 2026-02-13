using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_MoonIntro : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MoonIntro _003C_003E4__this;

		public Level level;

		private float _003Ct_003E5__2;

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
			CS10_MoonIntro cS10_MoonIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_MoonIntro.player.StateMachine.State = 11;
				cS10_MoonIntro.player.Visible = false;
				cS10_MoonIntro.player.Active = false;
				cS10_MoonIntro.player.Dashes = 2;
				_003Ct_003E5__2 = 0f;
				goto IL_00d0;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__2 += Engine.DeltaTime / 0.9f;
				goto IL_00d0;
			case 2:
				_003C_003E1__state = -1;
				level.Camera.Position = new Vector2(cS10_MoonIntro.targetX, level.Camera.Y);
				if (cS10_MoonIntro.bird != null)
				{
					_003C_003E2__current = cS10_MoonIntro.bird.StartleAndFlyAway();
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_01e4;
			case 3:
				_003C_003E1__state = -1;
				level.Session.DoNotLoad.Add(cS10_MoonIntro.bird.EntityID);
				cS10_MoonIntro.bird = null;
				goto IL_01e4;
			case 4:
				_003C_003E1__state = -1;
				cS10_MoonIntro.player.Speed = Vector2.Zero;
				cS10_MoonIntro.player.Position = level.GetSpawnPoint(cS10_MoonIntro.player.Position);
				cS10_MoonIntro.player.Active = true;
				cS10_MoonIntro.player.StateMachine.State = 23;
				goto IL_026c;
			case 5:
				_003C_003E1__state = -1;
				goto IL_026c;
			case 6:
				_003C_003E1__state = -1;
				Audio.Play("event:/new_content/char/madeline/screenentry_lowgrav", cS10_MoonIntro.player.Position);
				goto IL_02dc;
			case 7:
				_003C_003E1__state = -1;
				goto IL_02dc;
			case 8:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_MoonIntro.BadelineAppears();
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH9_LANDING", cS10_MoonIntro.BadelineTurns, cS10_MoonIntro.BadelineVanishes);
				_003C_003E1__state = 10;
				return true;
			case 10:
				{
					_003C_003E1__state = -1;
					cS10_MoonIntro.EndCutscene(level);
					return false;
				}
				IL_02dc:
				if (cS10_MoonIntro.player.StateMachine.State == 23)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				cS10_MoonIntro.player.X = (int)cS10_MoonIntro.player.X;
				cS10_MoonIntro.player.Y = (int)cS10_MoonIntro.player.Y;
				while (!cS10_MoonIntro.player.OnGround() && cS10_MoonIntro.player.Bottom < (float)level.Bounds.Bottom)
				{
					cS10_MoonIntro.player.MoveVExact(16);
				}
				cS10_MoonIntro.player.StateMachine.State = 11;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 8;
				return true;
				IL_01e4:
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
				IL_026c:
				if (cS10_MoonIntro.player.Top > (float)level.Bounds.Bottom)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 5;
					return true;
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 6;
				return true;
				IL_00d0:
				if (_003Ct_003E5__2 < 1f)
				{
					level.Wipe.Percent = 0f;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS10_MoonIntro.Add(new Coroutine(cS10_MoonIntro.FadeIn(5f)));
				level.Camera.Position = level.LevelOffset + new Vector2(-100f, 0f);
				_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(cS10_MoonIntro.targetX, level.Camera.Y), 6f, Ease.SineOut);
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
	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public CS10_MoonIntro _003C_003E4__this;

		public int target;

		internal void _003CBadelineTurns_003Eb__0(float v)
		{
			_003C_003E4__this.badeline.Sprite.Scale = new Vector2(target, 1f) * (1f + 0.2f * v);
		}
	}

	[CompilerGenerated]
	private sealed class _003CBadelineTurns_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MoonIntro _003C_003E4__this;

		private _003C_003Ec__DisplayClass9_0 _003C_003E8__1;

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
		public _003CBadelineTurns_003Ed__9(int _003C_003E1__state)
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
			CS10_MoonIntro cS10_MoonIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass9_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				int num2 = Math.Sign(cS10_MoonIntro.badeline.Sprite.Scale.X);
				_003C_003E8__1.target = num2 * -1;
				Wiggler component = Wiggler.Create(0.5f, 3f, delegate(float v)
				{
					_003C_003E8__1._003C_003E4__this.badeline.Sprite.Scale = new Vector2(_003C_003E8__1.target, 1f) * (1f + 0.2f * v);
				}, start: true, removeSelfOnFinish: true);
				cS10_MoonIntro.Add(component);
				Audio.Play((_003C_003E8__1.target < 0) ? "event:/char/badeline/jump_wall_left" : "event:/char/badeline/jump_wall_left", cS10_MoonIntro.badeline.Position);
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 2;
				return true;
			}
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
	private sealed class _003CBadelineAppears_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MoonIntro _003C_003E4__this;

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
		public _003CBadelineAppears_003Ed__10(int _003C_003E1__state)
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
			CS10_MoonIntro cS10_MoonIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS10_MoonIntro.Level.Session.Inventory.Dashes = 1;
				cS10_MoonIntro.player.Dashes = 1;
				cS10_MoonIntro.Level.Add(cS10_MoonIntro.badeline = new BadelineDummy(cS10_MoonIntro.player.Position));
				cS10_MoonIntro.Level.Displacement.AddBurst(cS10_MoonIntro.player.Center, 0.5f, 8f, 32f, 0.5f);
				Audio.Play("event:/char/badeline/maddy_split", cS10_MoonIntro.player.Position);
				cS10_MoonIntro.badeline.Sprite.Scale.X = 1f;
				_003C_003E2__current = cS10_MoonIntro.badeline.FloatTo(cS10_MoonIntro.player.Position + new Vector2(-16f, -16f), 1, faceDirection: false);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_MoonIntro.player.Facing = Facings.Left;
				_003C_003E2__current = null;
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

		public CS10_MoonIntro _003C_003E4__this;

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
			CS10_MoonIntro cS10_MoonIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS10_MoonIntro.badeline.Vanish();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				cS10_MoonIntro.badeline = null;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS10_MoonIntro.player.Facing = Facings.Right;
				_003C_003E2__current = 0.6f;
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
	private sealed class _003CFadeIn_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_MoonIntro _003C_003E4__this;

		public float duration;

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
		public _003CFadeIn_003Ed__12(int _003C_003E1__state)
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
			CS10_MoonIntro cS10_MoonIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS10_MoonIntro.fade > 0f)
			{
				cS10_MoonIntro.fade = Calc.Approach(cS10_MoonIntro.fade, 0f, Engine.DeltaTime / duration);
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

	public const string Flag = "moon_intro";

	private Player player;

	private BadelineDummy badeline;

	private BirdNPC bird;

	private float fade = 1f;

	private float targetX;

	public CS10_MoonIntro(Player player)
	{
		base.Depth = -8500;
		this.player = player;
		targetX = player.CameraTarget.X + 8f;
	}

	public override void OnBegin(Level level)
	{
		bird = base.Scene.Entities.FindFirst<BirdNPC>();
		player.StateMachine.State = 11;
		if (level.Wipe != null)
		{
			level.Wipe.Cancel();
		}
		level.Wipe = new FadeWipe(level, wipeIn: true);
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__8))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.Visible = false;
		player.Active = false;
		player.Dashes = 2;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / 0.9f)
		{
			level.Wipe.Percent = 0f;
			yield return null;
		}
		Add(new Coroutine(FadeIn(5f)));
		level.Camera.Position = level.LevelOffset + new Vector2(-100f, 0f);
		yield return CutsceneEntity.CameraTo(new Vector2(targetX, level.Camera.Y), 6f, Ease.SineOut);
		level.Camera.Position = new Vector2(targetX, level.Camera.Y);
		if (bird != null)
		{
			yield return bird.StartleAndFlyAway();
			level.Session.DoNotLoad.Add(bird.EntityID);
			bird = null;
		}
		yield return 0.5f;
		player.Speed = Vector2.Zero;
		player.Position = level.GetSpawnPoint(player.Position);
		player.Active = true;
		player.StateMachine.State = 23;
		while (player.Top > (float)level.Bounds.Bottom)
		{
			yield return null;
		}
		yield return 0.2f;
		Audio.Play("event:/new_content/char/madeline/screenentry_lowgrav", player.Position);
		while (player.StateMachine.State == 23)
		{
			yield return null;
		}
		player.X = (int)player.X;
		player.Y = (int)player.Y;
		while (!player.OnGround() && player.Bottom < (float)level.Bounds.Bottom)
		{
			player.MoveVExact(16);
		}
		player.StateMachine.State = 11;
		yield return 0.5f;
		yield return BadelineAppears();
		yield return Textbox.Say("CH9_LANDING", BadelineTurns, BadelineVanishes);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CBadelineTurns_003Ed__9))]
	private IEnumerator BadelineTurns()
	{
		yield return 0.1f;
		int num = Math.Sign(badeline.Sprite.Scale.X);
		int target = num * -1;
		Wiggler component = Wiggler.Create(0.5f, 3f, delegate(float v)
		{
			badeline.Sprite.Scale = new Vector2(target, 1f) * (1f + 0.2f * v);
		}, start: true, removeSelfOnFinish: true);
		Add(component);
		Audio.Play((target < 0) ? "event:/char/badeline/jump_wall_left" : "event:/char/badeline/jump_wall_left", badeline.Position);
		yield return 0.6f;
	}

	[IteratorStateMachine(typeof(_003CBadelineAppears_003Ed__10))]
	private IEnumerator BadelineAppears()
	{
		Level.Session.Inventory.Dashes = 1;
		player.Dashes = 1;
		Level.Add(badeline = new BadelineDummy(player.Position));
		Level.Displacement.AddBurst(player.Center, 0.5f, 8f, 32f, 0.5f);
		Audio.Play("event:/char/badeline/maddy_split", player.Position);
		badeline.Sprite.Scale.X = 1f;
		yield return badeline.FloatTo(player.Position + new Vector2(-16f, -16f), 1, faceDirection: false);
		player.Facing = Facings.Left;
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CBadelineVanishes_003Ed__11))]
	private IEnumerator BadelineVanishes()
	{
		yield return 0.5f;
		badeline.Vanish();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		badeline = null;
		yield return 0.8f;
		player.Facing = Facings.Right;
		yield return 0.6f;
	}

	[IteratorStateMachine(typeof(_003CFadeIn_003Ed__12))]
	private IEnumerator FadeIn(float duration)
	{
		while (fade > 0f)
		{
			fade = Calc.Approach(fade, 0f, Engine.DeltaTime / duration);
			yield return null;
		}
	}

	public override void OnEnd(Level level)
	{
		level.Session.Inventory.Dashes = 1;
		player.Dashes = 1;
		player.Depth = 0;
		player.Speed = Vector2.Zero;
		player.Position = level.GetSpawnPoint(player.Position) + new Vector2(0f, -32f);
		player.Active = true;
		player.Visible = true;
		player.StateMachine.State = 0;
		player.X = (int)player.X;
		player.Y = (int)player.Y;
		while (!player.OnGround() && player.Bottom < (float)level.Bounds.Bottom)
		{
			player.MoveVExact(16);
		}
		if (badeline != null)
		{
			badeline.RemoveSelf();
		}
		if (bird != null)
		{
			bird.RemoveSelf();
			level.Session.DoNotLoad.Add(bird.EntityID);
		}
		level.Camera.Position = new Vector2(targetX, level.Camera.Y);
		level.Session.SetFlag("moon_intro");
	}

	public override void Render()
	{
		Camera camera = (base.Scene as Level).Camera;
		Draw.Rect(camera.X - 10f, camera.Y - 10f, 340f, 200f, Color.Black * fade);
	}
}
