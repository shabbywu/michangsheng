using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C7D RID: 3197
	public class SkillSeidJsonData114 : IJSONClass
	{
		// Token: 0x06004D5D RID: 19805 RVA: 0x0020A440 File Offset: 0x00208640
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[114].list)
			{
				try
				{
					SkillSeidJsonData114 skillSeidJsonData = new SkillSeidJsonData114();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData114.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData114.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData114.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData114.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData114.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData114.OnInitFinishAction != null)
			{
				SkillSeidJsonData114.OnInitFinishAction();
			}
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004CB2 RID: 19634
		public static int SEIDID = 114;

		// Token: 0x04004CB3 RID: 19635
		public static Dictionary<int, SkillSeidJsonData114> DataDict = new Dictionary<int, SkillSeidJsonData114>();

		// Token: 0x04004CB4 RID: 19636
		public static List<SkillSeidJsonData114> DataList = new List<SkillSeidJsonData114>();

		// Token: 0x04004CB5 RID: 19637
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData114.OnInitFinish);

		// Token: 0x04004CB6 RID: 19638
		public int skillid;

		// Token: 0x04004CB7 RID: 19639
		public List<int> value1 = new List<int>();

		// Token: 0x04004CB8 RID: 19640
		public List<int> value2 = new List<int>();
	}
}
