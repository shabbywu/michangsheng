using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000B19 RID: 2841
	public class BuffSeidJsonData157 : IJSONClass
	{
		// Token: 0x060047CE RID: 18382 RVA: 0x001EA850 File Offset: 0x001E8A50
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

		// Token: 0x04004131 RID: 16689
		public static int SEIDID = 157;

		// Token: 0x04004132 RID: 16690
		public static Dictionary<int, BuffSeidJsonData157> DataDict = new Dictionary<int, BuffSeidJsonData157>();

		// Token: 0x04004133 RID: 16691
		public static List<BuffSeidJsonData157> DataList = new List<BuffSeidJsonData157>();

		// Token: 0x04004134 RID: 16692
		public int id;

		// Token: 0x04004135 RID: 16693
		public int target;

		// Token: 0x04004136 RID: 16694
		public int value1;

		// Token: 0x04004137 RID: 16695
		public int value2;
	}
}
