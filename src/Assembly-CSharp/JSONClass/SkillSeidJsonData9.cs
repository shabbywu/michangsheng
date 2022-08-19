using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000955 RID: 2389
	public class SkillSeidJsonData9 : IJSONClass
	{
		// Token: 0x06004366 RID: 17254 RVA: 0x001CB620 File Offset: 0x001C9820
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[9].list)
			{
				try
				{
					SkillSeidJsonData9 skillSeidJsonData = new SkillSeidJsonData9();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData9.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData9.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData9.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData9.OnInitFinishAction != null)
			{
				SkillSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004415 RID: 17429
		public static int SEIDID = 9;

		// Token: 0x04004416 RID: 17430
		public static Dictionary<int, SkillSeidJsonData9> DataDict = new Dictionary<int, SkillSeidJsonData9>();

		// Token: 0x04004417 RID: 17431
		public static List<SkillSeidJsonData9> DataList = new List<SkillSeidJsonData9>();

		// Token: 0x04004418 RID: 17432
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData9.OnInitFinish);

		// Token: 0x04004419 RID: 17433
		public int skillid;

		// Token: 0x0400441A RID: 17434
		public int value1;
	}
}
