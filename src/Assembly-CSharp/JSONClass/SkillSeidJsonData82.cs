using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200094D RID: 2381
	public class SkillSeidJsonData82 : IJSONClass
	{
		// Token: 0x06004346 RID: 17222 RVA: 0x001CAACC File Offset: 0x001C8CCC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[82].list)
			{
				try
				{
					SkillSeidJsonData82 skillSeidJsonData = new SkillSeidJsonData82();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData82.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData82.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData82.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData82.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData82.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData82.OnInitFinishAction != null)
			{
				SkillSeidJsonData82.OnInitFinishAction();
			}
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043DF RID: 17375
		public static int SEIDID = 82;

		// Token: 0x040043E0 RID: 17376
		public static Dictionary<int, SkillSeidJsonData82> DataDict = new Dictionary<int, SkillSeidJsonData82>();

		// Token: 0x040043E1 RID: 17377
		public static List<SkillSeidJsonData82> DataList = new List<SkillSeidJsonData82>();

		// Token: 0x040043E2 RID: 17378
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData82.OnInitFinish);

		// Token: 0x040043E3 RID: 17379
		public int skillid;

		// Token: 0x040043E4 RID: 17380
		public int value1;

		// Token: 0x040043E5 RID: 17381
		public int value2;
	}
}
