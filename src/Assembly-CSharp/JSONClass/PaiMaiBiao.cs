using System;
using System.Collections.Generic;

namespace JSONClass;

public class PaiMaiBiao : IJSONClass
{
	public static Dictionary<int, PaiMaiBiao> DataDict = new Dictionary<int, PaiMaiBiao>();

	public static List<PaiMaiBiao> DataList = new List<PaiMaiBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int PaiMaiID;

	public int ItemNum;

	public int Price;

	public int RuChangFei;

	public int circulation;

	public int paimaifenzu;

	public int jimainum;

	public int IsBuShuaXin;

	public int level;

	public string Name;

	public string ChangJing;

	public string StarTime;

	public string EndTime;

	public List<int> Type = new List<int>();

	public List<int> quality = new List<int>();

	public List<int> quanzhong1 = new List<int>();

	public List<int> guding = new List<int>();

	public List<int> quanzhong2 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PaiMaiBiao.list)
		{
			try
			{
				PaiMaiBiao paiMaiBiao = new PaiMaiBiao();
				paiMaiBiao.PaiMaiID = item["PaiMaiID"].I;
				paiMaiBiao.ItemNum = item["ItemNum"].I;
				paiMaiBiao.Price = item["Price"].I;
				paiMaiBiao.RuChangFei = item["RuChangFei"].I;
				paiMaiBiao.circulation = item["circulation"].I;
				paiMaiBiao.paimaifenzu = item["paimaifenzu"].I;
				paiMaiBiao.jimainum = item["jimainum"].I;
				paiMaiBiao.IsBuShuaXin = item["IsBuShuaXin"].I;
				paiMaiBiao.level = item["level"].I;
				paiMaiBiao.Name = item["Name"].Str;
				paiMaiBiao.ChangJing = item["ChangJing"].Str;
				paiMaiBiao.StarTime = item["StarTime"].Str;
				paiMaiBiao.EndTime = item["EndTime"].Str;
				paiMaiBiao.Type = item["Type"].ToList();
				paiMaiBiao.quality = item["quality"].ToList();
				paiMaiBiao.quanzhong1 = item["quanzhong1"].ToList();
				paiMaiBiao.guding = item["guding"].ToList();
				paiMaiBiao.quanzhong2 = item["quanzhong2"].ToList();
				if (DataDict.ContainsKey(paiMaiBiao.PaiMaiID))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PaiMaiBiao.DataDict添加数据时出现重复的键，Key:{paiMaiBiao.PaiMaiID}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(paiMaiBiao.PaiMaiID, paiMaiBiao);
				DataList.Add(paiMaiBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PaiMaiBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
