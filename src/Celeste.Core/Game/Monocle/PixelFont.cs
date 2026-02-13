using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Monocle;

public class PixelFont
{
	public string Face;

	public List<PixelFontSize> Sizes = new List<PixelFontSize>();

	private List<VirtualTexture> managedTextures = new List<VirtualTexture>();

	public PixelFont(string face)
	{
		Face = face;
	}

	public PixelFontSize AddFontSize(string path, Atlas atlas = null, bool outline = false)
	{
		XmlElement data = Calc.LoadXML(path)["font"];
		return AddFontSize(path, data, atlas, outline);
	}

	public PixelFontSize AddFontSize(string path, XmlElement data, Atlas atlas = null, bool outline = false)
	{
		float num = data["info"].AttrFloat("size");
		foreach (PixelFontSize size in Sizes)
		{
			if (size.Size == num)
			{
				return size;
			}
		}
		List<MTexture> list = new List<MTexture>();
		foreach (XmlElement item in data["pages"])
		{
			string text = item.Attr("file");
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
			if (atlas != null && atlas.Has(fileNameWithoutExtension))
			{
				list.Add(atlas[fileNameWithoutExtension]);
				continue;
			}
			VirtualTexture virtualTexture = VirtualContent.CreateTexture(Path.Combine(Path.GetDirectoryName(path).Substring(Engine.ContentDirectory.Length + 1), text));
			list.Add(new MTexture(virtualTexture));
			managedTextures.Add(virtualTexture);
		}
		PixelFontSize pixelFontSize = new PixelFontSize
		{
			Textures = list,
			Characters = new Dictionary<int, PixelFontCharacter>(),
			LineHeight = data["common"].AttrInt("lineHeight"),
			Size = num,
			Outline = outline
		};
		foreach (XmlElement item2 in data["chars"])
		{
			int num2 = item2.AttrInt("id");
			int index = item2.AttrInt("page", 0);
			pixelFontSize.Characters.Add(num2, new PixelFontCharacter(num2, list[index], item2));
		}
		if (data["kernings"] != null)
		{
			foreach (XmlElement item3 in data["kernings"])
			{
				int key = item3.AttrInt("first");
				int key2 = item3.AttrInt("second");
				int value = item3.AttrInt("amount");
				PixelFontCharacter value2 = null;
				if (pixelFontSize.Characters.TryGetValue(key, out value2))
				{
					value2.Kerning.Add(key2, value);
				}
			}
		}
		Sizes.Add(pixelFontSize);
		Sizes.Sort((PixelFontSize a, PixelFontSize b) => Math.Sign(a.Size - b.Size));
		return pixelFontSize;
	}

	public PixelFontSize Get(float size)
	{
		int i = 0;
		for (int num = Sizes.Count - 1; i < num; i++)
		{
			if (Sizes[i].Size >= size)
			{
				return Sizes[i];
			}
		}
		return Sizes[Sizes.Count - 1];
	}

	public bool Has(float size)
	{
		int i = 0;
		for (int num = Sizes.Count - 1; i < num; i++)
		{
			if (Sizes[i].Size == size)
			{
				return true;
			}
		}
		return false;
	}

	public void Draw(float baseSize, char character, Vector2 position, Vector2 justify, Vector2 scale, Color color)
	{
		PixelFontSize pixelFontSize = Get(baseSize * Math.Max(scale.X, scale.Y));
		scale *= baseSize / pixelFontSize.Size;
		pixelFontSize.Draw(character, position, justify, scale, color);
	}

	public void Draw(float baseSize, string text, Vector2 position, Vector2 justify, Vector2 scale, Color color, float edgeDepth, Color edgeColor, float stroke, Color strokeColor)
	{
		PixelFontSize pixelFontSize = Get(baseSize * Math.Max(scale.X, scale.Y));
		scale *= baseSize / pixelFontSize.Size;
		pixelFontSize.Draw(text, position, justify, scale, color, edgeDepth, edgeColor, stroke, strokeColor);
	}

	public void Draw(float baseSize, string text, Vector2 position, Color color)
	{
		Vector2 one = Vector2.One;
		PixelFontSize pixelFontSize = Get(baseSize * Math.Max(one.X, one.Y));
		one *= baseSize / pixelFontSize.Size;
		pixelFontSize.Draw(text, position, Vector2.Zero, Vector2.One, color, 0f, Color.Transparent, 0f, Color.Transparent);
	}

	public void Draw(float baseSize, string text, Vector2 position, Vector2 justify, Vector2 scale, Color color)
	{
		PixelFontSize pixelFontSize = Get(baseSize * Math.Max(scale.X, scale.Y));
		scale *= baseSize / pixelFontSize.Size;
		pixelFontSize.Draw(text, position, justify, scale, color, 0f, Color.Transparent, 0f, Color.Transparent);
	}

	public void DrawOutline(float baseSize, string text, Vector2 position, Vector2 justify, Vector2 scale, Color color, float stroke, Color strokeColor)
	{
		PixelFontSize pixelFontSize = Get(baseSize * Math.Max(scale.X, scale.Y));
		scale *= baseSize / pixelFontSize.Size;
		pixelFontSize.Draw(text, position, justify, scale, color, 0f, Color.Transparent, stroke, strokeColor);
	}

	public void DrawEdgeOutline(float baseSize, string text, Vector2 position, Vector2 justify, Vector2 scale, Color color, float edgeDepth, Color edgeColor, float stroke = 0f, Color strokeColor = default(Color))
	{
		PixelFontSize pixelFontSize = Get(baseSize * Math.Max(scale.X, scale.Y));
		scale *= baseSize / pixelFontSize.Size;
		pixelFontSize.Draw(text, position, justify, scale, color, edgeDepth, edgeColor, stroke, strokeColor);
	}

	public void Dispose()
	{
		foreach (VirtualTexture managedTexture in managedTextures)
		{
			managedTexture.Dispose();
		}
		Sizes.Clear();
	}
}
