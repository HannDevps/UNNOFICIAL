using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_OshiroRooftop : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

		public Level level;

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
		public _003CCutscene_003Ed__13(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0078;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0078;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00bc;
			case 3:
				_003C_003E1__state = -1;
				cS03_OshiroRooftop.evil = new BadelineDummy(new Vector2(cS03_OshiroRooftop.oshiro.X - 40f, level.Bounds.Bottom - 60));
				cS03_OshiroRooftop.evil.Sprite.Scale.X = 1f;
				cS03_OshiroRooftop.evil.Appear(level);
				level.Add(cS03_OshiroRooftop.evil);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS03_OshiroRooftop.player.Facing = Facings.Left;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_START_CHASE", cS03_OshiroRooftop.MaddyWalkAway, cS03_OshiroRooftop.MaddyTurnAround, cS03_OshiroRooftop.EnterOshiro, cS03_OshiroRooftop.OshiroGetsAngry);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroRooftop.OshiroTransform();
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS03_OshiroRooftop.Add(new Coroutine(cS03_OshiroRooftop.AnxietyAndCameraOut()));
				_003C_003E2__current = level.ZoomBack(0.5f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				{
					_003C_003E1__state = -1;
					cS03_OshiroRooftop.EndCutscene(level);
					return false;
				}
				IL_0078:
				if (cS03_OshiroRooftop.player == null)
				{
					cS03_OshiroRooftop.player = cS03_OshiroRooftop.Scene.Tracker.GetEntity<Player>();
					if (cS03_OshiroRooftop.player == null)
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
				cS03_OshiroRooftop.player.StateMachine.State = 11;
				cS03_OshiroRooftop.player.StateMachine.Locked = true;
				goto IL_00bc;
				IL_00bc:
				if (!cS03_OshiroRooftop.player.OnGround() || cS03_OshiroRooftop.player.Speed.Y < 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 0.6f;
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

	[CompilerGenerated]
	private sealed class _003CMaddyWalkAway_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

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
		public _003CMaddyWalkAway_003Ed__14(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Level level = cS03_OshiroRooftop.Scene as Level;
				cS03_OshiroRooftop.Add(new Coroutine(cS03_OshiroRooftop.player.DummyWalkTo((float)level.Bounds.Left + 170f)));
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/03_resort/suite_bad_moveroof", cS03_OshiroRooftop.evil.Position);
				cS03_OshiroRooftop.Add(new Coroutine(cS03_OshiroRooftop.evil.FloatTo(cS03_OshiroRooftop.evil.Position + new Vector2(80f, 30f))));
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			case 2:
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
	private sealed class _003CMaddyTurnAround_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

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
		public _003CMaddyTurnAround_003Ed__15(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_OshiroRooftop.player.Facing = Facings.Left;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
			{
				_003C_003E1__state = -1;
				Level level = cS03_OshiroRooftop.SceneAs<Level>();
				_003C_003E2__current = level.ZoomTo(new Vector2(150f, cS03_OshiroRooftop.bossSpawnPosition.Y - (float)level.Bounds.Y - 8f), 2f, 0.5f);
				_003C_003E1__state = 3;
				return true;
			}
			case 3:
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
	private sealed class _003CEnterOshiro_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

		private float _003Cfrom_003E5__2;

		private float _003Cp_003E5__3;

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
		public _003CEnterOshiro_003Ed__16(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
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
				cS03_OshiroRooftop.bossSpriteOffset = (cS03_OshiroRooftop.bossSprite.Justify.Value.Y - cS03_OshiroRooftop.oshiro.Sprite.Justify.Value.Y) * cS03_OshiroRooftop.bossSprite.Height;
				cS03_OshiroRooftop.oshiro.Visible = true;
				cS03_OshiroRooftop.oshiro.Sprite.Scale.X = 1f;
				cS03_OshiroRooftop.Add(new Coroutine(cS03_OshiroRooftop.oshiro.MoveTo(cS03_OshiroRooftop.bossSpawnPosition - new Vector2(0f, cS03_OshiroRooftop.bossSpriteOffset))));
				cS03_OshiroRooftop.oshiro.Add(new SoundSource("event:/char/oshiro/move_07_roof00_enter"));
				_003Cfrom_003E5__2 = cS03_OshiroRooftop.Level.ZoomFocusPoint.X;
				_003Cp_003E5__3 = 0f;
				goto IL_0191;
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime / 0.7f;
				goto IL_0191;
			case 3:
				_003C_003E1__state = -1;
				cS03_OshiroRooftop.player.Facing = Facings.Left;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					cS03_OshiroRooftop.evil.Sprite.Scale.X = -1f;
					return false;
				}
				IL_0191:
				if (_003Cp_003E5__3 < 1f)
				{
					cS03_OshiroRooftop.Level.ZoomFocusPoint.X = _003Cfrom_003E5__2 + (126f - _003Cfrom_003E5__2) * Ease.CubeInOut(_003Cp_003E5__3);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
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

	[CompilerGenerated]
	private sealed class _003COshiroGetsAngry_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

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
		public _003COshiroGetsAngry_003Ed__17(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_OshiroRooftop.evil.Vanish();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				cS03_OshiroRooftop.evil = null;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/oshiro/boss_transform_begin", cS03_OshiroRooftop.oshiro.Position);
				cS03_OshiroRooftop.oshiro.Remove(cS03_OshiroRooftop.oshiro.Sprite);
				cS03_OshiroRooftop.oshiro.Sprite = cS03_OshiroRooftop.bossSprite;
				cS03_OshiroRooftop.oshiro.Sprite.Play("transformStart");
				cS03_OshiroRooftop.oshiro.Y += cS03_OshiroRooftop.bossSpriteOffset;
				cS03_OshiroRooftop.oshiro.Add(cS03_OshiroRooftop.oshiro.Sprite);
				cS03_OshiroRooftop.oshiro.Depth = -12500;
				cS03_OshiroRooftop.oshiroRumble = true;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
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
	private sealed class _003COshiroTransform_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

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
		public _003COshiroTransform_003Ed__18(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/oshiro/boss_transform_burst", cS03_OshiroRooftop.oshiro.Position);
				cS03_OshiroRooftop.oshiro.Sprite.Play("transformFinish");
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				cS03_OshiroRooftop.SceneAs<Level>().Shake(0.5f);
				cS03_OshiroRooftop.SetChaseMusic();
				goto IL_00d1;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00d1;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00d1:
				if (cS03_OshiroRooftop.anxiety < 0.5f)
				{
					cS03_OshiroRooftop.anxiety = Calc.Approach(cS03_OshiroRooftop.anxiety, 0.5f, Engine.DeltaTime * 0.5f);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 0.25f;
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

	[CompilerGenerated]
	private sealed class _003CAnxietyAndCameraOut_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroRooftop _003C_003E4__this;

		private Level _003Clevel_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private Vector2 _003Cto_003E5__4;

		private float _003Ct_003E5__5;

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
		public _003CAnxietyAndCameraOut_003Ed__19(int _003C_003E1__state)
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
			CS03_OshiroRooftop cS03_OshiroRooftop = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = cS03_OshiroRooftop.Scene as Level;
				_003Cfrom_003E5__3 = _003Clevel_003E5__2.Camera.Position;
				_003Cto_003E5__4 = cS03_OshiroRooftop.player.CameraTarget;
				_003Ct_003E5__5 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__5 += Engine.DeltaTime * 2f;
				break;
			}
			if (_003Ct_003E5__5 < 1f)
			{
				cS03_OshiroRooftop.anxiety = Calc.Approach(cS03_OshiroRooftop.anxiety, 0f, Engine.DeltaTime * 4f);
				_003Clevel_003E5__2.Camera.Position = _003Cfrom_003E5__3 + (_003Cto_003E5__4 - _003Cfrom_003E5__3) * Ease.CubeInOut(_003Ct_003E5__5);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
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

	public const string Flag = "oshiro_resort_roof";

	private const float playerEndPosition = 170f;

	private Player player;

	private NPC oshiro;

	private BadelineDummy evil;

	private Vector2 bossSpawnPosition;

	private float anxiety;

	private float anxietyFlicker;

	private Sprite bossSprite = GFX.SpriteBank.Create("oshiro_boss");

	private float bossSpriteOffset;

	private bool oshiroRumble;

	public CS03_OshiroRooftop(NPC oshiro)
	{
		this.oshiro = oshiro;
	}

	public override void OnBegin(Level level)
	{
		bossSpawnPosition = new Vector2(oshiro.X, level.Bounds.Bottom - 40);
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__13))]
	private IEnumerator Cutscene(Level level)
	{
		while (player == null)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			if (player != null)
			{
				break;
			}
			yield return null;
		}
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		while (!player.OnGround() || player.Speed.Y < 0f)
		{
			yield return null;
		}
		yield return 0.6f;
		evil = new BadelineDummy(new Vector2(oshiro.X - 40f, level.Bounds.Bottom - 60));
		evil.Sprite.Scale.X = 1f;
		evil.Appear(level);
		level.Add(evil);
		yield return 0.1f;
		player.Facing = Facings.Left;
		yield return Textbox.Say("CH3_OSHIRO_START_CHASE", MaddyWalkAway, MaddyTurnAround, EnterOshiro, OshiroGetsAngry);
		yield return OshiroTransform();
		Add(new Coroutine(AnxietyAndCameraOut()));
		yield return level.ZoomBack(0.5f);
		yield return 0.25f;
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CMaddyWalkAway_003Ed__14))]
	private IEnumerator MaddyWalkAway()
	{
		Level level = base.Scene as Level;
		Add(new Coroutine(player.DummyWalkTo((float)level.Bounds.Left + 170f)));
		yield return 0.2f;
		Audio.Play("event:/game/03_resort/suite_bad_moveroof", evil.Position);
		Add(new Coroutine(evil.FloatTo(evil.Position + new Vector2(80f, 30f))));
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CMaddyTurnAround_003Ed__15))]
	private IEnumerator MaddyTurnAround()
	{
		yield return 0.25f;
		player.Facing = Facings.Left;
		yield return 0.1f;
		Level level = SceneAs<Level>();
		yield return level.ZoomTo(new Vector2(150f, bossSpawnPosition.Y - (float)level.Bounds.Y - 8f), 2f, 0.5f);
	}

	[IteratorStateMachine(typeof(_003CEnterOshiro_003Ed__16))]
	private IEnumerator EnterOshiro()
	{
		yield return 0.3f;
		bossSpriteOffset = (bossSprite.Justify.Value.Y - oshiro.Sprite.Justify.Value.Y) * bossSprite.Height;
		oshiro.Visible = true;
		oshiro.Sprite.Scale.X = 1f;
		Add(new Coroutine(oshiro.MoveTo(bossSpawnPosition - new Vector2(0f, bossSpriteOffset))));
		oshiro.Add(new SoundSource("event:/char/oshiro/move_07_roof00_enter"));
		float from = Level.ZoomFocusPoint.X;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.7f)
		{
			Level.ZoomFocusPoint.X = from + (126f - from) * Ease.CubeInOut(p);
			yield return null;
		}
		yield return 0.3f;
		player.Facing = Facings.Left;
		yield return 0.1f;
		evil.Sprite.Scale.X = -1f;
	}

	[IteratorStateMachine(typeof(_003COshiroGetsAngry_003Ed__17))]
	private IEnumerator OshiroGetsAngry()
	{
		yield return 0.1f;
		evil.Vanish();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		evil = null;
		yield return 0.8f;
		Audio.Play("event:/char/oshiro/boss_transform_begin", oshiro.Position);
		oshiro.Remove(oshiro.Sprite);
		oshiro.Sprite = bossSprite;
		oshiro.Sprite.Play("transformStart");
		oshiro.Y += bossSpriteOffset;
		oshiro.Add(oshiro.Sprite);
		oshiro.Depth = -12500;
		oshiroRumble = true;
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003COshiroTransform_003Ed__18))]
	private IEnumerator OshiroTransform()
	{
		yield return 0.2f;
		Audio.Play("event:/char/oshiro/boss_transform_burst", oshiro.Position);
		oshiro.Sprite.Play("transformFinish");
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		SceneAs<Level>().Shake(0.5f);
		SetChaseMusic();
		while (anxiety < 0.5f)
		{
			anxiety = Calc.Approach(anxiety, 0.5f, Engine.DeltaTime * 0.5f);
			yield return null;
		}
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CAnxietyAndCameraOut_003Ed__19))]
	private IEnumerator AnxietyAndCameraOut()
	{
		Level level = base.Scene as Level;
		Vector2 from = level.Camera.Position;
		Vector2 to = player.CameraTarget;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 2f)
		{
			anxiety = Calc.Approach(anxiety, 0f, Engine.DeltaTime * 4f);
			level.Camera.Position = from + (to - from) * Ease.CubeInOut(t);
			yield return null;
		}
	}

	private void SetChaseMusic()
	{
		Level obj = base.Scene as Level;
		obj.Session.Audio.Music.Event = "event:/music/lvl3/oshiro_chase";
		obj.Session.Audio.Apply();
	}

	public override void OnEnd(Level level)
	{
		Distort.Anxiety = (anxiety = (anxietyFlicker = 0f));
		if (evil != null)
		{
			level.Remove(evil);
		}
		player = base.Scene.Tracker.GetEntity<Player>();
		if (player != null)
		{
			player.StateMachine.Locked = false;
			player.StateMachine.State = 0;
			player.X = (float)level.Bounds.Left + 170f;
			player.Speed.Y = 0f;
			while (player.CollideCheck<Solid>())
			{
				player.Y--;
			}
			level.Camera.Position = player.CameraTarget;
		}
		if (WasSkipped)
		{
			SetChaseMusic();
		}
		oshiro.RemoveSelf();
		base.Scene.Add(new AngryOshiro(bossSpawnPosition, fromCutscene: true));
		level.Session.RespawnPoint = new Vector2((float)level.Bounds.Left + 170f, level.Bounds.Top + 160);
		level.Session.SetFlag("oshiro_resort_roof");
	}

	public override void Update()
	{
		Distort.Anxiety = anxiety + anxiety * anxietyFlicker;
		if (base.Scene.OnInterval(0.05f))
		{
			anxietyFlicker = -0.2f + Calc.Random.NextFloat(0.4f);
		}
		base.Update();
		if (oshiroRumble)
		{
			Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
		}
	}
}
