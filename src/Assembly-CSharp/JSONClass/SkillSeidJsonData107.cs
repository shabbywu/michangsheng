using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E9 RID: 2281
	public class SkillSeidJsonData107 : IJSONClass
	{
		// Token: 0x060041B7 RID: 16823 RVA: 0x001C1B38 File Offset: 0x001BFD38
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[107].list)
			{
				try
				{
					SkillSeidJsonData107 skillSeidJsonData = new SkillSeidJsonData107();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.target = jsonobject["target"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData107.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData107.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData107.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData107.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData107.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData107.OnInitFinishAction != null)
			{
				SkillSeidJsonData107.OnInitFinishAction();
			}
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004138 RID: 16696
		public static int SEIDID = 107;

		// Token: 0x04004139 RID: 16697
		public static Dictionary<int, SkillSeidJsonData107> DataDict = new Dictionary<int, SkillSeidJsonData107>();

		// Token: 0x0400413A RID: 16698
		public static List<SkillSeidJsonData107> DataList = new List<SkillSeidJsonData107>();

		// Token: 0x0400413B RID: 16699
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData107.OnInitFinish);

		// Token: 0x0400413C RID: 16700
		public int skillid;

		// Token: 0x0400413D RID: 16701
		public int target;

		// Token: 0x0400413E RID: 16702
		public int value1;
	}
}
