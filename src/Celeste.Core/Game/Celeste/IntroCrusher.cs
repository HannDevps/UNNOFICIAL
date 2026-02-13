using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class IntroCrusher : Solid
{
	[CompilerGenerated]
	private sealed class _003CSequence_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public IntroCrusher _003C_003E4__this;

		private float _003Ctime_003E5__2;

		private Shaker _003Cshaker_003E5__3;

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
		public _003CSequence_003Ed__9(int _003C_003E1__state)
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
			IntroCrusher CS_0024_003C_003E8__locals34 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_002d;
			case 1:
			{
				_003C_003E1__state = -1;
				Player entity2 = CS_0024_003C_003E8__locals34.Scene.Tracker.GetEntity<Player>();
				if (entity2 == null || !(entity2.X >= CS_0024_003C_003E8__locals34.X + 30f) || !(entity2.X <= CS_0024_003C_003E8__locals34.Right + 8f))
				{
					goto IL_002d;
				}
				CS_0024_003C_003E8__locals34.shakingSfx.Play("event:/game/00_prologue/fallblock_first_shake");
				_003Ctime_003E5__2 = 1.2f;
				_003Cshaker_003E5__3 = new Shaker(_003Ctime_003E5__2, removeOnFinish: true, delegate(Vector2 v)
				{
					CS_0024_003C_003E8__locals34.shake = v;
				});
				CS_0024_003C_003E8__locals34.Add(_003Cshaker_003E5__3);
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				goto IL_014e;
			}
			case 2:
				_003C_003E1__state = -1;
				_003Ctime_003E5__2 -= Engine.DeltaTime;
				goto IL_014e;
			case 3:
				{
					_003C_003E1__state = -1;
					_003Ctime_003E5__2 = Calc.Approach(_003Ctime_003E5__2, 1f, 2f * Engine.DeltaTime);
					CS_0024_003C_003E8__locals34.MoveTo(Vector2.Lerp(CS_0024_003C_003E8__locals34.start, CS_0024_003C_003E8__locals34.end, Ease.CubeIn(_003Ctime_003E5__2)));
					if (_003Ctime_003E5__2 >= 1f)
					{
						for (int i = 0; (float)i <= CS_0024_003C_003E8__locals34.Width; i += 4)
						{
							CS_0024_003C_003E8__locals34.SceneAs<Level>().ParticlesFG.Emit(FallingBlock.P_FallDustA, 1, new Vector2(CS_0024_003C_003E8__locals34.X + (float)i, CS_0024_003C_003E8__locals34.Bottom), Vector2.One * 4f, -(float)Math.PI / 2f);
							float direction = ((!((float)i < CS_0024_003C_003E8__locals34.Width / 2f)) ? 0f : ((float)Math.PI));
							CS_0024_003C_003E8__locals34.SceneAs<Level>().ParticlesFG.Emit(FallingBlock.P_LandDust, 1, new Vector2(CS_0024_003C_003E8__locals34.X + (float)i, CS_0024_003C_003E8__locals34.Bottom), Vector2.One * 4f, direction);
						}
						CS_0024_003C_003E8__locals34.shakingSfx.Stop();
						Audio.Play("event:/game/00_prologue/fallblock_first_impact", CS_0024_003C_003E8__locals34.Position);
						CS_0024_003C_003E8__locals34.SceneAs<Level>().Shake();
						Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
						CS_0024_003C_003E8__locals34.Add(new Shaker(0.25f, removeOnFinish: true, delegate(Vector2 v)
						{
							CS_0024_003C_003E8__locals34.shake = v;
						}));
						return false;
					}
					goto IL_0218;
				}
				IL_014e:
				if (_003Ctime_003E5__2 > 0f)
				{
					Player entity = CS_0024_003C_003E8__locals34.Scene.Tracker.GetEntity<Player>();
					if (entity == null || (!(entity.X >= CS_0024_003C_003E8__locals34.X + CS_0024_003C_003E8__locals34.Width - 8f) && !(entity.X < CS_0024_003C_003E8__locals34.X + 28f)))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 2;
						return true;
					}
					_003Cshaker_003E5__3.RemoveSelf();
				}
				_003Cshaker_003E5__3 = null;
				for (int num2 = 2; (float)num2 < CS_0024_003C_003E8__locals34.Width; num2 += 4)
				{
					CS_0024_003C_003E8__locals34.SceneAs<Level>().Particles.Emit(FallingBlock.P_FallDustA, 2, new Vector2(CS_0024_003C_003E8__locals34.X + (float)num2, CS_0024_003C_003E8__locals34.Y), Vector2.One * 4f, (float)Math.PI / 2f);
					CS_0024_003C_003E8__locals34.SceneAs<Level>().Particles.Emit(FallingBlock.P_FallDustB, 2, new Vector2(CS_0024_003C_003E8__locals34.X + (float)num2, CS_0024_003C_003E8__locals34.Y), Vector2.One * 4f);
				}
				CS_0024_003C_003E8__locals34.shakingSfx.Param("release", 1f);
				_003Ctime_003E5__2 = 0f;
				goto IL_0218;
				IL_002d:
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
				IL_0218:
				_003C_003E2__current = null;
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

	private Vector2 shake;

	private Vector2 start;

	private Vector2 end;

	private TileGrid tilegrid;

	private SoundSource shakingSfx;

	public IntroCrusher(Vector2 position, int width, int height, Vector2 node)
		: base(position, width, height, safe: true)
	{
		start = position;
		end = node;
		base.Depth = -10501;
		SurfaceSoundIndex = 4;
		Add(tilegrid = GFX.FGAutotiler.GenerateBox('3', width / 8, height / 8).TileGrid);
		Add(shakingSfx = new SoundSource());
	}

	public IntroCrusher(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Nodes[0] + offset)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (SceneAs<Level>().Session.GetLevelFlag("1") || SceneAs<Level>().Session.GetLevelFlag("0b"))
		{
			Position = end;
		}
		else
		{
			Add(new Coroutine(Sequence()));
		}
	}

	public override void Update()
	{
		tilegrid.Position = shake;
		base.Update();
	}

	[IteratorStateMachine(typeof(_003CSequence_003Ed__9))]
	private IEnumerator Sequence()
	{
		Player entity;
		do
		{
			yield return null;
			entity = base.Scene.Tracker.GetEntity<Player>();
		}
		while (entity == null || !(entity.X >= base.X + 30f) || !(entity.X <= base.Right + 8f));
		shakingSfx.Play("event:/game/00_prologue/fallblock_first_shake");
		float time = 1.2f;
		Shaker shaker = new Shaker(time, removeOnFinish: true, delegate(Vector2 v)
		{
			shake = v;
		});
		Add(shaker);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		while (time > 0f)
		{
			Player entity2 = base.Scene.Tracker.GetEntity<Player>();
			if (entity2 != null && (entity2.X >= base.X + base.Width - 8f || entity2.X < base.X + 28f))
			{
				shaker.RemoveSelf();
				break;
			}
			yield return null;
			time -= Engine.DeltaTime;
		}
		for (int num = 2; (float)num < base.Width; num += 4)
		{
			SceneAs<Level>().Particles.Emit(FallingBlock.P_FallDustA, 2, new Vector2(base.X + (float)num, base.Y), Vector2.One * 4f, (float)Math.PI / 2f);
			SceneAs<Level>().Particles.Emit(FallingBlock.P_FallDustB, 2, new Vector2(base.X + (float)num, base.Y), Vector2.One * 4f);
		}
		shakingSfx.Param("release", 1f);
		time = 0f;
		do
		{
			yield return null;
			time = Calc.Approach(time, 1f, 2f * Engine.DeltaTime);
			MoveTo(Vector2.Lerp(start, end, Ease.CubeIn(time)));
		}
		while (!(time >= 1f));
		for (int num2 = 0; (float)num2 <= base.Width; num2 += 4)
		{
			SceneAs<Level>().ParticlesFG.Emit(FallingBlock.P_FallDustA, 1, new Vector2(base.X + (float)num2, base.Bottom), Vector2.One * 4f, -(float)Math.PI / 2f);
			float direction = ((!((float)num2 < base.Width / 2f)) ? 0f : ((float)Math.PI));
			SceneAs<Level>().ParticlesFG.Emit(FallingBlock.P_LandDust, 1, new Vector2(base.X + (float)num2, base.Bottom), Vector2.One * 4f, direction);
		}
		shakingSfx.Stop();
		Audio.Play("event:/game/00_prologue/fallblock_first_impact", Position);
		SceneAs<Level>().Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		Add(new Shaker(0.25f, removeOnFinish: true, delegate(Vector2 v)
		{
			shake = v;
		}));
	}
}
