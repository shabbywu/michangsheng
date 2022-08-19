using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x0200086D RID: 2157
	public class jiaoHuanShopGoods : IJSONClass, IComparable
	{
		// Token: 0x06003FC6 RID: 16326 RVA: 0x001B33BC File Offset: 0x001B15BC
		public static void InitDataDict()
		{
			foreach (JSONObject jsonobject in jsonData.instance.jiaoHuanShopGoods.list)
			{
				try
				{
					jiaoHuanShopGoods jiaoHuanShopGoods = new jiaoHuanShopGoods();
					jiaoHuanShopGoods.id = jsonobject["id"].I;
					jiaoHuanShopGoods.ShopID = jsonobject["ShopID"].I;
					jiaoHuanShopGoods.EXGoodsID = jsonobject["EXGoodsID"].I;
					jiaoHuanShopGoods.Money = jsonobject["Money"].I;
					jiaoHuanShopGoods.GoodsID = jsonobject["GoodsID"].I;
					jiaoHuanShopGoods.percent = jsonobject["percent"].I;
					if (jiaoHuanShopGoods.DataDict.ContainsKey(jiaoHuanShopGoods.id))
					{
						PreloadManager.LogException(string.Format("!!!错误!!!向字典jiaoHuanShopGoods.DataDict添加数据时出现重复的键，Key:{0}，已跳过，请检查配表", jiaoHuanShopGoods.id));
					}
					else
					{
						jiaoHuanShopGoods.DataDict.Add(jiaoHuanShopGoods.id, jiaoHuanShopGoods);
						jiaoHuanShopGoods.DataList.Add(jiaoHuanShopGoods);
					}
				}
				catch (Exception arg)
				{
					PreloadManager.LogException("!!!错误!!!向字典jiaoHuanShopGoods.DataDict添加数据时出现异常，已跳过，请检查配表");
					PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
					PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
				}
			}
			if (jiaoHuanShopGoods.OnInitFinishAction != null)
			{
				jiaoHuanShopGoods.OnInitFinishAction();
			}
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x00004095 File Offset: 0x00002295
		private static void OnInitFinish()
		{
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x001B3550 File Offset: 0x001B1750
		public int CompareTo(object obj)
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[this.GoodsID];
			jiaoHuanShopGoods jiaoHuanShopGoods = obj as jiaoHuanShopGoods;
			_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[jiaoHuanShopGoods.GoodsID];
			int num = itemJsonData.type.CompareTo(itemJsonData2.type);
			if (num == 0)
			{
				return itemJsonData.quality.CompareTo(itemJsonData2.quality);
			}
			return -num;
		}

		// Token: 0x04003C50 RID: 15440
		public static Dictionary<int, jiaoHuanShopGoods> DataDict = new Dictionary<int, jiaoHuanShopGoods>();

		// Token: 0x04003C51 RID: 15441
		public static List<jiaoHuanShopGoods> DataList = new List<jiaoHuanShopGoods>();

		// Token: 0x04003C52 RID: 15442
		public static Action OnInitFinishAction = new Action(jiaoHuanShopGoods.OnInitFinish);

		// Token: 0x04003C53 RID: 15443
		public int id;

		// Token: 0x04003C54 RID: 15444
		public int ShopID;

		// Token: 0x04003C55 RID: 15445
		public int EXGoodsID;

		// Token: 0x04003C56 RID: 15446
		public int Money;

		// Token: 0x04003C57 RID: 15447
		public int GoodsID;

		// Token: 0x04003C58 RID: 15448
		public int percent;
	}
}
