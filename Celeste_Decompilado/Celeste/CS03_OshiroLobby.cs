using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_OshiroLobby : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public VertexLight light;

		public BloomPoint bloom;

		public Level level;

		public CS03_OshiroLobby _003C_003E4__this;

		public float endLightAlpha;

		internal void _003CCutscene_003Eb__1(Tween t)
		{
			level.Lighting.Alpha = MathHelper.Lerp(endLightAlpha, _003C_003E4__this.startLightAlpha, t.Percent);
			bloom.Alpha = 1f - t.Percent;
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass9_1
	{
		public Vector2 target;

		public _003C_003Ec__DisplayClass9_0 CS_0024_003C_003E8__locals1;

		internal void _003CCutscene_003Eb__0(Tween t)
		{
			CS_0024_003C_003E8__locals1.light.Alpha = (CS_0024_003C_003E8__locals1.bloom.Alpha = t.Percent);
			CS_0024_003C_003E8__locals1.light.Position = Vector2.Lerp(target - Vector2.UnitY * 48f, target, t.Percent);
			CS_0024_003C_003E8__locals1.level.Lighting.Alpha = MathHelper.Lerp(CS_0024_003C_003E8__locals1._003C_003E4__this.startLightAlpha, CS_0024_003C_003E8__locals1.endLightAlpha, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public CS03_OshiroLobby _003C_003E4__this;

		private _003C_003Ec__DisplayClass9_0 _003C_003E8__1;

		private float _003Cfrom_003E5__2;

		private Tween _003Ctween_003E5__3;

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
		public _003CCutscene_003Ed__9(int _003C_003E1__state)
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
			CS03_OshiroLobby cS03_OshiroLobby = _003C_003E4__this;
			_003C_003Ec__DisplayClass9_1 CS_0024_003C_003E8__locals15;
			Tween tween;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass9_0();
				_003C_003E8__1.level = level;
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				cS03_OshiroLobby.startLightAlpha = _003C_003E8__1.level.Lighting.Alpha;
				_003C_003E8__1.endLightAlpha = 1f;
				_003Cfrom_003E5__2 = cS03_OshiroLobby.oshiro.Y;
				cS03_OshiroLobby.player.StateMachine.State = 11;
				cS03_OshiroLobby.player.StateMachine.Locked = true;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroLobby.player.DummyWalkTo(cS03_OshiroLobby.oshiro.X - 16f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS03_OshiroLobby.player.Facing = Facings.Right;
				cS03_OshiroLobby.sfx.Play("event:/game/03_resort/sequence_oshiro_intro");
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				_003C_003E2__current = 1.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E8__1.level.Shake();
				_003C_003E8__1.level.Lighting.Alpha += 0.5f;
				goto IL_020a;
			case 4:
				_003C_003E1__state = -1;
				goto IL_020a;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003C_003E8__1.level.ZoomTo(new Vector2(170f, 126f), 2f, 0.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				_003C_003E8__1.level.Shake();
				cS03_OshiroLobby.oshiro.Sprite.Visible = true;
				cS03_OshiroLobby.oshiro.Sprite.Play("appear");
				_003C_003E2__current = cS03_OshiroLobby.player.DummyWalkToExact((int)(cS03_OshiroLobby.player.X - 12f), walkBackwards: true);
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				cS03_OshiroLobby.player.DummyAutoAnimate = false;
				cS03_OshiroLobby.player.Sprite.Play("shaking");
				Input.Rumble(RumbleStrength.Medium, RumbleLength.FullSecond);
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				cS03_OshiroLobby.createSparks = true;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				cS03_OshiroLobby.createSparks = false;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E8__1.level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				_003C_003E2__current = 1.4f;
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				_003C_003E8__1.level.Lighting.UnsetSpotlight();
				_003Ctween_003E5__3 = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 0.5f, start: true);
				_003Ctween_003E5__3.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1.level.Lighting.Alpha = MathHelper.Lerp(_003C_003E8__1.endLightAlpha, _003C_003E8__1._003C_003E4__this.startLightAlpha, t.Percent);
					_003C_003E8__1.bloom.Alpha = 1f - t.Percent;
				};
				cS03_OshiroLobby.Add(_003Ctween_003E5__3);
				goto IL_05b9;
			case 14:
				_003C_003E1__state = -1;
				goto IL_05b9;
			case 15:
				_003C_003E1__state = -1;
				_003Ctween_003E5__3 = null;
				Audio.SetMusic("event:/music/lvl3/oshiro_theme");
				cS03_OshiroLobby.player.DummyAutoAnimate = true;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_FRONT_DESK", cS03_OshiroLobby.ZoomOut);
				_003C_003E1__state = 16;
				return true;
			case 16:
				_003C_003E1__state = -1;
				foreach (MrOshiroDoor item in cS03_OshiroLobby.Scene.Entities.FindAll<MrOshiroDoor>())
				{
					item.Open();
				}
				cS03_OshiroLobby.oshiro.MoveToAndRemove(new Vector2(_003C_003E8__1.level.Bounds.Right + 64, cS03_OshiroLobby.oshiro.Y));
				cS03_OshiroLobby.oshiro.Add(new SoundSource("event:/char/oshiro/move_01_0xa_exit"));
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 17;
				return true;
			case 17:
				{
					_003C_003E1__state = -1;
					cS03_OshiroLobby.EndCutscene(_003C_003E8__1.level);
					return false;
				}
				IL_05b9:
				if (cS03_OshiroLobby.oshiro.Y != _003Cfrom_003E5__2)
				{
					cS03_OshiroLobby.oshiro.Y = Calc.Approach(cS03_OshiroLobby.oshiro.Y, _003Cfrom_003E5__2, Engine.DeltaTime * 40f);
					_003C_003E2__current = null;
					_003C_003E1__state = 14;
					return true;
				}
				_003C_003E2__current = _003Ctween_003E5__3.Wait();
				_003C_003E1__state = 15;
				return true;
				IL_020a:
				if (_003C_003E8__1.level.Lighting.Alpha > cS03_OshiroLobby.startLightAlpha)
				{
					_003C_003E8__1.level.Lighting.Alpha -= Engine.DeltaTime * 4f;
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E8__1.light = new VertexLight(new Vector2(0f, -8f), Color.White, 1f, 32, 64);
				_003C_003E8__1.bloom = new BloomPoint(new Vector2(0f, -8f), 1f, 16f);
				CS_0024_003C_003E8__locals15 = new _003C_003Ec__DisplayClass9_1();
				CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1 = _003C_003E8__1;
				CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.level.Lighting.SetSpotlight(CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.light);
				cS03_OshiroLobby.oshiro.Add(CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.light);
				cS03_OshiroLobby.oshiro.Add(CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.bloom);
				cS03_OshiroLobby.oshiro.Y -= 16f;
				CS_0024_003C_003E8__locals15.target = CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.light.Position;
				tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 0.5f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.light.Alpha = (CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.bloom.Alpha = t.Percent);
					CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.light.Position = Vector2.Lerp(CS_0024_003C_003E8__locals15.target - Vector2.UnitY * 48f, CS_0024_003C_003E8__locals15.target, t.Percent);
					CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.level.Lighting.Alpha = MathHelper.Lerp(CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1._003C_003E4__this.startLightAlpha, CS_0024_003C_003E8__locals15.CS_0024_003C_003E8__locals1.endLightAlpha, t.Eased);
				};
				cS03_OshiroLobby.Add(tween);
				_003C_003E2__current = tween.Wait();
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

	[CompilerGenerated]
	private sealed class _003CZoomOut_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroLobby _003C_003E4__this;

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
		public _003CZoomOut_003Ed__10(int _003C_003E1__state)
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
			CS03_OshiroLobby cS03_OshiroLobby = _003C_003E4__this;
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
				_003C_003E2__current = cS03_OshiroLobby.Level.ZoomBack(0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
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

	public const string Flag = "oshiro_resort_talked_1";

	private Player player;

	private NPC oshiro;

	private float startLightAlpha;

	private bool createSparks;

	private SoundSource sfx = new SoundSource();

	public CS03_OshiroLobby(Player player, NPC oshiro)
	{
		this.player = player;
		this.oshiro = oshiro;
		Add(sfx);
	}

	public override void Update()
	{
		base.Update();
		if (createSparks && Level.OnInterval(0.025f))
		{
			Vector2 vector = oshiro.Position + new Vector2(0f, -12f) + new Vector2(Calc.Random.Range(4, 12) * Calc.Random.Choose(1, -1), Calc.Random.Range(4, 12) * Calc.Random.Choose(1, -1));
			Level.Particles.Emit(NPC03_Oshiro_Lobby.P_AppearSpark, vector, (vector - oshiro.Position).Angle());
		}
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__9))]
	private IEnumerator Cutscene(Level level)
	{
		startLightAlpha = level.Lighting.Alpha;
		float endLightAlpha = 1f;
		float from = oshiro.Y;
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		yield return 0.5f;
		yield return player.DummyWalkTo(oshiro.X - 16f);
		player.Facing = Facings.Right;
		sfx.Play("event:/game/03_resort/sequence_oshiro_intro");
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		yield return 1.4f;
		level.Shake();
		level.Lighting.Alpha += 0.5f;
		while (level.Lighting.Alpha > startLightAlpha)
		{
			level.Lighting.Alpha -= Engine.DeltaTime * 4f;
			yield return null;
		}
		VertexLight light = new VertexLight(new Vector2(0f, -8f), Color.White, 1f, 32, 64);
		BloomPoint bloom = new BloomPoint(new Vector2(0f, -8f), 1f, 16f);
		level.Lighting.SetSpotlight(light);
		oshiro.Add(light);
		oshiro.Add(bloom);
		oshiro.Y -= 16f;
		Vector2 target = light.Position;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 0.5f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			light.Alpha = (bloom.Alpha = t.Percent);
			light.Position = Vector2.Lerp(target - Vector2.UnitY * 48f, target, t.Percent);
			level.Lighting.Alpha = MathHelper.Lerp(startLightAlpha, endLightAlpha, t.Eased);
		};
		Add(tween);
		yield return tween.Wait();
		yield return 0.2f;
		yield return level.ZoomTo(new Vector2(170f, 126f), 2f, 0.5f);
		yield return 0.6f;
		level.Shake();
		oshiro.Sprite.Visible = true;
		oshiro.Sprite.Play("appear");
		yield return player.DummyWalkToExact((int)(player.X - 12f), walkBackwards: true);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("shaking");
		Input.Rumble(RumbleStrength.Medium, RumbleLength.FullSecond);
		yield return 0.6f;
		createSparks = true;
		yield return 0.4f;
		createSparks = false;
		yield return 0.2f;
		level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		yield return 1.4f;
		level.Lighting.UnsetSpotlight();
		Tween tween2 = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 0.5f, start: true);
		tween2.OnUpdate = delegate(Tween t)
		{
			level.Lighting.Alpha = MathHelper.Lerp(endLightAlpha, startLightAlpha, t.Percent);
			bloom.Alpha = 1f - t.Percent;
		};
		Add(tween2);
		while (oshiro.Y != from)
		{
			oshiro.Y = Calc.Approach(oshiro.Y, from, Engine.DeltaTime * 40f);
			yield return null;
		}
		yield return tween2.Wait();
		Audio.SetMusic("event:/music/lvl3/oshiro_theme");
		player.DummyAutoAnimate = true;
		yield return Textbox.Say("CH3_OSHIRO_FRONT_DESK", ZoomOut);
		foreach (MrOshiroDoor item in base.Scene.Entities.FindAll<MrOshiroDoor>())
		{
			item.Open();
		}
		oshiro.MoveToAndRemove(new Vector2(level.Bounds.Right + 64, oshiro.Y));
		oshiro.Add(new SoundSource("event:/char/oshiro/move_01_0xa_exit"));
		yield return 1.5f;
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CZoomOut_003Ed__10))]
	private IEnumerator ZoomOut()
	{
		yield return 0.2f;
		yield return Level.ZoomBack(0.5f);
		yield return 0.2f;
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		if (WasSkipped)
		{
			foreach (MrOshiroDoor item in base.Scene.Entities.FindAll<MrOshiroDoor>())
			{
				item.InstantOpen();
			}
		}
		level.Lighting.Alpha = startLightAlpha;
		level.Lighting.UnsetSpotlight();
		level.Session.SetFlag("oshiro_resort_talked_1");
		level.Session.Audio.Music.Event = "event:/music/lvl3/explore";
		level.Session.Audio.Music.Progress = 1;
		level.Session.Audio.Apply();
		if (WasSkipped)
		{
			level.Remove(oshiro);
		}
	}
}
