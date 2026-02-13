using Microsoft.Xna.Framework;

namespace Monocle;

public struct SimpleCurve
{
	public Vector2 Begin;

	public Vector2 End;

	public Vector2 Control;

	public SimpleCurve(Vector2 begin, Vector2 end, Vector2 control)
	{
		Begin = begin;
		End = end;
		Control = control;
	}

	public void DoubleControl()
	{
		Vector2 vector = End - Begin;
		Vector2 vector2 = Begin + vector / 2f;
		Vector2 vector3 = Control - vector2;
		Control += vector3;
	}

	public Vector2 GetPoint(float percent)
	{
		float num = 1f - percent;
		return num * num * Begin + 2f * num * percent * Control + percent * percent * End;
	}

	public float GetLengthParametric(int resolution)
	{
		Vector2 vector = Begin;
		float num = 0f;
		for (int i = 1; i <= resolution; i++)
		{
			Vector2 point = GetPoint((float)i / (float)resolution);
			num += (point - vector).Length();
			vector = point;
		}
		return num;
	}

	public void Render(Vector2 offset, Color color, int resolution)
	{
		Vector2 start = offset + Begin;
		for (int i = 1; i <= resolution; i++)
		{
			Vector2 vector = offset + GetPoint((float)i / (float)resolution);
			Draw.Line(start, vector, color);
			start = vector;
		}
	}

	public void Render(Vector2 offset, Color color, int resolution, float thickness)
	{
		Vector2 start = offset + Begin;
		for (int i = 1; i <= resolution; i++)
		{
			Vector2 vector = offset + GetPoint((float)i / (float)resolution);
			Draw.Line(start, vector, color, thickness);
			start = vector;
		}
	}

	public void Render(Color color, int resolution)
	{
		Render(Vector2.Zero, color, resolution);
	}

	public void Render(Color color, int resolution, float thickness)
	{
		Render(Vector2.Zero, color, resolution, thickness);
	}
}
