using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class SaveLoadIcon : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__9 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SaveLoadIcon _003C_003E4__this;

		private float _003Ctimer_003E5__2;

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
		public _003CRoutine_003Ed__9(int _003C_003E1__state)
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
			SaveLoadIcon saveLoadIcon = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				saveLoadIcon.icon.Play("start", restart: true);
				saveLoadIcon.icon.Visible = true;
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__2 = 1f;
				goto IL_00c8;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00c8;
			case 3:
				_003C_003E1__state = -1;
				saveLoadIcon.icon.Visible = false;
				_003C_003E2__current = null;
				_003C_003E1__state = 4;
				return true;
			case 4:
				{
					_003C_003E1__state = -1;
					Instance = null;
					saveLoadIcon.RemoveSelf();
					return false;
				}
				IL_00c8:
				if (saveLoadIcon.display)
				{
					_003Ctimer_003E5__2 -= Engine.DeltaTime;
					if (_003Ctimer_003E5__2 <= 0f)
					{
						saveLoadIcon.wiggler.Start();
						_003Ctimer_003E5__2 = 1f;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				saveLoadIcon.icon.Play("end");
				_003C_003E2__current = 0.5f;
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

	public static SaveLoadIcon Instance;

	private bool display = true;

	private Sprite icon;

	private Wiggler wiggler;

	public static bool OnScreen => Instance != null;

	public static void Show(Scene scene)
	{
		if (Instance != null)
		{
			Instance.RemoveSelf();
		}
		scene.Add(Instance = new SaveLoadIcon());
	}

	public static void Hide()
	{
		if (Instance != null)
		{
			Instance.display = false;
		}
	}

	public SaveLoadIcon()
	{
		base.Tag = (int)Tags.HUD | (int)Tags.FrozenUpdate | (int)Tags.PauseUpdate | (int)Tags.Global;
		base.Depth = -1000000;
		Add(icon = GFX.GuiSpriteBank.Create("save"));
		icon.UseRawDeltaTime = true;
		Add(wiggler = Wiggler.Create(0.4f, 4f, delegate(float f)
		{
			icon.Rotation = f * 0.1f;
		}));
		wiggler.UseRawDeltaTime = true;
		Add(new Coroutine(Routine())
		{
			UseRawDeltaTime = true
		});
		icon.Visible = false;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__9))]
	private IEnumerator Routine()
	{
		icon.Play("start", restart: true);
		icon.Visible = true;
		yield return 0.25f;
		float timer = 1f;
		while (display)
		{
			timer -= Engine.DeltaTime;
			if (timer <= 0f)
			{
				wiggler.Start();
				timer = 1f;
			}
			yield return null;
		}
		icon.Play("end");
		yield return 0.5f;
		icon.Visible = false;
		yield return null;
		Instance = null;
		RemoveSelf();
	}

	public override void Render()
	{
		Position = new Vector2(1760f, 920f);
		base.Render();
	}
}
