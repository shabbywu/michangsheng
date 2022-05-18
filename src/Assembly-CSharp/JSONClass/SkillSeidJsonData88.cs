using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD7 RID: 3287
	public class SkillSeidJsonData88 : IJSONClass
	{
		// Token: 0x06004EC4 RID: 20164 RVA: 0x002112C4 File Offset: 0x0020F4C4
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

		// Token: 0x06004EC5 RID: 20165 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F18 RID: 20248
		public static int SEIDID = 88;

		// Token: 0x04004F19 RID: 20249
		public static Dictionary<int, SkillSeidJsonData88> DataDict = new Dictionary<int, SkillSeidJsonData88>();

		// Token: 0x04004F1A RID: 20250
		public static List<SkillSeidJsonData88> DataList = new List<SkillSeidJsonData88>();

		// Token: 0x04004F1B RID: 20251
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData88.OnInitFinish);

		// Token: 0x04004F1C RID: 20252
		public int skillid;

		// Token: 0x04004F1D RID: 20253
		public int value1;

		// Token: 0x04004F1E RID: 20254
		public int value2;
	}
}
