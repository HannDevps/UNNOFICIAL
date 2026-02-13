using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiJournalSpeedrun : OuiJournalPage
{
	private Table table;

	public OuiJournalSpeedrun(OuiJournal journal)
		: base(journal)
	{
		PageTexture = "page";
		Vector2 justify = new Vector2(0.5f, 0.5f);
		float num = 0.5f;
		Color color = Color.Black * 0.6f;
		table = new Table().AddColumn(new TextCell(Dialog.Clean("journal_speedruns"), new Vector2(1f, 0.5f), 0.7f, Color.Black * 0.7f)).AddColumn(new TextCell(Dialog.Clean("journal_mode_normal"), justify, num + 0.1f, color, 240f)).AddColumn(new TextCell(Dialog.Clean("journal_mode_normal_fullclear"), justify, num + 0.1f, color, 240f));
		if (SaveData.Instance.UnlockedModes >= 2)
		{
			table.AddColumn(new TextCell(Dialog.Clean("journal_mode_bside"), justify, num + 0.1f, color, 240f));
		}
		if (SaveData.Instance.UnlockedModes >= 3)
		{
			table.AddColumn(new TextCell(Dialog.Clean("journal_mode_cside"), justify, num + 0.1f, color, 240f));
		}
		foreach (AreaStats area in SaveData.Instance.Areas)
		{
			AreaData areaData = AreaData.Get(area.ID);
			if (areaData.Interlude || areaData.IsFinal)
			{
				continue;
			}
			if (areaData.ID > SaveData.Instance.UnlockedAreas)
			{
				break;
			}
			Row row = table.AddRow().Add(new TextCell(Dialog.Clean(areaData.Name), new Vector2(1f, 0.5f), num + 0.1f, color));
			if (area.Modes[0].BestTime > 0)
			{
				row.Add(new TextCell(Dialog.Time(area.Modes[0].BestTime), justify, num, color));
			}
			else
			{
				row.Add(new IconCell("dot"));
			}
			if (areaData.CanFullClear)
			{
				if (area.Modes[0].BestFullClearTime > 0)
				{
					row.Add(new TextCell(Dialog.Time(area.Modes[0].BestFullClearTime), justify, num, color));
				}
				else
				{
					row.Add(new IconCell("dot"));
				}
			}
			else
			{
				row.Add(new TextCell("-", TextJustify, 0.5f, TextColor));
			}
			if (SaveData.Instance.UnlockedModes >= 2)
			{
				if (areaData.HasMode(AreaMode.BSide))
				{
					if (area.Modes[1].BestTime > 0)
					{
						row.Add(new TextCell(Dialog.Time(area.Modes[1].BestTime), justify, num, color));
					}
					else
					{
						row.Add(new IconCell("dot"));
					}
				}
				else
				{
					row.Add(new TextCell("-", TextJustify, 0.5f, TextColor));
				}
			}
			if (SaveData.Instance.UnlockedModes < 3)
			{
				continue;
			}
			if (areaData.HasMode(AreaMode.CSide))
			{
				if (area.Modes[2].BestTime > 0)
				{
					row.Add(new TextCell(Dialog.Time(area.Modes[2].BestTime), justify, num, color));
				}
				else
				{
					row.Add(new IconCell("dot"));
				}
			}
			else
			{
				row.Add(new TextCell("-", TextJustify, 0.5f, TextColor));
			}
		}
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		bool flag4 = true;
		long num2 = 0L;
		long num3 = 0L;
		long num4 = 0L;
		long num5 = 0L;
		foreach (AreaStats area2 in SaveData.Instance.Areas)
		{
			AreaData areaData2 = AreaData.Get(area2.ID);
			if (!areaData2.Interlude && !areaData2.IsFinal)
			{
				if (area2.ID > SaveData.Instance.UnlockedAreas)
				{
					flag = (flag2 = (flag3 = (flag4 = false)));
					break;
				}
				num2 += area2.Modes[0].BestTime;
				num3 += area2.Modes[0].BestFullClearTime;
				num4 += area2.Modes[1].BestTime;
				num5 += area2.Modes[2].BestTime;
				if (area2.Modes[0].BestTime <= 0)
				{
					flag = false;
				}
				if (areaData2.CanFullClear && area2.Modes[0].BestFullClearTime <= 0)
				{
					flag2 = false;
				}
				if (areaData2.HasMode(AreaMode.BSide) && area2.Modes[1].BestTime <= 0)
				{
					flag3 = false;
				}
				if (areaData2.HasMode(AreaMode.CSide) && area2.Modes[2].BestTime <= 0)
				{
					flag4 = false;
				}
			}
		}
		if (flag || flag2 || flag3 || flag4)
		{
			table.AddRow();
			Row row2 = table.AddRow().Add(new TextCell(Dialog.Clean("journal_totals"), new Vector2(1f, 0.5f), num + 0.2f, color));
			if (flag)
			{
				row2.Add(new TextCell(Dialog.Time(num2), justify, num + 0.1f, color));
			}
			else
			{
				row2.Add(new IconCell("dot"));
			}
			if (flag2)
			{
				row2.Add(new TextCell(Dialog.Time(num3), justify, num + 0.1f, color));
			}
			else
			{
				row2.Add(new IconCell("dot"));
			}
			if (SaveData.Instance.UnlockedModes >= 2)
			{
				if (flag3)
				{
					row2.Add(new TextCell(Dialog.Time(num4), justify, num + 0.1f, color));
				}
				else
				{
					row2.Add(new IconCell("dot"));
				}
			}
			if (SaveData.Instance.UnlockedModes >= 3)
			{
				if (flag4)
				{
					row2.Add(new TextCell(Dialog.Time(num5), justify, num + 0.1f, color));
				}
				else
				{
					row2.Add(new IconCell("dot"));
				}
			}
			table.AddRow();
		}
		long num6 = 0L;
		foreach (AreaStats area3 in SaveData.Instance.Areas)
		{
			AreaData areaData3 = AreaData.Get(area3.ID);
			if (areaData3.IsFinal)
			{
				if (areaData3.ID > SaveData.Instance.UnlockedAreas)
				{
					break;
				}
				num6 += area3.Modes[0].BestTime;
				Row row3 = table.AddRow().Add(new TextCell(Dialog.Clean(areaData3.Name), new Vector2(1f, 0.5f), num + 0.1f, color));
				row3.Add(null);
				if (area3.Modes[0].BestTime > 0)
				{
					Cell cell;
					row3.Add(cell = new TextCell(Dialog.Time(area3.Modes[0].BestTime), justify, num, color));
				}
				else
				{
					Cell cell;
					row3.Add(cell = new IconCell("dot"));
				}
				table.AddRow();
			}
		}
		if (flag && flag2 && flag3 && flag4)
		{
			TextCell entry = new TextCell(Dialog.Time(num2 + num3 + num4 + num5 + num6), justify, num + 0.2f, color)
			{
				SpreadOverColumns = 1 + SaveData.Instance.UnlockedModes
			};
			table.AddRow().Add(new TextCell(Dialog.Clean("journal_grandtotal"), new Vector2(1f, 0.5f), num + 0.3f, color)).Add(entry);
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
