using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000914 RID: 2324
	public class SkillSeidJsonData17 : IJSONClass
	{
		// Token: 0x06004262 RID: 16994 RVA: 0x001C59D4 File Offset: 0x001C3BD4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[17].list)
			{
				try
				{
					SkillSeidJsonData17 skillSeidJsonData = new SkillSeidJsonData17();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData17.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData17.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData17.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData17.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData17.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData17.OnInitFinishAction != null)
			{
				SkillSeidJsonData17.OnInitFinishAction();
			}
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004263 RID: 16995
		public static int SEIDID = 17;

		// Token: 0x04004264 RID: 16996
		public static Dictionary<int, SkillSeidJsonData17> DataDict = new Dictionary<int, SkillSeidJsonData17>();

		// Token: 0x04004265 RID: 16997
		public static List<SkillSeidJsonData17> DataList = new List<SkillSeidJsonData17>();

		// Token: 0x04004266 RID: 16998
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData17.OnInitFinish);

		// Token: 0x04004267 RID: 16999
		public int skillid;

		// Token: 0x04004268 RID: 17000
		public int value1;

		// Token: 0x04004269 RID: 17001
		public int value2;
	}
}
