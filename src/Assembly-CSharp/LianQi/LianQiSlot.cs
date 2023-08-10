using Bag;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LianQi;

public class LianQiSlot : SlotBase
{
	public bool IsPlayer;

	public bool IsInBag;

	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (SlotType == SlotType.空)
		{
			return;
		}
		IsIn = true;
		if (!eventData.dragging)
		{
			if ((Object)(object)ToolTipsMag.Inst == (Object)null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(((Component)NewUICanvas.Inst).transform);
			}
			ToolTipsMag.Inst.Show(Item, IsPlayer);
		}
		_selectPanel.SetActive(true);
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Invalid comparison between Unknown and I4
		if (eventData.dragging || (IsNull() && IsInBag))
		{
			return;
		}
		if (IsInBag)
		{
			LianQiTotalManager.inst.PutItem(this);
		}
		else if ((int)eventData.button == 0)
		{
			LianQiTotalManager.inst.ToSlot = this;
			LianQiTotalManager.inst.OpenBag();
		}
		else if ((int)eventData.button == 1)
		{
			if (IsNull())
			{
				return;
			}
			LianQiTotalManager.inst.Bag.AddTempItem(Item.Clone(), 1);
			SetNull();
			LianQiTotalManager.inst.putCaiLiaoCallBack();
		}
		_selectPanel.SetActive(false);
	}

	public override bool CanDrag()
	{
		return false;
	}
}
