using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Overworld : Scene, IOverlayHandler
{
	public enum StartMode
	{
		Titlescreen,
		ReturnFromOptions,
		AreaComplete,
		AreaQuit,
		ReturnFromPico8,
		MainMenu
	}

	private class InputEntity : Entity
	{
		public Overworld Overworld;

		private Wiggler confirmWiggle;

		private Wiggler cancelWiggle;

		private float confirmWiggleDelay;

		private float cancelWiggleDelay;

		public InputEntity(Overworld overworld)
		{
			Overworld = overworld;
			base.Tag = Tags.HUD;
			base.Depth = -100000;
			Add(confirmWiggle = Wiggler.Create(0.4f, 4f));
			Add(cancelWiggle = Wiggler.Create(0.4f, 4f));
		}

		public override void Update()
		{
			if (Input.MenuConfirm.Pressed && confirmWiggleDelay <= 0f)
			{
				confirmWiggle.Start();
				confirmWiggleDelay = 0.5f;
			}
			if (Input.MenuCancel.Pressed && cancelWiggleDelay <= 0f)
			{
				cancelWiggle.Start();
				cancelWiggleDelay = 0.5f;
			}
			confirmWiggleDelay -= Engine.DeltaTime;
			cancelWiggleDelay -= Engine.DeltaTime;
			base.Update();
		}

		public override void Render()
		{
			float inputEase = Overworld.inputEase;
			if (inputEase > 0f)
			{
				float num = 0.5f;
				int num2 = 32;
				string label = Dialog.Clean("ui_cancel");
				string label2 = Dialog.Clean("ui_confirm");
				float num3 = ButtonUI.Width(label, Input.MenuCancel);
				float num4 = ButtonUI.Width(label2, Input.MenuConfirm);
				Vector2 position = new Vector2(1880f, 1024f);
				position.X += (40f + (num4 + num3) * num + (float)num2) * (1f - Ease.CubeOut(inputEase));
				ButtonUI.Render(position, label, Input.MenuCancel, num, 1f, cancelWiggle.Value * 0.05f);
				if (Overworld.ShowConfirmUI)
				{
					position.X -= num * num3 + (float)num2;
					ButtonUI.Render(position, label2, Input.MenuConfirm, num, 1f, confirmWiggle.Value * 0.05f);
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CGotoRoutine_003Ed__31 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Overworld _003C_003E4__this;

		public Oui next;

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
		public _003CGotoRoutine_003Ed__31(int _003C_003E1__state)
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
			Overworld overworld = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0046;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0046;
			case 2:
				_003C_003E1__state = -1;
				if (next.Scene != null)
				{
					_003C_003E2__current = next.Enter(overworld.Last);
					_003C_003E1__state = 3;
					return true;
				}
				break;
			case 3:
				{
					_003C_003E1__state = -1;
					next.Focused = true;
					overworld.Current = next;
					overworld.transitioning = false;
					break;
				}
				IL_0046:
				if (overworld.Current == null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				overworld.transitioning = true;
				overworld.Next = next;
				overworld.Last = overworld.Current;
				overworld.Current = null;
				overworld.Last.Focused = false;
				_003C_003E2__current = overworld.Last.Leave(next);
				_003C_003E1__state = 2;
				return true;
			}
			overworld.Next = null;
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

	public List<Oui> UIs = new List<Oui>();

	public Oui Current;

	public Oui Last;

	public Oui Next;

	public bool EnteringPico8;

	public bool ShowInputUI = true;

	public bool ShowConfirmUI = true;

	private float inputEase;

	public MountainRenderer Mountain;

	public HiresSnow Snow;

	private Snow3D Snow3D;

	public Maddy3D Maddy;

	private Entity routineEntity;

	private bool transitioning;

	private int lastArea = -1;

	public Overlay Overlay { get; set; }

	public Overworld(OverworldLoader loader)
	{
		Add(Mountain = new MountainRenderer());
		Add(new HudRenderer());
		Add(routineEntity = new Entity());
		Add(new InputEntity(this));
		Snow = loader.Snow;
		if (Snow == null)
		{
			Snow = new HiresSnow();
		}
		Add(Snow);
		base.RendererList.UpdateLists();
		Add(Snow3D = new Snow3D(Mountain.Model));
		Add(new MoonParticle3D(Mountain.Model, new Vector3(0f, 31f, 0f)));
		Add(Maddy = new Maddy3D(Mountain));
		ReloadMenus(loader.StartMode);
		Mountain.OnEaseEnd = delegate
		{
			if (Mountain.Area >= 0 && (!Maddy.Show || lastArea != Mountain.Area))
			{
				Maddy.Running(Mountain.Area < 7);
				Maddy.Wiggler.Start();
			}
			lastArea = Mountain.Area;
		};
		lastArea = Mountain.Area;
		if (Mountain.Area < 0)
		{
			Maddy.Hide();
		}
		else
		{
			Maddy.Position = AreaData.Areas[Mountain.Area].MountainCursor;
		}
		Settings.Instance.ApplyVolumes();
	}

	public override void Begin()
	{
		base.Begin();
		SetNormalMusic();
		ScreenWipe.WipeColor = Color.Black;
		new FadeWipe(this, wipeIn: true);
		base.RendererList.UpdateLists();
		if (!EnteringPico8)
		{
			base.RendererList.MoveToFront(Snow);
			base.RendererList.UpdateLists();
		}
		EnteringPico8 = false;
		ReloadMountainStuff();
	}

	public override void End()
	{
		if (!EnteringPico8)
		{
			Mountain.Dispose();
		}
		base.End();
	}

	public void ReloadMenus(StartMode startMode = StartMode.Titlescreen)
	{
		foreach (Oui uI in UIs)
		{
			Remove(uI);
		}
		UIs.Clear();
		Type[] types = Assembly.GetExecutingAssembly().GetTypes();
		foreach (Type type in types)
		{
			if (typeof(Oui).IsAssignableFrom(type) && !type.IsAbstract)
			{
				Oui oui = (Oui)Activator.CreateInstance(type);
				oui.Visible = false;
				Add(oui);
				UIs.Add(oui);
				if (oui.IsStart(this, startMode))
				{
					oui.Visible = true;
					Last = (Current = oui);
				}
			}
		}
	}

	public void SetNormalMusic()
	{
		Audio.SetMusic("event:/music/menu/level_select");
		Audio.SetAmbience("event:/env/amb/worldmap");
	}

	public void ReloadMountainStuff()
	{
		MTN.MountainBird.ReassignVertices();
		MTN.MountainMoon.ReassignVertices();
		MTN.MountainTerrain.ReassignVertices();
		MTN.MountainBuildings.ReassignVertices();
		MTN.MountainCoreWall.ReassignVertices();
		Mountain.Model.DisposeBillboardBuffers();
		Mountain.Model.ResetBillboardBuffers();
	}

	public override void HandleGraphicsReset()
	{
		ReloadMountainStuff();
		base.HandleGraphicsReset();
	}

	public override void Update()
	{
		if (Mountain.Area >= 0 && !Mountain.Animating)
		{
			Vector3 mountainCursor = AreaData.Areas[Mountain.Area].MountainCursor;
			if (mountainCursor != Vector3.Zero)
			{
				Maddy.Position = mountainCursor + new Vector3(0f, (float)Math.Sin(TimeActive * 2f) * 0.02f, 0f);
			}
		}
		if (Overlay != null)
		{
			if (Overlay.XboxOverlay)
			{
				Mountain.Update(this);
				Snow3D.Update();
			}
			Overlay.Update();
			base.Entities.UpdateLists();
			if (Snow != null)
			{
				Snow.Update(this);
			}
		}
		else
		{
			if (!transitioning || !ShowInputUI)
			{
				inputEase = Calc.Approach(inputEase, (ShowInputUI && !Input.GuiInputController()) ? 1 : 0, Engine.DeltaTime * 4f);
			}
			base.Update();
		}
		if (SaveData.Instance != null && SaveData.Instance.LastArea.ID == 10 && 10 <= SaveData.Instance.UnlockedAreas && !IsCurrent<OuiMainMenu>())
		{
			Audio.SetMusicParam("moon", 1f);
		}
		else
		{
			Audio.SetMusicParam("moon", 0f);
		}
		float num = 1f;
		bool flag = false;
		foreach (Renderer renderer in base.RendererList.Renderers)
		{
			if (renderer is ScreenWipe)
			{
				flag = true;
				num = (renderer as ScreenWipe).Duration;
			}
		}
		bool flag2 = (Current is OuiTitleScreen && Next == null) || Next is OuiTitleScreen;
		if (Snow != null)
		{
			Snow.ParticleAlpha = Calc.Approach(Snow.ParticleAlpha, (flag2 || flag || (Overlay != null && !Overlay.XboxOverlay)) ? 1 : 0, Engine.DeltaTime / num);
		}
	}

	public T Goto<T>() where T : Oui
	{
		T uI = GetUI<T>();
		if (uI != null)
		{
			routineEntity.Add(new Coroutine(GotoRoutine(uI)));
		}
		return uI;
	}

	public bool IsCurrent<T>() where T : Oui
	{
		if (Current != null)
		{
			return Current is T;
		}
		return Last is T;
	}

	public T GetUI<T>() where T : Oui
	{
		Oui oui = null;
		foreach (Oui uI in UIs)
		{
			if (uI is T)
			{
				oui = uI;
			}
		}
		return oui as T;
	}

	[IteratorStateMachine(typeof(_003CGotoRoutine_003Ed__31))]
	private IEnumerator GotoRoutine(Oui next)
	{
		while (Current == null)
		{
			yield return null;
		}
		transitioning = true;
		Next = next;
		Last = Current;
		Current = null;
		Last.Focused = false;
		yield return Last.Leave(next);
		if (next.Scene != null)
		{
			yield return next.Enter(Last);
			next.Focused = true;
			Current = next;
			transitioning = false;
		}
		Next = null;
	}
}
