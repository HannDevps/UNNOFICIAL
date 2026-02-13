using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Monocle;

public class Tracker
{
	[CompilerGenerated]
	private sealed class _003CEnumerateEntities_003Ed__33<T> : IEnumerator<T>, IDisposable, IEnumerator where T : Entity
	{
		private int _003C_003E1__state;

		private T _003C_003E2__current;

		public Tracker _003C_003E4__this;

		private List<Entity>.Enumerator _003C_003E7__wrap1;

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
		public _003CEnumerateEntities_003Ed__33(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
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
				Tracker tracker = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E7__wrap1 = tracker.Entities[typeof(T)].GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				if (_003C_003E7__wrap1.MoveNext())
				{
					Entity current = _003C_003E7__wrap1.Current;
					_003C_003E2__current = current as T;
					_003C_003E1__state = 1;
					return true;
				}
				_003C_003Em__Finally1();
				_003C_003E7__wrap1 = default(List<Entity>.Enumerator);
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
	}

	[CompilerGenerated]
	private sealed class _003CEnumerateComponents_003Ed__39<T> : IEnumerator<T>, IDisposable, IEnumerator where T : Component
	{
		private int _003C_003E1__state;

		private T _003C_003E2__current;

		public Tracker _003C_003E4__this;

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
		public _003CEnumerateComponents_003Ed__39(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
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
				Tracker tracker = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003C_003E7__wrap1 = tracker.Components[typeof(T)].GetEnumerator();
					_003C_003E1__state = -3;
					break;
				case 1:
					_003C_003E1__state = -3;
					break;
				}
				if (_003C_003E7__wrap1.MoveNext())
				{
					Component current = _003C_003E7__wrap1.Current;
					_003C_003E2__current = current as T;
					_003C_003E1__state = 1;
					return true;
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
	}

	public static Dictionary<Type, List<Type>> TrackedEntityTypes { get; private set; }

	public static Dictionary<Type, List<Type>> TrackedComponentTypes { get; private set; }

	public static HashSet<Type> StoredEntityTypes { get; private set; }

	public static HashSet<Type> StoredComponentTypes { get; private set; }

	public Dictionary<Type, List<Entity>> Entities { get; private set; }

	public Dictionary<Type, List<Component>> Components { get; private set; }

	public static void Initialize()
	{
		TrackedEntityTypes = new Dictionary<Type, List<Type>>();
		TrackedComponentTypes = new Dictionary<Type, List<Type>>();
		StoredEntityTypes = new HashSet<Type>();
		StoredComponentTypes = new HashSet<Type>();
		Type[] types = Assembly.GetExecutingAssembly().GetTypes();
		foreach (Type type in types)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(Tracked), inherit: false);
			if (customAttributes.Length == 0)
			{
				continue;
			}
			bool inherited = (customAttributes[0] as Tracked).Inherited;
			if (typeof(Entity).IsAssignableFrom(type))
			{
				if (!type.IsAbstract)
				{
					if (!TrackedEntityTypes.ContainsKey(type))
					{
						TrackedEntityTypes.Add(type, new List<Type>());
					}
					TrackedEntityTypes[type].Add(type);
				}
				StoredEntityTypes.Add(type);
				if (!inherited)
				{
					continue;
				}
				foreach (Type subclass in GetSubclasses(type))
				{
					if (!subclass.IsAbstract)
					{
						if (!TrackedEntityTypes.ContainsKey(subclass))
						{
							TrackedEntityTypes.Add(subclass, new List<Type>());
						}
						TrackedEntityTypes[subclass].Add(type);
					}
				}
				continue;
			}
			if (typeof(Component).IsAssignableFrom(type))
			{
				if (!type.IsAbstract)
				{
					if (!TrackedComponentTypes.ContainsKey(type))
					{
						TrackedComponentTypes.Add(type, new List<Type>());
					}
					TrackedComponentTypes[type].Add(type);
				}
				StoredComponentTypes.Add(type);
				if (!inherited)
				{
					continue;
				}
				foreach (Type subclass2 in GetSubclasses(type))
				{
					if (!subclass2.IsAbstract)
					{
						if (!TrackedComponentTypes.ContainsKey(subclass2))
						{
							TrackedComponentTypes.Add(subclass2, new List<Type>());
						}
						TrackedComponentTypes[subclass2].Add(type);
					}
				}
				continue;
			}
			throw new Exception("Type '" + type.Name + "' cannot be Tracked because it does not derive from Entity or Component");
		}
	}

	private static List<Type> GetSubclasses(Type type)
	{
		List<Type> list = new List<Type>();
		Type[] types = Assembly.GetExecutingAssembly().GetTypes();
		foreach (Type type2 in types)
		{
			if (type != type2 && type.IsAssignableFrom(type2))
			{
				list.Add(type2);
			}
		}
		return list;
	}

