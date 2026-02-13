using System;
using System.Xml.Serialization;

namespace Celeste;

[Serializable]
public struct EntityID
{
	public static readonly EntityID None = new EntityID("null", -1);

	[XmlIgnore]
	public string Level;

	[XmlIgnore]
	public int ID;

	[XmlAttribute]
	public string Key
	{
		get
		{
			return Level + ":" + ID;
		}
		set
		{
			string[] array = value.Split(':');
			Level = array[0];
			ID = int.Parse(array[1]);
		}
	}

	public EntityID(string level, int entityID)
	{
		Level = level;
		ID = entityID;
	}

	public override string ToString()
	{
		return Key;
	}

	public override int GetHashCode()
	{
		return Level.GetHashCode() ^ ID;
	}
}
