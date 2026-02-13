using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class WaveDashPresentation : Entity
{
	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__33 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPresentation _003C_003E4__this;

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
		public _003CRoutine_003Ed__33(int _003C_003E1__state)
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
			WaveDashPresentation waveDashPresentation = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0052;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0052;
			case 2:
				_003C_003E1__state = -1;
				goto IL_014f;
			case 3:
				_003C_003E1__state = -1;
				if (!waveDashPresentation.CurrPage.AutoProgress)
				{
					waveDashPresentation.waitingForPageTurn = true;
					goto IL_01b6;
				}
				goto IL_01d4;
			case 4:
				_003C_003E1__state = -1;
				goto IL_01b6;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0273;
			case 6:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0052:
				if (waveDashPresentation.loading)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				waveDashPresentation.pages.Add(new WaveDashPage00());
				waveDashPresentation.pages.Add(new WaveDashPage01());
				waveDashPresentation.pages.Add(new WaveDashPage02());
				waveDashPresentation.pages.Add(new WaveDashPage03());
				waveDashPresentation.pages.Add(new WaveDashPage04());
				waveDashPresentation.pages.Add(new WaveDashPage05());
				waveDashPresentation.pages.Add(new WaveDashPage06());
				foreach (WaveDashPage page in waveDashPresentation.pages)
				{
					page.Added(waveDashPresentation);
				}
				waveDashPresentation.Add(new BeforeRenderHook(waveDashPresentation.BeforeRender));
				goto IL_014f;
				IL_01b6:
				if (!Input.MenuConfirm.Pressed)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				waveDashPresentation.waitingForPageTurn = false;
				Audio.Play("event:/new_content/game/10_farewell/ppt_mouseclick");
				goto IL_01d4;
				IL_01d4:
				waveDashPresentation.pageUpdating = false;
				waveDashPresentation.pageIndex++;
				if (waveDashPresentation.pageIndex < waveDashPresentation.pages.Count)
				{
					float num2 = 0.5f;
					if (waveDashPresentation.CurrPage.Transition == WaveDashPage.Transitions.Rotate3D)
					{
						num2 = 1.5f;
					}
					else if (waveDashPresentation.CurrPage.Transition == WaveDashPage.Transitions.Blocky)
					{
						num2 = 1f;
					}
					waveDashPresentation.pageTurning = true;
					waveDashPresentation.pageEase = 0f;
					waveDashPresentation.Add(new Coroutine(waveDashPresentation.TurnPage(num2)));
					_003C_003E2__current = num2 * 0.8f;
					_003C_003E1__state = 5;
					return true;
				}
				goto IL_0273;
				IL_0273:
				if (waveDashPresentation.pageIndex < waveDashPresentation.pages.Count)
				{
					waveDashPresentation.pageUpdating = true;
					_003C_003E2__current = waveDashPresentation.CurrPage.Routine();
					_003C_003E1__state = 3;
					return true;
				}
				if (waveDashPresentation.usingSfx != null)
				{
					Audio.SetParameter(waveDashPresentation.usingSfx, "end", 1f);
					waveDashPresentation.usingSfx.release();
				}
				Audio.Play("event:/new_content/game/10_farewell/cafe_computer_off");
				break;
				IL_014f:
				if (waveDashPresentation.ease < 1f)
				{
					waveDashPresentation.ease = Calc.Approach(waveDashPresentation.ease, 1f, Engine.DeltaTime * 2f);
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_0273;
			}
			if (waveDashPresentation.ease > 0f)
			{
				waveDashPresentation.ease = Calc.Approach(waveDashPresentation.ease, 0f, Engine.DeltaTime * 2f);
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
				return true;
			}
			waveDashPresentation.Viewing = false;
			waveDashPresentation.RemoveSelf();
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
	private sealed class _003CTurnPage_003Ed__34 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WaveDashPresentation _003C_003E4__this;

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
		public _003CTurnPage_003Ed__34(int _003C_003E1__state)
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
			WaveDashPresentation waveDashPresentation = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				if (waveDashPresentation.CurrPage.Transition != WaveDashPage.Transitions.ScaleIn && waveDashPresentation.CurrPage.Transition != WaveDashPage.Transitions.FadeIn)
				{
					if (waveDashPresentation.CurrPage.Transition == WaveDashPage.Transitions.Rotate3D)
					{
						Audio.Play("event:/new_content/game/10_farewell/ppt_cube_transition");
					}
					else if (waveDashPresentation.CurrPage.Transition == WaveDashPage.Transitions.Blocky)
					{
						Audio.Play("event:/new_content/game/10_farewell/ppt_dissolve_transition");
					}
					else if (waveDashPresentation.CurrPage.Transition == WaveDashPage.Transitions.Spiral)
					{
						Audio.Play("event:/new_content/game/10_farewell/ppt_spinning_transition");
					}
				}
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (waveDashPresentation.pageEase < 1f)
			{
				waveDashPresentation.pageEase += Engine.DeltaTime / duration;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			waveDashPresentation.pageTurning = false;
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

	public Vector2 ScaleInPoint = new Vector2(1920f, 1080f) / 2f;

	public readonly int ScreenWidth = 1920;

	public readonly int ScreenHeight = 1080;

	private float ease;

	private bool loading;

	private float waitingForInputTime;

	private VirtualRenderTarget screenBuffer;

	private VirtualRenderTarget prevPageBuffer;

	private VirtualRenderTarget currPageBuffer;

	private int pageIndex;

	private List<WaveDashPage> pages = new List<WaveDashPage>();

	private float pageEase;

	private bool pageTurning;

	private bool pageUpdating;

	private bool waitingForPageTurn;

	private VertexPositionColorTexture[] verts = new VertexPositionColorTexture[6];

	private FMOD.Studio.EventInstance usingSfx;

	public bool Viewing { get; private set; }

	public Atlas Gfx { get; private set; }

	public bool ShowInput
	{
		get
		{
			if (!waitingForPageTurn)
			{
				if (CurrPage != null)
				{
					return CurrPage.WaitingForInput;
				}
				return false;
			}
			return true;
		}
	}

	private WaveDashPage PrevPage
	{
		get
		{
			if (pageIndex <= 0)
			{
				return null;
			}
			return pages[pageIndex - 1];
		}
	}

	private WaveDashPage CurrPage
	{
		get
		{
			if (pageIndex >= pages.Count)
			{
				return null;
			}
			return pages[pageIndex];
		}
	}

	public WaveDashPresentation(FMOD.Studio.EventInstance usingSfx = null)
	{
		base.Tag = Tags.HUD;
		Viewing = true;
		loading = true;
		Add(new Coroutine(Routine()));
		this.usingSfx = usingSfx;
		RunThread.Start(LoadingThread, "Wave Dash Presentation Loading", highPriority: true);
	}

	private void LoadingThread()
	{
		Gfx = Atlas.FromAtlas(Path.Combine("Graphics", "Atlases", "WaveDashing"), Atlas.AtlasDataFormat.Packer);
		loading = false;
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__33))]
	private IEnumerator Routine()
	{
		while (loading)
		{
			yield return null;
		}
		pages.Add(new WaveDashPage00());
		pages.Add(new WaveDashPage01());
		pages.Add(new WaveDashPage02());
		pages.Add(new WaveDashPage03());
		pages.Add(new WaveDashPage04());
		pages.Add(new WaveDashPage05());
		pages.Add(new WaveDashPage06());
		foreach (WaveDashPage page in pages)
		{
			page.Added(this);
		}
		Add(new BeforeRenderHook(BeforeRender));
		while (ease < 1f)
		{
			ease = Calc.Approach(ease, 1f, Engine.DeltaTime * 2f);
			yield return null;
		}
		while (pageIndex < pages.Count)
		{
			pageUpdating = true;
			yield return CurrPage.Routine();
			if (!CurrPage.AutoProgress)
			{
				waitingForPageTurn = true;
				while (!Input.MenuConfirm.Pressed)
				{
					yield return null;
				}
				waitingForPageTurn = false;
				Audio.Play("event:/new_content/game/10_farewell/ppt_mouseclick");
			}
			pageUpdating = false;
			pageIndex++;
			if (pageIndex < pages.Count)
			{
				float num = 0.5f;
				if (CurrPage.Transition == WaveDashPage.Transitions.Rotate3D)
				{
					num = 1.5f;
				}
				else if (CurrPage.Transition == WaveDashPage.Transitions.Blocky)
				{
					num = 1f;
				}
				pageTurning = true;
				pageEase = 0f;
				Add(new Coroutine(TurnPage(num)));
				yield return num * 0.8f;
			}
		}
		if (usingSfx != null)
		{
			Audio.SetParameter(usingSfx, "end", 1f);
			usingSfx.release();
		}
		Audio.Play("event:/new_content/game/10_farewell/cafe_computer_off");
		while (ease > 0f)
		{
			ease = Calc.Approach(ease, 0f, Engine.DeltaTime * 2f);
			yield return null;
		}
		Viewing = false;
		RemoveSelf();
	}

	[IteratorStateMachine(typeof(_003CTurnPage_003Ed__34))]
	private IEnumerator TurnPage(float duration)
	{
		if (CurrPage.Transition != WaveDashPage.Transitions.ScaleIn && CurrPage.Transition != WaveDashPage.Transitions.FadeIn)
		{
			if (CurrPage.Transition == WaveDashPage.Transitions.Rotate3D)
			{
				Audio.Play("event:/new_content/game/10_farewell/ppt_cube_transition");
			}
			else if (CurrPage.Transition == WaveDashPage.Transitions.Blocky)
			{
				Audio.Play("event:/new_content/game/10_farewell/ppt_dissolve_transition");
			}
			else if (CurrPage.Transition == WaveDashPage.Transitions.Spiral)
			{
				Audio.Play("event:/new_content/game/10_farewell/ppt_spinning_transition");
			}
		}
		while (pageEase < 1f)
		{
			pageEase += Engine.DeltaTime / duration;
			yield return null;
		}
		pageTurning = false;
	}

	private void BeforeRender()
	{
		if (loading)
		{
			return;
		}
		if (screenBuffer == null || screenBuffer.IsDisposed)
		{
			screenBuffer = VirtualContent.CreateRenderTarget("WaveDash-Buffer", ScreenWidth, ScreenHeight, depth: true);
		}
		if (prevPageBuffer == null || prevPageBuffer.IsDisposed)
		{
			prevPageBuffer = VirtualContent.CreateRenderTarget("WaveDash-Screen1", ScreenWidth, ScreenHeight);
		}
		if (currPageBuffer == null || currPageBuffer.IsDisposed)
		{
			currPageBuffer = VirtualContent.CreateRenderTarget("WaveDash-Screen2", ScreenWidth, ScreenHeight);
		}
		if (pageTurning && PrevPage != null)
		{
			Engine.Graphics.GraphicsDevice.SetRenderTarget(prevPageBuffer);
			Engine.Graphics.GraphicsDevice.Clear(PrevPage.ClearColor);
			Draw.SpriteBatch.Begin();
			PrevPage.Render();
			Draw.SpriteBatch.End();
		}
		if (CurrPage != null)
		{
			Engine.Graphics.GraphicsDevice.SetRenderTarget(currPageBuffer);
			Engine.Graphics.GraphicsDevice.Clear(CurrPage.ClearColor);
			Draw.SpriteBatch.Begin();
			CurrPage.Render();
			Draw.SpriteBatch.End();
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(screenBuffer);
		Engine.Graphics.GraphicsDevice.Clear(Color.Black);
		if (pageTurning)
		{
			if (CurrPage.Transition == WaveDashPage.Transitions.ScaleIn)
			{
				Draw.SpriteBatch.Begin();
				Draw.SpriteBatch.Draw((RenderTarget2D)prevPageBuffer, Vector2.Zero, Color.White);
				Vector2 scale = Vector2.One * pageEase;
				Draw.SpriteBatch.Draw((RenderTarget2D)currPageBuffer, ScaleInPoint, currPageBuffer.Bounds, Color.White, 0f, ScaleInPoint, scale, SpriteEffects.None, 0f);
				Draw.SpriteBatch.End();
			}
			else if (CurrPage.Transition == WaveDashPage.Transitions.FadeIn)
			{
				Draw.SpriteBatch.Begin();
				Draw.SpriteBatch.Draw((RenderTarget2D)prevPageBuffer, Vector2.Zero, Color.White);
				Draw.SpriteBatch.Draw((RenderTarget2D)currPageBuffer, Vector2.Zero, Color.White * pageEase);
				Draw.SpriteBatch.End();
			}
			else if (CurrPage.Transition == WaveDashPage.Transitions.Rotate3D)
			{
				float num = -(float)Math.PI / 2f * pageEase;
				RenderQuad((RenderTarget2D)prevPageBuffer, pageEase, num);
				RenderQuad((RenderTarget2D)currPageBuffer, pageEase, (float)Math.PI / 2f + num);
			}
			else if (CurrPage.Transition == WaveDashPage.Transitions.Blocky)
			{
				Draw.SpriteBatch.Begin();
				Draw.SpriteBatch.Draw((RenderTarget2D)prevPageBuffer, Vector2.Zero, Color.White);
				uint seed = 1u;
				int num2 = ScreenWidth / 60;
				for (int i = 0; i < ScreenWidth; i += num2)
				{
					for (int j = 0; j < ScreenHeight; j += num2)
					{
						if (PseudoRandRange(ref seed, 0f, 1f) <= pageEase)
						{
							Draw.SpriteBatch.Draw((RenderTarget2D)currPageBuffer, new Rectangle(i, j, num2, num2), new Rectangle(i, j, num2, num2), Color.White);
						}
					}
				}
				Draw.SpriteBatch.End();
			}
			else if (CurrPage.Transition == WaveDashPage.Transitions.Spiral)
			{
				Draw.SpriteBatch.Begin();
				Draw.SpriteBatch.Draw((RenderTarget2D)prevPageBuffer, Vector2.Zero, Color.White);
				Vector2 scale2 = Vector2.One * pageEase;
				float rotation = (1f - pageEase) * 12f;
				Draw.SpriteBatch.Draw((RenderTarget2D)currPageBuffer, Celeste.TargetCenter, currPageBuffer.Bounds, Color.White, rotation, Celeste.TargetCenter, scale2, SpriteEffects.None, 0f);
				Draw.SpriteBatch.End();
			}
		}
		else
		{
			Draw.SpriteBatch.Begin();
			Draw.SpriteBatch.Draw((RenderTarget2D)currPageBuffer, Vector2.Zero, Color.White);
			Draw.SpriteBatch.End();
		}
	}

	private void RenderQuad(Texture texture, float ease, float rotation)
	{
		float num = (float)screenBuffer.Width / (float)screenBuffer.Height;
		float num2 = num;
		float num3 = 1f;
		Vector3 position = new Vector3(0f - num2, num3, 0f);
		Vector3 position2 = new Vector3(num2, num3, 0f);
		Vector3 position3 = new Vector3(num2, 0f - num3, 0f);
		Vector3 position4 = new Vector3(0f - num2, 0f - num3, 0f);
		verts[0].Position = position;
		verts[0].TextureCoordinate = new Vector2(0f, 0f);
		verts[0].Color = Color.White;
		verts[1].Position = position2;
		verts[1].TextureCoordinate = new Vector2(1f, 0f);
		verts[1].Color = Color.White;
		verts[2].Position = position3;
		verts[2].TextureCoordinate = new Vector2(1f, 1f);
		verts[2].Color = Color.White;
		verts[3].Position = position;
		verts[3].TextureCoordinate = new Vector2(0f, 0f);
		verts[3].Color = Color.White;
		verts[4].Position = position3;
		verts[4].TextureCoordinate = new Vector2(1f, 1f);
		verts[4].Color = Color.White;
		verts[5].Position = position4;
		verts[5].TextureCoordinate = new Vector2(0f, 1f);
		verts[5].Color = Color.White;
		float num4 = 4.15f + Calc.YoYo(ease) * 1.7f;
		Matrix value = Matrix.CreateTranslation(0f, 0f, num) * Matrix.CreateRotationY(rotation) * Matrix.CreateTranslation(0f, 0f, 0f - num4) * Matrix.CreatePerspectiveFieldOfView((float)Math.PI / 4f, num, 1f, 10f);
		Engine.Instance.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
		Engine.Instance.GraphicsDevice.BlendState = BlendState.AlphaBlend;
		Engine.Instance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
		Engine.Instance.GraphicsDevice.Textures[0] = texture;
		GFX.FxTexture.Parameters["World"].SetValue(value);
		foreach (EffectPass pass in GFX.FxTexture.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Instance.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, verts, 0, verts.Length / 3);
		}
	}

	public override void Update()
	{
		base.Update();
		if (ShowInput)
		{
			waitingForInputTime += Engine.DeltaTime;
		}
		else
		{
			waitingForInputTime = 0f;
		}
		if (!loading && CurrPage != null && pageUpdating)
		{
			CurrPage.Update();
		}
	}

	public override void Render()
	{
		if (!loading && screenBuffer != null && !screenBuffer.IsDisposed)
		{
			float num = (float)ScreenWidth * Ease.CubeOut(Calc.ClampedMap(ease, 0f, 0.5f));
			float num2 = (float)ScreenHeight * Ease.CubeInOut(Calc.ClampedMap(ease, 0.5f, 1f, 0.2f));
			Rectangle rectangle = new Rectangle((int)((1920f - num) / 2f), (int)((1080f - num2) / 2f), (int)num, (int)num2);
			Draw.SpriteBatch.Draw((RenderTarget2D)screenBuffer, rectangle, Color.White);
			if (ShowInput && waitingForInputTime > 0.2f)
			{
				GFX.Gui["textboxbutton"].DrawCentered(new Vector2(1856f, 1016 + ((base.Scene.TimeActive % 1f < 0.25f) ? 6 : 0)), Color.Black);
			}
			if ((base.Scene as Level).Paused)
			{
				Draw.Rect(rectangle, Color.Black * 0.7f);
			}
		}
	}

	public override void Removed(Scene scene)
	{
		base.Removed(scene);
		Dispose();
	}

	public override void SceneEnd(Scene scene)
	{
		base.SceneEnd(scene);
		Dispose();
	}

	private void Dispose()
	{
		while (loading)
		{
			Thread.Sleep(1);
		}
		if (screenBuffer != null)
		{
			screenBuffer.Dispose();
		}
		screenBuffer = null;
		if (prevPageBuffer != null)
		{
			prevPageBuffer.Dispose();
		}
		prevPageBuffer = null;
		if (currPageBuffer != null)
		{
			currPageBuffer.Dispose();
		}
		currPageBuffer = null;
		Gfx.Dispose();
		Gfx = null;
	}

	private static uint PseudoRand(ref uint seed)
	{
		uint num = seed;
		num ^= num << 13;
		num ^= num >> 17;
		return seed = num ^ (num << 5);
	}

	public static float PseudoRandRange(ref uint seed, float min, float max)
	{
		return min + (float)(PseudoRand(ref seed) % 1000) / 1000f * (max - min);
	}
}
