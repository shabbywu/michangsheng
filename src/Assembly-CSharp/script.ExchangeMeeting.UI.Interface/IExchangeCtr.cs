using script.ExchangeMeeting.UI.Factory;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IExchangeCtr
{
	public IExchangeUI UI;

	public IExchangeFactory Factory;

	public SysExchangeDataUI ClickDataUI;

	public abstract void UpdateEventList();
}
