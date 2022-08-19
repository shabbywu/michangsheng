using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200093E RID: 2366
	public class SkillSeidJsonData63 : IJSONClass
	{
		// Token: 0x0600430A RID: 17162 RVA: 0x001C9568 File Offset: 0x001C7768
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[63].list)
			{
				try
				{
					SkillSeidJsonData63 skillSeidJsonData = new SkillSeidJsonData63();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData63.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData63.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData63.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData63.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData63.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData63.OnInitFinishAction != null)
			{
				SkillSeidJsonData63.OnInitFinishAction();
			}
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400437B RID: 17275
		public static int SEIDID = 63;

		// Token: 0x0400437C RID: 17276
		public static Dictionary<int, SkillSeidJsonData63> DataDict = new Dictionary<int, SkillSeidJsonData63>();

		// Token: 0x0400437D RID: 17277
		public static List<SkillSeidJsonData63> DataList = new List<SkillSeidJsonData63>();

		// Token: 0x0400437E RID: 17278
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData63.OnInitFinish);

		// Token: 0x0400437F RID: 17279
		public int skillid;

		// Token: 0x04004380 RID: 17280
		public int value1;

		// Token: 0x04004381 RID: 17281
		public int value2;
	}
}
