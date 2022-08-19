using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200095F RID: 2399
	public class StaticLVToLevelJsonData : IJSONClass
	{
		// Token: 0x0600438E RID: 17294 RVA: 0x001CC3EC File Offset: 0x001CA5EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.StaticLVToLevelJsonData.list)
			{
				try
				{
					StaticLVToLevelJsonData staticLVToLevelJsonData = new StaticLVToLevelJsonData();
					staticLVToLevelJsonData.id = jsonobject["id"].I;
					staticLVToLevelJsonData.Max1 = jsonobject["Max1"].I;
					staticLVToLevelJsonData.Max2 = jsonobject["Max2"].I;
					staticLVToLevelJsonData.Max3 = jsonobject["Max3"].I;
					staticLVToLevelJsonData.Name = jsonobject["Name"].Str;
					if (StaticLVToLevelJsonData.DataDict.ContainsKey(staticLVToLevelJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典StaticLVToLevelJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", staticLVToLevelJsonData.id));
					}
					else
					{
						StaticLVToLevelJsonData.DataDict.Add(staticLVToLevelJsonData.id, staticLVToLevelJsonData);
						StaticLVToLevelJsonData.DataList.Add(staticLVToLevelJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典StaticLVToLevelJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (StaticLVToLevelJsonData.OnInitFinishAction != null)
			{
				StaticLVToLevelJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004455 RID: 17493
		public static Dictionary<int, StaticLVToLevelJsonData> DataDict = new Dictionary<int, StaticLVToLevelJsonData>();

		// Token: 0x04004456 RID: 17494
		public static List<StaticLVToLevelJsonData> DataList = new List<StaticLVToLevelJsonData>();

		// Token: 0x04004457 RID: 17495
		public static Action OnInitFinishAction = new Action(StaticLVToLevelJsonData.OnInitFinish);

		// Token: 0x04004458 RID: 17496
		public int id;

		// Token: 0x04004459 RID: 17497
		public int Max1;

		// Token: 0x0400445A RID: 17498
		public int Max2;

		// Token: 0x0400445B RID: 17499
		public int Max3;

		// Token: 0x0400445C RID: 17500
		public string Name;
	}
}
