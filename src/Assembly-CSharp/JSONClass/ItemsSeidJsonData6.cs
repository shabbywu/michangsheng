using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000865 RID: 2149
	public class ItemsSeidJsonData6 : IJSONClass
	{
		// Token: 0x06003FA6 RID: 16294 RVA: 0x001B2808 File Offset: 0x001B0A08
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[6].list)
			{
				try
				{
					ItemsSeidJsonData6 itemsSeidJsonData = new ItemsSeidJsonData6();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData6.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData6.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData6.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData6.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData6.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData6.OnInitFinishAction != null)
			{
				ItemsSeidJsonData6.OnInitFinishAction();
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C1A RID: 15386
		public static int SEIDID = 6;

		// Token: 0x04003C1B RID: 15387
		public static Dictionary<int, ItemsSeidJsonData6> DataDict = new Dictionary<int, ItemsSeidJsonData6>();

		// Token: 0x04003C1C RID: 15388
		public static List<ItemsSeidJsonData6> DataList = new List<ItemsSeidJsonData6>();

		// Token: 0x04003C1D RID: 15389
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData6.OnInitFinish);

		// Token: 0x04003C1E RID: 15390
		public int id;

		// Token: 0x04003C1F RID: 15391
		public int value1;
	}
}
