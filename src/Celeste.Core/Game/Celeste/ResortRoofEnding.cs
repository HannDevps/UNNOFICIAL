using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ResortRoofEnding : Solid
{
	[CompilerGenerated]
	private sealed class _003CWobbleImage_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Image img;

		public float delay;

		public ResortRoofEnding _003C_003E4__this;

		public bool fall;

		public Player player;

		private float _003Corig_003E5__2;

		private float _003Cp_003E5__3;

		private float _003Camount_003E5__4;

		private float _003CspeedY_003E5__5;

		private float _003Cup_003E5__6;

		private float _003Coff_003E5__7;

		private float _003Cp_003E5__8;

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
		public _003CWobbleImage_003Ed__8(int _003C_003E1__state)
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
			ResortRoofEnding resortRoofEnding = _003C_003E4__this;
			float num3;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Corig_003E5__2 = img.Y;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
			{
				_003C_003E1__state = -1;
				for (int i = 0; i < 2; i++)
				{
					resortRoofEnding.Scene.Add(Engine.Pooler.Create<Debris>().Init(resortRoofEnding.Position + img.Position + new Vector2(-4 + i * 8, Calc.Random.Range(0, 8)), '9'));
				}
				if (!fall)
				{
					_003Cp_003E5__3 = 0f;
					_003Camount_003E5__4 = 5f;
					goto IL_00e1;
				}
				if (!fall)
				{
					break;
				}
				goto IL_0239;
			}
			case 2:
				_003C_003E1__state = -1;
				goto IL_00e1;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0239;
			case 4:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__8 += Engine.DeltaTime;
					goto IL_03fb;
				}
				IL_0239:
				if (!resortRoofEnding.BeginFalling)
				{
					int num2 = Calc.Random.Range(0, 2);
					img.Y = _003Corig_003E5__2 + (float)num2;
					if (player != null && Math.Abs(resortRoofEnding.X + img.X - player.X) < 16f)
					{
						player.Sprite.Y = num2;
					}
					_003C_003E2__current = 0.01f;
					_003C_003E1__state = 3;
					return true;
				}
				img.Texture = GFX.Game["decals/3-resort/roofCenter_snapped_" + Calc.Random.Choose("a", "b", "c")];
				resortRoofEnding.Collidable = false;
				_003Camount_003E5__4 = Calc.Random.NextFloat();
				_003Cp_003E5__3 = -24f + Calc.Random.NextFloat(48f);
				_003CspeedY_003E5__5 = 0f - (80f + Calc.Random.NextFloat(80f));
				_003Cup_003E5__6 = new Vector2(0f, -1f).Angle();
				_003Coff_003E5__7 = Calc.Random.NextFloat();
				_003Cp_003E5__8 = 0f;
				goto IL_03fb;
				IL_00e1:
				_003Cp_003E5__3 += Engine.DeltaTime * 16f;
				_003Camount_003E5__4 = Calc.Approach(_003Camount_003E5__4, 1f, Engine.DeltaTime * 5f);
				num3 = (float)Math.Sin(_003Cp_003E5__3) * _003Camount_003E5__4;
				img.Y = _003Corig_003E5__2 + num3;
				if (player != null && Math.Abs(resortRoofEnding.X + img.X - player.X) < 16f)
				{
					player.Sprite.Y = num3;
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
				IL_03fb:
				if (_003Cp_003E5__8 < 4f)
				{
					img.Position += new Vector2(_003Cp_003E5__3, _003CspeedY_003E5__5) * Engine.DeltaTime;
					img.Rotation += _003Camount_003E5__4 * Ease.CubeIn(_003Cp_003E5__8);
					_003Cp_003E5__3 = Calc.Approach(_003Cp_003E5__3, 0f, Engine.DeltaTime * 200f);
					_003CspeedY_003E5__5 += 600f * Engine.DeltaTime;
					if (resortRoofEnding.Scene.OnInterval(0.1f, _003Coff_003E5__7))
					{
						Dust.Burst(resortRoofEnding.Position + img.Position, _003Cup_003E5__6);
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				break;
			}
			player.Sprite.Y = 0f;
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

	private MTexture[] roofCenters = new MTexture[4]
	{
		GFX.Game["decals/3-resort/roofCenter"],
		GFX.Game["decals/3-resort/roofCenter_b"],
		GFX.Game["decals/3-resort/roofCenter_c"],
		GFX.Game["decals/3-resort/roofCenter_d"]
	};

	private List<Image> images = new List<Image>();

	private List<Coroutine> wobbleRoutines = new List<Coroutine>();

	public bool BeginFalling;

	public ResortRoofEnding(EntityData data, Vector2 offset)
		: base(data.Position + offset, data.Width, 2f, safe: true)
	{
		EnableAssistModeChecks = false;
		Image image = new Image(GFX.Game["decals/3-resort/roofEdge_d"]);
		image.CenterOrigin();
		image.X = 8f;
		image.Y = 4f;
		Add(image);
		int i;
		for (i = 0; (float)i < base.Width; i += 16)
		{
			Image image2 = new Image(Calc.Random.Choose(roofCenters));
			image2.CenterOrigin();
			image2.X = i + 8;
			image2.Y = 4f;
			Add(image2);
			images.Add(image2);
		}
		Image image3 = new Image(GFX.Game["decals/3-resort/roofEdge"]);
		image3.CenterOrigin();
		image3.X = i + 8;
		image3.Y = 4f;
		Add(image3);
		images.Add(image3);
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (!(base.Scene as Level).Session.GetFlag("oshiroEnding"))
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				base.Scene.Add(new CS03_Ending(this, entity));
			}
		}
	}

	public override void Render()
	{
		Position += base.Shake;
		base.Render();
		Position -= base.Shake;
	}

	public void Wobble(AngryOshiro ghost, bool fall = false)
	{
		foreach (Coroutine wobbleRoutine in wobbleRoutines)
		{
			wobbleRoutine.RemoveSelf();
		}
		wobbleRoutines.Clear();
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		foreach (Image image in images)
		{
			Coroutine coroutine = new Coroutine(WobbleImage(image, Math.Abs(base.X + image.X - ghost.X) * 0.001f, entity, fall));
			Add(coroutine);
			wobbleRoutines.Add(coroutine);
		}
	}

	[IteratorStateMachine(typeof(_003CWobbleImage_003Ed__8))]
	private IEnumerator WobbleImage(Image img, float delay, Player player, bool fall)
	{
		float orig = img.Y;
		yield return delay;
		for (int i = 0; i < 2; i++)
		{
			base.Scene.Add(Engine.Pooler.Create<Debris>().Init(Position + img.Position + new Vector2(-4 + i * 8, Calc.Random.Range(0, 8)), '9'));
		}
		if (!fall)
		{
			float p = 0f;
			float amount = 5f;
			while (true)
			{
				p += Engine.DeltaTime * 16f;
				amount = Calc.Approach(amount, 1f, Engine.DeltaTime * 5f);
				float num = (float)Math.Sin(p) * amount;
				img.Y = orig + num;
				if (player != null && Math.Abs(base.X + img.X - player.X) < 16f)
				{
					player.Sprite.Y = num;
				}
				yield return null;
			}
		}
		if (fall)
		{
			while (!BeginFalling)
			{
				int num2 = Calc.Random.Range(0, 2);
				img.Y = orig + (float)num2;
				if (player != null && Math.Abs(base.X + img.X - player.X) < 16f)
				{
					player.Sprite.Y = num2;
				}
				yield return 0.01f;
			}
			img.Texture = GFX.Game["decals/3-resort/roofCenter_snapped_" + Calc.Random.Choose("a", "b", "c")];
			Collidable = false;
			float amount = Calc.Random.NextFloat();
			float p = -24f + Calc.Random.NextFloat(48f);
			float speedY = 0f - (80f + Calc.Random.NextFloat(80f));
			float up = new Vector2(0f, -1f).Angle();
			float off = Calc.Random.NextFloat();
			for (float p2 = 0f; p2 < 4f; p2 += Engine.DeltaTime)
			{
				img.Position += new Vector2(p, speedY) * Engine.DeltaTime;
				img.Rotation += amount * Ease.CubeIn(p2);
				p = Calc.Approach(p, 0f, Engine.DeltaTime * 200f);
				speedY += 600f * Engine.DeltaTime;
				if (base.Scene.OnInterval(0.1f, off))
				{
					Dust.Burst(Position + img.Position, up);
				}
				yield return null;
			}
		}
		player.Sprite.Y = 0f;
	}
}
