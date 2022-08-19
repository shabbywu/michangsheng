using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000956 RID: 2390
	public class SkillSeidJsonData90 : IJSONClass
	{
		// Token: 0x0600436A RID: 17258 RVA: 0x001CB778 File Offset: 0x001C9978
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[90].list)
			{
				try
				{
					SkillSeidJsonData90 skillSeidJsonData = new SkillSeidJsonData90();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData90.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData90.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData90.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData90.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData90.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData90.OnInitFinishAction != null)
			{
				SkillSeidJsonData90.OnInitFinishAction();
			}
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400441B RID: 17435
		public static int SEIDID = 90;

		// Token: 0x0400441C RID: 17436
		public static Dictionary<int, SkillSeidJsonData90> DataDict = new Dictionary<int, SkillSeidJsonData90>();

		// Token: 0x0400441D RID: 17437
		public static List<SkillSeidJsonData90> DataList = new List<SkillSeidJsonData90>();

		// Token: 0x0400441E RID: 17438
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData90.OnInitFinish);

		// Token: 0x0400441F RID: 17439
		public int skillid;

		// Token: 0x04004420 RID: 17440
		public int value1;

		// Token: 0x04004421 RID: 17441
		public int value2;
	}
}
