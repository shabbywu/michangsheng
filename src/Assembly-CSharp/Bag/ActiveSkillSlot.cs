using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x02000D3E RID: 3390
	public class ActiveSkillSlot : SlotBase
	{
		// Token: 0x06005086 RID: 20614 RVA: 0x00039F6D File Offset: 0x0003816D
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			if (!this.IsIn && !DragMag.Inst.EndDrag())
			{
				SingletonMono<TabUIMag>.Instance.ShenTongPanel.RemoveSkill(this.index);
			}
			DragMag.Inst.Clear();
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x00039FAB File Offset: 0x000381AB
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (eventData.button == 1 && !base.IsNull())
			{
				ToolTipsMag.Inst.Close();
				SingletonMono<TabUIMag>.Instance.ShenTongPanel.RemoveSkill(this.index);
			}
		}

		// Token: 0x040051CA RID: 20938
		public int index;
	}
}
