using System;
using System.Collections.Generic;
using Bag;

namespace script.ExchangeMeeting.Logic.Interface;

public abstract class IExchangeIO
{
	protected int minRandomNum = 2;

	protected int minTotalNum = 5;

	protected readonly Random random = new Random();

	protected IExchangeSource source => PlayerEx.Player.StreamData.ExchangeSource;

	public abstract void Add(IExchangeData data);

	public abstract void Remove(IExchangeData data);

	public abstract bool NeedUpdateNpcExchange();

	public abstract List<int> GetGuDingList();

	public abstract void CreateNpcExchange();

	protected abstract List<int> GetRandomExchangeList(int count);

	public abstract void CreatePlayerExchange(List<BaseItem> needs, List<BaseItem> gets);

	public abstract List<IExchangeData> GetPlayerList();

	public abstract List<IExchangeData> GetSystemList();

	public abstract void SaveGuDingId(int id);
}
