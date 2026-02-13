using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class LevelEnter : Scene
{
	private class BSideTitle : Entity
	{
		[CompilerGenerated]
		private sealed class _003CEaseIn_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public BSideTitle _003C_003E4__this;

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
			public _003CEaseIn_003Ed__10(int _003C_003E1__state)
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
				BSideTitle bSideTitle = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					bSideTitle.Add(new Coroutine(bSideTitle.FadeTo(0, 1f, 1f)));
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					bSideTitle.Add(new Coroutine(bSideTitle.FadeTo(1, 1f, 1f)));
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 2;
					return true;
				case 2:
					_003C_003E1__state = -1;
					bSideTitle.Add(new Coroutine(bSideTitle.FadeTo(2, 1f, 1f)));
					_003C_003E2__current = 1.8f;
					_003C_003E1__state = 3;
					return true;
				case 3:
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
		private sealed class _003CEaseOut_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public BSideTitle _003C_003E4__this;

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
			public _003CEaseOut_003Ed__11(int _003C_003E1__state)
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
				BSideTitle bSideTitle = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					bSideTitle.Add(new Coroutine(bSideTitle.FadeTo(0, 0f, 1f)));
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					bSideTitle.Add(new Coroutine(bSideTitle.FadeTo(1, 0f, 1f)));
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 2;
					return true;
				case 2:
					_003C_003E1__state = -1;
					bSideTitle.Add(new Coroutine(bSideTitle.FadeTo(2, 0f, 1f)));
					_003C_003E2__current = 1f;
					_003C_003E1__state = 3;
					return true;
				case 3:
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
		private sealed class _003CFadeTo_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float target;

			public BSideTitle _003C_003E4__this;

			public int index;

			public float duration;

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
			public _003CFadeTo_003Ed__12(int _003C_003E1__state)
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
				BSideTitle bSideTitle = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				}
				if ((bSideTitle.fade[index] = Calc.Approach(bSideTitle.fade[index], target, Engine.DeltaTime / duration)) != target)
				{
					if (target == 0f)
					{
						bSideTitle.offsets[index] = Ease.CubeIn(1f - bSideTitle.fade[index]) * 32f;
					}
					else
					{
						bSideTitle.offsets[index] = (0f - Ease.CubeIn(1f - bSideTitle.fade[index])) * 32f;
					}
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

		private string title;

		private string musicBy;

		private string artist;

		private MTexture artistImage;

		private string album;

		private float musicByWidth;

		private float[] fade = new float[3];

		private float[] offsets = new float[3];

		private float offset;

		public BSideTitle(Session session)
		{
			base.Tag = Tags.HUD;
			switch (session.Area.ID)
			{
			case 1:
				artist = Credits.Remixers[0];
				break;
			case 2:
				artist = Credits.Remixers[1];
				break;
			case 3:
				artist = Credits.Remixers[2];
				break;
			case 4:
				artist = Credits.Remixers[3];
				break;
			case 5:
				artist = Credits.Remixers[4];
				break;
			case 6:
				artist = Credits.Remixers[5];
				break;
			case 7:
				artist = Credits.Remixers[6];
				break;
			case 9:
				artist = Credits.Remixers[7];
				break;
			}
			if (artist.StartsWith("image:"))
			{
				artistImage = GFX.Gui[artist.Substring(6)];
			}
			title = Dialog.Get(AreaData.Get(session).Name) + " " + Dialog.Get(AreaData.Get(session).Name + "_remix");
			musicBy = Dialog.Get("remix_by") + " ";
			musicByWidth = ActiveFont.Measure(musicBy).X;
			album = Dialog.Get("remix_album");
		}

		[IteratorStateMachine(typeof(_003CEaseIn_003Ed__10))]
		public IEnumerator EaseIn()
		{
			Add(new Coroutine(FadeTo(0, 1f, 1f)));
			yield return 0.2f;
			Add(new Coroutine(FadeTo(1, 1f, 1f)));
			yield return 0.2f;
			Add(new Coroutine(FadeTo(2, 1f, 1f)));
			yield return 1.8f;
		}

		[IteratorStateMachine(typeof(_003CEaseOut_003Ed__11))]
		public IEnumerator EaseOut()
		{
			Add(new Coroutine(FadeTo(0, 0f, 1f)));
			yield return 0.2f;
			Add(new Coroutine(FadeTo(1, 0f, 1f)));
			yield return 0.2f;
			Add(new Coroutine(FadeTo(2, 0f, 1f)));
			yield return 1f;
		}

		[IteratorStateMachine(typeof(_003CFadeTo_003Ed__12))]
		private IEnumerator FadeTo(int index, float target, float duration)
		{
			while ((fade[index] = Calc.Approach(fade[index], target, Engine.DeltaTime / duration)) != target)
			{
				if (target == 0f)
				{
					offsets[index] = Ease.CubeIn(1f - fade[index]) * 32f;
				}
				else
				{
					offsets[index] = (0f - Ease.CubeIn(1f - fade[index])) * 32f;
				}
				yield return null;
			}
		}

		public override void Update()
		{
			base.Update();
			offset += Engine.DeltaTime * 32f;
		}

		public override void Render()
		{
			Vector2 vector = new Vector2(60f + offset, 800f);
			ActiveFont.Draw(title, vector + new Vector2(offsets[0], 0f), Color.White * fade[0]);
			ActiveFont.Draw(musicBy, vector + new Vector2(offsets[1], 60f), Color.White * fade[1]);
			if (artistImage != null)
			{
				artistImage.Draw(vector + new Vector2(musicByWidth + offsets[1], 68f), Vector2.Zero, Color.White * fade[1]);
			}
			else
			{
				ActiveFont.Draw(artist, vector + new Vector2(musicByWidth + offsets[1], 60f), Color.White * fade[1]);
			}
			ActiveFont.Draw(album, vector + new Vector2(offsets[2], 120f), Color.White * fade[2]);
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LevelEnter _003C_003E4__this;

		private int _003Carea_003E5__2;

		private BSideTitle _003Ctitle_003E5__3;

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
		public _003CRoutine_003Ed__5(int _003C_003E1__state)
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
			LevelEnter levelEnter = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Carea_003E5__2 = -1;
				if (levelEnter.session.StartedFromBeginning && !levelEnter.fromSaveData && levelEnter.session.Area.Mode == AreaMode.Normal && (!SaveData.Instance.Areas[levelEnter.session.Area.ID].Modes[0].Completed || SaveData.Instance.DebugMode) && levelEnter.session.Area.ID >= 1 && levelEnter.session.Area.ID <= 6)
				{
					_003Carea_003E5__2 = levelEnter.session.Area.ID;
				}
				if (_003Carea_003E5__2 >= 0)
				{
					_003C_003E2__current = 1f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0161;
			case 1:
				_003C_003E1__state = -1;
				levelEnter.Add(levelEnter.postcard = new Postcard(Dialog.Get("postcard_area_" + _003Carea_003E5__2), _003Carea_003E5__2));
				_003C_003E2__current = levelEnter.postcard.DisplayRoutine();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0161;
			case 3:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = _003Ctitle_003E5__3.EaseOut();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					_003Ctitle_003E5__3 = null;
					break;
				}
				IL_0161:
				if (levelEnter.session.StartedFromBeginning && !levelEnter.fromSaveData && levelEnter.session.Area.Mode == AreaMode.BSide)
				{
					_003Ctitle_003E5__3 = new BSideTitle(levelEnter.session);
					levelEnter.Add(_003Ctitle_003E5__3);
					Audio.Play("event:/ui/main/bside_intro_text");
					_003C_003E2__current = _003Ctitle_003E5__3.EaseIn();
					_003C_003E1__state = 3;
					return true;
				}
				break;
			}
			Input.SetLightbarColor(AreaData.Get(levelEnter.session.Area).TitleBaseColor);
			Engine.Scene = new LevelLoader(levelEnter.session);
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

	private Session session;

	private Postcard postcard;

	private bool fromSaveData;

	public static void Go(Session session, bool fromSaveData)
	{
		HiresSnow snow = null;
		if (Engine.Scene is Overworld)
		{
			snow = (Engine.Scene as Overworld).Snow;
		}
		bool flag = !fromSaveData && session.StartedFromBeginning;
		if (flag && session.Area.ID == 0)
		{
			Engine.Scene = new IntroVignette(session, snow);
		}
		else if (flag && session.Area.ID == 7 && session.Area.Mode == AreaMode.Normal)
		{
			Engine.Scene = new SummitVignette(session);
		}
		else if (flag && session.Area.ID == 9 && session.Area.Mode == AreaMode.Normal)
		{
			Engine.Scene = new CoreVignette(session, snow);
		}
		else
		{
			Engine.Scene = new LevelEnter(session, fromSaveData);
		}
	}

	private LevelEnter(Session session, bool fromSaveData)
	{
		this.session = session;
		this.fromSaveData = fromSaveData;
		Add(new Entity
		{
			new Coroutine(Routine())
		});
		Add(new HudRenderer());
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__5))]
	private IEnumerator Routine()
	{
		int area = -1;
		if (session.StartedFromBeginning && !fromSaveData && session.Area.Mode == AreaMode.Normal && (!SaveData.Instance.Areas[session.Area.ID].Modes[0].Completed || SaveData.Instance.DebugMode) && session.Area.ID >= 1 && session.Area.ID <= 6)
		{
			area = session.Area.ID;
		}
		if (area >= 0)
		{
			yield return 1f;
			Add(postcard = new Postcard(Dialog.Get("postcard_area_" + area), area));
			yield return postcard.DisplayRoutine();
		}
		if (session.StartedFromBeginning && !fromSaveData && session.Area.Mode == AreaMode.BSide)
		{
			BSideTitle title = new BSideTitle(session);
			Add(title);
			Audio.Play("event:/ui/main/bside_intro_text");
			yield return title.EaseIn();
			yield return 0.25f;
			yield return title.EaseOut();
			yield return 0.25f;
		}
		Input.SetLightbarColor(AreaData.Get(session.Area).TitleBaseColor);
		Engine.Scene = new LevelLoader(session);
	}

	public override void BeforeRender()
	{
		base.BeforeRender();
		if (postcard != null)
		{
			postcard.BeforeRender();
		}
	}
}
