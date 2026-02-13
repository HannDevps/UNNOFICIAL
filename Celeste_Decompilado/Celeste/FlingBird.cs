using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class FlingBird : Entity
{
	private enum States
	{
		Wait,
		Fling,
		Move,
		WaitForLightningClear,
		Leaving
	}

	[CompilerGenerated]
	private sealed class _003CDoFlingRoutine_003Ed__23 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FlingBird _003C_003E4__this;

		public Player player;

		private Level _003Clevel_003E5__2;

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
		public _003CDoFlingRoutine_003Ed__23(int _003C_003E1__state)
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
			FlingBird flingBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = flingBird.Scene as Level;
				Vector2 position = _003Clevel_003E5__2.Camera.Position;
				Vector2 screenSpaceFocusPoint = player.Position - position;
				screenSpaceFocusPoint.X = Calc.Clamp(screenSpaceFocusPoint.X, 145f, 215f);
				screenSpaceFocusPoint.Y = Calc.Clamp(screenSpaceFocusPoint.Y, 85f, 95f);
				flingBird.Add(new Coroutine(_003Clevel_003E5__2.ZoomTo(screenSpaceFocusPoint, 1.1f, 0.2f)));
				Engine.TimeRate = 0.8f;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				goto IL_00e9;
			}
			case 1:
				_003C_003E1__state = -1;
				goto IL_00e9;
			case 2:
				_003C_003E1__state = -1;
				Celeste.Freeze(0.05f);
				flingBird.flingTargetSpeed = FlingSpeed;
				flingBird.flingAccel = 6000f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				Engine.TimeRate = 1f;
				_003Clevel_003E5__2.Shake();
				flingBird.Add(new Coroutine(_003Clevel_003E5__2.ZoomBack(0.1f)));
				player.FinishFlingBird();
				flingBird.flingTargetSpeed = Vector2.Zero;
				flingBird.flingAccel = 4000f;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					flingBird.Add(new Coroutine(flingBird.MoveRoutine()));
					return false;
				}
				IL_00e9:
				if (flingBird.flingSpeed != Vector2.Zero)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				flingBird.sprite.Play("throw");
				flingBird.sprite.Scale.X = 1f;
				flingBird.flingSpeed = new Vector2(-140f, 140f);
				flingBird.flingTargetSpeed = Vector2.Zero;
				flingBird.flingAccel = 1400f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
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

	[CompilerGenerated]
	private sealed class _003CMoveRoutine_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FlingBird _003C_003E4__this;

		private int _003CnodeIndex_003E5__2;

		private bool _003CatEnding_003E5__3;

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
		public _003CMoveRoutine_003Ed__24(int _003C_003E1__state)
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
			FlingBird flingBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				flingBird.state = States.Move;
				flingBird.sprite.Play("fly");
				flingBird.sprite.Scale.X = 1f;
				flingBird.moveSfx.Play("event:/new_content/game/10_farewell/bird_relocate");
				_003CnodeIndex_003E5__2 = 1;
				goto IL_00e9;
			case 1:
				_003C_003E1__state = -1;
				_003CnodeIndex_003E5__2 += 2;
				goto IL_00e9;
			case 2:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_00e9:
				if (_003CnodeIndex_003E5__2 < flingBird.NodeSegments[flingBird.segmentIndex].Length - 1)
				{
					Vector2 position = flingBird.Position;
					Vector2 anchor = flingBird.NodeSegments[flingBird.segmentIndex][_003CnodeIndex_003E5__2];
					Vector2 to = flingBird.NodeSegments[flingBird.segmentIndex][_003CnodeIndex_003E5__2 + 1];
					_003C_003E2__current = flingBird.MoveOnCurve(position, anchor, to);
					_003C_003E1__state = 1;
					return true;
				}
				flingBird.segmentIndex++;
				_003CatEnding_003E5__3 = flingBird.segmentIndex >= flingBird.NodeSegments.Count;
				if (!_003CatEnding_003E5__3)
				{
					Vector2 position2 = flingBird.Position;
					Vector2 anchor2 = flingBird.NodeSegments[flingBird.segmentIndex - 1][flingBird.NodeSegments[flingBird.segmentIndex - 1].Length - 1];
					Vector2 to2 = flingBird.NodeSegments[flingBird.segmentIndex][0];
					_003C_003E2__current = flingBird.MoveOnCurve(position2, anchor2, to2);
					_003C_003E1__state = 2;
					return true;
				}
				break;
			}
			flingBird.sprite.Rotation = 0f;
			flingBird.sprite.Scale = Vector2.One;
			if (_003CatEnding_003E5__3)
			{
				flingBird.sprite.Play("hoverStressed");
				flingBird.sprite.Scale.X = 1f;
				flingBird.state = States.WaitForLightningClear;
			}
			else
			{
				if (flingBird.SegmentsWaiting[flingBird.segmentIndex])
				{
					flingBird.sprite.Play("hoverStressed");
				}
				else
				{
					flingBird.sprite.Play("hover");
				}
				flingBird.sprite.Scale.X = -1f;
				flingBird.state = States.Wait;
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
	private sealed class _003CLeaveRoutine_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FlingBird _003C_003E4__this;

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
		public _003CLeaveRoutine_003Ed__25(int _003C_003E1__state)
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
			FlingBird flingBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				flingBird.sprite.Scale.X = 1f;
				flingBird.sprite.Play("fly");
				Vector2 vector = new Vector2((flingBird.Scene as Level).Bounds.Right + 32, flingBird.Y);
				_003C_003E2__current = flingBird.MoveOnCurve(flingBird.Position, (flingBird.Position + vector) * 0.5f - Vector2.UnitY * 12f, vector);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				flingBird.RemoveSelf();
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
	private sealed class _003CMoveOnCurve_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Vector2 from;

		public Vector2 to;

		public Vector2 anchor;

		public FlingBird _003C_003E4__this;

		private SimpleCurve _003Ccurve_003E5__2;

		private float _003Cduration_003E5__3;

		private Vector2 _003Cwas_003E5__4;

		private float _003Ct_003E5__5;

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
		public _003CMoveOnCurve_003Ed__26(int _003C_003E1__state)
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
			FlingBird flingBird = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ccurve_003E5__2 = new SimpleCurve(from, to, anchor);
				_003Cduration_003E5__3 = _003Ccurve_003E5__2.GetLengthParametric(32) / 500f;
				_ = from;
				_003Cwas_003E5__4 = from;
				_003Ct_003E5__5 = 0.016f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__5 += Engine.DeltaTime / _003Cduration_003E5__3;
				break;
			}
			if (_003Ct_003E5__5 <= 1f)
			{
				flingBird.Position = _003Ccurve_003E5__2.GetPoint(_003Ct_003E5__5).Floor();
				flingBird.sprite.Rotation = Calc.Angle(_003Ccurve_003E5__2.GetPoint(Math.Max(0f, _003Ct_003E5__5 - 0.05f)), _003Ccurve_003E5__2.GetPoint(Math.Min(1f, _003Ct_003E5__5 + 0.05f)));
				flingBird.sprite.Scale.X = 1.25f;
				flingBird.sprite.Scale.Y = 0.7f;
				if ((_003Cwas_003E5__4 - flingBird.Position).Length() > 32f)
				{
					TrailManager.Add(flingBird, flingBird.trailColor);
					_003Cwas_003E5__4 = flingBird.Position;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			flingBird.Position = to;
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

	public static ParticleType P_Feather;

	public const float SkipDist = 100f;

	public static readonly Vector2 FlingSpeed = new Vector2(380f, -100f);

	private Vector2 spriteOffset = new Vector2(0f, 8f);

	private Sprite sprite;

	private States state;

	private Vector2 flingSpeed;

	private Vector2 flingTargetSpeed;

	private float flingAccel;

	private Color trailColor = Calc.HexToColor("639bff");

	private EntityData entityData;

	private SoundSource moveSfx;

	private int segmentIndex;

	public List<Vector2[]> NodeSegments;

	public List<bool> SegmentsWaiting;

	public bool LightningRemoved;

	public FlingBird(Vector2[] nodes, bool skippable)
		: base(nodes[0])
	{
		base.Depth = -1;
		Add(sprite = GFX.SpriteBank.Create("bird"));
		sprite.Play("hover");
		sprite.Scale.X = -1f;
		sprite.Position = spriteOffset;
		sprite.OnFrameChange = delegate
		{
			BirdNPC.FlapSfxCheck(sprite);
		};
		base.Collider = new Circle(16f);
		Add(new PlayerCollider(OnPlayer));
		Add(moveSfx = new SoundSource());
		NodeSegments = new List<Vector2[]>();
		NodeSegments.Add(nodes);
		SegmentsWaiting = new List<bool>();
		SegmentsWaiting.Add(skippable);
		Add(new TransitionListener
		{
			OnOut = delegate(float t)
			{
				sprite.Color = Color.White * (1f - Calc.Map(t, 0f, 0.4f));
			}
		});
	}

	public FlingBird(EntityData data, Vector2 levelOffset)
		: this(data.NodesWithPosition(levelOffset), data.Bool("waiting"))
	{
		entityData = data;
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		List<FlingBird> list = base.Scene.Entities.FindAll<FlingBird>();
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (list[num].entityData.Level.Name != entityData.Level.Name)
			{
				list.RemoveAt(num);
			}
		}
		list.Sort((FlingBird a, FlingBird b) => Math.Sign(a.X - b.X));
		if (list[0] == this)
		{
			for (int num2 = 1; num2 < list.Count; num2++)
			{
				NodeSegments.Add(list[num2].NodeSegments[0]);
				SegmentsWaiting.Add(list[num2].SegmentsWaiting[0]);
				list[num2].RemoveSelf();
			}
		}
		if (SegmentsWaiting[0])
		{
			sprite.Play("hoverStressed");
			sprite.Scale.X = 1f;
		}
		Player entity = scene.Tracker.GetEntity<Player>();
		if (entity != null && entity.X > base.X)
		{
			RemoveSelf();
		}
	}

	private void Skip()
	{
		state = States.Move;
		Add(new Coroutine(MoveRoutine()));
	}

	private void OnPlayer(Player player)
	{
		if (state == States.Wait && player.DoFlingBird(this))
		{
			flingSpeed = player.Speed * 0.4f;
			flingSpeed.Y = 120f;
			flingTargetSpeed = Vector2.Zero;
			flingAccel = 1000f;
			player.Speed = Vector2.Zero;
			state = States.Fling;
			Add(new Coroutine(DoFlingRoutine(player)));
			Audio.Play("event:/new_content/game/10_farewell/bird_throw", base.Center);
		}
	}

	public override void Update()
	{
		base.Update();
		if (state != States.Wait)
		{
			sprite.Position = Calc.Approach(sprite.Position, spriteOffset, 32f * Engine.DeltaTime);
		}
		switch (state)
		{
		case States.Wait:
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.X - base.X >= 100f)
			{
				Skip();
			}
			else if (SegmentsWaiting[segmentIndex] && LightningRemoved)
			{
				Skip();
			}
			else if (entity != null)
			{
				float num = Calc.ClampedMap((entity.Center - Position).Length(), 16f, 64f, 12f, 0f);
				Vector2 vector = (entity.Center - Position).SafeNormalize();
				sprite.Position = Calc.Approach(sprite.Position, spriteOffset + vector * num, 32f * Engine.DeltaTime);
			}
			break;
		}
		case States.Fling:
			if (flingAccel > 0f)
			{
				flingSpeed = Calc.Approach(flingSpeed, flingTargetSpeed, flingAccel * Engine.DeltaTime);
			}
			Position += flingSpeed * Engine.DeltaTime;
			break;
		case States.WaitForLightningClear:
			if (base.Scene.Entities.FindFirst<Lightning>() == null || base.X > (float)(base.Scene as Level).Bounds.Right)
			{
				sprite.Scale.X = 1f;
				state = States.Leaving;
				Add(new Coroutine(LeaveRoutine()));
			}
			break;
		case States.Move:
			break;
		}
	}

	[IteratorStateMachine(typeof(_003CDoFlingRoutine_003Ed__23))]
	private IEnumerator DoFlingRoutine(Player player)
	{
		Level level = base.Scene as Level;
		Vector2 position = level.Camera.Position;
		Vector2 screenSpaceFocusPoint = player.Position - position;
		screenSpaceFocusPoint.X = Calc.Clamp(screenSpaceFocusPoint.X, 145f, 215f);
		screenSpaceFocusPoint.Y = Calc.Clamp(screenSpaceFocusPoint.Y, 85f, 95f);
		Add(new Coroutine(level.ZoomTo(screenSpaceFocusPoint, 1.1f, 0.2f)));
		Engine.TimeRate = 0.8f;
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		while (flingSpeed != Vector2.Zero)
		{
			yield return null;
		}
		sprite.Play("throw");
		sprite.Scale.X = 1f;
		flingSpeed = new Vector2(-140f, 140f);
		flingTargetSpeed = Vector2.Zero;
		flingAccel = 1400f;
		yield return 0.1f;
		Celeste.Freeze(0.05f);
		flingTargetSpeed = FlingSpeed;
		flingAccel = 6000f;
		yield return 0.1f;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		Engine.TimeRate = 1f;
		level.Shake();
		Add(new Coroutine(level.ZoomBack(0.1f)));
		player.FinishFlingBird();
		flingTargetSpeed = Vector2.Zero;
		flingAccel = 4000f;
		yield return 0.3f;
		Add(new Coroutine(MoveRoutine()));
	}

	[IteratorStateMachine(typeof(_003CMoveRoutine_003Ed__24))]
	private IEnumerator MoveRoutine()
	{
		state = States.Move;
		sprite.Play("fly");
		sprite.Scale.X = 1f;
		moveSfx.Play("event:/new_content/game/10_farewell/bird_relocate");
		for (int nodeIndex = 1; nodeIndex < NodeSegments[segmentIndex].Length - 1; nodeIndex += 2)
		{
			Vector2 position = Position;
			Vector2 anchor = NodeSegments[segmentIndex][nodeIndex];
			Vector2 to = NodeSegments[segmentIndex][nodeIndex + 1];
			yield return MoveOnCurve(position, anchor, to);
		}
		segmentIndex++;
		bool atEnding = segmentIndex >= NodeSegments.Count;
		if (!atEnding)
		{
			Vector2 position2 = Position;
			Vector2 anchor2 = NodeSegments[segmentIndex - 1][NodeSegments[segmentIndex - 1].Length - 1];
			Vector2 to2 = NodeSegments[segmentIndex][0];
			yield return MoveOnCurve(position2, anchor2, to2);
		}
		sprite.Rotation = 0f;
		sprite.Scale = Vector2.One;
		if (atEnding)
		{
			sprite.Play("hoverStressed");
			sprite.Scale.X = 1f;
			state = States.WaitForLightningClear;
			yield break;
		}
		if (SegmentsWaiting[segmentIndex])
		{
			sprite.Play("hoverStressed");
		}
		else
		{
			sprite.Play("hover");
		}
		sprite.Scale.X = -1f;
		state = States.Wait;
	}

	[IteratorStateMachine(typeof(_003CLeaveRoutine_003Ed__25))]
	private IEnumerator LeaveRoutine()
	{
		sprite.Scale.X = 1f;
		sprite.Play("fly");
		Vector2 vector = new Vector2((base.Scene as Level).Bounds.Right + 32, base.Y);
		yield return MoveOnCurve(Position, (Position + vector) * 0.5f - Vector2.UnitY * 12f, vector);
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CMoveOnCurve_003Ed__26))]
	private IEnumerator MoveOnCurve(Vector2 from, Vector2 anchor, Vector2 to)
	{
		SimpleCurve curve = new SimpleCurve(from, to, anchor);
		float duration = curve.GetLengthParametric(32) / 500f;
		_ = from;
		Vector2 was = from;
		for (float t = 0.016f; t <= 1f; t += Engine.DeltaTime / duration)
		{
			Position = curve.GetPoint(t).Floor();
			sprite.Rotation = Calc.Angle(curve.GetPoint(Math.Max(0f, t - 0.05f)), curve.GetPoint(Math.Min(1f, t + 0.05f)));
			sprite.Scale.X = 1.25f;
			sprite.Scale.Y = 0.7f;
			if ((was - Position).Length() > 32f)
			{
				TrailManager.Add(this, trailColor);
				was = Position;
			}
			yield return null;
		}
		Position = to;
	}

	public override void Render()
	{
		base.Render();
	}

	private void DrawLine(Vector2 a, Vector2 anchor, Vector2 b)
	{
		new SimpleCurve(a, b, anchor).Render(Color.Red, 32);
	}
}
