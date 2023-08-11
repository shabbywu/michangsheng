using Bag;
using UnityEngine;
using UnityEngine.EventSystems;
using script.ExchangeMeeting.UI.Interface;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Base;

public class ExchangeSlotC : ExchangeSlot
{
	public SysExchangeDataUI UI;

	public void Init(SysExchangeDataUI ui)
	{
		UI = ui;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Invalid comparison between Unknown and I4
		if (IsInBag)
		{
			if (IsNull())
			{
				return;
			}
			if (IExchangeUIMag.Inst == null)
			{
				Debug.LogError((object)"IExchangeUIMag.Inst is null");
				return;
			}
			if ((Object)(object)IExchangeUIMag.Inst.SubmitBag.ToSlot == (Object)null)
			{
				Debug.LogError((object)"IExchangeUIMag.Inst.SubmitBag.ToSlot is null");
				return;
			}
			if (IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI == null)
			{
				Debug.LogError((object)"IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI is null");
				return;
			}
			int putNum = IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI.GetPutNum(Item);
			if (putNum > 0 && PlayerEx.Player.getItemNum(Item.Id) >= putNum)
			{
				BaseItem baseItem = Item.Clone();
				baseItem.Count = putNum;
				IExchangeUIMag.Inst.SubmitBag.ToSlot.SetSlotData(baseItem);
				IExchangeUIMag.Inst.SubmitBag.RemoveTempItem(baseItem.Uid, putNum);
				IExchangeUIMag.Inst.SubmitBag.Hide();
				IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI.UpdateCanSubmit();
			}
			else
			{
				UIPopTip.Inst.Pop("物品数量不足，请重新放入");
				Debug.LogError((object)"物品数量异常");
			}
			_selectPanel.SetActive(false);
		}
		else
		{
			if ((int)eventData.button == 0)
			{
				IExchangeUIMag.Inst.SubmitBag.ToSlot = this;
				IExchangeUIMag.Inst.SubmitBag.FiddlerAction = UI.IsNeedItem;
				IExchangeUIMag.Inst.SubmitBag.Open();
				IExchangeUIMag.Inst.ExchangeCtr.ClickDataUI = UI;
			}
			else if ((int)eventData.button == 1 && !IsNull())
			{
				IExchangeUIMag.Inst.SubmitBag.RemoveTempItem(Item.Uid, Item.Count);
				SetNull();
				UI.UpdateCanSubmit();
			}
			bg.sprite = nomalSprite;
		}
	}
}
