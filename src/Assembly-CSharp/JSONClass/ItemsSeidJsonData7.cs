using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000866 RID: 2150
	public class ItemsSeidJsonData7 : IJSONClass
	{
		// Token: 0x06003FAA RID: 16298 RVA: 0x001B2960 File Offset: 0x001B0B60
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[7].list)
			{
				try
				{
					ItemsSeidJsonData7 itemsSeidJsonData = new ItemsSeidJsonData7();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData7.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData7.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData7.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData7.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData7.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData7.OnInitFinishAction != null)
			{
				ItemsSeidJsonData7.OnInitFinishAction();
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C20 RID: 15392
		public static int SEIDID = 7;

		// Token: 0x04003C21 RID: 15393
		public static Dictionary<int, ItemsSeidJsonData7> DataDict = new Dictionary<int, ItemsSeidJsonData7>();

		// Token: 0x04003C22 RID: 15394
		public static List<ItemsSeidJsonData7> DataList = new List<ItemsSeidJsonData7>();

		// Token: 0x04003C23 RID: 15395
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData7.OnInitFinish);

		// Token: 0x04003C24 RID: 15396
		public int id;

		// Token: 0x04003C25 RID: 15397
		public int value1;
	}
}
