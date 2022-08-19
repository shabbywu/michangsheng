using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200094F RID: 2383
	public class SkillSeidJsonData84 : IJSONClass
	{
		// Token: 0x0600434E RID: 17230 RVA: 0x001CADC0 File Offset: 0x001C8FC0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[84].list)
			{
				try
				{
					SkillSeidJsonData84 skillSeidJsonData = new SkillSeidJsonData84();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData84.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData84.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData84.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData84.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData84.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData84.OnInitFinishAction != null)
			{
				SkillSeidJsonData84.OnInitFinishAction();
			}
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043ED RID: 17389
		public static int SEIDID = 84;

		// Token: 0x040043EE RID: 17390
		public static Dictionary<int, SkillSeidJsonData84> DataDict = new Dictionary<int, SkillSeidJsonData84>();

		// Token: 0x040043EF RID: 17391
		public static List<SkillSeidJsonData84> DataList = new List<SkillSeidJsonData84>();

		// Token: 0x040043F0 RID: 17392
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData84.OnInitFinish);

		// Token: 0x040043F1 RID: 17393
		public int skillid;

		// Token: 0x040043F2 RID: 17394
		public int value1;

		// Token: 0x040043F3 RID: 17395
		public int value2;
	}
}
