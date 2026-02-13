using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class ResortMirror : Entity
{
	[CompilerGenerated]
	private sealed class _003CEvilAppearRoutine_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ResortMirror _003C_003E4__this;

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
		public _003CEvilAppearRoutine_003Ed__13(int _003C_003E1__state)
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
			ResortMirror resortMirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				resortMirror.evil = new BadelineDummy(new Vector2(resortMirror.mirror.Width + 8, resortMirror.mirror.Height));
				_003C_003E2__current = resortMirror.evil.WalkTo(resortMirror.mirror.Width / 2);
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
	private sealed class _003CFadeLights_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ResortMirror _003C_003E4__this;

		private Level _003Clevel_003E5__2;

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
		public _003CFadeLights_003Ed__14(int _003C_003E1__state)
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
			ResortMirror resortMirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = resortMirror.SceneAs<Level>();
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (_003Clevel_003E5__2.Lighting.Alpha != 0.35f)
			{
				_003Clevel_003E5__2.Lighting.Alpha = Calc.Approach(_003Clevel_003E5__2.Lighting.Alpha, 0.35f, Engine.DeltaTime * 0.1f);
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
	private sealed class _003CSmashRoutine_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ResortMirror _003C_003E4__this;

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
		public _003CSmashRoutine_003Ed__15(int _003C_003E1__state)
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
			ResortMirror resortMirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = resortMirror.evil.FloatTo(new Vector2(resortMirror.mirror.Width / 2, resortMirror.mirror.Height - 8));
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				resortMirror.breakingGlass = GFX.SpriteBank.Create("glass");
				resortMirror.breakingGlass.Position = new Vector2(resortMirror.mirror.Width / 2, resortMirror.mirror.Height);
				resortMirror.breakingGlass.Play("break");
				resortMirror.breakingGlass.Color = Color.White * resortMirror.shineAlpha;
				Input.Rumble(RumbleStrength.Light, RumbleLength.FullSecond);
				break;
			case 2:
				_003C_003E1__state = -1;
				break;
			}
			if (resortMirror.breakingGlass.CurrentAnimationID == "break")
			{
				if (resortMirror.breakingGlass.CurrentAnimationFrame == 7)
				{
					resortMirror.SceneAs<Level>().Shake();
				}
				resortMirror.shineAlpha = Calc.Approach(resortMirror.shineAlpha, 1f, Engine.DeltaTime * 2f);
				resortMirror.mirrorAlpha = Calc.Approach(resortMirror.mirrorAlpha, 1f, Engine.DeltaTime * 2f);
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			resortMirror.SceneAs<Level>().Shake();
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			for (float num2 = (0f - resortMirror.breakingGlass.Width) / 2f; num2 < resortMirror.breakingGlass.Width / 2f; num2 += 8f)
			{
				for (float num3 = 0f - resortMirror.breakingGlass.Height; num3 < 0f; num3 += 8f)
				{
					if (Calc.Random.Chance(0.5f))
					{
						(resortMirror.Scene as Level).Particles.Emit(DreamMirror.P_Shatter, 2, resortMirror.Position + new Vector2(num2 + 4f, num3 + 4f), new Vector2(8f, 8f), new Vector2(num2, num3).Angle());
					}
				}
			}
			resortMirror.shardReflection = true;
			resortMirror.evil = null;
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

	private bool smashed;

	private Image bg;

	private Image frame;

	private MTexture glassfg = GFX.Game["objects/mirror/glassfg"];

	private Sprite breakingGlass;

	private VirtualRenderTarget mirror;

	private float shineAlpha = 0.7f;

	private float mirrorAlpha = 0.7f;

	private BadelineDummy evil;

	private bool shardReflection;

	public ResortMirror(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Add(new BeforeRenderHook(BeforeRender));
		base.Depth = 9500;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		smashed = SceneAs<Level>().Session.GetFlag("oshiro_resort_suite");
		Entity entity = new Entity(Position);
		entity.Depth = 9000;
		entity.Add(frame = new Image(GFX.Game["objects/mirror/resortframe"]));
		frame.JustifyOrigin(0.5f, 1f);
		base.Scene.Add(entity);
		MTexture glassbg = GFX.Game["objects/mirror/glassbg"];
		int w = (int)frame.Width - 2;
		int h = (int)frame.Height - 12;
		if (!smashed)
		{
			mirror = VirtualContent.CreateRenderTarget("resort-mirror", w, h);
		}
		else
		{
			glassbg = GFX.Game["objects/mirror/glassbreak09"];
		}
		Add(bg = new Image(glassbg.GetSubtexture((glassbg.Width - w) / 2, glassbg.Height - h, w, h)));
		bg.JustifyOrigin(0.5f, 1f);
		List<MTexture> atlasSubtextures = GFX.Game.GetAtlasSubtextures("objects/mirror/mirrormask");
		MTexture temp = new MTexture();
		foreach (MTexture shard in atlasSubtextures)
		{
			MirrorSurface surface = new MirrorSurface();
			surface.OnRender = delegate
			{
				shard.GetSubtexture((glassbg.Width - w) / 2, glassbg.Height - h, w, h, temp).DrawJustified(Position, new Vector2(0.5f, 1f), surface.ReflectionColor * (shardReflection ? 1 : 0));
			};
			surface.ReflectionOffset = new Vector2(9 + Calc.Random.Range(-4, 4), 4 + Calc.Random.Range(-2, 2));
			Add(surface);
		}
	}

	public void EvilAppear()
	{
		Add(new Coroutine(EvilAppearRoutine()));
		Add(new Coroutine(FadeLights()));
	}

	[IteratorStateMachine(typeof(_003CEvilAppearRoutine_003Ed__13))]
	private IEnumerator EvilAppearRoutine()
	{
		evil = new BadelineDummy(new Vector2(mirror.Width + 8, mirror.Height));
		yield return evil.WalkTo(mirror.Width / 2);
	}

	[IteratorStateMachine(typeof(_003CFadeLights_003Ed__14))]
	private IEnumerator FadeLights()
	{
		Level level = SceneAs<Level>();
		while (level.Lighting.Alpha != 0.35f)
		{
			level.Lighting.Alpha = Calc.Approach(level.Lighting.Alpha, 0.35f, Engine.DeltaTime * 0.1f);
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CSmashRoutine_003Ed__15))]
	public IEnumerator SmashRoutine()
	{
		yield return evil.FloatTo(new Vector2(mirror.Width / 2, mirror.Height - 8));
		breakingGlass = GFX.SpriteBank.Create("glass");
		breakingGlass.Position = new Vector2(mirror.Width / 2, mirror.Height);
		breakingGlass.Play("break");
		breakingGlass.Color = Color.White * shineAlpha;
		Input.Rumble(RumbleStrength.Light, RumbleLength.FullSecond);
		while (breakingGlass.CurrentAnimationID == "break")
		{
			if (breakingGlass.CurrentAnimationFrame == 7)
			{
				SceneAs<Level>().Shake();
			}
			shineAlpha = Calc.Approach(shineAlpha, 1f, Engine.DeltaTime * 2f);
			mirrorAlpha = Calc.Approach(mirrorAlpha, 1f, Engine.DeltaTime * 2f);
			yield return null;
		}
		SceneAs<Level>().Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		for (float num = (0f - breakingGlass.Width) / 2f; num < breakingGlass.Width / 2f; num += 8f)
		{
			for (float num2 = 0f - breakingGlass.Height; num2 < 0f; num2 += 8f)
			{
				if (Calc.Random.Chance(0.5f))
				{
					(base.Scene as Level).Particles.Emit(DreamMirror.P_Shatter, 2, Position + new Vector2(num + 4f, num2 + 4f), new Vector2(8f, 8f), new Vector2(num, num2).Angle());
				}
			}
		}
		shardReflection = true;
		evil = null;
	}

	public void Broken()
	{
		evil = null;
		smashed = true;
		shardReflection = true;
		MTexture mTexture = GFX.Game["objects/mirror/glassbreak09"];
		bg.Texture = mTexture.GetSubtexture((int)((float)mTexture.Width - bg.Width) / 2, mTexture.Height - (int)bg.Height, (int)bg.Width, (int)bg.Height);
	}

	private void BeforeRender()
	{
		if (smashed || mirror == null)
		{
			return;
		}
		Level level = SceneAs<Level>();
		Engine.Graphics.GraphicsDevice.SetRenderTarget(mirror);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
		NPC nPC = base.Scene.Entities.FindFirst<NPC>();
		if (nPC != null)
		{
			Vector2 renderPosition = nPC.Sprite.RenderPosition;
			nPC.Sprite.RenderPosition = renderPosition - Position + new Vector2(mirror.Width / 2, mirror.Height) + new Vector2(8f, -4f);
			nPC.Sprite.Render();
			nPC.Sprite.RenderPosition = renderPosition;
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			Vector2 position = entity.Position;
			entity.Position = position - Position + new Vector2(mirror.Width / 2, mirror.Height) + new Vector2(8f, 0f);
			Vector2 vector = entity.Position - position;
			for (int i = 0; i < entity.Hair.Nodes.Count; i++)
			{
				entity.Hair.Nodes[i] += vector;
			}
			entity.Render();
			for (int j = 0; j < entity.Hair.Nodes.Count; j++)
			{
				entity.Hair.Nodes[j] -= vector;
			}
			entity.Position = position;
		}
		if (evil != null)
		{
			evil.Update();
			evil.Hair.Facing = (Facings)Math.Sign(evil.Sprite.Scale.X);
			evil.Hair.AfterUpdate();
			evil.Render();
		}
		if (breakingGlass != null)
		{
			breakingGlass.Color = Color.White * shineAlpha;
			breakingGlass.Update();
			breakingGlass.Render();
		}
		else
		{
			int num = -(int)(level.Camera.Y * 0.8f % (float)glassfg.Height);
			glassfg.DrawJustified(new Vector2(mirror.Width / 2, num), new Vector2(0.5f, 1f), Color.White * shineAlpha);
			glassfg.DrawJustified(new Vector2(mirror.Height / 2, num - glassfg.Height), new Vector2(0.5f, 1f), Color.White * shineAlpha);
		}
		Draw.SpriteBatch.End();
	}

	public override void Render()
	{
		bg.Render();
		if (!smashed)
		{
			Draw.SpriteBatch.Draw((RenderTarget2D)mirror, Position + new Vector2(-mirror.Width / 2, -mirror.Height), Color.White * mirrorAlpha);
		}
		frame.Render();
	}

	public override void SceneEnd(Scene scene)
	{
		Dispose();
		base.SceneEnd(scene);
	}

	public override void Removed(Scene scene)
	{
		Dispose();
		base.Removed(scene);
	}

	private void Dispose()
	{
		if (mirror != null)
		{
			mirror.Dispose();
		}
		mirror = null;
	}
}
