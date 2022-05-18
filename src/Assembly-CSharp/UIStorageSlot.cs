using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000466 RID: 1126 RVA: 0x00007EA1 File Offset: 0x000060A1
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

	// Token: 0x06000467 RID: 1127 RVA: 0x00007EC4 File Offset: 0x000060C4
	protected override InvGameItem Replace(InvGameItem item)
	{
		if (!(this.storage != null))
		{
			return item;
		}
		return this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000291 RID: 657
	public UIItemStorage storage;

	// Token: 0x04000292 RID: 658
	public int slot;
}
