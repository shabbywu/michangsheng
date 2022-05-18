using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C79 RID: 3193
	public class SkillSeidJsonData11 : IJSONClass
	{
		// Token: 0x06004D4D RID: 19789 RVA: 0x00209F60 File Offset: 0x00208160
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[11].list)
			{
				try
				{
					SkillSeidJsonData11 skillSeidJsonData = new SkillSeidJsonData11();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					skillSeidJsonData.value3 = jsonobject["value3"].I;
					if (SkillSeidJsonData11.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData11.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData11.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData11.OnInitFinishAction != null)
			{
				SkillSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004C97 RID: 19607
		public static int SEIDID = 11;

		// Token: 0x04004C98 RID: 19608
		public static Dictionary<int, SkillSeidJsonData11> DataDict = new Dictionary<int, SkillSeidJsonData11>();

		// Token: 0x04004C99 RID: 19609
		public static List<SkillSeidJsonData11> DataList = new List<SkillSeidJsonData11>();

		// Token: 0x04004C9A RID: 19610
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData11.OnInitFinish);

		// Token: 0x04004C9B RID: 19611
		public int skillid;

		// Token: 0x04004C9C RID: 19612
		public int value1;

		// Token: 0x04004C9D RID: 19613
		public int value2;

		// Token: 0x04004C9E RID: 19614
		public int value3;
	}
}
