using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000966 RID: 2406
	public class StaticSkillSeidJsonData6 : IJSONClass
	{
		// Token: 0x060043AA RID: 17322 RVA: 0x001CCF64 File Offset: 0x001CB164
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[6].list)
			{
				try
				{
					StaticSkillSeidJsonData6 staticSkillSeidJsonData = new StaticSkillSeidJsonData6();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData6.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData6.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData6.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData6.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004491 RID: 17553
		public static int SEIDID = 6;

		// Token: 0x04004492 RID: 17554
		public static Dictionary<int, StaticSkillSeidJsonData6> DataDict = new Dictionary<int, StaticSkillSeidJsonData6>();

		// Token: 0x04004493 RID: 17555
		public static List<StaticSkillSeidJsonData6> DataList = new List<StaticSkillSeidJsonData6>();

		// Token: 0x04004494 RID: 17556
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData6.OnInitFinish);

		// Token: 0x04004495 RID: 17557
		public int skillid;

		// Token: 0x04004496 RID: 17558
		public int value1;
	}
}
