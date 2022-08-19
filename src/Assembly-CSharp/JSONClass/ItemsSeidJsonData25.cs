using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000859 RID: 2137
	public class ItemsSeidJsonData25 : IJSONClass
	{
		// Token: 0x06003F76 RID: 16246 RVA: 0x001B1744 File Offset: 0x001AF944
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[25].list)
			{
				try
				{
					ItemsSeidJsonData25 itemsSeidJsonData = new ItemsSeidJsonData25();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData25.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData25.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData25.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData25.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData25.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData25.OnInitFinishAction != null)
			{
				ItemsSeidJsonData25.OnInitFinishAction();
			}
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BCC RID: 15308
		public static int SEIDID = 25;

		// Token: 0x04003BCD RID: 15309
		public static Dictionary<int, ItemsSeidJsonData25> DataDict = new Dictionary<int, ItemsSeidJsonData25>();

		// Token: 0x04003BCE RID: 15310
		public static List<ItemsSeidJsonData25> DataList = new List<ItemsSeidJsonData25>();

		// Token: 0x04003BCF RID: 15311
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData25.OnInitFinish);

		// Token: 0x04003BD0 RID: 15312
		public int id;

		// Token: 0x04003BD1 RID: 15313
		public int value1;

		// Token: 0x04003BD2 RID: 15314
		public int value2;
	}
}
