using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200090B RID: 2315
	public class SkillSeidJsonData153 : IJSONClass
	{
		// Token: 0x0600423F RID: 16959 RVA: 0x001C4CE4 File Offset: 0x001C2EE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[153].list)
			{
				SkillSeidJsonData153 skillSeidJsonData = new SkillSeidJsonData153();
				skillSeidJsonData.id = jsonobject["id"].I;
				skillSeidJsonData.target = jsonobject["target"].I;
				skillSeidJsonData.value1 = jsonobject["value1"].I;
				skillSeidJsonData.value2 = jsonobject["value2"].I;
				SkillSeidJsonData153.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
				SkillSeidJsonData153.DataList.Add(skillSeidJsonData);
			}
		}

		// Token: 0x04004223 RID: 16931
		public static int SEIDID = 153;

		// Token: 0x04004224 RID: 16932
		public static Dictionary<int, SkillSeidJsonData153> DataDict = new Dictionary<int, SkillSeidJsonData153>();

		// Token: 0x04004225 RID: 16933
		public static List<SkillSeidJsonData153> DataList = new List<SkillSeidJsonData153>();

		// Token: 0x04004226 RID: 16934
		public int id;

		// Token: 0x04004227 RID: 16935
		public int target;

		// Token: 0x04004228 RID: 16936
		public int value1;

		// Token: 0x04004229 RID: 16937
		public int value2;
	}
}
