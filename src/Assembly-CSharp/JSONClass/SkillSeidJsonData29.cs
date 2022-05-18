using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C9F RID: 3231
	public class SkillSeidJsonData29 : IJSONClass
	{
		// Token: 0x06004DE4 RID: 19940 RVA: 0x0020CE88 File Offset: 0x0020B088
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

		// Token: 0x06004DE5 RID: 19941 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D9E RID: 19870
		public static int SEIDID = 29;

		// Token: 0x04004D9F RID: 19871
		public static Dictionary<int, SkillSeidJsonData29> DataDict = new Dictionary<int, SkillSeidJsonData29>();

		// Token: 0x04004DA0 RID: 19872
		public static List<SkillSeidJsonData29> DataList = new List<SkillSeidJsonData29>();

		// Token: 0x04004DA1 RID: 19873
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData29.OnInitFinish);

		// Token: 0x04004DA2 RID: 19874
		public int skillid;

		// Token: 0x04004DA3 RID: 19875
		public int value1;
	}
}
