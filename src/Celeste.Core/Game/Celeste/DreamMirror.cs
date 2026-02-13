using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class DreamMirror : Entity
{
	[CompilerGenerated]
	private sealed class _003CInteractRoutine_003Ed__25 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DreamMirror _003C_003E4__this;

		private Player _003Cplayer_003E5__2;

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
		public _003CInteractRoutine_003Ed__25(int _003C_003E1__state)
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
			DreamMirror dreamMirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cplayer_003E5__2 = null;
				goto IL_0063;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0063;
			case 2:
				_003C_003E1__state = -1;
				goto IL_0084;
			case 3:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_0063:
				if (_003Cplayer_003E5__2 == null)
				{
					_003Cplayer_003E5__2 = dreamMirror.Scene.Tracker.GetEntity<Player>();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				goto IL_0084;
				IL_0084:
				if (!dreamMirror.hitbox.Collide(_003Cplayer_003E5__2))
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				dreamMirror.hitbox.Width += 32f;
				dreamMirror.hitbox.Position.X -= 16f;
				Audio.SetMusic(null);
				break;
			}
			if (dreamMirror.hitbox.Collide(_003Cplayer_003E5__2))
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 3;
				return true;
			}
			dreamMirror.Scene.Add(new CS02_Mirror(_003Cplayer_003E5__2, dreamMirror));
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
	private sealed class _003CBreakRoutine_003Ed__26 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DreamMirror _003C_003E4__this;

		public int direction;

		private float _003Cspeed_003E5__2;

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
		public _003CBreakRoutine_003Ed__26(int _003C_003E1__state)
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
			DreamMirror dreamMirror = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				dreamMirror.autoUpdateReflection = false;
				dreamMirror.reflectionSprite.Play("runFast");
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
				goto IL_00a0;
			case 1:
				_003C_003E1__state = -1;
				goto IL_00a0;
			case 2:
				_003C_003E1__state = -1;
				dreamMirror.Add(dreamMirror.sfx = new SoundSource());
				dreamMirror.sfx.Play("event:/game/02_old_site/sequence_mirror");
				_003C_003E2__current = 0.15f;
				_003C_003E1__state = 3;
				return true;
			case 3:
				_003C_003E1__state = -1;
				dreamMirror.Add(dreamMirror.sfxSting = new SoundSource("event:/music/lvl2/dreamblock_sting_pt2"));
				Input.Rumble(RumbleStrength.Light, RumbleLength.FullSecond);
				dreamMirror.updateShine = false;
				goto IL_01c8;
			case 4:
				_003C_003E1__state = -1;
				goto IL_01c8;
			case 5:
			{
				_003C_003E1__state = -1;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				(dreamMirror.Scene as Level).Shake();
				for (float num2 = (0f - dreamMirror.breakingGlass.Width) / 2f; num2 < dreamMirror.breakingGlass.Width / 2f; num2 += 8f)
				{
					for (float num3 = 0f - dreamMirror.breakingGlass.Height; num3 < 0f; num3 += 8f)
					{
						if (Calc.Random.Chance(0.5f))
						{
							(dreamMirror.Scene as Level).Particles.Emit(P_Shatter, 2, dreamMirror.Position + new Vector2(num2 + 4f, num3 + 4f), new Vector2(8f, 8f), new Vector2(num2, num3).Angle());
						}
					}
				}
				dreamMirror.smashEnded = true;
				dreamMirror.badeline = new BadelineDummy(dreamMirror.reflection.Position + dreamMirror.Position - dreamMirror.breakingGlass.Origin);
				dreamMirror.badeline.Floatness = 0f;
				for (int i = 0; i < dreamMirror.badeline.Hair.Nodes.Count; i++)
				{
					dreamMirror.badeline.Hair.Nodes[i] = dreamMirror.reflectionHair.Nodes[i];
				}
				dreamMirror.Scene.Add(dreamMirror.badeline);
				dreamMirror.badeline.Sprite.Play("idle");
				dreamMirror.badeline.Sprite.Scale = dreamMirror.reflectionSprite.Scale;
				dreamMirror.reflection = null;
				_003C_003E2__current = 1.2f;
				_003C_003E1__state = 6;
				return true;
			}
			case 6:
				_003C_003E1__state = -1;
				_003Cspeed_003E5__2 = (float)(-direction) * 32f;
				dreamMirror.badeline.Sprite.Scale.X = -direction;
				dreamMirror.badeline.Sprite.Play("runFast");
				goto IL_0497;
			case 7:
				_003C_003E1__state = -1;
				goto IL_0497;
			case 8:
				_003C_003E1__state = -1;
				goto IL_054d;
			case 9:
				{
					_003C_003E1__state = -1;
					return false;
				}
				IL_0497:
				if (Math.Abs(dreamMirror.badeline.X - dreamMirror.X) < 60f)
				{
					_003Cspeed_003E5__2 += Engine.DeltaTime * (float)(-direction) * 128f;
					dreamMirror.badeline.X += _003Cspeed_003E5__2 * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 7;
					return true;
				}
				dreamMirror.badeline.Sprite.Play("jumpFast");
				goto IL_054d;
				IL_054d:
				if (Math.Abs(dreamMirror.badeline.X - dreamMirror.X) < 128f)
				{
					_003Cspeed_003E5__2 += Engine.DeltaTime * (float)(-direction) * 128f;
					dreamMirror.badeline.X += _003Cspeed_003E5__2 * Engine.DeltaTime;
					dreamMirror.badeline.Y -= Math.Abs(_003Cspeed_003E5__2) * Engine.DeltaTime * 0.8f;
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				dreamMirror.badeline.RemoveSelf();
				dreamMirror.badeline = null;
				_003C_003E2__current = 1.5f;
				_003C_003E1__state = 9;
				return true;
				IL_01c8:
				if (dreamMirror.shineOffset != 33f || dreamMirror.shineAlpha < 1f)
				{
					dreamMirror.shineOffset = Calc.Approach(dreamMirror.shineOffset, 33f, Engine.DeltaTime * 120f);
					dreamMirror.shineAlpha = Calc.Approach(dreamMirror.shineAlpha, 1f, Engine.DeltaTime * 4f);
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				dreamMirror.smashed = true;
				dreamMirror.breakingGlass.Play("break");
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 5;
				return true;
				IL_00a0:
				if (Math.Abs(dreamMirror.reflection.X - dreamMirror.breakingGlass.Width / 2f) > 3f)
				{
					dreamMirror.reflection.X += (float)(direction * 32) * Engine.DeltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				dreamMirror.reflectionSprite.Play("idle");
				_003C_003E2__current = 0.65f;
				_003C_003E1__state = 2;
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

	public static ParticleType P_Shatter;

	private Image frame;

	private MTexture glassbg = GFX.Game["objects/mirror/glassbg"];

	private MTexture glassfg = GFX.Game["objects/mirror/glassfg"];

	private Sprite breakingGlass;

	private Hitbox hitbox;

	private VirtualRenderTarget mirror;

	private float shineAlpha = 0.5f;

	private float shineOffset;

	private Entity reflection;

	private PlayerSprite reflectionSprite;

	private PlayerHair reflectionHair;

	private float reflectionAlpha = 0.7f;

	private bool autoUpdateReflection = true;

	private BadelineDummy badeline;

	private bool smashed;

	private bool smashEnded;

	private bool updateShine = true;

	private Coroutine smashCoroutine;

	private SoundSource sfx;

	private SoundSource sfxSting;

	public DreamMirror(Vector2 position)
		: base(position)
	{
		base.Depth = 9500;
		Add(breakingGlass = GFX.SpriteBank.Create("glass"));
		breakingGlass.Play("idle");
		Add(new BeforeRenderHook(BeforeRender));
		foreach (MTexture shard in GFX.Game.GetAtlasSubtextures("objects/mirror/mirrormask"))
		{
			MirrorSurface surface = new MirrorSurface();
			surface.OnRender = delegate
			{
				shard.DrawJustified(Position, new Vector2(0.5f, 1f), surface.ReflectionColor * (smashEnded ? 1 : 0));
			};
			surface.ReflectionOffset = new Vector2(9 + Calc.Random.Range(-4, 4), 4 + Calc.Random.Range(-2, 2));
			Add(surface);
		}
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		smashed = SceneAs<Level>().Session.Inventory.DreamDash;
		if (smashed)
		{
			breakingGlass.Play("broken");
			smashEnded = true;
		}
		else
		{
			reflection = new Entity();
			reflectionSprite = new PlayerSprite(PlayerSpriteMode.Badeline);
			reflectionHair = new PlayerHair(reflectionSprite);
			reflectionHair.Color = BadelineOldsite.HairColor;
			reflectionHair.Border = Color.Black;
			reflection.Add(reflectionHair);
			reflection.Add(reflectionSprite);
			reflectionHair.Start();
			reflectionSprite.OnFrameChange = delegate(string anim)
			{
				if (!smashed && CollideCheck<Player>())
				{
					int currentAnimationFrame = reflectionSprite.CurrentAnimationFrame;
					if ((anim == "walk" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "runSlow" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)) || (anim == "runFast" && (currentAnimationFrame == 0 || currentAnimationFrame == 6)))
					{
						Audio.Play("event:/char/badeline/footstep", base.Center);
					}
				}
			};
			Add(smashCoroutine = new Coroutine(InteractRoutine()));
		}
		Entity entity = new Entity(Position);
		entity.Depth = 9000;
		entity.Add(frame = new Image(GFX.Game["objects/mirror/frame"]));
		frame.JustifyOrigin(0.5f, 1f);
		base.Scene.Add(entity);
		base.Collider = (hitbox = new Hitbox((int)frame.Width - 16, (int)frame.Height + 32, -(int)frame.Width / 2 + 8, -(int)frame.Height - 32));
	}

	public override void Update()
	{
		base.Update();
		if (reflection != null)
		{
			reflection.Update();
			reflectionHair.Facing = (Facings)Math.Sign(reflectionSprite.Scale.X);
			reflectionHair.AfterUpdate();
		}
	}

	private void BeforeRender()
	{
		if (smashed)
		{
			return;
		}
		Level level = base.Scene as Level;
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity == null)
		{
			return;
		}
		if (autoUpdateReflection && reflection != null)
		{
			reflection.Position = new Vector2(base.X - entity.X, entity.Y - base.Y) + breakingGlass.Origin;
			reflectionSprite.Scale.X = (float)(0 - entity.Facing) * Math.Abs(entity.Sprite.Scale.X);
			reflectionSprite.Scale.Y = entity.Sprite.Scale.Y;
			if (reflectionSprite.CurrentAnimationID != entity.Sprite.CurrentAnimationID && entity.Sprite.CurrentAnimationID != null && reflectionSprite.Has(entity.Sprite.CurrentAnimationID))
			{
				reflectionSprite.Play(entity.Sprite.CurrentAnimationID);
			}
		}
		if (mirror == null)
		{
			mirror = VirtualContent.CreateRenderTarget("dream-mirror", glassbg.Width, glassbg.Height);
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(mirror);
		Engine.Graphics.GraphicsDevice.Clear(Color.Transparent);
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
		if (updateShine)
		{
			shineOffset = glassfg.Height - (int)(level.Camera.Y * 0.8f % (float)glassfg.Height);
		}
		glassbg.Draw(Vector2.Zero);
		if (reflection != null)
		{
			reflection.Render();
		}
		glassfg.Draw(new Vector2(0f, shineOffset), Vector2.Zero, Color.White * shineAlpha);
		glassfg.Draw(new Vector2(0f, shineOffset - (float)glassfg.Height), Vector2.Zero, Color.White * shineAlpha);
		Draw.SpriteBatch.End();
	}

	[IteratorStateMachine(typeof(_003CInteractRoutine_003Ed__25))]
	private IEnumerator InteractRoutine()
	{
		Player player = null;
		while (player == null)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			yield return null;
		}
		while (!hitbox.Collide(player))
		{
			yield return null;
		}
		hitbox.Width += 32f;
		hitbox.Position.X -= 16f;
		Audio.SetMusic(null);
		while (hitbox.Collide(player))
		{
			yield return null;
		}
		base.Scene.Add(new CS02_Mirror(player, this));
	}

	[IteratorStateMachine(typeof(_003CBreakRoutine_003Ed__26))]
	public IEnumerator BreakRoutine(int direction)
	{
		autoUpdateReflection = false;
		reflectionSprite.Play("runFast");
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Short);
		while (Math.Abs(reflection.X - breakingGlass.Width / 2f) > 3f)
		{
			reflection.X += (float)(direction * 32) * Engine.DeltaTime;
			yield return null;
		}
		reflectionSprite.Play("idle");
		yield return 0.65f;
		Add(sfx = new SoundSource());
		sfx.Play("event:/game/02_old_site/sequence_mirror");
		yield return 0.15f;
		Add(sfxSting = new SoundSource("event:/music/lvl2/dreamblock_sting_pt2"));
		Input.Rumble(RumbleStrength.Light, RumbleLength.FullSecond);
		updateShine = false;
		while (shineOffset != 33f || shineAlpha < 1f)
		{
			shineOffset = Calc.Approach(shineOffset, 33f, Engine.DeltaTime * 120f);
			shineAlpha = Calc.Approach(shineAlpha, 1f, Engine.DeltaTime * 4f);
			yield return null;
		}
		smashed = true;
		breakingGlass.Play("break");
		yield return 0.6f;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		(base.Scene as Level).Shake();
		for (float num = (0f - breakingGlass.Width) / 2f; num < breakingGlass.Width / 2f; num += 8f)
		{
			for (float num2 = 0f - breakingGlass.Height; num2 < 0f; num2 += 8f)
			{
				if (Calc.Random.Chance(0.5f))
				{
					(base.Scene as Level).Particles.Emit(P_Shatter, 2, Position + new Vector2(num + 4f, num2 + 4f), new Vector2(8f, 8f), new Vector2(num, num2).Angle());
				}
			}
		}
		smashEnded = true;
		badeline = new BadelineDummy(reflection.Position + Position - breakingGlass.Origin);
		badeline.Floatness = 0f;
		for (int i = 0; i < badeline.Hair.Nodes.Count; i++)
		{
			badeline.Hair.Nodes[i] = reflectionHair.Nodes[i];
		}
		base.Scene.Add(badeline);
		badeline.Sprite.Play("idle");
		badeline.Sprite.Scale = reflectionSprite.Scale;
		reflection = null;
		yield return 1.2f;
		float speed = (float)(-direction) * 32f;
		badeline.Sprite.Scale.X = -direction;
		badeline.Sprite.Play("runFast");
		while (Math.Abs(badeline.X - base.X) < 60f)
		{
			speed += Engine.DeltaTime * (float)(-direction) * 128f;
			badeline.X += speed * Engine.DeltaTime;
			yield return null;
		}
		badeline.Sprite.Play("jumpFast");
		while (Math.Abs(badeline.X - base.X) < 128f)
		{
			speed += Engine.DeltaTime * (float)(-direction) * 128f;
			badeline.X += speed * Engine.DeltaTime;
			badeline.Y -= Math.Abs(speed) * Engine.DeltaTime * 0.8f;
			yield return null;
		}
		badeline.RemoveSelf();
		badeline = null;
		yield return 1.5f;
	}

	public void Broken(bool wasSkipped)
	{
		updateShine = false;
		smashed = true;
		smashEnded = true;
		breakingGlass.Play("broken");
		if (wasSkipped && badeline != null)
		{
			badeline.RemoveSelf();
		}
		if (wasSkipped && sfx != null)
		{
			sfx.Stop();
		}
		if (wasSkipped && sfxSting != null)
		{
			sfxSting.Stop();
		}
	}

	public override void Render()
	{
		if (smashed)
		{
			breakingGlass.Render();
		}
		else
		{
			Draw.SpriteBatch.Draw(mirror.Target, Position - breakingGlass.Origin, Color.White * reflectionAlpha);
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
