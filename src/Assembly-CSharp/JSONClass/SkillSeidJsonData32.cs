using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200091F RID: 2335
	public class SkillSeidJsonData32 : IJSONClass
	{
		// Token: 0x0600428E RID: 17038 RVA: 0x001C6934 File Offset: 0x001C4B34
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[32].list)
			{
				try
				{
					SkillSeidJsonData32 skillSeidJsonData = new SkillSeidJsonData32();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData32.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData32.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData32.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData32.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData32.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData32.OnInitFinishAction != null)
			{
				SkillSeidJsonData32.OnInitFinishAction();
			}
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042A9 RID: 17065
		public static int SEIDID = 32;

		// Token: 0x040042AA RID: 17066
		public static Dictionary<int, SkillSeidJsonData32> DataDict = new Dictionary<int, SkillSeidJsonData32>();

		// Token: 0x040042AB RID: 17067
		public static List<SkillSeidJsonData32> DataList = new List<SkillSeidJsonData32>();

		// Token: 0x040042AC RID: 17068
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData32.OnInitFinish);

		// Token: 0x040042AD RID: 17069
		public int skillid;

		// Token: 0x040042AE RID: 17070
		public int value1;

		// Token: 0x040042AF RID: 17071
		public int value2;
	}
}
