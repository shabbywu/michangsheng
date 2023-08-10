using System;
using System.Collections.Generic;
using Bag;

namespace script.ExchangeMeeting.Logic.Interface;

[Serializable]
public abstract class IExchangeData
{
	[NonSerialized]
	protected static readonly Random random = new Random();

	public List<BaseItem> NeedItems = new List<BaseItem>();

	public List<BaseItem> GiveItems = new List<BaseItem>();

	public int Id;
}
