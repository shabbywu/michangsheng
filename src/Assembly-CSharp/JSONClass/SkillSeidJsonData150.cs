using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C8F RID: 3215
	public class SkillSeidJsonData150 : IJSONClass
	{
		// Token: 0x06004DA5 RID: 19877 RVA: 0x0020BABC File Offset: 0x00209CBC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[150].list)
			{
				try
				{
					SkillSeidJsonData150 skillSeidJsonData = new SkillSeidJsonData150();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					skillSeidJsonData.value3 = jsonobject["value3"].ToList();
					if (SkillSeidJsonData150.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData150.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData150.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData150.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData150.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData150.OnInitFinishAction != null)
			{
				SkillSeidJsonData150.OnInitFinishAction();
			}
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004D2D RID: 19757
		public static int SEIDID = 150;

		// Token: 0x04004D2E RID: 19758
		public static Dictionary<int, SkillSeidJsonData150> DataDict = new Dictionary<int, SkillSeidJsonData150>();

		// Token: 0x04004D2F RID: 19759
		public static List<SkillSeidJsonData150> DataList = new List<SkillSeidJsonData150>();

		// Token: 0x04004D30 RID: 19760
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData150.OnInitFinish);

		// Token: 0x04004D31 RID: 19761
		public int skillid;

		// Token: 0x04004D32 RID: 19762
		public int value1;

		// Token: 0x04004D33 RID: 19763
		public List<int> value2 = new List<int>();

		// Token: 0x04004D34 RID: 19764
		public List<int> value3 = new List<int>();
	}
}
