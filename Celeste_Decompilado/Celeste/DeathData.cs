using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class DeathData
{
	public Vector2 Position;

	public int Amount;

	public DeathData(Vector2 position)
	{
		Position = position;
		Amount = 1;
	}

	public DeathData(DeathData old, Vector2 add)
	{
		Position = Vector2.Lerp(old.Position, add, 1f / (float)(old.Amount + 1));
		Amount = old.Amount + 1;
	}

	public bool CombinesWith(Vector2 position)
	{
		return Vector2.DistanceSquared(Position, position) <= 100f;
	}

	public void Render()
	{
		float num = Math.Min(0.7f, 0.3f + 0.1f * (float)Amount);
		int num2 = Math.Min(6, Amount + 1);
		Draw.Rect(Position.X - (float)num2, Position.Y - (float)num2, num2 * 2, num2 * 2, Color.Red * num);
	}
}
