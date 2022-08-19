using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E7 RID: 2279
	public class SkillSeidJsonData105 : IJSONClass
	{
		// Token: 0x060041AF RID: 16815 RVA: 0x001C17FC File Offset: 0x001BF9FC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[105].list)
			{
				try
				{
					SkillSeidJsonData105 skillSeidJsonData = new SkillSeidJsonData105();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData105.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData105.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData105.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData105.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData105.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData105.OnInitFinishAction != null)
			{
				SkillSeidJsonData105.OnInitFinishAction();
			}
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004129 RID: 16681
		public static int SEIDID = 105;

		// Token: 0x0400412A RID: 16682
		public static Dictionary<int, SkillSeidJsonData105> DataDict = new Dictionary<int, SkillSeidJsonData105>();

		// Token: 0x0400412B RID: 16683
		public static List<SkillSeidJsonData105> DataList = new List<SkillSeidJsonData105>();

		// Token: 0x0400412C RID: 16684
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData105.OnInitFinish);

		// Token: 0x0400412D RID: 16685
		public int skillid;

		// Token: 0x0400412E RID: 16686
		public int value1;
	}
}
