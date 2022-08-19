using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000932 RID: 2354
	public class SkillSeidJsonData50 : IJSONClass
	{
		// Token: 0x060042DA RID: 17114 RVA: 0x001C849C File Offset: 0x001C669C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[50].list)
			{
				try
				{
					SkillSeidJsonData50 skillSeidJsonData = new SkillSeidJsonData50();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData50.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData50.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData50.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData50.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData50.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData50.OnInitFinishAction != null)
			{
				SkillSeidJsonData50.OnInitFinishAction();
			}
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400432E RID: 17198
		public static int SEIDID = 50;

		// Token: 0x0400432F RID: 17199
		public static Dictionary<int, SkillSeidJsonData50> DataDict = new Dictionary<int, SkillSeidJsonData50>();

		// Token: 0x04004330 RID: 17200
		public static List<SkillSeidJsonData50> DataList = new List<SkillSeidJsonData50>();

		// Token: 0x04004331 RID: 17201
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData50.OnInitFinish);

		// Token: 0x04004332 RID: 17202
		public int skillid;

		// Token: 0x04004333 RID: 17203
		public int value1;

		// Token: 0x04004334 RID: 17204
		public int value2;
	}
}
