using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS10_HubIntro : CutsceneEntity
{
	private class Bird : Entity
	{
		[CompilerGenerated]
		private sealed class _003CIdleRoutine_003Ed__3 : IEnumerator<object>, IDisposable, IEnumerator
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
			public _003CIdleRoutine_003Ed__3(int _003C_003E1__state)
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

		[CompilerGenerated]
		private sealed class _003CFlyAwayRoutine_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Bird _003C_003E4__this;

			private Level _003Clevel_003E5__2;

			private float _003Cspd_003E5__3;

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
			public _003CFlyAwayRoutine_003Ed__4(int _003C_003E1__state)
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
				Bird bird = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Clevel_003E5__2 = bird.Scene as Level;
					bird.sfx = Audio.Play("event:/new_content/game/10_farewell/bird_fly_uptonext", bird.Position);
					bird.sprite.Play("flyup");
					_003Cspd_003E5__3 = -32f;
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				}
				if (bird.Y > (float)(_003Clevel_003E5__2.Bounds.Top - 32))
				{
					_003Cspd_003E5__3 -= 400f * Engine.DeltaTime;
					bird.Y += _003Cspd_003E5__3 * Engine.DeltaTime;
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

		private Sprite sprite;

		public FMOD.Studio.EventInstance sfx;

		public Bird(Vector2 position)
		{
			Position = position;
			base.Depth = -8500;
			Add(sprite = GFX.SpriteBank.Create("bird"));
			sprite.Play("hover");
			sprite.OnFrameChange = delegate
			{
				BirdNPC.FlapSfxCheck(sprite);
			};
		}

		[IteratorStateMachine(typeof(_003CIdleRoutine_003Ed__3))]
		public IEnumerator IdleRoutine()
		{
			yield return 0.5f;
		}

		[IteratorStateMachine(typeof(_003CFlyAwayRoutine_003Ed__4))]
		public IEnumerator FlyAwayRoutine()
		{
			Level level = base.Scene as Level;
			sfx = Audio.Play("event:/new_content/game/10_farewell/bird_fly_uptonext", Position);
			sprite.Play("flyup");
			float spd = -32f;
			while (base.Y > (float)(level.Bounds.Top - 32))
			{
				spd -= 400f * Engine.DeltaTime;
				base.Y += spd * Engine.DeltaTime;
				yield return null;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS10_HubIntro _003C_003E4__this;

		public Level level;

		private float _003Cduration_003E5__2;

		private string _003Csfx_003E5__3;

		private int _003CdoorIndex_003E5__4;

		private float _003Ct_003E5__5;

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
		public _003CCutscene_003Ed__10(int _003C_003E1__state)
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
			CS10_HubIntro cS10_HubIntro = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (cS10_HubIntro.player.Holding != null)
				{
					cS10_HubIntro.player.Throw();
				}
				cS10_HubIntro.player.StateMachine.State = 11;
				cS10_HubIntro.player.ForceCameraUpdate = true;
				goto IL_009c;
			case 1:
				_003C_003E1__state = -1;
				goto IL_009c;
			case 2:
				_003C_003E1__state = -1;
				cS10_HubIntro.player.DummyAutoAnimate = false;
				cS10_HubIntro.player.Sprite.Play("lookUp");
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				level.Add(cS10_HubIntro.bird = new Bird(new Vector2(cS10_HubIntro.spawn.X, (float)level.Bounds.Top + 190f)));
				Audio.Play("event:/new_content/game/10_farewell/bird_camera_pan_up");
				_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(cS10_HubIntro.spawn.X - 160f, (float)level.Bounds.Top + 190f - 90f), 2f, Ease.CubeInOut);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_HubIntro.bird.IdleRoutine();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS10_HubIntro.Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(level.Camera.X, level.Bounds.Top), 0.8f, Ease.CubeInOut, 0.1f)));
				Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
				_003C_003E2__current = cS10_HubIntro.bird.FlyAwayRoutine();
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS10_HubIntro.bird.RemoveSelf();
				cS10_HubIntro.bird = null;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003Cduration_003E5__2 = 6f;
				_003Csfx_003E5__3 = "event:/new_content/game/10_farewell/locked_door_appear_1".Substring(0, "event:/new_content/game/10_farewell/locked_door_appear_1".Length - 1);
				_003CdoorIndex_003E5__4 = 1;
				cS10_HubIntro.Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(level.Camera.X, level.Bounds.Bottom - 180), _003Cduration_003E5__2, Ease.SineInOut)));
				cS10_HubIntro.Add(new Coroutine(cS10_HubIntro.Level.ZoomTo(new Vector2(160f, 90f), 1.5f, _003Cduration_003E5__2)));
				_003Ct_003E5__5 = 0f;
				goto IL_0426;
			case 8:
				_003C_003E1__state = -1;
				_003Ct_003E5__5 += Engine.DeltaTime;
				goto IL_0426;
			case 9:
				_003C_003E1__state = -1;
				if (cS10_HubIntro.booster != null)
				{
					Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
					cS10_HubIntro.booster.Appear();
				}
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS10_HubIntro.Level.ZoomBack(0.3f);
				_003C_003E1__state = 11;
				return true;
			case 11:
				{
					_003C_003E1__state = -1;
					cS10_HubIntro.EndCutscene(level);
					return false;
				}
				IL_0426:
				if (_003Ct_003E5__5 < _003Cduration_003E5__2)
				{
					foreach (LockBlock @lock in cS10_HubIntro.locks)
					{
						if (!@lock.Visible && level.Camera.Y + 90f > @lock.Y - 20f)
						{
							cS10_HubIntro.sfxs.Add(Audio.Play(_003Csfx_003E5__3 + _003CdoorIndex_003E5__4, @lock.Center));
							@lock.Appear();
							Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
							_003CdoorIndex_003E5__4++;
						}
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				_003Csfx_003E5__3 = null;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 9;
				return true;
				IL_009c:
				if (!cS10_HubIntro.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS10_HubIntro.player.ForceCameraUpdate = false;
				_003C_003E2__current = 0.1f;
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

	public const string Flag = "hub_intro";

	public const float BirdOffset = 190f;

	private Player player;

	private List<LockBlock> locks;

	private Booster booster;

	private Bird bird;

	private Vector2 spawn;

	private List<FMOD.Studio.EventInstance> sfxs = new List<FMOD.Studio.EventInstance>();

	public CS10_HubIntro(Scene scene, Player player)
	{
		this.player = player;
		spawn = (scene as Level).GetSpawnPoint(player.Position);
		locks = scene.Entities.FindAll<LockBlock>();
		locks.Sort((LockBlock a, LockBlock b) => (int)(a.Y - b.Y));
		foreach (LockBlock @lock in locks)
		{
			@lock.Visible = false;
		}
		booster = scene.Entities.FindFirst<Booster>();
		if (booster != null)
		{
			booster.Visible = false;
		}
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__10))]
	private IEnumerator Cutscene(Level level)
	{
		if (player.Holding != null)
		{
			player.Throw();
		}
		player.StateMachine.State = 11;
		player.ForceCameraUpdate = true;
		while (!player.OnGround())
		{
			yield return null;
		}
		player.ForceCameraUpdate = false;
		yield return 0.1f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("lookUp");
		yield return 0.25f;
		level.Add(bird = new Bird(new Vector2(spawn.X, (float)level.Bounds.Top + 190f)));
		Audio.Play("event:/new_content/game/10_farewell/bird_camera_pan_up");
		yield return CutsceneEntity.CameraTo(new Vector2(spawn.X - 160f, (float)level.Bounds.Top + 190f - 90f), 2f, Ease.CubeInOut);
		yield return bird.IdleRoutine();
		Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(level.Camera.X, level.Bounds.Top), 0.8f, Ease.CubeInOut, 0.1f)));
		Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
		yield return bird.FlyAwayRoutine();
		bird.RemoveSelf();
		bird = null;
		yield return 0.5f;
		float duration = 6f;
		string sfx = "event:/new_content/game/10_farewell/locked_door_appear_1".Substring(0, "event:/new_content/game/10_farewell/locked_door_appear_1".Length - 1);
		int doorIndex = 1;
		Add(new Coroutine(CutsceneEntity.CameraTo(new Vector2(level.Camera.X, level.Bounds.Bottom - 180), duration, Ease.SineInOut)));
		Add(new Coroutine(Level.ZoomTo(new Vector2(160f, 90f), 1.5f, duration)));
		for (float t = 0f; t < duration; t += Engine.DeltaTime)
		{
			foreach (LockBlock @lock in locks)
			{
				if (!@lock.Visible && level.Camera.Y + 90f > @lock.Y - 20f)
				{
					sfxs.Add(Audio.Play(sfx + doorIndex, @lock.Center));
					@lock.Appear();
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					doorIndex++;
				}
			}
			yield return null;
		}
		yield return 0.5f;
		if (booster != null)
		{
			Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
			booster.Appear();
		}
		yield return 0.3f;
		yield return Level.ZoomBack(0.3f);
		EndCutscene(level);
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped)
		{
			foreach (FMOD.Studio.EventInstance sfx in sfxs)
			{
				Audio.Stop(sfx);
			}
			if (bird != null)
			{
				Audio.Stop(bird.sfx);
			}
		}
		foreach (LockBlock @lock in locks)
		{
			@lock.Visible = true;
		}
		if (booster != null)
		{
			booster.Visible = true;
		}
		if (bird != null)
		{
			bird.RemoveSelf();
		}
		if (WasSkipped)
		{
			player.Position = spawn;
		}
		player.Speed = Vector2.Zero;
		player.DummyAutoAnimate = true;
		player.ForceCameraUpdate = false;
		player.StateMachine.State = 0;
		level.Camera.Y = level.Bounds.Bottom - 180;
		level.Session.SetFlag("hub_intro");
		level.ResetZoom();
	}
}
