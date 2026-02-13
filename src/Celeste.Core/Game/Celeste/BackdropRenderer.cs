using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;

namespace Celeste;

public class BackdropRenderer : Renderer
{
	[CompilerGenerated]
	private sealed class _003CGetEach_003Ed__9<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : class
	{
		private int _003C_003E1__state;

		private T _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public BackdropRenderer _003C_003E4__this;

		private List<Backdrop>.Enumerator _003C_003E7__wrap1;

		T IEnumerator<T>.Current
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
		public _003CGetEach_003Ed__9(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			_003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				BackdropRenderer backdropRenderer = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E7__wrap1 = backdropRenderer.Backdrops.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				while (_003C_003E7__wrap1.MoveNext())
				{
					Backdrop current = _003C_003E7__wrap1.Current;
					if (current is T)
					{
						_003C_003E2__current = current as T;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap1 = default(List<Backdrop>.Enumerator);
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap1/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			_003CGetEach_003Ed__9<T> result;
			if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
			{
				_003C_003E1__state = 0;
				result = this;
			}
			else
			{
				result = new _003CGetEach_003Ed__9<T>(0)
				{
					_003C_003E4__this = _003C_003E4__this
				};
			}
			return result;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}
	}

	[CompilerGenerated]
	private sealed class _003CGetEach_003Ed__10<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : class
	{
		private int _003C_003E1__state;

		private T _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public BackdropRenderer _003C_003E4__this;

		private string tag;

		public string _003C_003E3__tag;

		private List<Backdrop>.Enumerator _003C_003E7__wrap1;

		T IEnumerator<T>.Current
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
		public _003CGetEach_003Ed__10(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			_003C_003El__initialThreadId = Environment.CurrentManagedThreadId;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = _003C_003E1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					_003C_003Em__Finally1();
				}
			}
		}

		private bool MoveNext()
		{
			try
			{
				int num = _003C_003E1__state;
				BackdropRenderer backdropRenderer = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E7__wrap1 = backdropRenderer.Backdrops.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				while (_003C_003E7__wrap1.MoveNext())
				{
					Backdrop current = _003C_003E7__wrap1.Current;
					if (current is T && current.Tags.Contains(tag))
					{
						_003C_003E2__current = current as T;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap1 = default(List<Backdrop>.Enumerator);
				return false;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			_003C_003E1__state = -1;
			((IDisposable)_003C_003E7__wrap1/*cast due to .constrained prefix*/).Dispose();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			_003CGetEach_003Ed__10<T> _003CGetEach_003Ed__;
			if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
			{
				_003C_003E1__state = 0;
				_003CGetEach_003Ed__ = this;
			}
			else
			{
				_003CGetEach_003Ed__ = new _003CGetEach_003Ed__10<T>(0)
				{
					_003C_003E4__this = _003C_003E4__this
				};
			}
			_003CGetEach_003Ed__.tag = _003C_003E3__tag;
			return _003CGetEach_003Ed__;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}
	}

	public Matrix Matrix = Matrix.Identity;

	public List<Backdrop> Backdrops = new List<Backdrop>();

	public float Fade;

	public Color FadeColor = Color.Black;

	private bool usingSpritebatch;

	public override void BeforeRender(Scene scene)
	{
		foreach (Backdrop backdrop in Backdrops)
		{
			backdrop.BeforeRender(scene);
		}
	}

	public override void Update(Scene scene)
	{
		foreach (Backdrop backdrop in Backdrops)
		{
			backdrop.Update(scene);
		}
	}

	public void Ended(Scene scene)
	{
		foreach (Backdrop backdrop in Backdrops)
		{
			backdrop.Ended(scene);
		}
	}

	public T Get<T>() where T : class
	{
		foreach (Backdrop backdrop in Backdrops)
		{
			if (backdrop is T)
			{
				return backdrop as T;
			}
		}
		return null;
	}

	[IteratorStateMachine(typeof(_003CGetEach_003Ed__9<>))]
	public IEnumerable<T> GetEach<T>() where T : class
	{
		foreach (Backdrop backdrop in Backdrops)
		{
			if (backdrop is T)
			{
				yield return backdrop as T;
			}
		}
	}

	[IteratorStateMachine(typeof(_003CGetEach_003Ed__10<>))]
	public IEnumerable<T> GetEach<T>(string tag) where T : class
	{
		foreach (Backdrop backdrop in Backdrops)
		{
			if (backdrop is T && backdrop.Tags.Contains(tag))
			{
				yield return backdrop as T;
			}
		}
	}

	public void StartSpritebatch(BlendState blendState)
	{
		if (!usingSpritebatch)
		{
			Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, blendState, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Matrix);
		}
		usingSpritebatch = true;
	}

	public void EndSpritebatch()
	{
		if (usingSpritebatch)
		{
			Draw.SpriteBatch.End();
		}
		usingSpritebatch = false;
	}

	public override void Render(Scene scene)
	{
		BlendState blendState = BlendState.AlphaBlend;
		foreach (Backdrop backdrop in Backdrops)
		{
			if (backdrop.Visible)
			{
				if (backdrop is Parallax && (backdrop as Parallax).BlendState != blendState)
				{
					EndSpritebatch();
					blendState = (backdrop as Parallax).BlendState;
				}
				if (backdrop.UseSpritebatch && !usingSpritebatch)
				{
					StartSpritebatch(blendState);
				}
				if (!backdrop.UseSpritebatch && usingSpritebatch)
				{
					EndSpritebatch();
				}
				backdrop.Render(scene);
			}
		}
		if (Fade > 0f)
		{
			Draw.Rect(-10f, -10f, 340f, 200f, FadeColor * Fade);
		}
		EndSpritebatch();
	}

	public void Remove<T>() where T : Backdrop
	{
		for (int num = Backdrops.Count - 1; num >= 0; num--)
		{
			if (Backdrops[num].GetType() == typeof(T))
			{
				Backdrops.RemoveAt(num);
			}
		}
	}
}
