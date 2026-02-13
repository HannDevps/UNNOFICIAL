using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC05_Badeline : NPC
{
	[CompilerGenerated]
	private sealed class _003CFirstScene_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC05_Badeline _003C_003E4__this;

		private bool _003CplayerHasFallen_003E5__2;

		private bool _003CstartedMusic_003E5__3;

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
		public _003CFirstScene_003Ed__9(int _003C_003E1__state)
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
			NPC05_Badeline nPC05_Badeline = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC05_Badeline.shadow.Sprite.Scale.X = -1f;
				nPC05_Badeline.shadow.FloatSpeed = 150f;
				_003CplayerHasFallen_003E5__2 = false;
				_003CstartedMusic_003E5__3 = false;
				_003Cplayer_003E5__4 = null;
				goto IL_0068;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0068;
			case 2:
				{
					_003C_003E1__state = -1;
					if (_003Cplayer_003E5__4.X > nPC05_Badeline.shadow.X - 24f)
					{
						nPC05_Badeline.shadow.X = _003Cplayer_003E5__4.X + 24f;
					}
					break;
				}
				IL_0068:
				_003Cplayer_003E5__4 = nPC05_Badeline.Scene.Tracker.GetEntity<Player>();
				if (_003Cplayer_003E5__4 != null && _003Cplayer_003E5__4.Y > (float)(nPC05_Badeline.Level.Bounds.Top + 180) && !_003Cplayer_003E5__4.OnGround() && !_003CplayerHasFallen_003E5__2)
				{
					_003Cplayer_003E5__4.StateMachine.State = 20;
					_003CplayerHasFallen_003E5__2 = true;
				}
				if (((_003Cplayer_003E5__4 != null) & _003CplayerHasFallen_003E5__2) && !_003CstartedMusic_003E5__3 && _003Cplayer_003E5__4.OnGround())
				{
					nPC05_Badeline.Level.Session.Audio.Music.Event = "event:/music/lvl5/middle_temple";
					nPC05_Badeline.Level.Session.Audio.Apply();
					_003CstartedMusic_003E5__3 = true;
				}
				if (_003Cplayer_003E5__4 == null || !(_003Cplayer_003E5__4.X > nPC05_Badeline.X - 64f) || !(_003Cplayer_003E5__4.Y > nPC05_Badeline.Y - 32f))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				nPC05_Badeline.MoveToNode(0, chatMove: false);
				break;
			}
			if (nPC05_Badeline.shadow.X < (float)(nPC05_Badeline.Level.Bounds.Right + 8))
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			nPC05_Badeline.Scene.Remove(nPC05_Badeline.shadow);
			nPC05_Badeline.RemoveSelf();
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
	private sealed class _003CSecondScene_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC05_Badeline _003C_003E4__this;

		public int startIndex;

		private int _003Cindex_003E5__2;

		private Player _003Cplayer_003E5__3;

		private CS05_Badeline _003Ccutscene_003E5__4;

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
		public _003CSecondScene_003Ed__10(int _003C_003E1__state)
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
			NPC05_Badeline nPC05_Badeline = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC05_Badeline.shadow.Sprite.Scale.X = -1f;
				nPC05_Badeline.shadow.FloatSpeed = 300f;
				nPC05_Badeline.shadow.FloatAccel = 400f;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cindex_003E5__2 = startIndex;
				goto IL_01ae;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00cb;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0183;
			case 4:
				{
					_003C_003E1__state = -1;
					goto IL_0183;
				}
				IL_0183:
				if (_003Ccutscene_003E5__4.Scene != null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				_003Cindex_003E5__2++;
				_003Ccutscene_003E5__4 = null;
				goto IL_01a7;
				IL_00cb:
				if (_003Cplayer_003E5__3 == null || (_003Cplayer_003E5__3.Position - nPC05_Badeline.shadow.Position).Length() > 70f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				if (_003Cindex_003E5__2 < 4 && !nPC05_Badeline.Session.GetFlag(CS05_Badeline.GetFlag(_003Cindex_003E5__2)))
				{
					_003Ccutscene_003E5__4 = new CS05_Badeline(_003Cplayer_003E5__3, nPC05_Badeline, nPC05_Badeline.shadow, _003Cindex_003E5__2);
					nPC05_Badeline.Scene.Add(_003Ccutscene_003E5__4);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_01a7;
				IL_01a7:
				_003Cplayer_003E5__3 = null;
				goto IL_01ae;
				IL_01ae:
				if (_003Cindex_003E5__2 < nPC05_Badeline.nodes.Length)
				{
					_003Cplayer_003E5__3 = nPC05_Badeline.Scene.Tracker.GetEntity<Player>();
					goto IL_00cb;
				}
				nPC05_Badeline.Tag |= Tags.TransitionUpdate;
				nPC05_Badeline.shadow.Tag |= Tags.TransitionUpdate;
				nPC05_Badeline.Scene.Remove(nPC05_Badeline.shadow);
				nPC05_Badeline.RemoveSelf();
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

	public const string FirstLevel = "c-00";

	public const string SecondLevel = "c-01";

	public const string ThirdLevel = "c-01b";

	private BadelineDummy shadow;

	private Vector2[] nodes;

	private Rectangle levelBounds;

	private SoundSource moveSfx;

	public NPC05_Badeline(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		nodes = data.NodesOffset(offset);
		Add(moveSfx = new SoundSource());
		Add(new TransitionListener
		{
			OnOut = delegate(float f)
			{
				if (shadow != null)
				{
					shadow.Hair.Alpha = 1f - Math.Min(1f, f * 2f);
					shadow.Sprite.Color = Color.White * shadow.Hair.Alpha;
					shadow.Light.Alpha = shadow.Hair.Alpha;
				}
			}
		});
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (base.Session.Level.Equals("c-00"))
		{
			if (!base.Session.GetLevelFlag("c-01"))
			{
				scene.Add(shadow = new BadelineDummy(Position));
				shadow.Depth = -1000000;
				Add(new Coroutine(FirstScene()));
			}
			else
			{
				RemoveSelf();
			}
		}
		else if (base.Session.Level.Equals("c-01"))
		{
			if (!base.Session.GetLevelFlag("c-01b"))
			{
				int i;
				for (i = 0; i < 4 && base.Session.GetFlag(CS05_Badeline.GetFlag(i)); i++)
				{
				}
				if (i >= 4)
				{
					RemoveSelf();
				}
				else
				{
					Vector2 position = Position;
					if (i > 0)
					{
						position = nodes[i - 1];
					}
					scene.Add(shadow = new BadelineDummy(position));
					shadow.Depth = -1000000;
					Add(new Coroutine(SecondScene(i)));
				}
			}
			else
			{
				RemoveSelf();
			}
		}
		levelBounds = (scene as Level).Bounds;
	}

	[IteratorStateMachine(typeof(_003CFirstScene_003Ed__9))]
	private IEnumerator FirstScene()
	{
		shadow.Sprite.Scale.X = -1f;
		shadow.FloatSpeed = 150f;
		bool playerHasFallen = false;
		bool startedMusic = false;
		Player player;
		while (true)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			if (player != null && player.Y > (float)(Level.Bounds.Top + 180) && !player.OnGround() && !playerHasFallen)
			{
				player.StateMachine.State = 20;
				playerHasFallen = true;
			}
			if (player != null && playerHasFallen && !startedMusic && player.OnGround())
			{
				Level.Session.Audio.Music.Event = "event:/music/lvl5/middle_temple";
				Level.Session.Audio.Apply();
				startedMusic = true;
			}
			if (player != null && player.X > base.X - 64f && player.Y > base.Y - 32f)
			{
				break;
			}
			yield return null;
		}
		MoveToNode(0, chatMove: false);
		while (shadow.X < (float)(Level.Bounds.Right + 8))
		{
			yield return null;
			if (player.X > shadow.X - 24f)
			{
				shadow.X = player.X + 24f;
			}
		}
		base.Scene.Remove(shadow);
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CSecondScene_003Ed__10))]
	private IEnumerator SecondScene(int startIndex)
	{
		shadow.Sprite.Scale.X = -1f;
		shadow.FloatSpeed = 300f;
		shadow.FloatAccel = 400f;
		yield return 0.1f;
		int index = startIndex;
		while (index < nodes.Length)
		{
			Player player = base.Scene.Tracker.GetEntity<Player>();
			while (player == null || (player.Position - shadow.Position).Length() > 70f)
			{
				yield return null;
			}
			if (index < 4 && !base.Session.GetFlag(CS05_Badeline.GetFlag(index)))
			{
				CS05_Badeline cutscene = new CS05_Badeline(player, this, shadow, index);
				base.Scene.Add(cutscene);
				yield return null;
				while (cutscene.Scene != null)
				{
					yield return null;
				}
				index++;
			}
		}
		base.Tag |= Tags.TransitionUpdate;
		shadow.Tag |= Tags.TransitionUpdate;
		base.Scene.Remove(shadow);
		RemoveSelf();
	}

	public void MoveToNode(int index, bool chatMove = true)
	{
		if (chatMove)
		{
			moveSfx.Play("event:/char/badeline/temple_move_chats");
		}
		else
		{
			SoundEmitter.Play("event:/char/badeline/temple_move_first", this);
		}
		Vector2 start = shadow.Position;
		Vector2 end = nodes[index];
		Tween tween = Tween.Create(Tween.TweenMode.Oneshot, Ease.CubeInOut, 0.5f, start: true);
		tween.OnUpdate = delegate(Tween t)
		{
			shadow.Position = Vector2.Lerp(start, end, t.Eased);
			if (base.Scene.OnInterval(0.03f))
			{
				SceneAs<Level>().ParticlesFG.Emit(BadelineOldsite.P_Vanish, 2, shadow.Position + new Vector2(0f, -6f), Vector2.One * 2f);
			}
			if (t.Eased >= 0.1f && t.Eased <= 0.9f && base.Scene.OnInterval(0.05f))
			{
				TrailManager.Add(shadow, Color.Red, 0.5f);
			}
		};
		Add(tween);
	}

	public void SnapToNode(int index)
	{
		shadow.Position = nodes[index];
	}
}
