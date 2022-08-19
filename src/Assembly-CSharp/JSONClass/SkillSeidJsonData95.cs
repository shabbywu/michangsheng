using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200095A RID: 2394
	public class SkillSeidJsonData95 : IJSONClass
	{
		// Token: 0x0600437A RID: 17274 RVA: 0x001CBD28 File Offset: 0x001C9F28
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[95].list)
			{
				try
				{
					SkillSeidJsonData95 skillSeidJsonData = new SkillSeidJsonData95();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData95.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData95.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData95.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData95.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData95.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData95.OnInitFinishAction != null)
			{
				SkillSeidJsonData95.OnInitFinishAction();
			}
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004437 RID: 17463
		public static int SEIDID = 95;

		// Token: 0x04004438 RID: 17464
		public static Dictionary<int, SkillSeidJsonData95> DataDict = new Dictionary<int, SkillSeidJsonData95>();

		// Token: 0x04004439 RID: 17465
		public static List<SkillSeidJsonData95> DataList = new List<SkillSeidJsonData95>();

		// Token: 0x0400443A RID: 17466
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData95.OnInitFinish);

		// Token: 0x0400443B RID: 17467
		public int skillid;

		// Token: 0x0400443C RID: 17468
		public int value1;
	}
}
