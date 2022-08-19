using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200085D RID: 2141
	public class ItemsSeidJsonData29 : IJSONClass
	{
		// Token: 0x06003F86 RID: 16262 RVA: 0x001B1CF8 File Offset: 0x001AFEF8
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[29].list)
			{
				try
				{
					ItemsSeidJsonData29 itemsSeidJsonData = new ItemsSeidJsonData29();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData29.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData29.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData29.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData29.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData29.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData29.OnInitFinishAction != null)
			{
				ItemsSeidJsonData29.OnInitFinishAction();
			}
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BE7 RID: 15335
		public static int SEIDID = 29;

		// Token: 0x04003BE8 RID: 15336
		public static Dictionary<int, ItemsSeidJsonData29> DataDict = new Dictionary<int, ItemsSeidJsonData29>();

		// Token: 0x04003BE9 RID: 15337
		public static List<ItemsSeidJsonData29> DataList = new List<ItemsSeidJsonData29>();

		// Token: 0x04003BEA RID: 15338
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData29.OnInitFinish);

		// Token: 0x04003BEB RID: 15339
		public int id;

		// Token: 0x04003BEC RID: 15340
		public int value1;

		// Token: 0x04003BED RID: 15341
		public int value2;
	}
}
