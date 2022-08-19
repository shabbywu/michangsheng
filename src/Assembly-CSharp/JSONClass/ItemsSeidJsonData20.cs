using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000856 RID: 2134
	public class ItemsSeidJsonData20 : IJSONClass
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x001B130C File Offset: 0x001AF50C
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[20].list)
			{
				try
				{
					ItemsSeidJsonData20 itemsSeidJsonData = new ItemsSeidJsonData20();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					itemsSeidJsonData.value2 = jsonobject["value2"].ToList();
					if (ItemsSeidJsonData20.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData20.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData20.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData20.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData20.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData20.OnInitFinishAction != null)
			{
				ItemsSeidJsonData20.OnInitFinishAction();
			}
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BB9 RID: 15289
		public static int SEIDID = 20;

		// Token: 0x04003BBA RID: 15290
		public static Dictionary<int, ItemsSeidJsonData20> DataDict = new Dictionary<int, ItemsSeidJsonData20>();

		// Token: 0x04003BBB RID: 15291
		public static List<ItemsSeidJsonData20> DataList = new List<ItemsSeidJsonData20>();

		// Token: 0x04003BBC RID: 15292
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData20.OnInitFinish);

		// Token: 0x04003BBD RID: 15293
		public int id;

		// Token: 0x04003BBE RID: 15294
		public List<int> value1 = new List<int>();

		// Token: 0x04003BBF RID: 15295
		public List<int> value2 = new List<int>();
	}
}
