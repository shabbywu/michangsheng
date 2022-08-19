using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200090A RID: 2314
	public class SkillSeidJsonData152 : IJSONClass
	{
		// Token: 0x0600423B RID: 16955 RVA: 0x001C4B58 File Offset: 0x001C2D58
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[152].list)
			{
				try
				{
					SkillSeidJsonData152 skillSeidJsonData = new SkillSeidJsonData152();
					skillSeidJsonData.id = jsonobject["id"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData152.DataDict.ContainsKey(skillSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData152.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.id));
					}
					else
					{
						SkillSeidJsonData152.DataDict.Add(skillSeidJsonData.id, skillSeidJsonData);
						SkillSeidJsonData152.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData152.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData152.OnInitFinishAction != null)
			{
				SkillSeidJsonData152.OnInitFinishAction();
			}
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400421B RID: 16923
		public static int SEIDID = 152;

		// Token: 0x0400421C RID: 16924
		public static Dictionary<int, SkillSeidJsonData152> DataDict = new Dictionary<int, SkillSeidJsonData152>();

		// Token: 0x0400421D RID: 16925
		public static List<SkillSeidJsonData152> DataList = new List<SkillSeidJsonData152>();

		// Token: 0x0400421E RID: 16926
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData152.OnInitFinish);

		// Token: 0x0400421F RID: 16927
		public int id;

		// Token: 0x04004220 RID: 16928
		public int target;

		// Token: 0x04004221 RID: 16929
		public int value1;

		// Token: 0x04004222 RID: 16930
		public int value2;
	}
}
