using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000784 RID: 1924
	public class BuffSeidJsonData160 : IJSONClass
	{
		// Token: 0x06003C23 RID: 15395 RVA: 0x0019D5BC File Offset: 0x0019B7BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[160].list)
			{
				BuffSeidJsonData160 buffSeidJsonData = new BuffSeidJsonData160();
				buffSeidJsonData.id = jsonobject["id"].I;
				buffSeidJsonData.value1 = jsonobject["value1"].I;
				buffSeidJsonData.value2 = jsonobject["value2"].I;
				BuffSeidJsonData160.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				BuffSeidJsonData160.DataList.Add(buffSeidJsonData);
			}
		}

		// Token: 0x040035AE RID: 13742
		public static int SEIDID = 160;

		// Token: 0x040035AF RID: 13743
		public static Dictionary<int, BuffSeidJsonData160> DataDict = new Dictionary<int, BuffSeidJsonData160>();

		// Token: 0x040035B0 RID: 13744
		public static List<BuffSeidJsonData160> DataList = new List<BuffSeidJsonData160>();

		// Token: 0x040035B1 RID: 13745
		public int id;

		// Token: 0x040035B2 RID: 13746
		public int value1;

		// Token: 0x040035B3 RID: 13747
		public int value2;
	}
}
