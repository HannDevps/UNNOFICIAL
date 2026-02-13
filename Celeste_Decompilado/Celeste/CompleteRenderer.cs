using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class CompleteRenderer : HiresRenderer, IDisposable
{
	public abstract class Layer
	{
		public Vector2 Position;

		public Vector2 ScrollFactor;

		public Layer(XmlElement xml)
		{
			Position = xml.Position(Vector2.Zero);
			if (xml.HasAttr("scroll"))
			{
				ScrollFactor.X = (ScrollFactor.Y = xml.AttrFloat("scroll"));
				return;
			}
			ScrollFactor.X = xml.AttrFloat("scrollX", 0f);
			ScrollFactor.Y = xml.AttrFloat("scrollY", 0f);
		}

		public virtual void Update(Scene scene)
		{
		}

		public abstract void Render(Vector2 scroll);

		public Vector2 GetScrollPosition(Vector2 scroll)
		{
			Vector2 position = Position;
			if (ScrollFactor != Vector2.Zero)
			{
				position.X = MathHelper.Lerp(Position.X, Position.X + scroll.X, ScrollFactor.X);
				position.Y = MathHelper.Lerp(Position.Y, Position.Y + scroll.Y, ScrollFactor.Y);
			}
			return position;
		}
	}

	public class UILayer : Layer
	{
		private CompleteRenderer renderer;

		public UILayer(CompleteRenderer renderer, XmlElement xml)
			: base(xml)
		{
			this.renderer = renderer;
		}

		public override void Render(Vector2 scroll)
		{
			if (renderer.RenderUI != null)
			{
				renderer.RenderUI(scroll);
			}
		}
	}

	public class ImageLayer : Layer
	{
		public List<MTexture> Images = new List<MTexture>();

		public float Frame;

		public float FrameRate;

		public float Alpha;

		public Vector2 Offset;

		public Vector2 Speed;

		public float Scale;

		public ImageLayer(Vector2 offset, Atlas atlas, XmlElement xml)
			: base(xml)
		{
			Position += offset;
			string[] array = xml.Attr("img").Split(',');
			foreach (string id in array)
			{
				if (atlas.Has(id))
				{
					Images.Add(atlas[id]);
				}
				else
				{
					Images.Add(null);
				}
			}
			FrameRate = xml.AttrFloat("fps", 6f);
			Alpha = xml.AttrFloat("alpha", 1f);
			Speed = new Vector2(xml.AttrFloat("speedx", 0f), xml.AttrFloat("speedy", 0f));
			Scale = xml.AttrFloat("scale", 1f);
		}

		public override void Update(Scene scene)
		{
			Frame += Engine.DeltaTime * FrameRate;
			Offset += Speed * Engine.DeltaTime;
		}

		public override void Render(Vector2 scroll)
		{
			Vector2 position = GetScrollPosition(scroll).Floor();
			MTexture mTexture = Images[(int)(Frame % (float)Images.Count)];
			if (mTexture == null)
			{
				return;
			}
			bool flag = SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode;
			if (flag)
			{
				position.X = 1920f - position.X - mTexture.DrawOffset.X * Scale - (float)mTexture.Texture.Texture.Width * Scale;
				position.Y += mTexture.DrawOffset.Y * Scale;
			}
			else
			{
				position += mTexture.DrawOffset * Scale;
			}
			Rectangle value = mTexture.ClipRect;
			int num;
			if (Offset.X == 0f)
			{
				num = ((Offset.Y != 0f) ? 1 : 0);
				if (num == 0)
				{
					goto IL_015b;
				}
			}
			else
			{
				num = 1;
			}
			value = new Rectangle((int)((0f - Offset.X) / Scale) + 1, (int)((0f - Offset.Y) / Scale) + 1, mTexture.ClipRect.Width - 2, mTexture.ClipRect.Height - 2);
			HiresRenderer.EndRender();
			HiresRenderer.BeginRender(BlendState.AlphaBlend, SamplerState.LinearWrap);
			goto IL_015b;
			IL_015b:
			Draw.SpriteBatch.Draw(mTexture.Texture.Texture, position, value, Color.White * Alpha, 0f, Vector2.Zero, Scale, flag ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			if (num != 0)
			{
				HiresRenderer.EndRender();
				HiresRenderer.BeginRender(BlendState.AlphaBlend, SamplerState.LinearClamp);
			}
		}
	}

	[CompilerGenerated]
	private sealed class _003CSlideRoutine_003Ed__23 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CompleteRenderer _003C_003E4__this;

		public Action onDoneSlide;

		private float _003Cp_003E5__2;

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
		public _003CSlideRoutine_003Ed__23(int _003C_003E1__state)
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
			CompleteRenderer completeRenderer = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cp_003E5__2 = 0f;
				goto IL_00d7;
			case 2:
				_003C_003E1__state = -1;
				completeRenderer.Scroll = Vector2.Lerp(completeRenderer.StartScroll, completeRenderer.CenterScroll, Ease.SineOut(_003Cp_003E5__2));
				completeRenderer.fadeAlpha = Calc.LerpClamp(1f, 0f, _003Cp_003E5__2 * 2f);
				_003Cp_003E5__2 += Engine.DeltaTime / completeRenderer.SlideDuration;
				goto IL_00d7;
			case 3:
				_003C_003E1__state = -1;
				if (onDoneSlide != null)
				{
					onDoneSlide();
				}
				break;
			case 4:
				{
					_003C_003E1__state = -1;
					break;
				}
				IL_00d7:
				if (_003Cp_003E5__2 < 1f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 2;
					return true;
				}
				completeRenderer.Scroll = completeRenderer.CenterScroll;
				completeRenderer.fadeAlpha = 0f;
				_003C_003E2__current = 0.2f;
				_003C_003E1__state = 3;
				return true;
			}
			completeRenderer.controlMult = Calc.Approach(completeRenderer.controlMult, 1f, 5f * Engine.DeltaTime);
			_003C_003E2__current = null;
			_003C_003E1__state = 4;
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

	private const float ScrollRange = 200f;

	private const float ScrollSpeed = 600f;

	private Atlas atlas;

	private XmlElement xml;

	private float fadeAlpha = 1f;

	private Coroutine routine;

	private Vector2 controlScroll;

	private float controlMult;

	public float SlideDuration = 1.5f;

	public List<Layer> Layers = new List<Layer>();

	public Vector2 Scroll;

	public Vector2 StartScroll;

	public Vector2 CenterScroll;

	public Vector2 Offset;

	public float Scale;

	public Action<Vector2> RenderUI;

	public Action RenderPostUI;

	public bool HasUI { get; private set; }

	public CompleteRenderer(XmlElement xml, Atlas atlas, float delay, Action onDoneSlide = null)
	{
		this.atlas = atlas;
		this.xml = xml;
		if (xml != null)
		{
			if (xml["start"] != null)
			{
				StartScroll = xml["start"].Position();
			}
			if (xml["center"] != null)
			{
				CenterScroll = xml["center"].Position();
			}
			if (xml["offset"] != null)
			{
				Offset = xml["offset"].Position();
			}
			foreach (object item in xml["layers"])
			{
				if (item is XmlElement)
				{
					XmlElement xmlElement = item as XmlElement;
					if (xmlElement.Name == "layer")
					{
						Layers.Add(new ImageLayer(Offset, atlas, xmlElement));
					}
					else if (xmlElement.Name == "ui")
					{
						HasUI = true;
						Layers.Add(new UILayer(this, xmlElement));
					}
				}
			}
		}
		Scroll = StartScroll;
		routine = new Coroutine(SlideRoutine(delay, onDoneSlide));
	}

	public void Dispose()
	{
		if (atlas != null)
		{
			atlas.Dispose();
		}
	}

	[IteratorStateMachine(typeof(_003CSlideRoutine_003Ed__23))]
	private IEnumerator SlideRoutine(float delay, Action onDoneSlide)
	{
		yield return delay;
		for (float p = 0f; p < 1f; p += Engine.DeltaTime / SlideDuration)
		{
			yield return null;
			Scroll = Vector2.Lerp(StartScroll, CenterScroll, Ease.SineOut(p));
			fadeAlpha = Calc.LerpClamp(1f, 0f, p * 2f);
		}
		Scroll = CenterScroll;
		fadeAlpha = 0f;
		yield return 0.2f;
		onDoneSlide?.Invoke();
		while (true)
		{
			controlMult = Calc.Approach(controlMult, 1f, 5f * Engine.DeltaTime);
			yield return null;
		}
	}

	public override void Update(Scene scene)
	{
		Vector2 value = Input.Aim.Value;
		value += Input.MountainAim.Value;
		if (value.Length() > 1f)
		{
			value.Normalize();
		}
		value *= 200f;
		controlScroll = Calc.Approach(controlScroll, value, 600f * Engine.DeltaTime);
		foreach (Layer layer in Layers)
		{
			layer.Update(scene);
		}
		routine.Update();
	}

	public override void RenderContent(Scene scene)
	{
		HiresRenderer.BeginRender(BlendState.AlphaBlend, SamplerState.LinearClamp);
		foreach (Layer layer in Layers)
		{
			layer.Render(-Scroll - controlScroll * controlMult);
		}
		if (RenderPostUI != null)
		{
			RenderPostUI();
		}
		if (fadeAlpha > 0f)
		{
			Draw.Rect(-10f, -10f, Engine.Width + 20, Engine.Height + 20, Color.Black * fadeAlpha);
		}
		HiresRenderer.EndRender();
	}
}
