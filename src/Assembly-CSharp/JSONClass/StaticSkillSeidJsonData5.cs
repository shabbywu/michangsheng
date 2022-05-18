using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE9 RID: 3305
	public class StaticSkillSeidJsonData5 : IJSONClass
	{
		// Token: 0x06004F0C RID: 20236 RVA: 0x002129F0 File Offset: 0x00210BF0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillSeidJsonData[5].list)
			{
				try
				{
					StaticSkillSeidJsonData5 staticSkillSeidJsonData = new StaticSkillSeidJsonData5();
					staticSkillSeidJsonData.skillid = jsonobject["skillid"].I;
					staticSkillSeidJsonData.value1 = jsonobject["value1"].I;
					if (StaticSkillSeidJsonData5.DataDict.ContainsKey(staticSkillSeidJsonData.skillid))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticSkillSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticSkillSeidJsonData.skillid));
					}
					else
					{
						StaticSkillSeidJsonData5.DataDict.Add(staticSkillSeidJsonData.skillid, staticSkillSeidJsonData);
						StaticSkillSeidJsonData5.DataList.Add(staticSkillSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticSkillSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticSkillSeidJsonData5.OnInitFinishAction != null)
			{
				StaticSkillSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F9B RID: 20379
		public static int SEIDID = 5;

		// Token: 0x04004F9C RID: 20380
		public static Dictionary<int, StaticSkillSeidJsonData5> DataDict = new Dictionary<int, StaticSkillSeidJsonData5>();

		// Token: 0x04004F9D RID: 20381
		public static List<StaticSkillSeidJsonData5> DataList = new List<StaticSkillSeidJsonData5>();

		// Token: 0x04004F9E RID: 20382
		public static Action OnInitFinishAction = new Action(StaticSkillSeidJsonData5.OnInitFinish);

		// Token: 0x04004F9F RID: 20383
		public int skillid;

		// Token: 0x04004FA0 RID: 20384
		public int value1;
	}
}
