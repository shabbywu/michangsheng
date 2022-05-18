using System;
using Tab;
using UnityEngine.EventSystems;

namespace Bag
{
	// Token: 0x02000D42 RID: 3394
	[Serializable]
	public class PasstiveSkillSlot : SlotBase
	{
		// Token: 0x06005095 RID: 20629 RVA: 0x0003A032 File Offset: 0x00038232
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

		// Token: 0x06005096 RID: 20630 RVA: 0x0003A070 File Offset: 0x00038270
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

		// Token: 0x040051CC RID: 20940
		public GongFaSlotType SkillSlotType;
	}
}
