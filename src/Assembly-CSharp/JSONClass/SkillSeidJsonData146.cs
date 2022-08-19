using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000903 RID: 2307
	public class SkillSeidJsonData146 : IJSONClass
	{
		// Token: 0x0600421F RID: 16927 RVA: 0x001C4014 File Offset: 0x001C2214
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

		// Token: 0x06004220 RID: 16928 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041E3 RID: 16867
		public static int SEIDID = 146;

		// Token: 0x040041E4 RID: 16868
		public static Dictionary<int, SkillSeidJsonData146> DataDict = new Dictionary<int, SkillSeidJsonData146>();

		// Token: 0x040041E5 RID: 16869
		public static List<SkillSeidJsonData146> DataList = new List<SkillSeidJsonData146>();

		// Token: 0x040041E6 RID: 16870
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData146.OnInitFinish);

		// Token: 0x040041E7 RID: 16871
		public int skillid;

		// Token: 0x040041E8 RID: 16872
		public int value1;
	}
}
