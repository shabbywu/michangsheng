using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02000D3E RID: 3390
	internal static class TableConversions
	{
		// Token: 0x06005F74 RID: 24436 RVA: 0x0026B754 File Offset: 0x00269954
		internal static Table ConvertIListToTable(Script script, IList list)
		{
			Table table = new Table(script);
			for (int i = 0; i < list.Count; i++)
			{
				table[i + 1] = ClrToScriptConversions.ObjectToDynValue(script, list[i]);
			}
			return table;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x0026B798 File Offset: 0x00269998
		internal static Table ConvertIDictionaryToTable(Script script, IDictionary dict)
		{
			Table table = new Table(script);
			foreach (object obj in dict)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				DynValue key = ClrToScriptConversions.ObjectToDynValue(script, dictionaryEntry.Key);
				DynValue value = ClrToScriptConversions.ObjectToDynValue(script, dictionaryEntry.Value);
				table.Set(key, value);
			}
			return table;
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x0026B818 File Offset: 0x00269A18
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
			return t.IsArray && t.GetArrayRank() == 1;
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x0026B954 File Offset: 0x00269B54
		internal static object ConvertTableToType(Table table, Type t)
		{
			if (Framework.Do.IsAssignableFrom(t, typeof(Dictionary<object, object>)))
			{
				return TableConversions.TableToDictionary<object, object>(table, (DynValue v) => v.ToObject(), (DynValue v) => v.ToObject());
			}
			if (Framework.Do.IsAssignableFrom(t, typeof(Dictionary<DynValue, DynValue>)))
			{
				return TableConversions.TableToDictionary<DynValue, DynValue>(table, (DynValue v) => v, (DynValue v) => v);
			}
			if (Framework.Do.IsAssignableFrom(t, typeof(List<object>)))
			{
				return TableConversions.TableToList<object>(table, (DynValue v) => v.ToObject());
			}
			if (Framework.Do.IsAssignableFrom(t, typeof(List<DynValue>)))
			{
				return TableConversions.TableToList<DynValue>(table, (DynValue v) => v);
			}
			if (Framework.Do.IsAssignableFrom(t, typeof(object[])))
			{
				return TableConversions.TableToList<object>(table, (DynValue v) => v.ToObject()).ToArray();
			}
			if (Framework.Do.IsAssignableFrom(t, typeof(DynValue[])))
			{
				return TableConversions.TableToList<DynValue>(table, (DynValue v) => v).ToArray();
			}
			if (Framework.Do.IsGenericType(t))
			{
				Type genericTypeDefinition = t.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(List<>) || genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>))
				{
					return TableConversions.ConvertTableToListOfGenericType(t, Framework.Do.GetGenericArguments(t)[0], table);
				}
				if (genericTypeDefinition == typeof(Dictionary<, >) || genericTypeDefinition == typeof(IDictionary<, >))
				{
					return TableConversions.ConvertTableToDictionaryOfGenericType(t, Framework.Do.GetGenericArguments(t)[0], Framework.Do.GetGenericArguments(t)[1], table);
				}
			}
			if (t.IsArray && t.GetArrayRank() == 1)
			{
				return TableConversions.ConvertTableToArrayOfGenericType(t, t.GetElementType(), table);
			}
			return null;
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x0026BBF4 File Offset: 0x00269DF4
		internal static object ConvertTableToDictionaryOfGenericType(Type dictionaryType, Type keyType, Type valueType, Table table)
		{
			if (dictionaryType.GetGenericTypeDefinition() != typeof(Dictionary<, >))
			{
				dictionaryType = typeof(Dictionary<, >);
				dictionaryType = dictionaryType.MakeGenericType(new Type[]
				{
					keyType,
					valueType
				});
			}
			IDictionary dictionary = (IDictionary)Activator.CreateInstance(dictionaryType);
			foreach (TablePair tablePair in table.Pairs)
			{
				object key = ScriptToClrConversions.DynValueToObjectOfType(tablePair.Key, keyType, null, false);
				object value = ScriptToClrConversions.DynValueToObjectOfType(tablePair.Value, valueType, null, false);
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x0026BCAC File Offset: 0x00269EAC
		internal static object ConvertTableToArrayOfGenericType(Type arrayType, Type itemType, Table table)
		{
			List<object> list = new List<object>();
			int i = 1;
			int length = table.Length;
			while (i <= length)
			{
				object item = ScriptToClrConversions.DynValueToObjectOfType(table.Get(i), itemType, null, false);
				list.Add(item);
				i++;
			}
			IList list2 = (IList)Activator.CreateInstance(arrayType, new object[]
			{
				list.Count
			});
			for (int j = 0; j < list.Count; j++)
			{
				list2[j] = list[j];
			}
			return list2;
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x0026BD34 File Offset: 0x00269F34
		internal static object ConvertTableToListOfGenericType(Type listType, Type itemType, Table table)
		{
			if (listType.GetGenericTypeDefinition() != typeof(List<>))
			{
				listType = typeof(List<>);
				listType = listType.MakeGenericType(new Type[]
				{
					itemType
				});
			}
			IList list = (IList)Activator.CreateInstance(listType);
			int i = 1;
			int length = table.Length;
			while (i <= length)
			{
				object value = ScriptToClrConversions.DynValueToObjectOfType(table.Get(i), itemType, null, false);
				list.Add(value);
				i++;
			}
			return list;
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x0026BDB0 File Offset: 0x00269FB0
		internal static List<T> TableToList<T>(Table table, Func<DynValue, T> converter)
		{
			List<T> list = new List<T>();
			int i = 1;
			int length = table.Length;
			while (i <= length)
			{
				DynValue arg = table.Get(i);
				T item = converter(arg);
				list.Add(item);
				i++;
			}
			return list;
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x0026BDF0 File Offset: 0x00269FF0
		internal static Dictionary<TK, TV> TableToDictionary<TK, TV>(Table table, Func<DynValue, TK> keyconverter, Func<DynValue, TV> valconverter)
		{
			Dictionary<TK, TV> dictionary = new Dictionary<TK, TV>();
			foreach (TablePair tablePair in table.Pairs)
			{
				TK key = keyconverter(tablePair.Key);
				TV value = valconverter(tablePair.Value);
				dictionary.Add(key, value);
			}
			return dictionary;
		}
	}
}
