using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class DetachStrawberryTrigger : Trigger
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public Entity entity;

		internal void _003CDetatchFollower_003Eb__0()
		{
			entity.RemoveTag(Tags.Global);
		}
	}

	[CompilerGenerated]
	private sealed class _003CDetatchFollower_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Follower follower;

		public DetachStrawberryTrigger _003C_003E4__this;

		private _003C_003Ec__DisplayClass4_0 _003C_003E8__1;

		private float _003Ctime_003E5__2;

		private SimpleCurve _003Ccurve_003E5__3;

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
		public _003CDetatchFollower_003Ed__4(int _003C_003E1__state)
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
			DetachStrawberryTrigger detachStrawberryTrigger = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass4_0();
				Leader leader = follower.Leader;
				_003C_003E8__1.entity = follower.Entity;
				float num2 = (_003C_003E8__1.entity.Position - detachStrawberryTrigger.Target).Length();
				_003Ctime_003E5__2 = num2 / 200f;
				if (_003C_003E8__1.entity is Strawberry strawberry)
				{
					strawberry.ReturnHomeWhenLost = false;
				}
				leader.LoseFollower(follower);
				_003C_003E8__1.entity.Active = false;
				_003C_003E8__1.entity.Collidable = false;
				if (detachStrawberryTrigger.Global)
				{
					_003C_003E8__1.entity.AddTag(Tags.Global);
					Follower obj = follower;
					obj.OnGainLeader = (Action)Delegate.Combine(obj.OnGainLeader, (Action)delegate
					{
						_003C_003E8__1.entity.RemoveTag(Tags.Global);
					});
				}
				else
				{
					_003C_003E8__1.entity.AddTag(Tags.Persistent);
				}
				Audio.Play("event:/new_content/game/10_farewell/strawberry_gold_detach", _003C_003E8__1.entity.Position);
				Vector2 position = _003C_003E8__1.entity.Position;
				_003Ccurve_003E5__3 = new SimpleCurve(position, detachStrawberryTrigger.Target, position + (detachStrawberryTrigger.Target - position) * 0.5f + new Vector2(0f, -64f));
				_003Cp_003E5__4 = 0f;
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime / _003Ctime_003E5__2;
				break;
			}
			if (_003Cp_003E5__4 < 1f)
			{
				_003C_003E8__1.entity.Position = _003Ccurve_003E5__3.GetPoint(Ease.CubeInOut(_003Cp_003E5__4));
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			_003C_003E8__1.entity.Active = true;
			_003C_003E8__1.entity.Collidable = true;
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

	public Vector2 Target;

	public bool Global;

	public DetachStrawberryTrigger(EntityData data, Vector2 offset)
		: base(data, offset)
	{
		Vector2[] array = data.NodesOffset(offset);
		if (array.Length != 0)
		{
			Target = array[0];
		}
		Global = data.Bool("global", defaultValue: true);
	}

	public override void OnEnter(Player player)
	{
		base.OnEnter(player);
		for (int num = player.Leader.Followers.Count - 1; num >= 0; num--)
		{
			if (player.Leader.Followers[num].Entity is Strawberry)
			{
				Add(new Coroutine(DetatchFollower(player.Leader.Followers[num])));
			}
		}
	}

	[IteratorStateMachine(typeof(_003CDetatchFollower_003Ed__4))]
	private IEnumerator DetatchFollower(Follower follower)
	{
		Leader leader = follower.Leader;
		Entity entity = follower.Entity;
		float num = (entity.Position - Target).Length();
		float time = num / 200f;
		if (entity is Strawberry strawberry)
		{
			strawberry.ReturnHomeWhenLost = false;
		}
		leader.LoseFollower(follower);
		entity.Active = false;
		entity.Collidable = false;
		if (Global)
		{
			entity.AddTag(Tags.Global);
			follower.OnGainLeader = (Action)Delegate.Combine(follower.OnGainLeader, (Action)delegate
			{
				entity.RemoveTag(Tags.Global);
			});
		}
		else
		{
			entity.AddTag(Tags.Persistent);
		}
		Audio.Play("event:/new_content/game/10_farewell/strawberry_gold_detach", entity.Position);
		Vector2 position = entity.Position;
		SimpleCurve curve = new SimpleCurve(position, Target, position + (Target - position) * 0.5f + new Vector2(0f, -64f));
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / time)
		{
			entity.Position = curve.GetPoint(Ease.CubeInOut(p));
			yield return null;
		}
		entity.Active = true;
		entity.Collidable = true;
	}
}
