using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CBB RID: 3259
	public class SkillSeidJsonData56 : IJSONClass
	{
		// Token: 0x06004E54 RID: 20052 RVA: 0x0020F0F0 File Offset: 0x0020D2F0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[56].list)
			{
				try
				{
					SkillSeidJsonData56 skillSeidJsonData = new SkillSeidJsonData56();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData56.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData56.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData56.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData56.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData56.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData56.OnInitFinishAction != null)
			{
				SkillSeidJsonData56.OnInitFinishAction();
			}
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004E5F RID: 20063
		public static int SEIDID = 56;

		// Token: 0x04004E60 RID: 20064
		public static Dictionary<int, SkillSeidJsonData56> DataDict = new Dictionary<int, SkillSeidJsonData56>();

		// Token: 0x04004E61 RID: 20065
		public static List<SkillSeidJsonData56> DataList = new List<SkillSeidJsonData56>();

		// Token: 0x04004E62 RID: 20066
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData56.OnInitFinish);

		// Token: 0x04004E63 RID: 20067
		public int skillid;

		// Token: 0x04004E64 RID: 20068
		public int value1;

		// Token: 0x04004E65 RID: 20069
		public int value2;
	}
}
