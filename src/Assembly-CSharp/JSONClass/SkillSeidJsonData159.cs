using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200090E RID: 2318
	public class SkillSeidJsonData159 : IJSONClass
	{
		// Token: 0x0600424A RID: 16970 RVA: 0x001C5128 File Offset: 0x001C3328
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[159].list)
			{
				try
				{
					SkillSeidJsonData159 skillSeidJsonData = new SkillSeidJsonData159();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData159.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData159.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData159.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData159.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData159.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData159.OnInitFinishAction != null)
			{
				SkillSeidJsonData159.OnInitFinishAction();
			}
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400423A RID: 16954
		public static int SEIDID = 159;

		// Token: 0x0400423B RID: 16955
		public static Dictionary<int, SkillSeidJsonData159> DataDict = new Dictionary<int, SkillSeidJsonData159>();

		// Token: 0x0400423C RID: 16956
		public static List<SkillSeidJsonData159> DataList = new List<SkillSeidJsonData159>();

		// Token: 0x0400423D RID: 16957
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData159.OnInitFinish);

		// Token: 0x0400423E RID: 16958
		public int id;

		// Token: 0x0400423F RID: 16959
		public int target;

		// Token: 0x04004240 RID: 16960
		public int value1;
	}
}
