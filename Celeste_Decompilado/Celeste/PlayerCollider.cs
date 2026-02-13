using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class PlayerCollider : Component
{
	public Action<Player> OnCollide;

	public Collider Collider;

	public Collider FeatherCollider;

	public PlayerCollider(Action<Player> onCollide, Collider collider = null, Collider featherCollider = null)
		: base(active: false, visible: false)
	{
		OnCollide = onCollide;
		Collider = collider;
		FeatherCollider = featherCollider;
	}

	public bool Check(Player player)
	{
		Collider collider = Collider;
		if (FeatherCollider != null && player.StateMachine.State == 19)
		{
			collider = FeatherCollider;
		}
		if (collider == null)
		{
			if (player.CollideCheck(base.Entity))
			{
				OnCollide(player);
				return true;
			}
			return false;
		}
		Collider collider2 = base.Entity.Collider;
		base.Entity.Collider = collider;
		bool num = player.CollideCheck(base.Entity);
		base.Entity.Collider = collider2;
		if (num)
		{
			OnCollide(player);
			return true;
		}
		return false;
	}

	public override void DebugRender(Camera camera)
	{
		if (Collider != null)
		{
			Collider collider = base.Entity.Collider;
			base.Entity.Collider = Collider;
			Collider.Render(camera, Color.HotPink);
			base.Entity.Collider = collider;
		}
	}
}
