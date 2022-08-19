using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200085E RID: 2142
	public class ItemsSeidJsonData3 : IJSONClass
	{
		// Token: 0x06003F8A RID: 16266 RVA: 0x001B1E64 File Offset: 0x001B0064
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[3].list)
			{
				try
				{
					ItemsSeidJsonData3 itemsSeidJsonData = new ItemsSeidJsonData3();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData3.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData3.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData3.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData3.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData3.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData3.OnInitFinishAction != null)
			{
				ItemsSeidJsonData3.OnInitFinishAction();
			}
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BEE RID: 15342
		public static int SEIDID = 3;

		// Token: 0x04003BEF RID: 15343
		public static Dictionary<int, ItemsSeidJsonData3> DataDict = new Dictionary<int, ItemsSeidJsonData3>();

		// Token: 0x04003BF0 RID: 15344
		public static List<ItemsSeidJsonData3> DataList = new List<ItemsSeidJsonData3>();

		// Token: 0x04003BF1 RID: 15345
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData3.OnInitFinish);

		// Token: 0x04003BF2 RID: 15346
		public int id;

		// Token: 0x04003BF3 RID: 15347
		public int value1;
	}
}
