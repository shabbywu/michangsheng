using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCChuShiShuZiDate : IJSONClass
{
	public static Dictionary<int, NPCChuShiShuZiDate> DataDict = new Dictionary<int, NPCChuShiShuZiDate>();

	public static List<NPCChuShiShuZiDate> DataList = new List<NPCChuShiShuZiDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int xiuwei;

	public int equipWeapon;

	public int equipWeapon2;

	public int equipClothing;

	public int equipRing;

	public int bag;

	public List<int> age = new List<int>();

	public List<int> shouYuan = new List<int>();

	public List<int> SexType = new List<int>();

	public List<int> HP = new List<int>();

	public List<int> ziZhi = new List<int>();

	public List<int> wuXin = new List<int>();

	public List<int> dunSu = new List<int>();

	public List<int> shengShi = new List<int>();

	public List<int> MoneyType = new List<int>();

	public List<int> ShopType = new List<int>();

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			try
			{
				NPCChuShiShuZiDate nPCChuShiShuZiDate = new NPCChuShiShuZiDate();
				nPCChuShiShuZiDate.id = item["id"].I;
				nPCChuShiShuZiDate.xiuwei = item["xiuwei"].I;
				nPCChuShiShuZiDate.equipWeapon = item["equipWeapon"].I;
				nPCChuShiShuZiDate.equipWeapon2 = item["equipWeapon2"].I;
				nPCChuShiShuZiDate.equipClothing = item["equipClothing"].I;
				nPCChuShiShuZiDate.equipRing = item["equipRing"].I;
				nPCChuShiShuZiDate.bag = item["bag"].I;
				nPCChuShiShuZiDate.age = item["age"].ToList();
				nPCChuShiShuZiDate.shouYuan = item["shouYuan"].ToList();
				nPCChuShiShuZiDate.SexType = item["SexType"].ToList();
				nPCChuShiShuZiDate.HP = item["HP"].ToList();
				nPCChuShiShuZiDate.ziZhi = item["ziZhi"].ToList();
				nPCChuShiShuZiDate.wuXin = item["wuXin"].ToList();
				nPCChuShiShuZiDate.dunSu = item["dunSu"].ToList();
				nPCChuShiShuZiDate.shengShi = item["shengShi"].ToList();
				nPCChuShiShuZiDate.MoneyType = item["MoneyType"].ToList();
				nPCChuShiShuZiDate.ShopType = item["ShopType"].ToList();
				nPCChuShiShuZiDate.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(nPCChuShiShuZiDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCChuShiShuZiDate.DataDict添加数据时出现重复的键，Key:{nPCChuShiShuZiDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCChuShiShuZiDate.id, nPCChuShiShuZiDate);
				DataList.Add(nPCChuShiShuZiDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCChuShiShuZiDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
