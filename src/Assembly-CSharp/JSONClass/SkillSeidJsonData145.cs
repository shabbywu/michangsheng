using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000902 RID: 2306
	public class SkillSeidJsonData145 : IJSONClass
	{
		// Token: 0x0600421B RID: 16923 RVA: 0x001C3EB4 File Offset: 0x001C20B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[145].list)
			{
				try
				{
					SkillSeidJsonData145 skillSeidJsonData = new SkillSeidJsonData145();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData145.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData145.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData145.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData145.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData145.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData145.OnInitFinishAction != null)
			{
				SkillSeidJsonData145.OnInitFinishAction();
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041DD RID: 16861
		public static int SEIDID = 145;

		// Token: 0x040041DE RID: 16862
		public static Dictionary<int, SkillSeidJsonData145> DataDict = new Dictionary<int, SkillSeidJsonData145>();

		// Token: 0x040041DF RID: 16863
		public static List<SkillSeidJsonData145> DataList = new List<SkillSeidJsonData145>();

		// Token: 0x040041E0 RID: 16864
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData145.OnInitFinish);

		// Token: 0x040041E1 RID: 16865
		public int skillid;

		// Token: 0x040041E2 RID: 16866
		public int value1;
	}
}
