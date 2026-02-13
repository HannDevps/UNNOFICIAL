using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

[Tracked(false)]
public class LightningRenderer : Entity
{
	private class Bolt
	{
		[CompilerGenerated]
		private sealed class _003CRun_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Bolt _003C_003E4__this;

			private int _003Ci_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CRun_003Ed__14(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int num = _003C_003E1__state;
				Bolt bolt = _003C_003E4__this;
				List<Vector2> list;
				List<Vector2> list2;
				Vector2 item3;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E2__current = Calc.Random.Range(0f, 4f);
					_003C_003E1__state = 1;
					return true;
				case 1:
					_003C_003E1__state = -1;
					goto IL_0064;
				case 2:
					_003C_003E1__state = -1;
					_003Ci_003E5__2++;
					goto IL_02fc;
				case 3:
				{
					_003C_003E1__state = -1;
					float num2 = (float)_003Ci_003E5__2 / 5f;
					bolt.visible = true;
					bolt.size = (1f - num2) * 5f;
					bolt.gap = num2;
					bolt.alpha = 1f - num2;
					bolt.visible = true;
					bolt.seed = (uint)Calc.Random.Next();
					_003C_003E2__current = 0.025f;
					_003C_003E1__state = 4;
					return true;
				}
				case 4:
					_003C_003E1__state = -1;
					_003Ci_003E5__2++;
					break;
				case 5:
					{
						_003C_003E1__state = -1;
						goto IL_0064;
					}
					IL_0064:
					list = new List<Vector2>();
					for (int i = 0; i < 3; i++)
					{
						Vector2 item = Calc.Random.Choose(new Vector2(0f, Calc.Random.Range(8, bolt.height - 16)), new Vector2(Calc.Random.Range(8, bolt.width - 16), 0f), new Vector2(bolt.width, Calc.Random.Range(8, bolt.height - 16)), new Vector2(Calc.Random.Range(8, bolt.width - 16), bolt.height));
						Vector2 item2 = ((item.X <= 0f || item.X >= (float)bolt.width) ? new Vector2((float)bolt.width - item.X, item.Y) : new Vector2(item.X, (float)bolt.height - item.Y));
						list.Add(item);
						list.Add(item2);
					}
					list2 = new List<Vector2>();
					for (int j = 0; j < 3; j++)
					{
						list2.Add(new Vector2(Calc.Random.Range(0.25f, 0.75f) * (float)bolt.width, Calc.Random.Range(0.25f, 0.75f) * (float)bolt.height));
					}
					bolt.nodes.Clear();
					foreach (Vector2 item4 in list)
					{
						bolt.nodes.Add(item4);
						bolt.nodes.Add(list2.ClosestTo(item4));
					}
					item3 = list2[list2.Count - 1];
					foreach (Vector2 item5 in list2)
					{
						bolt.nodes.Add(item3);
						bolt.nodes.Add(item5);
						item3 = item5;
					}
					bolt.flash = 1f;
					bolt.visible = true;
					bolt.size = 5f;
					bolt.gap = 0f;
					bolt.alpha = 1f;
					_003Ci_003E5__2 = 0;
					goto IL_02fc;
					IL_02fc:
					if (_003Ci_003E5__2 < 4)
					{
						bolt.seed = (uint)Calc.Random.Next();
						_003C_003E2__current = 0.1f;
						_003C_003E1__state = 2;
						return true;
					}
					_003Ci_003E5__2 = 0;
					break;
				}
				if (_003Ci_003E5__2 < 5)
				{
					if (!Settings.Instance.DisableFlashes)
					{
						bolt.visible = false;
					}
					_003C_003E2__current = 0.05f + (float)_003Ci_003E5__2 * 0.02f;
					_003C_003E1__state = 3;
					return true;
				}
				bolt.visible = false;
				_003C_003E2__current = Calc.Random.Range(4f, 8f);
				_003C_003E1__state = 5;
				return true;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		private List<Vector2> nodes = new List<Vector2>();

