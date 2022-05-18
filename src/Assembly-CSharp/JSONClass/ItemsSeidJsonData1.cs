using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BDC RID: 3036
	public class ItemsSeidJsonData1 : IJSONClass
	{
		// Token: 0x06004AD8 RID: 19160 RVA: 0x001FA94C File Offset: 0x001F8B4C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[1].list)
			{
				try
				{
					ItemsSeidJsonData1 itemsSeidJsonData = new ItemsSeidJsonData1();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].I;
					if (ItemsSeidJsonData1.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData1.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData1.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData1.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData1.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData1.OnInitFinishAction != null)
			{
				ItemsSeidJsonData1.OnInitFinishAction();
			}
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x040046DF RID: 18143
		public static int SEIDID = 1;

		// Token: 0x040046E0 RID: 18144
		public static Dictionary<int, ItemsSeidJsonData1> DataDict = new Dictionary<int, ItemsSeidJsonData1>();

		// Token: 0x040046E1 RID: 18145
		public static List<ItemsSeidJsonData1> DataList = new List<ItemsSeidJsonData1>();

		// Token: 0x040046E2 RID: 18146
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData1.OnInitFinish);

		// Token: 0x040046E3 RID: 18147
		public int id;

		// Token: 0x040046E4 RID: 18148
		public int value1;
	}
}
