using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200084D RID: 2125
	public class ItemsSeidJsonData1 : IJSONClass
	{
		// Token: 0x06003F46 RID: 16198 RVA: 0x001B069C File Offset: 0x001AE89C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[1].list)
			{
				try
				{
					ItemsSeidJsonData1 itemsSeidJsonData = new ItemsSeidJsonData1();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData1.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData1.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData1.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData1.OnInitFinishAction != null)
			{
				ItemsSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B81 RID: 15233
		public static int SEIDID = 1;

		// Token: 0x04003B82 RID: 15234
		public static Dictionary<int, ItemsSeidJsonData1> DataDict = new Dictionary<int, ItemsSeidJsonData1>();

		// Token: 0x04003B83 RID: 15235
		public static List<ItemsSeidJsonData1> DataList = new List<ItemsSeidJsonData1>();

		// Token: 0x04003B84 RID: 15236
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData1.OnInitFinish);

		// Token: 0x04003B85 RID: 15237
		public int id;

		// Token: 0x04003B86 RID: 15238
		public int value1;
	}
}
