using System;
using System.Collections.Generic;
using Bag;
using JSONClass;

namespace script.ExchangeMeeting.Logic.Interface;

[Serializable]
public class PlayerExchangeData : IExchangeData
{
	public int NeedTime = 1;

	public int CostMoney;

	public int HasCostTime;

	public bool NeedUpdate = true;

	public PlayerExchangeData(List<BaseItem> needItems, List<BaseItem> giveItems)
	{
		if (needItems == null || giveItems == null)
		{
			throw new Exception("needItems or giveItems is null");
		}
		if (needItems.Count == 0 || giveItems.Count == 0)
		{
			throw new Exception("needItems or giveItems is empty");
		}
		foreach (BaseItem needItem in needItems)
		{
			NeedItems.Add(needItem);
		}
		foreach (BaseItem giveItem in giveItems)
		{
			GiveItems.Add(giveItem);
		}
		Calculate();
	}

	private void Calculate()
	{
		int num = 0;
		int num2 = 0;
		foreach (BaseItem needItem in NeedItems)
		{
			if (_ItemJsonData.DataDict[needItem.Id].ItemFlag.Contains(53))
			{
				NeedUpdate = false;
			}
			num += needItem.BasePrice * needItem.Count;
		}
		foreach (BaseItem giveItem in GiveItems)
		{
			int num3 = giveItem.BasePrice * giveItem.Count;
			if (_ItemJsonData.DataDict[giveItem.Id].ItemFlag.Contains(52))
			{
				num3 = num3 * 130 / 100;
			}
			if (_ItemJsonData.DataDict[giveItem.Id].ItemFlag.Contains(53))
			{
				num3 = num3 * 130 / 100;
			}
			num2 += num3;
		}
		int num4 = num * 2 - num2;
		int num5 = IExchangeData.random.Next(900, 1101);
		NeedTime = num4 / num5;
		if (num4 % num5 != 0)
		{
			NeedTime++;
		}
		if (NeedTime <= 0)
		{
			NeedTime = 1;
		}
		CostMoney = num * 2 / 100;
	}
}
