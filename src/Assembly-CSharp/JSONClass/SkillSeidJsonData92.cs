using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000958 RID: 2392
	public class SkillSeidJsonData92 : IJSONClass
	{
		// Token: 0x06004372 RID: 17266 RVA: 0x001CBA50 File Offset: 0x001C9C50
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[92].list)
			{
				try
				{
					SkillSeidJsonData92 skillSeidJsonData = new SkillSeidJsonData92();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData92.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData92.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData92.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData92.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData92.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData92.OnInitFinishAction != null)
			{
				SkillSeidJsonData92.OnInitFinishAction();
			}
		}

		// Token: 0x06004373 RID: 17267 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004429 RID: 17449
		public static int SEIDID = 92;

		// Token: 0x0400442A RID: 17450
		public static Dictionary<int, SkillSeidJsonData92> DataDict = new Dictionary<int, SkillSeidJsonData92>();

		// Token: 0x0400442B RID: 17451
		public static List<SkillSeidJsonData92> DataList = new List<SkillSeidJsonData92>();

		// Token: 0x0400442C RID: 17452
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData92.OnInitFinish);

		// Token: 0x0400442D RID: 17453
		public int skillid;

		// Token: 0x0400442E RID: 17454
		public int value1;

		// Token: 0x0400442F RID: 17455
		public int value2;
	}
}
