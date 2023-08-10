using UnityEngine;
using script.ExchangeMeeting.Logic;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Factory;

public class ViewExchangeFactory : IExchangeFactory
{
	public override void Create(GameObject gameObject, Transform parent, IExchangeData data)
	{
		GameObject gameObject2 = gameObject.Inst(parent);
		if (data is SysExchangeData)
		{
			new SysExchangeDataUI(gameObject2, data);
		}
		else if (data is PlayerExchangeData)
		{
			new PlayerExchangeDataUI(gameObject2, data);
		}
	}
}
