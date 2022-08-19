using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000950 RID: 2384
	public class SkillSeidJsonData85 : IJSONClass
	{
		// Token: 0x06004352 RID: 17234 RVA: 0x001CAF2C File Offset: 0x001C912C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[85].list)
			{
				try
				{
					SkillSeidJsonData85 skillSeidJsonData = new SkillSeidJsonData85();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData85.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData85.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData85.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData85.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData85.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData85.OnInitFinishAction != null)
			{
				SkillSeidJsonData85.OnInitFinishAction();
			}
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043F4 RID: 17396
		public static int SEIDID = 85;

		// Token: 0x040043F5 RID: 17397
		public static Dictionary<int, SkillSeidJsonData85> DataDict = new Dictionary<int, SkillSeidJsonData85>();

		// Token: 0x040043F6 RID: 17398
		public static List<SkillSeidJsonData85> DataList = new List<SkillSeidJsonData85>();

		// Token: 0x040043F7 RID: 17399
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData85.OnInitFinish);

		// Token: 0x040043F8 RID: 17400
		public int skillid;

		// Token: 0x040043F9 RID: 17401
		public int value1;

		// Token: 0x040043FA RID: 17402
		public int value2;
	}
}