		private Coroutine routine;

		private bool visible;

		private float size;

		private float gap;

		private float alpha;

		private uint seed;

		private float flash;

		private readonly Color color;

		private readonly float scale;

		private readonly int width;

		private readonly int height;

		public Bolt(Color color, float scale, int width, int height)
		{
			this.color = color;
			this.width = width;
			this.height = height;
			this.scale = scale;
			routine = new Coroutine(Run());
		}

		public void Update(Scene scene)
		{
			routine.Update();
			flash = Calc.Approach(flash, 0f, Engine.DeltaTime * 2f);
		}

		[IteratorStateMachine(typeof(_003CRun_003Ed__14))]
		private IEnumerator Run()
		{
			yield return Calc.Random.Range(0f, 4f);
			while (true)
			{
				List<Vector2> list = new List<Vector2>();
				for (int i = 0; i < 3; i++)
				{
					Vector2 item = Calc.Random.Choose(new Vector2(0f, Calc.Random.Range(8, height - 16)), new Vector2(Calc.Random.Range(8, width - 16), 0f), new Vector2(width, Calc.Random.Range(8, height - 16)), new Vector2(Calc.Random.Range(8, width - 16), height));
					Vector2 item2 = ((item.X <= 0f || item.X >= (float)width) ? new Vector2((float)width - item.X, item.Y) : new Vector2(item.X, (float)height - item.Y));
					list.Add(item);
					list.Add(item2);
				}
				List<Vector2> list2 = new List<Vector2>();
				for (int j = 0; j < 3; j++)
				{
					list2.Add(new Vector2(Calc.Random.Range(0.25f, 0.75f) * (float)width, Calc.Random.Range(0.25f, 0.75f) * (float)height));
				}
				nodes.Clear();
				foreach (Vector2 item4 in list)
				{
					nodes.Add(item4);
					nodes.Add(list2.ClosestTo(item4));
				}
				Vector2 item3 = list2[list2.Count - 1];
				foreach (Vector2 item5 in list2)
				{
					nodes.Add(item3);
					nodes.Add(item5);
					item3 = item5;
				}
				flash = 1f;
				visible = true;
				size = 5f;
				gap = 0f;
				alpha = 1f;
				for (int k = 0; k < 4; k++)
				{
					seed = (uint)Calc.Random.Next();
					yield return 0.1f;
				}
				for (int k = 0; k < 5; k++)
				{
					if (!Settings.Instance.DisableFlashes)
					{
						visible = false;
					}
					yield return 0.05f + (float)k * 0.02f;
					float num = (float)k / 5f;
					visible = true;
					size = (1f - num) * 5f;
					gap = num;
					alpha = 1f - num;
					visible = true;
					seed = (uint)Calc.Random.Next();
					yield return 0.025f;
				}
				visible = false;
				yield return Calc.Random.Range(4f, 8f);
			}
		}

		public void Render()
		{
			if (flash > 0f && !Settings.Instance.DisableFlashes)
			{
				Draw.Rect(0f, 0f, width, height, Color.White * flash * 0.15f * scale);
			}
			if (visible)
			{
				for (int i = 0; i < nodes.Count; i += 2)
				{
					DrawFatLightning(seed, nodes[i], nodes[i + 1], size * scale, gap, color * alpha);
				}
			}
		}
	}

	private class Edge
	{
		public Lightning Parent;

		public bool Visible;

		public Vector2 A;

		public Vector2 B;

		public Vector2 Min;

		public Vector2 Max;

