using Bag;
using script.ExchangeMeeting.UI.Factory;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IPublishCtr
{
	public IPublishUI UI;

	public IExchangeFactory Factory;

	public abstract void Publish();

	public abstract void CreatePlayerList();

	public abstract void UpdatePlayerList();

	public abstract void PutNeedItem(BaseItem baseItem);

	public abstract void BackNeedItem();

	public abstract void PutGiveItem(BaseSlot slot);

	public abstract void BackGiveItem(BaseSlot slot);

	public abstract bool CheckCanClickPublish();
}
