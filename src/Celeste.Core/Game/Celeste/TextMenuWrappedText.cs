using System;
using Microsoft.Xna.Framework;

namespace Celeste;

public class TextMenuWrappedText : TextMenu.Item
{
	private readonly string text;

	private readonly float scale;

	private readonly int maxWidth;

	private string wrappedText = "";

	private int wrappedWidth = -1;

	public TextMenuWrappedText(string text, int maxWidth = 1200, float scale = 0.75f, bool selectable = false)
	{
		this.text = text ?? "";
		this.scale = Math.Max(0.25f, scale);
		this.maxWidth = Math.Max(64, maxWidth);
		Selectable = selectable;
		IncludeWidthInMeasurement = false;
	}

	public override float LeftWidth()
	{
		EnsureWrapped();
		return ActiveFont.Measure(wrappedText).X * scale;
	}

	public override float Height()
	{
		EnsureWrapped();
		float num = ActiveFont.Measure(wrappedText).Y * scale;
		if (!(num > 0f))
		{
			return ActiveFont.LineHeight * scale;
		}
		return num;
	}

	public override void Render(Vector2 position, bool highlighted)
	{
		EnsureWrapped();
		float alpha = Container.Alpha;
		Color strokeColor = Color.Black * (alpha * alpha * alpha);
		Color color = ((highlighted ? Container.HighlightColor : Color.White) * alpha);
		ActiveFont.DrawOutline(wrappedText, position, new Vector2(0f, 0.5f), Vector2.One * scale, color, 2f, strokeColor);
	}

	private void EnsureWrapped()
	{
		int wrapWidth = GetWrapWidth();
		if (wrappedWidth == wrapWidth)
		{
			return;
		}

		wrappedWidth = wrapWidth;
		wrappedText = ActiveFont.FontSize.AutoNewline(text, wrapWidth);
	}

	private int GetWrapWidth()
	{
		float num = maxWidth;
		if (Container != null)
		{
			if (Container.Width > 0f)
			{
				num = Math.Min(num, Container.Width);
			}
			else if (Container.MinWidth > 0f)
			{
				num = Math.Min(num, Container.MinWidth);
			}
		}

		return Math.Max(16, (int)(num / scale));
	}
}
