using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200084C RID: 2124
	public class ItemGoodSeid1JsonData : IJSONClass
	{
		// Token: 0x06003F42 RID: 16194 RVA: 0x001B0550 File Offset: 0x001AE750
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemGoodSeid1JsonData.list)
			{
				try
				{
					ItemGoodSeid1JsonData itemGoodSeid1JsonData = new ItemGoodSeid1JsonData();
					itemGoodSeid1JsonData.id = jsonobject["id"].I;
					itemGoodSeid1JsonData.value1 = jsonobject["value1"].I;
					if (ItemGoodSeid1JsonData.DataDict.ContainsKey(itemGoodSeid1JsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemGoodSeid1JsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemGoodSeid1JsonData.id));
					}
					else
					{
						ItemGoodSeid1JsonData.DataDict.Add(itemGoodSeid1JsonData.id, itemGoodSeid1JsonData);
						ItemGoodSeid1JsonData.DataList.Add(itemGoodSeid1JsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemGoodSeid1JsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemGoodSeid1JsonData.OnInitFinishAction != null)
			{
				ItemGoodSeid1JsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B7C RID: 15228
		public static Dictionary<int, ItemGoodSeid1JsonData> DataDict = new Dictionary<int, ItemGoodSeid1JsonData>();

		// Token: 0x04003B7D RID: 15229
		public static List<ItemGoodSeid1JsonData> DataList = new List<ItemGoodSeid1JsonData>();

		// Token: 0x04003B7E RID: 15230
		public static Action OnInitFinishAction = new Action(ItemGoodSeid1JsonData.OnInitFinish);

		// Token: 0x04003B7F RID: 15231
		public int id;

		// Token: 0x04003B80 RID: 15232
		public int value1;
	}
}
