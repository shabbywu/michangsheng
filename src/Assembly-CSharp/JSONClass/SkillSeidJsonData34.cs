using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000921 RID: 2337
	public class SkillSeidJsonData34 : IJSONClass
	{
		// Token: 0x06004296 RID: 17046 RVA: 0x001C6C0C File Offset: 0x001C4E0C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[34].list)
			{
				try
				{
					SkillSeidJsonData34 skillSeidJsonData = new SkillSeidJsonData34();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData34.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData34.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData34.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData34.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData34.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData34.OnInitFinishAction != null)
			{
				SkillSeidJsonData34.OnInitFinishAction();
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042B7 RID: 17079
		public static int SEIDID = 34;

		// Token: 0x040042B8 RID: 17080
		public static Dictionary<int, SkillSeidJsonData34> DataDict = new Dictionary<int, SkillSeidJsonData34>();

		// Token: 0x040042B9 RID: 17081
		public static List<SkillSeidJsonData34> DataList = new List<SkillSeidJsonData34>();

		// Token: 0x040042BA RID: 17082
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData34.OnInitFinish);

		// Token: 0x040042BB RID: 17083
		public int skillid;

		// Token: 0x040042BC RID: 17084
		public int value1;
	}
}
