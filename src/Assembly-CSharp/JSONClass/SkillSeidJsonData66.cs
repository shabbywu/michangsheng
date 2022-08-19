using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000941 RID: 2369
	public class SkillSeidJsonData66 : IJSONClass
	{
		// Token: 0x06004316 RID: 17174 RVA: 0x001C9984 File Offset: 0x001C7B84
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[66].list)
			{
				try
				{
					SkillSeidJsonData66 skillSeidJsonData = new SkillSeidJsonData66();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData66.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData66.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData66.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData66.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData66.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData66.OnInitFinishAction != null)
			{
				SkillSeidJsonData66.OnInitFinishAction();
			}
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400438E RID: 17294
		public static int SEIDID = 66;

		// Token: 0x0400438F RID: 17295
		public static Dictionary<int, SkillSeidJsonData66> DataDict = new Dictionary<int, SkillSeidJsonData66>();

		// Token: 0x04004390 RID: 17296
		public static List<SkillSeidJsonData66> DataList = new List<SkillSeidJsonData66>();

		// Token: 0x04004391 RID: 17297
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData66.OnInitFinish);

		// Token: 0x04004392 RID: 17298
		public int skillid;

		// Token: 0x04004393 RID: 17299
		public int value1;
	}
}
