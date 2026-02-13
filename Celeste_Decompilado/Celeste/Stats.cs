using System.Collections.Generic;

namespace Celeste;

public static class Stats
{
	private static Dictionary<Stat, string> statToString = new Dictionary<Stat, string>();

	private static bool ready;

	public static void MakeRequest()
	{
		ready = true;
	}


	public static bool Has()
	{
		return ready;
	}

	public static void Increment(Stat stat, int increment = 1)
	{
		if (ready)
		{
			string value = null;
			if (!statToString.TryGetValue(stat, out value))
			{
				statToString.Add(stat, value = stat.ToString());
			}
			{
			}
		}
	}

	public static int Local(Stat stat)
	{
		int pData = 0;
		if (ready)
		{
			string value = null;
			if (!statToString.TryGetValue(stat, out value))
			{
				statToString.Add(stat, value = stat.ToString());
			}
		}
		return pData;
	}

	public static long Global(Stat stat)
	{
		long pData = 0L;
		if (ready)
		{
			string value = null;
			if (!statToString.TryGetValue(stat, out value))
			{
				statToString.Add(stat, value = stat.ToString());
			}
		}
		return pData;
	}

	public static void Store()
	{
		if (ready)
		{
		}
	}

	public static string Name(Stat stat)
	{
		return Dialog.Clean("STAT_" + stat);
	}
}
