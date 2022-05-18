using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000AEB RID: 2795
	public class BackpackJsonData : IJSONClass
	{
		// Token: 0x06004716 RID: 18198 RVA: 0x001E6F94 File Offset: 0x001E5194
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

		// Token: 0x06004717 RID: 18199 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003FFA RID: 16378
		public static Dictionary<int, BackpackJsonData> DataDict = new Dictionary<int, BackpackJsonData>();

		// Token: 0x04003FFB RID: 16379
		public static List<BackpackJsonData> DataList = new List<BackpackJsonData>();

		// Token: 0x04003FFC RID: 16380
		public static Action OnInitFinishAction = new Action(BackpackJsonData.OnInitFinish);

		// Token: 0x04003FFD RID: 16381
		public int id;

		// Token: 0x04003FFE RID: 16382
		public int AvatrID;

		// Token: 0x04003FFF RID: 16383
		public int Type;

		// Token: 0x04004000 RID: 16384
		public int quality;

		// Token: 0x04004001 RID: 16385
		public int CanSell;

		// Token: 0x04004002 RID: 16386
		public int SellPercent;

		// Token: 0x04004003 RID: 16387
		public int CanDrop;

		// Token: 0x04004004 RID: 16388
		public string BackpackName;

		// Token: 0x04004005 RID: 16389
		public List<int> ItemID = new List<int>();

		// Token: 0x04004006 RID: 16390
		public List<int> randomNum = new List<int>();
	}
}
