using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste;

public class CrumblePlatform : Solid
{
	[CompilerGenerated]
	private sealed class _003CSequence_003Ed__11 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CrumblePlatform _003C_003E4__this;

		private bool _003ConTop_003E5__2;

		private float _003Ctimer_003E5__3;

		private int _003Ci_003E5__4;

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
		public _003CSequence_003Ed__11(int _003C_003E1__state)
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
			CrumblePlatform crumblePlatform = _003C_003E4__this;
			float num2;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				goto IL_0039;
			case 1:
				_003C_003E1__state = -1;
				goto IL_0040;
			case 2:
				_003C_003E1__state = -1;
				foreach (Image image in crumblePlatform.images)
				{
					crumblePlatform.SceneAs<Level>().Particles.Emit(P_Crumble, 2, crumblePlatform.Position + image.Position + new Vector2(0f, 2f), Vector2.One * 3f);
				}
				_003Ci_003E5__4++;
				goto IL_01ef;
			case 3:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__3 -= Engine.DeltaTime;
				goto IL_0244;
			case 4:
				_003C_003E1__state = -1;
				_003Ctimer_003E5__3 -= Engine.DeltaTime;
				goto IL_0284;
			case 5:
				_003C_003E1__state = -1;
				goto IL_0361;
			case 6:
				{
					_003C_003E1__state = -1;
					goto IL_0361;
				}
				IL_0361:
				if (crumblePlatform.CollideCheck<Actor>() || crumblePlatform.CollideCheck<Solid>())
				{
					break;
				}
				crumblePlatform.outlineFader.Replace(crumblePlatform.OutlineFade(0f));
				crumblePlatform.occluder.Visible = true;
				crumblePlatform.Collidable = true;
				for (int i = 0; i < 4; i++)
				{
					for (int j = 0; j < crumblePlatform.images.Count; j++)
					{
						if (j % 4 - i == 0)
						{
							crumblePlatform.falls[j].Replace(crumblePlatform.TileIn(j, crumblePlatform.images[crumblePlatform.fallOrder[j]], 0.05f * (float)i));
						}
					}
				}
				goto IL_0039;
				IL_0244:
				if (_003Ctimer_003E5__3 > 0f && crumblePlatform.GetPlayerOnTop() != null)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 3;
					return true;
				}
				goto IL_0291;
				IL_01ef:
				if (_003Ci_003E5__4 < (_003ConTop_003E5__2 ? 1 : 3))
				{
					_003C_003E2__current = 0.2f;
					_003C_003E1__state = 2;
					return true;
				}
				_003Ctimer_003E5__3 = 0.4f;
				if (_003ConTop_003E5__2)
				{
					goto IL_0244;
				}
				goto IL_0284;
				IL_0291:
				crumblePlatform.outlineFader.Replace(crumblePlatform.OutlineFade(1f));
				crumblePlatform.occluder.Visible = false;
				crumblePlatform.Collidable = false;
				num2 = 0.05f;
				for (int k = 0; k < 4; k++)
				{
					for (int l = 0; l < crumblePlatform.images.Count; l++)
					{
						if (l % 4 - k == 0)
						{
							crumblePlatform.falls[l].Replace(crumblePlatform.TileOut(crumblePlatform.images[crumblePlatform.fallOrder[l]], num2 * (float)k));
						}
					}
				}
				_003C_003E2__current = 2f;
				_003C_003E1__state = 5;
				return true;
				IL_0039:
				_003ConTop_003E5__2 = false;
				goto IL_0040;
				IL_0040:
				if (crumblePlatform.GetPlayerOnTop() != null)
				{
					_003ConTop_003E5__2 = true;
					Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				}
				else
				{
					if (crumblePlatform.GetPlayerClimbing() == null)
					{
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
					_003ConTop_003E5__2 = false;
					Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
				}
				Audio.Play("event:/game/general/platform_disintegrate", crumblePlatform.Center);
				crumblePlatform.shaker.ShakeFor(_003ConTop_003E5__2 ? 0.6f : 1f, removeOnFinish: false);
				foreach (Image image2 in crumblePlatform.images)
				{
					crumblePlatform.SceneAs<Level>().Particles.Emit(P_Crumble, 2, crumblePlatform.Position + image2.Position + new Vector2(0f, 2f), Vector2.One * 3f);
				}
				_003Ci_003E5__4 = 0;
				goto IL_01ef;
				IL_0284:
				if (_003Ctimer_003E5__3 > 0f)
				{
					_003C_003E2__current = null;
					_003C_003E1__state = 4;
					return true;
				}
				goto IL_0291;
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 6;
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

	[CompilerGenerated]
	private sealed class _003COutlineFade_003Ed__12 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float to;

		public CrumblePlatform _003C_003E4__this;

		private float _003Cfrom_003E5__2;

		private float _003Ct_003E5__3;

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
		public _003COutlineFade_003Ed__12(int _003C_003E1__state)
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
			CrumblePlatform crumblePlatform = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Cfrom_003E5__2 = 1f - to;
				_003Ct_003E5__3 = 0f;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ct_003E5__3 += Engine.DeltaTime * 2f;
				break;
			}
			if (_003Ct_003E5__3 < 1f)
			{
				Color color = Color.White * (_003Cfrom_003E5__2 + (to - _003Cfrom_003E5__2) * Ease.CubeInOut(_003Ct_003E5__3));
				foreach (Image item in crumblePlatform.outline)
				{
					item.Color = color;
				}
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

	[CompilerGenerated]
	private sealed class _003CTileOut_003Ed__13 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Image img;

		public float delay;

		private float _003Cdistance_003E5__2;

		private Vector2 _003Cfrom_003E5__3;

		private float _003Ctime_003E5__4;

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
		public _003CTileOut_003Ed__13(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			switch (_003C_003E1__state)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				img.Color = Color.Gray;
				_003C_003E2__current = delay;
				_003C_003E1__state = 1;
				return true;
			case 1:
				_003C_003E1__state = -1;
				_003Cdistance_003E5__2 = (img.X * 7f % 3f + 1f) * 12f;
				_003Cfrom_003E5__3 = img.Position;
				_003Ctime_003E5__4 = 0f;
				break;
			case 2:
				_003C_003E1__state = -1;
				img.Position = _003Cfrom_003E5__3 + Vector2.UnitY * Ease.CubeIn(_003Ctime_003E5__4) * _003Cdistance_003E5__2;
				img.Color = Color.Gray * (1f - _003Ctime_003E5__4);
				img.Scale = Vector2.One * (1f - _003Ctime_003E5__4 * 0.5f);
				_003Ctime_003E5__4 += Engine.DeltaTime / 0.4f;
				break;
			}
			if (_003Ctime_003E5__4 < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			img.Visible = false;
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

	[CompilerGenerated]
	private sealed class _003CTileIn_003Ed__14 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CrumblePlatform _003C_003E4__this;

		public Image img;

		public int index;

		private float _003Ctime_003E5__2;

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
		public _003CTileIn_003Ed__14(int _003C_003E1__state)
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
			CrumblePlatform crumblePlatform = _003C_003E4__this;
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
				Audio.Play("event:/game/general/platform_return", crumblePlatform.Center);
				img.Visible = true;
				img.Color = Color.White;
				img.Position = new Vector2(index * 8 + 4, 4f);
				_003Ctime_003E5__2 = 0f;
				break;
			case 2:
				_003C_003E1__state = -1;
				img.Scale = Vector2.One * (1f + Ease.BounceOut(1f - _003Ctime_003E5__2) * 0.2f);
				_003Ctime_003E5__2 += Engine.DeltaTime / 0.25f;
				break;
			}
			if (_003Ctime_003E5__2 < 1f)
			{
				_003C_003E2__current = null;
				_003C_003E1__state = 2;
				return true;
			}
			img.Scale = Vector2.One;
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

	public static ParticleType P_Crumble;

	private List<Image> images;

	private List<Image> outline;

	private List<Coroutine> falls;

	private List<int> fallOrder;

	private ShakerList shaker;

	private LightOcclude occluder;

	private Coroutine outlineFader;

	public CrumblePlatform(Vector2 position, float width)
		: base(position, width, 8f, safe: false)
	{
		EnableAssistModeChecks = false;
	}

	public CrumblePlatform(EntityData data, Vector2 offset)
		: this(data.Position + offset, data.Width)
	{
	}

	public override void Added(Scene scene)
	{
		base.Added(scene);
		MTexture mTexture = GFX.Game["objects/crumbleBlock/outline"];
		outline = new List<Image>();
		if (base.Width <= 8f)
		{
			Image image = new Image(mTexture.GetSubtexture(24, 0, 8, 8));
			image.Color = Color.White * 0f;
			Add(image);
			outline.Add(image);
		}
		else
		{
			for (int i = 0; (float)i < base.Width; i += 8)
			{
				int num = ((i != 0) ? ((i > 0 && (float)i < base.Width - 8f) ? 1 : 2) : 0);
				Image image2 = new Image(mTexture.GetSubtexture(num * 8, 0, 8, 8));
				image2.Position = new Vector2(i, 0f);
				image2.Color = Color.White * 0f;
				Add(image2);
				outline.Add(image2);
			}
		}
		Add(outlineFader = new Coroutine());
		outlineFader.RemoveOnComplete = false;
		images = new List<Image>();
		falls = new List<Coroutine>();
		fallOrder = new List<int>();
		MTexture mTexture2 = GFX.Game["objects/crumbleBlock/" + AreaData.Get(scene).CrumbleBlock];
		for (int j = 0; (float)j < base.Width; j += 8)
		{
			int num2 = (int)((Math.Abs(base.X) + (float)j) / 8f) % 4;
			Image image3 = new Image(mTexture2.GetSubtexture(num2 * 8, 0, 8, 8));
			image3.Position = new Vector2(4 + j, 4f);
			image3.CenterOrigin();
			Add(image3);
			images.Add(image3);
			Coroutine coroutine = new Coroutine();
			coroutine.RemoveOnComplete = false;
			falls.Add(coroutine);
			Add(coroutine);
			fallOrder.Add(j / 8);
		}
		fallOrder.Shuffle();
		Add(new Coroutine(Sequence()));
		Add(shaker = new ShakerList(images.Count, on: false, delegate(Vector2[] v)
		{
			for (int k = 0; k < images.Count; k++)
			{
				images[k].Position = new Vector2(4 + k * 8, 4f) + v[k];
			}
		}));
		Add(occluder = new LightOcclude(0.2f));
	}

	[IteratorStateMachine(typeof(_003CSequence_003Ed__11))]
	private IEnumerator Sequence()
	{
		while (true)
		{
			bool onTop;
			if (GetPlayerOnTop() != null)
			{
				onTop = true;
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
			}
			else
			{
				if (GetPlayerClimbing() == null)
				{
					yield return null;
					continue;
				}
				onTop = false;
				Input.Rumble(RumbleStrength.Medium, RumbleLength.Medium);
			}
			Audio.Play("event:/game/general/platform_disintegrate", base.Center);
			shaker.ShakeFor(onTop ? 0.6f : 1f, removeOnFinish: false);
			foreach (Image image in images)
			{
				SceneAs<Level>().Particles.Emit(P_Crumble, 2, Position + image.Position + new Vector2(0f, 2f), Vector2.One * 3f);
			}
			for (int i = 0; i < (onTop ? 1 : 3); i++)
			{
				yield return 0.2f;
				foreach (Image image2 in images)
				{
					SceneAs<Level>().Particles.Emit(P_Crumble, 2, Position + image2.Position + new Vector2(0f, 2f), Vector2.One * 3f);
				}
			}
			float timer = 0.4f;
			if (onTop)
			{
				while (timer > 0f && GetPlayerOnTop() != null)
				{
					yield return null;
					timer -= Engine.DeltaTime;
				}
			}
			else
			{
				while (timer > 0f)
				{
					yield return null;
					timer -= Engine.DeltaTime;
				}
			}
			outlineFader.Replace(OutlineFade(1f));
			occluder.Visible = false;
			Collidable = false;
			float num = 0.05f;
			for (int j = 0; j < 4; j++)
			{
				for (int k = 0; k < images.Count; k++)
				{
					if (k % 4 - j == 0)
					{
						falls[k].Replace(TileOut(images[fallOrder[k]], num * (float)j));
					}
				}
			}
			yield return 2f;
			while (CollideCheck<Actor>() || CollideCheck<Solid>())
			{
				yield return null;
			}
			outlineFader.Replace(OutlineFade(0f));
			occluder.Visible = true;
			Collidable = true;
			for (int l = 0; l < 4; l++)
			{
				for (int m = 0; m < images.Count; m++)
				{
					if (m % 4 - l == 0)
					{
						falls[m].Replace(TileIn(m, images[fallOrder[m]], 0.05f * (float)l));
					}
				}
			}
		}
	}

	[IteratorStateMachine(typeof(_003COutlineFade_003Ed__12))]
	private IEnumerator OutlineFade(float to)
	{
		float from = 1f - to;
		for (float t = 0f; t < 1f; t += Engine.DeltaTime * 2f)
		{
			Color color = Color.White * (from + (to - from) * Ease.CubeInOut(t));
			foreach (Image item in outline)
			{
				item.Color = color;
			}
			yield return null;
		}
	}

	[IteratorStateMachine(typeof(_003CTileOut_003Ed__13))]
	private IEnumerator TileOut(Image img, float delay)
	{
		img.Color = Color.Gray;
		yield return delay;
		float distance = (img.X * 7f % 3f + 1f) * 12f;
		Vector2 from = img.Position;
		for (float time = 0f; time < 1f; time += Engine.DeltaTime / 0.4f)
		{
			yield return null;
			img.Position = from + Vector2.UnitY * Ease.CubeIn(time) * distance;
			img.Color = Color.Gray * (1f - time);
			img.Scale = Vector2.One * (1f - time * 0.5f);
		}
		img.Visible = false;
	}

	[IteratorStateMachine(typeof(_003CTileIn_003Ed__14))]
	private IEnumerator TileIn(int index, Image img, float delay)
	{
		yield return delay;
		Audio.Play("event:/game/general/platform_return", base.Center);
		img.Visible = true;
		img.Color = Color.White;
		img.Position = new Vector2(index * 8 + 4, 4f);
		for (float time = 0f; time < 1f; time += Engine.DeltaTime / 0.25f)
		{
			yield return null;
			img.Scale = Vector2.One * (1f + Ease.BounceOut(1f - time) * 0.2f);
		}
		img.Scale = Vector2.One;
	}
}
