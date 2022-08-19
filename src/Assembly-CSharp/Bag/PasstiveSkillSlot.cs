using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x020009BA RID: 2490
	[Serializable]
	public class PasstiveSkillSlot : SlotBase
	{
		// Token: 0x06004532 RID: 17714 RVA: 0x001D5CE6 File Offset: 0x001D3EE6
		public override void OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag())
			{
				return;
			}
			if (!this.IsIn && !DragMag.Inst.EndDrag())
			{
				SingletonMono<TabUIMag>.Instance.GongFaPanel.RemoveSkill(this.SkillSlotType);
			}
			DragMag.Inst.Clear();
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x001D5D24 File Offset: 0x001D3F24
		public override void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.dragging)
			{
				return;
			}
			if (eventData.button == 1 && !base.IsNull())
			{
				ToolTipsMag.Inst.Close();
				SingletonMono<TabUIMag>.Instance.GongFaPanel.RemoveSkill(this.SkillSlotType);
			}
		}

		// Token: 0x040046CA RID: 18122
		public GongFaSlotType SkillSlotType;
	}
}
