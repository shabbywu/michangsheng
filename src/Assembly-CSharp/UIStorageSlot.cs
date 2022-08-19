using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x00016FF5 File Offset: 0x000151F5
	protected override InvGameItem observedItem
	{
		get
		{
			if (!(this.storage != null))
			{
				return null;
			}
			return this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00017018 File Offset: 0x00015218
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.storage != null))
		{
			return item;
		}
		return this.storage.Replace(this.slot, item);
	}

	// Token: 0x0400024B RID: 587
	public UIItemStorage storage;

	// Token: 0x0400024C RID: 588
	public int slot;
}
