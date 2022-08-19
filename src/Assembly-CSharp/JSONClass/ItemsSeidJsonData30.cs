using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200085F RID: 2143
	public class ItemsSeidJsonData30 : IJSONClass
	{
		// Token: 0x06003F8E RID: 16270 RVA: 0x001B1FBC File Offset: 0x001B01BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[30].list)
			{
				try
				{
					ItemsSeidJsonData30 itemsSeidJsonData = new ItemsSeidJsonData30();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData30.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData30.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData30.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData30.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData30.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData30.OnInitFinishAction != null)
			{
				ItemsSeidJsonData30.OnInitFinishAction();
			}
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BF4 RID: 15348
		public static int SEIDID = 30;

		// Token: 0x04003BF5 RID: 15349
		public static Dictionary<int, ItemsSeidJsonData30> DataDict = new Dictionary<int, ItemsSeidJsonData30>();

		// Token: 0x04003BF6 RID: 15350
		public static List<ItemsSeidJsonData30> DataList = new List<ItemsSeidJsonData30>();

		// Token: 0x04003BF7 RID: 15351
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData30.OnInitFinish);

		// Token: 0x04003BF8 RID: 15352
		public int id;

		// Token: 0x04003BF9 RID: 15353
		public int value1;

		// Token: 0x04003BFA RID: 15354
		public int value2;
	}
}
