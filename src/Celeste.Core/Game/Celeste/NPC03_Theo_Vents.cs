using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC03_Theo_Vents : NPC
{
	private class Grate : Entity
	{
		private Image sprite;

		private float shake;

		private Vector2 speed;

		private bool falling;

		private float alpha = 1f;

		public Grate(Vector2 position)
			: base(position)
		{
			Add(sprite = new Image(GFX.Game["scenery/grate"]));
			sprite.JustifyOrigin(0.5f, 0f);
		}

		public void Shake()
		{
			if (!falling)
			{
				Audio.Play("event:/char/theo/resort_ceilingvent_shake", Position);
				shake = 0.5f;
			}
		}

		public void Fall()
		{
			Audio.Play("event:/char/theo/resort_ceilingvent_popoff", Position);
			falling = true;
			speed = new Vector2(40f, 200f);
			base.Collider = new Hitbox(2f, 2f, -1f);
		}

		public override void Update()
		{
			if (shake > 0f)
			{
				shake -= Engine.DeltaTime;
				if (base.Scene.OnInterval(0.05f))
				{
					sprite.X = 1f - sprite.X;
				}
			}
			if (falling)
			{
				speed.X = Calc.Approach(speed.X, 0f, Engine.DeltaTime * 80f);
				speed.Y += 200f * Engine.DeltaTime;
				Position += speed * Engine.DeltaTime;
				if (CollideCheck<Solid>(Position + new Vector2(0f, 2f)) && speed.Y > 0f)
				{
					speed.Y = (0f - speed.Y) * 0.25f;
				}
				alpha -= Engine.DeltaTime;
				sprite.Rotation += Engine.DeltaTime;
				sprite.Color = Color.White * alpha;
				if (alpha <= 0f)
				{
					RemoveSelf();
				}
			}
			base.Update();
		}
	}

	[CompilerGenerated]
	private sealed class _003CAppear_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Theo_Vents _003C_003E4__this;

		private int _003Cfrom_003E5__2;

		private float _003Cp_003E5__3;

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
		public _003CAppear_003Ed__9(int _003C_003E1__state)
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
			NPC03_Theo_Vents nPC03_Theo_Vents = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (nPC03_Theo_Vents.Session.GetFlag("theoVentsAppeared"))
				{
					break;
				}
				nPC03_Theo_Vents.grate = new Grate(nPC03_Theo_Vents.Position);
				nPC03_Theo_Vents.Scene.Add(nPC03_Theo_Vents.grate);
				goto IL_0060;
			case 1:
			{
				_003C_003E1__state = -1;
				Player entity = nPC03_Theo_Vents.Scene.Tracker.GetEntity<Player>();
				if (entity == null || !(entity.X > nPC03_Theo_Vents.X - 32f))
				{
					goto IL_0060;
				}
				Audio.Play("event:/char/theo/resort_ceilingvent_hey", nPC03_Theo_Vents.Position);
				nPC03_Theo_Vents.Level.ParticlesFG.Emit(ParticleTypes.VentDust, 24, nPC03_Theo_Vents.Position, new Vector2(6f, 0f));
				nPC03_Theo_Vents.grate.Fall();
				_003Cfrom_003E5__2 = -24;
				_003Cp_003E5__3 = 0f;
				goto IL_0160;
			}
			case 2:
				{
					_003C_003E1__state = -1;
					nPC03_Theo_Vents.Visible = true;
					nPC03_Theo_Vents.Sprite.Y = (float)_003Cfrom_003E5__2 + (float)(-8 - _003Cfrom_003E5__2) * Ease.CubeOut(_003Cp_003E5__3);
					_003Cp_003E5__3 += Engine.DeltaTime * 2f;
					goto IL_0160;
				}
				IL_0060:
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
				IL_0160:
				if (_003Cp_003E5__3 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				nPC03_Theo_Vents.Session.SetFlag("theoVentsAppeared");
				break;
			}
			nPC03_Theo_Vents.appeared = true;
			nPC03_Theo_Vents.Sprite.Y = -8f;
			nPC03_Theo_Vents.Visible = true;
			nPC03_Theo_Vents.Add(nPC03_Theo_Vents.Talker = new TalkComponent(new Rectangle(-16, 0, 32, 100), new Vector2(0f, -8f), nPC03_Theo_Vents.OnTalk));
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
	private sealed class _003CTalk_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Theo_Vents _003C_003E4__this;

		public Player player;

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
		public _003CTalk_003Ed__11(int _003C_003E1__state)
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
			NPC03_Theo_Vents nPC03_Theo_Vents = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Theo_Vents.PlayerApproach(player, turnToFace: true, 10f, -1);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				player.DummyAutoAnimate = false;
				player.Sprite.Play("lookUp");
				_003C_003E2__current = CutsceneEntity.CameraTo(new Vector2(nPC03_Theo_Vents.Level.Bounds.Right - 320, nPC03_Theo_Vents.Level.Bounds.Top), 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Theo_Vents.Level.ZoomTo(new Vector2(240f, 70f), 2f, 0.5f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH3_THEO_VENTS");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Theo_Vents.Disappear();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Theo_Vents.Level.ZoomBack(0.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				nPC03_Theo_Vents.Level.EndCutscene();
				nPC03_Theo_Vents.OnTalkEnd(nPC03_Theo_Vents.Level);
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
	private sealed class _003CDisappear_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Theo_Vents _003C_003E4__this;

		private int _003Cto_003E5__2;

		private float _003Cfrom_003E5__3;

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
		public _003CDisappear_003Ed__13(int _003C_003E1__state)
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
			NPC03_Theo_Vents nPC03_Theo_Vents = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/theo/resort_ceilingvent_seeya", nPC03_Theo_Vents.Position);
				_003Cto_003E5__2 = -24;
				_003Cfrom_003E5__3 = nPC03_Theo_Vents.Sprite.Y;
				_003Cp_003E5__4 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				nPC03_Theo_Vents.Level.ParticlesFG.Emit(ParticleTypes.VentDust, 1, nPC03_Theo_Vents.Position, new Vector2(6f, 0f));
				nPC03_Theo_Vents.Sprite.Y = _003Cfrom_003E5__3 + ((float)_003Cto_003E5__2 - _003Cfrom_003E5__3) * Ease.BackIn(_003Cp_003E5__4);
				_003Cp_003E5__4 += Engine.DeltaTime * 2f;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
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

	private const string AppeardFlag = "theoVentsAppeared";

	private const string TalkedFlag = "theoVentsTalked";

	private const int SpriteAppearY = -8;

	private float particleDelay;

	private bool appeared;

	private Grate grate;

	public NPC03_Theo_Vents(Vector2 position)
		: base(position)
	{
		base.Tag = Tags.TransitionUpdate;
		Add(Sprite = GFX.SpriteBank.Create("theo"));
		Sprite.Scale.Y = -1f;
		Sprite.Scale.X = -1f;
		Visible = false;
		Maxspeed = 48f;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (base.Session.GetFlag("theoVentsTalked"))
		{
			RemoveSelf();
		}
		else
		{
			Add(new Coroutine(Appear()));
		}
	}

	public override void Update()
	{
		base.Update();
		if (appeared)
		{
			return;
		}
		particleDelay -= Engine.DeltaTime;
		if (particleDelay <= 0f)
		{
			Level.ParticlesFG.Emit(ParticleTypes.VentDust, 8, Position, new Vector2(6f, 0f));
			if (grate != null)
			{
				grate.Shake();
			}
			particleDelay = Calc.Random.Choose(1f, 2f, 3f);
		}
	}

	[IteratorStateMachine(typeof(_003CAppear_003Ed__9))]
	private IEnumerator Appear()
	{
		if (!base.Session.GetFlag("theoVentsAppeared"))
		{
			grate = new Grate(Position);
			base.Scene.Add(grate);
			Player entity;
			do
			{
				yield return null;
				entity = base.Scene.Tracker.GetEntity<Player>();
			}
			while (entity == null || !(entity.X > base.X - 32f));
			Audio.Play("event:/char/theo/resort_ceilingvent_hey", Position);
			Level.ParticlesFG.Emit(ParticleTypes.VentDust, 24, Position, new Vector2(6f, 0f));
			grate.Fall();
			int from = -24;
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 2f)
			{
				yield return null;
				Visible = true;
				Sprite.Y = (float)from + (float)(-8 - from) * Ease.CubeOut(p);
			}
			base.Session.SetFlag("theoVentsAppeared");
		}
		appeared = true;
		Sprite.Y = -8f;
		Visible = true;
		Add(Talker = new TalkComponent(new Rectangle(-16, 0, 32, 100), new Vector2(0f, -8f), OnTalk));
	}

	private void OnTalk(Player player)
	{
		Level.StartCutscene(OnTalkEnd);
		Add(new Coroutine(Talk(player)));
	}

	[IteratorStateMachine(typeof(_003CTalk_003Ed__11))]
	private IEnumerator Talk(Player player)
	{
		yield return PlayerApproach(player, turnToFace: true, 10f, -1);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("lookUp");
		yield return CutsceneEntity.CameraTo(new Vector2(Level.Bounds.Right - 320, Level.Bounds.Top), 0.5f);
		yield return Level.ZoomTo(new Vector2(240f, 70f), 2f, 0.5f);
		yield return Textbox.Say("CH3_THEO_VENTS");
		yield return Disappear();
		yield return 0.25f;
		yield return Level.ZoomBack(0.5f);
		Level.EndCutscene();
		OnTalkEnd(Level);
	}

	private void OnTalkEnd(Level level)
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.DummyAutoAnimate = true;
			entity.StateMachine.Locked = false;
			entity.StateMachine.State = 0;
		}
		base.Session.SetFlag("theoVentsTalked");
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CDisappear_003Ed__13))]
	private IEnumerator Disappear()
	{
		Audio.Play("event:/char/theo/resort_ceilingvent_seeya", Position);
		int to = -24;
		float from = Sprite.Y;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 2f)
		{
			yield return null;
			Level.ParticlesFG.Emit(ParticleTypes.VentDust, 1, Position, new Vector2(6f, 0f));
			Sprite.Y = from + ((float)to - from) * Ease.BackIn(p);
		}
	}
}
