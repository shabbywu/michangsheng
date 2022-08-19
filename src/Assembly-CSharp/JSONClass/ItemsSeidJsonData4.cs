using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000863 RID: 2147
	public class ItemsSeidJsonData4 : IJSONClass
	{
		// Token: 0x06003F9E RID: 16286 RVA: 0x001B2558 File Offset: 0x001B0758
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[4].list)
			{
				try
				{
					ItemsSeidJsonData4 itemsSeidJsonData = new ItemsSeidJsonData4();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData4.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData4.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData4.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData4.OnInitFinishAction != null)
			{
				ItemsSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C0E RID: 15374
		public static int SEIDID = 4;

		// Token: 0x04003C0F RID: 15375
		public static Dictionary<int, ItemsSeidJsonData4> DataDict = new Dictionary<int, ItemsSeidJsonData4>();

		// Token: 0x04003C10 RID: 15376
		public static List<ItemsSeidJsonData4> DataList = new List<ItemsSeidJsonData4>();

		// Token: 0x04003C11 RID: 15377
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData4.OnInitFinish);

		// Token: 0x04003C12 RID: 15378
		public int id;

		// Token: 0x04003C13 RID: 15379
		public int value1;
	}
}
