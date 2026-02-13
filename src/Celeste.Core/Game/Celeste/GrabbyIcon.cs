using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class GrabbyIcon : Entity
{
	private bool enabled;

	private Wiggler wiggler;

	public GrabbyIcon()
	{
		base.Depth = -1000001;
		base.Tag = (int)Tags.Global | (int)Tags.PauseUpdate | (int)Tags.TransitionUpdate;
		Add(wiggler = Wiggler.Create(0.1f, 0.3f));
	}

	public override void Update()
	{
		base.Update();
		bool flag = false;
		if (!SceneAs<Level>().InCutscene)
		{
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null && !entity.Dead && Settings.Instance.GrabMode == GrabModes.Toggle && Input.GrabCheck)
			{
				flag = true;
			}
		}
		if (flag != enabled)
		{
			enabled = flag;
			wiggler.Start();
		}
	}

	public override void Render()
	{
		if (enabled)
		{
			Vector2 scale = Vector2.One * (1f + wiggler.Value * 0.2f);
			Player entity = base.Scene.Tracker.GetEntity<Player>();
			if (entity != null)
			{
				GFX.Game["util/glove"].DrawJustified(new Vector2(entity.X, entity.Y - 16f), new Vector2(0.5f, 1f), Color.White, scale);
			}
		}
	}
}
