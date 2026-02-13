using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS03_OshiroBreakdown : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

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
		public _003CCutscene_003Ed__9(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				cS03_OshiroBreakdown.player.StateMachine.State = 11;
				cS03_OshiroBreakdown.player.StateMachine.Locked = true;
				cS03_OshiroBreakdown.Add(new Coroutine(cS03_OshiroBreakdown.player.DummyWalkTo(cS03_OshiroBreakdown.player.X - 64f)));
				List<DustStaticSpinner> list = level.Entities.FindAll<DustStaticSpinner>();
				list.Shuffle();
				foreach (DustStaticSpinner item in list)
				{
					if ((item.Position - cS03_OshiroBreakdown.oshiro.Position).Length() < 128f)
					{
						cS03_OshiroBreakdown.creatures.Add(item);
						cS03_OshiroBreakdown.creatureHomes.Add(item.Position);
						item.Visible = false;
					}
				}
				_003C_003E2__current = cS03_OshiroBreakdown.PanCamera(level.Bounds.Left);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroBreakdown.Level.ZoomTo(new Vector2(100f, 120f), 2f, 0.5f);
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH3_OSHIRO_BREAKDOWN", cS03_OshiroBreakdown.WalkLeft, cS03_OshiroBreakdown.WalkRight, cS03_OshiroBreakdown.CreateDustA, cS03_OshiroBreakdown.CreateDustB);
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				cS03_OshiroBreakdown.Add(new Coroutine(cS03_OshiroBreakdown.oshiro.MoveTo(new Vector2(level.Bounds.Left - 64, cS03_OshiroBreakdown.oshiro.Y))));
				cS03_OshiroBreakdown.oshiro.Add(new SoundSource("event:/char/oshiro/move_06_04d_exit"));
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroBreakdown.PanCamera(cS03_OshiroBreakdown.player.CameraTarget.X);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS03_OshiroBreakdown.EndCutscene(level);
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
	private sealed class _003CPanCamera_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

		public float to;

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
		public _003CPanCamera_003Ed__10(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = cS03_OshiroBreakdown.Level.Camera.X;
				_003Cp_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__3 += Engine.DeltaTime;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
			{
				cS03_OshiroBreakdown.Level.Camera.X = _003Cfrom_003E5__2 + (to - _003Cfrom_003E5__2) * Ease.CubeInOut(_003Cp_003E5__3);
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
	private sealed class _003CWalkLeft_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

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
		public _003CWalkLeft_003Ed__11(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				(cS03_OshiroBreakdown.oshiro.Sprite as OshiroSprite).AllowSpriteChanges = false;
				_003C_003E2__current = cS03_OshiroBreakdown.oshiro.MoveTo(cS03_OshiroBreakdown.origin + new Vector2(-24f, 0f));
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				(cS03_OshiroBreakdown.oshiro.Sprite as OshiroSprite).AllowSpriteChanges = true;
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
	private sealed class _003CWalkRight_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

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
		public _003CWalkRight_003Ed__12(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				(cS03_OshiroBreakdown.oshiro.Sprite as OshiroSprite).AllowSpriteChanges = false;
				_003C_003E2__current = cS03_OshiroBreakdown.oshiro.MoveTo(cS03_OshiroBreakdown.origin + new Vector2(0f, 0f));
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				(cS03_OshiroBreakdown.oshiro.Sprite as OshiroSprite).AllowSpriteChanges = true;
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
	private sealed class _003CCreateDustA_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

		private int _003Ci_003E5__2;

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
		public _003CCreateDustA_003Ed__14(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_OshiroBreakdown.Add(new SoundSource(cS03_OshiroBreakdown.oshiro.Position, "event:/game/03_resort/sequence_oshirofluff_pt1"));
				(cS03_OshiroBreakdown.oshiro.Sprite as OshiroSprite).AllowSpriteChanges = false;
				cS03_OshiroBreakdown.oshiro.Sprite.Play("fall");
				Audio.Play("event:/char/oshiro/chat_collapse", cS03_OshiroBreakdown.oshiro.Position);
				Distort.AnxietyOrigin = new Vector2(0.5f, 0.5f);
				_003Ci_003E5__2 = 0;
				goto IL_0186;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0176;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0176;
			case 3:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0176:
				_003Ci_003E5__2++;
				goto IL_0186;
				IL_0186:
				if (_003Ci_003E5__2 < 4)
				{
					cS03_OshiroBreakdown.Add(new Coroutine(cS03_OshiroBreakdown.MoveDust(cS03_OshiroBreakdown.creatures[_003Ci_003E5__2], cS03_OshiroBreakdown.creatureHomes[_003Ci_003E5__2])));
					Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
					if (_003Ci_003E5__2 % 4 == 0)
					{
						Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
						cS03_OshiroBreakdown.Level.Shake();
						Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
						_003C_003E2__current = 0.4f;
						_003C_003E1__state = 1;
						return true;
					}
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 0.5f;
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
	private sealed class _003CCreateDustB_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

		private int _003Ci_003E5__2;

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
		public _003CCreateDustB_003Ed__15(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_OshiroBreakdown.Add(new SoundSource(cS03_OshiroBreakdown.oshiro.Position, "event:/game/03_resort/sequence_oshirofluff_pt2"));
				_003Ci_003E5__2 = 4;
				goto IL_0149;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0139;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0139;
			case 3:
				_003C_003E1__state = -1;
				goto IL_01a8;
			case 4:
				_003C_003E1__state = -1;
				goto IL_01a8;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_OshiroBreakdown.player.DummyWalkToExact(cS03_OshiroBreakdown.Level.Bounds.Left + 200);
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1f;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				Audio.Play("event:/char/oshiro/chat_get_up", cS03_OshiroBreakdown.oshiro.Position);
				cS03_OshiroBreakdown.oshiro.Sprite.Play("recover");
				_003C_003E2__current = 0.7f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS03_OshiroBreakdown.oshiro.Sprite.Scale.X = 1f;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_01a8:
				if (Distort.Anxiety > 0f)
				{
					Distort.Anxiety -= Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003C_003E2__current = cS03_OshiroBreakdown.Level.ZoomBack(0.5f);
				_003C_003E1__state = 5;
				return true;
				IL_0149:
				if (_003Ci_003E5__2 < cS03_OshiroBreakdown.creatures.Count)
				{
					cS03_OshiroBreakdown.Add(new Coroutine(cS03_OshiroBreakdown.MoveDust(cS03_OshiroBreakdown.creatures[_003Ci_003E5__2], cS03_OshiroBreakdown.creatureHomes[_003Ci_003E5__2])));
					Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
					cS03_OshiroBreakdown.Level.Shake();
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					if ((_003Ci_003E5__2 - 4) % 4 == 0)
					{
						Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
						_003C_003E2__current = 0.4f;
						_003C_003E1__state = 1;
						return true;
					}
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 2;
					return true;
				}
				_003C_003E2__current = 1f;
				_003C_003E1__state = 3;
				return true;
				IL_0139:
				_003Ci_003E5__2++;
				goto IL_0149;
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
	private sealed class _003CMoveDust_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_OshiroBreakdown _003C_003E4__this;

		public Vector2 to;

		public DustStaticSpinner creature;

		private SimpleCurve _003Ccurve_003E5__2;

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
		public _003CMoveDust_003Ed__16(int _003C_003E1__state)
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
			CS03_OshiroBreakdown cS03_OshiroBreakdown = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Vector2 vector = cS03_OshiroBreakdown.oshiro.Position + new Vector2(0f, -12f);
				_003Ccurve_003E5__2 = new SimpleCurve(vector, to, (to + vector) / 2f + Vector2.UnitY * (-30f + Calc.Random.NextFloat(60f)));
				_003Cp_003E5__3 = 0f;
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				creature.Sprite.Scale = 0.5f + _003Cp_003E5__3 * 0.5f;
				creature.Position = _003Ccurve_003E5__2.GetPoint(Ease.CubeOut(_003Cp_003E5__3));
				creature.Visible = true;
				if (cS03_OshiroBreakdown.Scene.OnInterval(0.02f))
				{
					cS03_OshiroBreakdown.SceneAs<Level>().ParticlesBG.Emit(DustStaticSpinner.P_Move, 1, creature.Position, Vector2.One * 4f);
				}
				_003Cp_003E5__3 += Engine.DeltaTime;
				break;
			}
			if (_003Cp_003E5__3 < 1f)
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

	public const string Flag = "oshiro_breakdown";

	private const int PlayerWalkTo = 200;

	private List<DustStaticSpinner> creatures = new List<DustStaticSpinner>();

	private List<Vector2> creatureHomes = new List<Vector2>();

	private NPC oshiro;

	private Player player;

	private Vector2 origin;

	private const int DustAmountA = 4;

	public CS03_OshiroBreakdown(Player player, NPC oshiro)
	{
		this.oshiro = oshiro;
		this.player = player;
		origin = oshiro.Position;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__9))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		Add(new Coroutine(player.DummyWalkTo(player.X - 64f)));
		List<DustStaticSpinner> list = level.Entities.FindAll<DustStaticSpinner>();
		list.Shuffle();
		foreach (DustStaticSpinner item in list)
		{
			if ((item.Position - oshiro.Position).Length() < 128f)
			{
				creatures.Add(item);
				creatureHomes.Add(item.Position);
				item.Visible = false;
			}
		}
		yield return PanCamera(level.Bounds.Left);
		yield return 0.2f;
		yield return Level.ZoomTo(new Vector2(100f, 120f), 2f, 0.5f);
		yield return Textbox.Say("CH3_OSHIRO_BREAKDOWN", WalkLeft, WalkRight, CreateDustA, CreateDustB);
		Add(new Coroutine(oshiro.MoveTo(new Vector2(level.Bounds.Left - 64, oshiro.Y))));
		oshiro.Add(new SoundSource("event:/char/oshiro/move_06_04d_exit"));
		yield return 0.25f;
		yield return PanCamera(player.CameraTarget.X);
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CPanCamera_003Ed__10))]
	private IEnumerator PanCamera(float to)
	{
		float from = Level.Camera.X;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			Level.Camera.X = from + (to - from) * Ease.CubeInOut(p);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CWalkLeft_003Ed__11))]
	private IEnumerator WalkLeft()
	{
		(oshiro.Sprite as OshiroSprite).AllowSpriteChanges = false;
		yield return oshiro.MoveTo(origin + new Vector2(-24f, 0f));
		(oshiro.Sprite as OshiroSprite).AllowSpriteChanges = true;
	}

	[IteratorStateMachine(typeof(_003CWalkRight_003Ed__12))]
	private IEnumerator WalkRight()
	{
		(oshiro.Sprite as OshiroSprite).AllowSpriteChanges = false;
		yield return oshiro.MoveTo(origin + new Vector2(0f, 0f));
		(oshiro.Sprite as OshiroSprite).AllowSpriteChanges = true;
	}

	[IteratorStateMachine(typeof(_003CCreateDustA_003Ed__14))]
	private IEnumerator CreateDustA()
	{
		Add(new SoundSource(oshiro.Position, "event:/game/03_resort/sequence_oshirofluff_pt1"));
		(oshiro.Sprite as OshiroSprite).AllowSpriteChanges = false;
		oshiro.Sprite.Play("fall");
		Audio.Play("event:/char/oshiro/chat_collapse", oshiro.Position);
		Distort.AnxietyOrigin = new Vector2(0.5f, 0.5f);
		for (int i = 0; i < 4; i++)
		{
			Add(new Coroutine(MoveDust(creatures[i], creatureHomes[i])));
			Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
			if (i % 4 == 0)
			{
				Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
				Level.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				yield return 0.4f;
			}
			else
			{
				yield return 0.1f;
			}
		}
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CCreateDustB_003Ed__15))]
	private IEnumerator CreateDustB()
	{
		Add(new SoundSource(oshiro.Position, "event:/game/03_resort/sequence_oshirofluff_pt2"));
		for (int i = 4; i < creatures.Count; i++)
		{
			Add(new Coroutine(MoveDust(creatures[i], creatureHomes[i])));
			Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
			Level.Shake();
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			if ((i - 4) % 4 == 0)
			{
				Distort.Anxiety = 0.1f + Calc.Random.NextFloat(0.1f);
				yield return 0.4f;
			}
			else
			{
				yield return 0.1f;
			}
		}
		yield return 1f;
		while (Distort.Anxiety > 0f)
		{
			Distort.Anxiety -= Engine.DeltaTime;
			yield return null;
		}
		yield return Level.ZoomBack(0.5f);
		yield return player.DummyWalkToExact(Level.Bounds.Left + 200);
		yield return 1f;
		Audio.Play("event:/char/oshiro/chat_get_up", oshiro.Position);
		oshiro.Sprite.Play("recover");
		yield return 0.7f;
		oshiro.Sprite.Scale.X = 1f;
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CMoveDust_003Ed__16))]
	private IEnumerator MoveDust(DustStaticSpinner creature, Vector2 to)
	{
		Vector2 vector = oshiro.Position + new Vector2(0f, -12f);
		SimpleCurve curve = new SimpleCurve(vector, to, (to + vector) / 2f + Vector2.UnitY * (-30f + Calc.Random.NextFloat(60f)));
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			yield return null;
			creature.Sprite.Scale = 0.5f + p * 0.5f;
			creature.Position = curve.GetPoint(Ease.CubeOut(p));
			creature.Visible = true;
			if (base.Scene.OnInterval(0.02f))
			{
				SceneAs<Level>().ParticlesBG.Emit(DustStaticSpinner.P_Move, 1, creature.Position, Vector2.One * 4f);
			}
		}
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		if (WasSkipped)
		{
			player.X = level.Bounds.Left + 200;
			while (!player.OnGround())
			{
				player.Y++;
			}
			for (int i = 0; i < creatures.Count; i++)
			{
				creatures[i].ForceInstantiate();
				creatures[i].Visible = true;
				creatures[i].Position = creatureHomes[i];
			}
		}
		level.Camera.Position = player.CameraTarget;
		level.Remove(oshiro);
		level.Session.SetFlag("oshiro_breakdown");
	}
}
