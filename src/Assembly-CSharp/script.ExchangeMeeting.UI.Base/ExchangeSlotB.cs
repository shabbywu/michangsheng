using UnityEngine;
using UnityEngine.EventSystems;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.Base;

public class ExchangeSlotB : ExchangeSlot
{
	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Invalid comparison between Unknown and I4
		if (IsInBag)
		{
			if (!IsNull())
			{
				if (IExchangeUIMag.Inst == null)
				{
					Debug.LogError((object)"IExchangeUIMag.Inst is null");
					return;
				}
				if ((Object)(object)IExchangeUIMag.Inst.PlayerBag.ToSlot == (Object)null)
				{
					Debug.LogError((object)"IExchangeUIMag.Inst.PlayerBag.ToSlot is null");
					return;
				}
				IExchangeUIMag.Inst.PublishCtr.PutGiveItem(this);
				IExchangeUIMag.Inst.PlayerBag.Hide();
				_selectPanel.SetActive(false);
			}
		}
		else
		{
			if ((int)eventData.button == 0)
			{
				IExchangeUIMag.Inst.PlayerBag.ToSlot = this;
				IExchangeUIMag.Inst.PlayerBag.Open();
			}
			else if ((int)eventData.button == 1 && !IsNull())
			{
				IExchangeUIMag.Inst.PublishCtr.BackGiveItem(this);
			}
			bg.sprite = nomalSprite;
		}
	}
}
