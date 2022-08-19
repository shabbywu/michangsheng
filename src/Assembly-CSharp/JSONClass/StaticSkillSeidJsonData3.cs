using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000963 RID: 2403
	public class StaticSkillSeidJsonData3 : IJSONClass
	{
		// Token: 0x0600439E RID: 17310 RVA: 0x001CCB5C File Offset: 0x001CAD5C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[3].list)
			{
				try
				{
					StaticSkillSeidJsonData3 staticSkillSeidJsonData = new StaticSkillSeidJsonData3();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData3.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData3.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData3.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData3.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400447F RID: 17535
		public static int SEIDID = 3;

		// Token: 0x04004480 RID: 17536
		public static Dictionary<int, StaticSkillSeidJsonData3> DataDict = new Dictionary<int, StaticSkillSeidJsonData3>();

		// Token: 0x04004481 RID: 17537
		public static List<StaticSkillSeidJsonData3> DataList = new List<StaticSkillSeidJsonData3>();

		// Token: 0x04004482 RID: 17538
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData3.OnInitFinish);

		// Token: 0x04004483 RID: 17539
		public int skillid;

		// Token: 0x04004484 RID: 17540
		public int value1;
	}
}
