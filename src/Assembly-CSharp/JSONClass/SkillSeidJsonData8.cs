using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200094A RID: 2378
	public class SkillSeidJsonData8 : IJSONClass
	{
		// Token: 0x0600433A RID: 17210 RVA: 0x001CA694 File Offset: 0x001C8894
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[8].list)
			{
				try
				{
					SkillSeidJsonData8 skillSeidJsonData = new SkillSeidJsonData8();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData8.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData8.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData8.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData8.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData8.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData8.OnInitFinishAction != null)
			{
				SkillSeidJsonData8.OnInitFinishAction();
			}
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043CC RID: 17356
		public static int SEIDID = 8;

		// Token: 0x040043CD RID: 17357
		public static Dictionary<int, SkillSeidJsonData8> DataDict = new Dictionary<int, SkillSeidJsonData8>();

		// Token: 0x040043CE RID: 17358
		public static List<SkillSeidJsonData8> DataList = new List<SkillSeidJsonData8>();

		// Token: 0x040043CF RID: 17359
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData8.OnInitFinish);

		// Token: 0x040043D0 RID: 17360
		public int skillid;

		// Token: 0x040043D1 RID: 17361
		public int value1;
	}
}
