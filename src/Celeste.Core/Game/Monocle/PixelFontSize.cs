using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;

namespace Monocle;

public class PixelFontSize
{
	public List<MTexture> Textures;

	public Dictionary<int, PixelFontCharacter> Characters;

	public int LineHeight;

	public float Size;

	public bool Outline;

	private StringBuilder temp = new StringBuilder();

	public string AutoNewline(string text, int width)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		temp.Clear();
		string[] array = Regex.Split(text, "(\\s)");
		float num = 0f;
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			float x = Measure(text2).X;
			if (x + num > (float)width)
			{
				temp.Append('\n');
				num = 0f;
				if (text2.Equals(" "))
				{
					continue;
				}
			}
			if (x > (float)width)
			{
				int j = 1;
				int num2 = 0;
				for (; j < text2.Length; j++)
				{
					if (j - num2 > 1 && Measure(text2.Substring(num2, j - num2 - 1)).X > (float)width)
					{
						temp.Append(text2.Substring(num2, j - num2 - 1));
						temp.Append('\n');
						num2 = j - 1;
					}
				}
				string text3 = text2.Substring(num2, text2.Length - num2);
				temp.Append(text3);
				num += Measure(text3).X;
			}
			else
			{
				num += x;
				temp.Append(text2);
			}
		}
		return temp.ToString();
	}

	public PixelFontCharacter Get(int id)
	{
		PixelFontCharacter value = null;
		if (Characters.TryGetValue(id, out value))
		{
			return value;
		}
		return null;
	}

	public Vector2 Measure(char text)
	{
		PixelFontCharacter value = null;
		if (Characters.TryGetValue(text, out value))
		{
			return new Vector2(value.XAdvance, LineHeight);
		}
		return Vector2.Zero;
	}

	public Vector2 Measure(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return Vector2.Zero;
		}
		Vector2 result = new Vector2(0f, LineHeight);
		float num = 0f;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\n')
			{
				result.Y += LineHeight;
				if (num > result.X)
				{
					result.X = num;
				}
				num = 0f;
				continue;
			}
			PixelFontCharacter value = null;
			if (Characters.TryGetValue(text[i], out value))
			{
				num += (float)value.XAdvance;
				if (i < text.Length - 1 && value.Kerning.TryGetValue(text[i + 1], out var value2))
				{
					num += (float)value2;
				}
			}
		}
		if (num > result.X)
		{
			result.X = num;
		}
		return result;
	}

	public float WidthToNextLine(string text, int start)
	{
		if (string.IsNullOrEmpty(text))
		{
			return 0f;
		}
		float num = 0f;
		int i = start;
		for (int length = text.Length; i < length && text[i] != '\n'; i++)
		{
			PixelFontCharacter value = null;
			if (Characters.TryGetValue(text[i], out value))
			{
				num += (float)value.XAdvance;
				if (i < length - 1 && value.Kerning.TryGetValue(text[i + 1], out var value2))
				{
					num += (float)value2;
				}
			}
		}
		return num;
	}

	public float HeightOf(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return 0f;
		}
		int num = 1;
		if (text.IndexOf('\n') >= 0)
		{
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '\n')
				{
					num++;
				}
			}
		}
		return num * LineHeight;
	}

	public void Draw(char character, Vector2 position, Vector2 justify, Vector2 scale, Color color)
	{
		if (!char.IsWhiteSpace(character))
		{
			PixelFontCharacter value = null;
			if (Characters.TryGetValue(character, out value))
			{
				Vector2 vector = Measure(character);
				Vector2 vector2 = new Vector2(vector.X * justify.X, vector.Y * justify.Y);
				Vector2 val = position + (new Vector2(value.XOffset, value.YOffset) - vector2) * scale;
				value.Texture.Draw(val.FloorV2(), Vector2.Zero, color, scale);
			}
		}
	}

	public void Draw(string text, Vector2 position, Vector2 justify, Vector2 scale, Color color, float edgeDepth, Color edgeColor, float stroke, Color strokeColor)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		Vector2 zero = Vector2.Zero;
		float num = ((justify.X != 0f) ? WidthToNextLine(text, 0) : 0f);
		Vector2 vector = new Vector2(num * justify.X, HeightOf(text) * justify.Y);
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\n')
			{
				zero.X = 0f;
				zero.Y += LineHeight;
				if (justify.X != 0f)
				{
					vector.X = WidthToNextLine(text, i + 1) * justify.X;
				}
				continue;
			}
			PixelFontCharacter value = null;
			if (!Characters.TryGetValue(text[i], out value))
			{
				continue;
			}
			Vector2 vector2 = position + (zero + new Vector2(value.XOffset, value.YOffset) - vector) * scale;
			if (stroke > 0f && !Outline)
			{
				if (edgeDepth > 0f)
				{
					value.Texture.Draw(vector2 + new Vector2(0f, 0f - stroke), Vector2.Zero, strokeColor, scale);
					for (float num2 = 0f - stroke; num2 < edgeDepth + stroke; num2 += stroke)
					{
						value.Texture.Draw(vector2 + new Vector2(0f - stroke, num2), Vector2.Zero, strokeColor, scale);
						value.Texture.Draw(vector2 + new Vector2(stroke, num2), Vector2.Zero, strokeColor, scale);
					}
					value.Texture.Draw(vector2 + new Vector2(0f - stroke, edgeDepth + stroke), Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(0f, edgeDepth + stroke), Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(stroke, edgeDepth + stroke), Vector2.Zero, strokeColor, scale);
				}
				else
				{
					value.Texture.Draw(vector2 + new Vector2(-1f, -1f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(0f, -1f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(1f, -1f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(-1f, 0f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(1f, 0f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(-1f, 1f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(0f, 1f) * stroke, Vector2.Zero, strokeColor, scale);
					value.Texture.Draw(vector2 + new Vector2(1f, 1f) * stroke, Vector2.Zero, strokeColor, scale);
				}
			}
			if (edgeDepth > 0f)
			{
				value.Texture.Draw(vector2 + Vector2.UnitY * edgeDepth, Vector2.Zero, edgeColor, scale);
			}
			value.Texture.Draw(vector2, Vector2.Zero, color, scale);
			zero.X += value.XAdvance;
			if (i < text.Length - 1 && value.Kerning.TryGetValue(text[i + 1], out var value2))
			{
				zero.X += value2;
			}
		}
	}

	public void Draw(string text, Vector2 position, Color color)
	{
		Draw(text, position, Vector2.Zero, Vector2.One, color, 0f, Color.Transparent, 0f, Color.Transparent);
	}

	public void Draw(string text, Vector2 position, Vector2 justify, Vector2 scale, Color color)
	{
		Draw(text, position, justify, scale, color, 0f, Color.Transparent, 0f, Color.Transparent);
	}

	public void DrawOutline(string text, Vector2 position, Vector2 justify, Vector2 scale, Color color, float stroke, Color strokeColor)
	{
		Draw(text, position, justify, scale, color, 0f, Color.Transparent, stroke, strokeColor);
	}

	public void DrawEdgeOutline(string text, Vector2 position, Vector2 justify, Vector2 scale, Color color, float edgeDepth, Color edgeColor, float stroke = 0f, Color strokeColor = default(Color))
	{
		Draw(text, position, justify, scale, color, edgeDepth, edgeColor, stroke, strokeColor);
	}
}
