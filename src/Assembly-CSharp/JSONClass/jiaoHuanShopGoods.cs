using System;
using System.Collections.Generic;

namespace JSONClass
{
	// Token: 0x02000BFB RID: 3067
	public class jiaoHuanShopGoods : IJSONClass, IComparable
	{
		// Token: 0x06004B54 RID: 19284 RVA: 0x001FCEE0 File Offset: 0x001FB0E0
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

		// Token: 0x06004B55 RID: 19285 RVA: 0x000042DD File Offset: 0x000024DD
		private static void OnInitFinish()
		{
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x001FD074 File Offset: 0x001FB274
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

		// Token: 0x040047A9 RID: 18345
		public static Dictionary<int, jiaoHuanShopGoods> DataDict = new Dictionary<int, jiaoHuanShopGoods>();

		// Token: 0x040047AA RID: 18346
		public static List<jiaoHuanShopGoods> DataList = new List<jiaoHuanShopGoods>();

		// Token: 0x040047AB RID: 18347
		public static Action OnInitFinishAction = new Action(jiaoHuanShopGoods.OnInitFinish);

		// Token: 0x040047AC RID: 18348
		public int id;

		// Token: 0x040047AD RID: 18349
		public int ShopID;

		// Token: 0x040047AE RID: 18350
		public int EXGoodsID;

		// Token: 0x040047AF RID: 18351
		public int Money;

		// Token: 0x040047B0 RID: 18352
		public int GoodsID;

		// Token: 0x040047B1 RID: 18353
		public int percent;
	}
}
