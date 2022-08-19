using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000925 RID: 2341
	public class SkillSeidJsonData38 : IJSONClass
	{
		// Token: 0x060042A6 RID: 17062 RVA: 0x001C7180 File Offset: 0x001C5380
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[38].list)
			{
				try
				{
					SkillSeidJsonData38 skillSeidJsonData = new SkillSeidJsonData38();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData38.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData38.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData38.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData38.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData38.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData38.OnInitFinishAction != null)
			{
				SkillSeidJsonData38.OnInitFinishAction();
			}
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042D0 RID: 17104
		public static int SEIDID = 38;

		// Token: 0x040042D1 RID: 17105
		public static Dictionary<int, SkillSeidJsonData38> DataDict = new Dictionary<int, SkillSeidJsonData38>();

		// Token: 0x040042D2 RID: 17106
		public static List<SkillSeidJsonData38> DataList = new List<SkillSeidJsonData38>();

		// Token: 0x040042D3 RID: 17107
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData38.OnInitFinish);

		// Token: 0x040042D4 RID: 17108
		public int skillid;

		// Token: 0x040042D5 RID: 17109
		public int value1;

		// Token: 0x040042D6 RID: 17110
		public int value2;
	}
}
