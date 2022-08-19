using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200093F RID: 2367
	public class SkillSeidJsonData64 : IJSONClass
	{
		// Token: 0x0600430E RID: 17166 RVA: 0x001C96D4 File Offset: 0x001C78D4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[64].list)
			{
				try
				{
					SkillSeidJsonData64 skillSeidJsonData = new SkillSeidJsonData64();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData64.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData64.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData64.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData64.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData64.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData64.OnInitFinishAction != null)
			{
				SkillSeidJsonData64.OnInitFinishAction();
			}
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004382 RID: 17282
		public static int SEIDID = 64;

		// Token: 0x04004383 RID: 17283
		public static Dictionary<int, SkillSeidJsonData64> DataDict = new Dictionary<int, SkillSeidJsonData64>();

		// Token: 0x04004384 RID: 17284
		public static List<SkillSeidJsonData64> DataList = new List<SkillSeidJsonData64>();

		// Token: 0x04004385 RID: 17285
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData64.OnInitFinish);

		// Token: 0x04004386 RID: 17286
		public int skillid;

		// Token: 0x04004387 RID: 17287
		public int value1;
	}
}
