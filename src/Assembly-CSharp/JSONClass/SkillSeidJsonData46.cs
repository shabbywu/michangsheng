using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200092E RID: 2350
	public class SkillSeidJsonData46 : IJSONClass
	{
		// Token: 0x060042CA RID: 17098 RVA: 0x001C7E88 File Offset: 0x001C6088
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[46].list)
			{
				try
				{
					SkillSeidJsonData46 skillSeidJsonData = new SkillSeidJsonData46();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData46.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData46.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData46.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData46.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData46.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData46.OnInitFinishAction != null)
			{
				SkillSeidJsonData46.OnInitFinishAction();
			}
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400430F RID: 17167
		public static int SEIDID = 46;

		// Token: 0x04004310 RID: 17168
		public static Dictionary<int, SkillSeidJsonData46> DataDict = new Dictionary<int, SkillSeidJsonData46>();

		// Token: 0x04004311 RID: 17169
		public static List<SkillSeidJsonData46> DataList = new List<SkillSeidJsonData46>();

		// Token: 0x04004312 RID: 17170
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData46.OnInitFinish);

		// Token: 0x04004313 RID: 17171
		public int skillid;

		// Token: 0x04004314 RID: 17172
		public int value1;

		// Token: 0x04004315 RID: 17173
		public int value2;

		// Token: 0x04004316 RID: 17174
		public int value3;
	}
}
