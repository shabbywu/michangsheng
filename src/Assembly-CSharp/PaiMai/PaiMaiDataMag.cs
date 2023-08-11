using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

namespace PaiMai;

[Serializable]
public class PaiMaiDataMag
{
	public Dictionary<int, PaiMaiData> PaiMaiDict = new Dictionary<int, PaiMaiData>();

	[NonSerialized]
	public bool IsInit;

	[NonSerialized]
	private Dictionary<int, List<int>> PaiMaiShopTypeDict;

	[NonSerialized]
	private Dictionary<int, List<int>> PaiMaiShopQualityDict;

	public PaiMaiData GetShopInfo(int paiMaiId)
	{
		if (PaiMaiDict.Count == 0)
		{
			AuToUpDate();
		}
		else if (PaiMaiDict[paiMaiId].IsJoined)
		{
			UpdateById(paiMaiId);
		}
		if (!PaiMaiDict.ContainsKey(paiMaiId))
		{
			Debug.LogError((object)$"没有拍卖会的商品数据，拍卖会Id:{paiMaiId}");
			return null;
		}
		return PaiMaiDict[paiMaiId];
	}

	private void Init()
	{
		PaiMaiShopTypeDict = new Dictionary<int, List<int>>();
		PaiMaiShopQualityDict = new Dictionary<int, List<int>>();
		foreach (PaiMaiBiao data in PaiMaiBiao.DataList)
		{
			try
			{
				if (data.Type.Count <= 0)
				{
					continue;
				}
				PaiMaiShopTypeDict.Add(data.PaiMaiID, new List<int>());
				PaiMaiShopQualityDict.Add(data.PaiMaiID, new List<int>());
				for (int i = 0; i < data.Type.Count; i++)
				{
					for (int j = 0; j < data.quanzhong1[i]; j++)
					{
						PaiMaiShopTypeDict[data.PaiMaiID].Add(data.Type[i]);
						PaiMaiShopQualityDict[data.PaiMaiID].Add(data.quality[i]);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex.Message);
				Debug.LogError((object)"初始化拍卖会失败");
				Debug.LogError((object)$"拍卖会ID：{data.PaiMaiID}");
				Debug.LogError((object)($"商品类型数目：{data.Type.Count}," + $"品阶数目：{data.quality.Count},权重数目{data.quanzhong1.Count}"));
			}
		}
		foreach (PaiMaiBiao data2 in PaiMaiBiao.DataList)
		{
			if (PaiMaiDict.Count < PaiMaiBiao.DataList.Count && !PaiMaiDict.ContainsKey(data2.PaiMaiID))
			{
				PaiMaiData paiMaiData = new PaiMaiData();
				paiMaiData.Id = data2.PaiMaiID;
				paiMaiData.IsJoined = false;
				paiMaiData.No = GetNowPaiMaiJieNum(data2.PaiMaiID);
				paiMaiData.ShopList = RandomPaiMaiShopList(paiMaiData.Id);
				if (paiMaiData.No > 0)
				{
					paiMaiData.NextUpdateTime = DateTime.Parse(PaiMaiBiao.DataDict[data2.PaiMaiID].EndTime).AddYears((paiMaiData.No - 1) * data2.circulation).AddDays(1.0);
				}
				else
				{
					paiMaiData.No = 1;
					paiMaiData.NextUpdateTime = DateTime.Parse(PaiMaiBiao.DataDict[data2.PaiMaiID].EndTime).AddDays(1.0);
				}
				PaiMaiDict.Add(paiMaiData.Id, paiMaiData);
			}
		}
	}

	public bool IsJoin(int id)
	{
		if (PaiMaiDict.ContainsKey(id))
		{
			return PaiMaiDict[id].IsJoined;
		}
		Init();
		if (PaiMaiDict.ContainsKey(id))
		{
			return PaiMaiDict[id].IsJoined;
		}
		Debug.LogError((object)$"不存在此拍卖会的Id{id}");
		return false;
	}

	public void AuToUpDate()
	{
		if (!IsInit)
		{
			Init();
			IsInit = true;
		}
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		foreach (int key in PaiMaiDict.Keys)
		{
			if (!(nowTime >= PaiMaiDict[key].NextUpdateTime) || PaiMaiBiao.DataDict[key].IsBuShuaXin != 0)
			{
				continue;
			}
			int num = (nowTime.Year - PaiMaiDict[key].NextUpdateTime.Year) / PaiMaiBiao.DataDict[key].circulation;
			if (num == 0)
			{
				UpdateById(key);
				continue;
			}
			UpdateById(key, num);
			if (nowTime > PaiMaiDict[key].NextUpdateTime)
			{
				UpdateById(key, 1);
			}
		}
	}

	public void UpdateById(int id)
	{
		if (!IsInit)
		{
			Init();
			IsInit = true;
		}
		PaiMaiDict[id].No++;
		PaiMaiDict[id].NextUpdateTime = PaiMaiDict[id].NextUpdateTime.AddYears(PaiMaiBiao.DataDict[id].circulation);
		PaiMaiDict[id].ShopList = RandomPaiMaiShopList(id);
		PaiMaiDict[id].IsJoined = false;
	}

	public void UpdateById(int id, int num)
	{
		if (!IsInit)
		{
			Init();
			IsInit = true;
		}
		PaiMaiDict[id].No += num;
		PaiMaiDict[id].NextUpdateTime = PaiMaiDict[id].NextUpdateTime.AddYears(PaiMaiBiao.DataDict[id].circulation * num);
		PaiMaiDict[id].ShopList = RandomPaiMaiShopList(id);
		PaiMaiDict[id].IsJoined = false;
	}

	public int GetNowPaiMaiJieNum(int id)
	{
		DateTime dateTime = DateTime.Parse(PaiMaiBiao.DataDict[id].StarTime);
		DateTime dateTime2 = DateTime.Parse(PaiMaiBiao.DataDict[id].EndTime);
		DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
		int result = 0;
		int circulation = PaiMaiBiao.DataDict[id].circulation;
		if (nowTime >= dateTime)
		{
			result = nowTime.Year / circulation;
			if (nowTime > dateTime2.AddYears((result - 1) * circulation))
			{
				result++;
			}
			return result;
		}
		return result;
	}

	private List<int> RandomPaiMaiShopList(int id)
	{
		List<int> list = new List<int>();
		int num = 0;
		if (PaiMaiBiao.DataDict[id].Type.Count == 0)
		{
			list.Add(PaiMaiBiao.DataDict[id].guding[0]);
		}
		else
		{
			int count = PaiMaiBiao.DataDict[id].guding.Count;
			int num2 = PaiMaiBiao.DataDict[id].ItemNum;
			int num3 = 0;
			if (count > 0)
			{
				num = PaiMaiBiao.DataDict[id].quanzhong2[0];
			}
			int num4 = 0;
			int num5 = 0;
			foreach (int item in PaiMaiBiao.DataDict[id].quanzhong1)
			{
				num3 += item;
			}
			List<int> list2 = new List<int>();
			for (int i = 0; i < PaiMaiShopQualityDict[id].Count; i++)
			{
				list2.Add(i);
			}
			while (num2 > 0)
			{
				if (count > 0 && num2 == 1)
				{
					if (Tools.instance.GetRandomInt(0, num3) <= num)
					{
						list.Add(PaiMaiBiao.DataDict[id].guding[0]);
					}
					else
					{
						if (list2.Count > 0)
						{
							num4 = Tools.instance.GetRandomInt(0, list2.Count - 1);
							list2.RemoveAt(num4);
							num5 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(PaiMaiShopTypeDict[id][num4], PaiMaiShopQualityDict[id][num4])["id"].I;
						}
						else
						{
							do
							{
								num4 = Tools.instance.GetRandomInt(0, PaiMaiShopQualityDict[id].Count - 1);
								num5 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(PaiMaiShopTypeDict[id][num4], PaiMaiShopQualityDict[id][num4])["id"].I;
							}
							while (list.Contains(num5));
						}
						list.Add(num5);
					}
				}
				else
				{
					if (list2.Count > 0)
					{
						num4 = Tools.instance.GetRandomInt(0, list2.Count - 1);
						list2.RemoveAt(num4);
						num5 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(PaiMaiShopTypeDict[id][num4], PaiMaiShopQualityDict[id][num4])["id"].I;
					}
					else
					{
						do
						{
							num4 = Tools.instance.GetRandomInt(0, PaiMaiShopQualityDict[id].Count - 1);
							num5 = FactoryManager.inst.npcFactory.GetRandomItemByShopType(PaiMaiShopTypeDict[id][num4], PaiMaiShopQualityDict[id][num4])["id"].I;
						}
						while (list.Contains(num5));
					}
					list.Add(num5);
				}
				num2--;
			}
		}
		return list;
	}
}
