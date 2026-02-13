using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiJournalDeaths : OuiJournalPage
{
	private Table table;

	public OuiJournalDeaths(OuiJournal journal)
		: base(journal)
	{
		PageTexture = "page";
		table = new Table().AddColumn(new TextCell(Dialog.Clean("journal_deaths"), new Vector2(1f, 0.5f), 0.7f, TextColor, 300f));
		for (int i = 0; i < SaveData.Instance.UnlockedModes; i++)
		{
			table.AddColumn(new TextCell(Dialog.Clean("journal_mode_" + (AreaMode)i), TextJustify, 0.6f, TextColor, 240f));
		}
		bool[] array = new bool[3]
		{
			true,
			SaveData.Instance.UnlockedModes >= 2,
			SaveData.Instance.UnlockedModes >= 3
		};
		int[] array2 = new int[3];
		foreach (AreaStats area in SaveData.Instance.Areas)
		{
			AreaData areaData = AreaData.Get(area.ID);
			if (areaData.Interlude || areaData.IsFinal)
			{
				continue;
			}
			if (areaData.ID > SaveData.Instance.UnlockedAreas)
			{
				bool flag;
				array[1] = (flag = (array[2] = false));
				array[0] = flag;
				break;
			}
			Row row = table.AddRow();
			row.Add(new TextCell(Dialog.Clean(areaData.Name), new Vector2(1f, 0.5f), 0.6f, TextColor));
			for (int j = 0; j < SaveData.Instance.UnlockedModes; j++)
			{
				if (areaData.HasMode((AreaMode)j))
				{
					if (area.Modes[j].SingleRunCompleted)
					{
						int num = area.Modes[j].BestDeaths;
						if (num > 0)
						{
							foreach (EntityData goldenberry in AreaData.Areas[area.ID].Mode[j].MapData.Goldenberries)
							{
								EntityID item = new EntityID(goldenberry.Level.Name, goldenberry.ID);
								if (area.Modes[j].Strawberries.Contains(item))
								{
									num = 0;
								}
							}
						}
						row.Add(new TextCell(Dialog.Deaths(num), TextJustify, 0.5f, TextColor));
						array2[j] += num;
					}
					else
					{
						row.Add(new IconCell("dot"));
						array[j] = false;
					}
				}
				else
				{
					row.Add(new TextCell("-", TextJustify, 0.5f, TextColor));
				}
			}
		}
		if (array[0] || array[1] || array[2])
		{
			table.AddRow();
			Row row2 = table.AddRow();
			row2.Add(new TextCell(Dialog.Clean("journal_totals"), new Vector2(1f, 0.5f), 0.7f, TextColor));
			for (int k = 0; k < SaveData.Instance.UnlockedModes; k++)
			{
				row2.Add(new TextCell(Dialog.Deaths(array2[k]), TextJustify, 0.6f, TextColor));
			}
			table.AddRow();
		}
		int num2 = 0;
		foreach (AreaStats area2 in SaveData.Instance.Areas)
		{
			AreaData areaData2 = AreaData.Get(area2.ID);
			if (!areaData2.IsFinal)
			{
				continue;
			}
			if (areaData2.ID > SaveData.Instance.UnlockedAreas)
			{
				break;
			}
			Row row3 = table.AddRow();
			row3.Add(new TextCell(Dialog.Clean(areaData2.Name), new Vector2(1f, 0.5f), 0.6f, TextColor));
			if (area2.Modes[0].SingleRunCompleted)
			{
				int num3 = area2.Modes[0].BestDeaths;
				if (num3 > 0)
				{
					foreach (EntityData goldenberry2 in AreaData.Areas[area2.ID].Mode[0].MapData.Goldenberries)
					{
						EntityID item2 = new EntityID(goldenberry2.Level.Name, goldenberry2.ID);
						if (area2.Modes[0].Strawberries.Contains(item2))
						{
							num3 = 0;
						}
					}
				}
				TextCell entry = new TextCell(Dialog.Deaths(num3), TextJustify, 0.5f, TextColor);
				row3.Add(entry);
				num2 += num3;
			}
			else
			{
				row3.Add(new IconCell("dot"));
			}
			table.AddRow();
		}
		if (array[0] && array[1] && array[2])
		{
			TextCell entry2 = new TextCell(Dialog.Deaths(array2[0] + array2[1] + array2[2] + num2), TextJustify, 0.6f, TextColor)
			{
				SpreadOverColumns = 3
			};
			table.AddRow().Add(new TextCell(Dialog.Clean("journal_grandtotal"), new Vector2(1f, 0.5f), 0.7f, TextColor)).Add(entry2);
		}
	}

	public override void Redraw(VirtualRenderTarget buffer)
	{
		base.Redraw(buffer);
		Draw.SpriteBatch.Begin();
		table.Render(new Vector2(60f, 20f));
		Draw.SpriteBatch.End();
	}
}
