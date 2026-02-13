using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Key : Entity
{
	[CompilerGenerated]
	private sealed class _003CNodeRoutine_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Player player;

		public Key _003C_003E4__this;

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
		public _003CNodeRoutine_003Ed__26(int _003C_003E1__state)
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
			Key key = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (!player.Dead)
				{
					Audio.Play("event:/game/general/cassette_bubblereturn", key.SceneAs<Level>().Camera.Position + new Vector2(160f, 90f));
					player.StartCassetteFly(key.nodes[1], key.nodes[0]);
				}
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
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public Key _003C_003E4__this;

		public SimpleCurve curve;

		public Action<Tween> _003C_003E9__3;

		public Action<Tween> _003C_003E9__4;

		internal void _003CUseRoutine_003Eb__0(Tween t)
		{
			_003C_003E4__this.Position = curve.GetPoint(t.Eased);
			_003C_003E4__this.sprite.Rate = 1f + t.Eased * 2f;
		}

		internal void _003CUseRoutine_003Eb__1(Tween t)
		{
			_003C_003E4__this.sprite.Rotation = t.Eased * ((float)Math.PI / 2f);
		}

		internal void _003CUseRoutine_003Eb__2()
		{
			_003C_003E4__this.alarm = null;
			_003C_003E4__this.tween = Tween.Create(Tween.TweenMode.Oneshot, null, 1f, start: true);
			_003C_003E4__this.tween.OnUpdate = delegate(Tween t)
			{
				_003C_003E4__this.light.Alpha = 1f - t.Eased;
			};
			_003C_003E4__this.tween.OnComplete = delegate
			{
				_003C_003E4__this.RemoveSelf();
			};
			_003C_003E4__this.Add(_003C_003E4__this.tween);
		}

		internal void _003CUseRoutine_003Eb__3(Tween t)
		{
			_003C_003E4__this.light.Alpha = 1f - t.Eased;
		}

		internal void _003CUseRoutine_003Eb__4(Tween t)
		{
			_003C_003E4__this.RemoveSelf();
		}
	}

	[CompilerGenerated]
	private sealed class _003CUseRoutine_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Key _003C_003E4__this;

		public Vector2 target;

		private _003C_003Ec__DisplayClass28_0 _003C_003E8__1;

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
		public _003CUseRoutine_003Ed__28(int _003C_003E1__state)
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
			Key key = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass28_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				key.Turning = true;
				key.follower.MoveTowardsLeader = false;
				key.wiggler.Start();
				key.wobbleActive = false;
				key.sprite.Y = 0f;
				Vector2 position = key.Position;
				_003C_003E8__1.curve = new SimpleCurve(position, target, (target + position) / 2f + new Vector2(0f, -48f));
				key.tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 1f, start: true);
				key.tween.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1._003C_003E4__this.Position = _003C_003E8__1.curve.GetPoint(t.Eased);
					_003C_003E8__1._003C_003E4__this.sprite.Rate = 1f + t.Eased * 2f;
				};
				key.Add(key.tween);
				_003C_003E2__current = key.tween.Wait();
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				key.tween = null;
				goto IL_014e;
			case 2:
				_003C_003E1__state = -1;
				goto IL_014e;
			case 3:
				_003C_003E1__state = -1;
				key.tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 0.3f, start: true);
				key.tween.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1._003C_003E4__this.sprite.Rotation = t.Eased * ((float)Math.PI / 2f);
				};
				key.Add(key.tween);
				_003C_003E2__current = key.tween.Wait();
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				key.tween = null;
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				key.alarm = Alarm.Set(key, 1f, delegate
				{
					_003C_003E8__1._003C_003E4__this.alarm = null;
					_003C_003E8__1._003C_003E4__this.tween = Tween.Create(Tween.TweenMode.Oneshot, null, 1f, start: true);
					_003C_003E8__1._003C_003E4__this.tween.OnUpdate = delegate(Tween t)
					{
						_003C_003E8__1._003C_003E4__this.light.Alpha = 1f - t.Eased;
					};
					_003C_003E8__1._003C_003E4__this.tween.OnComplete = delegate
					{
						_003C_003E8__1._003C_003E4__this.RemoveSelf();
					};
					_003C_003E8__1._003C_003E4__this.Add(_003C_003E8__1._003C_003E4__this.tween);
				});
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					for (int i = 0; i < 8; i++)
					{
						key.SceneAs<Level>().ParticlesFG.Emit(P_Insert, key.Center, (float)Math.PI / 4f * (float)i);
					}
					key.sprite.Visible = false;
					key.Turning = false;
					return false;
				}
				IL_014e:
				if (key.sprite.CurrentAnimationFrame != 4)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				key.shimmerParticles.Active = false;
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				for (int num2 = 0; num2 < 16; num2++)
				{
					key.SceneAs<Level>().ParticlesFG.Emit(P_Insert, key.Center, (float)Math.PI / 8f * (float)num2);
				}
				key.sprite.Play("enter");
				_003C_003E2__current = 0.3f;
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

	public static ParticleType P_Shimmer;

	public static ParticleType P_Insert;

	public static ParticleType P_Collect;

	public EntityID ID;

	public bool IsUsed;

	public bool StartedUsing;

	private Follower follower;

	private Sprite sprite;

	private Wiggler wiggler;

	private VertexLight light;

	private ParticleEmitter shimmerParticles;

	private float wobble;

	private bool wobbleActive;

	private Tween tween;

	private Alarm alarm;

	private Vector2[] nodes;

	public bool Turning { get; private set; }

	public Key(Vector2 position, EntityID id, Vector2[] nodes)
		: base(position)
	{
		ID = id;
		base.Collider = new Hitbox(12f, 12f, -6f, -6f);
		this.nodes = nodes;
		Add(follower = new Follower(id));
		Add(new PlayerCollider(OnPlayer));
		Add(new MirrorReflection());
		Add(sprite = GFX.SpriteBank.Create("key"));
		sprite.CenterOrigin();
		sprite.Play("idle");
		Add(new TransitionListener
		{
			OnOut = delegate
			{
				StartedUsing = false;
				if (!IsUsed)
				{
					if (tween != null)
					{
						tween.RemoveSelf();
						tween = null;
					}
					if (alarm != null)
					{
						alarm.RemoveSelf();
						alarm = null;
					}
					Turning = false;
					Visible = true;
					sprite.Visible = true;
					sprite.Rate = 1f;
					sprite.Scale = Vector2.One;
					sprite.Play("idle");
					sprite.Rotation = 0f;
					wiggler.Stop();
					follower.MoveTowardsLeader = true;
				}
			}
		});
		Add(wiggler = Wiggler.Create(0.4f, 4f, delegate(float v)
		{
			sprite.Scale = Vector2.One * (1f + v * 0.35f);
		}));
		Add(light = new VertexLight(Color.White, 1f, 32, 48));
	}

	public Key(EntityData data, Vector2 offset, EntityID id)
		: this(data.Position + offset, id, data.NodesOffset(offset))
	{
	}

	public Key(Player player, EntityID id)
		: this(player.Position + new Vector2(-12 * (int)player.Facing, -8f), id, null)
	{
		player.Leader.GainFollower(follower);
		Collidable = false;
		base.Depth = -1000000;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		ParticleSystem particlesFG = (scene as Level).ParticlesFG;
		Add(shimmerParticles = new ParticleEmitter(particlesFG, P_Shimmer, Vector2.Zero, new Vector2(6f, 6f), 1, 0.1f));
		shimmerParticles.SimulateCycle();
	}

	public override void Update()
	{
		if (wobbleActive)
		{
			wobble += Engine.DeltaTime * 4f;
			sprite.Y = (float)Math.Sin(wobble);
		}
		base.Update();
	}

	private void OnPlayer(Player player)
	{
		SceneAs<Level>().Particles.Emit(P_Collect, 10, Position, Vector2.One * 3f);
		Audio.Play("event:/game/general/key_get", Position);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		player.Leader.GainFollower(follower);
		Collidable = false;
		Session session = SceneAs<Level>().Session;
		session.DoNotLoad.Add(ID);
		session.Keys.Add(ID);
		session.UpdateLevelStartDashes();
		wiggler.Start();
		base.Depth = -1000000;
		if (nodes != null && nodes.Length >= 2)
		{
			Add(new Coroutine(NodeRoutine(player)));
		}
	}

	[IteratorStateMachine(typeof(_003CNodeRoutine_003Ed__26))]
	private IEnumerator NodeRoutine(Player player)
	{
		yield return 0.3f;
		if (!player.Dead)
		{
			Audio.Play("event:/game/general/cassette_bubblereturn", SceneAs<Level>().Camera.Position + new Vector2(160f, 90f));
			player.StartCassetteFly(nodes[1], nodes[0]);
		}
	}

	public void RegisterUsed()
	{
		IsUsed = true;
		if (follower.Leader != null)
		{
			follower.Leader.LoseFollower(follower);
		}
		SceneAs<Level>().Session.Keys.Remove(ID);
	}

	[IteratorStateMachine(typeof(_003CUseRoutine_003Ed__28))]
	public IEnumerator UseRoutine(Vector2 target)
	{
		Turning = true;
		follower.MoveTowardsLeader = false;
		wiggler.Start();
		wobbleActive = false;
		sprite.Y = 0f;
		Vector2 position = Position;
		SimpleCurve curve = new SimpleCurve(position, target, (target + position) / 2f + new Vector2(0f, -48f));
		tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeOut, 1f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			Position = curve.GetPoint(t.Eased);
			sprite.Rate = 1f + t.Eased * 2f;
		};
		Add(tween);
		yield return tween.Wait();
		tween = null;
		while (sprite.CurrentAnimationFrame != 4)
		{
			yield return null;
		}
		shimmerParticles.Active = false;
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		for (int num = 0; num < 16; num++)
		{
			SceneAs<Level>().ParticlesFG.Emit(P_Insert, base.Center, (float)Math.PI / 8f * (float)num);
		}
		sprite.Play("enter");
		yield return 0.3f;
		tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 0.3f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			sprite.Rotation = t.Eased * ((float)Math.PI / 2f);
		};
		Add(tween);
		yield return tween.Wait();
		tween = null;
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		alarm = Alarm.Set(this, 1f, delegate
		{
			alarm = null;
			tween = Tween.Create(Tween.TweenMode.Oneshot, null, 1f, start: true);
			tween.OnUpdate = delegate(Tween t)
			{
				light.Alpha = 1f - t.Eased;
			};
			tween.OnComplete = delegate
			{
				RemoveSelf();
			};
			Add(tween);
		});
		yield return 0.2f;
		for (int num2 = 0; num2 < 8; num2++)
		{
			SceneAs<Level>().ParticlesFG.Emit(P_Insert, base.Center, (float)Math.PI / 4f * (float)num2);
		}
		sprite.Visible = false;
		Turning = false;
	}
}
