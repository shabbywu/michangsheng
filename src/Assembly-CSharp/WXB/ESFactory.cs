using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020009A3 RID: 2467
	public class ESFactory
	{
		// Token: 0x06003EE4 RID: 16100 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
		static ESFactory()
		{
			ESFactory.TypeList.Add("Default", new DefaultES());
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0002D410 File Offset: 0x0002B610
		public static void Add(string name, ElementSegment es)
		{
			ESFactory.TypeList.Add(name, es);
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x0002D41E File Offset: 0x0002B61E
		public static bool Remove(string name)
		{
			return ESFactory.TypeList.Remove(name);
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x001B85E8 File Offset: 0x001B67E8
		public static ElementSegment Get(string name)
		{
			ElementSegment result = null;
			if (ESFactory.TypeList.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x001B860C File Offset: 0x001B680C
		public static List<string> GetAllName()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, ElementSegment> keyValuePair in ESFactory.TypeList)
			{
				list.Add(keyValuePair.Key);
			}
			return list;
		}

		// Token: 0x040038A3 RID: 14499
		private static Dictionary<string, ElementSegment> TypeList = new Dictionary<string, ElementSegment>();
	}
}
