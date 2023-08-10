using System;
using System.Collections.Generic;
using Bag;

namespace script.ExchangeMeeting.Logic.Interface;

public abstract class IExchangeDataFactory
{
	protected readonly Random random = new Random();

	public abstract IExchangeData Create(int id, int type);

	public abstract IExchangeData Create(List<BaseItem> needItems, List<BaseItem> giveItems);

	protected abstract int GetConditionCount();

	protected abstract Dictionary<int, Dictionary<int, int>> GetConditionDict(List<int> tags1, List<int> nums1, List<int> items1, List<int> nums2);
}
