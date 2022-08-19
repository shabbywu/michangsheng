using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000937 RID: 2359
	public class SkillSeidJsonData56 : IJSONClass
	{
		// Token: 0x060042EE RID: 17134 RVA: 0x001C8BBC File Offset: 0x001C6DBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[56].list)
			{
				try
				{
					SkillSeidJsonData56 skillSeidJsonData = new SkillSeidJsonData56();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData56.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData56.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData56.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData56.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData56.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData56.OnInitFinishAction != null)
			{
				SkillSeidJsonData56.OnInitFinishAction();
			}
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400434F RID: 17231
		public static int SEIDID = 56;

		// Token: 0x04004350 RID: 17232
		public static Dictionary<int, SkillSeidJsonData56> DataDict = new Dictionary<int, SkillSeidJsonData56>();

		// Token: 0x04004351 RID: 17233
		public static List<SkillSeidJsonData56> DataList = new List<SkillSeidJsonData56>();

		// Token: 0x04004352 RID: 17234
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData56.OnInitFinish);

		// Token: 0x04004353 RID: 17235
		public int skillid;

		// Token: 0x04004354 RID: 17236
		public int value1;

		// Token: 0x04004355 RID: 17237
		public int value2;
	}
}
