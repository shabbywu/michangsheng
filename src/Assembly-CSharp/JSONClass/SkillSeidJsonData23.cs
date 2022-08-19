using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000919 RID: 2329
	public class SkillSeidJsonData23 : IJSONClass
	{
		// Token: 0x06004276 RID: 17014 RVA: 0x001C60B0 File Offset: 0x001C42B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[23].list)
			{
				try
				{
					SkillSeidJsonData23 skillSeidJsonData = new SkillSeidJsonData23();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData23.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData23.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData23.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData23.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData23.OnInitFinishAction != null)
			{
				SkillSeidJsonData23.OnInitFinishAction();
			}
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004282 RID: 17026
		public static int SEIDID = 23;

		// Token: 0x04004283 RID: 17027
		public static Dictionary<int, SkillSeidJsonData23> DataDict = new Dictionary<int, SkillSeidJsonData23>();

		// Token: 0x04004284 RID: 17028
		public static List<SkillSeidJsonData23> DataList = new List<SkillSeidJsonData23>();

		// Token: 0x04004285 RID: 17029
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData23.OnInitFinish);

		// Token: 0x04004286 RID: 17030
		public int skillid;

		// Token: 0x04004287 RID: 17031
		public int value1;
	}
}
