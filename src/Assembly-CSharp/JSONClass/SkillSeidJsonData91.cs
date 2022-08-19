using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000957 RID: 2391
	public class SkillSeidJsonData91 : IJSONClass
	{
		// Token: 0x0600436E RID: 17262 RVA: 0x001CB8E4 File Offset: 0x001C9AE4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[91].list)
			{
				try
				{
					SkillSeidJsonData91 skillSeidJsonData = new SkillSeidJsonData91();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData91.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData91.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData91.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData91.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData91.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData91.OnInitFinishAction != null)
			{
				SkillSeidJsonData91.OnInitFinishAction();
			}
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004422 RID: 17442
		public static int SEIDID = 91;

		// Token: 0x04004423 RID: 17443
		public static Dictionary<int, SkillSeidJsonData91> DataDict = new Dictionary<int, SkillSeidJsonData91>();

		// Token: 0x04004424 RID: 17444
		public static List<SkillSeidJsonData91> DataList = new List<SkillSeidJsonData91>();

		// Token: 0x04004425 RID: 17445
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData91.OnInitFinish);

		// Token: 0x04004426 RID: 17446
		public int skillid;

		// Token: 0x04004427 RID: 17447
		public int value1;

		// Token: 0x04004428 RID: 17448
		public int value2;
	}
}
