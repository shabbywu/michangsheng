using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000862 RID: 2146
	public class ItemsSeidJsonData34 : IJSONClass
	{
		// Token: 0x06003F9A RID: 16282 RVA: 0x001B23EC File Offset: 0x001B05EC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[34].list)
			{
				try
				{
					ItemsSeidJsonData34 itemsSeidJsonData = new ItemsSeidJsonData34();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (ItemsSeidJsonData34.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData34.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData34.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData34.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData34.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData34.OnInitFinishAction != null)
			{
				ItemsSeidJsonData34.OnInitFinishAction();
			}
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003C08 RID: 15368
		public static int SEIDID = 34;

		// Token: 0x04003C09 RID: 15369
		public static Dictionary<int, ItemsSeidJsonData34> DataDict = new Dictionary<int, ItemsSeidJsonData34>();

		// Token: 0x04003C0A RID: 15370
		public static List<ItemsSeidJsonData34> DataList = new List<ItemsSeidJsonData34>();

		// Token: 0x04003C0B RID: 15371
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData34.OnInitFinish);

		// Token: 0x04003C0C RID: 15372
		public int id;

		// Token: 0x04003C0D RID: 15373
		public List<int> value1 = new List<int>();
	}
}
