using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS04_Gondola : CutsceneEntity
{
	private enum GondolaStates
	{
		Stopped,
		MovingToCenter,
		InCenter,
		Shaking,
		MovingToEnd
	}

	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CCutscene_003Ed__26(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.player.StateMachine.State = 11;
				_003C_003E2__current = cS04_Gondola.player.DummyWalkToExact((int)cS04_Gondola.gondola.X + 16);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0093;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0093;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.ShowPhoto();
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					cS04_Gondola.EndCutscene(cS04_Gondola.Level);
					return false;
				}
				IL_0093:
				if (!cS04_Gondola.player.OnGround())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				Audio.SetMusic("event:/music/lvl1/theo");
				_003C_003E2__current = Textbox.Say("CH4_GONDOLA", cS04_Gondola.EnterTheo, cS04_Gondola.CheckOnTheo, cS04_Gondola.GetUpTheo, cS04_Gondola.LookAtLever, cS04_Gondola.PullLever, cS04_Gondola.WaitABit, cS04_Gondola.WaitForCenter, cS04_Gondola.SelfieThenStallsOut, cS04_Gondola.MovePlayerLeft, cS04_Gondola.SnapLeverOff, cS04_Gondola.DarknessAppears, cS04_Gondola.DarknessConsumes, cS04_Gondola.CantBreath, cS04_Gondola.StartBreathing, cS04_Gondola.Ascend, cS04_Gondola.WaitABit, cS04_Gondola.TheoTakesOutPhone, cS04_Gondola.FaceTheo);
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
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public CS04_Gondola _003C_003E4__this;

		public Vector2 start;

		internal void _003CEnterTheo_003Eb__0(Tween t)
		{
			_003C_003E4__this.theo.Sprite.Scale.X = MathHelper.Lerp(start.X, 1f, t.Eased);
			_003C_003E4__this.theo.Sprite.Scale.Y = MathHelper.Lerp(start.Y, 1f, t.Eased);
		}
	}

	[CompilerGenerated]
	private sealed class _003CEnterTheo_003Ed__28 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

		private _003C_003Ec__DisplayClass28_0 _003C_003E8__1;

		private float _003Cspeed_003E5__2;

		private float _003CtheoStartX_003E5__3;

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
		public _003CEnterTheo_003Ed__28(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E8__1 = new _003C_003Ec__DisplayClass28_0();
				_003C_003E8__1._003C_003E4__this = _003C_003E4__this;
				cS04_Gondola.player.Facing = Facings.Left;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.PanCamera(new Vector2(cS04_Gondola.Level.Bounds.Left, cS04_Gondola.theo.Y - 90f), 1f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Visible = true;
				_003CtheoStartX_003E5__3 = cS04_Gondola.theo.X;
				_003C_003E2__current = cS04_Gondola.theo.MoveTo(new Vector2(_003CtheoStartX_003E5__3 + 35f, cS04_Gondola.theo.Y));
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.theo.MoveTo(new Vector2(_003CtheoStartX_003E5__3 + 60f, cS04_Gondola.theo.Y));
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/gondola_theo_fall", cS04_Gondola.theo.Position);
				cS04_Gondola.theo.Sprite.Play("idleEdge");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Play("falling");
				cS04_Gondola.theo.X += 4f;
				cS04_Gondola.theo.Depth = -10010;
				_003Cspeed_003E5__2 = 80f;
				goto IL_0291;
			case 7:
				_003C_003E1__state = -1;
				goto IL_0291;
			case 8:
			{
				_003C_003E1__state = -1;
				_003C_003E8__1.start = cS04_Gondola.theo.Sprite.Scale;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 2f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					_003C_003E8__1._003C_003E4__this.theo.Sprite.Scale.X = MathHelper.Lerp(_003C_003E8__1.start.X, 1f, t.Eased);
					_003C_003E8__1._003C_003E4__this.theo.Sprite.Scale.Y = MathHelper.Lerp(_003C_003E8__1.start.Y, 1f, t.Eased);
				};
				cS04_Gondola.Add(tween);
				_003C_003E2__current = cS04_Gondola.PanCamera(new Vector2(cS04_Gondola.Level.Bounds.Left, cS04_Gondola.theo.Y - 120f), 1f);
				_003C_003E1__state = 9;
				return true;
			}
			case 9:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 10;
				return true;
			case 10:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0291:
				if (cS04_Gondola.theo.Y < cS04_Gondola.player.Y)
				{
					cS04_Gondola.theo.Y += _003Cspeed_003E5__2 * Engine.DeltaTime;
					_003Cspeed_003E5__2 += 120f * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				cS04_Gondola.Level.DirectionalShake(new Vector2(0f, 1f));
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				cS04_Gondola.theo.Y = cS04_Gondola.player.Y;
				cS04_Gondola.theo.Sprite.Play("hitGround");
				cS04_Gondola.theo.Sprite.Rate = 0f;
				cS04_Gondola.theo.Depth = 1000;
				cS04_Gondola.theo.Sprite.Scale = new Vector2(1.3f, 0.8f);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 8;
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
	private sealed class _003CCheckOnTheo_003Ed__29 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CCheckOnTheo_003Ed__29(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.player.DummyWalkTo(cS04_Gondola.gondola.X - 18f);
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
	private sealed class _003CGetUpTheo_003Ed__30 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CGetUpTheo_003Ed__30(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.4f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/gondola_theo_recover", cS04_Gondola.theo.Position);
				cS04_Gondola.theo.Sprite.Rate = 1f;
				cS04_Gondola.theo.Sprite.Play("recoverGround");
				_003C_003E2__current = 1.6f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.theo.MoveTo(new Vector2(cS04_Gondola.gondola.X - 50f, cS04_Gondola.player.Y));
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
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
	private sealed class _003CLookAtLever_003Ed__31 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CLookAtLever_003Ed__31(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.theo.MoveTo(new Vector2(cS04_Gondola.gondola.X + 7f, cS04_Gondola.theo.Y));
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.player.Facing = Facings.Right;
				cS04_Gondola.theo.Sprite.Scale.X = -1f;
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
	private sealed class _003CPullLever_003Ed__32 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CPullLever_003Ed__32(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.Add(new Coroutine(cS04_Gondola.player.DummyWalkToExact((int)cS04_Gondola.gondola.X - 7)));
				cS04_Gondola.theo.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/gondola_theo_lever_start", cS04_Gondola.theo.Position);
				cS04_Gondola.theo.Sprite.Play("pullVent");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				cS04_Gondola.gondola.Lever.Play("pulled");
				cS04_Gondola.theo.Sprite.Play("fallVent");
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.PanCamera(cS04_Gondola.gondola.Position + new Vector2(-160f, -120f), 1f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
			{
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Background.Backdrops.Add(cS04_Gondola.loopingCloud = new Parallax(GFX.Game["bgs/04/bgCloudLoop"]));
				cS04_Gondola.Level.Background.Backdrops.Add(cS04_Gondola.bottomCloud = new Parallax(GFX.Game["bgs/04/bgCloud"]));
				cS04_Gondola.loopingCloud.LoopX = (cS04_Gondola.bottomCloud.LoopX = true);
				cS04_Gondola.loopingCloud.LoopY = (cS04_Gondola.bottomCloud.LoopY = false);
				cS04_Gondola.loopingCloud.Position.Y = cS04_Gondola.Level.Camera.Top - (float)cS04_Gondola.loopingCloud.Texture.Height - (float)cS04_Gondola.bottomCloud.Texture.Height;
				cS04_Gondola.bottomCloud.Position.Y = cS04_Gondola.Level.Camera.Top - (float)cS04_Gondola.bottomCloud.Texture.Height;
				cS04_Gondola.LoopCloudsAt = cS04_Gondola.bottomCloud.Position.Y;
				cS04_Gondola.AutoSnapCharacters = true;
				cS04_Gondola.theoXOffset = cS04_Gondola.theo.X - cS04_Gondola.gondola.X;
				cS04_Gondola.playerXOffset = cS04_Gondola.player.X - cS04_Gondola.gondola.X;
				cS04_Gondola.player.StateMachine.State = 17;
				Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 16f, start: true);
				tween.OnUpdate = delegate(Tween t)
				{
					if (Audio.CurrentMusic == "event:/music/lvl1/theo")
					{
						Audio.SetMusicParam("fade", 1f - t.Eased);
					}
				};
				cS04_Gondola.Add(tween);
				SoundSource soundSource = new SoundSource();
				soundSource.Position = cS04_Gondola.gondola.LeftCliffside.Position;
				soundSource.Play("event:/game/04_cliffside/gondola_cliffmechanism_start");
				cS04_Gondola.Add(soundSource);
				cS04_Gondola.moveLoopSfx.Play("event:/game/04_cliffside/gondola_movement_loop");
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.FullSecond);
				cS04_Gondola.gondolaSpeed = 32f;
				cS04_Gondola.gondola.RotationSpeed = 1f;
				cS04_Gondola.gondolaState = GondolaStates.MovingToCenter;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 7;
				return true;
			}
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.MoveTheoOnGondola(12f, changeFacing: false);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Scale.X = -1f;
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
	private sealed class _003CWaitABit_003Ed__33 : IEnumerator<object>, IDisposable, IEnumerator
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
		public _003CWaitABit_003Ed__33(int _003C_003E1__state)
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
				_003C_003E2__current = 1f;
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
	private sealed class _003CWaitForCenter_003Ed__34 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CWaitForCenter_003Ed__34(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
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
				_003C_003E2__current = cS04_Gondola.MovePlayerOnGondola(-20f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_004a:
				if (cS04_Gondola.gondolaState != GondolaStates.InCenter)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS04_Gondola.theo.Sprite.Scale.X = 1f;
				_003C_003E2__current = 1f;
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
	private sealed class _003CSelfieThenStallsOut_003Ed__35 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CSelfieThenStallsOut_003Ed__35(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusic("event:/music/lvl4/minigame");
				cS04_Gondola.Add(new Coroutine(cS04_Gondola.Level.ZoomTo(new Vector2(160f, 110f), 2f, 0.5f)));
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS04_Gondola.Add(new Coroutine(cS04_Gondola.MovePlayerOnGondola(cS04_Gondola.theoXOffset - 8f)));
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/gondola_theoselfie_halt", cS04_Gondola.theo.Position);
				cS04_Gondola.theo.Sprite.Play("holdOutPhone");
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS04_Gondola.theoXOffset += 4f;
				cS04_Gondola.playerXOffset += 4f;
				cS04_Gondola.gondola.RotationSpeed = -1f;
				cS04_Gondola.gondolaState = GondolaStates.Stopped;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				cS04_Gondola.theo.Sprite.Play("takeSelfieImmediate");
				cS04_Gondola.Add(new Coroutine(cS04_Gondola.PanCamera(cS04_Gondola.gondola.Position + (cS04_Gondola.gondola.Destination - cS04_Gondola.gondola.Position).SafeNormalize() * 32f + new Vector2(-160f, -120f), 0.3f, Ease.CubeOut)));
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Flash(Color.White);
				cS04_Gondola.Level.Add(cS04_Gondola.evil = new BadelineDummy(Vector2.Zero));
				cS04_Gondola.evil.Appear(cS04_Gondola.Level);
				cS04_Gondola.evil.Floatness = 0f;
				cS04_Gondola.evil.Depth = -1000000;
				cS04_Gondola.moveLoopSfx.Stop();
				cS04_Gondola.haltLoopSfx.Play("event:/game/04_cliffside/gondola_halted_loop");
				cS04_Gondola.gondolaState = GondolaStates.Shaking;
				_003C_003E2__current = cS04_Gondola.PanCamera(cS04_Gondola.gondola.Position + new Vector2(-160f, -120f), 1f);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 7;
				return true;
			case 7:
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
	private sealed class _003CMovePlayerLeft_003Ed__36 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CMovePlayerLeft_003Ed__36(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.MovePlayerOnGondola(-20f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.MovePlayerOnGondola(20f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.MovePlayerOnGondola(-10f);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS04_Gondola.player.Facing = Facings.Right;
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
	private sealed class _003CSnapLeverOff_003Ed__37 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CSnapLeverOff_003Ed__37(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.MoveTheoOnGondola(7f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/gondola_theo_lever_fail", cS04_Gondola.theo.Position);
				cS04_Gondola.theo.Sprite.Play("pullVent");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Play("fallVent");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS04_Gondola.gondola.BreakLever();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				cS04_Gondola.Level.Shake();
				_003C_003E2__current = 2.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
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
	private sealed class _003CDarknessAppears_003Ed__38 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

		private float _003Cp_003E5__2;

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
		public _003CDarknessAppears_003Ed__38(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				Audio.SetMusicParam("calm", 0f);
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.player.Sprite.Play("tired");
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				cS04_Gondola.evil.Vanish();
				cS04_Gondola.evil = null;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.NextColorGrade("panicattack");
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
				cS04_Gondola.BurstTentacles(3, 90f);
				Audio.Play("event:/game/04_cliffside/gondola_scaryhair_01", cS04_Gondola.gondola.Position);
				_003Cp_003E5__2 = 0f;
				goto IL_019d;
			case 4:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Background.Fade = _003Cp_003E5__2;
				cS04_Gondola.anxiety = _003Cp_003E5__2;
				if (cS04_Gondola.windSnowFg != null)
				{
					cS04_Gondola.windSnowFg.Alpha = 1f - _003Cp_003E5__2;
				}
				_003Cp_003E5__2 += Engine.DeltaTime / 2f;
				goto IL_019d;
			case 5:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_019d:
				if (_003Cp_003E5__2 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
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
	private sealed class _003CDarknessConsumes_003Ed__39 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CDarknessConsumes_003Ed__39(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				Audio.Play("event:/game/04_cliffside/gondola_scaryhair_02", cS04_Gondola.gondola.Position);
				cS04_Gondola.BurstTentacles(2, 60f);
				_003C_003E2__current = cS04_Gondola.MoveTheoOnGondola(0f);
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Play("comfortStart");
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
	private sealed class _003CCantBreath_003Ed__40 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CCantBreath_003Ed__40(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				Audio.Play("event:/game/04_cliffside/gondola_scaryhair_03", cS04_Gondola.gondola.Position);
				cS04_Gondola.BurstTentacles(1, 30f);
				cS04_Gondola.BurstTentacles(0, 0f, 100f);
				cS04_Gondola.rumbler = new BreathingRumbler();
				cS04_Gondola.Scene.Add(cS04_Gondola.rumbler);
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
	private sealed class _003CStartBreathing_003Ed__41 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

		private BreathingMinigame _003Cbreathing_003E5__2;

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
		public _003CStartBreathing_003Ed__41(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cbreathing_003E5__2 = new BreathingMinigame(winnable: true, cS04_Gondola.rumbler);
				cS04_Gondola.Scene.Add(_003Cbreathing_003E5__2);
				goto IL_006d;
			case 1:
				_003C_003E1__state = -1;
				goto IL_006d;
			case 2:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/04_cliffside/gondola_restart", cS04_Gondola.gondola.Position);
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS04_Gondola.moveLoopSfx.Play("event:/game/04_cliffside/gondola_movement_loop");
				cS04_Gondola.haltLoopSfx.Stop();
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				cS04_Gondola.gondolaState = GondolaStates.InCenter;
				cS04_Gondola.gondola.RotationSpeed = 0.5f;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_006d:
				if (!_003Cbreathing_003E5__2.Completed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				foreach (ReflectionTentacles tentacle in cS04_Gondola.tentacles)
				{
					tentacle.RemoveSelf();
				}
				cS04_Gondola.anxiety = 0f;
				cS04_Gondola.Level.Background.Fade = 0f;
				cS04_Gondola.Level.SnapColorGrade(null);
				cS04_Gondola.gondola.CancelPullSides();
				cS04_Gondola.Level.ResetZoom();
				_003C_003E2__current = 0.5f;
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
	private sealed class _003CAscend_003Ed__42 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CAscend_003Ed__42(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.gondolaState = GondolaStates.MovingToEnd;
				goto IL_0059;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0059;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00e4;
			case 3:
				_003C_003E1__state = -1;
				goto IL_00e4;
			case 4:
				_003C_003E1__state = -1;
				cS04_Gondola.player.DummyAutoAnimate = false;
				cS04_Gondola.player.Sprite.Play("tired");
				_003C_003E2__current = cS04_Gondola.theo.MoveTo(new Vector2(cS04_Gondola.gondola.X + 64f, cS04_Gondola.theo.Y));
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_00e4:
				if (cS04_Gondola.gondola.Rotation > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				cS04_Gondola.gondola.Rotation = (cS04_Gondola.gondola.RotationSpeed = 0f);
				cS04_Gondola.Level.Shake();
				cS04_Gondola.AutoSnapCharacters = false;
				cS04_Gondola.player.StateMachine.State = 11;
				cS04_Gondola.player.Position = cS04_Gondola.player.Position.Floor();
				while (cS04_Gondola.player.CollideCheck<Solid>())
				{
					cS04_Gondola.player.Y--;
				}
				cS04_Gondola.theo.Position.Y = cS04_Gondola.player.Position.Y;
				cS04_Gondola.theo.Sprite.Play("comfortRecover");
				cS04_Gondola.theo.Sprite.Scale.X = 1f;
				_003C_003E2__current = cS04_Gondola.player.DummyWalkTo(cS04_Gondola.gondola.X + 80f);
				_003C_003E1__state = 4;
				return true;
				IL_0059:
				if (cS04_Gondola.gondolaState != GondolaStates.Stopped)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				cS04_Gondola.Level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				cS04_Gondola.moveLoopSfx.Stop();
				Audio.Play("event:/game/04_cliffside/gondola_finish", cS04_Gondola.gondola.Position);
				cS04_Gondola.gondola.RotationSpeed = 0.5f;
				_003C_003E2__current = 0.1f;
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
	private sealed class _003CTheoTakesOutPhone_003Ed__43 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CTheoTakesOutPhone_003Ed__43(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.player.Facing = Facings.Right;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Play("usePhone");
				_003C_003E2__current = 2f;
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
	private sealed class _003CFaceTheo_003Ed__44 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

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
		public _003CFaceTheo_003Ed__44(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.player.DummyAutoAnimate = true;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.player.Facing = Facings.Left;
				_003C_003E2__current = 0.2f;
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
	private sealed class _003CShowPhoto_003Ed__45 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

		private Selfie _003Cselfie_003E5__2;

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
		public _003CShowPhoto_003Ed__45(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Scale.X = -1f;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS04_Gondola.player.DummyWalkTo(cS04_Gondola.theo.X + 5f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003Cselfie_003E5__2 = new Selfie(cS04_Gondola.SceneAs<Level>());
				cS04_Gondola.Scene.Add(_003Cselfie_003E5__2);
				_003C_003E2__current = _003Cselfie_003E5__2.OpenRoutine("selfieGondola");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003Cselfie_003E5__2.WaitForInput();
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

	[CompilerGenerated]
	private sealed class _003CPanCamera_003Ed__48 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Ease.Easer ease;

		public CS04_Gondola _003C_003E4__this;

		public Vector2 to;

		public float duration;

		private Vector2 _003Cfrom_003E5__2;

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
		public _003CPanCamera_003Ed__48(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (ease == null)
				{
					ease = Ease.CubeInOut;
				}
				_003Cfrom_003E5__2 = cS04_Gondola.Level.Camera.Position;
				_003Ct_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				cS04_Gondola.Level.Camera.Position = _003Cfrom_003E5__2 + (to - _003Cfrom_003E5__2) * ease(Math.Min(_003Ct_003E5__3, 1f));
				_003Ct_003E5__3 += Engine.DeltaTime / duration;
				break;
			}
			if (_003Ct_003E5__3 < 1f)
			{
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

	[CompilerGenerated]
	private sealed class _003CMovePlayerOnGondola_003Ed__49 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

		public float x;

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
		public _003CMovePlayerOnGondola_003Ed__49(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.player.Sprite.Play("walk");
				cS04_Gondola.player.Facing = (Facings)Math.Sign(x - cS04_Gondola.playerXOffset);
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS04_Gondola.playerXOffset != x)
			{
				cS04_Gondola.playerXOffset = Calc.Approach(cS04_Gondola.playerXOffset, x, 48f * Engine.DeltaTime);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS04_Gondola.player.Sprite.Play("idle");
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
	private sealed class _003CMoveTheoOnGondola_003Ed__50 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS04_Gondola _003C_003E4__this;

		public bool changeFacing;

		public float x;

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
		public _003CMoveTheoOnGondola_003Ed__50(int _003C_003E1__state)
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
			CS04_Gondola cS04_Gondola = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS04_Gondola.theo.Sprite.Play("walk");
				if (changeFacing)
				{
					cS04_Gondola.theo.Sprite.Scale.X = Math.Sign(x - cS04_Gondola.theoXOffset);
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (cS04_Gondola.theoXOffset != x)
			{
				cS04_Gondola.theoXOffset = Calc.Approach(cS04_Gondola.theoXOffset, x, 48f * Engine.DeltaTime);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			cS04_Gondola.theo.Sprite.Play("idle");
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

	private NPC theo;

	private Gondola gondola;

	private Player player;

	private BadelineDummy evil;

	private Parallax loopingCloud;

	private Parallax bottomCloud;

	private WindSnowFG windSnowFg;

	private float LoopCloudsAt;

	private List<ReflectionTentacles> tentacles = new List<ReflectionTentacles>();

	private SoundSource moveLoopSfx;

	private SoundSource haltLoopSfx;

	private float gondolaPercent;

	private bool AutoSnapCharacters;

	private float theoXOffset;

	private float playerXOffset;

	private float gondolaSpeed;

	private float shakeTimer;

	private const float gondolaMaxSpeed = 64f;

	private float anxiety;

	private float anxietyStutter;

	private float anxietyRumble;

	private BreathingRumbler rumbler;

	private GondolaStates gondolaState;

	public CS04_Gondola(NPC theo, Gondola gondola, Player player)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.theo = theo;
		this.gondola = gondola;
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		level.RegisterAreaComplete();
		foreach (Backdrop backdrop in level.Foreground.Backdrops)
		{
			if (backdrop is WindSnowFG)
			{
				windSnowFg = backdrop as WindSnowFG;
			}
		}
		Add(moveLoopSfx = new SoundSource());
		Add(haltLoopSfx = new SoundSource());
		Add(new Coroutine(Cutscene()));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__26))]
	private IEnumerator Cutscene()
	{
		player.StateMachine.State = 11;
		yield return player.DummyWalkToExact((int)gondola.X + 16);
		while (!player.OnGround())
		{
			yield return null;
		}
		Audio.SetMusic("event:/music/lvl1/theo");
		yield return Textbox.Say("CH4_GONDOLA", EnterTheo, CheckOnTheo, GetUpTheo, LookAtLever, PullLever, WaitABit, WaitForCenter, SelfieThenStallsOut, MovePlayerLeft, SnapLeverOff, DarknessAppears, DarknessConsumes, CantBreath, StartBreathing, Ascend, WaitABit, TheoTakesOutPhone, FaceTheo);
		yield return ShowPhoto();
		EndCutscene(Level);
	}

	public override void OnEnd(Level level)
	{
		if (rumbler != null)
		{
			rumbler.RemoveSelf();
			rumbler = null;
		}
		level.CompleteArea();
		if (!WasSkipped)
		{
			SpotlightWipe.Modifier = 120f;
			SpotlightWipe.FocusPoint = new Vector2(320f, 180f) / 2f;
		}
	}

	[IteratorStateMachine(typeof(_003CEnterTheo_003Ed__28))]
	private IEnumerator EnterTheo()
	{
		player.Facing = Facings.Left;
		yield return 0.2f;
		yield return PanCamera(new Vector2(Level.Bounds.Left, theo.Y - 90f), 1f);
		theo.Visible = true;
		float theoStartX = theo.X;
		yield return theo.MoveTo(new Vector2(theoStartX + 35f, theo.Y));
		yield return 0.6f;
		yield return theo.MoveTo(new Vector2(theoStartX + 60f, theo.Y));
		Audio.Play("event:/game/04_cliffside/gondola_theo_fall", theo.Position);
		theo.Sprite.Play("idleEdge");
		yield return 1f;
		theo.Sprite.Play("falling");
		theo.X += 4f;
		theo.Depth = -10010;
		float speed = 80f;
		while (theo.Y < player.Y)
		{
			theo.Y += speed * Engine.DeltaTime;
			speed += 120f * Engine.DeltaTime;
			yield return null;
		}
		Level.DirectionalShake(new Vector2(0f, 1f));
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		theo.Y = player.Y;
		theo.Sprite.Play("hitGround");
		theo.Sprite.Rate = 0f;
		theo.Depth = 1000;
		theo.Sprite.Scale = new Vector2(1.3f, 0.8f);
		yield return 0.5f;
		Vector2 start = theo.Sprite.Scale;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 2f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			theo.Sprite.Scale.X = MathHelper.Lerp(start.X, 1f, t.Eased);
			theo.Sprite.Scale.Y = MathHelper.Lerp(start.Y, 1f, t.Eased);
		};
		Add(tween);
		yield return PanCamera(new Vector2(Level.Bounds.Left, theo.Y - 120f), 1f);
		yield return 0.6f;
	}

	[IteratorStateMachine(typeof(_003CCheckOnTheo_003Ed__29))]
	private IEnumerator CheckOnTheo()
	{
		yield return player.DummyWalkTo(gondola.X - 18f);
	}

	[IteratorStateMachine(typeof(_003CGetUpTheo_003Ed__30))]
	private IEnumerator GetUpTheo()
	{
		yield return 1.4f;
		Audio.Play("event:/game/04_cliffside/gondola_theo_recover", theo.Position);
		theo.Sprite.Rate = 1f;
		theo.Sprite.Play("recoverGround");
		yield return 1.6f;
		yield return theo.MoveTo(new Vector2(gondola.X - 50f, player.Y));
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CLookAtLever_003Ed__31))]
	private IEnumerator LookAtLever()
	{
		yield return theo.MoveTo(new Vector2(gondola.X + 7f, theo.Y));
		player.Facing = Facings.Right;
		theo.Sprite.Scale.X = -1f;
	}

	[IteratorStateMachine(typeof(_003CPullLever_003Ed__32))]
	private IEnumerator PullLever()
	{
		Add(new Coroutine(player.DummyWalkToExact((int)gondola.X - 7)));
		theo.Sprite.Scale.X = -1f;
		yield return 0.2f;
		Audio.Play("event:/game/04_cliffside/gondola_theo_lever_start", theo.Position);
		theo.Sprite.Play("pullVent");
		yield return 1f;
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		gondola.Lever.Play("pulled");
		theo.Sprite.Play("fallVent");
		yield return 0.6f;
		Level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		yield return 0.5f;
		yield return PanCamera(gondola.Position + new Vector2(-160f, -120f), 1f);
		yield return 0.5f;
		Level.Background.Backdrops.Add(loopingCloud = new Parallax(GFX.Game["bgs/04/bgCloudLoop"]));
		Level.Background.Backdrops.Add(bottomCloud = new Parallax(GFX.Game["bgs/04/bgCloud"]));
		Parallax parallax = loopingCloud;
		Parallax parallax2 = bottomCloud;
		bool loopX = true;
		parallax2.LoopX = true;
		parallax.LoopX = loopX;
		Parallax parallax3 = loopingCloud;
		Parallax parallax4 = bottomCloud;
		loopX = false;
		parallax4.LoopY = false;
		parallax3.LoopY = loopX;
		loopingCloud.Position.Y = Level.Camera.Top - (float)loopingCloud.Texture.Height - (float)bottomCloud.Texture.Height;
		bottomCloud.Position.Y = Level.Camera.Top - (float)bottomCloud.Texture.Height;
		LoopCloudsAt = bottomCloud.Position.Y;
		AutoSnapCharacters = true;
		theoXOffset = theo.X - gondola.X;
		playerXOffset = player.X - gondola.X;
		player.StateMachine.State = 17;
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, null, 16f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			if (Audio.CurrentMusic == "event:/music/lvl1/theo")
			{
				Audio.SetMusicParam("fade", 1f - t.Eased);
			}
		};
		Add(tween);
		SoundSource soundSource = new SoundSource();
		soundSource.Position = gondola.LeftCliffside.Position;
		soundSource.Play("event:/game/04_cliffside/gondola_cliffmechanism_start");
		Add(soundSource);
		moveLoopSfx.Play("event:/game/04_cliffside/gondola_movement_loop");
		Level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.FullSecond);
		gondolaSpeed = 32f;
		gondola.RotationSpeed = 1f;
		gondolaState = GondolaStates.MovingToCenter;
		yield return 1f;
		yield return MoveTheoOnGondola(12f, changeFacing: false);
		yield return 0.2f;
		theo.Sprite.Scale.X = -1f;
	}

	[IteratorStateMachine(typeof(_003CWaitABit_003Ed__33))]
	private IEnumerator WaitABit()
	{
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CWaitForCenter_003Ed__34))]
	private IEnumerator WaitForCenter()
	{
		while (gondolaState != GondolaStates.InCenter)
		{
			yield return null;
		}
		theo.Sprite.Scale.X = 1f;
		yield return 1f;
		yield return MovePlayerOnGondola(-20f);
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CSelfieThenStallsOut_003Ed__35))]
	private IEnumerator SelfieThenStallsOut()
	{
		Audio.SetMusic("event:/music/lvl4/minigame");
		Add(new Coroutine(Level.ZoomTo(new Vector2(160f, 110f), 2f, 0.5f)));
		yield return 0.3f;
		theo.Sprite.Scale.X = 1f;
		yield return 0.2f;
		Add(new Coroutine(MovePlayerOnGondola(theoXOffset - 8f)));
		yield return 0.4f;
		Audio.Play("event:/game/04_cliffside/gondola_theoselfie_halt", theo.Position);
		theo.Sprite.Play("holdOutPhone");
		yield return 1.5f;
		theoXOffset += 4f;
		playerXOffset += 4f;
		gondola.RotationSpeed = -1f;
		gondolaState = GondolaStates.Stopped;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		theo.Sprite.Play("takeSelfieImmediate");
		Add(new Coroutine(PanCamera(gondola.Position + (gondola.Destination - gondola.Position).SafeNormalize() * 32f + new Vector2(-160f, -120f), 0.3f, Ease.CubeOut)));
		yield return 0.5f;
		Level.Flash(Color.White);
		Level.Add(evil = new BadelineDummy(Vector2.Zero));
		evil.Appear(Level);
		evil.Floatness = 0f;
		evil.Depth = -1000000;
		moveLoopSfx.Stop();
		haltLoopSfx.Play("event:/game/04_cliffside/gondola_halted_loop");
		gondolaState = GondolaStates.Shaking;
		yield return PanCamera(gondola.Position + new Vector2(-160f, -120f), 1f);
		yield return 1f;
	}

	[IteratorStateMachine(typeof(_003CMovePlayerLeft_003Ed__36))]
	private IEnumerator MovePlayerLeft()
	{
		yield return MovePlayerOnGondola(-20f);
		theo.Sprite.Scale.X = -1f;
		yield return 0.5f;
		yield return MovePlayerOnGondola(20f);
		yield return 0.5f;
		yield return MovePlayerOnGondola(-10f);
		yield return 0.5f;
		player.Facing = Facings.Right;
	}

	[IteratorStateMachine(typeof(_003CSnapLeverOff_003Ed__37))]
	private IEnumerator SnapLeverOff()
	{
		yield return MoveTheoOnGondola(7f);
		Audio.Play("event:/game/04_cliffside/gondola_theo_lever_fail", theo.Position);
		theo.Sprite.Play("pullVent");
		yield return 1f;
		theo.Sprite.Play("fallVent");
		yield return 1f;
		gondola.BreakLever();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		Level.Shake();
		yield return 2.5f;
	}

	[IteratorStateMachine(typeof(_003CDarknessAppears_003Ed__38))]
	private IEnumerator DarknessAppears()
	{
		Audio.SetMusicParam("calm", 0f);
		yield return 0.25f;
		player.Sprite.Play("tired");
		yield return 0.25f;
		evil.Vanish();
		evil = null;
		yield return 0.3f;
		Level.NextColorGrade("panicattack");
		Level.Shake();
		Input.Rumble(RumbleStrength.Light, RumbleLength.Medium);
		BurstTentacles(3, 90f);
		Audio.Play("event:/game/04_cliffside/gondola_scaryhair_01", gondola.Position);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 2f)
		{
			yield return null;
			Level.Background.Fade = p;
			anxiety = p;
			if (windSnowFg != null)
			{
				windSnowFg.Alpha = 1f - p;
			}
		}
		yield return 0.25f;
	}

	[IteratorStateMachine(typeof(_003CDarknessConsumes_003Ed__39))]
	private IEnumerator DarknessConsumes()
	{
		Level.Shake();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		Audio.Play("event:/game/04_cliffside/gondola_scaryhair_02", gondola.Position);
		BurstTentacles(2, 60f);
		yield return MoveTheoOnGondola(0f);
		theo.Sprite.Play("comfortStart");
	}

	[IteratorStateMachine(typeof(_003CCantBreath_003Ed__40))]
	private IEnumerator CantBreath()
	{
		Level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		Audio.Play("event:/game/04_cliffside/gondola_scaryhair_03", gondola.Position);
		BurstTentacles(1, 30f);
		BurstTentacles(0, 0f, 100f);
		rumbler = new BreathingRumbler();
		base.Scene.Add(rumbler);
		yield return null;
	}

	[IteratorStateMachine(typeof(_003CStartBreathing_003Ed__41))]
	private IEnumerator StartBreathing()
	{
		BreathingMinigame breathing = new BreathingMinigame(winnable: true, rumbler);
		base.Scene.Add(breathing);
		while (!breathing.Completed)
		{
			yield return null;
		}
		foreach (ReflectionTentacles tentacle in tentacles)
		{
			tentacle.RemoveSelf();
		}
		anxiety = 0f;
		Level.Background.Fade = 0f;
		Level.SnapColorGrade(null);
		gondola.CancelPullSides();
		Level.ResetZoom();
		yield return 0.5f;
		Audio.Play("event:/game/04_cliffside/gondola_restart", gondola.Position);
		yield return 1f;
		moveLoopSfx.Play("event:/game/04_cliffside/gondola_movement_loop");
		haltLoopSfx.Stop();
		Level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		gondolaState = GondolaStates.InCenter;
		gondola.RotationSpeed = 0.5f;
		yield return 1.2f;
	}

	[IteratorStateMachine(typeof(_003CAscend_003Ed__42))]
	private IEnumerator Ascend()
	{
		gondolaState = GondolaStates.MovingToEnd;
		while (gondolaState != GondolaStates.Stopped)
		{
			yield return null;
		}
		Level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		moveLoopSfx.Stop();
		Audio.Play("event:/game/04_cliffside/gondola_finish", gondola.Position);
		gondola.RotationSpeed = 0.5f;
		yield return 0.1f;
		while (gondola.Rotation > 0f)
		{
			yield return null;
		}
		gondola.Rotation = (gondola.RotationSpeed = 0f);
		Level.Shake();
		AutoSnapCharacters = false;
		player.StateMachine.State = 11;
		player.Position = player.Position.Floor();
		while (player.CollideCheck<Solid>())
		{
			player.Y--;
		}
		theo.Position.Y = player.Position.Y;
		theo.Sprite.Play("comfortRecover");
		theo.Sprite.Scale.X = 1f;
		yield return player.DummyWalkTo(gondola.X + 80f);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("tired");
		yield return theo.MoveTo(new Vector2(gondola.X + 64f, theo.Y));
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CTheoTakesOutPhone_003Ed__43))]
	private IEnumerator TheoTakesOutPhone()
	{
		player.Facing = Facings.Right;
		yield return 0.25f;
		theo.Sprite.Play("usePhone");
		yield return 2f;
	}

	[IteratorStateMachine(typeof(_003CFaceTheo_003Ed__44))]
	private IEnumerator FaceTheo()
	{
		player.DummyAutoAnimate = true;
		yield return 0.2f;
		player.Facing = Facings.Left;
		yield return 0.2f;
	}

	[IteratorStateMachine(typeof(_003CShowPhoto_003Ed__45))]
	private IEnumerator ShowPhoto()
	{
		theo.Sprite.Scale.X = -1f;
		yield return 0.25f;
		yield return player.DummyWalkTo(theo.X + 5f);
		yield return 1f;
		Selfie selfie = new Selfie(SceneAs<Level>());
		base.Scene.Add(selfie);
		yield return selfie.OpenRoutine("selfieGondola");
		yield return selfie.WaitForInput();
	}

	public override void Update()
	{
		base.Update();
		if (anxietyRumble > 0f)
		{
			Input.RumbleSpecific(anxietyRumble, 0.1f);
		}
		if (base.Scene.OnInterval(0.05f))
		{
			anxietyStutter = Calc.Random.NextFloat(0.1f);
		}
		Distort.AnxietyOrigin = new Vector2(0.5f, 0.5f);
		Distort.Anxiety = anxiety * 0.2f + anxietyStutter * anxiety;
		if (moveLoopSfx != null && gondola != null)
		{
			moveLoopSfx.Position = gondola.Position;
		}
		if (haltLoopSfx != null && gondola != null)
		{
			haltLoopSfx.Position = gondola.Position;
		}
		if (gondolaState == GondolaStates.MovingToCenter)
		{
			MoveGondolaTowards(0.5f);
			if (gondolaPercent >= 0.5f)
			{
				gondolaState = GondolaStates.InCenter;
			}
		}
		else if (gondolaState == GondolaStates.InCenter)
		{
			Vector2 vector = (gondola.Destination - gondola.Position).SafeNormalize() * gondolaSpeed;
			loopingCloud.CameraOffset.X += vector.X * Engine.DeltaTime;
			loopingCloud.CameraOffset.Y += vector.Y * Engine.DeltaTime;
			windSnowFg.CameraOffset = loopingCloud.CameraOffset;
			loopingCloud.LoopY = true;
		}
		else if (gondolaState != GondolaStates.Stopped)
		{
			if (gondolaState == GondolaStates.Shaking)
			{
				Level.Wind.X = -400f;
				if (shakeTimer <= 0f && (gondola.Rotation == 0f || gondola.Rotation < -0.25f))
				{
					shakeTimer = 1f;
					gondola.RotationSpeed = 0.5f;
				}
				shakeTimer -= Engine.DeltaTime;
			}
			else if (gondolaState == GondolaStates.MovingToEnd)
			{
				MoveGondolaTowards(1f);
				if (gondolaPercent >= 1f)
				{
					gondolaState = GondolaStates.Stopped;
				}
			}
		}
		if (loopingCloud != null && !loopingCloud.LoopY && Level.Camera.Bottom < LoopCloudsAt)
		{
			loopingCloud.LoopY = true;
		}
		if (AutoSnapCharacters)
		{
			theo.Position = gondola.GetRotatedFloorPositionAt(theoXOffset);
			player.Position = gondola.GetRotatedFloorPositionAt(playerXOffset);
			if (evil != null)
			{
				evil.Position = gondola.GetRotatedFloorPositionAt(-24f, 20f);
			}
		}
	}

	private void MoveGondolaTowards(float percent)
	{
		float num = (gondola.Start - gondola.Destination).Length();
		gondolaSpeed = Calc.Approach(gondolaSpeed, 64f, 120f * Engine.DeltaTime);
		gondolaPercent = Calc.Approach(gondolaPercent, percent, gondolaSpeed / num * Engine.DeltaTime);
		gondola.Position = (gondola.Start + (gondola.Destination - gondola.Start) * gondolaPercent).Floor();
		Level.Camera.Position = gondola.Position + new Vector2(-160f, -120f);
	}

	[IteratorStateMachine(typeof(_003CPanCamera_003Ed__48))]
	private IEnumerator PanCamera(Vector2 to, float duration, Ease.Easer ease = null)
	{
		if (ease == null)
		{
			ease = Ease.CubeInOut;
		}
		Vector2 from = Level.Camera.Position;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime / duration)
		{
			yield return null;
			Level.Camera.Position = from + (to - from) * ease(Math.Min(t, 1f));
		}
	}

	[IteratorStateMachine(typeof(_003CMovePlayerOnGondola_003Ed__49))]
	private IEnumerator MovePlayerOnGondola(float x)
	{
		player.Sprite.Play("walk");
		player.Facing = (Facings)Math.Sign(x - playerXOffset);
		while (playerXOffset != x)
		{
			playerXOffset = Calc.Approach(playerXOffset, x, 48f * Engine.DeltaTime);
			yield return null;
		}
		player.Sprite.Play("idle");
	}

	[IteratorStateMachine(typeof(_003CMoveTheoOnGondola_003Ed__50))]
	private IEnumerator MoveTheoOnGondola(float x, bool changeFacing = true)
	{
		theo.Sprite.Play("walk");
		if (changeFacing)
		{
			theo.Sprite.Scale.X = Math.Sign(x - theoXOffset);
		}
		while (theoXOffset != x)
		{
			theoXOffset = Calc.Approach(theoXOffset, x, 48f * Engine.DeltaTime);
			yield return null;
		}
		theo.Sprite.Play("idle");
	}

	private void BurstTentacles(int layer, float dist, float from = 200f)
	{
		Vector2 vector = Level.Camera.Position + new Vector2(160f, 90f);
		ReflectionTentacles reflectionTentacles = new ReflectionTentacles();
		reflectionTentacles.Create(0f, 0, layer, new List<Vector2>
		{
			vector + new Vector2(0f - from, 0f),
			vector + new Vector2(-800f, 0f)
		});
		reflectionTentacles.SnapTentacles();
		reflectionTentacles.Nodes[0] = vector + new Vector2(0f - dist, 0f);
		ReflectionTentacles reflectionTentacles2 = new ReflectionTentacles();
		reflectionTentacles2.Create(0f, 0, layer, new List<Vector2>
		{
			vector + new Vector2(from, 0f),
			vector + new Vector2(800f, 0f)
		});
		reflectionTentacles2.SnapTentacles();
		reflectionTentacles2.Nodes[0] = vector + new Vector2(dist, 0f);
		tentacles.Add(reflectionTentacles);
		tentacles.Add(reflectionTentacles2);
		Level.Add(reflectionTentacles);
		Level.Add(reflectionTentacles2);
	}
}
