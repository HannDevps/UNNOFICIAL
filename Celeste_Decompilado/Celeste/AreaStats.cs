using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Celeste;

[Serializable]
public class AreaStats
{
	[XmlAttribute]
	public int ID;

	[XmlAttribute]
	public bool Cassette;

	public AreaModeStats[] Modes;

	public int TotalStrawberries
	{
		get
		{
			int num = 0;
			for (int i = 0; i < Modes.Length; i++)
			{
				num += Modes[i].TotalStrawberries;
			}
			return num;
		}
	}

	public int TotalDeaths
	{
		get
		{
			int num = 0;
			for (int i = 0; i < Modes.Length; i++)
			{
				num += Modes[i].Deaths;
			}
			return num;
		}
	}

	public long TotalTimePlayed
	{
		get
		{
			long num = 0L;
			for (int i = 0; i < Modes.Length; i++)
			{
				num += Modes[i].TimePlayed;
			}
			return num;
		}
	}

	public int BestTotalDeaths
	{
		get
		{
			int num = 0;
			for (int i = 0; i < Modes.Length; i++)
			{
				num += Modes[i].BestDeaths;
			}
			return num;
		}
	}

	public int BestTotalDashes
	{
		get
		{
			int num = 0;
			for (int i = 0; i < Modes.Length; i++)
			{
				num += Modes[i].BestDashes;
			}
			return num;
		}
	}

	public long BestTotalTime
	{
		get
		{
			long num = 0L;
			for (int i = 0; i < Modes.Length; i++)
			{
				num += Modes[i].BestTime;
			}
			return num;
		}
	}

	public AreaStats(int id)
	{
		ID = id;
		int length = Enum.GetValues(typeof(AreaMode)).Length;
		Modes = new AreaModeStats[length];
		for (int i = 0; i < Modes.Length; i++)
		{
			Modes[i] = new AreaModeStats();
		}
	}

	private AreaStats()
	{
		int length = Enum.GetValues(typeof(AreaMode)).Length;
		Modes = new AreaModeStats[length];
		for (int i = 0; i < length; i++)
		{
			Modes[i] = new AreaModeStats();
		}
	}

	public AreaStats Clone()
	{
		AreaStats areaStats = new AreaStats
		{
			ID = ID,
			Cassette = Cassette
		};
		for (int i = 0; i < Modes.Length; i++)
		{
			areaStats.Modes[i] = Modes[i].Clone();
		}
		return areaStats;
	}

	public void CleanCheckpoints()
	{
		foreach (AreaMode value in Enum.GetValues(typeof(AreaMode)))
		{
			if (AreaData.Get(ID).Mode.Length <= (int)value)
			{
				continue;
			}
			AreaModeStats areaModeStats = Modes[(int)value];
			ModeProperties modeProperties = AreaData.Get(ID).Mode[(int)value];
			HashSet<string> hashSet = new HashSet<string>(areaModeStats.Checkpoints);
			areaModeStats.Checkpoints.Clear();
			if (modeProperties == null || modeProperties.Checkpoints == null)
			{
				continue;
			}
			CheckpointData[] checkpoints = modeProperties.Checkpoints;
			foreach (CheckpointData checkpointData in checkpoints)
			{
				if (hashSet.Contains(checkpointData.Level))
				{
					areaModeStats.Checkpoints.Add(checkpointData.Level);
				}
			}
		}
	}
}
