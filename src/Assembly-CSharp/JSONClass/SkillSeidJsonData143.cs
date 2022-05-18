using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C87 RID: 3207
	public class SkillSeidJsonData143 : IJSONClass
	{
		// Token: 0x06004D85 RID: 19845 RVA: 0x0020B044 File Offset: 0x00209244
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[143].list)
			{
				try
				{
					SkillSeidJsonData143 skillSeidJsonData = new SkillSeidJsonData143();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					if (SkillSeidJsonData143.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData143.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData143.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData143.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData143.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData143.OnInitFinishAction != null)
			{
				SkillSeidJsonData143.OnInitFinishAction();
			}
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CF3 RID: 19699
		public static int SEIDID = 143;

		// Token: 0x04004CF4 RID: 19700
		public static Dictionary<int, SkillSeidJsonData143> DataDict = new Dictionary<int, SkillSeidJsonData143>();

		// Token: 0x04004CF5 RID: 19701
		public static List<SkillSeidJsonData143> DataList = new List<SkillSeidJsonData143>();

		// Token: 0x04004CF6 RID: 19702
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData143.OnInitFinish);

		// Token: 0x04004CF7 RID: 19703
		public int skillid;

		// Token: 0x04004CF8 RID: 19704
		public int value1;
	}
}
