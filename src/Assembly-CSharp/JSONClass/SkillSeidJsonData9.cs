using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD9 RID: 3289
	public class SkillSeidJsonData9 : IJSONClass
	{
		// Token: 0x06004ECC RID: 20172 RVA: 0x00211528 File Offset: 0x0020F728
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

		// Token: 0x06004ECD RID: 20173 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F25 RID: 20261
		public static int SEIDID = 9;

		// Token: 0x04004F26 RID: 20262
		public static Dictionary<int, SkillSeidJsonData9> DataDict = new Dictionary<int, SkillSeidJsonData9>();

		// Token: 0x04004F27 RID: 20263
		public static List<SkillSeidJsonData9> DataList = new List<SkillSeidJsonData9>();

		// Token: 0x04004F28 RID: 20264
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData9.OnInitFinish);

		// Token: 0x04004F29 RID: 20265
		public int skillid;

		// Token: 0x04004F2A RID: 20266
		public int value1;
	}
}
