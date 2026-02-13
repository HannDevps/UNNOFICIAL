using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class TempleMirrorPortal : Entity
{
	private struct Debris
	{
		public Vector2 Direction;

		public float Percent;

		public float Duration;

		public bool Enabled;
	}

	private class Bg : Entity
	{
		private MirrorSurface surface;

		private Vector2[] offsets;

		private List<MTexture> textures;

		public Bg(Vector2 position)
			: base(position)
		{
			base.Depth = 9500;
			textures = GFX.Game.GetAtlasSubtextures("objects/temple/portal/reflection");
			Vector2 vector = new Vector2(10f, 4f);
			offsets = new Vector2[textures.Count];
			for (int i = 0; i < offsets.Length; i++)
			{
				offsets[i] = vector + new Vector2(Calc.Random.Range(-4, 4), Calc.Random.Range(-4, 4));
			}
			Add(surface = new MirrorSurface());
			surface.OnRender = delegate
			{
				for (int j = 0; j < textures.Count; j++)
				{
					surface.ReflectionOffset = offsets[j];
					textures[j].DrawCentered(Position, surface.ReflectionColor);
				}
			};
		}

		public override void Render()
		{
			GFX.Game["objects/temple/portal/surface"].DrawCentered(Position);
		}
	}

	private class Curtain : Solid
	{
		public Sprite Sprite;

		public Curtain(Vector2 position)
			: base(position, 140f, 12f, safe: true)
		{
			Add(Sprite = GFX.SpriteBank.Create("temple_portal_curtain"));
			base.Depth = 1999;
			base.Collider.Position.X = -70f;
			base.Collider.Position.Y = 33f;
			Collidable = false;
			SurfaceSoundIndex = 17;
		}

		public override void Update()
		{
			base.Update();
			if (Collidable)
			{
				Player player;
				if ((player = CollideFirst<Player>(Position + new Vector2(-1f, 0f))) != null && player.OnGround() && Input.Aim.Value.X > 0f)
				{
					player.MoveV(base.Top - player.Bottom);
					player.MoveH(1f);
				}
				else if ((player = CollideFirst<Player>(Position + new Vector2(1f, 0f))) != null && player.OnGround() && Input.Aim.Value.X < 0f)
				{
					player.MoveV(base.Top - player.Bottom);
					player.MoveH(-1f);
				}
			}
		}

		public void Drop()
		{
			Sprite.Play("fall");
			base.Depth = -8999;
			Collidable = true;
			bool flag = false;
			Player player;
			while ((player = CollideFirst<Player>(Position)) != null && !flag)
			{
				Collidable = false;
				flag = player.MoveV(-1f);
				Collidable = true;
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003COnSwitchRoutine_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int side;

		public TempleMirrorPortal _003C_003E4__this;

		private LightingRenderer _003Clighting_003E5__2;

		private float _003ClightTarget_003E5__3;

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
		public _003COnSwitchRoutine_003Ed__19(int _003C_003E1__state)
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
			TempleMirrorPortal templeMirrorPortal = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = 0.4f;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				if (side < 0)
				{
					templeMirrorPortal.leftTorch.Light(templeMirrorPortal.switchCounter);
				}
				else
				{
					templeMirrorPortal.rightTorch.Light(templeMirrorPortal.switchCounter);
				}
				templeMirrorPortal.switchCounter++;
				if ((templeMirrorPortal.Scene as Level).Session.Area.Mode == AreaMode.Normal)
				{
					_003Clighting_003E5__2 = (templeMirrorPortal.Scene as Level).Lighting;
					_003ClightTarget_003E5__3 = Math.Max(0f, _003Clighting_003E5__2.Alpha - 0.2f);
					goto IL_00fc;
				}
				goto IL_0125;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00fc;
			case 3:
				_003C_003E1__state = -1;
				if (templeMirrorPortal.switchCounter >= 2)
				{
					_003C_003E2__current = 0.1f;
					_003C_003E1__state = 4;
					return true;
				}
				break;
			case 4:
				_003C_003E1__state = -1;
				Audio.Play("event:/game/05_mirror_temple/mainmirror_reveal", templeMirrorPortal.Position);
				templeMirrorPortal.curtain.Drop();
				templeMirrorPortal.canTrigger = true;
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 5;
				return true;
			case 5:
				{
					_003C_003E1__state = -1;
					Level level = templeMirrorPortal.SceneAs<Level>();
					for (int i = 0; i < 120; i += 12)
					{
						for (int j = 0; j < 60; j += 6)
						{
							level.Particles.Emit(P_CurtainDrop, 1, templeMirrorPortal.curtain.Position + new Vector2(-57 + i, -27 + j), new Vector2(6f, 3f));
						}
					}
					break;
				}
				IL_00fc:
				if ((_003Clighting_003E5__2.Alpha -= Engine.DeltaTime) > _003ClightTarget_003E5__3)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Clighting_003E5__2 = null;
				goto IL_0125;
				IL_0125:
				_003C_003E2__current = 0.15f;
				_003C_003E1__state = 3;
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
	private sealed class _003CActivateRoutine_003Ed__21 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TempleMirrorPortal _003C_003E4__this;

		private LightingRenderer _003Clight_003E5__2;

		private float _003CdebrisStart_003E5__3;

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
		public _003CActivateRoutine_003Ed__21(int _003C_003E1__state)
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
			TempleMirrorPortal templeMirrorPortal = _003C_003E4__this;
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
				Level level = templeMirrorPortal.Scene as Level;
				_003Clight_003E5__2 = level.Lighting;
				_003CdebrisStart_003E5__3 = 0f;
				templeMirrorPortal.Add(new BeforeRenderHook(templeMirrorPortal.BeforeRender));
				templeMirrorPortal.Add(new DisplacementRenderHook(templeMirrorPortal.RenderDisplacement));
			}
			templeMirrorPortal.bufferAlpha = Calc.Approach(templeMirrorPortal.bufferAlpha, 1f, Engine.DeltaTime);
			templeMirrorPortal.bufferTimer += 4f * Engine.DeltaTime;
			_003Clight_003E5__2.Alpha = Calc.Approach(_003Clight_003E5__2.Alpha, 0.2f, Engine.DeltaTime * 0.25f);
			if (_003CdebrisStart_003E5__3 < (float)templeMirrorPortal.debris.Length)
			{
				int num2 = (int)_003CdebrisStart_003E5__3;
				templeMirrorPortal.debris[num2].Direction = Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 1f);
				templeMirrorPortal.debris[num2].Enabled = true;
				templeMirrorPortal.debris[num2].Duration = 0.5f + Calc.Random.NextFloat(0.7f);
			}
			_003CdebrisStart_003E5__3 += Engine.DeltaTime * 10f;
			for (int i = 0; i < templeMirrorPortal.debris.Length; i++)
			{
				if (templeMirrorPortal.debris[i].Enabled)
				{
					templeMirrorPortal.debris[i].Percent %= 1f;
					templeMirrorPortal.debris[i].Percent += Engine.DeltaTime / templeMirrorPortal.debris[i].Duration;
				}
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 1;
			return true;
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

	public static ParticleType P_CurtainDrop;

	public float DistortionFade = 1f;

	private bool canTrigger;

	private int switchCounter;

	private VirtualRenderTarget buffer;

	private float bufferAlpha;

	private float bufferTimer;

	private Debris[] debris = new Debris[50];

	private Color debrisColorFrom = Calc.HexToColor("f442d4");

	private Color debrisColorTo = Calc.HexToColor("000000");

	private MTexture debrisTexture = GFX.Game["particles/blob"];

	private Curtain curtain;

	private TemplePortalTorch leftTorch;

	private TemplePortalTorch rightTorch;

	public TempleMirrorPortal(Vector2 position)
		: base(position)
	{
		base.Depth = 2000;
		base.Collider = new Hitbox(120f, 64f, -60f, -32f);
		Add(new PlayerCollider(OnPlayer));
	}

	public TempleMirrorPortal(EntityData data, Vector2 offset)
		: this(data.Position + offset)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		scene.Add(curtain = new Curtain(Position));
		scene.Add(new Bg(Position));
		scene.Add(leftTorch = new TemplePortalTorch(Position + new Vector2(-90f, 0f)));
		scene.Add(rightTorch = new TemplePortalTorch(Position + new Vector2(90f, 0f)));
	}

	public void OnSwitchHit(int side)
	{
		Add(new Coroutine(OnSwitchRoutine(side)));
	}

	[IteratorStateMachine(typeof(_003COnSwitchRoutine_003Ed__19))]
	private IEnumerator OnSwitchRoutine(int side)
	{
		yield return 0.4f;
		if (side < 0)
		{
			leftTorch.Light(switchCounter);
		}
		else
		{
			rightTorch.Light(switchCounter);
		}
		switchCounter++;
		if ((base.Scene as Level).Session.Area.Mode == AreaMode.Normal)
		{
			LightingRenderer lighting = (base.Scene as Level).Lighting;
			float lightTarget = Math.Max(0f, lighting.Alpha - 0.2f);
			while ((lighting.Alpha -= Engine.DeltaTime) > lightTarget)
			{
				yield return null;
			}
		}
		yield return 0.15f;
		if (switchCounter < 2)
		{
			yield break;
		}
		yield return 0.1f;
		Audio.Play("event:/game/05_mirror_temple/mainmirror_reveal", Position);
		curtain.Drop();
		canTrigger = true;
		yield return 0.1f;
		Level level = SceneAs<Level>();
		for (int i = 0; i < 120; i += 12)
		{
			for (int j = 0; j < 60; j += 6)
			{
				level.Particles.Emit(P_CurtainDrop, 1, curtain.Position + new Vector2(-57 + i, -27 + j), new Vector2(6f, 3f));
			}
		}
	}

	public void Activate()
	{
		Add(new Coroutine(ActivateRoutine()));
	}

	[IteratorStateMachine(typeof(_003CActivateRoutine_003Ed__21))]
	private IEnumerator ActivateRoutine()
	{
		Level level = base.Scene as Level;
		LightingRenderer light = level.Lighting;
		float debrisStart = 0f;
		Add(new BeforeRenderHook(BeforeRender));
		Add(new DisplacementRenderHook(RenderDisplacement));
		while (true)
		{
			bufferAlpha = Calc.Approach(bufferAlpha, 1f, Engine.DeltaTime);
			bufferTimer += 4f * Engine.DeltaTime;
			light.Alpha = Calc.Approach(light.Alpha, 0.2f, Engine.DeltaTime * 0.25f);
			if (debrisStart < (float)debris.Length)
			{
				int num = (int)debrisStart;
				debris[num].Direction = Calc.AngleToVector(Calc.Random.NextFloat((float)Math.PI * 2f), 1f);
				debris[num].Enabled = true;
				debris[num].Duration = 0.5f + Calc.Random.NextFloat(0.7f);
			}
			debrisStart += Engine.DeltaTime * 10f;
			for (int i = 0; i < debris.Length; i++)
			{
				if (debris[i].Enabled)
				{
					debris[i].Percent %= 1f;
					debris[i].Percent += Engine.DeltaTime / debris[i].Duration;
				}
			}
			yield return null;
		}
	}

	private void BeforeRender()
	{
		if (buffer == null)
		{
			buffer = VirtualContent.CreateRenderTarget("temple-portal", 120, 64);
		}
		Vector2 position = new Vector2(buffer.Width, buffer.Height) / 2f;
		MTexture mTexture = GFX.Game["objects/temple/portal/portal"];
		Engine.Graphics.GraphicsDevice.SetRenderTarget(buffer);
		Engine.Graphics.GraphicsDevice.Clear(Color.Black);
		Draw.SpriteBatch.Begin();
		for (int i = 0; (float)i < 10f; i++)
		{
			float num = bufferTimer % 1f * 0.1f + (float)i / 10f;
			Color color = Color.Lerp(Color.Black, Color.Purple, num);
			float scale = num;
			float rotation = (float)Math.PI * 2f * num;
			mTexture.DrawCentered(position, color, scale, rotation);
		}
		Draw.SpriteBatch.End();
	}

	private void RenderDisplacement()
	{
		Draw.Rect(base.X - 60f, base.Y - 32f, 120f, 64f, new Color(0.5f, 0.5f, 0.25f * DistortionFade * bufferAlpha, 1f));
	}

	public override void Render()
	{
		base.Render();
		if (buffer != null)
		{
			Draw.SpriteBatch.Draw((RenderTarget2D)buffer, Position + new Vector2((0f - base.Collider.Width) / 2f, (0f - base.Collider.Height) / 2f), Color.White * bufferAlpha);
		}
		GFX.Game["objects/temple/portal/portalframe"].DrawCentered(Position);
		Level level = base.Scene as Level;
		for (int i = 0; i < this.debris.Length; i++)
		{
			Debris debris = this.debris[i];
			if (debris.Enabled)
			{
				float num = Ease.SineOut(debris.Percent);
				Vector2 position = Position + debris.Direction * (1f - num) * (190f - level.Zoom * 30f);
				Color color = Color.Lerp(debrisColorFrom, debrisColorTo, num);
				float scale = Calc.LerpClamp(1f, 0.2f, num);
				debrisTexture.DrawCentered(position, color, scale, (float)i * 0.05f);
			}
		}
	}

	private void OnPlayer(Player player)
	{
		if (canTrigger)
		{
			canTrigger = false;
			base.Scene.Add(new CS04_MirrorPortal(player, this));
		}
	}

	public override void Removed(Scene scene)
	{
		Dispose();
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		Dispose();
		base.SceneEnd(scene);
	}

	private void Dispose()
	{
		if (buffer != null)
		{
			buffer.Dispose();
		}
		buffer = null;
	}
}
