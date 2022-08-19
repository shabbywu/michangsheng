using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000909 RID: 2313
	public class SkillSeidJsonData151 : IJSONClass
	{
		// Token: 0x06004237 RID: 16951 RVA: 0x001C49B0 File Offset: 0x001C2BB0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[151].list)
			{
				try
				{
					SkillSeidJsonData151 skillSeidJsonData = new SkillSeidJsonData151();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (SkillSeidJsonData151.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData151.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData151.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData151.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData151.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData151.OnInitFinishAction != null)
			{
				SkillSeidJsonData151.OnInitFinishAction();
			}
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004213 RID: 16915
		public static int SEIDID = 151;

		// Token: 0x04004214 RID: 16916
		public static Dictionary<int, SkillSeidJsonData151> DataDict = new Dictionary<int, SkillSeidJsonData151>();

		// Token: 0x04004215 RID: 16917
		public static List<SkillSeidJsonData151> DataList = new List<SkillSeidJsonData151>();

		// Token: 0x04004216 RID: 16918
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData151.OnInitFinish);

		// Token: 0x04004217 RID: 16919
		public int skillid;

		// Token: 0x04004218 RID: 16920
		public int value1;

		// Token: 0x04004219 RID: 16921
		public List<int> value2 = new List<int>();

		// Token: 0x0400421A RID: 16922
		public List<int> value3 = new List<int>();
	}
}
