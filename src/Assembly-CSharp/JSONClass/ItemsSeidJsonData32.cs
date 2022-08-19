using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000860 RID: 2144
	public class ItemsSeidJsonData32 : IJSONClass
	{
		// Token: 0x06003F92 RID: 16274 RVA: 0x001B2128 File Offset: 0x001B0328
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[32].list)
			{
				try
				{
					ItemsSeidJsonData32 itemsSeidJsonData = new ItemsSeidJsonData32();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData32.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData32.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData32.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData32.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData32.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData32.OnInitFinishAction != null)
			{
				ItemsSeidJsonData32.OnInitFinishAction();
			}
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BFB RID: 15355
		public static int SEIDID = 32;

		// Token: 0x04003BFC RID: 15356
		public static Dictionary<int, ItemsSeidJsonData32> DataDict = new Dictionary<int, ItemsSeidJsonData32>();

		// Token: 0x04003BFD RID: 15357
		public static List<ItemsSeidJsonData32> DataList = new List<ItemsSeidJsonData32>();

		// Token: 0x04003BFE RID: 15358
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData32.OnInitFinish);

		// Token: 0x04003BFF RID: 15359
		public int id;

		// Token: 0x04003C00 RID: 15360
		public int value1;
	}
}
