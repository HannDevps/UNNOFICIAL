using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_OshiroMasterSuite : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CCutscene_003Ed__7(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0039;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0039;
			case 2:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.StateMachine.State = 11;
				cS03_OshiroMasterSuite.player.StateMachine.Locked = true;
				cS03_OshiroMasterSuite.Add(new Coroutine(cS03_OshiroMasterSuite.player.DummyWalkTo(cS03_OshiroMasterSuite.oshiro.X + 32f)));
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/music/lvl3/oshiro_theme");
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_SUITE", cS03_OshiroMasterSuite.SuiteShadowAppear, cS03_OshiroMasterSuite.SuiteShadowDisrupt, cS03_OshiroMasterSuite.SuiteShadowCeiling, cS03_OshiroMasterSuite.Wander, cS03_OshiroMasterSuite.Console, cS03_OshiroMasterSuite.JumpBack, cS03_OshiroMasterSuite.Collapse, cS03_OshiroMasterSuite.AwkwardPause);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.evil.Add(new SoundSource(Vector2.Zero, "event:/game/03_resort/suite_bad_exittop"));
				_003C_003E2__current = cS03_OshiroMasterSuite.evil.FloatTo(new Vector2(cS03_OshiroMasterSuite.evil.X, level.Bounds.Top - 32));
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.Scene.Remove(cS03_OshiroMasterSuite.evil);
				break;
			case 6:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0039:
				cS03_OshiroMasterSuite.player = cS03_OshiroMasterSuite.Scene.Tracker.GetEntity<Player>();
				if (cS03_OshiroMasterSuite.player == null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				Audio.SetMusic(null);
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 2;
				return true;
			}
			if (level.Lighting.Alpha != level.BaseLightingAlpha)
			{
				level.Lighting.Alpha = Calc.Approach(level.Lighting.Alpha, level.BaseLightingAlpha, Engine.DeltaTime * 0.5f);
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
				return true;
			}
			cS03_OshiroMasterSuite.EndCutscene(level);
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
	private sealed class _003CWander_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CWander_003Ed__8(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.Facing = Facings.Right;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroMasterSuite.player.DummyWalkToExact((int)cS03_OshiroMasterSuite.oshiro.X + 48);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.Facing = Facings.Left;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroMasterSuite.player.DummyWalkToExact((int)cS03_OshiroMasterSuite.oshiro.X - 32);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.oshiro.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.DummyAutoAnimate = false;
				cS03_OshiroMasterSuite.player.Sprite.Play("lookUp");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.DummyAutoAnimate = true;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.Facing = Facings.Right;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroMasterSuite.player.DummyWalkToExact((int)cS03_OshiroMasterSuite.oshiro.X - 24);
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 13;
				return true;
			case 13:
			{
				_003C_003E1__state = -1;
				Level level = cS03_OshiroMasterSuite.SceneAs<Level>();
				_003C_003E2__current = level.ZoomTo(new Vector2(190f, 110f), 2f, 0.5f);
				_003C_003E1__state = 14;
				return true;
			}
			case 14:
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
	private sealed class _003CAwkwardPause_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CAwkwardPause_003Ed__9(int _003C_003E1__state)
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
				_003C_003E2__current = 2f;
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
	private sealed class _003CSuiteShadowAppear_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

		private Vector2 _003Cfrom_003E5__2;

		private Vector2 _003Cto_003E5__3;

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
		public _003CSuiteShadowAppear_003Ed__10(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (cS03_OshiroMasterSuite.mirror == null)
				{
					return false;
				}
				cS03_OshiroMasterSuite.mirror.EvilAppear();
				cS03_OshiroMasterSuite.SetMusic();
				Audio.Play("event:/game/03_resort/suite_bad_intro", cS03_OshiroMasterSuite.mirror.Position);
				_003Cfrom_003E5__2 = cS03_OshiroMasterSuite.Level.ZoomFocusPoint;
				_003Cto_003E5__3 = new Vector2(216f, 110f);
				_003Cp_003E5__4 = 0f;
				goto IL_00f8;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__4 += Engine.DeltaTime * 2f;
				goto IL_00f8;
			case 2:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00f8:
				if (_003Cp_003E5__4 < 1f)
				{
					cS03_OshiroMasterSuite.Level.ZoomFocusPoint = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.SineInOut(_003Cp_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
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
	private sealed class _003CSuiteShadowDisrupt_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CSuiteShadowDisrupt_003Ed__11(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (cS03_OshiroMasterSuite.mirror == null)
				{
					return false;
				}
				Audio.Play("event:/game/03_resort/suite_bad_mirrorbreak", cS03_OshiroMasterSuite.mirror.Position);
				_003C_003E2__current = cS03_OshiroMasterSuite.mirror.SmashRoutine();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.evil = new BadelineDummy(cS03_OshiroMasterSuite.mirror.Position + new Vector2(0f, -8f));
				cS03_OshiroMasterSuite.Scene.Add(cS03_OshiroMasterSuite.evil);
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.oshiro.Sprite.Scale.X = 1f;
				_003C_003E2__current = cS03_OshiroMasterSuite.evil.FloatTo(cS03_OshiroMasterSuite.oshiro.Position + new Vector2(32f, -24f));
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
	private sealed class _003CCollapse_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CCollapse_003Ed__12(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.oshiro.Sprite.Play("fall");
				Audio.Play("event:/char/oshiro/chat_collapse", cS03_OshiroMasterSuite.oshiro.Position);
				_003C_003E2__current = null;
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
	private sealed class _003CConsole_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CConsole_003Ed__13(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroMasterSuite.player.DummyWalkToExact((int)cS03_OshiroMasterSuite.oshiro.X - 16);
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
	private sealed class _003CJumpBack_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CJumpBack_003Ed__14(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroMasterSuite.player.DummyWalkToExact((int)cS03_OshiroMasterSuite.oshiro.X - 24, walkBackwards: true);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
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
	private sealed class _003CSuiteShadowCeiling_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroMasterSuite _003C_003E4__this;

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
		public _003CSuiteShadowCeiling_003Ed__15(int _003C_003E1__state)
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
			CS03_OshiroMasterSuite cS03_OshiroMasterSuite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroMasterSuite.SceneAs<Level>().ZoomBack(0.5f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.evil.Add(new SoundSource(Vector2.Zero, "event:/game/03_resort/suite_bad_movestageleft"));
				_003C_003E2__current = cS03_OshiroMasterSuite.evil.FloatTo(new Vector2(cS03_OshiroMasterSuite.Level.Bounds.Left + 96, cS03_OshiroMasterSuite.evil.Y - 16f), 1);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.player.Facing = Facings.Left;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS03_OshiroMasterSuite.evil.Add(new SoundSource(Vector2.Zero, "event:/game/03_resort/suite_bad_ceilingbreak"));
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				cS03_OshiroMasterSuite.Level.DirectionalShake(-Vector2.UnitY);
				_003C_003E2__current = cS03_OshiroMasterSuite.evil.SmashBlock(cS03_OshiroMasterSuite.evil.Position + new Vector2(0f, -32f));
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.8f;
				_003C_003E1__state = 5;
				return true;
			case 5:
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

	public const string Flag = "oshiro_resort_suite";

	private Player player;

	private NPC oshiro;

	private BadelineDummy evil;

	private ResortMirror mirror;

	public CS03_OshiroMasterSuite(NPC oshiro)
	{
		this.oshiro = oshiro;
	}

	public override void OnBegin(Level level)
	{
		mirror = base.Scene.Entities.FindFirst<ResortMirror>();
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene(Level level)
	{
		while (true)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			if (player != null)
			{
				break;
			}
			yield return null;
		}
		Audio.SetMusic(null);
		yield return 0.4f;
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		Add(new Coroutine(player.DummyWalkTo(oshiro.X + 32f)));
		yield return 1f;
		Audio.SetMusic("event:/music/lvl3/oshiro_theme");
		yield return Textbox.Say("CH3_OSHIRO_SUITE", SuiteShadowAppear, SuiteShadowDisrupt, SuiteShadowCeiling, Wander, Console, JumpBack, Collapse, AwkwardPause);
		evil.Add(new SoundSource(Vector2.Zero, "event:/game/03_resort/suite_bad_exittop"));
		yield return evil.FloatTo(new Vector2(evil.X, level.Bounds.Top - 32));
		base.Scene.Remove(evil);
		while (level.Lighting.Alpha != level.BaseLightingAlpha)
		{
			level.Lighting.Alpha = Calc.Approach(level.Lighting.Alpha, level.BaseLightingAlpha, Engine.DeltaTime * 0.5f);
			yield return null;
		}
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CWander_003Ed__8))]
	private IEnumerator Wander()
	{
		yield return 0.5f;
		player.Facing = Facings.Right;
		yield return 0.1f;
		yield return player.DummyWalkToExact((int)oshiro.X + 48);
		yield return 1f;
		player.Facing = Facings.Left;
		yield return 0.2f;
		yield return player.DummyWalkToExact((int)oshiro.X - 32);
		yield return 0.1f;
		oshiro.Sprite.Scale.X = -1f;
		yield return 0.2f;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("lookUp");
		yield return 1f;
		player.DummyAutoAnimate = true;
		yield return 0.4f;
		player.Facing = Facings.Right;
		yield return 0.2f;
		yield return player.DummyWalkToExact((int)oshiro.X - 24);
		yield return 0.5f;
		Level level = SceneAs<Level>();
		yield return level.ZoomTo(new Vector2(190f, 110f), 2f, 0.5f);
	}

	[IteratorStateMachine(typeof(_003CAwkwardPause_003Ed__9))]
	private IEnumerator AwkwardPause()
	{
		yield return 2f;
	}

	[IteratorStateMachine(typeof(_003CSuiteShadowAppear_003Ed__10))]
	private IEnumerator SuiteShadowAppear()
	{
		if (mirror != null)
		{
			mirror.EvilAppear();
			SetMusic();
			Audio.Play("event:/game/03_resort/suite_bad_intro", mirror.Position);
			Vector2 from = Level.ZoomFocusPoint;
			Vector2 to = new Vector2(216f, 110f);
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 2f)
			{
				Level.ZoomFocusPoint = from + (to - from) * Ease.SineInOut(p);
				yield return null;
			}
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CSuiteShadowDisrupt_003Ed__11))]
	private IEnumerator SuiteShadowDisrupt()
	{
		if (mirror != null)
		{
			Audio.Play("event:/game/03_resort/suite_bad_mirrorbreak", mirror.Position);
			yield return mirror.SmashRoutine();
			evil = new BadelineDummy(mirror.Position + new Vector2(0f, -8f));
			base.Scene.Add(evil);
			yield return 1.2f;
			oshiro.Sprite.Scale.X = 1f;
			yield return evil.FloatTo(oshiro.Position + new Vector2(32f, -24f));
		}
	}

	[IteratorStateMachine(typeof(_003CCollapse_003Ed__12))]
	private IEnumerator Collapse()
	{
		oshiro.Sprite.Play("fall");
		Audio.Play("event:/char/oshiro/chat_collapse", oshiro.Position);
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CConsole_003Ed__13))]
	private IEnumerator Console()
	{
		yield return player.DummyWalkToExact((int)oshiro.X - 16);
	}

	[IteratorStateMachine(typeof(_003CJumpBack_003Ed__14))]
	private IEnumerator JumpBack()
	{
		yield return player.DummyWalkToExact((int)oshiro.X - 24, walkBackwards: true);
		yield return 0.8f;
	}

	[IteratorStateMachine(typeof(_003CSuiteShadowCeiling_003Ed__15))]
	private IEnumerator SuiteShadowCeiling()
	{
		yield return SceneAs<Level>().ZoomBack(0.5f);
		evil.Add(new SoundSource(Vector2.Zero, "event:/game/03_resort/suite_bad_movestageleft"));
		yield return evil.FloatTo(new Vector2(Level.Bounds.Left + 96, evil.Y - 16f), 1);
		player.Facing = Facings.Left;
		yield return 0.25f;
		evil.Add(new SoundSource(Vector2.Zero, "event:/game/03_resort/suite_bad_ceilingbreak"));
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		Level.DirectionalShake(-Vector2.UnitY);
		yield return evil.SmashBlock(evil.Position + new Vector2(0f, -32f));
		yield return 0.8f;
	}

	private void SetMusic()
	{
		if (Level.Session.Area.Mode == AreaMode.Normal)
		{
			Level.Session.Audio.Music.Event = "event:/music/lvl2/evil_madeline";
			Level.Session.Audio.Apply();
		}
	}

	public override void OnEnd(Level level)
	{
		if (WasSkipped)
		{
			if (evil != null)
			{
				base.Scene.Remove(evil);
			}
			if (mirror != null)
			{
				mirror.Broken();
			}
			base.Scene.Entities.FindFirst<DashBlock>()?.RemoveAndFlagAsGone();
			oshiro.Sprite.Play("idle_ground");
		}
		oshiro.Talker.Enabled = true;
		if (player != null)
		{
			player.StateMachine.Locked = false;
			player.StateMachine.State = 0;
		}
		level.Lighting.Alpha = level.BaseLightingAlpha;
		level.Session.SetFlag("oshiro_resort_suite");
		SetMusic();
	}
}
