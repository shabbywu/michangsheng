using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200085A RID: 2138
	public class ItemsSeidJsonData26 : IJSONClass
	{
		// Token: 0x06003F7A RID: 16250 RVA: 0x001B18B0 File Offset: 0x001AFAB0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[26].list)
			{
				try
				{
					ItemsSeidJsonData26 itemsSeidJsonData = new ItemsSeidJsonData26();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData26.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData26.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData26.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData26.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData26.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData26.OnInitFinishAction != null)
			{
				ItemsSeidJsonData26.OnInitFinishAction();
			}
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BD3 RID: 15315
		public static int SEIDID = 26;

		// Token: 0x04003BD4 RID: 15316
		public static Dictionary<int, ItemsSeidJsonData26> DataDict = new Dictionary<int, ItemsSeidJsonData26>();

		// Token: 0x04003BD5 RID: 15317
		public static List<ItemsSeidJsonData26> DataList = new List<ItemsSeidJsonData26>();

		// Token: 0x04003BD6 RID: 15318
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData26.OnInitFinish);

		// Token: 0x04003BD7 RID: 15319
		public int id;

		// Token: 0x04003BD8 RID: 15320
		public int value1;
	}
}
