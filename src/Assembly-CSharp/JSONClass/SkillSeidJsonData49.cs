using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000931 RID: 2353
	public class SkillSeidJsonData49 : IJSONClass
	{
		// Token: 0x060042D6 RID: 17110 RVA: 0x001C8314 File Offset: 0x001C6514
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[49].list)
			{
				try
				{
					SkillSeidJsonData49 skillSeidJsonData = new SkillSeidJsonData49();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData49.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData49.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData49.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData49.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData49.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData49.OnInitFinishAction != null)
			{
				SkillSeidJsonData49.OnInitFinishAction();
			}
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004327 RID: 17191
		public static int SEIDID = 49;

		// Token: 0x04004328 RID: 17192
		public static Dictionary<int, SkillSeidJsonData49> DataDict = new Dictionary<int, SkillSeidJsonData49>();

		// Token: 0x04004329 RID: 17193
		public static List<SkillSeidJsonData49> DataList = new List<SkillSeidJsonData49>();

		// Token: 0x0400432A RID: 17194
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData49.OnInitFinish);

		// Token: 0x0400432B RID: 17195
		public int skillid;

		// Token: 0x0400432C RID: 17196
		public List<int> value1 = new List<int>();

		// Token: 0x0400432D RID: 17197
		public List<int> value2 = new List<int>();
	}
}
