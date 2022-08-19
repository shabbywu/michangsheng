using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200084F RID: 2127
	public class ItemsSeidJsonData11 : IJSONClass
	{
		// Token: 0x06003F4E RID: 16206 RVA: 0x001B094C File Offset: 0x001AEB4C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[11].list)
			{
				try
				{
					ItemsSeidJsonData11 itemsSeidJsonData = new ItemsSeidJsonData11();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData11.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData11.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData11.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData11.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData11.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData11.OnInitFinishAction != null)
			{
				ItemsSeidJsonData11.OnInitFinishAction();
			}
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B8D RID: 15245
		public static int SEIDID = 11;

		// Token: 0x04003B8E RID: 15246
		public static Dictionary<int, ItemsSeidJsonData11> DataDict = new Dictionary<int, ItemsSeidJsonData11>();

		// Token: 0x04003B8F RID: 15247
		public static List<ItemsSeidJsonData11> DataList = new List<ItemsSeidJsonData11>();

		// Token: 0x04003B90 RID: 15248
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData11.OnInitFinish);

		// Token: 0x04003B91 RID: 15249
		public int id;

		// Token: 0x04003B92 RID: 15250
		public int value1;
	}
}
