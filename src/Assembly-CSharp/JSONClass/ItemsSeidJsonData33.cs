using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000861 RID: 2145
	public class ItemsSeidJsonData33 : IJSONClass
	{
		// Token: 0x06003F96 RID: 16278 RVA: 0x001B2280 File Offset: 0x001B0480
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[33].list)
			{
				try
				{
					ItemsSeidJsonData33 itemsSeidJsonData = new ItemsSeidJsonData33();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData33.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData33.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData33.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData33.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData33.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData33.OnInitFinishAction != null)
			{
				ItemsSeidJsonData33.OnInitFinishAction();
			}
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C01 RID: 15361
		public static int SEIDID = 33;

		// Token: 0x04003C02 RID: 15362
		public static Dictionary<int, ItemsSeidJsonData33> DataDict = new Dictionary<int, ItemsSeidJsonData33>();

		// Token: 0x04003C03 RID: 15363
		public static List<ItemsSeidJsonData33> DataList = new List<ItemsSeidJsonData33>();

		// Token: 0x04003C04 RID: 15364
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData33.OnInitFinish);

		// Token: 0x04003C05 RID: 15365
		public int id;

		// Token: 0x04003C06 RID: 15366
		public int value1;

		// Token: 0x04003C07 RID: 15367
		public int value2;
	}
}
