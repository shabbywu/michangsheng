using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000781 RID: 1921
	public class BuffSeidJsonData157 : IJSONClass
	{
		// Token: 0x06003C18 RID: 15384 RVA: 0x0019D1A8 File Offset: 0x0019B3A8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BuffSeidJsonData[157].list)
			{
				BuffSeidJsonData157 buffSeidJsonData = new BuffSeidJsonData157();
				buffSeidJsonData.id = jsonobject["id"].I;
				buffSeidJsonData.target = jsonobject["target"].I;
				buffSeidJsonData.value1 = jsonobject["value1"].I;
				buffSeidJsonData.value2 = jsonobject["value2"].I;
				BuffSeidJsonData157.DataDict.Add(buffSeidJsonData.id, buffSeidJsonData);
				BuffSeidJsonData157.DataList.Add(buffSeidJsonData);
			}
		}

		// Token: 0x04003598 RID: 13720
		public static int SEIDID = 157;

		// Token: 0x04003599 RID: 13721
		public static Dictionary<int, BuffSeidJsonData157> DataDict = new Dictionary<int, BuffSeidJsonData157>();

		// Token: 0x0400359A RID: 13722
		public static List<BuffSeidJsonData157> DataList = new List<BuffSeidJsonData157>();

		// Token: 0x0400359B RID: 13723
		public int id;

		// Token: 0x0400359C RID: 13724
		public int target;

		// Token: 0x0400359D RID: 13725
		public int value1;

		// Token: 0x0400359E RID: 13726
		public int value2;
	}
}
