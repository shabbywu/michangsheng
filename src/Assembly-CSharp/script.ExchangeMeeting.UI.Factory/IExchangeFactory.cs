using UnityEngine;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.UI.Factory;

public abstract class IExchangeFactory
{
	public abstract void Create(GameObject gameObject, Transform parent, IExchangeData data);
}
