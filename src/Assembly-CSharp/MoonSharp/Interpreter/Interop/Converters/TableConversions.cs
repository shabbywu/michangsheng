using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02001147 RID: 4423
	internal static class TableConversions
	{
		// Token: 0x06006B46 RID: 27462 RVA: 0x00293574 File Offset: 0x00291774
		internal static Table ConvertIListToTable(Script script, IList list)
		{
			Table table = new Table(script);
			for (int i = 0; i < list.Count; i++)
			{
				table[i + 1] = ClrToScriptConversions.ObjectToDynValue(script, list[i]);
			}
			return table;
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x002935B8 File Offset: 0x002917B8
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

		// Token: 0x06006B48 RID: 27464 RVA: 0x00293638 File Offset: 0x00291838
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

		// Token: 0x06006B49 RID: 27465 RVA: 0x00293774 File Offset: 0x00291974
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

		// Token: 0x06006B4A RID: 27466 RVA: 0x00293A14 File Offset: 0x00291C14
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

		// Token: 0x06006B4B RID: 27467 RVA: 0x00293ACC File Offset: 0x00291CCC
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

		// Token: 0x06006B4C RID: 27468 RVA: 0x00293B54 File Offset: 0x00291D54
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

		// Token: 0x06006B4D RID: 27469 RVA: 0x00293BD0 File Offset: 0x00291DD0
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

		// Token: 0x06006B4E RID: 27470 RVA: 0x00293C10 File Offset: 0x00291E10
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
