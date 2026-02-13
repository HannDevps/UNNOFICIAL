using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class IntroCar : JumpThru
{
	private Image bodySprite;

	private Entity wheels;

	private float startY;

	private bool didHaveRider;

	public IntroCar(Vector2 position)
		: base(position, 25, safe: true)
	{
		startY = position.Y;
		base.Depth = 1;
		Add(bodySprite = new Image(GFX.Game["scenery/car/body"]));
		bodySprite.Origin = new Vector2(bodySprite.Width / 2f, bodySprite.Height);
		Hitbox hitbox = new Hitbox(25f, 4f, -15f, -17f);
		Hitbox hitbox2 = new Hitbox(19f, 4f, 8f, -11f);
		base.Collider = new ColliderList(hitbox, hitbox2);
		SurfaceSoundIndex = 2;
	}

	public IntroCar(EntityData data, Vector2 offset)
		: this(data.Position + offset)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Image image = new Image(GFX.Game["scenery/car/wheels"]);
		image.Origin = new Vector2(image.Width / 2f, image.Height);
		wheels = new Entity(Position);
		wheels.Add(image);
		wheels.Depth = 3;
		scene.Add(wheels);
		Level level = scene as Level;
		if (level.Session.Area.ID == 0)
		{
			IntroPavement introPavement = new IntroPavement(new Vector2(level.Bounds.Left, base.Y), (int)(base.X - (float)level.Bounds.Left - 48f));
			introPavement.Depth = -10001;
			level.Add(introPavement);
			level.Add(new IntroCarBarrier(Position + new Vector2(32f, 0f), -10, Color.White));
			level.Add(new IntroCarBarrier(Position + new Vector2(41f, 0f), 5, Color.DarkGray));
		}
	}

	public override void Update()
	{
		bool flag = HasRider();
		if (base.Y > startY && (!flag || base.Y > startY + 1f))
		{
			float moveV = -10f * Engine.DeltaTime;
			MoveV(moveV);
		}
		if (base.Y <= startY && !didHaveRider && flag)
		{
			MoveV(2f);
		}
		if (didHaveRider && !flag)
		{
			Audio.Play("event:/game/00_prologue/car_up", Position);
		}
		didHaveRider = flag;
		base.Update();
	}

	public override int GetLandSoundIndex(Entity entity)
	{
		Audio.Play("event:/game/00_prologue/car_down", Position);
		return -1;
	}
}
