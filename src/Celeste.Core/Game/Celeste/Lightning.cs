using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Lightning : Entity
{
	[CompilerGenerated]
	private sealed class _003CMoveRoutine_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Lightning _003C_003E4__this;

		public Vector2 start;

		public Vector2 end;

		public float moveTime;

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
		public _003CMoveRoutine_003Ed__15(int _003C_003E1__state)
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
			Lightning lightning = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0029;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = lightning.Move(end, start, moveTime);
				_003C_003E1__state = 2;
				return true;
			case 2:
				{
					_003C_003E1__state = -1;
					goto IL_0029;
				}
				IL_0029:
				_003C_003E2__current = lightning.Move(start, end, moveTime);
				_003C_003E1__state = 1;
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
	private sealed class _003CMove_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Lightning _003C_003E4__this;

		public Vector2 start;

		public Vector2 end;

		public float moveTime;

		private float _003Cat_003E5__2;

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
		public _003CMove_003Ed__16(int _003C_003E1__state)
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
			Lightning lightning = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				_003Cat_003E5__2 = MathHelper.Clamp(_003Cat_003E5__2 + Engine.DeltaTime / moveTime, 0f, 1f);
			}
			else
			{
				_003C_003E1__state = -1;
				_003Cat_003E5__2 = 0f;
			}
			lightning.Position = Vector2.Lerp(start, end, Ease.SineInOut(_003Cat_003E5__2));
			if (_003Cat_003E5__2 >= 1f)
			{
				return false;
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
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
	private sealed class _003CPulseRoutine_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		private float _003Ct_003E5__2;

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
		public _003CPulseRoutine_003Ed__18(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ct_003E5__2 = 0f;
				goto IL_006f;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__2 += Engine.DeltaTime * 8f;
				goto IL_006f;
			case 2:
				{
					_003C_003E1__state = -1;
					_003Ct_003E5__2 -= Engine.DeltaTime * 8f;
					break;
				}
				IL_006f:
				if (_003Ct_003E5__2 < 1f)
				{
					SetPulseValue(level, _003Ct_003E5__2);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003Ct_003E5__2 = 1f;
				break;
			}
			if (_003Ct_003E5__2 > 0f)
			{
				SetPulseValue(level, _003Ct_003E5__2);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			SetPulseValue(level, 0f);
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
	private sealed class _003CRemoveRoutine_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Level level;

		public Action onComplete;

		private List<Lightning> _003Cblocks_003E5__2;

		private float _003Ct_003E5__3;

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
		public _003CRemoveRoutine_003Ed__21(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				_003Cblocks_003E5__2 = level.Entities.FindAll<Lightning>();
				foreach (Lightning item in new List<Lightning>(_003Cblocks_003E5__2))
				{
					item.disappearing = true;
					if (item.Right < level.Camera.Left || item.Bottom < level.Camera.Top || item.Left > level.Camera.Right || item.Top > level.Camera.Bottom)
					{
						_003Cblocks_003E5__2.Remove(item);
						item.RemoveSelf();
					}
				}
				LightningRenderer entity = level.Tracker.GetEntity<LightningRenderer>();
				entity.StopAmbience();
				entity.UpdateSeeds = false;
				_003Ct_003E5__3 = 0f;
				goto IL_0155;
			}
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__3 += Engine.DeltaTime * 4f;
				goto IL_0155;
			case 2:
				{
					_003C_003E1__state = -1;
					_003Ct_003E5__3 += Engine.DeltaTime * 8f;
					break;
				}
				IL_0155:
				if (_003Ct_003E5__3 < 1f)
				{
					SetBreakValue(level, _003Ct_003E5__3);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				SetBreakValue(level, 1f);
				level.Shake();
				for (int num = _003Cblocks_003E5__2.Count - 1; num >= 0; num--)
				{
					_003Cblocks_003E5__2[num].Shatter();
				}
				_003Ct_003E5__3 = 0f;
				break;
			}
			if (_003Ct_003E5__3 < 1f)
			{
				SetBreakValue(level, 1f - _003Ct_003E5__3);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			SetBreakValue(level, 0f);
			foreach (Lightning item2 in _003Cblocks_003E5__2)
			{
				item2.RemoveSelf();
			}
			FlingBird flingBird = level.Entities.FindFirst<FlingBird>();
			if (flingBird != null)
			{
				flingBird.LightningRemoved = true;
			}
			if (onComplete != null)
			{
				onComplete();
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

	public static ParticleType P_Shatter;

	public const string Flag = "disable_lightning";

	public float Fade;

	private bool disappearing;

	private float toggleOffset;

	public int VisualWidth;

	public int VisualHeight;

	public Lightning(Vector2 position, int width, int height, Vector2? node, float moveTime)
		: base(position)
	{
		VisualWidth = width;
		VisualHeight = height;
		base.Collider = new Hitbox(width - 2, height - 2, 1f, 1f);
		Add(new PlayerCollider(OnPlayer));
		if (node.HasValue)
		{
			Add(new Coroutine(MoveRoutine(position, node.Value, moveTime)));
		}
		toggleOffset = Calc.Random.NextFloat();
	}

	public Lightning(EntityData data, Vector2 levelOffset)
		: this(data.Position + levelOffset, data.Width, data.Height, data.FirstNodeNullable(levelOffset), data.Float("moveTime"))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		scene.Tracker.GetEntity<LightningRenderer>().Track(this);
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		scene.Tracker.GetEntity<LightningRenderer>().Untrack(this);
	}

	public override void Update()
	{
		if (Collidable && base.Scene.OnInterval(0.25f, toggleOffset))
		{
			ToggleCheck();
		}
		if (!Collidable && base.Scene.OnInterval(0.05f, toggleOffset))
		{
			ToggleCheck();
		}
		base.Update();
	}

	public void ToggleCheck()
	{
		Collidable = (Visible = InView());
	}

	private bool InView()
	{
		Camera camera = (base.Scene as Level).Camera;
		if (base.X + base.Width > camera.X - 16f && base.Y + base.Height > camera.Y - 16f && base.X < camera.X + 320f + 16f)
		{
			return base.Y < camera.Y + 180f + 16f;
		}
		return false;
	}

	private void OnPlayer(Player player)
	{
		if (!disappearing && !SaveData.Instance.Assists.Invincible)
		{
			int num = Math.Sign(player.X - base.X);
			if (num == 0)
			{
				num = -1;
			}
			player.Die(Vector2.UnitX * num);
		}
	}

	[IteratorStateMachine(typeof(_003CMoveRoutine_003Ed__15))]
	private IEnumerator MoveRoutine(Vector2 start, Vector2 end, float moveTime)
	{
		while (true)
		{
			yield return Move(start, end, moveTime);
			yield return Move(end, start, moveTime);
		}
	}

	[IteratorStateMachine(typeof(_003CMove_003Ed__16))]
	private IEnumerator Move(Vector2 start, Vector2 end, float moveTime)
	{
		float at = 0f;
		while (true)
		{
			Position = Vector2.Lerp(start, end, Ease.SineInOut(at));
			if (at >= 1f)
			{
				break;
			}
			yield return null;
			at = MathHelper.Clamp(at + Engine.DeltaTime / moveTime, 0f, 1f);
		}
	}

	private void Shatter()
	{
		if (base.Scene == null)
		{
			return;
		}
		for (int i = 4; (float)i < base.Width; i += 8)
		{
			for (int j = 4; (float)j < base.Height; j += 8)
			{
				SceneAs<Level>().ParticlesFG.Emit(P_Shatter, 1, base.TopLeft + new Vector2(i, j), Vector2.One * 3f);
			}
		}
	}

	[IteratorStateMachine(typeof(_003CPulseRoutine_003Ed__18))]
	public static IEnumerator PulseRoutine(Level level)
	{
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 8f)
		{
			SetPulseValue(level, t);
			yield return null;
		}
		for (float t = 1f; t > 0f; t -= Engine.DeltaTime * 8f)
		{
			SetPulseValue(level, t);
			yield return null;
		}
		SetPulseValue(level, 0f);
	}

	private static void SetPulseValue(Level level, float t)
	{
		BloomRenderer bloom = level.Bloom;
		LightningRenderer entity = level.Tracker.GetEntity<LightningRenderer>();
		Glitch.Value = MathHelper.Lerp(0f, 0.075f, t);
		bloom.Strength = MathHelper.Lerp(1f, 1.2f, t);
		entity.Fade = t * 0.2f;
	}

	private static void SetBreakValue(Level level, float t)
	{
		BloomRenderer bloom = level.Bloom;
		LightningRenderer entity = level.Tracker.GetEntity<LightningRenderer>();
		Glitch.Value = MathHelper.Lerp(0f, 0.15f, t);
		bloom.Strength = MathHelper.Lerp(1f, 1.5f, t);
		entity.Fade = t * 0.6f;
	}

	[IteratorStateMachine(typeof(_003CRemoveRoutine_003Ed__21))]
	public static IEnumerator RemoveRoutine(Level level, Action onComplete = null)
	{
		List<Lightning> blocks = level.Entities.FindAll<Lightning>();
		foreach (Lightning item in new List<Lightning>(blocks))
		{
			item.disappearing = true;
			if (item.Right < level.Camera.Left || item.Bottom < level.Camera.Top || item.Left > level.Camera.Right || item.Top > level.Camera.Bottom)
			{
				blocks.Remove(item);
				item.RemoveSelf();
			}
		}
		LightningRenderer entity = level.Tracker.GetEntity<LightningRenderer>();
		entity.StopAmbience();
		entity.UpdateSeeds = false;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 4f)
		{
			SetBreakValue(level, t);
			yield return null;
		}
		SetBreakValue(level, 1f);
		level.Shake();
		for (int num = blocks.Count - 1; num >= 0; num--)
		{
			blocks[num].Shatter();
		}
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 8f)
		{
			SetBreakValue(level, 1f - t);
			yield return null;
		}
		SetBreakValue(level, 0f);
		foreach (Lightning item2 in blocks)
		{
			item2.RemoveSelf();
		}
		FlingBird flingBird = level.Entities.FindFirst<FlingBird>();
		if (flingBird != null)
		{
			flingBird.LightningRemoved = true;
		}
		onComplete?.Invoke();
	}
}
