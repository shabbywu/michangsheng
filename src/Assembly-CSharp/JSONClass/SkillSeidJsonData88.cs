using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000953 RID: 2387
	public class SkillSeidJsonData88 : IJSONClass
	{
		// Token: 0x0600435E RID: 17246 RVA: 0x001CB35C File Offset: 0x001C955C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[88].list)
			{
				try
				{
					SkillSeidJsonData88 skillSeidJsonData = new SkillSeidJsonData88();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData88.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData88.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData88.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData88.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData88.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData88.OnInitFinishAction != null)
			{
				SkillSeidJsonData88.OnInitFinishAction();
			}
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004408 RID: 17416
		public static int SEIDID = 88;

		// Token: 0x04004409 RID: 17417
		public static Dictionary<int, SkillSeidJsonData88> DataDict = new Dictionary<int, SkillSeidJsonData88>();

		// Token: 0x0400440A RID: 17418
		public static List<SkillSeidJsonData88> DataList = new List<SkillSeidJsonData88>();

		// Token: 0x0400440B RID: 17419
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData88.OnInitFinish);

		// Token: 0x0400440C RID: 17420
		public int skillid;

		// Token: 0x0400440D RID: 17421
		public int value1;

		// Token: 0x0400440E RID: 17422
		public int value2;
	}
}
