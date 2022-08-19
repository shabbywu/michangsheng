using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000933 RID: 2355
	public class SkillSeidJsonData51 : IJSONClass
	{
		// Token: 0x060042DE RID: 17118 RVA: 0x001C8608 File Offset: 0x001C6808
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[51].list)
			{
				try
				{
					SkillSeidJsonData51 skillSeidJsonData = new SkillSeidJsonData51();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (SkillSeidJsonData51.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData51.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData51.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData51.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData51.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData51.OnInitFinishAction != null)
			{
				SkillSeidJsonData51.OnInitFinishAction();
			}
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004335 RID: 17205
		public static int SEIDID = 51;

		// Token: 0x04004336 RID: 17206
		public static Dictionary<int, SkillSeidJsonData51> DataDict = new Dictionary<int, SkillSeidJsonData51>();

		// Token: 0x04004337 RID: 17207
		public static List<SkillSeidJsonData51> DataList = new List<SkillSeidJsonData51>();

		// Token: 0x04004338 RID: 17208
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData51.OnInitFinish);

		// Token: 0x04004339 RID: 17209
		public int skillid;

		// Token: 0x0400433A RID: 17210
		public List<int> value1 = new List<int>();

		// Token: 0x0400433B RID: 17211
		public List<int> value2 = new List<int>();

		// Token: 0x0400433C RID: 17212
		public List<int> value3 = new List<int>();
	}
}
