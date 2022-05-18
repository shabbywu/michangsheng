using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE6 RID: 3046
	public class ItemsSeidJsonData22 : IJSONClass
	{
		// Token: 0x06004B00 RID: 19200 RVA: 0x001FB518 File Offset: 0x001F9718
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[22].list)
			{
				try
				{
					ItemsSeidJsonData22 itemsSeidJsonData = new ItemsSeidJsonData22();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData22.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData22.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData22.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData22.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData22.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData22.OnInitFinishAction != null)
			{
				ItemsSeidJsonData22.OnInitFinishAction();
			}
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x0400471E RID: 18206
		public static int SEIDID = 22;

		// Token: 0x0400471F RID: 18207
		public static Dictionary<int, ItemsSeidJsonData22> DataDict = new Dictionary<int, ItemsSeidJsonData22>();

		// Token: 0x04004720 RID: 18208
		public static List<ItemsSeidJsonData22> DataList = new List<ItemsSeidJsonData22>();

		// Token: 0x04004721 RID: 18209
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData22.OnInitFinish);

		// Token: 0x04004722 RID: 18210
		public int id;

		// Token: 0x04004723 RID: 18211
		public int value1;
	}
}
