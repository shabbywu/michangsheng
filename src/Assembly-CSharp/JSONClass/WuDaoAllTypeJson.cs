using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200097E RID: 2430
	public class WuDaoAllTypeJson : IJSONClass
	{
		// Token: 0x0600440C RID: 17420 RVA: 0x001CFC54 File Offset: 0x001CDE54
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.WuDaoAllTypeJson.list)
			{
				try
				{
					WuDaoAllTypeJson wuDaoAllTypeJson = new WuDaoAllTypeJson();
					wuDaoAllTypeJson.id = jsonobject["id"].I;
					wuDaoAllTypeJson.name = jsonobject["name"].Str;
					wuDaoAllTypeJson.name1 = jsonobject["name1"].Str;
					if (WuDaoAllTypeJson.DataDict.ContainsKey(wuDaoAllTypeJson.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典WuDaoAllTypeJson.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", wuDaoAllTypeJson.id));
					}
					else
					{
						WuDaoAllTypeJson.DataDict.Add(wuDaoAllTypeJson.id, wuDaoAllTypeJson);
						WuDaoAllTypeJson.DataList.Add(wuDaoAllTypeJson);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典WuDaoAllTypeJson.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (WuDaoAllTypeJson.OnInitFinishAction != null)
			{
				WuDaoAllTypeJson.OnInitFinishAction();
			}
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400457B RID: 17787
		public static Dictionary<int, WuDaoAllTypeJson> DataDict = new Dictionary<int, WuDaoAllTypeJson>();

		// Token: 0x0400457C RID: 17788
		public static List<WuDaoAllTypeJson> DataList = new List<WuDaoAllTypeJson>();

		// Token: 0x0400457D RID: 17789
		public static Action OnInitFinishAction = new Action(WuDaoAllTypeJson.OnInitFinish);

		// Token: 0x0400457E RID: 17790
		public int id;

		// Token: 0x0400457F RID: 17791
		public string name;

		// Token: 0x04004580 RID: 17792
		public string name1;
	}
}
