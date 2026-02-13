using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class LockBlock : Solid
{
	[CompilerGenerated]
	private sealed class _003CUnlockRoutine_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LockBlock _003C_003E4__this;

		public Follower fol;

		private SoundEmitter _003Cemitter_003E5__2;

		private Level _003Clevel_003E5__3;

		private Key _003Ckey_003E5__4;

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
		public _003CUnlockRoutine_003Ed__12(int _003C_003E1__state)
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
			LockBlock lockBlock = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cemitter_003E5__2 = SoundEmitter.Play(lockBlock.unlockSfxName, lockBlock);
				_003Cemitter_003E5__2.Source.DisposeOnTransition = true;
				_003Clevel_003E5__3 = lockBlock.SceneAs<Level>();
				_003Ckey_003E5__4 = fol.Entity as Key;
				lockBlock.Add(new Coroutine(_003Ckey_003E5__4.UseRoutine(lockBlock.Center + new Vector2(0f, 2f))));
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				lockBlock.UnlockingRegistered = true;
				if (lockBlock.stepMusicProgress)
				{
					_003Clevel_003E5__3.Session.Audio.Music.Progress++;
					_003Clevel_003E5__3.Session.Audio.Apply();
				}
				_003Clevel_003E5__3.Session.DoNotLoad.Add(lockBlock.ID);
				_003Ckey_003E5__4.RegisterUsed();
				goto IL_0159;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0159;
			case 3:
				_003C_003E1__state = -1;
				_003Clevel_003E5__3.Shake();
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				_003C_003E2__current = lockBlock.sprite.PlayRoutine("burst");
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					lockBlock.RemoveSelf();
					return false;
				}
				IL_0159:
				if (_003Ckey_003E5__4.Turning)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				lockBlock.Tag |= Tags.TransitionUpdate;
				lockBlock.Collidable = false;
				_003Cemitter_003E5__2.Source.DisposeOnTransition = false;
				_003C_003E2__current = lockBlock.sprite.PlayRoutine("open");
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

	public static ParticleType P_Appear;

	public EntityID ID;

	public bool UnlockingRegistered;

	private Sprite sprite;

	private bool opening;

	private bool stepMusicProgress;

	private string unlockSfxName;

	public LockBlock(Vector2 position, EntityID id, bool stepMusicProgress, string spriteName, string unlock_sfx)
		: base(position, 32f, 32f, safe: false)
	{
		ID = id;
		DisableLightsInside = false;
		this.stepMusicProgress = stepMusicProgress;
		Add(new PlayerCollider(OnPlayer, new Circle(60f, 16f, 16f)));
		Add(sprite = GFX.SpriteBank.Create("lockdoor_" + spriteName));
		sprite.Play("idle");
		sprite.Position = new Vector2(base.Width / 2f, base.Height / 2f);
		if (string.IsNullOrWhiteSpace(unlock_sfx))
		{
			unlockSfxName = "event:/game/03_resort/key_unlock";
			if (spriteName == "temple_a")
			{
				unlockSfxName = "event:/game/05_mirror_temple/key_unlock_light";
			}
			else if (spriteName == "temple_b")
			{
				unlockSfxName = "event:/game/05_mirror_temple/key_unlock_dark";
			}
		}
		else
		{
			unlockSfxName = SFX.EventnameByHandle(unlock_sfx);
		}
	}

	public LockBlock(EntityData data, Vector2 offset, EntityID id)
		: this(data.Position + offset, id, data.Bool("stepMusicProgress"), data.Attr("sprite", "wood"), data.Attr("unlock_sfx", null))
	{
	}

	public void Appear()
	{
		Visible = true;
		sprite.Play("appear");
		Add(Alarm.Create(Alarm.AlarmMode.Oneshot, delegate
		{
			Level level = base.Scene as Level;
			if (!CollideCheck<Solid>(Position - Vector2.UnitX))
			{
				level.Particles.Emit(P_Appear, 16, Position + new Vector2(3f, 16f), new Vector2(2f, 10f), (float)Math.PI);
				level.Particles.Emit(P_Appear, 16, Position + new Vector2(29f, 16f), new Vector2(2f, 10f), 0f);
			}
			level.Shake();
		}, 0.25f, start: true));
	}

	private void OnPlayer(Player player)
	{
		if (opening)
		{
			return;
		}
		foreach (Follower follower in player.Leader.Followers)
		{
			if (follower.Entity is Key && !(follower.Entity as Key).StartedUsing)
			{
				TryOpen(player, follower);
				break;
			}
		}
	}

	private void TryOpen(Player player, Follower fol)
	{
		Collidable = false;
		if (!base.Scene.CollideCheck<Solid>(player.Center, base.Center))
		{
			opening = true;
			(fol.Entity as Key).StartedUsing = true;
			Add(new Coroutine(UnlockRoutine(fol)));
		}
		Collidable = true;
	}

	[IteratorStateMachine(typeof(_003CUnlockRoutine_003Ed__12))]
	private IEnumerator UnlockRoutine(Follower fol)
	{
		SoundEmitter emitter = SoundEmitter.Play(unlockSfxName, this);
		emitter.Source.DisposeOnTransition = true;
		Level level = SceneAs<Level>();
		Key key = fol.Entity as Key;
		Add(new Coroutine(key.UseRoutine(base.Center + new Vector2(0f, 2f))));
		yield return 1.2f;
		UnlockingRegistered = true;
		if (stepMusicProgress)
		{
			level.Session.Audio.Music.Progress++;
			level.Session.Audio.Apply();
		}
		level.Session.DoNotLoad.Add(ID);
		key.RegisterUsed();
		while (key.Turning)
		{
			yield return null;
		}
		base.Tag |= Tags.TransitionUpdate;
		Collidable = false;
		emitter.Source.DisposeOnTransition = false;
		yield return sprite.PlayRoutine("open");
		level.Shake();
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		yield return sprite.PlayRoutine("burst");
		RemoveSelf();
	}
}
