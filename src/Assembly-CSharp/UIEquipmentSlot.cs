using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x0600040D RID: 1037 RVA: 0x000169DC File Offset: 0x00014BDC
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

	// Token: 0x0600040E RID: 1038 RVA: 0x000169FF File Offset: 0x00014BFF
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.equipment != null))
		{
			return item;
		}
		return this.equipment.Replace(this.slot, item);
	}

	// Token: 0x04000238 RID: 568
	public InvEquipment equipment;

	// Token: 0x04000239 RID: 569
	public InvBaseItem.Slot slot;
}
