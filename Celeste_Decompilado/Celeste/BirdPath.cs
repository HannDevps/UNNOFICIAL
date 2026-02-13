using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class BirdPath : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BirdPath _003C_003E4__this;

		private int _003Ci_003E5__2;

		private Vector2 _003Cnext_003E5__3;

		private SimpleCurve _003Ccurve_003E5__4;

		private float _003Cduration_003E5__5;

		private bool _003CplayedSfx_003E5__6;

		private float _003Cp_003E5__7;

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
		public _003CRoutine_003Ed__18(int _003C_003E1__state)
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
			BirdPath birdPath = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				_003Cp_003E5__7 += Engine.DeltaTime * birdPath.speedMult / _003Cduration_003E5__5;
				goto IL_015d;
			}
			_003C_003E1__state = -1;
			Vector2 begin = birdPath.start;
			_003Ci_003E5__2 = 0;
			goto IL_019a;
			IL_015d:
			if (_003Cp_003E5__7 < 1f)
			{
				birdPath.target = _003Ccurve_003E5__4.GetPoint(_003Cp_003E5__7);
				if (_003Cp_003E5__7 > 0.9f)
				{
					if (!_003CplayedSfx_003E5__6 && birdPath.sprite.CurrentAnimationID != "flyupRoll")
					{
						birdPath.Add(new SoundSource("event:/new_content/game/10_farewell/bird_flyuproll")
						{
							RemoveOnOneshotEnd = true
						});
						_003CplayedSfx_003E5__6 = true;
					}
					birdPath.sprite.Play("flyupRoll");
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			begin = _003Cnext_003E5__3;
			_003Cnext_003E5__3 = default(Vector2);
			_003Ccurve_003E5__4 = default(SimpleCurve);
			_003Ci_003E5__2 += 2;
			goto IL_019a;
			IL_019a:
			if (_003Ci_003E5__2 <= birdPath.nodes.Length - 1)
			{
				Vector2 control = birdPath.nodes[_003Ci_003E5__2];
				_003Cnext_003E5__3 = birdPath.nodes[_003Ci_003E5__2 + 1];
				_003Ccurve_003E5__4 = new SimpleCurve(begin, _003Cnext_003E5__3, control);
				float lengthParametric = _003Ccurve_003E5__4.GetLengthParametric(32);
				_003Cduration_003E5__5 = lengthParametric / birdPath.maxspeed;
				_003CplayedSfx_003E5__6 = false;
				_ = birdPath.Position;
				_003Cp_003E5__7 = 0f;
				goto IL_015d;
			}
			birdPath.RemoveSelf();
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

	private Vector2 start;

	private Sprite sprite;

	private Vector2[] nodes;

	private Color trailColor = Calc.HexToColor("639bff");

	private Vector2 target;

	private Vector2 speed;

	private float maxspeed;

	private Vector2 lastTrail;

	private float speedMult;

	private EntityID ID;

	private bool onlyOnce;

	private bool onlyIfLeft;

	public BirdPath(EntityID id, EntityData data, Vector2 offset)
		: this(id, data.Position + offset, data.NodesOffset(offset), data.Bool("only_once"), data.Bool("onlyIfLeft"), data.Float("speedMult", 1f))
	{
	}

	public BirdPath(EntityID id, Vector2 position, Vector2[] nodes, bool onlyOnce, bool onlyIfLeft, float speedMult)
	{
		base.Tag = Tags.TransitionUpdate;
		ID = id;
		Position = position;
		start = position;
		this.nodes = nodes;
		this.onlyOnce = onlyOnce;
		this.onlyIfLeft = onlyIfLeft;
		this.speedMult = speedMult;
		maxspeed = 150f * speedMult;
		Add(sprite = GFX.SpriteBank.Create("bird"));
		sprite.Play("flyupRoll");
		sprite.JustifyOrigin(0.5f, 0.75f);
		Add(new SoundSource("event:/new_content/game/10_farewell/bird_flyuproll")
		{
			RemoveOnOneshotEnd = true
		});
		Add(new Coroutine(Routine()));
	}

	public void WaitForTrigger()
	{
		Visible = (Active = false);
	}

	public void Trigger()
	{
		Visible = (Active = true);
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (onlyOnce)
		{
			(base.Scene as Level).Session.DoNotLoad.Add(ID);
		}
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (onlyIfLeft)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.X > base.X)
			{
				RemoveSelf();
			}
		}
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__18))]
	private IEnumerator Routine()
	{
		Vector2 begin = start;
		for (int i = 0; i <= nodes.Length - 1; i += 2)
		{
			Vector2 control = nodes[i];
			Vector2 next = nodes[i + 1];
			SimpleCurve curve = new SimpleCurve(begin, next, control);
			float lengthParametric = curve.GetLengthParametric(32);
			float duration = lengthParametric / maxspeed;
			bool playedSfx = false;
			_ = Position;
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * speedMult / duration)
			{
				target = curve.GetPoint(p);
				if (p > 0.9f)
				{
					if (!playedSfx && sprite.CurrentAnimationID != "flyupRoll")
					{
						SoundSource soundSource = new SoundSource("event:/new_content/game/10_farewell/bird_flyuproll");
						soundSource.RemoveOnOneshotEnd = true;
						Add(soundSource);
						playedSfx = true;
					}
					sprite.Play("flyupRoll");
				}
				yield return null;
			}
			begin = next;
		}
		RemoveSelf();
	}

	public override void Update()
	{
		if ((base.Scene as Level).Transitioning)
		{
			using (IEnumerator<Component> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is SoundSource soundSource)
					{
						soundSource.UpdateSfxPosition();
					}
				}
				return;
			}
		}
		base.Update();
		int num = Math.Sign(base.X - target.X);
		Vector2 vector = (target - Position).SafeNormalize();
		float num2 = 800f;
		speed += vector * num2 * Engine.DeltaTime;
		if (speed.Length() > maxspeed)
		{
			speed = speed.SafeNormalize(maxspeed);
		}
		Position += speed * Engine.DeltaTime;
		if (num != Math.Sign(base.X - target.X))
		{
			speed.X *= 0.75f;
		}
		float startAngle = speed.Angle();
		float endAngle = Calc.Angle(Position, target);
		float num3 = Calc.AngleLerp(startAngle, endAngle, 0.5f);
		sprite.Rotation = (float)Math.PI / 2f + num3;
		if ((lastTrail - Position).Length() > 32f)
		{
			TrailManager.Add(this, trailColor);
			lastTrail = Position;
		}
	}

	public override void Render()
	{
		base.Render();
	}

	public override void DebugRender(Camera camera)
	{
		Vector2 begin = start;
		for (int i = 0; i < nodes.Length - 1; i += 2)
		{
			Vector2 vector = nodes[i + 1];
			new SimpleCurve(begin, vector, nodes[i]).Render(Color.Red * 0.25f, 32);
			begin = vector;
		}
		Draw.Line(Position, Position + (target - Position).SafeNormalize() * ((target - Position).Length() - 3f), Color.Yellow);
		Draw.Circle(target, 3f, Color.Yellow, 16);
	}
}
