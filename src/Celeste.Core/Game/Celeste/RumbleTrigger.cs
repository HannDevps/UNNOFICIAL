using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class RumbleTrigger : Trigger
{
	[CompilerGenerated]
	private sealed class _003CRumbleRoutine_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public RumbleTrigger _003C_003E4__this;

		private List<CrumbleWallOnRumble>.Enumerator _003C_003E7__wrap1;

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
		public _003CRumbleRoutine_003Ed__13(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 2)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				RumbleTrigger rumbleTrigger = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E2__current = delay;
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					_ = rumbleTrigger.Scene;
					rumbleTrigger.rumble = 1f;
					Audio.Play("event:/new_content/game/10_farewell/quake_onset", rumbleTrigger.Position);
					Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
					foreach (Decal decal in rumbleTrigger.decals)
					{
						decal.Visible = true;
					}
					_003C_003E7__wrap1 = rumbleTrigger.crumbles.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 2:
					_003C_003E1__state = -3;
					break;
				}
				if (_003C_003E7__wrap1.MoveNext())
				{
					_003C_003E7__wrap1.Current.Break();
					_003C_003E2__current = 0.05f;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap1 = default(List<CrumbleWallOnRumble>.Enumerator);
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap1/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private bool manualTrigger;

	private bool started;

	private bool persistent;

	private EntityID id;

	private float rumble;

	private float left;

	private float right;

	private List<Decal> decals = new List<Decal>();

	private List<CrumbleWallOnRumble> crumbles = new List<CrumbleWallOnRumble>();

	public RumbleTrigger(EntityData data, Vector2 offset, EntityID id)
		: base(data, offset)
	{
		manualTrigger = data.Bool("manualTrigger");
		persistent = data.Bool("persistent");
		this.id = id;
		Vector2[] array = data.NodesOffset(offset);
		if (array.Length >= 2)
		{
			left = Math.Min(array[0].X, array[1].X);
			right = Math.Max(array[0].X, array[1].X);
		}
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Level level = base.Scene as Level;
		bool flag = false;
		if (persistent && level.Session.GetFlag(id.ToString()))
		{
			flag = true;
		}
		foreach (CrumbleWallOnRumble entity in scene.Tracker.GetEntities<CrumbleWallOnRumble>())
		{
			if (entity.X >= left && entity.X <= right)
			{
				if (flag)
				{
					entity.RemoveSelf();
				}
				else
				{
					crumbles.Add(entity);
				}
			}
		}
		if (!flag)
		{
			foreach (Decal item in scene.Entities.FindAll<Decal>())
			{
				if (item.IsCrack && item.X >= left && item.X <= right)
				{
					item.Visible = false;
					decals.Add(item);
				}
			}
			crumbles.Sort((CrumbleWallOnRumble a, CrumbleWallOnRumble b) => (!Calc.Random.Chance(0.5f)) ? 1 : (-1));
		}
		if (flag)
		{
			RemoveSelf();
		}
	}

	public override void OnEnter(Player player)
	{
		base.OnEnter(player);
		if (!manualTrigger)
		{
			Invoke();
		}
	}

	private void Invoke(float delay = 0f)
	{
		if (!started)
		{
			started = true;
			if (persistent)
			{
				(base.Scene as Level).Session.SetFlag(id.ToString());
			}
			Add(new Coroutine(RumbleRoutine(delay)));
			Add(new DisplacementRenderHook(RenderDisplacement));
		}
	}

	[IteratorStateMachine(typeof(_003CRumbleRoutine_003Ed__13))]
	private IEnumerator RumbleRoutine(float delay)
	{
		yield return delay;
		_ = base.Scene;
		rumble = 1f;
		Audio.Play("event:/new_content/game/10_farewell/quake_onset", Position);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		foreach (Decal decal in decals)
		{
			decal.Visible = true;
		}
		foreach (CrumbleWallOnRumble crumble in crumbles)
		{
			crumble.Break();
			yield return 0.05f;
		}
	}

	public override void Update()
	{
		base.Update();
		rumble = Calc.Approach(rumble, 0f, Engine.DeltaTime * 0.7f);
	}

	private void RenderDisplacement()
	{
		if (!(rumble <= 0f) && Settings.Instance.ScreenShake != ScreenshakeAmount.Off)
		{
			Camera camera = (base.Scene as Level).Camera;
			int num = (int)(camera.Left / 8f) - 1;
			int num2 = (int)(camera.Right / 8f) + 1;
			for (int i = num; i <= num2; i++)
			{
				float num3 = (float)Math.Sin(base.Scene.TimeActive * 60f + (float)i * 0.4f) * 0.06f * rumble;
				Draw.Rect(color: new Color(0.5f, 0.5f + num3, 0f, 1f), x: i * 8, y: camera.Top - 2f, width: 8f, height: 184f);
			}
		}
	}

	public static void ManuallyTrigger(float x, float delay)
	{
		foreach (RumbleTrigger item in Engine.Scene.Entities.FindAll<RumbleTrigger>())
		{
			if (item.manualTrigger && x >= item.left && x <= item.right)
			{
				item.Invoke(delay);
			}
		}
	}
}
