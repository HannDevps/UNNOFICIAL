using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class CS02_Journal : CutsceneEntity
{
	private class PoemPage : Entity
	{
		[CompilerGenerated]
		private sealed class _003CEaseIn_003Ed__10 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PoemPage _003C_003E4__this;

			private Vector2 _003Cfrom_003E5__2;

			private Vector2 _003Cto_003E5__3;

			private float _003CrFrom_003E5__4;

			private float _003CrTo_003E5__5;

			private float _003Cp_003E5__6;

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
				PoemPage poemPage = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
				{
					_003C_003E1__state = -1;
					Audio.Play("event:/game/03_resort/memo_in");
					Vector2 vector = new Vector2(Engine.Width, Engine.Height) / 2f;
					_003Cfrom_003E5__2 = vector + new Vector2(0f, 200f);
					_003Cto_003E5__3 = vector;
					_003CrFrom_003E5__4 = -0.1f;
					_003CrTo_003E5__5 = 0.05f;
					_003Cp_003E5__6 = 0f;
					break;
				}
				case 1:
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime;
					break;
				}
				if (_003Cp_003E5__6 < 1f)
				{
					poemPage.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeOut(_003Cp_003E5__6);
					poemPage.alpha = Ease.CubeOut(_003Cp_003E5__6);
					poemPage.rotation = _003CrFrom_003E5__4 + (_003CrTo_003E5__5 - _003CrFrom_003E5__4) * Ease.CubeOut(_003Cp_003E5__6);
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

		[CompilerGenerated]
		private sealed class _003CEaseOut_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PoemPage _003C_003E4__this;

			private Vector2 _003Cfrom_003E5__2;

			private Vector2 _003Cto_003E5__3;

			private float _003CrFrom_003E5__4;

			private float _003CrTo_003E5__5;

			private float _003Cp_003E5__6;

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
				PoemPage poemPage = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					Audio.Play("event:/game/03_resort/memo_out");
					poemPage.easingOut = true;
					_003Cfrom_003E5__2 = poemPage.Position;
					_003Cto_003E5__3 = new Vector2(Engine.Width, Engine.Height) / 2f + new Vector2(0f, -200f);
					_003CrFrom_003E5__4 = poemPage.rotation;
					_003CrTo_003E5__5 = poemPage.rotation + 0.1f;
					_003Cp_003E5__6 = 0f;
					break;
				case 1:
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime * 1.5f;
					break;
				}
				if (_003Cp_003E5__6 < 1f)
				{
					poemPage.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeIn(_003Cp_003E5__6);
					poemPage.alpha = 1f - Ease.CubeIn(_003Cp_003E5__6);
					poemPage.rotation = _003CrFrom_003E5__4 + (_003CrTo_003E5__5 - _003CrFrom_003E5__4) * Ease.CubeIn(_003Cp_003E5__6);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				poemPage.RemoveSelf();
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

		private const float TextScale = 0.7f;

		private MTexture paper;

		private VirtualRenderTarget target;

		private FancyText.Text text;

		private float alpha = 1f;

		private float scale = 1f;

		private float rotation;

		private float timer;

		private bool easingOut;

		public PoemPage()
		{
			base.Tag = Tags.HUD;
			paper = GFX.Gui["poempage"];
			text = FancyText.Parse(Dialog.Get("CH2_POEM"), (int)((float)(paper.Width - 120) / 0.7f), -1, 1f, Color.Black * 0.6f);
			Add(new BeforeRenderHook(BeforeRender));
		}

		[IteratorStateMachine(typeof(_003CEaseIn_003Ed__10))]
		public IEnumerator EaseIn()
		{
			Audio.Play("event:/game/03_resort/memo_in");
			Vector2 vector = new Vector2(Engine.Width, Engine.Height) / 2f;
			Vector2 from = vector + new Vector2(0f, 200f);
			Vector2 to = vector;
			float rFrom = -0.1f;
			float rTo = 0.05f;
			for (float p = 0f; p < 1f; p += Engine.DeltaTime)
			{
				Position = from + (to - from) * Ease.CubeOut(p);
				alpha = Ease.CubeOut(p);
				rotation = rFrom + (rTo - rFrom) * Ease.CubeOut(p);
				yield return null;
			}
		}

		[IteratorStateMachine(typeof(_003CEaseOut_003Ed__11))]
		public IEnumerator EaseOut()
		{
			Audio.Play("event:/game/03_resort/memo_out");
			easingOut = true;
			Vector2 from = Position;
			Vector2 to = new Vector2(Engine.Width, Engine.Height) / 2f + new Vector2(0f, -200f);
			float rFrom = rotation;
			float rTo = rotation + 0.1f;
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 1.5f)
			{
				Position = from + (to - from) * Ease.CubeIn(p);
				alpha = 1f - Ease.CubeIn(p);
				rotation = rFrom + (rTo - rFrom) * Ease.CubeIn(p);
				yield return null;
			}
			RemoveSelf();
		}

		public void BeforeRender()
		{
			if (target == null)
			{
				target = VirtualContent.CreateRenderTarget("journal-poem", paper.Width, paper.Height);
			}
			Engine.Graphics.GraphicsDevice.SetRenderTarget(target);
			Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			paper.Draw(Vector2.Zero);
			text.DrawJustifyPerLine(new Vector2(paper.Width, paper.Height) / 2f, new Vector2(0.5f, 0.5f), Vector2.One * 0.7f, 1f);
			Draw.SpriteBatch.End();
		}

		public override void Removed(Scene scene)
		{
			if (target != null)
			{
				target.Dispose();
			}
			target = null;
			base.Removed(scene);
		}

		public override void SceneEnd(Scene scene)
		{
			if (target != null)
			{
				target.Dispose();
			}
			target = null;
			base.SceneEnd(scene);
		}

		public override void Update()
		{
			timer += Engine.DeltaTime;
			base.Update();
		}

		public override void Render()
		{
			if ((!(base.Scene is Level level) || (!level.FrozenOrPaused && level.RetryPlayerCorpse == null && !level.SkippingCutscene)) && target != null)
			{
				Draw.SpriteBatch.Draw((RenderTarget2D)target, Position, target.Bounds, Color.White * alpha, rotation, new Vector2(target.Width, target.Height) / 2f, scale, SpriteEffects.None, 0f);
				if (!easingOut)
				{
					GFX.Gui["textboxbutton"].DrawCentered(Position + new Vector2(target.Width / 2 + 40, target.Height / 2 + ((timer % 1f < 0.25f) ? 6 : 0)));
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS02_Journal _003C_003E4__this;

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
			CS02_Journal cS02_Journal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS02_Journal.player.StateMachine.State = 11;
				cS02_Journal.player.StateMachine.Locked = true;
				if (!cS02_Journal.Level.Session.GetFlag("poem_read"))
				{
					_003C_003E2__current = Textbox.Say("ch2_journal");
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_00b5;
			case 1:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00b5;
			case 3:
				_003C_003E1__state = -1;
				goto IL_010b;
			case 4:
				_003C_003E1__state = -1;
				goto IL_010b;
			case 5:
				{
					_003C_003E1__state = -1;
					cS02_Journal.poem = null;
					cS02_Journal.EndCutscene(cS02_Journal.Level);
					return false;
				}
				IL_010b:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				Audio.Play("event:/ui/main/button_lowkey");
				_003C_003E2__current = cS02_Journal.poem.EaseOut();
				_003C_003E1__state = 5;
				return true;
				IL_00b5:
				cS02_Journal.poem = new PoemPage();
				cS02_Journal.Scene.Add(cS02_Journal.poem);
				_003C_003E2__current = cS02_Journal.poem.EaseIn();
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

	private const string ReadOnceFlag = "poem_read";

	private Player player;

	private PoemPage poem;

	public CS02_Journal(Player player)
	{
		this.player = player;
	}

	public override void OnBegin(Level level)
	{
		Add(new Coroutine(Routine()));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__5))]
	private IEnumerator Routine()
	{
		player.StateMachine.State = 11;
		player.StateMachine.Locked = true;
		if (!Level.Session.GetFlag("poem_read"))
		{
			yield return Textbox.Say("ch2_journal");
			yield return 0.1f;
		}
		poem = new PoemPage();
		base.Scene.Add(poem);
		yield return poem.EaseIn();
		while (!Input.MenuConfirm.Pressed)
		{
			yield return null;
		}
		Audio.Play("event:/ui/main/button_lowkey");
		yield return poem.EaseOut();
		poem = null;
		EndCutscene(Level);
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		level.Session.SetFlag("poem_read");
		if (poem != null)
		{
			poem.RemoveSelf();
		}
	}
}
