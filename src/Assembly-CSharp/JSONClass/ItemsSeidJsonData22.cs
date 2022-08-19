using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000857 RID: 2135
	public class ItemsSeidJsonData22 : IJSONClass
	{
		// Token: 0x06003F6E RID: 16238 RVA: 0x001B1494 File Offset: 0x001AF694
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

		// Token: 0x06003F6F RID: 16239 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BC0 RID: 15296
		public static int SEIDID = 22;

		// Token: 0x04003BC1 RID: 15297
		public static Dictionary<int, ItemsSeidJsonData22> DataDict = new Dictionary<int, ItemsSeidJsonData22>();

		// Token: 0x04003BC2 RID: 15298
		public static List<ItemsSeidJsonData22> DataList = new List<ItemsSeidJsonData22>();

		// Token: 0x04003BC3 RID: 15299
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData22.OnInitFinish);

		// Token: 0x04003BC4 RID: 15300
		public int id;

		// Token: 0x04003BC5 RID: 15301
		public int value1;
	}
}
