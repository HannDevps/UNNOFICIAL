using Monocle;

namespace Celeste;

public class OldSiteChaseMusicHandler : Entity
{
	public OldSiteChaseMusicHandler()
	{
		base.Tag = (int)Tags.TransitionUpdate | (int)Tags.Global;
	}

	public override void Update()
	{
		base.Update();
		int num = 1150;
		int num2 = 2832;
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		if (entity != null && Audio.CurrentMusic == "event:/music/lvl2/chase")
		{
			float value = (entity.X - (float)num) / (float)(num2 - num);
			Audio.SetMusicParam("escape", value);
		}
	}
}
