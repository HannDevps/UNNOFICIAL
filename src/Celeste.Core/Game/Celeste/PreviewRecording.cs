using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monocle;

namespace Celeste;

public class PreviewRecording : Scene
{
	public string Filename;

	public List<Player.ChaserState> Timeline;

	public PlayerPlayback entity;

	public float ScreenWidth = 640f;

	public float ScreenHeight = 360f;

	public float Width;

	public float Height;

	private Matrix Matrix => Matrix.CreateScale(1920f / ScreenWidth) * Engine.ScreenMatrix;

	public PreviewRecording(string filename)
	{
		Filename = filename;
		byte[] buffer = File.ReadAllBytes(filename);
		Timeline = PlaybackData.Import(buffer);
		float num = float.MaxValue;
		float num2 = float.MinValue;
		float num3 = float.MinValue;
		float num4 = float.MaxValue;
		foreach (Player.ChaserState item in Timeline)
		{
			num = Math.Min(item.Position.X, num);
			num2 = Math.Max(item.Position.X, num2);
			num4 = Math.Min(item.Position.Y, num4);
			num3 = Math.Max(item.Position.Y, num3);
		}
		Width = (int)(num2 - num);
		Height = (int)(num3 - num4);
		Add(entity = new PlayerPlayback(new Vector2((ScreenWidth - Width) / 2f - num, (ScreenHeight - Height) / 2f - num4), PlayerSpriteMode.Madeline, Timeline));
	}

	public override void Update()
	{
		if (MInput.Keyboard.Check(Keys.A))
		{
			entity.TrimStart = Math.Max(0f, entity.TrimStart -= Engine.DeltaTime);
		}
		if (MInput.Keyboard.Check(Keys.D))
		{
			entity.TrimStart = Math.Min(entity.Duration, entity.TrimStart += Engine.DeltaTime);
		}
		if (MInput.Keyboard.Check(Keys.Left))
		{
			entity.TrimEnd = Math.Max(0f, entity.TrimEnd -= Engine.DeltaTime);
		}
		if (MInput.Keyboard.Check(Keys.Right))
		{
			entity.TrimEnd = Math.Min(entity.Duration, entity.TrimEnd += Engine.DeltaTime);
		}
		if (MInput.Keyboard.Check(Keys.LeftControl) && MInput.Keyboard.Pressed(Keys.S))
		{
			while (Timeline[0].TimeStamp < entity.TrimStart)
			{
				Timeline.RemoveAt(0);
			}
			while (Timeline[Timeline.Count - 1].TimeStamp > entity.TrimEnd)
			{
				Timeline.RemoveAt(Timeline.Count - 1);
			}
			PlaybackData.Export(Timeline, Filename);
			Engine.Scene = new PreviewRecording(Filename);
		}
		base.Update();
		entity.Hair.AfterUpdate();
	}

	public override void Render()
	{
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, RasterizerState.CullNone, null, Engine.ScreenMatrix);
		ActiveFont.Draw("A/D:        Move Start Trim", new Vector2(8f, 8f), new Vector2(0f, 0f), Vector2.One * 0.5f, Color.White);
		ActiveFont.Draw("Left/Right: Move End Trim", new Vector2(8f, 32f), new Vector2(0f, 0f), Vector2.One * 0.5f, Color.White);
		ActiveFont.Draw("CTRL+S: Save New Trim", new Vector2(8f, 56f), new Vector2(0f, 0f), Vector2.One * 0.5f, Color.White);
		Draw.SpriteBatch.End();
		Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, RasterizerState.CullNone, null, Matrix);
		Draw.HollowRect((ScreenWidth - Width) / 2f - 16f, (ScreenHeight - Height) / 2f - 16f, Width + 32f, Height + 32f, Color.Red * 0.6f);
		Draw.HollowRect((ScreenWidth - 320f) / 2f, (ScreenHeight - 180f) / 2f, 320f, 180f, Color.White * 0.6f);
		if (entity.Visible)
		{
			entity.Render();
		}
		Draw.Rect(32f, ScreenHeight - 48f, ScreenWidth - 64f, 16f, Color.DarkGray);
		Draw.Rect(32f, ScreenHeight - 48f, (ScreenWidth - 64f) * (entity.Time / entity.Duration), 16f, Color.White);
		Draw.Rect(32f + (ScreenWidth - 64f) * (entity.Time / entity.Duration) - 2f, ScreenHeight - 48f, 4f, 16f, Color.LimeGreen);
		Draw.Rect(32f + (ScreenWidth - 64f) * (entity.TrimStart / entity.Duration) - 2f, ScreenHeight - 48f, 4f, 16f, Color.Red);
		Draw.Rect(32f + (ScreenWidth - 64f) * (entity.TrimEnd / entity.Duration) - 2f, ScreenHeight - 48f, 4f, 16f, Color.Red);
		Draw.SpriteBatch.End();
	}
}
