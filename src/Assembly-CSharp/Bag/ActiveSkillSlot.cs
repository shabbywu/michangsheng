using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x020009B6 RID: 2486
	public class ActiveSkillSlot : SlotBase
	{
		// Token: 0x06004523 RID: 17699 RVA: 0x001D5B4A File Offset: 0x001D3D4A
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

		// Token: 0x06004524 RID: 17700 RVA: 0x001D5B88 File Offset: 0x001D3D88
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

		// Token: 0x040046C8 RID: 18120
		public int index;
	}
}
