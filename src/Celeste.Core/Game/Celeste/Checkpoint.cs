using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Checkpoint : Entity
{
	[CompilerGenerated]
	private sealed class _003CEaseLightsOn_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Checkpoint _003C_003E4__this;

		private float _003ClightStartRadius_003E5__2;

		private float _003ClightEndRadius_003E5__3;

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
		public _003CEaseLightsOn_003Ed__18(int _003C_003E1__state)
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
			Checkpoint checkpoint = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003ClightStartRadius_003E5__2 = checkpoint.light.StartRadius;
				_003ClightEndRadius_003E5__3 = checkpoint.light.EndRadius;
				_003Cp_003E5__4 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime * 0.5f;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
			{
				float num2 = Ease.BigBackOut(_003Cp_003E5__4);
				checkpoint.light.Alpha = 0.8f * num2;
				checkpoint.light.StartRadius = (int)(_003ClightStartRadius_003E5__2 + Calc.YoYo(_003Cp_003E5__4) * 8f);
				checkpoint.light.EndRadius = (int)(_003ClightEndRadius_003E5__3 + Calc.YoYo(_003Cp_003E5__4) * 16f);
				checkpoint.bloom.Alpha = 0.5f * num2;
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

	private const float LightAlpha = 0.8f;

	private const float BloomAlpha = 0.5f;

	private const float TargetFade = 0.5f;

	private Image image;

	private Sprite sprite;

	private Sprite flash;

	private VertexLight light;

	private BloomPoint bloom;

	private bool triggered;

	private float sine = (float)Math.PI / 2f;

	private float fade = 1f;

	private string bg;

	public Vector2 SpawnOffset;

	public Checkpoint(Vector2 position, string bg = "", Vector2? spawnTarget = null)
		: base(position)
	{
		base.Depth = 9990;
		SpawnOffset = (spawnTarget.HasValue ? (spawnTarget.Value - Position) : Vector2.Zero);
		this.bg = bg;
	}

	public Checkpoint(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Attr("bg"), data.FirstNodeNullable(offset))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Level level = base.Scene as Level;
		int iD = level.Session.Area.ID;
		string text = "";
		text = ((!string.IsNullOrWhiteSpace(bg)) ? ("objects/checkpoint/bg/" + bg) : ("objects/checkpoint/bg/" + iD));
		if (GFX.Game.Has(text))
		{
			Add(image = new Image(GFX.Game[text]));
			image.JustifyOrigin(0.5f, 1f);
		}
		Add(sprite = GFX.SpriteBank.Create("checkpoint_highlight"));
		sprite.Play("off");
		Add(flash = GFX.SpriteBank.Create("checkpoint_flash"));
		flash.Visible = false;
		flash.Color = Color.White * 0.6f;
		if (SaveData.Instance.HasCheckpoint(level.Session.Area, level.Session.Level))
		{
			TurnOn(animate: false);
		}
	}

	public override void Update()
	{
		if (!triggered)
		{
			Level level = base.Scene as Level;
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && !level.Transitioning)
			{
				if (!entity.CollideCheck<CheckpointBlockerTrigger>() && SaveData.Instance.SetCheckpoint(level.Session.Area, level.Session.Level))
				{
					level.AutoSave();
					TurnOn(animate: true);
				}
				triggered = true;
			}
		}
		if (triggered && sprite.CurrentAnimationID == "on")
		{
			sine += Engine.DeltaTime * 2f;
			fade = Calc.Approach(fade, 0.5f, Engine.DeltaTime);
			float num = (float)(1.0 + Math.Sin(sine)) / 2f;
			sprite.Color = Color.White * (0.5f + num * 0.5f) * fade;
		}
		base.Update();
	}

	private void TurnOn(bool animate)
	{
		triggered = true;
		Add(light = new VertexLight(Color.White, 0f, 16, 32));
		Add(bloom = new BloomPoint(0f, 16f));
		light.Position = new Vector2(0f, -8f);
		bloom.Position = new Vector2(0f, -8f);
		flash.Visible = true;
		flash.Play("flash", restart: true);
		if (animate)
		{
			sprite.Play("turnOn");
			Add(new Coroutine(EaseLightsOn()));
			fade = 1f;
		}
		else
		{
			fade = 0.5f;
			sprite.Play("on");
			sprite.Color = Color.White * 0.5f;
			light.Alpha = 0.8f;
			bloom.Alpha = 0.5f;
		}
	}

	[IteratorStateMachine(typeof(_003CEaseLightsOn_003Ed__18))]
	private IEnumerator EaseLightsOn()
	{
		float lightStartRadius = light.StartRadius;
		float lightEndRadius = light.EndRadius;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 0.5f)
		{
			float num = Ease.BigBackOut(p);
			light.Alpha = 0.8f * num;
			light.StartRadius = (int)(lightStartRadius + Calc.YoYo(p) * 8f);
			light.EndRadius = (int)(lightEndRadius + Calc.YoYo(p) * 16f);
			bloom.Alpha = 0.5f * num;
			yield return null;
		}
	}
}
