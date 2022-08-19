using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x02000690 RID: 1680
	public class ESFactory
	{
		// Token: 0x06003529 RID: 13609 RVA: 0x001702BC File Offset: 0x0016E4BC
		static ESFactory()
		{
			ESFactory.TypeList.Add("Default", new DefaultES());
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x001702DC File Offset: 0x0016E4DC
		public static void Add(string name, ElementSegment es)
		{
			ESFactory.TypeList.Add(name, es);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x001702EA File Offset: 0x0016E4EA
		public static bool Remove(string name)
		{
			return ESFactory.TypeList.Remove(name);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x001702F8 File Offset: 0x0016E4F8
		public static ElementSegment Get(string name)
		{
			ElementSegment result = null;
			if (ESFactory.TypeList.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0017031C File Offset: 0x0016E51C
		public static List<string> GetAllName()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, ElementSegment> keyValuePair in ESFactory.TypeList)
			{
				list.Add(keyValuePair.Key);
			}
			return list;
		}

		// Token: 0x04002EEB RID: 12011
		private static Dictionary<string, ElementSegment> TypeList = new Dictionary<string, ElementSegment>();
	}
}
