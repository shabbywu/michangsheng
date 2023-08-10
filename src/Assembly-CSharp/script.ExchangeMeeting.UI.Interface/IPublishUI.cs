using UnityEngine;
using script.ExchangeMeeting.UI.Base;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IPublishUI : BasePanelB
{
	public IPublishDataUI PublishDataUI;

	public GameObject ExchangePrefab;

	public Transform ExchangeParent;

	public FpBtn BackBtn;
}
