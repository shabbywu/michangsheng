using System;
using System.Collections.Generic;

namespace JSONClass;

public class _ItemJsonData : IJSONClass
{
	public static Dictionary<int, _ItemJsonData> DataDict = new Dictionary<int, _ItemJsonData>();

	public static List<_ItemJsonData> DataList = new List<_ItemJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int ItemIcon;

	public int maxNum;

	public int TuJianType;

	public int ShopType;

	public int WuWeiType;

	public int ShuXingType;

	public int type;

	public int quality;

	public int typePinJie;

	public int StuTime;

	public int vagueType;

	public int price;

	public int CanSale;

	public int DanDu;

	public int CanUse;

	public int NPCCanUse;

	public int yaoZhi1;

	public int yaoZhi2;

	public int yaoZhi3;

	public int ShuaXin;

	public string name;

	public string FaBaoType;

	public string desc;

	public string desc2;

	public List<int> Affix = new List<int>();

	public List<int> ItemFlag = new List<int>();

	public List<int> seid = new List<int>();

	public List<int> wuDao = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._ItemJsonData.list)
		{
			try
			{
				_ItemJsonData itemJsonData = new _ItemJsonData();
				itemJsonData.id = item["id"].I;
				itemJsonData.ItemIcon = item["ItemIcon"].I;
				itemJsonData.maxNum = item["maxNum"].I;
				itemJsonData.TuJianType = item["TuJianType"].I;
				itemJsonData.ShopType = item["ShopType"].I;
				itemJsonData.WuWeiType = item["WuWeiType"].I;
				itemJsonData.ShuXingType = item["ShuXingType"].I;
				itemJsonData.type = item["type"].I;
				itemJsonData.quality = item["quality"].I;
				itemJsonData.typePinJie = item["typePinJie"].I;
				itemJsonData.StuTime = item["StuTime"].I;
				itemJsonData.vagueType = item["vagueType"].I;
				itemJsonData.price = item["price"].I;
				itemJsonData.CanSale = item["CanSale"].I;
				itemJsonData.DanDu = item["DanDu"].I;
				itemJsonData.CanUse = item["CanUse"].I;
				itemJsonData.NPCCanUse = item["NPCCanUse"].I;
				itemJsonData.yaoZhi1 = item["yaoZhi1"].I;
				itemJsonData.yaoZhi2 = item["yaoZhi2"].I;
				itemJsonData.yaoZhi3 = item["yaoZhi3"].I;
				itemJsonData.ShuaXin = item["ShuaXin"].I;
				itemJsonData.name = item["name"].Str;
				itemJsonData.FaBaoType = item["FaBaoType"].Str;
				itemJsonData.desc = item["desc"].Str;
				itemJsonData.desc2 = item["desc2"].Str;
				itemJsonData.Affix = item["Affix"].ToList();
				itemJsonData.ItemFlag = item["ItemFlag"].ToList();
				itemJsonData.seid = item["seid"].ToList();
				itemJsonData.wuDao = item["wuDao"].ToList();
				if (DataDict.ContainsKey(itemJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_ItemJsonData.DataDict添加数据时出现重复的键，Key:{itemJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(itemJsonData.id, itemJsonData);
				DataList.Add(itemJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_ItemJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
}
