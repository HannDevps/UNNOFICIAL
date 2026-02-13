using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class StarfieldWipe : ScreenWipe
{
	private struct Star
	{
		public float X;

		public float Y;

		public float Sine;

		public float SineDistance;

		public float Speed;

		public float Scale;

		public float Rotation;

		public Star(float scale)
		{
			Scale = scale;
			float num = 1f - scale;
			X = Calc.Random.Range(0, 2920);
			Y = 1080f * (0.5f + (float)Calc.Random.Choose(-1, 1) * num * Calc.Random.Range(0.25f, 0.5f));
			Sine = Calc.Random.NextFloat((float)Math.PI * 2f);
			SineDistance = scale * 1080f * 0.05f;
			Speed = (0.5f + (1f - Scale) * 0.5f) * 1920f * 0.05f;
			Rotation = Calc.Random.NextFloat((float)Math.PI * 2f);
		}

		public void Update()
		{
			X += Speed * Engine.DeltaTime;
			Sine += (1f - Scale) * 8f * Engine.DeltaTime;
			Rotation += (1f - Scale) * Engine.DeltaTime;
		}
	}

	public static readonly BlendState SubtractBlendmode = new BlendState
	{
		ColorSourceBlend = Blend.One,
		ColorDestinationBlend = Blend.One,
		ColorBlendFunction = BlendFunction.ReverseSubtract,
		AlphaSourceBlend = Blend.One,
		AlphaDestinationBlend = Blend.One,
		AlphaBlendFunction = BlendFunction.Add
	};

	private Star[] stars = new Star[64];

	private VertexPositionColor[] verts = new VertexPositionColor[1536];

	private Vector2[] starShape = new Vector2[5];

	private bool hasDrawn;

	public StarfieldWipe(Scene scene, bool wipeIn, Action onComplete = null)
		: base(scene, wipeIn, onComplete)
	{
		for (int i = 0; i < 5; i++)
		{
			starShape[i] = Calc.AngleToVector((float)i / 5f * ((float)Math.PI * 2f), 1f);
		}
		for (int j = 0; j < stars.Length; j++)
		{
			stars[j] = new Star((float)Math.Pow((float)j / (float)stars.Length, 5.0));
		}
		for (int k = 0; k < verts.Length; k++)
		{
			verts[k].Color = (WipeIn ? Color.Black : Color.White);
		}
	}

	public override void Update(Scene scene)
	{
		base.Update(scene);
		for (int i = 0; i < stars.Length; i++)
		{
			stars[i].Update();
		}
	}

	public override void BeforeRender(Scene scene)
	{
		hasDrawn = true;
		Engine.Graphics.GraphicsDevice.SetRenderTarget(Celeste.WipeTarget);
		Engine.Graphics.GraphicsDevice.Clear(WipeIn ? Color.White : Color.Black);
		if (Percent > 0.8f)
		{
			float num = Calc.Map(Percent, 0.8f, 1f) * 1082f;
			Draw.SpriteBatch.Begin();
			Draw.Rect(-1f, (1080f - num) * 0.5f, 1922f, num, (!WipeIn) ? Color.White : Color.Black);
			Draw.SpriteBatch.End();
		}
		int index = 0;
		for (int i = 0; i < stars.Length; i++)
		{
			float xPosition = -500f + stars[i].X % 2920f;
			float yPosition = (float)((double)stars[i].Y + Math.Sin(stars[i].Sine) * (double)stars[i].SineDistance);
			float scale = (0.1f + stars[i].Scale * 0.9f) * 1080f * 0.8f * Ease.CubeIn(Percent);
			DrawStar(ref index, Matrix.CreateRotationZ(stars[i].Rotation) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(xPosition, yPosition, 0f));
		}
		GFX.DrawVertices(Matrix.Identity, verts, verts.Length);
	}

	private void DrawStar(ref int index, Matrix matrix)
	{
		int num = index;
		for (int i = 1; i < starShape.Length - 1; i++)
		{
			verts[index++].Position = new Vector3(starShape[0], 0f);
			verts[index++].Position = new Vector3(starShape[i], 0f);
			verts[index++].Position = new Vector3(starShape[i + 1], 0f);
		}
		for (int j = 0; j < 5; j++)
		{
			Vector2 vector = starShape[j];
			Vector2 vector2 = starShape[(j + 1) % 5];
			Vector2 value = (vector + vector2) * 0.5f + (vector - vector2).SafeNormalize().TurnRight();
			verts[index++].Position = new Vector3(vector, 0f);
			verts[index++].Position = new Vector3(value, 0f);
			verts[index++].Position = new Vector3(vector2, 0f);
		}
		for (int k = num; k < num + 24; k++)
		{
			verts[k].Position = Vector3.Transform(verts[k].Position, matrix);
		}
	}

	public override void Render(Scene scene)
	{
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, SubtractBlendmode, SamplerState.LinearClamp, null, null, null, Engine.ScreenMatrix);
		if ((WipeIn && Percent <= 0.01f) || (!WipeIn && Percent >= 0.99f))
		{
			Draw.Rect(-1f, -1f, 1922f, 1082f, Color.White);
		}
		else if (hasDrawn)
		{
			Draw.SpriteBatch.Draw((RenderTarget2D)Celeste.WipeTarget, new Vector2(-1f, -1f), Color.White);
		}
		Draw.SpriteBatch.End();
	}
}
