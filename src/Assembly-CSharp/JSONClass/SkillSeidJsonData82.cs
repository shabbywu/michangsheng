using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CD1 RID: 3281
	public class SkillSeidJsonData82 : IJSONClass
	{
		// Token: 0x06004EAC RID: 20140 RVA: 0x00210B70 File Offset: 0x0020ED70
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[82].list)
			{
				try
				{
					SkillSeidJsonData82 skillSeidJsonData = new SkillSeidJsonData82();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData82.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData82.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData82.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData82.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData82.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData82.OnInitFinishAction != null)
			{
				SkillSeidJsonData82.OnInitFinishAction();
			}
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004EEF RID: 20207
		public static int SEIDID = 82;

		// Token: 0x04004EF0 RID: 20208
		public static Dictionary<int, SkillSeidJsonData82> DataDict = new Dictionary<int, SkillSeidJsonData82>();

		// Token: 0x04004EF1 RID: 20209
		public static List<SkillSeidJsonData82> DataList = new List<SkillSeidJsonData82>();

		// Token: 0x04004EF2 RID: 20210
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData82.OnInitFinish);

		// Token: 0x04004EF3 RID: 20211
		public int skillid;

		// Token: 0x04004EF4 RID: 20212
		public int value1;

		// Token: 0x04004EF5 RID: 20213
		public int value2;
	}
}
