using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000939 RID: 2361
	public class SkillSeidJsonData58 : IJSONClass
	{
		// Token: 0x060042F6 RID: 17142 RVA: 0x001C8EB0 File Offset: 0x001C70B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[58].list)
			{
				try
				{
					SkillSeidJsonData58 skillSeidJsonData = new SkillSeidJsonData58();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData58.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData58.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData58.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData58.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData58.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData58.OnInitFinishAction != null)
			{
				SkillSeidJsonData58.OnInitFinishAction();
			}
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400435D RID: 17245
		public static int SEIDID = 58;

		// Token: 0x0400435E RID: 17246
		public static Dictionary<int, SkillSeidJsonData58> DataDict = new Dictionary<int, SkillSeidJsonData58>();

		// Token: 0x0400435F RID: 17247
		public static List<SkillSeidJsonData58> DataList = new List<SkillSeidJsonData58>();

		// Token: 0x04004360 RID: 17248
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData58.OnInitFinish);

		// Token: 0x04004361 RID: 17249
		public int skillid;

		// Token: 0x04004362 RID: 17250
		public int value1;
	}
}
