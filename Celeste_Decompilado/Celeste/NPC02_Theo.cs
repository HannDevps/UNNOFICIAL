using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class NPC02_Theo : NPC
{
	[CompilerGenerated]
	private sealed class _003CTalk_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC02_Theo _003C_003E4__this;

		public Player player;

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
		public _003CTalk_003Ed__10(int _003C_003E1__state)
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
			NPC02_Theo nPC02_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (!SaveData.Instance.HasFlag("MetTheo"))
				{
					nPC02_Theo.Session.SetFlag("hadntMetTheoAtStart");
					SaveData.Instance.SetFlag("MetTheo");
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 1;
					return true;
				}
				if (!SaveData.Instance.HasFlag("TheoKnowsName"))
				{
					nPC02_Theo.Session.SetFlag("hadntMetTheoAtStart");
					SaveData.Instance.SetFlag("TheoKnowsName");
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 3;
					return true;
				}
				if (nPC02_Theo.CurrentConversation <= 0)
				{
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player);
					_003C_003E1__state = 5;
					return true;
				}
				if (nPC02_Theo.CurrentConversation == 1)
				{
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 10;
					return true;
				}
				if (nPC02_Theo.CurrentConversation == 2)
				{
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 12;
					return true;
				}
				if (nPC02_Theo.CurrentConversation == 3)
				{
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 14;
					return true;
				}
				if (nPC02_Theo.CurrentConversation == 4)
				{
					_003C_003E2__current = nPC02_Theo.PlayerApproachRightSide(player, turnToFace: true, 48f);
					_003C_003E1__state = 16;
					return true;
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_INTRO_NEVER_MET");
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				break;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_INTRO_NEVER_INTRODUCED");
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				break;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				_003C_003E1__state = -1;
				if (nPC02_Theo.Session.GetFlag("hadntMetTheoAtStart"))
				{
					_003C_003E2__current = nPC02_Theo.PlayerApproach48px();
					_003C_003E1__state = 7;
					return true;
				}
				_003C_003E2__current = Textbox.Say("CH2_THEO_A_EXT", nPC02_Theo.ShowPhotos, nPC02_Theo.HidePhotos, nPC02_Theo.Selfie, nPC02_Theo.PlayerApproach48px);
				_003C_003E1__state = 9;
				return true;
			case 7:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_A", nPC02_Theo.ShowPhotos, nPC02_Theo.HidePhotos, nPC02_Theo.Selfie);
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				break;
			case 9:
				_003C_003E1__state = -1;
				break;
			case 10:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_B", nPC02_Theo.SelfieFiltered);
				_003C_003E1__state = 11;
				return true;
			case 11:
				_003C_003E1__state = -1;
				break;
			case 12:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_C");
				_003C_003E1__state = 13;
				return true;
			case 13:
				_003C_003E1__state = -1;
				break;
			case 14:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_D");
				_003C_003E1__state = 15;
				return true;
			case 15:
				_003C_003E1__state = -1;
				break;
			case 16:
				_003C_003E1__state = -1;
				_003C_003E2__current = Textbox.Say("CH2_THEO_E");
				_003C_003E1__state = 17;
				return true;
			case 17:
				_003C_003E1__state = -1;
				break;
			}
			nPC02_Theo.Level.EndCutscene();
			nPC02_Theo.OnTalkEnd(nPC02_Theo.Level);
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
	private sealed class _003CShowPhotos_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC02_Theo _003C_003E4__this;

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
		public _003CShowPhotos_003Ed__12(int _003C_003E1__state)
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
			NPC02_Theo nPC02_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				Player entity = nPC02_Theo.Scene.Tracker.GetEntity<Player>();
				_003C_003E2__current = nPC02_Theo.PlayerApproach(entity, turnToFace: true, 10f);
				_003C_003E1__state = 1;
				return true;
			}
			case 1:
				_003C_003E1__state = -1;
				nPC02_Theo.Sprite.Play("getPhone");
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
	private sealed class _003CHidePhotos_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC02_Theo _003C_003E4__this;

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
		public _003CHidePhotos_003Ed__13(int _003C_003E1__state)
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
			NPC02_Theo nPC02_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC02_Theo.Sprite.Play("idle");
				_003C_003E2__current = 0.5f;
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
	private sealed class _003CSelfie_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC02_Theo _003C_003E4__this;

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
		public _003CSelfie_003Ed__14(int _003C_003E1__state)
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
			NPC02_Theo nPC02_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/02_old_site/theoselfie_foley", nPC02_Theo.Position);
				nPC02_Theo.Sprite.Scale.X = 0f - nPC02_Theo.Sprite.Scale.X;
				nPC02_Theo.Sprite.Play("takeSelfie");
				_003C_003E2__current = 1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				nPC02_Theo.Scene.Add(nPC02_Theo.selfie = new Selfie(nPC02_Theo.SceneAs<Level>()));
				_003C_003E2__current = nPC02_Theo.selfie.PictureRoutine();
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				nPC02_Theo.selfie = null;
				nPC02_Theo.Sprite.Scale.X = 0f - nPC02_Theo.Sprite.Scale.X;
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
	private sealed class _003CSelfieFiltered_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NPC02_Theo _003C_003E4__this;

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
		public _003CSelfieFiltered_003Ed__15(int _003C_003E1__state)
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
			NPC02_Theo nPC02_Theo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				nPC02_Theo.Scene.Add(nPC02_Theo.selfie = new Selfie(nPC02_Theo.SceneAs<Level>()));
				_003C_003E2__current = nPC02_Theo.selfie.FilterRoutine();
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				nPC02_Theo.selfie = null;
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

	private const string DoneTalking = "theoDoneTalking";

	private const string HadntMetAtStart = "hadntMetTheoAtStart";

	private Coroutine talkRoutine;

	private Selfie selfie;

	private int CurrentConversation
	{
		get
		{
			return base.Session.GetCounter("theo");
		}
		set
		{
			base.Session.SetCounter("theo", value);
		}
	}

	public NPC02_Theo(Vector2 position)
		: base(position)
	{
		Add(Sprite = GFX.SpriteBank.Create("theo"));
		Sprite.Play("idle");
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (!base.Session.GetFlag("theoDoneTalking"))
		{
			Add(Talker = new TalkComponent(new Rectangle(-20, -8, 100, 8), new Vector2(0f, -24f), OnTalk));
		}
	}

	private void OnTalk(Player player)
	{
		if (!SaveData.Instance.HasFlag("MetTheo") || !SaveData.Instance.HasFlag("TheoKnowsName"))
		{
			CurrentConversation = -1;
		}
		Level.StartCutscene(OnTalkEnd);
		Add(talkRoutine = new Coroutine(Talk(player)));
	}

	[IteratorStateMachine(typeof(_003CTalk_003Ed__10))]
	private IEnumerator Talk(Player player)
	{
		if (!SaveData.Instance.HasFlag("MetTheo"))
		{
			base.Session.SetFlag("hadntMetTheoAtStart");
			SaveData.Instance.SetFlag("MetTheo");
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH2_THEO_INTRO_NEVER_MET");
		}
		else if (!SaveData.Instance.HasFlag("TheoKnowsName"))
		{
			base.Session.SetFlag("hadntMetTheoAtStart");
			SaveData.Instance.SetFlag("TheoKnowsName");
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH2_THEO_INTRO_NEVER_INTRODUCED");
		}
		else if (CurrentConversation <= 0)
		{
			yield return PlayerApproachRightSide(player);
			yield return 0.2f;
			if (base.Session.GetFlag("hadntMetTheoAtStart"))
			{
				yield return PlayerApproach48px();
				yield return Textbox.Say("CH2_THEO_A", ShowPhotos, HidePhotos, Selfie);
			}
			else
			{
				yield return Textbox.Say("CH2_THEO_A_EXT", ShowPhotos, HidePhotos, Selfie, base.PlayerApproach48px);
			}
		}
		else if (CurrentConversation == 1)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH2_THEO_B", SelfieFiltered);
		}
		else if (CurrentConversation == 2)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH2_THEO_C");
		}
		else if (CurrentConversation == 3)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH2_THEO_D");
		}
		else if (CurrentConversation == 4)
		{
			yield return PlayerApproachRightSide(player, turnToFace: true, 48f);
			yield return Textbox.Say("CH2_THEO_E");
		}
		Level.EndCutscene();
		OnTalkEnd(Level);
	}

	private void OnTalkEnd(Level level)
	{
		if (CurrentConversation == 4)
		{
			base.Session.SetFlag("theoDoneTalking");
			Remove(Talker);
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			entity.StateMachine.Locked = false;
			entity.StateMachine.State = 0;
			if (level.SkippingCutscene)
			{
				entity.X = (int)(base.X + 48f);
				entity.Facing = Facings.Left;
			}
		}
		Sprite.Scale.X = 1f;
		if (selfie != null)
		{
			selfie.RemoveSelf();
		}
		CurrentConversation++;
		talkRoutine.Cancel();
		talkRoutine.RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CShowPhotos_003Ed__12))]
	private IEnumerator ShowPhotos()
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		yield return PlayerApproach(entity, turnToFace: true, 10f);
		Sprite.Play("getPhone");
		yield return 2f;
	}

	[IteratorStateMachine(typeof(_003CHidePhotos_003Ed__13))]
	private IEnumerator HidePhotos()
	{
		Sprite.Play("idle");
		yield return 0.5f;
	}

	[IteratorStateMachine(typeof(_003CSelfie_003Ed__14))]
	private IEnumerator Selfie()
	{
		yield return 0.5f;
		Audio.Play("event:/game/02_old_site/theoselfie_foley", Position);
		Sprite.Scale.X = 0f - Sprite.Scale.X;
		Sprite.Play("takeSelfie");
		yield return 1f;
		base.Scene.Add(selfie = new Selfie(SceneAs<Level>()));
		yield return selfie.PictureRoutine();
		selfie = null;
		Sprite.Scale.X = 0f - Sprite.Scale.X;
	}

	[IteratorStateMachine(typeof(_003CSelfieFiltered_003Ed__15))]
	private IEnumerator SelfieFiltered()
	{
		base.Scene.Add(selfie = new Selfie(SceneAs<Level>()));
		yield return selfie.FilterRoutine();
		selfie = null;
	}
}
