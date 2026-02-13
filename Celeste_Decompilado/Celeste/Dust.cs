using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public static class Dust
{
	public static void Burst(Vector2 position, float direction, int count = 1, ParticleType particleType = null)
	{
		if (particleType == null)
		{
			particleType = ParticleTypes.Dust;
		}
		Vector2 vector = Calc.AngleToVector(direction - (float)Math.PI / 2f, 4f);
		vector.X = Math.Abs(vector.X);
		vector.Y = Math.Abs(vector.Y);
		Level level = Engine.Scene as Level;
		for (int i = 0; i < count; i++)
		{
			level.Particles.Emit(particleType, position + Calc.Random.Range(-vector, vector), direction);
		}
	}

	public static void BurstFG(Vector2 position, float direction, int count = 1, float range = 4f, ParticleType particleType = null)
	{
		if (particleType == null)
		{
			particleType = ParticleTypes.Dust;
		}
		Vector2 vector = Calc.AngleToVector(direction - (float)Math.PI / 2f, range);
		vector.X = Math.Abs(vector.X);
		vector.Y = Math.Abs(vector.Y);
		Level level = Engine.Scene as Level;
		for (int i = 0; i < count; i++)
		{
			level.ParticlesFG.Emit(particleType, position + Calc.Random.Range(-vector, vector), direction);
		}
	}
}
