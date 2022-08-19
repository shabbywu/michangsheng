using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000854 RID: 2132
	public class ItemsSeidJsonData17 : IJSONClass
	{
		// Token: 0x06003F62 RID: 16226 RVA: 0x001B1048 File Offset: 0x001AF248
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.ItemsSeidJsonData[17].list)
			{
				try
				{
					ItemsSeidJsonData17 itemsSeidJsonData = new ItemsSeidJsonData17();
					itemsSeidJsonData.id = jsonobject["id"].I;
					itemsSeidJsonData.value1 = jsonobject["value1"].ToList();
					if (ItemsSeidJsonData17.DataDict.ContainsKey(itemsSeidJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典ItemsSeidJsonData17.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", itemsSeidJsonData.id));
					}
					else
					{
						ItemsSeidJsonData17.DataDict.Add(itemsSeidJsonData.id, itemsSeidJsonData);
						ItemsSeidJsonData17.DataList.Add(itemsSeidJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典ItemsSeidJsonData17.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (ItemsSeidJsonData17.OnInitFinishAction != null)
			{
				ItemsSeidJsonData17.OnInitFinishAction();
			}
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003BAD RID: 15277
		public static int SEIDID = 17;

		// Token: 0x04003BAE RID: 15278
		public static Dictionary<int, ItemsSeidJsonData17> DataDict = new Dictionary<int, ItemsSeidJsonData17>();

		// Token: 0x04003BAF RID: 15279
		public static List<ItemsSeidJsonData17> DataList = new List<ItemsSeidJsonData17>();

		// Token: 0x04003BB0 RID: 15280
		public static Action OnInitFinishAction = new Action(ItemsSeidJsonData17.OnInitFinish);

		// Token: 0x04003BB1 RID: 15281
		public int id;

		// Token: 0x04003BB2 RID: 15282
		public List<int> value1 = new List<int>();
	}
}
