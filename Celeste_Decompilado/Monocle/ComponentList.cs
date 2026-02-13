using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Monocle;

public class ComponentList : IEnumerable<Component>, IEnumerable
{
	public enum LockModes
	{
		Open,
		Locked,
		Error
	}

	[CompilerGenerated]
	private sealed class _003CGetAll_003Ed__36<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator where T : Component
	{
		private int _003C_003E1__state;

		private T _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public ComponentList _003C_003E4__this;

		private List<Component>.Enumerator _003C_003E7__wrap1;

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
		public _003CGetAll_003Ed__36(int _003C_003E1__state)
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
				ComponentList componentList = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E7__wrap1 = componentList.components.GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				while (_003C_003E7__wrap1.MoveNext())
				{
					Component current = _003C_003E7__wrap1.Current;
					if (current is T)
					{
						_003C_003E2__current = current as T;
						_003C_003E1__state = 1;
						return true;
					}
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap1 = default(List<Component>.Enumerator);
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
			_003CGetAll_003Ed__36<T> result;
			if (_003C_003E1__state == -2 && _003C_003El__initialThreadId == Environment.CurrentManagedThreadId)
			{
				_003C_003E1__state = 0;
				result = this;
			}
			else
			{
				result = new _003CGetAll_003Ed__36<T>(0)
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

	private List<Component> components;

	private List<Component> toAdd;

	private List<Component> toRemove;

	private HashSet<Component> current;

	private HashSet<Component> adding;

	private HashSet<Component> removing;

	private LockModes lockMode;

	public Entity Entity { get; internal set; }

	internal LockModes LockMode
	{
		get
		{
			return lockMode;
		}
		set
		{
			lockMode = value;
			if (toAdd.Count > 0)
			{
				foreach (Component item in toAdd)
				{
					if (!current.Contains(item))
					{
						current.Add(item);
						components.Add(item);
						item.Added(Entity);
					}
				}
				adding.Clear();
				toAdd.Clear();
			}
			if (toRemove.Count <= 0)
			{
				return;
			}
			foreach (Component item2 in toRemove)
			{
				if (current.Contains(item2))
				{
					current.Remove(item2);
					components.Remove(item2);
					item2.Removed(Entity);
				}
			}
			removing.Clear();
			toRemove.Clear();
		}
	}

	public int Count => components.Count;

	public Component this[int index]
	{
		get
		{
			if (index < 0 || index >= components.Count)
			{
				throw new IndexOutOfRangeException();
			}
			return components[index];
		}
	}

	internal ComponentList(Entity entity)
	{
		Entity = entity;
		components = new List<Component>();
		toAdd = new List<Component>();
		toRemove = new List<Component>();
		current = new HashSet<Component>();
		adding = new HashSet<Component>();
		removing = new HashSet<Component>();
	}

	public void Add(Component component)
	{
		switch (lockMode)
		{
		case LockModes.Open:
			if (!current.Contains(component))
			{
				current.Add(component);
				components.Add(component);
				component.Added(Entity);
			}
			break;
		case LockModes.Locked:
			if (!current.Contains(component) && !adding.Contains(component))
			{
				adding.Add(component);
				toAdd.Add(component);
			}
			break;
		case LockModes.Error:
			throw new Exception("Cannot add or remove Entities at this time!");
		}
	}

	public void Remove(Component component)
	{
		switch (lockMode)
		{
		case LockModes.Open:
			if (current.Contains(component))
			{
				current.Remove(component);
				components.Remove(component);
				component.Removed(Entity);
			}
			break;
		case LockModes.Locked:
			if (current.Contains(component) && !removing.Contains(component))
			{
				removing.Add(component);
				toRemove.Add(component);
			}
			break;
		case LockModes.Error:
			throw new Exception("Cannot add or remove Entities at this time!");
		}
	}

	public void Add(IEnumerable<Component> components)
	{
		foreach (Component component in components)
		{
			Add(component);
		}
	}

	public void Remove(IEnumerable<Component> components)
	{
		foreach (Component component in components)
		{
			Remove(component);
		}
	}

	public void RemoveAll<T>() where T : Component
	{
		Remove(GetAll<T>());
	}

	public void Add(params Component[] components)
	{
		foreach (Component component in components)
		{
			Add(component);
		}
	}

	public void Remove(params Component[] components)
	{
		foreach (Component component in components)
		{
			Remove(component);
		}
	}

	public IEnumerator<Component> GetEnumerator()
	{
		return components.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public Component[] ToArray()
	{
		return Enumerable.ToArray(components);
	}

	internal void Update()
	{
		LockMode = LockModes.Locked;
		foreach (Component component in components)
		{
			if (component.Active)
			{
				component.Update();
			}
		}
		LockMode = LockModes.Open;
	}

	internal void Render()
	{
		LockMode = LockModes.Error;
		foreach (Component component in components)
		{
			if (component.Visible)
			{
				component.Render();
			}
		}
		LockMode = LockModes.Open;
	}

	internal void DebugRender(Camera camera)
	{
		LockMode = LockModes.Error;
		foreach (Component component in components)
		{
			component.DebugRender(camera);
		}
		LockMode = LockModes.Open;
	}

	internal void HandleGraphicsReset()
	{
		LockMode = LockModes.Error;
		foreach (Component component in components)
		{
			component.HandleGraphicsReset();
		}
		LockMode = LockModes.Open;
	}

	internal void HandleGraphicsCreate()
	{
		LockMode = LockModes.Error;
		foreach (Component component in components)
		{
			component.HandleGraphicsCreate();
		}
		LockMode = LockModes.Open;
	}

	public T Get<T>() where T : Component
	{
		foreach (Component component in components)
		{
			if (component is T)
			{
				return component as T;
			}
		}
		return null;
	}

	[IteratorStateMachine(typeof(_003CGetAll_003Ed__36<>))]
	public IEnumerable<T> GetAll<T>() where T : Component
	{
		foreach (Component component in components)
		{
			if (component is T)
			{
				yield return component as T;
			}
		}
	}
}
