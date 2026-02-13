using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Snow3D : Entity
{
	[Tracked(false)]
	public class Particle : Billboard
	{
		public Snow3D Manager;

		public Vector2 Float;

		public float Percent;

		public float Duration;

		public float Speed;

		private float size;

		public Particle(Snow3D manager, float size)
			: base(OVR.Atlas["snow"], Vector3.Zero)
		{
			Manager = manager;
			this.size = size;
			Size = Vector2.One * size;
			Reset(Calc.Random.NextFloat());
			ResetPosition();
		}

		public void ResetPosition()
		{
			float range = Manager.Range;
			Position = Manager.Model.Camera.Position + Manager.Model.Forward * range * 0.5f + new Vector3(Calc.Random.Range(0f - range, range), Calc.Random.Range(0f - range, range), Calc.Random.Range(0f - range, range));
		}

		public void Reset(float percent = 0f)
		{
			float num = Manager.Range / 30f;
			Speed = Calc.Random.Range(1f, 6f) * num;
			Percent = percent;
			Duration = Calc.Random.Range(1f, 5f);
			Float = new Vector2(Calc.Random.Range(-1, 1), Calc.Random.Range(-1, 1)).SafeNormalize() * 0.25f;
			Scale = Vector2.One * 0.05f * num;
		}

		public override void Update()
		{
			if (Percent > 1f || !InView())
			{
				ResetPosition();
				int num = 0;
				while (!InView() && num++ < 10)
				{
					ResetPosition();
				}
				if (num > 10)
				{
					Color = Color.Transparent;
					return;
				}
				Reset((!InLastView()) ? Calc.Random.NextFloat() : 0f);
			}
			Percent += Engine.DeltaTime / Duration;
			float num2 = Calc.YoYo(Percent);
			if (Manager.Model.SnowForceFloat > 0f)
			{
				num2 *= Manager.Model.SnowForceFloat;
			}
			else if (Manager.Model.StarEase > 0f)
			{
				num2 *= Calc.Map(Manager.Model.StarEase, 0f, 1f, 1f, 0f);
			}
			Color = Color.White * num2;
			Size.Y = size + Manager.Model.SnowStretch * (1f - Manager.Model.SnowForceFloat);
			Position.Y -= (Speed + Manager.Model.SnowSpeedAddition) * (1f - Manager.Model.SnowForceFloat) * Engine.DeltaTime;
			Position.X += Float.X * Engine.DeltaTime;
			Position.Z += Float.Y * Engine.DeltaTime;
		}

		private bool InView()
		{
			if (Manager.Frustum.Contains(Position) == ContainmentType.Contains)
			{
				return Position.Y > 0f;
			}
			return false;
		}

		private bool InLastView()
		{
			if (Manager.LastFrustum != null)
			{
				return Manager.LastFrustum.Contains(Position) == ContainmentType.Contains;
			}
			return false;
		}
	}

	private static Color[] alphas = new Color[32];

	private List<Particle> particles = new List<Particle>();

	private BoundingFrustum Frustum = new BoundingFrustum(Matrix.Identity);

	private BoundingFrustum LastFrustum = new BoundingFrustum(Matrix.Identity);

	private MountainModel Model;

	private float Range = 30f;

	public Snow3D(MountainModel model)
	{
		Model = model;
		for (int i = 0; i < alphas.Length; i++)
		{
			alphas[i] = Color.White * ((float)i / (float)alphas.Length);
		}
		for (int j = 0; j < 400; j++)
		{
			Particle particle = new Particle(this, 1f);
			particles.Add(particle);
			Add(particle);
		}
	}

	public override void Update()
	{
		Overworld overworld = base.Scene as Overworld;
		Range = 20f;
		if (SaveData.Instance != null && overworld != null && (overworld.IsCurrent<OuiChapterPanel>() || overworld.IsCurrent<OuiChapterSelect>()))
		{
			switch (SaveData.Instance.LastArea.ID)
			{
			case 0:
			case 2:
			case 8:
				Range = 3f;
				break;
			case 1:
				Range = 12f;
				break;
			}
		}
		Matrix matrix = Matrix.CreatePerspectiveFieldOfView((float)Math.PI * 5f / 16f, (float)Engine.Width / (float)Engine.Height, 0.1f, Range);
		Matrix matrix2 = Matrix.CreateTranslation(-Model.Camera.Position) * Matrix.CreateFromQuaternion(Model.Camera.Rotation) * matrix;
		if (base.Scene.OnInterval(0.05f))
		{
			LastFrustum.Matrix = matrix2;
		}
		Frustum.Matrix = matrix2;
		base.Update();
	}
}
