using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ClutterDoor : Solid
{
	[CompilerGenerated]
	private sealed class _003CUnlockRoutine_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ClutterDoor _003C_003E4__this;

		private Camera _003Ccamera_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private Vector2 _003Cto_003E5__4;

		private float _003Cp_003E5__5;

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
		public _003CUnlockRoutine_003Ed__7(int _003C_003E1__state)
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
			ClutterDoor clutterDoor = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ccamera_003E5__2 = clutterDoor.SceneAs<Level>().Camera;
				_003Cfrom_003E5__3 = _003Ccamera_003E5__2.Position;
				_003Cto_003E5__4 = clutterDoor.CameraTarget();
				if ((_003Cfrom_003E5__3 - _003Cto_003E5__4).Length() > 8f)
				{
					_003Cp_003E5__5 = 0f;
					goto IL_00f0;
				}
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__5 += Engine.DeltaTime;
				goto IL_00f0;
			case 2:
				_003C_003E1__state = -1;
				goto IL_011f;
			case 3:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__5 += Engine.DeltaTime;
					break;
				}
				IL_00f0:
				if (_003Cp_003E5__5 < 1f)
				{
					_003Ccamera_003E5__2.Position = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.CubeInOut(_003Cp_003E5__5);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_011f;
				IL_011f:
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				Audio.Play("event:/game/03_resort/forcefield_vanish", clutterDoor.Position);
				clutterDoor.sprite.Play("open");
				clutterDoor.Collidable = false;
				_003Cp_003E5__5 = 0f;
				break;
			}
			if (_003Cp_003E5__5 < 0.4f)
			{
				_003Ccamera_003E5__2.Position = clutterDoor.CameraTarget();
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
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

	public ClutterBlock.Colors Color;

	private Sprite sprite;

	private Wiggler wiggler;

	public ClutterDoor(EntityData data, Vector2 offset, Session session)
		: base(data.Position + offset, data.Width, data.Height, safe: false)
	{
		Color = data.Enum("type", ClutterBlock.Colors.Green);
		SurfaceSoundIndex = 20;
		base.Tag = Tags.TransitionUpdate;
		Add(sprite = GFX.SpriteBank.Create("ghost_door"));
		sprite.Position = new Vector2(base.Width, base.Height) / 2f;
		sprite.Play("idle");
		OnDashCollide = OnDashed;
		Add(wiggler = Wiggler.Create(0.6f, 3f, delegate(float f)
		{
			sprite.Scale = Vector2.One * (1f - f * 0.2f);
		}));
		if (!IsLocked(session))
		{
			InstantUnlock();
		}
	}

	public override void Update()
	{
		Level level = base.Scene as Level;
		if (level.Transitioning && CollideCheck<Player>())
		{
			Visible = false;
			Collidable = false;
		}
		else if (!Collidable && IsLocked(level.Session) && !CollideCheck<Player>())
		{
			Visible = true;
			Collidable = true;
			wiggler.Start();
			Audio.Play("event:/game/03_resort/forcefield_bump", Position);
		}
		base.Update();
	}

	public bool IsLocked(Session session)
	{
		if (session.GetFlag("oshiro_clutter_door_open"))
		{
			return IsComplete(session);
		}
		return true;
	}

	public bool IsComplete(Session session)
	{
		return session.GetFlag("oshiro_clutter_cleared_" + (int)Color);
	}

	[IteratorStateMachine(typeof(_003CUnlockRoutine_003Ed__7))]
	public IEnumerator UnlockRoutine()
	{
		Camera camera = SceneAs<Level>().Camera;
		Vector2 from = camera.Position;
		Vector2 to = CameraTarget();
		if ((from - to).Length() > 8f)
		{
			for (float p = 0f; p < 1f; p += Engine.DeltaTime)
			{
				camera.Position = from + (to - from) * Ease.CubeInOut(p);
				yield return null;
			}
		}
		else
		{
			yield return 0.2f;
		}
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		Audio.Play("event:/game/03_resort/forcefield_vanish", Position);
		sprite.Play("open");
		Collidable = false;
		for (float p = 0f; p < 0.4f; p += Engine.DeltaTime)
		{
			camera.Position = CameraTarget();
			yield return null;
		}
	}

	public void InstantUnlock()
	{
		Visible = (Collidable = false);
	}

	private Vector2 CameraTarget()
	{
		Level level = SceneAs<Level>();
		Vector2 result = Position - new Vector2(320f, 180f) / 2f;
		result.X = MathHelper.Clamp(result.X, level.Bounds.Left, level.Bounds.Right - 320);
		result.Y = MathHelper.Clamp(result.Y, level.Bounds.Top, level.Bounds.Bottom - 180);
		return result;
	}

	private DashCollisionResults OnDashed(Player player, Vector2 direction)
	{
		wiggler.Start();
		Audio.Play("event:/game/03_resort/forcefield_bump", Position);
		return DashCollisionResults.Bounce;
	}
}
