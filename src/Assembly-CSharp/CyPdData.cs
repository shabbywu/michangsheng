using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;

public class CyPdData
{
	public int id;

	public List<int> npcActionList;

	public int npcType;

	public int minLevel;

	public int maxLevel;

	public int staticId;

	public int staticFuHao;

	public int staticValue;

	public int needHaoGanDu;

	public int haoGanFuHao;

	public string startTime = "";

	public string endTime = "";

	public int npcState;

	public bool isOnly;

	public int cyType;

	public int baseRate;

	public int actionId;

	public int itemPrice;

	public int outTime;

	public int addHaoGan;

	public int talkId;

	public int qingFen;

	public void SetStaticFuHao(string msg)
	{
		switch (msg)
		{
		case "=":
			staticFuHao = 1;
			break;
		case "<":
			staticFuHao = 2;
			break;
		case ">":
			staticFuHao = 3;
			break;
		}
	}

	public void SetHaoGanFuHao(string msg)
	{
		switch (msg)
		{
		case "=":
			haoGanFuHao = 1;
			break;
		case "<":
			haoGanFuHao = 2;
			break;
		case ">":
			haoGanFuHao = 3;
			break;
		}
	}

	public bool StaticValuePd()
	{
		int num = GlobalValue.Get(staticId, "CyPdData.StaticValuePd");
		if (staticFuHao == 1 && num == staticValue)
		{
			return true;
		}
		if (staticFuHao == 2 && num < staticValue)
		{
			return true;
		}
		if (staticFuHao == 3 && num > staticValue)
		{
			return true;
		}
		return false;
	}

	public bool HaoGanPd(int haogan)
	{
		if (haoGanFuHao == 1 && haogan == needHaoGanDu)
		{
			return true;
		}
		if (haoGanFuHao == 2 && haogan < needHaoGanDu)
		{
			return true;
		}
		if (haoGanFuHao == 3 && haogan > needHaoGanDu)
		{
			return true;
		}
		return false;
	}

	public bool IsinTime()
	{
		if (startTime == "")
		{
			return true;
		}
		try
		{
			DateTime dateTime = DateTime.Parse(NpcJieSuanManager.inst.JieSuanTime);
			DateTime dateTime2 = DateTime.Parse(startTime);
			DateTime dateTime3 = DateTime.Parse(endTime);
			if (dateTime >= dateTime2 && dateTime <= dateTime3)
			{
				return true;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return false;
		}
		return false;
	}

	public int GetRate(int haogan)
	{
		int num = (haogan - 40) * 100 / 150;
		if (num < 0)
		{
			num = 0;
		}
		return num + baseRate;
	}

	public List<int> GetItem(int npcId)
	{
		List<int> list = new List<int>();
		if (id >= 41 && id <= 45)
		{
			foreach (int item in CyNpcSendData.DataDict[id].RandomItemID)
			{
				if (NpcJieSuanManager.inst.npcUseItem.GetDanYaoCanUseNum(npcId, item) > 0)
				{
					list.Add(item);
					break;
				}
			}
			if (list.Count > 0)
			{
				int num = itemPrice;
				int i = jsonData.instance.ItemJsonData[list[0].ToString()]["price"].I;
				list.Add(1);
				for (num -= i; num > 0; num -= i)
				{
					list[1]++;
				}
			}
			return list;
		}
		int randomInt = NpcJieSuanManager.inst.getRandomInt(0, NpcJieSuanManager.inst.cyDictionary[id].Count - 1);
		int randomInt2 = NpcJieSuanManager.inst.getRandomInt(NpcJieSuanManager.inst.cyDictionary[id][randomInt][0], NpcJieSuanManager.inst.cyDictionary[id][randomInt][1]);
		int num2 = itemPrice;
		int i2 = jsonData.instance.ItemJsonData[randomInt2.ToString()]["price"].I;
		list.Add(randomInt2);
		list.Add(1);
		for (num2 -= i2; num2 > 0; num2 -= i2)
		{
			list[1]++;
		}
		return list;
	}
}
