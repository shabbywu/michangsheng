using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200084E RID: 2126
	public class ItemsSeidJsonData10 : IJSONClass
	{
		// Token: 0x06003F4A RID: 16202 RVA: 0x001B07F4 File Offset: 0x001AE9F4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[10].list)
			{
				try
				{
					ItemsSeidJsonData10 itemsSeidJsonData = new ItemsSeidJsonData10();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData10.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData10.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData10.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData10.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData10.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData10.OnInitFinishAction != null)
			{
				ItemsSeidJsonData10.OnInitFinishAction();
			}
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B87 RID: 15239
		public static int SEIDID = 10;

		// Token: 0x04003B88 RID: 15240
		public static Dictionary<int, ItemsSeidJsonData10> DataDict = new Dictionary<int, ItemsSeidJsonData10>();

		// Token: 0x04003B89 RID: 15241
		public static List<ItemsSeidJsonData10> DataList = new List<ItemsSeidJsonData10>();

		// Token: 0x04003B8A RID: 15242
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData10.OnInitFinish);

		// Token: 0x04003B8B RID: 15243
		public int id;

		// Token: 0x04003B8C RID: 15244
		public int value1;
	}
}
