using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class FinalBossMovingBlock : Solid
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public Vector2 from;

		public Vector2 to;

		public FinalBossMovingBlock _003C_003E4__this;

		internal void _003CMoveSequence_003Eb__0(Tween t)
		{
			_003C_003E4__this.MoveTo(Vector2.Lerp(from, to, t.Eased));
		}

		internal void _003CMoveSequence_003Eb__1(Tween t)
		{
			if (_003C_003E4__this.CollideCheck<SolidTiles>(_003C_003E4__this.Position + (to - from).SafeNormalize() * 2f))
			{
				Audio.Play("event:/game/06_reflection/fallblock_boss_impact", _003C_003E4__this.Center);
				_003C_003E4__this.ImpactParticles(to - from);
			}
			else
			{
				_003C_003E4__this.StopParticles(to - from);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CMoveSequence_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FinalBossMovingBlock _003C_003E4__this;

		private _003C_003Ec__DisplayClass14_0 _003C_003E8__1;

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
		public _003CMoveSequence_003Ed__14(int _003C_003E1__state)
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
			FinalBossMovingBlock finalBossMovingBlock = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_002d;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 += Engine.DeltaTime / (0.2f + finalBossMovingBlock.startDelay + 0.2f);
				goto IL_00e1;
			case 2:
				_003C_003E1__state = -1;
				break;
			case 3:
				{
					_003C_003E1__state = -1;
					_003C_003E8__1 = null;
					goto IL_002d;
				}
				IL_002d:
				_003C_003E8__1 = new _003C_003Ec__DisplayClass14_0();
				_003C_003E8__1._003C_003E4__this = finalBossMovingBlock;
				finalBossMovingBlock.StartShaking(0.2f + finalBossMovingBlock.startDelay);
				if (!finalBossMovingBlock.isHighlighted)
				{
					_003Cp_003E5__2 = 0f;
					goto IL_00e1;
				}
				_003C_003E2__current = 0.2f + finalBossMovingBlock.startDelay + 0.2f;
				_003C_003E1__state = 2;
				return true;
				IL_00e1:
				if (_003Cp_003E5__2 < 1f)
				{
					finalBossMovingBlock.highlight.Alpha = Ease.CubeIn(_003Cp_003E5__2);
					finalBossMovingBlock.sprite.Alpha = 1f - finalBossMovingBlock.highlight.Alpha;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				finalBossMovingBlock.highlight.Alpha = 1f;
				finalBossMovingBlock.sprite.Alpha = 0f;
				finalBossMovingBlock.isHighlighted = true;
				break;
			}
			finalBossMovingBlock.startDelay = 0f;
			finalBossMovingBlock.nodeIndex++;
			finalBossMovingBlock.nodeIndex %= finalBossMovingBlock.nodes.Length;
			_003C_003E8__1.from = finalBossMovingBlock.Position;
			_003C_003E8__1.to = finalBossMovingBlock.nodes[finalBossMovingBlock.nodeIndex];
			Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 0.8f, start: true);
			tween.OnUpdate = delegate(Tween t)
			{
				_003C_003E8__1._003C_003E4__this.MoveTo(Vector2.Lerp(_003C_003E8__1.from, _003C_003E8__1.to, t.Eased));
			};
			tween.OnComplete = delegate
			{
				if (_003C_003E8__1._003C_003E4__this.CollideCheck<SolidTiles>(_003C_003E8__1._003C_003E4__this.Position + (_003C_003E8__1.to - _003C_003E8__1.from).SafeNormalize() * 2f))
				{
					Audio.Play("event:/game/06_reflection/fallblock_boss_impact", _003C_003E8__1._003C_003E4__this.Center);
					_003C_003E8__1._003C_003E4__this.ImpactParticles(_003C_003E8__1.to - _003C_003E8__1.from);
				}
				else
				{
					_003C_003E8__1._003C_003E4__this.StopParticles(_003C_003E8__1.to - _003C_003E8__1.from);
				}
			};
			finalBossMovingBlock.Add(tween);
			_003C_003E2__current = 0.8f;
			_003C_003E1__state = 3;
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

	public static ParticleType P_Stop;

	public static ParticleType P_Break;

	public int BossNodeIndex;

	private float startDelay;

	private int nodeIndex;

	private Vector2[] nodes;

	private TileGrid sprite;

	private TileGrid highlight;

	private Coroutine moveCoroutine;

	private bool isHighlighted;

	public FinalBossMovingBlock(Vector2[] nodes, float width, float height, int bossNodeIndex)
		: base(nodes[0], width, height, safe: false)
	{
		BossNodeIndex = bossNodeIndex;
		this.nodes = nodes;
		int newSeed = Calc.Random.Next();
		Calc.PushRandom(newSeed);
		sprite = GFX.FGAutotiler.GenerateBox('g', (int)base.Width / 8, (int)base.Height / 8).TileGrid;
		Add(sprite);
		Calc.PopRandom();
		Calc.PushRandom(newSeed);
		highlight = GFX.FGAutotiler.GenerateBox('G', (int)(base.Width / 8f), (int)base.Height / 8).TileGrid;
		highlight.Alpha = 0f;
		Add(highlight);
		Calc.PopRandom();
		Add(new TileInterceptor(sprite, highPriority: false));
		Add(new LightOcclude());
	}

	public FinalBossMovingBlock(EntityData data, Vector2 offset)
		: this(data.NodesWithPosition(offset), data.Width, data.Height, data.Int("nodeIndex"))
	{
	}

	public override void OnShake(Vector2 amount)
	{
		base.OnShake(amount);
		sprite.Position = amount;
	}

	public void StartMoving(float delay)
	{
		startDelay = delay;
		Add(moveCoroutine = new Coroutine(MoveSequence()));
	}

	[IteratorStateMachine(typeof(_003CMoveSequence_003Ed__14))]
	private IEnumerator MoveSequence()
	{
		while (true)
		{
			StartShaking(0.2f + startDelay);
			if (!isHighlighted)
			{
				for (float p = 0f; p < 1f; p += Engine.DeltaTime / (0.2f + startDelay + 0.2f))
				{
					highlight.Alpha = Ease.CubeIn(p);
					sprite.Alpha = 1f - highlight.Alpha;
					yield return null;
				}
				highlight.Alpha = 1f;
				sprite.Alpha = 0f;
				isHighlighted = true;
			}
			else
			{
				yield return 0.2f + startDelay + 0.2f;
			}
			startDelay = 0f;
			nodeIndex++;
			nodeIndex %= nodes.Length;
			Vector2 from = Position;
			Vector2 to = nodes[nodeIndex];
			Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeIn, 0.8f, start: true);
			tween.OnUpdate = delegate(Tween t)
			{
				MoveTo(Vector2.Lerp(from, to, t.Eased));
			};
			tween.OnComplete = delegate
			{
				if (CollideCheck<SolidTiles>(Position + (to - from).SafeNormalize() * 2f))
				{
					Audio.Play("event:/game/06_reflection/fallblock_boss_impact", base.Center);
					ImpactParticles(to - from);
				}
				else
				{
					StopParticles(to - from);
				}
			};
			Add(tween);
			yield return 0.8f;
		}
	}

	private void StopParticles(Vector2 moved)
	{
		Level level = SceneAs<Level>();
		float direction = moved.Angle();
		if (moved.X > 0f)
		{
			Vector2 vector = new Vector2(base.Right - 1f, base.Top);
			for (int i = 0; (float)i < base.Height; i += 4)
			{
				level.Particles.Emit(P_Stop, vector + Vector2.UnitY * (2 + i + Calc.Random.Range(-1, 1)), direction);
			}
		}
		else if (moved.X < 0f)
		{
			Vector2 vector2 = new Vector2(base.Left, base.Top);
			for (int j = 0; (float)j < base.Height; j += 4)
			{
				level.Particles.Emit(P_Stop, vector2 + Vector2.UnitY * (2 + j + Calc.Random.Range(-1, 1)), direction);
			}
		}
		if (moved.Y > 0f)
		{
			Vector2 vector3 = new Vector2(base.Left, base.Bottom - 1f);
			for (int k = 0; (float)k < base.Width; k += 4)
			{
				level.Particles.Emit(P_Stop, vector3 + Vector2.UnitX * (2 + k + Calc.Random.Range(-1, 1)), direction);
			}
		}
		else if (moved.Y < 0f)
		{
			Vector2 vector4 = new Vector2(base.Left, base.Top);
			for (int l = 0; (float)l < base.Width; l += 4)
			{
				level.Particles.Emit(P_Stop, vector4 + Vector2.UnitX * (2 + l + Calc.Random.Range(-1, 1)), direction);
			}
		}
	}

	private void BreakParticles()
	{
		Vector2 center = base.Center;
		for (int i = 0; (float)i < base.Width; i += 4)
		{
			for (int j = 0; (float)j < base.Height; j += 4)
			{
				Vector2 vector = Position + new Vector2(2 + i, 2 + j);
				SceneAs<Level>().Particles.Emit(P_Break, 1, vector, Vector2.One * 2f, (vector - center).Angle());
			}
		}
	}

	private void ImpactParticles(Vector2 moved)
	{
		if (moved.X < 0f)
		{
			Vector2 vector = new Vector2(0f, 2f);
			for (int i = 0; (float)i < base.Height / 8f; i++)
			{
				Vector2 vector2 = new Vector2(base.Left - 1f, base.Top + 4f + (float)(i * 8));
				if (!base.Scene.CollideCheck<Water>(vector2) && base.Scene.CollideCheck<Solid>(vector2))
				{
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector2 + vector, 0f);
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector2 - vector, 0f);
				}
			}
		}
		else if (moved.X > 0f)
		{
			Vector2 vector3 = new Vector2(0f, 2f);
			for (int j = 0; (float)j < base.Height / 8f; j++)
			{
				Vector2 vector4 = new Vector2(base.Right + 1f, base.Top + 4f + (float)(j * 8));
				if (!base.Scene.CollideCheck<Water>(vector4) && base.Scene.CollideCheck<Solid>(vector4))
				{
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector4 + vector3, (float)Math.PI);
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector4 - vector3, (float)Math.PI);
				}
			}
		}
		if (moved.Y < 0f)
		{
			Vector2 vector5 = new Vector2(2f, 0f);
			for (int k = 0; (float)k < base.Width / 8f; k++)
			{
				Vector2 vector6 = new Vector2(base.Left + 4f + (float)(k * 8), base.Top - 1f);
				if (!base.Scene.CollideCheck<Water>(vector6) && base.Scene.CollideCheck<Solid>(vector6))
				{
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector6 + vector5, (float)Math.PI / 2f);
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector6 - vector5, (float)Math.PI / 2f);
				}
			}
		}
		else
		{
			if (!(moved.Y > 0f))
			{
				return;
			}
			Vector2 vector7 = new Vector2(2f, 0f);
			for (int l = 0; (float)l < base.Width / 8f; l++)
			{
				Vector2 vector8 = new Vector2(base.Left + 4f + (float)(l * 8), base.Bottom + 1f);
				if (!base.Scene.CollideCheck<Water>(vector8) && base.Scene.CollideCheck<Solid>(vector8))
				{
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector8 + vector7, -(float)Math.PI / 2f);
					SceneAs<Level>().ParticlesFG.Emit(CrushBlock.P_Impact, vector8 - vector7, -(float)Math.PI / 2f);
				}
			}
		}
	}

	public override void Render()
	{
		Vector2 position = Position;
		Position += base.Shake;
		base.Render();
		if (highlight.Alpha > 0f && highlight.Alpha < 1f)
		{
			int num = (int)((1f - highlight.Alpha) * 16f);
			Rectangle rect = new Rectangle((int)base.X, (int)base.Y, (int)base.Width, (int)base.Height);
			rect.Inflate(num, num);
			Draw.HollowRect(rect, Color.Lerp(Color.Purple, Color.Pink, 0.7f));
		}
		Position = position;
	}

	private void Finish()
	{
		Vector2 vector = base.CenterRight + Vector2.UnitX * 10f;
		for (int i = 0; (float)i < base.Width / 8f; i++)
		{
			for (int j = 0; (float)j < base.Height / 8f; j++)
			{
				base.Scene.Add(Engine.Pooler.Create<Debris>().Init(Position + new Vector2(4 + i * 8, 4 + j * 8), 'f').BlastFrom(vector));
			}
		}
		BreakParticles();
		DestroyStaticMovers();
		RemoveSelf();
	}

	public void Destroy(float delay)
	{
		if (base.Scene != null)
		{
			if (moveCoroutine != null)
			{
				Remove(moveCoroutine);
			}
			if (delay <= 0f)
			{
				Finish();
				return;
			}
			StartShaking(delay);
			Alarm.Set(this, delay, Finish);
		}
	}
}
