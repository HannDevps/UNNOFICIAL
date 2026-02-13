using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class Flagline : Component
{
	private struct Cloth
	{
		public int Color;

		public int Height;

		public int Length;

		public int Step;
	}

	private Color[] colors;

	private Color[] highlights;

	private Color lineColor;

	private Color pinColor;

	private Cloth[] clothes;

	private float waveTimer;

	public float ClothDroopAmount = 0.6f;

	public Vector2 To;

	public Vector2 From => base.Entity.Position;

	public Flagline(Vector2 to, Color lineColor, Color pinColor, Color[] colors, int minFlagHeight, int maxFlagHeight, int minFlagLength, int maxFlagLength, int minSpace, int maxSpace)
		: base(active: true, visible: true)
	{
		To = to;
		this.colors = colors;
		this.lineColor = lineColor;
		this.pinColor = pinColor;
		waveTimer = Calc.Random.NextFloat() * ((float)Math.PI * 2f);
		highlights = new Color[colors.Length];
		for (int i = 0; i < colors.Length; i++)
		{
			highlights[i] = Color.Lerp(colors[i], Color.White, 0.1f);
		}
		clothes = new Cloth[10];
		for (int j = 0; j < clothes.Length; j++)
		{
			clothes[j] = new Cloth
			{
				Color = Calc.Random.Next(colors.Length),
				Height = Calc.Random.Next(minFlagHeight, maxFlagHeight),
				Length = Calc.Random.Next(minFlagLength, maxFlagLength),
				Step = Calc.Random.Next(minSpace, maxSpace)
			};
		}
	}

	public override void Update()
	{
		waveTimer += Engine.DeltaTime;
		base.Update();
	}

	public override void Render()
	{
		Vector2 vector = ((From.X < To.X) ? From : To);
		Vector2 vector2 = ((From.X < To.X) ? To : From);
		float num = (vector - vector2).Length();
		float num2 = num / 8f;
		SimpleCurve simpleCurve = new SimpleCurve(vector, vector2, (vector2 + vector) / 2f + Vector2.UnitY * (num2 + (float)Math.Sin(waveTimer) * num2 * 0.3f));
		Vector2 vector3 = vector;
		Vector2 vector4 = vector;
		float num3 = 0f;
		int num4 = 0;
		bool flag = false;
		while (num3 < 1f)
		{
			Cloth cloth = clothes[num4 % clothes.Length];
			num3 += (float)(flag ? cloth.Length : cloth.Step) / num;
			vector4 = simpleCurve.GetPoint(num3);
			Draw.Line(vector3, vector4, lineColor);
			if (num3 < 1f && flag)
			{
				float num5 = (float)cloth.Length * ClothDroopAmount;
				SimpleCurve simpleCurve2 = new SimpleCurve(vector3, vector4, (vector3 + vector4) / 2f + new Vector2(0f, num5 + (float)Math.Sin(waveTimer * 2f + num3) * num5 * 0.4f));
				Vector2 vector5 = vector3;
				for (float num6 = 1f; num6 <= (float)cloth.Length; num6 += 1f)
				{
					Vector2 point = simpleCurve2.GetPoint(num6 / (float)cloth.Length);
					if (point.X != vector5.X)
					{
						Draw.Rect(vector5.X, vector5.Y, point.X - vector5.X + 1f, cloth.Height, colors[cloth.Color]);
						vector5 = point;
					}
				}
				Draw.Rect(vector3.X, vector3.Y, 1f, cloth.Height, highlights[cloth.Color]);
				Draw.Rect(vector4.X, vector4.Y, 1f, cloth.Height, highlights[cloth.Color]);
				Draw.Rect(vector3.X, vector3.Y - 1f, 1f, 3f, pinColor);
				Draw.Rect(vector4.X, vector4.Y - 1f, 1f, 3f, pinColor);
				num4++;
			}
			vector3 = vector4;
			flag = !flag;
		}
	}
}
