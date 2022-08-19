using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000858 RID: 2136
	public class ItemsSeidJsonData23 : IJSONClass
	{
		// Token: 0x06003F72 RID: 16242 RVA: 0x001B15EC File Offset: 0x001AF7EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[23].list)
			{
				try
				{
					ItemsSeidJsonData23 itemsSeidJsonData = new ItemsSeidJsonData23();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData23.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData23.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData23.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData23.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData23.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData23.OnInitFinishAction != null)
			{
				ItemsSeidJsonData23.OnInitFinishAction();
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BC6 RID: 15302
		public static int SEIDID = 23;

		// Token: 0x04003BC7 RID: 15303
		public static Dictionary<int, ItemsSeidJsonData23> DataDict = new Dictionary<int, ItemsSeidJsonData23>();

		// Token: 0x04003BC8 RID: 15304
		public static List<ItemsSeidJsonData23> DataList = new List<ItemsSeidJsonData23>();

		// Token: 0x04003BC9 RID: 15305
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData23.OnInitFinish);

		// Token: 0x04003BCA RID: 15306
		public int id;

		// Token: 0x04003BCB RID: 15307
		public int value1;
	}
}
