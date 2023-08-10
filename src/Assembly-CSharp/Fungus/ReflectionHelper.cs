using System;
using System.Collections.Generic;

namespace Fungus;

public static class ReflectionHelper
{
	private static Dictionary<string, Type> types = new Dictionary<string, Type>();

	public static Type GetType(string typeName)
	{
		if (types.ContainsKey(typeName))
		{
			return types[typeName];
		}
		types[typeName] = Type.GetType(typeName);
		return types[typeName];
	}
}
