using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class TempleBigEyeball : Entity
{
	private class Fader : Entity
	{
		public float Fade;

		public Fader()
		{
			base.Tag = Tags.HUD;
		}

		public override void Render()
		{
			Draw.Rect(-10f, -10f, Engine.Width + 20, Engine.Height + 20, Color.White * Fade);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public float start;

		internal void _003CBurst_003Eb__0(Tween t)
		{
			Glitch.Value = MathHelper.Lerp(start, 0f, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003CBurst_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TempleBigEyeball _003C_003E4__this;

		private Level _003Clevel_003E5__2;

		private Player _003Cplayer_003E5__3;

		private Fader _003Cfade_003E5__4;

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
		public _003CBurst_003Ed__14(int _003C_003E1__state)
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
			TempleBigEyeball templeBigEyeball = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				templeBigEyeball.bursting = true;
				_003Clevel_003E5__2 = templeBigEyeball.Scene as Level;
				_003Clevel_003E5__2.StartCutscene(templeBigEyeball.OnSkip, fadeInOnSkip: false, endingChapterAfterCutscene: true);
				_003Clevel_003E5__2.RegisterAreaComplete();
				Celeste.Freeze(0.1f);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass14_0 CS_0024_003C_003E8__locals1 = new _003C_003Ec__DisplayClass14_0
				{
					start = Glitch.Value
				};
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 0.5f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					Glitch.Value = MathHelper.Lerp(CS_0024_003C_003E8__locals1.start, 0f, t.Eased);
				};
				templeBigEyeball.Add(tween);
				_003Cplayer_003E5__3 = templeBigEyeball.Scene.Tracker.GetEntity<Player>();
				TheoCrystal entity = templeBigEyeball.Scene.Tracker.GetEntity<TheoCrystal>();
				if (_003Cplayer_003E5__3 != null)
				{
					_003Cplayer_003E5__3.StateMachine.State = 11;
					_003Cplayer_003E5__3.StateMachine.Locked = true;
					if (_003Cplayer_003E5__3.OnGround())
					{
						_003Cplayer_003E5__3.DummyAutoAnimate = false;
						_003Cplayer_003E5__3.Sprite.Play("shaking");
					}
				}
				templeBigEyeball.Add(new Coroutine(_003Clevel_003E5__2.ZoomTo(entity.TopCenter - _003Clevel_003E5__2.Camera.Position, 2f, 0.5f)));
				templeBigEyeball.Add(new Coroutine(entity.Shatter()));
				foreach (TempleEye item in templeBigEyeball.Scene.Entities.FindAll<TempleEye>())
				{
					item.Burst();
				}
				templeBigEyeball.sprite.Play("burst");
				templeBigEyeball.pupil.Visible = false;
				_003Clevel_003E5__2.Shake(0.4f);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 2;
				return true;
			}
			case 2:
				_003C_003E1__state = -1;
				if (_003Cplayer_003E5__3 != null && _003Cplayer_003E5__3.OnGround())
				{
					_003Cplayer_003E5__3.DummyAutoAnimate = false;
					_003Cplayer_003E5__3.Sprite.Play("shaking");
				}
				templeBigEyeball.Visible = false;
				_003Cfade_003E5__4 = new Fader();
				_003Clevel_003E5__2.Add(_003Cfade_003E5__4);
				goto IL_029c;
			case 3:
				_003C_003E1__state = -1;
				goto IL_029c;
			case 4:
				{
					_003C_003E1__state = -1;
					_003Cfade_003E5__4 = null;
					_003Clevel_003E5__2.EndCutscene();
					_003Clevel_003E5__2.CompleteArea(spotlightWipe: false);
					return false;
				}
				IL_029c:
				if ((_003Cfade_003E5__4.Fade += Engine.DeltaTime) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
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

	private Sprite sprite;

	private Image pupil;

	private bool triggered;

	private Vector2 pupilTarget;

	private float pupilDelay;

	private Wiggler bounceWiggler;

	private Wiggler pupilWiggler;

	private float shockwaveTimer;

	private bool shockwaveFlag;

	private float pupilSpeed = 40f;

	private bool bursting;

	public TempleBigEyeball(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(sprite = GFX.SpriteBank.Create("temple_eyeball"));
		Add(pupil = new Image(GFX.Game["danger/templeeye/pupil"]));
		pupil.CenterOrigin();
		base.Collider = new Hitbox(48f, 64f, -24f, -32f);
		Add(new PlayerCollider(OnPlayer));
		Add(new HoldableCollider(OnHoldable));
		Add(bounceWiggler = Wiggler.Create(0.5f, 3f));
		Add(pupilWiggler = Wiggler.Create(0.5f, 3f));
		shockwaveTimer = 2f;
	}

	private void OnPlayer(Player player)
	{
		if (!triggered)
		{
			Audio.Play("event:/game/05_mirror_temple/eyewall_bounce", player.Position);
			player.ExplodeLaunch(player.Center + Vector2.UnitX * 20f);
			player.Swat(-1);
			bounceWiggler.Start();
		}
	}

	private void OnHoldable(Holdable h)
	{
		if (!(h.Entity is TheoCrystal))
		{
			return;
		}
		TheoCrystal theoCrystal = h.Entity as TheoCrystal;
		if (!triggered && theoCrystal.Speed.X > 32f && !theoCrystal.Hold.IsHeld)
		{
			theoCrystal.Speed.X = -50f;
			theoCrystal.Speed.Y = -10f;
			triggered = true;
			bounceWiggler.Start();
			Collidable = false;
			Audio.SetAmbience(null);
			Audio.Play("event:/game/05_mirror_temple/eyewall_destroy", Position);
			Alarm.Set(this, 1.3f, delegate
			{
				Audio.SetMusic(null);
			});
			Add(new Coroutine(Burst()));
		}
	}

	[IteratorStateMachine(typeof(_003CBurst_003Ed__14))]
	private IEnumerator Burst()
	{
		bursting = true;
		Level level = base.Scene as Level;
		level.StartCutscene(OnSkip, fadeInOnSkip: false, endingChapterAfterCutscene: true);
		level.RegisterAreaComplete();
		Celeste.Freeze(0.1f);
		yield return null;
		float start = Glitch.Value;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 0.5f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			Glitch.Value = MathHelper.Lerp(start, 0f, t.Eased);
		};
		Add(tween);
		Player player = base.Scene.Tracker.GetEntity<Player>();
		TheoCrystal entity = base.Scene.Tracker.GetEntity<TheoCrystal>();
		if (player != null)
		{
			player.StateMachine.State = 11;
			player.StateMachine.Locked = true;
			if (player.OnGround())
			{
				player.DummyAutoAnimate = false;
				player.Sprite.Play("shaking");
			}
		}
		Add(new Coroutine(level.ZoomTo(entity.TopCenter - level.Camera.Position, 2f, 0.5f)));
		Add(new Coroutine(entity.Shatter()));
		foreach (TempleEye item in base.Scene.Entities.FindAll<TempleEye>())
		{
			item.Burst();
		}
		sprite.Play("burst");
		pupil.Visible = false;
		level.Shake(0.4f);
		yield return 2f;
		if (player != null && player.OnGround())
		{
			player.DummyAutoAnimate = false;
			player.Sprite.Play("shaking");
		}
		Visible = false;
		Fader fade = new Fader();
		level.Add(fade);
		while ((fade.Fade += Engine.DeltaTime) < 1f)
		{
			yield return null;
		}
		yield return 1f;
		level.EndCutscene();
		level.CompleteArea(spotlightWipe: false);
	}

	private void OnSkip(Level level)
	{
		level.CompleteArea(spotlightWipe: false);
	}

	public override void Update()
	{
		base.Update();
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			Audio.SetMusicParam("eye_distance", Calc.ClampedMap(entity.X, (base.Scene as Level).Bounds.Left, base.X));
		}
		if (entity != null && !bursting)
		{
			Glitch.Value = Calc.ClampedMap(Math.Abs(base.X - entity.X), 100f, 900f, 0.2f, 0f);
		}
		if (!triggered && shockwaveTimer > 0f)
		{
			shockwaveTimer -= Engine.DeltaTime;
			if (shockwaveTimer <= 0f)
			{
				if (entity != null)
				{
					shockwaveTimer = Calc.ClampedMap(Math.Abs(base.X - entity.X), 100f, 500f, 2f, 3f);
					shockwaveFlag = !shockwaveFlag;
					if (shockwaveFlag)
					{
						shockwaveTimer -= 1f;
					}
				}
				base.Scene.Add(Engine.Pooler.Create<TempleBigEyeballShockwave>().Init(base.Center + new Vector2(50f, 0f)));
				pupilWiggler.Start();
				pupilTarget = new Vector2(-1f, 0f);
				pupilSpeed = 120f;
				pupilDelay = Math.Max(0.5f, pupilDelay);
			}
		}
		pupil.Position = Calc.Approach(pupil.Position, pupilTarget * 12f, Engine.DeltaTime * pupilSpeed);
		pupilSpeed = Calc.Approach(pupilSpeed, 40f, Engine.DeltaTime * 400f);
		TheoCrystal entity2 = base.Scene.Tracker.GetEntity<TheoCrystal>();
		if (entity2 != null && Math.Abs(base.X - entity2.X) < 64f && Math.Abs(base.Y - entity2.Y) < 64f)
		{
			pupilTarget = (entity2.Center - Position).SafeNormalize();
		}
		else if (pupilDelay < 0f)
		{
			pupilTarget = Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 1f);
			pupilDelay = Calc.Random.Choose(0.2f, 1f, 2f);
		}
		else
		{
			pupilDelay -= Engine.DeltaTime;
		}
		if (entity != null)
		{
			Level level = base.Scene as Level;
			Audio.SetMusicParam("eye_distance", Calc.ClampedMap(entity.X, level.Bounds.Left + 32, base.X - 32f, 1f, 0f));
		}
	}

	public override void Render()
	{
		sprite.Scale.X = 1f + 0.15f * bounceWiggler.Value;
		pupil.Scale = Vector2.One * (1f + pupilWiggler.Value * 0.15f);
		base.Render();
	}
}
