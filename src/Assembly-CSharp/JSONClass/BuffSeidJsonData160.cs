using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B1C RID: 2844
	public class BuffSeidJsonData160 : IJSONClass
	{
		// Token: 0x060047D9 RID: 18393 RVA: 0x001EABDC File Offset: 0x001E8DDC
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

		// Token: 0x04004147 RID: 16711
		public static int SEIDID = 160;

		// Token: 0x04004148 RID: 16712
		public static Dictionary<int, BuffSeidJsonData160> DataDict = new Dictionary<int, BuffSeidJsonData160>();

		// Token: 0x04004149 RID: 16713
		public static List<BuffSeidJsonData160> DataList = new List<BuffSeidJsonData160>();

		// Token: 0x0400414A RID: 16714
		public int id;

		// Token: 0x0400414B RID: 16715
		public int value1;

		// Token: 0x0400414C RID: 16716
		public int value2;
	}
}
