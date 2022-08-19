using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000944 RID: 2372
	public class SkillSeidJsonData71 : IJSONClass
	{
		// Token: 0x06004322 RID: 17186 RVA: 0x001C9E14 File Offset: 0x001C8014
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[71].list)
			{
				try
				{
					SkillSeidJsonData71 skillSeidJsonData = new SkillSeidJsonData71();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData71.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData71.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData71.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData71.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData71.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData71.OnInitFinishAction != null)
			{
				SkillSeidJsonData71.OnInitFinishAction();
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043A4 RID: 17316
		public static int SEIDID = 71;

		// Token: 0x040043A5 RID: 17317
		public static Dictionary<int, SkillSeidJsonData71> DataDict = new Dictionary<int, SkillSeidJsonData71>();

		// Token: 0x040043A6 RID: 17318
		public static List<SkillSeidJsonData71> DataList = new List<SkillSeidJsonData71>();

		// Token: 0x040043A7 RID: 17319
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData71.OnInitFinish);

		// Token: 0x040043A8 RID: 17320
		public int skillid;

		// Token: 0x040043A9 RID: 17321
		public int value1;

		// Token: 0x040043AA RID: 17322
		public int value2;

		// Token: 0x040043AB RID: 17323
		public int value3;
	}
}
