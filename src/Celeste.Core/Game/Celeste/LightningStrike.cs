using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class LightningStrike : Entity
{
	private class Node
	{
		public Vector2 Position;

		public float Size;

		public List<Node> Children;

		public Node(float x, float y, float size)
			: this(new Vector2(x, y), size)
		{
		}

		public Node(Vector2 position, float size)
		{
			Position = position;
			Children = new List<Node>();
			Size = size;
		}

		public void Wiggle(Random rand)
		{
			Position.X += rand.Range(-2, 2);
			if (Position.Y != 0f)
			{
				Position.Y += rand.Range(-1, 1);
			}
			foreach (Node child in Children)
			{
				child.Wiggle(rand);
			}
		}

		public void Render(Vector2 offset, float scale)
		{
			float num = Size * scale;
			foreach (Node child in Children)
			{
				Vector2 vector = (child.Position - Position).SafeNormalize();
				Draw.Line(offset + Position, offset + child.Position + vector * num * 0.5f, Color.White, num);
				child.Render(offset, scale);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public LightningStrike _003C_003E4__this;

		private int _003Cj_003E5__2;

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
		public _003CRoutine_003Ed__7(int _003C_003E1__state)
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
			LightningStrike lightningStrike = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (delay > 0f)
				{
					_003C_003E2__current = delay;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_005b;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005b;
			case 2:
				_003C_003E1__state = -1;
				lightningStrike.scale -= 0.2f;
				lightningStrike.on = false;
				lightningStrike.strike.Wiggle(lightningStrike.rand);
				_003C_003E2__current = 0.01f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				{
					_003C_003E1__state = -1;
					_003Cj_003E5__2++;
					break;
				}
				IL_005b:
				lightningStrike.scale = 1f;
				lightningStrike.GenerateStikeNodes(-1, 10f);
				_003Cj_003E5__2 = 0;
				break;
			}
			if (_003Cj_003E5__2 < 5)
			{
				lightningStrike.on = true;
				_003C_003E2__current = (1f - (float)_003Cj_003E5__2 / 5f) * 0.1f;
				_003C_003E1__state = 2;
				return true;
			}
			lightningStrike.RemoveSelf();
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

	private bool on;

	private float scale;

	private Random rand;

	private float strikeHeight;

	private Node strike;

	public LightningStrike(Vector2 position, int seed, float height, float delay = 0f)
	{
		Position = position;
		base.Depth = 10010;
		rand = new Random(seed);
		strikeHeight = height;
		Add(new Coroutine(Routine(delay)));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__7))]
	private IEnumerator Routine(float delay)
	{
		if (delay > 0f)
		{
			yield return delay;
		}
		scale = 1f;
		GenerateStikeNodes(-1, 10f);
		for (int j = 0; j < 5; j++)
		{
			on = true;
			yield return (1f - (float)j / 5f) * 0.1f;
			scale -= 0.2f;
			on = false;
			strike.Wiggle(rand);
			yield return 0.01f;
		}
		RemoveSelf();
	}

	private void GenerateStikeNodes(int direction, float size, Node parent = null)
	{
		if (parent == null)
		{
			parent = (strike = new Node(0f, 0f, size));
		}
		if (!(parent.Position.Y >= strikeHeight))
		{
			float num = direction * rand.Range(-8, 20);
			float num2 = rand.Range(8, 16);
			float size2 = (0.25f + (1f - (parent.Position.Y + num2) / strikeHeight) * 0.75f) * size;
			Node node = new Node(parent.Position + new Vector2(num, num2), size2);
			parent.Children.Add(node);
			GenerateStikeNodes(direction, size, node);
			if (rand.Chance(0.1f))
			{
				Node node2 = new Node(parent.Position + new Vector2(0f - num, num2 * 1.5f), size2);
				parent.Children.Add(node2);
				GenerateStikeNodes(-direction, size, node2);
			}
		}
	}

	public override void Render()
	{
		if (on)
		{
			strike.Render(Position, scale);
		}
	}
}
