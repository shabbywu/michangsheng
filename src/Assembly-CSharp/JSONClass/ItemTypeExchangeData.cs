using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000869 RID: 2153
	public class ItemTypeExchangeData : IJSONClass
	{
		// Token: 0x06003FB6 RID: 16310 RVA: 0x001B2D68 File Offset: 0x001B0F68
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemTypeExchangeData.list)
			{
				try
				{
					ItemTypeExchangeData itemTypeExchangeData = new ItemTypeExchangeData();
					itemTypeExchangeData.type = jsonobject["type"].I;
					itemTypeExchangeData.quality = jsonobject["quality"].ToList();
					if (ItemTypeExchangeData.DataDict.ContainsKey(itemTypeExchangeData.type))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemTypeExchangeData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemTypeExchangeData.type));
					}
					else
					{
						ItemTypeExchangeData.DataDict.Add(itemTypeExchangeData.type, itemTypeExchangeData);
						ItemTypeExchangeData.DataList.Add(itemTypeExchangeData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemTypeExchangeData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemTypeExchangeData.OnInitFinishAction != null)
			{
				ItemTypeExchangeData.OnInitFinishAction();
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C32 RID: 15410
		public static Dictionary<int, ItemTypeExchangeData> DataDict = new Dictionary<int, ItemTypeExchangeData>();

		// Token: 0x04003C33 RID: 15411
		public static List<ItemTypeExchangeData> DataList = new List<ItemTypeExchangeData>();

		// Token: 0x04003C34 RID: 15412
		public static Action OnInitFinishAction = new Action(ItemTypeExchangeData.OnInitFinish);

		// Token: 0x04003C35 RID: 15413
		public int type;

		// Token: 0x04003C36 RID: 15414
		public List<int> quality = new List<int>();
	}
}
