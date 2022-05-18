using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CDE RID: 3294
	public class SkillSeidJsonData95 : IJSONClass
	{
		// Token: 0x06004EE0 RID: 20192 RVA: 0x00211B40 File Offset: 0x0020FD40
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[95].list)
			{
				try
				{
					SkillSeidJsonData95 skillSeidJsonData = new SkillSeidJsonData95();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData95.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData95.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData95.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData95.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData95.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData95.OnInitFinishAction != null)
			{
				SkillSeidJsonData95.OnInitFinishAction();
			}
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F47 RID: 20295
		public static int SEIDID = 95;

		// Token: 0x04004F48 RID: 20296
		public static Dictionary<int, SkillSeidJsonData95> DataDict = new Dictionary<int, SkillSeidJsonData95>();

		// Token: 0x04004F49 RID: 20297
		public static List<SkillSeidJsonData95> DataList = new List<SkillSeidJsonData95>();

		// Token: 0x04004F4A RID: 20298
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData95.OnInitFinish);

		// Token: 0x04004F4B RID: 20299
		public int skillid;

		// Token: 0x04004F4C RID: 20300
		public int value1;
	}
}
