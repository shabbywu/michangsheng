using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200085C RID: 2140
	public class ItemsSeidJsonData28 : IJSONClass
	{
		// Token: 0x06003F82 RID: 16258 RVA: 0x001B1B8C File Offset: 0x001AFD8C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[28].list)
			{
				try
				{
					ItemsSeidJsonData28 itemsSeidJsonData = new ItemsSeidJsonData28();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (ItemsSeidJsonData28.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData28.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData28.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData28.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData28.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData28.OnInitFinishAction != null)
			{
				ItemsSeidJsonData28.OnInitFinishAction();
			}
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BE1 RID: 15329
		public static int SEIDID = 28;

		// Token: 0x04003BE2 RID: 15330
		public static Dictionary<int, ItemsSeidJsonData28> DataDict = new Dictionary<int, ItemsSeidJsonData28>();

		// Token: 0x04003BE3 RID: 15331
		public static List<ItemsSeidJsonData28> DataList = new List<ItemsSeidJsonData28>();

		// Token: 0x04003BE4 RID: 15332
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData28.OnInitFinish);

		// Token: 0x04003BE5 RID: 15333
		public int id;

		// Token: 0x04003BE6 RID: 15334
		public List<int> value1 = new List<int>();
	}
}
