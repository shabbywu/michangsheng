using System;
using System.Collections.Generic;
using Bag;
using Boo.Lang.Runtime;
using JSONClass;
using UnityEngine;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.Logic;

public class ExchangeDataFactory : IExchangeDataFactory
{
	public override IExchangeData Create(int id, int type)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		List<BaseItem> list = new List<BaseItem>();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		List<BaseItem> list2 = new List<BaseItem>();
		Dictionary<int, Dictionary<int, int>> conditionDict;
		switch (type)
		{
		case 1:
		{
			if (!RandomExchangeData.DataDict.ContainsKey(id))
			{
				throw new RuntimeException($"在随机易物表中不存在Id：{id}");
			}
			RandomExchangeData randomExchangeData = RandomExchangeData.DataDict[id];
			if (randomExchangeData.YiWuFlag.Count != randomExchangeData.NumFlag.Count)
			{
				throw new RuntimeException($"YiWuFlag和NumFlag数量不一致，请检查随机易物表Id为：{id}");
			}
			if (randomExchangeData.YiWuItem.Count != randomExchangeData.NumItem.Count)
			{
				throw new RuntimeException($"YiWuItem和NumItem数量不一致，请检查随机易物表Id为：{id}");
			}
			conditionDict = GetConditionDict(randomExchangeData.YiWuFlag, randomExchangeData.NumFlag, randomExchangeData.YiWuItem, randomExchangeData.NumItem);
			list2.Add(BaseItem.Create(randomExchangeData.ItemID, 1, Tools.getUUID(), Tools.CreateItemSeid(randomExchangeData.ItemID)));
			break;
		}
		case 2:
		{
			if (!GuDingExchangeData.DataDict.ContainsKey(id))
			{
				throw new RuntimeException($"在随机易物表中不存在Id：{id}");
			}
			GuDingExchangeData guDingExchangeData = GuDingExchangeData.DataDict[id];
			if (guDingExchangeData.YiWuFlag.Count != guDingExchangeData.NumFlag.Count)
			{
				throw new RuntimeException($"YiWuFlag和NumFlag数量不一致，请检查固定易物表Id为：{id}");
			}
			if (guDingExchangeData.YiWuItem.Count != guDingExchangeData.NumItem.Count)
			{
				throw new RuntimeException($"YiWuItem和NumItem数量不一致，请检查固定易物表Id为：{id}");
			}
			conditionDict = GetConditionDict(guDingExchangeData.YiWuFlag, guDingExchangeData.NumFlag, guDingExchangeData.YiWuItem, guDingExchangeData.NumItem);
			list2.Add(BaseItem.Create(guDingExchangeData.ItemID, 1, Tools.getUUID(), Tools.CreateItemSeid(guDingExchangeData.ItemID)));
			break;
		}
		default:
			throw new RuntimeException($"type类型错误：{type}");
		}
		foreach (int key in conditionDict.Keys)
		{
			if (key == 1)
			{
				foreach (int key2 in conditionDict[key].Keys)
				{
					dictionary.Add(key2, conditionDict[key][key2]);
				}
				continue;
			}
			foreach (int key3 in conditionDict[key].Keys)
			{
				list.Add(BaseItem.Create(key3, conditionDict[key][key3], Tools.getUUID(), Tools.CreateItemSeid(key3)));
			}
		}
		return new SysExchangeData(list, dictionary, list2, id, type == 2);
	}

	public override IExchangeData Create(List<BaseItem> needItems, List<BaseItem> giveItems)
	{
		return new PlayerExchangeData(needItems, giveItems);
	}

	protected override int GetConditionCount()
	{
		return random.Next(2, 4);
	}

	protected override Dictionary<int, Dictionary<int, int>> GetConditionDict(List<int> tags1, List<int> nums1, List<int> items1, List<int> nums2)
	{
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<int, Dictionary<int, int>> dictionary = new Dictionary<int, Dictionary<int, int>>();
		int num = 0;
		List<int> list = new List<int>(tags1);
		List<int> list2 = new List<int>(nums1);
		List<int> list3 = new List<int>(items1);
		List<int> list4 = new List<int>(nums2);
		dictionary.Add(1, new Dictionary<int, int>());
		dictionary.Add(2, new Dictionary<int, int>());
		int num2 = 0;
		int num3 = 0;
		int num4 = tags1.Count + items1.Count;
		num3 = GetConditionCount();
		if (num3 > num4)
		{
			num3 = num4;
		}
		try
		{
			for (int i = 0; i < num3; i++)
			{
				num = ((list.Count > 0 && list3.Count > 0) ? ((num3 == 2) ? 1 : 2) : ((list.Count > 0) ? 1 : 2));
				if (num == 1)
				{
					num2 = random.Next(0, list.Count);
					dictionary[num].Add(list[num2], list2[num2]);
					list.RemoveAt(num2);
					list2.RemoveAt(num2);
				}
				else
				{
					num2 = random.Next(0, list3.Count);
					dictionary[num].Add(list3[num2], list4[num2]);
					list3.RemoveAt(num2);
					list4.RemoveAt(num2);
				}
			}
			return dictionary;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			throw new RuntimeException("GetConditionDict运行出错");
		}
	}
}
