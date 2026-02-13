using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;

namespace Monocle;

public class Sprite : Image
{
	private class Animation
	{
		public float Delay;

		public MTexture[] Frames;

		public Chooser<string> Goto;
	}

	[CompilerGenerated]
	private sealed class _003CPlayUtil_003Ed__40 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Sprite _003C_003E4__this;

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
		public _003CPlayUtil_003Ed__40(int _003C_003E1__state)
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
			Sprite sprite = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				break;
			case 1:
				_003C_003E1__state = -1;
				break;
			}
			if (sprite.Animating)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			return false;
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

	public float Rate = 1f;

	public bool UseRawDeltaTime;

	public Vector2? Justify;

	public Action<string> OnFinish;

	public Action<string> OnLoop;

	public Action<string> OnFrameChange;

	public Action<string> OnLastFrame;

	public Action<string, string> OnChange;

	private Atlas atlas;

	public string Path;

	private Dictionary<string, Animation> animations;

	private Animation currentAnimation;

	private float animationTimer;

	private int width;

	private int height;

	public Vector2 Center => new Vector2(Width / 2f, Height / 2f);

	public bool Animating { get; private set; }

	public string CurrentAnimationID { get; private set; }

	public string LastAnimationID { get; private set; }

	public int CurrentAnimationFrame { get; private set; }

	public int CurrentAnimationTotalFrames
	{
		get
		{
			if (currentAnimation != null)
			{
				return currentAnimation.Frames.Length;
			}
			return 0;
		}
	}

	public override float Width => width;

	public override float Height => height;

	public Sprite(Atlas atlas, string path)
		: base(null, active: true)
	{
		this.atlas = atlas;
		Path = path;
		animations = new Dictionary<string, Animation>(StringComparer.OrdinalIgnoreCase);
		CurrentAnimationID = "";
	}

	public void Reset(Atlas atlas, string path)
	{
		this.atlas = atlas;
		Path = path;
		animations = new Dictionary<string, Animation>(StringComparer.OrdinalIgnoreCase);
		currentAnimation = null;
		CurrentAnimationID = "";
		OnFinish = null;
		OnLoop = null;
		OnFrameChange = null;
		OnChange = null;
		Animating = false;
	}

	public MTexture GetFrame(string animation, int frame)
	{
		return animations[animation].Frames[frame];
	}

	public override void Update()
	{
		if (!Animating)
		{
			return;
		}
		if (UseRawDeltaTime)
		{
			animationTimer += Engine.RawDeltaTime * Rate;
		}
		else
		{
			animationTimer += Engine.DeltaTime * Rate;
		}
		if (!(Math.Abs(animationTimer) >= currentAnimation.Delay))
		{
			return;
		}
		CurrentAnimationFrame += Math.Sign(animationTimer);
		animationTimer -= (float)Math.Sign(animationTimer) * currentAnimation.Delay;
		if (CurrentAnimationFrame < 0 || CurrentAnimationFrame >= currentAnimation.Frames.Length)
		{
			string currentAnimationID = CurrentAnimationID;
			if (OnLastFrame != null)
			{
				OnLastFrame(CurrentAnimationID);
			}
			if (!(currentAnimationID == CurrentAnimationID))
			{
				return;
			}
			if (currentAnimation.Goto != null)
			{
				CurrentAnimationID = currentAnimation.Goto.Choose();
				if (OnChange != null)
				{
					OnChange(LastAnimationID, CurrentAnimationID);
				}
				LastAnimationID = CurrentAnimationID;
				currentAnimation = animations[LastAnimationID];
				if (CurrentAnimationFrame < 0)
				{
					CurrentAnimationFrame = currentAnimation.Frames.Length - 1;
				}
				else
				{
					CurrentAnimationFrame = 0;
				}
				SetFrame(currentAnimation.Frames[CurrentAnimationFrame]);
				if (OnLoop != null)
				{
					OnLoop(CurrentAnimationID);
				}
			}
			else
			{
				if (CurrentAnimationFrame < 0)
				{
					CurrentAnimationFrame = 0;
				}
				else
				{
					CurrentAnimationFrame = currentAnimation.Frames.Length - 1;
				}
				Animating = false;
				string currentAnimationID2 = CurrentAnimationID;
				CurrentAnimationID = "";
				currentAnimation = null;
				animationTimer = 0f;
				if (OnFinish != null)
				{
					OnFinish(currentAnimationID2);
				}
			}
		}
		else
		{
			SetFrame(currentAnimation.Frames[CurrentAnimationFrame]);
		}
	}

	private void SetFrame(MTexture texture)
	{
		if (texture != Texture)
		{
			Texture = texture;
			if (width == 0)
			{
				width = texture.Width;
			}
			if (height == 0)
			{
				height = texture.Height;
			}
			if (Justify.HasValue)
			{
				Origin = new Vector2((float)Texture.Width * Justify.Value.X, (float)Texture.Height * Justify.Value.Y);
			}
			if (OnFrameChange != null)
			{
				OnFrameChange(CurrentAnimationID);
			}
		}
	}

	public void SetAnimationFrame(int frame)
	{
		animationTimer = 0f;
		CurrentAnimationFrame = frame % currentAnimation.Frames.Length;
		SetFrame(currentAnimation.Frames[CurrentAnimationFrame]);
	}

	public void AddLoop(string id, string path, float delay)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path),
			Goto = new Chooser<string>(id, 1f)
		};
	}

	public void AddLoop(string id, string path, float delay, params int[] frames)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path, frames),
			Goto = new Chooser<string>(id, 1f)
		};
	}

	public void AddLoop(string id, float delay, params MTexture[] frames)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = frames,
			Goto = new Chooser<string>(id, 1f)
		};
	}

	public void Add(string id, string path)
	{
		animations[id] = new Animation
		{
			Delay = 0f,
			Frames = GetFrames(path),
			Goto = null
		};
	}

	public void Add(string id, string path, float delay)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path),
			Goto = null
		};
	}

	public void Add(string id, string path, float delay, params int[] frames)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path, frames),
			Goto = null
		};
	}

	public void Add(string id, string path, float delay, string into)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path),
			Goto = Chooser<string>.FromString<string>(into)
		};
	}

	public void Add(string id, string path, float delay, Chooser<string> into)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path),
			Goto = into
		};
	}

	public void Add(string id, string path, float delay, string into, params int[] frames)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path, frames),
			Goto = Chooser<string>.FromString<string>(into)
		};
	}

	public void Add(string id, float delay, string into, params MTexture[] frames)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = frames,
			Goto = Chooser<string>.FromString<string>(into)
		};
	}

	public void Add(string id, string path, float delay, Chooser<string> into, params int[] frames)
	{
		animations[id] = new Animation
		{
			Delay = delay,
			Frames = GetFrames(path, frames),
			Goto = into
		};
	}

	private MTexture[] GetFrames(string path, int[] frames = null)
	{
		MTexture[] array;
		if (frames == null || frames.Length == 0)
		{
			array = atlas.GetAtlasSubtextures(Path + path).ToArray();
		}
		else
		{
			string text = Path + path;
			MTexture[] array2 = new MTexture[frames.Length];
			for (int i = 0; i < frames.Length; i++)
			{
				MTexture atlasSubtexturesAt = atlas.GetAtlasSubtexturesAt(text, frames[i]);
				if (atlasSubtexturesAt == null)
				{
					throw new Exception("Can't find sprite " + text + " with index " + frames[i]);
				}
				array2[i] = atlasSubtexturesAt;
			}
			array = array2;
		}
		width = Math.Max(array[0].Width, width);
		height = Math.Max(array[0].Height, height);
		return array;
	}

	public void ClearAnimations()
	{
		animations.Clear();
	}

	public void Play(string id, bool restart = false, bool randomizeFrame = false)
	{
		if (CurrentAnimationID != id || restart)
		{
			if (OnChange != null)
			{
				OnChange(LastAnimationID, id);
			}
			string lastAnimationID = (CurrentAnimationID = id);
			LastAnimationID = lastAnimationID;
			currentAnimation = animations[id];
			Animating = currentAnimation.Delay > 0f;
			if (randomizeFrame)
			{
				animationTimer = Calc.Random.NextFloat(currentAnimation.Delay);
				CurrentAnimationFrame = Calc.Random.Next(currentAnimation.Frames.Length);
			}
			else
			{
				animationTimer = 0f;
				CurrentAnimationFrame = 0;
			}
			SetFrame(currentAnimation.Frames[CurrentAnimationFrame]);
		}
	}

	public void PlayOffset(string id, float offset, bool restart = false)
	{
		if (!(CurrentAnimationID != id || restart))
		{
			return;
		}
		if (OnChange != null)
		{
			OnChange(LastAnimationID, id);
		}
		string lastAnimationID = (CurrentAnimationID = id);
		LastAnimationID = lastAnimationID;
		currentAnimation = animations[id];
		if (currentAnimation.Delay > 0f)
		{
			Animating = true;
			float num = currentAnimation.Delay * (float)currentAnimation.Frames.Length * offset;
			CurrentAnimationFrame = 0;
			while (num >= currentAnimation.Delay)
			{
				CurrentAnimationFrame++;
				num -= currentAnimation.Delay;
			}
			CurrentAnimationFrame %= currentAnimation.Frames.Length;
			animationTimer = num;
			SetFrame(currentAnimation.Frames[CurrentAnimationFrame]);
		}
		else
		{
			animationTimer = 0f;
			Animating = false;
			CurrentAnimationFrame = 0;
			SetFrame(currentAnimation.Frames[0]);
		}
	}

	public IEnumerator PlayRoutine(string id, bool restart = false)
	{
		Play(id, restart);
		return PlayUtil();
	}

	public IEnumerator ReverseRoutine(string id, bool restart = false)
	{
		Reverse(id, restart);
		return PlayUtil();
	}

	[IteratorStateMachine(typeof(_003CPlayUtil_003Ed__40))]
	private IEnumerator PlayUtil()
	{
		while (Animating)
		{
			yield return null;
		}
	}

	public void Reverse(string id, bool restart = false)
	{
		Play(id, restart);
		if (Rate > 0f)
		{
			Rate *= -1f;
		}
	}

	public bool Has(string id)
	{
		if (id != null)
		{
			return animations.ContainsKey(id);
		}
		return false;
	}

	public void Stop()
	{
		Animating = false;
		currentAnimation = null;
		CurrentAnimationID = "";
	}

	internal Sprite()
		: base(null, active: true)
	{
	}

	internal Sprite CreateClone()
	{
		return CloneInto(new Sprite());
	}

	internal Sprite CloneInto(Sprite clone)
	{
		clone.Texture = Texture;
		clone.Position = Position;
		clone.Justify = Justify;
		clone.Origin = Origin;
		clone.animations = new Dictionary<string, Animation>(animations, StringComparer.OrdinalIgnoreCase);
		clone.currentAnimation = currentAnimation;
		clone.animationTimer = animationTimer;
		clone.width = width;
		clone.height = height;
		clone.Animating = Animating;
		clone.CurrentAnimationID = CurrentAnimationID;
		clone.LastAnimationID = LastAnimationID;
		clone.CurrentAnimationFrame = CurrentAnimationFrame;
		return clone;
	}

	public void DrawSubrect(Vector2 offset, Rectangle rectangle)
	{
		if (Texture != null)
		{
			Rectangle relativeRect = Texture.GetRelativeRect(rectangle);
			Vector2 vector = new Vector2(0f - Math.Min((float)rectangle.X - Texture.DrawOffset.X, 0f), 0f - Math.Min((float)rectangle.Y - Texture.DrawOffset.Y, 0f));
			Draw.SpriteBatch.Draw(Texture.Texture.Texture, base.RenderPosition + offset, relativeRect, Color, Rotation, Origin - vector, Scale, Effects, 0f);
		}
	}

	public void LogAnimations()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, Animation> animation in animations)
		{
			Animation value = animation.Value;
			stringBuilder.Append(animation.Key);
			stringBuilder.Append("\n{\n\t");
			object[] frames = value.Frames;
			stringBuilder.Append(string.Join("\n\t", frames));
			stringBuilder.Append("\n}\n");
		}
		Calc.Log(stringBuilder.ToString());
	}
}
