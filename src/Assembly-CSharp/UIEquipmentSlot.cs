using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000455 RID: 1109 RVA: 0x00007CE1 File Offset: 0x00005EE1
	protected override InvGameItem observedItem
	{
		get
		{
			if (!(this.equipment != null))
			{
				return null;
			}
			return this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00007D04 File Offset: 0x00005F04
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.equipment != null))
		{
			return item;
		}
		return this.equipment.Replace(this.slot, item);
	}

	// Token: 0x0400027E RID: 638
	public InvEquipment equipment;

	// Token: 0x0400027F RID: 639
	public InvBaseItem.Slot slot;
}
