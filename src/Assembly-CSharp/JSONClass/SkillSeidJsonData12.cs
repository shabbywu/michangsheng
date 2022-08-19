using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F5 RID: 2293
	public class SkillSeidJsonData12 : IJSONClass
	{
		// Token: 0x060041E7 RID: 16871 RVA: 0x001C2C80 File Offset: 0x001C0E80
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[12].list)
			{
				try
				{
					SkillSeidJsonData12 skillSeidJsonData = new SkillSeidJsonData12();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData12.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData12.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData12.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData12.OnInitFinishAction != null)
			{
				SkillSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400418A RID: 16778
		public static int SEIDID = 12;

		// Token: 0x0400418B RID: 16779
		public static Dictionary<int, SkillSeidJsonData12> DataDict = new Dictionary<int, SkillSeidJsonData12>();

		// Token: 0x0400418C RID: 16780
		public static List<SkillSeidJsonData12> DataList = new List<SkillSeidJsonData12>();

		// Token: 0x0400418D RID: 16781
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData12.OnInitFinish);

		// Token: 0x0400418E RID: 16782
		public int skillid;

		// Token: 0x0400418F RID: 16783
		public int value1;
	}
}
