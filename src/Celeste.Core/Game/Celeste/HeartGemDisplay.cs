using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class HeartGemDisplay : Component
{
	[CompilerGenerated]
	private sealed class _003CAppearSequence_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Sprite sprite;

		public HeartGemDisplay _003C_003E4__this;

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
		public _003CAppearSequence_003Ed__17(int _003C_003E1__state)
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
			HeartGemDisplay heartGemDisplay = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				sprite.Play("idle");
				sprite.Visible = true;
				sprite.Scale = new Vector2(0.8f, 1.4f);
				_003C_003E2__current = heartGemDisplay.tween.Wait();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				sprite.Scale = new Vector2(1.4f, 0.8f);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				sprite.CenterOrigin();
				heartGemDisplay.rotateWiggler.Start();
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				sprite.Play("spin");
				heartGemDisplay.routine = null;
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

	public Vector2 Position;

	public Sprite[] Sprites;

	public Vector2 TargetPosition;

	private Image bg;

	private Wiggler rotateWiggler;

	private Coroutine routine;

	private Vector2 bounce;

	private Tween tween;

	private Color SpriteColor
	{
		get
		{
			return Sprites[0].Color;
		}
		set
		{
			for (int i = 0; i < Sprites.Length; i++)
			{
				Sprites[i].Color = value;
			}
		}
	}

	public HeartGemDisplay(int heartgem, bool hasGem)
		: base(active: true, visible: true)
	{
		Sprites = new Sprite[3];
		for (int i = 0; i < Sprites.Length; i++)
		{
			Sprites[i] = GFX.GuiSpriteBank.Create("heartgem" + i);
			Sprites[i].Visible = heartgem == i && hasGem;
			Sprites[i].Play("spin");
		}
		bg = new Image(GFX.Gui["collectables/heartgem/0/spin00"]);
		bg.Color = Color.Black;
		bg.CenterOrigin();
		rotateWiggler = Wiggler.Create(0.4f, 6f);
		rotateWiggler.UseRawDeltaTime = true;
		SimpleCurve curve = new SimpleCurve(Vector2.UnitY * 80f, Vector2.Zero, Vector2.UnitY * -160f);
		tween = Tween.Create(Tween.TweenMode.Oneshot, null, 0.4f);
		tween.OnStart = delegate
		{
			SpriteColor = Color.Transparent;
		};
		tween.OnUpdate = delegate(Tween t)
		{
			bounce = curve.GetPoint(t.Eased);
			SpriteColor = Color.White * Calc.LerpClamp(0f, 1f, t.Percent * 1.5f);
		};
	}

	public void Wiggle()
	{
		rotateWiggler.Start();
		for (int i = 0; i < Sprites.Length; i++)
		{
			if (Sprites[i].Visible)
			{
				Sprites[i].Play("spin", restart: true);
				Sprites[i].SetAnimationFrame(19);
			}
		}
	}

	public void Appear(AreaMode mode)
	{
		tween.Start();
		routine = new Coroutine(AppearSequence(Sprites[(int)mode]));
		routine.UseRawDeltaTime = true;
	}

	public void SetCurrentMode(AreaMode mode, bool has)
	{
		for (int i = 0; i < Sprites.Length; i++)
		{
			Sprites[i].Visible = i == (int)mode && has;
		}
		if (!has)
		{
			routine = null;
		}
	}

	public override void Update()
	{
		base.Update();
		if (routine != null && routine.Active)
		{
			routine.Update();
		}
		if (rotateWiggler.Active)
		{
			rotateWiggler.Update();
		}
		for (int i = 0; i < Sprites.Length; i++)
		{
			if (Sprites[i].Active)
			{
				Sprites[i].Update();
			}
		}
		if (tween != null && tween.Active)
		{
			tween.Update();
		}
		Position = Calc.Approach(Position, TargetPosition, 200f * Engine.DeltaTime);
		for (int j = 0; j < Sprites.Length; j++)
		{
			Sprites[j].Scale.X = Calc.Approach(Sprites[j].Scale.X, 1f, 2f * Engine.DeltaTime);
			Sprites[j].Scale.Y = Calc.Approach(Sprites[j].Scale.Y, 1f, 2f * Engine.DeltaTime);
		}
	}

	public override void Render()
	{
		base.Render();
		bg.Position = base.Entity.Position + Position;
		for (int i = 0; i < Sprites.Length; i++)
		{
			if (Sprites[i].Visible)
			{
				Sprites[i].Rotation = rotateWiggler.Value * 30f * ((float)Math.PI / 180f);
				Sprites[i].Position = base.Entity.Position + Position + bounce;
				Sprites[i].Render();
			}
		}
	}

	[IteratorStateMachine(typeof(_003CAppearSequence_003Ed__17))]
	private IEnumerator AppearSequence(Sprite sprite)
	{
		sprite.Play("idle");
		sprite.Visible = true;
		sprite.Scale = new Vector2(0.8f, 1.4f);
		yield return tween.Wait();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		sprite.Scale = new Vector2(1.4f, 0.8f);
		yield return 0.4f;
		sprite.CenterOrigin();
		rotateWiggler.Start();
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		sprite.Play("spin");
		routine = null;
	}
}
