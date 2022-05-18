using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF2 RID: 3058
	public class ItemsSeidJsonData4 : IJSONClass
	{
		// Token: 0x06004B30 RID: 19248 RVA: 0x001FC374 File Offset: 0x001FA574
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[4].list)
			{
				try
				{
					ItemsSeidJsonData4 itemsSeidJsonData = new ItemsSeidJsonData4();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData4.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData4.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData4.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData4.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData4.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData4.OnInitFinishAction != null)
			{
				ItemsSeidJsonData4.OnInitFinishAction();
			}
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400476C RID: 18284
		public static int SEIDID = 4;

		// Token: 0x0400476D RID: 18285
		public static Dictionary<int, ItemsSeidJsonData4> DataDict = new Dictionary<int, ItemsSeidJsonData4>();

		// Token: 0x0400476E RID: 18286
		public static List<ItemsSeidJsonData4> DataList = new List<ItemsSeidJsonData4>();

		// Token: 0x0400476F RID: 18287
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData4.OnInitFinish);

		// Token: 0x04004770 RID: 18288
		public int id;

		// Token: 0x04004771 RID: 18289
		public int value1;
	}
}
