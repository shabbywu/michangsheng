using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000917 RID: 2327
	public class SkillSeidJsonData2 : IJSONClass
	{
		// Token: 0x0600426E RID: 17006 RVA: 0x001C5E00 File Offset: 0x001C4000
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[2].list)
			{
				try
				{
					SkillSeidJsonData2 skillSeidJsonData = new SkillSeidJsonData2();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData2.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData2.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData2.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData2.OnInitFinishAction != null)
			{
				SkillSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004276 RID: 17014
		public static int SEIDID = 2;

		// Token: 0x04004277 RID: 17015
		public static Dictionary<int, SkillSeidJsonData2> DataDict = new Dictionary<int, SkillSeidJsonData2>();

		// Token: 0x04004278 RID: 17016
		public static List<SkillSeidJsonData2> DataList = new List<SkillSeidJsonData2>();

		// Token: 0x04004279 RID: 17017
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData2.OnInitFinish);

		// Token: 0x0400427A RID: 17018
		public int skillid;

		// Token: 0x0400427B RID: 17019
		public int value1;
	}
}
