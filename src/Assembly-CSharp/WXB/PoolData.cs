using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x0200069A RID: 1690
	internal class PoolData<T> where T : new()
	{
		// Token: 0x0600356C RID: 13676 RVA: 0x00170BA7 File Offset: 0x0016EDA7
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

		// Token: 0x0600356D RID: 13677 RVA: 0x00170BE7 File Offset: 0x0016EDE7
		public static void Free(T t)
		{
			PoolData<T>.bufs.Add(t);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x00170BF4 File Offset: 0x0016EDF4
		public static void FreeList(List<T> list, Action<T> fun)
		{
			for (int i = 0; i < list.Count; i++)
			{
				fun(list[i]);
			}
			PoolData<T>.bufs.AddRange(list);
			list.Clear();
		}

		// Token: 0x04002F03 RID: 12035
		public static List<T> bufs = new List<T>();
	}
}
