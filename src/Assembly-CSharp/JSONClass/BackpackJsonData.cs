using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000753 RID: 1875
	public class BackpackJsonData : IJSONClass
	{
		// Token: 0x06003B60 RID: 15200 RVA: 0x00198F20 File Offset: 0x00197120
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.BackpackJsonData.list)
			{
				try
				{
					BackpackJsonData backpackJsonData = new BackpackJsonData();
					backpackJsonData.id = jsonobject["id"].I;
					backpackJsonData.AvatrID = jsonobject["AvatrID"].I;
					backpackJsonData.Type = jsonobject["Type"].I;
					backpackJsonData.quality = jsonobject["quality"].I;
					backpackJsonData.CanSell = jsonobject["CanSell"].I;
					backpackJsonData.SellPercent = jsonobject["SellPercent"].I;
					backpackJsonData.CanDrop = jsonobject["CanDrop"].I;
					backpackJsonData.BackpackName = jsonobject["BackpackName"].Str;
					backpackJsonData.ItemID = jsonobject["ItemID"].ToList();
					backpackJsonData.randomNum = jsonobject["randomNum"].ToList();
					if (BackpackJsonData.DataDict.ContainsKey(backpackJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典BackpackJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", backpackJsonData.id));
					}
					else
					{
						BackpackJsonData.DataDict.Add(backpackJsonData.id, backpackJsonData);
						BackpackJsonData.DataList.Add(backpackJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典BackpackJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (BackpackJsonData.OnInitFinishAction != null)
			{
				BackpackJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003461 RID: 13409
		public static Dictionary<int, BackpackJsonData> DataDict = new Dictionary<int, BackpackJsonData>();

		// Token: 0x04003462 RID: 13410
		public static List<BackpackJsonData> DataList = new List<BackpackJsonData>();

		// Token: 0x04003463 RID: 13411
		public static Action OnInitFinishAction = new Action(BackpackJsonData.OnInitFinish);

		// Token: 0x04003464 RID: 13412
		public int id;

		// Token: 0x04003465 RID: 13413
		public int AvatrID;

		// Token: 0x04003466 RID: 13414
		public int Type;

		// Token: 0x04003467 RID: 13415
		public int quality;

		// Token: 0x04003468 RID: 13416
		public int CanSell;

		// Token: 0x04003469 RID: 13417
		public int SellPercent;

		// Token: 0x0400346A RID: 13418
		public int CanDrop;

		// Token: 0x0400346B RID: 13419
		public string BackpackName;

		// Token: 0x0400346C RID: 13420
		public List<int> ItemID = new List<int>();

		// Token: 0x0400346D RID: 13421
		public List<int> randomNum = new List<int>();
	}
}
