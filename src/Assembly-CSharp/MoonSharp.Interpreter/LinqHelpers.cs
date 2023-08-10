using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter;

public static class LinqHelpers
{
	public static IEnumerable<T> Convert<T>(this IEnumerable<DynValue> enumerable, DataType type)
	{
		return from v in enumerable
			where v.Type == type
			select v.ToObject<T>();
	}

	public static IEnumerable<DynValue> OfDataType(this IEnumerable<DynValue> enumerable, DataType type)
	{
		return enumerable.Where((DynValue v) => v.Type == type);
	}

	public static IEnumerable<object> AsObjects(this IEnumerable<DynValue> enumerable)
	{
		return enumerable.Select((DynValue v) => v.ToObject());
	}

	public static IEnumerable<T> AsObjects<T>(this IEnumerable<DynValue> enumerable)
	{
		return enumerable.Select((DynValue v) => v.ToObject<T>());
	}
}
