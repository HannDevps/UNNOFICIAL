using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class HeartGemDoor : Entity
{
	private struct Particle
	{
		public Vector2 Position;

		public float Speed;

		public Color Color;
	}

	private class WhiteLine : Entity
	{
		private float fade = 1f;

		private int blockSize;

		public WhiteLine(Vector2 origin, int blockSize)
			: base(origin)
		{
			base.Depth = -1000000;
			this.blockSize = blockSize;
		}

		public override void Update()
		{
			base.Update();
			fade = Calc.Approach(fade, 0f, Engine.DeltaTime);
			if (!(fade <= 0f))
			{
				return;
			}
			RemoveSelf();
			Level level = SceneAs<Level>();
			for (float num = (int)level.Camera.Left; num < level.Camera.Right; num += 1f)
			{
				if (num < base.X || num >= base.X + (float)blockSize)
				{
					level.Particles.Emit(P_Slice, new Vector2(num, base.Y));
				}
			}
		}

		public override void Render()
		{
			Vector2 position = (base.Scene as Level).Camera.Position;
			float num = Math.Max(1f, 4f * fade);
			Draw.Rect(position.X - 10f, base.Y - num / 2f, 340f, num, Color.White);
		}
	}

	[CompilerGenerated]
	private sealed class _003CRoutine_003Ed__32 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeartGemDoor _003C_003E4__this;

		private Level _003Clevel_003E5__2;

		private float _003CtopTo_003E5__3;

		private float _003CbotTo_003E5__4;

		private float _003CtopFrom_003E5__5;

		private float _003CbotFrom_003E5__6;

		private float _003Cp_003E5__7;

		private int _003Ctarget_003E5__8;

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
		public _003CRoutine_003Ed__32(int _003C_003E1__state)
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
			HeartGemDoor heartGemDoor = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Clevel_003E5__2 = heartGemDoor.Scene as Level;
				if (heartGemDoor.startHidden)
				{
					goto IL_0061;
				}
				goto IL_04a7;
			case 1:
			{
				_003C_003E1__state = -1;
				Player entity = heartGemDoor.Scene.Tracker.GetEntity<Player>();
				if (entity == null || !(Math.Abs(entity.X - heartGemDoor.Center.X) < 100f))
				{
					goto IL_0061;
				}
				Audio.Play("event:/new_content/game/10_farewell/heart_door", heartGemDoor.Position);
				heartGemDoor.Visible = true;
				heartGemDoor.heartAlpha = 0f;
				_003CtopTo_003E5__3 = heartGemDoor.TopSolid.Y;
				_003CbotTo_003E5__4 = heartGemDoor.BotSolid.Y;
				_003CtopFrom_003E5__5 = (heartGemDoor.TopSolid.Y -= 240f);
				_003CbotFrom_003E5__6 = (heartGemDoor.BotSolid.Y -= 240f);
				_003Cp_003E5__7 = 0f;
				goto IL_0283;
			}
			case 2:
				_003C_003E1__state = -1;
				_003Cp_003E5__7 += Engine.DeltaTime * 1.2f;
				goto IL_0283;
			case 3:
				_003C_003E1__state = -1;
				goto IL_0309;
			case 4:
				_003C_003E1__state = -1;
				goto IL_04a7;
			case 5:
				_003C_003E1__state = -1;
				if (heartGemDoor.Counter < (float)_003Ctarget_003E5__8)
				{
					Audio.Play("event:/game/09_core/frontdoor_heartfill", heartGemDoor.Position);
				}
				goto IL_0490;
			case 6:
				_003C_003E1__state = -1;
				goto IL_04a7;
			case 7:
				_003C_003E1__state = -1;
				heartGemDoor.Scene.Add(new WhiteLine(heartGemDoor.Position, heartGemDoor.Size));
				_003Clevel_003E5__2.Shake();
				Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
				_003Clevel_003E5__2.Flash(Color.White * 0.5f);
				Audio.Play("event:/game/09_core/frontdoor_unlock", heartGemDoor.Position);
				heartGemDoor.Opened = true;
				_003Clevel_003E5__2.Session.SetFlag("opened_heartgem_door_" + heartGemDoor.Requires);
				heartGemDoor.offset = 0f;
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 8;
				return true;
			case 8:
				_003C_003E1__state = -1;
				_003CbotFrom_003E5__6 = heartGemDoor.TopSolid.Y;
				_003CtopFrom_003E5__5 = heartGemDoor.TopSolid.Y - heartGemDoor.openDistance;
				_003CbotTo_003E5__4 = heartGemDoor.BotSolid.Y;
				_003CtopTo_003E5__3 = heartGemDoor.BotSolid.Y + heartGemDoor.openDistance;
				_003Cp_003E5__7 = 0f;
				break;
			case 9:
				{
					_003C_003E1__state = -1;
					_003Cp_003E5__7 += Engine.DeltaTime;
					break;
				}
				IL_04a7:
				if (!heartGemDoor.Opened && heartGemDoor.Counter < (float)heartGemDoor.Requires)
				{
					Player entity2 = heartGemDoor.Scene.Tracker.GetEntity<Player>();
					if (entity2 != null && Math.Abs(entity2.X - heartGemDoor.Center.X) < 80f && entity2.X < heartGemDoor.X)
					{
						if (heartGemDoor.Counter == 0f && heartGemDoor.HeartGems > 0)
						{
							Audio.Play("event:/game/09_core/frontdoor_heartfill", heartGemDoor.Position);
						}
						if (heartGemDoor.HeartGems < heartGemDoor.Requires)
						{
							_003Clevel_003E5__2.Session.SetFlag("granny_door");
						}
						int num2 = (int)heartGemDoor.Counter;
						_003Ctarget_003E5__8 = Math.Min(heartGemDoor.HeartGems, heartGemDoor.Requires);
						heartGemDoor.Counter = Calc.Approach(heartGemDoor.Counter, _003Ctarget_003E5__8, Engine.DeltaTime * (float)heartGemDoor.Requires * 0.8f);
						if (num2 != (int)heartGemDoor.Counter)
						{
							_003C_003E2__current = 0.1f;
							_003C_003E1__state = 5;
							return true;
						}
					}
					else
					{
						heartGemDoor.Counter = Calc.Approach(heartGemDoor.Counter, 0f, Engine.DeltaTime * (float)heartGemDoor.Requires * 4f);
					}
					goto IL_0490;
				}
				_003C_003E2__current = 0.5f;
				_003C_003E1__state = 7;
				return true;
				IL_0283:
				if (_003Cp_003E5__7 < 1f)
				{
					float num3 = Ease.CubeIn(_003Cp_003E5__7);
					heartGemDoor.TopSolid.MoveToY(_003CtopFrom_003E5__5 + (_003CtopTo_003E5__3 - _003CtopFrom_003E5__5) * num3);
					heartGemDoor.BotSolid.MoveToY(_003CbotFrom_003E5__6 + (_003CbotTo_003E5__4 - _003CbotFrom_003E5__6) * num3);
					DashBlock dashBlock = heartGemDoor.Scene.CollideFirst<DashBlock>(heartGemDoor.BotSolid.Collider.Bounds);
					if (dashBlock != null)
					{
						_003Clevel_003E5__2.Shake(0.5f);
						Celeste.Freeze(0.1f);
						Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
						dashBlock.Break(heartGemDoor.BotSolid.BottomCenter, new Vector2(0f, 1f), playSound: true, playDebrisSound: false);
						Player entity3 = heartGemDoor.Scene.Tracker.GetEntity<Player>();
						if (entity3 != null && Math.Abs(entity3.X - heartGemDoor.Center.X) < 40f)
						{
							entity3.PointBounce(entity3.Position + Vector2.UnitX * 8f);
						}
					}
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				_003Clevel_003E5__2.Shake(0.5f);
				Celeste.Freeze(0.1f);
				heartGemDoor.TopSolid.Y = _003CtopTo_003E5__3;
				heartGemDoor.BotSolid.Y = _003CbotTo_003E5__4;
				goto IL_0309;
				IL_0061:
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
				IL_0309:
				if (heartGemDoor.heartAlpha < 1f)
				{
					heartGemDoor.heartAlpha = Calc.Approach(heartGemDoor.heartAlpha, 1f, Engine.DeltaTime * 2f);
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				_003C_003E2__current = 0.6f;
				_003C_003E1__state = 4;
				return true;
				IL_0490:
				_003C_003E2__current = null;
				_003C_003E1__state = 6;
				return true;
			}
			if (_003Cp_003E5__7 < 1f)
			{
				_003Clevel_003E5__2.Shake();
				heartGemDoor.openPercent = Ease.CubeIn(_003Cp_003E5__7);
				heartGemDoor.TopSolid.MoveToY(MathHelper.Lerp(_003CbotFrom_003E5__6, _003CtopFrom_003E5__5, heartGemDoor.openPercent));
				heartGemDoor.BotSolid.MoveToY(MathHelper.Lerp(_003CbotTo_003E5__4, _003CtopTo_003E5__3, heartGemDoor.openPercent));
				if (_003Cp_003E5__7 >= 0.4f && _003Clevel_003E5__2.OnInterval(0.1f))
				{
					for (int i = 4; i < heartGemDoor.Size; i += 4)
					{
						_003Clevel_003E5__2.ParticlesBG.Emit(P_Shimmer, 1, new Vector2(heartGemDoor.TopSolid.Left + (float)i + 1f, heartGemDoor.TopSolid.Bottom - 2f), new Vector2(2f, 2f), -(float)Math.PI / 2f);
						_003Clevel_003E5__2.ParticlesBG.Emit(P_Shimmer, 1, new Vector2(heartGemDoor.BotSolid.Left + (float)i + 1f, heartGemDoor.BotSolid.Top + 2f), new Vector2(2f, 2f), (float)Math.PI / 2f);
					}
				}
				_003C_003E2__current = null;
				_003C_003E1__state = 9;
				return true;
			}
			heartGemDoor.TopSolid.MoveToY(_003CtopFrom_003E5__5);
			heartGemDoor.BotSolid.MoveToY(_003CtopTo_003E5__3);
			heartGemDoor.openPercent = 1f;
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

	private const string OpenedFlag = "opened_heartgem_door_";

	public static ParticleType P_Shimmer;

	public static ParticleType P_Slice;

	public readonly int Requires;

	public int Size;

	private readonly float openDistance;

	private float openPercent;

	private Solid TopSolid;

	private Solid BotSolid;

	private float offset;

	private Vector2 mist;

	private MTexture temp = new MTexture();

	private List<MTexture> icon;

	private Particle[] particles = new Particle[50];

	private bool startHidden;

	private float heartAlpha = 1f;

	public int HeartGems
	{
		get
		{
			if (SaveData.Instance.CheatMode)
			{
				return Requires;
			}
			return SaveData.Instance.TotalHeartGems;
		}
	}

	public float Counter { get; private set; }

	public bool Opened { get; private set; }

	private float openAmount => openPercent * openDistance;

	public HeartGemDoor(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Requires = data.Int("requires");
		Add(new CustomBloom(RenderBloom));
		Size = data.Width;
		openDistance = 32f;
		Vector2? vector = data.FirstNodeNullable(offset);
		if (vector.HasValue)
		{
			openDistance = Math.Abs(vector.Value.Y - base.Y);
		}
		icon = GFX.Game.GetAtlasSubtextures("objects/heartdoor/icon");
		startHidden = data.Bool("startHidden");
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Level level = scene as Level;
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Position = new Vector2(Calc.Random.NextFloat(Size), Calc.Random.NextFloat(level.Bounds.Height));
			particles[i].Speed = Calc.Random.Range(4, 12);
			particles[i].Color = Color.White * Calc.Random.Range(0.2f, 0.6f);
		}
		level.Add(TopSolid = new Solid(new Vector2(base.X, level.Bounds.Top - 32), Size, base.Y - (float)level.Bounds.Top + 32f, safe: true));
		TopSolid.SurfaceSoundIndex = 32;
		TopSolid.SquishEvenInAssistMode = true;
		TopSolid.EnableAssistModeChecks = false;
		level.Add(BotSolid = new Solid(new Vector2(base.X, base.Y), Size, (float)level.Bounds.Bottom - base.Y + 32f, safe: true));
		BotSolid.SurfaceSoundIndex = 32;
		BotSolid.SquishEvenInAssistMode = true;
		BotSolid.EnableAssistModeChecks = false;
		if ((base.Scene as Level).Session.GetFlag("opened_heartgem_door_" + Requires))
		{
			Opened = true;
			Visible = true;
			openPercent = 1f;
			Counter = Requires;
			TopSolid.Y -= openDistance;
			BotSolid.Y += openDistance;
		}
		else
		{
			Add(new Coroutine(Routine()));
		}
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		if (Opened)
		{
			base.Scene.CollideFirst<DashBlock>(BotSolid.Collider.Bounds)?.RemoveSelf();
		}
		else if (startHidden)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && entity.X > base.X)
			{
				startHidden = false;
				base.Scene.CollideFirst<DashBlock>(BotSolid.Collider.Bounds)?.RemoveSelf();
			}
			else
			{
				Visible = false;
			}
		}
	}

	[IteratorStateMachine(typeof(_003CRoutine_003Ed__32))]
	private IEnumerator Routine()
	{
		Level level = base.Scene as Level;
		float botFrom;
		float topFrom;
		float botTo;
		float topTo;
		if (startHidden)
		{
			Player entity;
			do
			{
				yield return null;
				entity = base.Scene.Tracker.GetEntity<Player>();
			}
			while (entity == null || !(Math.Abs(entity.X - base.Center.X) < 100f));
			Audio.Play("event:/new_content/game/10_farewell/heart_door", Position);
			Visible = true;
			heartAlpha = 0f;
			topTo = TopSolid.Y;
			botTo = BotSolid.Y;
			topFrom = (TopSolid.Y -= 240f);
			botFrom = (BotSolid.Y -= 240f);
			for (float p = 0f; p < 1f; p += Engine.DeltaTime * 1.2f)
			{
				float num = Ease.CubeIn(p);
				TopSolid.MoveToY(topFrom + (topTo - topFrom) * num);
				BotSolid.MoveToY(botFrom + (botTo - botFrom) * num);
				DashBlock dashBlock = base.Scene.CollideFirst<DashBlock>(BotSolid.Collider.Bounds);
				if (dashBlock != null)
				{
					level.Shake(0.5f);
					Celeste.Freeze(0.1f);
					Input.Rumble(RumbleStrength.Strong, RumbleLength.Medium);
					dashBlock.Break(BotSolid.BottomCenter, new Vector2(0f, 1f), playSound: true, playDebrisSound: false);
					Player entity2 = base.Scene.Tracker.GetEntity<Player>();
					if (entity2 != null && Math.Abs(entity2.X - base.Center.X) < 40f)
					{
						entity2.PointBounce(entity2.Position + Vector2.UnitX * 8f);
					}
				}
				yield return null;
			}
			level.Shake(0.5f);
			Celeste.Freeze(0.1f);
			TopSolid.Y = topTo;
			BotSolid.Y = botTo;
			while (heartAlpha < 1f)
			{
				heartAlpha = Calc.Approach(heartAlpha, 1f, Engine.DeltaTime * 2f);
				yield return null;
			}
			yield return 0.6f;
		}
		while (!Opened && Counter < (float)Requires)
		{
			Player entity3 = base.Scene.Tracker.GetEntity<Player>();
			if (entity3 != null && Math.Abs(entity3.X - base.Center.X) < 80f && entity3.X < base.X)
			{
				if (Counter == 0f && HeartGems > 0)
				{
					Audio.Play("event:/game/09_core/frontdoor_heartfill", Position);
				}
				if (HeartGems < Requires)
				{
					level.Session.SetFlag("granny_door");
				}
				int num2 = (int)Counter;
				int target = Math.Min(HeartGems, Requires);
				Counter = Calc.Approach(Counter, target, Engine.DeltaTime * (float)Requires * 0.8f);
				if (num2 != (int)Counter)
				{
					yield return 0.1f;
					if (Counter < (float)target)
					{
						Audio.Play("event:/game/09_core/frontdoor_heartfill", Position);
					}
				}
			}
			else
			{
				Counter = Calc.Approach(Counter, 0f, Engine.DeltaTime * (float)Requires * 4f);
			}
			yield return null;
		}
		yield return 0.5f;
		base.Scene.Add(new WhiteLine(Position, Size));
		level.Shake();
		Input.Rumble(RumbleStrength.Strong, RumbleLength.Long);
		level.Flash(Color.White * 0.5f);
		Audio.Play("event:/game/09_core/frontdoor_unlock", Position);
		Opened = true;
		level.Session.SetFlag("opened_heartgem_door_" + Requires);
		offset = 0f;
		yield return 0.6f;
		botFrom = TopSolid.Y;
		topFrom = TopSolid.Y - openDistance;
		botTo = BotSolid.Y;
		topTo = BotSolid.Y + openDistance;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime)
		{
			level.Shake();
			openPercent = Ease.CubeIn(p);
			TopSolid.MoveToY(MathHelper.Lerp(botFrom, topFrom, openPercent));
			BotSolid.MoveToY(MathHelper.Lerp(botTo, topTo, openPercent));
			if (p >= 0.4f && level.OnInterval(0.1f))
			{
				for (int i = 4; i < Size; i += 4)
				{
					level.ParticlesBG.Emit(P_Shimmer, 1, new Vector2(TopSolid.Left + (float)i + 1f, TopSolid.Bottom - 2f), new Vector2(2f, 2f), -(float)Math.PI / 2f);
					level.ParticlesBG.Emit(P_Shimmer, 1, new Vector2(BotSolid.Left + (float)i + 1f, BotSolid.Top + 2f), new Vector2(2f, 2f), (float)Math.PI / 2f);
				}
			}
			yield return null;
		}
		TopSolid.MoveToY(topFrom);
		BotSolid.MoveToY(topTo);
		openPercent = 1f;
	}

	public override void Update()
	{
		base.Update();
		if (!Opened)
		{
			offset += 12f * Engine.DeltaTime;
			mist.X -= 4f * Engine.DeltaTime;
			mist.Y -= 24f * Engine.DeltaTime;
			for (int i = 0; i < particles.Length; i++)
			{
				particles[i].Position.Y += particles[i].Speed * Engine.DeltaTime;
			}
		}
	}

	public void RenderBloom()
	{
		if (!Opened && Visible)
		{
			DrawBloom(new Rectangle((int)TopSolid.X, (int)TopSolid.Y, Size, (int)(TopSolid.Height + BotSolid.Height)));
		}
	}

	private void DrawBloom(Rectangle bounds)
	{
		Draw.Rect(bounds.Left - 4, bounds.Top, 2f, bounds.Height, Color.White * 0.25f);
		Draw.Rect(bounds.Left - 2, bounds.Top, 2f, bounds.Height, Color.White * 0.5f);
		Draw.Rect(bounds, Color.White * 0.75f);
		Draw.Rect(bounds.Right, bounds.Top, 2f, bounds.Height, Color.White * 0.5f);
		Draw.Rect(bounds.Right + 2, bounds.Top, 2f, bounds.Height, Color.White * 0.25f);
	}

	private void DrawMist(Rectangle bounds, Vector2 mist)
	{
		Color color = Color.White * 0.6f;
		MTexture mTexture = GFX.Game["objects/heartdoor/mist"];
		int num = mTexture.Width / 2;
		int num2 = mTexture.Height / 2;
		for (int i = 0; i < bounds.Width; i += num)
		{
			for (int j = 0; j < bounds.Height; j += num2)
			{
				mTexture.GetSubtexture((int)Mod(mist.X, num), (int)Mod(mist.Y, num2), Math.Min(num, bounds.Width - i), Math.Min(num2, bounds.Height - j), temp);
				temp.Draw(new Vector2(bounds.X + i, bounds.Y + j), Vector2.Zero, color);
			}
		}
	}

	private void DrawInterior(Rectangle bounds)
	{
		Draw.Rect(bounds, Calc.HexToColor("18668f"));
		DrawMist(bounds, mist);
		DrawMist(bounds, new Vector2(mist.Y, mist.X) * 1.5f);
		Vector2 vector = (base.Scene as Level).Camera.Position;
		if (Opened)
		{
			vector = Vector2.Zero;
		}
		for (int i = 0; i < particles.Length; i++)
		{
			Vector2 vector2 = particles[i].Position + vector * 0.2f;
			vector2.X = Mod(vector2.X, bounds.Width);
			vector2.Y = Mod(vector2.Y, bounds.Height);
			Draw.Pixel.Draw(new Vector2(bounds.X, bounds.Y) + vector2, Vector2.Zero, particles[i].Color);
		}
	}

	private void DrawEdges(Rectangle bounds, Color color)
	{
		MTexture mTexture = GFX.Game["objects/heartdoor/edge"];
		MTexture mTexture2 = GFX.Game["objects/heartdoor/top"];
		int num = (int)(offset % 8f);
		if (num > 0)
		{
			mTexture.GetSubtexture(0, 8 - num, 7, num, temp);
			temp.DrawJustified(new Vector2(bounds.Left + 4, bounds.Top), new Vector2(0.5f, 0f), color, new Vector2(-1f, 1f));
			temp.DrawJustified(new Vector2(bounds.Right - 4, bounds.Top), new Vector2(0.5f, 0f), color, new Vector2(1f, 1f));
		}
		for (int i = num; i < bounds.Height; i += 8)
		{
			mTexture.GetSubtexture(0, 0, 8, Math.Min(8, bounds.Height - i), temp);
			temp.DrawJustified(new Vector2(bounds.Left + 4, bounds.Top + i), new Vector2(0.5f, 0f), color, new Vector2(-1f, 1f));
			temp.DrawJustified(new Vector2(bounds.Right - 4, bounds.Top + i), new Vector2(0.5f, 0f), color, new Vector2(1f, 1f));
		}
		for (int j = 0; j < bounds.Width; j += 8)
		{
			mTexture2.DrawCentered(new Vector2(bounds.Left + 4 + j, bounds.Top + 4), color);
			mTexture2.DrawCentered(new Vector2(bounds.Left + 4 + j, bounds.Bottom - 4), color, new Vector2(1f, -1f));
		}
	}

	public override void Render()
	{
		Color color = (Opened ? (Color.White * 0.25f) : Color.White);
		if (!Opened && TopSolid.Visible && BotSolid.Visible)
		{
			Rectangle bounds = new Rectangle((int)TopSolid.X, (int)TopSolid.Y, Size, (int)(TopSolid.Height + BotSolid.Height));
			DrawInterior(bounds);
			DrawEdges(bounds, color);
		}
		else
		{
			if (TopSolid.Visible)
			{
				Rectangle bounds2 = new Rectangle((int)TopSolid.X, (int)TopSolid.Y, Size, (int)TopSolid.Height);
				DrawInterior(bounds2);
				DrawEdges(bounds2, color);
			}
			if (BotSolid.Visible)
			{
				Rectangle bounds3 = new Rectangle((int)BotSolid.X, (int)BotSolid.Y, Size, (int)BotSolid.Height);
				DrawInterior(bounds3);
				DrawEdges(bounds3, color);
			}
		}
		if (!(heartAlpha > 0f))
		{
			return;
		}
		float num = 12f;
		int num2 = (int)((float)(Size - 8) / num);
		int num3 = (int)Math.Ceiling((float)Requires / (float)num2);
		Color color2 = color * heartAlpha;
		for (int i = 0; i < num3; i++)
		{
			int num4 = (((i + 1) * num2 < Requires) ? num2 : (Requires - i * num2));
			Vector2 vector = new Vector2(base.X + (float)Size * 0.5f, base.Y) + new Vector2((float)(-num4) / 2f + 0.5f, (float)(-num3) / 2f + (float)i + 0.5f) * num;
			if (Opened)
			{
				if (i < num3 / 2)
				{
					vector.Y -= openAmount + 8f;
				}
				else
				{
					vector.Y += openAmount + 8f;
				}
			}
			for (int j = 0; j < num4; j++)
			{
				int num5 = i * num2 + j;
				float num6 = Ease.CubeIn(Calc.ClampedMap(Counter, num5, (float)num5 + 1f));
				icon[(int)(num6 * (float)(icon.Count - 1))].DrawCentered(vector + new Vector2((float)j * num, 0f), color2);
			}
		}
	}

	private float Mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
