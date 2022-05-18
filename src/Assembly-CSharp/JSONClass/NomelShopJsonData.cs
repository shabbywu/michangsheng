using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000C21 RID: 3105
	public class NomelShopJsonData : IJSONClass
	{
		// Token: 0x06004BED RID: 19437 RVA: 0x00200C14 File Offset: 0x001FEE14
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

		// Token: 0x06004BEE RID: 19438 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x04004912 RID: 18706
		public static Dictionary<int, NomelShopJsonData> DataDict = new Dictionary<int, NomelShopJsonData>();

		// Token: 0x04004913 RID: 18707
		public static List<NomelShopJsonData> DataList = new List<NomelShopJsonData>();

		// Token: 0x04004914 RID: 18708
		public static Action OnInitFinishAction = new Action(NomelShopJsonData.OnInitFinish);

		// Token: 0x04004915 RID: 18709
		public int id;

		// Token: 0x04004916 RID: 18710
		public int threeScene;

		// Token: 0x04004917 RID: 18711
		public int SType;

		// Token: 0x04004918 RID: 18712
		public int shopType;

		// Token: 0x04004919 RID: 18713
		public int price;

		// Token: 0x0400491A RID: 18714
		public int ExShopID;

		// Token: 0x0400491B RID: 18715
		public string ChildTitle;

		// Token: 0x0400491C RID: 18716
		public string Title;

		// Token: 0x0400491D RID: 18717
		public List<int> items = new List<int>();
	}
}
