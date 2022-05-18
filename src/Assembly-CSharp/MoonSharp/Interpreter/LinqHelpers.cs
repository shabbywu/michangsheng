using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001089 RID: 4233
	public static class LinqHelpers
	{
		// Token: 0x06006651 RID: 26193 RVA: 0x00283B9C File Offset: 0x00281D9C
		public static IEnumerable<T> Convert<T>(this IEnumerable<DynValue> enumerable, DataType type)
		{
			return from v in enumerable
			where v.Type == type
			select v.ToObject<T>();
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x00283BEC File Offset: 0x00281DEC
		public static IEnumerable<DynValue> OfDataType(this IEnumerable<DynValue> enumerable, DataType type)
		{
			return from v in enumerable
			where v.Type == type
			select v;
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x00046997 File Offset: 0x00044B97
		public static IEnumerable<object> AsObjects(this IEnumerable<DynValue> enumerable)
		{
			return from v in enumerable
			select v.ToObject();
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x000469BE File Offset: 0x00044BBE
		public static IEnumerable<T> AsObjects<T>(this IEnumerable<DynValue> enumerable)
		{
			return from v in enumerable
			select v.ToObject<T>();
		}
	}
}
