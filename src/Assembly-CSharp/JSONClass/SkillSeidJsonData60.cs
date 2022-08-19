using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200093B RID: 2363
	public class SkillSeidJsonData60 : IJSONClass
	{
		// Token: 0x060042FE RID: 17150 RVA: 0x001C9160 File Offset: 0x001C7360
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[60].list)
			{
				try
				{
					SkillSeidJsonData60 skillSeidJsonData = new SkillSeidJsonData60();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData60.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData60.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData60.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData60.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData60.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData60.OnInitFinishAction != null)
			{
				SkillSeidJsonData60.OnInitFinishAction();
			}
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004369 RID: 17257
		public static int SEIDID = 60;

		// Token: 0x0400436A RID: 17258
		public static Dictionary<int, SkillSeidJsonData60> DataDict = new Dictionary<int, SkillSeidJsonData60>();

		// Token: 0x0400436B RID: 17259
		public static List<SkillSeidJsonData60> DataList = new List<SkillSeidJsonData60>();

		// Token: 0x0400436C RID: 17260
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData60.OnInitFinish);

		// Token: 0x0400436D RID: 17261
		public int skillid;

		// Token: 0x0400436E RID: 17262
		public int value1;
	}
}
