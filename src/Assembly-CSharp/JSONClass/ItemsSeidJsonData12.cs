using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000850 RID: 2128
	public class ItemsSeidJsonData12 : IJSONClass
	{
		// Token: 0x06003F52 RID: 16210 RVA: 0x001B0AA4 File Offset: 0x001AECA4
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[12].list)
			{
				try
				{
					ItemsSeidJsonData12 itemsSeidJsonData = new ItemsSeidJsonData12();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					itemsSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (ItemsSeidJsonData12.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData12.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData12.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData12.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData12.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData12.OnInitFinishAction != null)
			{
				ItemsSeidJsonData12.OnInitFinishAction();
			}
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003B93 RID: 15251
		public static int SEIDID = 12;

		// Token: 0x04003B94 RID: 15252
		public static Dictionary<int, ItemsSeidJsonData12> DataDict = new Dictionary<int, ItemsSeidJsonData12>();

		// Token: 0x04003B95 RID: 15253
		public static List<ItemsSeidJsonData12> DataList = new List<ItemsSeidJsonData12>();

		// Token: 0x04003B96 RID: 15254
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData12.OnInitFinish);

		// Token: 0x04003B97 RID: 15255
		public int id;

		// Token: 0x04003B98 RID: 15256
		public List<int> value1 = new List<int>();

		// Token: 0x04003B99 RID: 15257
		public List<int> value2 = new List<int>();
	}
}
