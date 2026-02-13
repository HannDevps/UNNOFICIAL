using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC03_Theo_Escaping : NPC
{
	public class Grate : Entity
	{
		public Image Sprite;

		private Vector2 speed;

		private bool falling;

		private float alpha = 1f;

		public Grate(Vector2 position)
			: base(position)
		{
			Add(Sprite = new Image(GFX.Game["scenery/grate"]));
			Sprite.JustifyOrigin(0.5f, 0f);
			Sprite.Rotation = (float)Math.PI / 2f;
		}

		public void Fall()
		{
			Audio.Play("event:/char/theo/resort_vent_tumble", Position);
			falling = true;
			speed = new Vector2(-120f, -120f);
			base.Collider = new Hitbox(2f, 2f, -2f, -1f);
		}

		public override void Update()
		{
			if (falling)
			{
				speed.X = Calc.Approach(speed.X, 0f, Engine.DeltaTime * 120f);
				speed.Y += 400f * Engine.DeltaTime;
				Position += speed * Engine.DeltaTime;
				if (CollideCheck<Solid>(Position + new Vector2(0f, 2f)) && speed.Y > 0f)
				{
					speed.Y = (0f - speed.Y) * 0.25f;
				}
				alpha -= Engine.DeltaTime;
				Sprite.Rotation += Engine.DeltaTime * speed.Length() * 0.05f;
				Sprite.Color = Color.White * alpha;
				if (alpha <= 0f)
				{
					RemoveSelf();
				}
			}
			base.Update();
		}
	}

	[CompilerGenerated]
	private sealed class _003CCrawlUntilOutRoutine_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Theo_Escaping _003C_003E4__this;

		private int _003Ctarget_003E5__2;

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
		public _003CCrawlUntilOutRoutine_003Ed__8(int _003C_003E1__state)
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
			NPC03_Theo_Escaping nPC03_Theo_Escaping = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC03_Theo_Escaping.AddTag(Tags.Global);
				_003Ctarget_003E5__2 = (nPC03_Theo_Escaping.Scene as Level).Bounds.Right + 280;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (nPC03_Theo_Escaping.X != (float)_003Ctarget_003E5__2)
			{
				nPC03_Theo_Escaping.X = Calc.Approach(nPC03_Theo_Escaping.X, _003Ctarget_003E5__2, 20f * Engine.DeltaTime);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			nPC03_Theo_Escaping.Scene.Remove(nPC03_Theo_Escaping);
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

	private bool talked;

	private VertexLight light;

	public Grate grate;

	public NPC03_Theo_Escaping(Vector2 position)
		: base(position)
	{
		Add(Sprite = GFX.SpriteBank.Create("theo"));
		Sprite.Play("idle");
		Sprite.X = -4f;
		SetupTheoSpriteSounds();
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Add(light = new VertexLight(base.Center - Position, Color.White, 1f, 32, 64));
		while (!CollideCheck<Solid>(Position + new Vector2(1f, 0f)))
		{
			base.X++;
		}
		grate = new Grate(Position + new Vector2(base.Width / 2f, -8f));
		base.Scene.Add(grate);
		Sprite.Play("goToVent");
	}

	public override void Update()
	{
		base.Update();
		Player player = base.Scene.Entities.FindFirst<Player>();
		if (player != null && !talked && player.X > base.X - 100f)
		{
			Talk(player);
		}
		if (Sprite.CurrentAnimationID == "pullVent" && Sprite.CurrentAnimationFrame > 0)
		{
			grate.Sprite.X = 0f;
		}
		else
		{
			grate.Sprite.X = 1f;
		}
	}

	private void Talk(Player player)
	{
		talked = true;
		base.Scene.Add(new CS03_TheoEscape(this, player));
	}

	public void CrawlUntilOut()
	{
		Sprite.Scale.X = 1f;
		Sprite.Play("crawl");
		Add(new Coroutine(CrawlUntilOutRoutine()));
	}

	[IteratorStateMachine(typeof(_003CCrawlUntilOutRoutine_003Ed__8))]
	private IEnumerator CrawlUntilOutRoutine()
	{
		AddTag(Tags.Global);
		int target = (base.Scene as Level).Bounds.Right + 280;
		while (base.X != (float)target)
		{
			base.X = Calc.Approach(base.X, target, 20f * Engine.DeltaTime);
			yield return null;
		}
		base.Scene.Remove(this);
	}
}
