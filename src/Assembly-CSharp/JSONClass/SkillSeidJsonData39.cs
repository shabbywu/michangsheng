using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000926 RID: 2342
	public class SkillSeidJsonData39 : IJSONClass
	{
		// Token: 0x060042AA RID: 17066 RVA: 0x001C72EC File Offset: 0x001C54EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[39].list)
			{
				try
				{
					SkillSeidJsonData39 skillSeidJsonData = new SkillSeidJsonData39();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData39.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData39.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData39.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData39.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData39.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData39.OnInitFinishAction != null)
			{
				SkillSeidJsonData39.OnInitFinishAction();
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042D7 RID: 17111
		public static int SEIDID = 39;

		// Token: 0x040042D8 RID: 17112
		public static Dictionary<int, SkillSeidJsonData39> DataDict = new Dictionary<int, SkillSeidJsonData39>();

		// Token: 0x040042D9 RID: 17113
		public static List<SkillSeidJsonData39> DataList = new List<SkillSeidJsonData39>();

		// Token: 0x040042DA RID: 17114
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData39.OnInitFinish);

		// Token: 0x040042DB RID: 17115
		public int skillid;

		// Token: 0x040042DC RID: 17116
		public int value1;
	}
}
