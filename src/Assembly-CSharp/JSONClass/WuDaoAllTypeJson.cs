using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000CFE RID: 3326
	public class WuDaoAllTypeJson : IJSONClass
	{
		// Token: 0x06004F62 RID: 20322 RVA: 0x00214E08 File Offset: 0x00213008
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

		// Token: 0x06004F63 RID: 20323 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400506F RID: 20591
		public static Dictionary<int, WuDaoAllTypeJson> DataDict = new Dictionary<int, WuDaoAllTypeJson>();

		// Token: 0x04005070 RID: 20592
		public static List<WuDaoAllTypeJson> DataList = new List<WuDaoAllTypeJson>();

		// Token: 0x04005071 RID: 20593
		public static Action OnInitFinishAction = new Action(WuDaoAllTypeJson.OnInitFinish);

		// Token: 0x04005072 RID: 20594
		public int id;

		// Token: 0x04005073 RID: 20595
		public string name;

		// Token: 0x04005074 RID: 20596
		public string name1;
	}
}
