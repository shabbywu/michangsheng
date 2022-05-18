using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BEE RID: 3054
	public class ItemsSeidJsonData30 : IJSONClass
	{
		// Token: 0x06004B20 RID: 19232 RVA: 0x001FBEAC File Offset: 0x001FA0AC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[30].list)
			{
				try
				{
					ItemsSeidJsonData30 itemsSeidJsonData = new ItemsSeidJsonData30();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					itemsSeidJsonData.value2 = jsonobject["value2"].I;
					if (ItemsSeidJsonData30.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData30.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData30.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData30.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData30.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData30.OnInitFinishAction != null)
			{
				ItemsSeidJsonData30.OnInitFinishAction();
			}
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004752 RID: 18258
		public static int SEIDID = 30;

		// Token: 0x04004753 RID: 18259
		public static Dictionary<int, ItemsSeidJsonData30> DataDict = new Dictionary<int, ItemsSeidJsonData30>();

		// Token: 0x04004754 RID: 18260
		public static List<ItemsSeidJsonData30> DataList = new List<ItemsSeidJsonData30>();

		// Token: 0x04004755 RID: 18261
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData30.OnInitFinish);

		// Token: 0x04004756 RID: 18262
		public int id;

		// Token: 0x04004757 RID: 18263
		public int value1;

		// Token: 0x04004758 RID: 18264
		public int value2;
	}
}
