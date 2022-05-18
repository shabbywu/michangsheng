using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020009A6 RID: 2470
	internal static class ListPool<T>
	{
		// Token: 0x06003EFA RID: 16122 RVA: 0x0002D4D6 File Offset: 0x0002B6D6
		public static List<T> Get()
		{
			return ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x0002D4E2 File Offset: 0x0002B6E2
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x040038AB RID: 14507
		private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
