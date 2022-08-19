using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x02000693 RID: 1683
	internal static class ListPool<T>
	{
		// Token: 0x0600353F RID: 13631 RVA: 0x00170636 File Offset: 0x0016E836
		public static List<T> Get()
		{
			return ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x00170642 File Offset: 0x0016E842
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x04002EF3 RID: 12019
		private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});
	}
}
