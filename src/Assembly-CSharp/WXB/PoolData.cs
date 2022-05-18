using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020009AE RID: 2478
	internal class PoolData<T> where T : new()
	{
		// Token: 0x06003F2A RID: 16170 RVA: 0x0002D620 File Offset: 0x0002B820
		public static T Get()
		{
			if (PoolData<T>.bufs.Count == 0)
			{
				return Activator.CreateInstance<T>();
			}
			T result = PoolData<T>.bufs[PoolData<T>.bufs.Count - 1];
			PoolData<T>.bufs.RemoveAt(PoolData<T>.bufs.Count - 1);
			return result;
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x0002D660 File Offset: 0x0002B860
		public static void Free(T t)
		{
			PoolData<T>.bufs.Add(t);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x001B8CBC File Offset: 0x001B6EBC
		public static void FreeList(List<T> list, Action<T> fun)
		{
			for (int i = 0; i < list.Count; i++)
			{
				fun(list[i]);
			}
			PoolData<T>.bufs.AddRange(list);
			list.Clear();
		}

		// Token: 0x040038BC RID: 14524
		public static List<T> bufs = new List<T>();
	}
}
