namespace script.ExchangeMeeting.Logic.Interface;

public abstract class IUpdateExchange
{
	public abstract void UpdateProcess(int times);

	protected abstract void SuccessExchange(IExchangeData data);
}
