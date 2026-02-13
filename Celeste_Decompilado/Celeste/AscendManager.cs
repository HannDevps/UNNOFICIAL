using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class AscendManager : Entity
{
	public class Streaks : Entity
	{
		private class Particle
		{
			public Vector2 Position;

			public float Speed;

			public int Index;

			public int Color;
		}

		private const float MinSpeed = 600f;

		private const float MaxSpeed = 2000f;

		public float Alpha = 1f;

		private Particle[] particles = new Particle[80];

		private List<MTexture> textures;

		private Color[] colors;

		private Color[] alphaColors;

		private AscendManager manager;

		public Streaks(AscendManager manager)
		{
			this.manager = manager;
			if (manager == null || !manager.Dark)
			{
				colors = new Color[2]
				{
					Color.White,
					Calc.HexToColor("e69ecb")
				};
			}
			else
			{
				colors = new Color[2]
				{
					Calc.HexToColor("041b44"),
					Calc.HexToColor("011230")
				};
			}
			base.Depth = 20;
			textures = GFX.Game.GetAtlasSubtextures("scenery/launch/slice");
			alphaColors = new Color[colors.Length];
			for (int i = 0; i < particles.Length; i++)
			{
				float num = 160f + Calc.Random.Range(24f, 144f) * (float)Calc.Random.Choose(-1, 1);
				float y = Calc.Random.NextFloat(436f);
				float speed = Calc.ClampedMap(Math.Abs(num - 160f), 0f, 160f, 0.25f) * Calc.Random.Range(600f, 2000f);
				particles[i] = new Particle
				{
					Position = new Vector2(num, y),
					Speed = speed,
					Index = Calc.Random.Next(textures.Count),
					Color = Calc.Random.Next(colors.Length)
				};
			}
		}

		public override void Update()
		{
			base.Update();
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i].Position.Y += particles[i].Speed * Engine.DeltaTime;
			}
		}

		public override void Render()
		{
			float num = Ease.SineInOut(((manager != null) ? manager.fade : 1f) * Alpha);
			Vector2 position = (base.Scene as Level).Camera.Position;
			for (int i = 0; i < colors.Length; i++)
			{
				alphaColors[i] = colors[i] * num;
			}
			for (int j = 0; j < particles.Length; j++)
			{
				Vector2 position2 = particles[j].Position;
				position2.X = Mod(position2.X, 320f);
				position2.Y = -128f + Mod(position2.Y, 436f);
				position2 += position;
				Vector2 scale = new Vector2
				{
					X = Calc.ClampedMap(particles[j].Speed, 600f, 2000f, 1f, 0.25f),
					Y = Calc.ClampedMap(particles[j].Speed, 600f, 2000f, 1f, 2f)
				};
				scale *= Calc.ClampedMap(particles[j].Speed, 600f, 2000f, 1f, 4f);
				textures[particles[j].Index].DrawCentered(color: alphaColors[particles[j].Color], position: position2, scale: scale);
			}
			Draw.Rect(position.X - 10f, position.Y - 10f, 26f, 200f, alphaColors[0]);
			Draw.Rect(position.X + 320f - 16f, position.Y - 10f, 26f, 200f, alphaColors[0]);
		}
	}

	public class Clouds : Entity
	{
		private class Particle
		{
			public Vector2 Position;

			public float Speed;

			public int Index;
		}

		public float Alpha;

		private AscendManager manager;

		private List<MTexture> textures;

		private Particle[] particles = new Particle[10];

		private Color color;

		public Clouds(AscendManager manager)
		{
			this.manager = manager;
			if (manager == null || !manager.Dark)
			{
				color = Calc.HexToColor("b64a86");
			}
			else
			{
				color = Calc.HexToColor("082644");
			}
			base.Depth = -1000000;
			textures = GFX.Game.GetAtlasSubtextures("scenery/launch/cloud");
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i] = new Particle
				{
					Position = new Vector2(Calc.Random.NextFloat(320f), Calc.Random.NextFloat(900f)),
					Speed = Calc.Random.Range(400, 800),
					Index = Calc.Random.Next(textures.Count)
				};
			}
		}

		public override void Update()
		{
			base.Update();
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i].Position.Y += particles[i].Speed * Engine.DeltaTime;
			}
		}

		public override void Render()
		{
			float num = ((manager != null) ? manager.fade : 1f) * Alpha;
			Color color = this.color * num;
			Vector2 position = (base.Scene as Level).Camera.Position;
			for (int i = 0; i < particles.Length; i++)
			{
				Vector2 position2 = particles[i].Position;
				position2.Y = -360f + Mod(position2.Y, 900f);
				position2 += position;
				textures[particles[i].Index].DrawCentered(position2, color);
			}
		}
	}

	private class Fader : Entity
	{
		public float Fade;

		private AscendManager manager;

		public Fader(AscendManager manager)
		{
			this.manager = manager;
			base.Depth = -1000010;
		}

		public override void Render()
		{
			if (Fade > 0f)
			{
				Vector2 position = (base.Scene as Level).Camera.Position;
				Draw.Rect(position.X - 10f, position.Y - 10f, 340f, 200f, (manager.Dark ? Color.Black : Color.White) * Fade);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__15 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AscendManager _003C_003E4__this;

		private Player _003Cplayer_003E5__2;

		private CS07_Ascend _003Ccs_003E5__3;

		private Vector2 _003Cfrom_003E5__4;

		private Fader _003Cfader_003E5__5;

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
		public _003CRoutine_003Ed__15(int _003C_003E1__state)
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
			AscendManager ascendManager = _003C_003E4__this;
			Streaks entity;
			Clouds clouds;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cplayer_003E5__2 = ascendManager.Scene.Tracker.GetEntity<Player>();
				goto IL_0096;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0096;
			case 2:
				_003C_003E1__state = -1;
				goto IL_00db;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0350;
			case 4:
				_003C_003E1__state = -1;
				if (ascendManager.Ch9Ending)
				{
					ascendManager.level.Add(new CS10_FreeBird());
					goto IL_0272;
				}
				if (!string.IsNullOrEmpty(ascendManager.cutscene))
				{
					_003C_003E2__current = 0.25f;
					_003C_003E1__state = 6;
					return true;
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 9;
				return true;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0272;
			case 6:
				_003C_003E1__state = -1;
				_003Ccs_003E5__3 = new CS07_Ascend(ascendManager.index, ascendManager.cutscene, ascendManager.Dark);
				ascendManager.level.Add(_003Ccs_003E5__3);
				_003C_003E2__current = null;
				_003C_003E1__state = 7;
				return true;
			case 7:
				_003C_003E1__state = -1;
				goto IL_0319;
			case 8:
				_003C_003E1__state = -1;
				goto IL_0319;
			case 9:
				_003C_003E1__state = -1;
				goto IL_0350;
			case 10:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__4 = _003Cplayer_003E5__2.Position;
				_003Cp_003E5__6 = 0f;
				goto IL_0451;
			case 11:
				_003C_003E1__state = -1;
				_003Cp_003E5__6 += Engine.DeltaTime / 1f;
				goto IL_0451;
			case 12:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__6 += Engine.DeltaTime / 0.5f;
					break;
				}
				IL_0451:
				if (_003Cp_003E5__6 < 1f)
				{
					_003Cplayer_003E5__2.Position = Vector2.Lerp(_003Cfrom_003E5__4, _003Cfrom_003E5__4 + new Vector2(0f, 60f), Ease.CubeInOut(_003Cp_003E5__6)) + Calc.Random.ShakeVector();
					Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
					_003C_003E2__current = null;
					_003C_003E1__state = 11;
					return true;
				}
				_003Cfader_003E5__5 = new Fader(ascendManager);
				ascendManager.Scene.Add(_003Cfader_003E5__5);
				_003Cfrom_003E5__4 = _003Cplayer_003E5__2.Position;
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
				_003Cp_003E5__6 = 0f;
				break;
				IL_0350:
				ascendManager.level.CanRetry = false;
				_003Cplayer_003E5__2.Sprite.Play("launch");
				Audio.Play("event:/char/madeline/summit_flytonext", _003Cplayer_003E5__2.Position);
				_003C_003E2__current = 0.25f;
				_003C_003E1__state = 10;
				return true;
				IL_0319:
				if (_003Ccs_003E5__3.Running)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 8;
					return true;
				}
				_003Ccs_003E5__3 = null;
				goto IL_0350;
				IL_0272:
				_003C_003E2__current = null;
				_003C_003E1__state = 5;
				return true;
				IL_0096:
				if (_003Cplayer_003E5__2 == null || _003Cplayer_003E5__2.Y > ascendManager.Y)
				{
					_003Cplayer_003E5__2 = ascendManager.Scene.Tracker.GetEntity<Player>();
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				if (ascendManager.index == 9)
				{
					_003C_003E2__current = 1.6f;
					_003C_003E1__state = 2;
					return true;
				}
				goto IL_00db;
				IL_00db:
				entity = new Streaks(ascendManager);
				ascendManager.Scene.Add(entity);
				clouds = null;
				if (!ascendManager.Dark)
				{
					clouds = new Clouds(ascendManager);
					ascendManager.Scene.Add(clouds);
				}
				ascendManager.level.Session.SetFlag("beginswap_" + ascendManager.index);
				_003Cplayer_003E5__2.Sprite.Play("launch");
				_003Cplayer_003E5__2.Speed = Vector2.Zero;
				_003Cplayer_003E5__2.StateMachine.State = 11;
				_003Cplayer_003E5__2.DummyGravity = false;
				_003Cplayer_003E5__2.DummyAutoAnimate = false;
				if (!string.IsNullOrWhiteSpace(ascendManager.ambience))
				{
					if (ascendManager.ambience.Equals("null", StringComparison.InvariantCultureIgnoreCase))
					{
						Audio.SetAmbience(null);
					}
					else
					{
						Audio.SetAmbience(SFX.EventnameByHandle(ascendManager.ambience));
					}
				}
				if (ascendManager.introLaunch)
				{
					ascendManager.FadeSnapTo(1f);
					ascendManager.level.Camera.Position = _003Cplayer_003E5__2.Center + new Vector2(-160f, -90f);
					_003C_003E2__current = 2.3f;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = ascendManager.FadeTo(1f, ascendManager.Dark ? 2f : 0.8f);
				_003C_003E1__state = 4;
				return true;
			}
			if (_003Cp_003E5__6 < 1f)
			{
				float y = _003Cplayer_003E5__2.Y;
				_003Cplayer_003E5__2.Position = Vector2.Lerp(_003Cfrom_003E5__4, _003Cfrom_003E5__4 + new Vector2(0f, -160f), Ease.SineIn(_003Cp_003E5__6));
				if (_003Cp_003E5__6 == 0f || Calc.OnInterval(_003Cplayer_003E5__2.Y, y, 16f))
				{
					ascendManager.level.Add(Engine.Pooler.Create<SpeedRing>().Init(_003Cplayer_003E5__2.Center, new Vector2(0f, -1f).Angle(), Color.White));
				}
				if (_003Cp_003E5__6 >= 0.5f)
				{
					_003Cfader_003E5__5.Fade = (_003Cp_003E5__6 - 0.5f) * 2f;
				}
				else
				{
					_003Cfader_003E5__5.Fade = 0f;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 12;
				return true;
			}
			_003Cfrom_003E5__4 = default(Vector2);
			_003Cfader_003E5__5 = null;
			ascendManager.level.CanRetry = true;
			ascendManager.outTheTop = true;
			_003Cplayer_003E5__2.Y = ascendManager.level.Bounds.Top;
			_003Cplayer_003E5__2.SummitLaunch(_003Cplayer_003E5__2.X);
			_003Cplayer_003E5__2.DummyGravity = true;
			_003Cplayer_003E5__2.DummyAutoAnimate = true;
			ascendManager.level.Session.SetFlag("bgswap_" + ascendManager.index);
			ascendManager.level.NextTransitionDuration = 0.05f;
			if (ascendManager.introLaunch)
			{
				ascendManager.level.Add(new HeightDisplay(-1));
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
	private sealed class _003CFadeTo_003Ed__19 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AscendManager _003C_003E4__this;

		public float target;

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
		public _003CFadeTo_003Ed__19(int _003C_003E1__state)
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
			AscendManager ascendManager = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if ((ascendManager.fade = Calc.Approach(ascendManager.fade, target, Engine.DeltaTime / duration)) != target)
			{
				ascendManager.FadeSnapTo(ascendManager.fade);
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			ascendManager.FadeSnapTo(target);
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

	private const string BeginSwapFlag = "beginswap_";

	private const string BgSwapFlag = "bgswap_";

	public readonly bool Dark;

	public readonly bool Ch9Ending;

	private bool introLaunch;

	private int index;

	private string cutscene;

	private Level level;

	private float fade;

	private float scroll;

	private bool outTheTop;

	private Color background;

	private string ambience;

	public AscendManager(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		base.Tag = Tags.TransitionUpdate;
		base.Depth = 8900;
		index = data.Int("index");
		cutscene = data.Attr("cutscene");
		introLaunch = data.Bool("intro_launch");
		Dark = data.Bool("dark");
		Ch9Ending = cutscene.Equals("CH9_FREE_BIRD", StringComparison.InvariantCultureIgnoreCase);
		ambience = data.Attr("ambience");
		background = (Dark ? Color.Black : Calc.HexToColor("75a0ab"));
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = base.Scene as Level;
		Add(new Coroutine(Routine()));
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__15))]
	private IEnumerator Routine()
	{
		Player player = base.Scene.Tracker.GetEntity<Player>();
		while (player == null || player.Y > base.Y)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			yield return null;
		}
		if (index == 9)
		{
			yield return 1.6f;
		}
		Streaks entity = new Streaks(this);
		base.Scene.Add(entity);
		if (!Dark)
		{
			Clouds entity2 = new Clouds(this);
			base.Scene.Add(entity2);
		}
		level.Session.SetFlag("beginswap_" + index);
		player.Sprite.Play("launch");
		player.Speed = Vector2.Zero;
		player.StateMachine.State = 11;
		player.DummyGravity = false;
		player.DummyAutoAnimate = false;
		if (!string.IsNullOrWhiteSpace(ambience))
		{
			if (ambience.Equals("null", StringComparison.InvariantCultureIgnoreCase))
			{
				Audio.SetAmbience(null);
			}
			else
			{
				Audio.SetAmbience(SFX.EventnameByHandle(ambience));
			}
		}
		if (introLaunch)
		{
			FadeSnapTo(1f);
			level.Camera.Position = player.Center + new Vector2(-160f, -90f);
			yield return 2.3f;
		}
		else
		{
			yield return FadeTo(1f, Dark ? 2f : 0.8f);
			if (Ch9Ending)
			{
				level.Add(new CS10_FreeBird());
				while (true)
				{
					yield return null;
				}
			}
			if (!string.IsNullOrEmpty(cutscene))
			{
				yield return 0.25f;
				CS07_Ascend cs = new CS07_Ascend(index, cutscene, Dark);
				level.Add(cs);
				yield return null;
				while (cs.Running)
				{
					yield return null;
				}
			}
			else
			{
				yield return 0.5f;
			}
		}
		level.CanRetry = false;
		player.Sprite.Play("launch");
		Audio.Play("event:/char/madeline/summit_flytonext", player.Position);
		yield return 0.25f;
		Vector2 from = player.Position;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 1f)
		{
			player.Position = Vector2.Lerp(from, from + new Vector2(0f, 60f), Ease.CubeInOut(p)) + Calc.Random.ShakeVector();
			Input.Rumble(RumbleStrength.Light, RumbleLength.Short);
			yield return null;
		}
		Fader fader = new Fader(this);
		base.Scene.Add(fader);
		from = player.Position;
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / 0.5f)
		{
			float y = player.Y;
			player.Position = Vector2.Lerp(from, from + new Vector2(0f, -160f), Ease.SineIn(p));
			if (p == 0f || Calc.OnInterval(player.Y, y, 16f))
			{
				level.Add(Engine.Pooler.Create<SpeedRing>().Init(player.Center, new Vector2(0f, -1f).Angle(), Color.White));
			}
			if (p >= 0.5f)
			{
				fader.Fade = (p - 0.5f) * 2f;
			}
			else
			{
				fader.Fade = 0f;
			}
			yield return null;
		}
		level.CanRetry = true;
		outTheTop = true;
		player.Y = level.Bounds.Top;
		player.SummitLaunch(player.X);
		player.DummyGravity = true;
		player.DummyAutoAnimate = true;
		level.Session.SetFlag("bgswap_" + index);
		level.NextTransitionDuration = 0.05f;
		if (introLaunch)
		{
			level.Add(new HeightDisplay(-1));
		}
	}

	public override void Update()
	{
		scroll += Engine.DeltaTime * 240f;
		base.Update();
	}

	public override void Render()
	{
		Draw.Rect(level.Camera.X - 10f, level.Camera.Y - 10f, 340f, 200f, background * fade);
	}

	public override void Removed(Scene scene)
	{
		FadeSnapTo(0f);
		level.Session.SetFlag("bgswap_" + index, setTo: false);
		level.Session.SetFlag("beginswap_" + index, setTo: false);
		if (outTheTop)
		{
			ScreenWipe.WipeColor = (Dark ? Color.Black : Color.White);
			if (introLaunch)
			{
				new MountainWipe(base.Scene, wipeIn: true);
			}
			else if (index == 0)
			{
				AreaData.Get(1).DoScreenWipe(base.Scene, wipeIn: true);
			}
			else if (index == 1)
			{
				AreaData.Get(2).DoScreenWipe(base.Scene, wipeIn: true);
			}
			else if (index == 2)
			{
				AreaData.Get(3).DoScreenWipe(base.Scene, wipeIn: true);
			}
			else if (index == 3)
			{
				AreaData.Get(4).DoScreenWipe(base.Scene, wipeIn: true);
			}
			else if (index == 4)
			{
				AreaData.Get(5).DoScreenWipe(base.Scene, wipeIn: true);
			}
			else if (index == 5)
			{
				AreaData.Get(7).DoScreenWipe(base.Scene, wipeIn: true);
			}
			else if (index >= 9)
			{
				AreaData.Get(10).DoScreenWipe(base.Scene, wipeIn: true);
			}
			ScreenWipe.WipeColor = Color.Black;
		}
		base.Removed(scene);
	}

	[IteratorStateMachine(typeof(_003CFadeTo_003Ed__19))]
	private IEnumerator FadeTo(float target, float duration = 0.8f)
	{
		while ((fade = Calc.Approach(fade, target, Engine.DeltaTime / duration)) != target)
		{
			FadeSnapTo(fade);
			yield return null;
		}
		FadeSnapTo(target);
	}

	private void FadeSnapTo(float target)
	{
		fade = target;
		SetSnowAlpha(1f - fade);
		SetBloom(fade * 0.1f);
		if (!Dark)
		{
			return;
		}
		foreach (Parallax item in level.Background.GetEach<Parallax>())
		{
			item.CameraOffset.Y -= 25f * target;
		}
		foreach (Parallax item2 in level.Foreground.GetEach<Parallax>())
		{
			item2.Alpha = 1f - fade;
		}
	}

	private void SetBloom(float add)
	{
		level.Bloom.Base = AreaData.Get(level).BloomBase + add;
	}

	private void SetSnowAlpha(float value)
	{
		Snow snow = level.Foreground.Get<Snow>();
		if (snow != null)
		{
			snow.Alpha = value;
		}
		RainFG rainFG = level.Foreground.Get<RainFG>();
		if (rainFG != null)
		{
			rainFG.Alpha = value;
		}
		WindSnowFG windSnowFG = level.Foreground.Get<WindSnowFG>();
		if (windSnowFG != null)
		{
			windSnowFG.Alpha = value;
		}
	}

	private static float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
