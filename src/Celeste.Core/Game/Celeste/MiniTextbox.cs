using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class MiniTextbox : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__18 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniTextbox _003C_003E4__this;

		private string _003CbeginAnim_003E5__2;

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
		public _003CRoutine_003Ed__18(int _003C_003E1__state)
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
			MiniTextbox miniTextbox = _003C_003E4__this;
			float num2;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				List<Entity> entities = miniTextbox.Scene.Tracker.GetEntities<MiniTextbox>();
				foreach (MiniTextbox item in entities)
				{
					if (item != miniTextbox)
					{
						item.Add(new Coroutine(item.Close()));
					}
				}
				if (entities.Count > 0)
				{
					_003C_003E2__current = 0.3f;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00d6;
			}
			case 1:
				_003C_003E1__state = -1;
				goto IL_00d6;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00d6;
			case 3:
				_003C_003E1__state = -1;
				goto IL_016a;
			case 4:
				_003C_003E1__state = -1;
				num2 = 0f;
				goto IL_0298;
			case 5:
				_003C_003E1__state = -1;
				_003C_003E2__current = miniTextbox.Close();
				_003C_003E1__state = 6;
				return true;
			case 6:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_016a:
				if (miniTextbox.portrait.CurrentAnimationID == _003CbeginAnim_003E5__2 && miniTextbox.portrait.Animating)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_018f;
				IL_00d6:
				if ((miniTextbox.ease += Engine.DeltaTime * 4f) < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				miniTextbox.ease = 1f;
				if (miniTextbox.portrait != null)
				{
					_003CbeginAnim_003E5__2 = "begin_" + miniTextbox.portraitData.Animation;
					if (miniTextbox.portrait.Has(_003CbeginAnim_003E5__2))
					{
						miniTextbox.portrait.Play(_003CbeginAnim_003E5__2);
						goto IL_016a;
					}
					goto IL_018f;
				}
				goto IL_0218;
				IL_018f:
				miniTextbox.portrait.Play("talk_" + miniTextbox.portraitData.Animation);
				miniTextbox.talkerSfx = new SoundSource().Play(miniTextbox.portraitData.SfxEvent);
				miniTextbox.talkerSfx.Param("dialogue_portrait", miniTextbox.portraitData.SfxExpression);
				miniTextbox.talkerSfx.Param("dialogue_end", 0f);
				miniTextbox.Add(miniTextbox.talkerSfx);
				_003CbeginAnim_003E5__2 = null;
				goto IL_0218;
				IL_0218:
				num2 = 0f;
				goto IL_0298;
				IL_0298:
				while (miniTextbox.index < miniTextbox.text.Nodes.Count)
				{
					if (miniTextbox.text.Nodes[miniTextbox.index] is FancyText.Char)
					{
						num2 += (miniTextbox.text.Nodes[miniTextbox.index] as FancyText.Char).Delay;
					}
					miniTextbox.index++;
					if (num2 > 0.016f)
					{
						_003C_003E2__current = num2;
						_003C_003E1__state = 4;
						return true;
					}
				}
				if (miniTextbox.portrait != null)
				{
					miniTextbox.portrait.Play("idle_" + miniTextbox.portraitData.Animation);
				}
				if (miniTextbox.talkerSfx != null)
				{
					miniTextbox.talkerSfx.Param("dialogue_portrait", 0f);
					miniTextbox.talkerSfx.Param("dialogue_end", 1f);
				}
				Audio.EndSnapshot(Level.DialogSnapshot);
				_003C_003E2__current = 3f;
				_003C_003E1__state = 5;
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

	[CompilerGenerated]
	private sealed class _003CClose_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniTextbox _003C_003E4__this;

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
		public _003CClose_003Ed__19(int _003C_003E1__state)
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
			MiniTextbox miniTextbox = _003C_003E4__this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
			}
			else
			{
				_003C_003E1__state = -1;
				if (miniTextbox.closing)
				{
					goto IL_0079;
				}
				miniTextbox.closing = true;
			}
			if ((miniTextbox.ease -= Engine.DeltaTime * 4f) > 0f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			miniTextbox.ease = 0f;
			miniTextbox.RemoveSelf();
			goto IL_0079;
			IL_0079:
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

	public const float TextScale = 0.75f;

	public const float BoxWidth = 1688f;

	public const float BoxHeight = 144f;

	public const float HudElementHeight = 180f;

	private int index;

	private FancyText.Text text;

	private MTexture box;

	private float ease;

	private bool closing;

	private Coroutine routine;

	private Sprite portrait;

	private FancyText.Portrait portraitData;

	private float portraitSize;

	private float portraitScale;

	private SoundSource talkerSfx;

	public static bool Displayed
	{
		get
		{
			foreach (MiniTextbox entity in Engine.Scene.Tracker.GetEntities<MiniTextbox>())
			{
				if (!entity.closing && entity.ease > 0.25f)
				{
					return true;
				}
			}
			return false;
		}
	}

	public MiniTextbox(string dialogId)
	{
		base.Tag = (int)Tags.HUD | (int)Tags.TransitionUpdate;
		portraitSize = 112f;
		box = GFX.Portraits["textbox/default_mini"];
		text = FancyText.Parse(Dialog.Get(dialogId.Trim()), (int)(1688f - portraitSize - 32f), 2);
		foreach (FancyText.Node node in text.Nodes)
		{
			if (node is FancyText.Portrait)
			{
				FancyText.Portrait portrait = (portraitData = node as FancyText.Portrait);
				this.portrait = GFX.PortraitsSpriteBank.Create("portrait_" + portrait.Sprite);
				XmlElement xML = GFX.PortraitsSpriteBank.SpriteData["portrait_" + portrait.Sprite].Sources[0].XML;
				portraitScale = portraitSize / xML.AttrFloat("size", 160f);
				string id = "textbox/" + xML.Attr("textbox", "default") + "_mini";
				if (GFX.Portraits.Has(id))
				{
					box = GFX.Portraits[id];
				}
				Add(this.portrait);
			}
		}
		Add(routine = new Coroutine(Routine()));
		routine.UseRawDeltaTime = true;
		Add(new TransitionListener
		{
			OnOutBegin = delegate
			{
				if (!closing)
				{
					routine.Replace(Close());
				}
			}
		});
		if (Level.DialogSnapshot == null)
		{
			Level.DialogSnapshot = Audio.CreateSnapshot("snapshot:/dialogue_in_progress", start: false);
		}
		Audio.ResumeSnapshot(Level.DialogSnapshot);
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__18))]
	private IEnumerator Routine()
	{
		List<Entity> entities = base.Scene.Tracker.GetEntities<MiniTextbox>();
		foreach (MiniTextbox item in entities)
		{
			if (item != this)
			{
				item.Add(new Coroutine(item.Close()));
			}
		}
		if (entities.Count > 0)
		{
			yield return 0.3f;
		}
		while ((ease += Engine.DeltaTime * 4f) < 1f)
		{
			yield return null;
		}
		ease = 1f;
		if (portrait != null)
		{
			string beginAnim = "begin_" + portraitData.Animation;
			if (portrait.Has(beginAnim))
			{
				portrait.Play(beginAnim);
				while (portrait.CurrentAnimationID == beginAnim && portrait.Animating)
				{
					yield return null;
				}
			}
			portrait.Play("talk_" + portraitData.Animation);
			talkerSfx = new SoundSource().Play(portraitData.SfxEvent);
			talkerSfx.Param("dialogue_portrait", portraitData.SfxExpression);
			talkerSfx.Param("dialogue_end", 0f);
			Add(talkerSfx);
		}
		float num = 0f;
		while (index < text.Nodes.Count)
		{
			if (text.Nodes[index] is FancyText.Char)
			{
				num += (text.Nodes[index] as FancyText.Char).Delay;
			}
			index++;
			if (num > 0.016f)
			{
				yield return num;
				num = 0f;
			}
		}
		if (portrait != null)
		{
			portrait.Play("idle_" + portraitData.Animation);
		}
		if (talkerSfx != null)
		{
			talkerSfx.Param("dialogue_portrait", 0f);
			talkerSfx.Param("dialogue_end", 1f);
		}
		Audio.EndSnapshot(Level.DialogSnapshot);
		yield return 3f;
		yield return Close();
	}

	[IteratorStateMachine(typeof(_003CClose_003Ed__19))]
	private IEnumerator Close()
	{
		if (!closing)
		{
			closing = true;
			while ((ease -= Engine.DeltaTime * 4f) > 0f)
			{
				yield return null;
			}
			ease = 0f;
			RemoveSelf();
		}
	}

	public override void Update()
	{
		if ((base.Scene as Level).RetryPlayerCorpse != null && !closing)
		{
			routine.Replace(Close());
		}
		base.Update();
	}

	public override void Render()
	{
		if (ease <= 0f)
		{
			return;
		}
		Level level = base.Scene as Level;
		if (!level.FrozenOrPaused && level.RetryPlayerCorpse == null && !level.SkippingCutscene)
		{
			Vector2 vector = new Vector2(Engine.Width / 2, 72f + ((float)Engine.Width - 1688f) / 4f);
			Vector2 vector2 = vector + new Vector2(-828f, -56f);
			box.DrawCentered(vector, Color.White, new Vector2(1f, ease));
			if (portrait != null)
			{
				portrait.Scale = new Vector2(1f, ease) * portraitScale;
				portrait.RenderPosition = vector2 + new Vector2(portraitSize / 2f, portraitSize / 2f);
				portrait.Render();
			}
			text.Draw(new Vector2(vector2.X + portraitSize + 32f, vector.Y), new Vector2(0f, 0.5f), new Vector2(1f, ease) * 0.75f, 1f, 0, index);
		}
	}

	public override void Removed(Scene scene)
	{
		Audio.EndSnapshot(Level.DialogSnapshot);
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Audio.EndSnapshot(Level.DialogSnapshot);
		base.SceneEnd(scene);
	}
}
