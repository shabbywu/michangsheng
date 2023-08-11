using System.Collections.Generic;
using Bag;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IPublishDataUI : UIBase
{
	public List<BaseSlot> GiveItems;

	public BaseSlot NeedItem;

	public int DrawMoney;

	public Text 抽成;

	public FpBtn Btn;

	protected UnityAction _action;

	protected int giveItemCount = 4;

	protected virtual void Init()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		NeedItem = Get<BaseSlot>("需求物品/1");
		NeedItem.SetNull();
		GiveItems = new List<BaseSlot>();
		for (int i = 1; i <= giveItemCount; i++)
		{
			GiveItems.Add(Get<BaseSlot>($"兑换物品/{i}"));
			GiveItems[i - 1].SetNull();
		}
		抽成 = Get<Text>("抽成/Value");
		Btn = Get<FpBtn>("按钮/按钮");
		Btn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			UnityAction action = _action;
			if (action != null)
			{
				action.Invoke();
			}
		});
	}

	public virtual void Clear()
	{
	}

	public virtual void SetClickAction(UnityAction action)
	{
		_action = action;
	}

	public virtual void UpdateUI()
	{
	}
}
