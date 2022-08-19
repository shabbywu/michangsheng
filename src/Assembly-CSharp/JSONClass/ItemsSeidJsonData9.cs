using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000868 RID: 2152
	public class ItemsSeidJsonData9 : IJSONClass
	{
		// Token: 0x06003FB2 RID: 16306 RVA: 0x001B2C10 File Offset: 0x001B0E10
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[9].list)
			{
				try
				{
					ItemsSeidJsonData9 itemsSeidJsonData = new ItemsSeidJsonData9();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData9.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData9.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData9.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData9.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData9.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData9.OnInitFinishAction != null)
			{
				ItemsSeidJsonData9.OnInitFinishAction();
			}
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C2C RID: 15404
		public static int SEIDID = 9;

		// Token: 0x04003C2D RID: 15405
		public static Dictionary<int, ItemsSeidJsonData9> DataDict = new Dictionary<int, ItemsSeidJsonData9>();

		// Token: 0x04003C2E RID: 15406
		public static List<ItemsSeidJsonData9> DataList = new List<ItemsSeidJsonData9>();

		// Token: 0x04003C2F RID: 15407
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData9.OnInitFinish);

		// Token: 0x04003C30 RID: 15408
		public int id;

		// Token: 0x04003C31 RID: 15409
		public int value1;
	}
}
