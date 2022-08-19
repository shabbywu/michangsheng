using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000940 RID: 2368
	public class SkillSeidJsonData65 : IJSONClass
	{
		// Token: 0x06004312 RID: 17170 RVA: 0x001C982C File Offset: 0x001C7A2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[65].list)
			{
				try
				{
					SkillSeidJsonData65 skillSeidJsonData = new SkillSeidJsonData65();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData65.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData65.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData65.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData65.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData65.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData65.OnInitFinishAction != null)
			{
				SkillSeidJsonData65.OnInitFinishAction();
			}
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004388 RID: 17288
		public static int SEIDID = 65;

		// Token: 0x04004389 RID: 17289
		public static Dictionary<int, SkillSeidJsonData65> DataDict = new Dictionary<int, SkillSeidJsonData65>();

		// Token: 0x0400438A RID: 17290
		public static List<SkillSeidJsonData65> DataList = new List<SkillSeidJsonData65>();

		// Token: 0x0400438B RID: 17291
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData65.OnInitFinish);

		// Token: 0x0400438C RID: 17292
		public int skillid;

		// Token: 0x0400438D RID: 17293
		public int value1;
	}
}
