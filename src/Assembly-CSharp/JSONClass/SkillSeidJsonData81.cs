using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200094C RID: 2380
	public class SkillSeidJsonData81 : IJSONClass
	{
		// Token: 0x06004342 RID: 17218 RVA: 0x001CA974 File Offset: 0x001C8B74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[81].list)
			{
				try
				{
					SkillSeidJsonData81 skillSeidJsonData = new SkillSeidJsonData81();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData81.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData81.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData81.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData81.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData81.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData81.OnInitFinishAction != null)
			{
				SkillSeidJsonData81.OnInitFinishAction();
			}
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040043D9 RID: 17369
		public static int SEIDID = 81;

		// Token: 0x040043DA RID: 17370
		public static Dictionary<int, SkillSeidJsonData81> DataDict = new Dictionary<int, SkillSeidJsonData81>();

		// Token: 0x040043DB RID: 17371
		public static List<SkillSeidJsonData81> DataList = new List<SkillSeidJsonData81>();

		// Token: 0x040043DC RID: 17372
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData81.OnInitFinish);

		// Token: 0x040043DD RID: 17373
		public int skillid;

		// Token: 0x040043DE RID: 17374
		public int value1;
	}
}
