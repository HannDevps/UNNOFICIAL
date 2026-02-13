using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class MovingPlatformLine : Entity
{
	private Color lineEdgeColor;

	private Color lineInnerColor;

	private Vector2 end;

	public MovingPlatformLine(Vector2 position, Vector2 end)
	{
		Position = position;
		base.Depth = 9001;
		this.end = end;
	}

	public override void Added(Scene scene)
	{
		if ((scene as Level).Session.Area.ID == 4)
		{
			lineEdgeColor = Calc.HexToColor("a4464a");
			lineInnerColor = Calc.HexToColor("86354e");
		}
		else
		{
			lineEdgeColor = Calc.HexToColor("2a1923");
			lineInnerColor = Calc.HexToColor("160b12");
		}
		base.Added(scene);
	}

	public override void Render()
	{
		Vector2 vector = (end - Position).SafeNormalize();
		Vector2 vector2 = new Vector2(0f - vector.Y, vector.X);
		Draw.Line(Position - vector - vector2, end + vector - vector2, lineEdgeColor);
		Draw.Line(Position - vector, end + vector, lineEdgeColor);
		Draw.Line(Position - vector + vector2, end + vector + vector2, lineEdgeColor);
		Draw.Line(Position, end, lineInnerColor);
	}
}
