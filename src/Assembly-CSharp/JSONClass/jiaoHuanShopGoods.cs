using System;
using System.Collections.Generic;

namespace JSONClass;

public class jiaoHuanShopGoods : IJSONClass, IComparable
{
	public static Dictionary<int, jiaoHuanShopGoods> DataDict = new Dictionary<int, jiaoHuanShopGoods>();

	public static List<jiaoHuanShopGoods> DataList = new List<jiaoHuanShopGoods>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ShopID;

	public int EXGoodsID;

	public int Money;

	public int GoodsID;

	public int percent;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.jiaoHuanShopGoods.list)
		{
			try
			{
				jiaoHuanShopGoods jiaoHuanShopGoods2 = new jiaoHuanShopGoods();
				jiaoHuanShopGoods2.id = item["id"].I;
				jiaoHuanShopGoods2.ShopID = item["ShopID"].I;
				jiaoHuanShopGoods2.EXGoodsID = item["EXGoodsID"].I;
				jiaoHuanShopGoods2.Money = item["Money"].I;
				jiaoHuanShopGoods2.GoodsID = item["GoodsID"].I;
				jiaoHuanShopGoods2.percent = item["percent"].I;
				if (DataDict.ContainsKey(jiaoHuanShopGoods2.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典jiaoHuanShopGoods.DataDict添加数据时出现重复的键，Key:{jiaoHuanShopGoods2.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(jiaoHuanShopGoods2.id, jiaoHuanShopGoods2);
				DataList.Add(jiaoHuanShopGoods2);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典jiaoHuanShopGoods.DataDict添加数据时出现异常，已跳过，请检查配表");
				PreloadManager.LogException($"异常信息:\n{arg}");
				PreloadManager.LogException($"数据序列化:\n{item}");
			}
		}
		if (OnInitFinishAction != null)
		{
			OnInitFinishAction();
		}
	}

	private static void OnInitFinish()
	{
	}

	public int CompareTo(object obj)
	{
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[GoodsID];
		jiaoHuanShopGoods jiaoHuanShopGoods2 = obj as jiaoHuanShopGoods;
		_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[jiaoHuanShopGoods2.GoodsID];
		int num = itemJsonData.type.CompareTo(itemJsonData2.type);
		if (num == 0)
		{
			return itemJsonData.quality.CompareTo(itemJsonData2.quality);
		}
		return -num;
	}
}
