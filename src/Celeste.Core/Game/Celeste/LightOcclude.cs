using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class LightOcclude : Component
{
	public float Alpha = 1f;

	private Rectangle? bounds;

	public Rectangle RenderBounds;

	private Rectangle lastSize;

	private bool lastVisible;

	private float lastAlpha;

	public int Left
	{
		get
		{
			if (bounds.HasValue)
			{
				return (int)base.Entity.X + bounds.Value.Left;
			}
			return (int)base.Entity.Collider.AbsoluteLeft;
		}
	}

	public int Width
	{
		get
		{
			if (bounds.HasValue)
			{
				return bounds.Value.Width;
			}
			return (int)base.Entity.Collider.Width;
		}
	}

	public int Top
	{
		get
		{
			if (bounds.HasValue)
			{
				return (int)base.Entity.Y + bounds.Value.Top;
			}
			return (int)base.Entity.Collider.AbsoluteTop;
		}
	}

	public int Height
	{
		get
		{
			if (bounds.HasValue)
			{
				return bounds.Value.Height;
			}
			return (int)base.Entity.Collider.Height;
		}
	}

	public int Right => Left + Width;

	public int Bottom => Top + Height;

	public LightOcclude(float alpha = 1f)
		: base(active: true, visible: true)
	{
		Alpha = alpha;
	}

	public LightOcclude(Rectangle bounds, float alpha = 1f)
		: this(alpha)
	{
		this.bounds = bounds;
	}

	public override void Update()
	{
		base.Update();
		bool flag = Visible && base.Entity.Visible;
		Rectangle rectangle = new Rectangle(Left, Top, Width, Height);
		if (lastSize != rectangle || lastVisible != flag || lastAlpha != Alpha)
		{
			MakeLightsDirty();
			lastVisible = flag;
			lastSize = rectangle;
			lastAlpha = Alpha;
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
		Rectangle rectangle = new Rectangle(Left, Top, Width, Height);
		foreach (VertexLight component in base.Entity.Scene.Tracker.GetComponents<VertexLight>())
		{
			if (!component.Dirty)
			{
				Rectangle value = new Rectangle((int)(component.Center.X - component.EndRadius), (int)(component.Center.Y - component.EndRadius), (int)component.EndRadius * 2, (int)component.EndRadius * 2);
				if (rectangle.Intersects(value) || lastSize.Intersects(value))
				{
					component.Dirty = true;
				}
			}
		}
	}
}
