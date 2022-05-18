using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BEC RID: 3052
	public class ItemsSeidJsonData29 : IJSONClass
	{
		// Token: 0x06004B18 RID: 19224 RVA: 0x001FBC48 File Offset: 0x001F9E48
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[29].list)
			{
				try
				{
					ItemsSeidJsonData29 itemsSeidJsonData = new ItemsSeidJsonData29();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData29.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData29.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData29.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData29.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData29.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData29.OnInitFinishAction != null)
			{
				ItemsSeidJsonData29.OnInitFinishAction();
			}
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004745 RID: 18245
		public static int SEIDID = 29;

		// Token: 0x04004746 RID: 18246
		public static Dictionary<int, ItemsSeidJsonData29> DataDict = new Dictionary<int, ItemsSeidJsonData29>();

		// Token: 0x04004747 RID: 18247
		public static List<ItemsSeidJsonData29> DataList = new List<ItemsSeidJsonData29>();

		// Token: 0x04004748 RID: 18248
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData29.OnInitFinish);

		// Token: 0x04004749 RID: 18249
		public int id;

		// Token: 0x0400474A RID: 18250
		public int value1;

		// Token: 0x0400474B RID: 18251
		public int value2;
	}
}
