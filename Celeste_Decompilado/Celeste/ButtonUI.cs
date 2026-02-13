using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public static class ButtonUI
{
	public static float Width(string label, VirtualButton button)
	{
		MTexture mTexture = Input.GuiButton(button);
		return ActiveFont.Measure(label).X + 8f + (float)mTexture.Width;
	}

	public static void Render(Vector2 position, string label, VirtualButton button, float scale, float justifyX = 0.5f, float wiggle = 0f, float alpha = 1f)
	{
		MTexture mTexture = Input.GuiButton(button);
		float num = Width(label, button);
		position.X -= scale * num * (justifyX - 0.5f);
		mTexture.Draw(position, new Vector2((float)mTexture.Width - num / 2f, (float)mTexture.Height / 2f), Color.White * alpha, scale + wiggle);
		DrawText(label, position, num / 2f, scale + wiggle, alpha);
	}

	private static void DrawText(string text, Vector2 position, float justify, float scale, float alpha)
	{
		float x = ActiveFont.Measure(text).X;
		ActiveFont.DrawOutline(text, position, new Vector2(justify / x, 0.5f), Vector2.One * scale, Color.White * alpha, 2f, Color.Black * alpha);
	}
}
