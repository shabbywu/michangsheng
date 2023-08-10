using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter;

internal static class Extension_Methods
{
	public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
	{
		if (dictionary.TryGetValue(key, out var value))
		{
			return value;
		}
		return default(TValue);
	}

	public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> creator)
	{
		if (!dictionary.TryGetValue(key, out var value))
		{
			value = creator();
			dictionary.Add(key, value);
		}
		return value;
	}
}
