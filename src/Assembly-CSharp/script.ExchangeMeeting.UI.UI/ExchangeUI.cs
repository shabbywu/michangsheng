using UnityEngine;
using UnityEngine.Events;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.UI;

public class ExchangeUI : IExchangeUI
{
	public UnityAction UpdateAction;

	public ExchangeUI(GameObject gameObject, UnityAction action)
	{
		_go = gameObject;
		UpdateAction = action;
		BackBtn = Get<FpBtn>("寄换物品按钮");
		SysEvent = Get("事件列表/Mask/Content/系统事件");
		PlayerEvent = Get("事件列表/Mask/Content/玩家事件");
		EventParent = Get("事件列表/Mask/Content").transform;
	}

	public override void Show()
	{
		UnityAction updateAction = UpdateAction;
		if (updateAction != null)
		{
			updateAction.Invoke();
		}
		base.Show();
	}
}