	public Tracker()
	{
		Entities = new Dictionary<Type, List<Entity>>(TrackedEntityTypes.Count);
		foreach (Type storedEntityType in StoredEntityTypes)
		{
			Entities.Add(storedEntityType, new List<Entity>());
		}
		Components = new Dictionary<Type, List<Component>>(TrackedComponentTypes.Count);
		foreach (Type storedComponentType in StoredComponentTypes)
		{
			Components.Add(storedComponentType, new List<Component>());
		}
	}

	public bool IsEntityTracked<T>() where T : Entity
	{
		return Entities.ContainsKey(typeof(T));
	}

	public bool IsComponentTracked<T>() where T : Component
	{
		return Components.ContainsKey(typeof(T));
	}

	public T GetEntity<T>() where T : Entity
	{
		List<Entity> list = Entities[typeof(T)];
		if (list.Count == 0)
		{
			return null;
		}
		return list[0] as T;
	}

	public T GetNearestEntity<T>(Vector2 nearestTo) where T : Entity
	{
		List<Entity> entities = GetEntities<T>();
		T val = null;
		float num = 0f;
		foreach (T item in entities)
		{
			float num2 = Vector2.DistanceSquared(nearestTo, item.Position);
			if (val == null || num2 < num)
			{
				val = item;
				num = num2;
			}
		}
		return val;
	}

	public List<Entity> GetEntities<T>() where T : Entity
	{
		return Entities[typeof(T)];
	}

	public List<Entity> GetEntitiesCopy<T>() where T : Entity
	{
		return new List<Entity>(GetEntities<T>());
	}

	[IteratorStateMachine(typeof(_003CEnumerateEntities_003Ed__33<>))]
	public IEnumerator<T> EnumerateEntities<T>() where T : Entity
	{
		foreach (Entity item in Entities[typeof(T)])
		{
			yield return item as T;
		}
	}

	public int CountEntities<T>() where T : Entity
	{
		return Entities[typeof(T)].Count;
	}

	public T GetComponent<T>() where T : Component
	{
		List<Component> list = Components[typeof(T)];
		if (list.Count == 0)
		{
			return null;
		}
		return list[0] as T;
	}

	public T GetNearestComponent<T>(Vector2 nearestTo) where T : Component
	{
		List<Component> components = GetComponents<T>();
		T val = null;
		float num = 0f;
		foreach (T item in components)
		{
			float num2 = Vector2.DistanceSquared(nearestTo, item.Entity.Position);
			if (val == null || num2 < num)
			{
				val = item;
				num = num2;
			}
		}
		return val;
	}

	public List<Component> GetComponents<T>() where T : Component
	{
		return Components[typeof(T)];
	}

	public List<Component> GetComponentsCopy<T>() where T : Component
	{
		return new List<Component>(GetComponents<T>());
	}

	[IteratorStateMachine(typeof(_003CEnumerateComponents_003Ed__39<>))]
	public IEnumerator<T> EnumerateComponents<T>() where T : Component
	{
		foreach (Component item in Components[typeof(T)])
		{
			yield return item as T;
		}
	}

	public int CountComponents<T>() where T : Component
	{
		return Components[typeof(T)].Count;
	}

	internal void EntityAdded(Entity entity)
	{
		Type type = entity.GetType();
		if (!TrackedEntityTypes.TryGetValue(type, out var value))
		{
			return;
		}
		foreach (Type item in value)
		{
			Entities[item].Add(entity);
		}
	}

	internal void EntityRemoved(Entity entity)
	{
		Type type = entity.GetType();
		if (!TrackedEntityTypes.TryGetValue(type, out var value))
		{
			return;
		}
		foreach (Type item in value)
		{
			Entities[item].Remove(entity);
		}
	}

	internal void ComponentAdded(Component component)
	{
		Type type = component.GetType();
		if (!TrackedComponentTypes.TryGetValue(type, out var value))
		{
			return;
		}
		foreach (Type item in value)
		{
			Components[item].Add(component);
		}
	}

	internal void ComponentRemoved(Component component)
	{
		Type type = component.GetType();
		if (!TrackedComponentTypes.TryGetValue(type, out var value))
		{
			return;
		}
		foreach (Type item in value)
		{
			Components[item].Remove(component);
		}
	}

	public void LogEntities()
	{
		foreach (KeyValuePair<Type, List<Entity>> entity in Entities)
		{
			string obj = entity.Key.Name + " : " + entity.Value.Count;
			Engine.Commands.Log(obj);
		}
	}

	public void LogComponents()
	{
		foreach (KeyValuePair<Type, List<Component>> component in Components)
		{
			string obj = component.Key.Name + " : " + component.Value.Count;
			Engine.Commands.Log(obj);
		}
	}
}
