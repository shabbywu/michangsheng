using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200095D RID: 2397
	public class SkillSeidJsonData98 : IJSONClass
	{
		// Token: 0x06004386 RID: 17286 RVA: 0x001CC130 File Offset: 0x001CA330
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[98].list)
			{
				try
				{
					SkillSeidJsonData98 skillSeidJsonData = new SkillSeidJsonData98();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData98.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData98.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData98.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData98.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData98.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData98.OnInitFinishAction != null)
			{
				SkillSeidJsonData98.OnInitFinishAction();
			}
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004449 RID: 17481
		public static int SEIDID = 98;

		// Token: 0x0400444A RID: 17482
		public static Dictionary<int, SkillSeidJsonData98> DataDict = new Dictionary<int, SkillSeidJsonData98>();

		// Token: 0x0400444B RID: 17483
		public static List<SkillSeidJsonData98> DataList = new List<SkillSeidJsonData98>();

		// Token: 0x0400444C RID: 17484
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData98.OnInitFinish);

		// Token: 0x0400444D RID: 17485
		public int skillid;

		// Token: 0x0400444E RID: 17486
		public int value1;
	}
}
