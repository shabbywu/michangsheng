using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200089D RID: 2205
	[Serializable]
	public class ItemHolder
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000294E6 File Offset: 0x000276E6
		public bool HasItem
		{
			get
			{
				return this.CurrentItem != null;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000294F1 File Offset: 0x000276F1
		// (set) Token: 0x060038D4 RID: 14548 RVA: 0x000294F9 File Offset: 0x000276F9
		public SavableItem CurrentItem { get; private set; }

		// Token: 0x060038D5 RID: 14549 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static implicit operator bool(ItemHolder holder)
		{
			return holder != null;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x001A3B6C File Offset: 0x001A1D6C
		public bool TryAddItem(ItemData itemData, int amount, out int added, List<ItemProperty.Value> customPropertyValues = null, ulong _uuid = 0UL, int _index = 0)
		{
			added = 0;
			if (this.HasItem && itemData.Id != this.CurrentItem.Id)
			{
				return false;
			}
			if (!this.HasItem)
			{
				this.CurrentItem = new SavableItem(itemData, 1, customPropertyValues);
				this.CurrentItem.CurrentInStack = 0;
				this.CurrentItem.PropertyChanged.AddListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				this.CurrentItem.StackChanged.AddListener(new Action(this.On_StackChanged));
				this.uuid = _uuid;
				this.index = _index;
				this.itemID = itemData.Id;
			}
			int currentInStack = this.CurrentItem.CurrentInStack;
			int num = amount + currentInStack - itemData.StackSize;
			int num2 = currentInStack;
			if (num <= 0)
			{
				num2 += amount;
			}
			else
			{
				num2 = itemData.StackSize;
			}
			this.CurrentItem.CurrentInStack = num2;
			added = num2 - currentInStack;
			this.Updated.Send(this);
			return added > 0;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x001A3C5C File Offset: 0x001A1E5C
		public void SetItem(SavableItem item)
		{
			if (this.CurrentItem)
			{
				this.CurrentItem.PropertyChanged.RemoveListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				this.CurrentItem.StackChanged.RemoveListener(new Action(this.On_StackChanged));
			}
			this.CurrentItem = item;
			if (this.CurrentItem)
			{
				this.CurrentItem.PropertyChanged.AddListener(new Action<ItemProperty.Value>(this.On_PropertyChanged));
				this.CurrentItem.StackChanged.AddListener(new Action(this.On_StackChanged));
			}
			this.Updated.Send(this);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x001A3D08 File Offset: 0x001A1F08
		public void RemoveFromStack(int amount, out int removed)
		{
			removed = 0;
			if (!this.HasItem)
			{
				return;
			}
			if (amount >= this.CurrentItem.CurrentInStack)
			{
				removed = this.CurrentItem.CurrentInStack;
				this.SetItem(null);
				return;
			}
			int currentInStack = this.CurrentItem.CurrentInStack;
			this.CurrentItem.CurrentInStack = Mathf.Max(this.CurrentItem.CurrentInStack - amount, 0);
			removed = currentInStack - this.CurrentItem.CurrentInStack;
			this.Updated.Send(this);
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x001A3D8C File Offset: 0x001A1F8C
		public void RemoveFromStack(int amount)
		{
			if (!this.HasItem)
			{
				return;
			}
			int currentInStack = this.CurrentItem.CurrentInStack;
			this.CurrentItem.CurrentInStack = Mathf.Max(this.CurrentItem.CurrentInStack - amount, 0);
			this.Updated.Send(this);
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x00029502 File Offset: 0x00027702
		private void On_PropertyChanged(ItemProperty.Value propertyValue)
		{
			this.Updated.Send(this);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x00029502 File Offset: 0x00027702
		private void On_StackChanged()
		{
			this.Updated.Send(this);
		}

		// Token: 0x04003314 RID: 13076
		public Message<ItemHolder> Updated = new Message<ItemHolder>();

		// Token: 0x04003316 RID: 13078
		public ulong uuid;

		// Token: 0x04003317 RID: 13079
		public int index;

		// Token: 0x04003318 RID: 13080
		public int itemID;
	}
}
