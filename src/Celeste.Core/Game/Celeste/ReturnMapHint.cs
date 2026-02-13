using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class ReturnMapHint : Entity
{
	private MTexture checkpoint;

	public ReturnMapHint()
	{
		base.Tag = Tags.HUD;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		Session session = (base.Scene as Level).Session;
		AreaKey area = session.Area;
		HashSet<string> checkpoints = SaveData.Instance.GetCheckpoints(area);
		CheckpointData checkpointData = null;
		ModeProperties modeProperties = AreaData.Areas[area.ID].Mode[(int)area.Mode];
		if (modeProperties.Checkpoints != null)
		{
			CheckpointData[] checkpoints2 = modeProperties.Checkpoints;
			foreach (CheckpointData checkpointData2 in checkpoints2)
			{
				if (session.LevelFlags.Contains(checkpointData2.Level) && checkpoints.Contains(checkpointData2.Level))
				{
					checkpointData = checkpointData2;
				}
			}
		}
		string text = area.ToString();
		if (checkpointData != null)
		{
			text = text + "_" + checkpointData.Level;
		}
		if (MTN.Checkpoints.Has(text))
		{
			checkpoint = MTN.Checkpoints[text];
		}
	}

	public static string GetCheckpointPreviewName(AreaKey area, string level)
	{
		if (level == null)
		{
			return area.ToString();
		}
		return area.ToString() + "_" + level;
	}

	private MTexture GetCheckpointPreview(AreaKey area, string level)
	{
		string checkpointPreviewName = GetCheckpointPreviewName(area, level);
		if (MTN.Checkpoints.Has(checkpointPreviewName))
		{
			return MTN.Checkpoints[checkpointPreviewName];
		}
		return null;
	}

	public override void Render()
	{
		MTexture mTexture = GFX.Gui["checkpoint"];
		string text = Dialog.Clean("MENU_RETURN_INFO");
		MTexture mTexture2 = MTN.Checkpoints["polaroid"];
		float num = ActiveFont.Measure(text).X * 0.75f;
		if (checkpoint != null)
		{
			float num2 = (float)mTexture2.Width * 0.25f;
			Vector2 vector = new Vector2((1920f - num - num2 - 64f) / 2f, 730f);
			float num3 = 720f / (float)checkpoint.ClipRect.Width;
			ActiveFont.DrawOutline(text, vector + new Vector2(num / 2f, 0f), new Vector2(0.5f, 0.5f), Vector2.One * 0.75f, Color.LightGray, 2f, Color.Black);
			vector.X += num + 64f;
			mTexture2.DrawCentered(vector + new Vector2(num2 / 2f, 0f), Color.White, 0.25f, 0.1f);
			checkpoint.DrawCentered(vector + new Vector2(num2 / 2f, 0f), Color.White, 0.25f * num3, 0.1f);
			mTexture.DrawCentered(vector + new Vector2(num2 * 0.8f, (float)mTexture2.Height * 0.25f * 0.5f * 0.8f), Color.White, 0.75f);
		}
		else
		{
			float num4 = (float)mTexture.Width * 0.75f;
			Vector2 vector2 = new Vector2((1920f - num - num4 - 64f) / 2f, 730f);
			ActiveFont.DrawOutline(text, vector2 + new Vector2(num / 2f, 0f), new Vector2(0.5f, 0.5f), Vector2.One * 0.75f, Color.LightGray, 2f, Color.Black);
			vector2.X += num + 64f;
			mTexture.DrawCentered(vector2 + new Vector2(num4 * 0.5f, 0f), Color.White, 0.75f);
		}
	}
}
