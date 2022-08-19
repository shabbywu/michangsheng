using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008FB RID: 2299
	public class SkillSeidJsonData13 : IJSONClass
	{
		// Token: 0x060041FF RID: 16895 RVA: 0x001C3500 File Offset: 0x001C1700
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[13].list)
			{
				try
				{
					SkillSeidJsonData13 skillSeidJsonData = new SkillSeidJsonData13();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].I;
					skillSeidJsonData.value2 = jsonobject["value2"].I;
					if (SkillSeidJsonData13.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData13.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData13.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData13.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData13.OnInitFinishAction != null)
			{
				SkillSeidJsonData13.OnInitFinishAction();
			}
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x040041B2 RID: 16818
		public static int SEIDID = 13;

		// Token: 0x040041B3 RID: 16819
		public static Dictionary<int, SkillSeidJsonData13> DataDict = new Dictionary<int, SkillSeidJsonData13>();

		// Token: 0x040041B4 RID: 16820
		public static List<SkillSeidJsonData13> DataList = new List<SkillSeidJsonData13>();

		// Token: 0x040041B5 RID: 16821
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData13.OnInitFinish);

		// Token: 0x040041B6 RID: 16822
		public int skillid;

		// Token: 0x040041B7 RID: 16823
		public int value1;

		// Token: 0x040041B8 RID: 16824
		public int value2;
	}
}
