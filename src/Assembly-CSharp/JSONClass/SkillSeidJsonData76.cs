using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000947 RID: 2375
	public class SkillSeidJsonData76 : IJSONClass
	{
		// Token: 0x0600432E RID: 17198 RVA: 0x001CA25C File Offset: 0x001C845C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[76].list)
			{
				try
				{
					SkillSeidJsonData76 skillSeidJsonData = new SkillSeidJsonData76();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData76.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData76.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData76.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData76.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData76.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData76.OnInitFinishAction != null)
			{
				SkillSeidJsonData76.OnInitFinishAction();
			}
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043B9 RID: 17337
		public static int SEIDID = 76;

		// Token: 0x040043BA RID: 17338
		public static Dictionary<int, SkillSeidJsonData76> DataDict = new Dictionary<int, SkillSeidJsonData76>();

		// Token: 0x040043BB RID: 17339
		public static List<SkillSeidJsonData76> DataList = new List<SkillSeidJsonData76>();

		// Token: 0x040043BC RID: 17340
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData76.OnInitFinish);

		// Token: 0x040043BD RID: 17341
		public int skillid;

		// Token: 0x040043BE RID: 17342
		public int value1;
	}
}
