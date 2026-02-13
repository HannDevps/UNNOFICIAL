using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
[Pooled]
public class SlashFx : Entity
{
	public Sprite Sprite;

	public Vector2 Direction;

	public SlashFx()
	{
		Add(Sprite = new Sprite(GFX.Game, "effects/slash/"));
		Sprite.Add("play", "", 0.1f, 0, 1, 2, 3);
		Sprite.CenterOrigin();
		Sprite.OnFinish = delegate
		{
			RemoveSelf();
		};
		base.Depth = -100;
	}

	public override void Update()
	{
		Position += Direction * 8f * Engine.DeltaTime;
		base.Update();
	}

	public static SlashFx Burst(Vector2 position, float direction)
	{
		Scene scene = Engine.Scene;
		SlashFx slashFx = Engine.Pooler.Create<SlashFx>();
		scene.Add(slashFx);
		slashFx.Position = position;
		slashFx.Direction = Calc.AngleToVector(direction, 1f);
		slashFx.Sprite.Play("play", restart: true);
		slashFx.Sprite.Scale = Vector2.One;
		slashFx.Sprite.Rotation = 0f;
		if (Math.Abs(direction - (float)Math.PI) > 0.01f)
		{
			slashFx.Sprite.Rotation = direction;
		}
		slashFx.Visible = (slashFx.Active = true);
		return slashFx;
	}
}
