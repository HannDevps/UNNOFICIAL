using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ChimneySmokeFx
{
	public static void Burst(Vector2 position, float direction, int count, ParticleSystem system = null)
	{
		Vector2 vector = Calc.AngleToVector(direction - (float)Math.PI / 2f, 2f);
		vector.X = Math.Abs(vector.X);
		vector.Y = Math.Abs(vector.Y);
		if (system == null)
		{
			system = (Engine.Scene as Level).ParticlesFG;
		}
		for (int i = 0; i < count; i++)
		{
			system.Emit(Calc.Random.Choose<ParticleType>(ParticleTypes.Chimney), position + Calc.Random.Range(-vector, vector), direction);
		}
	}
}
