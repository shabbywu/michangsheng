using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BF1 RID: 3057
	public class ItemsSeidJsonData34 : IJSONClass
	{
		// Token: 0x06004B2C RID: 19244 RVA: 0x001FC24C File Offset: 0x001FA44C
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

		// Token: 0x06004B2D RID: 19245 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004766 RID: 18278
		public static int SEIDID = 34;

		// Token: 0x04004767 RID: 18279
		public static Dictionary<int, ItemsSeidJsonData34> DataDict = new Dictionary<int, ItemsSeidJsonData34>();

		// Token: 0x04004768 RID: 18280
		public static List<ItemsSeidJsonData34> DataList = new List<ItemsSeidJsonData34>();

		// Token: 0x04004769 RID: 18281
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData34.OnInitFinish);

		// Token: 0x0400476A RID: 18282
		public int id;

		// Token: 0x0400476B RID: 18283
		public List<int> value1 = new List<int>();
	}
}
