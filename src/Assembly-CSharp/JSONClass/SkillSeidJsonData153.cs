using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C92 RID: 3218
	public class SkillSeidJsonData153 : IJSONClass
	{
		// Token: 0x06004DB1 RID: 19889 RVA: 0x0020BEC4 File Offset: 0x0020A0C4
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

		// Token: 0x04004D45 RID: 19781
		public static int SEIDID = 153;

		// Token: 0x04004D46 RID: 19782
		public static Dictionary<int, SkillSeidJsonData153> DataDict = new Dictionary<int, SkillSeidJsonData153>();

		// Token: 0x04004D47 RID: 19783
		public static List<SkillSeidJsonData153> DataList = new List<SkillSeidJsonData153>();

		// Token: 0x04004D48 RID: 19784
		public int id;

		// Token: 0x04004D49 RID: 19785
		public int target;

		// Token: 0x04004D4A RID: 19786
		public int value1;

		// Token: 0x04004D4B RID: 19787
		public int value2;
	}
}
