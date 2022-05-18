using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BEF RID: 3055
	public class ItemsSeidJsonData32 : IJSONClass
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x001FBFE8 File Offset: 0x001FA1E8
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

		// Token: 0x06004B25 RID: 19237 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004759 RID: 18265
		public static int SEIDID = 32;

		// Token: 0x0400475A RID: 18266
		public static Dictionary<int, ItemsSeidJsonData32> DataDict = new Dictionary<int, ItemsSeidJsonData32>();

		// Token: 0x0400475B RID: 18267
		public static List<ItemsSeidJsonData32> DataList = new List<ItemsSeidJsonData32>();

		// Token: 0x0400475C RID: 18268
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData32.OnInitFinish);

		// Token: 0x0400475D RID: 18269
		public int id;

		// Token: 0x0400475E RID: 18270
		public int value1;
	}
}
