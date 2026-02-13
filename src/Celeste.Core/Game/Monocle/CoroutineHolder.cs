using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Monocle;

public class CoroutineHolder : Component
{
	private class CoroutineData
	{
		public int ID;

		public Stack<IEnumerator> Data;

		public CoroutineData(int id, IEnumerator functionCall)
		{
			ID = id;
			Data = new Stack<IEnumerator>();
			Data.Push(functionCall);
		}
	}

	[CompilerGenerated]
	private sealed class _003CWaitForFrames_003Ed__8 : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int frames;

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
		public _003CWaitForFrames_003Ed__8(int _003C_003E1__state)
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
				_003Ci_003E5__2 = 0;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ci_003E5__2++;
				break;
			}
			if (_003Ci_003E5__2 < frames)
			{
				_003C_003E2__current = 0;
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

	private List<CoroutineData> coroutineList;

	private HashSet<CoroutineData> toRemove;

	private int nextID;

	private bool isRunning;

	public CoroutineHolder()
		: base(active: true, visible: false)
	{
		coroutineList = new List<CoroutineData>();
		toRemove = new HashSet<CoroutineData>();
	}

	public override void Update()
	{
		isRunning = true;
		for (int i = 0; i < coroutineList.Count; i++)
		{
			IEnumerator enumerator = coroutineList[i].Data.Peek();
			if (enumerator.MoveNext())
			{
				if (enumerator.Current is IEnumerator)
				{
					coroutineList[i].Data.Push(enumerator.Current as IEnumerator);
				}
				continue;
			}
			coroutineList[i].Data.Pop();
			if (coroutineList[i].Data.Count == 0)
			{
				toRemove.Add(coroutineList[i]);
			}
		}
		isRunning = false;
		if (toRemove.Count <= 0)
		{
			return;
		}
		foreach (CoroutineData item in toRemove)
		{
			coroutineList.Remove(item);
		}
		toRemove.Clear();
	}

	public void EndCoroutine(int id)
	{
		foreach (CoroutineData coroutine in coroutineList)
		{
			if (coroutine.ID == id)
			{
				if (isRunning)
				{
					toRemove.Add(coroutine);
				}
				else
				{
					coroutineList.Remove(coroutine);
				}
				break;
			}
		}
	}

	public int StartCoroutine(IEnumerator functionCall)
	{
		CoroutineData coroutineData = new CoroutineData(nextID++, functionCall);
		coroutineList.Add(coroutineData);
		return coroutineData.ID;
	}

	[IteratorStateMachine(typeof(_003CWaitForFrames_003Ed__8))]
	public static IEnumerator WaitForFrames(int frames)
	{
		for (int i = 0; i < frames; i++)
		{
			yield return 0;
		}
	}
}
