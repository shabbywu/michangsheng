using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000936 RID: 2358
	public class SkillSeidJsonData55 : IJSONClass
	{
		// Token: 0x060042EA RID: 17130 RVA: 0x001C8A64 File Offset: 0x001C6C64
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[55].list)
			{
				try
				{
					SkillSeidJsonData55 skillSeidJsonData = new SkillSeidJsonData55();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData55.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData55.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData55.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData55.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData55.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData55.OnInitFinishAction != null)
			{
				SkillSeidJsonData55.OnInitFinishAction();
			}
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004349 RID: 17225
		public static int SEIDID = 55;

		// Token: 0x0400434A RID: 17226
		public static Dictionary<int, SkillSeidJsonData55> DataDict = new Dictionary<int, SkillSeidJsonData55>();

		// Token: 0x0400434B RID: 17227
		public static List<SkillSeidJsonData55> DataList = new List<SkillSeidJsonData55>();

		// Token: 0x0400434C RID: 17228
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData55.OnInitFinish);

		// Token: 0x0400434D RID: 17229
		public int skillid;

		// Token: 0x0400434E RID: 17230
		public int value1;
	}
}
