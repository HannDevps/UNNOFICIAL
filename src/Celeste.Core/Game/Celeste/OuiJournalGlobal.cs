using System;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class OuiJournalGlobal : OuiJournalPage
{
	private Table table;

	public OuiJournalGlobal(OuiJournal journal)
		: base(journal)
	{
		PageTexture = "page";
		table = new Table().AddColumn(new TextCell("", new Vector2(1f, 0.5f), 1f, TextColor, 700f)).AddColumn(new TextCell(Dialog.Clean("STATS_TITLE"), new Vector2(0.5f, 0.5f), 1f, TextColor, 48f, forceWidth: true)).AddColumn(new TextCell("", new Vector2(1f, 0.5f), 0.7f, TextColor, 700f));
		foreach (Stat value in Enum.GetValues(typeof(Stat)))
		{
			if (SaveData.Instance.CheatMode || SaveData.Instance.DebugMode || ((value != Stat.GOLDBERRIES || SaveData.Instance.TotalHeartGems >= 16) && ((value != Stat.PICO_BERRIES && value != Stat.PICO_COMPLETES && value != Stat.PICO_DEATHS) || Settings.Instance.Pico8OnMainMenu)))
			{
				string text = Stats.Global(value).ToString();
				string text2 = Stats.Name(value);
				string text3 = "";
				int num = text.Length - 1;
				int num2 = 0;
				while (num >= 0)
				{
					text3 = text[num] + ((num2 > 0 && num2 % 3 == 0) ? "," : "") + text3;
					num--;
					num2++;
				}
				Row row = table.AddRow();
				row.Add(new TextCell(text2, new Vector2(1f, 0.5f), 0.7f, TextColor));
				row.Add(null);
				row.Add(new TextCell(text3, new Vector2(0f, 0.5f), 0.8f, TextColor));
			}
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
