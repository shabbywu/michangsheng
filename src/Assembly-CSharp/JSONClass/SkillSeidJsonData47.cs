using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200092F RID: 2351
	public class SkillSeidJsonData47 : IJSONClass
	{
		// Token: 0x060042CE RID: 17102 RVA: 0x001C800C File Offset: 0x001C620C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[47].list)
			{
				try
				{
					SkillSeidJsonData47 skillSeidJsonData = new SkillSeidJsonData47();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData47.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData47.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData47.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData47.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData47.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData47.OnInitFinishAction != null)
			{
				SkillSeidJsonData47.OnInitFinishAction();
			}
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004317 RID: 17175
		public static int SEIDID = 47;

		// Token: 0x04004318 RID: 17176
		public static Dictionary<int, SkillSeidJsonData47> DataDict = new Dictionary<int, SkillSeidJsonData47>();

		// Token: 0x04004319 RID: 17177
		public static List<SkillSeidJsonData47> DataList = new List<SkillSeidJsonData47>();

		// Token: 0x0400431A RID: 17178
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData47.OnInitFinish);

		// Token: 0x0400431B RID: 17179
		public int skillid;

		// Token: 0x0400431C RID: 17180
		public int value1;

		// Token: 0x0400431D RID: 17181
		public int value2;

		// Token: 0x0400431E RID: 17182
		public int value3;
	}
}
