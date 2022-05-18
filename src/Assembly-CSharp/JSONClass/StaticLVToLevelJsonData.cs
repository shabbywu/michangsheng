using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CE3 RID: 3299
	public class StaticLVToLevelJsonData : IJSONClass
	{
		// Token: 0x06004EF4 RID: 20212 RVA: 0x0021211C File Offset: 0x0021031C
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

		// Token: 0x06004EF5 RID: 20213 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004F65 RID: 20325
		public static Dictionary<int, StaticLVToLevelJsonData> DataDict = new Dictionary<int, StaticLVToLevelJsonData>();

		// Token: 0x04004F66 RID: 20326
		public static List<StaticLVToLevelJsonData> DataList = new List<StaticLVToLevelJsonData>();

		// Token: 0x04004F67 RID: 20327
		public static Action OnInitFinishAction = new Action(StaticLVToLevelJsonData.OnInitFinish);

		// Token: 0x04004F68 RID: 20328
		public int id;

		// Token: 0x04004F69 RID: 20329
		public int Max1;

		// Token: 0x04004F6A RID: 20330
		public int Max2;

		// Token: 0x04004F6B RID: 20331
		public int Max3;

		// Token: 0x04004F6C RID: 20332
		public string Name;
	}
}
