using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class SolidOnInvinciblePlayer : Component
{
	private class Outline : Entity
	{
		public SolidOnInvinciblePlayer Parent;

		public Outline(SolidOnInvinciblePlayer parent)
		{
			Parent = parent;
			base.Depth = -10;
		}

		public override void Render()
		{
			if (Parent != null && Parent.Entity != null)
			{
				Entity entity = Parent.Entity;
				int num = (int)entity.Left;
				int num2 = (int)entity.Right;
				int num3 = (int)entity.Top;
				int num4 = (int)entity.Bottom;
				Draw.Rect(num + 4, num3 + 4, entity.Width - 8f, entity.Height - 8f, Color.White * 0.25f);
				for (float num5 = num; num5 < (float)(num2 - 3); num5 += 3f)
				{
					Draw.Line(num5, num3, num5 + 2f, num3, Color.White);
					Draw.Line(num5, num4 - 1, num5 + 2f, num4 - 1, Color.White);
				}
				for (float num6 = num3; num6 < (float)(num4 - 3); num6 += 3f)
				{
					Draw.Line(num + 1, num6, num + 1, num6 + 2f, Color.White);
					Draw.Line(num2, num6, num2, num6 + 2f, Color.White);
				}
				Draw.Rect(num + 1, num3, 1f, 2f, Color.White);
				Draw.Rect(num2 - 2, num3, 2f, 2f, Color.White);
				Draw.Rect(num, num4 - 2, 2f, 2f, Color.White);
				Draw.Rect(num2 - 2, num4 - 2, 2f, 2f, Color.White);
			}
		}
	}

	private bool wasCollidable;

	private bool wasVisible;

	private Outline outline;

	public SolidOnInvinciblePlayer()
		: base(active: true, visible: false)
	{
	}

	public override void Added(Entity entity)
	{
		base.Added(entity);
		Audio.Play("event:/game/general/assist_nonsolid_in", entity.Center);
		wasCollidable = entity.Collidable;
		wasVisible = entity.Visible;
		entity.Collidable = false;
		entity.Visible = false;
		if (entity.Scene != null)
		{
			entity.Scene.Add(outline = new Outline(this));
		}
	}

	public override void Update()
	{
		base.Update();
		base.Entity.Collidable = true;
		if (!base.Entity.CollideCheck<Player>() && !base.Entity.CollideCheck<TheoCrystal>())
		{
			RemoveSelf();
		}
		else
		{
			base.Entity.Collidable = false;
		}
	}

	public override void Removed(Entity entity)
	{
		Audio.Play("event:/game/general/assist_nonsolid_out", entity.Center);
		entity.Collidable = wasCollidable;
		entity.Visible = wasVisible;
		if (outline != null)
		{
			outline.RemoveSelf();
		}
		base.Removed(entity);
	}
}
