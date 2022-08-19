using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000962 RID: 2402
	public class StaticSkillSeidJsonData2 : IJSONClass
	{
		// Token: 0x0600439A RID: 17306 RVA: 0x001CCA04 File Offset: 0x001CAC04
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[2].list)
			{
				try
				{
					StaticSkillSeidJsonData2 staticSkillSeidJsonData = new StaticSkillSeidJsonData2();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData2.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData2.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData2.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData2.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004479 RID: 17529
		public static int SEIDID = 2;

		// Token: 0x0400447A RID: 17530
		public static Dictionary<int, StaticSkillSeidJsonData2> DataDict = new Dictionary<int, StaticSkillSeidJsonData2>();

		// Token: 0x0400447B RID: 17531
		public static List<StaticSkillSeidJsonData2> DataList = new List<StaticSkillSeidJsonData2>();

		// Token: 0x0400447C RID: 17532
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData2.OnInitFinish);

		// Token: 0x0400447D RID: 17533
		public int skillid;

		// Token: 0x0400447E RID: 17534
		public int value1;
	}
}
