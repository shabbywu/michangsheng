using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000913 RID: 2323
	public class SkillSeidJsonData165 : IJSONClass
	{
		// Token: 0x0600425E RID: 16990 RVA: 0x001C5874 File Offset: 0x001C3A74
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[165].list)
			{
				try
				{
					SkillSeidJsonData165 skillSeidJsonData = new SkillSeidJsonData165();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData165.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData165.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData165.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData165.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData165.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData165.OnInitFinishAction != null)
			{
				SkillSeidJsonData165.OnInitFinishAction();
			}
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400425D RID: 16989
		public static int SEIDID = 165;

		// Token: 0x0400425E RID: 16990
		public static Dictionary<int, SkillSeidJsonData165> DataDict = new Dictionary<int, SkillSeidJsonData165>();

		// Token: 0x0400425F RID: 16991
		public static List<SkillSeidJsonData165> DataList = new List<SkillSeidJsonData165>();

		// Token: 0x04004260 RID: 16992
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData165.OnInitFinish);

		// Token: 0x04004261 RID: 16993
		public int skillid;

		// Token: 0x04004262 RID: 16994
		public int value1;
	}
}
