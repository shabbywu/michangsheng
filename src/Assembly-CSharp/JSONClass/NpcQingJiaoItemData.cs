using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcQingJiaoItemData : IJSONClass
{
	public static Dictionary<int, NpcQingJiaoItemData> DataDict = new Dictionary<int, NpcQingJiaoItemData>();

	public static List<NpcQingJiaoItemData> DataList = new List<NpcQingJiaoItemData>();

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
		foreach (JSONObject item in jsonData.instance.NpcQingJiaoItemData.list)
		{
			try
			{
				NpcQingJiaoItemData npcQingJiaoItemData = new NpcQingJiaoItemData();
				npcQingJiaoItemData.id = item["id"].I;
				npcQingJiaoItemData.ItemIcon = item["ItemIcon"].I;
				npcQingJiaoItemData.maxNum = item["maxNum"].I;
				npcQingJiaoItemData.TuJianType = item["TuJianType"].I;
				npcQingJiaoItemData.ShopType = item["ShopType"].I;
				npcQingJiaoItemData.WuWeiType = item["WuWeiType"].I;
				npcQingJiaoItemData.ShuXingType = item["ShuXingType"].I;
				npcQingJiaoItemData.type = item["type"].I;
				npcQingJiaoItemData.quality = item["quality"].I;
				npcQingJiaoItemData.typePinJie = item["typePinJie"].I;
				npcQingJiaoItemData.StuTime = item["StuTime"].I;
				npcQingJiaoItemData.vagueType = item["vagueType"].I;
				npcQingJiaoItemData.price = item["price"].I;
				npcQingJiaoItemData.CanSale = item["CanSale"].I;
				npcQingJiaoItemData.DanDu = item["DanDu"].I;
				npcQingJiaoItemData.CanUse = item["CanUse"].I;
				npcQingJiaoItemData.NPCCanUse = item["NPCCanUse"].I;
				npcQingJiaoItemData.yaoZhi1 = item["yaoZhi1"].I;
				npcQingJiaoItemData.yaoZhi2 = item["yaoZhi2"].I;
				npcQingJiaoItemData.yaoZhi3 = item["yaoZhi3"].I;
				npcQingJiaoItemData.ShuaXin = item["ShuaXin"].I;
				npcQingJiaoItemData.name = item["name"].Str;
				npcQingJiaoItemData.FaBaoType = item["FaBaoType"].Str;
				npcQingJiaoItemData.desc = item["desc"].Str;
				npcQingJiaoItemData.desc2 = item["desc2"].Str;
				npcQingJiaoItemData.Affix = item["Affix"].ToList();
				npcQingJiaoItemData.ItemFlag = item["ItemFlag"].ToList();
				npcQingJiaoItemData.seid = item["seid"].ToList();
				npcQingJiaoItemData.wuDao = item["wuDao"].ToList();
				if (DataDict.ContainsKey(npcQingJiaoItemData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcQingJiaoItemData.DataDict添加数据时出现重复的键，Key:{npcQingJiaoItemData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcQingJiaoItemData.id, npcQingJiaoItemData);
				DataList.Add(npcQingJiaoItemData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcQingJiaoItemData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
