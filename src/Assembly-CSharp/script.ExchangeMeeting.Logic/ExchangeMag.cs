using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.Logic;

public class ExchangeMag : IExchangeMag
{
	public ExchangeMag()
	{
		IUpdateExchange = new UpdateExchange();
		ExchangeIO = new ExchangeIO();
		ExchangeDataFactory = new ExchangeDataFactory();
	}
}
