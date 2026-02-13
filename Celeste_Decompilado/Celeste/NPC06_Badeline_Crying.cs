using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC06_Badeline_Crying : NPC
{
	private class Orb : Entity
	{
		[CompilerGenerated]
		private sealed class _003CFloatRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Orb _003C_003E4__this;

			private Vector2 _003Cspeed_003E5__2;

			private Vector2 _003Ctarget_003E5__3;

			private float _003Creset_003E5__4;

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
			public _003CFloatRoutine_003Ed__9(int _003C_003E1__state)
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
				Orb orb = _003C_003E4__this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					_003C_003E1__state = -1;
					goto IL_0151;
				}
				_003C_003E1__state = -1;
				_003Cspeed_003E5__2 = Vector2.Zero;
				orb.Ease = 0.2f;
				goto IL_0037;
				IL_0151:
				if (_003Creset_003E5__4 < 1f && (_003Ctarget_003E5__3 - orb.Position).Length() > 8f)
				{
					Vector2 vector = (_003Ctarget_003E5__3 - orb.Position).SafeNormalize();
					_003Cspeed_003E5__2 += vector * 420f * Engine.DeltaTime;
					if (_003Cspeed_003E5__2.Length() > 90f)
					{
						_003Cspeed_003E5__2 = _003Cspeed_003E5__2.SafeNormalize(90f);
					}
					orb.Position += _003Cspeed_003E5__2 * Engine.DeltaTime;
					_003Creset_003E5__4 += Engine.DeltaTime;
					orb.Ease = Calc.Approach(orb.Ease, 1f, Engine.DeltaTime * 4f);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				_003Ctarget_003E5__3 = default(Vector2);
				goto IL_0037;
				IL_0037:
				_003Ctarget_003E5__3 = orb.Target + Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 16f + Calc.Random.NextFloat(40f));
				_003Creset_003E5__4 = 0f;
				goto IL_0151;
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
		private sealed class _003CCircleRoutine_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Orb _003C_003E4__this;

			public float offset;

			private Vector2 _003Cfrom_003E5__2;

			private float _003Cease_003E5__3;

			private Player _003Cplayer_003E5__4;

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
			public _003CCircleRoutine_003Ed__10(int _003C_003E1__state)
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
				Orb orb = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Cfrom_003E5__2 = orb.Position;
					_003Cease_003E5__3 = 0f;
					_003Cplayer_003E5__4 = orb.Scene.Tracker.GetEntity<Player>();
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				}
				if (_003Cplayer_003E5__4 != null)
				{
					float angleRadians = orb.Scene.TimeActive * 2f + offset;
					Vector2 vector = _003Cplayer_003E5__4.Center + Calc.AngleToVector(angleRadians, 24f);
					_003Cease_003E5__3 = Calc.Approach(_003Cease_003E5__3, 1f, Engine.DeltaTime * 2f);
					orb.Position = _003Cfrom_003E5__2 + (vector - _003Cfrom_003E5__2) * Monocle.Ease.CubeInOut(_003Cease_003E5__3);
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
		private sealed class _003CAbsorbRoutine_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Orb _003C_003E4__this;

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
			public _003CAbsorbRoutine_003Ed__11(int _003C_003E1__state)
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
				Orb orb = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
				{
					_003C_003E1__state = -1;
					Player entity = orb.Scene.Tracker.GetEntity<Player>();
					if (entity == null)
					{
						return false;
					}
					_003Cfrom_003E5__2 = orb.Position;
					_003Cto_003E5__3 = entity.Center;
					_003Cp_003E5__4 = 0f;
					break;
				}
				case 1:
					_003C_003E1__state = -1;
					_003Cp_003E5__4 += Engine.DeltaTime;
					break;
				}
				if (_003Cp_003E5__4 < 1f)
				{
					float num2 = Monocle.Ease.BigBackIn(_003Cp_003E5__4);
					orb.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * num2;
					orb.Ease = 0.2f + (1f - num2) * 0.8f;
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

		public Image Sprite;

		public BloomPoint Bloom;

		private float ease;

		public Vector2 Target;

		public Coroutine Routine;

		public float Ease
		{
			get
			{
				return ease;
			}
			set
			{
				ease = value;
				Sprite.Scale = Vector2.One * ease;
				Bloom.Alpha = ease;
			}
		}

		public Orb(Vector2 position)
			: base(position)
		{
			Add(Sprite = new Image(GFX.Game["characters/badeline/orb"]));
			Add(Bloom = new BloomPoint(0f, 32f));
			Add(Routine = new Coroutine(FloatRoutine()));
			Sprite.CenterOrigin();
			base.Depth = -10001;
		}

		[IteratorStateMachine(typeof(_003CFloatRoutine_003Ed__9))]
		public IEnumerator FloatRoutine()
		{
			Vector2 speed = Vector2.Zero;
			Ease = 0.2f;
			while (true)
			{
				Vector2 target = Target + Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 16f + Calc.Random.NextFloat(40f));
				float reset = 0f;
				while (reset < 1f && (target - Position).Length() > 8f)
				{
					Vector2 vector = (target - Position).SafeNormalize();
					speed += vector * 420f * Engine.DeltaTime;
					if (speed.Length() > 90f)
					{
						speed = speed.SafeNormalize(90f);
					}
					Position += speed * Engine.DeltaTime;
					reset += Engine.DeltaTime;
					Ease = Calc.Approach(Ease, 1f, Engine.DeltaTime * 4f);
					yield return null;
				}
			}
		}

		[IteratorStateMachine(typeof(_003CCircleRoutine_003Ed__10))]
		public IEnumerator CircleRoutine(float offset)
		{
			Vector2 from = Position;
			float ease = 0f;
			Player player = base.Scene.Tracker.GetEntity<Player>();
			while (player != null)
			{
				float angleRadians = base.Scene.TimeActive * 2f + offset;
				Vector2 vector = player.Center + Calc.AngleToVector(angleRadians, 24f);
				ease = Calc.Approach(ease, 1f, Engine.DeltaTime * 2f);
				Position = from + (vector - from) * Monocle.Ease.CubeInOut(ease);
				yield return null;
			}
		}

		[IteratorStateMachine(typeof(_003CAbsorbRoutine_003Ed__11))]
		public IEnumerator AbsorbRoutine()
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				Vector2 from = Position;
				Vector2 to = entity.Center;
				for (float p = 0f; p < 1f; p += Engine.DeltaTime)
				{
					float num = Monocle.Ease.BigBackIn(p);
					Position = from + (to - from) * num;
					Ease = 0.2f + (1f - num) * 0.8f;
					yield return null;
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CTurnWhite_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float duration;

		public NPC06_Badeline_Crying _003C_003E4__this;

		private float _003Calpha_003E5__2;

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
		public _003CTurnWhite_003Ed__10(int _003C_003E1__state)
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
			NPC06_Badeline_Crying nPC06_Badeline_Crying = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Calpha_003E5__2 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Calpha_003E5__2 < 1f)
			{
				_003Calpha_003E5__2 += Engine.DeltaTime / duration;
				nPC06_Badeline_Crying.white.Color = Color.White * _003Calpha_003E5__2;
				nPC06_Badeline_Crying.bloom.Alpha = _003Calpha_003E5__2;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			nPC06_Badeline_Crying.Sprite.Visible = false;
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
	private sealed class _003CDisperse_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC06_Badeline_Crying _003C_003E4__this;

		private float _003Csize_003E5__2;

		private int _003Ci_003E5__3;

		private float _003Cto_003E5__4;

		private List<Orb>.Enumerator _003C_003E7__wrap4;

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
		public _003CDisperse_003Ed__11(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 3)
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
				NPC06_Badeline_Crying nPC06_Badeline_Crying = _003C_003E4__this;
				Orb orb;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
					_003Csize_003E5__2 = 1f;
					goto IL_0122;
				case 1:
					_003C_003E1__state = -1;
					goto IL_00d0;
				case 2:
					_003C_003E1__state = -1;
					_003Ci_003E5__3 = 0;
					_003C_003E7__wrap4 = nPC06_Badeline_Crying.orbs.GetEnumerator();
					_003C_003E1__state = -3;
					goto IL_01e5;
				case 3:
					_003C_003E1__state = -3;
					goto IL_01e5;
				case 4:
					_003C_003E1__state = -1;
					foreach (Orb orb2 in nPC06_Badeline_Crying.orbs)
					{
						orb2.Routine.Replace(orb2.AbsorbRoutine());
					}
					_003C_003E2__current = 1f;
					_003C_003E1__state = 5;
					return true;
				case 5:
					{
						_003C_003E1__state = -1;
						return false;
					}
					IL_0122:
					if (nPC06_Badeline_Crying.orbs.Count < 8)
					{
						_003Cto_003E5__4 = _003Csize_003E5__2 - 0.125f;
						goto IL_00d0;
					}
					_003C_003E2__current = 3.25f;
					_003C_003E1__state = 2;
					return true;
					IL_01e5:
					if (_003C_003E7__wrap4.MoveNext())
					{
						Orb current2 = _003C_003E7__wrap4.Current;
						current2.Routine.Replace(current2.CircleRoutine((float)_003Ci_003E5__3 / 8f * ((float)Math.PI * 2f)));
						_003Ci_003E5__3++;
						_003C_003E2__current = 0.2f;
						_003C_003E1__state = 3;
						return true;
					}
					_003C_003Em__Finally1();
					_003C_003E7__wrap4 = default(List<Orb>.Enumerator);
					_003C_003E2__current = 2f;
					_003C_003E1__state = 4;
					return true;
					IL_00d0:
					if (_003Csize_003E5__2 > _003Cto_003E5__4)
					{
						nPC06_Badeline_Crying.white.Scale = Vector2.One * _003Csize_003E5__2;
						nPC06_Badeline_Crying.light.Alpha = _003Csize_003E5__2;
						nPC06_Badeline_Crying.bloom.Alpha = _003Csize_003E5__2;
						_003Csize_003E5__2 -= Engine.DeltaTime;
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					orb = new Orb(nPC06_Badeline_Crying.Position)
					{
						Target = nPC06_Badeline_Crying.Position + new Vector2(-16f, -40f)
					};
					nPC06_Badeline_Crying.Scene.Add(orb);
					nPC06_Badeline_Crying.orbs.Add(orb);
					goto IL_0122;
				}
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
			((IDisposable)_003C_003E7__wrap4/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private bool started;

	private Image white;

	private BloomPoint bloom;

	private VertexLight light;

	public SoundSource LoopingSfx;

	private List<Orb> orbs = new List<Orb>();

	public NPC06_Badeline_Crying(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(Sprite = GFX.SpriteBank.Create("badeline_boss"));
		Sprite.Play("scaredIdle");
		Add(white = new Image(GFX.Game["characters/badelineBoss/calm_white"]));
		white.Color = Color.White * 0f;
		white.Origin = Sprite.Origin;
		white.Position = Sprite.Position;
		Add(bloom = new BloomPoint(new Vector2(0f, -6f), 0f, 16f));
		Add(light = new VertexLight(new Vector2(0f, -6f), Color.White, 1f, 24, 64));
		Add(LoopingSfx = new SoundSource("event:/char/badeline/boss_idle_ground"));
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (!base.Session.GetFlag("badeline_connection"))
		{
			return;
		}
		FinalBossStarfield finalBossStarfield = (scene as Level).Background.Get<FinalBossStarfield>();
		if (finalBossStarfield != null)
		{
			finalBossStarfield.Alpha = 0f;
		}
		foreach (Entity entity in base.Scene.Tracker.GetEntities<ReflectionTentacles>())
		{
			entity.RemoveSelf();
		}
		RemoveSelf();
	}

	public override void Update()
	{
		base.Update();
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (!started && entity != null && entity.X > base.X - 32f)
		{
			base.Scene.Add(new CS06_BossEnd(entity, this));
			started = true;
		}
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		foreach (Orb orb in orbs)
		{
			orb.RemoveSelf();
		}
	}

	[IteratorStateMachine(typeof(_003CTurnWhite_003Ed__10))]
	public IEnumerator TurnWhite(float duration)
	{
		float alpha = 0f;
		while (alpha < 1f)
		{
			alpha += Engine.DeltaTime / duration;
			white.Color = Color.White * alpha;
			bloom.Alpha = alpha;
			yield return null;
		}
		Sprite.Visible = false;
	}

	[IteratorStateMachine(typeof(_003CDisperse_003Ed__11))]
	public IEnumerator Disperse()
	{
		Input.Rumble(RumbleStrength.Light, RumbleLength.Long);
		float size = 1f;
		while (orbs.Count < 8)
		{
			float to = size - 0.125f;
			while (size > to)
			{
				white.Scale = Vector2.One * size;
				light.Alpha = size;
				bloom.Alpha = size;
				size -= Engine.DeltaTime;
				yield return null;
			}
			Orb orb = new Orb(Position);
			orb.Target = Position + new Vector2(-16f, -40f);
			base.Scene.Add(orb);
			orbs.Add(orb);
		}
		yield return 3.25f;
		int i = 0;
		foreach (Orb orb2 in orbs)
		{
			orb2.Routine.Replace(orb2.CircleRoutine((float)i / 8f * ((float)Math.PI * 2f)));
			i++;
			yield return 0.2f;
		}
		yield return 2f;
		foreach (Orb orb3 in orbs)
		{
			orb3.Routine.Replace(orb3.AbsorbRoutine());
		}
		yield return 1f;
	}
}
