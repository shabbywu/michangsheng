using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000942 RID: 2370
	public class SkillSeidJsonData67 : IJSONClass
	{
		// Token: 0x0600431A RID: 17178 RVA: 0x001C9ADC File Offset: 0x001C7CDC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[67].list)
			{
				try
				{
					SkillSeidJsonData67 skillSeidJsonData = new SkillSeidJsonData67();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					skillSeidJsonData.value4 = jsonobject["value4"].I;
					if (SkillSeidJsonData67.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData67.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData67.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData67.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData67.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData67.OnInitFinishAction != null)
			{
				SkillSeidJsonData67.OnInitFinishAction();
			}
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004394 RID: 17300
		public static int SEIDID = 67;

		// Token: 0x04004395 RID: 17301
		public static Dictionary<int, SkillSeidJsonData67> DataDict = new Dictionary<int, SkillSeidJsonData67>();

		// Token: 0x04004396 RID: 17302
		public static List<SkillSeidJsonData67> DataList = new List<SkillSeidJsonData67>();

		// Token: 0x04004397 RID: 17303
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData67.OnInitFinish);

		// Token: 0x04004398 RID: 17304
		public int skillid;

		// Token: 0x04004399 RID: 17305
		public int value1;

		// Token: 0x0400439A RID: 17306
		public int value2;

		// Token: 0x0400439B RID: 17307
		public int value3;

		// Token: 0x0400439C RID: 17308
		public int value4;
	}
}
