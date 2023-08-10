using System;

namespace script.ExchangeMeeting.Logic.Interface;

public abstract class IExchangeMag
{
	private static IExchangeMag _inst;

	public readonly Random random = new Random();

	public IExchangeIO ExchangeIO;

	public IUpdateExchange IUpdateExchange;

	public IExchangeDataFactory ExchangeDataFactory;

	public static IExchangeMag Inst
	{
		get
		{
			if (_inst == null)
			{
				_inst = new ExchangeMag();
			}
			return _inst;
		}
	}
}
