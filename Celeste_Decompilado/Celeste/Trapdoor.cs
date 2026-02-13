using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Trapdoor : Entity
{
	[CompilerGenerated]
	private sealed class _003COpenFromBottom_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Trapdoor _003C_003E4__this;

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
		public _003COpenFromBottom_003Ed__5(int _003C_003E1__state)
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
			Trapdoor trapdoor = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				trapdoor.sprite.Scale.Y = -1f;
				_003C_003E2__current = trapdoor.sprite.PlayRoutine("open_partial");
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				trapdoor.sprite.Rate = -1f;
				_003C_003E2__current = trapdoor.sprite.PlayRoutine("open_partial", restart: true);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				trapdoor.sprite.Scale.Y = 1f;
				trapdoor.sprite.Rate = 1f;
				trapdoor.sprite.Play("open", restart: true);
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

	private Sprite sprite;

	private PlayerCollider playerCollider;

	private LightOcclude occluder;

	public Trapdoor(EntityData data, Vector2 offset)
	{
		Position = data.Position + offset;
		base.Depth = 8999;
		Add(sprite = GFX.SpriteBank.Create("trapdoor"));
		sprite.Play("idle");
		sprite.Y = 6f;
		base.Collider = new Hitbox(24f, 4f, 0f, 6f);
		Add(playerCollider = new PlayerCollider(Open));
		Add(occluder = new LightOcclude(new Rectangle(0, 6, 24, 2)));
	}

	private void Open(Player player)
	{
		Collidable = false;
		occluder.Visible = false;
		if (player.Speed.Y >= 0f)
		{
			Audio.Play("event:/game/03_resort/trapdoor_fromtop", Position);
			sprite.Play("open");
		}
		else
		{
			Audio.Play("event:/game/03_resort/trapdoor_frombottom", Position);
			Add(new Coroutine(OpenFromBottom()));
		}
	}

	[IteratorStateMachine(typeof(_003COpenFromBottom_003Ed__5))]
	private IEnumerator OpenFromBottom()
	{
		sprite.Scale.Y = -1f;
		yield return sprite.PlayRoutine("open_partial");
		yield return 0.1f;
		sprite.Rate = -1f;
		yield return sprite.PlayRoutine("open_partial", restart: true);
		sprite.Scale.Y = 1f;
		sprite.Rate = 1f;
		sprite.Play("open", restart: true);
	}
}
