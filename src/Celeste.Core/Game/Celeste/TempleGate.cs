using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class TempleGate : Solid
{
	public enum Types
	{
		NearestSwitch,
		CloseBehindPlayer,
		CloseBehindPlayerAlways,
		HoldingTheo,
		TouchSwitches,
		CloseBehindPlayerAndTheo
	}

	[CompilerGenerated]
	private sealed class _003CCloseBehindPlayer_003Ed__27 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TempleGate _003C_003E4__this;

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
		public _003CCloseBehindPlayer_003Ed__27(int _003C_003E1__state)
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
			TempleGate templeGate = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
			}
			Player entity = templeGate.Scene.Tracker.GetEntity<Player>();
			if (templeGate.lockState || entity == null || !(entity.Left > templeGate.Right + 4f))
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			templeGate.Close();
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
	private sealed class _003CCloseBehindPlayerAndTheo_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TempleGate _003C_003E4__this;

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
		public _003CCloseBehindPlayerAndTheo_003Ed__28(int _003C_003E1__state)
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
			TempleGate templeGate = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
			}
			Player entity = templeGate.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.Left > templeGate.Right + 4f)
			{
				TheoCrystal entity2 = templeGate.Scene.Tracker.GetEntity<TheoCrystal>();
				if (!templeGate.lockState && entity2 != null && entity2.Left > templeGate.Right + 4f)
				{
					templeGate.Close();
					return false;
				}
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
	private sealed class _003CCheckTouchSwitches_003Ed__29 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TempleGate _003C_003E4__this;

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
		public _003CCheckTouchSwitches_003Ed__29(int _003C_003E1__state)
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
			TempleGate templeGate = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_004a;
			case 1:
				_003C_003E1__state = -1;
				goto IL_004a;
			case 2:
				_003C_003E1__state = -1;
				templeGate.shaker.ShakeFor(0.2f, removeOnFinish: false);
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				break;
			case 4:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_004a:
				if (!Switch.Check(templeGate.Scene))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				templeGate.sprite.Play("open");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			}
			if (templeGate.lockState)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
			}
			templeGate.Open();
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

	private const int OpenHeight = 0;

	private const float HoldingWaitTime = 0.2f;

	private const float HoldingOpenDistSq = 4096f;

	private const float HoldingCloseDistSq = 6400f;

	private const int MinDrawHeight = 4;

	public string LevelID;

	public Types Type;

	public bool ClaimedByASwitch;

	private bool theoGate;

	private int closedHeight;

	private Sprite sprite;

	private Shaker shaker;

	private float drawHeight;

	private float drawHeightMoveSpeed;

	private bool open;

	private float holdingWaitTimer = 0.2f;

	private Vector2 holdingCheckFrom;

	private bool lockState;

	public TempleGate(Vector2 position, int height, Types type, string spriteName, string levelID)
		: base(position, 8f, height, safe: true)
	{
		Type = type;
		closedHeight = height;
		LevelID = levelID;
		Add(sprite = GFX.SpriteBank.Create("templegate_" + spriteName));
		sprite.X = base.Collider.Width / 2f;
		sprite.Play("idle");
		Add(shaker = new Shaker(on: false));
		base.Depth = -9000;
		theoGate = spriteName.Equals("theo", StringComparison.InvariantCultureIgnoreCase);
		holdingCheckFrom = Position + new Vector2(base.Width / 2f, height / 2);
	}

	public TempleGate(EntityData data, Vector2 offset, string levelID)
		: this(data.Position + offset, data.Height, data.Enum("type", Types.NearestSwitch), data.Attr("sprite", "default"), levelID)
	{
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (Type == Types.CloseBehindPlayer)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.Left < base.Right && entity.Bottom >= base.Top && entity.Top <= base.Bottom)
			{
				StartOpen();
				Add(new Coroutine(CloseBehindPlayer()));
			}
		}
		else if (Type == Types.CloseBehindPlayerAlways)
		{
			StartOpen();
			Add(new Coroutine(CloseBehindPlayer()));
		}
		else if (Type == Types.CloseBehindPlayerAndTheo)
		{
			StartOpen();
			Add(new Coroutine(CloseBehindPlayerAndTheo()));
		}
		else if (Type == Types.HoldingTheo)
		{
			if (TheoIsNearby())
			{
				StartOpen();
			}
			base.Hitbox.Width = 16f;
		}
		else if (Type == Types.TouchSwitches)
		{
			Add(new Coroutine(CheckTouchSwitches()));
		}
		drawHeight = Math.Max(4f, base.Height);
	}

	public bool CloseBehindPlayerCheck()
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			return entity.X < base.X;
		}
		return false;
	}

	public void SwitchOpen()
	{
		sprite.Play("open");
		Alarm.Set(this, 0.2f, delegate
		{
			shaker.ShakeFor(0.2f, removeOnFinish: false);
			Alarm.Set(this, 0.2f, Open);
		});
	}

	public void Open()
	{
		Audio.Play(theoGate ? "event:/game/05_mirror_temple/gate_theo_open" : "event:/game/05_mirror_temple/gate_main_open", Position);
		holdingWaitTimer = 0.2f;
		drawHeightMoveSpeed = 200f;
		drawHeight = base.Height;
		shaker.ShakeFor(0.2f, removeOnFinish: false);
		SetHeight(0);
		sprite.Play("open");
		open = true;
	}

	public void StartOpen()
	{
		SetHeight(0);
		drawHeight = 4f;
		open = true;
	}

	public void Close()
	{
		Audio.Play(theoGate ? "event:/game/05_mirror_temple/gate_theo_close" : "event:/game/05_mirror_temple/gate_main_close", Position);
		holdingWaitTimer = 0.2f;
		drawHeightMoveSpeed = 300f;
		drawHeight = Math.Max(4f, base.Height);
		shaker.ShakeFor(0.2f, removeOnFinish: false);
		SetHeight(closedHeight);
		sprite.Play("hit");
		open = false;
	}

	[IteratorStateMachine(typeof(_003CCloseBehindPlayer_003Ed__27))]
	private IEnumerator CloseBehindPlayer()
	{
		while (true)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (!lockState && entity != null && entity.Left > base.Right + 4f)
			{
				break;
			}
			yield return null;
		}
		Close();
	}

	[IteratorStateMachine(typeof(_003CCloseBehindPlayerAndTheo_003Ed__28))]
	private IEnumerator CloseBehindPlayerAndTheo()
	{
		while (true)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.Left > base.Right + 4f)
			{
				TheoCrystal entity2 = base.Scene.Tracker.GetEntity<TheoCrystal>();
				if (!lockState && entity2 != null && entity2.Left > base.Right + 4f)
				{
					break;
				}
			}
			yield return null;
		}
		Close();
	}

	[IteratorStateMachine(typeof(_003CCheckTouchSwitches_003Ed__29))]
	private IEnumerator CheckTouchSwitches()
	{
		while (!Switch.Check(base.Scene))
		{
			yield return null;
		}
		sprite.Play("open");
		yield return 0.5f;
		shaker.ShakeFor(0.2f, removeOnFinish: false);
		yield return 0.2f;
		while (lockState)
		{
			yield return null;
		}
		Open();
	}

	public bool TheoIsNearby()
	{
		TheoCrystal entity = base.Scene.Tracker.GetEntity<TheoCrystal>();
		if (entity != null && !(entity.X > base.X + 10f))
		{
			return Vector2.DistanceSquared(holdingCheckFrom, entity.Center) < (open ? 6400f : 4096f);
		}
		return true;
	}

	private void SetHeight(int height)
	{
		if ((float)height < base.Collider.Height)
		{
			base.Collider.Height = height;
			return;
		}
		float y = base.Y;
		int num = (int)base.Collider.Height;
		if (base.Collider.Height < 64f)
		{
			base.Y -= 64f - base.Collider.Height;
			base.Collider.Height = 64f;
		}
		MoveVExact(height - num);
		base.Y = y;
		base.Collider.Height = height;
	}

	public override void Update()
	{
		base.Update();
		if (Type == Types.HoldingTheo)
		{
			if (holdingWaitTimer > 0f)
			{
				holdingWaitTimer -= Engine.DeltaTime;
			}
			else if (!lockState)
			{
				if (open && !TheoIsNearby())
				{
					Close();
					CollideFirst<Player>(Position + new Vector2(8f, 0f))?.Die(Vector2.Zero);
				}
				else if (!open && TheoIsNearby())
				{
					Open();
				}
			}
		}
		float num = Math.Max(4f, base.Height);
		if (drawHeight != num)
		{
			lockState = true;
			drawHeight = Calc.Approach(drawHeight, num, drawHeightMoveSpeed * Engine.DeltaTime);
		}
		else
		{
			lockState = false;
		}
	}

	public override void Render()
	{
		Vector2 vector = new Vector2(Math.Sign(shaker.Value.X), 0f);
		Draw.Rect(base.X - 2f, base.Y - 8f, 14f, 10f, Color.Black);
		sprite.DrawSubrect(Vector2.Zero + vector, new Rectangle(0, (int)(sprite.Height - drawHeight), (int)sprite.Width, (int)drawHeight));
	}
}
