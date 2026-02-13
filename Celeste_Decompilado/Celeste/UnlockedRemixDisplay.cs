using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class UnlockedRemixDisplay : Entity
{
	[CompilerGenerated]
	private sealed class _003CDisplayRoutine_003Ed__17 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UnlockedRemixDisplay _003C_003E4__this;

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
		public _003CDisplayRoutine_003Ed__17(int _003C_003E1__state)
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
			UnlockedRemixDisplay CS_0024_003C_003E8__locals11 = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				CS_0024_003C_003E8__locals11.strawberries = CS_0024_003C_003E8__locals11.Scene.Entities.FindFirst<TotalStrawberriesDisplay>();
				CS_0024_003C_003E8__locals11.Visible = true;
				goto IL_0063;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0063;
			case 2:
				_003C_003E1__state = -1;
				break;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0063:
				if ((CS_0024_003C_003E8__locals11.drawLerp += Engine.DeltaTime * 1.2f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				CS_0024_003C_003E8__locals11.Add(CS_0024_003C_003E8__locals11.wiggler = Wiggler.Create(0.8f, 4f, delegate(float f)
				{
					CS_0024_003C_003E8__locals11.rotation = f * 0.1f;
				}, start: true));
				CS_0024_003C_003E8__locals11.drawLerp = 1f;
				_003C_003E2__current = 4f;
				_003C_003E1__state = 2;
				return true;
			}
			if ((CS_0024_003C_003E8__locals11.drawLerp -= Engine.DeltaTime * 2f) > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			CS_0024_003C_003E8__locals11.Visible = false;
			CS_0024_003C_003E8__locals11.RemoveSelf();
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

	private const float DisplayDuration = 4f;

	private const float LerpInSpeed = 1.2f;

	private const float LerpOutSpeed = 2f;

	private const float IconSize = 128f;

	private const float Spacing = 20f;

	private string text;

	private float drawLerp;

	private MTexture bg;

	private MTexture icon;

	private float rotation;

	private bool unlockedRemix;

	private TotalStrawberriesDisplay strawberries;

	private Wiggler wiggler;

	private bool hasCassetteAlready;

	public UnlockedRemixDisplay()
	{
		base.Tag = (int)Tags.HUD | (int)Tags.Global | (int)Tags.PauseUpdate | (int)Tags.TransitionUpdate;
		bg = GFX.Gui["strawberryCountBG"];
		icon = GFX.Gui["collectables/cassette"];
		text = Dialog.Clean("ui_remix_unlocked");
		Visible = false;
		base.Y = 96f;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		hasCassetteAlready = SaveData.Instance.Areas[AreaData.Get(base.Scene).ID].Cassette;
		unlockedRemix = (scene as Level).Session.Cassette;
	}

	public override void Update()
	{
		base.Update();
		if (!unlockedRemix && (base.Scene as Level).Session.Cassette)
		{
			unlockedRemix = true;
			Add(new Coroutine(DisplayRoutine()));
		}
		if (Visible)
		{
			float num = 96f;
			if (Settings.Instance.SpeedrunClock == SpeedrunType.Chapter)
			{
				num += 58f;
			}
			else if (Settings.Instance.SpeedrunClock == SpeedrunType.File)
			{
				num += 78f;
			}
			if (strawberries.Visible)
			{
				num += 96f;
			}
			base.Y = Calc.Approach(base.Y, num, Engine.DeltaTime * 800f);
		}
	}

	[IteratorStateMachine(typeof(_003CDisplayRoutine_003Ed__17))]
	private IEnumerator DisplayRoutine()
	{
		strawberries = base.Scene.Entities.FindFirst<TotalStrawberriesDisplay>();
		Visible = true;
		while ((drawLerp += Engine.DeltaTime * 1.2f) < 1f)
		{
			yield return null;
		}
		Add(wiggler = Wiggler.Create(0.8f, 4f, delegate(float f)
		{
			rotation = f * 0.1f;
		}, start: true));
		drawLerp = 1f;
		yield return 4f;
		while ((drawLerp -= Engine.DeltaTime * 2f) > 0f)
		{
			yield return null;
		}
		Visible = false;
		RemoveSelf();
	}

	public override void Render()
	{
		float num = 0f;
		num = ((!hasCassetteAlready) ? (ActiveFont.Measure(text).X + 128f + 80f) : 188f);
		Vector2 vector = Vector2.Lerp(new Vector2(0f - num, base.Y), new Vector2(0f, base.Y), Ease.CubeOut(drawLerp));
		bg.DrawJustified(vector + new Vector2(num, 0f), new Vector2(1f, 0.5f));
		Draw.Rect(vector.X, vector.Y - (float)(bg.Height / 2), num - (float)bg.Width + 1f, bg.Height, Color.Black);
		float num2 = 128f / (float)icon.Width;
		icon.DrawJustified(vector + new Vector2(20f + (float)icon.Width * num2 * 0.5f, 0f), new Vector2(0.5f, 0.5f), Color.White, num2, rotation);
		if (!hasCassetteAlready)
		{
			ActiveFont.DrawOutline(text, vector + new Vector2(168f, 0f), new Vector2(0f, 0.6f), Vector2.One, Color.White, 2f, Color.Black);
		}
	}
}
