using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000851 RID: 2129
	public class ItemsSeidJsonData13 : IJSONClass
	{
		// Token: 0x06003F56 RID: 16214 RVA: 0x001B0C2C File Offset: 0x001AEE2C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[13].list)
			{
				try
				{
					ItemsSeidJsonData13 itemsSeidJsonData = new ItemsSeidJsonData13();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData13.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData13.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData13.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData13.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData13.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData13.OnInitFinishAction != null)
			{
				ItemsSeidJsonData13.OnInitFinishAction();
			}
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B9A RID: 15258
		public static int SEIDID = 13;

		// Token: 0x04003B9B RID: 15259
		public static Dictionary<int, ItemsSeidJsonData13> DataDict = new Dictionary<int, ItemsSeidJsonData13>();

		// Token: 0x04003B9C RID: 15260
		public static List<ItemsSeidJsonData13> DataList = new List<ItemsSeidJsonData13>();

		// Token: 0x04003B9D RID: 15261
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData13.OnInitFinish);

		// Token: 0x04003B9E RID: 15262
		public int id;

		// Token: 0x04003B9F RID: 15263
		public int value1;
	}
}
