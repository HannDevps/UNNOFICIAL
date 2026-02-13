using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class BridgeFixed : Solid
{
	public BridgeFixed(EntityData data, Vector2 offset)
		: base(data.Position + offset, data.Width, 8f, safe: true)
	{
		MTexture mTexture = GFX.Game["scenery/bridge_fixed"];
		for (int i = 0; (float)i < base.Width; i += mTexture.Width)
		{
			Rectangle rectangle = new Rectangle(0, 0, mTexture.Width, mTexture.Height);
			if ((float)(i + rectangle.Width) > base.Width)
			{
				rectangle.Width = (int)base.Width - i;
			}
			Add(new Image(mTexture)
			{
				Position = new Vector2(i, -8f)
			});
		}
	}
}
