using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class Skybox
{
	public VertexPositionColorTexture[] Verts;

	public VirtualTexture Texture;

	public Skybox(VirtualTexture texture, float size = 25f)
	{
		Texture = texture;
		Verts = new VertexPositionColorTexture[30];
		Vector3 vector = new Vector3(0f - size, size, 0f - size);
		Vector3 vector2 = new Vector3(size, size, 0f - size);
		Vector3 vector3 = new Vector3(size, size, size);
		Vector3 vector4 = new Vector3(0f - size, size, size);
		Vector3 vector5 = new Vector3(0f - size, 0f - size, 0f - size);
		Vector3 vector6 = new Vector3(size, 0f - size, 0f - size);
		Vector3 vector7 = new Vector3(size, 0f - size, size);
		Vector3 vector8 = new Vector3(0f - size, 0f - size, size);
		MTexture mTexture = new MTexture(texture);
		MTexture subtexture = mTexture.GetSubtexture(0, 0, 820, 820);
		MTexture subtexture2 = mTexture.GetSubtexture(820, 0, 820, 820);
		MTexture subtexture3 = mTexture.GetSubtexture(2460, 0, 820, 820);
		MTexture subtexture4 = mTexture.GetSubtexture(1640, 0, 820, 820);
		MTexture subtexture5 = mTexture.GetSubtexture(3280, 0, 819, 820);
		AddFace(Verts, 0, subtexture, vector, vector2, vector3, vector4);
		AddFace(Verts, 1, subtexture3, vector2, vector, vector5, vector6);
		AddFace(Verts, 2, subtexture2, vector4, vector3, vector7, vector8);
		AddFace(Verts, 3, subtexture5, vector3, vector2, vector6, vector7);
		AddFace(Verts, 4, subtexture4, vector, vector4, vector8, vector5);
	}

	private void AddFace(VertexPositionColorTexture[] verts, int face, MTexture tex, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		float x = (float)(tex.ClipRect.Left + 1) / (float)tex.Texture.Width;
		float y = (float)(tex.ClipRect.Top + 1) / (float)tex.Texture.Height;
		float x2 = (float)(tex.ClipRect.Right - 1) / (float)tex.Texture.Width;
		float y2 = (float)(tex.ClipRect.Bottom - 1) / (float)tex.Texture.Height;
		int num = face * 6;
		verts[num++] = new VertexPositionColorTexture(a, Color.White, new Vector2(x, y));
		verts[num++] = new VertexPositionColorTexture(b, Color.White, new Vector2(x2, y));
		verts[num++] = new VertexPositionColorTexture(c, Color.White, new Vector2(x2, y2));
		verts[num++] = new VertexPositionColorTexture(a, Color.White, new Vector2(x, y));
		verts[num++] = new VertexPositionColorTexture(c, Color.White, new Vector2(x2, y2));
		verts[num++] = new VertexPositionColorTexture(d, Color.White, new Vector2(x, y2));
	}

	public void Draw(Matrix matrix, Color color)
	{
		Engine.Graphics.GraphicsDevice.RasterizerState = MountainModel.CullNoneRasterizer;
		Engine.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
		Engine.Graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
		Engine.Graphics.GraphicsDevice.Textures[0] = Texture.Texture;
		for (int i = 0; i < Verts.Length; i++)
		{
			Verts[i].Color = color;
		}
		GFX.FxTexture.Parameters["World"].SetValue(matrix);
		foreach (EffectPass pass in GFX.FxTexture.CurrentTechnique.Passes)
		{
			pass.Apply();
			Engine.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, Verts, 0, Verts.Length / 3);
		}
	}
}
