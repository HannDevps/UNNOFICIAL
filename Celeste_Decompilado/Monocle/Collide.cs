using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monocle;

public static class Collide
{
	public static bool Check(Entity a, Entity b)
	{
		if (a.Collider == null || b.Collider == null)
		{
			return false;
		}
		if (a != b && b.Collidable)
		{
			return a.Collider.Collide(b);
		}
		return false;
	}

	public static bool Check(Entity a, Entity b, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		bool result = Check(a, b);
		a.Position = position;
		return result;
	}

	public static bool Check(Entity a, IEnumerable<Entity> b)
	{
		foreach (Entity item in b)
		{
			if (Check(a, item))
			{
				return true;
			}
		}
		return false;
	}

	public static bool Check(Entity a, IEnumerable<Entity> b, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		bool result = Check(a, b);
		a.Position = position;
		return result;
	}

	public static Entity First(Entity a, IEnumerable<Entity> b)
	{
		foreach (Entity item in b)
		{
			if (Check(a, item))
			{
				return item;
			}
		}
		return null;
	}

	public static Entity First(Entity a, IEnumerable<Entity> b, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		Entity result = First(a, b);
		a.Position = position;
		return result;
	}

	public static List<Entity> All(Entity a, IEnumerable<Entity> b, List<Entity> into)
	{
		foreach (Entity item in b)
		{
			if (Check(a, item))
			{
				into.Add(item);
			}
		}
		return into;
	}

	public static List<Entity> All(Entity a, IEnumerable<Entity> b, List<Entity> into, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		List<Entity> result = All(a, b, into);
		a.Position = position;
		return result;
	}

	public static List<Entity> All(Entity a, IEnumerable<Entity> b)
	{
		return All(a, b, new List<Entity>());
	}

	public static List<Entity> All(Entity a, IEnumerable<Entity> b, Vector2 at)
	{
		return All(a, b, new List<Entity>(), at);
	}

	public static bool CheckPoint(Entity a, Vector2 point)
	{
		if (a.Collider == null)
		{
			return false;
		}
		return a.Collider.Collide(point);
	}

	public static bool CheckPoint(Entity a, Vector2 point, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		bool result = CheckPoint(a, point);
		a.Position = position;
		return result;
	}

	public static bool CheckLine(Entity a, Vector2 from, Vector2 to)
	{
		if (a.Collider == null)
		{
			return false;
		}
		return a.Collider.Collide(from, to);
	}

	public static bool CheckLine(Entity a, Vector2 from, Vector2 to, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		bool result = CheckLine(a, from, to);
		a.Position = position;
		return result;
	}

	public static bool CheckRect(Entity a, Rectangle rect)
	{
		if (a.Collider == null)
		{
			return false;
		}
		return a.Collider.Collide(rect);
	}

	public static bool CheckRect(Entity a, Rectangle rect, Vector2 at)
	{
		Vector2 position = a.Position;
		a.Position = at;
		bool result = CheckRect(a, rect);
		a.Position = position;
		return result;
	}

