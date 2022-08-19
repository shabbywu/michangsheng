using System;
using Bag;
using UnityEngine.EventSystems;

// Token: 0x020002B0 RID: 688
public class UITianJieSkillSlot : SlotBase
{
	// Token: 0x06001849 RID: 6217 RVA: 0x000A98C2 File Offset: 0x000A7AC2
	public void SetSelected(bool value)
	{
		this.IsSelected = value;
		this._selectPanel.SetActive(value);
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000A98D8 File Offset: 0x000A7AD8
	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (DragMag.Inst.IsDraging)
		{
			DragMag.Inst.ToSlot = this;
		}
		if (this.SlotType == SlotType.空)
		{
			return;
		}
		this.IsIn = true;
		if (this.TianJieSkill.IsLingWu && !eventData.dragging)
		{
			if (ToolTipsMag.Inst == null)
			{
				ResManager.inst.LoadPrefab("ToolTips").Inst(NewUICanvas.Inst.transform);
			}
			ToolTipsMag.Inst.Show(this.TianJieSkill.BindSkill);
		}
		this._selectPanel.SetActive(true);
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x000A996E File Offset: 0x000A7B6E
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (this.IsSelected)
		{
			this._selectPanel.SetActive(true);
		}
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x000A998C File Offset: 0x000A7B8C
	public override void OnPointerUp(PointerEventData eventData)
	{
		if (!this.IsSelected)
		{
			this._selectPanel.SetActive(false);
		}
		if (eventData.button == null && this.OnLiftClick != null)
		{
			this.OnLiftClick(this);
		}
		if (eventData.button == 1 && this.OnRightClick != null)
		{
			this.OnRightClick(this);
		}
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000A99E8 File Offset: 0x000A7BE8
	public override void OnEndDrag(PointerEventData eventData)
	{
		if (!this.CanDrag())
		{
			return;
		}
		bool flag = DragMag.Inst.EndDrag();
		if (this.IsEquipSlot && !flag)
		{
			UIDuJieZhunBei.Inst.ClearTiaoZhengSlotByID(this.TianJieSkill.MiShu.id);
			UIDuJieZhunBei.Inst.SaveTiaoZheng();
		}
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x000A9A38 File Offset: 0x000A7C38
	public override bool CanDrag()
	{
		return !base.IsNull() && base.CanDrag();
	}

	// Token: 0x04001361 RID: 4961
	public Action<UITianJieSkillSlot> OnLiftClick;

	// Token: 0x04001362 RID: 4962
	public new Action<UITianJieSkillSlot> OnRightClick;

	// Token: 0x04001363 RID: 4963
	public bool IsSelected;

	// Token: 0x04001364 RID: 4964
	public bool IsEquipSlot;
}
