using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Monocle;

public class SpriteData
{
	public List<SpriteDataSource> Sources = new List<SpriteDataSource>();

	public Sprite Sprite;

	public Atlas Atlas;

	public SpriteData(Atlas atlas)
	{
		Sprite = new Sprite(atlas, "");
		Atlas = atlas;
	}

	public void Add(XmlElement xml, string overridePath = null)
	{
		SpriteDataSource spriteDataSource = new SpriteDataSource();
		spriteDataSource.XML = xml;
		spriteDataSource.Path = spriteDataSource.XML.Attr("path");
		spriteDataSource.OverridePath = overridePath;
		string text = "Sprite '" + spriteDataSource.XML.Name + "': ";
		if (!spriteDataSource.XML.HasAttr("path") && string.IsNullOrEmpty(overridePath))
		{
			throw new Exception(text + "'path' is missing!");
		}
		HashSet<string> hashSet = new HashSet<string>();
		foreach (XmlElement item in spriteDataSource.XML.GetElementsByTagName("Anim"))
		{
			CheckAnimXML(item, text, hashSet);
		}
		foreach (XmlElement item2 in spriteDataSource.XML.GetElementsByTagName("Loop"))
		{
			CheckAnimXML(item2, text, hashSet);
		}
		if (spriteDataSource.XML.HasAttr("start") && !hashSet.Contains(spriteDataSource.XML.Attr("start")))
		{
			throw new Exception(text + "starting animation '" + spriteDataSource.XML.Attr("start") + "' is missing!");
		}
		if (spriteDataSource.XML.HasChild("Justify") && spriteDataSource.XML.HasChild("Origin"))
		{
			throw new Exception(text + "has both Origin and Justify tags!");
		}
		string text2 = spriteDataSource.XML.Attr("path", "");
		float defaultValue = spriteDataSource.XML.AttrFloat("delay", 0f);
		foreach (XmlElement item3 in spriteDataSource.XML.GetElementsByTagName("Anim"))
		{
			Chooser<string> chooser = ((!item3.HasAttr("goto")) ? null : Chooser<string>.FromString<string>(item3.Attr("goto")));
			string id = item3.Attr("id");
			string text3 = item3.Attr("path", "");
			int[] frames = Calc.ReadCSVIntWithTricks(item3.Attr("frames", ""));
			text3 = ((string.IsNullOrEmpty(overridePath) || !HasFrames(Atlas, overridePath + text3, frames)) ? (text2 + text3) : (overridePath + text3));
			Sprite.Add(id, text3, item3.AttrFloat("delay", defaultValue), chooser, frames);
		}
		foreach (XmlElement item4 in spriteDataSource.XML.GetElementsByTagName("Loop"))
		{
			string id2 = item4.Attr("id");
			string text4 = item4.Attr("path", "");
			int[] frames2 = Calc.ReadCSVIntWithTricks(item4.Attr("frames", ""));
			text4 = ((string.IsNullOrEmpty(overridePath) || !HasFrames(Atlas, overridePath + text4, frames2)) ? (text2 + text4) : (overridePath + text4));
			Sprite.AddLoop(id2, text4, item4.AttrFloat("delay", defaultValue), frames2);
		}
		if (spriteDataSource.XML.HasChild("Center"))
		{
			Sprite.CenterOrigin();
			Sprite.Justify = new Vector2(0.5f, 0.5f);
		}
		else if (spriteDataSource.XML.HasChild("Justify"))
		{
			Sprite.JustifyOrigin(spriteDataSource.XML.ChildPosition("Justify"));
			Sprite.Justify = spriteDataSource.XML.ChildPosition("Justify");
		}
		else if (spriteDataSource.XML.HasChild("Origin"))
		{
			Sprite.Origin = spriteDataSource.XML.ChildPosition("Origin");
		}
		if (spriteDataSource.XML.HasChild("Position"))
		{
			Sprite.Position = spriteDataSource.XML.ChildPosition("Position");
		}
		if (spriteDataSource.XML.HasAttr("start"))
		{
			Sprite.Play(spriteDataSource.XML.Attr("start"));
		}
		Sources.Add(spriteDataSource);
	}

	private bool HasFrames(Atlas atlas, string path, int[] frames = null)
	{
		if (frames == null || frames.Length == 0)
		{
			return atlas.GetAtlasSubtexturesAt(path, 0) != null;
		}
		for (int i = 0; i < frames.Length; i++)
		{
			if (atlas.GetAtlasSubtexturesAt(path, frames[i]) == null)
			{
				return false;
			}
		}
		return true;
	}

	private void CheckAnimXML(XmlElement xml, string prefix, HashSet<string> ids)
	{
		if (!xml.HasAttr("id"))
		{
			throw new Exception(prefix + "'id' is missing on " + xml.Name + "!");
		}
		if (ids.Contains(xml.Attr("id")))
		{
			throw new Exception(prefix + "multiple animations with id '" + xml.Attr("id") + "'!");
		}
		ids.Add(xml.Attr("id"));
	}

	public Sprite Create()
	{
		return Sprite.CreateClone();
	}

	public Sprite CreateOn(Sprite sprite)
	{
		return Sprite.CloneInto(sprite);
	}
}
