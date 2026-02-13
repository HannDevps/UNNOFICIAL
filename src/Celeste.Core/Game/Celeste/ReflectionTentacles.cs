using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class ReflectionTentacles : Entity
{
	private struct Tentacle
	{
		public Vector2 Position;

		public float Width;

		public float Length;

		public float Approach;

		public float WaveOffset;

		public int TexIndex;

		public int FillerTexIndex;

		public Vector2 LerpPositionFrom;

		public float LerpPercent;

		public float LerpDuration;
	}

	public int Index;

	public List<Vector2> Nodes = new List<Vector2>();

	private Vector2 outwards;

	private Vector2 lastOutwards;

	private float ease;

	private Vector2 p;

	private Player player;

	private float fearDistance;

	private float offset;

	private bool createdFromLevel;

	private int slideUntilIndex;

	private int layer;

	private const int NodesPerTentacle = 10;

	private Tentacle[] tentacles;

	private int tentacleCount;

	private VertexPositionColorTexture[] vertices;

	private int vertexCount;

	private Color color = Color.Purple;

	private float soundDelay = 0.25f;

	private List<MTexture[]> arms = new List<MTexture[]>();

	private List<MTexture> fillers;

	public ReflectionTentacles()
	{
	}

	public ReflectionTentacles(EntityData data, Vector2 offset)
		: base(data.Position + offset)
	{
		Nodes.Add(Position);
		Vector2[] nodes = data.Nodes;
		foreach (Vector2 vector in nodes)
		{
			Nodes.Add(offset + vector);
		}
		switch (data.Attr("fear_distance"))
		{
		case "close":
			fearDistance = 16f;
			break;
		case "medium":
			fearDistance = 40f;
			break;
		case "far":
			fearDistance = 80f;
			break;
		}
		int num = data.Int("slide_until");
		Create(fearDistance, num, 0, Nodes);
		createdFromLevel = true;
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		if (createdFromLevel)
		{
			for (int i = 1; i < 4; i++)
			{
				ReflectionTentacles reflectionTentacles = new ReflectionTentacles();
				reflectionTentacles.Create(fearDistance, slideUntilIndex, i, Nodes);
				scene.Add(reflectionTentacles);
			}
		}
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		Player entity = base.Scene.Tracker.GetEntity<Player>();
		bool flag = false;
		while (entity != null && Index < Nodes.Count - 1)
		{
			Vector2 vector = (p = Calc.ClosestPointOnLine(Nodes[Index], Nodes[Index] + outwards * 10000f, entity.Center));
			if (!((Nodes[Index] - vector).Length() < fearDistance))
			{
				break;
			}
			flag = true;
			Retreat();
		}
		if (flag)
		{
			ease = 1f;
			SnapTentacles();
		}
	}

	public void Create(float fearDistance, int slideUntilIndex, int layer, List<Vector2> startNodes)
	{
		Nodes = new List<Vector2>();
		foreach (Vector2 startNode in startNodes)
		{
			Nodes.Add(startNode + new Vector2(Calc.Random.Range(-8, 8), Calc.Random.Range(-8, 8)));
		}
		base.Tag = Tags.TransitionUpdate;
		Position = Nodes[0];
		outwards = (Nodes[0] - Nodes[1]).SafeNormalize();
		this.fearDistance = fearDistance;
		this.slideUntilIndex = slideUntilIndex;
		this.layer = layer;
		switch (layer)
		{
		case 0:
			base.Depth = -1000000;
			color = Calc.HexToColor("3f2a4f");
			offset = 110f;
			break;
		case 1:
			base.Depth = 8990;
			color = Calc.HexToColor("7b3555");
			offset = 80f;
			break;
		case 2:
			base.Depth = 10010;
			color = Calc.HexToColor("662847");
			offset = 50f;
			break;
		case 3:
			base.Depth = 10011;
			color = Calc.HexToColor("492632");
			offset = 20f;
			break;
		}
		foreach (MTexture atlasSubtexture in GFX.Game.GetAtlasSubtextures("scenery/tentacles/arms"))
		{
			MTexture[] array = new MTexture[10];
			int num = atlasSubtexture.Width / 10;
			for (int i = 0; i < 10; i++)
			{
				array[i] = atlasSubtexture.GetSubtexture(num * (10 - i - 1), 0, num, atlasSubtexture.Height);
			}
			arms.Add(array);
		}
		fillers = GFX.Game.GetAtlasSubtextures("scenery/tentacles/filler");
		tentacles = new Tentacle[100];
		float num2 = 0f;
		int num3 = 0;
		while (num3 < tentacles.Length && num2 < 440f)
		{
			tentacles[num3].Approach = 0.25f + Calc.Random.NextFloat() * 0.75f;
			tentacles[num3].Length = 32f + Calc.Random.NextFloat(64f);
			tentacles[num3].Width = 4f + Calc.Random.NextFloat(16f);
			tentacles[num3].Position = TargetTentaclePosition(tentacles[num3], Nodes[0], num2);
			tentacles[num3].WaveOffset = Calc.Random.NextFloat();
			tentacles[num3].TexIndex = Calc.Random.Next(arms.Count);
			tentacles[num3].FillerTexIndex = Calc.Random.Next(fillers.Count);
			tentacles[num3].LerpDuration = 0.5f + Calc.Random.NextFloat(0.25f);
			num2 += tentacles[num3].Width;
			num3++;
			tentacleCount++;
		}
		vertices = new VertexPositionColorTexture[tentacleCount * 12 * 6];
		for (int j = 0; j < vertices.Length; j++)
		{
			vertices[j].Color = color;
		}
	}

	private Vector2 TargetTentaclePosition(Tentacle tentacle, Vector2 position, float along)
	{
		Vector2 vector2;
		Vector2 vector = (vector2 = position - outwards * offset);
		if (player != null)
		{
			Vector2 vector3 = outwards.Perpendicular();
			vector2 = Calc.ClosestPointOnLine(vector2 - vector3 * 200f, vector2 + vector3 * 200f, player.Position);
		}
		Vector2 vector4 = vector + outwards.Perpendicular() * (-220f + along + tentacle.Width * 0.5f);
		float num = (vector2 - vector4).Length();
		return vector4 + outwards * num * 0.6f;
	}

	public void Retreat()
	{
		if (Index < Nodes.Count - 1)
		{
			lastOutwards = outwards;
			ease = 0f;
			Index++;
			if (layer == 0 && soundDelay <= 0f)
			{
				Audio.Play(((Nodes[Index - 1] - Nodes[Index]).Length() > 180f) ? "event:/game/06_reflection/scaryhair_whoosh" : "event:/game/06_reflection/scaryhair_move");
			}
			for (int i = 0; i < tentacleCount; i++)
			{
				tentacles[i].LerpPercent = 0f;
				tentacles[i].LerpPositionFrom = tentacles[i].Position;
			}
		}
	}

	public override void Update()
	{
		soundDelay -= Engine.DeltaTime;
		if (slideUntilIndex > Index)
		{
			player = base.Scene.Tracker.GetEntity<Player>();
			if (player != null)
			{
				Vector2 vector = (p = Calc.ClosestPointOnLine(Nodes[Index] - outwards * 10000f, Nodes[Index] + outwards * 10000f, player.Center));
				if ((vector - Nodes[Index]).Length() < 32f)
				{
					Retreat();
					outwards = (Nodes[Index - 1] - Nodes[Index]).SafeNormalize();
				}
				else
				{
					MoveTentacles(vector - outwards * 190f);
				}
			}
		}
		else
		{
			FinalBoss entity = base.Scene.Tracker.GetEntity<FinalBoss>();
			player = base.Scene.Tracker.GetEntity<Player>();
			if (entity == null && player != null && Index < Nodes.Count - 1)
			{
				Vector2 vector2 = (p = Calc.ClosestPointOnLine(Nodes[Index], Nodes[Index] + outwards * 10000f, player.Center));
				if ((Nodes[Index] - vector2).Length() < fearDistance)
				{
					Retreat();
				}
			}
			if (Index > 0)
			{
				ease = Calc.Approach(ease, 1f, (float)((Index != Nodes.Count - 1) ? 1 : 2) * Engine.DeltaTime);
				outwards = Calc.AngleToVector(Calc.AngleLerp(lastOutwards.Angle(), (Nodes[Index - 1] - Nodes[Index]).Angle(), Ease.QuadOut(ease)), 1f);
				float num = 0f;
				for (int i = 0; i < tentacleCount; i++)
				{
					Vector2 vector3 = TargetTentaclePosition(tentacles[i], Nodes[Index], num);
					if (tentacles[i].LerpPercent < 1f)
					{
						tentacles[i].LerpPercent += Engine.DeltaTime / tentacles[i].LerpDuration;
						tentacles[i].Position = Vector2.Lerp(tentacles[i].LerpPositionFrom, vector3, Ease.CubeInOut(tentacles[i].LerpPercent));
					}
					else
					{
						tentacles[i].Position += (vector3 - tentacles[i].Position) * (1f - (float)Math.Pow(0.1f * tentacles[i].Approach, Engine.DeltaTime));
					}
					num += tentacles[i].Width;
				}
			}
			else
			{
				MoveTentacles(Nodes[Index]);
			}
		}
		if (Index == Nodes.Count - 1)
		{
			Color color = this.color * (1f - ease);
			for (int j = 0; j < vertices.Length; j++)
			{
				vertices[j].Color = color;
			}
		}
		UpdateVertices();
	}

	private void MoveTentacles(Vector2 pos)
	{
		float num = 0f;
		for (int i = 0; i < tentacleCount; i++)
		{
			Vector2 vector = TargetTentaclePosition(tentacles[i], pos, num);
			tentacles[i].Position += (vector - tentacles[i].Position) * (1f - (float)Math.Pow(0.1f * tentacles[i].Approach, Engine.DeltaTime));
			num += tentacles[i].Width;
		}
	}

	public void SnapTentacles()
	{
		float num = 0f;
		for (int i = 0; i < tentacleCount; i++)
		{
			tentacles[i].LerpPercent = 1f;
			tentacles[i].Position = TargetTentaclePosition(tentacles[i], Nodes[Index], num);
			num += tentacles[i].Width;
		}
	}

	private void UpdateVertices()
	{
		Vector2 vector = -outwards.Perpendicular();
		int n = 0;
		for (int i = 0; i < tentacleCount; i++)
		{
			Vector2 position = tentacles[i].Position;
			Vector2 vector2 = vector * (tentacles[i].Width * 0.5f + 2f);
			MTexture[] array = arms[tentacles[i].TexIndex];
			Quad(ref n, position + vector2, position + vector2 * 1.5f - outwards * 240f, position - vector2 * 1.5f - outwards * 240f, position - vector2, fillers[tentacles[i].FillerTexIndex]);
			Vector2 vector3 = position;
			Vector2 vector4 = vector2;
			float num = tentacles[i].Length / 10f;
			num += Calc.YoYo(tentacles[i].LerpPercent) * 6f;
			for (int j = 1; j <= 10; j++)
			{
				float num2 = (float)j / 10f;
				float num3 = base.Scene.TimeActive * tentacles[i].WaveOffset * (float)Math.Pow(1.100000023841858, j) * 2f;
				float num4 = tentacles[i].WaveOffset * 3f + (float)j * 0.05f;
				float num5 = 2f + 4f * num2;
				Vector2 vector5 = vector * (float)Math.Sin(num3 + num4) * num5;
				Vector2 vector6 = vector3 + outwards * num + vector5;
				Vector2 vector7 = vector2 * (1f - num2);
				Quad(ref n, vector6 - vector7, vector3 - vector4, vector3 + vector4, vector6 + vector7, array[j - 1]);
				vector3 = vector6;
				vector4 = vector7;
			}
		}
		vertexCount = n;
	}

	private void Quad(ref int n, Vector2 a, Vector2 b, Vector2 c, Vector2 d, MTexture subtexture = null)
	{
		if (subtexture == null)
		{
			subtexture = GFX.Game["util/pixel"];
		}
		float num = 1f / (float)subtexture.Texture.Texture.Width;
		float num2 = 1f / (float)subtexture.Texture.Texture.Height;
		Vector2 textureCoordinate = new Vector2((float)subtexture.ClipRect.Left * num, (float)subtexture.ClipRect.Top * num2);
		Vector2 textureCoordinate2 = new Vector2((float)subtexture.ClipRect.Right * num, (float)subtexture.ClipRect.Top * num2);
		Vector2 textureCoordinate3 = new Vector2((float)subtexture.ClipRect.Left * num, (float)subtexture.ClipRect.Bottom * num2);
		Vector2 textureCoordinate4 = new Vector2((float)subtexture.ClipRect.Right * num, (float)subtexture.ClipRect.Bottom * num2);
		vertices[n].Position = new Vector3(a, 0f);
		vertices[n++].TextureCoordinate = textureCoordinate;
		vertices[n].Position = new Vector3(b, 0f);
		vertices[n++].TextureCoordinate = textureCoordinate2;
		vertices[n].Position = new Vector3(d, 0f);
		vertices[n++].TextureCoordinate = textureCoordinate3;
		vertices[n].Position = new Vector3(d, 0f);
		vertices[n++].TextureCoordinate = textureCoordinate3;
		vertices[n].Position = new Vector3(b, 0f);
		vertices[n++].TextureCoordinate = textureCoordinate2;
		vertices[n].Position = new Vector3(c, 0f);
		vertices[n++].TextureCoordinate = textureCoordinate4;
	}

	public override void Render()
	{
		if (vertexCount > 0)
		{
			GameplayRenderer.End();
			Engine.Graphics.GraphicsDevice.Textures[0] = arms[0][0].Texture.Texture;
			GFX.DrawVertices((base.Scene as Level).Camera.Matrix, vertices, vertexCount, GFX.FxTexture);
			GameplayRenderer.Begin();
		}
	}
}
