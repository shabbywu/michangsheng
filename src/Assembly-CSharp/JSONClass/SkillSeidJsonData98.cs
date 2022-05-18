using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE1 RID: 3297
	public class SkillSeidJsonData98 : IJSONClass
	{
		// Token: 0x06004EEC RID: 20204 RVA: 0x00211EB8 File Offset: 0x002100B8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[98].list)
			{
				try
				{
					SkillSeidJsonData98 skillSeidJsonData = new SkillSeidJsonData98();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData98.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData98.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData98.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData98.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData98.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData98.OnInitFinishAction != null)
			{
				SkillSeidJsonData98.OnInitFinishAction();
			}
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F59 RID: 20313
		public static int SEIDID = 98;

		// Token: 0x04004F5A RID: 20314
		public static Dictionary<int, SkillSeidJsonData98> DataDict = new Dictionary<int, SkillSeidJsonData98>();

		// Token: 0x04004F5B RID: 20315
		public static List<SkillSeidJsonData98> DataList = new List<SkillSeidJsonData98>();

		// Token: 0x04004F5C RID: 20316
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData98.OnInitFinish);

		// Token: 0x04004F5D RID: 20317
		public int skillid;

		// Token: 0x04004F5E RID: 20318
		public int value1;
	}
}
