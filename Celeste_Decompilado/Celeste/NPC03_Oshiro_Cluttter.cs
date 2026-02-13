using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC03_Oshiro_Cluttter : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalkRoutine_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Oshiro_Cluttter _003C_003E4__this;

		public Player player;

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
		public _003CTalkRoutine_003Ed__19(int _003C_003E1__state)
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
			NPC03_Oshiro_Cluttter nPC03_Oshiro_Cluttter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Oshiro_Cluttter.PlayerApproach(player, turnToFace: true, 24f, (nPC03_Oshiro_Cluttter.sectionsComplete != 1 && nPC03_Oshiro_Cluttter.sectionsComplete != 2) ? 1 : (-1));
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Oshiro_Cluttter.Level.ZoomTo(nPC03_Oshiro_Cluttter.ZoomPoint, 2f, 0.5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_CLUTTER" + nPC03_Oshiro_Cluttter.sectionsComplete + "_B", nPC03_Oshiro_Cluttter.StandUp);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = nPC03_Oshiro_Cluttter.Level.ZoomBack(0.5f);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				nPC03_Oshiro_Cluttter.Level.EndCutscene();
				nPC03_Oshiro_Cluttter.EndTalkRoutine(nPC03_Oshiro_Cluttter.Level);
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
	private sealed class _003CStandUp_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Oshiro_Cluttter _003C_003E4__this;

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
		public _003CStandUp_003Ed__21(int _003C_003E1__state)
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
			NPC03_Oshiro_Cluttter nPC03_Oshiro_Cluttter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/oshiro/chat_get_up", nPC03_Oshiro_Cluttter.Position);
				(nPC03_Oshiro_Cluttter.Sprite as OshiroSprite).Pop("idle", flip: false);
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
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
	private sealed class _003CPace_003Ed__22 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Oshiro_Cluttter _003C_003E4__this;

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
		public _003CPace_003Ed__22(int _003C_003E1__state)
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
			NPC03_Oshiro_Cluttter nPC03_Oshiro_Cluttter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0031;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0076;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0076;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00d3;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_00d3;
				}
				IL_00d3:
				if (nPC03_Oshiro_Cluttter.paceTimer < 2.266f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				nPC03_Oshiro_Cluttter.paceTimer = 0f;
				goto IL_0031;
				IL_0076:
				if (nPC03_Oshiro_Cluttter.paceTimer < 2.266f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				nPC03_Oshiro_Cluttter.paceTimer = 0f;
				(nPC03_Oshiro_Cluttter.Sprite as OshiroSprite).Wiggle();
				_003C_003E2__current = nPC03_Oshiro_Cluttter.PaceRight();
				_003C_003E1__state = 3;
				return true;
				IL_0031:
				(nPC03_Oshiro_Cluttter.Sprite as OshiroSprite).Wiggle();
				_003C_003E2__current = nPC03_Oshiro_Cluttter.PaceLeft();
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
	private sealed class _003CPaceRight_003Ed__23 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Oshiro_Cluttter _003C_003E4__this;

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
		public _003CPaceRight_003Ed__23(int _003C_003E1__state)
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
			NPC03_Oshiro_Cluttter nPC03_Oshiro_Cluttter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Vector2 homePosition = nPC03_Oshiro_Cluttter.HomePosition;
				if ((nPC03_Oshiro_Cluttter.Position - homePosition).Length() > 8f)
				{
					nPC03_Oshiro_Cluttter.paceSfx.Play("event:/char/oshiro/move_04_pace_right");
				}
				_003C_003E2__current = nPC03_Oshiro_Cluttter.MoveTo(homePosition);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
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
	private sealed class _003CPaceLeft_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC03_Oshiro_Cluttter _003C_003E4__this;

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
		public _003CPaceLeft_003Ed__24(int _003C_003E1__state)
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
			NPC03_Oshiro_Cluttter nPC03_Oshiro_Cluttter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Vector2 vector = nPC03_Oshiro_Cluttter.HomePosition + new Vector2(-20f, 0f);
				if ((nPC03_Oshiro_Cluttter.Position - vector).Length() > 8f)
				{
					nPC03_Oshiro_Cluttter.paceSfx.Play("event:/char/oshiro/move_04_pace_left");
				}
				_003C_003E2__current = nPC03_Oshiro_Cluttter.MoveTo(vector);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
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

	public const string TalkFlagsA = "oshiro_clutter_";

	public const string TalkFlagsB = "oshiro_clutter_optional_";

	public const string ClearedFlags = "oshiro_clutter_cleared_";

	public const string FinishedFlag = "oshiro_clutter_finished";

	public const string DoorOpenFlag = "oshiro_clutter_door_open";

	public Vector2 HomePosition;

	private int sectionsComplete;

	private bool talked;

	private bool inRoutine;

	private List<Vector2> nodes = new List<Vector2>();

	private Coroutine paceRoutine;

	private Coroutine talkRoutine;

	private SoundSource paceSfx;

	private float paceTimer;

	public Vector2 ZoomPoint
	{
		get
		{
			if (sectionsComplete < 2)
			{
				return Position + new Vector2(0f, -30f) - Level.Camera.Position;
			}
			return Position + new Vector2(0f, -15f) - Level.Camera.Position;
		}
	}

	public NPC03_Oshiro_Cluttter(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(Sprite = new OshiroSprite(-1));
		Add(Talker = new TalkComponent(new Rectangle(-24, -8, 48, 8), new Vector2(0f, -24f), OnTalk));
		Add(Light = new VertexLight(-Vector2.UnitY * 16f, Color.White, 1f, 32, 64));
		MoveAnim = "move";
		IdleAnim = "idle";
		Vector2[] array = data.Nodes;
		foreach (Vector2 vector in array)
		{
			nodes.Add(vector + offset);
		}
		Add(paceSfx = new SoundSource());
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (base.Session.GetFlag("oshiro_clutter_finished"))
		{
			RemoveSelf();
		}
		else
		{
			if (base.Session.GetFlag("oshiro_clutter_cleared_0"))
			{
				sectionsComplete++;
			}
			if (base.Session.GetFlag("oshiro_clutter_cleared_1"))
			{
				sectionsComplete++;
			}
			if (base.Session.GetFlag("oshiro_clutter_cleared_2"))
			{
				sectionsComplete++;
			}
			if (sectionsComplete == 0 || sectionsComplete == 3)
			{
				Sprite.Scale.X = 1f;
			}
			if (sectionsComplete > 0)
			{
				Position = nodes[sectionsComplete - 1];
			}
			else if (!base.Session.GetFlag("oshiro_clutter_0"))
			{
				Add(paceRoutine = new Coroutine(Pace()));
			}
			if (sectionsComplete == 0 && base.Session.GetFlag("oshiro_clutter_0") && !base.Session.GetFlag("oshiro_clutter_optional_0"))
			{
				Sprite.Play("idle_ground");
			}
			if (sectionsComplete == 3 || base.Session.GetFlag("oshiro_clutter_optional_" + sectionsComplete))
			{
				Remove(Talker);
			}
		}
		HomePosition = Position;
	}

	private void OnTalk(Player player)
	{
		talked = true;
		if (paceRoutine != null)
		{
			paceRoutine.RemoveSelf();
		}
		paceRoutine = null;
		if (!base.Session.GetFlag("oshiro_clutter_" + sectionsComplete))
		{
			base.Scene.Add(new CS03_OshiroClutter(player, this, sectionsComplete));
			return;
		}
		Level.StartCutscene(EndTalkRoutine);
		base.Session.SetFlag("oshiro_clutter_optional_" + sectionsComplete);
		Add(talkRoutine = new Coroutine(TalkRoutine(player)));
		if (Talker != null)
		{
			Talker.Enabled = false;
		}
	}

	[IteratorStateMachine(typeof(_003CTalkRoutine_003Ed__19))]
	private IEnumerator TalkRoutine(Player player)
	{
		yield return PlayerApproach(player, turnToFace: true, 24f, (sectionsComplete != 1 && sectionsComplete != 2) ? 1 : (-1));
		yield return Level.ZoomTo(ZoomPoint, 2f, 0.5f);
		yield return Textbox.Say("CH3_OSHIRO_CLUTTER" + sectionsComplete + "_B", StandUp);
		yield return Level.ZoomBack(0.5f);
		Level.EndCutscene();
		EndTalkRoutine(Level);
	}

	private void EndTalkRoutine(Level level)
	{
		if (talkRoutine != null)
		{
			talkRoutine.RemoveSelf();
		}
		talkRoutine = null;
		(Sprite as OshiroSprite).Pop("idle", flip: false);
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.StateMachine.Locked = false;
			entity.StateMachine.State = 0;
		}
	}

	[IteratorStateMachine(typeof(_003CStandUp_003Ed__21))]
	private IEnumerator StandUp()
	{
		Audio.Play("event:/char/oshiro/chat_get_up", Position);
		(Sprite as OshiroSprite).Pop("idle", flip: false);
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CPace_003Ed__22))]
	private IEnumerator Pace()
	{
		while (true)
		{
			(Sprite as OshiroSprite).Wiggle();
			yield return PaceLeft();
			while (paceTimer < 2.266f)
			{
				yield return null;
			}
			paceTimer = 0f;
			(Sprite as OshiroSprite).Wiggle();
			yield return PaceRight();
			while (paceTimer < 2.266f)
			{
				yield return null;
			}
			paceTimer = 0f;
		}
	}

	[IteratorStateMachine(typeof(_003CPaceRight_003Ed__23))]
	public IEnumerator PaceRight()
	{
		Vector2 homePosition = HomePosition;
		if ((Position - homePosition).Length() > 8f)
		{
			paceSfx.Play("event:/char/oshiro/move_04_pace_right");
		}
		yield return MoveTo(homePosition);
	}

	[IteratorStateMachine(typeof(_003CPaceLeft_003Ed__24))]
	public IEnumerator PaceLeft()
	{
		Vector2 vector = HomePosition + new Vector2(-20f, 0f);
		if ((Position - vector).Length() > 8f)
		{
			paceSfx.Play("event:/char/oshiro/move_04_pace_left");
		}
		yield return MoveTo(vector);
	}

	public override void Update()
	{
		base.Update();
		paceTimer += Engine.DeltaTime;
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (sectionsComplete == 3 && !inRoutine && entity != null && entity.X < base.X + 32f && entity.Y <= base.Y)
		{
			OnTalk(entity);
			inRoutine = true;
		}
		if (sectionsComplete == 0 && !talked)
		{
			Level level = base.Scene as Level;
			if (entity != null && !entity.Dead)
			{
				float num = Calc.ClampedMap(Vector2.Distance(base.Center, entity.Center), 40f, 128f);
				level.Session.Audio.Music.Layer(1, num);
				level.Session.Audio.Music.Layer(2, 1f - num);
				level.Session.Audio.Apply();
			}
			else
			{
				level.Session.Audio.Music.Layer(1, value: true);
				level.Session.Audio.Music.Layer(2, value: false);
				level.Session.Audio.Apply();
			}
		}
	}
}
