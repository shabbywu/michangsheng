using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CDB RID: 3291
	public class SkillSeidJsonData91 : IJSONClass
	{
		// Token: 0x06004ED4 RID: 20180 RVA: 0x0021178C File Offset: 0x0020F98C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[91].list)
			{
				try
				{
					SkillSeidJsonData91 skillSeidJsonData = new SkillSeidJsonData91();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData91.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData91.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData91.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData91.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData91.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData91.OnInitFinishAction != null)
			{
				SkillSeidJsonData91.OnInitFinishAction();
			}
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F32 RID: 20274
		public static int SEIDID = 91;

		// Token: 0x04004F33 RID: 20275
		public static Dictionary<int, SkillSeidJsonData91> DataDict = new Dictionary<int, SkillSeidJsonData91>();

		// Token: 0x04004F34 RID: 20276
		public static List<SkillSeidJsonData91> DataList = new List<SkillSeidJsonData91>();

		// Token: 0x04004F35 RID: 20277
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData91.OnInitFinish);

		// Token: 0x04004F36 RID: 20278
		public int skillid;

		// Token: 0x04004F37 RID: 20279
		public int value1;

		// Token: 0x04004F38 RID: 20280
		public int value2;
	}
}
