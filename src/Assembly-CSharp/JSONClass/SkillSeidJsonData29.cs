using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200091B RID: 2331
	public class SkillSeidJsonData29 : IJSONClass
	{
		// Token: 0x0600427E RID: 17022 RVA: 0x001C6360 File Offset: 0x001C4560
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[29].list)
			{
				try
				{
					SkillSeidJsonData29 skillSeidJsonData = new SkillSeidJsonData29();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData29.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData29.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData29.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData29.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData29.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData29.OnInitFinishAction != null)
			{
				SkillSeidJsonData29.OnInitFinishAction();
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400428E RID: 17038
		public static int SEIDID = 29;

		// Token: 0x0400428F RID: 17039
		public static Dictionary<int, SkillSeidJsonData29> DataDict = new Dictionary<int, SkillSeidJsonData29>();

		// Token: 0x04004290 RID: 17040
		public static List<SkillSeidJsonData29> DataList = new List<SkillSeidJsonData29>();

		// Token: 0x04004291 RID: 17041
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData29.OnInitFinish);

		// Token: 0x04004292 RID: 17042
		public int skillid;

		// Token: 0x04004293 RID: 17043
		public int value1;
	}
}
