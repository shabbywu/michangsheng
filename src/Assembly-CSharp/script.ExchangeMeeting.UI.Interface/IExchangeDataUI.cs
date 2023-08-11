using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IExchangeDataUI : UIBase
{
	protected IExchangeData _data;

	protected abstract void Init();
}
