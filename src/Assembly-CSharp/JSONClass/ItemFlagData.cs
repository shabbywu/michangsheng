using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200084B RID: 2123
	public class ItemFlagData : IJSONClass
	{
		// Token: 0x06003F3E RID: 16190 RVA: 0x001B0404 File Offset: 0x001AE604
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemFlagData.list)
			{
				try
				{
					ItemFlagData itemFlagData = new ItemFlagData();
					itemFlagData.id = jsonobject["id"].I;
					itemFlagData.name = jsonobject["name"].Str;
					if (ItemFlagData.DataDict.ContainsKey(itemFlagData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemFlagData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemFlagData.id));
					}
					else
					{
						ItemFlagData.DataDict.Add(itemFlagData.id, itemFlagData);
						ItemFlagData.DataList.Add(itemFlagData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemFlagData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemFlagData.OnInitFinishAction != null)
			{
				ItemFlagData.OnInitFinishAction();
			}
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B77 RID: 15223
		public static Dictionary<int, ItemFlagData> DataDict = new Dictionary<int, ItemFlagData>();

		// Token: 0x04003B78 RID: 15224
		public static List<ItemFlagData> DataList = new List<ItemFlagData>();

		// Token: 0x04003B79 RID: 15225
		public static Action OnInitFinishAction = new Action(ItemFlagData.OnInitFinish);

		// Token: 0x04003B7A RID: 15226
		public int id;

		// Token: 0x04003B7B RID: 15227
		public string name;
	}
}
