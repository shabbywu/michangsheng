using Bag;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI.Base;

public class ExchangeSlot : BaseSlot
{
	public Sprite nomalSprite;

	public Sprite mouseEnterSprite;

	public Sprite mouseDownSprite;

	protected Image bg;

	public bool IsInBag;

	public override void InitUI()
	{
		if (!IsInBag)
		{
			bg = Get<Image>("Null/BG");
		}
		base.InitUI();
	}

	public override bool CanDrag()
	{
		return false;
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!IsInBag)
		{
			bg.sprite = mouseDownSprite;
		}
		base.OnPointerDown(eventData);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		IsIn = true;
		if (!IsInBag)
		{
			bg.sprite = mouseEnterSprite;
		}
		if (!IsNull())
		{
			if (IsInBag)
			{
				_selectPanel.SetActive(true);
			}
			if ((Object)(object)ToolTipsMag.Inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
			}
			ToolTipsMag.Inst.Show(Item);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Invalid comparison between Unknown and I4
		if (IsInBag)
		{
			if (!IsNull())
			{
				IExchangeUIMag.Inst.PublishCtr.PutNeedItem(Item);
				IExchangeUIMag.Inst.NeedBag.Hide();
				_selectPanel.SetActive(false);
			}
			return;
		}
		if ((int)eventData.button == 0)
		{
			IExchangeUIMag.Inst.NeedBag.Open();
		}
		else if ((int)eventData.button == 1 && !IsNull())
		{
			IExchangeUIMag.Inst.PublishCtr.BackNeedItem();
		}
		bg.sprite = nomalSprite;
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		if (!IsInBag)
		{
			bg.sprite = nomalSprite;
		}
		base.OnPointerExit(eventData);
	}
}
