using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x020008E5 RID: 2277
	public class SkillSeidJsonData102 : IJSONClass
	{
		// Token: 0x060041A7 RID: 16807 RVA: 0x001C151C File Offset: 0x001BF71C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.SkillSeidJsonData[102].list)
			{
				try
				{
					SkillSeidJsonData102 skillSeidJsonData = new SkillSeidJsonData102();
					skillSeidJsonData.skillid = jsonobject["skillid"].I;
					skillSeidJsonData.value1 = jsonobject["value1"].ToList();
					skillSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (SkillSeidJsonData102.DataDict.ContainsKey(skillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典SkillSeidJsonData102.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", skillSeidJsonData.skillid));
					}
					else
					{
						SkillSeidJsonData102.DataDict.Add(skillSeidJsonData.skillid, skillSeidJsonData);
						SkillSeidJsonData102.DataList.Add(skillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典SkillSeidJsonData102.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (SkillSeidJsonData102.OnInitFinishAction != null)
			{
				SkillSeidJsonData102.OnInitFinishAction();
			}
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400411C RID: 16668
		public static int SEIDID = 102;

		// Token: 0x0400411D RID: 16669
		public static Dictionary<int, SkillSeidJsonData102> DataDict = new Dictionary<int, SkillSeidJsonData102>();

		// Token: 0x0400411E RID: 16670
		public static List<SkillSeidJsonData102> DataList = new List<SkillSeidJsonData102>();

		// Token: 0x0400411F RID: 16671
		public static Action OnInitFinishAction = new Action(SkillSeidJsonData102.OnInitFinish);

		// Token: 0x04004120 RID: 16672
		public int skillid;

		// Token: 0x04004121 RID: 16673
		public List<int> value1 = new List<int>();

		// Token: 0x04004122 RID: 16674
		public List<int> value2 = new List<int>();
	}
}
