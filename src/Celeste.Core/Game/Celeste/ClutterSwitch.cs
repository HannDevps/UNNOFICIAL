using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ClutterSwitch : Solid
{
	[CompilerGenerated]
	private sealed class _003CLightningRoutine_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClutterSwitch _003C_003E4__this;

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
		public _003CLightningRoutine_003Ed__25(int _003C_003E1__state)
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
			ClutterSwitch clutterSwitch = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Level level = clutterSwitch.SceneAs<Level>();
				level.Session.SetFlag("disable_lightning");
				level.Session.Audio.Music.Progress++;
				level.Session.Audio.Apply();
				_003C_003E2__current = Lightning.RemoveRoutine(level);
				_003C_003E1__state = 1;
				return true;
			}
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
	private sealed class _003C_003Ec__DisplayClass26_0
	{
		public ClutterSwitch _003C_003E4__this;

		public Level level;

		internal void _003CAbsorbRoutine_003Eb__0()
		{
			Audio.Play("event:/game/03_resort/clutterswitch_finish", _003C_003E4__this.Position);
		}

		internal void _003CAbsorbRoutine_003Eb__1()
		{
			_003C_003Ec__DisplayClass26_2 CS_0024_003C_003E8__locals7 = new _003C_003Ec__DisplayClass26_2
			{
				CS_0024_003C_003E8__locals2 = this,
				start = _003C_003E4__this.vertexLight.StartRadius,
				end = _003C_003E4__this.vertexLight.EndRadius
			};
			Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
			tween.OnUpdate = delegate(Tween t)
			{
				CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals2.level.Lighting.Alpha = MathHelper.Lerp(0.05f, CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals2.level.BaseLightingAlpha + CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals2.level.Session.LightingAlphaAdd, t.Eased);
				CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals2._003C_003E4__this.vertexLight.StartRadius = (int)Math.Round(MathHelper.Lerp(CS_0024_003C_003E8__locals7.start, 24f, t.Eased));
				CS_0024_003C_003E8__locals7.CS_0024_003C_003E8__locals2._003C_003E4__this.vertexLight.EndRadius = (int)Math.Round(MathHelper.Lerp(CS_0024_003C_003E8__locals7.end, 48f, t.Eased));
			};
			_003C_003E4__this.Add(tween);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass26_1
	{
		public float start;

		public _003C_003Ec__DisplayClass26_0 CS_0024_003C_003E8__locals1;

		internal void _003CAbsorbRoutine_003Eb__2(Tween t)
		{
			CS_0024_003C_003E8__locals1.level.Lighting.Alpha = MathHelper.Lerp(start, 0.05f, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass26_2
	{
		public float start;

		public float end;

		public _003C_003Ec__DisplayClass26_0 CS_0024_003C_003E8__locals2;

		internal void _003CAbsorbRoutine_003Eb__3(Tween t)
		{
			CS_0024_003C_003E8__locals2.level.Lighting.Alpha = MathHelper.Lerp(0.05f, CS_0024_003C_003E8__locals2.level.BaseLightingAlpha + CS_0024_003C_003E8__locals2.level.Session.LightingAlphaAdd, t.Eased);
			CS_0024_003C_003E8__locals2._003C_003E4__this.vertexLight.StartRadius = (int)Math.Round(MathHelper.Lerp(start, 24f, t.Eased));
			CS_0024_003C_003E8__locals2._003C_003E4__this.vertexLight.EndRadius = (int)Math.Round(MathHelper.Lerp(end, 48f, t.Eased));
		}
	}

	[CompilerGenerated]
	private sealed class _003CAbsorbRoutine_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClutterSwitch _003C_003E4__this;

		public Player player;

		private _003C_003Ec__DisplayClass26_0 _003C_003E8__1;

		private Vector2 _003Ctarget_003E5__2;

		private ClutterAbsorbEffect _003Ceffect_003E5__3;

		private List<MTexture> _003Cimages_003E5__4;

		private int _003Ci_003E5__5;

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
		public _003CAbsorbRoutine_003Ed__26(int _003C_003E1__state)
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
			ClutterSwitch clutterSwitch = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass26_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				clutterSwitch.Add(clutterSwitch.cutsceneSfx = new SoundSource());
				float duration = 0f;
				if (clutterSwitch.color == ClutterBlock.Colors.Green)
				{
					clutterSwitch.cutsceneSfx.Play("event:/game/03_resort/clutterswitch_books");
					duration = 6.366f;
				}
				else if (clutterSwitch.color == ClutterBlock.Colors.Red)
				{
					clutterSwitch.cutsceneSfx.Play("event:/game/03_resort/clutterswitch_linens");
					duration = 6.15f;
				}
				else if (clutterSwitch.color == ClutterBlock.Colors.Yellow)
				{
					clutterSwitch.cutsceneSfx.Play("event:/game/03_resort/clutterswitch_boxes");
					duration = 6.066f;
				}
				clutterSwitch.Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
				{
					Audio.Play("event:/game/03_resort/clutterswitch_finish", _003C_003E8__1._003C_003E4__this.Position);
				}, duration, start: true));
				player.StateMachine.State = 11;
				_003Ctarget_003E5__2 = clutterSwitch.Position + new Vector2(clutterSwitch.Width / 2f, 0f);
				_003Ceffect_003E5__3 = new ClutterAbsorbEffect();
				clutterSwitch.Scene.Add(_003Ceffect_003E5__3);
				clutterSwitch.sprite.Play("break");
				_003C_003E8__1.level = clutterSwitch.SceneAs<Level>();
				_003C_003E8__1.level.Session.Audio.Music.Progress++;
				_003C_003E8__1.level.Session.Audio.Apply();
				_003C_003E8__1.level.Session.LightingAlphaAdd -= 0.05f;
				_003C_003Ec__DisplayClass26_1 CS_0024_003C_003E8__locals5 = new _003C_003Ec__DisplayClass26_1();
				CS_0024_003C_003E8__locals5.CS_0024_003C_003E8__locals1 = _003C_003E8__1;
				CS_0024_003C_003E8__locals5.start = CS_0024_003C_003E8__locals5.CS_0024_003C_003E8__locals1.level.Lighting.Alpha;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					CS_0024_003C_003E8__locals5.CS_0024_003C_003E8__locals1.level.Lighting.Alpha = MathHelper.Lerp(CS_0024_003C_003E8__locals5.start, 0.05f, t.Eased);
				};
				clutterSwitch.Add(tween);
				Alarm.Set(clutterSwitch, 3f, delegate
				{
					_003C_003Ec__DisplayClass26_2 CS_0024_003C_003E8__locals13 = new _003C_003Ec__DisplayClass26_2
					{
						CS_0024_003C_003E8__locals2 = _003C_003E8__1,
						start = _003C_003E8__1._003C_003E4__this.vertexLight.StartRadius,
						end = _003C_003E8__1._003C_003E4__this.vertexLight.EndRadius
					};
					Tween tween2 = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
					tween2.OnUpdate = delegate(Tween t)
					{
						CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals2.level.Lighting.Alpha = MathHelper.Lerp(0.05f, CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals2.level.BaseLightingAlpha + CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals2.level.Session.LightingAlphaAdd, t.Eased);
						CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals2._003C_003E4__this.vertexLight.StartRadius = (int)Math.Round(MathHelper.Lerp(CS_0024_003C_003E8__locals13.start, 24f, t.Eased));
						CS_0024_003C_003E8__locals13.CS_0024_003C_003E8__locals2._003C_003E4__this.vertexLight.EndRadius = (int)Math.Round(MathHelper.Lerp(CS_0024_003C_003E8__locals13.end, 48f, t.Eased));
					};
					_003C_003E8__1._003C_003E4__this.Add(tween2);
				});
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				foreach (ClutterBlock item in clutterSwitch.Scene.Entities.FindAll<ClutterBlock>())
				{
					if (item.BlockColor == clutterSwitch.color)
					{
						item.Absorb(_003Ceffect_003E5__3);
					}
				}
				foreach (ClutterBlockBase item2 in clutterSwitch.Scene.Entities.FindAll<ClutterBlockBase>())
				{
					if (item2.BlockColor == clutterSwitch.color)
					{
						item2.Deactivate();
					}
				}
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				player.StateMachine.State = 0;
				_003Cimages_003E5__4 = GFX.Game.GetAtlasSubtextures("objects/resortclutter/" + clutterSwitch.color.ToString() + "_");
				_003Ci_003E5__5 = 0;
				goto IL_041f;
			case 2:
				_003C_003E1__state = -1;
				_003Ci_003E5__5++;
				goto IL_041f;
			case 3:
				_003C_003E1__state = -1;
				_003Ceffect_003E5__3.CloseCabinets();
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Medium, RumbleLength.FullSecond);
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_041f:
				if (_003Ci_003E5__5 < 25)
				{
					for (int i = 0; i < 5; i++)
					{
						Vector2 position = _003Ctarget_003E5__2 + Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 320f);
						_003Ceffect_003E5__3.FlyClutter(position, Calc.Random.Choose(_003Cimages_003E5__4), shake: false, 0f);
					}
					_003C_003E8__1.level.Shake();
					Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
					_003C_003E2__current = 0.05f;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 3;
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

	public const float LightingAlphaAdd = 0.05f;

	public static ParticleType P_Pressed;

	public static ParticleType P_ClutterFly;

	private const int PressedAdd = 10;

	private const int PressedSpriteAdd = 2;

	private const int UnpressedLightRadius = 32;

	private const int PressedLightRadius = 24;

	private const int BrightLightRadius = 64;

	private ClutterBlock.Colors color;

	private float startY;

	private float atY;

	private float speedY;

	private bool pressed;

	private Sprite sprite;

	private Image icon;

	private float targetXScale = 1f;

	private VertexLight vertexLight;

	private bool playerWasOnTop;

	private SoundSource cutsceneSfx;

	public ClutterSwitch(Vector2 position, ClutterBlock.Colors color)
		: base(position, 32f, 16f, safe: true)
	{
		this.color = color;
		startY = (atY = base.Y);
		OnDashCollide = OnDashed;
		SurfaceSoundIndex = 21;
		Add(sprite = GFX.SpriteBank.Create("clutterSwitch"));
		sprite.Position = new Vector2(16f, 16f);
		sprite.Play("idle");
		Add(icon = new Image(GFX.Game["objects/resortclutter/icon_" + color]));
		icon.CenterOrigin();
		icon.Position = new Vector2(16f, 8f);
		Add(vertexLight = new VertexLight(new Vector2(base.CenterX - base.X, -1f), Color.Aqua, 1f, 32, 64));
	}

	public ClutterSwitch(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Enum("type", ClutterBlock.Colors.Green))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (color == ClutterBlock.Colors.Lightning && SceneAs<Level>().Session.GetFlag("disable_lightning"))
		{
			BePressed();
		}
		else if (SceneAs<Level>().Session.GetFlag("oshiro_clutter_cleared_" + (int)color))
		{
			BePressed();
		}
	}

	private void BePressed()
	{
		pressed = true;
		atY += 10f;
		base.Y += 10f;
		sprite.Y += 2f;
		sprite.Play("active");
		Remove(icon);
		vertexLight.StartRadius = 24f;
		vertexLight.EndRadius = 48f;
	}

	public override void Update()
	{
		base.Update();
		if (HasPlayerOnTop())
		{
			if (speedY < 0f)
			{
				speedY = 0f;
			}
			speedY = Calc.Approach(speedY, 70f, 200f * Engine.DeltaTime);
			MoveTowardsY(atY + (float)(pressed ? 2 : 4), speedY * Engine.DeltaTime);
			targetXScale = 1.2f;
			if (!playerWasOnTop)
			{
				Audio.Play("event:/game/03_resort/clutterswitch_squish", Position);
			}
			playerWasOnTop = true;
		}
		else
		{
			if (speedY > 0f)
			{
				speedY = 0f;
			}
			speedY = Calc.Approach(speedY, -150f, 200f * Engine.DeltaTime);
			MoveTowardsY(atY, (0f - speedY) * Engine.DeltaTime);
			targetXScale = 1f;
			if (playerWasOnTop)
			{
				Audio.Play("event:/game/03_resort/clutterswitch_return", Position);
			}
			playerWasOnTop = false;
		}
		sprite.Scale.X = Calc.Approach(sprite.Scale.X, targetXScale, 0.8f * Engine.DeltaTime);
	}

	private DashCollisionResults OnDashed(Player player, Vector2 direction)
	{
		if (!pressed && direction == Vector2.UnitY)
		{
			Celeste.Freeze(0.2f);
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			Level obj = base.Scene as Level;
			obj.Session.SetFlag("oshiro_clutter_cleared_" + (int)color);
			obj.Session.SetFlag("oshiro_clutter_door_open", setTo: false);
			vertexLight.StartRadius = 64f;
			vertexLight.EndRadius = 128f;
			obj.DirectionalShake(Vector2.UnitY, 0.6f);
			obj.Particles.Emit(P_Pressed, 20, base.TopCenter - Vector2.UnitY * 10f, new Vector2(16f, 8f));
			BePressed();
			sprite.Scale.X = 1.5f;
			if (color == ClutterBlock.Colors.Lightning)
			{
				Add(new Coroutine(LightningRoutine(player)));
			}
			else
			{
				Add(new Coroutine(AbsorbRoutine(player)));
			}
		}
		return DashCollisionResults.NormalCollision;
	}

	[IteratorStateMachine(typeof(_003CLightningRoutine_003Ed__25))]
	private IEnumerator LightningRoutine(Player player)
	{
		Level level = SceneAs<Level>();
		level.Session.SetFlag("disable_lightning");
		level.Session.Audio.Music.Progress++;
		level.Session.Audio.Apply();
		yield return Lightning.RemoveRoutine(level);
	}

	[IteratorStateMachine(typeof(_003CAbsorbRoutine_003Ed__26))]
	private IEnumerator AbsorbRoutine(Player player)
	{
		Add(cutsceneSfx = new SoundSource());
		float duration = 0f;
		if (color == ClutterBlock.Colors.Green)
		{
			cutsceneSfx.Play("event:/game/03_resort/clutterswitch_books");
			duration = 6.366f;
		}
		else if (color == ClutterBlock.Colors.Red)
		{
			cutsceneSfx.Play("event:/game/03_resort/clutterswitch_linens");
			duration = 6.15f;
		}
		else if (color == ClutterBlock.Colors.Yellow)
		{
			cutsceneSfx.Play("event:/game/03_resort/clutterswitch_boxes");
			duration = 6.066f;
		}
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			Audio.Play("event:/game/03_resort/clutterswitch_finish", Position);
		}, duration, start: true));
		player.StateMachine.State = 11;
		Vector2 target = Position + new Vector2(base.Width / 2f, 0f);
		ClutterAbsorbEffect effect = new ClutterAbsorbEffect();
		base.Scene.Add(effect);
		sprite.Play("break");
		Level level = SceneAs<Level>();
		level.Session.Audio.Music.Progress++;
		level.Session.Audio.Apply();
		level.Session.LightingAlphaAdd -= 0.05f;
		float start = level.Lighting.Alpha;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			level.Lighting.Alpha = MathHelper.Lerp(start, 0.05f, t.Eased);
		};
		Add(tween);
		Alarm.Set(this, 3f, delegate
		{
			float start2 = vertexLight.StartRadius;
			float end = vertexLight.EndRadius;
			Tween tween2 = Tween.Create(Tween.TweenMode.Oneshot, Ease.SineInOut, 2f, start: true);
			tween2.OnUpdate = delegate(Tween t)
			{
				level.Lighting.Alpha = MathHelper.Lerp(0.05f, level.BaseLightingAlpha + level.Session.LightingAlphaAdd, t.Eased);
				vertexLight.StartRadius = (int)Math.Round(MathHelper.Lerp(start2, 24f, t.Eased));
				vertexLight.EndRadius = (int)Math.Round(MathHelper.Lerp(end, 48f, t.Eased));
			};
			Add(tween2);
		});
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		foreach (ClutterBlock item in base.Scene.Entities.FindAll<ClutterBlock>())
		{
			if (item.BlockColor == color)
			{
				item.Absorb(effect);
			}
		}
		foreach (ClutterBlockBase item2 in base.Scene.Entities.FindAll<ClutterBlockBase>())
		{
			if (item2.BlockColor == color)
			{
				item2.Deactivate();
			}
		}
		yield return 1.5f;
		player.StateMachine.State = 0;
		List<MTexture> images = GFX.Game.GetAtlasSubtextures("objects/resortclutter/" + color.ToString() + "_");
		for (int i = 0; i < 25; i++)
		{
			for (int num = 0; num < 5; num++)
			{
				Vector2 position = target + Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 320f);
				effect.FlyClutter(position, Calc.Random.Choose(images), shake: false, 0f);
			}
			level.Shake();
			Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
			yield return 0.05f;
		}
		yield return 1.5f;
		effect.CloseCabinets();
		yield return 0.2f;
		Input.Rumble(RumbleStrength.Medium, RumbleLength.FullSecond);
		yield return 0.3f;
	}

	public override void Removed(Scene scene)
	{
		Level level = scene as Level;
		level.Lighting.Alpha = level.BaseLightingAlpha + level.Session.LightingAlphaAdd;
		base.Removed(scene);
	}
}
