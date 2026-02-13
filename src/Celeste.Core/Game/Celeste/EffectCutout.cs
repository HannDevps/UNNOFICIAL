using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class EffectCutout : Component
{
	public float Alpha = 1f;

	private Rectangle lastSize;

	private bool lastVisible;

	private float lastAlpha;

	public int Left => (int)base.Entity.Collider.AbsoluteLeft;

	public int Right => (int)base.Entity.Collider.AbsoluteRight;

	public int Top => (int)base.Entity.Collider.AbsoluteTop;

	public int Bottom => (int)base.Entity.Collider.AbsoluteBottom;

	public Rectangle Bounds => base.Entity.Collider.Bounds;

	public EffectCutout()
		: base(active: true, visible: true)
	{
	}

	public override void Update()
	{
		bool flag = Visible && base.Entity.Visible;
		Rectangle bounds = Bounds;
		if (lastSize != bounds || lastAlpha != Alpha || lastVisible != flag)
		{
			MakeLightsDirty();
			lastSize = bounds;
			lastAlpha = Alpha;
			lastVisible = flag;
		}
	}

	public override void Removed(Entity entity)
	{
		MakeLightsDirty();
		base.Removed(entity);
	}

	public override void EntityRemoved(Scene scene)
	{
		MakeLightsDirty();
		base.EntityRemoved(scene);
	}

	private void MakeLightsDirty()
	{
		Rectangle bounds = Bounds;
		foreach (VertexLight component in base.Entity.Scene.Tracker.GetComponents<VertexLight>())
		{
			if (!component.Dirty)
			{
				Rectangle value = new Rectangle((int)(component.Center.X - component.EndRadius), (int)(component.Center.Y - component.EndRadius), (int)component.EndRadius * 2, (int)component.EndRadius * 2);
				if (bounds.Intersects(value) || lastSize.Intersects(value))
				{
					component.Dirty = true;
				}
			}
		}
	}
}
