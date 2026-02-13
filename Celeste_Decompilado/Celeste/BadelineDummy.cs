using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class BadelineDummy : Entity
{
	[CompilerGenerated]
	private sealed class _003CFloatTo_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineDummy _003C_003E4__this;

		public bool faceDirection;

		public Vector2 target;

		public bool fadeLight;

		public bool quickEnd;

		public int? turnAtEndTo;

		private Vector2 _003Cperp_003E5__2;

		private float _003Cspeed_003E5__3;

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
		public _003CFloatTo_003Ed__13(int _003C_003E1__state)
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
			BadelineDummy badelineDummy = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				badelineDummy.Sprite.Play("fallSlow");
				if (faceDirection && Math.Sign(target.X - badelineDummy.X) != 0)
				{
					badelineDummy.Sprite.Scale.X = Math.Sign(target.X - badelineDummy.X);
				}
				Vector2 vector = (target - badelineDummy.Position).SafeNormalize();
				_003Cperp_003E5__2 = new Vector2(0f - vector.Y, vector.X);
				_003Cspeed_003E5__3 = 0f;
				goto IL_0196;
			}
			case 1:
				_003C_003E1__state = -1;
				goto IL_0196;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_01f9;
				}
				IL_01f9:
				if (badelineDummy.Floatness != 2f)
				{
					badelineDummy.Floatness = Calc.Approach(badelineDummy.Floatness, 2f, 8f * Engine.DeltaTime);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				break;
				IL_0196:
				if (badelineDummy.Position != target)
				{
					_003Cspeed_003E5__3 = Calc.Approach(_003Cspeed_003E5__3, badelineDummy.FloatSpeed, badelineDummy.FloatAccel * Engine.DeltaTime);
					badelineDummy.Position = Calc.Approach(badelineDummy.Position, target, _003Cspeed_003E5__3 * Engine.DeltaTime);
					badelineDummy.Floatness = Calc.Approach(badelineDummy.Floatness, 4f, 8f * Engine.DeltaTime);
					badelineDummy.floatNormal = Calc.Approach(badelineDummy.floatNormal, _003Cperp_003E5__2, Engine.DeltaTime * 12f);
					if (fadeLight)
					{
						badelineDummy.Light.Alpha = Calc.Approach(badelineDummy.Light.Alpha, 0f, Engine.DeltaTime * 2f);
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (quickEnd)
				{
					badelineDummy.Floatness = 2f;
					break;
				}
				goto IL_01f9;
			}
			if (turnAtEndTo.HasValue)
			{
				badelineDummy.Sprite.Scale.X = turnAtEndTo.Value;
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
	private sealed class _003CWalkTo_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineDummy _003C_003E4__this;

		public float x;

		public float speed;

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
		public _003CWalkTo_003Ed__14(int _003C_003E1__state)
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
			BadelineDummy badelineDummy = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				badelineDummy.Floatness = 0f;
				badelineDummy.Sprite.Play("walk");
				if (Math.Sign(x - badelineDummy.X) != 0)
				{
					badelineDummy.Sprite.Scale.X = Math.Sign(x - badelineDummy.X);
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (badelineDummy.X != x)
			{
				badelineDummy.X = Calc.Approach(badelineDummy.X, x, Engine.DeltaTime * speed);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			badelineDummy.Sprite.Play("idle");
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
	private sealed class _003CSmashBlock_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BadelineDummy _003C_003E4__this;

		public Vector2 target;

		private Vector2 _003Cfrom_003E5__2;

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
		public _003CSmashBlock_003Ed__15(int _003C_003E1__state)
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
			BadelineDummy badelineDummy = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				badelineDummy.SceneAs<Level>().Displacement.AddBurst(badelineDummy.Position, 0.5f, 24f, 96f);
				badelineDummy.Sprite.Play("dreamDashLoop");
				_003Cfrom_003E5__2 = badelineDummy.Position;
				_003Cp_003E5__3 = 0f;
				goto IL_00e7;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime * 6f;
				goto IL_00e7;
			case 2:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__3 += Engine.DeltaTime * 4f;
					break;
				}
				IL_00e7:
				if (_003Cp_003E5__3 < 1f)
				{
					badelineDummy.Position = _003Cfrom_003E5__2 + (target - _003Cfrom_003E5__2) * Ease.CubeOut(_003Cp_003E5__3);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				badelineDummy.Scene.Entities.FindFirst<DashBlock>().Break(badelineDummy.Position, new Vector2(0f, -1f), playSound: false);
				badelineDummy.Sprite.Play("idle");
				_003Cp_003E5__3 = 0f;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				badelineDummy.Position = target + (_003Cfrom_003E5__2 - target) * Ease.CubeOut(_003Cp_003E5__3);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			badelineDummy.Sprite.Play("fallSlow");
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

	public PlayerSprite Sprite;

	public PlayerHair Hair;

	public BadelineAutoAnimator AutoAnimator;

	public SineWave Wave;

	public VertexLight Light;

	public float FloatSpeed = 120f;

	public float FloatAccel = 240f;

	public float Floatness = 2f;

	public Vector2 floatNormal = new Vector2(0f, 1f);

	public BadelineDummy(Vector2 position)
		: base(position)
	{
		base.Collider = new Hitbox(6f, 6f, -3f, -7f);
		Sprite = new PlayerSprite(PlayerSpriteMode.Badeline);
		Sprite.Play("fallSlow");
		Sprite.Scale.X = -1f;
		Hair = new PlayerHair(Sprite);
		Hair.Color = BadelineOldsite.HairColor;
		Hair.Border = Color.Black;
		Hair.Facing = Facings.Left;
		Add(Hair);
		Add(Sprite);
		Add(AutoAnimator = new BadelineAutoAnimator());
		Sprite.OnFrameChange = delegate(string anim)
		{
			int currentAnimationFrame = Sprite.CurrentAnimationFrame;
			if ((anim == "walk" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "runSlow" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "runFast" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)))
			{
				Audio.Play("event:/char/badeline/footstep", Position);
			}
		};
		Add(Wave = new SineWave(0.25f));
		Wave.OnUpdate = delegate(float f)
		{
			Sprite.Position = floatNormal * f * Floatness;
		};
		Add(Light = new VertexLight(new Vector2(0f, -8f), Color.PaleVioletRed, 1f, 20, 60));
	}

	public void Appear(Level level, bool silent = false)
	{
		if (!silent)
		{
			Audio.Play("event:/char/badeline/appear", Position);
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		}
		level.Displacement.AddBurst(base.Center, 0.5f, 24f, 96f, 0.4f);
		level.Particles.Emit(BadelineOldsite.P_Vanish, 12, base.Center, Vector2.One * 6f);
	}

	public void Vanish()
	{
		Audio.Play("event:/char/badeline/disappear", Position);
		Shockwave();
		SceneAs<Level>().Particles.Emit(BadelineOldsite.P_Vanish, 12, base.Center, Vector2.One * 6f);
		RemoveSelf();
	}

	private void Shockwave()
	{
		SceneAs<Level>().Displacement.AddBurst(base.Center, 0.5f, 24f, 96f, 0.4f);
	}

	[IteratorStateMachine(typeof(_003CFloatTo_003Ed__13))]
	public IEnumerator FloatTo(Vector2 target, int? turnAtEndTo = null, bool faceDirection = true, bool fadeLight = false, bool quickEnd = false)
	{
		Sprite.Play("fallSlow");
		if (faceDirection && Math.Sign(target.X - base.X) != 0)
		{
			Sprite.Scale.X = Math.Sign(target.X - base.X);
		}
		Vector2 vector = (target - Position).SafeNormalize();
		Vector2 perp = new Vector2(0f - vector.Y, vector.X);
		float speed = 0f;
		while (Position != target)
		{
			speed = Calc.Approach(speed, FloatSpeed, FloatAccel * Engine.DeltaTime);
			Position = Calc.Approach(Position, target, speed * Engine.DeltaTime);
			Floatness = Calc.Approach(Floatness, 4f, 8f * Engine.DeltaTime);
			floatNormal = Calc.Approach(floatNormal, perp, Engine.DeltaTime * 12f);
			if (fadeLight)
			{
				Light.Alpha = Calc.Approach(Light.Alpha, 0f, Engine.DeltaTime * 2f);
			}
			yield return null;
		}
		if (quickEnd)
		{
			Floatness = 2f;
		}
		else
		{
			while (Floatness != 2f)
			{
				Floatness = Calc.Approach(Floatness, 2f, 8f * Engine.DeltaTime);
				yield return null;
			}
		}
		if (turnAtEndTo.HasValue)
		{
			Sprite.Scale.X = turnAtEndTo.Value;
		}
	}

	[IteratorStateMachine(typeof(_003CWalkTo_003Ed__14))]
	public IEnumerator WalkTo(float x, float speed = 64f)
	{
		Floatness = 0f;
		Sprite.Play("walk");
		if (Math.Sign(x - base.X) != 0)
		{
			Sprite.Scale.X = Math.Sign(x - base.X);
		}
		while (base.X != x)
		{
			base.X = Calc.Approach(base.X, x, Engine.DeltaTime * speed);
			yield return null;
		}
		Sprite.Play("idle");
	}

	[IteratorStateMachine(typeof(_003CSmashBlock_003Ed__15))]
	public IEnumerator SmashBlock(Vector2 target)
	{
		SceneAs<Level>().Displacement.AddBurst(Position, 0.5f, 24f, 96f);
		Sprite.Play("dreamDashLoop");
		Vector2 from = Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 6f)
		{
			Position = from + (target - from) * Ease.CubeOut(p);
			yield return null;
		}
		base.Scene.Entities.FindFirst<DashBlock>().Break(Position, new Vector2(0f, -1f), playSound: false);
		Sprite.Play("idle");
		for (float p = 0f; p < 1f; p += Engine.DeltaTime * 4f)
		{
			Position = target + (from - target) * Ease.CubeOut(p);
			yield return null;
		}
		Sprite.Play("fallSlow");
	}

	public override void Update()
	{
		if (Sprite.Scale.X != 0f)
		{
			Hair.Facing = (Facings)Math.Sign(Sprite.Scale.X);
		}
		base.Update();
	}

	public override void Render()
	{
		Vector2 renderPosition = Sprite.RenderPosition;
		Sprite.RenderPosition = Sprite.RenderPosition.Floor();
		base.Render();
		Sprite.RenderPosition = renderPosition;
	}
}
