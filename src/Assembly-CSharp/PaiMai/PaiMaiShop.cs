using System;
using System.Collections.Generic;
using JSONClass;

namespace PaiMai;

[Serializable]
public class PaiMaiShop : IComparable<PaiMaiShop>
{
	public int ShopId;

	public int Count;

	public int Price;

	public int CurPrice;

	public int MinAddPrice;

	public int MayPrice;

	public bool IsPlayer;

	public string ShopName;

	public int Type;

	public int BaseQuality;

	public int Level;

	public List<int> TagList;

	public PaiMaiAvatar Owner;

	public JSONObject Seid;

	public int Quality { get; private set; }

	public void Init()
	{
		TagList = new List<int>();
		if (Seid.HasField("ItemFlag"))
		{
			TagList = Seid["ItemFlag"].ToList();
		}
		else
		{
			foreach (int item in _ItemJsonData.DataDict[ShopId].ItemFlag)
			{
				TagList.Add(item);
			}
		}
		if (Seid.HasField("Money"))
		{
			Price = Seid["Money"].I * Count;
		}
		else
		{
			Price *= Count;
		}
		if (Seid.HasField("Name"))
		{
			ShopName = Seid["Name"].Str;
		}
		else
		{
			ShopName = _ItemJsonData.DataDict[ShopId].name;
		}
		CurPrice = (int)((float)Price * 0.3f);
		Type = _ItemJsonData.DataDict[ShopId].type;
		if (Seid.HasField("quality"))
		{
			Quality = Seid["quality"].I;
		}
		else
		{
			Quality = _ItemJsonData.DataDict[ShopId].quality;
		}
		BaseQuality = Quality;
		if (Type == 1 || Type == 2)
		{
			Quality++;
		}
		else if (Type == 3 || Type == 4)
		{
			Quality *= 2;
		}
		MinAddPrice = (int)((double)Price * 0.02);
		if (MinAddPrice == 0)
		{
			MinAddPrice = 1;
		}
		Level = -1;
		if (TagList.Contains(50))
		{
			Level = 0;
		}
		else if (TagList.Contains(51))
		{
			Level = 1;
		}
		else if (TagList.Contains(52))
		{
			Level = 2;
		}
		if (Level >= 0)
		{
			MayPrice = Price + (int)((float)Price * 0.01f * (float)PaiMaiPanDing.DataDict[21 + Level].JiaWei);
		}
		else
		{
			MayPrice = Price;
		}
	}

	public int CompareTo(PaiMaiShop other)
	{
		return Price.CompareTo(other.Price);
	}
}
