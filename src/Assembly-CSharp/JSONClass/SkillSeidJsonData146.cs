using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C8A RID: 3210
	public class SkillSeidJsonData146 : IJSONClass
	{
		// Token: 0x06004D91 RID: 19857 RVA: 0x0020B3C8 File Offset: 0x002095C8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[146].list)
			{
				try
				{
					SkillSeidJsonData146 skillSeidJsonData = new SkillSeidJsonData146();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData146.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData146.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData146.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData146.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData146.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData146.OnInitFinishAction != null)
			{
				SkillSeidJsonData146.OnInitFinishAction();
			}
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D05 RID: 19717
		public static int SEIDID = 146;

		// Token: 0x04004D06 RID: 19718
		public static Dictionary<int, SkillSeidJsonData146> DataDict = new Dictionary<int, SkillSeidJsonData146>();

		// Token: 0x04004D07 RID: 19719
		public static List<SkillSeidJsonData146> DataList = new List<SkillSeidJsonData146>();

		// Token: 0x04004D08 RID: 19720
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData146.OnInitFinish);

		// Token: 0x04004D09 RID: 19721
		public int skillid;

		// Token: 0x04004D0A RID: 19722
		public int value1;
	}
}
