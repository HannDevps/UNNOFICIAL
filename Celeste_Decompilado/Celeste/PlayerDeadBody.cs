using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class PlayerDeadBody : Entity
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass15_0
	{
		public Vector2 from;

		public Vector2 to;

		public PlayerDeadBody _003C_003E4__this;

		internal void _003CDeathRoutine_003Eb__1(Tween t)
		{
			_003C_003E4__this.Position = from + (to - from) * t.Eased;
			_003C_003E4__this.scale = 1.5f - t.Eased * 0.5f;
			_003C_003E4__this.sprite.Rotation = (float)(Math.Floor(t.Eased * 4f) * 6.2831854820251465);
		}
	}

	[CompilerGenerated]
	private sealed class _003CDeathRoutine_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PlayerDeadBody _003C_003E4__this;

		private _003C_003Ec__DisplayClass15_0 _003C_003E8__1;

		private Level _003Clevel_003E5__2;

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
		public _003CDeathRoutine_003Ed__15(int _003C_003E1__state)
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
			PlayerDeadBody CS_0024_003C_003E8__locals24 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = CS_0024_003C_003E8__locals24.SceneAs<Level>();
				if (CS_0024_003C_003E8__locals24.bounce != Vector2.Zero)
				{
					_003C_003E8__1 = new _003C_003Ec__DisplayClass15_0();
					_003C_003E8__1._003C_003E4__this = CS_0024_003C_003E8__locals24;
					Audio.Play("event:/char/madeline/predeath", CS_0024_003C_003E8__locals24.Position);
					CS_0024_003C_003E8__locals24.scale = 1.5f;
					Celeste.Freeze(0.05f);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0166;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E8__1.from = CS_0024_003C_003E8__locals24.Position;
				_003C_003E8__1.to = _003C_003E8__1.from + CS_0024_003C_003E8__locals24.bounce * 24f;
				_003Ctween_003E5__3 = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 0.5f, start: true);
				CS_0024_003C_003E8__locals24.Add(_003Ctween_003E5__3);
				_003Ctween_003E5__3.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1._003C_003E4__this.Position = _003C_003E8__1.from + (_003C_003E8__1.to - _003C_003E8__1.from) * t.Eased;
					_003C_003E8__1._003C_003E4__this.scale = 1.5f - t.Eased * 0.5f;
					_003C_003E8__1._003C_003E4__this.sprite.Rotation = (float)(Math.Floor(t.Eased * 4f) * 6.2831854820251465);
				};
				_003C_003E2__current = _003Ctween_003E5__3.Duration * 0.75f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003Ctween_003E5__3.Stop();
				_003C_003E8__1 = null;
				_003Ctween_003E5__3 = null;
				goto IL_0166;
			case 3:
				_003C_003E1__state = -1;
				if (CS_0024_003C_003E8__locals24.ActionDelay > 0f)
				{
					_003C_003E2__current = CS_0024_003C_003E8__locals24.ActionDelay;
					_003C_003E1__state = 4;
					return true;
				}
				break;
			case 4:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0166:
				CS_0024_003C_003E8__locals24.Position += Vector2.UnitY * -5f;
				_003Clevel_003E5__2.Displacement.AddBurst(CS_0024_003C_003E8__locals24.Position, 0.3f, 0f, 80f);
				_003Clevel_003E5__2.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				Audio.Play(CS_0024_003C_003E8__locals24.HasGolden ? "event:/new_content/char/madeline/death_golden" : "event:/char/madeline/death", CS_0024_003C_003E8__locals24.Position);
				CS_0024_003C_003E8__locals24.deathEffect = new DeathEffect(CS_0024_003C_003E8__locals24.initialHairColor, CS_0024_003C_003E8__locals24.Center - CS_0024_003C_003E8__locals24.Position);
				CS_0024_003C_003E8__locals24.deathEffect.OnUpdate = delegate(float f)
				{
					CS_0024_003C_003E8__locals24.light.Alpha = 1f - f;
				};
				CS_0024_003C_003E8__locals24.Add(CS_0024_003C_003E8__locals24.deathEffect);
				_003C_003E2__current = CS_0024_003C_003E8__locals24.deathEffect.Duration * 0.65f;
				_003C_003E1__state = 3;
				return true;
			}
			CS_0024_003C_003E8__locals24.End();
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

	public Action DeathAction;

	public float ActionDelay;

	public bool HasGolden;

	private Color initialHairColor;

	private Vector2 bounce = Vector2.Zero;

	private Player player;

	private PlayerHair hair;

	private PlayerSprite sprite;

	private VertexLight light;

	private DeathEffect deathEffect;

	private Facings facing;

	private float scale = 1f;

	private bool finished;

	public PlayerDeadBody(Player player, Vector2 direction)
	{
		base.Depth = -1000000;
		this.player = player;
		facing = player.Facing;
		Position = player.Position;
		Add(hair = player.Hair);
		Add(sprite = player.Sprite);
		Add(light = player.Light);
		sprite.Color = Color.White;
		initialHairColor = hair.Color;
		bounce = direction;
		Add(new Coroutine(DeathRoutine()));
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (!(bounce != Vector2.Zero))
		{
			return;
		}
		if (Math.Abs(bounce.X) > Math.Abs(bounce.Y))
		{
			sprite.Play("deadside");
			facing = (Facings)(-Math.Sign(bounce.X));
			return;
		}
		bounce = Calc.AngleToVector(Calc.AngleApproach(bounce.Angle(), new Vector2(0 - player.Facing, 0f).Angle(), 0.5f), 1f);
		if (bounce.Y < 0f)
		{
			sprite.Play("deadup");
		}
		else
		{
			sprite.Play("deaddown");
		}
	}

	[IteratorStateMachine(typeof(_003CDeathRoutine_003Ed__15))]
	private IEnumerator DeathRoutine()
	{
		Level level = SceneAs<Level>();
		if (bounce != Vector2.Zero)
		{
			Audio.Play("event:/char/madeline/predeath", Position);
			scale = 1.5f;
			Celeste.Freeze(0.05f);
			yield return null;
			Vector2 from = Position;
			Vector2 to = from + bounce * 24f;
			Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 0.5f, start: true);
			Add(tween);
			tween.OnUpdate = delegate(Tween t)
			{
				Position = from + (to - from) * t.Eased;
				scale = 1.5f - t.Eased * 0.5f;
				sprite.Rotation = (float)(Math.Floor(t.Eased * 4f) * 6.2831854820251465);
			};
			yield return tween.Duration * 0.75f;
			tween.Stop();
		}
		Position += Vector2.UnitY * -5f;
		level.Displacement.AddBurst(Position, 0.3f, 0f, 80f);
		level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		Audio.Play(HasGolden ? "event:/new_content/char/madeline/death_golden" : "event:/char/madeline/death", Position);
		deathEffect = new DeathEffect(initialHairColor, base.Center - Position);
		deathEffect.OnUpdate = delegate(float f)
		{
			light.Alpha = 1f - f;
		};
		Add(deathEffect);
		yield return deathEffect.Duration * 0.65f;
		if (ActionDelay > 0f)
		{
			yield return ActionDelay;
		}
		End();
	}

	private void End()
	{
		if (!finished)
		{
			finished = true;
			Level level = SceneAs<Level>();
			if (DeathAction == null)
			{
				DeathAction = level.Reload;
			}
			level.DoScreenWipe(wipeIn: false, DeathAction);
		}
	}

	public override void Update()
	{
		base.Update();
		if (Input.MenuConfirm.Pressed && !finished)
		{
			End();
		}
		hair.Color = ((sprite.CurrentAnimationFrame == 0) ? Color.White : initialHairColor);
	}

	public override void Render()
	{
		if (deathEffect == null)
		{
			sprite.Scale.X = (float)facing * scale;
			sprite.Scale.Y = scale;
			hair.Facing = facing;
			base.Render();
		}
		else
		{
			deathEffect.Render();
		}
	}
}
