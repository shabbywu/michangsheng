using UnityEngine;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Factory;

public class PlayerExchangeFactory : IExchangeFactory
{
	public override void Create(GameObject gameObject, Transform parent, IExchangeData data)
	{
		new PublishingDataUI(gameObject.Inst(parent), data);
	}
}
