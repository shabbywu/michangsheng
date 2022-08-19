using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000893 RID: 2195
	public class NomelShopJsonData : IJSONClass
	{
		// Token: 0x0600405F RID: 16479 RVA: 0x001B77CC File Offset: 0x001B59CC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.NomelShopJsonData.list)
			{
				try
				{
					NomelShopJsonData nomelShopJsonData = new NomelShopJsonData();
					nomelShopJsonData.id = jsonobject["id"].I;
					nomelShopJsonData.threeScene = jsonobject["threeScene"].I;
					nomelShopJsonData.SType = jsonobject["SType"].I;
					nomelShopJsonData.shopType = jsonobject["shopType"].I;
					nomelShopJsonData.price = jsonobject["price"].I;
					nomelShopJsonData.ExShopID = jsonobject["ExShopID"].I;
					nomelShopJsonData.ChildTitle = jsonobject["ChildTitle"].Str;
					nomelShopJsonData.Title = jsonobject["Title"].Str;
					nomelShopJsonData.items = jsonobject["items"].ToList();
					if (NomelShopJsonData.DataDict.ContainsKey(nomelShopJsonData.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典NomelShopJsonData.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", nomelShopJsonData.id));
					}
					else
					{
						NomelShopJsonData.DataDict.Add(nomelShopJsonData.id, nomelShopJsonData);
						NomelShopJsonData.DataList.Add(nomelShopJsonData);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典NomelShopJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (NomelShopJsonData.OnInitFinishAction != null)
			{
				NomelShopJsonData.OnInitFinishAction();
			}
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x04003DB9 RID: 15801
		public static Dictionary<int, NomelShopJsonData> DataDict = new Dictionary<int, NomelShopJsonData>();

		// Token: 0x04003DBA RID: 15802
		public static List<NomelShopJsonData> DataList = new List<NomelShopJsonData>();

		// Token: 0x04003DBB RID: 15803
		public static Action OnInitFinishAction = new Action(NomelShopJsonData.OnInitFinish);

		// Token: 0x04003DBC RID: 15804
		public int id;

		// Token: 0x04003DBD RID: 15805
		public int threeScene;

		// Token: 0x04003DBE RID: 15806
		public int SType;

		// Token: 0x04003DBF RID: 15807
		public int shopType;

		// Token: 0x04003DC0 RID: 15808
		public int price;

		// Token: 0x04003DC1 RID: 15809
		public int ExShopID;

		// Token: 0x04003DC2 RID: 15810
		public string ChildTitle;

		// Token: 0x04003DC3 RID: 15811
		public string Title;

		// Token: 0x04003DC4 RID: 15812
		public List<int> items = new List<int>();
	}
}
