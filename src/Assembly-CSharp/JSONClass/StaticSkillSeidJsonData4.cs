using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000964 RID: 2404
	public class StaticSkillSeidJsonData4 : IJSONClass
	{
		// Token: 0x060043A2 RID: 17314 RVA: 0x001CCCB4 File Offset: 0x001CAEB4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[4].list)
			{
				try
				{
					StaticSkillSeidJsonData4 staticSkillSeidJsonData = new StaticSkillSeidJsonData4();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData4.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData4.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData4.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData4.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004485 RID: 17541
		public static int SEIDID = 4;

		// Token: 0x04004486 RID: 17542
		public static Dictionary<int, StaticSkillSeidJsonData4> DataDict = new Dictionary<int, StaticSkillSeidJsonData4>();

		// Token: 0x04004487 RID: 17543
		public static List<StaticSkillSeidJsonData4> DataList = new List<StaticSkillSeidJsonData4>();

		// Token: 0x04004488 RID: 17544
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData4.OnInitFinish);

		// Token: 0x04004489 RID: 17545
		public int skillid;

		// Token: 0x0400448A RID: 17546
		public int value1;
	}
}