	public static bool LineCheck(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
	{
		Vector2 vector = a2 - a1;
		Vector2 vector2 = b2 - b1;
		float num = vector.X * vector2.Y - vector.Y * vector2.X;
		if (num == 0f)
		{
			return false;
		}
		Vector2 vector3 = b1 - a1;
		float num2 = (vector3.X * vector2.Y - vector3.Y * vector2.X) / num;
		if (num2 < 0f || num2 > 1f)
		{
			return false;
		}
		float num3 = (vector3.X * vector.Y - vector3.Y * vector.X) / num;
		if (num3 < 0f || num3 > 1f)
		{
			return false;
		}
		return true;
	}

	public static bool LineCheck(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
	{
		intersection = Vector2.Zero;
		Vector2 vector = a2 - a1;
		Vector2 vector2 = b2 - b1;
		float num = vector.X * vector2.Y - vector.Y * vector2.X;
		if (num == 0f)
		{
			return false;
		}
		Vector2 vector3 = b1 - a1;
		float num2 = (vector3.X * vector2.Y - vector3.Y * vector2.X) / num;
		if (num2 < 0f || num2 > 1f)
		{
			return false;
		}
		float num3 = (vector3.X * vector.Y - vector3.Y * vector.X) / num;
		if (num3 < 0f || num3 > 1f)
		{
			return false;
		}
		intersection = a1 + num2 * vector;
		return true;
	}

	public static bool CircleToLine(Vector2 cPosiition, float cRadius, Vector2 lineFrom, Vector2 lineTo)
	{
		return Vector2.DistanceSquared(cPosiition, Calc.ClosestPointOnLine(lineFrom, lineTo, cPosiition)) < cRadius * cRadius;
	}

	public static bool CircleToPoint(Vector2 cPosition, float cRadius, Vector2 point)
	{
		return Vector2.DistanceSquared(cPosition, point) < cRadius * cRadius;
	}

	public static bool CircleToRect(Vector2 cPosition, float cRadius, float rX, float rY, float rW, float rH)
	{
		return RectToCircle(rX, rY, rW, rH, cPosition, cRadius);
	}

	public static bool CircleToRect(Vector2 cPosition, float cRadius, Rectangle rect)
	{
		return RectToCircle(rect, cPosition, cRadius);
	}

	public static bool RectToCircle(float rX, float rY, float rW, float rH, Vector2 cPosition, float cRadius)
	{
		if (RectToPoint(rX, rY, rW, rH, cPosition))
		{
			return true;
		}
		PointSectors sector = GetSector(rX, rY, rW, rH, cPosition);
		if ((sector & PointSectors.Top) != PointSectors.Center)
		{
			Vector2 lineFrom = new Vector2(rX, rY);
			Vector2 lineTo = new Vector2(rX + rW, rY);
			if (CircleToLine(cPosition, cRadius, lineFrom, lineTo))
			{
				return true;
			}
		}
		if ((sector & PointSectors.Bottom) != PointSectors.Center)
		{
			Vector2 lineFrom = new Vector2(rX, rY + rH);
			Vector2 lineTo = new Vector2(rX + rW, rY + rH);
			if (CircleToLine(cPosition, cRadius, lineFrom, lineTo))
			{
				return true;
			}
		}
		if ((sector & PointSectors.Left) != PointSectors.Center)
		{
			Vector2 lineFrom = new Vector2(rX, rY);
			Vector2 lineTo = new Vector2(rX, rY + rH);
			if (CircleToLine(cPosition, cRadius, lineFrom, lineTo))
			{
				return true;
			}
		}
		if ((sector & PointSectors.Right) != PointSectors.Center)
		{
			Vector2 lineFrom = new Vector2(rX + rW, rY);
			Vector2 lineTo = new Vector2(rX + rW, rY + rH);
			if (CircleToLine(cPosition, cRadius, lineFrom, lineTo))
			{
				return true;
			}
		}
		return false;
	}

	public static bool RectToCircle(Rectangle rect, Vector2 cPosition, float cRadius)
	{
		return RectToCircle(rect.X, rect.Y, rect.Width, rect.Height, cPosition, cRadius);
	}

	public static bool RectToLine(float rX, float rY, float rW, float rH, Vector2 lineFrom, Vector2 lineTo)
	{
		PointSectors sector = GetSector(rX, rY, rW, rH, lineFrom);
		PointSectors sector2 = GetSector(rX, rY, rW, rH, lineTo);
		if (sector == PointSectors.Center || sector2 == PointSectors.Center)
		{
			return true;
		}
		if ((sector & sector2) != PointSectors.Center)
		{
			return false;
		}
		PointSectors pointSectors = sector | sector2;
		if ((pointSectors & PointSectors.Top) != PointSectors.Center)
		{
			Vector2 a = new Vector2(rX, rY);
			Vector2 a2 = new Vector2(rX + rW, rY);
			if (LineCheck(a, a2, lineFrom, lineTo))
			{
				return true;
			}
		}
		if ((pointSectors & PointSectors.Bottom) != PointSectors.Center)
		{
			Vector2 a3 = new Vector2(rX, rY + rH);
			Vector2 a2 = new Vector2(rX + rW, rY + rH);
			if (LineCheck(a3, a2, lineFrom, lineTo))
			{
				return true;
			}
		}
		if ((pointSectors & PointSectors.Left) != PointSectors.Center)
		{
			Vector2 a4 = new Vector2(rX, rY);
			Vector2 a2 = new Vector2(rX, rY + rH);
			if (LineCheck(a4, a2, lineFrom, lineTo))
			{
				return true;
			}
		}
		if ((pointSectors & PointSectors.Right) != PointSectors.Center)
		{
			Vector2 a5 = new Vector2(rX + rW, rY);
			Vector2 a2 = new Vector2(rX + rW, rY + rH);
			if (LineCheck(a5, a2, lineFrom, lineTo))
			{
				return true;
			}
		}
		return false;
	}

	public static bool RectToLine(Rectangle rect, Vector2 lineFrom, Vector2 lineTo)
	{
		return RectToLine(rect.X, rect.Y, rect.Width, rect.Height, lineFrom, lineTo);
	}

	public static bool RectToPoint(float rX, float rY, float rW, float rH, Vector2 point)
	{
		if (point.X >= rX && point.Y >= rY && point.X < rX + rW)
		{
			return point.Y < rY + rH;
		}
		return false;
	}

	public static bool RectToPoint(Rectangle rect, Vector2 point)
	{
		return RectToPoint(rect.X, rect.Y, rect.Width, rect.Height, point);
	}

	public static PointSectors GetSector(Rectangle rect, Vector2 point)
	{
		PointSectors pointSectors = PointSectors.Center;
		if (point.X < (float)rect.Left)
		{
			pointSectors |= PointSectors.Left;
		}
		else if (point.X >= (float)rect.Right)
		{
			pointSectors |= PointSectors.Right;
		}
		if (point.Y < (float)rect.Top)
		{
			pointSectors |= PointSectors.Top;
		}
		else if (point.Y >= (float)rect.Bottom)
		{
			pointSectors |= PointSectors.Bottom;
		}
		return pointSectors;
	}

	public static PointSectors GetSector(float rX, float rY, float rW, float rH, Vector2 point)
	{
		PointSectors pointSectors = PointSectors.Center;
		if (point.X < rX)
		{
			pointSectors |= PointSectors.Left;
		}
		else if (point.X >= rX + rW)
		{
			pointSectors |= PointSectors.Right;
		}
		if (point.Y < rY)
		{
			pointSectors |= PointSectors.Top;
		}
		else if (point.Y >= rY + rH)
		{
			pointSectors |= PointSectors.Bottom;
		}
		return pointSectors;
	}
}
