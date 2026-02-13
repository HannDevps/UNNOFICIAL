using System;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class HoldableCollider : Component
{
	private Collider collider;

	public Action<Holdable> OnCollide;

	public HoldableCollider(Action<Holdable> onCollide, Collider collider = null)
		: base(active: false, visible: false)
	{
		this.collider = collider;
		OnCollide = onCollide;
	}

	public bool Check(Holdable holdable)
	{
		Collider collider = base.Entity.Collider;
		if (this.collider != null)
		{
			base.Entity.Collider = this.collider;
		}
		bool result = holdable.Entity.CollideCheck(base.Entity);
		base.Entity.Collider = collider;
		return result;
	}
}
