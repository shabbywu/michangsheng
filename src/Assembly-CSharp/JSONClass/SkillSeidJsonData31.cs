using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200091E RID: 2334
	public class SkillSeidJsonData31 : IJSONClass
	{
		// Token: 0x0600428A RID: 17034 RVA: 0x001C67AC File Offset: 0x001C49AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[31].list)
			{
				try
				{
					SkillSeidJsonData31 skillSeidJsonData = new SkillSeidJsonData31();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData31.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData31.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData31.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData31.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData31.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData31.OnInitFinishAction != null)
			{
				SkillSeidJsonData31.OnInitFinishAction();
			}
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042A2 RID: 17058
		public static int SEIDID = 31;

		// Token: 0x040042A3 RID: 17059
		public static Dictionary<int, SkillSeidJsonData31> DataDict = new Dictionary<int, SkillSeidJsonData31>();

		// Token: 0x040042A4 RID: 17060
		public static List<SkillSeidJsonData31> DataList = new List<SkillSeidJsonData31>();

		// Token: 0x040042A5 RID: 17061
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData31.OnInitFinish);

		// Token: 0x040042A6 RID: 17062
		public int skillid;

		// Token: 0x040042A7 RID: 17063
		public List<int> value1 = new List<int>();

		// Token: 0x040042A8 RID: 17064
		public List<int> value2 = new List<int>();
	}
}
