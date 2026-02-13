using System;
using System.Collections.Generic;
using System.Reflection;

namespace Celeste;

public static class SpawnManager
{
	public static Dictionary<string, Spawn> SpawnActions = new Dictionary<string, Spawn>(StringComparer.InvariantCultureIgnoreCase);

	public static void Init()
	{
		Type[] types = Assembly.GetCallingAssembly().GetTypes();
		foreach (Type type in types)
		{
			if (CustomAttributeExtensions.GetCustomAttribute((MemberInfo)type, typeof(SpawnableAttribute)) == null)
			{
				continue;
			}
			MethodInfo[] methods = type.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				SpawnerAttribute spawnerAttribute = CustomAttributeExtensions.GetCustomAttribute((MemberInfo)methodInfo, typeof(SpawnerAttribute)) as SpawnerAttribute;
				if (methodInfo.IsStatic && spawnerAttribute != null)
				{
					string name = spawnerAttribute.Name;
					if (name == null)
					{
						name = type.Name;
					}
					SpawnActions.Add(name, (Spawn)methodInfo.CreateDelegate(typeof(Spawn)));
				}
			}
		}
	}
}
