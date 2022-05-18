using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C85 RID: 3205
	public class SkillSeidJsonData141 : IJSONClass
	{
		// Token: 0x06004D7D RID: 19837 RVA: 0x0020ADEC File Offset: 0x00208FEC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[141].list)
			{
				try
				{
					SkillSeidJsonData141 skillSeidJsonData = new SkillSeidJsonData141();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData141.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData141.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData141.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData141.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData141.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData141.OnInitFinishAction != null)
			{
				SkillSeidJsonData141.OnInitFinishAction();
			}
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CE7 RID: 19687
		public static int SEIDID = 141;

		// Token: 0x04004CE8 RID: 19688
		public static Dictionary<int, SkillSeidJsonData141> DataDict = new Dictionary<int, SkillSeidJsonData141>();

		// Token: 0x04004CE9 RID: 19689
		public static List<SkillSeidJsonData141> DataList = new List<SkillSeidJsonData141>();

		// Token: 0x04004CEA RID: 19690
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData141.OnInitFinish);

		// Token: 0x04004CEB RID: 19691
		public int skillid;

		// Token: 0x04004CEC RID: 19692
		public int value1;
	}
}
