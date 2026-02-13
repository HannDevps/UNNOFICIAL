using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class TriggerSpikes : Entity
{
	public enum Directions
	{
		Up,
		Down,
		Left,
		Right
	}

	private struct SpikeInfo
	{
		public TriggerSpikes Parent;

		public int Index;

		public Vector2 WorldPosition;

		public bool Triggered;

		public float RetractTimer;

		public float DelayTimer;

		public float Lerp;

		public float ParticleTimerOffset;

		public int TextureIndex;

		public float TextureRotation;

		public int DustOutDistance;

		public int TentacleColor;

		public float TentacleFrame;

		public void Update()
		{
			if (Triggered)
			{
				if (DelayTimer > 0f)
				{
					DelayTimer -= Engine.DeltaTime;
					if (DelayTimer <= 0f)
					{
						if (PlayerCheck())
						{
							DelayTimer = 0.05f;
						}
						else
						{
							Audio.Play("event:/game/03_resort/fluff_tendril_emerge", WorldPosition);
						}
					}
				}
				else
				{
					Lerp = Calc.Approach(Lerp, 1f, 8f * Engine.DeltaTime);
				}
				TextureRotation += Engine.DeltaTime * 1.2f;
			}
			else
			{
				Lerp = Calc.Approach(Lerp, 0f, 4f * Engine.DeltaTime);
				TentacleFrame += Engine.DeltaTime * 12f;
				if (Lerp <= 0f)
				{
					Triggered = false;
				}
			}
		}

		public bool PlayerCheck()
		{
			return Parent.PlayerCheck(Index);
		}

		public bool OnPlayer(Player player, Vector2 outwards)
		{
			if (!Triggered)
			{
				Audio.Play("event:/game/03_resort/fluff_tendril_touch", WorldPosition);
				Triggered = true;
				DelayTimer = 0.4f;
				RetractTimer = 6f;
			}
			else if (Lerp >= 1f)
			{
				player.Die(outwards);
				return true;
			}
			return false;
		}
	}

	private const float RetractTime = 6f;

	private const float DelayTime = 0.4f;

	private Directions direction;

	private Vector2 outwards;

	private Vector2 offset;

	private PlayerCollider pc;

	private Vector2 shakeOffset;

	private SpikeInfo[] spikes;

	private List<MTexture> dustTextures;

	private List<MTexture> tentacleTextures;

	private Color[] tentacleColors;

	private int size;

	public TriggerSpikes(Vector2 position, int size, Directions direction)
		: base(position)
	{
		this.size = size;
		this.direction = direction;
		switch (direction)
		{
		case Directions.Up:
			tentacleTextures = GFX.Game.GetAtlasSubtextures("danger/triggertentacle/wiggle_v");
			outwards = new Vector2(0f, -1f);
			offset = new Vector2(0f, -1f);
			base.Collider = new Hitbox(size, 4f, 0f, -4f);
			Add(new SafeGroundBlocker());
			Add(new LedgeBlocker(UpSafeBlockCheck));
			break;
		case Directions.Down:
			tentacleTextures = GFX.Game.GetAtlasSubtextures("danger/triggertentacle/wiggle_v");
			outwards = new Vector2(0f, 1f);
			base.Collider = new Hitbox(size, 4f);
			break;
		case Directions.Left:
			tentacleTextures = GFX.Game.GetAtlasSubtextures("danger/triggertentacle/wiggle_h");
			outwards = new Vector2(-1f, 0f);
			base.Collider = new Hitbox(4f, size, -4f);
			Add(new SafeGroundBlocker());
			Add(new LedgeBlocker(SideSafeBlockCheck));
			break;
		case Directions.Right:
			tentacleTextures = GFX.Game.GetAtlasSubtextures("danger/triggertentacle/wiggle_h");
			outwards = new Vector2(1f, 0f);
			offset = new Vector2(1f, 0f);
			base.Collider = new Hitbox(4f, size);
			Add(new SafeGroundBlocker());
			Add(new LedgeBlocker(SideSafeBlockCheck));
			break;
		}
		Add(pc = new PlayerCollider(OnCollide));
		Add(new StaticMover
		{
			OnShake = OnShake,
			SolidChecker = IsRiding,
			JumpThruChecker = IsRiding
		});
		Add(new DustEdge(RenderSpikes));
		base.Depth = -50;
	}

	public TriggerSpikes(EntityData data, Vector2 offset, Directions dir)
		: this(data.Position + offset, GetSize(data, dir), dir)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Vector3[] edgeColors = DustStyles.Get(scene).EdgeColors;
		dustTextures = GFX.Game.GetAtlasSubtextures("danger/dustcreature/base");
		tentacleColors = new Color[edgeColors.Length];
		for (int i = 0; i < tentacleColors.Length; i++)
		{
			tentacleColors[i] = Color.Lerp(new Color(edgeColors[i]), Color.DarkSlateBlue, 0.4f);
		}
		Vector2 vector = new Vector2(Math.Abs(outwards.Y), Math.Abs(outwards.X));
		spikes = new SpikeInfo[size / 4];
		for (int j = 0; j < spikes.Length; j++)
		{
			spikes[j].Parent = this;
			spikes[j].Index = j;
			spikes[j].WorldPosition = Position + vector * (2 + j * 4);
			spikes[j].ParticleTimerOffset = Calc.Random.NextFloat(0.25f);
			spikes[j].TextureIndex = Calc.Random.Next(dustTextures.Count);
			spikes[j].DustOutDistance = Calc.Random.Choose(3, 4, 6);
			spikes[j].TentacleColor = Calc.Random.Next(tentacleColors.Length);
			spikes[j].TentacleFrame = Calc.Random.NextFloat(tentacleTextures.Count);
		}
	}

	private void OnShake(Vector2 amount)
	{
		shakeOffset += amount;
	}

	private bool UpSafeBlockCheck(Player player)
	{
		int num = 8 * (int)player.Facing;
		int num2 = (int)((player.Left + (float)num - base.Left) / 4f);
		int num3 = (int)((player.Right + (float)num - base.Left) / 4f);
		if (num3 < 0 || num2 >= spikes.Length)
		{
			return false;
		}
		num2 = Math.Max(num2, 0);
		num3 = Math.Min(num3, spikes.Length - 1);
		for (int i = num2; i <= num3; i++)
		{
			if (spikes[i].Lerp >= 1f)
			{
				return true;
			}
		}
		return false;
	}

	private bool SideSafeBlockCheck(Player player)
	{
		int num = (int)((player.Top - base.Top) / 4f);
		int num2 = (int)((player.Bottom - base.Top) / 4f);
		if (num2 < 0 || num >= spikes.Length)
		{
			return false;
		}
		num = Math.Max(num, 0);
		num2 = Math.Min(num2, spikes.Length - 1);
		for (int i = num; i <= num2; i++)
		{
			if (spikes[i].Lerp >= 1f)
			{
				return true;
			}
		}
		return false;
	}

	private void OnCollide(Player player)
	{
		GetPlayerCollideIndex(player, out var minIndex, out var maxIndex);
		if (maxIndex >= 0 && minIndex < spikes.Length)
		{
			minIndex = Math.Max(minIndex, 0);
			maxIndex = Math.Min(maxIndex, spikes.Length - 1);
			for (int i = minIndex; i <= maxIndex && !spikes[i].OnPlayer(player, outwards); i++)
			{
			}
		}
	}

	private void GetPlayerCollideIndex(Player player, out int minIndex, out int maxIndex)
	{
		minIndex = (maxIndex = -1);
		switch (direction)
		{
		case Directions.Up:
			if (player.Speed.Y >= 0f)
			{
				minIndex = (int)((player.Left - base.Left) / 4f);
				maxIndex = (int)((player.Right - base.Left) / 4f);
			}
			break;
		case Directions.Down:
			if (player.Speed.Y <= 0f)
			{
				minIndex = (int)((player.Left - base.Left) / 4f);
				maxIndex = (int)((player.Right - base.Left) / 4f);
			}
			break;
		case Directions.Left:
			if (player.Speed.X >= 0f)
			{
				minIndex = (int)((player.Top - base.Top) / 4f);
				maxIndex = (int)((player.Bottom - base.Top) / 4f);
			}
			break;
		case Directions.Right:
			if (player.Speed.X <= 0f)
			{
				minIndex = (int)((player.Top - base.Top) / 4f);
				maxIndex = (int)((player.Bottom - base.Top) / 4f);
			}
			break;
		}
	}

	private bool PlayerCheck(int spikeIndex)
	{
		Player player = CollideFirst<Player>();
		if (player != null)
		{
			GetPlayerCollideIndex(player, out var minIndex, out var maxIndex);
			if (minIndex <= spikeIndex + 1)
			{
				return maxIndex >= spikeIndex - 1;
			}
			return false;
		}
		return false;
	}

	private static int GetSize(EntityData data, Directions dir)
	{
		if ((uint)dir > 1u)
		{
			_ = dir - 2;
			_ = 1;
			return data.Height;
		}
		return data.Width;
	}

	public override void Update()
	{
		base.Update();
		for (int i = 0; i < spikes.Length; i++)
		{
			spikes[i].Update();
		}
	}

	public override void Render()
	{
		base.Render();
		Vector2 vector = new Vector2(Math.Abs(outwards.Y), Math.Abs(outwards.X));
		int count = tentacleTextures.Count;
		Vector2 one = Vector2.One;
		Vector2 justify = new Vector2(0f, 0.5f);
		if (direction == Directions.Left)
		{
			one.X = -1f;
		}
		else if (direction == Directions.Up)
		{
			one.Y = -1f;
		}
		if (direction == Directions.Up || direction == Directions.Down)
		{
			justify = new Vector2(0.5f, 0f);
		}
		for (int i = 0; i < spikes.Length; i++)
		{
			if (!spikes[i].Triggered)
			{
				MTexture mTexture = tentacleTextures[(int)(spikes[i].TentacleFrame % (float)count)];
				Vector2 vector2 = Position + vector * (2 + i * 4);
				mTexture.DrawJustified(vector2 + vector, justify, Color.Black, one, 0f);
				mTexture.DrawJustified(vector2, justify, tentacleColors[spikes[i].TentacleColor], one, 0f);
			}
		}
		RenderSpikes();
	}

	private void RenderSpikes()
	{
		Vector2 vector = new Vector2(Math.Abs(outwards.Y), Math.Abs(outwards.X));
		for (int i = 0; i < spikes.Length; i++)
		{
			if (spikes[i].Triggered)
			{
				MTexture mTexture = dustTextures[spikes[i].TextureIndex];
				Vector2 position = Position + outwards * (-4f + spikes[i].Lerp * (float)spikes[i].DustOutDistance) + vector * (2 + i * 4);
				mTexture.DrawCentered(position, Color.White, 0.5f * spikes[i].Lerp, spikes[i].TextureRotation);
			}
		}
	}

	private bool IsRiding(Solid solid)
	{
		return direction switch
		{
			Directions.Up => CollideCheckOutside(solid, Position + Vector2.UnitY), 
			Directions.Down => CollideCheckOutside(solid, Position - Vector2.UnitY), 
			Directions.Left => CollideCheckOutside(solid, Position + Vector2.UnitX), 
			Directions.Right => CollideCheckOutside(solid, Position - Vector2.UnitX), 
			_ => false, 
		};
	}

	private bool IsRiding(JumpThru jumpThru)
	{
		if (direction != Directions.Up)
		{
			return false;
		}
		return CollideCheck(jumpThru, Position + Vector2.UnitY);
	}
}
