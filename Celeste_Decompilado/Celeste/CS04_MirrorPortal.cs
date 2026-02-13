using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS04_MirrorPortal : CutsceneEntity
{
	private class Fader : Entity
	{
		public float Target;

		public bool Ended;

		private float fade;

		public Fader()
		{
			base.Depth = -1000000;
		}

		public override void Update()
		{
			fade = Calc.Approach(fade, Target, Engine.DeltaTime * 0.5f);
			if (Target <= 0f && fade <= 0f && Ended)
			{
				RemoveSelf();
			}
			base.Update();
		}

		public override void Render()
		{
			Camera camera = (base.Scene as Level).Camera;
			if (fade > 0f)
			{
				Draw.Rect(camera.X - 10f, camera.Y - 10f, 340f, 200f, Color.Black * fade);
			}
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && !entity.OnGround(2))
			{
				entity.Render();
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__6 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_MirrorPortal _003C_003E4__this;

		public Level level;

		private Vector2 _003Ctarget_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

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
			CS04_MirrorPortal cS04_MirrorPortal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.StateMachine.State = 11;
				cS04_MirrorPortal.player.StateMachine.Locked = true;
				cS04_MirrorPortal.player.Dashes = 1;
				if (level.Session.Area.Mode == AreaMode.Normal)
				{
					Audio.SetMusic(null);
				}
				else
				{
					cS04_MirrorPortal.Add(new Coroutine(cS04_MirrorPortal.MusicFadeOutBSide()));
				}
				cS04_MirrorPortal.Add(cS04_MirrorPortal.sfx = new SoundSource());
				cS04_MirrorPortal.sfx.Position = cS04_MirrorPortal.portal.Center;
				cS04_MirrorPortal.sfx.Play("event:/music/lvl5/mirror_cutscene");
				cS04_MirrorPortal.Add(new Coroutine(cS04_MirrorPortal.CenterCamera()));
				_003C_003E2__current = cS04_MirrorPortal.player.DummyWalkToExact((int)cS04_MirrorPortal.portal.X);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_MirrorPortal.player.DummyWalkToExact((int)cS04_MirrorPortal.portal.X - 16);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_MirrorPortal.player.DummyWalkToExact((int)cS04_MirrorPortal.portal.X + 16);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.Facing = Facings.Left;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_MirrorPortal.player.DummyWalkToExact((int)cS04_MirrorPortal.portal.X);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.DummyAutoAnimate = false;
				cS04_MirrorPortal.player.Sprite.Play("lookUp");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.DummyAutoAnimate = true;
				cS04_MirrorPortal.portal.Activate();
				cS04_MirrorPortal.Add(new Coroutine(level.ZoomTo(new Vector2(160f, 90f), 3f, 12f)));
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.ForceStrongWindHair.X = -1f;
				_003C_003E2__current = cS04_MirrorPortal.player.DummyWalkToExact((int)cS04_MirrorPortal.player.X + 12, walkBackwards: true);
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.Facing = Facings.Right;
				cS04_MirrorPortal.player.DummyAutoAnimate = false;
				cS04_MirrorPortal.player.DummyGravity = false;
				cS04_MirrorPortal.player.Sprite.Play("runWind");
				goto IL_049d;
			case 14:
				_003C_003E1__state = -1;
				goto IL_049d;
			case 15:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.Sprite.Play("fallFast");
				cS04_MirrorPortal.player.Sprite.Rate = 1f;
				_003Ctarget_003E5__2 = cS04_MirrorPortal.portal.Center + new Vector2(0f, 8f);
				_003Cfrom_003E5__3 = cS04_MirrorPortal.player.Position;
				_003Cp_003E5__4 = 0f;
				goto IL_05b3;
			case 16:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime * 2f;
				goto IL_05b3;
			case 17:
				_003C_003E1__state = -1;
				cS04_MirrorPortal.player.Sprite.Play("sleep");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 18;
				return true;
			case 18:
				_003C_003E1__state = -1;
				_003C_003E2__current = level.ZoomBack(1f);
				_003C_003E1__state = 19;
				return true;
			case 19:
				_003C_003E1__state = -1;
				if (level.Session.Area.Mode != AreaMode.Normal)
				{
					break;
				}
				level.Session.ColorGrade = "templevoid";
				_003Cp_003E5__4 = 0f;
				goto IL_070b;
			case 20:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime;
				goto IL_070b;
			case 21:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_070b:
				if (_003Cp_003E5__4 < 1f)
				{
					Glitch.Value = _003Cp_003E5__4 * 0.05f;
					level.ScreenPadding = 32f * _003Cp_003E5__4;
					_003C_003E2__current = null;
					_003C_003E1__state = 20;
					return true;
				}
				break;
				IL_05b3:
				if (_003Cp_003E5__4 < 1f)
				{
					cS04_MirrorPortal.player.Position = _003Cfrom_003E5__3 + (_003Ctarget_003E5__2 - _003Cfrom_003E5__3) * Ease.SineInOut(_003Cp_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 16;
					return true;
				}
				cS04_MirrorPortal.player.ForceStrongWindHair.X = 0f;
				_003Ctarget_003E5__2 = default(Vector2);
				_003Cfrom_003E5__3 = default(Vector2);
				cS04_MirrorPortal.fader.Target = 1f;
				_003C_003E2__current = 2f;
				_003C_003E1__state = 17;
				return true;
				IL_049d:
				if (cS04_MirrorPortal.player.Sprite.Rate > 0f)
				{
					cS04_MirrorPortal.player.MoveH(cS04_MirrorPortal.player.Sprite.Rate * 10f * Engine.DeltaTime);
					cS04_MirrorPortal.player.MoveV((0f - (1f - cS04_MirrorPortal.player.Sprite.Rate)) * 6f * Engine.DeltaTime);
					cS04_MirrorPortal.player.Sprite.Rate -= Engine.DeltaTime * 0.15f;
					_003C_003E2__current = null;
					_003C_003E1__state = 14;
					return true;
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 15;
				return true;
			}
			if ((cS04_MirrorPortal.portal.DistortionFade -= Engine.DeltaTime * 2f) > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 21;
				return true;
			}
			cS04_MirrorPortal.EndCutscene(level);
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
	private sealed class _003CCenterCamera_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_MirrorPortal _003C_003E4__this;

		private Camera _003Ccamera_003E5__2;

		private Vector2 _003Ctarget_003E5__3;

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
		public _003CCenterCamera_003Ed__7(int _003C_003E1__state)
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
			CS04_MirrorPortal cS04_MirrorPortal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ccamera_003E5__2 = cS04_MirrorPortal.Level.Camera;
				_003Ctarget_003E5__3 = cS04_MirrorPortal.portal.Center - new Vector2(160f, 90f);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if ((_003Ccamera_003E5__2.Position - _003Ctarget_003E5__3).Length() > 1f)
			{
				_003Ccamera_003E5__2.Position += (_003Ctarget_003E5__3 - _003Ccamera_003E5__2.Position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
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
	private sealed class _003CMusicFadeOutBSide_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private float _003Cp_003E5__2;

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
		public _003CMusicFadeOutBSide_003Ed__8(int _003C_003E1__state)
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
				_003Cp_003E5__2 = 1f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 -= Engine.DeltaTime;
				break;
			}
			if (_003Cp_003E5__2 > 0f)
			{
				Audio.SetMusicParam("fade", _003Cp_003E5__2);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			Audio.SetMusicParam("fade", 0f);
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

	private Player player;

	private TempleMirrorPortal portal;

	private Fader fader;

	private SoundSource sfx;

	public CS04_MirrorPortal(Player player, TempleMirrorPortal portal)
	{
		this.player = player;
		this.portal = portal;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
		level.Add(fader = new Fader());
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__6))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		player.Dashes = 1;
		if (level.Session.Area.Mode == AreaMode.Normal)
		{
			Audio.SetMusic(null);
		}
		else
		{
			Add(new Coroutine(MusicFadeOutBSide()));
		}
		Add(sfx = new SoundSource());
		sfx.Position = portal.Center;
		sfx.Play("event:/music/lvl5/mirror_cutscene");
		Add(new Coroutine(CenterCamera()));
		yield return player.DummyWalkToExact((int)portal.X);
		yield return 0.25f;
		yield return player.DummyWalkToExact((int)portal.X - 16);
		yield return 0.5f;
		yield return player.DummyWalkToExact((int)portal.X + 16);
		yield return 0.25f;
		player.Facing = Facings.Left;
		yield return 0.25f;
		yield return player.DummyWalkToExact((int)portal.X);
		yield return 0.1f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("lookUp");
		yield return 1f;
		player.DummyAutoAnimate = true;
		portal.Activate();
		Add(new Coroutine(level.ZoomTo(new Vector2(160f, 90f), 3f, 12f)));
		yield return 0.25f;
		player.ForceStrongWindHair.X = -1f;
		yield return player.DummyWalkToExact((int)player.X + 12, walkBackwards: true);
		yield return 0.5f;
		player.Facing = Facings.Right;
		player.DummyAutoAnimate = false;
		player.DummyGravity = false;
		player.Sprite.Play("runWind");
		while (player.Sprite.Rate > 0f)
		{
			player.MoveH(player.Sprite.Rate * 10f * Engine.DeltaTime);
			player.MoveV((0f - (1f - player.Sprite.Rate)) * 6f * Engine.DeltaTime);
			player.Sprite.Rate -= Engine.DeltaTime * 0.15f;
			yield return null;
		}
		yield return 0.5f;
		player.Sprite.Play("fallFast");
		player.Sprite.Rate = 1f;
		Vector2 target = portal.Center + new Vector2(0f, 8f);
		Vector2 from = player.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 2f)
		{
			player.Position = from + (target - from) * Ease.SineInOut(p);
			yield return null;
		}
		player.ForceStrongWindHair.X = 0f;
		fader.Target = 1f;
		yield return 2f;
		player.Sprite.Play("sleep");
		yield return 1f;
		yield return level.ZoomBack(1f);
		if (level.Session.Area.Mode == AreaMode.Normal)
		{
			level.Session.ColorGrade = "templevoid";
			for (float p = 0f; p < 1f; p += Engine.DeltaTime)
			{
				Glitch.Value = p * 0.05f;
				level.ScreenPadding = 32f * p;
				yield return null;
			}
		}
		while ((portal.DistortionFade -= Engine.DeltaTime * 2f) > 0f)
		{
			yield return null;
		}
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CCenterCamera_003Ed__7))]
	private IEnumerator CenterCamera()
	{
		Camera camera = Level.Camera;
		Vector2 target = portal.Center - new Vector2(160f, 90f);
		while ((camera.Position - target).Length() > 1f)
		{
			camera.Position += (target - camera.Position) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CMusicFadeOutBSide_003Ed__8))]
	private IEnumerator MusicFadeOutBSide()
	{
		for (float p = 1f; p > 0f; p -= Engine.DeltaTime)
		{
			Audio.SetMusicParam("fade", p);
			yield return null;
		}
		Audio.SetMusicParam("fade", 0f);
	}

	public override void OnEnd(Level level)
	{
		level.OnEndOfFrame += delegate
		{
			if (fader != null && !WasSkipped)
			{
				fader.Tag = Tags.Global;
				fader.Target = 0f;
				fader.Ended = true;
			}
			Leader.StoreStrawberries(player.Leader);
			level.Remove(player);
			level.UnloadLevel();
			level.Session.Dreaming = true;
			level.Session.Keys.Clear();
			if (level.Session.Area.Mode == AreaMode.Normal)
			{
				level.Session.Level = "void";
				level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));
				level.LoadLevel(Player.IntroTypes.TempleMirrorVoid);
			}
			else
			{
				level.Session.Level = "c-00";
				level.Session.RespawnPoint = level.GetSpawnPoint(new Vector2(level.Bounds.Left, level.Bounds.Top));
				level.LoadLevel(Player.IntroTypes.WakeUp);
				Audio.SetMusicParam("fade", 1f);
			}
			Leader.RestoreStrawberries(level.Tracker.GetEntity<Player>().Leader);
			level.Camera.Y -= 8f;
			if (!WasSkipped && level.Wipe != null)
			{
				level.Wipe.Cancel();
			}
			if (fader != null)
			{
				fader.RemoveTag(Tags.Global);
			}
		};
	}
}
