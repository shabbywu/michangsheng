using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000864 RID: 2148
	public class ItemsSeidJsonData5 : IJSONClass
	{
		// Token: 0x06003FA2 RID: 16290 RVA: 0x001B26B0 File Offset: 0x001B08B0
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[5].list)
			{
				try
				{
					ItemsSeidJsonData5 itemsSeidJsonData = new ItemsSeidJsonData5();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData5.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData5.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData5.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData5.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData5.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData5.OnInitFinishAction != null)
			{
				ItemsSeidJsonData5.OnInitFinishAction();
			}
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C14 RID: 15380
		public static int SEIDID = 5;

		// Token: 0x04003C15 RID: 15381
		public static Dictionary<int, ItemsSeidJsonData5> DataDict = new Dictionary<int, ItemsSeidJsonData5>();

		// Token: 0x04003C16 RID: 15382
		public static List<ItemsSeidJsonData5> DataList = new List<ItemsSeidJsonData5>();

		// Token: 0x04003C17 RID: 15383
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData5.OnInitFinish);

		// Token: 0x04003C18 RID: 15384
		public int id;

		// Token: 0x04003C19 RID: 15385
		public int value1;
	}
}
