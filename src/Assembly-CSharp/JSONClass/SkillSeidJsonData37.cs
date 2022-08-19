using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000924 RID: 2340
	public class SkillSeidJsonData37 : IJSONClass
	{
		// Token: 0x060042A2 RID: 17058 RVA: 0x001C7028 File Offset: 0x001C5228
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[37].list)
			{
				try
				{
					SkillSeidJsonData37 skillSeidJsonData = new SkillSeidJsonData37();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData37.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData37.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData37.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData37.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData37.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData37.OnInitFinishAction != null)
			{
				SkillSeidJsonData37.OnInitFinishAction();
			}
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042CA RID: 17098
		public static int SEIDID = 37;

		// Token: 0x040042CB RID: 17099
		public static Dictionary<int, SkillSeidJsonData37> DataDict = new Dictionary<int, SkillSeidJsonData37>();

		// Token: 0x040042CC RID: 17100
		public static List<SkillSeidJsonData37> DataList = new List<SkillSeidJsonData37>();

		// Token: 0x040042CD RID: 17101
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData37.OnInitFinish);

		// Token: 0x040042CE RID: 17102
		public int skillid;

		// Token: 0x040042CF RID: 17103
		public int value1;
	}
}
