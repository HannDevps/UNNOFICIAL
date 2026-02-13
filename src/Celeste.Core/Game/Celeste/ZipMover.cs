using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ZipMover : Solid
{
	public enum Themes
	{
		Normal,
		Moon
	}

	private class ZipMoverPathRenderer : Entity
	{
		public ZipMover ZipMover;

		private MTexture cog;

		private Vector2 from;

		private Vector2 to;

		private Vector2 sparkAdd;

		private float sparkDirFromA;

		private float sparkDirFromB;

		private float sparkDirToA;

		private float sparkDirToB;

		public ZipMoverPathRenderer(ZipMover zipMover)
		{
			base.Depth = 5000;
			ZipMover = zipMover;
			from = ZipMover.start + new Vector2(ZipMover.Width / 2f, ZipMover.Height / 2f);
			to = ZipMover.target + new Vector2(ZipMover.Width / 2f, ZipMover.Height / 2f);
			sparkAdd = (from - to).SafeNormalize(5f).Perpendicular();
			float num = (from - to).Angle();
			sparkDirFromA = num + (float)Math.PI / 8f;
			sparkDirFromB = num - (float)Math.PI / 8f;
			sparkDirToA = num + (float)Math.PI - (float)Math.PI / 8f;
			sparkDirToB = num + (float)Math.PI + (float)Math.PI / 8f;
			if (zipMover.theme == Themes.Moon)
			{
				cog = GFX.Game["objects/zipmover/moon/cog"];
			}
			else
			{
				cog = GFX.Game["objects/zipmover/cog"];
			}
		}

		public void CreateSparks()
		{
			SceneAs<Level>().ParticlesBG.Emit(P_Sparks, from + sparkAdd + Calc.Random.Range(-Vector2.One, Vector2.One), sparkDirFromA);
			SceneAs<Level>().ParticlesBG.Emit(P_Sparks, from - sparkAdd + Calc.Random.Range(-Vector2.One, Vector2.One), sparkDirFromB);
			SceneAs<Level>().ParticlesBG.Emit(P_Sparks, to + sparkAdd + Calc.Random.Range(-Vector2.One, Vector2.One), sparkDirToA);
			SceneAs<Level>().ParticlesBG.Emit(P_Sparks, to - sparkAdd + Calc.Random.Range(-Vector2.One, Vector2.One), sparkDirToB);
		}

		public override void Render()
		{
			DrawCogs(Vector2.UnitY, Color.Black);
			DrawCogs(Vector2.Zero);
			if (ZipMover.drawBlackBorder)
			{
				Draw.Rect(new Rectangle((int)(ZipMover.X + ZipMover.Shake.X - 1f), (int)(ZipMover.Y + ZipMover.Shake.Y - 1f), (int)ZipMover.Width + 2, (int)ZipMover.Height + 2), Color.Black);
			}
		}

		private void DrawCogs(Vector2 offset, Color? colorOverride = null)
		{
			Vector2 vector = (to - from).SafeNormalize();
			Vector2 vector2 = vector.Perpendicular() * 3f;
			Vector2 vector3 = -vector.Perpendicular() * 4f;
			float rotation = ZipMover.percent * (float)Math.PI * 2f;
			Draw.Line(from + vector2 + offset, to + vector2 + offset, colorOverride.HasValue ? colorOverride.Value : ropeColor);
			Draw.Line(from + vector3 + offset, to + vector3 + offset, colorOverride.HasValue ? colorOverride.Value : ropeColor);
			for (float num = 4f - ZipMover.percent * (float)Math.PI * 8f % 4f; num < (to - from).Length(); num += 4f)
			{
				Vector2 vector4 = from + vector2 + vector.Perpendicular() + vector * num;
				Vector2 vector5 = to + vector3 - vector * num;
				Draw.Line(vector4 + offset, vector4 + vector * 2f + offset, colorOverride.HasValue ? colorOverride.Value : ropeLightColor);
				Draw.Line(vector5 + offset, vector5 - vector * 2f + offset, colorOverride.HasValue ? colorOverride.Value : ropeLightColor);
			}
			cog.DrawCentered(from + offset, colorOverride.HasValue ? colorOverride.Value : Color.White, 1f, rotation);
			cog.DrawCentered(to + offset, colorOverride.HasValue ? colorOverride.Value : Color.White, 1f, rotation);
		}
	}

	[CompilerGenerated]
	private sealed class _003CSequence_003Ed__24 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ZipMover _003C_003E4__this;

		private Vector2 _003Cstart_003E5__2;

		private float _003Cat_003E5__3;

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
		public _003CSequence_003Ed__24(int _003C_003E1__state)
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
			ZipMover zipMover = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cstart_003E5__2 = zipMover.Position;
				goto IL_005e;
			case 1:
				_003C_003E1__state = -1;
				goto IL_005e;
			case 2:
				_003C_003E1__state = -1;
				zipMover.streetlight.SetAnimationFrame(3);
				zipMover.StopPlayerRunIntoAnimation = false;
				_003Cat_003E5__3 = 0f;
				goto IL_0172;
			case 3:
			{
				_003C_003E1__state = -1;
				_003Cat_003E5__3 = Calc.Approach(_003Cat_003E5__3, 1f, 2f * Engine.DeltaTime);
				zipMover.percent = Ease.SineIn(_003Cat_003E5__3);
				Vector2 vector = Vector2.Lerp(_003Cstart_003E5__2, zipMover.target, zipMover.percent);
				zipMover.ScrapeParticlesCheck(vector);
				if (zipMover.Scene.OnInterval(0.1f))
				{
					zipMover.pathRenderer.CreateSparks();
				}
				zipMover.MoveTo(vector);
				goto IL_0172;
			}
			case 4:
				_003C_003E1__state = -1;
				zipMover.StopPlayerRunIntoAnimation = false;
				zipMover.streetlight.SetAnimationFrame(2);
				_003Cat_003E5__3 = 0f;
				break;
			case 5:
			{
				_003C_003E1__state = -1;
				_003Cat_003E5__3 = Calc.Approach(_003Cat_003E5__3, 1f, 0.5f * Engine.DeltaTime);
				zipMover.percent = 1f - Ease.SineIn(_003Cat_003E5__3);
				Vector2 position = Vector2.Lerp(zipMover.target, _003Cstart_003E5__2, Ease.SineIn(_003Cat_003E5__3));
				zipMover.MoveTo(position);
				break;
			}
			case 6:
				{
					_003C_003E1__state = -1;
					goto IL_005e;
				}
				IL_0172:
				if (_003Cat_003E5__3 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				zipMover.StartShaking(0.2f);
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				zipMover.SceneAs<Level>().Shake();
				zipMover.StopPlayerRunIntoAnimation = true;
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 4;
				return true;
				IL_005e:
				if (!zipMover.HasPlayerRider())
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				zipMover.sfx.Play((zipMover.theme == Themes.Normal) ? "event:/game/01_forsaken_city/zip_mover" : "event:/new_content/game/10_farewell/zip_mover");
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Short);
				zipMover.StartShaking(0.1f);
				_003C_003E2__current = 0.1f;
				_003C_003E1__state = 2;
				return true;
			}
			if (_003Cat_003E5__3 < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
				return true;
			}
			zipMover.StopPlayerRunIntoAnimation = true;
			zipMover.StartShaking(0.2f);
			zipMover.streetlight.SetAnimationFrame(1);
			_003C_003E2__current = 0.5f;
			_003C_003E1__state = 6;
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

	public static ParticleType P_Scrape;

	public static ParticleType P_Sparks;

	private Themes theme;

	private MTexture[,] edges = new MTexture[3, 3];

	private Sprite streetlight;

	private BloomPoint bloom;

	private ZipMoverPathRenderer pathRenderer;

	private List<MTexture> innerCogs;

	private MTexture temp = new MTexture();

	private bool drawBlackBorder;

	private Vector2 start;

	private Vector2 target;

	private float percent;

	private static readonly Color ropeColor = Calc.HexToColor("663931");

	private static readonly Color ropeLightColor = Calc.HexToColor("9b6157");

	private SoundSource sfx = new SoundSource();

	public ZipMover(Vector2 position, int width, int height, Vector2 target, Themes theme)
		: base(position, width, height, safe: false)
	{
		base.Depth = -9999;
		start = Position;
		this.target = target;
		this.theme = theme;
		Add(new Coroutine(Sequence()));
		Add(new LightOcclude());
		string path;
		string id;
		string key;
		if (theme == Themes.Moon)
		{
			path = "objects/zipmover/moon/light";
			id = "objects/zipmover/moon/block";
			key = "objects/zipmover/moon/innercog";
			drawBlackBorder = false;
		}
		else
		{
			path = "objects/zipmover/light";
			id = "objects/zipmover/block";
			key = "objects/zipmover/innercog";
			drawBlackBorder = true;
		}
		innerCogs = GFX.Game.GetAtlasSubtextures(key);
		Add(streetlight = new Sprite(GFX.Game, path));
		streetlight.Add("frames", "", 1f);
		streetlight.Play("frames");
		streetlight.Active = false;
		streetlight.SetAnimationFrame(1);
		streetlight.Position = new Vector2(base.Width / 2f - streetlight.Width / 2f, 0f);
		Add(bloom = new BloomPoint(1f, 6f));
		bloom.Position = new Vector2(base.Width / 2f, 4f);
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				edges[i, j] = GFX.Game[id].GetSubtexture(i * 8, j * 8, 8, 8);
			}
		}
		SurfaceSoundIndex = 7;
		sfx.Position = new Vector2(base.Width, base.Height) / 2f;
		Add(sfx);
	}

	public ZipMover(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width, data.Height, data.Nodes[0] + offset, data.Enum("theme", Themes.Normal))
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		scene.Add(pathRenderer = new ZipMoverPathRenderer(this));
	}

	public override void Removed(Scene scene)
	{
		scene.Remove(pathRenderer);
		pathRenderer = null;
		base.Removed(scene);
	}

	public override void Update()
	{
		base.Update();
		bloom.Y = streetlight.CurrentAnimationFrame * 3;
	}

	public override void Render()
	{
		Vector2 position = Position;
		Position += base.Shake;
		Draw.Rect(base.X + 1f, base.Y + 1f, base.Width - 2f, base.Height - 2f, Color.Black);
		int num = 1;
		float num2 = 0f;
		int count = innerCogs.Count;
		for (int i = 4; (float)i <= base.Height - 4f; i += 8)
		{
			int num3 = num;
			for (int j = 4; (float)j <= base.Width - 4f; j += 8)
			{
				int index = (int)(mod((num2 + (float)num * percent * (float)Math.PI * 4f) / ((float)Math.PI / 2f), 1f) * (float)count);
				MTexture mTexture = innerCogs[index];
				Rectangle rectangle = new Rectangle(0, 0, mTexture.Width, mTexture.Height);
				Vector2 zero = Vector2.Zero;
				if (j <= 4)
				{
					zero.X = 2f;
					rectangle.X = 2;
					rectangle.Width -= 2;
				}
				else if ((float)j >= base.Width - 4f)
				{
					zero.X = -2f;
					rectangle.Width -= 2;
				}
				if (i <= 4)
				{
					zero.Y = 2f;
					rectangle.Y = 2;
					rectangle.Height -= 2;
				}
				else if ((float)i >= base.Height - 4f)
				{
					zero.Y = -2f;
					rectangle.Height -= 2;
				}
				mTexture = mTexture.GetSubtexture(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, temp);
				mTexture.DrawCentered(Position + new Vector2(j, i) + zero, Color.White * ((num < 0) ? 0.5f : 1f));
				num = -num;
				num2 += (float)Math.PI / 3f;
			}
			if (num3 == num)
			{
				num = -num;
			}
		}
		for (int k = 0; (float)k < base.Width / 8f; k++)
		{
			for (int l = 0; (float)l < base.Height / 8f; l++)
			{
				int num4 = ((k != 0) ? (((float)k != base.Width / 8f - 1f) ? 1 : 2) : 0);
				int num5 = ((l != 0) ? (((float)l != base.Height / 8f - 1f) ? 1 : 2) : 0);
				if (num4 != 1 || num5 != 1)
				{
					edges[num4, num5].Draw(new Vector2(base.X + (float)(k * 8), base.Y + (float)(l * 8)));
				}
			}
		}
		base.Render();
		Position = position;
	}

	private void ScrapeParticlesCheck(Vector2 to)
	{
		if (!base.Scene.OnInterval(0.03f))
		{
			return;
		}
		bool flag = to.Y != base.ExactPosition.Y;
		bool flag2 = to.X != base.ExactPosition.X;
		if (flag && !flag2)
		{
			int num = Math.Sign(to.Y - base.ExactPosition.Y);
			Vector2 vector = ((num != 1) ? base.TopLeft : base.BottomLeft);
			int num2 = 4;
			if (num == 1)
			{
				num2 = Math.Min((int)base.Height - 12, 20);
			}
			int num3 = (int)base.Height;
			if (num == -1)
			{
				num3 = Math.Max(16, (int)base.Height - 16);
			}
			if (base.Scene.CollideCheck<Solid>(vector + new Vector2(-2f, num * -2)))
			{
				for (int i = num2; i < num3; i += 8)
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Scrape, base.TopLeft + new Vector2(0f, (float)i + (float)num * 2f), (num == 1) ? (-(float)Math.PI / 4f) : ((float)Math.PI / 4f));
				}
			}
			if (base.Scene.CollideCheck<Solid>(vector + new Vector2(base.Width + 2f, num * -2)))
			{
				for (int j = num2; j < num3; j += 8)
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Scrape, base.TopRight + new Vector2(-1f, (float)j + (float)num * 2f), (num == 1) ? ((float)Math.PI * -3f / 4f) : ((float)Math.PI * 3f / 4f));
				}
			}
		}
		else
		{
			if (!flag2 || flag)
			{
				return;
			}
			int num4 = Math.Sign(to.X - base.ExactPosition.X);
			Vector2 vector2 = ((num4 != 1) ? base.TopLeft : base.TopRight);
			int num5 = 4;
			if (num4 == 1)
			{
				num5 = Math.Min((int)base.Width - 12, 20);
			}
			int num6 = (int)base.Width;
			if (num4 == -1)
			{
				num6 = Math.Max(16, (int)base.Width - 16);
			}
			if (base.Scene.CollideCheck<Solid>(vector2 + new Vector2(num4 * -2, -2f)))
			{
				for (int k = num5; k < num6; k += 8)
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Scrape, base.TopLeft + new Vector2((float)k + (float)num4 * 2f, -1f), (num4 == 1) ? ((float)Math.PI * 3f / 4f) : ((float)Math.PI / 4f));
				}
			}
			if (base.Scene.CollideCheck<Solid>(vector2 + new Vector2(num4 * -2, base.Height + 2f)))
			{
				for (int l = num5; l < num6; l += 8)
				{
					SceneAs<Level>().ParticlesFG.Emit(P_Scrape, base.BottomLeft + new Vector2((float)l + (float)num4 * 2f, 0f), (num4 == 1) ? ((float)Math.PI * -3f / 4f) : (-(float)Math.PI / 4f));
				}
			}
		}
	}

	[IteratorStateMachine(typeof(_003CSequence_003Ed__24))]
	private IEnumerator Sequence()
	{
		Vector2 start = Position;
		while (true)
		{
			if (!HasPlayerRider())
			{
				yield return null;
				continue;
			}
			sfx.Play((theme == Themes.Normal) ? "event:/game/01_forsaken_city/zip_mover" : "event:/new_content/game/10_farewell/zip_mover");
			Input.Rumble(RumbleStrength.Medium, RumbleLength.Short);
			StartShaking(0.1f);
			yield return 0.1f;
			streetlight.SetAnimationFrame(3);
			StopPlayerRunIntoAnimation = false;
			float at = 0f;
			while (at < 1f)
			{
				yield return null;
				at = Calc.Approach(at, 1f, 2f * Engine.DeltaTime);
				percent = Ease.SineIn(at);
				Vector2 vector = Vector2.Lerp(start, target, percent);
				ScrapeParticlesCheck(vector);
				if (base.Scene.OnInterval(0.1f))
				{
					pathRenderer.CreateSparks();
				}
				MoveTo(vector);
			}
			StartShaking(0.2f);
			Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
			SceneAs<Level>().Shake();
			StopPlayerRunIntoAnimation = true;
			yield return 0.5f;
			StopPlayerRunIntoAnimation = false;
			streetlight.SetAnimationFrame(2);
			at = 0f;
			while (at < 1f)
			{
				yield return null;
				at = Calc.Approach(at, 1f, 0.5f * Engine.DeltaTime);
				percent = 1f - Ease.SineIn(at);
				Vector2 position = Vector2.Lerp(target, start, Ease.SineIn(at));
				MoveTo(position);
			}
			StopPlayerRunIntoAnimation = true;
			StartShaking(0.2f);
			streetlight.SetAnimationFrame(1);
			yield return 0.5f;
		}
	}

	private float mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
