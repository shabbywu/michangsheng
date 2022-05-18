using System;
using Bag;
using UnityEngine.EventSystems;

// Token: 0x020003E8 RID: 1000
public class UITianJieSkillSlot : SlotBase
{
	// Token: 0x06001B3B RID: 6971 RVA: 0x00017040 File Offset: 0x00015240
	public void SetSelected(bool value)
	{
		this.IsSelected = value;
		this._selectPanel.SetActive(value);
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x000F06CC File Offset: 0x000EE8CC
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

	// Token: 0x06001B3D RID: 6973 RVA: 0x00017055 File Offset: 0x00015255
	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if (this.IsSelected)
		{
			this._selectPanel.SetActive(true);
		}
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x000F0764 File Offset: 0x000EE964
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

	// Token: 0x06001B3F RID: 6975 RVA: 0x000F07C0 File Offset: 0x000EE9C0
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

	// Token: 0x06001B40 RID: 6976 RVA: 0x00017072 File Offset: 0x00015272
	public override bool CanDrag()
	{
		return !base.IsNull() && base.CanDrag();
	}

	// Token: 0x040016FE RID: 5886
	public Action<UITianJieSkillSlot> OnLiftClick;

	// Token: 0x040016FF RID: 5887
	public new Action<UITianJieSkillSlot> OnRightClick;

	// Token: 0x04001700 RID: 5888
	public bool IsSelected;

	// Token: 0x04001701 RID: 5889
	public bool IsEquipSlot;
}
