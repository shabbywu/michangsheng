using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE7 RID: 3303
	public class StaticSkillSeidJsonData3 : IJSONClass
	{
		// Token: 0x06004F04 RID: 20228 RVA: 0x002127A0 File Offset: 0x002109A0
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

		// Token: 0x06004F05 RID: 20229 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F8F RID: 20367
		public static int SEIDID = 3;

		// Token: 0x04004F90 RID: 20368
		public static Dictionary<int, StaticSkillSeidJsonData3> DataDict = new Dictionary<int, StaticSkillSeidJsonData3>();

		// Token: 0x04004F91 RID: 20369
		public static List<StaticSkillSeidJsonData3> DataList = new List<StaticSkillSeidJsonData3>();

		// Token: 0x04004F92 RID: 20370
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData3.OnInitFinish);

		// Token: 0x04004F93 RID: 20371
		public int skillid;

		// Token: 0x04004F94 RID: 20372
		public int value1;
	}
}
