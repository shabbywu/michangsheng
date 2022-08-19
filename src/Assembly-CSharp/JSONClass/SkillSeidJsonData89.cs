using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000954 RID: 2388
	public class SkillSeidJsonData89 : IJSONClass
	{
		// Token: 0x06004362 RID: 17250 RVA: 0x001CB4C8 File Offset: 0x001C96C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[89].list)
			{
				try
				{
					SkillSeidJsonData89 skillSeidJsonData = new SkillSeidJsonData89();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData89.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData89.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData89.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData89.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData89.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData89.OnInitFinishAction != null)
			{
				SkillSeidJsonData89.OnInitFinishAction();
			}
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400440F RID: 17423
		public static int SEIDID = 89;

		// Token: 0x04004410 RID: 17424
		public static Dictionary<int, SkillSeidJsonData89> DataDict = new Dictionary<int, SkillSeidJsonData89>();

		// Token: 0x04004411 RID: 17425
		public static List<SkillSeidJsonData89> DataList = new List<SkillSeidJsonData89>();

		// Token: 0x04004412 RID: 17426
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData89.OnInitFinish);

		// Token: 0x04004413 RID: 17427
		public int skillid;

		// Token: 0x04004414 RID: 17428
		public int value1;
	}
}
