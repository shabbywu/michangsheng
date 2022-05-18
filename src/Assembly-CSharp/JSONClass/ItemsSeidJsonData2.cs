using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE4 RID: 3044
	public class ItemsSeidJsonData2 : IJSONClass
	{
		// Token: 0x06004AF8 RID: 19192 RVA: 0x001FB2B4 File Offset: 0x001F94B4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[2].list)
			{
				try
				{
					ItemsSeidJsonData2 itemsSeidJsonData = new ItemsSeidJsonData2();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData2.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData2.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData2.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData2.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData2.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData2.OnInitFinishAction != null)
			{
				ItemsSeidJsonData2.OnInitFinishAction();
			}
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004711 RID: 18193
		public static int SEIDID = 2;

		// Token: 0x04004712 RID: 18194
		public static Dictionary<int, ItemsSeidJsonData2> DataDict = new Dictionary<int, ItemsSeidJsonData2>();

		// Token: 0x04004713 RID: 18195
		public static List<ItemsSeidJsonData2> DataList = new List<ItemsSeidJsonData2>();

		// Token: 0x04004714 RID: 18196
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData2.OnInitFinish);

		// Token: 0x04004715 RID: 18197
		public int id;

		// Token: 0x04004716 RID: 18198
		public int value1;
	}
}
