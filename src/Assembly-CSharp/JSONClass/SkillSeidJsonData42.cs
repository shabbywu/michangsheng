using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200092A RID: 2346
	public class SkillSeidJsonData42 : IJSONClass
	{
		// Token: 0x060042BA RID: 17082 RVA: 0x001C78A4 File Offset: 0x001C5AA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[42].list)
			{
				try
				{
					SkillSeidJsonData42 skillSeidJsonData = new SkillSeidJsonData42();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData42.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData42.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData42.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData42.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData42.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData42.OnInitFinishAction != null)
			{
				SkillSeidJsonData42.OnInitFinishAction();
			}
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040042F2 RID: 17138
		public static int SEIDID = 42;

		// Token: 0x040042F3 RID: 17139
		public static Dictionary<int, SkillSeidJsonData42> DataDict = new Dictionary<int, SkillSeidJsonData42>();

		// Token: 0x040042F4 RID: 17140
		public static List<SkillSeidJsonData42> DataList = new List<SkillSeidJsonData42>();

		// Token: 0x040042F5 RID: 17141
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData42.OnInitFinish);

		// Token: 0x040042F6 RID: 17142
		public int skillid;

		// Token: 0x040042F7 RID: 17143
		public int value1;

		// Token: 0x040042F8 RID: 17144
		public int value2;
	}
}
