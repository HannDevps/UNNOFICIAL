using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class WindTrigger : Trigger
{
	public WindController.Patterns Pattern;

	public WindTrigger(EntityData data, Vector2 offset)
		: base(data, offset)
	{
		Pattern = data.Enum("pattern", WindController.Patterns.None);
	}

	public override void OnEnter(Player player)
	{
		base.OnEnter(player);
		WindController windController = base.Scene.Entities.FindFirst<WindController>();
		if (windController == null)
		{
			windController = new WindController(Pattern);
			base.Scene.Add(windController);
		}
		else
		{
			windController.SetPattern(Pattern);
		}
	}
}
