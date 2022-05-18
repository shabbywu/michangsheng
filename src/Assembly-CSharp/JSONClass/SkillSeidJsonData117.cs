using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C7F RID: 3199
	public class SkillSeidJsonData117 : IJSONClass
	{
		// Token: 0x06004D65 RID: 19813 RVA: 0x0020A6D0 File Offset: 0x002088D0
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

		// Token: 0x06004D66 RID: 19814 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CC1 RID: 19649
		public static int SEIDID = 117;

		// Token: 0x04004CC2 RID: 19650
		public static Dictionary<int, SkillSeidJsonData117> DataDict = new Dictionary<int, SkillSeidJsonData117>();

		// Token: 0x04004CC3 RID: 19651
		public static List<SkillSeidJsonData117> DataList = new List<SkillSeidJsonData117>();

		// Token: 0x04004CC4 RID: 19652
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData117.OnInitFinish);

		// Token: 0x04004CC5 RID: 19653
		public int skillid;

		// Token: 0x04004CC6 RID: 19654
		public int value1;
	}
}
