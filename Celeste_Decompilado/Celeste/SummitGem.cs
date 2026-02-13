using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class SummitGem : Entity
{
	private class BgFlash : Entity
	{
		private float alpha = 1f;

		public BgFlash()
		{
			base.Depth = 10100;
			base.Tag = Tags.Persistent;
		}

		public override void Update()
		{
			base.Update();
			alpha = Calc.Approach(alpha, 0f, Engine.DeltaTime * 0.5f);
			if (alpha <= 0f)
			{
				RemoveSelf();
			}
		}

		public override void Render()
		{
			Vector2 position = (base.Scene as Level).Camera.Position;
			Draw.Rect(position.X - 10f, position.Y - 10f, 340f, 200f, Color.Black * alpha);
		}
	}

	[CompilerGenerated]
	private sealed class _003CSmashRoutine_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SummitGem _003C_003E4__this;

		public Player player;

		public Level level;

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
		public _003CSmashRoutine_003Ed__11(int _003C_003E1__state)
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
			SummitGem summitGem = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
			{
				_003C_003E1__state = -1;
				summitGem.Visible = false;
				summitGem.Collidable = false;
				player.Stamina = 110f;
				SoundEmitter.Play("event:/game/07_summit/gem_get", summitGem);
				Session session = (summitGem.Scene as Level).Session;
				session.DoNotLoad.Add(summitGem.GID);
				session.SummitGems[summitGem.GemID] = true;
				SaveData.Instance.RegisterSummitGem(summitGem.GemID);
				level.Shake();
				Celeste.Freeze(0.1f);
				P_Shatter.Color = GemColors[summitGem.GemID];
				float num2 = player.Speed.Angle();
				level.ParticlesFG.Emit(P_Shatter, 5, summitGem.Position, Vector2.One * 4f, num2 - (float)Math.PI / 2f);
				level.ParticlesFG.Emit(P_Shatter, 5, summitGem.Position, Vector2.One * 4f, num2 + (float)Math.PI / 2f);
				SlashFx.Burst(summitGem.Position, num2);
				for (int i = 0; i < 10; i++)
				{
					summitGem.Scene.Add(new AbsorbOrb(summitGem.Position, player));
				}
				level.Flash(Color.White, drawPlayerOver: true);
				summitGem.Scene.Add(new BgFlash());
				Engine.TimeRate = 0.5f;
				break;
			}
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (Engine.TimeRate < 1f)
			{
				Engine.TimeRate += Engine.RawDeltaTime * 0.5f;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			summitGem.RemoveSelf();
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

	public static ParticleType P_Shatter;

	public static readonly Color[] GemColors = new Color[6]
	{
		Calc.HexToColor("9ee9ff"),
		Calc.HexToColor("54baff"),
		Calc.HexToColor("90ff2d"),
		Calc.HexToColor("ffd300"),
		Calc.HexToColor("ff609d"),
		Calc.HexToColor("c5e1ba")
	};

	public int GemID;

	public EntityID GID;

	private Sprite sprite;

	private Wiggler scaleWiggler;

	private Vector2 moveWiggleDir;

	private Wiggler moveWiggler;

	private float bounceSfxDelay;

	public SummitGem(EntityData data, Vector2 position, EntityID gid)
		: base(data.Position + position)
	{
		GID = gid;
		GemID = data.Int("gem");
		base.Collider = new Hitbox(12f, 12f, -6f, -6f);
		Add(sprite = new Sprite(GFX.Game, "collectables/summitgems/" + GemID + "/gem"));
		sprite.AddLoop("idle", "", 0.08f);
		sprite.Play("idle");
		sprite.CenterOrigin();
		if (SaveData.Instance.SummitGems != null && SaveData.Instance.SummitGems[GemID])
		{
			sprite.Color = Color.White * 0.5f;
		}
		Add(scaleWiggler = Wiggler.Create(0.5f, 4f, delegate(float f)
		{
			sprite.Scale = Vector2.One * (1f + f * 0.3f);
		}));
		moveWiggler = Wiggler.Create(0.8f, 2f);
		moveWiggler.StartZero = true;
		Add(moveWiggler);
		Add(new PlayerCollider(OnPlayer));
	}

	private void OnPlayer(Player player)
	{
		Level level = base.Scene as Level;
		if (player.DashAttacking)
		{
			Add(new Coroutine(SmashRoutine(player, level)));
			return;
		}
		player.PointBounce(base.Center);
		moveWiggler.Start();
		scaleWiggler.Start();
		moveWiggleDir = (base.Center - player.Center).SafeNormalize(Vector2.UnitY);
		Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
		if (bounceSfxDelay <= 0f)
		{
			Audio.Play("event:/game/general/crystalheart_bounce", Position);
			bounceSfxDelay = 0.1f;
		}
	}

	[IteratorStateMachine(typeof(_003CSmashRoutine_003Ed__11))]
	private IEnumerator SmashRoutine(Player player, Level level)
	{
		Visible = false;
		Collidable = false;
		player.Stamina = 110f;
		SoundEmitter.Play("event:/game/07_summit/gem_get", this);
		Session session = (base.Scene as Level).Session;
		session.DoNotLoad.Add(GID);
		session.SummitGems[GemID] = true;
		SaveData.Instance.RegisterSummitGem(GemID);
		level.Shake();
		Celeste.Freeze(0.1f);
		P_Shatter.Color = GemColors[GemID];
		float num = player.Speed.Angle();
		level.ParticlesFG.Emit(P_Shatter, 5, Position, Vector2.One * 4f, num - (float)Math.PI / 2f);
		level.ParticlesFG.Emit(P_Shatter, 5, Position, Vector2.One * 4f, num + (float)Math.PI / 2f);
		SlashFx.Burst(Position, num);
		for (int i = 0; i < 10; i++)
		{
			base.Scene.Add(new AbsorbOrb(Position, player));
		}
		level.Flash(Color.White, drawPlayerOver: true);
		base.Scene.Add(new BgFlash());
		Engine.TimeRate = 0.5f;
		while (Engine.TimeRate < 1f)
		{
			Engine.TimeRate += Engine.RawDeltaTime * 0.5f;
			yield return null;
		}
		RemoveSelf();
	}

	public override void Update()
	{
		base.Update();
		bounceSfxDelay -= Engine.DeltaTime;
		sprite.Position = moveWiggleDir * moveWiggler.Value * -8f;
	}
}
