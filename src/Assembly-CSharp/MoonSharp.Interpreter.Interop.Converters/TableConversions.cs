using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.Converters;

internal static class TableConversions
{
	internal static Table ConvertIListToTable(Script script, IList list)
	{
		Table table = new Table(script);
		for (int i = 0; i < list.Count; i++)
		{
			table[i + 1] = ClrToScriptConversions.ObjectToDynValue(script, list[i]);
		}
		return table;
	}

	internal static Table ConvertIDictionaryToTable(Script script, IDictionary dict)
	{
		Table table = new Table(script);
		foreach (DictionaryEntry item in dict)
		{
			DynValue key = ClrToScriptConversions.ObjectToDynValue(script, item.Key);
			DynValue value = ClrToScriptConversions.ObjectToDynValue(script, item.Value);
			table.Set(key, value);
		}
		return table;
	}

	internal static bool CanConvertTableToType(Table table, Type t)
	{
		if (Framework.Do.IsAssignableFrom(t, typeof(Dictionary<object, object>)))
		{
			return true;
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(Dictionary<DynValue, DynValue>)))
		{
			return true;
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(List<object>)))
		{
			return true;
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(List<DynValue>)))
		{
			return true;
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(object[])))
		{
			return true;
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(DynValue[])))
		{
			return true;
		}
		if (Framework.Do.IsGenericType(t))
		{
			Type genericTypeDefinition = t.GetGenericTypeDefinition();
			if (genericTypeDefinition == typeof(List<>) || genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>))
			{
				return true;
			}
			if (genericTypeDefinition == typeof(Dictionary<, >) || genericTypeDefinition == typeof(IDictionary<, >))
			{
				return true;
			}
		}
		if (t.IsArray && t.GetArrayRank() == 1)
		{
			return true;
		}
		return false;
	}

	internal static object ConvertTableToType(Table table, Type t)
	{
		if (Framework.Do.IsAssignableFrom(t, typeof(Dictionary<object, object>)))
		{
			return TableToDictionary(table, (DynValue v) => v.ToObject(), (DynValue v) => v.ToObject());
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(Dictionary<DynValue, DynValue>)))
		{
			return TableToDictionary(table, (DynValue v) => v, (DynValue v) => v);
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(List<object>)))
		{
			return TableToList(table, (DynValue v) => v.ToObject());
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(List<DynValue>)))
		{
			return TableToList(table, (DynValue v) => v);
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(object[])))
		{
			return TableToList(table, (DynValue v) => v.ToObject()).ToArray();
		}
		if (Framework.Do.IsAssignableFrom(t, typeof(DynValue[])))
		{
			return TableToList(table, (DynValue v) => v).ToArray();
		}
		if (Framework.Do.IsGenericType(t))
		{
			Type genericTypeDefinition = t.GetGenericTypeDefinition();
			if (genericTypeDefinition == typeof(List<>) || genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>))
			{
				return ConvertTableToListOfGenericType(t, Framework.Do.GetGenericArguments(t)[0], table);
			}
			if (genericTypeDefinition == typeof(Dictionary<, >) || genericTypeDefinition == typeof(IDictionary<, >))
			{
				return ConvertTableToDictionaryOfGenericType(t, Framework.Do.GetGenericArguments(t)[0], Framework.Do.GetGenericArguments(t)[1], table);
			}
		}
		if (t.IsArray && t.GetArrayRank() == 1)
		{
			return ConvertTableToArrayOfGenericType(t, t.GetElementType(), table);
		}
		return null;
	}

	internal static object ConvertTableToDictionaryOfGenericType(Type dictionaryType, Type keyType, Type valueType, Table table)
	{
		if (dictionaryType.GetGenericTypeDefinition() != typeof(Dictionary<, >))
		{
			dictionaryType = typeof(Dictionary<, >);
			dictionaryType = dictionaryType.MakeGenericType(keyType, valueType);
		}
		IDictionary dictionary = (IDictionary)Activator.CreateInstance(dictionaryType);
		foreach (TablePair pair in table.Pairs)
		{
			object key = ScriptToClrConversions.DynValueToObjectOfType(pair.Key, keyType, null, isOptional: false);
			object value = ScriptToClrConversions.DynValueToObjectOfType(pair.Value, valueType, null, isOptional: false);
			dictionary.Add(key, value);
		}
		return dictionary;
	}

	internal static object ConvertTableToArrayOfGenericType(Type arrayType, Type itemType, Table table)
	{
		List<object> list = new List<object>();
		int i = 1;
		for (int length = table.Length; i <= length; i++)
		{
			object item = ScriptToClrConversions.DynValueToObjectOfType(table.Get(i), itemType, null, isOptional: false);
			list.Add(item);
		}
		IList list2 = (IList)Activator.CreateInstance(arrayType, list.Count);
		for (int j = 0; j < list.Count; j++)
		{
			list2[j] = list[j];
		}
		return list2;
	}

	internal static object ConvertTableToListOfGenericType(Type listType, Type itemType, Table table)
	{
		if (listType.GetGenericTypeDefinition() != typeof(List<>))
		{
			listType = typeof(List<>);
			listType = listType.MakeGenericType(itemType);
		}
		IList list = (IList)Activator.CreateInstance(listType);
		int i = 1;
		for (int length = table.Length; i <= length; i++)
		{
			object value = ScriptToClrConversions.DynValueToObjectOfType(table.Get(i), itemType, null, isOptional: false);
			list.Add(value);
		}
		return list;
	}

	internal static List<T> TableToList<T>(Table table, Func<DynValue, T> converter)
	{
		List<T> list = new List<T>();
		int i = 1;
		for (int length = table.Length; i <= length; i++)
		{
			DynValue arg = table.Get(i);
			T item = converter(arg);
			list.Add(item);
		}
		return list;
	}

	internal static Dictionary<TK, TV> TableToDictionary<TK, TV>(Table table, Func<DynValue, TK> keyconverter, Func<DynValue, TV> valconverter)
	{
		Dictionary<TK, TV> dictionary = new Dictionary<TK, TV>();
		foreach (TablePair pair in table.Pairs)
		{
			TK key = keyconverter(pair.Key);
			TV value = valconverter(pair.Value);
			dictionary.Add(key, value);
		}
		return dictionary;
	}
}
