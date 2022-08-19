using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008F2 RID: 2290
	public class SkillSeidJsonData117 : IJSONClass
	{
		// Token: 0x060041DB RID: 16859 RVA: 0x001C2848 File Offset: 0x001C0A48
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[117].list)
			{
				try
				{
					SkillSeidJsonData117 skillSeidJsonData = new SkillSeidJsonData117();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData117.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData117.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData117.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData117.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData117.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData117.OnInitFinishAction != null)
			{
				SkillSeidJsonData117.OnInitFinishAction();
			}
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004177 RID: 16759
		public static int SEIDID = 117;

		// Token: 0x04004178 RID: 16760
		public static Dictionary<int, SkillSeidJsonData117> DataDict = new Dictionary<int, SkillSeidJsonData117>();

		// Token: 0x04004179 RID: 16761
		public static List<SkillSeidJsonData117> DataList = new List<SkillSeidJsonData117>();

		// Token: 0x0400417A RID: 16762
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData117.OnInitFinish);

		// Token: 0x0400417B RID: 16763
		public int skillid;

		// Token: 0x0400417C RID: 16764
		public int value1;
	}
}
