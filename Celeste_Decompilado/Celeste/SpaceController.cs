using Monocle;

namespace Celeste;

public class SpaceController : Entity
{
	private Level level;

	public override void Added(Scene scene)
	{
		base.Added(scene);
		level = SceneAs<Level>();
	}

	public override void Update()
	{
		base.Update();
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null)
		{
			if (entity.Top > level.Camera.Bottom + 12f)
			{
				entity.Bottom = level.Camera.Top - 4f;
			}
			else if (entity.Bottom < level.Camera.Top - 4f)
			{
				entity.Top = level.Camera.Bottom + 12f;
			}
		}
	}
}
