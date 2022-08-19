using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000908 RID: 2312
	public class SkillSeidJsonData150 : IJSONClass
	{
		// Token: 0x06004233 RID: 16947 RVA: 0x001C4808 File Offset: 0x001C2A08
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

		// Token: 0x06004234 RID: 16948 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400420B RID: 16907
		public static int SEIDID = 150;

		// Token: 0x0400420C RID: 16908
		public static Dictionary<int, SkillSeidJsonData150> DataDict = new Dictionary<int, SkillSeidJsonData150>();

		// Token: 0x0400420D RID: 16909
		public static List<SkillSeidJsonData150> DataList = new List<SkillSeidJsonData150>();

		// Token: 0x0400420E RID: 16910
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData150.OnInitFinish);

		// Token: 0x0400420F RID: 16911
		public int skillid;

		// Token: 0x04004210 RID: 16912
		public int value1;

		// Token: 0x04004211 RID: 16913
		public List<int> value2 = new List<int>();

		// Token: 0x04004212 RID: 16914
		public List<int> value3 = new List<int>();
	}
}
