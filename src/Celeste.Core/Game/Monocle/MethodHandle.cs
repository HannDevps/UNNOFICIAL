using System.Reflection;

namespace Monocle;

public class MethodHandle<T> where T : Entity
{
	private MethodInfo info;

	public MethodHandle(string methodName)
	{
		info = typeof(T).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public);
	}

	public void Call(T instance)
	{
		info.Invoke(instance, null);
	}
}
