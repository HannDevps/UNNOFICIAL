using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CS01_Ending : CutsceneEntity
{
	[CompilerGenerated]
	private sealed class _003CCutscene_003Ed__4 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS01_Ending _003C_003E4__this;

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
		public _003CCutscene_003Ed__4(int _003C_003E1__state)
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
			CS01_Ending cS01_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS01_Ending.player.StateMachine.State = 11;
				cS01_Ending.player.Dashes = 1;
				level.Session.Audio.Music.Layer(3, value: false);
				level.Session.Audio.Apply();
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS01_Ending.player.DummyWalkTo(cS01_Ending.bonfire.X + 40f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS01_Ending.player.Facing = Facings.Left;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH1_END", cS01_Ending.EndCityTrigger);
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.3f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS01_Ending.EndCutscene(level);
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
	private sealed class _003CEndCityTrigger_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS01_Ending _003C_003E4__this;

		private BirdNPC _003Cbird_003E5__2;

		private FMOD.Studio.EventInstance _003Cinstance_003E5__3;

		private Vector2 _003Cfrom_003E5__4;

		private Vector2 _003Cto_003E5__5;

		private float _003Cpercent_003E5__6;

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
		public _003CEndCityTrigger_003Ed__5(int _003C_003E1__state)
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
			CS01_Ending cS01_Ending = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS01_Ending.player.DummyWalkTo(cS01_Ending.bonfire.X - 12f);
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				cS01_Ending.player.Facing = Facings.Right;
				cS01_Ending.player.DummyAutoAnimate = false;
				cS01_Ending.player.Sprite.Play("duck");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				if (cS01_Ending.bonfire != null)
				{
					cS01_Ending.bonfire.SetMode(Bonfire.Mode.Lit);
				}
				_003C_003E2__current = 1f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				cS01_Ending.player.Sprite.Play("idle");
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				cS01_Ending.player.DummyAutoAnimate = true;
				_003C_003E2__current = cS01_Ending.player.DummyWalkTo(cS01_Ending.bonfire.X - 24f);
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				cS01_Ending.player.DummyAutoAnimate = false;
				cS01_Ending.player.Facing = Facings.Right;
				cS01_Ending.player.Sprite.Play("sleep");
				Audio.Play("event:/char/madeline/campfire_sit", cS01_Ending.player.Position);
				_003C_003E2__current = 4f;
				_003C_003E1__state = 9;
				return true;
			case 9:
				_003C_003E1__state = -1;
				_003Cbird_003E5__2 = new BirdNPC(cS01_Ending.player.Position + new Vector2(88f, -200f), BirdNPC.Modes.None);
				cS01_Ending.Scene.Add(_003Cbird_003E5__2);
				_003Cinstance_003E5__3 = Audio.Play("event:/game/general/bird_in", _003Cbird_003E5__2.Position);
				_003Cbird_003E5__2.Facing = Facings.Left;
				_003Cbird_003E5__2.Sprite.Play("fall");
				_003Cfrom_003E5__4 = _003Cbird_003E5__2.Position;
				_003Cto_003E5__5 = cS01_Ending.player.Position + new Vector2(1f, -12f);
				_003Cpercent_003E5__6 = 0f;
				goto IL_03c1;
			case 10:
				_003C_003E1__state = -1;
				goto IL_03c1;
			case 11:
				_003C_003E1__state = -1;
				_003Cbird_003E5__2.Sprite.Play("croak");
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 12;
				return true;
			case 12:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/general/bird_squawk", _003Cbird_003E5__2.Position);
				_003C_003E2__current = 0.9f;
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				_003Cbird_003E5__2.Sprite.Play("sleep");
				_003C_003E2__current = null;
				_003C_003E1__state = 14;
				return true;
			case 14:
				_003C_003E1__state = -1;
				_003Cbird_003E5__2 = null;
				_003Cinstance_003E5__3 = null;
				_003Cfrom_003E5__4 = default(Vector2);
				_003Cto_003E5__5 = default(Vector2);
				_003C_003E2__current = 2f;
				_003C_003E1__state = 15;
				return true;
			case 15:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_03c1:
				if (_003Cpercent_003E5__6 < 1f)
				{
					_003Cbird_003E5__2.Position = _003Cfrom_003E5__4 + (_003Cto_003E5__5 - _003Cfrom_003E5__4) * Ease.QuadOut(_003Cpercent_003E5__6);
					Audio.Position(_003Cinstance_003E5__3, _003Cbird_003E5__2.Position);
					if (_003Cpercent_003E5__6 > 0.5f)
					{
						_003Cbird_003E5__2.Sprite.Play("fly");
					}
					_003Cpercent_003E5__6 += Engine.DeltaTime * 0.5f;
					_003C_003E2__current = null;
					_003C_003E1__state = 10;
					return true;
				}
				_003Cbird_003E5__2.Position = _003Cto_003E5__5;
				_003Cbird_003E5__2.Sprite.Play("idle");
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 11;
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

	private Player player;

	private Bonfire bonfire;

	public CS01_Ending(Player player)
		: base(fadeInOnSkip: false, endingChapterAfter: true)
	{
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		level.RegisterAreaComplete();
		bonfire = base.Scene.Tracker.GetEntity<Bonfire>();
		Add(new Coroutine(Cutscene(level)));
	}

	[IteratorStateMachine(typeof(_003CCutscene_003Ed__4))]
	private IEnumerator Cutscene(Level level)
	{
		player.StateMachine.State = 11;
		player.Dashes = 1;
		level.Session.Audio.Music.Layer(3, value: false);
		level.Session.Audio.Apply();
		yield return 0.5f;
		yield return player.DummyWalkTo(bonfire.X + 40f);
		yield return 1.5f;
		player.Facing = Facings.Left;
		yield return 0.5f;
		yield return Textbox.Say("CH1_END", EndCityTrigger);
		yield return 0.3f;
		EndCutscene(level);
	}

	[IteratorStateMachine(typeof(_003CEndCityTrigger_003Ed__5))]
	private IEnumerator EndCityTrigger()
	{
		yield return 0.2f;
		yield return player.DummyWalkTo(bonfire.X - 12f);
		yield return 0.2f;
		player.Facing = Facings.Right;
		player.DummyAutoAnimate = false;
		player.Sprite.Play("duck");
		yield return 0.5f;
		if (bonfire != null)
		{
			bonfire.SetMode(Bonfire.Mode.Lit);
		}
		yield return 1f;
		player.Sprite.Play("idle");
		yield return 0.4f;
		player.DummyAutoAnimate = true;
		yield return player.DummyWalkTo(bonfire.X - 24f);
		yield return 0.4f;
		player.DummyAutoAnimate = false;
		player.Facing = Facings.Right;
		player.Sprite.Play("sleep");
		Audio.Play("event:/char/madeline/campfire_sit", player.Position);
		yield return 4f;
		BirdNPC bird = new BirdNPC(player.Position + new Vector2(88f, -200f), BirdNPC.Modes.None);
		base.Scene.Add(bird);
		FMOD.Studio.EventInstance instance = Audio.Play("event:/game/general/bird_in", bird.Position);
		bird.Facing = Facings.Left;
		bird.Sprite.Play("fall");
		Vector2 from = bird.Position;
		Vector2 to = player.Position + new Vector2(1f, -12f);
		float percent = 0f;
		while (percent < 1f)
		{
			bird.Position = from + (to - from) * Ease.QuadOut(percent);
			Audio.Position(instance, bird.Position);
			if (percent > 0.5f)
			{
				bird.Sprite.Play("fly");
			}
			percent += Engine.DeltaTime * 0.5f;
			yield return null;
		}
		bird.Position = to;
		bird.Sprite.Play("idle");
		yield return 0.5f;
		bird.Sprite.Play("croak");
		yield return 0.6f;
		Audio.Play("event:/game/general/bird_squawk", bird.Position);
		yield return 0.9f;
		bird.Sprite.Play("sleep");
		yield return null;
		yield return 2f;
	}

	public override void OnEnd(Level level)
	{
		level.CompleteArea();
	}
}
