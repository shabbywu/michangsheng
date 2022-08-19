using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000959 RID: 2393
	public class SkillSeidJsonData94 : IJSONClass
	{
		// Token: 0x06004376 RID: 17270 RVA: 0x001CBBBC File Offset: 0x001C9DBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[94].list)
			{
				try
				{
					SkillSeidJsonData94 skillSeidJsonData = new SkillSeidJsonData94();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData94.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData94.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData94.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData94.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData94.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData94.OnInitFinishAction != null)
			{
				SkillSeidJsonData94.OnInitFinishAction();
			}
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004430 RID: 17456
		public static int SEIDID = 94;

		// Token: 0x04004431 RID: 17457
		public static Dictionary<int, SkillSeidJsonData94> DataDict = new Dictionary<int, SkillSeidJsonData94>();

		// Token: 0x04004432 RID: 17458
		public static List<SkillSeidJsonData94> DataList = new List<SkillSeidJsonData94>();

		// Token: 0x04004433 RID: 17459
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData94.OnInitFinish);

		// Token: 0x04004434 RID: 17460
		public int skillid;

		// Token: 0x04004435 RID: 17461
		public int value1;

		// Token: 0x04004436 RID: 17462
		public int value2;
	}
}
