using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200095B RID: 2395
	public class SkillSeidJsonData96 : IJSONClass
	{
		// Token: 0x0600437E RID: 17278 RVA: 0x001CBE80 File Offset: 0x001CA080
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[96].list)
			{
				try
				{
					SkillSeidJsonData96 skillSeidJsonData = new SkillSeidJsonData96();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData96.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData96.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData96.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData96.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData96.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData96.OnInitFinishAction != null)
			{
				SkillSeidJsonData96.OnInitFinishAction();
			}
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400443D RID: 17469
		public static int SEIDID = 96;

		// Token: 0x0400443E RID: 17470
		public static Dictionary<int, SkillSeidJsonData96> DataDict = new Dictionary<int, SkillSeidJsonData96>();

		// Token: 0x0400443F RID: 17471
		public static List<SkillSeidJsonData96> DataList = new List<SkillSeidJsonData96>();

		// Token: 0x04004440 RID: 17472
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData96.OnInitFinish);

		// Token: 0x04004441 RID: 17473
		public int skillid;

		// Token: 0x04004442 RID: 17474
		public int value1;
	}
}
