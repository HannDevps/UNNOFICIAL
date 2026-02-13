using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class RidgeGate : Solid
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public RidgeGate _003C_003E4__this;

		public Vector2 start;

		public Vector2 moveTo;

		internal void _003CEnterSequence_003Eb__0(Tween t)
		{
			_003C_003E4__this.MoveTo(Vector2.Lerp(start, moveTo, t.Eased));
		}
	}

	[CompilerGenerated]
	private sealed class _003CEnterSequence_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RidgeGate _003C_003E4__this;

		public Vector2 moveTo;

		private _003C_003Ec__DisplayClass4_0 _003C_003E8__1;

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
		public _003CEnterSequence_003Ed__4(int _003C_003E1__state)
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
			RidgeGate ridgeGate = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass4_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				_003C_003E8__1.moveTo = moveTo;
				ridgeGate.Visible = (ridgeGate.Collidable = true);
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/stone_blockade", ridgeGate.Position);
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1.start = ridgeGate.Position;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 1f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1._003C_003E4__this.MoveTo(Vector2.Lerp(_003C_003E8__1.start, _003C_003E8__1.moveTo, t.Eased));
				};
				ridgeGate.Add(tween);
				return false;
			}
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

	private Vector2? node;

	public RidgeGate(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.FirstNodeNullable(offset), data.Bool("ridge", defaultValue: true))
	{
	}

	public RidgeGate(Vector2 position, float width, float height, Vector2? node, bool ridgeImage = true)
		: base(position, width, height, safe: true)
	{
		this.node = node;
		Add(new Image(GFX.Game[ridgeImage ? "objects/ridgeGate" : "objects/farewellGate"]));
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (node.HasValue && CollideCheck<Player>())
		{
			Visible = (Collidable = false);
			Vector2 position = Position;
			Position = node.Value;
			Add(new Coroutine(EnterSequence(position)));
		}
	}

	[IteratorStateMachine(typeof(_003CEnterSequence_003Ed__4))]
	private IEnumerator EnterSequence(Vector2 moveTo)
	{
		RidgeGate ridgeGate = this;
		RidgeGate ridgeGate2 = this;
		bool visible = true;
		ridgeGate2.Collidable = true;
		ridgeGate.Visible = visible;
		yield return 0.25f;
		Audio.Play("event:/game/04_cliffside/stone_blockade", Position);
		yield return 0.25f;
		Vector2 start = Position;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 1f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			MoveTo(Vector2.Lerp(start, moveTo, t.Eased));
		};
		Add(tween);
	}
}
