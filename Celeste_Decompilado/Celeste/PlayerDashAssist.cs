using System;
using System.Collections.Generic;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class PlayerDashAssist : Entity
{
	public float Direction;

	public float Scale;

	public Vector2 Offset;

	private List<MTexture> images;

	private EventInstance snapshot;

	private float timer;

	private bool paused;

	private int lastIndex;

	public PlayerDashAssist()
	{
		base.Tag = Tags.Global;
		base.Depth = -1000000;
		Visible = false;
		images = GFX.Game.GetAtlasSubtextures("util/dasharrow/dasharrow");
	}

	public override void Update()
	{
		if (!Engine.DashAssistFreeze)
		{
			if (paused)
			{
				if (!base.Scene.Paused)
				{
					Audio.PauseGameplaySfx = false;
				}
				DisableSnapshot();
				timer = 0f;
				paused = false;
			}
			return;
		}
		paused = true;
		Audio.PauseGameplaySfx = true;
		timer += Engine.RawDeltaTime;
		if (timer > 0.2f && snapshot == null)
		{
			EnableSnapshot();
		}
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			float num = Input.GetAimVector(entity.Facing).Angle();
			if (Calc.AbsAngleDiff(num, Direction) >= 1.5807964f)
			{
				Direction = num;
				Scale = 0f;
			}
			else
			{
				Direction = Calc.AngleApproach(Direction, num, (float)Math.PI * 6f * Engine.RawDeltaTime);
			}
			Scale = Calc.Approach(Scale, 1f, Engine.DeltaTime * 4f);
			int num2 = 1 + (8 + (int)Math.Round(num / ((float)Math.PI / 4f))) % 8;
			if (lastIndex != 0 && lastIndex != num2)
			{
				Audio.Play("event:/game/general/assist_dash_aim", entity.Center, "dash_direction", num2);
			}
			lastIndex = num2;
		}
	}

	public override void Render()
	{
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity == null || !Engine.DashAssistFreeze)
		{
			return;
		}
		MTexture mTexture = null;
		float num = float.MaxValue;
		for (int i = 0; i < 8; i++)
		{
			float num2 = Calc.AngleDiff((float)Math.PI * 2f * ((float)i / 8f), Direction);
			if (Math.Abs(num2) < Math.Abs(num))
			{
				num = num2;
				mTexture = images[i];
			}
		}
		if (mTexture != null)
		{
			if (Math.Abs(num) < 0.05f)
			{
				num = 0f;
			}
			mTexture.DrawOutlineCentered((entity.Center + Offset + Calc.AngleToVector(Direction, 20f)).Round(), Color.White, Ease.BounceOut(Scale), num);
		}
	}

	private void EnableSnapshot()
	{
	}

	private void DisableSnapshot()
	{
		if (snapshot != null)
		{
			Audio.ReleaseSnapshot(snapshot);
			snapshot = null;
		}
	}

	public override void Removed(Scene scene)
	{
		DisableSnapshot();
		base.Removed(scene);
	}

	public override void SceneEnd(Scene scene)
	{
		DisableSnapshot();
		base.SceneEnd(scene);
	}
}
