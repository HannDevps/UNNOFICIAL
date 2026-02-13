using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class CS03_Memo : CutsceneEntity
{
	private class MemoPage : Entity
	{
		[CompilerGenerated]
		private sealed class _003CEaseIn_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MemoPage _003C_003E4__this;

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
			public _003CEaseIn_003Ed__14(int _003C_003E1__state)
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
				MemoPage memoPage = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					Audio.Play("event:/game/03_resort/memo_in");
					_003Cfrom_003E5__2 = new Vector2(Engine.Width / 2, Engine.Height + 100);
					_003Cto_003E5__3 = new Vector2(Engine.Width / 2, Engine.Height / 2 - 150);
					_003CrFrom_003E5__4 = -0.1f;
					_003CrTo_003E5__5 = 0.05f;
					_003Cp_003E5__6 = 0f;
					break;
				case 1:
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime;
					break;
				}
				if (_003Cp_003E5__6 < 1f)
				{
					memoPage.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeOut(_003Cp_003E5__6);
					memoPage.alpha = Ease.CubeOut(_003Cp_003E5__6);
					memoPage.rotation = _003CrFrom_003E5__4 + (_003CrTo_003E5__5 - _003CrFrom_003E5__4) * Ease.CubeOut(_003Cp_003E5__6);
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
		private sealed class _003CWait_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MemoPage _003C_003E4__this;

			private float _003Cstart_003E5__2;

			private int _003Cindex_003E5__3;

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
			public _003CWait_003Ed__15(int _003C_003E1__state)
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
				MemoPage memoPage = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Cstart_003E5__2 = memoPage.Position.Y;
					_003Cindex_003E5__3 = 0;
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				}
				if (!Input.MenuCancel.Pressed)
				{
					float num2 = _003Cstart_003E5__2 - (float)(_003Cindex_003E5__3 * 400);
					memoPage.Position.Y += (num2 - memoPage.Position.Y) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
					if (Input.MenuUp.Pressed && _003Cindex_003E5__3 > 0)
					{
						_003Cindex_003E5__3--;
					}
					else if (_003Cindex_003E5__3 < 2)
					{
						if ((Input.MenuDown.Pressed && !Input.MenuDown.Repeating) || Input.MenuConfirm.Pressed)
						{
							_003Cindex_003E5__3++;
						}
					}
					else if (Input.MenuConfirm.Pressed)
					{
						goto IL_0123;
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0123;
				IL_0123:
				Audio.Play("event:/ui/main/button_lowkey");
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
		private sealed class _003CEaseOut_003Ed__16 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MemoPage _003C_003E4__this;

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
			public _003CEaseOut_003Ed__16(int _003C_003E1__state)
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
				MemoPage memoPage = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					Audio.Play("event:/game/03_resort/memo_out");
					memoPage.easingOut = true;
					_003Cfrom_003E5__2 = memoPage.Position;
					_003Cto_003E5__3 = new Vector2(Engine.Width / 2, -memoPage.target.Height);
					_003CrFrom_003E5__4 = memoPage.rotation;
					_003CrTo_003E5__5 = memoPage.rotation + 0.1f;
					_003Cp_003E5__6 = 0f;
					break;
				case 1:
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime * 1.5f;
					break;
				}
				if (_003Cp_003E5__6 < 1f)
				{
					memoPage.Position = _003Cfrom_003E5__2 + (_003Cto_003E5__3 - _003Cfrom_003E5__2) * Ease.CubeIn(_003Cp_003E5__6);
					memoPage.alpha = 1f - Ease.CubeIn(_003Cp_003E5__6);
					memoPage.rotation = _003CrFrom_003E5__4 + (_003CrTo_003E5__5 - _003CrFrom_003E5__4) * Ease.CubeIn(_003Cp_003E5__6);
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				memoPage.RemoveSelf();
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

		private const float TextScale = 0.75f;

		private const float PaperScale = 1.5f;

		private Atlas atlas;

		private MTexture paper;

		private MTexture title;

		private VirtualRenderTarget target;

		private FancyText.Text text;

		private float textDownscale = 1f;

		private float alpha = 1f;

		private float scale = 1f;

		private float rotation;

		private float timer;

		private bool easingOut;

		public MemoPage()
		{
			base.Tag = Tags.HUD;
			atlas = Atlas.FromAtlas(Path.Combine("Graphics", "Atlases", "Memo"), Atlas.AtlasDataFormat.Packer);
			paper = atlas["memo"];
			if (atlas.Has("title_" + Settings.Instance.Language))
			{
				title = atlas["title_" + Settings.Instance.Language];
			}
			else
			{
				title = atlas["title_english"];
			}
			float num = (float)paper.Width * 1.5f - 120f;
			text = FancyText.Parse(Dialog.Get("CH3_MEMO"), (int)(num / 0.75f), -1, 1f, Color.Black * 0.6f);
			float num2 = text.WidestLine() * 0.75f;
			if (num2 > num)
			{
				textDownscale = num / num2;
			}
			Add(new BeforeRenderHook(BeforeRender));
		}

		[IteratorStateMachine(typeof(_003CEaseIn_003Ed__14))]
		public IEnumerator EaseIn()
		{
			Audio.Play("event:/game/03_resort/memo_in");
			Vector2 from = new Vector2(Engine.Width / 2, Engine.Height + 100);
			Vector2 to = new Vector2(Engine.Width / 2, Engine.Height / 2 - 150);
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

		[IteratorStateMachine(typeof(_003CWait_003Ed__15))]
		public IEnumerator Wait()
		{
			float start = Position.Y;
			int index = 0;
			while (!Input.MenuCancel.Pressed)
			{
				float num = start - (float)(index * 400);
				Position.Y += (num - Position.Y) * (1f - (float)Math.Pow(0.009999999776482582, Engine.DeltaTime));
				if (Input.MenuUp.Pressed && index > 0)
				{
					index--;
				}
				else if (index < 2)
				{
					if ((Input.MenuDown.Pressed && !Input.MenuDown.Repeating) || Input.MenuConfirm.Pressed)
					{
						index++;
					}
				}
				else if (Input.MenuConfirm.Pressed)
				{
					break;
				}
				yield return null;
			}
			Audio.Play("event:/ui/main/button_lowkey");
		}

		[IteratorStateMachine(typeof(_003CEaseOut_003Ed__16))]
		public IEnumerator EaseOut()
		{
			Audio.Play("event:/game/03_resort/memo_out");
			easingOut = true;
			Vector2 from = Position;
			Vector2 to = new Vector2(Engine.Width / 2, -target.Height);
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
				target = VirtualContent.CreateRenderTarget("oshiro-memo", (int)((float)paper.Width * 1.5f), (int)((float)paper.Height * 1.5f));
			}
			Engine.Graphics.GraphicsDevice.SetRenderTarget(target);
			Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			paper.Draw(Vector2.Zero, Vector2.Zero, Color.White, 1.5f);
			title.Draw(Vector2.Zero, Vector2.Zero, Color.White, 1.5f);
			text.Draw(new Vector2((float)paper.Width * 1.5f / 2f, 210f), new Vector2(0.5f, 0f), Vector2.One * 0.75f * textDownscale, 1f);
			Draw.SpriteBatch.End();
		}

		public override void Removed(Scene scene)
		{
			if (target != null)
			{
				target.Dispose();
			}
			target = null;
			atlas.Dispose();
			base.Removed(scene);
		}

		public override void SceneEnd(Scene scene)
		{
			if (target != null)
			{
				target.Dispose();
			}
			target = null;
			atlas.Dispose();
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
				Draw.SpriteBatch.Draw((RenderTarget2D)target, Position, target.Bounds, Color.White * alpha, rotation, new Vector2(target.Width, 0f) / 2f, scale, SpriteEffects.None, 0f);
				if (!easingOut)
				{
					GFX.Gui["textboxbutton"].DrawCentered(Position + new Vector2(target.Width / 2 + 40, target.Height + ((timer % 1f < 0.25f) ? 6 : 0)));
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__5 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CS03_Memo _003C_003E4__this;

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
			CS03_Memo cS03_Memo = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				cS03_Memo.player.StateMachine.State = 11;
				cS03_Memo.player.StateMachine.Locked = true;
				if (!cS03_Memo.Level.Session.GetFlag("memo_read"))
				{
					_003C_003E2__current = Textbox.Say("ch3_memo_opening");
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
				_003C_003E2__current = cS03_Memo.memo.Wait();
				_003C_003E1__state = 4;
				return true;
			case 4:
				_003C_003E1__state = -1;
				_003C_003E2__current = cS03_Memo.memo.EaseOut();
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					cS03_Memo.memo = null;
					cS03_Memo.EndCutscene(cS03_Memo.Level);
					return false;
				}
				IL_00b5:
				cS03_Memo.memo = new MemoPage();
				cS03_Memo.Scene.Add(cS03_Memo.memo);
				_003C_003E2__current = cS03_Memo.memo.EaseIn();
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

	private const string ReadOnceFlag = "memo_read";

	private Player player;

	private MemoPage memo;

	public CS03_Memo(Player player)
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
		if (!Level.Session.GetFlag("memo_read"))
		{
			yield return Textbox.Say("ch3_memo_opening");
			yield return 0.1f;
		}
		memo = new MemoPage();
		base.Scene.Add(memo);
		yield return memo.EaseIn();
		yield return memo.Wait();
		yield return memo.EaseOut();
		memo = null;
		EndCutscene(Level);
	}

	public override void OnEnd(Level level)
	{
		player.StateMachine.Locked = false;
		player.StateMachine.State = 0;
		level.Session.SetFlag("memo_read");
		if (memo != null)
		{
			memo.RemoveSelf();
		}
	}
}
