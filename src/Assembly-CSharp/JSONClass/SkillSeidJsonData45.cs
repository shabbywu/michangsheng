using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200092D RID: 2349
	public class SkillSeidJsonData45 : IJSONClass
	{
		// Token: 0x060042C6 RID: 17094 RVA: 0x001C7D04 File Offset: 0x001C5F04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[45].list)
			{
				try
				{
					SkillSeidJsonData45 skillSeidJsonData = new SkillSeidJsonData45();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData45.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData45.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData45.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData45.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData45.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData45.OnInitFinishAction != null)
			{
				SkillSeidJsonData45.OnInitFinishAction();
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004307 RID: 17159
		public static int SEIDID = 45;

		// Token: 0x04004308 RID: 17160
		public static Dictionary<int, SkillSeidJsonData45> DataDict = new Dictionary<int, SkillSeidJsonData45>();

		// Token: 0x04004309 RID: 17161
		public static List<SkillSeidJsonData45> DataList = new List<SkillSeidJsonData45>();

		// Token: 0x0400430A RID: 17162
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData45.OnInitFinish);

		// Token: 0x0400430B RID: 17163
		public int skillid;

		// Token: 0x0400430C RID: 17164
		public int value1;

		// Token: 0x0400430D RID: 17165
		public int value2;

		// Token: 0x0400430E RID: 17166
		public int value3;
	}
}
