using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Celeste;

public class CustomSpriteEffect : Effect
{
	private EffectParameter matrixParam;

	public CustomSpriteEffect(Effect effect)
		: base(effect)
	{
		matrixParam = base.Parameters["MatrixTransform"];
	}

	protected override void OnApply()
	{
		Viewport viewport = base.GraphicsDevice.Viewport;
		Matrix matrix = Matrix.CreateOrthographicOffCenter(0f, viewport.Width, viewport.Height, 0f, 0f, 1f);
		Matrix matrix2 = Matrix.CreateTranslation(-0.5f, -0.5f, 0f);
		matrixParam.SetValue(matrix2 * matrix);
		base.OnApply();
	}
}