		public Edge(Lightning parent, Vector2 a, Vector2 b)
		{
			Parent = parent;
			Visible = true;
			A = a;
			B = b;
			Min = new Vector2(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
			Max = new Vector2(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
		}

		public bool InView(ref Rectangle view)
		{
			if ((float)view.Left < Parent.X + Max.X && (float)view.Right > Parent.X + Min.X && (float)view.Top < Parent.Y + Max.Y)
			{
				return (float)view.Bottom > Parent.Y + Min.Y;
			}
			return false;
		}
	}

	private List<Lightning> list = new List<Lightning>();

	private List<Edge> edges = new List<Edge>();

	private List<Bolt> bolts = new List<Bolt>();

	private VertexPositionColor[] edgeVerts;

	private VirtualMap<bool> tiles;

	private Rectangle levelTileBounds;

	private uint edgeSeed;

	private uint leapSeed;

	private bool dirty;

	private Color[] electricityColors = new Color[2]
	{
		Calc.HexToColor("fcf579"),
		Calc.HexToColor("8cf7e2")
	};

	private Color[] electricityColorsLerped;

	public float Fade;

	public bool UpdateSeeds = true;

	public const int BoltBufferSize = 160;

	public bool DrawEdges = true;

	public SoundSource AmbientSfx;

	public LightningRenderer()
	{
		base.Tag = (int)Tags.Global | (int)Tags.TransitionUpdate;
		base.Depth = -1000100;
		electricityColorsLerped = new Color[electricityColors.Length];
		Add(new CustomBloom(OnRenderBloom));
		Add(new BeforeRenderHook(OnBeforeRender));
		Add(AmbientSfx = new SoundSource());
		AmbientSfx.DisposeOnTransition = false;
	}

	public override void Awake(Scene scene)
	{
		base.Awake(scene);
		for (int i = 0; i < 4; i++)
		{
			bolts.Add(new Bolt(electricityColors[0], 1f, 160, 160));
			bolts.Add(new Bolt(electricityColors[1], 0.35f, 160, 160));
		}
	}

	public void StartAmbience()
	{
		if (!AmbientSfx.Playing)
		{
			AmbientSfx.Play("event:/new_content/env/10_electricity");
		}
	}

	public void StopAmbience()
	{
		AmbientSfx.Stop();
	}

	public void Reset()
	{
		UpdateSeeds = true;
		Fade = 0f;
	}

	public void Track(Lightning block)
	{
		list.Add(block);
		if (tiles == null)
		{
			levelTileBounds = (base.Scene as Level).TileBounds;
			tiles = new VirtualMap<bool>(levelTileBounds.Width, levelTileBounds.Height, emptyValue: false);
		}
		for (int i = (int)block.X / 8; i < ((int)block.X + block.VisualWidth) / 8; i++)
		{
			for (int j = (int)block.Y / 8; j < ((int)block.Y + block.VisualHeight) / 8; j++)
			{
				tiles[i - levelTileBounds.X, j - levelTileBounds.Y] = true;
			}
		}
		dirty = true;
	}

	public void Untrack(Lightning block)
	{
		list.Remove(block);
		if (list.Count <= 0)
		{
			tiles = null;
		}
		else
		{
			for (int i = (int)block.X / 8; (float)i < block.Right / 8f; i++)
			{
				for (int j = (int)block.Y / 8; (float)j < block.Bottom / 8f; j++)
				{
					tiles[i - levelTileBounds.X, j - levelTileBounds.Y] = false;
				}
			}
		}
		dirty = true;
	}

	public override void Update()
	{
		if (dirty)
		{
			RebuildEdges();
		}
		ToggleEdges();
		if (list.Count <= 0)
		{
			return;
		}
		foreach (Bolt bolt in bolts)
		{
			bolt.Update(base.Scene);
		}
		if (UpdateSeeds)
		{
			if (base.Scene.OnInterval(0.1f))
			{
				edgeSeed = (uint)Calc.Random.Next();
			}
			if (base.Scene.OnInterval(0.7f))
			{
				leapSeed = (uint)Calc.Random.Next();
			}
		}
	}

	public void ToggleEdges(bool immediate = false)
	{
		Camera camera = (base.Scene as Level).Camera;
		Rectangle view = new Rectangle((int)camera.Left - 4, (int)camera.Top - 4, (int)(camera.Right - camera.Left) + 8, (int)(camera.Bottom - camera.Top) + 8);
		for (int i = 0; i < edges.Count; i++)
		{
			if (immediate)
			{
				edges[i].Visible = edges[i].InView(ref view);
			}
			else if (!edges[i].Visible && base.Scene.OnInterval(0.05f, (float)i * 0.01f) && edges[i].InView(ref view))
			{
				edges[i].Visible = true;
			}
			else if (edges[i].Visible && base.Scene.OnInterval(0.25f, (float)i * 0.01f) && !edges[i].InView(ref view))
			{
				edges[i].Visible = false;
			}
		}
	}

	private void RebuildEdges()
	{
		dirty = false;
		edges.Clear();
		if (list.Count <= 0)
		{
			return;
		}
		Level obj = base.Scene as Level;
		_ = obj.TileBounds.Left;
		_ = obj.TileBounds.Top;
		_ = obj.TileBounds.Right;
		_ = obj.TileBounds.Bottom;
		Point[] array = new Point[4]
		{
			new Point(0, -1),
			new Point(0, 1),
			new Point(-1, 0),
			new Point(1, 0)
		};
		foreach (Lightning item in list)
		{
			for (int i = (int)item.X / 8; (float)i < item.Right / 8f; i++)
			{
				for (int j = (int)item.Y / 8; (float)j < item.Bottom / 8f; j++)
				{
					Point[] array2 = array;
					for (int k = 0; k < array2.Length; k++)
					{
						Point point = array2[k];
						Point point2 = new Point(-point.Y, point.X);
						if (Inside(i + point.X, j + point.Y) || (Inside(i - point2.X, j - point2.Y) && !Inside(i + point.X - point2.X, j + point.Y - point2.Y)))
						{
							continue;
						}
						Point point3 = new Point(i, j);
						Point point4 = new Point(i + point2.X, j + point2.Y);
						Vector2 vector = new Vector2(4f) + new Vector2(point.X - point2.X, point.Y - point2.Y) * 4f;
						int num = 1;
						while (Inside(point4.X, point4.Y) && !Inside(point4.X + point.X, point4.Y + point.Y))
						{
							point4.X += point2.X;
							point4.Y += point2.Y;
							num++;
							if (num > 8)
							{
								Vector2 a = new Vector2(point3.X, point3.Y) * 8f + vector - item.Position;
								Vector2 b = new Vector2(point4.X, point4.Y) * 8f + vector - item.Position;
								edges.Add(new Edge(item, a, b));
								num = 0;
								point3 = point4;
							}
						}
						if (num > 0)
						{
							Vector2 a = new Vector2(point3.X, point3.Y) * 8f + vector - item.Position;
							Vector2 b = new Vector2(point4.X, point4.Y) * 8f + vector - item.Position;
							edges.Add(new Edge(item, a, b));
						}
					}
				}
			}
		}
		if (edgeVerts == null)
		{
			edgeVerts = new VertexPositionColor[1024];
		}
	}

	private bool Inside(int tx, int ty)
	{
		return tiles[tx - levelTileBounds.X, ty - levelTileBounds.Y];
	}

	private void OnRenderBloom()
	{
		Camera camera = (base.Scene as Level).Camera;
		new Rectangle((int)camera.Left, (int)camera.Top, (int)(camera.Right - camera.Left), (int)(camera.Bottom - camera.Top));
		Color color = Color.White * (0.25f + Fade * 0.75f);
		foreach (Edge edge in edges)
		{
			if (edge.Visible)
			{
				Draw.Line(edge.Parent.Position + edge.A, edge.Parent.Position + edge.B, color, 4f);
			}
		}
		foreach (Lightning item in list)
		{
			if (item.Visible)
			{
				Draw.Rect(item.X, item.Y, item.VisualWidth, item.VisualHeight, color);
			}
		}
		if (Fade > 0f)
		{
			Level level = base.Scene as Level;
			Draw.Rect(level.Camera.X, level.Camera.Y, 320f, 180f, Color.White * Fade);
		}
	}

	private void OnBeforeRender()
	{
		if (list.Count <= 0)
		{
			return;
		}
		Engine.Graphics.GraphicsDevice.SetRenderTarget(GameplayBuffers.Lightning);
		Engine.Graphics.GraphicsDevice.Clear(Color.Lerp(Calc.HexToColor("f7b262") * 0.1f, Color.White, Fade));
		Draw.SpriteBatch.Begin();
		foreach (Bolt bolt in bolts)
		{
			bolt.Render();
		}
		Draw.SpriteBatch.End();
	}

	public override void Render()
	{
		if (list.Count <= 0)
		{
			return;
		}
		Camera camera = (base.Scene as Level).Camera;
		new Rectangle((int)camera.Left, (int)camera.Top, (int)(camera.Right - camera.Left), (int)(camera.Bottom - camera.Top));
		foreach (Lightning item in list)
		{
			if (item.Visible)
			{
				Draw.SpriteBatch.Draw((RenderTarget2D)GameplayBuffers.Lightning, item.Position, new Rectangle((int)item.X, (int)item.Y, item.VisualWidth, item.VisualHeight), Color.White);
			}
		}
		if (edges.Count <= 0 || !DrawEdges)
		{
			return;
		}
		for (int i = 0; i < electricityColorsLerped.Length; i++)
		{
			electricityColorsLerped[i] = Color.Lerp(electricityColors[i], Color.White, Fade);
		}
		int index = 0;
		uint seed = leapSeed;
		foreach (Edge edge in edges)
		{
			if (edge.Visible)
			{
				DrawSimpleLightning(ref index, ref edgeVerts, edgeSeed, edge.Parent.Position, edge.A, edge.B, electricityColorsLerped[0], 1f + Fade * 3f);
				DrawSimpleLightning(ref index, ref edgeVerts, edgeSeed + 1, edge.Parent.Position, edge.A, edge.B, electricityColorsLerped[1], 1f + Fade * 3f);
				if (PseudoRand(ref seed) % 30 == 0)
				{
					DrawBezierLightning(ref index, ref edgeVerts, edgeSeed, edge.Parent.Position, edge.A, edge.B, 24f, 10, electricityColorsLerped[1]);
				}
			}
		}
		if (index > 0)
		{
			GameplayRenderer.End();
			GFX.DrawVertices(camera.Matrix, edgeVerts, index);
			GameplayRenderer.Begin();
		}
	}

	private static void DrawSimpleLightning(ref int index, ref VertexPositionColor[] verts, uint seed, Vector2 pos, Vector2 a, Vector2 b, Color color, float thickness = 1f)
	{
		seed += (uint)(a.GetHashCode() + b.GetHashCode());
		a += pos;
		b += pos;
		float num = (b - a).Length();
		Vector2 vector = (b - a) / num;
		Vector2 vector2 = vector.TurnRight();
		a += vector2;
		b += vector2;
		Vector2 vector3 = a;
		int num2 = ((PseudoRand(ref seed) % 2 != 0) ? 1 : (-1));
		float num3 = PseudoRandRange(ref seed, 0f, (float)Math.PI * 2f);
		float num4 = 0f;
		float num5 = (float)index + ((b - a).Length() / 4f + 1f) * 6f;
		while (num5 >= (float)verts.Length)
		{
			Array.Resize(ref verts, verts.Length * 2);
		}
		for (int i = index; (float)i < num5; i++)
		{
			verts[i].Color = color;
		}
		do
		{
			float num6 = PseudoRandRange(ref seed, 0f, 4f);
			num3 += 0.1f;
			num4 += 4f + num6;
			Vector2 vector4 = a + vector * num4;
			if (num4 < num)
			{
				vector4 += num2 * vector2 * num6 - vector2;
			}
			else
			{
				vector4 = b;
			}
			verts[index++].Position = new Vector3(vector3 - vector2 * thickness, 0f);
			verts[index++].Position = new Vector3(vector4 - vector2 * thickness, 0f);
			verts[index++].Position = new Vector3(vector4 + vector2 * thickness, 0f);
			verts[index++].Position = new Vector3(vector3 - vector2 * thickness, 0f);
			verts[index++].Position = new Vector3(vector4 + vector2 * thickness, 0f);
			verts[index++].Position = new Vector3(vector3, 0f);
			vector3 = vector4;
			num2 = -num2;
		}
		while (num4 < num);
	}

	private static void DrawBezierLightning(ref int index, ref VertexPositionColor[] verts, uint seed, Vector2 pos, Vector2 a, Vector2 b, float anchor, int steps, Color color)
	{
		seed += (uint)(a.GetHashCode() + b.GetHashCode());
		a += pos;
		b += pos;
		Vector2 vector = (b - a).SafeNormalize().TurnRight();
		SimpleCurve simpleCurve = new SimpleCurve(a, b, (b + a) / 2f + vector * anchor);
		int num = index + (steps + 2) * 6;
		while (num >= verts.Length)
		{
			Array.Resize(ref verts, verts.Length * 2);
		}
		Vector2 vector2 = simpleCurve.GetPoint(0f);
		for (int i = 0; i <= steps; i++)
		{
			Vector2 point = simpleCurve.GetPoint((float)i / (float)steps);
			if (i != steps)
			{
				point += new Vector2(PseudoRandRange(ref seed, -2f, 2f), PseudoRandRange(ref seed, -2f, 2f));
			}
			verts[index].Position = new Vector3(vector2 - vector, 0f);
			verts[index++].Color = color;
			verts[index].Position = new Vector3(point - vector, 0f);
			verts[index++].Color = color;
			verts[index].Position = new Vector3(point, 0f);
			verts[index++].Color = color;
			verts[index].Position = new Vector3(vector2 - vector, 0f);
			verts[index++].Color = color;
			verts[index].Position = new Vector3(point, 0f);
			verts[index++].Color = color;
			verts[index].Position = new Vector3(vector2, 0f);
			verts[index++].Color = color;
			vector2 = point;
		}
	}

	private static void DrawFatLightning(uint seed, Vector2 a, Vector2 b, float size, float gap, Color color)
	{
		seed += (uint)(a.GetHashCode() + b.GetHashCode());
		float num = (b - a).Length();
		Vector2 vector = (b - a) / num;
		Vector2 vector2 = vector.TurnRight();
		Vector2 vector3 = a;
		int num2 = 1;
		PseudoRandRange(ref seed, 0f, (float)Math.PI * 2f);
		float num3 = 0f;
		do
		{
			num3 += PseudoRandRange(ref seed, 10f, 14f);
			Vector2 vector4 = a + vector * num3;
			if (num3 < num)
			{
				vector4 += num2 * vector2 * PseudoRandRange(ref seed, 0f, 6f);
			}
			else
			{
				vector4 = b;
			}
			Vector2 vector5 = vector4;
			if (gap > 0f)
			{
				vector5 = vector3 + (vector4 - vector3) * (1f - gap);
				Draw.Line(vector3, vector4 + vector, color, size * 0.5f);
			}
			Draw.Line(vector3, vector5 + vector, color, size);
			vector3 = vector4;
			num2 = -num2;
		}
		while (num3 < num);
	}

	private static uint PseudoRand(ref uint seed)
	{
		seed ^= seed << 13;
		seed ^= seed >> 17;
		return seed;
	}

	public static float PseudoRandRange(ref uint seed, float min, float max)
	{
		return min + (float)(PseudoRand(ref seed) & 0x3FF) / 1024f * (max - min);
	}
}
