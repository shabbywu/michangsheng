using System;
using System.Collections.Generic;

namespace script.ExchangeMeeting.Logic.Interface;

[Serializable]
public abstract class IExchangeSource
{
	public List<IExchangeData> playerList;

	public List<IExchangeData> sysList;

	public List<int> guDingList;

	public DateTime NextUpdate = DateTime.Parse("0001-1-1");

	public void Init()
	{
		if (playerList == null)
		{
			playerList = new List<IExchangeData>();
		}
		if (sysList == null)
		{
			sysList = new List<IExchangeData>();
		}
		if (guDingList == null)
		{
			guDingList = new List<int>();
		}
	}
}
