using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class SummitGemManager : Entity
{
	private class Gem : Entity
	{
		public Vector2 Shake;

		public Sprite Sprite;

		public Image Bg;

		public BloomPoint Bloom;

		public Gem(int index, Vector2 position)
			: base(position)
		{
			base.Depth = -10010;
			Add(Bg = new Image(GFX.Game["collectables/summitgems/" + index + "/bg"]));
			Add(Sprite = new Sprite(GFX.Game, "collectables/summitgems/" + index + "/gem"));
			Add(Bloom = new BloomPoint(0f, 20f));
			Sprite.AddLoop("idle", "", 0.05f, default(int));
			Sprite.Add("spin", "", 0.05f, "idle");
			Sprite.Play("idle");
			Sprite.CenterOrigin();
			Bg.CenterOrigin();
		}

		public override void Update()
		{
			Bloom.Position = Sprite.Position;
			base.Update();
		}

		public override void Render()
		{
			Vector2 position = Sprite.Position;
			Sprite.Position += Shake;
			base.Render();
			Sprite.Position = position;
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SummitGemManager _003C_003E4__this;

		private Level _003Clevel_003E5__2;

		private bool _003CalreadyHasHeart_003E5__3;

		private int _003Cbroken_003E5__4;

		private int _003Cindex_003E5__5;

		private List<Gem>.Enumerator _003C_003E7__wrap5;

		private Gem _003Cgem_003E5__7;

		private HeartGem _003Cheart_003E5__8;

		private Vector2 _003Cfrom_003E5__9;

		private float _003Cp_003E5__10;

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
		public _003CRoutine_003Ed__4(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || (uint)(num - 3) <= 2u)
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
				SummitGemManager summitGemManager = _003C_003E4__this;
				Player entity;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Clevel_003E5__2 = summitGemManager.Scene as Level;
					if (_003Clevel_003E5__2.Session.HeartGem)
					{
						foreach (Gem gem in summitGemManager.gems)
						{
							gem.Sprite.RemoveSelf();
						}
						summitGemManager.gems.Clear();
						return false;
					}
					goto IL_00af;
				case 1:
					_003C_003E1__state = -1;
					goto IL_00af;
				case 2:
					_003C_003E1__state = -1;
					_003CalreadyHasHeart_003E5__3 = _003Clevel_003E5__2.Session.OldStats.Modes[0].HeartGem;
					_003Cbroken_003E5__4 = 0;
					_003Cindex_003E5__5 = 0;
					_003C_003E7__wrap5 = summitGemManager.gems.GetEnumerator();
					_003C_003E1__state = -3;
					goto IL_04d4;
				case 3:
					_003C_003E1__state = -3;
					goto IL_0391;
				case 4:
				{
					_003C_003E1__state = -3;
					_003Clevel_003E5__2.Shake();
					Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
					for (int i = 0; i < 20; i++)
					{
						_003Clevel_003E5__2.ParticlesFG.Emit(SummitGem.P_Shatter, _003Cgem_003E5__7.Position + new Vector2(Calc.Random.Range(-8, 8), Calc.Random.Range(-8, 8)), SummitGem.GemColors[_003Cindex_003E5__5], Calc.Random.NextFloat((float)Math.PI * 2f));
					}
					_003Cbroken_003E5__4++;
					_003Cgem_003E5__7.Bloom.RemoveSelf();
					_003Cgem_003E5__7.Sprite.RemoveSelf();
					_003C_003E2__current = 0.25f;
					_003C_003E1__state = 5;
					return true;
				}
				case 5:
					_003C_003E1__state = -3;
					goto IL_04bb;
				case 6:
					_003C_003E1__state = -1;
					_003Cfrom_003E5__9 = _003Cheart_003E5__8.Position;
					_003Cp_003E5__10 = 0f;
					goto IL_05e7;
				case 7:
					{
						_003C_003E1__state = -1;
						_003Cp_003E5__10 += Engine.DeltaTime;
						goto IL_05e7;
					}
					IL_05e7:
					if (_003Cp_003E5__10 < 1f && _003Cheart_003E5__8.Scene != null)
					{
						_003Cheart_003E5__8.Position = Vector2.Lerp(_003Cfrom_003E5__9, summitGemManager.Position + new Vector2(0f, -16f), Ease.CubeOut(_003Cp_003E5__10));
						_003C_003E2__current = null;
						_003C_003E1__state = 7;
						return true;
					}
					_003Cfrom_003E5__9 = default(Vector2);
					goto IL_0610;
					IL_0610:
					_003Cheart_003E5__8 = null;
					break;
					IL_00af:
					entity = summitGemManager.Scene.Tracker.GetEntity<Player>();
					if (entity == null || !((entity.Position - summitGemManager.Position).Length() < 64f))
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 2;
					return true;
					IL_0391:
					if (_003Cgem_003E5__7.Sprite.CurrentAnimationID == "spin")
					{
						_003Cgem_003E5__7.Bloom.Alpha = Calc.Approach(_003Cgem_003E5__7.Bloom.Alpha, 1f, Engine.DeltaTime * 3f);
						if (_003Cgem_003E5__7.Bloom.Alpha > 0.5f)
						{
							_003Cgem_003E5__7.Shake = Calc.Random.ShakeVector();
						}
						_003Cgem_003E5__7.Sprite.Y -= Engine.DeltaTime * 8f;
						_003Cgem_003E5__7.Sprite.Scale = Vector2.One * (1f + _003Cgem_003E5__7.Bloom.Alpha * 0.1f);
						_003C_003E2__current = null;
						_003C_003E1__state = 3;
						return true;
					}
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 4;
					return true;
					IL_04bb:
					_003Cindex_003E5__5++;
					_003Cgem_003E5__7 = null;
					goto IL_04d4;
					IL_04d4:
					if (_003C_003E7__wrap5.MoveNext())
					{
						_003Cgem_003E5__7 = _003C_003E7__wrap5.Current;
						bool flag = _003Clevel_003E5__2.Session.SummitGems[_003Cindex_003E5__5];
						if (!_003CalreadyHasHeart_003E5__3)
						{
							flag |= SaveData.Instance.SummitGems != null && SaveData.Instance.SummitGems[_003Cindex_003E5__5];
						}
						if (flag)
						{
							if (_003Cindex_003E5__5 == 0)
							{
								Audio.Play("event:/game/07_summit/gem_unlock_1", _003Cgem_003E5__7.Position);
							}
							else if (_003Cindex_003E5__5 == 1)
							{
								Audio.Play("event:/game/07_summit/gem_unlock_2", _003Cgem_003E5__7.Position);
							}
							else if (_003Cindex_003E5__5 == 2)
							{
								Audio.Play("event:/game/07_summit/gem_unlock_3", _003Cgem_003E5__7.Position);
							}
							else if (_003Cindex_003E5__5 == 3)
							{
								Audio.Play("event:/game/07_summit/gem_unlock_4", _003Cgem_003E5__7.Position);
							}
							else if (_003Cindex_003E5__5 == 4)
							{
								Audio.Play("event:/game/07_summit/gem_unlock_5", _003Cgem_003E5__7.Position);
							}
							else if (_003Cindex_003E5__5 == 5)
							{
								Audio.Play("event:/game/07_summit/gem_unlock_6", _003Cgem_003E5__7.Position);
							}
							_003Cgem_003E5__7.Sprite.Play("spin");
							goto IL_0391;
						}
						goto IL_04bb;
					}
					_003C_003Em__Finally1();
					_003C_003E7__wrap5 = default(List<Gem>.Enumerator);
					if (_003Cbroken_003E5__4 < 6)
					{
						break;
					}
					_003Cheart_003E5__8 = summitGemManager.Scene.Entities.FindFirst<HeartGem>();
					if (_003Cheart_003E5__8 != null)
					{
						Audio.Play("event:/game/07_summit/gem_unlock_complete", _003Cheart_003E5__8.Position);
						_003C_003E2__current = 0.1f;
						_003C_003E1__state = 6;
						return true;
					}
					goto IL_0610;
				}
				return false;
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
			((IDisposable)_003C_003E7__wrap5/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private List<Gem> gems = new List<Gem>();

	public SummitGemManager(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		base.Depth = -10010;
		int num = 0;
		Vector2[] array = data.NodesOffset(offset);
		foreach (Vector2 position in array)
		{
			Gem item = new Gem(num, position);
			gems.Add(item);
			num++;
		}
		Add(new Coroutine(Routine()));
	}

	public override void Awake(Scene scene)
	{
		foreach (Gem gem in gems)
		{
			scene.Add(gem);
		}
		base.Awake(scene);
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__4))]
	private IEnumerator Routine()
	{
		Level level = base.Scene as Level;
		if (level.Session.HeartGem)
		{
			foreach (Gem gem2 in gems)
			{
				gem2.Sprite.RemoveSelf();
			}
			gems.Clear();
			yield break;
		}
		while (true)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && (entity.Position - Position).Length() < 64f)
			{
				break;
			}
			yield return null;
		}
		yield return 0.5f;
		bool alreadyHasHeart = level.Session.OldStats.Modes[0].HeartGem;
		int broken = 0;
		int index = 0;
		foreach (Gem gem in gems)
		{
			bool flag = level.Session.SummitGems[index];
			if (!alreadyHasHeart)
			{
				flag |= SaveData.Instance.SummitGems != null && SaveData.Instance.SummitGems[index];
			}
			if (flag)
			{
				switch (index)
				{
				case 0:
					Audio.Play("event:/game/07_summit/gem_unlock_1", gem.Position);
					break;
				case 1:
					Audio.Play("event:/game/07_summit/gem_unlock_2", gem.Position);
					break;
				case 2:
					Audio.Play("event:/game/07_summit/gem_unlock_3", gem.Position);
					break;
				case 3:
					Audio.Play("event:/game/07_summit/gem_unlock_4", gem.Position);
					break;
				case 4:
					Audio.Play("event:/game/07_summit/gem_unlock_5", gem.Position);
					break;
				case 5:
					Audio.Play("event:/game/07_summit/gem_unlock_6", gem.Position);
					break;
				}
				gem.Sprite.Play("spin");
				while (gem.Sprite.CurrentAnimationID == "spin")
				{
					gem.Bloom.Alpha = Calc.Approach(gem.Bloom.Alpha, 1f, Engine.DeltaTime * 3f);
					if (gem.Bloom.Alpha > 0.5f)
					{
						gem.Shake = Calc.Random.ShakeVector();
					}
					gem.Sprite.Y -= Engine.DeltaTime * 8f;
					gem.Sprite.Scale = Vector2.One * (1f + gem.Bloom.Alpha * 0.1f);
					yield return null;
				}
				yield return 0.2f;
				level.Shake();
				Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
				for (int i = 0; i < 20; i++)
				{
					level.ParticlesFG.Emit(SummitGem.P_Shatter, gem.Position + new Vector2(Calc.Random.Range(-8, 8), Calc.Random.Range(-8, 8)), SummitGem.GemColors[index], Calc.Random.NextFloat((float)Math.PI * 2f));
				}
				broken++;
				gem.Bloom.RemoveSelf();
				gem.Sprite.RemoveSelf();
				yield return 0.25f;
			}
			index++;
		}
		if (broken < 6)
		{
			yield break;
		}
		HeartGem heart = base.Scene.Entities.FindFirst<HeartGem>();
		if (heart == null)
		{
			yield break;
		}
		Audio.Play("event:/game/07_summit/gem_unlock_complete", heart.Position);
		yield return 0.1f;
		Vector2 from = heart.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			if (heart.Scene == null)
			{
				break;
			}
			heart.Position = Vector2.Lerp(from, Position + new Vector2(0f, -16f), Ease.CubeOut(p));
			yield return null;
		}
	}
}
