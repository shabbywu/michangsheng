using UnityEngine;
using script.ExchangeMeeting.UI.Base;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IExchangeUI : BasePanelB
{
	public FpBtn BackBtn;

	public GameObject SysEvent;

	public GameObject PlayerEvent;

	public Transform EventParent;
}
