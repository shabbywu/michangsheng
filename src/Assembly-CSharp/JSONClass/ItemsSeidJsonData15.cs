using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BE2 RID: 3042
	public class ItemsSeidJsonData15 : IJSONClass
	{
		// Token: 0x06004AF0 RID: 19184 RVA: 0x001FB050 File Offset: 0x001F9250
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[15].list)
			{
				try
				{
					ItemsSeidJsonData15 itemsSeidJsonData = new ItemsSeidJsonData15();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData15.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData15.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData15.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData15.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData15.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData15.OnInitFinishAction != null)
			{
				ItemsSeidJsonData15.OnInitFinishAction();
			}
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004704 RID: 18180
		public static int SEIDID = 15;

		// Token: 0x04004705 RID: 18181
		public static Dictionary<int, ItemsSeidJsonData15> DataDict = new Dictionary<int, ItemsSeidJsonData15>();

		// Token: 0x04004706 RID: 18182
		public static List<ItemsSeidJsonData15> DataList = new List<ItemsSeidJsonData15>();

		// Token: 0x04004707 RID: 18183
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData15.OnInitFinish);

		// Token: 0x04004708 RID: 18184
		public int id;

		// Token: 0x04004709 RID: 18185
		public int value1;

		// Token: 0x0400470A RID: 18186
		public int value2;
	}
}
