using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200091A RID: 2330
	public class SkillSeidJsonData27 : IJSONClass
	{
		// Token: 0x0600427A RID: 17018 RVA: 0x001C6208 File Offset: 0x001C4408
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[27].list)
			{
				try
				{
					SkillSeidJsonData27 skillSeidJsonData = new SkillSeidJsonData27();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData27.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData27.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData27.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData27.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData27.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData27.OnInitFinishAction != null)
			{
				SkillSeidJsonData27.OnInitFinishAction();
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004288 RID: 17032
		public static int SEIDID = 27;

		// Token: 0x04004289 RID: 17033
		public static Dictionary<int, SkillSeidJsonData27> DataDict = new Dictionary<int, SkillSeidJsonData27>();

		// Token: 0x0400428A RID: 17034
		public static List<SkillSeidJsonData27> DataList = new List<SkillSeidJsonData27>();

		// Token: 0x0400428B RID: 17035
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData27.OnInitFinish);

		// Token: 0x0400428C RID: 17036
		public int skillid;

		// Token: 0x0400428D RID: 17037
		public int value1;
	}
}
