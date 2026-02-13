using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OverworldLoader : Scene
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public OverworldLoader _003C_003E4__this;

		public Session session;

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
		public _003CRoutine_003Ed__12(int _003C_003E1__state)
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
			OverworldLoader overworldLoader = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if ((overworldLoader.StartMode == Overworld.StartMode.AreaComplete || overworldLoader.StartMode == Overworld.StartMode.AreaQuit) && session != null)
				{
					if (session.UnlockedCSide)
					{
						overworldLoader.showUnlockCSidePostcard = true;
					}
					if (!Settings.Instance.VariantsUnlocked && SaveData.Instance != null && SaveData.Instance.TotalHeartGems >= 24)
					{
						overworldLoader.showVariantPostcard = true;
					}
				}
				if (overworldLoader.showUnlockCSidePostcard)
				{
					_003C_003E2__current = 3f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0121;
			case 1:
				_003C_003E1__state = -1;
				overworldLoader.Add(overworldLoader.postcard = new Postcard(Dialog.Get("POSTCARD_CSIDES"), "event:/ui/main/postcard_csides_in", "event:/ui/main/postcard_csides_out"));
				_003C_003E2__current = overworldLoader.postcard.DisplayRoutine();
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0121;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0121;
			case 4:
				_003C_003E1__state = -1;
				Settings.Instance.VariantsUnlocked = true;
				overworldLoader.Add(overworldLoader.postcard = new Postcard(Dialog.Get("POSTCARD_VARIANTS"), "event:/new_content/ui/postcard_variants_in", "event:/new_content/ui/postcard_variants_out"));
				_003C_003E2__current = overworldLoader.postcard.DisplayRoutine();
				_003C_003E1__state = 5;
				return true;
			case 5:
				_003C_003E1__state = -1;
				UserIO.SaveHandler(file: false, settings: true);
				goto IL_01c9;
			case 6:
				_003C_003E1__state = -1;
				goto IL_01c9;
			case 7:
				{
					_003C_003E1__state = -1;
					goto IL_01e9;
				}
				IL_01c9:
				if (UserIO.Saving)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 6;
					return true;
				}
				goto IL_01e9;
				IL_01e9:
				if (SaveLoadIcon.Instance != null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				break;
				IL_0121:
				if (!overworldLoader.loaded)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				if (overworldLoader.showVariantPostcard)
				{
					_003C_003E2__current = 3f;
					_003C_003E1__state = 4;
					return true;
				}
				break;
			}
			Engine.Scene = overworldLoader.overworld;
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

	public Overworld.StartMode StartMode;

	public HiresSnow Snow;

	private bool loaded;

	private bool fadeIn;

	private Overworld overworld;

	private Postcard postcard;

	private bool showVariantPostcard;

	private bool showUnlockCSidePostcard;

	private Thread activeThread;

	public OverworldLoader(Overworld.StartMode startMode, HiresSnow snow = null)
	{
		StartMode = startMode;
		Snow = ((snow == null) ? new HiresSnow() : snow);
		fadeIn = snow == null;
	}

	public override void Begin()
	{
		Add(new HudRenderer());
		Add(Snow);
		if (fadeIn)
		{
			ScreenWipe.WipeColor = Color.Black;
			new FadeWipe(this, wipeIn: true);
		}
		base.RendererList.UpdateLists();
		Session session = null;
		if (SaveData.Instance != null)
		{
			session = SaveData.Instance.CurrentSession;
		}
		Entity entity = new Entity();
		entity.Add(new Coroutine(Routine(session)));
		Add(entity);
		activeThread = Thread.CurrentThread;
		activeThread.Priority = ThreadPriority.Lowest;
		RunThread.Start(LoadThread, "OVERWORLD_LOADER", highPriority: true);
	}

	private void LoadThread()
	{
		if (!MTN.Loaded)
		{
			MTN.Load();
		}
		if (!MTN.DataLoaded)
		{
			MTN.LoadData();
		}
		CheckVariantsPostcardAtLaunch();
		overworld = new Overworld(this);
		overworld.Entities.UpdateLists();
		loaded = true;
		activeThread.Priority = ThreadPriority.Normal;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__12))]
	private IEnumerator Routine(Session session)
	{
		if ((StartMode == Overworld.StartMode.AreaComplete || StartMode == Overworld.StartMode.AreaQuit) && session != null)
		{
			if (session.UnlockedCSide)
			{
				showUnlockCSidePostcard = true;
			}
			if (!Settings.Instance.VariantsUnlocked && SaveData.Instance != null && SaveData.Instance.TotalHeartGems >= 24)
			{
				showVariantPostcard = true;
			}
		}
		if (showUnlockCSidePostcard)
		{
			yield return 3f;
			Add(postcard = new Postcard(Dialog.Get("POSTCARD_CSIDES"), "event:/ui/main/postcard_csides_in", "event:/ui/main/postcard_csides_out"));
			yield return postcard.DisplayRoutine();
		}
		while (!loaded)
		{
			yield return null;
		}
		if (showVariantPostcard)
		{
			yield return 3f;
			Settings.Instance.VariantsUnlocked = true;
			Add(postcard = new Postcard(Dialog.Get("POSTCARD_VARIANTS"), "event:/new_content/ui/postcard_variants_in", "event:/new_content/ui/postcard_variants_out"));
			yield return postcard.DisplayRoutine();
			UserIO.SaveHandler(file: false, settings: true);
			while (UserIO.Saving)
			{
				yield return null;
			}
			while (SaveLoadIcon.Instance != null)
			{
				yield return null;
			}
		}
		Engine.Scene = overworld;
	}

	public override void BeforeRender()
	{
		base.BeforeRender();
		if (postcard != null)
		{
			postcard.BeforeRender();
		}
	}

	private void CheckVariantsPostcardAtLaunch()
	{
		if (StartMode != Overworld.StartMode.Titlescreen || Settings.Instance.VariantsUnlocked || (Settings.LastVersion != null && !(new Version(Settings.LastVersion) <= new Version(1, 2, 4, 2))) || !UserIO.Open(UserIO.Mode.Read))
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			if (!UserIO.Exists(SaveData.GetFilename(i)))
			{
				continue;
			}
			SaveData saveData = UserIO.Load<SaveData>(SaveData.GetFilename(i));
			if (saveData != null)
			{
				saveData.AfterInitialize();
				if (saveData.TotalHeartGems >= 24)
				{
					showVariantPostcard = true;
					break;
				}
			}
		}
		UserIO.Close();
		SaveData.Instance = null;
	}
}
