using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monocle;

public class PixelText : Component
{
	private struct Char
	{
		public Vector2 Offset;

		public PixelFontCharacter CharData;

		public Rectangle Bounds;
	}

	private List<Char> characters = new List<Char>();

	private PixelFont font;

	private PixelFontSize size;

	private string text;

	private bool dirty;

	public Vector2 Position;

	public Color Color = Color.White;

	public Vector2 Scale = Vector2.One;

	public PixelFont Font
	{
		get
		{
			return font;
		}
		set
		{
			if (value != font)
			{
				dirty = true;
			}
			font = value;
		}
	}

	public float Size
	{
		get
		{
			return size.Size;
		}
		set
		{
			if (value != size.Size)
			{
				dirty = true;
			}
			size = font.Get(value);
		}
	}

	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (value != text)
			{
				dirty = true;
			}
			text = value;
		}
	}

	public int Width { get; private set; }

	public int Height { get; private set; }

	public PixelText(PixelFont font, string text, Color color)
		: base(active: false, visible: true)
	{
		Font = font;
		Text = text;
		Color = color;
		Text = text;
		size = Font.Sizes[0];
		Refresh();
	}

	public void Refresh()
	{
		dirty = false;
		characters.Clear();
		int num = 0;
		int num2 = 1;
		Vector2 zero = Vector2.Zero;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\n')
			{
				zero.X = 0f;
				zero.Y += size.LineHeight;
				num2++;
			}
			PixelFontCharacter pixelFontCharacter = size.Get(text[i]);
			if (pixelFontCharacter != null)
			{
				characters.Add(new Char
				{
					Offset = zero + new Vector2(pixelFontCharacter.XOffset, pixelFontCharacter.YOffset),
					CharData = pixelFontCharacter,
					Bounds = pixelFontCharacter.Texture.ClipRect
				});
				if (zero.X > (float)num)
				{
					num = (int)zero.X;
				}
				zero.X += pixelFontCharacter.XAdvance;
			}
		}
		Width = num;
		Height = num2 * size.LineHeight;
	}

	public override void Render()
	{
		if (dirty)
		{
			Refresh();
		}
		for (int i = 0; i < characters.Count; i++)
		{
			characters[i].CharData.Texture.Draw(Position + characters[i].Offset, Vector2.Zero, Color);
		}
	}
}
