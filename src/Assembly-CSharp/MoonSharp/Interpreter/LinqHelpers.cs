using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CBA RID: 3258
	public static class LinqHelpers
	{
		// Token: 0x06005B5F RID: 23391 RVA: 0x00259E34 File Offset: 0x00258034
		public static IEnumerable<T> Convert<T>(this IEnumerable<DynValue> enumerable, DataType type)
		{
			return from v in enumerable
			where v.Type == type
			select v.ToObject<T>();
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x00259E84 File Offset: 0x00258084
		public static IEnumerable<DynValue> OfDataType(this IEnumerable<DynValue> enumerable, DataType type)
		{
			return from v in enumerable
			where v.Type == type
			select v;
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x00259EB0 File Offset: 0x002580B0
		public static IEnumerable<object> AsObjects(this IEnumerable<DynValue> enumerable)
		{
			return from v in enumerable
			select v.ToObject();
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00259ED7 File Offset: 0x002580D7
		public static IEnumerable<T> AsObjects<T>(this IEnumerable<DynValue> enumerable)
		{
			return from v in enumerable
			select v.ToObject<T>();
		}
	}
}
