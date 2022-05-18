using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C82 RID: 3202
	public class SkillSeidJsonData13 : IJSONClass
	{
		// Token: 0x06004D71 RID: 19825 RVA: 0x0020AA5C File Offset: 0x00208C5C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[13].list)
			{
				try
				{
					SkillSeidJsonData13 skillSeidJsonData = new SkillSeidJsonData13();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData13.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData13.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData13.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData13.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData13.OnInitFinishAction != null)
			{
				SkillSeidJsonData13.OnInitFinishAction();
			}
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CD4 RID: 19668
		public static int SEIDID = 13;

		// Token: 0x04004CD5 RID: 19669
		public static Dictionary<int, SkillSeidJsonData13> DataDict = new Dictionary<int, SkillSeidJsonData13>();

		// Token: 0x04004CD6 RID: 19670
		public static List<SkillSeidJsonData13> DataList = new List<SkillSeidJsonData13>();

		// Token: 0x04004CD7 RID: 19671
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData13.OnInitFinish);

		// Token: 0x04004CD8 RID: 19672
		public int skillid;

		// Token: 0x04004CD9 RID: 19673
		public int value1;

		// Token: 0x04004CDA RID: 19674
		public int value2;
	}
}
