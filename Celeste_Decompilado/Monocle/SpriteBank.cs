using System;
using System.Collections.Generic;
using System.Xml;

namespace Monocle;

public class SpriteBank
{
	public Atlas Atlas;

	public XmlDocument XML;

	public Dictionary<string, SpriteData> SpriteData;

	public SpriteBank(Atlas atlas, XmlDocument xml)
	{
		Atlas = atlas;
		XML = xml;
		SpriteData = new Dictionary<string, SpriteData>(StringComparer.OrdinalIgnoreCase);
		Dictionary<string, XmlElement> dictionary = new Dictionary<string, XmlElement>();
		foreach (object childNode in XML["Sprites"].ChildNodes)
		{
			if (childNode is XmlElement)
			{
				XmlElement xmlElement = childNode as XmlElement;
				dictionary.Add(xmlElement.Name, xmlElement);
				if (SpriteData.ContainsKey(xmlElement.Name))
				{
					throw new Exception("Duplicate sprite name in SpriteData: '" + xmlElement.Name + "'!");
				}
				SpriteData spriteData = (SpriteData[xmlElement.Name] = new SpriteData(Atlas));
				SpriteData spriteData3 = spriteData;
				if (xmlElement.HasAttr("copy"))
				{
					spriteData3.Add(dictionary[xmlElement.Attr("copy")], xmlElement.Attr("path"));
				}
				spriteData3.Add(xmlElement);
			}
		}
	}

	public SpriteBank(Atlas atlas, string xmlPath)
		: this(atlas, Calc.LoadContentXML(xmlPath))
	{
	}

	public bool Has(string id)
	{
		return SpriteData.ContainsKey(id);
	}

	public Sprite Create(string id)
	{
		if (SpriteData.ContainsKey(id))
		{
			return SpriteData[id].Create();
		}
		throw new Exception("Missing animation name in SpriteData: '" + id + "'!");
	}

	public Sprite CreateOn(Sprite sprite, string id)
	{
		if (SpriteData.ContainsKey(id))
		{
			return SpriteData[id].CreateOn(sprite);
		}
		throw new Exception("Missing animation name in SpriteData: '" + id + "'!");
	}
}
