using System;
using System.Collections.Generic;
using System.Threading;
using Celeste.Core.Platform.Interop;
using Monocle;

namespace Celeste;

public static class RunThread
{
	private static List<Thread> threads = new List<Thread>();

	public static void Start(Action method, string name, bool highPriority = false)
	{
		CelestePathBridge.LogInfo("THREAD", $"Starting thread '{name}' highPriority={highPriority}");
		Thread thread = new Thread((ThreadStart)delegate
		{
			RunThreadWithLogging(method);
		});
		lock (threads)
		{
			threads.Add(thread);
		}
		thread.Name = name;
		thread.IsBackground = true;
		if (highPriority)
		{
			thread.Priority = ThreadPriority.Highest;
		}
		thread.Start();
	}

	private static void RunThreadWithLogging(Action method)
	{
		try
		{
			method();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
			string text = Thread.CurrentThread.Name ?? "unnamed";
			string text2 = BuildThreadContext(text);
			CelestePathBridge.LogError("THREAD", $"Unhandled exception in thread '{text}': {ex}");
			CelestePathBridge.LogError("THREAD", "THREAD_CONTEXT: " + text2);
			try
			{
				ErrorLog.Write(ex);
			}
			catch (Exception ex2)
			{
				CelestePathBridge.LogError("THREAD", "Failed to write error log: " + ex2);
			}

			if (!OperatingSystem.IsAndroid())
			{
				try
				{
					ErrorLog.Open();
				}
				catch (Exception ex3)
				{
					CelestePathBridge.LogWarn("THREAD", "Failed to open error log: " + ex3.Message);
				}
			}

			try
			{
				Engine.Instance?.Exit();
			}
			catch (Exception ex4)
			{
				CelestePathBridge.LogError("THREAD", "Failed to request engine exit after thread exception: " + ex4);
			}
		}
		finally
		{
			lock (threads)
			{
				threads.Remove(Thread.CurrentThread);
			}
		}
	}

	private static string BuildThreadContext(string threadName)
	{
		string text = Engine.Scene?.GetType().FullName ?? "null";
		return $"thread={threadName}; managedBytes={GC.GetTotalMemory(false)}; gc0={GC.CollectionCount(0)}; gc1={GC.CollectionCount(1)}; gc2={GC.CollectionCount(2)}; scene={text}";
	}

	public static void WaitAll()
	{
		while (true)
		{
			Thread thread;
			lock (threads)
			{
				if (threads.Count == 0)
				{
					break;
				}
				thread = threads[0];
			}
			thread.Join();
		}
	}
}
