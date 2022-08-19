using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F6 RID: 2294
	public class SkillSeidJsonData120 : IJSONClass
	{
		// Token: 0x060041EB RID: 16875 RVA: 0x001C2DD8 File Offset: 0x001C0FD8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[120].list)
			{
				try
				{
					SkillSeidJsonData120 skillSeidJsonData = new SkillSeidJsonData120();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData120.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData120.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData120.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData120.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData120.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData120.OnInitFinishAction != null)
			{
				SkillSeidJsonData120.OnInitFinishAction();
			}
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004190 RID: 16784
		public static int SEIDID = 120;

		// Token: 0x04004191 RID: 16785
		public static Dictionary<int, SkillSeidJsonData120> DataDict = new Dictionary<int, SkillSeidJsonData120>();

		// Token: 0x04004192 RID: 16786
		public static List<SkillSeidJsonData120> DataList = new List<SkillSeidJsonData120>();

		// Token: 0x04004193 RID: 16787
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData120.OnInitFinish);

		// Token: 0x04004194 RID: 16788
		public int skillid;

		// Token: 0x04004195 RID: 16789
		public int value1;

		// Token: 0x04004196 RID: 16790
		public int value2;
	}
}
