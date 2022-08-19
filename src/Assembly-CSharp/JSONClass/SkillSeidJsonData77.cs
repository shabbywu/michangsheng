using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000948 RID: 2376
	public class SkillSeidJsonData77 : IJSONClass
	{
		// Token: 0x06004332 RID: 17202 RVA: 0x001CA3B4 File Offset: 0x001C85B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[77].list)
			{
				try
				{
					SkillSeidJsonData77 skillSeidJsonData = new SkillSeidJsonData77();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData77.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData77.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData77.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData77.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData77.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData77.OnInitFinishAction != null)
			{
				SkillSeidJsonData77.OnInitFinishAction();
			}
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043BF RID: 17343
		public static int SEIDID = 77;

		// Token: 0x040043C0 RID: 17344
		public static Dictionary<int, SkillSeidJsonData77> DataDict = new Dictionary<int, SkillSeidJsonData77>();

		// Token: 0x040043C1 RID: 17345
		public static List<SkillSeidJsonData77> DataList = new List<SkillSeidJsonData77>();

		// Token: 0x040043C2 RID: 17346
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData77.OnInitFinish);

		// Token: 0x040043C3 RID: 17347
		public int skillid;

		// Token: 0x040043C4 RID: 17348
		public int value1;
	}
}
