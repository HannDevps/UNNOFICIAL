using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class DustTrackSpinner : TrackSpinner
{
	private DustGraphic dusty;

	private Vector2 outwards;

	public DustTrackSpinner(EntityData data, Vector2 offset)
		: base(data, offset)
	{
		Add(dusty = new DustGraphic(ignoreSolids: true));
		dusty.EyeDirection = (dusty.EyeTargetDirection = (base.End - base.Start).SafeNormalize());
		dusty.OnEstablish = Establish;
		base.Depth = -50;
	}

	private void Establish()
	{
		Vector2 vector = (base.End - base.Start).SafeNormalize();
		Vector2 vector2 = new Vector2(0f - vector.Y, vector.X);
		bool flag = base.Scene.CollideCheck<Solid>(new Rectangle((int)(base.X + vector2.X * 4f) - 2, (int)(base.Y + vector2.Y * 4f) - 2, 4, 4));
		if (!flag)
		{
			vector2 = -vector2;
			flag = base.Scene.CollideCheck<Solid>(new Rectangle((int)(base.X + vector2.X * 4f) - 2, (int)(base.Y + vector2.Y * 4f) - 2, 4, 4));
		}
		if (!flag)
		{
			return;
		}
		float num = (base.End - base.Start).Length();
		for (int i = 8; (float)i < num && flag; i += 8)
		{
			flag = flag && base.Scene.CollideCheck<Solid>(new Rectangle((int)(base.X + vector2.X * 4f + vector.X * (float)i) - 2, (int)(base.Y + vector2.Y * 4f + vector.Y * (float)i) - 2, 4, 4));
		}
		if (!flag)
		{
			return;
		}
		List<DustGraphic.Node> list = null;
		if (vector2.X < 0f)
		{
			list = dusty.LeftNodes;
		}
		else if (vector2.X > 0f)
		{
			list = dusty.RightNodes;
		}
		else if (vector2.Y < 0f)
		{
			list = dusty.TopNodes;
		}
		else if (vector2.Y > 0f)
		{
			list = dusty.BottomNodes;
		}
		if (list != null)
		{
			foreach (DustGraphic.Node item in list)
			{
				item.Enabled = false;
			}
		}
		outwards = -vector2;
		dusty.Position -= vector2;
		dusty.EyeDirection = (dusty.EyeTargetDirection = Calc.AngleToVector(Calc.AngleLerp(outwards.Angle(), Up ? (Angle + (float)Math.PI) : Angle, 0.3f), 1f));
	}

	public override void Update()
	{
		base.Update();
		if (Moving && PauseTimer < 0f && base.Scene.OnInterval(0.02f))
		{
			SceneAs<Level>().ParticlesBG.Emit(DustStaticSpinner.P_Move, 1, Position, Vector2.One * 4f);
		}
	}

	public override void OnPlayer(Player player)
	{
		base.OnPlayer(player);
		dusty.OnHitPlayer();
	}

	public override void OnTrackEnd()
	{
		if (outwards != Vector2.Zero)
		{
			dusty.EyeTargetDirection = Calc.AngleToVector(Calc.AngleLerp(outwards.Angle(), Up ? (Angle + (float)Math.PI) : Angle, 0.3f), 1f);
			return;
		}
		dusty.EyeTargetDirection = Calc.AngleToVector(Up ? (Angle + (float)Math.PI) : Angle, 1f);
		dusty.EyeFlip = -dusty.EyeFlip;
	}
}
