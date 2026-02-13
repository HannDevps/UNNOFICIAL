using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class StrawberriesCounter : Component
{
	public static readonly Color FlashColor = Calc.HexToColor("FF5E76");

	private const int IconWidth = 60;

	public bool Golden;

	public Vector2 Position;

	public bool CenteredX;

	public bool CanWiggle = true;

	public float Scale = 1f;

	public float Stroke = 2f;

	public float Rotation;

	public Color Color = Color.White;

	public Color OutOfColor = Color.LightGray;

	public bool OverworldSfx;

	private int amount;

	private int outOf = -1;

	private Wiggler wiggler;

	private float flashTimer;

	private string sAmount;

	private string sOutOf;

	private MTexture x;

	private bool showOutOf;

	public int Amount
	{
		get
		{
			return amount;
		}
		set
		{
			if (amount == value)
			{
				return;
			}
			amount = value;
			UpdateStrings();
			if (CanWiggle)
			{
				if (OverworldSfx)
				{
					Audio.Play(Golden ? "event:/ui/postgame/goldberry_count" : "event:/ui/postgame/strawberry_count");
				}
				else
				{
					Audio.Play("event:/ui/game/increment_strawberry");
				}
				wiggler.Start();
				flashTimer = 0.5f;
			}
		}
	}

	public int OutOf
	{
		get
		{
			return outOf;
		}
		set
		{
			outOf = value;
			UpdateStrings();
		}
	}

	public bool ShowOutOf
	{
		get
		{
			return showOutOf;
		}
		set
		{
			if (showOutOf != value)
			{
				showOutOf = value;
				UpdateStrings();
			}
		}
	}

	public float FullHeight => Math.Max(ActiveFont.LineHeight, GFX.Gui["collectables/strawberry"].Height);

	public Vector2 RenderPosition => (((base.Entity != null) ? base.Entity.Position : Vector2.Zero) + Position).RoundV2();

	public StrawberriesCounter(bool centeredX, int amount, int outOf = 0, bool showOutOf = false)
		: base(active: true, visible: true)
	{
		CenteredX = centeredX;
		this.amount = amount;
		this.outOf = outOf;
		this.showOutOf = showOutOf;
		UpdateStrings();
		wiggler = Wiggler.Create(0.5f, 3f);
		wiggler.StartZero = true;
		wiggler.UseRawDeltaTime = true;
		x = GFX.Gui["x"];
	}

	private void UpdateStrings()
	{
		sAmount = amount.ToString();
		if (outOf > -1)
		{
			sOutOf = "/" + outOf;
		}
		else
		{
			sOutOf = "";
		}
	}

	public void Wiggle()
	{
		wiggler.Start();
		flashTimer = 0.5f;
	}

	public override void Update()
	{
		base.Update();
		if (wiggler.Active)
		{
			wiggler.Update();
		}
		if (flashTimer > 0f)
		{
			flashTimer -= Engine.RawDeltaTime;
		}
	}

	public override void Render()
	{
		Vector2 renderPosition = RenderPosition;
		Vector2 vector = Calc.AngleToVector(Rotation, 1f);
		Vector2 vector2 = new Vector2(0f - vector.Y, vector.X);
		string text = (showOutOf ? sOutOf : "");
		float num = ActiveFont.Measure(sAmount).X;
		float num2 = ActiveFont.Measure(text).X;
		float num3 = 62f + (float)x.Width + 2f + num + num2;
		Color color = Color;
		if (flashTimer > 0f && base.Scene != null && base.Scene.BetweenRawInterval(0.05f))
		{
			color = FlashColor;
		}
		if (CenteredX)
		{
			renderPosition -= vector * (num3 / 2f) * Scale;
		}
		string id = (Golden ? "collectables/goldberry" : "collectables/strawberry");
		GFX.Gui[id].DrawCentered(renderPosition + vector * 60f * 0.5f * Scale, Color.White, Scale);
		x.DrawCentered(renderPosition + vector * (62f + (float)x.Width * 0.5f) * Scale + vector2 * 2f * Scale, color, Scale);
		ActiveFont.DrawOutline(sAmount, renderPosition + vector * (num3 - num2 - num * 0.5f) * Scale + vector2 * (wiggler.Value * 18f) * Scale, new Vector2(0.5f, 0.5f), Vector2.One * Scale, color, Stroke, Color.Black);
		if (text != "")
		{
			ActiveFont.DrawOutline(text, renderPosition + vector * (num3 - num2 / 2f) * Scale, new Vector2(0.5f, 0.5f), Vector2.One * Scale, OutOfColor, Stroke, Color.Black);
		}
	}
}
