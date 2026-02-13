using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class FallEffects : Entity
{
	private struct Particle
	{
		public Vector2 Position;

		public float Speed;

		public int Color;
	}

	private static readonly Color[] colors = new Color[2]
	{
		Color.White,
		Color.LightGray
	};

	private static readonly Color[] faded = new Color[2];

	private Particle[] particles = new Particle[50];

	private float fade;

	private bool enabled;

	public static float SpeedMultiplier = 1f;

	public FallEffects()
	{
		base.Tag = Tags.Global;
		base.Depth = -1000000;
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Position = new Vector2(Calc.Random.Range(0, 320), Calc.Random.Range(0, 180));
			particles[i].Speed = Calc.Random.Range(120, 240);
			particles[i].Color = Calc.Random.Next(colors.Length);
		}
	}

	public static void Show(bool visible)
	{
		FallEffects fallEffects = Engine.Scene.Tracker.GetEntity<FallEffects>();
		if (fallEffects == null && visible)
		{
			Engine.Scene.Add(fallEffects = new FallEffects());
		}
		if (fallEffects != null)
		{
			fallEffects.enabled = visible;
		}
		SpeedMultiplier = 1f;
	}

	public override void Update()
	{
		base.Update();
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Position -= Vector2.UnitY * particles[i].Speed * SpeedMultiplier * Engine.DeltaTime;
		}
		fade = Calc.Approach(fade, enabled ? 1f : 0f, (float)(enabled ? 1 : 4) * Engine.DeltaTime);
	}

	public override void Render()
	{
		if (!(fade <= 0f))
		{
			Camera camera = (base.Scene as Level).Camera;
			for (int i = 0; i < faded.Length; i++)
			{
				faded[i] = colors[i] * fade;
			}
			for (int j = 0; j < particles.Length; j++)
			{
				float num = 8f * SpeedMultiplier;
				Vector2 vector = new Vector2
				{
					X = mod(particles[j].Position.X - camera.X, 320f),
					Y = mod(particles[j].Position.Y - camera.Y - 16f, 212f)
				};
				vector += camera.Position;
				Draw.Rect(vector - new Vector2(0f, num / 2f), 1f, num, faded[particles[j].Color]);
			}
		}
	}

	private float mod(float x, float m)
	{
		return (x % m + m) % m;
	}
}
