using UnityEngine;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.UI;

public class PublishUI : IPublishUI
{
	private int giveItemCount = 4;

	public PublishUI(GameObject gameObject)
	{
		_go = gameObject;
		PublishDataUI = new PublishDataUI(Get("发布"));
		ExchangePrefab = Get("任务列表/Viewport/Content/已发布");
		ExchangeParent = Get("任务列表/Viewport/Content/").transform;
		BackBtn = Get<FpBtn>("返回交易按钮");
	}

	public override void Hide()
	{
		PublishDataUI?.Clear();
		base.Hide();
	}
}
