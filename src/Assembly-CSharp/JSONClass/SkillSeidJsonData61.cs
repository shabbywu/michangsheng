using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200093C RID: 2364
	public class SkillSeidJsonData61 : IJSONClass
	{
		// Token: 0x06004302 RID: 17154 RVA: 0x001C92B8 File Offset: 0x001C74B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[61].list)
			{
				try
				{
					SkillSeidJsonData61 skillSeidJsonData = new SkillSeidJsonData61();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData61.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData61.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData61.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData61.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData61.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData61.OnInitFinishAction != null)
			{
				SkillSeidJsonData61.OnInitFinishAction();
			}
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400436F RID: 17263
		public static int SEIDID = 61;

		// Token: 0x04004370 RID: 17264
		public static Dictionary<int, SkillSeidJsonData61> DataDict = new Dictionary<int, SkillSeidJsonData61>();

		// Token: 0x04004371 RID: 17265
		public static List<SkillSeidJsonData61> DataList = new List<SkillSeidJsonData61>();

		// Token: 0x04004372 RID: 17266
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData61.OnInitFinish);

		// Token: 0x04004373 RID: 17267
		public int skillid;

		// Token: 0x04004374 RID: 17268
		public int value1;
	}
}
