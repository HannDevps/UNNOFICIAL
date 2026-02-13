using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS02_Mirror : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__7 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_Mirror _003C_003E4__this;

		public Level level;

		private Vector2 _003Cfrom_003E5__2;

		private Vector2 _003Cto_003E5__3;

		private float _003Cease_003E5__4;

		private List<Entity>.Enumerator _003C_003E7__wrap4;

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
			int num = _003C_003E1__state;
			if (num == -3 || num == 9)
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
				CS02_Mirror cS02_Mirror = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					cS02_Mirror.Add(cS02_Mirror.sfx = new SoundSource());
					cS02_Mirror.sfx.Position = cS02_Mirror.mirror.Center;
					cS02_Mirror.sfx.Play("event:/music/lvl2/dreamblock_sting_pt1");
					cS02_Mirror.direction = Math.Sign(cS02_Mirror.player.X - cS02_Mirror.mirror.X);
					cS02_Mirror.player.StateMachine.State = 11;
					cS02_Mirror.playerEndX = 8 * cS02_Mirror.direction;
					_003C_003E2__current = 1f;
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					cS02_Mirror.player.Facing = (Facings)(-cS02_Mirror.direction);
					_003C_003E2__current = 0.4f;
					_003C_003E1__state = 2;
					return true;
				case 2:
					_003C_003E1__state = -1;
					_003C_003E2__current = cS02_Mirror.player.DummyRunTo(cS02_Mirror.mirror.X + cS02_Mirror.playerEndX);
					_003C_003E1__state = 3;
					return true;
				case 3:
					_003C_003E1__state = -1;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 4;
					return true;
				case 4:
					_003C_003E1__state = -1;
					_003C_003E2__current = level.ZoomTo(cS02_Mirror.mirror.Position - level.Camera.Position - Vector2.UnitY * 24f, 2f, 1f);
					_003C_003E1__state = 5;
					return true;
				case 5:
					_003C_003E1__state = -1;
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 6;
					return true;
				case 6:
					_003C_003E1__state = -1;
					_003C_003E2__current = cS02_Mirror.mirror.BreakRoutine(cS02_Mirror.direction);
					_003C_003E1__state = 7;
					return true;
				case 7:
					_003C_003E1__state = -1;
					cS02_Mirror.player.DummyAutoAnimate = false;
					cS02_Mirror.player.Sprite.Play("lookUp");
					_003Cfrom_003E5__2 = level.Camera.Position;
					_003Cto_003E5__3 = level.Camera.Position + new Vector2(0f, -80f);
					_003Cease_003E5__4 = 0f;
					goto IL_0326;
				case 8:
					_003C_003E1__state = -1;
					_003Cease_003E5__4 += Engine.DeltaTime * 1.2f;
					goto IL_0326;
				case 9:
					_003C_003E1__state = -3;
					goto IL_03af;
				case 10:
					{
						_003C_003E1__state = -1;
						cS02_Mirror.EndCutscene(level);
						return false;
					}
					IL_0326:
					if (_003Cease_003E5__4 < 1f)
					{
						level.Camera.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeInOut(_003Cease_003E5__4);
						_003C_003E2__current = null;
						_003C_003E1__state = 8;
						return true;
					}
					cS02_Mirror.Add(new Coroutine(cS02_Mirror.ZoomBack()));
					_003C_003E7__wrap4 = cS02_Mirror.Scene.Tracker.GetEntities<DreamBlock>().GetEnumerator();
					_003C_003E1__state = -3;
					if (_003C_003E7__wrap4.MoveNext())
					{
						DreamBlock dreamBlock = (DreamBlock)_003C_003E7__wrap4.Current;
						_003C_003E2__current = dreamBlock.Activate();
						_003C_003E1__state = 9;
						return true;
					}
					goto IL_03af;
					IL_03af:
					_003C_003Em__Finally1();
					_003C_003E7__wrap4 = default(List<Entity>.Enumerator);
					_003Cfrom_003E5__2 = default(Vector2);
					_003Cto_003E5__3 = default(Vector2);
					_003C_003E2__current = 0.5f;
					_003C_003E1__state = 10;
					return true;
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

	[CompilerGenerated]
	private sealed class _003CZoomBack_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_Mirror _003C_003E4__this;

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
		public _003CZoomBack_003Ed__8(int _003C_003E1__state)
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
			CS02_Mirror cS02_Mirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS02_Mirror.Level.ZoomBack(3f);
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

	private Player player;

	private DreamMirror mirror;

	private float playerEndX;

	private int direction = 1;

	private SoundSource sfx;

	public CS02_Mirror(Player player, DreamMirror mirror)
	{
		this.player = player;
		this.mirror = mirror;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__7))]
	private IEnumerator Cutscene(Level level)
	{
		Add(sfx = new SoundSource());
		sfx.Position = mirror.Center;
		sfx.Play("event:/music/lvl2/dreamblock_sting_pt1");
		direction = Math.Sign(player.X - mirror.X);
		player.StateMachine.State = 11;
		playerEndX = 8 * direction;
		yield return 1f;
		player.Facing = (Facings)(-direction);
		yield return 0.4f;
		yield return player.DummyRunTo(mirror.X + playerEndX);
		yield return 0.5f;
		yield return level.ZoomTo(mirror.Position - level.Camera.Position - Vector2.UnitY * 24f, 2f, 1f);
		yield return 0.5f;
		yield return mirror.BreakRoutine(direction);
		player.DummyAutoAnimate = false;
		player.Sprite.Play("lookUp");
		Vector2 from = level.Camera.Position;
		Vector2 to = level.Camera.Position + new Vector2(0f, -80f);
		for (float ease = 0f; ease < 1f; ease += Engine.DeltaTime * 1.2f)
		{
			level.Camera.Position = from + (to - from) * Ease.CubeInOut(ease);
			yield return null;
		}
		Add(new Coroutine(ZoomBack()));
		using (List<Entity>.Enumerator enumerator = base.Scene.Tracker.GetEntities<DreamBlock>().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				DreamBlock dreamBlock = (DreamBlock)enumerator.Current;
				yield return dreamBlock.Activate();
			}
		}
		yield return 0.5f;
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CZoomBack_003Ed__8))]
	private IEnumerator ZoomBack()
	{
		yield return 1.2f;
		yield return Level.ZoomBack(3f);
	}

	public override void OnEnd(Level level)
	{
		mirror.Broken(WasSkipped);
		if (WasSkipped)
		{
			SceneAs<Level>().ParticlesFG.Clear();
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.StateMachine.State = 0;
			entity.DummyAutoAnimate = true;
			entity.Speed = Vector2.Zero;
			entity.X = mirror.X + playerEndX;
			if (direction != 0)
			{
				entity.Facing = (Facings)(-direction);
			}
			else
			{
				entity.Facing = Facings.Right;
			}
		}
		foreach (DreamBlock entity2 in base.Scene.Tracker.GetEntities<DreamBlock>())
		{
			entity2.ActivateNoRoutine();
		}
		level.ResetZoom();
		level.Session.Inventory.DreamDash = true;
		level.Session.Audio.Music.Event = "event:/music/lvl2/mirror";
		level.Session.Audio.Apply();
	}
}
