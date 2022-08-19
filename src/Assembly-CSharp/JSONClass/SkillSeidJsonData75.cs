using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000946 RID: 2374
	public class SkillSeidJsonData75 : IJSONClass
	{
		// Token: 0x0600432A RID: 17194 RVA: 0x001CA0F0 File Offset: 0x001C82F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[75].list)
			{
				try
				{
					SkillSeidJsonData75 skillSeidJsonData = new SkillSeidJsonData75();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData75.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData75.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData75.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData75.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData75.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData75.OnInitFinishAction != null)
			{
				SkillSeidJsonData75.OnInitFinishAction();
			}
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043B2 RID: 17330
		public static int SEIDID = 75;

		// Token: 0x040043B3 RID: 17331
		public static Dictionary<int, SkillSeidJsonData75> DataDict = new Dictionary<int, SkillSeidJsonData75>();

		// Token: 0x040043B4 RID: 17332
		public static List<SkillSeidJsonData75> DataList = new List<SkillSeidJsonData75>();

		// Token: 0x040043B5 RID: 17333
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData75.OnInitFinish);

		// Token: 0x040043B6 RID: 17334
		public int skillid;

		// Token: 0x040043B7 RID: 17335
		public int value1;

		// Token: 0x040043B8 RID: 17336
		public int value2;
	}
}
